using System.Collections.Generic;
using Mitchell1.Catalog.Framework.Interfaces;

namespace Mitchell1.Online.Catalog.Host
{
	public interface IExtendedPriceCheckAlternatePart : IPriceCheckAlternatePart, IMetadata
	{
		new IList<Location> Locations { get; }
	}
}