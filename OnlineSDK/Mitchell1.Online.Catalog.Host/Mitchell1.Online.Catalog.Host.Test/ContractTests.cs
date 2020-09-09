using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mitchell1.Online.Catalog.Host.Orders;

namespace Mitchell1.Online.Catalog.Host.Test
{
	[TestClass]
	public class ContractTests
	{
		[TestMethod]
		public void StaticInterfacesStayConstant()
		{
			// Test ensures that interfaces used in data contracts do not change - would negatively affect integration with existing catalogs
			// Do not rename properties:
			ValidateProperty(nameof(TrackingRequestResponse), nameof(TrackingRequestResponse.ExternalTrackingUrl), "ExternalTrackingUrl");
			ValidateProperty(nameof(TrackingRequestResponse), nameof(TrackingRequestResponse.StatusDisplay), "StatusDisplay");
		}

		private void ValidateProperty(string className, string actualName, string expectedName)
		{
			Assert.IsTrue(actualName == expectedName, $"Integration API Changed! {className}.{expectedName} changed to: {actualName}");
		}
	}
}