using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mitchell1.Catalog.Framework.Common;
using Mitchell1.Catalog.Framework.Interfaces;
using Mitchell1.Online.Catalog.Host.Controllers;
using Mitchell1.Online.Catalog.Host.TransferObjects;
using Newtonsoft.Json.Linq;
using static Mitchell1.Online.Catalog.Host.API.v1.ParseHelper;

namespace Mitchell1.Online.Catalog.Host.API.v1
{
	public class CatalogRestApiV1 : ICatalogRestController
    {
        private readonly OnlineCatalogInformation onlineCatalogInformation;
        private readonly Logger logger;
        private readonly NewCatalogHostingForm newCatalogHostingForm;
        private readonly ShowCatalogWaitForm showCatalogWaitForm;

        public CatalogRestApiV1(OnlineCatalogInformation onlineCatalogInformation, Logger logger, NewCatalogHostingForm newCatalogHostingForm)
        {
            this.onlineCatalogInformation = onlineCatalogInformation;
            this.logger = logger;
            this.newCatalogHostingForm = newCatalogHostingForm ?? CatalogHostingForm.New;
            showCatalogWaitForm = new ShowCatalogWaitForm(onlineCatalogInformation);
        }

        public void Dispose() { }

        private string GetUrl(CatalogApiPart catalogPart)
        {
            return onlineCatalogInformation.GetAbsoluteUrl(catalogPart, true);
        }

	    private IVehicle ValidateVehicle(IVehicle vehicle)
	    {
		    return vehicle == null || (vehicle.Year <= 0 && string.IsNullOrWhiteSpace(vehicle.Vin)) ? null : vehicle;
	    }

	    public bool PriceCheck(IHostData hostData, IVendor vendor, IExtendedPriceCheck priceCheck, IVehicle vehicle)
	    {
		    if (priceCheck?.Parts == null || priceCheck.Parts.Count == 0)
		    {
			    throw new ArgumentException(nameof(priceCheck));
		    }

		    var url = GetUrl(CatalogApiPart.PriceCheck);

		    var json = Json.SerializeObject(new PriceCheckRequest
		    {
			    HostData = ToHostData(hostData),
			    Vendor = ToVendor(vendor),
			    PriceCheck = ToPriceCheck(priceCheck),
			    Vehicle = ToVehicle(ValidateVehicle(vehicle))
		    });

		    logger.LogPriceCheck(Direction.Request, Json.SerializeObject(priceCheck));

		    string response = CallRemoteApiInteractive(url, json, LogPriceCheckResponse, "Price Check");

		    return UpdatePriceCheck(response, priceCheck);
	    }

		public static bool UpdatePriceCheck(string responseRaw, IExtendedPriceCheck priceCheck)
		{
			if (responseRaw == null) return false;

			var response = Json.DeserializeObject<PriceCheckResponse>(responseRaw);

			var parts = response.Parts;

			if (parts == null || parts.Count != priceCheck.Parts.Count)
				throw new CatalogException("Catalog did not return the parts we sent it - size mismatch or no parts returned.");

			for (int i = 0; i < parts.Count; ++i)
			{
				var remotePart = parts[i];
				var localPart = priceCheck.Parts[i];

				localPart.Found = remotePart.Found;
				localPart.Metadata = remotePart.Metadata;
				localPart.QuantityRequested = remotePart.QuantityRequested;
				if (remotePart.Locations != null && remotePart.Locations.Count > 0)
				{
					localPart.Status = remotePart.Status;

					// Give server a chance to update these, otherwise keep local values
					localPart.ManufacturerLineCode = remotePart.ManufacturerLineCode ?? localPart.ManufacturerLineCode;
					localPart.PartNumber = remotePart.PartNumber ?? localPart.PartNumber;
					localPart.ManufacturerName = remotePart.ManufacturerName ?? localPart.ManufacturerName;
                    localPart.Metadata = remotePart.Metadata;

					if (string.IsNullOrEmpty(localPart.Description) && !string.IsNullOrEmpty(remotePart.Description))
					{
						localPart.Description = remotePart.Description;
					}

					var selectedLocationId = localPart.SelectedLocation?.Id;
					localPart.Locations.Clear();
					int locationIndex = 1;
					foreach (var loc in remotePart.Locations)
					{
						var location = new Location
						{
							Id = loc.Id ?? (locationIndex++).ToString(),
							Name = loc.Name,
							UnitCost = loc.UnitCost,
							UnitList = loc.UnitList,
							UnitCore = loc.UnitCore,
							QuantityAvailable = loc.QuantityAvailable,
							SupplierName = loc.SupplierName,
							ShippingDescription = loc.ShippingDescription,
							ShippingCost = ToDecimal(loc.ShippingCost)
						};
						
						localPart.AddLocation(location);
					}

					// If remote had suggested a location, select it now
					if (localPart.Locations.Count > 0)
					{
						var foundLocation =  selectedLocationId != null ? localPart.Locations.FirstOrDefault(x => x.Id == selectedLocationId) : null;
						localPart.SelectedLocation = foundLocation ?? localPart.Locations[0];
					}

					localPart.AlternateParts.Clear();
					if (remotePart.AlternateParts != null && remotePart.AlternateParts.Count > 0)
					{
						foreach (var alternatePart in remotePart.AlternateParts)
						{
							var altPart = localPart.NewAlternatePart();
							altPart.Description = alternatePart.Description;
							altPart.ManufacturerLineCode = alternatePart.ManufacturerLineCode;
							altPart.ManufacturerName = alternatePart.ManufacturerName;
							altPart.PartNumber = alternatePart.PartNumber;
							altPart.Status = alternatePart.Status;
							altPart.Metadata = alternatePart.Metadata;

							localPart.AddAlternatePart(altPart);

							foreach (var loc in alternatePart.Locations)
							{
								altPart.AddLocation(new Location
								{
									Id = loc.Id ?? (locationIndex++).ToString(),
									Name = loc.Name,
									QuantityAvailable = loc.QuantityAvailable,
									UnitCore = loc.UnitCore,
									UnitCost = loc.UnitCost,
									UnitList = loc.UnitList,
									SupplierName = loc.SupplierName,
									ShippingDescription = loc.ShippingDescription,
									ShippingCost = ToDecimal(loc.ShippingCost)
								});
							}
						}
					}
				}
			}

			return true;
		}

		/// <summary>
		/// For use with catalogs that can return single or multiple purchase orders for a single request order
		/// </summary>
		public OrderPartsResponse OrderParts(IHostData hostData, IVendor vendor, IExtendedOrder order, IVehicle vehicle)
		{
			if (order?.Parts == null || order.Parts.Count == 0)
			{
				throw new ArgumentException(nameof(order));
			}

			bool useMultiplePurchaseOrdersEndpoint = onlineCatalogInformation.SupportsMultiplePurchaseOrders && string.IsNullOrEmpty(order.PurchaseOrderNumber);
			if (!useMultiplePurchaseOrdersEndpoint)
			{
				order.PurchaseOrderNumber += !string.IsNullOrWhiteSpace(order.ReferenceInvoiceNumber) ? $"-{order.ReferenceInvoiceNumber}" : "";
				order.ReferenceInvoiceNumber = null;
			}

			var url = GetUrl(useMultiplePurchaseOrdersEndpoint ? CatalogApiPart.PartsOrderV2 : CatalogApiPart.PartsOrder);

			var json = Json.SerializeObject(new OrderRequest
			{
				HostData = ToHostData(hostData),
				Vendor = ToVendor(vendor),
				Order = ToOrder(order),
				Vehicle = ToVehicle(ValidateVehicle(vehicle))
			});

			logger.LogOrderParts(Direction.Request, Json.SerializeObject(order));

			string response = CallRemoteApiInteractive(url, json, LogOrderPartsResponse, "Order");

			if (!string.IsNullOrWhiteSpace(response))
            {
				return useMultiplePurchaseOrdersEndpoint
				? Json.DeserializeObject<OrderPartsResponse>(response)
				: MapV1OrderResponseToV2(response);
			}
			return new OrderPartsResponse()
			{
				PurchaseOrders = new List<PurchaseOrder>()
			};
		}

		private OrderPartsResponse MapV1OrderResponseToV2(string orderResponseString)
        {
			var orderPartsResponse = new OrderPartsResponse();
			orderPartsResponse.PurchaseOrders = new List<PurchaseOrder>();
			if (string.IsNullOrWhiteSpace(orderResponseString))
				return orderPartsResponse;

			try
			{
				var orderResponse = Json.DeserializeObject<OrderResponse>(orderResponseString);
				if (orderResponse != null)
				{
					orderPartsResponse.AbsoluteRedirectUrl = orderResponse.AbsoluteRedirectUrl;
					orderPartsResponse.PurchaseOrders.Add(new PurchaseOrder()
					{
						ConfirmationNumber = orderResponse.ConfirmationNumber,
						TrackingNumber = orderResponse.TrackingNumber,
						Parts = orderResponse.Parts
					});
				}
			}
            catch (Exception)
            {
				//nothing
			}
			return orderPartsResponse;
		}

		public async Task<TrackingResponse> GetOrderTracking(IHostData hostData, IVendor vendor, string orderTrackingNumber, CancellationToken cancellationToken)
	    {
		    var url = GetUrl(CatalogApiPart.OrderTracking);

		    var json = Json.SerializeObject(new TrackingRequest
		    {
			    HostData = ToHostData(hostData),
			    Vendor = ToVendor(vendor),
			    OrderTrackingNumber = orderTrackingNumber
			});

		    return await CallRemoteApi<TrackingResponse>(HttpMethod.Post, url, json, "application/json", LogPriceCheckResponse, cancellationToken);
	    }

		private async Task<string> CallRemoteApiRaw(HttpMethod method, string url, string body, string contentType, Action<string> logging, CancellationToken cancellationToken)
        {
            if (method == HttpMethod.Post || method == HttpMethod.Put)
            {
                if (body == null || contentType == null)
                    throw new ArgumentException(@"Unable to perform call - missing data for post/put", nameof(method));
            }
            else
            {
                if (body != null || contentType != null)
                    throw new ArgumentException("Unable to perform call - unexpected data for " + method);
            }

	        try
	        {
		        using (var client = new HttpClient())
		        {
			        var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
			        requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			        requestMessage.Method = method;

			        if (body != null)
				        requestMessage.Content = new StringContent(body, Encoding.UTF8, contentType);

			        var response = await client.SendAsync(requestMessage, cancellationToken);

			        var status = response.StatusCode;

					if (status == HttpStatusCode.Forbidden)
				        throw new CatalogAuthenticationException();

					var data = await response.Content.ReadAsStringAsync();

					if (!response.IsSuccessStatusCode)
					{
						logging(data);
						var reason = response.ReasonPhrase ?? "";
						ApiCallException.ContentType type;
						if (response.Content.Headers.ContentType.MediaType == "text/plain")
							type = ApiCallException.ContentType.Text;
						else if (response.Content.Headers.ContentType.MediaType == "text/html")
							type = ApiCallException.ContentType.HTML;
						else
							throw new CatalogException($"Catalog returned error {status} {reason}");

						throw new ApiCallException(data, type, status, reason);
					}

			        return data;
		        }			
	        }
	        catch (AggregateException ex)
	        {
		        if (ex.InnerException == null)
		        {
			        throw;
		        }

		        // Just Raise an Auth Error
		        if (typeof(CatalogAuthenticationException) == ex.InnerException.GetType())
		        {
			        throw ex.InnerException;
		        }

		        // Otherwise, show a more useful error than just multiple errors occurred.
		        Trace.WriteLine($"Error connecting to '{onlineCatalogInformation.DisplayName}' Catalog - Url: {url} >> {ex.InnerException.Message}");
		        throw new Exception($"'{onlineCatalogInformation.DisplayName}' : {ex.InnerException.Message}", ex.InnerException);
	        }
        }

		private void LogPriceCheckResponse(string message) => logger.LogPriceCheck(Direction.Response, message);

		private void LogOrderPartsResponse(string message) => logger.LogOrderParts(Direction.Response, message);

		private async Task<T> CallRemoteApi<T>(HttpMethod method, string url, string body, string contentType, Action<string> logging, CancellationToken cancellationToken)
		{
			string data = await CallRemoteApiRaw(method, url, body, contentType, logging, cancellationToken);
			logging(data);
			return Json.DeserializeObject<T>(data);
		}

		private string CallRemoteApiInteractive(string url, string json, Action<string> logging, string taskName)
		{
			string responseRaw = showCatalogWaitForm.MakeWebCallWithErrorHandling(async token
				=> await CallRemoteApiRaw(HttpMethod.Post, url, json, "application/json", logging, token), taskName);
			try
			{
				var webResponseObject = Json.DeserializeObject<dynamic>(responseRaw);
				//Don't want a RuntimeBinderException if JSON is not an object; e.g. array
				var redirectUrl = webResponseObject is JObject ? (string) webResponseObject.AbsoluteRedirectUrl : null;

				if (redirectUrl != null)
				{
					var catalogController = new PartsView(redirectUrl);
					using (var hostingForm = newCatalogHostingForm(onlineCatalogInformation, catalogController))
					{
						if (!hostingForm.ShowWebPage())
						{
							logging(responseRaw);
							return null;
						}

						responseRaw = catalogController.Response;
					}
				}

				logging(responseRaw);
				return responseRaw;
			}
			catch (Exception)
			{
				logging(responseRaw);
				throw;
			}
		}

		private static HostData ToHostData(IHostData hostData) =>
			new HostData
			{
				ApplicationTitle = hostData.ApplicationTitle,
				ApplicationVersion = hostData.ApplicationVersion,
				LaborRate = hostData.LaborRate,
				HostApiLevel = OnlineCatalogInformation.HostApiLevel
			};

		private static Vendor ToVendor(IVendor vendor) =>
			new Vendor
			{
				Name = vendor.Name,
				Code = vendor.Code,
				Qualifier = vendor.Qualifier
			};

		private static Vehicle ToVehicle(IVehicle vehicle)
		{
			if (vehicle == null)
				return null;

			var vehicleObject = new Vehicle
			{
				Vin = vehicle.Vin,
				Year = vehicle.Year,
				Make = vehicle.Make,
				Model = vehicle.Model,
				SubModel = vehicle.SubModel,
				Transmission = vehicle.Transmission,
				Engine = vehicle.Engine,
				DriveType = vehicle.DriveType,
				Brake = vehicle.Brake,
				Gvw = vehicle.Gvw,
				Body = vehicle.Body,
				AcesEngineId = vehicle.AcesEngineId,
				AcesId = vehicle.AcesId,
				AcesBaseId = vehicle.AcesBaseId,
			};
			if (vehicle is IVehicleAcesProvider aces)
			{
				vehicleObject.AcesEngineBaseId = aces.GetAcesId(AcesId.EngineBaseID);
				vehicleObject.AcesEngineConfigId = aces.GetAcesId(AcesId.EngineConfigID);
				vehicleObject.AcesSubmodelId = aces.GetAcesId(AcesId.SubModelID);
			}
			return vehicleObject;
		}

		private static PriceCheck ToPriceCheck(IExtendedPriceCheck priceCheck) =>
			new PriceCheck
			{
				DeliveryOption = priceCheck.DeliveryOption,
				Parts = priceCheck.Parts.Select(part => new PriceCheckPart
				{
					AlternateParts = part.AlternateParts.Select(alternatePart => new PriceCheckAlternatePart
					{
						Description = alternatePart.Description,
						Locations = alternatePart.Locations.Select(ToLocation).ToList(),
						ManufacturerLineCode = alternatePart.ManufacturerLineCode,
						ManufacturerName = alternatePart.ManufacturerName,
						PartNumber = alternatePart.PartNumber,
						QuantityRequested = part.QuantityRequested,
						Status = alternatePart.Status,
						Metadata = alternatePart.Metadata
					}).ToList(),
					Description = part.Description,
					Found = part.Found,
					Locations = part.Locations.Select(ToLocation).ToList(),
					ManufacturerLineCode = part.ManufacturerLineCode,
					ManufacturerName = part.ManufacturerName,
					PartNumber = part.PartNumber,
					QuantityRequested = part.QuantityRequested,
					SelectedLocation = ToLocation((Location)part.SelectedLocation),
					Status = part.Status,
					Metadata = part.Metadata
				}).ToList()
			};

		private static TransferObjects.Location ToLocation(Location location) =>
			new TransferObjects.Location
			{
				Id = location.Id,
				Name = location.Name,
				QuantityAvailable = location.QuantityAvailable,
				UnitCore = location.UnitCore,
				UnitCost = location.UnitCost,
				UnitList = location.UnitList,
				SupplierName = location.SupplierName,
				ShippingDescription = location.ShippingDescription,
				ShippingCost = location.ShippingCost
			};

		private static Order ToOrder(IExtendedOrder order) =>
			new Order
			{
				Parts = order.Parts.Select((part, i) => new OrderPart
				{
					Index = i,
					Description = part.Description,
					Found = part.Found,
					LocationId = part.LocationId,
					LocationName = part.LocationName,
					ManufacturerLineCode = part.ManufacturerLineCode,
					ManufacturerName = part.ManufacturerName,
					PartNumber = part.PartNumber,
					QuantityAvailable = part.QuantityAvailable,
					QuantityOrdered = part.QuantityOrdered,
					QuantityRequested = part.QuantityRequested,
					Status = part.Status,
					UnitCore = part.UnitCore,
					UnitCost = part.UnitCost,
					UnitList = part.UnitList,
					SupplierName = part.SupplierName,
					Metadata = part.Metadata,
					ShippingDescription = part.ShippingDescription,
					ShippingCost = part.ShippingCost
				}).ToList(),
				DeliveryOption = order.DeliveryOption,
				OrderMessage = order.OrderMessage,
				PurchaseOrderNumber = order.PurchaseOrderNumber,
				ReferenceInvoiceNumber = order.ReferenceInvoiceNumber
			};
    }
}
