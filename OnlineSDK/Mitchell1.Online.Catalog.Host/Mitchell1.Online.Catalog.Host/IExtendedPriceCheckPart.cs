using System.Collections.Generic;
using Mitchell1.Catalog.Framework.Interfaces;

namespace Mitchell1.Online.Catalog.Host
{
	public interface IExtendedPriceCheckPart : IPriceCheckPart, IMetadata
	{
		new IList<IExtendedPriceCheckAlternatePart> AlternateParts { get; }

		new IList<Location> Locations { get; }

		new IExtendedPriceCheckAlternatePart NewAlternatePart();
	}
}