using System.Collections.Generic;
using Mitchell1.Catalog.Framework.Interfaces;

namespace Mitchell1.Online.Catalog.Host.TransferObjects
{
	public class OrderRequest
	{
		public string DeliveryOption { get; set; }

		public string OrderMessage { get; set; }

		public IList<IOrderPart> Parts { get; set; }

		public string PurchaseOrderNumber { get; set; }
	}

	public class OrderResponse
	{
		public string ConfirmationNumber { get; set; }

		public IList<IOrderPart> Parts { get; set; }

		public string TrackingNumber { get; set; }
    }
}