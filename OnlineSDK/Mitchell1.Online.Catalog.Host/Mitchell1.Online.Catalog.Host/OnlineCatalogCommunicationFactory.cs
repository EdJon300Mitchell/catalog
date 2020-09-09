using System;
using Mitchell1.Catalog.Framework.Interfaces;
using Mitchell1.Online.Catalog.Host.API.v1;
using Mitchell1.Online.Catalog.Host.Controllers;
using Mitchell1.Online.Catalog.Host.TransferObjects;

namespace Mitchell1.Online.Catalog.Host
{
    internal static class OnlineCatalogCommunicationFactory
    {
        /// <summary>
        /// Returns a generic controller capable of saving/loading configuration for Vendor Setup
        /// </summary>
        internal static IEmbeddedCatalogController GetCatalogVendorSetupController(OnlineCatalogInformation catalogInformation, IVendor vendor, IHostData hostData)
        {
            if (catalogInformation == null)
                throw new ArgumentNullException(nameof(catalogInformation));

            if (catalogInformation.ApiVersionLevel == 1)
                return new VendorSetupV1(catalogInformation)
                {
                    Vendor = vendor,
                    HostData = hostData
                };

            throw new CatalogException($"Unsupported Catalog API Level: {catalogInformation.ApiVersionLevel}");
        }

        /// <summary>
        /// Returns a controller suitable for transferring parts - aka go shopping
        /// </summary>
        internal static IEmbeddedCatalogTransferController GetEmbeddedCatalogTransferController(OnlineCatalogInformation catalogInformation, ShoppingCart cart, IVendor vendor, IHostData hostData, IVehicle vehicle)
        {
            if (catalogInformation == null)
                throw new ArgumentNullException(nameof(catalogInformation));

            if (catalogInformation.ApiVersionLevel == 1)
                return new GoShoppingV1(catalogInformation)
                {
                    Cart = cart,
                    Vendor = vendor,
                    HostData = hostData,
                    Vehicle = vehicle
                };

            throw new CatalogException($"Unsupported Catalog API Level: {catalogInformation.ApiVersionLevel}");
        }

        /// <summary>
        /// Used for API based calls. Price Check / Order Parts
        /// </summary>
        internal static ICatalogRestController GetRestApiController(OnlineCatalogInformation catalogInformation)
        {
            if (catalogInformation == null)
                throw new ArgumentNullException(nameof(catalogInformation));

            if (catalogInformation.ApiVersionLevel == 1)
                return new CatalogRestApiV1(catalogInformation);

            throw new CatalogException($"Unsupported Catalog API Level: {catalogInformation.ApiVersionLevel}");
        }
    }
}
