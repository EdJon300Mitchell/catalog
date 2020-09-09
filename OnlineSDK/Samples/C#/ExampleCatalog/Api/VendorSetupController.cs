using System.Runtime.CompilerServices;
using ExampleCatalog.DataLayer;
using Nancy;

namespace ExampleCatalog.Api
{
	public class ShoppingController : Nancy.NancyModule
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

		private IVendor GetVendorFromQuery()
		{
			return new IVendor
			{
				Qualifier = Request.Query["Qualifier"]
			};
		}
	}
}