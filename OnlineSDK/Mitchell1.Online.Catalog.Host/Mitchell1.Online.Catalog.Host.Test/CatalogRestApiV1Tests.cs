using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mitchell1.Catalog.Driver.Helpers;
using Mitchell1.Catalog.Framework.Common;
using Mitchell1.Online.Catalog.Host.API.v1;

namespace Mitchell1.Online.Catalog.Host.Test
{
	[TestClass]
	public class CatalogRestApiV1Tests
	{
		private Order order;
		private PriceCheck priceCheck;

		[TestInitialize]
		public void Initialize()
		{
			order = new Order();
			priceCheck = new PriceCheck();
		}

		[TestMethod]
		public void UpdatePriceCheck_null_response_returns_false_Test()
		{
			var success = CatalogRestApiV1.UpdatePriceCheck(null, priceCheck);

			Assert.IsFalse(success);
		}

		[TestMethod]
		public void UpdatePriceCheck_minimal_parts_returns_true_Test()
		{
			priceCheck.Parts.Add(new PriceCheckPart {PartNumber = "123"});
			priceCheck.Parts.Add(new PriceCheckPart {PartNumber = "456"});
			var response = Json.SerializeObject(new
			{
				Parts = new List<object>
				{
					new {Found = true, PartNumber = "123"},
					new {Found = false, PartNumber = "456"}
				}
			});
			var success = CatalogRestApiV1.UpdatePriceCheck(response, priceCheck);

			Assert.IsTrue(success);
		}

		[TestMethod]
		public void UpdateOrder_null_response_returns_false_Test()
		{
			var success = CatalogRestApiV1.UpdateOrder(null, order);

			Assert.IsFalse(success);
		}

		[TestMethod]
		[DataRow("", "0")]
		[DataRow(@",""ShippingCost"":7", "7")]
		[DataRow(@",""ShippingCost"":3.49", "3.49")]
		[DataRow(@",""ShippingCost"":0", "0")]
		[DataRow(@",""ShippingCost"":""3.45""", "3.45")]
		public void UpdateOrder_ShippingCost_Test(string shippingCost, string value)
		{
			order.Parts.Add(new OrderPart {PartNumber = "0"});
			var response = $@"{{
				""ConfirmationNumber"":""1234"",
				""Parts"":[
					{{""Found"":false,""PartNumber"":""0""{shippingCost}}}
				]}}";
			var success = CatalogRestApiV1.UpdateOrder(response, order);

			Assert.IsTrue(success);

			Assert.AreEqual(decimal.Parse(value), order.Parts[0].ShippingCost, "order.Parts[0].ShippingCost");
		}

		[TestMethod]
		public void Test()
		{
			order.Parts.Add(new OrderPart {Description = "Test1"});

			Assert.AreEqual("Test1", order.Parts[0].Description);

			Assert.AreEqual("Test1", ((IExtendedOrder)order).Parts[0].Description);
		}
	}
}