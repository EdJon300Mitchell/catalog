using System.Collections.Generic;
using Mitchell1.Catalog.Framework.Interfaces;
using System.ComponentModel;

namespace Mitchell1.Catalog.Driver.Helpers
{
	internal class PriceCheck : IPriceCheck
	{
		private readonly List<IPriceCheckPart> parts = new List<IPriceCheckPart>();

		[DescriptionAttribute("The delivery method by which the part is transferred to the customer")]
		public string DeliveryOption { get; set; }

		public void AddPart(IPriceCheckPart part)
		{
			parts.Add(part);
		}

		public IList<IPriceCheckPart> Parts
		{
			get { return parts; }
		}

		public PriceCheck()
		{
			DeliveryOption = string.Empty;
		}
	}
}
