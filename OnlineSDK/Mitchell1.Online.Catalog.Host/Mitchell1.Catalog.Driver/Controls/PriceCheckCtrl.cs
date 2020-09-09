using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Mitchell1.Catalog.Driver.Helpers;
using Mitchell1.Catalog.Framework.Interfaces;
using Mitchell1.Online.Catalog.Host;
using Mitchell1.Online.Catalog.Host.TransferObjects;

namespace Mitchell1.Catalog.Driver.Controls
{
    public partial class PriceCheckCtrl : UserControl
	{
		private static readonly ExternalCatalogAdapterErrorHandler catalogExceptionHandler = new ExternalCatalogAdapterErrorHandler();
		private int startingPO = 1000;
		
		private readonly IOnlineCatalog catalog;
		private readonly ShoppingCart cart;
    	private readonly IPriceCheck priceCheck;
    	private readonly OrderRequestResponse order;
		private readonly IVendor vendor;
		private readonly IVehicle vehicle;
		private readonly IHostData hostData;

		public PriceCheckCtrl(IOnlineCatalogInfo catalogInfo, ShoppingCart cart, IPriceCheck priceCheck, OrderRequestResponse order, 
			Vendor vendor, IVehicle vehicle, IHostData hostData)
        {
            this.cart = cart;
        	this.priceCheck = priceCheck;
        	this.order = order;
            this.vendor = vendor;
            this.vehicle = vehicle;
            this.hostData = hostData;
            InitializeComponent();

            textBoxPONumber.Text = $"#{startingPO}";

			if (catalogInfo == null)
            {
                buttonOrderParts.Enabled = false;
                buttonPriceCheck.Enabled = false;
            }
            else
            {
				catalog = VendorHelper.GetCatalog(catalogInfo, vendor, vehicle, hostData);
            	FillTree();
                setButtonState();
            }
			FillSettingsTree();
			SetDeliveryMethodOptions();
		}

		#region Methods

		private void FillSettingsTree()
		{
			treeViewAppSettings.Nodes["CatalogSettings"].Tag = catalog;
			treeViewAppSettings.Nodes["CatalogSettings"].Nodes["Vendor"].Tag = vendor;
			treeViewAppSettings.Nodes["CatalogSettings"].Nodes["Vehicle"].Tag = vehicle;
			treeViewAppSettings.Nodes["CatalogSettings"].Nodes["HostData"].Tag = hostData;
		}

        private void FillTree()
        {
            IList<IPartItem> parts = new List<IPartItem>();
            if (cart != null)
            {
                foreach (ICartItem item in cart.Items)
                {
					if (item is IPartItem)
					{
						parts.Add((IPartItem) item);
					}
                }
            }
            TreeNode partsNode = treeViewCart.Nodes["Parts"];
            partsNode.Text = "Parts (" + parts.Count + ")";
            partsNode.Nodes.Clear();
            partsNode.Text = "Part Items (" + parts.Count + ")";
            if (parts.Count > 0)
            {
                foreach (IPartItem part in parts)
                {
                    TreeNode partNode = new TreeNode(part.PartNumber + " : " + part.Description)
                	{
                		Tag = part,
                		ImageIndex = 18,
                		SelectedImageIndex = 18
                	};
                	partsNode.Nodes.Add(partNode);
                }
            }
			treeViewCart.ExpandAll();
        }

    	private static void AddPriceCheckPartToTree(TreeNode part, IPriceCheckPart priceCheckPart)
    	{
    		part.Nodes.Clear();
    		part.Tag = priceCheckPart;
    		part.Checked = false;
    		if (priceCheckPart.Found)
    		{
    			part.SelectedImageIndex = 5;
    			part.ImageIndex = 5;	
    		}
    		else
    		{
    			part.SelectedImageIndex = 14;
    			part.ImageIndex = 14;
    		}
    		foreach (ILocation location in priceCheckPart.Locations)
    		{
    			TreeNode locationNode = new TreeNode(location.Name) {Tag = location};
    			part.Nodes.Add(locationNode);
    		}
							
    		foreach (IPriceCheckAlternatePart alternatePart in priceCheckPart.AlternateParts)
    		{
    			TreeNode altPartNode = new TreeNode(alternatePart.PartNumber + " : " + alternatePart.Description)
               	{
               		Tag = alternatePart,
               		ImageIndex = 17,
               		SelectedImageIndex = 17
               	};
    			foreach (ILocation location in alternatePart.Locations)
    			{
    				TreeNode locationNode = new TreeNode(location.Name) {Tag = location};
    				altPartNode.Nodes.Add(locationNode);
    			}
    			part.Nodes.Add(altPartNode);
    		}
    	}

    	private static PriceCheckPart ConvertToPriceCheckPart(IPartItem partItem)
    	{
			// Emulate decimal rounding issue on Manager SE
			partItem.Quantity = decimal.Parse(partItem.Quantity.ToString("0.00"));

    		return new PriceCheckPart
    			{
    				Description = partItem.Description,
    				ManufacturerLineCode = partItem.ManufacturerLineCode,
    				ManufacturerName = partItem.ManufacturerName,
    				PartNumber = partItem.PartNumber,
    				QuantityAvailable = 0,
    				QuantityOrdered = 0,
    				QuantityRequested = partItem.Quantity,
    				Status = "",
    				UnitCore = partItem.UnitCore,
    				UnitCost = partItem.UnitCost,
    				UnitList = partItem.UnitList,
    				UnitPrice = (partItem.UnitCost * 1.5M)
    			};
    	}

    	private void AddOrderToTree()
	    {
		    buttonOrderTracking.Text = $"Track Order #{order.Response.TrackingNumber ?? "--"}";

			TreeNode orderNode = new TreeNode("Order : " + order.Response.ConfirmationNumber)
         	{
         		Name = "Order",
         		Tag = order,
         		SelectedImageIndex = 2,
         		ImageIndex = 2
         	};
			foreach (IOrderPart orderPart in order.Response.Parts)
			{
				TreeNode orderPartNode = new TreeNode(orderPart.PartNumber + ":" + orderPart.Description)
             	{
             		Tag = orderPart,
             		SelectedImageIndex = 9,
             		ImageIndex = 9
             	};
				orderNode.Nodes.Add(orderPartNode);
			}
			if(treeViewAppSettings.Nodes["Order"] != null)
			{
				treeViewAppSettings.Nodes["Order"].Remove();
			}
			treeViewAppSettings.Nodes.Insert(0, orderNode);
			treeViewAppSettings.Nodes["Order"].ExpandAll();
		}

		private void AddPartItemToOrder(IPartItem partItem)
		{
			OrderPart orderPart = new OrderPart
          	{
          		Description = partItem.Description,
          		ManufacturerLineCode = partItem.ManufacturerLineCode,
          		ManufacturerName = partItem.ManufacturerName,
          		PartNumber = partItem.PartNumber,
          		QuantityAvailable = 0,
          		QuantityOrdered = 0,
          		QuantityRequested = partItem.Quantity,
          		Status = "",
          		UnitCore = partItem.UnitCore,
          		UnitCost = partItem.UnitCost,
          		UnitList = partItem.UnitList,
          		UnitPrice = (partItem.UnitCost*1.5M)
          	};
			order.Request.Parts.Add(orderPart);
		}

		private void AddPriceCheckPartItemToOrder(IPriceCheckPart partItem, ILocation location)
		{
			OrderPart orderPart = new OrderPart
          	{
          		Description = partItem.Description,
          		ManufacturerLineCode = partItem.ManufacturerLineCode,
          		ManufacturerName = partItem.ManufacturerName,
          		PartNumber = partItem.PartNumber,
          		QuantityAvailable = 0,
          		QuantityOrdered = 0,
          		QuantityRequested = partItem.QuantityRequested,
          		Status = ""
          	};
			if (location != null)
			{
				orderPart.UnitCore = location.UnitCore;
				orderPart.UnitCost = location.UnitCost;
				orderPart.UnitList = location.UnitList;
				orderPart.UnitPrice = location.UnitCost * 1.5M;
				orderPart.LocationName = location.Name;
				orderPart.LocationId = location.Id;
			}
			order.Request.Parts.Add(orderPart);
		}

		private void AddPriceCheckAlternatePartItemToOrder(IPriceCheckAlternatePart partItem, IPriceCheckPart parentPart,
			ILocation location)
		{
			OrderPart orderPart = new OrderPart
          	{
          		Description = partItem.Description,
          		ManufacturerLineCode = partItem.ManufacturerLineCode,
          		ManufacturerName = partItem.ManufacturerName,
          		PartNumber = partItem.PartNumber,
          		QuantityAvailable = 0,
          		QuantityOrdered = 0,
          		QuantityRequested = parentPart.QuantityRequested,
          		Status = "",
          		UnitCore = location.UnitCore,
          		UnitCost = location.UnitCost,
          		UnitList = location.UnitList,
          		UnitPrice = (location.UnitCost*1.5M),
          		LocationName = location.Name,
          		LocationId = location.Id
          	};
			order.Request.Parts.Add(orderPart);
		}

        private void setButtonState()
        {
            int countCartItems = 0;
			int countPriceCheckFoundItems = 0;
			int countPriceCheckNotFoundItems = 0;
			int countPriceCheckAltItems = 0;

            foreach (TreeNode nodeMain in treeViewCart.Nodes)
            {
                if (nodeMain.Nodes.Count > 0)
                {
                    foreach (TreeNode nodePart in nodeMain.Nodes)
                    {
                        if (nodePart.Checked && nodePart.Tag is IPartItem)
                        {
                        	countCartItems++;
                        }
						else if (nodePart.Checked && nodePart.Tag is IPriceCheckPart)
                        {
							if (((IPriceCheckPart)nodePart.Tag).Found)
							{
								countPriceCheckFoundItems++;
							}
							else
							{
								countPriceCheckNotFoundItems++;
							}
                        }
						else if (!nodePart.Checked && nodePart.Tag is IPriceCheckPart)
						{
							foreach (TreeNode altParts in nodePart.Nodes)
							{
								if(altParts.Checked && altParts.Tag is IPriceCheckAlternatePart)
								{
									countPriceCheckAltItems++;
								}
							}
						}
                    }
                }
            }
			if (catalog == null)
			{
				buttonOrderParts.Enabled = false;
				buttonPriceCheck.Enabled = false;
			}
			else
			{
				SetPriceCheckButtonState(countCartItems, countPriceCheckFoundItems, countPriceCheckNotFoundItems,
					countPriceCheckAltItems);
				SetOrderPartsButtonState(countCartItems, countPriceCheckFoundItems, countPriceCheckNotFoundItems,
					countPriceCheckAltItems);
			}
			ShowOrderMessage();
			ShowDeliveryOptions();
        }

		private void ShowDeliveryOptions()
		{
			// Show the delivery options?
            deliveryMethodLabel.Visible = (buttonOrderParts.Enabled && catalog.DeliveryMethod != null);
		}

		private void ShowOrderMessage()
		{
			// Show the order message?
			bool showOrderMessage = (buttonOrderParts.Enabled && catalog.SupportsOrderMessage);
			orderMessage.Visible = showOrderMessage;
			orderMessageLabel.Visible = showOrderMessage;
		}

    	private void SetPriceCheckButtonState(int countCartItems, int countPriceCheckFoundItems,
			int countPriceCheckNotFoundItems, int countPriceCheckAltItems)
    	{
    		if (catalog.SupportsPriceCheck)
    		{
    			buttonPriceCheck.Visible = true;
    			buttonPriceCheck.Enabled =
    				(countCartItems + countPriceCheckFoundItems + 
    				 countPriceCheckNotFoundItems) > 0 && countPriceCheckAltItems == 0;       	
    		}
    		else
    		{
    			buttonPriceCheck.Visible = false;
    		}
    	}

    	private void SetOrderPartsButtonState(int countCartItems, int countPriceCheckFoundItems,
			int countPriceCheckNotFoundItems, int countPriceCheckAltItems)
    	{
    		if(catalog.RequiresPriceCheck)
    		{
    			if (catalog.AllowsNotFoundPartsToBeOrdered)
    			{
    				buttonOrderParts.Enabled = ((countCartItems) == 0 &&
    				                            (countPriceCheckFoundItems + countPriceCheckAltItems + 
    				                             countPriceCheckNotFoundItems) > 0);
    			}
    			else
    			{
    				buttonOrderParts.Enabled = ((countCartItems + countPriceCheckNotFoundItems) == 0 &&
    				                            (countPriceCheckFoundItems + countPriceCheckAltItems) > 0);
    			}
    		}
    		else
    		{
    			if (catalog.AllowsNotFoundPartsToBeOrdered)
    			{
    				buttonOrderParts.Enabled = ((countCartItems + countPriceCheckFoundItems + countPriceCheckAltItems +
    				                             countPriceCheckNotFoundItems) > 0);
    			}
    			else
    			{
    				buttonOrderParts.Enabled = ((countPriceCheckNotFoundItems) == 0 &&
    				                            (countCartItems + countPriceCheckFoundItems + 
    				                             countPriceCheckAltItems) > 0);
    			}
    		}
    	}

		private void SetDeliveryMethodOptions()
		{
			if (catalog != null && catalog.DeliveryMethod != null)
			{
			    radioButtonDeliver.Checked = true;
			    radioButtonWillCall.Checked = false;
			}
		}

		#endregion

		#region Event Handlers

		private void treeViewCart_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			propertyGridCartItems.SelectedObject = e.Node.Tag;
		}

		private void treeViewCart_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			e.Node.Checked = !(e.Node.Checked);
		}

		private void treeViewCart_AfterCheck(object sender, TreeViewEventArgs e)
		{
			if (e.Node.Name == "Parts")
			{
				foreach (TreeNode parts in e.Node.Nodes)
				{
					parts.Checked = e.Node.Checked;
				}
			}
			else if (e.Node.Tag is IPriceCheckAlternatePart)
			{
				if (e.Node.Checked)
				{
					e.Node.Parent.Checked = false;
					bool isLocationSelected = false;
					foreach (TreeNode locationNode in e.Node.Nodes)
					{
						if (locationNode.Checked)
						{
							isLocationSelected = true;
						}
					}
					if (!isLocationSelected && e.Node.Nodes.Count > 0)
					{
						e.Node.Nodes[0].Checked = true;
					}
				}
				else
				{
					foreach (TreeNode locationNode in e.Node.Nodes)
					{
						locationNode.Checked = false;
					}
				}

			}
			else if (e.Node.Tag is IPriceCheckPart)
			{
				if (e.Node.Checked)
				{
					bool isLocationSelected = false;
					foreach (TreeNode subNode in e.Node.Nodes)
					{
						if (subNode.Tag is IPriceCheckAlternatePart)
						{
							subNode.Checked = false;
						}
						else
						{
							if (subNode.Checked)
							{
								isLocationSelected = true;
							}
						}
					}
					if (!isLocationSelected && e.Node.Nodes.Count > 0)
					{
						e.Node.Nodes[0].Checked = true;
					}
				}
				else
				{
					foreach (TreeNode locationNode in e.Node.Nodes)
					{
						if (locationNode.Tag is ILocation)
						{
							locationNode.Checked = false;
						}
					}
				}
			}
			else if (e.Node.Tag is ILocation)
			{
				if (e.Node.Checked)
				{
					e.Node.Parent.Checked = true;
					foreach (TreeNode subNodes in e.Node.Parent.Nodes)
					{
						if (subNodes != e.Node)
						{
							if (subNodes.Tag is IPriceCheckAlternatePart)
							{
								subNodes.Checked = false;
							}
							subNodes.Checked = false;
						}
					}

				}
			}
			setButtonState();
			treeViewCart.SelectedNode = e.Node;
		}

		private void treeViewCart_BeforeCheck(object sender, TreeViewCancelEventArgs e)
		{
			if (catalog != null && e.Node.Tag is IPartItem && !catalog.AllowsBlankManufacturerCode)
			{
				IPartItem part = (IPartItem)e.Node.Tag;
				if (part.ManufacturerLineCode == string.Empty)
				{
					e.Cancel = true;
				}
			}
			else if (e.Node.Tag is ILocation)
			{
				if (e.Node.Checked && e.Node.Parent.Checked)
				{
					int taggedLocationsCount = 0;
					foreach (TreeNode node in e.Node.Parent.Nodes)
					{
						if (node.Tag is ILocation && node.Checked)
						{
							taggedLocationsCount++;
						}
					}
					if (taggedLocationsCount == 1)
					{
						e.Cancel = true;
					}
				}
			}
		}

		private void buttonPriceCheck_Click(object sender, EventArgs e)
		{
			priceCheck.Parts.Clear();
			var treeNodes = treeViewCart.Nodes["Parts"].Nodes;
			for (int i = 0; i < treeNodes.Count; i++)
			{
				TreeNode part = treeNodes[i];
				if (part.Checked)
				{
					PriceCheckPart priceCheckPart = ConvertToPriceCheckPart(part);
					if (priceCheckPart != null)
					{
						priceCheckPart.GridIndex = i + 1;
						priceCheck.Parts.Add(priceCheckPart);
					}
				}
			}
			PriceCheckHandlingErrors();
			foreach (PriceCheckPart priceCheckPart in priceCheck.Parts)
			{
				TreeNode part = treeNodes[priceCheckPart.GridIndex - 1];
				AddPriceCheckPartToTree(part, priceCheckPart);
			}
			treeViewCart.ExpandAll();
		}

    	private static PriceCheckPart ConvertToPriceCheckPart(TreeNode part)
    	{
    		PriceCheckPart priceCheckPart;
    		if (part.Tag is IPartItem)
    		{
    			priceCheckPart = ConvertToPriceCheckPart((IPartItem) part.Tag);
    		}
    		else if (part.Tag is PriceCheckPart)
    		{
    			priceCheckPart = (PriceCheckPart) part.Tag;
    			priceCheckPart.Locations.Clear();
    			priceCheckPart.AlternateParts.Clear();
    		}
    		else
    		{
    			priceCheckPart = null;
    		}
    		return priceCheckPart;
    	}

    	private void PriceCheckHandlingErrors()
    	{
	        try
	        {
		        if (catalog.HostShowsProgressOnPriceCheck)
			        throw new Exception("Online Catalogs show their own progress UI");

				catalog.PriceCheck(priceCheck);
	        }
	        catch (OperationCanceledException)
	        {
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
				catalogExceptionHandler.ShowPriceCheckOrderPartsCatalogExceptionMessage(ex.Message);
    		}
			catch (Exception ex)
			{
				catalogExceptionHandler.ShowGeneralCatalogExceptionMessage(ex.Message);
			}
		}


    	private void buttonOrderParts_Click(object sender, EventArgs e)
        {
	        if (string.IsNullOrWhiteSpace(textBoxPONumber.Text))
		        textBoxPONumber.Text = $"#{startingPO}";

			buttonOrderTracking.Text = "Track Order...";
			order.Request.Parts = new List<IOrderPart>();
			order.Request.PurchaseOrderNumber = textBoxPONumber.Text;

			var treeNodes = treeViewCart.Nodes["Parts"].Nodes;
			for (int i = 0; i < treeNodes.Count; i++)
    		{
				TreeNode part = treeNodes[i];
    			if (part.Checked && part.Tag is IPartItem)
    			{
    				AddPartItemToOrder((IPartItem) part.Tag);
    			}
    			else if (part.Tag is IPriceCheckPart)
    			{
    				if (part.Checked)
    				{
    					ILocation location = null;
    					foreach (TreeNode locationNode in part.Nodes)
    					{
    						if (locationNode.Tag is ILocation && locationNode.Checked)
    						{
    							location = (ILocation) locationNode.Tag;
    						}
    					}
    					AddPriceCheckPartItemToOrder((PriceCheckPart) part.Tag, location);
    				}
    				else
    				{
    					foreach (TreeNode subnode in part.Nodes)
    					{
    						if (subnode.Tag is IPriceCheckAlternatePart && subnode.Checked)
    						{
    							ILocation location = null;
    							foreach (TreeNode locationNode in subnode.Nodes)
    							{
    								if (locationNode.Tag is ILocation && locationNode.Checked)
    								{
    									location = (ILocation) locationNode.Tag;
    								}
    							}
    							AddPriceCheckAlternatePartItemToOrder((PriceCheckAlternatePart) subnode.Tag,
    							                                      (PriceCheckPart) part.Tag, location);
    						}
    					}
    				}
    			}
    		}
    		if (catalog.SupportsOrderMessage)
			{
				order.Request.OrderMessage = orderMessage.Text;
			}
			if (catalog.DeliveryMethod != null)
			{
			    order.Request.DeliveryOption = ToCatalogShippingOption(radioButtonDeliver.Checked ?POShipVia.Deliver : POShipVia.WillCall);
			}
			else
			{
			    order.Request.DeliveryOption = ToCatalogShippingOption(POShipVia.Deliver);
			}

			try
			{
				if (catalog.HostShowsProgressOnPriceCheck)
					throw new Exception("Online Catalogs show their own progress UI");

				order.Response = catalog.OrderParts(order.Request);
				if (order.Response != null)
					AddOrderToTree();
			}
			catch (OperationCanceledException)
			{
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
				catalogExceptionHandler.ShowPriceCheckOrderPartsCatalogExceptionMessage(ex.Message);
			}
			catch (Exception ex)
			{
				catalogExceptionHandler.ShowGeneralCatalogExceptionMessage(ex.Message);
			}
			// below forces the property grid to refresh (when Order node is selected)
			propertyGridCartItems.SelectedObjects = propertyGridCartItems.SelectedObjects;

			textBoxPONumber.Text = $"#{++startingPO}";
		}



		#endregion

	    public string ToCatalogShippingOption(POShipVia option)
	    {
            return (option == POShipVia.Deliver) ? "Deliver" : "WillCall";
	    }

		private void treeViewCart_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Delete)
			{
				TreeNode node = treeViewCart.SelectedNode;
				if (node.Tag is IPriceCheckPart)
				{
					treeViewCart.Nodes.Remove(node);
				}
			}
		}

		private void buttonOrderTracking_Click(object sender, EventArgs e)
		{
			if (order.Response == null)
			{
				MessageBox.Show("No Order Processed", "Error");
				return;
			}

			if (string.IsNullOrWhiteSpace(order.Response.TrackingNumber))
			{
				MessageBox.Show("Order Has No Tracking #", "Error");
				return;
			}

			try
			{
				var details = catalog.RequestOrderTracking(order.Response.TrackingNumber);
				if (details == null)
				{
					MessageBox.Show("Unable to get tracking status", "Error");
					return;
				}

				if (details.ExternalTrackingUrl.Scheme != "http" && details.ExternalTrackingUrl.Scheme != "https")
				{
					MessageBox.Show("Unsupported URL: " + details.ExternalTrackingUrl, "Error");
					return;
				}

				if (MessageBox.Show($"Status: {details.StatusDisplay}\r\nURL: {details.ExternalTrackingUrl}\r\n\r\nOpen URL?", "Order Tracking", MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					Process.Start(details.ExternalTrackingUrl.ToString());
				}
			}
			catch (OperationCanceledException)
			{
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}
	}

	public enum POShipVia
    {
        WillCall = 0,
        Deliver = 1
    }

	
}
