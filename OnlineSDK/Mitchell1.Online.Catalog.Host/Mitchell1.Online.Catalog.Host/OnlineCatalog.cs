using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mitchell1.Catalog.Framework.Common;
using Mitchell1.Catalog.Framework.Interfaces;
using Mitchell1.Online.Catalog.Host.API;
using Mitchell1.Online.Catalog.Host.Orders;
using Mitchell1.Online.Catalog.Host.TransferObjects;

namespace Mitchell1.Online.Catalog.Host
{
	public class OnlineCatalog : IOnlineCatalog
	{
        private readonly OnlineCatalogInformation onlineCatalogInformation;
        private readonly IVehicle vehicle;
        private readonly IVendor vendor;
        private readonly IHostData hostData;
        private readonly Logger logger;

		public OnlineCatalog(OnlineCatalogInformation onlineCatalogInformation, IVendor vendor, IVehicle vehicle, IHostData hostData)
        {
			if (onlineCatalogInformation == null)
                throw new ArgumentNullException(nameof(onlineCatalogInformation));

            logger = new Logger(onlineCatalogInformation.DisplayName);

			this.onlineCatalogInformation = onlineCatalogInformation;
            this.vendor = vendor;
            this.vehicle = vehicle;
            this.hostData = hostData;
        }

        public bool AllowsBlankManufacturerCode => onlineCatalogInformation.AllowsBlankManufacturerCode;

	    public bool AllowsNotFoundPartsToBeOrdered => onlineCatalogInformation.AllowsNotFoundPartsToBeOrdered;

	    public IDeliveryMethod DeliveryMethod => onlineCatalogInformation.DeliveryMethod;

	    public bool SupportsOrderTracking => !string.IsNullOrWhiteSpace(onlineCatalogInformation[CatalogApiPart.OrderTracking]);

		/// <summary>
		/// We need to control UI - to show any error messages on UI thread - cannot be delegated to background worker thread if Manager controls UI
		/// </summary>
		public bool HostShowsProgressOnOrder => false;
        
		/// <summary>
        /// We need to control UI - to show any error messages on UI thread - cannot be delegated to background worker thread if Manager controls UI
        /// </summary>
		public bool HostShowsProgressOnPriceCheck => false;

        public bool RequiresPriceCheck => onlineCatalogInformation.RequiresPriceCheck;

        public bool SupportsAlternateLocations => onlineCatalogInformation.SupportsAlternateLocations;

        public bool SupportsAlternateParts => onlineCatalogInformation.SupportsAlternateParts;

        public bool SupportsLocation => onlineCatalogInformation.SupportsLocation;

        public bool SupportsOrderMessage => onlineCatalogInformation.SupportsOrderMessage;

        public bool SupportsPriceCheck => onlineCatalogInformation.SupportsPriceCheck;

		public bool GoShopping(ICart cart) => throw new NotImplementedException($"This method is deprecated - use {nameof(IOnlineCatalog)}.{nameof(IOnlineCatalog.GoShopping)}");
		void ICatalog.OrderParts(IOrder order) => throw new NotImplementedException($"This method is deprecated - use {nameof(IOnlineCatalog)}.{nameof(IOnlineCatalog.OrderParts)}");

		public bool GoShopping(out ShoppingCart cart)
		{
			cart = new ShoppingCart();

			using (var hostingForm = new GoShoppingForm())
            {
                hostingForm.LoadOnlineCatalog(onlineCatalogInformation, cart, vendor, hostData, vehicle);
                if (hostingForm.ShowDialog() == DialogResult.OK)
                    return !cart.IsEmpty;

                if (hostingForm.HttpResponseCode == 403)
                    throw new CatalogAuthenticationException();

                return false;
            }
        }

        public void PriceCheck(IPriceCheck priceCheck)
        {
            MakeWebCallWithErrorHandling(async delegate (CancellationToken token)
			{ 
				using (var controller = OnlineCatalogCommunicationFactory.GetRestApiController(onlineCatalogInformation))
                {
                    await controller.PriceCheck(hostData, vendor, priceCheck, vehicle, logger, token);
                }
            }, "Price Check");
		}

		public OrderResponse OrderParts(OrderRequest order)
		{
			OrderResponse orderResponse = null;
			MakeWebCallWithErrorHandling(async delegate (CancellationToken token)
			{
				using (var controller = OnlineCatalogCommunicationFactory.GetRestApiController(onlineCatalogInformation))
				{
					orderResponse = await controller.OrderParts(hostData, vendor, order, vehicle, logger, token);
				}

			}, "Order");

			return orderResponse;
		}

		public async Task<TrackingRequestResponse> RequestOrderTrackingAsync(string orderTrackingId, CancellationToken cancellationToken)
		{
			using (var controller = OnlineCatalogCommunicationFactory.GetRestApiController(onlineCatalogInformation))
			{
				var tracking =  await controller.GetOrderTracking(hostData, vendor, orderTrackingId, logger, cancellationToken);
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

		public TrackingRequestResponse RequestOrderTracking(string orderTrackingId)
		{
			if (!SupportsOrderTracking)
				throw new CatalogConfigurationException("Catalog does not support tracking");

			TrackingRequestResponse tracking = null;
			MakeWebCallWithErrorHandling(async delegate (CancellationToken token)
			{
				tracking = await RequestOrderTrackingAsync(orderTrackingId, token);
			}, "Order Tracking");

			return tracking;
		}

		private void MakeWebCallWithErrorHandling(Func<CancellationToken, Task> action, string taskName)
        {
	        using (var form = new CatalogWaitForm())
	        {
		        form.DetailMessage = taskName;
		        form.Action = action;
		        form.ShowDialog();

		        switch (form.Error)
		        {
			        case null:
				        return;
					case HttpRequestException ex:
						var innerException = ex.InnerException as WebException;
						var message = innerException?.Message ?? ex.GetBaseException().Message;
						var status = innerException?.Status ?? WebExceptionStatus.UnknownError;
						message = $"Please check your Internet connectivity - unable to reach {taskName} for '{onlineCatalogInformation.DisplayName}'. If this error persists, please contact technical support.\r\n\r\nOperating System Reports: ({status}) {message}";

						var apiException = new ApiCallException(message, ApiCallException.ContentType.Text, HttpStatusCode.InternalServerError, status.ToString());
						HandleCatalogApiErrorDisplay(apiException, taskName);
						break;
					case ApiCallException ex:
					{
						HandleCatalogApiErrorDisplay(ex, taskName);
						break;
						}
			        default:
				        throw form.Error;
		        }
	        }
        }

        private void HandleCatalogApiErrorDisplay(ApiCallException ex, string taskName)
        {
	        using (var errorForm = new CatalogErrorForm(onlineCatalogInformation, ex))
	        {
		        errorForm.ShowDialog();
	        }

	        // We want to suppress error UI on Manager - however, still want call to abort - updated manager will suppress this error - we have already handled the error
	        var message = $"Catalog '{onlineCatalogInformation.DisplayName}': Operation '{taskName}' failed with: {ex.StatusCode}:{ex.Reason ?? "Unknown Error"}. {ex.Message}";
	        Trace.WriteLine(message);
	        throw new OperationCanceledException(message, ex);
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
}
