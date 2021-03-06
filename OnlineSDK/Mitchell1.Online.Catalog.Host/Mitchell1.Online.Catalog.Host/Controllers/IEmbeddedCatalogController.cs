using System;
using Mitchell1.Browser.Interfaces;

namespace Mitchell1.Online.Catalog.Host.Controllers
{
	public interface IEmbeddedCatalogController
	{
		void AttachBrowser<T>(IWebBrowserControl<T> browser);
		void DetachBrowser<T>(IWebBrowserControl<T> browser);

		event EventHandler<bool> RequestCompleted;
	}
}