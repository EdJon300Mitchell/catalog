using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Mitchell1.Catalog.Driver.Browser;

[assembly: InternalsVisibleTo("Mitchell1.Online.Catalog.Host.Test, PublicKey=" + 
    "0024000004800000940000000602000000240000525341310004000001000100d57e9d0c066f6c" +
	"965924b3af8a465f5938d820afc3a169ae186f48d981881986aa44c954131508bb5dd8abacee7c" +
	"a1f5fdf69ec4e83b852dca36122437a922bce8724e61cfe6962a8f3cc41ac45874c86b6b6de405" +
	"c175f6b953b6f4eb790f2c331b09ee548349857bc2f395235c68915d2a95e89bb8f4deba2402cd" +
	"99132ad2")]

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