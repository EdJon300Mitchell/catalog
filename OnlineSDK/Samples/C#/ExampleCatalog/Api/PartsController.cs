using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using ExampleCatalog.DataLayer;
using ExampleCatalog.Persistence;
using Nancy;
using Nancy.Extensions;
using Nancy.Helpers;
using Nancy.ModelBinding;
using Newtonsoft.Json;

namespace ExampleCatalog.Api
{
    public class PartsController : Nancy.NancyModule
    {
	    private ObjectStore objectStore = new ObjectStore();

		public PartsController(HttpServer server) : base("/api")
        {
            Post["/PriceCheck"] = HandlePriceCheck;
            Post["/OrderParts"] = HandleOrderParts;
        }

        public class AuthObject
        {
            public string Token { get; set; }
            public int Shop { get; set; }
        }

        public static bool CheckVendor(IVendor vendor, out AuthObject auth)
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

        private Response CreateRawResponse(string data, string contentType, HttpStatusCode statusCode)
        {
            var response = (Response)data;
            response.StatusCode = statusCode;
            response.ContentType = contentType;
            return response;
        }

        private Response CheckForSimulatedPriceCheckError(PriceCheckRequest request)
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

	    private Response CheckForSimulatedOrderPartsError(OrderRequest request)
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

                foreach (var part in priceCheck.PriceCheck.Parts)
                {
                    part.Found = true;
                    part.Locations.Add(new ILocation()
                    {
                        Id="SD1",
                        Name="San Diego One",
                        QuantityAvailable = 23,
                        UnitCore = 0,
                        UnitCost = 5.00m,
                        UnitList = 10m
                    });

                    // If you support alternate parts
                    var altLocation = new ILocation()
                    {
                        Id = "SD2",
                        Name = "San Diego Two",
                        QuantityAvailable = 21,
                        UnitCore = 0,
                        UnitCost = 6.00m,
                        UnitList = 10m
                    };
                    part.Locations.Add(altLocation);

                    part.AlternateParts.Add(new IPriceCheckAlternatePart()
                    {
                        Description = "Alternate Rotor Part",
                        ManufacturerLineCode = "alt123",
                        ManufacturerName = "Manufacturer",
                        PartNumber = "AltPart123",
                        Locations = new List<ILocation>(),
                        Status = ""
                    });

                    part.AlternateParts[0].Locations.Add(altLocation);
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

                return Response.AsJson(priceCheck.PriceCheck);
            }
            catch (Exception e)
            {
                Console.WriteLine("HandlePriceCheck: {0}", e.Message);
                return 500;
            }
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

                string vehicleJson = order.Vehicle != null ? JsonConvert.SerializeObject(order.Vehicle) : null;
                // string vehicleJson = null;  // use this instead if you don't want vehicles tracked

                var orderId = objectStore.CreateOrder(order.Order.PurchaseOrderNumber, auth.Shop, vehicleJson);
	            var trackingId = objectStore.CreateTracking(orderId);

				order.Order.ConfirmationNumber = $"Confirmation #{orderId} {order.Order.DeliveryOption ?? ""}";
	            order.Order.TrackingNumber = trackingId.ToString(CultureInfo.InvariantCulture);

				foreach (var part in order.Order.Parts)
                {
                    part.LocationId = "SD1";
                    part.LocationName = "San Diego One";
                    part.Status = "found";
                    part.QuantityAvailable = 25;
                    part.QuantityOrdered = part.QuantityRequested;
                    part.Found = true;
                }

                return Response.AsJson(order.Order);
            }
            catch (Exception e)
            {
                Console.WriteLine("HandleOrderParts: {0}", e.Message);
                return ServerError(e.Message, HttpStatusCode.InternalServerError);
            }
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
	}
}
