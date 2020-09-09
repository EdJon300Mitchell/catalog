using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Mitchell1.Catalog.Framework.Interfaces;

namespace Mitchell1.Online.Catalog.Host
{
    public partial class CatalogHostingForm : Form
    {
        private OnlineCatalogInformation onlineCatalogInformation;

        protected CatalogHostingForm()
        {
            InitializeComponent();
        }

        protected OnlineCatalogInformation Catalog => onlineCatalogInformation;

        protected CatalogHostingControl HostingControl => catalogHostingControl1;

        protected virtual void LoadOnlineCatalog(OnlineCatalogInformation catalog)
        {
            onlineCatalogInformation = catalog;
            catalogHostingControl1.OnlineCatalogInformation = onlineCatalogInformation;

            // TODO: Could have 16x16 Window Icon Set
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
                if (catalogHostingControl1 != null && !catalogHostingControl1.IsDisposed)
                {
                    catalogHostingControl1.ShowDeveloperTools();
                }
            }
        }
    }
}
