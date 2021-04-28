using System;
using Mitchell1.Catalog.Framework.Interfaces;

namespace Mitchell1.Online.Catalog.Host
{
	[Serializable]
	public class Location : ILocation, ISupplierAndShipping
	{
		private string name;
		private const int maxNameLength = 21;

		public Location()
		{
			Name = string.Empty;
			Id = string.Empty;
		}

		public Location(Location location)
		{
			Name = location.Name;
			Id = location.Id;
			QuantityAvailable = location.QuantityAvailable;
			UnitList = location.UnitList;
			UnitCost = location.UnitCost;
			UnitCore = location.UnitCore;
			SupplierName = location.SupplierName;
			ShippingDescription = location.ShippingDescription;
			ShippingCost = location.ShippingCost;
		}

		public string Name
		{
			get => name;
			set => name = !string.IsNullOrEmpty(value) && value.Length > maxNameLength
							? value.Substring(0, maxNameLength - 1)
							: value;
		}

		public string Id { get; set; }
		public decimal QuantityAvailable { get; set; }
		public decimal UnitList { get; set; }
		public decimal UnitCost { get; set; }
		public decimal UnitCore { get; set; }
		public string SupplierName { get; set; }
		public string ShippingDescription { get; set; }
		public decimal ShippingCost { get; set; }
	}
}