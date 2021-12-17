using System;

namespace Mitchell1.Browser.Interfaces
{
	/// <summary>
	/// Event Args for popup
	/// </summary>
	public class WebControlPopupEventArgs : EventArgs
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public WebControlPopupEventArgs(WebFrame frame, string url)
		{
			Frame = frame;
			Url = url;
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
		/// Set to true to handle.
		/// </summary>
		public bool Handled { get; set; }
	}
}
