using System;
using System.Diagnostics;
using System.Runtime.Remoting.Channels;
using Mitchell1.Browser.Interfaces;
using Mitchell1.Catalog.Framework.Interfaces;
using Mitchell1.Online.Catalog.Host.Controllers;

namespace Mitchell1.Online.Catalog.Host.API.v1
{
    public class VendorSetupV1 : IEmbeddedCatalogController
    {
        private readonly OnlineCatalogInformation onlineCatalogInformation;

        private string ConfigurationUrl => onlineCatalogInformation.GetAbsoluteUrl(CatalogApiPart.Setup);

        public VendorSetupV1(OnlineCatalogInformation catalogInformation)
        {
            onlineCatalogInformation = catalogInformation;
        }

        public event EventHandler<bool> RequestCompleted;

        public void AttachBrowser<T>(IWebBrowserControl<T> browser)
        {
            browser.JavaScriptRegisterActionCallback("save", SaveConfiguration);
            browser.JavaScriptRegisterActionCallback("cancel", CancelChanges);

            string queryString = "";
            if (!String.IsNullOrEmpty(Vendor?.Qualifier))
            {
                queryString = "?Qualifier=" + Uri.EscapeDataString(Vendor.Qualifier);
            }

            browser.Url = ConfigurationUrl + queryString;
        }

        public void DetachBrowser<T>(IWebBrowserControl<T> browser)
        {
            browser.JavaScriptUnregisterAction("save");
            browser.JavaScriptUnregisterAction("cancel");
        }

        public IVendor Vendor { get; set; }
        public IHostData HostData { get; set; }

        private void CancelChanges(object[] objects)
        {
            RequestCompleted?.Invoke(this, false);
        }

        private void SaveConfiguration(object[] objects)
        {
            if (objects != null && objects.Length > 1 && objects[1] is string)
            {
                Vendor.Qualifier = (string)objects[1];
            }

            RequestCompleted?.Invoke(this, true);
        }
    }
}
