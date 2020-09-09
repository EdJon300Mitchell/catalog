using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Mitchell1.Catalog.Driver.Helpers;
using Mitchell1.Catalog.Framework.Interfaces;
using Mitchell1.Online.Catalog.Host;
using Mitchell1.Online.Catalog.Host.TransferObjects;

namespace Mitchell1.Catalog.Driver.Controls
{
    public partial class GoShoppingCtrl : UserControl
    {
		private static readonly ExternalCatalogAdapterErrorHandler catalogExceptionHandler = new ExternalCatalogAdapterErrorHandler(); 


		private readonly IOnlineCatalog catalog;
		private readonly ICatalogInfo catalogInfo;
		private readonly IVendor vendor;
		private readonly IVehicle vehicle;
		private readonly IHostData hostData;
	    private readonly ICatalogDriver driver;

		public GoShoppingCtrl(IOnlineCatalogInfo catalogInfo, Vendor vendor, IVehicle vehicle, IHostData hostData, ICatalogDriver driver)
        {
            this.catalogInfo = catalogInfo;
            this.vendor = vendor;
            this.vehicle = vehicle;
            this.hostData = hostData;
	        this.driver = driver;

            InitializeComponent();

            if (catalogInfo == null)
            {
                buttonGoShopping.Enabled = false;
                buttonDemoImage.Enabled = false;
            }
            else
            {
				catalog = VendorHelper.GetCatalog(catalogInfo, vendor, vehicle, hostData);
				buttonDemoImage.Image = catalogInfo.ImageUp;
            }

	        propertyGridCartItems.SelectedObject = driver.ShoppingCart;
	        FillCartTree(driver.ShoppingCart);
			FillSettingsTree();
		}

		#region Events

		private void buttonGoShopping_Click(object sender, EventArgs e)
        {
			try
			{
				if (catalog.GoShopping(out var cart))
				{
					driver.ShoppingCart = cart;
					FillCartTree(cart);
				}
			}
			catch (CatalogAuthenticationException)
			{
				catalogExceptionHandler.ShowCatalogAuthenticationExceptionMessage();
			}
			catch (CatalogCommunicationException)
			{
				catalogExceptionHandler.ShowCatalogCommunicationExceptionMessage();
			}
			catch (CatalogException ex)
			{
				catalogExceptionHandler.ShowGoShoppingCatalogExceptionMessage(ex.Message);
			}
			catch (Exception ex)
			{
				catalogExceptionHandler.ShowGeneralCatalogExceptionMessage(ex.Message);
			}
		}

		private void buttonDemoImage_MouseDown(object sender, MouseEventArgs e)
		{
			buttonDemoImage.Image = catalogInfo.ImageDown;
		}

		private void buttonDemoImage_MouseUp(object sender, MouseEventArgs e)
		{
			buttonDemoImage.Image = catalogInfo.ImageUp;
		}

		private void treeViewCart_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			propertyGridCartItems.SelectedObject = e.Node.Tag;
		}

		private void buttonClearCart_Click(object sender, EventArgs e)
		{
			driver.ShoppingCart = null;
			propertyGridCartItems.SelectedObject = null;
			FillCartTree(null);
		}

		#endregion

		#region Methods

		private void FillSettingsTree()
		{
			TreeNode catalogNode = treeViewCart.Nodes["CatalogSettings"];
			catalogNode.Tag = catalog;
			catalogNode.Nodes["Vendor"].Tag = vendor;
			catalogNode.Nodes["Vehicle"].Tag = vehicle;
			catalogNode.Nodes["HostData"].Tag = hostData;
		}

		private void FillCartTree(ShoppingCart cart)
        {
            IList<IPartItem> parts = new List<IPartItem>();
            IList<ILaborItem> labor = new List<ILaborItem>();
            IList<INoteItem> notes = new List<INoteItem>();
            if (cart != null)
            {
                foreach (var item in cart.Items)
                {
	                switch (item)
	                {
		                case IPartItem p:
			                parts.Add(p);
			                break;
		                case ILaborItem l:
			                labor.Add(l);
			                break;
		                case INoteItem n:
			                notes.Add(n);
			                break;
	                }
                }
            }

            TreeNode cartNode = treeViewCart.Nodes["Cart"];
            cartNode.Text = "Cart (" + (parts.Count + labor.Count + notes.Count) + ")";
            TreeNode partsNode = cartNode.Nodes["PartItems"];
            partsNode.Nodes.Clear();
            partsNode.Text = "Part Items (" + parts.Count + ")";
            if (parts.Count > 0)
            {
                foreach (var partItem in parts)
                {
                    var partNode = new TreeNode(partItem.PartNumber + " : " + partItem.Description)
                	{
                		Tag = partItem,
                		ImageIndex = 18,
                		SelectedImageIndex = 18
                	};
                	partsNode.Nodes.Add(partNode);
                }
            }
            var laborNode = cartNode.Nodes["LaborItems"];
            laborNode.Nodes.Clear();
            laborNode.Text = "Labor Items (" + labor.Count + ")";
            if (labor.Count > 0)
            {
                foreach (var laborItem in labor)
                {
                    var laborItemNode = new TreeNode(laborItem.Description)
                 	{
                 		Tag = laborItem,
                 		ImageIndex = 11,
                 		SelectedImageIndex = 11
                 	};
                	laborNode.Nodes.Add(laborItemNode);
                }
            }
            var noteNode = cartNode.Nodes["NoteItems"];
            noteNode.Nodes.Clear();
            noteNode.Text = "Note Items (" + notes.Count + ")";
            if (notes.Count > 0)
            {
                foreach (var noteItem in notes)
                {
                    var noteItemNode = new TreeNode(noteItem.Note)
                	{
                		Tag = noteItem,
                		ImageIndex = 12,
                		SelectedImageIndex = 12
                	};
                	noteNode.Nodes.Add(noteItemNode);
                }
            }

		    TreeNode orderNode = treeViewCart.Nodes["OrderCart"];
            orderNode.Nodes.Clear();

	        if (cart != null)
	        {
		        orderNode.Text = $"ICartOrder's ({cart.Orders.Count})";

		        int index = 0;
		        foreach (var order in cart.Orders)
		        {
			        var node = new TreeNode($"Order# {++index}")
			        {
				        Tag = order,
				        ImageIndex = 0,
				        SelectedImageIndex = 0
			        };

			        foreach (var part in order.Parts)
			        {
				        var partNode = new TreeNode($"Part# {part.PartNumber}")
				        {
					        Tag = part,
					        ImageIndex = 18,
					        SelectedImageIndex = 18
				        };

				        node.Nodes.Add(partNode);
			        }

			        orderNode.Nodes.Add(node);
		        }
	        }

	        orderNode.ExpandAll();

		    treeViewCart.Nodes["Cart"].ExpandAll();
		}

		#endregion
    }
}
