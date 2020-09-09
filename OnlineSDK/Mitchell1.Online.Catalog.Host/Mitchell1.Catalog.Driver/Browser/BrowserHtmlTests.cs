using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mitchell1.Browser.Interfaces;
using Mitchell1.Catalog.Framework.Common;

namespace Mitchell1.Catalog.Driver.Browser
{
	public partial class BrowserHtmlTests : Form
	{
		private IWebBrowserControl<Control> browser;

		public BrowserHtmlTests(string url)
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

			browser.Url = url;

			Controls.Add(control);
		}
	}
}
