using System;

namespace Mitchell1.Browser.Interfaces
{
	/// <summary>
	/// Event Args for navigation completed
	/// </summary>
	public class WebControlNavigatingEventArgs : EventArgs
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public WebControlNavigatingEventArgs(WebFrame frame, string url)
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
	}
}
