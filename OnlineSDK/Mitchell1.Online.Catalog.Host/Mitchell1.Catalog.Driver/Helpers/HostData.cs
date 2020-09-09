using System.Reflection;
using Mitchell1.Catalog.Framework.Interfaces;
using System.ComponentModel;

namespace Mitchell1.Catalog.Driver.Helpers
{
	public class HostData : IHostData
	{
		private string applicationTitle = string.Empty;
		private string applicationVersion = string.Empty;
		private decimal laborRate = decimal.Zero;

		public HostData(string applicationTitle, decimal laborRate)
		{
			this.applicationTitle = applicationTitle;
			this.laborRate = laborRate;
			applicationVersion = new AssemblyName(Assembly.GetExecutingAssembly().FullName).Version.ToString(3);
		}

		#region IHostData Members

		[DescriptionAttribute("The title of the host application (like \"Mitchell1 SE\")")]
		public string ApplicationTitle
		{
			get { return applicationTitle; }
			set { applicationTitle = value; }
		}

		[DescriptionAttribute("The version of the host application (like \"2.0.0\")")]
		public string ApplicationVersion
		{
			get { return applicationVersion; }
			set { applicationVersion = value; }
		}

		[DescriptionAttribute("The hourly rate for labor services charged to a customer")]
		public decimal LaborRate
		{
			get { return laborRate; }
			set { laborRate = value; }
		}

		#endregion
	}
}
