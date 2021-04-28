using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mitchell1.Online.Catalog.Host.API.v1;
using Mitchell1.Online.Catalog.Host.TransferObjects;
using Newtonsoft.Json;

namespace Mitchell1.Online.Catalog.Host.Test
{
	[TestClass]
	public class HandleShoppingCartTests
	{
		private ExpandoList onlineCart;

		[TestInitialize]
		public void Initialize()
		{
			onlineCart = new ExpandoList();
		}

		[TestMethod]
		public void Null_onlineCart_returns_null_ShoppingCart_Test()
		{
			onlineCart = null;

			var cart = GoShoppingV1.HandleShoppingCart(onlineCart);

			Assert.IsNull(cart);
		}

		[TestMethod]
		public void Empty_onlineCart_does_not_change_Shopping_cart_Test()
		{
			onlineCart = new ExpandoList();

			var cart = GoShoppingV1.HandleShoppingCart(onlineCart);

			Assert.AreEqual(0, cart.Count);
		}

		[TestMethod]
		public void MinimalPartItem_is_translated_correctly_Test()
		{
			onlineCart.Add(new MinimalPartItem());

			var cart = GoShoppingV1.HandleShoppingCart(onlineCart);

			Assert.AreEqual(1, cart.Count);
			MinimalPartItem.Verify((PartItem)cart[0]);
		}

		[TestMethod]
		public void FullPartItem_is_translated_correctly_Test()
		{
			onlineCart.Add(new MinimalPartItem
			{
				{"PartCategory", "Tire"},
				{"SupplierName", "Test SupplierId"},
				{"Metadata", "Test Metadata"},
				{"ShippingDescription", "Test ShippingDescription"},
				{"ShippingCost", "3.50"},
			});

			var cart = GoShoppingV1.HandleShoppingCart(onlineCart);

			Assert.AreEqual(1, cart.Count);
			var partItem = (PartItem)cart[0];
			Assert.AreEqual(PartCategory.Tire, partItem.PartCategory, "PartCategory");
			Assert.AreEqual("Test SupplierId", partItem.SupplierName, "SupplierName");
			Assert.AreEqual("Test Metadata", partItem.Metadata, "Metadata");
			Assert.AreEqual("Test ShippingDescription", partItem.ShippingDescription, "ShippingDescription");
			Assert.AreEqual(3.50m, partItem.ShippingCost, "ShippingCost");
		}

		[TestMethod]
		public void OrderItem_with_no_parts_is_translated_correctly_Test()
		{
			onlineCart.Add(new OrderItem {{"Parts", new ExpandoList()}});

			var cart = GoShoppingV1.HandleShoppingCart(onlineCart);

			Assert.AreEqual(0, cart.Count);
		}

		[TestMethod]
		public void OrderItem_with_minimal_OrderPart_is_translated_correctly_Test()
		{
			onlineCart.Add(new MinimalOrderItem {
				{"Parts", new ExpandoList {new MinimalOrderPart()}}
			});

			var cart = GoShoppingV1.HandleShoppingCart(onlineCart);

			Assert.AreEqual(1, cart.Count);
			Assert.AreEqual(1, cart.OfType<ShoppingCartOrder>().Count());
			var order = (ShoppingCartOrder)cart[0];
			MinimalOrderItem.Verify(order);
			Assert.AreEqual(1, order.Parts.Count);
			var part = order.Parts[0];
			MinimalOrderPart.Verify(part);
		}

		[TestMethod]
		public void OrderItem_with_full_OrderPart_is_translated_correctly_Test()
		{
			onlineCart.Add(new OrderItem
			{
				{"OrderMessage", "I'm messing with you" },
				{"ConfirmationNumber", "ABC123" },
				{"DeliveryOptions", "WillCall" },
				{"TrackingNumber", "XXX456" },
				{"Parts", new ExpandoList {
					new MinimalOrderPart
					{
						{"PartCategory", "Wheel"},
						{"SupplierName", "Supplier2"},
						{"Metadata", "Test Metadata 2"},
						{"ShippingDescription", "Test Ship"},
						{"ShippingCost", "2.99"},
						{"Size", "15in" }
					}
				}},
			});

			var cart = GoShoppingV1.HandleShoppingCart(onlineCart);

			Assert.AreEqual(1, cart.Count);
			Assert.AreEqual(1, cart.OfType<ShoppingCartOrder>().Count());

			var order = (ShoppingCartOrder)cart[0];
			Assert.AreEqual("I'm messing with you", order.OrderMessage, "OrderMessage");
			Assert.AreEqual("ABC123", order.ConfirmationNumber, "ConfirmationNumber");
			Assert.AreEqual("WillCall", order.DeliveryOptions, "DeliveryOptions");
			Assert.AreEqual("XXX456", order.TrackingNumber, "TrackingNumber");

			Assert.AreEqual(1, order.Parts.Count);
			var part = order.Parts[0];
			Assert.AreEqual(PartCategory.Wheel, part.PartCategory, "PartCategory");
			Assert.AreEqual("Supplier2", part.SupplierName, "SupplierName");
			Assert.AreEqual("Test Metadata 2", part.Metadata, "Metadata");
			Assert.AreEqual("Test Ship", part.ShippingDescription, "ShippingDescription");
			Assert.AreEqual(2.99m, part.ShippingCost, "ShippingCost");
			Assert.AreEqual("15in", part.Size, "Size");
		}

		[TestMethod]
		public void OrderItem_with_misspelled_DeliveryOptions_Test()
		{
			onlineCart.Add(new MinimalOrderItem {
				{"DeliveryOptions", "Deliver"},
				{"Parts", new ExpandoList {new MinimalOrderPart()}}
			});

			var cart = GoShoppingV1.HandleShoppingCart(onlineCart);

			Assert.AreEqual(1, cart.Count);
			Assert.AreEqual(1, cart.OfType<ShoppingCartOrder>().Count());
			var order = (ShoppingCartOrder)cart[0];
			Assert.AreEqual("Deliver", order.DeliveryOptions, "DeliveryOptions");
		}

		[TestMethod]
		[DataRow("None", null, "0")]
		[DataRow("ShippingCost", null, "0")]
		[DataRow("ShippingCost", 7, "7")]
		[DataRow("ShippingCost", 3.49, "3.49")]
		[DataRow("ShippingCost", 0, "0")]
		[DataRow("ShippingCost", "3.45", "3.45")]
		public void UpdateOrder_ShippingCost_Test(string name, object value, string expectedValue)
		{
			onlineCart.Add(new MinimalOrderItem {
				{"DeliveryOptions", "Deliver"},
				{"Parts", new ExpandoList {new MinimalOrderPart {{name, value}}}}
			});

			var cart = GoShoppingV1.HandleShoppingCart(onlineCart);

			var order = (ShoppingCartOrder)cart[0];
			Assert.AreEqual(decimal.Parse(expectedValue), order.Parts[0].ShippingCost, "order.Parts[0].ShippingCost");
		}

		[TestMethod]
		public void SerializeCartTest()
		{
			var shoppingCart = new ShoppingCart
			{
				new PartItem
				{
					Description = "Part Description", IsTire = true, PartCategory = PartCategory.Tire, Size = "150"
				},
				new LaborItem
				{
					Description = "Labor Description", Hours = 3, Price = 150M
				},
				new NoteItem
				{
					Note = "Some Note"
				},
				new ShoppingCartOrder
				{
					Parts = new List<ShoppingCartOrderPart>
					{
						new ShoppingCartOrderPart
						{
							Description = "Some Part",
							PartNumber = "TX345",
							PartCategory = PartCategory.Wheel
						}
					},
					ConfirmationNumber = "12345"
				}
			};

			var cartJson = JsonConvert.SerializeObject(shoppingCart, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

			var deserializedCart = GoShoppingV1.HandleShoppingCart(cartJson);

			Assert.IsNotNull(deserializedCart);
			Assert.AreEqual(4, deserializedCart.Count);
			var items = deserializedCart;
			Assert.IsInstanceOfType(items, typeof(List<CartItem>));  // deserializes to a list
			Assert.IsInstanceOfType(items[0], typeof(PartItem));
			Assert.IsInstanceOfType(items[1], typeof(LaborItem));
			Assert.IsInstanceOfType(items[2], typeof(NoteItem));
			Assert.IsInstanceOfType(items[3], typeof(ShoppingCartOrder));
		}
	}

	public class ExpandoList : List<object> 
	{
		public void Add(Item cartItem)
		{
			var cartItemDictionary = (IDictionary<string, object>)new ExpandoObject();
			foreach (var property in cartItem)
			{
				cartItemDictionary.Add(property.Key, property.Value);
			}
			Add(cartItemDictionary);
		}

		public class Item : Dictionary<string, object> {}
	}

	public class MinimalPartItem : ExpandoList.Item
	{
		public MinimalPartItem()
		{
			Add("type", "IPartItem2");
			Add("PartNumber", "123");
			Add("ManufacturerLineCode", "ABC");
			Add("ManufacturerName", "A Big Company");
			Add("Description", "Thingamabob");
			Add("UnitList", null);
			Add("UnitCost", null);
			Add("UnitCore", null);
			Add("Quantity", null);
			Add("IsTire", false);
			Add("Size", null);
		}

		public static void Verify(PartItem part)
		{
			Assert.IsNotNull(part);
			Assert.AreEqual("123", part.PartNumber, "PartNumber");
			Assert.AreEqual("ABC", part.ManufacturerLineCode, "ManufacturerLineCode");
			Assert.AreEqual("A Big Company", part.ManufacturerName, "ManufacturerName");
			Assert.AreEqual("Thingamabob", part.Description, "Description");
			Assert.AreEqual(0, part.UnitList, "UnitList");
			Assert.AreEqual(0, part.UnitCost, "UnitCost");
			Assert.AreEqual(0, part.UnitCore, "UnitCore");
			Assert.AreEqual(0, part.Quantity, "Quantity");
			Assert.IsFalse(part.IsTire, "IsTire");
			Assert.AreEqual(null, part.Size, "Size");
			Assert.AreEqual(PartCategory.Unspecified, part.PartCategory, "PartCategory");
			Assert.AreEqual(null, part.SupplierName, "SupplierName");
			Assert.AreEqual(null, part.Metadata, "Metadata");
			Assert.AreEqual(null, part.ShippingDescription, "ShippingDescription");
			Assert.AreEqual(0, part.ShippingCost, "ShippingCost");
		}
	}

	public class OrderItem : ExpandoList.Item
	{
		public OrderItem() => Add("type", "IOrder");
	}

	public class MinimalOrderItem : OrderItem
	{
		public MinimalOrderItem()
		{
			Add("OrderMessage", "my message");
			Add("ConfirmationNumber", "12345");
		}

		public static void Verify(ShoppingCartOrder order)
		{
			Assert.IsNotNull(order);
			Assert.AreEqual("my message", order.OrderMessage, "OrderMessage");
			Assert.AreEqual("12345", order.ConfirmationNumber, "ConfirmationNumber");
			Assert.IsNull(order.DeliveryOptions, "DeliveryOptions");
			Assert.IsNull(order.TrackingNumber, "TrackingNumber");
		}
	}

	public class MinimalOrderPart : ExpandoList.Item
	{
		public MinimalOrderPart()
		{
			Add("Description", "Some part description");
			Add("LocationId", "123");
			Add("LocationName", "Somewhere");
			Add("ManufacturerLineCode", "RCC");
			Add("ManufacturerName", "Real Cool Company");
			Add("PartNumber", "AB44554");
			Add("QuantityRequested", "");
			Add("QuantityOrdered", "");
			Add("QuantityAvailable", "");
			Add("Status", "Good");
			Add("UnitList", "");
			Add("UnitCost", "");
			Add("UnitCore", "");
		}

		public static void Verify(ShoppingCartOrderPart part)
		{
			Assert.IsNotNull(part);
			Assert.AreEqual("Some part description", part.Description, "Description");
			Assert.IsTrue(part.Found, "Found");
			Assert.AreEqual("123", part.LocationId, "LocationId");
			Assert.AreEqual("Somewhere", part.LocationName, "LocationName");
			Assert.AreEqual("RCC", part.ManufacturerLineCode, "ManufacturerLineCode");
			Assert.AreEqual("Real Cool Company", part.ManufacturerName, "ManufacturerName");
			Assert.AreEqual("AB44554", part.PartNumber, "PartNumber");
			Assert.AreEqual(0, part.QuantityAvailable, "QuantityAvailable");
			Assert.AreEqual(0, part.QuantityOrdered, "QuantityOrdered");
			Assert.AreEqual(0, part.QuantityRequested, "QuantityRequested");
			Assert.AreEqual("Good", part.Status, "Status");
			Assert.AreEqual(0, part.UnitCore, "UnitCore");
			Assert.AreEqual(0, part.UnitCost, "UnitCost");
			Assert.AreEqual(0, part.UnitList, "UnitList");
			Assert.AreEqual(null, part.Size, "Size");
			Assert.AreEqual(PartCategory.Unspecified, part.PartCategory, "PartCategory");
			Assert.AreEqual(null, part.SupplierName, "SupplierName");
			Assert.AreEqual(null, part.Metadata, "Metadata");
			Assert.AreEqual(null, part.ShippingDescription, "ShippingDescription");
			Assert.AreEqual(0, part.ShippingCost, "ShippingCost");
		}
	}
}