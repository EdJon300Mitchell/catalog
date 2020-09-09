using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Mitchell1.Browser.Interfaces;
using Mitchell1.Catalog.Framework.Common;
using Mitchell1.Online.Catalog.Host.API;
using Mitchell1.Online.Catalog.Host.Properties;

namespace Mitchell1.Online.Catalog.Host
{
	public partial class CatalogErrorForm : Form
	{
		private IWebBrowserControl<Control> browserControl;
		private readonly OnlineCatalogInformation catalog;

		public CatalogErrorForm(OnlineCatalogInformation catalog, ApiCallException exception)
		{
			InitializeComponent();

			this.catalog = catalog;

			SetupUi(exception);
		}

		private void SetupUi(ApiCallException exception)
		{
			var catalogName = catalog?.DisplayName ?? "Catalog";
			Text = catalogName + " Error";
			groupBox.Text = catalogName;

			linkLabelSupport.Visible = !string.IsNullOrWhiteSpace(catalog?.SupportUrl);
			linkLabelSupport.TabStop = false;

			labelPhone.Visible = !string.IsNullOrWhiteSpace(catalog?.SupportPhone);
			labelPhone.Text = $"Support Phone #{catalog?.SupportPhone ?? ""}";

			if (exception.Type == ApiCallException.ContentType.HTML)
			{
				// Browser
				browserControl = WebBrowserFactory.CreateBrowserControl();
				browserControl.Control.Dock = DockStyle.Fill;
				browserControl.OpenExternalWindow = true;
				browserControl.DocumentText = exception.Message;

				panelBrowser.Controls.Add(browserControl.Control);
			}
			else
			{
				var errorPanel = new Panel {Dock = DockStyle.Fill, Padding = new Padding(5), Margin = new Padding(5)};

				panelBrowser.Controls.Add(errorPanel);
				
				// Ensure we have windows newlines
				var text = exception.Message.Replace("\r", "").Replace("\n", "\r\n");
				var label = new Label
				{
					Dock = DockStyle.Fill,
					Text = text,
					TextAlign = ContentAlignment.MiddleLeft,
					Padding = new Padding(10),
					Font = new Font("Arial", 8.25F, FontStyle.Bold, GraphicsUnit.Point)
				};

				var picture = new PictureBox {SizeMode = PictureBoxSizeMode.AutoSize, Image = Resources.stop, Dock = DockStyle.Left};

				errorPanel.Controls.Add(label);
				errorPanel.Controls.Add(picture);
			}
		}

		private void linkLabelSupport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(catalog?.SupportUrl) || !Uri.IsWellFormedUriString(catalog.SupportUrl, UriKind.Absolute))
				return;

			var uri = new Uri(catalog.SupportUrl, UriKind.Absolute);
			if (uri.Scheme != "http" && uri.Scheme != "https")
				return;

			try
			{
				Process.Start(catalog.SupportUrl);
			}
			catch (Exception)
			{
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}
	}
}
