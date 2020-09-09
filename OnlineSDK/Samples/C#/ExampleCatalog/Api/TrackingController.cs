using System;
using System.Linq;
using ExampleCatalog.DataLayer;
using Nancy;
using Nancy.ModelBinding;
using ExampleCatalog.Persistence;
using Nancy.Helpers;

namespace ExampleCatalog.Api
{
	public class TrackingController : Nancy.NancyModule
	{
		private readonly ObjectStore objectStore = new ObjectStore();

		public TrackingController(HttpServer server) : base("/api")
		{
			Post["/OrderTracking"] = HandleTrackingRequest;
			
			// This lives here for simplicity, could live anywhere
			Get["/OrderTrackingView/{shop}/{tracking}"] = (o) => HandleTrackingViewRequest(o.tracking, o.shop);
		}

		private dynamic HandleTrackingRequest(dynamic o)
		{
			try
			{
				var request = this.Bind<TrackingRequest>();
				if (!PartsController.CheckVendor(request.Vendor, out var auth))
					return 403;

				if (!long.TryParse(request.OrderTrackingNumber, out var trackingNumber))
					return TrackingNotFound(request.OrderTrackingNumber);

				var response = objectStore.GetTrackingStatus(auth.Shop, trackingNumber);
				if (response != null)
				{
					return new TrackingResponse
					{
						ExternalTrackingUrl = GenerateTrackingLink(auth.Shop, response.TrackingId),
						StatusDisplay = response.Status
					};
				}

				return TrackingNotFound(request.OrderTrackingNumber);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return PartsController.ServerError(e.Message, HttpStatusCode.InternalServerError);
			}
		}

		private Uri GenerateTrackingLink(int shop, long trackingId)
		{
			// Using current controller path - just for demonstration - can be anywhere
			var baseUrl = Request.Url;
			var newUrl = baseUrl.SiteBase + baseUrl.BasePath;
			return new Uri(newUrl + $"/Api/OrderTrackingView/{shop}/{trackingId}");
		}

		private Response HandleTrackingViewRequest(long trackingNumber, int shop)
		{
			try
			{
				var tracking = objectStore.GetTrackingStatus(shop, trackingNumber);
				if (tracking == null)
					return TrackingNotFound(trackingNumber.ToString());

				var relatedOrders = objectStore.GetRelatedOrders(shop, tracking.ManagerPurchaseOrder).
					Where(o => o.OrderId != tracking.OrderId).
					Select(t => new
					{
						Order = t.OrderId,
						Status = t.Status,
						Link = GenerateTrackingLink(shop, t.TrackingId)
					}).
					ToList();

				var relatedOrderDetails = "";
				if (relatedOrders.Count > 0)
				{
					relatedOrderDetails = "<b>Found other Orders tied to this PO:</b><br /><ul>";
					foreach (var order in relatedOrders)
					{
						relatedOrderDetails += $"<li>Order# {order.Order}, Status: <a href='{order.Link}'>{HttpUtility.HtmlEncode(order.Status)}</a></li>";
					}
					relatedOrderDetails += "</ul>";
				}

				return PartsController.CreateHtmlResponse(
					$@"<html>
						<body>
						<h1>Order#: {tracking.OrderId} Tracking: {tracking.TrackingId} - {tracking.Status}</h1><br /><br />
						Manager PO: {HttpUtility.HtmlEncode(tracking.ManagerPurchaseOrder)}<br />
						Ordered: {tracking.Ordered} UTC<br />
						Expected Arrival: {tracking.Arrives} UTC<br />
                        Vehicle: <br /><br />
                        <textarea disabled style=""width: 80%; height: 6em; padding:0.5em 1em"">{HttpUtility.HtmlEncode(tracking.VehicleJson ?? "")}</textarea>
						<br /><br />
						{relatedOrderDetails}
						</body>
					</html>", HttpStatusCode.OK);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return PartsController.ServerError(e.Message, HttpStatusCode.InternalServerError);
			}
		}

		private static Response TrackingNotFound(string trackingNumber)
		{
			Console.WriteLine($"TrackingNotFound {trackingNumber}");
			return PartsController.CreateHtmlResponse($"<html><body><b>Invalid Tracking# '{HttpUtility.HtmlEncode(trackingNumber)}'</b></body></html>", HttpStatusCode.NotFound);
		}
	}
}