using Nancy;

namespace ExampleCatalog.Api
{
	public class VendorSetupController : NancyModule
	{
		public VendorSetupController() : base("/View")
		{
			// Qualifier is available as query param (if already setup this catalog)
			Get["/vendorsetup"] = parameters => View["configuration.html", new {}];
		}
	}
}