using System;
using System.Collections.Generic;
using Mitchell1.Catalog.Framework.Interfaces;
using System.ComponentModel;
using Mitchell1.Online.Catalog.Host.Orders;
using Mitchell1.Online.Catalog.Host.TransferObjects;

namespace Mitchell1.Catalog.Driver.Helpers
{
	public class OrderRequestResponse
	{
		[TypeConverter(typeof(OrderRequestTypeConverter))]
		public OrderRequest Request { get; set; } = new OrderRequest();

		[TypeConverter(typeof(OrderResponseTypeConverter))]
		public OrderResponse Response { get; set; }
	}

	public class OrderRequestTypeConverter : TypeConverter
	{
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(typeof(OrderRequest));
		}
	}

	public class OrderResponseTypeConverter : TypeConverter
	{
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(typeof(OrderResponse));
		}
	}
}
