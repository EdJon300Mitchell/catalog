using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Annotations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using ExampleCatalog.Persistence;
using Mitchell1.Online.Catalog.Host.TransferObjects;
using Nancy;
using Nancy.Helpers;
using Nancy.ModelBinding;
using Newtonsoft.Json;

namespace ExampleCatalog.Api
{
	public class PartsController : NancyModule
	{
		private const string modulePath = "/api";
		private const string showUiPartNumber = "SHOWUI";
		private const string showUiManufacturerName = "M1";
		private const string priceCheckRelativeUrl = "/PriceCheckView";
		private const string orderPartsRelativeUrl = "/OrderPartsView";
		private static readonly JsonSerializerSettings ignoreNullValues = new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore};

		public PartsController(HttpServer server) : base(modulePath)
		{
			Post["/PriceCheck"] = HandlePriceCheck;
			Post["/OrderParts"] = HandleOrderParts;

			// This lives here for simplicity, could live anywhere
			Get[priceCheckRelativeUrl + "/{sessionId}"] = o => HandlePriceCheckViewRequest(o.sessionId);
			Get[orderPartsRelativeUrl + "/{sessionId}"] = o => HandleOrderPartsViewRequest(o.sessionId);
		}

		public class AuthObject
		{
			public string Token { get; set; }
			public int Shop { get; set; }
		}

		public static bool CheckVendor(Vendor vendor, out AuthObject auth)
		{
			auth = null;

			var configuration = vendor.Qualifier;
			if (string.IsNullOrEmpty(configuration))
			{
				Console.WriteLine("CheckConfiguration: Missing configuration");
				return false;
			}

			// Our sample format {"token":"D5A99C47-5CF5-4ED9-ACF9-E467D431E198","shop":12345}
			auth = JsonConvert.DeserializeObject<AuthObject>(configuration);
			if (auth.Token != "D5A99C47-5CF5-4ED9-ACF9-E467D431E198" || auth.Shop != 12345)
			{
				Console.WriteLine("CheckConfiguration: Invalid user");
				return false;
			}

			return true;
		}

		private static Response CreateRawResponse(string data, string contentType, HttpStatusCode statusCode)
		{
			var response = (Response)data;
			response.StatusCode = statusCode;
			response.ContentType = contentType;
			return response;
		}

		private static Response CheckForSimulatedPriceCheckError(PriceCheckRequest request)
		{
			var plainTextError = request.PriceCheck.Parts.FirstOrDefault(p => p.PartNumber == "555");
			if (plainTextError != null)
			{
				if (!int.TryParse(plainTextError.Description, out var errorCode))
					errorCode = 500;

				return CreateRawResponse("Price Check Test Error. Contains a lot of text that should in theory wrap around in the dialog that is shown. Please contact your support agent and let them know your account is... OK. Thanks!", "text/plain", (HttpStatusCode)errorCode);
			}

			var htmlError = request.PriceCheck.Parts.FirstOrDefault(p => p.PartNumber == "444");
			if (htmlError != null)
			{
				if (!int.TryParse(htmlError.Description, out var errorCode))
					errorCode = 500;

				return CreateRawResponse("<html><body style='background: red;'>PriceCheck Test Error<br/><a href='https://duckduckgo.com'>Test Link Internal/Abs</a><br/><a href='https://duckduckgo.com' target='_blank'>Test Link External/Abs</a></body></html>", "text/html", (HttpStatusCode)errorCode);
			}

			return null;
		}

		private static Response CheckForSimulatedOrderPartsError(OrderRequest request)
		{
			var plainTextError = request.Order.Parts.FirstOrDefault(p => p.PartNumber == "888");
			if (plainTextError != null)
			{
				if (!int.TryParse(plainTextError.Description, out var errorCode))
					errorCode = 500;

				return CreateRawResponse("Order Test Error", "text/plain", (HttpStatusCode)errorCode);
			}

			var htmlError = request.Order.Parts.FirstOrDefault(p => p.PartNumber == "777");
			if (htmlError != null)
			{
				if (!int.TryParse(htmlError.Description, out var errorCode))
					errorCode = 500;

				return CreateRawResponse("<html><body style='background: green;'>Order Test Error</body></html>", "text/html", (HttpStatusCode)errorCode);
			}

			return null;
		}

		private dynamic HandlePriceCheck(dynamic o)
		{
			Console.WriteLine("PriceCheck Requested");

			try
			{
				var priceCheck = this.Bind<PriceCheckRequest>();

				if (!CheckVendor(priceCheck.Vendor, out _))
					return 403;

				var errorResponse = CheckForSimulatedPriceCheckError(priceCheck);
				if (errorResponse != null)
					return errorResponse;

				if (IsAbsoluteRedirectUrlSupported(priceCheck.HostData)
				    && priceCheck.PriceCheck.Parts.Any(part => part.PartNumber == showUiPartNumber && part.ManufacturerName == showUiManufacturerName))
				{
					var sessionId = ObjectStore.CreatePriceCheckSession(priceCheck);
					return new { AbsoluteRedirectUrl = GenerateLink(sessionId, priceCheckRelativeUrl) };
				}

				var priceCheckResponse = TransformSamplePriceCheck(priceCheck);

				return Response.AsJson(priceCheckResponse);
			}
			catch (Exception e)
			{
				Console.WriteLine("HandlePriceCheck: {0}", e.Message);
				return 500;
			}
		}

		private bool IsAbsoluteRedirectUrlSupported(HostData hostData) => hostData.HostApiLevel >= 3;

		private static PriceCheckResponse TransformSamplePriceCheck(PriceCheckRequest priceCheck)
		{
			var priceCheckResponse = new PriceCheckResponse {Parts = priceCheck.PriceCheck.Parts};
			foreach (var part in priceCheckResponse.Parts)
			{
				part.Locations.Clear();
				var selectedLocation = part.SelectedLocation;
				if (!inventory.TryGetValue(new PartDictionary.PartKey(part.PartNumber, part.ManufacturerLineCode), out var foundPart))
				{
					part.Found = false;
					if (selectedLocation != null)
						selectedLocation.QuantityAvailable = 0;
				}
				else
				{
					part.Found = true;
					foreach (var location in foundPart.Locations)
					{
						part.Locations.Add(location);
					}

					var matchedLocation = part.Locations.FirstOrDefault(x => x.Id == selectedLocation.Id);
					part.SelectedLocation = matchedLocation ?? part.Locations[0];

					// optionally define metadata for the part (custom data)
					part.Metadata = foundPart.Metadata;

					// If you support alternate parts
					part.AlternateParts.Clear();
					foreach (var alternatePart in foundPart.AlternateParts)
					{
						alternatePart.QuantityRequested = part.QuantityRequested;
						part.AlternateParts.Add(alternatePart);
					}
				};
			}

			if (priceCheck.Vehicle != null)
			{ 
				// Current vehicle selected (use for tracking associated vehicle)
				Trace.WriteLine("Vehicle Vin - " + priceCheck.Vehicle.Vin);
				Trace.WriteLine("Vehicle Year - " + priceCheck.Vehicle.Year);
				Trace.WriteLine("Vehicle Make - " + priceCheck.Vehicle.Make);
				Trace.WriteLine("Vehicle Model - " + priceCheck.Vehicle.Model);
				Trace.WriteLine("Vehicle SubModel - " + priceCheck.Vehicle.SubModel);
				Trace.WriteLine("Vehicle Body - " + priceCheck.Vehicle.Body);
				Trace.WriteLine("Vehicle Brake - " + priceCheck.Vehicle.Brake);
				Trace.WriteLine("Vehicle Drive Type - " + priceCheck.Vehicle.DriveType);
				Trace.WriteLine("Vehicle Engine - " + priceCheck.Vehicle.Engine);
				Trace.WriteLine("Vehicle Gvw - " + priceCheck.Vehicle.Gvw);
			}
			// You may safely delete the trace output above

			return priceCheckResponse;
		}

		private dynamic HandleOrderParts(dynamic o)
		{
			Console.WriteLine("OrderParts Requested");

			try
			{
				var order = this.Bind<OrderRequest>();
				if (!CheckVendor(order.Vendor, out var auth))
					return 403;

				var errorResponse = CheckForSimulatedOrderPartsError(order);
				if (errorResponse != null)
					return errorResponse;

				if (IsAbsoluteRedirectUrlSupported(order.HostData) && order.Order.Parts.Any(part => part.PartNumber == showUiPartNumber && part.ManufacturerName == showUiManufacturerName))
				{
					var sessionId = ObjectStore.CreateOrderPartsSession(order);
					return new { AbsoluteRedirectUrl = GenerateLink(sessionId, orderPartsRelativeUrl) };
				}

				var orderResponse = TransformSampleOrder(order, auth);

				return Response.AsJson(orderResponse);
			}
			catch (Exception e)
			{
				Console.WriteLine("HandleOrderParts: {0}", e.Message);
				return ServerError(e.Message, HttpStatusCode.InternalServerError);
			}
		}

		private static OrderResponse TransformSampleOrder(OrderRequest order, AuthObject auth)
		{
			string vehicleJson = order.Vehicle != null ? JsonConvert.SerializeObject(order.Vehicle) : null;
			// string vehicleJson = null;  // use this instead if you don't want vehicles tracked

			var orderId = ObjectStore.CreateOrder(order.Order.PurchaseOrderNumber, auth.Shop, vehicleJson);
			var trackingId = ObjectStore.CreateTracking(orderId);

			var orderResponse = new OrderResponse
			{
				ConfirmationNumber = $"Confirmation #{orderId} {order.Order.DeliveryOption ?? ""}",
				TrackingNumber = trackingId.ToString(CultureInfo.InvariantCulture),
				Parts = order.Order.Parts
			};

			foreach (var part in orderResponse.Parts)
			{
				if (!inventory.TryGetValue(new PartDictionary.PartKey(part.PartNumber, part.ManufacturerLineCode), out var foundPart))
				{
					part.Found = false;
					part.QuantityOrdered = 0;
				}
				else
				{
					part.Found = true;
					var matchedLocation = foundPart.Locations.FirstOrDefault(x => x.Id == part.LocationId) ?? foundPart.Locations[0];
					bool matchingCosts = part.UnitCost == matchedLocation.UnitCost && part.ShippingCost == matchedLocation.ShippingCost && part.ShippingDescription == matchedLocation.ShippingDescription;
					part.QuantityOrdered = matchingCosts ? Math.Min(part.QuantityRequested, matchedLocation.QuantityAvailable) : 0;
					part.Status = (part.QuantityOrdered == part.QuantityRequested) ? "Ordered"
						: (part.QuantityOrdered > 0) ? "Partially Ordered"
						: "Not Ordered";
					if (part.Metadata != null && part.SupplierName != null)
					{
						// optionally define metadata for the parts (custom data)
						try
						{
							dynamic metadata = JsonConvert.DeserializeObject(part.Metadata);
							part.Metadata = null;

							if (metadata.Supplier != null && metadata.Supplier.SupplierName == part.SupplierName)
							{
								part.Metadata = JsonConvert.SerializeObject(new
								{
									metadata.Supplier
								});
							}
						}
						catch (Exception)
						{
							part.Metadata = foundPart.Metadata;
						}
					}
				}
			}

			return orderResponse;
		}

		public static Response ServerError(string message, HttpStatusCode statusCode)
		{
			return CreateHtmlResponse($"<html><body><b>{(int)statusCode}: {statusCode.ToString()}</b><br />{HttpUtility.HtmlEncode(message)}</body></html>", statusCode);
		}

		public static Response CreateHtmlResponse(string data, HttpStatusCode statusCode)
		{
			var response = (Response)data;
			response.StatusCode = statusCode;
			response.ContentType = "text/html";
			return response;
		}

		private Response HandlePriceCheckViewRequest(long sessionId)
		{
			try
			{
				PriceCheckRequest priceCheck = ObjectStore.GetPriceCheckSession(sessionId);
				if (priceCheck == null)
					return SessionIdNotFound(sessionId.ToString());

				var inputJson = JsonConvert.SerializeObject(priceCheck.PriceCheck, Formatting.Indented);
				var priceCheckResponse = TransformSamplePriceCheck(priceCheck);
				var outputJson = JsonConvert.SerializeObject(priceCheckResponse, Formatting.Indented, ignoreNullValues);

				return CreateHtmlResponse($@"
					<html>
						<head>
							<script type =""text/javascript"" src = ""/{HttpServer.Root}/Content/scripts/catalog-v3.0.0.js"" ></script>
						</head>
						<body>
							<h2>Price Check:</h2>
							Input: (SessionId#: {sessionId})<br />
							<textarea id=""inputJson"" style=""width: 100%; height: 6em; padding:0.5em 0.5em"">{inputJson}</textarea>
							<p>
								The price check json, shown below, is fully editable and will be saved when the submit button is clicked.
							</p>
							<input type=""button"" onclick=""SubmitRequestClicked();"" value=""Submit"" />
							<input type=""button"" style=""margin:5px;"" onclick=""CancelRequestClicked();"" value=""Cancel"" />
							<textarea id=""outputJson"" style=""width: 100%; height: 75%; padding:0.5em 0.5em"">{outputJson}</textarea>
							<br />
							<script type=""text/javascript"">
								var apiKey = 'apikey-12345-key';
								var catalogSdk = new Catalog(apiKey, false);
								function SubmitRequestClicked() {{
									catalogSdk.transfer(document.getElementById('outputJson').value);
								}}
								function CancelRequestClicked() {{
									catalogSdk.cancelRequest();
								}}
							</script>
						</body>
					</html>", HttpStatusCode.OK);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return ServerError(e.Message, HttpStatusCode.InternalServerError);
			}
		}

		private static Response HandleOrderPartsViewRequest(long sessionId)
		{
			try
			{
				OrderRequest orderRequest = ObjectStore.GetOrderPartsSession(sessionId);
				if (orderRequest == null)
					return SessionIdNotFound(sessionId.ToString());

				if (!CheckVendor(orderRequest.Vendor, out var auth))
					CreateHtmlResponse("<html><body><b>Not Authorized</b></body></html>", HttpStatusCode.Forbidden);

				var inputJson = JsonConvert.SerializeObject(orderRequest.Order, Formatting.Indented);
				var orderResponse = TransformSampleOrder(orderRequest, auth);
				var outputJson = JsonConvert.SerializeObject(orderResponse, Formatting.Indented, ignoreNullValues);

				return CreateHtmlResponse($@"
					<html>
						<head>
							<script type =""text/javascript"" src = ""/{HttpServer.Root}/Content/scripts/catalog-v3.0.0.js"" ></script>
						</head>
						<body>
							<h2>Order:</h2>
							Input: (SessionId#: {sessionId})<br />
							<textarea id=""inputJson"" style=""width: 100%; height: 6em; padding:0.5em 0.5em"">{inputJson}</textarea>
							<p>
								The order parts json, shown below, is fully editable and will be saved when the submit button is clicked.
							</p>
							<input type=""button"" onclick=""SubmitRequestClicked();"" value=""Submit"" />
							<input type=""button"" style=""margin:5px;"" onclick=""CancelRequestClicked();"" value=""Cancel"" />
							<textarea id=""outputJson"" style=""width: 100%; height: 75%; padding:0.5em 0.5em"">{outputJson}</textarea>
							<br />
							<script type=""text/javascript"">
								var apiKey = 'apikey-12345-key';
								var catalogSdk = new Catalog(apiKey, false);
								function SubmitRequestClicked() {{
									catalogSdk.transfer(document.getElementById('outputJson').value);
								}}
								function CancelRequestClicked() {{
									catalogSdk.cancelRequest();
								}}
							</script>
						</body>
					</html>", HttpStatusCode.OK);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return ServerError(e.Message, HttpStatusCode.InternalServerError);
			}
		}

		private static Response SessionIdNotFound(string sessionId)
		{
			Console.WriteLine($"SessionIdNotFound {sessionId}");
			return CreateHtmlResponse($"<html><body><b>Invalid SessionId# '{HttpUtility.HtmlEncode(sessionId)}'</b></body></html>", HttpStatusCode.NotFound);
		}

		private Uri GenerateLink(long sessionId, string relativeUrl)
		{
			// Using current controller path - just for demonstration - can be anywhere
			return new Uri(Request.Url.SiteBase + Request.Url.BasePath + modulePath + relativeUrl + $"/{sessionId}");
		}

		private static readonly PartDictionary inventory = new PartDictionary
		{
			{"P1", "CDE", "Car Destruction Enterprise", "PART 1", 2.0M, 1.0M, 0, 3},
			{"P2", "BFG", "Big Friendly Giant Co.", "Old Tire", 5.0M, 2.50M, 0, 7, "175", PartCategory.Tire},
			{"TIRE1", "BFG", "B. F. G.", "TIRE1", 55.88M, 29.99M, 10.00M, 15, "S1", PartCategory.Tire},
			{"WHEEL1", "BFG", "BFG", "WHEEL1", 28M, 14.0M, 0, 6, "S2", PartCategory.Wheel},
			{"P3", "BCD", "B. C. D.", "PART 3", 42.99M, 81.49M, 0, 11, "", PartCategory.Unspecified, "Supplier4", "Meta", "Next Day", 6M},
			{"P4", "BFG", "BFG", "PART 4", 2.99M, 1.49M, 0, 5, "S3", PartCategory.Unspecified, "Supplier ABC",
				"{\"Supplier\" : { \"SupplierName\" : \"Supplier ABC\", \"Coupon\" : \"NX123\" }}", "Delivered in 2 hours", 0.99M},
			{"SHOWUI", "M1", "M1", "SHOWUI", 24.99M, 12.0M, 0, 2, "S3", PartCategory.Unspecified, "Supplier XYZ",
				"Custom Metadata that is returned to the PriceCheck and/or OrderParts", "Next day", 1.99M},
		};
	}

	public class PartDictionary : Dictionary<PartDictionary.PartKey, PriceCheckPart>
	{
		public void Add(string partNumber, string lineCode, string manufacturerName, string description, decimal list, decimal cost, decimal core,
			decimal quantityAvailable, string size = null, PartCategory partCategory = PartCategory.Unspecified,
			string supplierName = null, string metadata = null, string shippingDescription = null, decimal shippingCost = 0M)
		{
			var priceCheckPart = new PriceCheckPart
			{
				PartNumber = partNumber,
				ManufacturerLineCode = lineCode,
				ManufacturerName = manufacturerName,
				Description = description,
				AlternateParts = new[]
				{
					new PriceCheckAlternatePart
					{
						Description = "Alt " + description,
						ManufacturerLineCode = lineCode + "A",
						ManufacturerName = "A" + manufacturerName,
						PartNumber = partNumber + "A",
						Metadata = JsonConvert.SerializeObject(new
						{
							Supplier = new {SupplierName = "AltSup", StoreAddress = "1234 Some Street, Jacumba, CA, 92934"}
						}),
						Status = "",
						Locations = new[] {NewLocation("4", list - 1M, cost - 0.5M, core, quantityAvailable + 3, "AltSup", "Next day", 2.29M)}
					}
				},
				Status = "",
				Locations = new[]
				{
					NewLocation("1", list, cost, core, quantityAvailable, supplierName, shippingDescription, shippingCost),
					NewLocation("2", list + 2, cost + 1, core, quantityAvailable - 2, "Sup2", "One Hour", 2.50M),
					NewLocation("3", list - 1, cost - 0.50M, core, quantityAvailable + 4),
				},
				Metadata = metadata,
			}; 
			Add(new PartKey(partNumber, lineCode), priceCheckPart);
			var alternatePart = priceCheckPart.AlternateParts[0];
			Add(new PartKey(alternatePart.PartNumber, alternatePart.ManufacturerLineCode), new PriceCheckPart
			{
				PartNumber = alternatePart.PartNumber,
				ManufacturerLineCode = alternatePart.ManufacturerLineCode,
				ManufacturerName = alternatePart.ManufacturerName,
				Description = alternatePart.Description,
				AlternateParts = new PriceCheckAlternatePart[0],
				Metadata = alternatePart.Metadata,
				Status = alternatePart.Status,
				Locations = alternatePart.Locations
			});
		}

		private static readonly Dictionary<string, string> locationName = new Dictionary<string, string>
		{
			{"1", "San Diego"}, {"2", "El Cajon"}, {"3", "Poway"}, {"4", "Chula Vista"}, {"5", "Vista"}
		};

		private static Location NewLocation(string id, decimal list, decimal cost, decimal core, decimal quantityAvailable,
			string supplierName = null, string shippingDescription = null, decimal shippingCost = 0M) => new Location
		{
			Id = id, Name = locationName[id], UnitList = list, UnitCost = cost, UnitCore = core, QuantityAvailable = quantityAvailable,
			SupplierName = supplierName, ShippingDescription = shippingDescription, ShippingCost = shippingCost
		};

		public class PartKey : Tuple<string, string>
		{
			public PartKey(string partNumber, string lineCode) : base(partNumber, lineCode) {}
			public string PartNumber => Item1;
			public string LineCode => Item2;
		}
	}
}
