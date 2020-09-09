using System.ComponentModel;
using Mitchell1.Catalog.Framework.Interfaces;

namespace Mitchell1.Catalog.Driver.Helpers
{
    public class Vendor : IVendor
	{
		#region Constructors

		public Vendor()
		{
			Name = string.Empty;
			Code = string.Empty;
			Qualifier = string.Empty;
			Catalog = string.Empty;
		}

		#endregion

		#region Properties

    	[Description("The name of this vendor (like \"NAPA Auto Parts\")")]
    	public string Name { get; set; }

    	[DescriptionAttribute("A unique code that identifies the vendor (like \"NAPA\")")]
    	public string Code { get; set; }

    	[DescriptionAttribute("A qualifier which uniquely identifies the vendor")]
    	public string Qualifier { get; set; }

		[DescriptionAttribute("Catalog from which the Qualifier was generated")]
		public string Catalog { get; set; }

    	#endregion
	}
}
