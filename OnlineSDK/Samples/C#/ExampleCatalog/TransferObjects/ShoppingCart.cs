using System.Collections.Generic;

namespace Mitchell1.Online.Catalog.Host.TransferObjects
{
	public class ShoppingCart : List<CartItem> {}

	public class CartItem
	{
		protected CartItem(string type) => this.type = type;
		public string type { get; }
	}

	public class PartItem : CartItem
	{
		public PartItem() : base("IPartItem2") {}
		public string PartNumber { get; set; }
		public string ManufacturerLineCode { get; set; }
		public string ManufacturerName { get; set; }
		public string Description { get; set; }
		public decimal UnitList { get; set; }
		public decimal UnitCost { get; set; }
		public decimal UnitCore { get; set; }
		public decimal Quantity { get; set; }
		public bool IsTire { get; set; }
		public string Size { get; set; }
		public string UpcCode { get; set; }
		public PartCategory PartCategory { get; set; }
		public string SupplierName { get; set; }
		public string Metadata { get; set; }
		public string ShippingDescription { get; set; }
		public decimal ShippingCost { get; set; }
	}

	public class NoteItem : CartItem
	{
		public NoteItem() : base("INoteItem") {}
		public string Note { get; set; }
	}

	public class LaborItem : CartItem
	{
		public LaborItem() : base("ILaborItem") {}
		public string Description { get; set; }
		public decimal Hours { get; set; }
		public decimal Price { get; set; }
	}

	public class ShoppingCartOrder : CartItem
	{
		public ShoppingCartOrder() : base("IOrder") {}
		public string OrderMessage { get; set; }
		public string DeliveryOptions { get; set; }		// For backwards compatibility this property must remain plural
		public string ConfirmationNumber { get; set; }
		public string TrackingNumber { get; set; }
		public IList<ShoppingCartOrderPart> Parts { get; set; }
	}

	public class ShoppingCartOrderPart
	{
		public const string type = "IOrderPart";
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
		public string Size { get; set; }
		public PartCategory PartCategory { get; set; }
		public string SupplierName { get; set; }
		public string Metadata { get; set; }
		public string ShippingDescription { get; set; }
		public decimal ShippingCost { get; set; }
	}

	public enum PartCategory
	{
		Unspecified,
		Tire,
		Wheel,
	}
}