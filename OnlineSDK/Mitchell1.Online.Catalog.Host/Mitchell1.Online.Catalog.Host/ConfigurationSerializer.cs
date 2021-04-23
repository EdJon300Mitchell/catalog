using System;
using System.Linq;
using System.Xml.Linq;
using static Mitchell1.Online.Catalog.Host.OnlineCatalogCommunicationFactory;

namespace Mitchell1.Online.Catalog.Host
{
    public static class ConfigurationSerializer
    {
        public class SerializeException : Exception
        {
            public SerializeException(string issue) : base(issue)
            {
            }
        }

        public static OnlineCatalogInformation FromElement(XElement element)
        {
            var catalog = new OnlineCatalogInformation();

            catalog.Identifier = element.Attribute("Identifier")?.Value ?? "";
            catalog.DisplayName = element.Attribute("DisplayName")?.Value ?? "";
            catalog.Description = element.Attribute("Description")?.Value ?? "";
            catalog.ApiVersionLevel = int.Parse(element.Attribute("ApiVersionLevel")?.Value ?? "1");
            catalog.ApiBaseUrl = element.Attribute("ApiBaseUrl")?.Value ?? "";

            catalog.AllowsBlankManufacturerCode = (bool)element.Attribute("AllowsBlankManufacturerCode");
            catalog.AllowsNotFoundPartsToBeOrdered = (bool)element.Attribute("AllowsNotFoundPartsToBeOrdered");
            catalog.RequiresPriceCheck = (bool)element.Attribute("RequiresPriceCheck");
            catalog.SupportsAlternateLocations = (bool)element.Attribute("SupportsAlternateLocations");
            catalog.SupportsAlternateParts = (bool)element.Attribute("SupportsAlternateParts");
            catalog.SupportsLocation = (bool)element.Attribute("SupportsLocation");
            catalog.SupportsOrderMessage = (bool)element.Attribute("SupportsOrderMessage");
            catalog.SupportsPriceCheck = (bool)element.Attribute("SupportsPriceCheck");
            catalog.ShowsDeliverWillCall = (bool)element.Attribute("ShowsDeliverWillCall");
            catalog.SupportUrl = element.Attribute("SupportUrl")?.Value ?? "";
            catalog.SupportPhone = element.Attribute("SupportPhone")?.Value ?? "";

	        var expectedPrefix = "CatalogApiPart.";
	        var attributes = element.Attributes().Where(a => a.Name.LocalName.StartsWith(expectedPrefix)).ToList();
	        foreach (var a in attributes)
	        {
				var name = a.Name.LocalName.Substring(expectedPrefix.Length);
		        var value = a.Value;

		        if (Enum.TryParse<CatalogApiPart>(name, true, out var result))
			        catalog[result] = value;
			}


			string message;
            if (!ValidateConfiguration(catalog, out message))
                throw new SerializeException(message);

            return catalog;
        }

        public static XElement ToElement(OnlineCatalogInformation catalog)
        {
            string message;
            if (!ValidateConfiguration(catalog, out message))
            {
                throw new SerializeException(message);
            }

            return
                new XElement("catalog",
                    new XAttribute("Identifier", catalog.Identifier),
                    new XAttribute("DisplayName", catalog.DisplayName),
                    new XAttribute("Description", catalog.Description),
                    new XAttribute("ApiVersionLevel", catalog.ApiVersionLevel),
                    new XAttribute("ApiBaseUrl", catalog.ApiBaseUrl),
					from key in catalog.GetUrlComponentKeys() select
						new XAttribute($"CatalogApiPart.{key}", catalog[key]),
					new XAttribute("AllowsBlankManufacturerCode", catalog.AllowsBlankManufacturerCode),
                    new XAttribute("AllowsNotFoundPartsToBeOrdered", catalog.AllowsNotFoundPartsToBeOrdered),
                    new XAttribute("RequiresPriceCheck", catalog.RequiresPriceCheck),
                    new XAttribute("SupportsAlternateLocations", catalog.SupportsAlternateLocations),
                    new XAttribute("SupportsAlternateParts", catalog.SupportsAlternateParts),
                    new XAttribute("SupportsLocation", catalog.SupportsLocation),
                    new XAttribute("SupportsOrderMessage", catalog.SupportsOrderMessage),
                    new XAttribute("SupportsPriceCheck", catalog.SupportsPriceCheck),
                    new XAttribute("ShowsDeliverWillCall", catalog.ShowsDeliverWillCall),
                    new XAttribute("SupportUrl", catalog.SupportUrl),
                    new XAttribute("SupportPhone", catalog.SupportPhone)
                );
        }

        public static bool ValidateConfiguration(OnlineCatalogInformation catalog, out string error)
        {
            error = null;

            if (string.IsNullOrEmpty(catalog.Identifier))
            {
                error = "Catalog Definition missing Identifier";
                return false;
            }

            if (string.IsNullOrEmpty(catalog.DisplayName))
            {
                error = "Catalog Definition missing DisplayName";
                return false;
            }

            if (string.IsNullOrEmpty(catalog.ApiBaseUrl))
            {
                error = "Catalog Definition missing ApiBaseUrl";
                return false;
            }

            if (!IsSupportedApiLevel(catalog.ApiVersionLevel))
            {
                error = "Catalog Definition Unsupported Api Level";
                return false;
            }

            return true;
        }
    }
}