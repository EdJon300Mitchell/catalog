using System.Collections.Generic;
using System.Linq;
using Mitchell1.Catalog.Framework.Interfaces;

namespace Mitchell1.Online.Catalog.Host
{
	public class OnlineCatalogInformation : ICatalogProperties
    {
        private readonly Dictionary<CatalogApiPart, string> urlParts = new Dictionary<CatalogApiPart, string>();

        public static int HostApiLevel { get; } = 3;

        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Identifier { get; set; }

        /// <summary>
        /// Minimum Version of Catalog API supported by this catalog.
        /// </summary>
        public int ApiVersionLevel { get; set; }

        /// <summary>
        /// Url of base API - Must be using HTTPS protocol. All other URL components are built off of this URL
        /// </summary>
        public string ApiBaseUrl { get; set; }

        public string GetAbsoluteUrl(CatalogApiPart part) => GetAbsoluteUrl(part, false);

		public string GetAbsoluteUrl(CatalogApiPart part, bool requiredNotNullEmpty)
		{
			var urlPart = this[part];
			if (requiredNotNullEmpty && string.IsNullOrWhiteSpace(urlPart))
				throw new CatalogConfigurationException($"{DisplayName} Missing {part}");

		    return $"{ApiBaseUrl}/{urlPart}";
	    }

		public string this[CatalogApiPart key]
        {
            get => urlParts.TryGetValue(key, out var foundKey) ? foundKey : "";
	        set => urlParts[key] = value;
        }

	    public IList<CatalogApiPart> GetUrlComponentKeys()
	    {
		    return urlParts.Keys.ToList();
	    }

		// Catalog API Methods
		public bool ShowsDeliverWillCall { get; set; }
		public bool AllowsBlankManufacturerCode { get; set; }
		public bool AllowsNotFoundPartsToBeOrdered { get; set; }
		public bool RequiresPriceCheck { get; set; }
		public bool SupportsAlternateLocations { get; set; }
		public bool SupportsAlternateParts { get; set; }
		public bool SupportsLocation { get; set; }
		public bool SupportsOrderMessage { get; set; }
		public bool SupportsPriceCheck { get; set; }

		// Additional Items specific to online
		public string SupportUrl { get; set; }
        public string SupportPhone { get; set; }
    }

    public enum CatalogApiPart
    {
        Icon,
        Setup,
        GoShopping,
        PriceCheck,
        PartsOrder,
		OrderTracking
    }
}
