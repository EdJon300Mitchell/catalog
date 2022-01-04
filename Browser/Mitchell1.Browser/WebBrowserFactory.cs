using System.Windows.Forms;
using Mitchell1.Browser.Interfaces;

namespace Mitchell1.Browser
{
	/// <summary>
	/// Single Instance Management of Browser. Use helper methods here to create controls
	/// </summary>
	public static class WebBrowserFactory
	{
		private static readonly CefBrowserSupport cefBrowserSupport = new CefBrowserSupport();

		/// <summary>
		/// Returns a Control that implements IWebBrowserControl for a WinForm Control
		/// </summary>
		public static IWebBrowserControl<Control> CreateBrowserControl()
		{
			lock (cefBrowserSupport)
			{
				if (!cefBrowserSupport.Initialized)
				{
					cefBrowserSupport.Initialize();
				}
			}

			return new WebBrowserControl(new WindowsFormsSynchronizationContext());
		}
	}
}
