using System;
using Mitchell1.Catalog.Driver.Helpers;
using Mitchell1.Catalog.Framework.Interfaces;
using Mitchell1.Online.Catalog.Host;

namespace Mitchell1.Catalog.Driver.Controls
{
	internal static class VendorHelper
	{
		public static void EnsureQualifierValid(IOnlineCatalogInfo catalogInfo, Vendor vendor)
		{
			if (vendor.Catalog != catalogInfo.DisplayName)
			{
				vendor.Qualifier = String.Empty;
			}
		}

		public static IOnlineCatalog GetCatalog(IOnlineCatalogInfo catalogInfo, Vendor vendor, IVehicle vehicle, IHostData hostData)
		{
            Vendor newVendor = new Vendor
            {
                Catalog = vendor.Catalog,
                Code = vendor.Code,
                Name = vendor.Name,
                Qualifier = vendor.Qualifier
            };
            EnsureQualifierValid(catalogInfo, newVendor);
            return catalogInfo.GetOnlineCatalog(newVendor, vehicle, hostData);
		}
	}
}
