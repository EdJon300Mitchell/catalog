using System;

namespace Mitchell1.Online.Catalog.Host.Orders
{
	public class TrackingRequestResponse
	{
		public Uri ExternalTrackingUrl { get; set; }

		public string StatusDisplay { get; set; }
	}
}