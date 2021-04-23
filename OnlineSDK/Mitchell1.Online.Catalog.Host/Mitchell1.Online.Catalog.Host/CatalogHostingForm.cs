using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Mitchell1.Catalog.Framework.Interfaces;
using Mitchell1.Online.Catalog.Host.Controllers;

namespace Mitchell1.Online.Catalog.Host
{
	public interface ICatalogHostingForm : IDisposable
	{
		bool ShowWebPage();
        Size ClientSize { set; }
        bool MaximizeBox { set; }
        FormWindowState WindowState { set; }
	}

	public delegate ICatalogHostingForm NewCatalogHostingForm(OnlineCatalogInformation onlineCatalogInformation, IEmbeddedCatalogController catalogController, string text = null);

	public sealed partial class CatalogHostingForm : Form, ICatalogHostingForm
	{
	    private readonly IEmbeddedCatalogController catalogController;

	    public static NewCatalogHostingForm New => (x, y, z) => new CatalogHostingForm(x, y, z);

        private CatalogHostingForm(OnlineCatalogInformation onlineCatalogInformation, IEmbeddedCatalogController catalogController, string text)
        {
	        this.catalogController = catalogController;
	        InitializeComponent();
	        catalogHostingControl.OnlineCatalogInformation = onlineCatalogInformation;
            Text = (text != null ? text + " - " : "") + onlineCatalogInformation.DisplayName;
            catalogController.RequestCompleted += CatalogControllerOnCloseWindowRequested;
            HostingControl.SetEmbeddedCatalogController(catalogController);
            // TODO: Could have 16x16 Window Icon Set
        }

        private CatalogHostingControl HostingControl => catalogHostingControl;

        private void CatalogControllerOnCloseWindowRequested(object sender, bool completed)
        {
	        DialogResult = completed ? DialogResult.OK : DialogResult.Cancel;
	        Close();
        }

        public bool ShowWebPage()
        {
	        var completed = ShowDialog() == DialogResult.OK;

	        if (!completed && catalogController is IEmbeddedCatalogTransferController controller && controller.HttpResponseCode == 403)
	        {
		        throw new CatalogAuthenticationException();
	        }

	        return completed;
        }

        protected override void OnClosed(EventArgs e)
        {
	        catalogController.RequestCompleted -= CatalogControllerOnCloseWindowRequested;
	        base.OnClosed(e);
        }

        // P/Invoke constants
        private const int WM_SYSCOMMAND = 0x112;
        private const int MF_STRING = 0x0;
        private const int MF_SEPARATOR = 0x800;

        // P/Invoke declarations
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool AppendMenu(IntPtr hMenu, int uFlags, int uIDNewItem, string lpNewItem);

        // ID for the About item on the system menu
        private int SYSMENU_DEVTOOLS_ID = 0x1;

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // Get a handle to a copy of this form's system (window) menu
            IntPtr hSysMenu = GetSystemMenu(this.Handle, false);

            if (hSysMenu != IntPtr.Zero)
            {
                // Add a separator
                AppendMenu(hSysMenu, MF_SEPARATOR, 0, string.Empty);

                // Add the About menu item
                AppendMenu(hSysMenu, MF_STRING, SYSMENU_DEVTOOLS_ID, "&Dev Tools");
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // Test if the About item was selected from the system menu
            if ((m.Msg == WM_SYSCOMMAND) && ((int)m.WParam == SYSMENU_DEVTOOLS_ID))
            {
                if (catalogHostingControl != null && !catalogHostingControl.IsDisposed)
                {
                    catalogHostingControl.ShowDeveloperTools();
                }
            }
        }
    }
}
