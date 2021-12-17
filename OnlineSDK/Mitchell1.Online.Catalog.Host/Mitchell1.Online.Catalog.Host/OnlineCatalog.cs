using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mitchell1.Catalog.Framework.Common;
using Mitchell1.Catalog.Framework.Interfaces;
using Mitchell1.Online.Catalog.Host.API;
using Mitchell1.Online.Catalog.Host.TransferObjects;

namespace Mitchell1.Online.Catalog.Host
{
	public class OnlineCatalog : IOnlineCatalog
	{
        private readonly OnlineCatalogInformation onlineCatalogInformation;
        private readonly ShowCatalogWaitForm showCatalogWaitForm;
        private readonly IVehicle vehicle;
        private readonly IVendor vendor;
        private readonly IHostData hostData;
        private readonly NewCatalogHostingForm newCatalogHostingForm;
        private readonly Logger logger;

		public OnlineCatalog(OnlineCatalogInformation onlineCatalogInformation, IVendor vendor, IVehicle vehicle, IHostData hostData, NewCatalogHostingForm newCatalogHostingForm = null)
        {
			if (onlineCatalogInformation == null)
                throw new ArgumentNullException(nameof(onlineCatalogInformation));

            logger = new Logger(onlineCatalogInformation.DisplayName);

			this.onlineCatalogInformation = onlineCatalogInformation;
			showCatalogWaitForm = new ShowCatalogWaitForm(onlineCatalogInformation);
            this.vendor = vendor;
            this.vehicle = vehicle;
            this.hostData = hostData;
            this.newCatalogHostingForm = newCatalogHostingForm ?? CatalogHostingForm.New;
        }

		public bool ShowsDeliverWillCall => onlineCatalogInformation.ShowsDeliverWillCall;

        public bool AllowsBlankManufacturerCode => onlineCatalogInformation.AllowsBlankManufacturerCode;

	    public bool AllowsNotFoundPartsToBeOrdered => onlineCatalogInformation.AllowsNotFoundPartsToBeOrdered;

	    public bool SupportsOrderTracking => !string.IsNullOrWhiteSpace(onlineCatalogInformation[CatalogApiPart.OrderTracking]);

        public bool RequiresPriceCheck => onlineCatalogInformation.RequiresPriceCheck;

        public bool SupportsAlternateLocations => onlineCatalogInformation.SupportsAlternateLocations;

        public bool SupportsAlternateParts => onlineCatalogInformation.SupportsAlternateParts;

        public bool SupportsLocation => onlineCatalogInformation.SupportsLocation;

        public bool SupportsOrderMessage => onlineCatalogInformation.SupportsOrderMessage;

        public bool SupportsPriceCheck => onlineCatalogInformation.SupportsPriceCheck;

        public bool SupportsMultiplePurchaseOrders => onlineCatalogInformation.SupportsMultiplePurchaseOrders;

		public bool GoShopping(out ShoppingCart cart)
		{
			var catalogController = OnlineCatalogCommunicationFactory.GetEmbeddedCatalogTransferController(onlineCatalogInformation, vendor, hostData, vehicle);
			using (var hostingForm = newCatalogHostingForm(onlineCatalogInformation, catalogController))
			{
				hostingForm.ClientSize = new System.Drawing.Size(875, 577);
				hostingForm.WindowState = FormWindowState.Maximized;
				if (hostingForm.ShowWebPage())
				{
					cart = catalogController.Cart;
					return cart != null && cart.Count != 0;
				}

				cart = new ShoppingCart();
				return false;
			}
		}

        public bool PriceCheck(IExtendedPriceCheck priceCheck)
        {
			using (var controller = OnlineCatalogCommunicationFactory.GetRestApiController(onlineCatalogInformation, logger, newCatalogHostingForm))
            {
				return controller.PriceCheck(hostData, vendor, priceCheck, vehicle);
			}
        }

		public OrderPartsResponse OrderParts(IExtendedOrder order)
		{
			using (var controller = OnlineCatalogCommunicationFactory.GetRestApiController(onlineCatalogInformation, logger, newCatalogHostingForm))
			{
                return controller.OrderParts(hostData, vendor, order, vehicle);
			}
		}

		public async Task<TrackingResponse> RequestOrderTrackingAsync(string orderTrackingId, CancellationToken cancellationToken)
		{
			using (var controller = OnlineCatalogCommunicationFactory.GetRestApiController(onlineCatalogInformation, logger, newCatalogHostingForm))
			{
				var tracking =  await controller.GetOrderTracking(hostData, vendor, orderTrackingId, cancellationToken);
				if (tracking.ExternalTrackingUrl != null)
				{
					var scheme = tracking.ExternalTrackingUrl.Scheme;
					// We only allow a website to be linked to
					if (scheme != "http" && scheme != "https")
						throw new CatalogConfigurationException("Tracking URL is not a supported scheme");
				}

				return tracking;
			}
		}

		public TrackingResponse RequestOrderTracking(string orderTrackingId)
		{
			if (!SupportsOrderTracking)
				throw new CatalogConfigurationException("Catalog does not support tracking");

			return showCatalogWaitForm.MakeWebCallWithErrorHandling(async token => await RequestOrderTrackingAsync(orderTrackingId, token), "Order Tracking");
		}

		public void ShowDetailExceptionMessageBox(ApiCallException exception, string title)
		{
			using (var errorForm = new CatalogErrorForm(onlineCatalogInformation, exception))
			{
				if (!string.IsNullOrWhiteSpace(title))
					errorForm.Text = title;

				errorForm.ShowDialog();
			}
		}
	}

	internal class ShowCatalogWaitForm
	{
		private readonly OnlineCatalogInformation onlineCatalogInformation;

		public ShowCatalogWaitForm(OnlineCatalogInformation onlineCatalogInformation) => this.onlineCatalogInformation = onlineCatalogInformation;

		public T MakeWebCallWithErrorHandling<T>(Func<CancellationToken, Task<T>> func, string taskName)
		{
			using (var form = new CatalogWaitForm())
			{
				form.DetailMessage = taskName;
				var result = form.GetResponse(func);

				switch (form.Error)
				{
					case null:
						return result;
					case HttpRequestException ex:
						var innerException = ex.InnerException as WebException;
						var message = innerException?.Message ?? ex.GetBaseException().Message;
						var status = innerException?.Status ?? WebExceptionStatus.UnknownError;
						message = $"Please check your Internet connectivity - unable to reach {taskName} for '{onlineCatalogInformation.DisplayName}'. If this error persists, please contact technical support.\r\n\r\nOperating System Reports: ({status}) {message}";

						var apiException = new ApiCallException(message, ApiCallException.ContentType.Text, HttpStatusCode.InternalServerError, status.ToString());
						throw HandleCatalogApiErrorDisplay(apiException, taskName);
					case ApiCallException ex:
						throw HandleCatalogApiErrorDisplay(ex, taskName);
					default:
						throw form.Error;
				}
			}
		}

		private OperationCanceledException HandleCatalogApiErrorDisplay(ApiCallException ex, string taskName)
		{
			using (var errorForm = new CatalogErrorForm(onlineCatalogInformation, ex))
			{
				errorForm.ShowDialog();
			}

			// We want to suppress error UI on Manager - however, still want call to abort - updated manager will suppress this error - we have already handled the error
			var message = $"Catalog '{onlineCatalogInformation.DisplayName}': Operation '{taskName}' failed with: {ex.StatusCode}:{ex.Reason ?? "Unknown Error"}. {ex.Message}";
			Trace.WriteLine(message);
			return new OperationCanceledException(message, ex);
		}
	}
}
