using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Mitchell1.Catalog.Driver.Browser;


namespace Mitchell1.Catalog.Driver
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string [] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var browserPrefix = "--browser=";
            var browserUrlParam = args.FirstOrDefault(a => a.StartsWith(browserPrefix));
            if (browserUrlParam != null)
            {
                Application.Run(new BrowserHtmlTests(browserUrlParam.Substring(browserPrefix.Length)));
                return;
            }

            Application.Run(new CatalogDriver());
        }
    }
}