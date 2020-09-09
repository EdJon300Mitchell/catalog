using System;
using System.Windows.Forms;

namespace Mitchell1.Catalog.Driver.Helpers
{
	internal class ExternalCatalogAdapterErrorHandler
	{
		public virtual void ShowCatalogAuthenticationExceptionMessage()
		{
			ShowCatalogExceptionMessage("Please verify your vendor setup.", "Catalog Authentication Error");
		}

		public virtual void ShowCatalogCommunicationExceptionMessage()
		{
			ShowCatalogExceptionMessage("No Internet connection, or remote server failed to respond.",
										"Catalog Communication Error");
		}

		public virtual void ShowGoShoppingCatalogExceptionMessage(string message)
		{
			ShowCatalogExceptionMessage("An error was detected from the catalog. " + message,
										"Catalog Error");
		}

		public virtual void ShowPriceCheckOrderPartsCatalogExceptionMessage(string message)
		{
			ShowCatalogExceptionMessage("Error performing Price Check/Order Parts.", "Catalog Error");
		}

		public virtual void ShowGeneralCatalogExceptionMessage(string message)
		{
			ShowCatalogExceptionMessage("An error was detected from the catalog. " + message,
										"Catalog Error");
		}

		public virtual void ShowCatalogExceptionMessage(string message, string caption)
		{
			MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}