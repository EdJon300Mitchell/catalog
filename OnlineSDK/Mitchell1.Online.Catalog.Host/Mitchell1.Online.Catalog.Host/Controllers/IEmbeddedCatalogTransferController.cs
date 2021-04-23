namespace Mitchell1.Online.Catalog.Host.Controllers
{
	public interface IEmbeddedCatalogTransferController : IEmbeddedCatalogController
	{
		int HttpResponseCode { get; }
	}
}