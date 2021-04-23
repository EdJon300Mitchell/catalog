namespace Mitchell1.Online.Catalog.Host.TransferObjects
{
	// See Mitchell1 Online Catalog SDK Doc
	public class TrackingRequest
	{
		public HostData HostData { get; set; }
		public Vendor Vendor { get; set; }
		public string OrderTrackingNumber { get; set; }
	}
}