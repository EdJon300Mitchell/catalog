using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mitchell1.Online.Catalog.Host.Test
{
    [TestClass]
    public class CatalogInfoTests
    {
        private OnlineCatalogInformation onlineCatalogInformation;

        [TestInitialize]
        public void Setup()
        {
            onlineCatalogInformation = new OnlineCatalogInformation();

            onlineCatalogInformation.DisplayName = "Test Catalog";
            onlineCatalogInformation.Description = "My Nice Catalog";
            onlineCatalogInformation.ApiVersionLevel = 1;
            onlineCatalogInformation.ApiBaseUrl = "http://localhost/catalog/";
            onlineCatalogInformation[CatalogApiPart.Icon] = "icon.png";
        }

        [TestMethod]
        public async Task CatalogSetupTest()
        {
            CatalogInfo info = new CatalogInfo();
            await info.LoadOnlineCatalogInformation(onlineCatalogInformation, CancellationToken.None);

            Assert.IsNotNull(info.DisplayName);
            Assert.IsNotNull(info.Description);
            Assert.IsNotNull(info.ImageDown);
            Assert.IsNotNull(info.ImageUp);

            Assert.AreEqual(info.DisplayName, "Test Catalog");
            Assert.AreEqual(info.Description, "My Nice Catalog");

            //info.LoadOnlineCatalogInformation()
        }

        [TestMethod]
        public void FindCachedImageTest()
        {
            CatalogInfo info = new CatalogInfo();

            string programData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string path = Path.Combine(programData, "M1-SK", "CatalogIcons");
            if (!Directory.Exists(path))
            {
                DirectorySecurity securityRules = new DirectorySecurity();
                securityRules.AddAccessRule(new FileSystemAccessRule("Users", FileSystemRights.Modify,
                    AccessControlType.Allow));
                try
                {
                    Directory.CreateDirectory(path, securityRules);
                }
                catch (Exception ex)
                {
                    Assert.Fail("Exception creating CategoryIcons directory:\r\n" + ex.Message);
                }
            }

            // Six day old icon file
            var sixDaysAgo = DateTime.Now.Subtract(TimeSpan.FromDays(6));
            var fileName = onlineCatalogInformation.DisplayName + " " + sixDaysAgo.ToString("yyyy-MM-dd") + ".png";

            var image6 = new Bitmap(1, 1);
            image6.Save(Path.Combine(path, fileName), ImageFormat.Png);
            image6.Dispose();

            Image image = info.FindCachedImage(onlineCatalogInformation, false);
            Assert.IsNotNull(image, "Cached image (6 days old) should be found");
            image.Dispose();

            // Seven day old icon file
            File.Delete(Path.Combine(path, fileName));
            var sevenDaysAgo = DateTime.Now.Subtract(TimeSpan.FromDays(7));
            fileName = onlineCatalogInformation.DisplayName + " " + sevenDaysAgo.ToString("yyyy-MM-dd") + ".png";

            var image7 = new Bitmap(1, 1);
            image7.Save(Path.Combine(path, fileName), ImageFormat.Png);
            image7.Dispose();

            image = info.FindCachedImage(onlineCatalogInformation, false);
            Assert.IsNull(image, "Cached image (7 days old) should not be found");

            // load stale image - ignore the date
            image = info.FindCachedImage(onlineCatalogInformation, true);
            Assert.IsNotNull(image, "Cached image (7 days old) should be found (ignoring stale cache)");
            image.Dispose();

            // No matching file
            File.Delete(Path.Combine(path, fileName));
            image = info.FindCachedImage(onlineCatalogInformation, false);
            Assert.IsNull(image, "Cached image should not be found");
        }

        [TestMethod]
        public void CacheImageTest()
        {
            CatalogInfo info = new CatalogInfo();

            string programData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string path = Path.Combine(programData, "M1-SK", "CatalogIcons");

            var iconFiles = Directory.EnumerateFiles(path, onlineCatalogInformation.DisplayName + "*.png");
            Assert.AreEqual(0, iconFiles.Count());

            var image = new Bitmap(1, 1);
            info.CacheImage(onlineCatalogInformation, image);
            image.Dispose();

            var fileName = onlineCatalogInformation.DisplayName + " " + DateTime.Now.ToString("yyyy-MM-dd") + ".png";
            iconFiles = Directory.EnumerateFiles(path, onlineCatalogInformation.DisplayName + "*");
            Assert.AreEqual(1, iconFiles.Count());
            Assert.AreEqual(Path.Combine(path, fileName), iconFiles.First());

            File.Delete(iconFiles.First());
        }

        [TestMethod]
        public void ClearOldImageTest()
        {
            CatalogInfo info = new CatalogInfo();

            string programData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string path = Path.Combine(programData, "M1-SK", "CatalogIcons");

            var iconFiles = Directory.EnumerateFiles(path, onlineCatalogInformation.DisplayName + "*.png");
            Assert.AreEqual(0, iconFiles.Count());

            var sixDaysAgo = DateTime.Now.Subtract(TimeSpan.FromDays(6));
            var fileName = onlineCatalogInformation.DisplayName + " " + sixDaysAgo.ToString("yyyy-MM-dd") + ".png";

            var image = new Bitmap(1, 1);
            image.Save(Path.Combine(path, fileName), ImageFormat.Png);
            image.Dispose();

            iconFiles = Directory.EnumerateFiles(path, onlineCatalogInformation.DisplayName + "*.png");
            Assert.AreEqual(1, iconFiles.Count());

            info.ClearOldImage(onlineCatalogInformation);

            iconFiles = Directory.EnumerateFiles(path, onlineCatalogInformation.DisplayName + "*.png");
            Assert.AreEqual(0, iconFiles.Count());
        }
    }
}
