using System.Collections.Generic;
using System.ComponentModel;
using Mitchell1.Online.Catalog.Host;

namespace Mitchell1.Catalog.Driver.Helpers
{
	public class PriceCheck : IExtendedPriceCheck
	{
		public PriceCheck()
		{
			DeliveryOption = string.Empty;
		}

		[Description("The delivery method by which the part is transferred to the customer")]
		public string DeliveryOption { get; }

		public IList<PriceCheckPart> Parts { get; } = new List<PriceCheckPart>();

		IReadOnlyList<IExtendedPriceCheckPart> IExtendedPriceCheck.Parts => (IReadOnlyList<IExtendedPriceCheckPart>)Parts;
	}
}
