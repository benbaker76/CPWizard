// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Runtime.InteropServices;
using System.IO;

namespace CPWizard
{
	class MAMEManager : IDisposable
	{
		public MAMEManager()
		{
			try
			{
				Globals.MAMEInterop.MAMEStart += OnMAMEStart;
				Globals.MAMEInterop.MAMEStop += OnMAMEStop;
				Globals.MAMEInterop.MAMEOutput += OnMAMEOutput;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("MAMEManager", "MAMEManager", ex.Message, ex.StackTrace);
			}
		}

		public MAMEMachineNode GetParentROMControls(MAMEMachineNode machineNode)
		{
			MAMEMachineNode parentGameInfo = machineNode;

			try
			{
				do
				{
					if (parentGameInfo.ControlsDat != null)
						return parentGameInfo;

					if (parentGameInfo.CloneOf != null) // Game has a clone?
						Globals.MAMEXml.MachineDictionary.TryGetValue(parentGameInfo.CloneOf, out parentGameInfo);

					if (parentGameInfo.ControlsDat != null)
						return parentGameInfo;

					if (parentGameInfo.ROMOf != null)
						Globals.MAMEXml.MachineDictionary.TryGetValue(parentGameInfo.ROMOf, out parentGameInfo);
				}
				while (parentGameInfo.ROMOf != null);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetParentROMControls", "MAMEManager", ex.Message, ex.StackTrace);
			}

			return parentGameInfo;
		}

		public void GetGameDetails(bool MapInputToLabels, bool layoutOverride, bool sub)
		{
			try
			{
				if (Settings.MAME.GameName != null && Globals.MAMEXml != null)
				{
					if (Globals.MAMEXml.GameDictionary.TryGetValue(Settings.MAME.GameName, out Settings.MAME.MachineNode))
					{
						if (MapInputToLabels)
						{
							if (sub)
							{
								Globals.LayoutManager.MapMAMEInputToLabels(ref Globals.LayoutSub, ref Globals.LayoutSubList, layoutOverride, true, true);

								if (Globals.LayoutSubList != null)
								{
									for (int i = 0; i < Globals.LayoutSubList.Count; i++)
									{
										Layout layout = Globals.LayoutSubList[i];
										Globals.LayoutManager.MapMAMEInputToLabels(ref layout, ref Globals.LayoutSubList, layoutOverride, false, true);
									}
								}
							}
							else
							{
								Globals.LayoutManager.MapMAMEInputToLabels(ref Globals.Layout, ref Globals.LayoutList, layoutOverride, true, false);

								if (Globals.LayoutList != null)
								{
									for (int i = 0; i < Globals.LayoutList.Count; i++)
									{
										Layout layout = Globals.LayoutList[i];
										Globals.LayoutManager.MapMAMEInputToLabels(ref layout, ref Globals.LayoutList, layoutOverride, false, false);
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetGameDetails", "MAMEManager", ex.Message, ex.StackTrace);
			}
		}

		/* public void GetMAMEScreenshot()
		{
			try
			{
				if (Settings.MAME.WindowInfoList.Count > 0)
				{
					foreach (WindowInfo windowInfo in Settings.MAME.WindowInfoList)
					   Global.ScreenshotArray.Add(ScreenShotNode(WindowScreenshot.GetClientRect(hWnd), WindowScreenshot.TakeWindowScreenshot(hWnd)));
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetMAMEScreenshot", "MAMEManager", ex.Message, ex.StackTrace);
			}
		} */

		public Bitmap GetIcon(string name, string romOf)
		{
			try
			{
				string iconPath1 = null;
				string iconPath2 = null;

				if (Settings.Folders.MAME.Icons != null)
				{
					iconPath1 = Path.Combine(Settings.Folders.MAME.Icons, name + ".ico");
					iconPath2 = Path.Combine(Settings.Folders.MAME.Icons, romOf + ".ico");
				}

				if (File.Exists(iconPath1))
				{
					using (Icon icon = new Icon(iconPath1))
						return icon.ToBitmap();
				}
				else if (File.Exists(iconPath2))
				{
					using (Icon icon = new Icon(iconPath2))
						return icon.ToBitmap();
				}
				else
					return FileIO.LoadImage(Path.Combine(Settings.Folders.Media, "IconDefault.png"));
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetIcon", "MAMEManager", ex.Message, ex.StackTrace);
			}

			return null;
		}

		public Bitmap GetMarquee(string Name, string RomOf)
		{
			try
			{
				string MarqueePath1 = null;
				string MarqueePath2 = null;

				if (Settings.Folders.MAME.Marquees != null)
				{
					MarqueePath1 = Path.Combine(Settings.Folders.MAME.Marquees, Name + ".png");
					MarqueePath2 = Path.Combine(Settings.Folders.MAME.Marquees, RomOf + ".png");
				}

				if (File.Exists(MarqueePath1))
					return FileIO.LoadImage(MarqueePath1);
				else if (File.Exists(MarqueePath2))
					return FileIO.LoadImage(MarqueePath2);
				else
					return FileIO.LoadImage(Path.Combine(Settings.Folders.Media, "MarqueeDefault.png"));
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetMarquee", "MAMEManager", ex.Message, ex.StackTrace);
			}

			return null;
		}

		public void MinimizeMAME()
		{
			try
			{
				if (Settings.MAME.WindowInfoList.Count > 0)
				{
					foreach (WindowInfo windowInfo in Settings.MAME.WindowInfoList)
					{
						Win32.SetWindowPos(windowInfo.Handle, Win32.HWND_NOTOPMOST, 0, 0, 0, 0, Win32.SWP_NOMOVE | Win32.SWP_NOSIZE);

						LogFile.VerboseWriteLine("MinimizeMAME", "MAMEManager", String.Format("Minimizing MAME hWnd: 0x{0:x8}", windowInfo.Handle));
						Win32.ShowWindow(windowInfo.Handle, (int)Win32.WindowShowStyle.Minimize);
						Win32.UpdateWindow(windowInfo.Handle);

						Win32.SendMessage(windowInfo.Handle, (uint)Win32.WM_USER_SET_MINSIZE, IntPtr.Zero, IntPtr.Zero);
					}

					LogFile.VerboseWriteLine("MinimizeMAME", "MAMEManager", "Minimizing MAME Console hWnd: 0x" + Settings.MAME.hWndConsole);
					if (Settings.MAME.hWndConsole != IntPtr.Zero)
					{
						Win32.ShowWindow(Settings.MAME.hWndConsole, (int)Win32.WindowShowStyle.Minimize);
						Win32.UpdateWindow(Settings.MAME.hWndConsole);
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("MinimizeMAME", "MAMEManager", ex.Message, ex.StackTrace);
			}
		}

		public void RestoreMAME()
		{
			try
			{
				LogFile.VerboseWriteLine("RestoreMAME", "MAMEManager", String.Format("Restoring MAME Console hWnd: 0x{0:x8}", Settings.MAME.hWndConsole));
				if (Settings.MAME.hWndConsole != IntPtr.Zero)
				{
					Win32.ShowWindow(Settings.MAME.hWndConsole, (int)Win32.WindowShowStyle.Restore);
					Win32.UpdateWindow(Settings.MAME.hWndConsole);
				}

				if (Settings.MAME.WindowInfoList.Count > 0)
				{
					for (int i = 0; i < Settings.MAME.WindowInfoList.Count; i++)
					{
						LogFile.VerboseWriteLine("RestoreMAME", "MAMEManager", String.Format("Restoring MAME hWnd: 0x{0:x8}", Settings.MAME.WindowInfoList[i].Handle));

						Win32.ShowWindow(Settings.MAME.WindowInfoList[i].Handle, (int)Win32.WindowShowStyle.Restore);
						Win32.UpdateWindow(Settings.MAME.WindowInfoList[i].Handle);

						Win32.SendMessage(Settings.MAME.WindowInfoList[i].Handle, (uint)Win32.WM_USER_SET_MAXSIZE, IntPtr.Zero, IntPtr.Zero);

						Win32.SetWindowPos(Settings.MAME.WindowInfoList[i].Handle, Win32.HWND_TOP, Settings.MAME.RectList[i].X, Settings.MAME.RectList[i].Y, Settings.MAME.RectList[i].Width, Settings.MAME.RectList[i].Height, 0);
						Win32.SetWindowPos(Settings.MAME.WindowInfoList[i].Handle, Win32.HWND_TOPMOST, 0, 0, 0, 0, Win32.SWP_NOMOVE | Win32.SWP_NOSIZE);
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("RestoreMAME", "MAMEManager", ex.Message, ex.StackTrace);
			}
		}

		public void PauseMAME()
		{
			try
			{
				switch (Settings.MAME.PauseMode)
				{
					case PauseMode.Msg:
						if (Settings.MAME.WindowInfoList.Count > 0)
						{
							LogFile.VerboseWriteLine("PauseMAME", "MAMEManager", "Sending Pause Message to MAME");
							Win32.PostMessage(Settings.MAME.WindowInfoList[0].Handle, Win32.WM_USER_UI_TEMP_PAUSE, new IntPtr(1), IntPtr.Zero);
						}
						break;
					case PauseMode.Diff:
						LogFile.VerboseWriteLine("PauseMAME", "MAMEManager", "Sending Pause State to MAME");
						Globals.MAMEInterop.PauseMAME(1);
						break;
					case PauseMode.Key:
						if (Settings.MAME.WindowInfoList.Count > 0)
						{
							LogFile.VerboseWriteLine("PauseMAME", "MAMEManager", "Sending Pause Keys to MAME");
							ProcessTools.TryAppActivate(Settings.MAME.WindowInfoList);

							Globals.SendKeys.SendKeyString("p", true);
						}
						break;
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("PauseMAME", "MAMEManager", ex.Message, ex.StackTrace);
			}
		}

		public void UnPauseMAME()
		{
			try
			{
				switch (Settings.MAME.PauseMode)
				{
					case PauseMode.Msg:
						if (Settings.MAME.WindowInfoList.Count > 0)
						{
							LogFile.VerboseWriteLine("UnPauseMAME", "MAMEManager", "Sending UnPause Message to MAME");
							Win32.PostMessage(Settings.MAME.WindowInfoList[0].Handle, Win32.WM_USER_UI_TEMP_PAUSE, IntPtr.Zero, IntPtr.Zero);
						}
						break;
					case PauseMode.Diff:
						LogFile.VerboseWriteLine("UnPauseMAME", "MAMEManager", "Sending UnPause State to MAME");
						Globals.MAMEInterop.PauseMAME(0);
						break;
					case PauseMode.Key:
						if (Settings.MAME.WindowInfoList.Count > 0)
						{
							LogFile.VerboseWriteLine("UnPauseMAME", "MAMEManager", "Sending Pause Keys to MAME");

							ProcessTools.TryAppActivate(Settings.MAME.WindowInfoList);
							Globals.SendKeys.SendKeyString("p", true);
						}
						break;
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("UnPauseMAME", "MAMEManager", ex.Message, ex.StackTrace);
			}
		}

		public void FindMAME()
		{
			try
			{
				ProcessTools.TryFindWindows("MAME:", "MAME", false, out Settings.MAME.WindowInfoList);

				if (Settings.MAME.WindowInfoList.Count > 0)
				{
					Settings.MAME.RectList = new List<Rectangle>();

					foreach (WindowInfo windowInfo in Settings.MAME.WindowInfoList)
					{
						LogFile.VerboseWriteLine("FindMAME", "MAMEManager", String.Format("MAME Running hWnd: 0x{0:x8}", windowInfo.Handle));

						HandleRef handleRef = new HandleRef(Globals.MainForm, windowInfo.Handle);
						Rectangle rect = Win32.GetWindowRectangle(handleRef);
						Settings.MAME.RectList.Add(rect);

						LogFile.VerboseWriteLine("FindMAME", "MAMEManager", String.Format("Window Rect X: {0} Y: {1} Width: {2} Height: {3}", rect.X, rect.Y, rect.Width, rect.Height));
					}

					int pid = 0;
					int pidParent = 0;
					Settings.MAME.hWndConsole = IntPtr.Zero;

					Win32.GetWindowThreadProcessId(Settings.MAME.WindowInfoList[0].Handle, out pid);

					if (pid != 0)
					{
						ProcessTools.TryGetParentPidFromPid(pid, out pidParent);

						if (pidParent != 0)
						{
							List<WindowInfo> windowInfoList = null;

							ProcessTools.TryGetWindowInfoFromPid(pidParent, false, out windowInfoList);

							if (windowInfoList.Count > 0)
								Settings.MAME.hWndConsole = windowInfoList[0].Handle;
						}
					}

					Settings.MAME.Running = true;

					ProcessTools.TryGetCommandLineFromHwnd(Settings.MAME.WindowInfoList, out Settings.MAME.CommandLine);

					LogFile.VerboseWriteLine("FindMAME", "MAMEManager", "Command Line: " + Settings.MAME.CommandLine);
				}
				else
				{
					LogFile.VerboseWriteLine("FindMAME", "MAMEManager", "MAME Not Running");

					Settings.MAME.Running = false;
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("FindMAME", "MAMEManager", ex.Message, ex.StackTrace);
			}
		}

		public string GetMAMEROMName()
		{
			if (Settings.MAME.WindowInfoList.Count == 0)
				return null;

			string windowTitle = null;

			ProcessTools.TryGetWindowTitle(Settings.MAME.WindowInfoList[0].Handle, out windowTitle);

			if (windowTitle != null)
			{
				if (windowTitle.IndexOf('[') < windowTitle.IndexOf(']'))
					return windowTitle.Substring(windowTitle.IndexOf('[') + 1, windowTitle.IndexOf(']') - windowTitle.IndexOf('[') - 1);
			}

			return null;
		}

		public void OnMAMEStart(object sender, MAMEEventArgs e)
		{
			try
			{
				if (!Settings.MAME.UseMAMEOutputSystem)
					return;

				LogFile.VerboseWriteLine("OnMAMEStart", "MAMEManager", "OnMAMEStart");

				Settings.MAME.GameName = e.ROMName;
				Settings.MAME.Running = true;
				Globals.EmulatorMode = EmulatorMode.MAME;

				LogFile.VerboseWriteLine("OnMAMEStart", "MAMEManager", "GameName: " + e.ROMName);

				if (Settings.MAME.SkipDisclaimer)
					SkipDisclaimer();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("OnMAMEStart", "MAMEManager", ex.Message, ex.StackTrace);
			}
		}

		public void OnMAMEStop(object sender, EventArgs e)
		{
			try
			{
				if (!Settings.MAME.UseMAMEOutputSystem)
					return;

				LogFile.VerboseWriteLine("OnMAMEStop", "MAMEManager", "OnMAMEStop");

				Settings.MAME.Running = false;
				Settings.MAME.Paused = false;
				Settings.MAME.WindowInfoList.Clear();
				Settings.MAME.GameName = null;
				Settings.MAME.MachineNode = null;

				LogFile.VerboseWriteLine("OnMAMEStop", "MAMEManager", "Success");
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("OnMAMEStop", "MAMEManager", ex.Message, ex.StackTrace);
			}
		}

		public void OnMAMEOutput(object sender, MAMEOutputEventArgs e)
		{
			try
			{
				if (!Settings.MAME.UseMAMEOutputSystem)
					return;

				LogFile.VerboseWriteLine("OnMAMEOutput", "MAMEManager", "OnMAMEOutput: " + e.Name + "," + e.State.ToString());

				switch (e.Name)
				{
					case "pause":
						if (e.State == 1)
						{
							if (Settings.MAME.DetectPause)
							{
								LogFile.VerboseWriteLine("OnMAMEOutput", "MAMEManager", "MAMEPaused");

								Settings.MAME.Paused = true;

								if (Globals.DisplayMode == DisplayMode.LayoutEditor)
								{
									LogFile.VerboseWriteLine("OnMAMEOutput", "MAMEManager", "Showing MAME");

									Globals.ProgramManager.Show(Settings.Data.General.ShowCPOnly, Settings.Data.General.ExitToMenu);
								}
								else
								{
									LogFile.VerboseWriteLine("OnMAMEOutput", "MAMEManager", "Error: " + Globals.DisplayModeString[(int)Globals.DisplayMode]);
								}
							}
						}
						else
						{
							LogFile.VerboseWriteLine("OnMAMEOutput", "MAMEManager", "MAMEUnPaused");

							Settings.MAME.Paused = false;
						}
						break;
					case "led0":
					case "led1":
						break;
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("OnMAMEOutput", "MAMEManager", ex.Message, ex.StackTrace);
			}
		}

		public void SkipDisclaimer()
		{
			try
			{
				LogFile.VerboseWriteLine("SkipDisclaimer", "MAMEManager", "SkipDisclaimer");

				Thread SendKeysThread = new Thread(new ThreadStart(SendOkKeys));
				SendKeysThread.Start();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("SkipDisclaimer", "MAMEManager", ex.Message, ex.StackTrace);
			}
		}

		public void SendOkKeys()
		{
			System.Diagnostics.Debug.WriteLine("SendOkKeys");

			try
			{
				if (Settings.MAME.WindowInfoList.Count > 0)
				{
					System.Threading.Thread.Sleep(1000);
					ProcessTools.TryAppActivate(Settings.MAME.WindowInfoList);
					Globals.SendKeys.SendKeyString("okokokokokokokokokokokokokok", true);
					//Global.SendKeys.PostString(new HandleRef(this, Settings.MAME.MAMEHwnd), "okokokokokokokokokokokokokok");
					//Global.SendKeys.SendRawString("okokokokokokokokokokokokokok");
					//Global.SendKeys.SendStringState("okokokokokokokokokokokokokok");
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("SendOkKeys", "MAMEManager", ex.Message, ex.StackTrace);
			}
		}

		#region IDisposable Members

		public void Dispose()
		{
		}

		#endregion
	}
}
