using System;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using Mitchell1.Catalog.Driver.Browser;
using Mitchell1.Catalog.Driver.Controls;
using Mitchell1.Catalog.Driver.Data;
using Mitchell1.Catalog.Driver.Helpers;
using Mitchell1.Catalog.Framework.Common;
using Mitchell1.Online.Catalog.Host;
using ShoppingCart = Mitchell1.Online.Catalog.Host.TransferObjects.ShoppingCart;

namespace Mitchell1.Catalog.Driver
{
	public partial class CatalogDriver : Form, ICatalogDriver
	{
        private enum DriverState
        {
			None,   
            VendorSetup,
			GoShopping,   
			PriceCheck,
			OrderParts,
			VehicleSetup,
			ConfigureCatalog
        }

    	private readonly PriceCheck priceCheck = new PriceCheck();
    	private readonly Order order = new Order();
        private readonly Vehicle vehicle = new Vehicle();
        private readonly Vendor vendor = new Vendor();
        private readonly HostData hostData = new HostData("Mitchell 1 Catalog Driver", "8.4", 70M);
    	private DriverState currentState = DriverState.None;
        private GoShoppingCtrl goShoppingCtrl = null;
        private VendorSetupCtrl vendorSetup = null;
		private CatalogInfo configuredCatalog;

		public CatalogDriver()
        {
            InitializeComponent();
	        Text = $"Mitchell1 Online Catalog Driver {Assembly.GetExecutingAssembly().GetName().Version} - Copyright (c) {DateTime.Now.Year} Mitchell Repair Information Company, LLC";

			ConfigureDefaultProxy();
		}

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

	        LoadVendorData();

			SelectCatalog();
        }

		private void LoadVendorData()
		{
			new DriverData().Load(vendor, vehicle);
			treeViewCatalogMethods.ExpandAll();
		}

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
			Close();
        }

        private void vehicleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetupVehicle();
        }

		private void vendorQualifierToolStripMenuItem_Click(object sender, EventArgs e)
		{
			vendor.Qualifier = string.Empty;
			SelectCatalog();
		}

		private void cartToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShoppingCart = null;
			SelectCatalog();
		}

		private void CatalogDriver_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveVendorData();
        }

		private void SaveVendorData()
		{
			new DriverData().Save(vendor, vehicle);
		}

		private void treeView_AfterSelect(object sender, TreeNodeMouseClickEventArgs e)
		{
			switch (e.Node.Name)
			{
				case "CatalogMethods":
				case "CatalogSetup":
					if(e.Node.IsExpanded)
					{
						e.Node.Collapse();
						treeViewCatalogMethods.SelectedNode = null;
					}
					else
					{
						e.Node.Expand();
						treeViewCatalogMethods.SelectedNode = null;
					}
					break;
                case "SelectCatalog":
                    SelectCatalog();
                    break;
                case "SetupVehicle":
                    SetupVehicle();
                    break;
				case "SetupVendor":
					SelectVendorSetup();
					break;
				case "GoShopping":
					SelectGoShopping();
					break;
				case "PriceCheck":
					SelectPriceCheck();
					break;
				case "OrderParts":
					SelectOrderParts();
					break;
			}
		}



        private void SelectCatalog()
        {
	        if (currentState == DriverState.ConfigureCatalog)
		        return;

	        currentState = DriverState.ConfigureCatalog;
	        var control = new CatalogConfiguration(this);
	        splitContainer1.Panel2.Controls.Clear();
	        splitContainer1.Panel2.Controls.Add(control);
	        splitContainer1.Panel2.Controls[0].Dock = DockStyle.Fill;
		}

        private void SetupVehicle()
        {
			if (currentState != DriverState.VehicleSetup)
			{
				currentState = DriverState.VehicleSetup;
				VehicleSetupCtrl vehicleSetupCtrl = new VehicleSetupCtrl(vehicle);
				splitContainer1.Panel2.Controls.Clear();
				splitContainer1.Panel2.Controls.Add(vehicleSetupCtrl);
				splitContainer1.Panel2.Controls[0].Dock = DockStyle.Fill;
			}
        }

		private void SelectOrderParts()
		{
			if (currentState != DriverState.OrderParts && currentState != DriverState.PriceCheck)
			{
				currentState = DriverState.OrderParts;
				PriceCheckCtrl priceCheckCtrl = new PriceCheckCtrl(ConfiguredCatalog, ShoppingCart, priceCheck, order, vendor,
					vehicle, hostData);
				splitContainer1.Panel2.Controls.Clear();
				splitContainer1.Panel2.Controls.Add(priceCheckCtrl);
				splitContainer1.Panel2.Controls[0].Dock = DockStyle.Fill;
			}
		}

		private void SelectPriceCheck()
		{
			if (currentState != DriverState.PriceCheck && currentState != DriverState.OrderParts)
			{
				currentState = DriverState.PriceCheck;
				PriceCheckCtrl priceCheckCtrl = new PriceCheckCtrl(ConfiguredCatalog, ShoppingCart, priceCheck, order, vendor,
					vehicle, hostData);
				splitContainer1.Panel2.Controls.Clear();
				splitContainer1.Panel2.Controls.Add(priceCheckCtrl);
				splitContainer1.Panel2.Controls[0].Dock = DockStyle.Fill;
			}
		}

		private void SelectGoShopping()
		{
			if (currentState != DriverState.GoShopping)
			{
				currentState = DriverState.GoShopping;
                goShoppingCtrl = new GoShoppingCtrl(ConfiguredCatalog, vendor, vehicle, hostData, this);

				splitContainer1.Panel2.Controls.Clear();
				splitContainer1.Panel2.Controls.Add(goShoppingCtrl);
				splitContainer1.Panel2.Controls[0].Dock = DockStyle.Fill;
			}
		}


		private void SelectVendorSetup()
		{
			if (currentState != DriverState.VendorSetup)
			{
				currentState = DriverState.VendorSetup;
			    vendorSetup = new VendorSetupCtrl(ConfiguredCatalog, vendor, hostData);
				splitContainer1.Panel2.Controls.Clear();
				splitContainer1.Panel2.Controls.Add(vendorSetup);
				splitContainer1.Panel2.Controls[0].Dock = DockStyle.Fill;
			}
		}

        internal static void ConfigureDefaultProxy()
        {
            if (InternetSettings.IsProxyValid)
            {
                WebRequest.DefaultWebProxy = InternetSettings.WebProxy;
            }
        }

		private void hTML5TestToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var form = new BrowserHtmlTests("https://html5test.com/");
			form.Show();
		}

		private void whatIsMyUserAgentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var form = new BrowserHtmlTests("http://www.whatsmyua.com/");
			form.Show();
		}

		private void licensesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var form = new BrowserHtmlTests("chrome://credits/");
			form.Show();
		}

		private void playgroundToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var form = new SampleBrowser();
			form.Show();
		}

		public CatalogInfo ConfiguredCatalog
		{
			get => configuredCatalog;
			set
			{
				configuredCatalog = value;
				toolStripStatusLabelCatalogName.Text = value?.DisplayName ?? "";
			}
		}

		public ShoppingCart ShoppingCart { get; set; }
	}
}
