using System.Collections.Generic;

namespace ExampleCatalog.DataLayer
{
    public class OrderRequest
    {
        public IVendor Vendor { get; set; }
        public IOrder Order { get; set; }
        public IVehicle Vehicle { get; set; }
    }

    // See Mitchell1 Catalog SDK Doc
    public class IOrder
    {
		// Request Properties
        public string DeliveryOption { get; set; }
		public string OrderMessage { get; set; }
        public string PurchaseOrderNumber { get; set; }

		// In Request and Response
	    public IList<IOrderPart> Parts { get; set; }

		// Response properties

		public string ConfirmationNumber { get; set; }
		public string TrackingNumber { get; set; }
	}

    // See Mitchell1 Catalog SDK Doc
    public class IOrderPart
    {
        public string Description { get; set; }
        public bool Found { get; set; }
        public string LocationId { get; set; }
        public string LocationName { get; set; }
        public string ManufacturerLineCode { get; set; }
        public string ManufacturerName { get; set; }
        public string PartNumber { get; set; }
        public decimal QuantityAvailable { get; set; }
        public decimal QuantityOrdered { get; set; }
        public decimal QuantityRequested { get; set; }
        public string Status { get; set; }
        public decimal UnitCore { get; set; }
        public decimal UnitCost { get; set; }
        public decimal UnitList { get; set; }
    }
}
