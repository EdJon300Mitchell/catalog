using System;
using Mitchell1.Browser.Interfaces;
using Mitchell1.Online.Catalog.Host.Controllers;

namespace Mitchell1.Online.Catalog.Host.API
{
	public abstract class CustomWebPageController : IEmbeddedCatalogController
	{
		public event EventHandler<bool> RequestCompleted;

		protected abstract string Url { get; }

		protected abstract string ActionLabel { get; }

		protected abstract bool Action(object[] objects);

		public virtual void AttachBrowser<T>(IWebBrowserControl<T> browser)
		{
			browser.JavaScriptRegisterActionCallback(ActionLabel, Submit);
			browser.JavaScriptRegisterActionCallback("cancel", Cancel);
			browser.Url = Url;
		}

		public virtual void DetachBrowser<T>(IWebBrowserControl<T> browser)
		{
			browser.JavaScriptUnregisterAction(ActionLabel);
			browser.JavaScriptUnregisterAction("cancel");
		}

		private void Submit(object[] objects) => ReturnCompleted(Action(objects));

		private void Cancel(object[] objects) => ReturnCompleted(false);

		protected void ReturnCompleted(bool value) => RequestCompleted?.Invoke(this, value);
	}
}