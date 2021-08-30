namespace Mitchell1.Online.Catalog.Host
{
	/// <summary>
	/// Information about the capabilities of this Catalog
	/// </summary>
	public interface ICatalogProperties
	{
		/// <summary>
		/// Indicates whether the host application will show a “Deliver” “WillCall” choice for placing orders. Manager SE passes which ever choice user made.
		/// </summary>
		bool ShowsDeliverWillCall { get; }

		/// <summary>
		/// Indicates whether the catalog allows an empty (undefined) manufacturer code for parts used in PriceCheck
		/// or OrderParts.
		/// </summary>
		bool AllowsBlankManufacturerCode { get; }

		/// <summary>
		/// Indicates whether the catalog allows parts which were not found in the PriceCheck to be ordered.
		/// </summary>
		bool AllowsNotFoundPartsToBeOrdered { get; }

		/// <summary>
		/// Indicates whether the host application should only allow ordering of parts that have been price checked.
		/// </summary>
		bool RequiresPriceCheck { get; } 

		/// <summary>
		/// Indicates whether the catalog supports alternate locations within the PriceCheck method.
		/// If true, the host application will allow the user to select from a list of Locations.
		/// </summary>
		bool SupportsAlternateLocations { get; }

		/// <summary>
		/// Indicates whether the catalog supports alternate parts within the PriceCheck method.
		/// If true, the host application will allow the user to select from a list of AlternateParts.
		/// </summary>
		bool SupportsAlternateParts { get; }

		/// <summary>
		/// Indicates whether the catalog can specify a location for a part.
		/// If true, the host application will display the part location.
		/// </summary>
		bool SupportsLocation { get; }

		/// <summary>
		/// Indicates whether the catalog supports the OrderMessage property within the IOrder object.
		/// If true, the host application will allow the user to enter an order message that will be
		/// sent with the order.
		/// </summary>
		bool SupportsOrderMessage { get; }

		/// <summary>
		/// Indicates whether the catalog supports the PriceCheck method.
		/// If true, the host application will allow the user to perform a price check.
		/// If false, the host application will not allow the user to perform a price check.
		/// </summary>
		bool SupportsPriceCheck { get; }

		/// <summary>
		/// True if the catalog provides an OrderParts endpoint that will return one or more purchase orders
		/// </summary>
		bool SupportsMultiplePurchaseOrders { get; }
	}
}