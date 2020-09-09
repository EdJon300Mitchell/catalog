using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Mitchell1.Catalog.Driver.Helpers;
using Mitchell1.Online.Catalog.Host.TransferObjects;

namespace Mitchell1.Catalog.Driver.Data
{
    internal class DriverData
    {
	    private static readonly string xmlPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"data\DriverData.xml");

		public DriverData()
		{
		}

		public void Load(Vendor vendor, Vehicle vehicle)
        {
			InitDocument();

			if (documentElement != null)
			{
				LoadVendor(vendor);
				LoadVehicle(vehicle);
			}
        }

		public void Save(Vendor vendor, Vehicle vehicle)
		{
			if (!File.Exists(xmlPath))
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(xmlPath))
				{
					XmlDocument dataXml = new XmlDocument();
					XmlNode driverData = dataXml.CreateNode("element", "DriverData", "");
					XmlNode catalogNode = dataXml.CreateNode("element", "Catalog", "");
					XmlNode vendorNode = dataXml.CreateNode("element", "VendorQualifier", "");
					XmlNode cartNode = dataXml.CreateNode("element", "Cart", "");
					driverData.AppendChild(catalogNode);
					driverData.AppendChild(vendorNode);
					driverData.AppendChild(cartNode);
					dataXml.AppendChild(driverData);
					dataXml.Save(xmlWriter);
					xmlWriter.Close();
				}
			}
			InitDocument();
			if (documentElement != null)
			{
				SaveVendor(vendor);
				SaveVehicle(vehicle);
				document.Save(xmlPath);
			}
		}

		private XmlDocument document;
    	private XmlElement documentElement;

		private void SaveVendor(Vendor vendor)
        {
			SetInnerText("VendorQualifier", vendor.Qualifier);
			SetInnerText("VendorName", vendor.Name);
			SetInnerText("VendorCode", vendor.Code);
			SetInnerText("VendorCatalog", vendor.Catalog);
		}

    	private void LoadVendor(Vendor vendor)
        {
			vendor.Qualifier = documentElement.SelectInnerText("VendorQualifier");
			vendor.Name = documentElement.SelectInnerText("VendorName");
			vendor.Code = documentElement.SelectInnerText("VendorCode");
			vendor.Catalog = documentElement.SelectInnerText("VendorCatalog");
        }

		private void SaveVehicle(Vehicle vehicle)
		{
			XmlNode vehicleNode = documentElement.SelectSingleNode("Vehicle");
			if (vehicleNode == null)
			{
				vehicleNode = document.CreateNode("element", "Vehicle", "");
				documentElement.AppendChild(vehicleNode);
			}
			vehicleNode.RemoveAll();
			vehicleNode.SetInnerText("AaiaId", vehicle.AaiaId.ToString(CultureInfo.InvariantCulture));
			vehicleNode.SetInnerText("AcesEngineId", vehicle.AcesEngineId.ToString(CultureInfo.InvariantCulture));
			vehicleNode.SetInnerText("AcesId", vehicle.AcesId.ToString(CultureInfo.InvariantCulture));
			vehicleNode.SetInnerText("AcesBaseId", vehicle.AcesBaseId.ToString(CultureInfo.InvariantCulture));
			vehicleNode.SetInnerText("Body", vehicle.Body);
			vehicleNode.SetInnerText("Brake", vehicle.Brake);
			vehicleNode.SetInnerText("DriveType", vehicle.DriveType);
			vehicleNode.SetInnerText("Engine", vehicle.Engine);
			vehicleNode.SetInnerText("Gvw", vehicle.Gvw);
			vehicleNode.SetInnerText("Make", vehicle.Make);
			vehicleNode.SetInnerText("Model", vehicle.Model);
			vehicleNode.SetInnerText("Qualifier", vehicle.Qualifier);
			vehicleNode.SetInnerText("SubModel", vehicle.SubModel);
			vehicleNode.SetInnerText("Transmission", vehicle.Transmission);
			vehicleNode.SetInnerText("Vin", vehicle.Vin);
			vehicleNode.SetInnerText("Year", vehicle.Year.ToString(CultureInfo.InvariantCulture));
		    vehicleNode.SetInnerText("AcesEngineConfigId", vehicle.AcesEngineConfigId.ToString(CultureInfo.InvariantCulture));
            vehicleNode.SetInnerText("AcesSubModelId", vehicle.AcesSubModelId.ToString(CultureInfo.InvariantCulture));
		}

		private void LoadVehicle(Vehicle vehicle)
		{
			XmlNode vehicleNode = documentElement.SelectSingleNode("Vehicle");
			if (vehicleNode != null)
			{
				vehicle.AaiaId = Convert.ToInt32(vehicleNode.SelectInnerText("AaiaId"));
				vehicle.AcesEngineId = Convert.ToInt32(vehicleNode.SelectInnerText("AcesEngineId"));
				vehicle.AcesId = Convert.ToInt32(vehicleNode.SelectInnerText("AcesId"));
				if (vehicleNode.SelectInnerText("AcesBaseId") == string.Empty)
				{
					vehicleNode.SetInnerText("AcesBaseId", vehicle.AcesBaseId.ToString(CultureInfo.InvariantCulture));
				}
				vehicle.AcesBaseId = Convert.ToInt32(vehicleNode.SelectInnerText("AcesBaseId"));

				vehicle.Body = vehicleNode.SelectInnerText("Body");
				vehicle.Brake = vehicleNode.SelectInnerText("Brake");
				vehicle.DriveType = vehicleNode.SelectInnerText("DriveType");
				vehicle.Engine = vehicleNode.SelectInnerText("Engine");
				vehicle.Gvw = vehicleNode.SelectInnerText("Gvw");
				vehicle.Make = vehicleNode.SelectInnerText("Make");
				vehicle.Model = vehicleNode.SelectInnerText("Model");
				vehicle.Qualifier = vehicleNode.SelectInnerText("Qualifier");
				vehicle.SubModel = vehicleNode.SelectInnerText("SubModel");
				vehicle.Transmission = vehicleNode.SelectInnerText("Transmission");
				vehicle.Vin = vehicleNode.SelectInnerText("Vin");
				vehicle.Year = Convert.ToInt32(vehicleNode.SelectInnerText("Year"));
			    vehicle.AcesEngineConfigId = Convert.ToInt32(vehicleNode.SelectInnerText("AcesEngineConfigId"));
			    vehicle.AcesSubModelId = Convert.ToInt32(vehicleNode.SelectInnerText("AcesSubModelId"));
			}
		}

    	private void SetInnerText(string xpath, string text)
		{
			XmlNode node = documentElement.SelectSingleNode(xpath);
			if (node == null)
			{
				node = document.CreateNode("element", xpath, "");
				documentElement.AppendChild(node);
			}
			node.InnerText = text;
		}

		private void InitDocument()
    	{
			try
			{
				document = new XmlDocument();
				document.Load(xmlPath);
				documentElement = document.DocumentElement;
			}
			catch (Exception)
			{
			}
		}
    }

	internal static class XmlDocumentHelpers
	{
		public static string SelectInnerText(this XmlNode parentNode, string elementName)
		{
			XmlNode node = parentNode.SelectSingleNode(elementName);
			return (node != null) ? node.InnerText : String.Empty;
		}

		public static void SetInnerText(this XmlNode parentNode, string elementName, string innerText)
		{
			XmlNode node = parentNode.OwnerDocument.CreateNode("element", elementName, "");
			node.InnerText = innerText;
			parentNode.AppendChild(node);
		}
	}
}
