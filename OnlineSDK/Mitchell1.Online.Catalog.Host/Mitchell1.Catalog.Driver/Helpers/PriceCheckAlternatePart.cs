using System.Collections.Generic;
using Mitchell1.Catalog.Framework.Interfaces;
using System.ComponentModel;

namespace Mitchell1.Catalog.Driver.Helpers
{
	internal class PriceCheckAlternatePart : IPriceCheckAlternatePart
	{
		#region IPriceCheckAlternatePart Members

		[DescriptionAttribute("The individual status for this Alternate Part when doing the Price Check")]
		public string Status { get; set; }

		[DescriptionAttribute(
			"A part number for this Alternate Part.  Together with the ManufacturerLineCode, uniquely identifies a part")]
		public string PartNumber { get; set; }

		[DescriptionAttribute(
			"A manufacturer line code for this Alternate Part.  Together with the PartNumber, uniquely identifies a part")]
		public string ManufacturerLineCode { get; set; }

		[DescriptionAttribute("The manufacturer name for the Alternate Part (like: \"Good Year\")")]
		public string ManufacturerName { get; set; }

		[DescriptionAttribute("A description for this Alternate Part (like: \"Front Caliper Bolt\")")]
		public string Description { get; set; }

		public void AddLocation(ILocation location)
		{
			locations.Add(location);
		}

		public IList<ILocation> Locations
		{
			get { return locations; }
		}

		public ILocation NewLocation()
		{
			return new Location();
		}

		#endregion

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

		private readonly IList<ILocation> locations = new List<ILocation>();
		private readonly ILocation EmptyLocation = new Location();
		private ILocation selectedLocation;
	}
}
