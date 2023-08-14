// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Management;
using System.Drawing;
using System.IO;
using System.Diagnostics;

namespace CPWizard
{
	public class ProcessTools
	{
		static ProcessTools()
		{
		}

		public static bool TryGetCommandLineFromHwnd(IntPtr hWnd, out string cmdLine)
		{
			cmdLine = null;

			try
			{
				int pid = 0;

				Win32.GetWindowThreadProcessId(hWnd, out pid);

				if (pid == 0)
					return false;

				return TryGetCommandLineFromPid(pid, out cmdLine);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("TryGetCommandLineFromHwnd", "ProcessTools", ex.Message, ex.StackTrace);

				return false;
			}
		}

		public static bool TryGetCommandLineFromHwnd(List<WindowInfo> windowInfoList, out string cmdLine)
		{
			cmdLine = null;

			try
			{
				int pid = 0;

				foreach (WindowInfo windowInfo in windowInfoList)
				{
					Win32.GetWindowThreadProcessId(windowInfo.Handle, out pid);

					if (pid == 0)
						return false;

					return TryGetCommandLineFromPid(pid, out cmdLine);
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("TryGetCommandLineFromHwnd", "ProcessTools", ex.Message, ex.StackTrace);

				return false;
			}

			return false;
		}

		public static bool TryGetCommandLineFromPid(int pid, out string cmdLine)
		{
			cmdLine = null;

			try
			{
				SelectQuery selectQuery = new SelectQuery(String.Format("select * from Win32_Process where ProcessId={0}", pid));

				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(selectQuery))
				{
					foreach (ManagementObject managementObject in managementObjectSearcher.Get())
					{
						cmdLine = (string)managementObject.Properties["CommandLine"].Value;

						return true;
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("TryGetCommandLineFromPid", "ProcessTools", ex.Message, ex.StackTrace);

				return false;
			}

			return false;
		}

		public static bool TryGetWindowInfoFromExe(string exe, bool includeChildWindows, out List<WindowInfo> windowInfoList)
		{
			windowInfoList = null;

			try
			{
				int pid = 0;

				if (!TryGetPidFromExe(exe, out pid))
					return false;

				return TryGetWindowInfoFromPid(pid, includeChildWindows, out windowInfoList);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("TryGetHwndFromExe", "ProcessTools", ex.Message, ex.StackTrace);

				return false;
			}
		}

		/* public static bool TryGetWindowInfoFromPid(int pid, bool includeChildWindows, out List<WindowInfo> windowInfoList)
		{
			List<WindowInfo> windowInfoTempList = windowInfoList = new List<WindowInfo>();

			try
			{
				Process process = Process.GetProcessById(pid);

				foreach (ProcessThread processThread in process.Threads)
				{
					Win32.EnumThreadWindows(processThread.Id, (hWnd, lParam) =>
					{
						WindowInfo windowInfo = new WindowInfo(hWnd, false);

						windowInfoTempList.Add(windowInfo);
						
						return true;
					}, IntPtr.Zero);
				}

				if (includeChildWindows)
				{
					for (int i = windowInfoTempList.Count - 1; i >= 0; i--)
					{
						List<WindowInfo> windowInfoChildList = new List<WindowInfo>();

						Win32.EnumChildWindows(windowInfoTempList[i].Handle, (hWnd, lParam) =>
						{
							WindowInfo windowInfo = new WindowInfo(hWnd, true);

							windowInfoChildList.Add(windowInfo);

							return true;

						}, IntPtr.Zero);

						windowInfoTempList.InsertRange(i + 1, windowInfoChildList);
					}
				}
		 
				for (int i = 0; i < windowInfoTempList.Count; i++)
					windowInfoTempList[i].Index = i;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("TryGetWindowInfoFromPid", "ProcessTools", ex.Message, ex.StackTrace);

				return false;
			}

			return (windowInfoList.Count > 0);
		} */

		public static bool TryGetWindowInfoFromPid(int pid, bool includeChildWindows, out List<WindowInfo> windowInfoList)
		{
			List<WindowInfo> windowInfoTempList = windowInfoList = new List<WindowInfo>();

			try
			{
				Win32.EnumWindows((hWnd, lParam) =>
				{
					int pidFound = 0;

					Win32.GetWindowThreadProcessId(hWnd, out pidFound);

					if (pid == pidFound)
					{
						WindowInfo windowInfo = new WindowInfo(hWnd, false);

						windowInfoTempList.Add(windowInfo);
					}

					return true;

				}, IntPtr.Zero);

				if (includeChildWindows)
				{
					for (int i = windowInfoTempList.Count - 1; i >= 0; i--)
					{
						List<WindowInfo> windowInfoChildList = new List<WindowInfo>();

						Win32.EnumChildWindows(windowInfoTempList[i].Handle, (hWnd, lParam) =>
						{
							WindowInfo windowInfo = new WindowInfo(hWnd, true);

							windowInfoChildList.Add(windowInfo);

							return true;

						}, IntPtr.Zero);

						windowInfoTempList.InsertRange(i + 1, windowInfoChildList);
					}
				}

				for (int i = 0; i < windowInfoTempList.Count; i++)
					windowInfoTempList[i].Index = i;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("TryGetWindowInfoFromPid", "ProcessTools", ex.Message, ex.StackTrace);

				return false;
			}

			return (windowInfoList.Count > 0);
		}

		public static bool TryGetTidFromPid(UInt32 pid, out UInt32 tid)
		{
			tid = 0;
			IntPtr hProcessSnap = IntPtr.Zero;
			IntPtr hThreadSnap = IntPtr.Zero;
			Win32.PROCESSENTRY32 pe32 = new Win32.PROCESSENTRY32();

			try
			{
				hProcessSnap = Win32.CreateToolhelp32Snapshot((uint)Win32.SnapshotFlags.Process, 0);
				hThreadSnap = Win32.CreateToolhelp32Snapshot((uint)Win32.SnapshotFlags.Thread, 0);
				pe32.dwSize = (UInt32)Marshal.SizeOf(typeof(Win32.PROCESSENTRY32));

				if (Win32.Process32First(hProcessSnap, ref pe32))
				{
					do
					{
						if (pe32.th32ProcessID == pid)
						{
							Win32.THREADENTRY32 te32 = new Win32.THREADENTRY32();
							te32.dwSize = (UInt32)Marshal.SizeOf(typeof(Win32.THREADENTRY32));

							if (Win32.Thread32First(hThreadSnap, ref te32))
							{
								do
								{
									if (te32.th32OwnerProcessID == pe32.th32ProcessID)
									{
										tid = te32.th32ThreadID;

										break;
									}
								}
								while (Win32.Thread32Next(hThreadSnap, ref te32));
							}
						}
					}
					while (Win32.Process32Next(hProcessSnap, ref pe32));
				}
				else
				{
					LogFile.WriteLine("TryGetTidFromPid", "ProcessTools", string.Format("Failed with win32 error code {0}", Marshal.GetLastWin32Error()));
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("TryGetTidFromPid", "ProcessTools", ex.Message, ex.StackTrace);

				return false;
			}
			finally
			{
				Win32.CloseHandle(hProcessSnap);
				Win32.CloseHandle(hThreadSnap);
			}

			return true;
		}

		public static bool TryGetParentPidFromPid(int pid, out int pidParent)
		{
			pidParent = 0;
			IntPtr hProcessSnap = IntPtr.Zero;
			Win32.PROCESSENTRY32 pe32 = new Win32.PROCESSENTRY32();

			try
			{
				hProcessSnap = Win32.CreateToolhelp32Snapshot((uint)Win32.SnapshotFlags.Process, 0);
				pe32.dwSize = (UInt32)Marshal.SizeOf(typeof(Win32.PROCESSENTRY32));

				if (Win32.Process32First(hProcessSnap, ref pe32))
				{
					do
					{
						if (pe32.th32ProcessID == pid)
						{
							pidParent = (int)pe32.th32ParentProcessID;

							break;
						}
					}
					while (Win32.Process32Next(hProcessSnap, ref pe32));
				}
				else
				{
					LogFile.WriteLine("TryGetParentPidFromPid", "ProcessTools", string.Format("Failed with win32 error code {0}", Marshal.GetLastWin32Error()));
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("TryGetParentPidFromPid", "ProcessTools", ex.Message, ex.StackTrace);

				return false;
			}
			finally
			{
				Win32.CloseHandle(hProcessSnap);
			}

			return true;
		}

		public static bool TryGetPidFromExe(string exe, out int pid)
		{
			pid = 0;

			if (String.IsNullOrEmpty(exe))
				return false;

			IntPtr hProcessSnap = IntPtr.Zero;
			Win32.PROCESSENTRY32 pe32 = new Win32.PROCESSENTRY32();

			try
			{
				hProcessSnap = Win32.CreateToolhelp32Snapshot((uint)Win32.SnapshotFlags.Process, 0);
				pe32.dwSize = (UInt32)Marshal.SizeOf(typeof(Win32.PROCESSENTRY32));

				if (Win32.Process32First(hProcessSnap, ref pe32))
				{
					do
					{
						if (String.Equals(exe, pe32.szExeFile, StringComparison.CurrentCultureIgnoreCase))
						{
							pid = (int)pe32.th32ProcessID;

							break;
						}
					}
					while (Win32.Process32Next(hProcessSnap, ref pe32));
				}
				else
				{
					LogFile.WriteLine("TryGetPidFromExe", "ProcessTools", string.Format("Failed with win32 error code {0}", Marshal.GetLastWin32Error()));
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("TryGetPidFromExe", "ProcessTools", ex.Message, ex.StackTrace);

				return false;
			}
			finally
			{
				Win32.CloseHandle(hProcessSnap);
			}

			return true;
		}

		public static bool TryGetExeFileFromPid(int pid, out string exeFile)
		{
			exeFile = null;
			IntPtr hProcessSnap = IntPtr.Zero;
			Win32.PROCESSENTRY32 pe32 = new Win32.PROCESSENTRY32();

			try
			{
				hProcessSnap = Win32.CreateToolhelp32Snapshot((uint)Win32.SnapshotFlags.Process, 0);
				pe32.dwSize = (UInt32)Marshal.SizeOf(typeof(Win32.PROCESSENTRY32));

				if (Win32.Process32First(hProcessSnap, ref pe32))
				{
					do
					{
						if (pe32.th32ProcessID == pid)
						{
							exeFile = pe32.szExeFile;

							break;
						}
					}
					while (Win32.Process32Next(hProcessSnap, ref pe32));
				}
				else
				{
					LogFile.WriteLine("TryGetExeFileFromPid", "ProcessTools", string.Format("Failed with win32 error code {0}", Marshal.GetLastWin32Error()));
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("TryGetExeFileFromPid", "ProcessTools", ex.Message, ex.StackTrace);

				return false;
			}
			finally
			{
				Win32.CloseHandle(hProcessSnap);
			}

			return true;
		}

		public static bool TryGetWindowTitle(IntPtr hWnd, out string windowTitle)
		{
			windowTitle = null;

			StringBuilder stringBuilder = new StringBuilder(1024);
			Win32.GetWindowText(hWnd, stringBuilder, 1023);

			windowTitle = stringBuilder.ToString();

			return true;
		}

		public static bool TryFindWindows(string windowTitle, string windowClass, bool includeChildWindows, out List<WindowInfo> windowInfoList)
		{
			List<WindowInfo> windowInfoTempList = windowInfoList = new List<WindowInfo>();

			if (String.IsNullOrEmpty(windowTitle) && String.IsNullOrEmpty(windowClass))
				return false;

			try
			{
				Win32.EnumWindows((hWnd, lParam) =>
				{
					if (IsWindowMatch(hWnd, windowTitle, windowClass))
					{
						WindowInfo windowInfo = new WindowInfo(hWnd, false);

						windowInfoTempList.Add(windowInfo);
					}

					return true;

				}, IntPtr.Zero);

				if (includeChildWindows)
				{
					for (int i = windowInfoTempList.Count - 1; i >= 0; i--)
					{
						List<WindowInfo> windowInfoChildList = new List<WindowInfo>();

						Win32.EnumChildWindows(windowInfoTempList[i].Handle, (hWnd, lParam) =>
						{
							if (IsWindowMatch(hWnd, windowTitle, windowClass))
							{
								WindowInfo windowInfo = new WindowInfo(hWnd, true);

								windowInfoChildList.Add(windowInfo);
							}

							return true;

						}, IntPtr.Zero);

						windowInfoTempList.InsertRange(i + 1, windowInfoChildList);
					}
				}

				for (int i = 0; i < windowInfoTempList.Count; i++)
					windowInfoTempList[i].Index = i;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("TryFindWindows", "ProcessTools", ex.Message, ex.StackTrace);

				return false;
			}

			return (windowInfoList.Count > 0);
		}

		public static bool TryFindWindows(int pid, string windowTitle, string windowClass, bool includeChildWindows, out List<WindowInfo> windowInfoList)
		{
			List<WindowInfo> windowInfoTempList = windowInfoList = new List<WindowInfo>();

			if (String.IsNullOrEmpty(windowTitle) && String.IsNullOrEmpty(windowClass))
				return false;

			try
			{
				Process process = Process.GetProcessById(pid);

				foreach (ProcessThread processThread in process.Threads)
				{
					Win32.EnumThreadWindows(processThread.Id, (hWnd, lParam) =>
					{
						if (IsWindowMatch(hWnd, windowTitle, windowClass))
						{
							WindowInfo windowInfo = new WindowInfo(hWnd, false);

							windowInfoTempList.Add(windowInfo);
						}

						return true;

					}, IntPtr.Zero);
				}

				if (includeChildWindows)
				{
					for (int i = windowInfoTempList.Count - 1; i >= 0; i--)
					{
						List<WindowInfo> windowInfoChildList = new List<WindowInfo>();

						Win32.EnumChildWindows(windowInfoTempList[i].Handle, (hWnd, lParam) =>
						{
							if (IsWindowMatch(hWnd, windowTitle, windowClass))
							{
								WindowInfo windowInfo = new WindowInfo(hWnd, true);

								windowInfoChildList.Add(windowInfo);
							}

							return true;

						}, IntPtr.Zero);

						windowInfoTempList.InsertRange(i + 1, windowInfoChildList);
					}
				}

				for (int i = 0; i < windowInfoTempList.Count; i++)
					windowInfoTempList[i].Index = i;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("TryFindWindows", "ProcessTools", ex.Message, ex.StackTrace);

				return false;
			}

			return (windowInfoList.Count > 0);
		}

		public static bool IsWindowMatch(IntPtr hWnd, string windowTitle, string windowClass)
		{
			StringBuilder windowTextStringBuilder = new StringBuilder(1024);
			StringBuilder classNameStringBuilder = new StringBuilder(1024);

			Win32.GetWindowText(hWnd, windowTextStringBuilder, windowTextStringBuilder.Capacity);
			Win32.GetClassName(hWnd, classNameStringBuilder, classNameStringBuilder.Capacity);

			if (!String.IsNullOrEmpty(windowTitle) && !String.IsNullOrEmpty(windowClass))
				return (windowTextStringBuilder.ToString().Contains(windowTitle) && classNameStringBuilder.ToString().Contains(windowClass));
			else if (!String.IsNullOrEmpty(windowTitle))
				return (windowTextStringBuilder.ToString().Contains(windowTitle));
			else if (!String.IsNullOrEmpty(windowClass))
				return (classNameStringBuilder.ToString().Contains(windowClass));

			return false;
		}

		public static void PressMouse(int x, int y)
		{
			Win32.mouse_event((int)Win32.MouseEventFlags.LEFTDOWN | (int)Win32.MouseEventFlags.LEFTUP, x, y, 0, 0);
		}

		public static bool AppActivate(string windowTitle)
		{
			bool retVal = false;

			retVal = TryAppActivate(windowTitle, null);

			if (!retVal)
				retVal = TryAppActivate(null, windowTitle);

			return retVal;
		}

		public static bool TryAppActivate(string windowTitle, string windowClass)
		{
			List<WindowInfo> windowInfoList = null;

			if (!TryFindWindows(windowTitle, windowClass, false, out windowInfoList))
				return false;

			return TryAppActivate(windowInfoList);
		}

		public static bool TryAppActivate(IntPtr hWnd)
		{
			if (hWnd == IntPtr.Zero)
				return false;

			Win32.SendMessage(hWnd, Win32.WM_SYSCOMMAND, (IntPtr)Win32.SC_HOTKEY, hWnd);
			Win32.SendMessage(hWnd, Win32.WM_SYSCOMMAND, (IntPtr)Win32.SC_RESTORE, hWnd);

			Win32.ShowWindow(hWnd, Win32.SW_SHOW);
			Win32.SetForegroundWindow(hWnd);
			Win32.SetFocus(hWnd);
			Win32.SetActiveWindow(hWnd);
			//Win32.WaitForInputIdle();

			return (hWnd == Win32.GetForegroundWindow());
		}

		public static bool TryAppActivate(List<WindowInfo> windowInfoList)
		{
			foreach (WindowInfo windowInfo in windowInfoList)
				TryAppActivate(windowInfo.Handle);

			return true;
		}

		public static bool IsProcessRunning(string exe)
		{
			if (String.IsNullOrEmpty(exe))
				return false;

			int pid = 0;

			if (!TryGetPidFromExe(exe, out pid))
				return false;

			return IsProcessRunning(pid);
		}

		public static bool IsProcessRunning(int pid)
		{
			if (pid == 0)
				return false;

			IntPtr process = Win32.OpenProcess(Win32.ProcessAccessFlags.Synchronize, false, pid);
			int ret = Win32.WaitForSingleObject(process, 0);
			Win32.CloseHandle(process);

			return (ret == Win32.WAIT_TIMEOUT);
		}

		public static bool TryCloseProcessWindows(string exe)
		{
			if (String.IsNullOrEmpty(exe))
				return false;

			List<WindowInfo> windowInfoList = null;

			if (!TryGetWindowInfoFromExe(exe, false, out windowInfoList))
				return false;

			foreach (WindowInfo windowInfo in windowInfoList)
				Win32.PostMessage(windowInfo.Handle, Win32.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);

			return true;
		}

		public static bool TryCloseProcess(string fileName)
		{
			if (String.IsNullOrEmpty(fileName))
				return false;

			string processName = Path.GetFileNameWithoutExtension(fileName);

			foreach (Process process in Process.GetProcessesByName(processName))
				process.Close();

			return true;
		}

		public static bool TryKillProcess(string fileName)
		{
			if (String.IsNullOrEmpty(fileName))
				return false;

			string processName = Path.GetFileNameWithoutExtension(fileName);

			foreach (Process process in Process.GetProcessesByName(processName))
				process.Kill();

			return true;
		}
	}

	public class WindowInfo : IComparable<WindowInfo>
	{
		public int Index = 0;
		public IntPtr Handle = IntPtr.Zero;
		public bool IsChildWindow = false;
		public bool IsWindowMatch = false;
		public bool IsClassMatch = false;

		public WindowInfo(IntPtr hWnd, bool isChildWindow)
		{
			Handle = hWnd;
			IsChildWindow = isChildWindow;
		}

		public int CompareTo(WindowInfo other)
		{
			if (this.IsWindowMatch != other.IsWindowMatch)
				return -this.IsWindowMatch.CompareTo(other.IsWindowMatch);

			if (this.IsClassMatch != other.IsClassMatch)
				return -this.IsClassMatch.CompareTo(other.IsClassMatch);

			if (this.IsWindowOwner != other.IsWindowOwner)
				return -this.IsWindowOwner.CompareTo(other.IsWindowOwner);

			if (this.IsForegroundWindow != other.IsForegroundWindow)
				return -this.IsForegroundWindow.CompareTo(other.IsForegroundWindow);

			if (this.IsWindowVisible != other.IsWindowVisible)
				return -this.IsWindowVisible.CompareTo(other.IsWindowVisible);

			return this.Index.CompareTo(other.Index);
		}

		public string WindowText
		{
			get
			{
				StringBuilder windowTextStringBuilder = new StringBuilder(1024);

				Win32.GetWindowText(Handle, windowTextStringBuilder, windowTextStringBuilder.Capacity);

				return windowTextStringBuilder.ToString();
			}
		}

		public string ClassName
		{
			get
			{
				StringBuilder classNameStringBuilder = new StringBuilder(1024);

				Win32.GetClassName(Handle, classNameStringBuilder, classNameStringBuilder.Capacity);

				return classNameStringBuilder.ToString();
			}
		}

		public bool IsMainWindow
		{
			get { return WindowTools.IsMainWindow(Handle); }
		}

		public bool IsWindowOwner
		{
			get { return WindowTools.IsWindowOwner(Handle); }
		}

		public bool IsForegroundWindow
		{
			get { return WindowTools.IsForegroundWindow(Handle); }
		}

		public bool IsWindowVisible
		{
			get { return Win32.IsWindowVisible(Handle); }
		}

		public override string ToString()
		{
			return String.Format("hWnd: 0x{0:x8} Title: '{1}' Class: '{2}' IsChildWindow: {3} IsMainWindow: {4} IsWindowOwner: {5} IsWindowVisible: {6} IsForegroundWindow: {7}", Handle, WindowText, ClassName, IsChildWindow, IsMainWindow, IsWindowOwner, IsWindowVisible, IsForegroundWindow);
		}
	}

	partial class Win32
	{
		//inner enum used only internally
		[Flags]
		public enum SnapshotFlags : uint
		{
			HeapList = 0x00000001,
			Process = 0x00000002,
			Thread = 0x00000004,
			Module = 0x00000008,
			Module32 = 0x00000010,
			Inherit = 0x80000000,
			All = 0x0000001F
		}

		[Flags]
		public enum ProcessAccessFlags : uint
		{
			All = 0x001F0FFF,
			Terminate = 0x00000001,
			CreateThread = 0x00000002,
			VirtualMemoryOperation = 0x00000008,
			VirtualMemoryRead = 0x00000010,
			VirtualMemoryWrite = 0x00000020,
			DuplicateHandle = 0x00000040,
			CreateProcess = 0x000000080,
			SetQuota = 0x00000100,
			SetInformation = 0x00000200,
			QueryInformation = 0x00000400,
			QueryLimitedInformation = 0x00001000,
			Synchronize = 0x00100000
		}

		//inner struct used only internally
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct PROCESSENTRY32
		{
			const int MAX_PATH = 260;
			internal UInt32 dwSize;
			internal UInt32 cntUsage;
			internal UInt32 th32ProcessID;
			internal IntPtr th32DefaultHeapID;
			internal UInt32 th32ModuleID;
			internal UInt32 cntThreads;
			internal UInt32 th32ParentProcessID;
			internal Int32 pcPriClassBase;
			internal UInt32 dwFlags;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
			internal string szExeFile;
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct THREADENTRY32
		{
			internal UInt32 dwSize;
			internal UInt32 cntUsage;
			internal UInt32 th32ThreadID;
			internal UInt32 th32OwnerProcessID;
			internal Int32 tpBasePri;
			internal Int32 tpDeltaPri;
			internal UInt32 dwFlags;
		}

		//[DllImport("user32.dll")]
		//public static extern short GetKeyState(Keys nVirtKey);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr FindWindow(string lpClassName, IntPtr lpWindowName);

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		public static extern int GetWindowTextLength(IntPtr hWnd);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern void GetWindowText(IntPtr hWnd, StringBuilder lpWindowText, int nMaxCount);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

		public const int SC_RESTORE = 0xf120;
		public const int SC_HOTKEY = 0xf150;
		public const int SW_SHOW = 5;

		public const int SWP_NOSIZE = 0x0001;
		public const int SWP_NOMOVE = 0x0002;
		public const int SWP_NOZORDER = 0x0004;
		public const int SWP_NOREDRAW = 0x0008;
		public const int SWP_NOACTIVATE = 0x0010;
		public const int SWP_FRAMECHANGED = 0x0020;
		public const int SWP_SHOWWINDOW = 0x0040;
		public const int SWP_HIDEWINDOW = 0x0080;
		public const int SWP_NOCOPYBITS = 0x0100;
		public const int SWP_NOOWNERZORDER = 0x0200;
		public const int SWP_NOSENDCHANGING = 0x0400;

		//public const uint WM_SYSCOMMAND = 0x0112;

		//public const Int32 WAIT_TIMEOUT = 0x102;

		public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
		public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
		public static readonly IntPtr HWND_TOP = new IntPtr(0);
		public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

		public const int WAIT_TIMEOUT = 0x102;

		[DllImport("user32.dll")]
		public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern int SendMessage(IntPtr hWnd, int messageID, int wParam, IntPtr lParam);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern int SendMessage(IntPtr hWnd, int messageID, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SetForegroundWindow(IntPtr hWnd);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		[DllImport("user32.dll")]
		public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

		[DllImport("user32.dll")]
		public static extern IntPtr SetFocus(IntPtr hWnd);

		[DllImport("user32.dll")]
		public static extern IntPtr SetActiveWindow(IntPtr hWnd);

		[DllImport("user32.dll")]
		public static extern IntPtr GetActiveWindow();

		[DllImport("kernel32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
		public static extern IntPtr CreateToolhelp32Snapshot([In]UInt32 dwFlags, [In]UInt32 th32ProcessID);

		[DllImport("kernel32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
		public static extern bool Process32First([In]IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

		[DllImport("kernel32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
		public static extern bool Process32Next([In]IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

		[DllImport("user32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
		public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

		[DllImport("kernel32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
		public static extern bool Thread32First([In]IntPtr hSnapshot, ref THREADENTRY32 lpte);

		[DllImport("kernel32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
		public static extern bool Thread32Next([In]IntPtr hSnapshot, ref THREADENTRY32 lpte);

		[DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
		public static extern IntPtr GetParent(IntPtr hWnd);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, int processId);

		public delegate bool EnumWindowsCallback(IntPtr hWnd, IntPtr lParam);

		[DllImport("user32.dll")]
		public static extern int EnumWindows(EnumWindowsCallback callPtr, IntPtr lParam);

		[DllImport("user32.dll")]
		public static extern bool EnumThreadWindows(int dwThreadId, EnumWindowsCallback callPtr, IntPtr lParam);

		[DllImport("user32.dll")]
		public static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsCallback callPtr, IntPtr lParam);

		[DllImport("user32.dll")]
		public static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);

		//[DllImport("kernel32", SetLastError = true, ExactSpelling = true)]
		//public static extern Int32 WaitForSingleObject(IntPtr handle, uint milliseconds);

		//[DllImport("kernel32")]
		//public static extern bool CloseHandle(IntPtr hObject);
	}
}
