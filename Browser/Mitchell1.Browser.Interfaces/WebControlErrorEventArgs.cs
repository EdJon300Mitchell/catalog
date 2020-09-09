using System;

namespace Mitchell1.Browser.Interfaces
{
	/// <summary>
	/// Event Args for error conditions
	/// </summary>
	public class WebControlErrorEventArgs : EventArgs
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public WebControlErrorEventArgs(WebFrame frame, string url, string error, int errorCode)
		{
			Frame = frame;
			Url = url;
			Error = error;
			ErrorCode = errorCode;
		}

		/// <summary>
		/// Frame
		/// </summary>
		public WebFrame Frame { get; protected set; }

		/// <summary>
		/// Url
		/// </summary>
		public string Url { get; protected set; }

		/// <summary>
		/// Error text (may be blank/null)
		/// </summary>
		public string Error { get; protected set; }

		/// <summary>
		/// Internal Error Code for reference
		/// </summary>
		public int ErrorCode { get; protected set; }
	}
}
