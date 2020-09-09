
/**
 * JavaScript entry Point into Online Catalog integration with ManagerSE. Use this class in your "Vendor Setup"
 * and "Go Shopping" Web pages.
*/
class Catalog {	
	/**
	* Create Catalog integration instance. You must only create one of these. Recomended to create this early in page life cycle in case you need
	* to use JavaScript debugger.
	* @param {string} apiKey - Pass in Vendor Identifier (will be assigned by Mitchell1)
	* @param {boolean} isSetupMode - True: if this is being used in Vendor Setup. False: if used in Go Shopping sessions. Changes the behavior of saveConfiguration
	*/
	constructor(apiKey, isSetupMode) {
		this.apiKey = apiKey;
		this.isSetupMode = isSetupMode;
		
		/**
		* Pass one of these constants for methods taking a PartsCategory
		* @property {string} UNSPECIFIED - General Part
		* @property {string} TIRE - Tires will get part category user has assigned for Tires & Wheels, and marked IsTire=true in Manager
		* @property {string} WHEEL - Wheels will get part category user has assigned for Tires & Wheels, but will have IsTire=false in Manager
		* @see Catalog#createPartItem2
		* @see Catalog#addOrderPart2
		*/
		this.PartCategory = Object.freeze({
			UNSPECIFIED: 'Unspecified',
			TIRE: 'Tire',
			WHEEL: 'Wheel'
		});

		// Check if extension is configured for Manager browser. Otherwise, assume web integration
		if(typeof nativeImplementation != "function") {
			this.communication = function(action) {
				var origin = this.getUrlParameterByName('Origin');			
				console.log('Sending message "' + action + '" to parent frame... origin: ' + origin);
				
				var args = Array.prototype.slice.call(arguments);
				window.top.postMessage(JSON.stringify(args), origin);
			};
		}
		else {
			this.communication = nativeImplementation;
		}
	}
	
	/**
	* Get any previously saved Vendor Qualifier (reads from Query string). Vendor Qualifier is saved from Vendor Setup, this is where stored data identifying shop customer is retrieved
	* @return {string} The Qualifier value.
	*/
	get qualifier() {
		return this.getUrlParameterByName('Qualifier');
	}
	
	/** Cancels the current operation. Will close the broser window  - nothing will be saved or transferred */
	cancelRequest() {
		this.communication('cancel', this.apiKey);
	}
	
	/** Only supported in isSetupMode=true - for Vendor setup, used to save the vendor qualifier information and close the browser window */
	saveConfiguration(configObject) {
		if(this.isSetupMode) {
			this.communication('save', this.apiKey, JSON.stringify(configObject));
		}
		else {
		    // Not Supported
		}
	}
	
	/**
	* Only supported in isSetupMode=false - for Go Shopping session, used to transfer items in the cart and close the browser window 
	* @param {Array<IPartItem3, INoteItem, INoteItem, IOrder>} cart - Array of items to transfer to Manager. Note, IOrder contains its own list of part items. Do not add those ordered parts to the cart directly
	*/
	transferParts(cart) {
		if(!this.isSetupMode) {
			this.communication('transfer', this.apiKey, cart);
		}
		else {
		    // Not Supported
		}
	}

	/**
	* Returns the Vehicle (if any) that was selected on Shop Management System's repair order (only populated within Go Shopping session)
	* @see IVehicle
	* @return {IVehicle}
	*/
    get vehicle() {
        return {
            Vin: this.getUrlParameterByName('Vin'),
            Year: this.getUrlParameterByName('Year'),
            Make: this.getUrlParameterByName('Make'),
            Model: this.getUrlParameterByName('Model'),
            SubModel: this.getUrlParameterByName('SubModel'),

            Transmission: this.getUrlParameterByName('Transmission'),
            Engine: this.getUrlParameterByName('Engine'),
            DriveType: this.getUrlParameterByName('DriveType'),
            Brake: this.getUrlParameterByName('Brake'),
            Gvw: this.getUrlParameterByName('Gvw'),
            Body: this.getUrlParameterByName('Body'),

            AcesEngineId: this.getUrlParameterByName('AcesEngineId'),
            AcesId: this.getUrlParameterByName('AcesId'),
            AcesBaseId: this.getUrlParameterByName('AcesBaseId'),
            AcesEngineBaseId: this.getUrlParameterByName('AcesEngineBaseId'),
            AcesEngineConfigId: this.getUrlParameterByName('AcesEngineConfigId'),
            AcesSubmodelId: this.getUrlParameterByName('AcesSubmodelId')
        }
    }
	
	/**
	* Helper method to get a Query string parameter from URL
	* @param {string} name - Query Param Name
	* @param {string} url - URL to parse. If not provided, will use URL from window.location.href
	* @return {string} Returns found query param, or null
	*/
	getUrlParameterByName(name, url) {
		if (!url)
			url = window.location.href;
		name = name.replace(/[\[\]]/g, "\\$&");
		var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
		results = regex.exec(url);
		if (!results)
			return null;
		if (!results[2])
			return '';
		return decodeURIComponent(results[2].replace(/\+/g, " "));
	}

    /**
	* See spec of Catalog SDK: IPartItem3 (versions of Manager prior to 8.1 will not recognize extended attribute PartCategory)
	* When adding multiples of the same part, pass correct quantity member, do not call createPartItem( ) multiple times for same part
	* @param {string} partNumber - Part Number
	* @param {string} manufacturerLineCode - Line Code - must not be blank
	* @param {string} manufacturerName - Manufacturer name
	* @param {string} description - Description of part
	* @param {number} unitList - List
	* @param {number} unitCost - Cost
	* @param {number} unitCore - Core
	* @param {number} quantity - Quantity transferred.
	* @param {boolean} isTire - True for IsTire
	* @param {string} size - Tire Size, or other size information
	* @param {string} upcCode - Not currently persisted in Shop Management System
	* @returns {IPartItem3}
	*/
	createPartItem(partNumber, manufacturerLineCode, manufacturerName, description, unitList, unitCost, unitCore, quantity, isTire, size, upcCode) {
	    return {
	        type: "IPartItem2",
	        PartNumber: partNumber,
	        ManufacturerLineCode: manufacturerLineCode,
	        ManufacturerName: manufacturerName,
	        Description: description,
	        UnitList: String(unitList),
	        UnitCost: String(unitCost),
	        UnitCore: String(unitCore),
	        Quantity: String(quantity),
	        IsTire: isTire,
	        Size: String(size),
	        UpcCode: String(upcCode),
			PartCategory: this.PartCategory.UNSPECIFIED
	    };
	}
	
	/**
	* See spec of Catalog SDK: IPartItem3 (versions of Manager prior to 8.1 will not recognize extended attribute PartCategory)
	* When adding multiples of the same part, pass correct quantity member, do not call createPartItem2( ) multiple times for same part
	* @param {string} partNumber - Part Number
	* @param {string} manufacturerLineCode - Line Code - must not be blank
	* @param {string} manufacturerName - Manufacturer name
	* @param {string} description - Description of part
	* @param {number} unitList - List
	* @param {number} unitCost - Cost
	* @param {number} unitCore - Core
	* @param {number} quantity - Quantity transferred.
	* @param {string} size - Tire Size, or other size information
	* @param {string} upcCode - Not currently persisted in Shop Management System
	* @param {PartCategory} partCategory - Type of part
	* @returns {IPartItem3}
	*/
	createPartItem2(partNumber, manufacturerLineCode, manufacturerName, description, unitList, unitCost, unitCore, quantity, size, upcCode, partCategory) {
		var isTire = (partCategory === this.PartCategory.TIRE);
		var part = this.createPartItem(partNumber, manufacturerLineCode, manufacturerName, description, unitList, unitCost, unitCore, quantity, isTire, size, upcCode);	
		part.PartCategory = partCategory;

	    return part;
	}

    /**
	* A note item you can create and add to your cart array
	* @param {string} note - Note text to create
	* @returns {INoteItem}
	*/
    createNoteItem(note) {
        return { type: "INoteItem", Note: note };
    }

    /**
	* A labor item you can create and add to your cart array
	* @param {string} description - Desription of labor
	* @param {number} hours - Hours of labor
	* @param {number} price - Price of labor
	* @returns {ILaborItem}
	*/
    createLaborItem(description, hours, price) {
        return { type: "ILaborItem", Description: description, Hours: String(hours), Price: String(price) };
    }

    /**
	* Typically parts are simply transferred and ordered later. 
	* However, if your catalog is placing orders directly within the Go Shopping session, use this method to record that purchase
	* back to Manager. Add in parts using addOrderPart2
	* @param {string} orderMessage - A message for order
	* @param {string} deliveryOptions - Delivery method
	* @param {string} confirmationNumber - Purchase Order/Confirmation Number
    * @param {string} trackingNumber - (Optional | Max 20 Chars) If provided, allows for tracking order shipping/tracking in Manager app. This field will be ignored in Manager releases < 8.2.1 
	* @see Catalog#addOrderPart2
	* @returns {IOrder}
	*/
    createOrderItem(orderMessage, deliveryOptions, confirmationNumber, trackingNumber) {
        return {
            type: "IOrder",
            OrderMessage: orderMessage,
            DeliveryOptions: deliveryOptions,
            ConfirmationNumber: confirmationNumber,
            TrackingNumber: trackingNumber,
            Parts: []
        };
    }

    /**
	* Must have created an IOrder object using factory method. Then you can use this to insert an ordered part into the purchased order
	* @param {string} orderItem - Order created via createOrderItem
	* @param {string} locationId - Identifier for location part is from
	* @param {string} locationName - Name for location part is from
	* @param {string} status - Order status
	* @param {string} partNumber - Part Number
	* @param {string} manufacturerLineCode - Manufacturer line code - required
	* @param {string} manufacturerName - Manufacturer Name
	* @param {string} description - Description of part
	* @param {number} unitList - List
	* @param {number} unitCost - Cose
	* @param {number} unitCore - Any Core Amount
	* @param {number} quantityRequested - Typically, qty requested = qty ordered = qty available. However, you can return different values. 
	* @param {number} quantityOrdered - Actual qty of parts successfully ordered
	* @param {number} quantityAvailable - Available qty
	* @deprecated Prefer using addOrderPart2
	* @returns {IOrderPart}
	* @see Catalog#addOrderPart2
	* @see Catalog#createOrderItem
	*/
    addOrderPart(orderItem, locationId, locationName, status, partNumber, manufacturerLineCode, manufacturerName, description, unitList, unitCost, unitCore, quantityRequested, quantityOrdered, quantityAvailable) {
        this.addOrderPart2(orderItem, locationId, locationName, status, partNumber, manufacturerLineCode, manufacturerName, description, unitList, unitCost, unitCore, quantityRequested, quantityOrdered, quantityAvailable, "", this.PartCategory.UNSPECIFIED)
    }
	
    /**
	* Must have created an IOrder object using factory method. Then you can use this to insert an ordered part into the purchased order
	* @param {string} orderItem - Order created via createOrderItem
	* @param {string} locationId - Identifier for location part is from
	* @param {string} locationName - Name for location part is from
	* @param {string} status - Order status
	* @param {string} partNumber - Part Number
	* @param {string} manufacturerLineCode - Manufacturer line code - required
	* @param {string} manufacturerName - Manufacturer Name
	* @param {string} description - Description of part
	* @param {number} unitList - List
	* @param {number} unitCost - Cose
	* @param {number} unitCore - Any Core Amount
	* @param {number} quantityRequested - Typically, qty requested = qty ordered = qty available. However, you can return different values. 
	* @param {number} quantityOrdered - Actual qty of parts successfully ordered
	* @param {number} quantityAvailable - Available qty
	* @param {string} size - Part Size (e.g. Tire Size)
	* @param {PartCategory} partCategory - Type of part
	* @returns {ICartOrderedPart}
	* @see Catalog#createOrderItem
	*/
    addOrderPart2(orderItem, locationId, locationName, status, partNumber, manufacturerLineCode, manufacturerName, description, unitList, unitCost, unitCore, quantityRequested, quantityOrdered, quantityAvailable, size, partCategory) {
        orderItem.Parts.push({
            type: 'IOrderPart',
            LocationId: locationId,
            LocationName: locationName,
            Status: status,
            PartNumber: partNumber,
            ManufacturerLineCode: manufacturerLineCode,
            ManufacturerName: manufacturerName,
            Description: description,
            UnitList: String(unitList),
            UnitCost: String(unitCost),
            UnitCore: String(unitCore),
            QuantityRequested: String(quantityRequested),
            QuantityOrdered: String(quantityOrdered),
            QuantityAvailable: String(quantityAvailable),
			PartCategory: partCategory,
			Size: size
        });
    }
}

/**
 * Vehicle Information Object
 * @class IVehicle
 * @property {string} Vin - VIN
 * @property {string} Year - Year
 * @property {string} Make - Make
 * @property {string} Model - Model
 * @property {string} SubModel - Submodel
 * @property {string} Transmission - Transmission
 * @property {string} Engine - Engine
 * @property {string} DriveType - Drive Type
 * @property {string} Brake - Brake
 * @property {string} Gvw - Gross Vehicle Weight
 * @property {string} Body - Body Description
 * @property {string} AcesEngineId
 * @property {string} AcesId
 * @property {string} AcesBaseId
 * @property {string} AcesEngineBaseId
 * @property {string} AcesEngineConfigId
 * @property {string} AcesSubmodelId
 * @see Catalog#vehicle
*/


/**
 * Opaque JavaScript object - used on ManagerSE side. Represents an actual order that will ship/billed to customer.
 * Use Catalog class to create and add to an IOrder
 * @class IOrder
 * @see Catalog#createOrderItem
 * @see Catalog#createPartItem2
*/

/**
 * Currently opaque JavaScript object. Used on ManagerSE side. Use Catalog class to create and add to an IOrder
 * @class IOrderPart
 * @deprecated Recomended new code use Catalog.addOrderPart2 (ICartOrderedPart)
 * @see Catalog#addOrderPart2
 * @see Catalog#addOrderPart
*/

/**
 * Currently opaque JavaScript object. Used on ManagerSE side. Use Catalog class to create and add to an IOrder
 * @class ICartOrderedPart
 * @see Catalog#addOrderPart2
*/

/**
 * Currently opaque JavaScript object. Used on ManagerSE side. Use Catalog class to create this object. Afterwards, add to the array you pass to transferParts(cart)
 * @class IPartItem3
 * @see Catalog#createPartItem2
 * @see Catalog#createPartItem
 * @see Catalog#transferParts
*/

/**
 * Currently opaque JavaScript object. Used on ManagerSE side. Use Catalog class to create this object. Afterwards, add to the array you pass to transferParts(cart)
 * @class INoteItem
 * @see Catalog#createNoteItem
 * @see Catalog#transferParts
*/

/**
 * Currently opaque JavaScript object. Used on ManagerSE side. Use Catalog class to create this object. Afterwards, add to the array you pass to transferParts(cart)
 * @class ILaborItem
 * @see Catalog#createLaborItem
 * @see Catalog#transferParts
*/
