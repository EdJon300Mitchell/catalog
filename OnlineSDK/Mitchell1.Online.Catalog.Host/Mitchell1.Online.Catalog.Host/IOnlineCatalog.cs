using System.Threading;
using System.Threading.Tasks;
using Mitchell1.Catalog.Framework.Interfaces;
using Mitchell1.Online.Catalog.Host.API;
using Mitchell1.Online.Catalog.Host.Orders;
using Mitchell1.Online.Catalog.Host.TransferObjects;

namespace Mitchell1.Online.Catalog.Host
{
	public interface IOnlineCatalog : ICatalog
	{
		bool SupportsOrderTracking { get; }

		/// <summary>
		/// Attempts to get latest tracking data. Shows a progress UI and any errors are shown to the user internally
		/// Throws OperationCancelledException if aborted/error. Behaves the same as PriceCheck/OrderPart
		/// </summary>
		/// <param name="orderTrackingId"></param>
		/// <returns></returns>
		TrackingRequestResponse RequestOrderTracking(string orderTrackingId);

		/// <summary>
		/// Same as RequestOrderTracking, however, shows no UI and catches no errors. Caller is responsible. Use this to completely background this lookup
		/// Can throw various exceptions. Specifically, ApiCallException will be thrown if Catalog Vendor returned custom error with text/html data.
		/// You can use ShowDetailExceptionMessageBox to show the HTML/Text message in a browser based message box.
		/// </summary>
		Task<TrackingRequestResponse> RequestOrderTrackingAsync(string orderTrackingId, CancellationToken cancellationToken);

		/// <summary>
		/// Shows either plain text catalog error or HTML markup message in a browser control.
		/// </summary>
		void ShowDetailExceptionMessageBox(ApiCallException exception, string title);

		/// <summary>
		/// Launch GoShopping session
		/// </summary>
		/// <param name="cart">Transfer Items and potentially ordered items</param>
		/// <returns>True if cart has data, otherwise false</returns>
		bool GoShopping(out ShoppingCart cart);

		/// <summary>
		/// Send a request to online catalog to order parts
		/// </summary>
		/// <param name="order"></param>
		/// <returns>Order response if successful. otherwise, null</returns>
		OrderResponse OrderParts(OrderRequest order);
	}
}