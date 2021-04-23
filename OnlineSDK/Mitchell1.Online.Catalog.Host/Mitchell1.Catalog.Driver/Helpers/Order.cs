using System.Collections.Generic;
using Mitchell1.Online.Catalog.Host;

namespace Mitchell1.Catalog.Driver.Helpers
{
	internal class Order : IExtendedOrder
	{
		public string ConfirmationNumber { get; set; }
		public string DeliveryOption { get; set; }
		public string OrderMessage { get; set; }
		public string TrackingNumber { get; set; }
		public string PurchaseOrderNumber { get; set; }
		public IList<OrderPart> Parts { get; } = new List<OrderPart>();

		IReadOnlyList<IExtendedOrderPart> IExtendedOrder.Parts => (IReadOnlyList<IExtendedOrderPart>)Parts;
	}
}
