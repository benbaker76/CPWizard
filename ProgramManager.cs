// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using Microsoft.Win32;
using System.Timers;
using System.Windows.Forms;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CPWizard
{
	public class ProgramManager
	{
		private System.Timers.Timer LayoutTimeoutTimer = null;
		private System.Timers.Timer AutoShowTimer = null;
		public System.Timers.Timer DisplaySettingsChangedTimer = null;

		private int DisplayRotation = 0;

		public ProgramManager()
		{
			try
			{
				Globals.MainForm.SetMenuEnabled(false);
				Globals.MainForm.SetToolbarEnabled(false);

				LogFile.WriteLine("Reading Config");
				ConfigIO.ReadConfig();

				if (Settings.HideDesktop.Enable)
				{
					LogFile.WriteLine("Hiding Desktop");

					HideDesktop.HideAll();
				}

				LogFile.WriteLine("Initializing Main Menu");
				Globals.MainMenu = new MainMenu();
				LogFile.WriteLine("Initializing Layout Manager");
				Globals.LayoutManager = new LayoutManager();
				LogFile.WriteLine("Initializing Controls Data");
				Globals.ControlsData = new ControlsData();
				LogFile.WriteLine("Initializing Input Codes");
				Globals.InputCodes = new InputCodes();
				LogFile.WriteLine("Initializing Layout Maps");
				Globals.LayoutMaps = new LayoutMaps();
				LogFile.WriteLine("Initializing Game Info");
				Globals.GameInfo = new GameInfo();
				LogFile.WriteLine("Initializing History Dat");
				Globals.HistoryDat = new HistoryDat();
				LogFile.WriteLine("Initializing MAMEInfo Dat");
				Globals.MAMEInfoDat = new MAMEInfoDat();
				LogFile.WriteLine("Initializing Command Dat");
				Globals.CommandDat = new CommandDat();
				LogFile.WriteLine("Initializing Story Dat");
				Globals.StoryDat = new StoryDat();
				LogFile.WriteLine("Initializing HiToText");
				Globals.HiToText = new HiToText();
				LogFile.WriteLine("Initializing Artwork Manager");
				Globals.ArtworkManager = new ArtworkManager();
				LogFile.WriteLine("Initializing MAME Manuals");
				Globals.MAMEManual = new PDFManager();
				LogFile.WriteLine("Initializing Emulator Manuals");
				Globals.EmulatorManual = new PDFManager();
				LogFile.WriteLine("Initializing Emulator OpCard");
				Globals.EmulatorOpCard = new PDFManager();
				LogFile.WriteLine("Initializing NFO Viewer");
				Globals.NFOViewer = new NFOViewer();
				LogFile.WriteLine("Initializing IRC");
				Globals.IRC = new IRC();

				LogFile.WriteLine("Initializing SendKeys");
				Globals.SendKeys = new SendKeys();
				LogFile.WriteLine("Initializing MAME Interop");
				Globals.MAMEInterop = new MAMEInterop(Globals.MainForm);
				LogFile.WriteLine("Initializing MAME Manager");
				Globals.MAMEManager = new MAMEManager();
				LogFile.WriteLine("Initializing Emulator Manager");
				Globals.EmulatorManager = new EmulatorManager();
				LogFile.WriteLine("Initializing Keyboard Hook");
				Globals.KeyboardHook = new KeyboardHook(Globals.MainForm);
				//LogFile.WriteLine("Initializing Mouse Hook");
				//Global.MouseHook = new MouseHook(Globals.MainForm);
				LogFile.WriteLine("Initializing Direct Input");
				Globals.DirectInput = new DirectInput(Globals.MainForm, null);
				Globals.DirectInput.ResourceLoad();
				LogFile.WriteLine("Initializing MCE Remote");
				Globals.MCERemote = new MCERemote(Globals.MainForm);
				LogFile.WriteLine("Initializing Input Manager");
				Globals.InputManager = new InputManager();
				LogFile.WriteLine("Initializing Profiles");
				Globals.Profiles = new Profiles();
				LogFile.WriteLine("Initializing Bezel");
				Globals.Bezel = new MAMEBezel();

				LoadMAMEData();

				LoadLayout(Path.Combine(Settings.Folders.Layout, Settings.Layout.Name + ".xml"), ref Globals.Layout, ref Globals.LayoutList, false);

				CreateBitmaps();

				Assembly assembly = Assembly.GetExecutingAssembly();
				Stream watermarkStream = assembly.GetManifestResourceStream("CPWizard.Watermark.png");
				Globals.WatermarkBitmap = new Bitmap(watermarkStream);

				LayoutTimeoutTimer = new System.Timers.Timer();
				LayoutTimeoutTimer.Elapsed += OnLayoutTimeOut;
				LayoutTimeoutTimer.SynchronizingObject = Globals.MainForm;
				LayoutTimeoutTimer.AutoReset = false;

				AutoShowTimer = new System.Timers.Timer();
				AutoShowTimer.Elapsed += OnAutoShow;
				AutoShowTimer.SynchronizingObject = Globals.MainForm;
				AutoShowTimer.AutoReset = false;

				DisplaySettingsChangedTimer = new System.Timers.Timer(Settings.Display.DisplayChangeDelay);
				DisplaySettingsChangedTimer.Elapsed += DisplaySettingsChangedTimer_Elapsed;
				DisplaySettingsChangedTimer.SynchronizingObject = Globals.MainForm;
				DisplaySettingsChangedTimer.AutoReset = false;

				EventManager.OnCmdArgsChanged += OnCmdArgsChanged;
				Globals.InputManager.InputEvent += OnInputEvent;
				Microsoft.Win32.SystemEvents.DisplaySettingsChanged += SystemEvents_DisplaySettingsChanged;

				Globals.MAMEInterop.MAMEStart += OnMAMEStart;
				Globals.Profiles.MAMEStart += OnMAMEStart;
				Globals.Profiles.EmuStart += OnEmuStart;

				Globals.MainForm.SetMenuEnabled(true);
				Globals.MainForm.SetToolbarEnabled(true);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ProgramManager", "ProgramManager", ex.Message, ex.StackTrace);
			}
		}

		private void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
		{
			LogFile.VerboseWriteLine("SystemEvents_DisplaySettingsChanged", "ProgramManager", "Display Settings Changed");

			if (Settings.Display.DisplayChange)
				DisplaySettingsChangedTimer.Start();
		}

		private void DisplaySettingsChangedTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			LogFile.VerboseWriteLine("DisplaySettingsChangedTimer_Elapsed", "ProgramManager", "Display Settings Changed");

			if (Globals.LayoutForm != null)
			{
				if (Globals.LoadingFormList != null)
				{
					foreach (frmLoading loadingForm in Globals.LoadingFormList)
						loadingForm.OnDisplaySettingsChanged();
				}

				Globals.LayoutForm.OnDisplaySettingsChanged();
			}
		}

		private void OnInputEvent(object sender, InputEventArgs e)
		{
			try
			{
				LayoutTimeoutTimer.Enabled = false;

				if (Settings.General.VolumeControlEnable)
				{
					if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.VolumeDown))
					{
						e.Handled = true;

						if (e.IsDown)
						{
							int volume = Math.Max(VolumeControl.GetVolume() - 5, 0);

							VolumeControl.SetVolume(volume);
						}
					}

					if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.VolumeUp))
					{
						e.Handled = true;

						if (e.IsDown)
						{
							int volume = Math.Min(VolumeControl.GetVolume() + 5, 100);

							VolumeControl.SetVolume(volume);
						}
					}
				}

				if (Settings.HideDesktop.Enable)
				{
					if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.ShowDesktop))
					{
						e.Handled = true;

						if (!e.IsDown)
						{
							HideDesktop.HideAll();
						}
					}

					if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.HideDesktop))
					{
						e.Handled = true;

						if (!e.IsDown)
						{
							HideDesktop.ShowAll();
						}
					}
				}

				if (Globals.DisplayMode == DisplayMode.LayoutEditor)
				{
					if (Settings.MAME.Running)
					{
						if (Settings.MAME.UseShowKey)
						{
							if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.ShowKey))
							{
								e.Handled = true;

								if (!e.IsDown)
								{
									Show(Settings.Data.General.ShowCPOnly, Settings.Data.General.ExitToMenu);
								}
							}
						}
					}
					else if (Settings.Emulator.Running && Settings.Emulator.Profile != null)
					{
						if (!String.IsNullOrEmpty(Settings.Emulator.Profile.ShowKey))
						{
							if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Emulator.Profile.ShowKey))
							{
								e.Handled = true;

								if (!e.IsDown)
								{
									Show(Settings.Data.General.ShowCPOnly, Settings.Data.General.ExitToMenu);
								}
							}
						}
						else
						{
							if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.ShowKey))
							{
								e.Handled = true;

								if (!e.IsDown)
								{
									Show(Settings.Data.General.ShowCPOnly, Settings.Data.General.ExitToMenu);
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("OnGlobalKeyEvent", "ProgramManager", ex.Message, ex.StackTrace);
			}
		}

		private void OnMAMEStart(object sender, MAMEEventArgs e)
		{
			if (Settings.MAME.AutoShow)
			{
				AutoShowTimer.Interval = Settings.MAME.AutoShowDelay;
				AutoShowTimer.Start();
			}

			if (Settings.Display.SubScreenEnable)
			{
				ProcessTools.TryFindWindows("MAME:", "MAME", false, out Settings.MAME.WindowInfoList);

				if (Settings.Display.SubScreenDisable && Settings.MAME.WindowInfoList.Count > 1)
				{
					if (Globals.LayoutSubForm != null)
						Globals.LayoutSubForm.Close();
				}
				else
					ShowSub();
			}
		}

		private void OnEmuStart(object sender, MAMEEventArgs e)
		{
			if (Settings.Display.SubScreenEnable)
				ShowSub();
		}

		private void OnAutoShow(object sender, ElapsedEventArgs e)
		{
			Show(Settings.Data.AutoShow.ShowCPOnly, Settings.Data.AutoShow.ExitToMenu);

			LayoutTimeoutTimer.Interval = Settings.MAME.AutoShowTimeout;
			LayoutTimeoutTimer.Start();
		}

		public void OnCmdArgsChanged(Dictionary<string, string> args)
		{
			try
			{
				if (args == null)
					return;

				string value = null;
				bool layoutOverride = false;
				bool exitToMenu = Settings.Data.General.ExitToMenu;
				DisplayMode displayMode = DisplayMode.Layout;

				if (args.TryGetValue("-exitmenu", out value))
					exitToMenu = true;

				if (args.TryGetValue("-minimized", out value))
					Settings.General.Minimized = true;

				if (args.TryGetValue("-rotate", out value))
				{
					switch (value)
					{
						case "0":
							Settings.Display.Rotation = 0;
							break;
						case "90":
							Settings.Display.Rotation = 90;
							break;
						case "180":
							Settings.Display.Rotation = 180;
							break;
						case "270":
							Settings.Display.Rotation = 270;
							break;
					}
				}

				if (args.TryGetValue("-mode", out value))
				{
					switch (value.ToLower())
					{
						case "mainmenu":
							displayMode = DisplayMode.MainMenu;
							break;
						case "layout":
							displayMode = DisplayMode.Layout;
							break;
						case "gameinfo":
							displayMode = DisplayMode.GameInfo;
							break;
						case "gamehistory":
							displayMode = DisplayMode.HistoryDat;
							break;
						case "mameinfo":
							displayMode = DisplayMode.MAMEInfoDat;
							break;
						case "gamecontrols":
							displayMode = DisplayMode.CommandDat;
							break;
						case "hiscore":
							displayMode = DisplayMode.StoryDat;
							break;
						case "hitotext":
							displayMode = DisplayMode.HiToText;
							break;
						case "artwork":
							displayMode = DisplayMode.Artwork;
							break;
						case "manual":
							displayMode = DisplayMode.Manual;
							break;
						case "irc":
							displayMode = DisplayMode.IRC;
							break;
					}
				}

				if (args.TryGetValue("-emu", out value))
				{
					switch (value.ToLower())
					{
						case "mame":
							Globals.EmulatorMode = EmulatorMode.MAME;
							break;
						default:
							Globals.EmulatorMode = EmulatorMode.Emulator;

							foreach (ProfileNode profile in Globals.Profiles.ProfileList)
							{
								if (profile.Name == value)
									Settings.Emulator.Profile = profile;
							}
							break;
					}
				}

				if (args.TryGetValue("-reload", out value))
					Globals.Profiles.TryReadProfileXml(ref Settings.Emulator.Profile);

				if (args.TryGetValue("-game", out value))
				{
					switch (Globals.EmulatorMode)
					{
						case EmulatorMode.Emulator:
							Settings.Emulator.GameName = value;
							break;
						case EmulatorMode.MAME:
							Settings.MAME.GameName = value.ToLower();
							break;
					}
				}

				if (args.TryGetValue("-ctrlr", out value))
				{
					Settings.Files.Ctrlr = Path.Combine(Settings.Folders.MAME.Ctrlr, value + ".cfg");
					Globals.MAMECfg.ReadMAMECtrlr(Settings.Files.Ctrlr);
				}

				if (args.TryGetValue("-layout", out value))
				{
					if (LoadLayout(Path.Combine(Settings.Folders.Layout, value + ".xml"), ref Globals.Layout, ref Globals.LayoutList, false))
					{
						Settings.Layout.Name = Globals.Layout.Name;
						layoutOverride = true;
					}
				}

				if (args.TryGetValue("-label", out value))
				{
					Globals.EmulatorManager.Labels.Clear();

					if (value.Contains(","))
					{
						string[] LabelSplit = value.Split(new char[] { ',' });

						for (int j = 0; j < LabelSplit.Length; j++)
						{
							if (LabelSplit[j].Contains("="))
							{
								string[] valueSplit = LabelSplit[j].Split(new char[] { '=' }, 2);
								if (valueSplit.Length == 2)
									Globals.EmulatorManager.Labels.Add(new EmuLabel(valueSplit[0], valueSplit[1]));
							}
						}
					}
					else
					{
						if (value.Contains("="))
						{
							string[] valueSplit = value.Split(new char[] { '=' }, 2);
							if (valueSplit.Length == 2)
								Globals.EmulatorManager.Labels.Add(new EmuLabel(valueSplit[0], valueSplit[1]));
						}
					}
				}

				if (args.TryGetValue("-labelfile", out value))
				{
					ParseLabelFile(Path.Combine(Settings.Folders.Labels, value + ".txt"), Settings.Emulator.GameName);
				}

				if (args.TryGetValue("-database", out value))
				{
					//GetGameInfoData(Settings.Emulator.GameName);
				}

				if (args.TryGetValue("-timeout", out value))
				{
					if (Settings.InterComm.FirstRun)
						Settings.InterComm.Exit = true;

					switch (displayMode)
					{
						case DisplayMode.MainMenu:
							ShowScreen(displayMode, layoutOverride, true, Settings.InterComm.FirstRun);
							break;
						default:
							ShowScreen(displayMode, layoutOverride, exitToMenu, Settings.InterComm.FirstRun);
							break;
					}

					LayoutTimeoutTimer.Interval = StringTools.FromString<float>(value);
					LayoutTimeoutTimer.Start();

					return;
				}

				if (args.TryGetValue("-show", out value))
				{
					if (Settings.InterComm.FirstRun)
						Settings.InterComm.Exit = true;

					switch (displayMode)
					{
						case DisplayMode.MainMenu:
							ShowScreen(displayMode, layoutOverride, true, Settings.InterComm.FirstRun);
							break;
						default:
							ShowScreen(displayMode, layoutOverride, exitToMenu, Settings.InterComm.FirstRun);
							break;
					}
				}

				if (args.TryGetValue("-exit", out value))
				{
					LogFile.WriteLine("Starting Exit...");

					Globals.MainForm.CloseType = frmMain.CloseMethodType.Auto;
					Globals.MainForm.Close();
					Globals.MainForm = null;
				}

				Globals.InterComm.SendMessage((int)DataMode.Exit, String.Empty);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("OnCmdArgsChanged", "ProgramManager", ex.Message, ex.StackTrace);
			}
		}

		private void OnLayoutTimeOut(object sender, ElapsedEventArgs e)
		{
			if (Globals.LayoutForm != null)
				Globals.LayoutForm.Close();

			//Global.DisplayMode = DisplayMode.LayoutEditor;
			//Global.LayoutManager.ResetControls();
			//Global.MainMenu.Hide();

			Globals.InterComm.SendMessage((int)DataMode.Exit, String.Empty);

			if (Settings.InterComm.Exit)
			{
				Globals.MainForm.CloseType = frmMain.CloseMethodType.Auto;
				Globals.MainForm.Close();
			}

			if (Settings.MAME.Running)
				Hide();
		}

		public void GetGameInfoData(string Database, string GoodName)
		{
			LogFile.VerboseWriteLine("GetGameInfoData", "ProgramManager", "Starting Method");
			try
			{
				GameInfoNode gameInfoNode = GameInfoDatabase.GetGameInfo(Database, StringTools.RemoveBrackets(GoodName));

				if (gameInfoNode != null)

				{
					LogFile.VerboseWriteLine("GetGameInfoData", "ProgramManager", "gameInfoNode goodname: " + gameInfoNode.GoodName);
					Globals.EmulatorManager.Labels.Add(new EmuLabel("EMU_GAMENAME", gameInfoNode.GoodName));
					Globals.EmulatorManager.Labels.Add(new EmuLabel("EMU_CATEGORY", gameInfoNode.Category));
					Globals.EmulatorManager.Labels.Add(new EmuLabel("EMU_DEVELOPER", gameInfoNode.Developer));
					Globals.EmulatorManager.Labels.Add(new EmuLabel("EMU_NUMPLAYERS", gameInfoNode.NumPlayers.ToString()));
					Globals.EmulatorManager.Labels.Add(new EmuLabel("EMU_DATE", gameInfoNode.Date));
					Globals.EmulatorManager.Labels.Add(new EmuLabel("EMU_DESCRIPTION", gameInfoNode.Description));
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetGameInfoData", "ProgramManager", ex.Message, ex.StackTrace);
			}
		}

		public void ParseLabelFile(string fileName, string romName)
		{
			LogFile.VerboseWriteLine("ParseLabelFile", "ProgramManager", "Starting Method");
			try
			{
				if (!File.Exists(fileName))
					return;

				IniFile iniFile = new IniFile(fileName);

				List<IniKeyNode> labelList = null;

				if (iniFile.TryGetKeyList("Default", out labelList))
				{
					LogFile.VerboseWriteLine("TryGetKeyList Default triggered");

					foreach (IniKeyNode iniKey in labelList)
						Globals.EmulatorManager.Labels.Add(new EmuLabel(iniKey.Key, iniKey.Value));
				}

				if (iniFile.TryGetKeyList(Settings.General.AllowBracketedGameNames ? romName : StringTools.RemoveBrackets(romName), out labelList))
				{
					LogFile.VerboseWriteLine("TryGetKeyList remove brackets triggered. Keys and values:");

					foreach (IniKeyNode iniKey in labelList)
					{
						LogFile.VerboseWriteLine("Key: " + iniKey.Key + " | Value: " + iniKey.Value);
						Globals.EmulatorManager.Labels.Add(new EmuLabel(iniKey.Key, iniKey.Value));
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ParseLabelFile", "ProgramManager", ex.Message, ex.StackTrace);
			}
		}

		public void LoadMAMEData()
		{
			try
			{
				LogFile.WriteLine("Reading MAME Data");

				GetMAMEFolders();

				if (!File.Exists(Settings.Files.MAME.MAMEExe))
				{
					LogFile.WriteLine(String.Format("MAME Exe Missing '{0}'", Settings.Files.MAME.MAMEExe));

					return;
				}

				if (Globals.MAMEXml != null)
				{
					Globals.MAMEXml.Dispose();
					Globals.MAMEXml = null;
				}
				if (Globals.MAMEIni != null)
				{
					Globals.MAMEIni.Dispose();
					Globals.MAMEIni = null;
				}
				if (Globals.MAMECfg != null)
				{
					Globals.MAMECfg.Dispose();
					Globals.MAMECfg = null;
				}

				ProcessTools.TryKillProcess(Settings.Files.MAME.MAMEExe);

				Globals.MAMEXml = new MAMEXml();

				if (!MAMEFilter.TryLoadXml(Settings.Files.FilterXml, out Globals.MAMEFilter))
				{
					Globals.MAMEFilter = new MAMEFilter();

					MAMEFilter.TrySaveXml(Settings.Files.FilterXml, Globals.MAMEFilter);
				}

				string mameVersion = null;

				MAMEXml.TryGetMAMEVersion(Settings.Files.MAME.MAMEExe, out mameVersion);

				if (Settings.LastWriteTime.MAME != ConfigIO.GetLastWriteTime(Settings.Files.MAME.MAMEExe) ||
					Settings.LastWriteTime.ControlsDat != ConfigIO.GetLastWriteTime(Settings.Files.ControlsDat) ||
					Settings.LastWriteTime.ColorsIni != ConfigIO.GetLastWriteTime(Settings.Files.ColorsIni) ||
					Settings.LastWriteTime.CatVer != ConfigIO.GetLastWriteTime(Settings.Files.CatVer) ||
					Settings.LastWriteTime.NPlayers != ConfigIO.GetLastWriteTime(Settings.Files.NPlayers) ||
					Settings.LastWriteTime.HallOfFame != ConfigIO.GetLastWriteTime(Settings.Files.HallOfFame) ||
					Settings.MAME.Version != mameVersion ||
					!File.Exists(Settings.Files.MiniInfo))
				{
					MAMEXml.TryGetMAMEVersion(Settings.Files.MAME.MAMEExe, out mameVersion);

					Settings.MAME.Version = mameVersion;

					SetMAMESettings();

					if (File.Exists(Settings.Files.ListInfo))
						File.Delete(Settings.Files.ListInfo);

					CreateMiniInfo();
				}
				else
				{
					LogFile.WriteLine("Reading MiniInfo Xml");
					Globals.MAMEXml.ReadListInfoXml(Settings.Files.MAME.MAMEExe, Settings.Files.MiniInfo);
				}

				LogFile.WriteLine("MAME Version " + Settings.MAME.Version);

				LogFile.WriteLine("Reading MAME Ini");
				Globals.MAMEIni = new MAMEIni();

				Globals.MAMEIni.ReadMAMEIni(Settings.Files.MAME.MAMEIni);

				LogFile.WriteLine("Initializing MAME Cfg");
				Globals.MAMECfg = new MAMECfg();

				LogFile.WriteLine("Initializing MAME Command Line");
				Globals.MAMEOptions = new MAMEOptions();

				if (!Settings.General.DynamicDataLoading)
				{
					if (Settings.Data.MAME.GameHistory)
					{
						LogFile.WriteLine("Reading History Dat");
						Globals.HistoryDat.ReadHistoryDat(Settings.Files.HistoryDat);
					}

					if (Settings.Data.MAME.MAMEInfo)
					{
						LogFile.WriteLine("Reading MAMEInfo Dat");
						Globals.MAMEInfoDat.ReadMAMEInfoDat(Settings.Files.MAMEInfoDat);
					}

					if (Settings.Data.MAME.ControlInfo)
					{
						LogFile.WriteLine("Reading Command Dat");
						Globals.CommandDat.ReadCommandDat(Settings.Files.CommandDat);
					}

					if (Settings.Data.MAME.HighScore)
					{
						LogFile.WriteLine("Reading Story Dat");
						Globals.StoryDat.ReadStoryDat(Settings.Files.StoryDat);
					}

					LogFile.WriteLine("Reading All MAME Cfg's");
					Globals.MAMECfg.ReadMAMECfg(Settings.Folders.MAME.Cfg, null);

					//LogFile.WriteLine("Reading All MAME Ini's");
					//Global.MAMEIni.ReadGameIni(Settings.Folders.MAME.Ini, null);
				}

				string ctrlrName = null;

				if (Globals.MAMEIni.MAMEIniDictionary.TryGetValue("ctrlr", out ctrlrName))
				{
					if (!String.IsNullOrEmpty(ctrlrName))
					{
						if (Settings.Folders.MAME.Ctrlr != null)
						{
							ctrlrName = Path.ChangeExtension(ctrlrName, ".cfg");
							LogFile.WriteLine(String.Format("Reading MAME Ctrlr File '{0}'", ctrlrName));
							Settings.Files.Ctrlr = Path.Combine(Settings.Folders.MAME.Ctrlr, ctrlrName);
							Globals.MAMECfg.ReadMAMECtrlr(Settings.Files.Ctrlr);
						}
					}
				}

				LogFile.WriteLine("Getting Parent ROMs");
				Globals.MAMEXml.GetParentROMs();

				LogFile.WriteLine("Creating Game List");
				Globals.MAMEXml.CreateGameList();

				Globals.MAMEXml.GameList.Sort();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("LoadMAMEData", "ProgramManager", ex.Message, ex.StackTrace);
			}
		}

		public void CreateMiniInfo()
		{
			try
			{
				LogFile.WriteLine("Reading Controls Xml");
				Globals.ControlsDat = new ControlsDat();
				Globals.ControlsDat.ReadControlsXml(Settings.Files.ControlsDat);

				LogFile.WriteLine("Reading Colors Ini");
				Globals.ColorsIni = new ColorsIni();
				Globals.ColorsIni.ReadColorsIni(Settings.Files.ColorsIni);

				LogFile.WriteLine("Adding Colors to ControlsDat");
				Globals.ControlsDat.AddColorsIni();

				LogFile.WriteLine("Reading CatVer Ini");
				Globals.CatVer = new CatVer();
				Globals.CatVer.ReadCatVerIni(Settings.Files.CatVer);

				LogFile.WriteLine("Reading NPlayers Ini");
				Globals.NPlayers = new NPlayers();
				Globals.NPlayers.ReadNPlayersIni(Settings.Files.NPlayers);

				LogFile.WriteLine("Reading Hall Of Fame Xml");
				Globals.HallOfFameXml = new HallOfFame();
				Globals.HallOfFameXml.ReadHallOfFameXml(Settings.Files.HallOfFame);

				if (!File.Exists(Settings.Files.ListInfo))
				{
					LogFile.WriteLine("Creating ListInfo Xml");

					Globals.MAMEXml.CreateListInfoXml(Settings.Files.MAME.MAMEExe, Settings.Files.ListInfo);
				}

				LogFile.WriteLine("Reading ListInfo Xml");
				Globals.MAMEXml.ReadListInfoXml(Settings.Files.MAME.MAMEExe, Settings.Files.ListInfo);

				LogFile.WriteLine("Adding Data to MAME Xml");
				Globals.MAMEXml.AddData();

				LogFile.WriteLine("Writing MiniInfo Xml");
				Globals.MAMEXml.WriteMiniInfoXml(Settings.Files.MiniInfo);

				if (Globals.ControlsDat != null)
				{
					Globals.ControlsDat.Dispose();
					Globals.ControlsDat = null;
				}
				if (Globals.ColorsIni != null)
				{
					Globals.ColorsIni.Dispose();
					Globals.ColorsIni = null;
				}
				if (Globals.CatVer != null)
				{
					Globals.CatVer.Dispose();
					Globals.CatVer = null;
				}
				if (Globals.NPlayers != null)
				{
					Globals.NPlayers.Dispose();
					Globals.NPlayers = null;
				}
				if (Globals.HallOfFameXml != null)
				{
					Globals.HallOfFameXml.Dispose();
					Globals.HallOfFameXml = null;
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("CreateMiniInfo", "ProgramManager", ex.Message, ex.StackTrace);
			}
		}

		public void SetMAMESettings()
		{
			try
			{
				string MAMEVersion = Settings.MAME.Version;

				if (String.IsNullOrEmpty(MAMEVersion))
					return;

				LogFile.WriteLine("Setting Options For MAME Version " + MAMEVersion);

				if (MAMEVersion.Contains("u"))
					MAMEVersion = MAMEVersion.Substring(0, MAMEVersion.IndexOf('u'));

				MAMEVersion = StringTools.RemoveAlpha(MAMEVersion);

				float Version = StringTools.FromString<float>(MAMEVersion);

				if (Version != 0f)
				{
					// MAME Output System introduced (108)

					if (Version > 0.112f) // Pause in MAME Output System introduced (112u2)
					{
						//Settings.MAME.UseMAMEOutputSystem = true;
						//Settings.MAME.SendPauseKey = false;
					}
					else
					{
						//Settings.MAME.UseMAMEOutputSystem = false;
						//Settings.MAME.SendPauseKey = true;
					}

					if (Version > 0.118f) // Raw Input System introduced (118)
					{
						//Settings.MAME.SendPauseKey = false;
						//Settings.MAME.SkipDisclaimer = false;
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("SetMAMESettings", "ProgramManager", ex.Message, ex.StackTrace);
			}
		}

		private void GetMAMEFolders()
		{
			if (File.Exists(Settings.Files.MAME.MAMEExe))
			{
				Settings.Folders.MAME.Root = Path.GetDirectoryName(Settings.Files.MAME.MAMEExe);
				Settings.Files.MAME.MAMEIni = Path.Combine(Settings.Folders.MAME.Root, "mame.ini");

				if (File.Exists(Settings.Files.MAME.MAMEIni))
					return;
			}

			if (!String.IsNullOrEmpty(Settings.Folders.MAME.Ini))
			{
				if (Directory.Exists(Settings.Folders.MAME.Ini))
				{
					Settings.Files.MAME.MAMEIni = Path.Combine(Settings.Folders.MAME.Ini, "mame.ini");
				}
			}
		}

		public void UpdateData()
		{
			try
			{
				GetMAMEFolders();

				if (Settings.General.RunOnStartup)
					AddProgramToStartup();
				else
					RemoveProgramFromStartup();

				StartMAMEInterop();

				//Global.ArtworkManager.UpdateFolders();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("UpdateData", "ProgramManager", ex.Message, ex.StackTrace);
			}
		}

		public void AddProgramToStartup()
		{
			try
			{
				RegistryKey RegKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
				RegKey.SetValue("CPWizard", Application.ExecutablePath + " -minimized", RegistryValueKind.String);
				RegKey.Close();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("AddProgramToStartup", "ProgramManager", ex.Message, ex.StackTrace);
			}
		}

		public void RemoveProgramFromStartup()
		{
			try
			{
				RegistryKey RegKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
				RegKey.DeleteValue("CPWizard", false);
				RegKey.Close();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("RemoveProgramFromStartup", "ProgramManager", ex.Message, ex.StackTrace);
			}
		}

		public bool LoadLayout(string FileName, ref Layout layout, ref List<Layout> layoutArray, bool sub)
		{
			try
			{
				if (!File.Exists(FileName))
					return false;

				if (!sub)
				{
					if (layout != null)
					{
						if (layout.PromptToSave)
						{
							if (MessageBox.Show("Save Layout?", "Save Layout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
								Globals.MainForm.SaveLayout();
							else
								layout.PromptToSave = false;
						}

						Globals.SelectedObjectList.Clear();
					}
				}

				if (layoutArray != null)
				{
					for (int i = 0; i < layoutArray.Count; i++)
					{
						if (layoutArray[i] != null)
						{
							layoutArray[i].Dispose();
							layoutArray[i] = null;
						}
					}

					layoutArray = null;
				}

				string name = Path.GetFileNameWithoutExtension(FileName);
				Regex regEx = new Regex(@"\s\(\d\)");

				if (regEx.IsMatch(name))
				{
					layoutArray = new List<Layout>();
					string fileName = regEx.Replace(name, "");

					DirectoryInfo di = new DirectoryInfo(Settings.Folders.Layout);

					FileInfo[] fiArray = di.GetFiles(String.Format("{0} (*.xml", fileName));
					List<string> fileList = new List<string>();

					foreach (FileInfo fi in fiArray)
						fileList.Add(fi.FullName);

					fileList.Sort();

					foreach (string file in fileList)
					{
						Layout newLayout = new Layout();

						if (newLayout.LoadLayoutXml(file))
						{
							if (Path.GetFileName(FileName) == Path.GetFileName(file))
								layout = newLayout;

							layoutArray.Add(newLayout);
						}
						else
							newLayout.Dispose();
					}
				}

				if (layoutArray != null)
					if (layoutArray.Count == 0)
						layoutArray = null;

				if (layoutArray == null)
				{
					layout = new Layout();

					layout.LoadLayoutXml(FileName);
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("LoadLayout", "ProgramManager", ex.Message, ex.StackTrace);

				return false;
			}

			return true;
		}

		public void ImportLayout(string FileName)
		{
			try
			{
				if (!File.Exists(FileName))
					return;

				if (Globals.Layout == null)
					return;

				if (Settings.Layout.Name == FileIO.GetRelativeFolder(Settings.Folders.Layout, FileName, true))
					return;

				Layout Layout = new Layout();

				if (Layout.LoadLayoutXml(FileName))
				{
					foreach (LayoutObject layoutObject in Layout.LayoutObjectList)
						Globals.Layout.LayoutObjectList.Add((LayoutObject)layoutObject.Clone());
				}

				Layout.Dispose();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ImportLayout", "ProgramManager", ex.Message, ex.StackTrace);
			}
		}

		public void LoadDataDynamically(string romName)
		{
			try
			{
				LogFile.VerboseWriteLine("LoadDataDynamically", "ProgramManager", "Reading Game History");

				if (Settings.Data.MAME.GameHistory)
					Globals.HistoryDat.ReadHistoryDat(Settings.Files.HistoryDat, romName);

				LogFile.VerboseWriteLine("LoadDataDynamically", "ProgramManager", "Reading MAME Info");

				if (Settings.Data.MAME.MAMEInfo)
					Globals.MAMEInfoDat.ReadMAMEInfoDat(Settings.Files.MAMEInfoDat, romName);

				LogFile.VerboseWriteLine("LoadDataDynamically", "ProgramManager", "Reading Control Info");

				if (Settings.Data.MAME.ControlInfo)
					Globals.CommandDat.ReadCommandDat(Settings.Files.CommandDat, romName);

				LogFile.VerboseWriteLine("LoadDataDynamically", "ProgramManager", "Reading High Score");

				if (Settings.Data.MAME.HighScore)
					Globals.StoryDat.ReadStoryDat(Settings.Files.StoryDat, romName);

				LogFile.VerboseWriteLine("LoadDataDynamically", "ProgramManager", "Reading List Info");

				if (Settings.Files.MAME.MAMEExe != null)
					Globals.MAMEXml.ReadListInfoXml(Settings.Files.MAME.MAMEExe, Settings.Files.MiniInfo, romName);

				LogFile.VerboseWriteLine("LoadDataDynamically", "ProgramManager", "Reading MAME Cfg");

				if (Settings.Folders.MAME.Cfg != null)
					Globals.MAMECfg.ReadMAMECfg(Settings.Folders.MAME.Cfg, romName);

				//LogFile.VerboseWriteLine("LoadDataDynamically", "ProgramManager", "Reading MAME Ini");

				//if(Settings.Folders.MAME.Ini!= null)
				//    Global.MAMEIni.ReadGameIni(Settings.Folders.MAME.Ini, ROMName);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("LoadDataDynamically", "ProgramManager", ex.Message, ex.StackTrace);
			}
		}

		public void ReadAllGameData()
		{
			if (Settings.General.DynamicDataLoading)
			{
				Globals.MAMEXml.ReadListInfoXml(Settings.Files.MAME.MAMEExe, Settings.Files.MiniInfo);
				Globals.MAMEXml.GameList.Sort();
			}
		}

		/* public void GetGameDetails(string GameName)
		{
			try
			{

			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ShowPreview", "ProgramManager", ex.Message, ex.StackTrace);
			}
		} */

		public void TakeScreenshot()
		{
			LogFile.VerboseWriteLine("TakeScreenshot", "ProgramManager", "Taking Screenshot");

			Globals.ScreenshotList = ScreenManager.GetScreenshotList();
		}

		public void ShowLoadingScreens(bool showScreenshot)
		{
			lock (this)
			{
				if (!Settings.Display.ShowLoadingScreens)
					return;

				LogFile.VerboseWriteLine("ShowLoadingScreens", "ProgramManager", "Showing Loading Screens");

				Settings.Display.ShowScreenshot = showScreenshot;

				if (Globals.LoadingFormList == null)
					Globals.LoadingFormList = new List<frmLoading>();

				Globals.LoadingFormList.Clear();

				for (int i = 0; i < Globals.ScreenshotList.Count; i++)
				{
					frmLoading loadingForm = new frmLoading();
					Globals.LoadingFormList.Add(loadingForm);

					LogFile.VerboseWriteLine("ShowLoadingScreens", "ProgramManager", "Showing Loading Form: " + i.ToString());

					loadingForm.ShowLoading(i, showScreenshot);
				}

				using (Graphics g = Graphics.FromImage(Globals.MainBitmap))
					Globals.DisplayManager.DrawScreenshot(g, Globals.MainBitmap.Width, Globals.MainBitmap.Height, showScreenshot, 1.0f);

				//Application.DoEvents();
			}
		}

		public void SetLoadingScreensNoTopMost()
		{
			lock (this)
			{
				if (!Settings.Display.ShowLoadingScreens)
					return;

				LogFile.VerboseWriteLine("SetLoadingScreensNoTopMost", "ProgramManager", "Settings Loading Screens NoTopMost");

				if (Globals.LoadingFormList != null)
				{
					foreach (frmLoading loadingForm in Globals.LoadingFormList)
						loadingForm.TopMost = false;
				}
			}
		}

		public void CloseLoadingScreens()
		{
			lock (this)
			{
				if (!Settings.Display.ShowLoadingScreens)
					return;

				LogFile.VerboseWriteLine("CloseLoadingScreens", "ProgramManager", "Closing Loading Screens");

				if (Globals.LoadingFormList == null)
					return;

				foreach (frmLoading loadingForm in Globals.LoadingFormList)
					loadingForm.Close();

				Globals.LoadingFormList.Clear();
			}
		}

		public bool Show(bool showCPOnly, bool exitToMenu)
		{
			try
			{
				lock (this)
				{
					if (Globals.LayoutShowing)
						return false;

					//if (!Settings.General.Minimized)
					//    return false;
					LogFile.VerboseWriteLine("Show", "ProgramManager", "Show");

					if (Globals.EmulatorMode == EmulatorMode.MAME && Settings.MAME.Running)
					{
						DisplayRotation = Settings.Display.Rotation;

						LogFile.VerboseWriteLine("Show", "ProgramManager", "MAME Running");

						if (Settings.MAME.UseMAMEOutputSystem)
						{
							LogFile.VerboseWriteLine("Show", "ProgramManager", "Searching for MAME");
							Globals.MAMEManager.FindMAME();
						}

						if (!Settings.MAME.Running || Settings.MAME.GameName == null)
							return false;

						LogFile.VerboseWriteLine("Show", "ProgramManager", "Game Found: " + Settings.MAME.GameName);

						LogFile.VerboseWriteLine("Show", "ProgramManager", "Getting Game Details");

						if (Settings.General.DynamicDataLoading)
							LoadDataDynamically(Settings.MAME.GameName);

						Globals.MAMEManager.GetGameDetails(true, true, false);

						if (Settings.Display.AutoRotate && Settings.MAME.MachineNode != null)
						{
							bool isVertical = Settings.MAME.MachineNode.IsVertical;

							Settings.Display.Rotation = (isVertical ? (Settings.Display.RotateLeft ? 270 : 90) : 0);
						}

						if (Settings.MAME.SendPause)
						{
							LogFile.VerboseWriteLine("Show", "ProgramManager", "Sending Pause Key");
							Globals.MAMEManager.PauseMAME();
						}

						TakeScreenshot();

						LogFile.VerboseWriteLine("Show", "ProgramManager", "Minimizing MAME");

						ShowLoadingScreens(Settings.MAME.Screenshot);

						Globals.MAMEManager.MinimizeMAME();

						SetLoadingScreensNoTopMost();

						if (Globals.LayoutForm == null)
							Globals.LayoutForm = new frmLayout();

						LogFile.VerboseWriteLine("Show", "ProgramManager", "Showing Layout Form");

						Globals.LayoutForm.Show(DisplayType.Show, -1, -1, false);
					}
					else if (Globals.EmulatorMode == EmulatorMode.Emulator && Settings.Emulator.Running)
					{
						LogFile.VerboseWriteLine("Show", "ProgramManager", "Emulator Running: " + Settings.Emulator.Profile.Name);

						if (Settings.Emulator.GameName == null)
							return false;

						LogFile.VerboseWriteLine("Show", "ProgramManager", "Game Found: " + Settings.Emulator.GameName);

						if (Settings.Emulator.Profile == null)
							return false;

						Globals.EmulatorManager.Labels.Clear();

						LogFile.VerboseWriteLine("Show", "ProgramManager", "Reading Database");

						if (!String.IsNullOrEmpty(Settings.Emulator.Profile.Database))
							GetGameInfoData(Path.Combine(Settings.Folders.Database, Settings.Emulator.Profile.Database + ".mdb"), Settings.Emulator.GameName);

						LogFile.VerboseWriteLine("Show", "ProgramManager", "Parsing Labels");

						if (!String.IsNullOrEmpty(Settings.Emulator.Profile.Labels))
							ParseLabelFile(Path.Combine(Settings.Folders.Labels, Settings.Emulator.Profile.Labels + ".ini"), Settings.Emulator.GameName);

						LogFile.VerboseWriteLine("Show", "ProgramManager", "Getting Game Details");

						Globals.EmulatorManager.GetGameDetails(true, false);

						LogFile.VerboseWriteLine("Show", "ProgramManager", "Sending Keys");

						Globals.SendKeys.SendKeyString(Settings.Emulator.Profile.ShowSendKeys);

						TakeScreenshot();

						LogFile.VerboseWriteLine("Show", "ProgramManager", "Minimizing Emulator");

						ShowLoadingScreens(Settings.Emulator.Profile.Screenshot);

						Globals.EmulatorManager.MinimizeEmu();

						SetLoadingScreensNoTopMost();

						if (Globals.LayoutForm == null)
							Globals.LayoutForm = new frmLayout();

						LogFile.VerboseWriteLine("Show", "ProgramManager", "Showing Layout Form");

						Globals.LayoutForm.Show(DisplayType.Show, -1, -1, false);
					}
					else
						return false;

					if (showCPOnly)
					{
						Globals.DisplayMode = DisplayMode.Layout;

						LogFile.VerboseWriteLine("Show", "ProgramManager", "Show CP Only is True");
						LogFile.VerboseWriteLine("Show", "ProgramManager", "Exit To Menu is " + exitToMenu.ToString());
						LogFile.VerboseWriteLine("Show", "ProgramManager", "Display Mode is " + Globals.DisplayModeString[(int)Globals.DisplayMode]);

						ShowScreen(exitToMenu);
					}
					else
					{
						Globals.DisplayMode = DisplayMode.MainMenu;

						LogFile.VerboseWriteLine("Show", "ProgramManager", "Show CP Only is False");
						LogFile.VerboseWriteLine("Show", "ProgramManager", "Exit To Menu is True");
						LogFile.VerboseWriteLine("Show", "ProgramManager", "Display Mode is " + Globals.DisplayModeString[(int)Globals.DisplayMode]);

						ShowScreen(true);
					}

					System.Windows.Forms.Application.DoEvents();

					return true;
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("Show", "ProgramManager", ex.Message, ex.StackTrace);
			}

			return false;
		}

		public bool ShowSub()
		{
			try
			{
				lock (this)
				{
					//if (Global.LayoutSubShowing)
					//    return false;

					LogFile.VerboseWriteLine("ShowSub", "ProgramManager", "Show");

					if (Globals.EmulatorMode == EmulatorMode.MAME && Settings.MAME.Running)
					{
						DisplayRotation = Settings.Display.Rotation;

						LogFile.VerboseWriteLine("ShowSub", "ProgramManager", "MAME Running");

						if (Settings.MAME.UseMAMEOutputSystem)
						{
							LogFile.VerboseWriteLine("ShowSub", "ProgramManager", "Searching for MAME");
							Globals.MAMEManager.FindMAME();
						}

						if (!Settings.MAME.Running || Settings.MAME.GameName == null)
							return false;

						LogFile.VerboseWriteLine("ShowSub", "ProgramManager", "Game Found: " + Settings.MAME.GameName);

						LogFile.VerboseWriteLine("ShowSub", "ProgramManager", "Getting Game Details");

						if (Settings.General.DynamicDataLoading)
							LoadDataDynamically(Settings.MAME.GameName);

						Globals.MAMEManager.GetGameDetails(true, true, true);

						if (Settings.Display.AutoRotate && Settings.MAME.MachineNode != null)
						{
							bool Vertical = Settings.MAME.MachineNode.IsVertical;

							Settings.Display.Rotation = (Vertical ? (Settings.Display.RotateLeft ? 270 : 90) : 0);
						}

						TakeScreenshot();

						SetLoadingScreensNoTopMost();

						if (Globals.LayoutSubForm == null)
							Globals.LayoutSubForm = new frmLayout();

						LogFile.VerboseWriteLine("ShowSub", "ProgramManager", "Showing LayoutSub Form");

						Globals.LayoutSubForm.Show(DisplayType.Show, -1, -1, true);
					}
					else if (Globals.EmulatorMode == EmulatorMode.Emulator && Settings.Emulator.Running)
					{
						LogFile.VerboseWriteLine("ShowSub", "ProgramManager", "Emulator Running: " + Settings.Emulator.Profile.Name);

						if (Settings.Emulator.GameName == null)
							return false;

						LogFile.VerboseWriteLine("ShowSub", "ProgramManager", "Game Found: " + Settings.Emulator.GameName);

						if (Settings.Emulator.Profile == null)
							return false;

						Globals.EmulatorManager.Labels.Clear();

						LogFile.VerboseWriteLine("ShowSub", "ProgramManager", "Reading Database");

						if (!String.IsNullOrEmpty(Settings.Emulator.Profile.Database))
							GetGameInfoData(Path.Combine(Settings.Folders.Database, Settings.Emulator.Profile.Database + ".mdb"), Settings.Emulator.GameName);

						LogFile.VerboseWriteLine("ShowSub", "ProgramManager", "Parsing Labels");

						if (!String.IsNullOrEmpty(Settings.Emulator.Profile.Labels))
							ParseLabelFile(Path.Combine(Settings.Folders.Labels, Settings.Emulator.Profile.Labels + ".ini"), Settings.Emulator.GameName);

						LogFile.VerboseWriteLine("ShowSub", "ProgramManager", "Getting Game Details");

						Globals.EmulatorManager.GetGameDetails(true, true);

						TakeScreenshot();

						SetLoadingScreensNoTopMost();

						if (Globals.LayoutSubForm == null)
							Globals.LayoutSubForm = new frmLayout();

						LogFile.VerboseWriteLine("ShowSub", "ProgramManager", "Showing LayoutSub Form");

						Globals.LayoutSubForm.Show(DisplayType.Show, -1, -1, true);
					}
					else
						return false;

					System.Windows.Forms.Application.DoEvents();

					return true;
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ShowSub", "ProgramManager", ex.Message, ex.StackTrace);
			}

			return false;
		}

		public void SetExitToMenu(bool value)
		{
			Globals.MainMenu.ExitToMenu = value;
			Globals.LayoutManager.ExitToMenu = value;
			Globals.GameInfo.ExitToMenu = value;
			Globals.HistoryDat.ExitToMenu = value;
			Globals.MAMEInfoDat.ExitToMenu = value;
			Globals.CommandDat.ExitToMenu = value;
			Globals.StoryDat.ExitToMenu = value;
			Globals.HiToText.ExitToMenu = value;
			Globals.ArtworkManager.ExitToMenu = value;
			Globals.MAMEManual.ExitToMenu = value;
			Globals.EmulatorManual.ExitToMenu = value;
			Globals.EmulatorOpCard.ExitToMenu = value;
			Globals.IRC.ExitToMenu = value;
		}

		public void ShowScreen(DisplayMode displayMode, bool layoutOverride, bool exitToMenu, bool dynamicLoad)
		{
			LogFile.VerboseWriteLine("ShowScreen", "ProgramManager", String.Format("ShowScreen: {0} (Display Mode) {1} (Layout Override) {2} (Exit To Menu) {3} (Dynamic Load)", Globals.DisplayModeString[(int)displayMode], layoutOverride.ToString(), exitToMenu.ToString(), dynamicLoad.ToString()));

			switch (Globals.EmulatorMode)
			{
				case EmulatorMode.Emulator:
					LogFile.VerboseWriteLine("ShowScreen", "ProgramManager", "Emulator Mode");

					if (Settings.Emulator.Profile != null)
					{
						LogFile.VerboseWriteLine("ShowScreen", "ProgramManager", "Clearing Labels");
						Globals.EmulatorManager.Labels.Clear();

						LogFile.VerboseWriteLine("ShowScreen", "ProgramManager", "Getting Game Info Data");

						GetGameInfoData(Path.Combine(Settings.Folders.Database, Settings.Emulator.Profile.Database + ".mdb"), Settings.Emulator.GameName);

						LogFile.VerboseWriteLine("ShowScreen", "ProgramManager", "Parsing Label File");

						ParseLabelFile(Path.Combine(Settings.Folders.Labels, Settings.Emulator.Profile.Labels + ".ini"), Settings.Emulator.GameName);

						LogFile.VerboseWriteLine("ShowScreen", "ProgramManager", "Getting Game Details");

						Globals.EmulatorManager.GetGameDetails(!layoutOverride, false);
					}
					break;
				case EmulatorMode.MAME:
					LogFile.VerboseWriteLine("ShowScreen", "ProgramManager", "MAME Mode");

					if (Settings.General.DynamicDataLoading || dynamicLoad)
					{
						LogFile.VerboseWriteLine("ShowScreen", "ProgramManager", "Loading Data Dynamically");
						LoadDataDynamically(Settings.MAME.GameName);
					}

					LogFile.VerboseWriteLine("ShowScreen", "ProgramManager", "Getting Game Details");

					Globals.MAMEManager.GetGameDetails(true, !layoutOverride, false);
					break;
			}

			if (Globals.LayoutForm == null)
				Globals.LayoutForm = new frmLayout();

			LogFile.VerboseWriteLine("ShowScreen", "ProgramManager", "Showing Layout Form");

			Globals.LayoutForm.Show(DisplayType.Show, -1, -1, false);

			Globals.DisplayMode = displayMode;

			LogFile.VerboseWriteLine("ShowScreen", "ProgramManager", "Display Mode is " + Globals.DisplayModeString[(int)Globals.DisplayMode]);

			ShowScreen(exitToMenu);
		}

		public void ShowScreen(bool exitToMenu)
		{
			LogFile.VerboseWriteLine("ShowScreen", "ProgramManager", "Hiding Main Menu");

			Globals.MainMenu.Hide();

			LogFile.VerboseWriteLine("ShowScreen", "ProgramManager", "Resetting Menu");

			Globals.MainMenu.Reset(Globals.EmulatorMode);

			LogFile.VerboseWriteLine("ShowScreen", "ProgramManager", "Setting Exit To Menu " + exitToMenu.ToString());

			SetExitToMenu(exitToMenu);

			LogFile.VerboseWriteLine("ShowScreen", "ProgramManager", "Showing " + Globals.DisplayModeString[(int)Globals.DisplayMode]);

			switch (Globals.DisplayMode)
			{
				case DisplayMode.MainMenu:
					Globals.MainMenu.Show();
					break;
				case DisplayMode.Layout:
					Globals.LayoutManager.Show();
					break;
				case DisplayMode.GameInfo:
					Globals.GameInfo.Show();
					break;
				case DisplayMode.HistoryDat:
					Globals.HistoryDat.Show();
					break;
				case DisplayMode.MAMEInfoDat:
					Globals.MAMEInfoDat.Show();
					break;
				case DisplayMode.CommandDat:
					Globals.CommandDat.Show();
					break;
				case DisplayMode.StoryDat:
					Globals.StoryDat.Show();
					break;
				case DisplayMode.Artwork:
					Globals.ArtworkManager.Show();
					break;
				case DisplayMode.Manual:
					if (Globals.EmulatorMode == EmulatorMode.MAME)
						Globals.MAMEManual.Show();
					else
						Globals.EmulatorManual.Show();
					break;
				case DisplayMode.OpCard:
					Globals.EmulatorOpCard.Show();
					break;
				case DisplayMode.IRC:
					Globals.IRC.Show();
					break;
			}
		}

		public void Hide()
		{
			try
			{
				lock (this)
				{
					System.Diagnostics.Debug.WriteLine("Hide");

					//Global.DisplayMode = DisplayMode.LayoutEditor;
					//Global.LayoutManager.ResetControls();
					//Global.MainMenu.Hide();

					if (Globals.LayoutForm != null)
						Globals.LayoutForm.Close();

					if (Settings.MAME.Running)
					{
						if (Settings.Display.AutoRotate)
							Settings.Display.Rotation = DisplayRotation;

						Globals.MAMEManager.RestoreMAME();

						if (Settings.MAME.SendPause)
							Globals.MAMEManager.UnPauseMAME();
					}
					else if (Settings.Emulator.Running)
					{
						Globals.EmulatorManager.RestoreEmu();

						Globals.SendKeys.SendKeyString(Settings.Emulator.Profile.HideSendKeys);
					}

					CloseLoadingScreens();

					Globals.InterComm.SendMessage((int)DataMode.Exit, String.Empty);

					if (Settings.InterComm.Exit)
					{
						Globals.MainForm.CloseType = frmMain.CloseMethodType.Auto;
						Globals.MainForm.Close();
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("Hide", "ProgramManager", ex.Message, ex.StackTrace);
			}
		}

		public void CreateBitmaps()
		{
			try
			{
				LogFile.VerboseWriteLine("Creating MainBitmap");
				Globals.MainBitmap = new Bitmap(Settings.General.MainScreenSize.Width, Settings.General.MainScreenSize.Height);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("CreateBitmaps", "ProgramManager", ex.Message, ex.StackTrace);
			}
		}

		public void StartMAMEInterop()
		{
			try
			{
				if (Settings.MAME.UseMAMEOutputSystem)
				{
					if (!Globals.MAMEInterop.IsRunning)
					{
						LogFile.WriteLine("Starting MAME Interop");
						Globals.MAMEInterop.Initialize(7777, "CPWizard", true);
					}
				}
				else
				{
					if (Globals.MAMEInterop.IsRunning)
					{
						LogFile.WriteLine("Shutting Down MAME Interop");

						Globals.MAMEInterop.Shutdown();
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("StartMAMEInterop", "ProgramManager", ex.Message, ex.StackTrace);
			}
		}

		#region IDisposable Members

		public void Dispose()
		{
			if (Globals.Layout != null)
			{
				Globals.Layout.Dispose();
				Globals.Layout = null;
			}

			if (Globals.ControlsDat != null)
			{
				Globals.ControlsDat.Dispose();
				Globals.ControlsDat = null;
			}

			if (Globals.ColorsIni != null)
			{
				Globals.ColorsIni.Dispose();
				Globals.ColorsIni = null;
			}

			if (Globals.CatVer != null)
			{
				Globals.CatVer.Dispose();
				Globals.CatVer = null;
			}

			if (Globals.NPlayers != null)
			{
				Globals.NPlayers.Dispose();
				Globals.NPlayers = null;
			}

			if (Globals.MAMEXml != null)
			{
				Globals.MAMEXml.Dispose();
				Globals.MAMEXml = null;
			}

			if (Globals.DisplayManager != null)
			{
				Globals.DisplayManager.Dispose();
				Globals.DisplayManager = null;
			}

			if (Globals.MAMEInterop != null)
			{
				Globals.MAMEInterop.Dispose();
				Globals.MAMEInterop = null;
			}

			if (Globals.SendKeys != null)
			{
				Globals.SendKeys.Dispose();
				Globals.SendKeys = null;
			}

			if (Globals.MAMEManager != null)
			{
				Globals.MAMEManager.Dispose();
				Globals.MAMEManager = null;
			}

			if (Globals.EmulatorManager != null)
			{
				Globals.EmulatorManager.Dispose();
				Globals.EmulatorManager = null;
			}

			if (Globals.MainMenu != null)
			{
				Globals.MainMenu.Dispose();
				Globals.MainMenu = null;
			}

			if (Globals.LayoutManager != null)
			{
				Globals.LayoutManager.Dispose();
				Globals.LayoutManager = null;
			}

			if (Globals.GameInfo != null)
			{
				Globals.GameInfo.Dispose();
				Globals.GameInfo = null;
			}

			if (Globals.HistoryDat != null)
			{
				Globals.HistoryDat.Dispose();
				Globals.HistoryDat = null;
			}

			if (Globals.MAMEInfoDat != null)
			{
				Globals.MAMEInfoDat.Dispose();
				Globals.MAMEInfoDat = null;
			}

			if (Globals.CommandDat != null)
			{
				Globals.CommandDat.Dispose();
				Globals.CommandDat = null;
			}

			if (Globals.StoryDat != null)
			{
				Globals.StoryDat.Dispose();
				Globals.StoryDat = null;
			}

			if (Globals.ArtworkManager != null)
			{
				Globals.ArtworkManager.Dispose();
				Globals.ArtworkManager = null;
			}

			if (Globals.MAMEManual != null)
			{
				Globals.MAMEManual.Dispose();
				Globals.MAMEManual = null;
			}

			if (Globals.EmulatorManual != null)
			{
				Globals.EmulatorManual.Dispose();
				Globals.EmulatorManual = null;
			}

			if (Globals.EmulatorOpCard != null)
			{
				Globals.EmulatorOpCard.Dispose();
				Globals.EmulatorOpCard = null;
			}

			if (Globals.NFOViewer != null)
			{
				Globals.NFOViewer.Dispose();
				Globals.NFOViewer = null;
			}

			if (Globals.IRC != null)
			{
				Globals.IRC.Dispose();
				Globals.IRC = null;
			}

			if (Globals.KeyboardHook != null)
			{
				Globals.KeyboardHook.Dispose();
				Globals.KeyboardHook = null;
			}

			/* if (Global.MouseHook != null)
			{
				Global.MouseHook.Dispose();
				Global.MouseHook = null;
			} */

			if (Globals.DirectInput != null)
			{
				Globals.DirectInput.Dispose();
				Globals.DirectInput = null;
			}

			if (Globals.MCERemote != null)
			{
				Globals.MCERemote.Dispose();
				Globals.MCERemote = null;
			}

			if (Globals.InputManager != null)
			{
				Globals.InputManager.Dispose();
				Globals.InputManager = null;
			}
		}

		#endregion
	}
}
