using System;

namespace Mitchell1.BrowserProcess32
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static int Main(string[] args)
		{
			return Mitchell1.BrowserProcessAny.Program.Main(args);
		}
	}
}