using Mitchell1.Online.Catalog.Host.TransferObjects;
using Nancy;

namespace ExampleCatalog.Api
{
	public class ShoppingController : NancyModule
	{
		public ShoppingController() : base("/View")
		{
			Get["/goshopping"] = parameters =>
			{
				// Qualifier=> Stored data passed back
				if (!PartsController.CheckVendor(GetVendorFromQuery(), out _))
					return PartsController.ServerError("Invalid Login", HttpStatusCode.Forbidden);

				// Other parameters
				// Vehicle Query params:
				//		Vin,Year,Make,Model,SubModel,Transmission,Engine,DriveType,Brake,Gvw,Body
				//		AcesId,AcesBaseId,AcesEngineId,AcesEngineBaseId,AcesEngineConfigId,AcesSubmodelId

				return View["shopping.html", new {}];
			};
		}

		private Vendor GetVendorFromQuery()
		{
			return new Vendor
			{
				Qualifier = Request.Query["Qualifier"]
			};
		}
	}
}