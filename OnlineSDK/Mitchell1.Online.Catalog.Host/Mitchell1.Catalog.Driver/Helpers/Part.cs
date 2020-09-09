using System;
using System.Collections.Generic;
using Mitchell1.Catalog.Framework.Interfaces;
using System.ComponentModel;

namespace Mitchell1.Catalog.Driver.Helpers
{
	internal class PriceCheckPart : IPriceCheckPart
    {
		private readonly IList<ILocation> locations = new List<ILocation>();
		private ILocation selectedLocation;

		private readonly ILocation EmptyLocation = new Location();

        #region Constructors

        public PriceCheckPart() 
        {
        	Found = false;
            PartNumber = string.Empty;
            ManufacturerLineCode = string.Empty;
            ManufacturerName = string.Empty;
            Description = string.Empty;
            QuantityRequested = decimal.Zero;
			QuantityOrdered = decimal.Zero;
			AlternateParts = new List<IPriceCheckAlternatePart>();
            GridIndex = 0;
            Status = string.Empty;
        }

        #endregion

        #region IPriceCheckPart Members

		[DescriptionAttribute("Was the part found in the catalog?")]
		public bool Found { get; set; }

		[DescriptionAttribute("The status for the part as returned by the catalog")]
		public string Status { get; set; }

		[DescriptionAttribute(
			"The part number for this Price Check part.  Together with the ManufacturerLineCode, it uniquely identifies a part")]
		public string PartNumber { get; set; }

		[DescriptionAttribute(
			"The manufacturer line code from the catalog.  Together with the PartNumber, it uniquely identifies a part")]
		public string ManufacturerLineCode { get; set; }

		[DescriptionAttribute("The manufacturer name for the Price Check part (like: \"Good Year\")")]
		public string ManufacturerName { get; set; }

		[DescriptionAttribute("A description for the Price Check part")]
		public string Description { get; set; }

		[DescriptionAttribute("The number of units requested in the Price Check request")]
		public decimal QuantityRequested { get; set; }

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

		[DescriptionAttribute("The unit price for a single quantity of the Price Check part")]
		public decimal UnitPrice { get; set; }

		[DescriptionAttribute("A list of alternate locations that this part is available from")]
        public IList<ILocation> Locations
        {
            get { return locations; }
        }

		[DescriptionAttribute("A list of alternate parts which may be substituted for this Price Check part")]
		public IList<IPriceCheckAlternatePart> AlternateParts { get; set; }

		public ILocation NewLocation()
        {
            return new Location();
        }

        public void AddLocation(ILocation location)
        {
            locations.Add(location);
        }

        public IPriceCheckAlternatePart NewAlternatePart()
        {
            return new PriceCheckAlternatePart();
        }

		public void AddAlternatePart(IPriceCheckAlternatePart partItem)
        {
            AlternateParts.Add(partItem);
        }

		#endregion

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

		[DescriptionAttribute("The location selected from the list of available locations")]
		public ILocation SelectedLocation
		{
			get
			{
				if (selectedLocation == null || selectedLocation == EmptyLocation)
				{
					if (Locations.Count > 0)
					{
						selectedLocation = Locations[0];
					}
					else
					{
						if (selectedLocation == null)
						{
							selectedLocation = EmptyLocation;
						}
					}
				}
				return selectedLocation;
			}
			set { selectedLocation = value; }
		}

		public int GridIndex { get; set; }

		public bool HasAlternateParts
		{
			get { return AlternateParts.Count != 0; }
		}

		public bool HasAlternateLocations
		{
			get { return Locations.Count > 1; }
		}
	}

    public class OrderPart : ICartOrderedPart
	{
		#region Constructors

		public OrderPart()
		{
			PartNumber = string.Empty;
			ManufacturerLineCode = string.Empty;
			ManufacturerName = string.Empty;
			Description = string.Empty;
			QuantityRequested = decimal.Zero;
			QuantityOrdered = decimal.Zero;
			GridIndex = 0;
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

		#endregion

		#region IOrderPart Members

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

		[Browsable(false)]
		public int GridIndex { get; set; }

	    [DescriptionAttribute("Indicates whether the part was found in the catalog (valid) or not (invalid)")]
		public bool Found { get; set; }

        public PartCategory PartCategory { get; set; }

        public string Size { get; set; }

	    #endregion
    }
}
