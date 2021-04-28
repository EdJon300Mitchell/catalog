using System;
using Mitchell1.Browser.Interfaces;
using Mitchell1.Online.Catalog.Host.Controllers;

namespace Mitchell1.Online.Catalog.Host.API
{
	public abstract class CustomWebPageControllerHandleForbidden : CustomWebPageController, IEmbeddedCatalogTransferController
	{
		private EventHandler<WebControlNavigatedEventArgs> eventHandler;

		public int HttpResponseCode { get; private set; }

		public override void AttachBrowser<T>(IWebBrowserControl<T> browser)
		{
			eventHandler = delegate(object sender, WebControlNavigatedEventArgs args)
			{
				// Ignore non main frame and internal http codes
				if (args.HttpCode != 0 && args.Frame.Identifier == browser.MainFrame?.Identifier)
				{
					HttpResponseCode = args.HttpCode;

					// 403 we treat special, close window and throw auth error
					if (args.HttpCode == 403)
					{
						ReturnCompleted(false);
					}
				}
			};
			browser.Navigated += eventHandler;
			base.AttachBrowser(browser);
		}

		public override void DetachBrowser<T>(IWebBrowserControl<T> browser)
		{
			browser.Navigated -= eventHandler;
			base.DetachBrowser(browser);
		}
	}
}