// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Threading;
using System.IO;

namespace CPWizard
{
	class EmuLabel
	{
		public string Name = null;
		public string Value = null;

		public EmuLabel(string name, string value)
		{
			Name = name;
			Value = value;
		}
	}

	class EmulatorManager : IDisposable
	{
		public List<EmuLabel> Labels = null;
		//public Bitmap EmuBak = null;

		public EmulatorManager()
		{
			Labels = new List<EmuLabel>();
		}

		public void GetGameDetails(bool layoutOverride, bool sub)
		{
			if (sub)
			{
				Globals.LayoutManager.MapEmuInputToLabels(ref Globals.LayoutSub, ref Globals.LayoutSubList, layoutOverride, true);

				if (Globals.LayoutSubList != null)
				{
					for (int i = 0; i < Globals.LayoutSubList.Count; i++)
					{
						Layout layout = Globals.LayoutSubList[i];

						Globals.LayoutManager.MapEmuInputToLabels(ref layout, ref Globals.LayoutSubList, layoutOverride, true);
					}
				}
			}
			else
			{
				Globals.LayoutManager.MapEmuInputToLabels(ref Globals.Layout, ref Globals.LayoutList, layoutOverride, false);

				if (Globals.LayoutList != null)
				{
					for (int i = 0; i < Globals.LayoutList.Count; i++)
					{
						Layout layout = Globals.LayoutList[i];

						Globals.LayoutManager.MapEmuInputToLabels(ref layout, ref Globals.LayoutList, layoutOverride, false);
					}
				}
			}
		}

		/* public void GetEmuScreenshot()
		{
			try
			{
				if (Settings.Emulator.HwndArray.Count > 0)
				{
					FindProcess.AppActivate(Settings.Emulator.HwndArray);

					foreach (IntPtr hWnd in Settings.Emulator.HwndArray)
						Global.ScreenshotArray.Add(WindowScreenshot.TakeWindowScreenshot(hWnd));
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetEmuScreenshot", "EmulatorManager", ex.Message, ex.StackTrace);
			}
		} */

		public void MinimizeEmu()
		{
			try
			{
				if (Settings.Emulator.Hwnd != IntPtr.Zero)
				{
					Win32.SetWindowPos(Settings.Emulator.Hwnd, Win32.HWND_NOTOPMOST, 0, 0, 0, 0, Win32.SWP_NOMOVE | Win32.SWP_NOSIZE);
					Win32.ShowWindow(Settings.Emulator.Hwnd, (int)Win32.WindowShowStyle.Minimize);
					Win32.UpdateWindow(Settings.Emulator.Hwnd);
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("MinimizeEmu", "EmulatorManager", ex.Message, ex.StackTrace);
			}
		}

		public void RestoreEmu()
		{
			try
			{
				if (Settings.Emulator.Hwnd != null)
				{
					Win32.ShowWindow(Settings.Emulator.Hwnd, (int)Win32.WindowShowStyle.Restore);
					Win32.UpdateWindow(Settings.Emulator.Hwnd);
					//FindProcess.AppActivate(Settings.Emulator.Hwnd);
					//HideDesktop.ForceForegroundWindow(hWnd);

					Win32.SetWindowPos(Settings.Emulator.Hwnd, Win32.HWND_TOP, Settings.Emulator.Rect.X, Settings.Emulator.Rect.Y, Settings.Emulator.Rect.Width, Settings.Emulator.Rect.Height, 0);
					Win32.SetWindowPos(Settings.Emulator.Hwnd, Win32.HWND_TOPMOST, 0, 0, 0, 0, Win32.SWP_NOMOVE | Win32.SWP_NOSIZE);
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("RestoreEmu", "EmulatorManager", ex.Message, ex.StackTrace);
			}
		}

		/* public void PauseEmu()
		{
			if (Settings.Emulator.Hwnd != IntPtr.Zero)
			{
				Global.SendKeys.AppActivate(Settings.Emulator.Hwnd);
				Global.SendKeys.SendKeyString("p", true);
			}
		}

		public void EmuStart(string GameName)
		{
			//Settings.Emulator.Profile.Running = true;
			Settings.Emulator.GameName = GameName;

			if (Global.Layout != null)
			{
				//if (Global.Layout.LayoutOverrideFolder != null)
				//    Global.ProgramManager.LoadLayout(Path.Combine(Global.Layout.LayoutOverrideFolder, Settings.Emulator.GameName + ".xml"));
			}
		}

		public void EmuStop()
		{
			//Settings.Emulator.Profile.Running = false;
			Settings.Emulator.Hwnd = IntPtr.Zero;
			Settings.Emulator.GameName = null;
			Settings.Mame.GameInfo = null;
		}

		public void SkipDisclaimer()
		{
			Thread SendKeysThread = new Thread(new ThreadStart(SendOkKeys));
			SendKeysThread.Start();
		}

		public void SendOkKeys()
		{
			if (Settings.Emulator.Profile.Hwnd != IntPtr.Zero)
			{
				System.Threading.Thread.Sleep(1000);
				Global.SendKeys.AppActivate(Settings.Emulator.Profile.Hwnd);
				Global.SendKeys.SendKeyString("okokokokokokokokokokokokokok", true);
				//Global.SendKeys.PostString(new HandleRef(this, Settings.Emulator.Profile.Hwnd), "okokokokokokokokokokokokokok");
				//Global.SendKeys.SendRawString("okokokokokokokokokokokokokok");
				//Global.SendKeys.SendStringState("okokokokokokokokokokokokokok");
			}
		}

		public void CheckForEmu()
		{
			if (Settings.Emulator.Profile.Hwnd == IntPtr.Zero)
			{
				FindEmulator();
			}
		} */

		#region IDisposable Members

		public void Dispose()
		{
		}

		#endregion
	}
}
