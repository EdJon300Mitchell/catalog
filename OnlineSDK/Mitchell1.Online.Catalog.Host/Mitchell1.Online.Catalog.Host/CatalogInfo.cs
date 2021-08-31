using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Mitchell1.Catalog.Framework.Interfaces;

namespace Mitchell1.Online.Catalog.Host
{
	public interface IOnlineCatalogInfo : ICatalogInfo
	{
		IOnlineCatalog GetOnlineCatalog(IVendor vendor, IVehicle vehicle, IHostData hostData);

		OnlineCatalogInformation OnlineCatalogInformation { get; }
	}

	public class CatalogInfo : IOnlineCatalogInfo
	{
		private readonly NewCatalogHostingForm newCatalogHostingForm;

		internal CatalogInfo(NewCatalogHostingForm newCatalogHostingForm, OnlineCatalogInformation onlineCatalogInformation)
		{
			this.newCatalogHostingForm = newCatalogHostingForm;
			OnlineCatalogInformation = onlineCatalogInformation;
		}

		public static async Task<CatalogInfo> NewCatalogInfoAsync(OnlineCatalogInformation onlineCatalogInformation)
		{
			var catalogInfo = new CatalogInfo(CatalogHostingForm.New, onlineCatalogInformation ?? throw new ArgumentNullException(nameof(onlineCatalogInformation)));
			await catalogInfo.LoadOnlineCatalogInformation(onlineCatalogInformation);
			return catalogInfo;
		}

		private async Task LoadOnlineCatalogInformation(OnlineCatalogInformation onlineCatalogInformation)
        {
            ImageDown = null;
            ImageUp = null;
       
            try
            {
                var image = FindCachedImage(onlineCatalogInformation, false);
                if (image == null && !string.IsNullOrEmpty(OnlineCatalogInformation[CatalogApiPart.Icon]))
                {
                    image = await LoadBitmapUrl(new Size(58, 28));
                }
                if (image == null)
                { 
                    image = FindCachedImage(onlineCatalogInformation, true);
                }
                if (image != null)
                { 
                    ImageDown = new Bitmap(image);
                    ImageUp = ImageDown;
                }
                else
                {
                    Trace.WriteLine("Could not load icon for " + onlineCatalogInformation.DisplayName + ": Not available online or in cache");
                }
            }
            catch (Exception e)
            {
                // try loading the stale cache image
                var image = FindCachedImage(onlineCatalogInformation, true);
                if (image != null)
                {
                    ImageDown = new Bitmap(image);
                    ImageUp = ImageDown;
                }
                else
                {
                    Trace.WriteLine("Could not load icon: " + e.Message);
                }
            }
        }

        private string iconDirectory;
        private string IconDirectory
        {
            get
            {
                if (iconDirectory == null)
                { 
                    string programData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                    iconDirectory = Path.Combine(programData, "M1-SK", "CatalogIcons");
                    if (!Directory.Exists(iconDirectory))
                    {
                        try
                        {
                            Directory.CreateDirectory(iconDirectory);
                        }
                        catch (Exception ex)
                        {
                            Trace.WriteLine($"Failed to create CategoryIcons directory: {iconDirectory}\r\n" + ex.Message);
                            iconDirectory = null;
                        }
                    }
                }

                return iconDirectory;
            }
        }

        public void ClearOldImage(OnlineCatalogInformation catalog)
        {
            var filePattern = catalog.DisplayName + @"*.png";
            IEnumerable<string> files = Directory.EnumerateFiles(IconDirectory, filePattern);
            Regex datePattern = new Regex(Regex.Escape(catalog.DisplayName) + @" (\d{4})-(\d{2})-(\d{2})\.\w+$");
            foreach (var name in files)
            {
                Match match = datePattern.Match(name);
                if (match.Success)
                {
                    try
                    {
                        File.Delete(name);
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine($"Unable to clear old catalog icon file: {name}\r\n{e.Message}");
                    }
                }
            }
        }

        public void CacheImage(OnlineCatalogInformation catalog, Image icon)
        {
            if (IconDirectory != null)
            {
                ClearOldImage(catalog);

                string fileName = catalog.DisplayName + " " + DateTime.Now.ToString("yyyy-MM-dd") + ".png";
                string fullName = Path.Combine(IconDirectory, fileName);
                try
                {
                    icon.Save(fullName, ImageFormat.Png);
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"Failed to cache (save) icon file: {0}\r\n{e.Message}");
                }
            }
        }

        public Image FindCachedImage(OnlineCatalogInformation catalog, bool ignoreDate)
        {
            var filesFound = new List<string>();

            if (IconDirectory != null)
            {
                var filePattern = catalog.DisplayName + @"*.png";
                IEnumerable<string> files = Directory.EnumerateFiles(IconDirectory, filePattern);
                Regex datePattern = new Regex(Regex.Escape(catalog.DisplayName) + @" (\d{4})-(\d{2})-(\d{2})\.\w+$");
                foreach (var name in files)
                {
                    Match match = datePattern.Match(name);
                    if (match.Success)
                    {
                        if (ignoreDate)
                        {
                            filesFound.Add(name);
                        }
                        else
                        {
	                        if (int.TryParse(match.Groups[1].Value, out var year) &&
                                int.TryParse(match.Groups[2].Value, out var month) &&
                                int.TryParse(match.Groups[3].Value, out var day))
                            {
                                var cacheDate = new DateTime(year, month, day);
                                if ((DateTime.Now - cacheDate).TotalDays < 7)
                                {
                                    filesFound.Add(name);
                                }
                            }
                        }
                    }
                }
            }

            try
            {
                return filesFound.Count > 0
                    ? Image.FromFile(filesFound[0])
                    : null;
            }
            catch (OutOfMemoryException)
            {
                try
                {
                    File.Delete(filesFound[0]);
                }
                catch (Exception e2)
                {
                    Trace.WriteLine($"Unable to delete invalid catalog icon file: {filesFound[0]}\r\n{e2.Message}");
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine($"Unable to load cached catalog icon file: {filesFound[0]}\r\n{e.Message}");
            }

            return null;
        }

        private async Task<Image> LoadBitmapUrl(Size requestedSize)
        {
            if (OnlineCatalogInformation == null || string.IsNullOrEmpty(OnlineCatalogInformation.ApiBaseUrl))
            {
                throw new CatalogConfigurationException("Invalid onlineCatalogInformation");
            }

            string imageUrl = $"{OnlineCatalogInformation.GetAbsoluteUrl(CatalogApiPart.Icon)}?width={requestedSize.Width}&height={requestedSize.Height}";
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(10);
                using (var stream = await client.GetStreamAsync(imageUrl))
                {
                    var image = Image.FromStream(stream);
                    CacheImage(OnlineCatalogInformation, image);
                    return image;
                }
            }
        }

		public OnlineCatalogInformation OnlineCatalogInformation { get; }

		public string DisplayName => OnlineCatalogInformation.DisplayName;
        public string Description => OnlineCatalogInformation.Description;
        public Bitmap ImageDown { get; private set; }
        public Bitmap ImageUp { get; private set; }

	    ICatalog ICatalogInfo.GetCatalog(IVendor vendor, IVehicle vehicle, IHostData hostData)
		    => throw new NotImplementedException($"This method is deprecated - use {nameof(IOnlineCatalogInfo)}.{nameof(GetOnlineCatalog)}");

		public IOnlineCatalog GetOnlineCatalog(IVendor vendor, IVehicle vehicle, IHostData hostData) => new OnlineCatalog(OnlineCatalogInformation, vendor, vehicle, hostData);

		public bool VendorSetup(IVendor vendor, IHostData hostData)
		{
			var catalogController = OnlineCatalogCommunicationFactory.GetCatalogVendorSetupController(OnlineCatalogInformation, vendor, hostData);
            using (var hostingForm = newCatalogHostingForm(OnlineCatalogInformation, catalogController, @"Vendor Setup"))
            {
	            hostingForm.ClientSize = new Size(537, 309);
	            hostingForm.MaximizeBox = false;
                return hostingForm.ShowWebPage();
            }
        }
    }
}
