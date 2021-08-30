using System;
using Mitchell1.Catalog.Framework.Common;
using Mitchell1.Catalog.Framework.Interfaces;
using Mitchell1.Online.Catalog.Host.API.v1;
using Mitchell1.Online.Catalog.Host.Controllers;

namespace Mitchell1.Online.Catalog.Host
{
	internal static class OnlineCatalogCommunicationFactory
    {
        /// <summary>
        /// Returns a generic controller capable of saving/loading configuration for Vendor Setup
        /// </summary>
        internal static IEmbeddedCatalogController GetCatalogVendorSetupController(OnlineCatalogInformation catalogInformation, IVendor vendor, IHostData hostData)
        {
	        CheckApiLevel(catalogInformation);

            return new VendorSetupV1(catalogInformation)
            {
                Vendor = vendor,
                HostData = hostData
            };
        }

        /// <summary>
        /// Returns a controller suitable for transferring parts - aka go shopping
        /// </summary>
        internal static GoShoppingV1 GetEmbeddedCatalogTransferController(OnlineCatalogInformation catalogInformation, IVendor vendor, IHostData hostData, IVehicle vehicle)
        {
	        CheckApiLevel(catalogInformation);

            return new GoShoppingV1(catalogInformation)
            {
                Vendor = vendor,
                HostData = hostData,
                Vehicle = vehicle
            };
        }

        /// <summary>
        /// Used for API based calls. Price Check / Order Parts
        /// </summary>
        internal static ICatalogRestController GetRestApiController(OnlineCatalogInformation catalogInformation, Logger logger, NewCatalogHostingForm newHostingForm)
        {
	        CheckApiLevel(catalogInformation);

			return new CatalogRestApiV1(catalogInformation, logger, newHostingForm);
        }

        internal static bool IsSupportedApiLevel(int apiVersionLevel) => apiVersionLevel >= 1 && apiVersionLevel <= OnlineCatalogInformation.HostApiLevel;

        private static void CheckApiLevel(OnlineCatalogInformation catalogInformation)
        {
	        if (catalogInformation == null)
		        throw new ArgumentNullException(nameof(catalogInformation));

	        int apiVersionLevel = catalogInformation.ApiVersionLevel;
            if (!IsSupportedApiLevel(apiVersionLevel))
	            throw new CatalogException($"Unsupported Catalog API Level: {apiVersionLevel}");
        }
    }
}
