using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Xilium.CefGlue;

namespace Mitchell1.Browser
{
	/// <summary>
	/// There must be only one instance of this class per process/app.
	/// </summary>
	internal class CefBrowserSupport
	{
		private const string ProductVersion = "ManagerSE/7.1";
		private const string SubProcessApp = "Mitchell1.BrowserProcess-{0}.exe";
		private const string ExpectedBrowserDllRoot = "Browser";

		public bool Initialized { get; private set; }

		public void Initialize()
		{
			if (Initialized)
			{
				return;
			}

			string chromiumFolder = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "M1-SK"), "Browser");
			string chromiumLog = Path.Combine(chromiumFolder, "Chromiumn.log");
			string chromiumCache = Path.Combine(chromiumFolder, "Cache");

			string browserRoot = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) ?? "";
			browserRoot = Path.Combine(browserRoot, ExpectedBrowserDllRoot);

			try
			{
				string path = browserRoot;
				path = Path.Combine(path, !ProcessorInfo.IsProcess64Bit ? "x86" : "64");

				CefRuntime.Load(path);
			}
			catch (DllNotFoundException ex)
			{
				Trace.WriteLine("Unable to load CefGlue DLL's: " + ex.Message);
				throw;
			}
			catch (CefRuntimeException ex)
			{
				Trace.WriteLine("Runtime error with Cef: " + ex.Message);
				throw;
			}
			catch (Exception ex)
			{
				Trace.WriteLine("Unknown error loading Cef: " + ex.Message);
				throw;
			}

			var mainArgs = new CefMainArgs(new string[0]);
			var app = new Mitchell1CefApp();

			CefRuntime.ExecuteProcess(mainArgs, app, IntPtr.Zero);

		    int remoteDebuggingPort = 0;
		    try
		    {
                var cmdArgName = "--remote-debugging-port=";
		        var cmdArgs = Environment.GetCommandLineArgs().FirstOrDefault(a => a.StartsWith(cmdArgName));
		        if (cmdArgs != null)
		        {
		            int parsedPort = int.Parse(cmdArgs.Substring(cmdArgName.Length));
		            if (parsedPort >= 1024 && parsedPort <= 65535)
		            {
                        Trace.WriteLine("Setting debugging port to : http://localhost:" + parsedPort);
		                remoteDebuggingPort = parsedPort;
		            }
		        }
		    }
		    catch (Exception e)
		    {
		        Trace.WriteLine("Failed Getting Web-Debugger port from cmd line: " + e.Message);
		        remoteDebuggingPort = 0;
		    }

			var subProcess = string.Format(SubProcessApp, !ProcessorInfo.IsProcess64Bit ? "x86" : "Any");
			var settings = new CefSettings
			{
				BrowserSubprocessPath = Path.Combine(browserRoot, subProcess),
				MultiThreadedMessageLoop = true,
				LogSeverity = CefLogSeverity.Error,
				LogFile = chromiumLog,
				ProductVersion = ProductVersion,
				CachePath = chromiumCache,
                RemoteDebuggingPort = remoteDebuggingPort
			};

			CefRuntime.Initialize(mainArgs, settings, app, IntPtr.Zero);

			Initialized = true;

			Application.ApplicationExit += ApplicationOnApplicationExit;
		}

		private void ApplicationOnApplicationExit(object sender, EventArgs eventArgs)
		{
			if (Initialized)
			{
				Initialized = false;
				CefRuntime.Shutdown();
			}
		}
	}

	internal sealed class Mitchell1CefApp : CefApp
	{
	    private Mitchell1CefBrowserProcessHandler browserProcessHandler = new Mitchell1CefBrowserProcessHandler();
		protected override CefBrowserProcessHandler GetBrowserProcessHandler()
		{
            return browserProcessHandler;
		}
	}

	internal sealed class Mitchell1CefBrowserProcessHandler : CefBrowserProcessHandler
	{
	}
}
