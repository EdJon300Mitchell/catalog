using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mitchell1.Browser.Interfaces;
using Mitchell1.Catalog.Framework.Common;

namespace Mitchell1.Catalog.Driver.Browser
{
	public partial class SampleBrowser : Form
	{
		private IWebBrowserControl<Control> browser;

		public SampleBrowser()
		{
			InitializeComponent();

			browser = WebBrowserFactory.CreateBrowserControl();
			var control = browser.Control;
			control.Dock = DockStyle.Fill;
			control.Location = new System.Drawing.Point(0, 0);
			control.MinimumSize = new System.Drawing.Size(20, 20);
			control.Name = "webBrowser1";
			control.Size = new System.Drawing.Size(621, 443);
			control.TabIndex = 0;

			browser.LoadError += BrowserOnLoadError;
			browser.Navigated += BrowserOnNavigated;
			browser.PopupRequested += BrowserOnPopupRequested;

			panel1.Controls.Add(control);
		}

		private void BrowserOnPopupRequested(object sender, WebControlPopupEventArgs args)
		{
			//args.Handled = true;
			//MessageBox.Show(@"Popup Not Supported: " + args.Url);
		}

		private void BrowserOnNavigated(object sender, WebControlNavigatedEventArgs args)
		{
			toolStripTextBoxUrl.Text = args.Url;
		}

		private void BrowserOnLoadError(object sender, WebControlErrorEventArgs args)
		{
			Trace.WriteLine(String.Format("Error Loading: {0}, Http: {1}, Text: {2}", args.Url, args.ErrorCode, args.Error));
		}

		private void toolStripButtonRefresh_Click(object sender, EventArgs e)
		{
			if (browser == null || browser.IsDisposed)
			{
				return;
			}

			try
			{
				Uri uri;
				if (Uri.TryCreate(toolStripTextBoxUrl.Text, UriKind.Absolute, out uri))
				{
					browser.Url = toolStripTextBoxUrl.Text;
				}
				else
				{
					browser.Url = "http://" + toolStripTextBoxUrl.Text;
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
				toolStripTextBoxUrl.Text = "";
			}
		}

		private void toolStripButtonDev_Click(object sender, EventArgs e)
		{
			if (browser == null || browser.IsDisposed || !browser.IsDeveloperToolAvailable)
			{
				return;
			}

			browser.ShowDeveloperTools();
		}

        private void toolStripTextBoxUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                e.Handled = true;
                toolStripButtonRefresh.PerformClick();
            }
        }

        private void toolStripButton1_CheckedChanged(object sender, EventArgs e)
        {
            browser.OpenExternalWindow = toolStripButton1.Checked;
        }
	}
}
