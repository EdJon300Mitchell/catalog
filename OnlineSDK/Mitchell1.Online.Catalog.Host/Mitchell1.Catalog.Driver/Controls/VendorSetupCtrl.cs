using System;
using System.Windows.Forms;
using Mitchell1.Catalog.Driver.Helpers;
using Mitchell1.Catalog.Framework.Interfaces;
using Mitchell1.Online.Catalog.Host;

namespace Mitchell1.Catalog.Driver.Controls
{
	public partial class VendorSetupCtrl : UserControl
	{
		#region Fields

		private readonly IOnlineCatalogInfo catalogInfo;
		private readonly Vendor vendor;
		private readonly HostData hostData;

		#endregion

		#region Constructor

		public VendorSetupCtrl(IOnlineCatalogInfo catalogInfo, Vendor vendor, HostData hostData)
		{
			this.catalogInfo = catalogInfo;
			this.vendor = vendor;
			this.hostData = hostData;
			InitializeComponent();
            if (catalogInfo == null)
            {
                buttonVendorSetup.Enabled = false;
            }
			TreeNode catalogNode = treeViewVendor.Nodes["CatalogSettings"];
			catalogNode.Tag = catalogInfo;
			catalogNode.Nodes["Vendor"].Tag = vendor;
			catalogNode.Nodes["HostData"].Tag = hostData;
			treeViewVendor.ExpandAll();
		}

		#endregion

	    public void Launch()
	    {
	        buttonVendorSetup.PerformClick();
	    }

		#region Event Handlers

		private void buttonVendorSetup_Click(object sender, EventArgs e)
		{
			VendorHelper.EnsureQualifierValid(catalogInfo, vendor);
			if (catalogInfo.VendorSetup(vendor, hostData))
			{
				vendor.Catalog = catalogInfo.DisplayName;
			}
		}

		private void treeViewVendor_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			propertyGrid1.SelectedObject = e.Node.Tag;
		}

		#endregion
	}
}
