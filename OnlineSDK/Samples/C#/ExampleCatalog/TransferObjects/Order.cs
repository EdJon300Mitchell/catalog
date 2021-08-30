using System;
using System.Collections.Generic;

namespace Mitchell1.Online.Catalog.Host.TransferObjects
{
	// See Mitchell1 Online Catalog SDK Doc
	public class OrderRequest
    {
        public HostData HostData { get; set; }
        public Vendor Vendor { get; set; }
        public Order Order { get; set; }
        public Vehicle Vehicle { get; set; }
    }

    // See Mitchell1 Online Catalog SDK Doc
	[Serializable]
    public class OrderResponse
    {
        public string AbsoluteRedirectUrl { get; set; }
	    public string ConfirmationNumber { get; set; }
	    public string TrackingNumber { get; set; }
	    public IList<OrderPart> Parts { get; set; }
    }

    // See Mitchell1 Online Catalog SDK Doc
    // This class represents an order request input
    public class Order
    {
        public string DeliveryOption { get; set; }
		public string OrderMessage { get; set; }
        public string PurchaseOrderNumber { get; set; }
		public string ReferenceInvoiceNumber { get; set; }
        public IList<OrderPart> Parts { get; set; }

    }

    // See Mitchell1 Online Catalog SDK Doc
	[Serializable]
    public class OrderPartsResponse
    {
        public string AbsoluteRedirectUrl { get; set; }
        public IList<PurchaseOrder> PurchaseOrders { get; set; }
    }

    // See Mitchell1 Online Catalog SDK Doc
	[Serializable]
    public class PurchaseOrder
    {
        public string ConfirmationNumber { get; set; }
        public string TrackingNumber { get; set; }
        public IList<OrderPart> Parts { get; set; }
    }

    // See Mitchell1 Online Catalog SDK Doc
    public class OrderPart
    {
		public int Index { get; set; }
        public bool Found { get; set; }
        public string LocationId { get; set; }
        public string LocationName { get; set; }
        public string Status { get; set; }
        public string PartNumber { get; set; }
        public string ManufacturerLineCode { get; set; }
        public string ManufacturerName { get; set; }
        public string Description { get; set; }
        public decimal UnitList { get; set; }
        public decimal UnitCost { get; set; }
        public decimal UnitCore { get; set; }
        public decimal QuantityRequested { get; set; }
        public decimal QuantityOrdered { get; set; }
	    public decimal QuantityAvailable { get; set; }
        public string SupplierName { get; set; }
		public string Metadata { get; set; }
        public string ShippingDescription { get; set; }
        public decimal ShippingCost { get; set; }
    }
}
