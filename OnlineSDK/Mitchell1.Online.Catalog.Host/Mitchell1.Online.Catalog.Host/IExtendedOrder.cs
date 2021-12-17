using System.Collections.Generic;

namespace Mitchell1.Online.Catalog.Host
{
	/// <summary>
	/// This IOrder extension supports more items that in legacy SDK (e.g. Online SDK extensions)
	/// </summary>
	public interface IExtendedOrder
	{
		string ConfirmationNumber { get; set; }

		/// <summary>
		/// User selected delivery option from the DeliveryMethod property on ICatalog.
		/// </summary>
		string DeliveryOption { get;}

		/// <summary>
		/// User entered message sent to the catalog along with the order.
		/// </summary>
		string OrderMessage { get; }

		/// <summary>
		/// Purchase Order Number as determined by host application.
		/// </summary>
		string PurchaseOrderNumber { get; set; }

		string ReferenceInvoiceNumber { get; set; }

		string TrackingNumber { get; set; }

		/// <summary>
		/// An ordered list of parts the user requested for ordering.  These parts will be updated
		/// with the status once ordering is done.
		/// </summary>
		IReadOnlyList<IExtendedOrderPart> Parts { get; }
	}
}