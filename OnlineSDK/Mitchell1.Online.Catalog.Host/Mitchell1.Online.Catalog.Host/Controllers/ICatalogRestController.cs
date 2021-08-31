using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Mitchell1.Catalog.Framework.Interfaces;
using Mitchell1.Online.Catalog.Host.TransferObjects;

namespace Mitchell1.Online.Catalog.Host.Controllers
{
	internal interface ICatalogRestController : IDisposable
	{
		bool PriceCheck(IHostData hostData, IVendor vendor, IExtendedPriceCheck priceCheck, IVehicle vehicle);

		OrderPartsResponse OrderParts(IHostData hostData, IVendor vendor, IExtendedOrder order, IVehicle vehicle);

		Task<TrackingResponse> GetOrderTracking(IHostData hostData, IVendor vendor, string orderTrackingNumber, CancellationToken cancellationToken);
	}
}