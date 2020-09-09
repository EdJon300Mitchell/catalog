using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Mitchell1.Online.Catalog.Host;
using Mitchell1.Online.Catalog.Host.TransferObjects;

namespace Mitchell1.Catalog.Driver.Controls
{
    public partial class CatalogConfiguration : UserControl
    {
	    private string linkTarget = null;
	    private readonly ICatalogDriver catalogDriver;

		public CatalogConfiguration(ICatalogDriver catalogDriver)
		{
			this.catalogDriver = catalogDriver;

			InitializeComponent();

            comboBox1.SelectedIndex = 0;
	        dataGridViewURLs.AutoGenerateColumns = false;
	        linkLabelConfig.TabStop = false;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

	        var existingConfiguration = LoadConfiguration(ConfigurationPath) ?? DefaultValues();
			await PopulateUiFromConfiguration(existingConfiguration);
		}

	    private OnlineCatalogInformation LoadConfiguration(string file)
	    {
		    if (string.IsNullOrEmpty(file) || !File.Exists(file))
		    {
			    SetConfigLink("", "Not Saved Yet");
			    return null;
		    }

		    try
		    {
			    SetConfigLink(ConfigurationPath, ConfigurationPath);

			    var doc = XDocument.Load(file);
			    var root = (XElement)doc.FirstNode;
			    return ConfigurationSerializer.FromElement(root);
		    }
		    catch (Exception e)
		    {
			    MessageBox.Show("Failed to load online config: " + e.Message);
		    }

		    return null;
	    }

		private OnlineCatalogInformation DefaultValues()
	    {
		    return new OnlineCatalogInformation
		    {
				Identifier = "not_assigned",
			    DisplayName = "Test Catalog",
			    Description = "Sample Catalog Application",
			    SupportUrl = "https://www.example.com/help",
			    SupportPhone = "555-555-5555",
			    ApiBaseUrl = "http://localhost/NancyExampleCatalog",
				SupportsPriceCheck = true,
			    [CatalogApiPart.Icon] = "Content/icon.png",
			    [CatalogApiPart.Setup] = "View/vendorsetup",
			    [CatalogApiPart.GoShopping] = "View/goshopping",
			    [CatalogApiPart.PriceCheck] = "Api/PriceCheck",
			    [CatalogApiPart.PartsOrder] = "Api/OrderParts",
			    [CatalogApiPart.OrderTracking] = "Api/OrderTracking"
		    };
	    }

        private OnlineCatalogInformation GetOnlineCatalogInformationFromUi()
        {
	        var onlineCatalogInformation = new OnlineCatalogInformation
	        {
		        Identifier = textBoxIdentifier.Text,
		        DisplayName = textBoxName.Text,
		        Description = textBoxDescription.Text,
		        ApiVersionLevel = int.Parse(comboBox1.Text),
		        ApiBaseUrl = textBoxApiUrl.Text
	        };

	        var urls = (List<CatalogUrlComponent>)dataGridViewURLs.DataSource;
	        foreach (var url in urls)
	        {
		        onlineCatalogInformation[url.CatalogApiPart] = url.Value ?? "";
	        }

            if (checkBoxDeliver.Checked)
                onlineCatalogInformation.DeliveryMethod = new DefaultDeliveryMethod();

            onlineCatalogInformation.AllowsBlankManufacturerCode = checkBoxAllowsBlankManufacturerCode.Checked;
            onlineCatalogInformation.AllowsNotFoundPartsToBeOrdered = checkBoxAllowsNotFoundPartsToBeOrdered.Checked;
            onlineCatalogInformation.RequiresPriceCheck = checkBoxRequiresPriceCheck.Checked;
            onlineCatalogInformation.SupportsAlternateLocations = checkBoxSupportsAlternateLocations.Checked;
            onlineCatalogInformation.SupportsAlternateParts = checkBoxSupportsAlternateParts.Checked;
            onlineCatalogInformation.SupportsLocation = checkBoxSupportsLocation.Checked;
            onlineCatalogInformation.SupportsOrderMessage = checkBoxSupportsOrderMessage.Checked;
            onlineCatalogInformation.SupportsPriceCheck = checkBoxSupportsPriceCheck.Checked;

            onlineCatalogInformation.SupportUrl = textBoxSite.Text;
            onlineCatalogInformation.SupportPhone = textBoxSupportPhone.Text;

            return onlineCatalogInformation;
        }

        private async Task PopulateUiFromConfiguration(OnlineCatalogInformation onlineCatalogInformation)
        {
            textBoxIdentifier.Text = onlineCatalogInformation.Identifier;
            textBoxName.Text = onlineCatalogInformation.DisplayName;
            textBoxDescription.Text = onlineCatalogInformation.Description;
            comboBox1.Text = "1"; // onlineCatalogInformation.ApiVersionLevel = int.Parse(c);
            textBoxApiUrl.Text = onlineCatalogInformation.ApiBaseUrl;

	        dataGridViewURLs.DataSource = CatalogUrlComponent.GetUrlComponentList(onlineCatalogInformation);

            checkBoxDeliver.Checked = onlineCatalogInformation.DeliveryMethod != null;
            checkBoxAllowsBlankManufacturerCode.Checked = onlineCatalogInformation.AllowsBlankManufacturerCode;
            checkBoxAllowsNotFoundPartsToBeOrdered.Checked = onlineCatalogInformation.AllowsNotFoundPartsToBeOrdered;
            checkBoxRequiresPriceCheck.Checked = onlineCatalogInformation.RequiresPriceCheck;
            checkBoxSupportsAlternateLocations.Checked = onlineCatalogInformation.SupportsAlternateLocations;
            checkBoxSupportsAlternateParts.Checked = onlineCatalogInformation.SupportsAlternateParts;
            checkBoxSupportsLocation.Checked = onlineCatalogInformation.SupportsLocation;
            checkBoxSupportsOrderMessage.Checked = onlineCatalogInformation.SupportsOrderMessage;
            checkBoxSupportsPriceCheck.Checked = onlineCatalogInformation.SupportsPriceCheck;

            textBoxSite.Text = onlineCatalogInformation.SupportUrl; 
            textBoxSupportPhone.Text = onlineCatalogInformation.SupportPhone;

	        await LoadConfiguredCatalog();
		}

		private async void buttonCancel_Click(object sender, EventArgs e)
        {
	        if (MessageBox.Show("Are you sure you want to reset settings?", "Confirm Reset...", MessageBoxButtons.YesNo) == DialogResult.No)
		        return;

	        await PopulateUiFromConfiguration(DefaultValues());
		}

		private async void buttonUse_Click(object sender, EventArgs e)
        {
			try
			{
		        var online = GetOnlineCatalogInformationFromUi();
		        var catalogElement = ConfigurationSerializer.ToElement(online);
				var doc = new XDocument(catalogElement);

				File.WriteAllText(ConfigurationPath, doc.ToString(SaveOptions.None));

				SetConfigLink(ConfigurationPath, ConfigurationPath);

				await LoadConfiguredCatalog();
			}
	        catch (Exception ex)
	        {
		        MessageBox.Show("Failed to save online config: " + ex.Message);
	        }
		}

	    private async Task LoadConfiguredCatalog()
	    {
		    try
		    {
			    var onlineCatalogInformation = GetOnlineCatalogInformationFromUi();
			    var catalogInfo = new CatalogInfo();
			    await catalogInfo.LoadOnlineCatalogInformation(onlineCatalogInformation, CancellationToken.None);

			    catalogDriver.ConfiguredCatalog = catalogInfo;
		    }
		    catch (Exception exception)
		    {
			    MessageBox.Show(exception.ToString());
		    }
	    }

	    private void SetConfigLink(string path, string text)
	    {
		    linkTarget = path;
			linkLabelConfig.Text = text ?? "";
		}

        private string ConfigurationPath => "OnlineConfig.xml";

	    class CatalogUrlComponent
	    {
		    CatalogUrlComponent(CatalogApiPart apiPart, string value)
		    {
			    CatalogApiPart = apiPart;
			    Name = apiPart.ToString();
			    Value = value ?? "";
		    }

		    public static List<CatalogUrlComponent> GetUrlComponentList(OnlineCatalogInformation catalogInformation)
		    {
			    var definedKeys = catalogInformation.GetUrlComponentKeys();
			    var allKeys = Enum.GetValues(typeof(CatalogApiPart)).OfType<CatalogApiPart>().ToList();

			    var urlList = new List<CatalogUrlComponent>(allKeys.Count);

			    foreach (var key in allKeys)
			    {
				    urlList.Add(new CatalogUrlComponent(key, definedKeys.Contains(key) ? catalogInformation[key] : ""));
			    }

			    return urlList;
		    }

		    public CatalogApiPart CatalogApiPart { get; }
		    public string Name { get; }

			// Set is used in UI bindings
		    public string Value { get; set; }
	    }

		private void linkLabelConfig_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(linkTarget))
				return;

			var dstPath = Path.Combine(Environment.CurrentDirectory, linkTarget);
			if (!File.Exists(dstPath))
				return;

			Process.Start("explorer.exe", $"/select,\"{dstPath}\"");
		}

		private async void buttonLoad_Click(object sender, EventArgs e)
		{
			try
			{
				using (var ofd = new OpenFileDialog())
				{
					ofd.CheckFileExists = true;
					ofd.AutoUpgradeEnabled = true;
					ofd.Filter = "Config Files (XML)|*.xml";
					if (ofd.ShowDialog(this) != DialogResult.OK)
						return;

					File.Copy(ofd.FileName, ConfigurationPath, true);

					var config = LoadConfiguration(ConfigurationPath);
					if (config != null)
						await PopulateUiFromConfiguration(config);
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

	public interface ICatalogDriver
	{
		CatalogInfo ConfiguredCatalog { get; set; }
		ShoppingCart ShoppingCart { get; set; }
	}
}
