using System.Collections.Generic;
using System.ComponentModel;
using Mitchell1.Catalog.Framework.Interfaces;
using Mitchell1.Online.Catalog.Host;

namespace Mitchell1.Catalog.Driver.Helpers
{
	internal class PriceCheckAlternatePart : IExtendedPriceCheckAlternatePart
	{
		private readonly LocationList locations = new LocationList();

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

		public void AddLocation(ILocation location) => locations.Add(location);

		public IList<Location> Locations => locations;

		IList<ILocation> IPriceCheckAlternatePart.Locations => locations;

		public ILocation NewLocation() => new Location();

		[DescriptionAttribute("The metadata contains JSON supplier information.")]
		public string Metadata { get; set; }
	}
}
