using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Mitchell1.Catalog.Framework.Interfaces;

[assembly: InternalsVisibleTo("Mitchell1.Online.Catalog.Host.Test, PublicKey=" + 
    "0024000004800000940000000602000000240000525341310004000001000100d57e9d0c066f6c" +
	"965924b3af8a465f5938d820afc3a169ae186f48d981881986aa44c954131508bb5dd8abacee7c" +
	"a1f5fdf69ec4e83b852dca36122437a922bce8724e61cfe6962a8f3cc41ac45874c86b6b6de405" +
	"c175f6b953b6f4eb790f2c331b09ee548349857bc2f395235c68915d2a95e89bb8f4deba2402cd" +
	"99132ad2")]

namespace Mitchell1.Online.Catalog.Host
{
	public interface IOnlineCatalogInfo : ICatalogInfo
	{
		Task LoadOnlineCatalogInformation(OnlineCatalogInformation catalog, CancellationToken cancellationToken);

		IOnlineCatalog GetOnlineCatalog(IVendor vendor, IVehicle vehicle, IHostData hostData);

		OnlineCatalogInformation OnlineCatalogInformation { get; }
	}

	public class CatalogInfo : IOnlineCatalogInfo
	{
		private readonly NewCatalogHostingForm newCatalogHostingForm;

		public CatalogInfo(NewCatalogHostingForm newCatalogHostingForm = null)
		{
			this.newCatalogHostingForm = newCatalogHostingForm ?? CatalogHostingForm.New;
			OnlineCatalogInformation = new OnlineCatalogInformation();
		}

        public async Task LoadOnlineCatalogInformation(OnlineCatalogInformation catalog, CancellationToken cancellationToken)
        {
	        OnlineCatalogInformation = catalog ?? throw new ArgumentNullException(nameof(catalog));
            ImageDown = null;
            ImageUp = null;
       
            try
            {
                var image = FindCachedImage(catalog, false);
                if (image == null && !string.IsNullOrEmpty(OnlineCatalogInformation[CatalogApiPart.Icon]))
                {
                    image = await LoadBitmapUrl(new Size(58, 28), cancellationToken);
                }
                if (image == null)
                { 
                    image = FindCachedImage(catalog, true);
                }
                if (image != null)
                { 
                    ImageDown = new Bitmap(image);
                    ImageUp = ImageDown;
                }
                else
                {
                    Trace.WriteLine("Could not load icon for " + catalog.DisplayName + ": Not available online or in cache");
                }
            }
            catch (Exception e)
            {
                // try loading the stale cache image
                var image = FindCachedImage(catalog, true);
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
                        DirectorySecurity securityRules = new DirectorySecurity();
                        securityRules.AddAccessRule(new FileSystemAccessRule("Users", FileSystemRights.Modify, AccessControlType.Allow));
                        try
                        {
                            Directory.CreateDirectory(iconDirectory, securityRules);
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

        public async Task<Image> LoadBitmapUrl(Size requestedSize, CancellationToken cancellationToken)
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

		public OnlineCatalogInformation OnlineCatalogInformation { get; private set; }

		public string DisplayName => OnlineCatalogInformation.DisplayName;
        public string Description => OnlineCatalogInformation.Description;
        public Bitmap ImageDown { get; set; }
        public Bitmap ImageUp { get; set; }

	    ICatalog ICatalogInfo.GetCatalog(IVendor vendor, IVehicle vehicle, IHostData hostData)
		    => throw new NotImplementedException($"This method is deprecated - use {nameof(IOnlineCatalogInfo)}.{nameof(GetOnlineCatalog)}");

		public IOnlineCatalog GetOnlineCatalog(IVendor vendor, IVehicle vehicle, IHostData hostData)
	    {
		    return new OnlineCatalog(OnlineCatalogInformation, vendor, vehicle, hostData);
	    }

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
