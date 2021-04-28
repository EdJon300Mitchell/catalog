using Mitchell1.Catalog.Framework.Interfaces;

namespace Mitchell1.Online.Catalog.Host
{
	public interface IExtendedOrderPart : IOrderPart, ISupplierAndShipping, IMetadata {}
}