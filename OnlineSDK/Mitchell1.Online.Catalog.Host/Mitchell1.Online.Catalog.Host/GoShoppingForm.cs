using System;
using System.Windows.Forms;
using Mitchell1.Catalog.Framework.Interfaces;
using Mitchell1.Online.Catalog.Host.Controllers;
using Mitchell1.Online.Catalog.Host.TransferObjects;

namespace Mitchell1.Online.Catalog.Host
{
    public partial class GoShoppingForm : CatalogHostingForm
    {
        private IEmbeddedCatalogTransferController catalogController;

        public GoShoppingForm()
        {
            InitializeComponent();
        }

        public void LoadOnlineCatalog(OnlineCatalogInformation catalog, ShoppingCart cart, IVendor vendor, IHostData hostData, IVehicle vehicle)
        {
            if (catalogController != null)
            {
                catalogController.RequestCompleted -= CatalogControllerOnCloseWindowRequested;
            }

            base.LoadOnlineCatalog(catalog);

            Text = catalog.DisplayName;
            catalogController = OnlineCatalogCommunicationFactory.GetEmbeddedCatalogTransferController(catalog, cart, vendor, hostData, vehicle);

            catalogController.RequestCompleted += CatalogControllerOnCloseWindowRequested;

            HostingControl.SetEmbeddedCatalogController(catalogController);
        }

        private void CatalogControllerOnCloseWindowRequested(object sender, bool completed)
        {
            DialogResult = completed ? DialogResult.OK : DialogResult.Cancel;
            Close();
        }

        public int HttpResponseCode { get; private set; }

        protected override void OnClosed(EventArgs e)
        {
            if (catalogController != null)
            {
                HttpResponseCode = catalogController.HttpResponseCode;
                catalogController.RequestCompleted -= CatalogControllerOnCloseWindowRequested;
            }

            base.OnClosed(e);
        }
    }
}
