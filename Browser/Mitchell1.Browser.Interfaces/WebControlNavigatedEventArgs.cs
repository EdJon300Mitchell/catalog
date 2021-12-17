using System;

namespace Mitchell1.Browser.Interfaces
{
	/// <summary>
	/// Event Args for navigation completed
	/// </summary>
	public class WebControlNavigatedEventArgs : EventArgs
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public WebControlNavigatedEventArgs(WebFrame frame, string url, int httpCode)
		{
			Frame = frame;
			Url = url;
			HttpCode = httpCode;
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
		/// Internal Error Code for reference
		/// </summary>
		public int HttpCode { get; protected set; }
	}
}
