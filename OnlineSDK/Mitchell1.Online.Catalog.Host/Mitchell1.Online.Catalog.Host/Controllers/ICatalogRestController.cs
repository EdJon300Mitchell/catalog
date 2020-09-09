using System;
using System.Threading;
using System.Threading.Tasks;
using Mitchell1.Catalog.Framework.Common;
using Mitchell1.Catalog.Framework.Interfaces;
using Mitchell1.Online.Catalog.Host.Orders;
using Mitchell1.Online.Catalog.Host.TransferObjects;

namespace Mitchell1.Online.Catalog.Host.Controllers
{
	public interface ICatalogRestController : IDisposable
	{
		Task PriceCheck(IHostData hostData, IVendor vendor, IPriceCheck priceCheck, IVehicle vehicle, Logger logger, CancellationToken cancellationToken);

		Task<OrderResponse> OrderParts(IHostData hostData, IVendor vendor, OrderRequest order, IVehicle vehicle, Logger logger, CancellationToken cancellationToken);

		Task<TrackingRequestResponse> GetOrderTracking(IHostData hostData, IVendor vendor, string orderTrackingNumber, Logger logger, CancellationToken cancellationToken);
	}
}