using System;
using System.Net;

namespace Mitchell1.Online.Catalog.Host.API
{
	public class ApiCallException : Exception
	{
		public ApiCallException(string data, ContentType contentType, HttpStatusCode statusCode, string reason) : base(data ?? $"Operation failed with {statusCode} error")
		{
			Type = contentType;
			StatusCode = statusCode;
			Reason = reason ?? "<Unknown>";
		}

		public enum ContentType
		{
			HTML,
			Text
		}

		public ContentType Type { get; }
		public HttpStatusCode StatusCode { get; }
		public string Reason { get; }
	}
}