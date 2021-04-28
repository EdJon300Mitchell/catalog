using System.Collections.Generic;

namespace Mitchell1.Online.Catalog.Host
{
	public interface IExtendedPriceCheck
	{
		string DeliveryOption { get; }
		IReadOnlyList<IExtendedPriceCheckPart> Parts { get; }
	}
}