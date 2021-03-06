
/**
 * @fileoverview This file contians a Catalog class used for integration with ManagerSE.
 * JavaScript entry Point into Online Catalog integration with ManagerSE. 
 * @version v3.0.0
*/
class Catalog {	
	/**
	* Create Catalog integration instance. You must only create one of these. Recommended to create this early in page life cycle in case you need
	* to use JavaScript debugger. Use this class in your "Vendor Setup", "Go Shopping", "Price Check", and "Order Parts" Web pages.
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

	/**
	* Only supported in isSetupMode=false. Will close the browser window, and pass back the json
	* @param {string} jsonString - json string to be transferred to the ManagerSE application.
	* Can be used by 'Price Check' (jsonString of PriceCheckResponse object), or 'Order Parts' (jsonString of OrderResponse object).
	*/
	transfer(jsonString) {
        if (!this.isSetupMode) {
			this.communication('transfer', this.apiKey, jsonString);
        }
	}

	/** Cancels the current operation. Will close the browser window  - nothing will be saved or transferred */
	cancelRequest() {
		this.communication('cancel', this.apiKey);
	}
	
	/** Only supported in isSetupMode=true - for Vendor setup, used to save the vendor qualifier information and close the browser window */
	saveConfiguration(configObject) {
		if(this.isSetupMode) {
			this.communication('save', this.apiKey, JSON.stringify(configObject));
		}
	}
	
	/**
	* Only supported in isSetupMode=false - for Go Shopping session, used to transfer items in the cart and close the browser window. 
	* For backwards compatibility with older versions of Manager SE, cart must be a JSON object (not a JSON string)
	* @param {Array<PartItem, LaborItem, NoteItem, ShoppingCartOrder>} cart - Array of items to transfer to Manager. Note, ShoppingCartOrder contains its own list of part items. Do not add those ordered parts to the cart directly
	*/
	transferParts(cart) {
		if(!this.isSetupMode) {
			this.communication('transfer', this.apiKey, cart);
		}
	}

	/**
	* Returns the Vehicle (if any) that was selected on Shop Management System's repair order (only populated within Go Shopping session)
	* @see Vehicle
	* @return {Vehicle}
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
	* Returns the HostData of Shop Management System (for Manager SE versions where HostApiLevel is less than 3 this will be empty)
	* @see HostData
	* @return {HostData}
	*/
	get hostData() {
		return {
			ApplicationTitle: this.getUrlParameterByName('ApplicationTitle'),
			ApplicationVersion: this.getUrlParameterByName('ApplicationVersion'),
			LaborRate: this.getUrlParameterByName('LaborRate'),
			HostApiLevel: this.getUrlParameterByName('HostApiLevel')
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
	* See spec of Online Catalog SDK: PartItem (versions of Manager prior to 8.1 will not recognize extended attribute PartCategory)
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
	* @returns {PartItem}
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
	* See spec of Online Catalog SDK: PartItem (Note: earlier versions of Manager may not recognize attributes after PartCategory)
	* When adding multiples of the same part, pass correct quantity member, do not call createPartItem2( ) multiple times for same part
	* @param {string} partNumber Part Number
	* @param {string} manufacturerLineCode Line Code - must not be blank
	* @param {string} manufacturerName Manufacturer name
	* @param {string} description Description of part
	* @param {number} unitList List
	* @param {number} unitCost Cost
	* @param {number} unitCore Core
	* @param {number} quantity Quantity transferred.
	* @param {string} size Tire Size, or other size information (optional)
	* @param {string} upcCode Not currently persisted in Shop Management System (optional)
	* @param {PartCategory} partCategory Type of part (optional)
	* @param {string} supplierName Name of the Supplier (optional)
	* @param {string} metadata Custom metadata for use by the catalog (optional)
    * @param {string} shippingDescription shipping description (optional)
    * @param {number} shippingCost shipping cost (optional)
	* @returns {PartItem}
	*/
	createPartItem2(partNumber, manufacturerLineCode, manufacturerName, description, unitList, unitCost, unitCore, quantity,
		size, upcCode, partCategory, supplierName, metadata, shippingDescription, shippingCost) {
		partCategory = (typeof partCategory !== "undefined") ? partCategory : this.PartCategory.UNSPECIFIED;
		var isTire = (partCategory === this.PartCategory.TIRE);
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
			PartCategory: partCategory,
			SupplierName: supplierName,
			Metadata: metadata,
			ShippingDescription: shippingDescription,
			ShippingCost: shippingCost
		};
	}

	/**
	 * A note item you can create and add to your cart array
	 * @param {string} note - Note text to create
	 * @returns {NoteItem}
	 */
    createNoteItem(note) {
        return { type: "INoteItem", Note: note };
    }

    /**
	* A labor item you can create and add to your cart array
	* @param {string} description - Description of labor
	* @param {number} hours - Hours of labor
	* @param {number} price - Price of labor
	* @returns {LaborItem}
	*/
    createLaborItem(description, hours, price) {
        return { type: "ILaborItem", Description: description, Hours: String(hours), Price: String(price) };
    }

    /**
	* Typically parts are simply transferred and ordered later. 
	* However, if your catalog is placing orders directly within the Go Shopping session, use this method to record that purchase
	* back to Manager. Add in parts using addOrderPart2
	* @param {string} orderMessage - A message for order
	* @param {string} deliveryOption - Delivery method
	* @param {string} confirmationNumber - Purchase Order/Confirmation Number
    * @param {string} trackingNumber - (Optional | Max 20 Chars) If provided, allows for tracking order shipping/tracking in Manager app. This field will be ignored in Manager releases < 8.2.1 
	* @see Catalog#addOrderPart2
	* @returns {ShoppingCartOrder}
	*/
    createOrderItem(orderMessage, deliveryOption, confirmationNumber, trackingNumber) {
        return {
            type: "IOrder",
            OrderMessage: orderMessage,
            DeliveryOptions: deliveryOption,
            ConfirmationNumber: confirmationNumber,
            TrackingNumber: trackingNumber,
            Parts: []
        };
    }

    /**
    * Adds a new ShoppingCartOrderPart to existing ShoppingCartOrder.
	* Must have created a ShoppingCartOrder object using factory method. Then you can use this to insert an ordered part into the purchased order
	* @param {string} orderItem - ShoppingCartOrder created via createOrderItem
	* @param {string} locationId - Identifier for location part is from
	* @param {string} locationName - Name for location part is from
	* @param {string} status - Order status
	* @param {string} partNumber - Part Number
	* @param {string} manufacturerLineCode - Manufacturer line code - required
	* @param {string} manufacturerName - Manufacturer Name
	* @param {string} description - Description of part
	* @param {number} unitList - List
	* @param {number} unitCost - Cost
	* @param {number} unitCore - Any Core Amount
	* @param {number} quantityRequested - Typically, qty requested = qty ordered = qty available. However, you can return different values. 
	* @param {number} quantityOrdered - Actual qty of parts successfully ordered
	* @param {number} quantityAvailable - Available qty
	* @deprecated Prefer using addOrderPart2
	* @see Catalog#addOrderPart2
	* @see Catalog#createOrderItem
	*/
    addOrderPart(orderItem, locationId, locationName, status, partNumber, manufacturerLineCode, manufacturerName, description, unitList, unitCost, unitCore, quantityRequested, quantityOrdered, quantityAvailable) {
        this.addOrderPart2(orderItem, locationId, locationName, status, partNumber, manufacturerLineCode, manufacturerName, description, unitList, unitCost, unitCore, quantityRequested, quantityOrdered, quantityAvailable, "", this.PartCategory.UNSPECIFIED);
    }
	
    /**
    * Adds a new ShoppingCartOrderPart to existing ShoppingCartOrder.
	* See spec of Online Catalog SDK: ShoppingCartOrderPart (Note: earlier versions of Manager may not recognize attributes after PartCategory)
	* Must have created an ShoppingCartOrder object using factory method. Then you can use this to insert an ordered part into the purchased order
	* @param {string} orderItem - Order created via createOrderItem
	* @param {string} locationId - Identifier for location part is from
	* @param {string} locationName - Name for location part is from
	* @param {string} status - Order status
	* @param {string} partNumber - Part Number
	* @param {string} manufacturerLineCode - Manufacturer line code - required
	* @param {string} manufacturerName - Manufacturer Name
	* @param {string} description - Description of part
	* @param {number} unitList - List
	* @param {number} unitCost - Cost
	* @param {number} unitCore - Any Core Amount
	* @param {number} quantityRequested - Typically, qty requested = qty ordered = qty available. However, you can return different values. 
	* @param {number} quantityOrdered - Actual qty of parts successfully ordered
	* @param {number} quantityAvailable - Available qty
	* @param {string} size - Part Size (e.g. Tire Size) (optional)
	* @param {PartCategory} partCategory - Type of part (optional)
    * @param {string} supplierName - supplier of the part (optional)
    * @param {string} metadata - custom metadata for the part (optional)
    * @param {string} shippingDescription - shipping description (optional)
    * @param {number} shippingCost - shipping cost (optional)
	* @see Catalog#createOrderItem
	*/
    addOrderPart2(orderItem, locationId, locationName, status, partNumber, manufacturerLineCode, manufacturerName, description, unitList, unitCost, unitCore, quantityRequested, quantityOrdered, quantityAvailable, size, partCategory, supplierName, metadata, shippingDescription, shippingCost) {
	    partCategory = (typeof partCategory !== "undefined") ? partCategory : this.PartCategory.UNSPECIFIED;
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
			Size: size,
			PartCategory: partCategory,
            SupplierName : supplierName,
            Metadata: metadata,
            ShippingDescription: shippingDescription,
            ShippingCost: shippingCost
        });
    }
}

/**
 * Vehicle Information Object
 * @class Vehicle
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
 * Use Catalog class to create and add to an Order
 * @class ShoppingCartOrder
 * @see Catalog#createOrderItem
 * @see Catalog#addOrderPart2
 * @see Catalog#transferParts
*/

/**
 * Currently opaque JavaScript object. Used on ManagerSE side. Use Catalog class to create and add to an Order
 * @class ShoppingCartOrderPart
 * @deprecated Recommended new code use Catalog.addOrderPart2 (ShoppingCartOrderPart)
 * @see Catalog#addOrderPart2
 * @see Catalog#addOrderPart
*/

/**
 * Currently opaque JavaScript object. Used on ManagerSE side. Use Catalog class to create this object. Afterwards, add to the array you pass to transferParts(cart)
 * @class PartItem
 * @see Catalog#createPartItem2
 * @see Catalog#createPartItem
 * @see Catalog#transferParts
*/

/**
 * Currently opaque JavaScript object. Used on ManagerSE side. Use Catalog class to create this object. Afterwards, add to the array you pass to transferParts(cart)
 * @class NoteItem
 * @see Catalog#createNoteItem
 * @see Catalog#transferParts
*/

/**
 * Currently opaque JavaScript object. Used on ManagerSE side. Use Catalog class to create this object. Afterwards, add to the array you pass to transferParts(cart)
 * @class LaborItem
 * @see Catalog#createLaborItem
 * @see Catalog#transferParts
*/
