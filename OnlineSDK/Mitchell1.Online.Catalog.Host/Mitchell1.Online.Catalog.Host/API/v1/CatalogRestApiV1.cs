using System;
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
using Mitchell1.Online.Catalog.Host.Orders;
using Mitchell1.Online.Catalog.Host.TransferObjects;

namespace Mitchell1.Online.Catalog.Host.API.v1
{
    public class CatalogRestApiV1 : ICatalogRestController
    {
        private readonly OnlineCatalogInformation onlineCatalogInformation;

        public CatalogRestApiV1(OnlineCatalogInformation onlineCatalogInformation)
        {
            this.onlineCatalogInformation = onlineCatalogInformation;
        }

        public void Dispose()
        {
        }

        private string GetUrl(CatalogApiPart catalogPart)
        {
            return onlineCatalogInformation.GetAbsoluteUrl(catalogPart, true);
        }

	    private IVehicle ValidateVehicle(IVehicle vehicle)
	    {
		    return vehicle == null || (vehicle.Year <= 0 && string.IsNullOrWhiteSpace(vehicle.Vin)) ? null : vehicle;
	    }

		public async Task PriceCheck(IHostData hostData, IVendor vendor, IPriceCheck priceCheck, IVehicle vehicle, Logger logger, CancellationToken cancellationToken)
        {
            if (priceCheck?.Parts == null || priceCheck.Parts.Count == 0)
            {
                throw new ArgumentException(nameof(priceCheck));
            }

            var url = GetUrl(CatalogApiPart.PriceCheck);

            var json = Json.SerializeObject(new
            {
                HostData = hostData,
                Vendor = vendor,
                PriceCheck = priceCheck,
                Vehicle = ValidateVehicle(vehicle)
			});

            logger.LogPriceCheck(Direction.Request, Json.SerializeObject(priceCheck));

            dynamic response = await CallRemoteApi<dynamic>(HttpMethod.Post, url, json, "application/json", (s) => logger.LogPriceCheck(Direction.Response, s), cancellationToken);
            if (response.Parts == null || response.Parts.Count != priceCheck.Parts.Count)
                throw new CatalogException("Catalog did not return the parts we sent it - size mismatch or no parts returned.");

            for (int i = 0; i < response.Parts.Count; ++i)
            {
                var remotePart = response.Parts[i];
                var localPart = priceCheck.Parts[i];

                localPart.Found = (bool) remotePart.Found;
                if (remotePart.Locations != null && remotePart.Locations.Count > 0)
                {
                    localPart.Status = remotePart.Status;

                    // Give server a chance to update these, otherwise keep local values
                    localPart.ManufacturerLineCode = remotePart.ManufacturerLineCode ?? localPart.ManufacturerLineCode;
                    localPart.PartNumber = remotePart.PartNumber ?? localPart.PartNumber;
                    localPart.ManufacturerName = remotePart.ManufacturerName ?? localPart.ManufacturerName;

                    if (string.IsNullOrEmpty(localPart.Description) && !string.IsNullOrEmpty(remotePart.Description))
                    {
                        localPart.Description = remotePart.Description;
                    }

                    foreach (var loc in remotePart.Locations)
                    {
                        var location = localPart.NewLocation();

                        location.Id = loc.Id;
                        location.Name = loc.Name;
                        location.UnitCost = loc.UnitCost;
                        location.UnitList = loc.UnitList;
                        location.UnitCore = loc.UnitCore;
                        location.QuantityAvailable = loc.QuantityAvailable;

                        localPart.AddLocation(location);

                        localPart.SelectedLocation = location;
                    }

                    // If remote had suggested a location, select it now
                    if (localPart.Locations.Count > 0 && remotePart.SelectedLocation != null && (string)remotePart.SelectedLocation.Id != "")
                    {
                        var foundLocation = localPart.Locations.FirstOrDefault(l => l.Id == (string)remotePart.SelectedLocation.Id);
                        if (foundLocation != null)
                        {
                            localPart.SelectedLocation = foundLocation;
                        }
                    }

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

                            localPart.AddAlternatePart(altPart);

                            foreach (var loc in alternatePart.Locations)
                            {
                                var location = altPart.NewLocation();
                                location.Id = loc.Id;
                                location.Name = loc.Name;
                                location.QuantityAvailable = loc.QuantityAvailable;
                                location.UnitCore = loc.UnitCore;
                                location.UnitCost = loc.UnitCost;
                                location.UnitList = loc.UnitList;

                                altPart.Locations.Add(location);
                            }
                        }
                    }
                }
            }
        }

        public async Task<OrderResponse> OrderParts(IHostData hostData, IVendor vendor, OrderRequest order, IVehicle vehicle, Logger logger, CancellationToken cancellationToken)
        {
            if (order?.Parts == null || order.Parts.Count == 0)
            {
                throw new ArgumentException(nameof(order));
            }

            var url = GetUrl(CatalogApiPart.PartsOrder);
            var json = Json.SerializeObject(new
            {
                HostData = hostData,
                Vendor = vendor,
                Order = order,
				Vehicle = ValidateVehicle(vehicle)
			});

            logger.LogOrderParts(Direction.Request, Json.SerializeObject(order));

            dynamic response = await CallRemoteApi<dynamic>(HttpMethod.Post, url, json, "application/json", (s) => logger.LogOrderParts(Direction.Response, s), cancellationToken);
            if (response.Parts == null || response.Parts.Count != order.Parts.Count)
                throw new CatalogException("Catalog did not return the parts we sent it - size mismatch or no parts returned.");

	        var confirmationNumber = (string)response.ConfirmationNumber;
            if (string.IsNullOrEmpty(confirmationNumber))
                throw new CatalogException("Order was not successful");

	        var orderResponse = new OrderResponse
	        {
		        ConfirmationNumber = confirmationNumber,
		        Parts = order.Parts,
				TrackingNumber = (string)response.TrackingNumber
            };

            for (int i = 0; i < response.Parts.Count; ++i)
            {
                var localPart = orderResponse.Parts[i];
                var remotePart = response.Parts[i];

                localPart.ManufacturerLineCode = remotePart.ManufacturerLineCode;
                localPart.PartNumber = remotePart.PartNumber;
                localPart.QuantityRequested = remotePart.QuantityRequested;
                localPart.QuantityOrdered = remotePart.QuantityOrdered;
                localPart.UnitCore = remotePart.UnitCore;
                localPart.UnitList = remotePart.UnitList;
                localPart.UnitCost = remotePart.UnitCost;
                localPart.Status = remotePart.Status;
                localPart.QuantityAvailable = remotePart.QuantityAvailable;
                localPart.Found = remotePart.Found;
                localPart.LocationId = remotePart.LocationId;
                localPart.LocationName = remotePart.LocationName;                           
            }

	        return orderResponse;
        }

	    public async Task<TrackingRequestResponse> GetOrderTracking(IHostData hostData, IVendor vendor, string orderTrackingNumber, Logger logger, CancellationToken cancellationToken)
	    {
		    var url = GetUrl(CatalogApiPart.OrderTracking);

		    var json = Json.SerializeObject(new
		    {
			    HostData = hostData,
			    Vendor = vendor,
			    OrderTrackingNumber = orderTrackingNumber
			});

		    return await CallRemoteApi<TrackingRequestResponse>(HttpMethod.Post, url, json, "application/json", (s) => logger.LogPriceCheck(Direction.Response, s), cancellationToken);
	    }

		private async Task<T> CallRemoteApi<T>(HttpMethod method, string url, string body, string contentType, Action<string> logging, CancellationToken cancellationToken)
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
			        logging(data);

					if (!response.IsSuccessStatusCode)
					{
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

			        return Json.DeserializeObject<T>(data);
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

		        // Otherwise, show a more useful error than just multiple errors occured.
		        Trace.WriteLine($"Error connecting to '{onlineCatalogInformation.DisplayName}' Catalog - Url: {url} >> {ex.InnerException.Message}");
		        throw new Exception($"'{onlineCatalogInformation.DisplayName}' : {ex.InnerException.Message}", ex.InnerException);
	        }
        }
    }
}
