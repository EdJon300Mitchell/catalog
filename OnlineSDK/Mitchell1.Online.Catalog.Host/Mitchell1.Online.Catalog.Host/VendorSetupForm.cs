using System;
using System.Windows.Forms;
using Mitchell1.Catalog.Framework.Interfaces;
using Mitchell1.Online.Catalog.Host.Controllers;

namespace Mitchell1.Online.Catalog.Host
{
    public partial class VendorSetupForm : CatalogHostingForm
    {
        private IEmbeddedCatalogController catalogController;

        public VendorSetupForm()
        {
            InitializeComponent();
        }

        public void LoadOnlineCatalog(OnlineCatalogInformation catalog, IVendor vendor, IHostData hostData)
        {
            if (catalogController != null)
            {
                catalogController.RequestCompleted -= CatalogControllerOnCloseWindowRequested;
            }

            base.LoadOnlineCatalog(catalog);

            Text = @"Vendor Setup - " + catalog.DisplayName;

            catalogController = OnlineCatalogCommunicationFactory.GetCatalogVendorSetupController(catalog, vendor, hostData);

            catalogController.RequestCompleted += CatalogControllerOnCloseWindowRequested;

            HostingControl.SetEmbeddedCatalogController(catalogController);
        }

        private void CatalogControllerOnCloseWindowRequested(object sender, bool completed)
        {
            DialogResult = completed ? DialogResult.OK : DialogResult.Cancel;

            Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            if (catalogController != null)
            {
                catalogController.RequestCompleted -= CatalogControllerOnCloseWindowRequested;
            }

            base.OnClosed(e);
        }
    }
}
