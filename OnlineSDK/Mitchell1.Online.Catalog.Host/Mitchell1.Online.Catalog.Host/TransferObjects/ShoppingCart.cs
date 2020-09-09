using System.Collections.Generic;
using Mitchell1.Catalog.Framework.Interfaces;

namespace Mitchell1.Online.Catalog.Host.TransferObjects
{
	public class ShoppingCart
	{
		public List<ICartItem> Items { get; } = new List<ICartItem>();

		public List<ShoppingCartOrder> Orders { get; } = new List<ShoppingCartOrder>();

		public bool IsEmpty => Items.Count == 0 && Orders.Count == 0;
	}

	public class PartItem : IPartItem3
	{
		public string Description { get; set; }
		public string ManufacturerLineCode { get; set; }
		public string ManufacturerName { get; set; }
		public string PartNumber { get; set; }
		public decimal Quantity { get; set; }
		public decimal UnitCore { get; set; }
		public decimal UnitCost { get; set; }
		public decimal UnitList { get; set; }
		public string UpcCode { get; set; }
		public bool IsTire { get; set; }
		public string Size { get; set; }
		public PartCategory PartCategory { get; set; }
	}

	public class NoteItem : INoteItem
	{
		public string Note { get; set; }
	}

	public class LaborItem : ILaborItem
	{
		public string Description { get; set; }
		public decimal Hours { get; set; }
		public decimal Price { get; set; }
	}

	public class ShoppingCartOrder
	{
		/// <summary>
		/// Confirmation Number generated and set by the catalog used to verify the order was successful.
		/// </summary>
		public string ConfirmationNumber { set; get; }

		/// <summary>
		/// User entered message sent to the catalog along with the order.
		/// </summary>
		public string OrderMessage { get; set; }

		/// <summary>
		/// Optional Tracking Number (20 chars) set by catalog for tracking shipment/order status
		/// </summary>
		public string TrackingNumber { set; get; }

		/// <summary>
		/// User selected delivery option from the DeliveryMethod property on ICatalog.
		/// </summary>
		public string DeliveryOption { get; set; }

		/// <summary>
		/// An ordered list of parts the user requested for ordering.  These parts will be updated
		/// with the status once ordering is done.
		/// </summary>
		public List<ShoppingCartOrderPart> Parts { get; set; } = new List<ShoppingCartOrderPart>();
	}

	public class ShoppingCartOrderPart
	{
		/// <summary>
		/// Description for the part to order.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		///  Does the catalog recognizes this part? (Set by the catalog)
		/// </summary>
		public bool Found { get; set; }

		/// <summary>
		/// Id of the location (Set by the host application as returned from a PriceCheck)
		/// </summary>
		public string LocationId { get; set; }

		/// <summary>
		/// Name of the location. (Set by the host application as returned from a PriceCheck)
		/// </summary>
		public string LocationName { get; set; }

		/// <summary>
		/// Manufacturer Line Code of part to order. (Set by the host application)
		/// </summary>
		public string ManufacturerLineCode { get; set; }

		/// <summary>
		/// Manufacturer Name of part to order.
		/// </summary>
		public string ManufacturerName { get; set; }

		/// <summary>
		/// Part Number of part to order. (Set by the host application)
		/// </summary>
		public string PartNumber { get; set; }

		/// <summary>
		/// Quantity of this part item available at this location.
		/// (Set by the host application as returned from a PriceCheck)
		/// </summary>
		public decimal QuantityAvailable { get; set; }

		/// <summary>
		/// Quantity of part ordered. (Set by the catalog)
		/// </summary>
		public decimal QuantityOrdered { get; set; }

		/// <summary>
		/// Quantity of part requested to be ordered. (Set by the host application)
		/// </summary>
		public decimal QuantityRequested { get; set; }

		/// <summary>
		/// <para>Custom Status string to display in host application. (Set by the catalog)</para>
		/// <para>If this property is null, a calculated status is displayed by host application.</para>
		/// </summary>
		public string Status { get; set; }

		/// <summary>
		/// Cost of a single core item. (Set by the host application as returned from a PriceCheck)
		/// </summary>
		public decimal UnitCore { get; set; }

		/// <summary>
		/// Cost for a single unit of this item. (Set by the host application as returned from a PriceCheck)
		/// </summary>
		public decimal UnitCost { get; set; }

		/// <summary>
		/// Suggested price for a single unit of this item. (Set by the host application as returned from a PriceCheck)
		/// </summary>
		public decimal UnitList { get; set; }

		/// <summary>
		/// The size of the part.  This field is limited to 20 characters (maximum length).
		/// </summary>
		public string Size { get; set; }

		/// <summary>
		/// Set if if part is a tire, wheel, other...
		/// </summary>
		public PartCategory PartCategory { get; set; }
	}
}