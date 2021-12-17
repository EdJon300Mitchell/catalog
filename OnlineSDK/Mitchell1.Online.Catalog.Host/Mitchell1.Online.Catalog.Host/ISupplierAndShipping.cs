namespace Mitchell1.Online.Catalog.Host
{
	public interface ISupplierAndShipping
	{
		/// <summary>
		/// Uniquely identifies (within this catalog) the source supplier for this part. This will be displayed to the user.
		/// </summary>
		string SupplierName { get; set; }

		/// <summary>
		/// Description of Shipping Cost. (Optional) This field is limited to 25 characters.
		/// </summary>
		string ShippingDescription { get; set; }

		/// <summary>
		/// Shipping cost for this part. (Optional - defaults to 0)
		/// </summary>
		decimal ShippingCost { get; set; }
	}
}