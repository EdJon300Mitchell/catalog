using System.Reflection;
using Mitchell1.Catalog.Framework.Interfaces;
using System.ComponentModel;

namespace Mitchell1.Catalog.Driver.Helpers
{
	public class HostData : IHostData
	{
		public HostData(string applicationTitle, string applicationVersion, decimal laborRate)
		{
			ApplicationTitle = applicationTitle;
			ApplicationVersion = applicationVersion;
			LaborRate = laborRate;
		}

		[DescriptionAttribute("The title of the host application (like \"Mitchell1 SE\")")]
		public string ApplicationTitle { get; set; }

		[DescriptionAttribute("The version of the host application (like \"8.3.0.0\")")]
		public string ApplicationVersion { get; set; }

		[DescriptionAttribute("The hourly rate for labor services charged to a customer")]
		public decimal LaborRate { get; set; }
	}
}
