using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mitchell1.Catalog.Framework.Interfaces;
using Moq;

namespace Mitchell1.Online.Catalog.Host.Test
{
	[TestClass]
	public class OnlineCatalogTests
	{
		private OnlineCatalog catalog;
		private Mock<IVendor> vendorMock;
		private Mock<IVehicle> vehicleMock;
		private Mock<IHostData> hostDataMock;
		private OnlineCatalogInformation onlineCatalogInformation;
		private Mock<ICatalogHostingForm> catalogHostingFormMock;

		[TestInitialize]
		public void Setup()
		{
			vendorMock = new Mock<IVendor>();
			vehicleMock = new Mock<IVehicle>();
			hostDataMock = new Mock<IHostData>();
			onlineCatalogInformation = new OnlineCatalogInformation {DisplayName = "Unit Test", ApiVersionLevel = 1};
			catalogHostingFormMock = new Mock<ICatalogHostingForm>();

			catalog = new OnlineCatalog(onlineCatalogInformation, vendorMock.Object, vehicleMock.Object, hostDataMock.Object, (x, y, z) => catalogHostingFormMock.Object);
		}

		[TestMethod]
		public void EmptyCartTest()
		{
			catalogHostingFormMock.Setup(x => x.ShowWebPage()).Returns(true);
			var success = catalog.GoShopping(out var cart);

			Assert.IsFalse(success);
			Assert.AreEqual(0, cart?.Count ?? 0);
		}
	}
}