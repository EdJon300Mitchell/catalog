using Mitchell1.Catalog.Framework.Interfaces;
using Mitchell1.Online.Catalog.Host.TransferObjects;

namespace Mitchell1.Online.Catalog.Host.Controllers
{
	public interface IEmbeddedCatalogTransferController : IEmbeddedCatalogController
	{
		ShoppingCart Cart { get; set; }
		IVehicle Vehicle { get; set; }
		int HttpResponseCode { get; }
	}
}