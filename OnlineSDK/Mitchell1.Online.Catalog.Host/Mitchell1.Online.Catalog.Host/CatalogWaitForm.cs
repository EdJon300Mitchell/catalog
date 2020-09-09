using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mitchell1.Online.Catalog.Host.API;

namespace Mitchell1.Online.Catalog.Host
{
	public partial class CatalogWaitForm : Form
	{
		private string detailMessage = "";

		public CatalogWaitForm()
		{
			InitializeComponent();
		}

		protected override async void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			if (Action == null)
			{
				Debug.Assert(false, "Should not have shown Catalog Wait Form with no action");
				DialogResult = DialogResult.OK;
				return;
			}

			try
			{
				await Action(CancellationToken.None);
				DialogResult = DialogResult.OK;
			}
			catch (Exception ex)
			{
				Error = ex;
				DialogResult = DialogResult.Abort;
			}
		}

		public string DetailMessage
		{
			get => detailMessage;
			set
			{
				detailMessage = value ?? "";
				labelMessage.Text = $"Processing {detailMessage} Request...";
			}
		}

		public Func<CancellationToken, Task> Action { get; set; }

		public Exception Error { get; private set; }
	}
}
