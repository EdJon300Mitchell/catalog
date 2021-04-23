using System;
using Mitchell1.Catalog.Framework.Interfaces;

namespace Mitchell1.Online.Catalog.Host.API.v1
{
	public class VendorSetupV1 : CustomWebPageController
    {
		private readonly OnlineCatalogInformation onlineCatalogInformation;

		private string ConfigurationUrl => onlineCatalogInformation.GetAbsoluteUrl(CatalogApiPart.Setup);

		public VendorSetupV1(OnlineCatalogInformation catalogInformation) => onlineCatalogInformation = catalogInformation;

		protected override string Url => ConfigurationUrl + (!string.IsNullOrEmpty(Vendor?.Qualifier) ? "?Qualifier=" + Uri.EscapeDataString(Vendor.Qualifier) : "");
        protected override string ActionLabel { get; } = "save";

		public IVendor Vendor { get; set; }
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public IHostData HostData { get; set; }

        protected override bool Action(object[] objects)
        {
			if (objects != null && objects.Length > 1 && objects[1] is string)
			{
				Vendor.Qualifier = (string)objects[1];
			}

			return true;
        }
    }
}
