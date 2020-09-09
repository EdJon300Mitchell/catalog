using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Mitchell1.Browser;
using Xilium.CefGlue;

namespace Mitchell1.BrowserProcessAny
{
	public static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		public static int Main(string[] args)
		{
			try
			{
				string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
				if (path != null)
				{
					path = Path.Combine(path, !ProcessorInfo.IsProcess64Bit ? "x86" : "64");
				}
				else
				{
					throw new Exception("Could not load assembly Browser Process");
				}

				CefRuntime.Load(path);
			}
			catch (DllNotFoundException ex)
			{
				MessageBox.Show(ex.Message, @"Error Loading Browser", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return 1;
			}
			catch (CefRuntimeException ex)
			{
				MessageBox.Show(ex.Message, @"Error Loading Browser", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return 2;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), @"Error Loading Browser", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return 3;
			}

			var mainArgs = new CefMainArgs(args);
			var app = new DemoApp();

			var exitCode = CefRuntime.ExecuteProcess(mainArgs, app, IntPtr.Zero);
			if (exitCode != -1)
				return exitCode;

			// We don't have any of our own UI. Are only purpose in life is to create a Cef Subprocess when requested.

			CefRuntime.Shutdown();
			return 0;
		}
	}
}
