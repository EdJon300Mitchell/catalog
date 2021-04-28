namespace Mitchell1.Online.Catalog.Host
{
	public interface ICatalogProperties
	{
		bool ShowsDeliverWillCall { get; }
		bool AllowsBlankManufacturerCode { get; }
		bool AllowsNotFoundPartsToBeOrdered { get; }
		bool RequiresPriceCheck { get; } 
		bool SupportsAlternateLocations { get; }
		bool SupportsAlternateParts { get; }
		bool SupportsLocation { get; }
		bool SupportsOrderMessage { get; }
		bool SupportsPriceCheck { get; }
	}
}