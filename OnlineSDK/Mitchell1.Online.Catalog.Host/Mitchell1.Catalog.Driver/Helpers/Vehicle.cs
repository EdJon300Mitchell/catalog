using System.Collections.Generic;
using Mitchell1.Catalog.Framework.Interfaces;
using System.ComponentModel;

namespace Mitchell1.Catalog.Driver.Helpers
{
	internal class Vehicle : IVehicle, IVehicleAcesProvider
    {
        private Dictionary<AcesId, int> acesLookup = new Dictionary<AcesId, int>();

        [DescriptionAttribute("The year for the vehicle")]
		public int Year { get; set; }

		[DescriptionAttribute("The vehicle make (like: \"Chevrolet\")")]
		public string Make { get; set; }

		[DescriptionAttribute("The vehicle model (like: \"Silverado\")")]
		public string Model { get; set; }

		[DescriptionAttribute("The vehicle submodel (like: \"C3500\")")]
		public string SubModel { get; set; }

		[DescriptionAttribute("The vehicle identification number (like: \"1C4GP64LXYB608478\")")]
		public string Vin { get; set; }

		[DescriptionAttribute("The vehicle engine type (like: \"3.3L,V6 (201CI) VIN(R)\")")]
		public string Engine { get; set; }

		[DescriptionAttribute("The vehicle drive front-wheel, rear-wheel or all-wheel (like: \"RWD\")")]
		public string DriveType { get; set; }

		[DescriptionAttribute("The vehicle body type (like: \"4D Wagon\")")]
		public string Body { get; set; }

		[DescriptionAttribute("The vehicle transmission type (like: \"4 speed Automatic AOD\")")]
		public string Transmission { get; set; }

		[DescriptionAttribute("The vehicle brake type (like: \"4-Wheel ABS\")")]
		public string Brake { get; set; }

		[DescriptionAttribute("The Gross Vehicle Weight (like: \"4500\" or \"9000-11000\")")]
		public string Gvw { get; set; }

		[DescriptionAttribute("A unique vehicle identifier as defined by the Automotive Aftermarket Industry Association")]
		public int AaiaId { get; set; }

	    [DescriptionAttribute("A unique vehicle identifier as defined by the Aftermarket Catalog Enhanced Standards")]
	    public int AcesId
	    {
            get { return acesLookup[Framework.Interfaces.AcesId.VehicleID]; }
            set { acesLookup[Framework.Interfaces.AcesId.VehicleID] = value; }
	    }

	    [DescriptionAttribute("A unique base vehicle engine identifier as defined by the Aftermarket Catalog Enhanced Standards")]
	    public int AcesEngineId
	    {
            get { return acesLookup[Framework.Interfaces.AcesId.EngineBaseID]; }
            set { acesLookup[Framework.Interfaces.AcesId.EngineBaseID] = value; }
        }

		[DescriptionAttribute("The vehicle qualifier created by the catalog used to uniquely identify a vehicle.")]
		public string Qualifier { get; set; }

	    [DescriptionAttribute("A unique base vehicle identifier as defined by the Aftermarket Catalog Enhanced Standards")]
	    public int AcesBaseId
	    {
            get { return acesLookup[Framework.Interfaces.AcesId.BaseVehicleID]; }
            set { acesLookup[Framework.Interfaces.AcesId.BaseVehicleID] = value; }
        }

	    [DescriptionAttribute("A unique vehicle engine configuration identifier as defined by the Aftermarket Catalog Enhanced Standards")]
	    public int AcesEngineConfigId
	    {
            get { return acesLookup[Framework.Interfaces.AcesId.EngineConfigID]; }
            set { acesLookup[Framework.Interfaces.AcesId.EngineConfigID] = value; }
        }

	    [DescriptionAttribute("A unique vehicle Sub Model identifier as defined by the Aftermarket Catalog Enhanced Standards")]
	    public int AcesSubModelId
	    {
            get { return acesLookup[Framework.Interfaces.AcesId.SubModelID]; }
            set { acesLookup[Framework.Interfaces.AcesId.SubModelID] = value; }
        }

        public Vehicle()
        {
            Year = 0;
            Make = string.Empty;
            Model = string.Empty;
            SubModel = string.Empty;
            Vin = string.Empty;
            Engine = string.Empty;
            DriveType = string.Empty;
            Body = string.Empty;
            Transmission = string.Empty;
            Brake = string.Empty;
            Gvw = string.Empty;
            AaiaId = 0;
            AcesId = 0;
        	AcesEngineId = 0;
			AcesBaseId = 0;
			Qualifier = string.Empty;
            AcesEngineConfigId = 0;
            AcesSubModelId = 0;
        }
        
        public Vehicle(int year, string make, string model,
            string subModel, string vin, string engine, string driveType,
            string body, string transmission, string brake, string gvw,
            int aaiaId, int acesId, int acesEngineId, int acesBaseId, 
            string qualifier, int acesEngineConfigId, int acesSubModelId)
        {
            Year = year;
            Make = make;
            Model = model;
            SubModel = subModel;
            Vin = vin;
            Engine = engine;
            DriveType = driveType;
            Body = body;
            Transmission = transmission;
            Brake = brake;
            Gvw = gvw;
            AaiaId = aaiaId;
            AcesId = acesId;
			AcesEngineId = acesEngineId;
			AcesBaseId = acesBaseId;
			Qualifier = qualifier;
            AcesEngineConfigId = acesEngineConfigId;
            AcesSubModelId = acesSubModelId;
        }

	    public int GetAcesId(AcesId id)
	    {
	        int foundId;
            if (acesLookup.TryGetValue(id, out foundId))
            {
                return foundId;
            }
	        return 0;
	    }
    }
}
