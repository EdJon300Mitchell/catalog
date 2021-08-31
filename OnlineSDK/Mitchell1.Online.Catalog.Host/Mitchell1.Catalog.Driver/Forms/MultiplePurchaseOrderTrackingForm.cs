using Mitchell1.Catalog.Driver.Models;
using Mitchell1.Online.Catalog.Host;
using Mitchell1.Online.Catalog.Host.TransferObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mitchell1.Catalog.Driver.Forms
{
    public partial class MultiplePurchaseOrderTrackingForm : Form
    {
        private IList<PurchaseOrderGridRow> _purchaseOrderRows;
        private int _maximumNumberOfOrderTrackingConcurrentRequests;
        private IOnlineCatalog _catalog;
        private Func<PurchaseOrder, ValidationResponse> _validatePurchaseOrderFunc;
        private Func<TrackingResponse, ValidationResponse> _validateTrackingResponseFunc;

        public MultiplePurchaseOrderTrackingForm(IList<PurchaseOrderGridRow> purchaseOrders,
            int maximumNumberOfOrderTrackingConcurrentRequests,
            IOnlineCatalog catalog,
            Func<PurchaseOrder, ValidationResponse> validatePurchaseOrderFunc,
            Func<TrackingResponse, ValidationResponse> validateTrackingResponseFunc)
        {
            _purchaseOrderRows = purchaseOrders ?? throw new ArgumentException(nameof(purchaseOrders));
            _maximumNumberOfOrderTrackingConcurrentRequests = maximumNumberOfOrderTrackingConcurrentRequests <= 0 ? 1 : maximumNumberOfOrderTrackingConcurrentRequests;
            _catalog = catalog;
            _validatePurchaseOrderFunc = validatePurchaseOrderFunc ?? throw new ArgumentException(nameof(validatePurchaseOrderFunc));
            _validateTrackingResponseFunc = validateTrackingResponseFunc ?? throw new ArgumentException(nameof(validateTrackingResponseFunc));
            InitializeComponent();
            // To use the DataGridLinkViewColumn, for clickable url, the column headers/types are added manually in the designer
            purchaseOrdersGrid.AutoGenerateColumns = false;
            purchaseOrdersGrid.DataSource = _purchaseOrderRows;
            purchaseOrdersGrid.Columns[nameof(PurchaseOrderGridRow.Parts)].Visible = false;
            purchaseOrdersGrid.Columns[nameof(PurchaseOrderGridRow.WasRetrieved)].Visible = false;
            purchaseOrdersGrid.CellContentClick += PurchaseOrdersGrid_CellContentClick;
        }

		private void PurchaseOrdersGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (sender is DataGridView dgv)
			{
				if (dgv.Columns[e.ColumnIndex].Name.IndexOf("url", 0, StringComparison.CurrentCultureIgnoreCase) != -1)
				{
					var url = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
					Uri uriResult;
					var validUrl = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
					               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
					if (validUrl)
						Process.Start(url);
				}
			}
		}

		private void requestTrackingDataButton_Click(object sender, EventArgs e)
        {
            var hadError = false;
            try
            {
                SetButtonStyling_Requesting();
                Task.Run(async () => await RequestTrackingData()).Wait();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                hadError = true;
            }
            finally
            {
                if (hadError)
                {
                    SetButtonStyling_Error();
                }
                else
                {
                    SetButtonStyling_Retrieved();
                }
            }
        }

        private async Task RequestTrackingData()
        {
            var statusColumnName = nameof(PurchaseOrderGridRow.Status);
            var urlColumnName = nameof(PurchaseOrderGridRow.Url);
            var trackingNumberColumnName = nameof(PurchaseOrderGridRow.TrackingNumber);
            var confirmationNumberColumnName = nameof(PurchaseOrderGridRow.ConfirmationNumber);
            var allTasks = new List<Task>();
            var throttling = new SemaphoreSlim(_maximumNumberOfOrderTrackingConcurrentRequests);
            foreach (DataGridViewRow gridRow in purchaseOrdersGrid.Rows)
            {
                await throttling.WaitAsync();
                allTasks.Add(
                    Task.Run(async () =>
                    {
                        var purchaseOrder = new PurchaseOrderGridRow()
                        {
                            TrackingNumber = gridRow.Cells[trackingNumberColumnName].Value.ToString(),
                            ConfirmationNumber = gridRow.Cells[confirmationNumberColumnName].Value.ToString()
                        };
                        var canRequestTracking = _validatePurchaseOrderFunc(purchaseOrder);
                        if (!canRequestTracking.IsTrue)
                        {
                            gridRow.Cells[statusColumnName].Value = "Error";
                            gridRow.Cells[urlColumnName].Value = canRequestTracking.ErrorMessage;
                            return;
                        }

                        try
                        {
                            var response = _catalog.RequestOrderTracking(purchaseOrder.TrackingNumber);
                            var wasAbleToRequestOrderTracking = _validateTrackingResponseFunc(response);
                            if (!wasAbleToRequestOrderTracking.IsTrue)
                            {
                                gridRow.Cells[statusColumnName].Value = "Error";
                                gridRow.Cells[urlColumnName].Value = wasAbleToRequestOrderTracking.ErrorMessage;
                                return;
                            }

                            gridRow.Cells[statusColumnName].Value = response.StatusDisplay;
                            gridRow.Cells[urlColumnName].Value = response.ExternalTrackingUrl.ToString();
                        }
                        catch (Exception exception)
                        {
                            gridRow.Cells[statusColumnName].Value = "Error";
                            gridRow.Cells[urlColumnName].Value = exception.Message;
                        }
                    }));
            }
            await Task.WhenAll(allTasks);
        }

        private void SetButtonStyling_Requesting()
        {
            this.requestTrackingDataButton.BackColor = System.Drawing.Color.Gray;
            this.requestTrackingDataButton.ForeColor = System.Drawing.Color.Black;
            this.requestTrackingDataButton.Enabled = false;
            this.requestTrackingDataButton.Text = "Requesting Tracking Data...";
        }

        private void SetButtonStyling_Error()
        {
            this.requestTrackingDataButton.BackColor = System.Drawing.Color.Red;
            this.requestTrackingDataButton.ForeColor = System.Drawing.Color.Black;
            this.requestTrackingDataButton.Enabled = false;
            this.requestTrackingDataButton.Text = "Request Error";
        }

        private void SetButtonStyling_Retrieved()
        {
            this.requestTrackingDataButton.BackColor = System.Drawing.Color.Beige;
            this.requestTrackingDataButton.ForeColor = System.Drawing.Color.Black;
            this.requestTrackingDataButton.Enabled = false;
            this.requestTrackingDataButton.Text = "Tracking Data Retrieved";
        }
    }
}
