using System;
using System.Collections.Generic;
using System.ComponentModel;
using Mitchell1.Catalog.Framework.Interfaces;
using Mitchell1.Online.Catalog.Host;
using PartCategory = Mitchell1.Catalog.Framework.Interfaces.PartCategory;

namespace Mitchell1.Catalog.Driver.Helpers
{
	internal class PriceCheckPart : IExtendedPriceCheckPart
	{
		private readonly LocationList locations = new LocationList();
		private Location selectedLocation;
		private readonly AlternatePartsList alternateParts = new AlternatePartsList();

		private readonly Location emptyLocation = new Location();

		public PriceCheckPart() 
        {
        	Found = false;
            PartNumber = string.Empty;
            ManufacturerLineCode = string.Empty;
            ManufacturerName = string.Empty;
            Description = string.Empty;
            QuantityRequested = decimal.Zero;
            Status = string.Empty;
        }

		#region IExtendedPriceCheckPart Members

		[DescriptionAttribute("Was the part found in the catalog?")]
		public bool Found { get; set; }

		[DescriptionAttribute("The status for the part as returned by the catalog")]
		public string Status { get; set; }

		[DescriptionAttribute("The part number for this Price Check part.  Together with the ManufacturerLineCode, it uniquely identifies a part")]
		public string PartNumber { get; set; }

		[DescriptionAttribute("The manufacturer line code from the catalog.  Together with the PartNumber, it uniquely identifies a part")]
		public string ManufacturerLineCode { get; set; }

		[DescriptionAttribute("The manufacturer name for the Price Check part (like: \"Good Year\")")]
		public string ManufacturerName { get; set; }

		[DescriptionAttribute("A description for the Price Check part")]
		public string Description { get; set; }

		[DescriptionAttribute("The number of units requested in the Price Check request")]
		public decimal QuantityRequested { get; set; }

		[DescriptionAttribute("A list of alternate locations that this part is available from")]
		public IList<Location> Locations => locations;

		IList<ILocation> IPriceCheckPart.Locations => locations;

		[DescriptionAttribute("The metadata contains JSON supplier information.")]
		public string Metadata { get; set; }

		[DescriptionAttribute("A list of alternate parts which may be substituted for this Price Check part")]
		public IList<IExtendedPriceCheckAlternatePart> AlternateParts => alternateParts;

		IList<IPriceCheckAlternatePart> IPriceCheckPart.AlternateParts => alternateParts;

		[DescriptionAttribute("The location selected from the list of available locations")]
		public ILocation SelectedLocation
		{
			get
			{
				if (selectedLocation == null || selectedLocation == emptyLocation)
				{
					if (Locations.Count > 0)
					{
						selectedLocation = Locations[0];
					}
					else
					{
						if (selectedLocation == null)
						{
							selectedLocation = emptyLocation;
						}
					}
				}
				return selectedLocation;
			}
			set => selectedLocation = (Location)value;
		}

		public ILocation NewLocation() => new Location();

		public void AddLocation(ILocation location) => locations.Add(location);

		public IExtendedPriceCheckAlternatePart NewAlternatePart() => new PriceCheckAlternatePart();

		IPriceCheckAlternatePart IPriceCheckPart.NewAlternatePart() => NewAlternatePart();

		public void AddAlternatePart(IPriceCheckAlternatePart partItem) => alternateParts.Add(partItem);

		#endregion

		[DescriptionAttribute("The number of units ordered in the Price Check request")]
		public decimal QuantityOrdered { get; set; }

		[DescriptionAttribute("The primary location for the part.  Where the part will be delivered from")]
		public string LocationName
		{
			get { return SelectedLocation.Name; }
			set { SelectedLocation.Name = value; }
		}

		[DescriptionAttribute("The primary location identifier.  Uniquely identifies the delivery source")]
		public string LocationId
		{
			get { return SelectedLocation.Id; }
			set { SelectedLocation.Id = value; }
		}

		[DescriptionAttribute("The list price for a single unit (of quantity) for the Price Check part")]
		public decimal UnitList
		{
			get { return SelectedLocation.UnitList; }
			set { SelectedLocation.UnitList = value; }
		}

		[DescriptionAttribute("The unit cost for a single quantity of the Price Check part")]
		public decimal UnitCost
		{
			get { return SelectedLocation.UnitCost; }
			set { SelectedLocation.UnitCost = value; }
		}

		[DescriptionAttribute("The core exchange cost for the Price Check part")]
		public decimal UnitCore
		{
			get { return SelectedLocation.UnitCore; }
			set { SelectedLocation.UnitCore = value; }
		}

		[DescriptionAttribute("Uniquely identifies (within this catalog) the source supplier for this part. This will be displayed to the user.")]
		public string SupplierName
		{
			get => selectedLocation.SupplierName;
			set => selectedLocation.SupplierName = value;
		}

		[DescriptionAttribute("Description of Shipping Cost. (Optional)")]
		public string ShippingDescription
		{
			get => selectedLocation.ShippingDescription;
			set => selectedLocation.ShippingDescription = value;
		}

		[DescriptionAttribute("Shipping cost for this part. (Optional - defaults to 0)")]
		public decimal ShippingCost
		{
			get => selectedLocation.ShippingCost;
			set => selectedLocation.ShippingCost = value;
		}

		[DescriptionAttribute("The total number of quantity available")]
		public decimal QuantityAvailable
		{
			get { return SelectedLocation.QuantityAvailable; }
			set
			{
				SelectedLocation.QuantityAvailable = value;
			}
		}

		[DescriptionAttribute("The availability status for this Price Check part (like: \"In-Stock\" or \"Partial\")")]
		public string Availability
		{
			get
			{
				string availability;

				if (!Found)
				{
					availability = "Not Found";
				}
				else if (QuantityAvailable >= QuantityRequested)
				{
					availability = "In-Stock";
				}
				else if (QuantityAvailable > Decimal.Zero)
				{
					availability = "Partial";
				}
				else
				{
					availability = "None";
				}

				return availability;
			}
		}

		public int GridIndex { get; set; }

		public bool HasAlternateParts => AlternateParts.Count != 0;

		public bool HasAlternateLocations => Locations.Count > 1;
	}

    internal class OrderPart : ICartOrderedPart, IExtendedOrderPart
	{
		public OrderPart()
		{
			PartNumber = string.Empty;
			ManufacturerLineCode = string.Empty;
			ManufacturerName = string.Empty;
			Description = string.Empty;
			QuantityRequested = decimal.Zero;
			QuantityOrdered = decimal.Zero;
			Status = string.Empty;
			LocationName = string.Empty;
			LocationId = string.Empty;
			UnitList = decimal.Zero;
			UnitCost = decimal.Zero;
			UnitCore = decimal.Zero;
			UnitPrice = decimal.Zero;
			QuantityAvailable = decimal.Zero;
		    PartCategory = PartCategory.Unspecified;
		    Size = "";
		}

		[DescriptionAttribute("The status for the order part")]
		public string Status { get; set; }

		[DescriptionAttribute(
			"The part number for this order part.  Together with the ManufacturerLineCode, uniquely identifies an Order Part")]
		public string PartNumber { get; set; }

		[DescriptionAttribute("The manufacturer line code.  Together with the PartNumber, uniquely identifies an Order Part")]
		public string ManufacturerLineCode { get; set; }

		[DescriptionAttribute("The manufacturer name (like: \"Good Year\")")]
		public string ManufacturerName { get; set; }

		[DescriptionAttribute("A description of the Order Part (like: \"Front Caliper Bolt\")")]
		public string Description { get; set; }

		[DescriptionAttribute("The amount of quantity requested for the Order Part")]
		public decimal QuantityRequested { get; set; }

		[DescriptionAttribute("The number of units (quantity) requested for this part on the order")]
		public decimal QuantityOrdered { get; set; }

		[DescriptionAttribute("The name of the selected location for the delivery source")]
		public string LocationName { get; set; }

		[DescriptionAttribute("An identifier which uniquely identifies the location that represents the delivery source")]
		public string LocationId { get; set; }

		[DescriptionAttribute("The unit list price for the Order Part")]
		public decimal UnitList { get; set; }

		[DescriptionAttribute("The unit cost for the Order Part")]
		public decimal UnitCost { get; set; }

		[DescriptionAttribute("The core exchange cost for Order Parts that have a core charge")]
		public decimal UnitCore { get; set; }

		[DescriptionAttribute("The unit price for the Order Part")]
		public decimal UnitPrice { get; set; }

		[DescriptionAttribute("The total number of units available from the catalog (parts vendor)")]
		public decimal QuantityAvailable { get; set; }

	    [DescriptionAttribute("Indicates whether the part was found in the catalog (valid) or not (invalid)")]
		public bool Found { get; set; }

        [DescriptionAttribute("Uniquely identifies (within this catalog) the source supplier for this part.")]
        public string SupplierName { get; set; }

        [DescriptionAttribute("The metadata contains JSON supplier information.")]
        public string Metadata { get; set; }

        [DescriptionAttribute("Set if part is a tire, wheel, other...")]
        public PartCategory PartCategory { get; set; }

        [DescriptionAttribute("The size of the part.  This field is limited to 20 characters (maximum length).")]
        public string Size { get; set; }

        [DescriptionAttribute("Description of Shipping Cost. (Optional)")]
	    public string ShippingDescription { get; set; }

	    [DescriptionAttribute("Shipping cost for this part. (Optional - defaults to 0)")]
	    public decimal ShippingCost { get; set; }
	}
}
