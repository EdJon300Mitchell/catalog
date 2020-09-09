using System;

namespace Mitchell1.Browser.Interfaces
{
	/// <summary>
	/// Request object
	/// </summary>
	public class JavascriptRequest
	{
		/// <summary>
		/// Required Callback to be executed when Javascript return ready (could be called from different thread)
		/// </summary>
		public Action<JavascriptRequest> Callback { get; set; }

		/// <summary>
		/// True/False indicated if Javascript completed successfully
		/// </summary>
		public bool Success { get; set; }

		/// <summary>
		/// Result of error or success
		/// </summary>
		public string Result { get; set; }
	}
}
