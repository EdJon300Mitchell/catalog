using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;

namespace Mitchell1.Browser.Interfaces
{
	/// <summary>
	/// Represents a browser control
	/// </summary>
	public interface IWebBrowserControl<T> : IDisposable
	{
		/// <summary>
		/// UI component
		/// </summary>
		T Control { get; }

        /// <summary>
        /// If true (defaults) launches external browser for popups
        /// </summary>
        bool OpenExternalWindow { get; set; }

	    /// <summary>
	    /// Get or Set the current URL
	    /// </summary>
	    string Url { get; set; }

	    /// <summary>
	    /// Returns if control is disposed
	    /// </summary>
	    bool IsDisposed { get; }

	    /// <summary>
	    /// Are dev tools available
	    /// </summary>
	    bool IsDeveloperToolAvailable { get; }

	    /// <summary>
	    /// Original HTML text (not updated with any DOM changes) of MainFrame
	    /// </summary>
	    string DocumentText { get; set; }

		/// <summary>
		/// Stops loading/rendering
		/// </summary>
		void Stop();

		/// <summary>
		/// If IsDeveloperToolAvailable = true, shows the tools window. Otherwise, does nothing
		/// </summary>
		void ShowDeveloperTools();

		/// <summary>
		/// Runs Javascript (returns no values)
		/// </summary>
		void ExecuteJavascript(string javascript);

		/// <summary>
		/// Runs Javascript (returns result asynchronously in callback)
		/// </summary>
		void ExecuteJavascriptAsync(string javascript, JavascriptRequest request);

        /// <summary>
        /// Registers a callback to be fired when page javascript executes nativeImplementation(action /*, ...*/) to be called
        /// </summary>
        /// <param name="action">Action string of JS string to match</param>
        /// <param name="arguments">Arguments that were passed from Javascript</param>
	    void JavaScriptRegisterActionCallback(string action, Action<object[]> arguments);

        /// <summary>
        /// Unregisters any action with given name
        /// </summary>
	    void JavaScriptUnregisterAction(string action);

		/// <summary>
		/// Gets a list of cookies for current URL.
		/// </summary>
		IList<Cookie> GetCookies(bool includeHttpOnly);

	    /// <summary>
	    /// Gets a list of cookies for specific URL
	    /// </summary>
	    IList<Cookie> GetCookies(string url);

		/// <summary>
		/// Finds frame - returns null if not found
		/// </summary>
		WebFrame FindFrameByIdentifier(long identifier);

		/// <summary>
		/// Finds frame - returns null if not found
		/// </summary>
		WebFrame FindFrameByName(string name);

        /// <summary>
        /// Returns the main Frame
        /// </summary>
        /// <returns></returns>
        WebFrame MainFrame { get; }

		/// <summary>
		/// Returns the given frames HTML
		/// </summary>
		string GetFrameDocumentText(WebFrame frame);

		/// <summary>
		/// Occurs when navigation started
		/// </summary>
		event EventHandler<WebControlNavigatingEventArgs> Navigating;

		/// <summary>
		/// Occurs when navigation finished (HTTP Code has page response)
		/// </summary>
		event EventHandler<WebControlNavigatedEventArgs> Navigated;

		/// <summary>
		/// Occurs when error navigating to Url (host not reachable.. etc)
		/// </summary>
		event EventHandler<WebControlErrorEventArgs> LoadError;

		/// <summary>
		/// Occurs if trying to open a popup as target (_blank).
		/// </summary>
		event EventHandler<WebControlPopupEventArgs> PopupRequested;

		/// <summary>
		/// Occurs when developer tools mode is available for use
		/// </summary>
		event EventHandler IsDeveloperToolAvailableChanged;
	}
}