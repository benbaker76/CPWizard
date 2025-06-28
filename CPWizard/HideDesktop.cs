// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.Win32;

namespace CPWizard
{
	class HideDesktop
	{
		public static IntPtr OldCursorArrow = IntPtr.Zero;
		public static bool ScreenSaverActive = false;
		public static Color OldDesktopColor = Color.Empty;
		public static frmBlank[] BlankForms;

		public static bool ForceForegroundWindow(IntPtr hWnd)
		{
			bool ret = false;

			try
			{
				IntPtr hWndForeground = Win32.GetForegroundWindow();

				if (hWnd == hWndForeground)
				{
					LogFile.VerboseWriteLine("ForceForegroundWindow", "HideDesktop", "Already Foreground hWnd: " + String.Format("0x{0:x8}", hWndForeground));

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
				LogFile.VerboseWriteLine("ForceForegroundWindow", "HideDesktop", "Foreground hWnd: " + String.Format("0x{0:x8}", hWndForeground));
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

				bool topMost = form.TopMost;

				form.TopMost = true;
				form.Focus();

				ForceForegroundWindow(form.Handle);

				if (topMost == false)
					form.TopMost = false;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ForceForegroundWindow", "HideDesktop", ex.Message, ex.StackTrace);
			}
		}

		public static void HideDesktopIcons()
		{
			try
			{
				IntPtr hWnd = IntPtr.Zero;

				hWnd = Win32.FindWindow("Progman", null);

				if (hWnd != IntPtr.Zero)
					Win32.ShowWindow(hWnd, (int)Win32.WindowShowStyle.Hide);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("HideDesktopIcons", "HideDesktop", ex.Message, ex.StackTrace);
			}
		}

		public static void ShowDesktopIcons()
		{
			try
			{
				IntPtr hWnd = IntPtr.Zero;

				hWnd = Win32.FindWindow("Progman", null);

				if (hWnd != IntPtr.Zero)
					Win32.ShowWindow(hWnd, (int)Win32.WindowShowStyle.Show);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ShowDesktopIcons", "HideDesktop", ex.Message, ex.StackTrace);
			}
		}

		public static void HideTaskBar()
		{
			try
			{
				IntPtr hWnd = IntPtr.Zero;

				hWnd = Win32.FindWindow("Shell_TrayWnd", null);

				if (hWnd != IntPtr.Zero)
					Win32.ShowWindow(hWnd, (int)Win32.WindowShowStyle.Hide);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("HideTaskBar", "HideDesktop", ex.Message, ex.StackTrace);
			}
		}

		public static void ShowTaskBar()
		{
			try
			{
				IntPtr hWnd = IntPtr.Zero;

				hWnd = Win32.FindWindow("Shell_TrayWnd", null);

				if (hWnd != IntPtr.Zero)
					Win32.ShowWindow(hWnd, (int)Win32.WindowShowStyle.Show);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ShowTaskBar", "HideDesktop", ex.Message, ex.StackTrace);
			}
		}

		public static void MoveMouseOffscreen()
		{
			try
			{
				Size Screen = System.Windows.Forms.Screen.AllScreens[0].Bounds.Size;

				Win32.SetCursorPos(Screen.Width, Screen.Height);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("MoveMouseOffscreen", "HideDesktop", ex.Message, ex.StackTrace);
			}
		}

		public static void SetDesktopColor(Color color)
		{
			try
			{
				OldDesktopColor = System.Drawing.ColorTranslator.FromWin32(Win32.GetSysColor(Win32.COLOR_DESKTOP));
				int[] elements = { Win32.COLOR_DESKTOP };
				int[] colors = { System.Drawing.ColorTranslator.ToWin32(color) };
				Win32.SetSysColors(elements.Length, elements, colors);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("SetDesktopColor", "HideDesktop", ex.Message, ex.StackTrace);
			}
		}

		public static void RestoreDesktopColor()
		{
			try
			{
				SetDesktopColor(OldDesktopColor);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("RestoreDesktopColor", "HideDesktop", ex.Message, ex.StackTrace);
			}
		}

		public static void SetBlackWallpaper()
		{
			try
			{
				SetDesktopColor(Color.Black);
				Win32.SystemParametersInfo(Win32.SPI_SETDESKWALLPAPER, 0, "", 0);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("SetBlackWallpaper", "HideDesktop", ex.Message, ex.StackTrace);
			}
		}

		public static void RestoreWallpaper()
		{
			try
			{
				RestoreDesktopColor();
				Win32.SystemParametersInfo(Win32.SPI_SETDESKWALLPAPER, 0, 0, 0);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("RestoreWallpaper", "HideDesktop", ex.Message, ex.StackTrace);
			}
		}

		public static bool IsScreenSaverActive()
		{
			try
			{
				bool Active = false;

				Win32.SystemParametersInfo(Win32.SPI_GETSCREENSAVEACTIVE, 0, ref Active, 0);

				return Active;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("IsScreenSaverActive", "HideDesktop", ex.Message, ex.StackTrace);
			}

			return false;
		}

		public static bool EnableScreenSaver(bool Status)
		{
			try
			{
				return Win32.SystemParametersInfo(Win32.SPI_SETSCREENSAVEACTIVE, 0, ref Status, 0);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("EnableScreenSaver", "HideDesktop", ex.Message, ex.StackTrace);
			}

			return false;
		}

		public static void HideMouseCursor()
		{
			try
			{
				IntPtr hCursor = Win32.LoadCursorFromFile(Path.Combine(Settings.Folders.Media, "Dot.cur"));
				Win32.SetSystemCursor(hCursor, Win32.OCR_NORMAL);
				Win32.DestroyCursor(hCursor);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("HideMouseCursor", "HideDesktop", ex.Message, ex.StackTrace);
			}
		}

		public static void RestoreMouseCursor()
		{
			try
			{
				Win32.SystemParametersInfo(Win32.SPI_SETCURSORS, 0, null, Win32.SPIF_UPDATEINIFILE | Win32.SPIF_SENDWININICHANGE);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("RestoreMouseCursor", "HideDesktop", ex.Message, ex.StackTrace);
			}
		}

		public static void HideAllDesktop()
		{
			try
			{
				BlankForms = new frmBlank[System.Windows.Forms.Screen.AllScreens.Length];

				for (int i = 0; i < BlankForms.Length; i++)
				{
					BlankForms[i] = new frmBlank();
					BlankForms[i].Left = System.Windows.Forms.Screen.AllScreens[i].Bounds.Left;
					BlankForms[i].Top = System.Windows.Forms.Screen.AllScreens[i].Bounds.Top;
					BlankForms[i].Width = System.Windows.Forms.Screen.AllScreens[i].Bounds.Width;
					BlankForms[i].Height = System.Windows.Forms.Screen.AllScreens[i].Bounds.Height;
					BlankForms[i].Show();
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("HideAllDesktop", "HideDesktop", ex.Message, ex.StackTrace);
			}
		}

		public static void ShowAllDesktop()
		{
			try
			{
				if (BlankForms == null)
					return;

				for (int i = 0; i < BlankForms.Length; i++)
				{
					BlankForms[i].Close();
					BlankForms[i].Dispose();
					BlankForms[i] = null;
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ShowAllDesktop", "HideDesktop", ex.Message, ex.StackTrace);
			}
		}

		public static void ShowAll()
		{
			try
			{
				if (Settings.HideDesktop.DisableScreenSaver)
					EnableScreenSaver(ScreenSaverActive);
				if (Settings.HideDesktop.SetWallpaperBlack)
					RestoreWallpaper();
				if (Settings.HideDesktop.HideDesktopIcons)
					ShowDesktopIcons();
				if (Settings.HideDesktop.HideTaskbar)
					ShowTaskBar();
				if (Settings.HideDesktop.HideDesktopUsingForms)
					ShowAllDesktop();
				if (Settings.HideDesktop.HideMouseCursor)
					RestoreMouseCursor();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ShowAll", "HideDesktop", ex.Message, ex.StackTrace);
			}
		}

		public static void HideAll()
		{
			try
			{
				if (Settings.HideDesktop.DisableScreenSaver)
				{
					ScreenSaverActive = IsScreenSaverActive();
					EnableScreenSaver(false);
				}
				if (Settings.HideDesktop.HideMouseCursor)
					HideMouseCursor();
				if (Settings.HideDesktop.HideDesktopUsingForms)
					HideAllDesktop();
				if (Settings.HideDesktop.SetWallpaperBlack)
					SetBlackWallpaper();
				if (Settings.HideDesktop.HideDesktopIcons)
					HideDesktopIcons();
				if (Settings.HideDesktop.HideTaskbar)
					HideTaskBar();
				if (Settings.HideDesktop.MoveMouseOffscreen)
					MoveMouseOffscreen();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("HideAll", "HideDesktop", ex.Message, ex.StackTrace);
			}
		}
	}

	partial class Win32
	{
		public const int COLOR_DESKTOP = 1;

		public const int IDC_ARROW = 32512;
		public const int OCR_NORMAL = 32512;

		public const int SPIF_UPDATEINIFILE = 0x1;
		public const int SPIF_SENDWININICHANGE = 0x2;
		public const int SPI_SETCURSORS = 0x57;

		public const int SPI_SETDESKWALLPAPER = 0x14;

		public const int SPI_SETSCREENSAVEACTIVE = 0x11;
		public const int SPI_GETSCREENSAVEACTIVE = 0x10;
		public const int SPI_GETSCREENSAVERRUNNING = 0x72;

		public enum WallpaperStyle
		{
			Center = 0,
			Tile = 1,
			Stretch = 2
		}

		[DllImport("user32.dll")]
		public static extern int GetSysColor(int nIndex);

		[DllImport("user32.dll")]
		public static extern bool SetSysColors(int cElements, int[] lpaElements, int[] lpaRgbValues);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SetSystemCursor(IntPtr hCursor, int id);

		[DllImport("user32.dll")]
		public static extern IntPtr LoadCursorFromFile(string lpFileName);

		[DllImport("user32.dll")]
		public static extern bool DestroyCursor(IntPtr hCursor);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int SystemParametersInfo(int uAction, int uParam, int lpvParam, int fuWinIni);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SystemParametersInfo(int uAction, int uParam, [MarshalAs(UnmanagedType.Bool)] ref bool lpvParam, int fuWinIni);

		//[DllImport("user32.dll")]
		//public static extern bool SetCursorPos(int X, int Y);

		//[DllImport("user32.dll", SetLastError = true)]
		//public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		//[DllImport("user32.dll")]
		//public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		[DllImport("user32.dll")]
		public static extern IntPtr GetForegroundWindow();

		//[DllImport("user32.dll")]
		//public static extern bool AllowSetForegroundWindow(int dwProcessId);

		//[DllImport("user32.dll")]
		//[return: MarshalAs(UnmanagedType.Bool)]
		//public static extern bool SetForegroundWindow(IntPtr hWnd);

		//[DllImport("user32.dll", SetLastError = true)]
		//public static extern int GetWindowThreadProcessId(IntPtr hWnd, int lpdwProcessId);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool IsIconic(IntPtr hWnd);

		//[DllImport("user32.dll")]
		//public static extern bool AttachThreadInput(int idAttach, int idAttachTo, int fAttach);

		[DllImport("user32.dll")]
		public static extern bool SetSystemCursor(IntPtr hcur, uint id);

		/* [Flags]
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
		} */

		public const int SPI_GETFOREGROUNDLOCKTIMEOUT = 0x2000;
		public const int SPI_SETFOREGROUNDLOCKTIMEOUT = 0x2001;
		public const int SPIF_SENDCHANGE = 0x2;

		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SystemParametersInfo(int uiAction, uint uiParam, IntPtr pvParam, int fWinIni);

		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SystemParametersInfo(int uiAction, uint uiParam, ref int pvParam, int fWinIni);

		//[DllImport("user32.dll", SetLastError = true)]
		//public static extern bool BringWindowToTop(IntPtr hWnd);

		//[DllImport("user32.dll", SetLastError = true)]
		//public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

		///[DllImport("user32.dll")]
		//public static extern bool UpdateWindow(IntPtr hWnd);
	}
}
