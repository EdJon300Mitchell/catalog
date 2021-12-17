using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Mitchell1.Browser.Interfaces;
using Xilium.CefGlue;
using Xilium.CefGlue.WindowsForms;

namespace Mitchell1.Browser
{
	internal class WebBrowserControl : CefWebBrowser, IWebBrowserControl<Control>
	{
		private readonly object pendingUrlLock = new object();
		private string pendingUrl;
		private string pendingDocText;
		private int javascriptCallbackId;
		private readonly Dictionary<int, JavascriptRequest> javascriptCallbacks = new Dictionary<int, JavascriptRequest>();
	    private readonly WindowsFormsSynchronizationContext sharedContext;
        private Dictionary<string, Action<object[]>> registeredCallbacks = new Dictionary<string, Action<object[]>>();
	    private bool openExternalWindow = true;

		public WebBrowserControl(WindowsFormsSynchronizationContext sharedContext)
			: base(sharedContext)
		{
		    this.sharedContext = sharedContext;
			StartUrl = null;
		}

		public Control Control
		{
			get { return this; }
		}

        [DefaultValue(true)]
	    public bool OpenExternalWindow
	    {
	        get { return openExternalWindow; }
            set { openExternalWindow = value; }
	    }

	    public WebFrame MainFrame
	    {
	        get
	        {
	            if (!IsDisposed && Browser != null)
	            {
	                var frame = Browser.GetMainFrame();
	                if (frame != null)
	                {
	                    return CreateFrame(frame);
	                }
	            }

	            return null;
	        }
	    }

		public WebFrame FindFrameByIdentifier(long identifier)
		{
			if (!IsDisposed && Browser != null)
			{
				var frame = Browser.GetFrame(identifier);
				if (frame != null)
				{
					return CreateFrame(frame);
				}
			}

			return null;
		}

		public WebFrame FindFrameByName(string name)
		{
			if (!IsDisposed && Browser != null)
			{
				var frame = Browser.GetFrame(name);
				if (frame != null)
				{
					return CreateFrame(frame);
				}
			}

			return null;
		}

		public string GetFrameDocumentText(WebFrame frame)
		{
			if (IsDisposed || frame == null || Browser == null)
			{
				return String.Empty;
			}

			var cefFrame = Browser.GetFrame(frame.Identifier);
			if (cefFrame == null)
			{
				return String.Empty;
			}

			var wait = new ManualResetEvent(false);
			var visitor = new SourceStringVisitor(wait);
			cefFrame.GetSource(visitor);

			if (wait.WaitOne(1000 * 10))
			{
				var source = visitor.Source;
				if (source == null)
				{
					Trace.WriteLine("No source found for document");
					return String.Empty;
				}
				return source;
			}

			Trace.WriteLine("Timed out retrieving source");
			return String.Empty;
		}

	    internal class Mitchell1CefWebClient : CefWebClient
	    {
	        private readonly WeakReference<WebBrowserControl> owner;
            public Mitchell1CefWebClient(WebBrowserControl browser)
                : base(browser)
	        {
	            owner = new WeakReference<WebBrowserControl>(browser);
	        }

	        protected override bool OnProcessMessageReceived(CefBrowser browser, CefFrame frame, CefProcessId sourceProcess, CefProcessMessage message)
	        {
#if DEBUG
                Trace.WriteLine("Mitchell1CefWebClient::OnProcessMessageReceived " + message.Name);
#endif

	            if (!String.IsNullOrEmpty(message.Name) && message.Name.StartsWith("Extension-"))
	            {
	                if (message.Arguments != null && message.Arguments.Count > 0)
	                {
                        if (message.Arguments.GetValueType(0) == CefValueType.String)
	                    {
	                        ParseExtensionMessage(message.Arguments);
	                    }
	                }
	            }

                return base.OnProcessMessageReceived(browser, frame, sourceProcess, message);
	        }

	        private static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
	        {
	            var expandoDict = expando as IDictionary<string, object>;
	            if (expandoDict.ContainsKey(propertyName))
	            {
	                expandoDict[propertyName] = propertyValue;
	            }
	            else
                {
	                expandoDict.Add(propertyName, propertyValue);
	            }
	        }

	        private object ParseDataItem(CefValue value)
	        {
	            switch (value.GetValueType())
	            {
	                case CefValueType.Invalid: return null;
	                case CefValueType.Null: return null;
	                case CefValueType.Bool: return value.GetBool();
	                case CefValueType.Int: return value.GetInt();
	                case CefValueType.Double: return value.GetDouble();
	                case CefValueType.String: return value.GetString();
	                case CefValueType.Binary: return value.GetBinary();
	                case CefValueType.Dictionary:
	                    var item = value.GetDictionary();
	                    var keys = item.GetKeys();
	                    if (!keys.Contains("type") && !keys.Contains("value"))
	                    {
	                        return null;
	                    }

	                    var itemType = item.GetString("type");
	                    if (itemType == "datetime")
	                    {
	                        var itemValue = item.GetBinary("value");
	                        return DateTime.FromBinary(BitConverter.ToInt64(itemValue.ToArray(), 0));
	                    }
	                    else if (itemType == "object")
	                    {
	                        var itemValue = item.GetDictionary("value");
	                        dynamic retObject = new ExpandoObject();
	                        var objectProperties = itemValue.GetKeys();
	                        foreach (var property in objectProperties)
	                        {
	                            var realValue = ParseDataItem(itemValue.GetValue(property));
	                            AddProperty(retObject, property, realValue);
	                        }

	                        return retObject;
	                    }
	                    
	                    break;
	                case CefValueType.List:
	                    var list = new List<object>();

	                    var existingList = value.GetList();
	                    for (int i = 0; i < existingList.Count; ++i)
	                    {
	                        list.Add(ParseDataItem(existingList.GetValue(i)));
	                    }

	                    return list;
	            }

	            return null;
	        }

	        private void ParseExtensionMessage(CefListValue values)
	        {
	            var action = values.GetString(0);

	            var count = values.Count;
	            object [] parameters = null;
	            if (count > 1)
	            {
	                var tempParam = new List<object>(count - 1);
	                for (int i = 1; i < count; ++i)
	                {
	                    tempParam.Add(ParseDataItem(values.GetValue(i)));
	                }

	                parameters = tempParam.ToArray();
	            }
                
	            WebBrowserControl browserControl;
	            if (owner.TryGetTarget(out browserControl))
	            {
	                browserControl.sharedContext.Post(delegate
	                {
	                    browserControl.DelegateActionEvent(action, parameters);
	                }, null);
	            }
	        }
	    }

	    private void DelegateActionEvent(string action, object[] arguments)
	    {
            if (IsDisposed || String.IsNullOrEmpty(action))
	        {
	            return;
	        }

	        Action<object[]> registeredAction;
	        if (registeredCallbacks.TryGetValue(action, out registeredAction))
	        {
	            try
	            {
	                registeredAction(arguments);
	            }
	            catch (Exception e)
	            {
	                Trace.WriteLine("Exception executing JavaScript Callback Handler: " + e.Message);
	            }
	        }
	    }

	    protected override CefWebClient CreateWebClient()
	    {
	        return new Mitchell1CefWebClient(this);
	    }

        protected override void OnBeforePopup(BeforePopupEventArgs e)
        {
            base.OnBeforePopup(e);

            var args = new WebControlPopupEventArgs(CreateFrame(e.Frame), e.TargetUrl);
            RaisePopupRequested(args);
            if (args.Handled)
            {
                // Prevent CefGlue from opening secondary windows
                e.Handled = true;
                return;
            }

            if (OpenExternalWindow)
            {
                // Prevent CefGlue from opening secondary windows
                e.Handled = true;

                try
                {
                    var uri = new Uri(e.TargetUrl);
                    var schema = uri.Scheme.ToLowerInvariant();
                    if (schema == "http" || schema == "https")
                    {
                        Process.Start(e.TargetUrl);
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("Failed loading browser: " + ex.Message);
                }
            }
        }

	    protected override void OnBeforeClose()
	    {
	        //Override and prevent base class from causing issues - CefGlue needs to be fixed when closing popups
	    }

	    protected override void OnBrowserAfterCreated(CefBrowser browser)
		{
            // Only call base class when browser is not already created - CefGlue needs to be fixed when opening popups
		    if (Browser == null)
		    {
		        base.OnBrowserAfterCreated(browser);
		    }

		    if (Browser != browser)
		    {
		        return;
		    }

			lock (pendingUrlLock)
			{
				if (!String.IsNullOrEmpty(pendingUrl))
				{
					browser.GetMainFrame().LoadUrl(pendingUrl);
				}
				else if (!String.IsNullOrEmpty(pendingDocText))
				{
					// Load a URL first to get HTML text to display
					browser.GetMainFrame().LoadUrl("about:blank");
				    browser.GetMainFrame().LoadUrl(GetDataURI(pendingDocText, "text/html"));
				}
			}

			IsDeveloperToolAvailable = true;
			RaiseIsDeveloperToolAvailableChanged();
		}

		private WebFrame CreateFrame(CefFrame frame)
		{
			return new WebFrame(frame.Identifier, frame.Name);
		}

		protected override void OnLoadEnd(LoadEndEventArgs e)
		{
			base.OnLoadEnd(e);

			//Trace.WriteLine(String.Format("LoadEnded: http: {0}, {1} - {2}", e.HttpStatusCode, e.Frame.Identifier, e.Frame.Url));
			RaiseNavigated(CreateFrame(e.Frame), e.Frame.Url, e.HttpStatusCode);
		}

		protected override void OnLoadStart(LoadStartEventArgs e)
		{
			base.OnLoadStart(e);

			//Trace.WriteLine(String.Format("LoadStarted: {0} - {1}", e.Frame.Identifier, e.Frame.Url));
			RaiseNavigating(CreateFrame(e.Frame), e.Frame.Url);
		}

		protected override void OnLoadError(LoadErrorEventArgs e)
		{
			base.OnLoadError(e);

			//Trace.WriteLine(String.Format("LoadError: {0} - {1}", e.Frame.Identifier, e.Frame.Url));
			RaiseLoadError(CreateFrame(e.Frame), e.FailedUrl, e.ErrorText ?? e.ErrorCode.ToString(), (int)e.ErrorCode);
		}

		public void Stop()
		{
			if (!IsDisposed && Browser != null)
			{
				Browser.StopLoad();
			}
		}

		public void ShowDeveloperTools()
		{
			if (!IsDisposed && IsDeveloperToolAvailable)
			{
				var host = Browser.GetHost();
				var windowInfo = CefWindowInfo.Create();

				windowInfo.SetAsPopup(IntPtr.Zero, "DevTools");
				host.ShowDevTools(windowInfo, new DevToolsWebClient(),
					new CefBrowserSettings(), new CefPoint(0, 0));
			}
		}

		public void ExecuteJavascript(string javascript)
		{
			if (!IsDisposed && Browser != null)
			{
				if (Browser.HasDocument)
				{
					Browser.GetMainFrame().ExecuteJavaScript(javascript, "http://localhost/internal.js", 1);
					return;
				}
			}

			Trace.WriteLine("Unable to ExecuteJavascript with disposed/incorrect state:");
			Trace.WriteLine(javascript);
		}

		public void ExecuteJavascriptAsync(string javascript, JavascriptRequest request)
		{
			if (String.IsNullOrEmpty(javascript))
			{
				throw new ArgumentNullException("javascript");
			}

			if (request == null)
			{
				throw new ArgumentNullException("request");
			}

			if (request.Callback == null)
			{
				throw new ArgumentException("request.Callback is null");
			}

			request.Result = "";
			request.Success = false;

			if (!IsDisposed && Browser != null && Browser.HasDocument)
			{
				int id = Interlocked.Increment(ref javascriptCallbackId);
				lock (javascriptCallbacks)
				{
					javascriptCallbacks[id] = request;
				}

				// TODO: Look at changing renderprocess to use Binding... However, the simplest implementation
				// console message will come back to us in the UI process here
				// Could use eval() to prevent malformed js, but then we would have to escape 's and \'a.
				string msgPrefix = String.Format("[JSC:{0}]", id);
				string wrappedJavascript = String.Format(
					@"
					try {{
						var jsFuncCallResult = (function() {{
							{0}
						}})();
						console.log('{1} Result<[' + jsFuncCallResult + ']>');
					}}
					catch(err) {{
						console.log('{1} Error<[' + err.message + ']>');
					}}",
					javascript, msgPrefix);

				Browser.GetMainFrame().ExecuteJavaScript(wrappedJavascript, "http://localhost/internal.js", 1);
				return;
			}

			Trace.WriteLine("Unable to ExecuteJavascript(Action) with disposed/incorrect state:");
			Trace.WriteLine(javascript);

			// Signal that request is done (with error)
			request.Callback(request);
		}

        public void JavaScriptRegisterActionCallback(string action, Action<object[]> callback)
	    {
            if (!String.IsNullOrEmpty(action) && callback != null)
            {
                registeredCallbacks[action] = callback;
            }
            else
            {
                throw new ArgumentNullException("action");
            }
	    }

	    public void JavaScriptUnregisterAction(string action)
	    {
            if (!String.IsNullOrEmpty(action) && registeredCallbacks.ContainsKey(action))
            {
                registeredCallbacks.Remove(action);
            }
	    }

	    public IList<Cookie> GetCookies(string url)
	    {
            if (String.IsNullOrEmpty(url))
	        {
                Trace.WriteLine("url empty");
	            return new List<Cookie>();
	        }

            return GetCookiesInternal(true, url);
	    }

	    public IList<Cookie> GetCookies(bool includeHttpOnly)
	    {
	        if (String.IsNullOrEmpty(Url))
	        {
	            Trace.WriteLine("Url empty");
	            return new List<Cookie>();
	        }

            return GetCookiesInternal(includeHttpOnly, Url);
	    }

	    protected IList<Cookie> GetCookiesInternal(bool includeHttpOnly, string url)
        {
            var resetEvent = new ManualResetEvent(false);
			// To have query run in correct thread, use callback.. this flow matches (more or less) cookie_unittest.cc
            var cookieManager = CefCookieManager.GetGlobal(new GenericCefCompletionCallback(resetEvent));
			resetEvent.WaitOne();

			resetEvent.Reset();
            IList<Cookie> cookies = new List<Cookie>();
            if (!cookieManager.VisitUrlCookies(url, includeHttpOnly, new Mitchell1CefCookieVisitor(resetEvent, cookies)))
			{
			    Trace.WriteLine("Cookies cannot be accessed at this time");
			    return new List<Cookie>();
			}
			resetEvent.WaitOne();

            return cookies;
		}

		public string DocumentText
		{
			get
			{
				if (!IsDisposed && Browser != null)
				{
					if (Browser.HasDocument)
					{
						var frame = Browser.GetMainFrame();
						if (frame != null)
						{
							var wait = new ManualResetEvent(false);
							var visitor = new SourceStringVisitor(wait);
							frame.GetSource(visitor);

							if (wait.WaitOne(1000 * 10))
							{
								var source = visitor.Source;
								if (source == null)
								{
									Trace.WriteLine("No source found for document");
									return String.Empty;
								}
								return source;
							}

							Trace.WriteLine("Timed out retrieving source");
						}
					}
				}

				return "";
			}
			set
			{
				if (!IsDisposed)
				{
					lock (pendingUrlLock)
					{
						var browser = Browser;
						if (browser == null)
						{
							pendingDocText = value;
						}
						else
						{
                            Browser.GetMainFrame().LoadUrl(GetDataURI(value, "text/html"));
						}
					}
				}
			}
		}

        private string GetDataURI(string data, string mimeType)
        {
            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(data));
            //cefclient was encoding URL. However, base64 looks supported directly in data uri - leaving here in case we find it needed
            //var uriEncoded = Uri.EscapeDataString(base64);
            return string.Format("data:{0};base64,{1}", mimeType, base64);
        }

		public string Url
		{
			get
			{
				return !IsDisposed ? Address : "";
			}
			set
			{
				if (!IsDisposed)
				{
					lock (pendingUrlLock)
					{
						var browser = Browser;
						if (browser == null)
						{
							pendingUrl = value;
						}
						else
						{
							browser.GetMainFrame().LoadUrl(value);
						}
					}
				}
			}
		}

		public bool IsDeveloperToolAvailable { get; private set; }

		public event EventHandler<WebControlNavigatingEventArgs> Navigating;

		public event EventHandler<WebControlNavigatedEventArgs> Navigated;

		public new event EventHandler<WebControlErrorEventArgs> LoadError;

		public event EventHandler<WebControlPopupEventArgs> PopupRequested;

		public event EventHandler IsDeveloperToolAvailableChanged;

		protected void RaisePopupRequested(WebControlPopupEventArgs args)
		{
			var registeredHandler = PopupRequested;
			if (registeredHandler == null || args == null)
			{
				return;
			}

			var clients = registeredHandler.GetInvocationList();
			foreach (var client in clients)
			{
				var eventHandler = client as EventHandler<WebControlPopupEventArgs>;
				if (eventHandler != null)
				{
					eventHandler.Invoke(this, args);
					if (args.Handled)
					{
						return;
					}
				}
			}
		}

		protected void RaiseNavigating(WebFrame frame, string uri)
		{
			var handler = Navigating;
			if (handler != null)
			{
				handler(this, new WebControlNavigatingEventArgs(frame, uri));
			}
		}

		protected void RaiseNavigated(WebFrame frame, string uri, int httpCode)
		{
			var handler = Navigated;
			if (handler != null)
			{
				handler(this, new WebControlNavigatedEventArgs(frame, uri, httpCode));
			}
		}

		protected void RaiseLoadError(WebFrame frame, string url, string error, int httpCode)
		{
			var handler = LoadError;
			if (handler != null)
			{
				handler(this, new WebControlErrorEventArgs(frame, url, error, httpCode));
			}
		}

		protected void RaiseIsDeveloperToolAvailableChanged()
		{
			var handler = IsDeveloperToolAvailableChanged;
			if (handler != null)
			{
				handler(this, EventArgs.Empty);
			}
		}

		protected override void OnConsoleMessage(ConsoleMessageEventArgs e)
		{
			base.OnConsoleMessage(e);

			if (e == null || e.Message == null || e.Message.Length == 0)
			{
				return;
			}

			string msg = e.Message;
			if (!msg.StartsWith("[JSC:"))
			{
				return;
			}

			int lastBracketIndex = msg.IndexOf("] ", StringComparison.InvariantCulture);
			if (lastBracketIndex < 6)
			{
				return;
			}

			e.Handled = true;

			string idString = msg.Substring(5, lastBracketIndex - 5);
			int messageId;
			if (!int.TryParse(idString, out messageId))
			{
				Trace.WriteLine("Invalid MessageId format");
				return;
			}

			JavascriptRequest request;
			lock (javascriptCallbacks)
			{
				if (!javascriptCallbacks.TryGetValue(messageId, out request))
				{
					Trace.WriteLine("JavascriptRequest no longer in queue");
					return;
				}

				javascriptCallbacks.Remove(messageId);
			}

			try
			{
				request.Result = "";
				request.Success = false;

				int index = msg.IndexOf("<[", StringComparison.InvariantCulture);
				int endIndex = msg.LastIndexOf("]>", StringComparison.InvariantCulture);
				if (index > 0 && endIndex > 0 && index < endIndex)
				{
					int offset = lastBracketIndex + 2;
					int length = index - offset;
					if (length > 0)
					{
						string resultType = msg.Substring(offset, length);

						offset = index + 2;
						length = endIndex - offset;
						if (length > 0)
						{
							request.Success = resultType == "Result";
							request.Result = msg.Substring(offset, length);
						}
					}
				}

				request.Callback(request);
			}
			catch (Exception ex)
			{
				Trace.WriteLine("Exception handling javascript message: " + ex);
			}
		}
	}

    internal class GenericCefCompletionCallback : CefCompletionCallback
    {
        private readonly ManualResetEvent resetEvent;

        public GenericCefCompletionCallback(ManualResetEvent resetEvent)
        {
            this.resetEvent = resetEvent;
        }

        protected override void OnComplete()
        {
            resetEvent.Set();
        }
    }

	internal class Mitchell1CefCookieVisitor : CefCookieVisitor
	{
	    private readonly ManualResetEvent resetEvent;
		private readonly IList<Cookie> cookieList;

		public Mitchell1CefCookieVisitor(ManualResetEvent resetEvent, IList<Cookie> cookieList)
		{
		    this.resetEvent = resetEvent;
			this.cookieList = cookieList;
		}

	    protected override void Dispose(bool disposing)
	    {
	        base.Dispose(disposing);
	        resetEvent.Set();
	    }

	    protected override bool Visit(CefCookie cookie, int count, int total, out bool delete)
		{
			delete = false;

		    var realCookie = new Cookie();
			//cookie.Creation;
			realCookie.Domain = cookie.Domain;

			if (cookie.Expires.HasValue)
				realCookie.Expires = cookie.Expires.Value;

			realCookie.HttpOnly = cookie.HttpOnly;
			realCookie.Name = cookie.Name;
			realCookie.Path = cookie.Path;
			realCookie.Secure = cookie.Secure;
			realCookie.Value = cookie.Value;

			cookieList.Add(realCookie);
			return true;
		}
	}

	internal class SourceStringVisitor : CefStringVisitor
	{
		private readonly ManualResetEvent handle;
		private string source;

		public SourceStringVisitor(ManualResetEvent handle)
		{
			this.handle = handle;
		}

		protected override void Visit(string value)
		{
			source = value;
			handle.Set();
		}

		public string Source
		{
			get { return source; }
		}
	}

	internal class DevToolsWebClient : CefClient
	{
	}
}
