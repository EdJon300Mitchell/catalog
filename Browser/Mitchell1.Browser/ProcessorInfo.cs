using System;
using System.Runtime.InteropServices;

namespace Mitchell1.Browser
{
	internal static class ProcessorInfo
	{
		private static readonly bool is64BitOperatingSystem = Is64BitOperatingSystem();
		private static readonly bool is64BitProcess = (IntPtr.Size == 8);

		public static bool IsOs64Bit { get { return is64BitOperatingSystem; } }

		public static bool IsProcess64Bit { get { return is64BitProcess; } }

		[DllImport("kernel32.dll")]
		static extern IntPtr GetCurrentProcess();

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		static extern IntPtr GetModuleHandle(string moduleName);

		[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
		static extern IntPtr GetProcAddress(IntPtr hModule, [MarshalAs(UnmanagedType.LPStr)]string procName);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool IsWow64Process(IntPtr hProcess, out bool wow64Process);

		private static bool Is64BitOperatingSystem()
		{
			if (IntPtr.Size == 8)
			{
				return true;
			}

			bool flag;
			return ((DoesWin32MethodExist("kernel32.dll", "IsWow64Process") &&
				IsWow64Process(GetCurrentProcess(), out flag)) && flag);
		}

		static bool DoesWin32MethodExist(string moduleName, string methodName)
		{
			IntPtr moduleHandle = GetModuleHandle(moduleName);
			if (moduleHandle == IntPtr.Zero)
			{
				return false;
			}
			return (GetProcAddress(moduleHandle, methodName) != IntPtr.Zero);
		}
	}
}
