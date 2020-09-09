using System;
using Mitchell1.Catalog.Framework.Interfaces;
using System.ComponentModel;

namespace Mitchell1.Catalog.Driver.Helpers
{
	internal class Location : ILocation
    {
		private string name;
		private const int maxNameLength = 21;

        public Location()
        {
            Name = string.Empty;
            Id = string.Empty;
            QuantityAvailable = decimal.Zero;
            UnitList = decimal.Zero;
            UnitCost = decimal.Zero;
            UnitCore = decimal.Zero;
        }

        public Location(string name, string id, decimal quantityAvailable, decimal unitList, decimal unitCost, 
            decimal unitCore)
        {
            Name = name;
            Id = id;
            QuantityAvailable = quantityAvailable;
            UnitList = unitList;
            UnitCost = unitCost;
            UnitCore = unitCore;
        }

		[DescriptionAttribute("The long name for the location (like: \"San Diego\")")]
        public string Name
        {
            get 
			{ 
				return name; 
			}
            set 
			{				
				name = !String.IsNullOrEmpty(value) && value.Length > maxNameLength
					? value.Substring(0, maxNameLength - 1) 
					: value;
			}
        }

		[DescriptionAttribute("An identifier which uniquely identifies this location")]
		public string Id { get; set; }

		[DescriptionAttribute("The available quantity (in-stock) for this location")]
		public decimal QuantityAvailable { get; set; }

		[DescriptionAttribute("The unit list price for a single quantity of the part")]
		public decimal UnitList { get; set; }

		[DescriptionAttribute("The unit cost for a single quantity of the part")]
		public decimal UnitCost { get; set; }

		[DescriptionAttribute("The core exchange cost for parts that require it")]
		public decimal UnitCore { get; set; }
    }
}
