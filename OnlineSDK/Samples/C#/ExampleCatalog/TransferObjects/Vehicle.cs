namespace Mitchell1.Online.Catalog.Host.TransferObjects
{
	// See Mitchell1 Online Catalog SDK Doc
	public class Vehicle
	{
		public string Vin { get; set; }
		public int Year { get; set; }
		public string Make { get; set; }
		public string Model { get; set; }
		public string SubModel { get; set; }
		public string Body { get; set; }
		public string Engine { get; set; }
		public string Transmission { get; set; }
		public string DriveType { get; set; }
		public string Brake { get; set; }
		public string Gvw { get; set; }
		public int AcesId { get; set; }
		public int AcesBaseId { get; set; }
		public int AcesEngineId { get; set; }
		public int AcesEngineBaseId { get; set; }
		public int AcesEngineConfigId { get; set; }
		public int AcesSubmodelId { get; set; }
	}
}