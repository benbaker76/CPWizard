using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace CPWizard
{
	public class WindowTools
	{
		public static void RemoveWindowMenu(IntPtr hWnd)
		{
			//IntPtr hMenu = Win32.GetMenu(hWnd);
			Win32.SetMenu(hWnd, IntPtr.Zero);
		}

		public static void ForceWindowFullScreen(IntPtr hWnd)
		{
			Size screenSize = System.Windows.Forms.Screen.AllScreens[0].Bounds.Size;

			Win32.SetWindowPos(hWnd, Win32.HWND_TOPMOST, 0, 0, screenSize.Width, screenSize.Height, Win32.SWP_SHOWWINDOW | Win32.SWP_NOZORDER);
			Win32.UpdateWindow(hWnd);
		}

		public static void SetWindowTopMost(IntPtr hWnd)
		{
			//int dwExStyle = Win32.GetWindowLong(hWnd, Win32.GWL_EXSTYLE);
			//Win32.SetWindowLong(hWnd, Win32.GWL_EXSTYLE, dwExStyle | Win32.WS_EX_TOPMOST);
			Win32.SetWindowPos(hWnd, Win32.HWND_TOPMOST, 0, 0, 0, 0, Win32.SWP_NOMOVE | Win32.SWP_NOSIZE | Win32.SWP_NOACTIVATE);
		}

		public static void RemoveWindowTitlebar(IntPtr hWnd, bool restoreBorder)
		{
			IntPtr dwStyle = Win32.GetWindowLongPtr(hWnd, Win32.GWL_STYLE);
			IntPtr dwTitleBarStyle = new IntPtr(Win32.WS_CAPTION | Win32.WS_SYSMENU | Win32.WS_THICKFRAME | Win32.WS_MINIMIZE | Win32.WS_MAXIMIZEBOX);

			dwStyle = new IntPtr(dwStyle.ToInt64() & ~(dwTitleBarStyle.ToInt64()));

			Win32.SetWindowLongPtr(hWnd, Win32.GWL_STYLE, dwStyle);

			if (restoreBorder)
			{
				dwStyle = Win32.GetWindowLongPtr(hWnd, Win32.GWL_EXSTYLE);
				Win32.SetWindowLongPtr(hWnd, Win32.GWL_EXSTYLE, new IntPtr(dwStyle.ToInt64() | Win32.WS_EX_DLGMODALFRAME));
			}

			Win32.SetWindowPos(hWnd, IntPtr.Zero, 0, 0, 0, 0, Win32.SWP_NOMOVE | Win32.SWP_NOSIZE | Win32.SWP_FRAMECHANGED);
		}

		public static void DestroyChildWindows(IntPtr hWndParent)
		{
			Win32.EnumChildWindows(hWndParent, (hWnd, lParam) =>
			{
				Win32.SendMessage(hWnd, Win32.WM_DESTROY, IntPtr.Zero, IntPtr.Zero);

				return true;

			}, IntPtr.Zero);
		}

		public static void SendWindowMouseClick(IntPtr hWnd)
		{
			Win32.RECT rect = new Win32.RECT();
			Win32.GetWindowRect(hWnd, out rect);

			SendMouse.DoMouseClick(rect.Left, rect.Top);
		}

		public static void BringWindowTopTop(IntPtr hWnd)
		{
			Win32.BringWindowToTop(hWnd);
		}

		public static void SetForegroundWindow(IntPtr hWnd)
		{
			Win32.SetForegroundWindow(hWnd);
		}

		public static void SetWindowFocus(IntPtr hWnd)
		{
			Win32.SetActiveWindow(hWnd);
			Win32.SetFocus(hWnd);
		}

		public static bool IsForegroundWindow(IntPtr hWnd)
		{
			return (Win32.GetForegroundWindow() == hWnd);
		}

		// Retrieves the window handle to the active window associated with the thread that calls the function
		public static bool IsActiveWindow(IntPtr hWnd)
		{
			return (Win32.GetActiveWindow() == hWnd);
		}

		// If the calling thread's message queue does not have an associated window with the keyboard focus, the return value is NULL
		public static bool HasFocus(IntPtr hWnd)
		{
			return (Win32.GetFocus() == hWnd);
		}

		public static bool IsWindowOwner(IntPtr hWnd)
		{
			return Win32.GetWindow(hWnd, Win32.GW_OWNER) == (IntPtr)0;
		}

		public static bool IsMainWindow(IntPtr hWnd)
		{
			if (!IsWindowOwner(hWnd) || !Win32.IsWindowVisible(hWnd))
				return false;

			return true;
		}

		public static bool CloseWindow(IntPtr hWnd)
		{
			if (hWnd == IntPtr.Zero)
				return false;

			//if ((Win32.GetWindowLongPtr(hWnd, -16).ToInt64() & 0x8000000) != 0)
			//	return false;

			Win32.PostMessage(hWnd, Win32.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
			//Win32.SendMessage(hWnd, Win32.WM_SYSCOMMAND, (IntPtr)Win32.SC_CLOSE, IntPtr.Zero);

			return true;
		}

		public static bool TryGetWindowInfo(IntPtr hWnd, out string windowTitle, out string windowClass)
		{
			StringBuilder stringBuilder = new StringBuilder(1024);

			Win32.GetWindowText(hWnd, stringBuilder, stringBuilder.Capacity);
			windowTitle = stringBuilder.ToString();

			Win32.GetClassName(hWnd, stringBuilder, stringBuilder.Capacity);
			windowClass = stringBuilder.ToString();

			return true;
		}

		public static bool TryGetWindowInfo(IntPtr hWnd, out string windowTitle, out string windowClass, out string exeFile, out int pid)
		{
			StringBuilder stringBuilder = new StringBuilder(1024);

			Win32.GetWindowText(hWnd, stringBuilder, stringBuilder.Capacity);
			windowTitle = stringBuilder.ToString();

			Win32.GetClassName(hWnd, stringBuilder, stringBuilder.Capacity);
			windowClass = stringBuilder.ToString();

			Win32.GetWindowThreadProcessId(hWnd, out pid);

			ProcessTools.TryGetExeFileFromPid(pid, out exeFile);

			return true;
		}

		public static bool ForceForegroundWindow(IntPtr hWnd)
		{
			bool ret = false;

			try
			{
				IntPtr hWndForeground = Win32.GetForegroundWindow();

				if (hWnd == hWndForeground)
				{
					LogFile.WriteLine("ForceForegroundWindow", "HideDesktop", "Already Foreground hWnd: " + String.Format("0x{0:x8}", hWndForeground));

					return true;
				}

				int foregroundThreadID = Win32.GetWindowThreadProcessId(hWndForeground, 0);
				int hWndThreadID = Win32.GetWindowThreadProcessId(hWnd, 0);

				if (Win32.AttachThreadInput(foregroundThreadID, hWndThreadID, 1))
				{
					ret = Win32.BringWindowToTop(hWnd);
					ret = Win32.SetForegroundWindow(hWnd);
					Win32.AttachThreadInput(foregroundThreadID, hWndThreadID, 0);
					Win32.InvalidateRect(hWnd, IntPtr.Zero, true);
					ret = (Win32.GetForegroundWindow() == hWnd);

					if (!ret)
					{
						int timeout = 0; int dummy = 0;
						Win32.SystemParametersInfo(Win32.SPI_GETFOREGROUNDLOCKTIMEOUT, 0, ref timeout, 0);
						Win32.SystemParametersInfo(Win32.SPI_SETFOREGROUNDLOCKTIMEOUT, 0, ref dummy, Win32.SPIF_SENDCHANGE);
						Win32.BringWindowToTop(hWnd); // IE 5.5 related hack
						Win32.SetForegroundWindow(hWnd);
						Win32.SystemParametersInfo(Win32.SPI_SETFOREGROUNDLOCKTIMEOUT, 0, ref timeout, Win32.SPIF_SENDCHANGE);
					}
				}
				else
				{
					ret = Win32.BringWindowToTop(hWnd);
					ret = Win32.SetForegroundWindow(hWnd);
					Win32.InvalidateRect(hWnd, IntPtr.Zero, true);
				}

				if (Win32.IsIconic(hWnd))
					Win32.ShowWindow(hWnd, (int)Win32.WindowShowStyle.Restore);
				else
					Win32.ShowWindow(hWnd, (int)Win32.WindowShowStyle.Show);

				ret = (Win32.GetForegroundWindow() == hWnd);

				if (!ret)
					Win32.SwitchToThisWindow(hWnd, true);

				hWndForeground = Win32.GetForegroundWindow();
				LogFile.WriteLine("ForceForegroundWindow", "HideDesktop", "Foreground hWnd: " + String.Format("0x{0:x8}", hWndForeground));
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ForceForegroundWindow", "HideDesktop", ex.Message, ex.StackTrace);
			}

			return ret;
		}

		public static void AllowForceForegroundWindow()
		{
			try
			{
				Win32.AllowSetForegroundWindow(System.Diagnostics.Process.GetCurrentProcess().Id);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("AllowForceForegroundWindow", "HideDesktop", ex.Message, ex.StackTrace);
			}
		}

		public static void ForceForegroundWindow(System.Windows.Forms.Form form)
		{
			try
			{
				if (form == null)
					return;

				bool TopMost = form.TopMost;

				form.TopMost = true;
				form.Focus();

				ForceForegroundWindow(form.Handle);

				if (TopMost == false)
					form.TopMost = false;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ForceForegroundWindow", "HideDesktop", ex.Message, ex.StackTrace);
			}
		}

		public static IntPtr FindChildWindow(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszTitle)
		{
			IntPtr hwnd = Win32.FindWindowEx(hwndParent, IntPtr.Zero, lpszClass, lpszTitle);

			if (hwnd == IntPtr.Zero)
			{
				IntPtr hwndChild = Win32.FindWindowEx(hwndParent, IntPtr.Zero, null, null);

				while (hwndChild != IntPtr.Zero && hwnd == IntPtr.Zero)
				{
					hwnd = FindChildWindow(hwndChild, IntPtr.Zero, lpszClass, lpszTitle);

					if (hwnd == IntPtr.Zero)
						hwndChild = Win32.FindWindowEx(hwndParent, hwndChild, null, null);
				}
			}

			return hwnd;
		}

		public static void ReadFormLocationIni(string fileName, Form form, bool readWindowState)
		{
			using (IniFile iniFile = new IniFile(fileName))
				ReadFormLocationIni(iniFile, form, readWindowState);
		}

		public static void WriteFormLocationIni(string fileName, Form form)
		{
			using (IniFile iniFile = new IniFile(fileName))
				WriteFormLocationIni(iniFile, form);
		}

		public static void ReadFormLocationIni(IniFile iniFile, Form form, bool readWindowState)
		{
			form.Size = iniFile.Read<Size>(form.Name, "Size", form.Size);
			form.Location = iniFile.Read<Point>(form.Name, "Location", form.Location);

			if (readWindowState)
				form.WindowState = iniFile.Read<System.Windows.Forms.FormWindowState>(form.Name, "WindowState", form.WindowState);
		}

		public static void WriteFormLocationIni(IniFile iniFile, Form form)
		{
			iniFile.Write<System.Windows.Forms.FormWindowState>(form.Name, "WindowState", form.WindowState);

			if (form.WindowState == System.Windows.Forms.FormWindowState.Normal)
			{
				iniFile.Write<Size>(form.Name, "Size", form.Size);
				iniFile.Write<Point>(form.Name, "Location", form.Location);
			}
			else
			{
				iniFile.Write<Size>(form.Name, "Size", form.RestoreBounds.Size);
				iniFile.Write<Point>(form.Name, "Location", form.RestoreBounds.Location);
			}
		}
	}

	public partial class Win32
	{
		[Flags]
		public enum WindowShowStyle : int
		{
			Hide = 0,
			ShowNormal = 1,
			ShowMinimized = 2,
			ShowMaximized = 3,
			Maximize = 3,
			ShowNormalNoActivate = 4,
			Show = 5,
			Minimize = 6,
			ShowMinNoActivate = 7,
			ShowNoActivate = 8,
			Restore = 9,
			ShowDefault = 10,
			ForceMinimized = 11
		}

		public const int GWL_HWNDPARENT = (-8);
		public const int GWL_ID = (-12);
		public const int GWL_STYLE = (-16);
		public const int GWL_EXSTYLE = (-20);

		// Window Styles
		public const UInt32 WS_OVERLAPPED = 0;
		public const UInt32 WS_POPUP = 0x80000000;
		public const UInt32 WS_CHILD = 0x40000000;
		public const UInt32 WS_MINIMIZE = 0x20000000;
		public const UInt32 WS_VISIBLE = 0x10000000;
		public const UInt32 WS_DISABLED = 0x8000000;
		public const UInt32 WS_CLIPSIBLINGS = 0x4000000;
		public const UInt32 WS_CLIPCHILDREN = 0x2000000;
		public const UInt32 WS_MAXIMIZE = 0x1000000;
		public const UInt32 WS_CAPTION = 0xC00000;      // WS_BORDER or WS_DLGFRAME  
		public const UInt32 WS_BORDER = 0x800000;
		public const UInt32 WS_DLGFRAME = 0x400000;
		public const UInt32 WS_VSCROLL = 0x200000;
		public const UInt32 WS_HSCROLL = 0x100000;
		public const UInt32 WS_SYSMENU = 0x80000;
		public const UInt32 WS_THICKFRAME = 0x40000;
		public const UInt32 WS_GROUP = 0x20000;
		public const UInt32 WS_TABSTOP = 0x10000;
		public const UInt32 WS_MINIMIZEBOX = 0x20000;
		public const UInt32 WS_MAXIMIZEBOX = 0x10000;
		public const UInt32 WS_TILED = WS_OVERLAPPED;
		public const UInt32 WS_ICONIC = WS_MINIMIZE;
		public const UInt32 WS_SIZEBOX = WS_THICKFRAME;

		// Extended Window Styles
		public const UInt32 WS_EX_DLGMODALFRAME = 0x0001;
		public const UInt32 WS_EX_NOPARENTNOTIFY = 0x0004;
		public const UInt32 WS_EX_TOPMOST = 0x0008;
		public const UInt32 WS_EX_ACCEPTFILES = 0x0010;
		public const UInt32 WS_EX_TRANSPARENT = 0x0020;
		public const UInt32 WS_EX_MDICHILD = 0x0040;
		public const UInt32 WS_EX_TOOLWINDOW = 0x0080;
		public const UInt32 WS_EX_WINDOWEDGE = 0x0100;
		public const UInt32 WS_EX_CLIENTEDGE = 0x0200;
		public const UInt32 WS_EX_CONTEXTHELP = 0x0400;
		public const UInt32 WS_EX_RIGHT = 0x1000;
		public const UInt32 WS_EX_LEFT = 0x0000;
		public const UInt32 WS_EX_RTLREADING = 0x2000;
		public const UInt32 WS_EX_LTRREADING = 0x0000;
		public const UInt32 WS_EX_LEFTSCROLLBAR = 0x4000;
		public const UInt32 WS_EX_RIGHTSCROLLBAR = 0x0000;
		public const UInt32 WS_EX_CONTROLPARENT = 0x10000;
		public const UInt32 WS_EX_STATICEDGE = 0x20000;
		public const UInt32 WS_EX_APPWINDOW = 0x40000;
		public const UInt32 WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE);
		public const UInt32 WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST);
		public const UInt32 WS_EX_LAYERED = 0x00080000;
		public const UInt32 WS_EX_NOINHERITLAYOUT = 0x00100000; // Disable inheritence of mirroring by children
		public const UInt32 WS_EX_LAYOUTRTL = 0x00400000; // Right to left mirroring
		public const UInt32 WS_EX_COMPOSITED = 0x02000000;
		public const UInt32 WS_EX_NOACTIVATE = 0x08000000;

		public const UInt32 GW_OWNER = 4;

		public const UInt32 SC_CLOSE = 0xF060;

		/* [DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32.dll")]
		public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		[DllImport("user32.dll")]
		public static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SetForegroundWindow(IntPtr hWnd); */

		[DllImport("user32.dll")]
		public static extern bool AllowSetForegroundWindow(int dwProcessId);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern int GetWindowThreadProcessId(IntPtr hWnd, int lpdwProcessId);

		[DllImport("user32.dll")]
		public static extern bool AttachThreadInput(int idAttach, int idAttachTo, int fAttach);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool BringWindowToTop(IntPtr hWnd);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

		[DllImport("user32.dll")]
		public static extern bool UpdateWindow(IntPtr hWnd);

		[DllImport("user32.dll")]
		public static extern IntPtr GetMenu(IntPtr hWnd);

		[DllImport("user32.dll")]
		public static extern bool SetMenu(IntPtr hWnd, IntPtr hMenu);

		[DllImport("user32.dll", EntryPoint = "SetWindowLong")]
		private static extern int SetWindowLong32(IntPtr hWnd, int nIndex, int dwNewLong);

		[DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
		private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

		public static IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
		{
			if (IntPtr.Size == 8)
				return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
			else
				return new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
		}

		[DllImport("user32.dll", EntryPoint = "GetWindowLong")]
		private static extern IntPtr GetWindowLongPtr32(IntPtr hWnd, int nIndex);

		[DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
		private static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);

		// This static method is required because Win32 does not support
		// GetWindowLongPtr directly
		public static IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex)
		{
			if (IntPtr.Size == 8)
				return GetWindowLongPtr64(hWnd, nIndex);
			else
				return GetWindowLongPtr32(hWnd, nIndex);
		}

		//[DllImport("user32.dll", SetLastError = true)]
		//public static extern IntPtr SetActiveWindow(IntPtr hWnd);

		//[DllImport("user32.dll", SetLastError = true)]
		//public static extern IntPtr SetFocus(IntPtr hWnd);

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		public static extern uint RegisterWindowMessage(string lpString);

		[DllImport("user32.dll")]
		public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

		[DllImport("user32.dll")]
		public static extern bool DestroyWindow(IntPtr hWnd);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool IsWindowVisible(IntPtr hWnd);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
	}
}
