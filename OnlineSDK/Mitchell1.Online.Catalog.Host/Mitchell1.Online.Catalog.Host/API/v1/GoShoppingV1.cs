using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Mitchell1.Catalog.Framework.Interfaces;
using Mitchell1.Online.Catalog.Host.TransferObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Mitchell1.Online.Catalog.Host.API.v1.ParseHelper;
using PartCategory = Mitchell1.Online.Catalog.Host.TransferObjects.PartCategory;

namespace Mitchell1.Online.Catalog.Host.API.v1
{
	public class GoShoppingV1 : CustomWebPageControllerHandleForbidden
    {
        private readonly OnlineCatalogInformation onlineCatalogInformation;

        private string GoShoppingUrl => onlineCatalogInformation.GetAbsoluteUrl(CatalogApiPart.GoShopping);

        public GoShoppingV1(OnlineCatalogInformation catalogInformation) => onlineCatalogInformation = catalogInformation;

        protected override string ActionLabel { get; } = "transfer";

        public IVendor Vendor { get; set; }
        public IHostData HostData { get; set; }
        public ShoppingCart Cart { get; private set; }
        public IVehicle Vehicle { get; set; }

        protected override string Url => BuildGoShoppingCatalogUrl();
        private string BuildGoShoppingCatalogUrl()
        {
	        StringBuilder url = new StringBuilder(GoShoppingUrl.Length + 100);

            // pass user authentication details to the website
            url.Append(GoShoppingUrl + "?Qualifier=");
            url.Append(Uri.EscapeDataString(Vendor.Qualifier ?? ""));

            // pass vehicle settings to the website
            if (Vehicle != null)
            {
                if (!string.IsNullOrEmpty(Vehicle.Vin) && Vehicle.Vin.Length >= 17)
                {
                    url.Append("&Vin=" + Uri.EscapeDataString(Vehicle.Vin));
                }

                if (url.AddIntPropertyIfValid(nameof(Vehicle.Year), Vehicle.Year))
                {
                    if (url.AddStringPropertyIfValid(nameof(Vehicle.Make), Vehicle.Make))
                    {
                        if (url.AddStringPropertyIfValid(nameof(Vehicle.Model), Vehicle.Model))
                        {
                            url.AddStringPropertyIfValid(nameof(Vehicle.SubModel), Vehicle.SubModel);
                            url.AddStringPropertyIfValid(nameof(Vehicle.Transmission), Vehicle.Transmission);
                            url.AddStringPropertyIfValid(nameof(Vehicle.Engine), Vehicle.Engine);
                            url.AddStringPropertyIfValid(nameof(Vehicle.DriveType), Vehicle.DriveType);
                            url.AddStringPropertyIfValid(nameof(Vehicle.Brake), Vehicle.Brake);
                            url.AddStringPropertyIfValid(nameof(Vehicle.Gvw), Vehicle.Gvw);
                            url.AddStringPropertyIfValid(nameof(Vehicle.Body), Vehicle.Body);
                        }
                    }
                }

                url.AddIntPropertyIfValid(nameof(Vehicle.AcesId), Vehicle.AcesId);
                url.AddIntPropertyIfValid(nameof(Vehicle.AcesBaseId), Vehicle.AcesBaseId);
                url.AddIntPropertyIfValid(nameof(Vehicle.AcesEngineId), Vehicle.AcesEngineId);

                if (Vehicle is IVehicleAcesProvider acesProvider)
                {
                    url.AddIntPropertyIfValid(nameof(TransferObjects.Vehicle.AcesEngineBaseId), acesProvider.GetAcesId(AcesId.EngineBaseID));
                    url.AddIntPropertyIfValid(nameof(TransferObjects.Vehicle.AcesEngineConfigId), acesProvider.GetAcesId(AcesId.EngineConfigID));
                    url.AddIntPropertyIfValid(nameof(TransferObjects.Vehicle.AcesSubmodelId), acesProvider.GetAcesId(AcesId.SubModelID));
                }
            }

            if (HostData != null)
            {
	            url.AddStringPropertyIfValid(nameof(TransferObjects.HostData.ApplicationTitle), HostData.ApplicationTitle);
	            url.AddStringPropertyIfValid(nameof(TransferObjects.HostData.ApplicationVersion), HostData.ApplicationVersion);
	            url.AddStringPropertyIfValid(nameof(TransferObjects.HostData.LaborRate), HostData.LaborRate.ToString(CultureInfo.InvariantCulture));
	            url.AddStringPropertyIfValid(nameof(TransferObjects.HostData.HostApiLevel), OnlineCatalogInformation.HostApiLevel.ToString());
            }

            return url.ToString();
        }

        protected override bool Action(object[] objects)
        {
            if (objects == null || objects.Length < 2)
            {
	            return false;
            }

            try
            {
	            Cart = HandleShoppingCart(objects[1]);
	            return Cart != null;
            }
            catch (Exception e)
            {
                string msg = "Unable to transfer shopping cart: " + e.Message;
                Trace.WriteLine(msg);
                MessageBox.Show(msg);

                return false;
            }
        }

        internal static ShoppingCart HandleShoppingCart(dynamic shoppingCart)
        {
	        if (shoppingCart is string json)
		        return JsonConvert.DeserializeObject<ShoppingCart>(json, new CartItemConverter());

	        if (shoppingCart is IList list)
		        return ProcessListOfCartItems(list);

	        return null;
        }

        private static ShoppingCart ProcessListOfCartItems(IList onlineCart)
        {
	        if (onlineCart == null)
		        return null;

			var cart = new ShoppingCart();
			foreach (dynamic cartItem in onlineCart)
			{
				var type = (string) cartItem.type;
				if (string.IsNullOrEmpty(type))
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
						Size = GetPropertyOrNull(cartItem, nameof(PartItem.Size)),
						UpcCode = GetPropertyOrNull(cartItem, nameof(PartItem.UpcCode)),
						PartCategory = ParseCategory(cartItem),
						SupplierName = GetPropertyOrNull(cartItem, nameof(PartItem.SupplierName)),
						Metadata = GetPropertyOrNull(cartItem, nameof(PartItem.Metadata)),
						ShippingDescription = GetPropertyOrNull(cartItem, nameof(PartItem.ShippingDescription)),
						ShippingCost = ToDecimal(GetPropertyOrNull(cartItem, nameof(PartItem.ShippingCost))),
					};

					cart.Add(part);
				}
				else if (type == "IOrder")
				{
					ShoppingCartOrder newOrder = HandleOrderList(cartItem);
					if (newOrder != null)
						cart.Add(newOrder);
				}
				else if (type == "INoteItem")
				{
					cart.Add(new NoteItem {Note = cartItem.Note});
				}
				else if (type == "ILaborItem")
				{
					var labor = new LaborItem
					{
						Description = cartItem.Description,
						Hours = ToDecimal(cartItem.Hours),
						Price = ToDecimal(cartItem.Price)
					};

					cart.Add(labor);
				}
			}

			return cart;
		}

        private static PartCategory ParseCategory(dynamic partItem)
		{
			if (ExpandoHasProperty(partItem, "PartCategory") && !string.IsNullOrEmpty(partItem.PartCategory))
			{
				if (Enum.TryParse(partItem.PartCategory, true, out PartCategory category))
					return category;
			}

			return PartCategory.Unspecified;
		}

		private static ShoppingCartOrder HandleOrderList(dynamic order)
		{
			var orderedPartsList = (IList) order.Parts;
			if (orderedPartsList.Count == 0)
				return null;

			var transferOrder = new ShoppingCartOrder
			{
				OrderMessage = order.OrderMessage ?? "",
				DeliveryOptions = GetPropertyOrNull(order, "DeliveryOptions"),
				ConfirmationNumber = order.ConfirmationNumber ?? "",
				Parts = new List<ShoppingCartOrderPart>(),
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
					LocationId = GetPropertyOrNull(orderedPart, nameof(ShoppingCartOrderPart.LocationId)),
					LocationName = GetPropertyOrNull(orderedPart, nameof(ShoppingCartOrderPart.LocationName)),
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
					PartCategory = ParseCategory(orderedPart),
					SupplierName = GetPropertyOrNull(orderedPart, nameof(PartItem.SupplierName)),
					Metadata = GetPropertyOrNull(orderedPart, nameof(PartItem.Metadata)),
					ShippingDescription = GetPropertyOrNull(orderedPart, nameof(PartItem.ShippingDescription)),
					ShippingCost = ToDecimal(GetPropertyOrNull(orderedPart, nameof(PartItem.ShippingCost))),
				};


				if (ExpandoHasProperty(orderedPart, nameof(ICartOrderedPart.Size)))
					newPart.Size = orderedPart.Size ?? "";

				transferOrder.Parts.Add(newPart);
			}

			return transferOrder;
		}
    }

	public class CartItemConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => typeof(CartItem).IsAssignableFrom(objectType);
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var jsonObject = JObject.Load(reader);
			string type = (string)jsonObject[nameof(CartItem.type)];
			var item = CreateCartItem();
			serializer.Populate(jsonObject.CreateReader(), item);
			return item;

			CartItem CreateCartItem()
			{
				switch (type)
				{
					case "IPartItem2" : return new PartItem();
					case "INoteItem" : return new NoteItem();
					case "ILaborItem" : return new LaborItem();
					case "IOrder" : return new ShoppingCartOrder();
					default: return null;
				}
			}
		}
		public override bool CanWrite => false;
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {}
	}

	public static class ParseHelper
	{
		public static decimal ToDecimal(dynamic value) => value is string stringValue
			? !string.IsNullOrEmpty(stringValue) && decimal.TryParse(stringValue, out var outValue) ? outValue : 0
			: value != null ? Convert.ToDecimal(value) : 0;

		public static bool ExpandoHasProperty(ExpandoObject expando, string propertyName)
		{
			try
			{
				return expando != null && ((IDictionary<string, object>) expando).ContainsKey(propertyName);
			}
			catch (Exception)
			{
				return false;
			}
		}

		public static dynamic GetPropertyOrNull(ExpandoObject expando, string propertyName)
		{
			try
			{
				return expando != null && ((IDictionary<string, object>) expando).TryGetValue(propertyName, out var value) ? value : null;
			}
			catch (Exception)
			{
				return null;
			}
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
