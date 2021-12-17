using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Mitchell1.Browser.Interfaces;
using Mitchell1.Catalog.Framework.Common;
using Mitchell1.Online.Catalog.Host.API;
using Mitchell1.Online.Catalog.Host.Controllers;

namespace Mitchell1.Online.Catalog.Host
{
    public class CatalogHostingControl : ContainerControl
    {
        private IWebBrowserControl<Control> browserControl;
        private IEmbeddedCatalogController catalogController;

        private void CreateBrowser()
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException("Hosting Control Already Disposed");
            }

            if (browserControl != null)
            {
                return;
            }

            browserControl = WebBrowserFactory.CreateBrowserControl();
            browserControl.Control.Dock = DockStyle.Fill;
            browserControl.OpenExternalWindow = true;

            browserControl.LoadError += BrowserControlOnLoadError;
            browserControl.Navigated += BrowserControl_Navigated;

            Controls.Add(browserControl.Control);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (browserControl != null)
            {
                catalogController?.DetachBrowser(browserControl);

                browserControl.LoadError -= BrowserControlOnLoadError;
                browserControl.Navigated -= BrowserControl_Navigated;

                browserControl.Dispose();
                browserControl = null;
            }

            catalogController = null;
        }

        public OnlineCatalogInformation OnlineCatalogInformation { get; set; }

        public void SetEmbeddedCatalogController(IEmbeddedCatalogController controller)
        {
            if (controller == null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            CreateBrowser();

            if (catalogController == controller)
            {
                return;
            }

            catalogController?.DetachBrowser(browserControl);
            catalogController = controller;
            catalogController.AttachBrowser(browserControl);
        }

        public void ShowDeveloperTools()
        {
            if (browserControl != null && !browserControl.IsDisposed && browserControl.IsDeveloperToolAvailable)
            {
                browserControl.ShowDeveloperTools();
            }
        }

        private void BrowserControl_Navigated(object sender, WebControlNavigatedEventArgs args)
        {
            // HTTPCode 0 is for internal navigation - eg already set error page
            if (args.HttpCode != 0 && args.HttpCode != 200 && args.Frame.Identifier == browserControl.MainFrame?.Identifier)
            {
                Trace.WriteLine(browserControl.Url + " failed to load (http) " + args.HttpCode);
                browserControl.DocumentText = HtmlErrorGenerator.GetFormattedError(args.HttpCode, OnlineCatalogInformation);
            }
        }

        private void BrowserControlOnLoadError(object sender, WebControlErrorEventArgs args)
        {
            if (args.Frame.Identifier == browserControl.MainFrame?.Identifier)
            {
                Trace.WriteLine(browserControl.Url + " failed to load (connection issue) " + args.Error);
                browserControl.DocumentText = HtmlErrorGenerator.GetFormattedError(args.ErrorCode, args.Error, OnlineCatalogInformation);
            }
        }
    }
}
