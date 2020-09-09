namespace ExampleCatalog.DataLayer
{
	public class TrackingRequest
	{
		public IHostData HostData { get; set; }
		public IVendor Vendor { get; set; }
		public string OrderTrackingNumber { get; set; }
	}
}