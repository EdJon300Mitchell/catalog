using System.Collections.Generic;

namespace Mitchell1.Online.Catalog.Host.TransferObjects
{
	// See Mitchell1 Online Catalog SDK Doc
    public class PriceCheckRequest
    {
        public HostData HostData { get; set; }
        public Vendor Vendor { get; set; }
        public PriceCheck PriceCheck { get; set; }
        public Vehicle Vehicle { get; set; }
    }

    // See Mitchell1 Online Catalog SDK Doc
    public class PriceCheckResponse
    {
	    public string AbsoluteRedirectUrl { get; set; }
	    public IList<PriceCheckPart> Parts { get; set; }
    }

    // See Mitchell1 Online Catalog SDK Doc
    public class PriceCheck
    {
        public string DeliveryOption { get; set; }
        public IList<PriceCheckPart> Parts { get; set; }
    }

    // See Mitchell1 Online Catalog SDK Doc
    public class PriceCheckPart
    {
        public IList<PriceCheckAlternatePart> AlternateParts { get; set; }
        public bool Found { get; set; }
        public IList<Location> Locations { get; set; }
        public string PartNumber { get; set; }
        public string ManufacturerLineCode { get; set; }
        public string ManufacturerName { get; set; }
        public string Description { get; set; }
        public decimal QuantityRequested { get; set; }
        public Location SelectedLocation { get; set; }
        public string Status { get; set; }
        public string Metadata { get; set; }
    }

    // See Mitchell1 Online Catalog SDK Doc
    public class PriceCheckAlternatePart
    {
        public IList<Location> Locations { get; set; }
        public string PartNumber { get; set; }
        public string ManufacturerLineCode { get; set; }
        public string ManufacturerName { get; set; }
        public string Description { get; set; }
        public decimal QuantityRequested { get; set; }
        public string Status { get; set; }
        public string Metadata { get; set; }
    }

	// See Mitchell1 Online Catalog SDK Doc
	public class Location
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public decimal QuantityAvailable { get; set; }
		public decimal UnitList { get; set; }
		public decimal UnitCost { get; set; }
		public decimal UnitCore { get; set; }
		public string SupplierName { get; set; }
		public string ShippingDescription { get; set; }
		public decimal ShippingCost { get; set; }
	}
}
