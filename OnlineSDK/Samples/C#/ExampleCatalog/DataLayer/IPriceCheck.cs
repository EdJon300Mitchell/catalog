using System.Collections.Generic;

namespace ExampleCatalog.DataLayer
{
    public class PriceCheckRequest
    {
        public IHostData HostData { get; set; }
        public IVendor Vendor { get; set; }
        public IPriceCheck PriceCheck { get; set; }
        public IVehicle Vehicle { get; set; }
    }

    // See Mitchell1 Catalog SDK Doc
    public class IHostData
    {
        public string ApplicationTitle { get; set; }
        public string ApplicationVersion { get; set; }
        public decimal LaborRate { get; set; }
    }

    // See Mitchell1 Catalog SDK Doc
    public class IVendor
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Qualifier { get; set; }
    }

    // See Mitchell1 Catalog SDK Doc
    public class IPriceCheck
    {
        public string DeliveryOption { get; set; }
        public IList<IPriceCheckPart> Parts { get; set; }
    }

    // See Mitchell1 Catalog SDK Doc
    public class IPriceCheckPart
    {
        public IList<IPriceCheckAlternatePart> AlternateParts { get; set; }
        public string Description { get; set; }
        public bool Found { get; set; }
        public IList<ILocation> Locations { get; set; }
        public string ManufacturerLineCode { get; set; }
        public string ManufacturerName { get; set; }
        public string PartNumber { get; set; }
        public decimal QuantityRequested { get; set; }
        public ILocation SelectedLocation { get; set; }
        public string Status { get; set; }
    }

    // See Mitchell1 Catalog SDK Doc
    public class IPriceCheckAlternatePart
    {
        public string Description { get; set; }
        public IList<ILocation> Locations { get; set; }
        public string ManufacturerLineCode { get; set; }
        public string ManufacturerName { get; set; }
        public string PartNumber { get; set; }
        public string Status { get; set; }
    }

    // See Mitchell1 Catalog SDK Doc
    public class ILocation
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal QuantityAvailable { get; set; }
        public decimal UnitCore { get; set; }
        public decimal UnitCost { get; set; }
        public decimal UnitList { get; set; }
    }

    public class IVehicle
    {
        public string Vin { get; set; }
        public string Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string SubModel { get; set; }
        public string Transmission { get; set; }
        public string Engine { get; set; }
        public string DriveType { get; set; }
        public string Brake { get; set; }
        public string Gvw { get; set; }
        public string Body { get; set; }
        public string AcesEngineId { get; set; }
        public string AcesId { get; set; }
        public string AcesBaseId { get; set; }
        public string AcesEngineBase { get; set; }
        public string AcesEngineConf { get; set; }
        public string AcesSubmodelId { get; set; }
    }
}
