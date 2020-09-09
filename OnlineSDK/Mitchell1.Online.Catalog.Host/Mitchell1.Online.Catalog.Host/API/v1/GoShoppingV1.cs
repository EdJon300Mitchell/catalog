using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Mitchell1.Browser.Interfaces;
using Mitchell1.Catalog.Framework.Interfaces;
using Mitchell1.Online.Catalog.Host.Controllers;
using Mitchell1.Online.Catalog.Host.TransferObjects;

namespace Mitchell1.Online.Catalog.Host.API.v1
{
    public class GoShoppingV1 : IEmbeddedCatalogTransferController
    {
        private readonly OnlineCatalogInformation onlineCatalogInformation;
        private EventHandler<WebControlNavigatedEventArgs> eventHandler;

        private string GoShoppingUrl => onlineCatalogInformation.GetAbsoluteUrl(CatalogApiPart.GoShopping);

        public GoShoppingV1(OnlineCatalogInformation catalogInformation)
        {
            onlineCatalogInformation = catalogInformation;
        }

        public event EventHandler<bool> RequestCompleted;

        public void AttachBrowser<T>(IWebBrowserControl<T> browser)
        {
            eventHandler = delegate(object sender, WebControlNavigatedEventArgs args)
            {
                // Ignore non main frame and internal http codes
                if (args.HttpCode != 0 && args.Frame.Identifier == browser.MainFrame?.Identifier)
                {
                    HttpResponseCode = args.HttpCode;

                    // 403 we treat special, close window and throw auth error
                    if (args.HttpCode == 403)
                    {
                        RequestCompleted?.Invoke(this, false);
                    }
                }
            };

            browser.JavaScriptRegisterActionCallback("transfer", TransferCart);
            browser.JavaScriptRegisterActionCallback("cancel", CancelChanges);
            browser.Navigated += eventHandler;

            browser.Url = BuildGoShoppingCatalogUrl(Vendor, Vehicle);
        }

        public void DetachBrowser<T>(IWebBrowserControl<T> browser)
        {
            browser.Navigated -= eventHandler;
            browser.JavaScriptUnregisterAction("transfer");
            browser.JavaScriptUnregisterAction("cancel");
        }

        public IVendor Vendor { get; set; }
        public IHostData HostData { get; set; }
        public ShoppingCart Cart { get; set; }
        public IVehicle Vehicle { get; set; }
        public int HttpResponseCode { get; private set; }

        public string BuildGoShoppingCatalogUrl(IVendor vendor, IVehicle vehicleSettings)
        {
            StringBuilder url = new StringBuilder(GoShoppingUrl.Length + 100);

            // pass user authentication details to the website
            url.Append(GoShoppingUrl + "?Qualifier=");
            url.Append(Uri.EscapeDataString(vendor.Qualifier ?? ""));

            // pass vehicle settings to the website
            if (vehicleSettings != null)
            {
                if (!string.IsNullOrEmpty(vehicleSettings.Vin) && vehicleSettings.Vin.Length >= 17)
                {
                    url.Append("&Vin=" + Uri.EscapeDataString(vehicleSettings.Vin));
                }

                if (url.AddIntPropertyIfValid(nameof(vehicleSettings.Year), vehicleSettings.Year))
                {
                    if (url.AddStringPropertyIfValid(nameof(vehicleSettings.Make), vehicleSettings.Make))
                    {
                        if (url.AddStringPropertyIfValid(nameof(vehicleSettings.Model), vehicleSettings.Model))
                        {
                            url.AddStringPropertyIfValid(nameof(vehicleSettings.SubModel), vehicleSettings.SubModel);
                            url.AddStringPropertyIfValid(nameof(vehicleSettings.Transmission), vehicleSettings.Transmission);
                            url.AddStringPropertyIfValid(nameof(vehicleSettings.Engine), vehicleSettings.Engine);
                            url.AddStringPropertyIfValid(nameof(vehicleSettings.DriveType), vehicleSettings.DriveType);
                            url.AddStringPropertyIfValid(nameof(vehicleSettings.Brake), vehicleSettings.Brake);
                            url.AddStringPropertyIfValid(nameof(vehicleSettings.Gvw), vehicleSettings.Gvw);
                            url.AddStringPropertyIfValid(nameof(vehicleSettings.Body), vehicleSettings.Body);
                        }
                    }
                }

                url.AddIntPropertyIfValid(nameof(vehicleSettings.AcesId), vehicleSettings.AcesId);
                url.AddIntPropertyIfValid(nameof(vehicleSettings.AcesBaseId), vehicleSettings.AcesBaseId);
                url.AddIntPropertyIfValid(nameof(vehicleSettings.AcesEngineId), vehicleSettings.AcesEngineId);

                IVehicleAcesProvider acesProvider = vehicleSettings as IVehicleAcesProvider;
                if (acesProvider != null)
                {
                    url.AddIntPropertyIfValid("AcesEngineBaseId", acesProvider.GetAcesId(AcesId.EngineBaseID));
                    url.AddIntPropertyIfValid("AcesEngineConfigId", acesProvider.GetAcesId(AcesId.EngineConfigID));
                    url.AddIntPropertyIfValid("AcesSubmodelId", acesProvider.GetAcesId(AcesId.SubModelID));
                }
            }

            return url.ToString();
        }

        private void CancelChanges(object[] objects)
        {
            RequestCompleted?.Invoke(this, false);
        }

        private void TransferCart(object[] objects)
        {
            if (objects == null || objects.Length < 2 || !(objects[1] is IList) || Cart == null)
            {
                RequestCompleted?.Invoke(this, false);
                return;
            }

            try
            {
                HandleShoppingCart((IList)objects[1]);
                RequestCompleted?.Invoke(this, true);
            }
            catch (Exception e)
            {
                string msg = "Unable to transfer shopping cart: " + e.Message;
                Trace.WriteLine(msg);
                MessageBox.Show(msg);

                RequestCompleted?.Invoke(this, false);
            }
        }

        private decimal ToDecimal(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return 0;
            }

            decimal outValue;
            if (decimal.TryParse(value, out outValue))
            {
                return outValue;
            }

            return 0;
        }

        private void HandleShoppingCart(IList onlineCart)
        {
            foreach (dynamic cartItem in onlineCart)
            {
                var type = (string)cartItem.type;
                if (String.IsNullOrEmpty(type))
                {
                    continue;
                }

                if (type == "IPartItem2")
                {
	                var part = new PartItem
	                {
		                PartNumber = cartItem.PartNumber,
		                ManufacturerLineCode = cartItem.ManufacturerLineCode,
		                ManufacturerName = cartItem.ManufacturerName,
		                Description = cartItem.Description,
		                UnitList = ToDecimal(cartItem.UnitList),
		                UnitCost = ToDecimal(cartItem.UnitCost),
		                UnitCore = ToDecimal(cartItem.UnitCore),
		                Quantity = ToDecimal(cartItem.Quantity),
		                IsTire = cartItem.IsTire,
		                Size = cartItem.Size,
		                UpcCode = cartItem.UpcCode,
		                PartCategory = ParseCategory(cartItem)
	                };

	                Cart.Items.Add(part);
                }
                else if (type == "IOrder")
                {
                    var newOrder = HandleOrderList(cartItem);
                    if (newOrder != null)
	                    Cart.Orders.Add(newOrder);
                }
                else if (type == "INoteItem")
                {
	                Cart.Items.Add(new NoteItem { Note = cartItem.Note });
                }
                else if (type == "ILaborItem")
                {
	                var labor = new LaborItem
	                {
		                Description = cartItem.Description,
		                Hours = ToDecimal(cartItem.Hours),
		                Price = ToDecimal(cartItem.Price)
	                };

	                Cart.Items.Add(labor);
                }
            }
        }

        private bool ExpandoHasProperty(ExpandoObject expando, string propertyName)
        {
            try
            {
                if (expando == null)
                    return false;

                if (((IDictionary<string, object>)expando).ContainsKey(propertyName))
                    return true;
            }
            catch (Exception)
            {
            }

            return false;
        }

        private PartCategory ParseCategory(dynamic partItem)
        {
            if (ExpandoHasProperty(partItem, "PartCategory") && !string.IsNullOrEmpty(partItem.PartCategory))
            {
	            if (Enum.TryParse(partItem.PartCategory, true, out PartCategory category))
                    return category;
            }

            return PartCategory.Unspecified;
        }

        private ShoppingCartOrder HandleOrderList(dynamic order)
        {
            var orderedPartsList = (IList)order.Parts;
            if (orderedPartsList.Count == 0)
                return null;

	        var transferOrder = new ShoppingCartOrder
	        {
		        OrderMessage = order.OrderMessage ?? "",
		        DeliveryOption = order.DeliveryOptions,
		        ConfirmationNumber = order.ConfirmationNumber ?? ""
	        };

	        if (ExpandoHasProperty(order, "TrackingNumber"))
	        {
		        var tracking = (string) order.TrackingNumber;
		        if (!string.IsNullOrWhiteSpace(tracking) && tracking.Length <= 20)
			        transferOrder.TrackingNumber = tracking;
	        }

	        foreach (dynamic orderedPart in orderedPartsList)
	        {
		        var newPart = new ShoppingCartOrderPart
		        {
			        Found = true,
			        LocationId = orderedPart.LocationId,
			        LocationName = orderedPart.LocationName,
			        Status = orderedPart.Status,
			        PartNumber = orderedPart.PartNumber,
			        ManufacturerLineCode = orderedPart.ManufacturerLineCode,
			        ManufacturerName = orderedPart.ManufacturerName,
			        Description = orderedPart.Description,
			        UnitList = ToDecimal(orderedPart.UnitList),
			        UnitCost = ToDecimal(orderedPart.UnitCost),
			        UnitCore = ToDecimal(orderedPart.UnitCore),
			        QuantityRequested = ToDecimal(orderedPart.QuantityRequested),
			        QuantityOrdered = ToDecimal(orderedPart.QuantityOrdered),
			        QuantityAvailable = ToDecimal(orderedPart.QuantityAvailable),
			        PartCategory = ParseCategory(orderedPart)
		        };


		        if (ExpandoHasProperty(orderedPart, nameof(ICartOrderedPart.Size)))
                    newPart.Size = orderedPart.Size ?? "";               

                transferOrder.Parts.Add(newPart);
            }

            return transferOrder;
        }
    }

    internal static class StringBuilderUrlExtensions
    {
        public static bool AddIntPropertyIfValid(this StringBuilder sb, string propertyName, int property)
        {
            if (sb == null || string.IsNullOrEmpty(propertyName))
            {
                return false;
            }

            if (property > 0)
            {
                sb.AppendFormat("&{0}={1}", propertyName, property.ToString(CultureInfo.InvariantCulture));
                return true;
            }

            return false;
        }

        public static bool AddStringPropertyIfValid(this StringBuilder sb, string propertyName, string property)
        {
            if (sb == null || string.IsNullOrEmpty(propertyName) || string.IsNullOrEmpty(property))
            {
                return false;
            }

            sb.AppendFormat("&{0}={1}", propertyName, Uri.EscapeDataString(property));
            return true;
        }
    }
}
