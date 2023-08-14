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
using System.Windows.Forms;

namespace CPWizard
{
	class ConfigIO
	{
		public static string GetLastWriteTime(string fileName)
		{
			try
			{
				if (String.IsNullOrEmpty(fileName))
					return null;

				if (!File.Exists(fileName))
					return null;

				DateTime LastWriteTime = File.GetLastWriteTime(fileName);

				if (LastWriteTime != null)
					return LastWriteTime.ToString();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetLastWriteTime", "ConfigIO", ex.Message, ex.StackTrace);
			}

			return null;
		}

		public static void ReadScreen()
		{
			try
			{
				IniFile iniFile = new IniFile(Settings.Files.Ini);

				Settings.Display.Screen = ScreenManager.ScreenNameToNumber(iniFile.Read("Display", "Screen", @"\\.\DISPLAY1"));
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ReadScreen", "ConfigIO", ex.Message, ex.StackTrace);
			}
		}

		public static void ReadConfig()
		{
			try
			{
				IniFile iniFile = new IniFile(Settings.Files.Ini);

				if (!File.Exists(Settings.Files.Ini))
					Settings.General.FirstRun = true;

				Settings.General.VerboseLogging = iniFile.Read<bool>("General", "VerboseLogging", false);
				Settings.General.RunOnStartup = iniFile.Read<bool>("General", "RunOnStartup", false);
				Settings.General.VolumeControlEnable = iniFile.Read<bool>("General", "VolumeControlEnable", false);
				Settings.General.DynamicDataLoading = iniFile.Read<bool>("General", "DynamicDataLoading", false);
				Settings.General.AllowBracketedGameNames = iniFile.Read<bool>("General", "AllowBracketedGameNames", true);
				Settings.General.DisableSystemSpecs = iniFile.Read<bool>("General", "DisableSystemSpecs", false);

				Settings.Files.GSExe = iniFile.Read("General", "GhostScriptExe", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), @"gs\gs\bin\gswin32c.exe"));
				Settings.Files.HiToTextExe = iniFile.Read("General", "HiToTextExe", Path.Combine(Settings.Folders.App, "HiToText.exe"));

				Settings.HideDesktop.Enable = iniFile.Read<bool>("HideDesktop", "Enable", false);
				Settings.HideDesktop.DisableScreenSaver = iniFile.Read<bool>("HideDesktop", "DisableScreenSaver", false);
				Settings.HideDesktop.HideMouseCursor = iniFile.Read<bool>("HideDesktop", "HideMouseCursor", false);
				Settings.HideDesktop.HideDesktopUsingForms = iniFile.Read<bool>("HideDesktop", "HideDesktopUsingForms", false);
				Settings.HideDesktop.SetWallpaperBlack = iniFile.Read<bool>("HideDesktop", "SetWallpaperBlack", false);
				Settings.HideDesktop.HideDesktopIcons = iniFile.Read<bool>("HideDesktop", "HideDesktopIcons", false);
				Settings.HideDesktop.HideTaskbar = iniFile.Read<bool>("HideDesktop", "HideTaskbar", false);
				Settings.HideDesktop.MoveMouseOffscreen = iniFile.Read<bool>("HideDesktop", "MoveMouseOffscreen", false);

				Settings.MAME.Version = iniFile.Read("MAME", "Version", null);
				Settings.Files.MAME.MAMEExe = iniFile.Read("MAME", "Exe", null);
				Settings.Folders.MAME.Cabinets = iniFile.Read("MAMEFolders", "Cabinets", null);
				Settings.Folders.MAME.Cfg = iniFile.Read("MAMEFolders", "Cfg", null);
				Settings.Folders.MAME.CPanel = iniFile.Read("MAMEFolders", "CPanel", null);
				Settings.Folders.MAME.Ctrlr = iniFile.Read("MAMEFolders", "Ctrlr", null);
				Settings.Folders.MAME.Flyers = iniFile.Read("MAMEFolders", "Flyers", null);
				Settings.Folders.MAME.Hi = iniFile.Read("MAMEFolders", "Hi", null);
				Settings.Folders.MAME.Icons = iniFile.Read("MAMEFolders", "Icons", null);
				Settings.Folders.MAME.Ini = iniFile.Read("MAMEFolders", "Ini", null);
				Settings.Folders.MAME.Manuals = iniFile.Read("MAMEFolders", "Manuals", null);
				Settings.Folders.MAME.Marquees = iniFile.Read("MAMEFolders", "Marquees", null);
				Settings.Folders.MAME.NvRam = iniFile.Read("MAMEFolders", "NvRam", null);
				Settings.Folders.MAME.PCB = iniFile.Read("MAMEFolders", "PCB", null);
				Settings.Folders.MAME.Previews = iniFile.Read("MAMEFolders", "Previews", null);
				Settings.Folders.MAME.Select = iniFile.Read("MAMEFolders", "Select", null);
				Settings.Folders.MAME.Snap = iniFile.Read("MAMEFolders", "Snap", null);
				Settings.Folders.MAME.Titles = iniFile.Read("MAMEFolders", "Titles", null);

				Settings.MAME.Layout = iniFile.Read("MAME", "Layout", "Default");
				Settings.MAME.LayoutOverride = iniFile.Read("MAME", "LayoutOverride", "MAME");
				Settings.MAME.LayoutSub = iniFile.Read("MAME", "LayoutSub", "Sub (0)");
				Settings.MAME.Bak = iniFile.Read<string>("MAME", "Bak", "Default");
				Settings.MAME.AutoShow = iniFile.Read<bool>("MAME", "AutoShow", false);
				Settings.MAME.AutoShowDelay = iniFile.Read<int>("MAME", "AutoShowDelay", 2000);
				Settings.MAME.AutoShowTimeout = iniFile.Read<int>("MAME", "AutoShowTimeout", 10000);
				Settings.MAME.UseMAMEOutputSystem = iniFile.Read<bool>("MAME", "UseMAMEOutputSystem", true);
				Settings.MAME.UseShowKey = iniFile.Read<bool>("MAME", "UseShowKey", true);
				Settings.MAME.Screenshot = iniFile.Read<bool>("MAME", "Screenshot", true);
				Settings.MAME.DetectPause = iniFile.Read<bool>("MAME", "DetectPause", false);
				Settings.MAME.SendPause = iniFile.Read<bool>("MAME", "SendPause", true);
				Settings.MAME.PauseMode = (PauseMode)iniFile.Read<int>("MAME", "PauseMode", 0);
				Settings.MAME.SkipDisclaimer = iniFile.Read<bool>("MAME", "SkipDisclaimer", false);

				Settings.Layout.Name = iniFile.Read("Layout", "LayoutName", "Default");

				Settings.Display.Rotation = iniFile.Read<int>("Display", "Rotate", 0);
				Settings.Display.FlipX = iniFile.Read<bool>("Display", "FlipX", false);
				Settings.Display.FlipY = iniFile.Read<bool>("Display", "FlipY", false);
				Settings.Display.AutoRotate = iniFile.Read<bool>("Display", "AutoRotate", false);
				Settings.Display.RotateLeft = iniFile.Read<bool>("Display", "RotateLeft", false);
				Settings.Display.Screen = ScreenManager.ScreenNameToNumber(iniFile.Read("Display", "Screen", @"\\.\DISPLAY1"));
				Settings.Display.SubScreenEnable = iniFile.Read<bool>("Display", "SubScreenEnable", false);
				Settings.Display.SubScreenDisable = iniFile.Read<bool>("Display", "SubScreenDisable", true);
				Settings.Display.SubScreenInterval = iniFile.Read<int>("Display", "SubScreenInterval", 10000);
				Settings.Display.SubScreen = ScreenManager.ScreenNameToNumber(iniFile.Read("Display", "SubScreen", @"\\.\DISPLAY1"));
				Settings.Display.DisplayChange = iniFile.Read<bool>("Display", "DisplayChange", true);
				Settings.Display.DisplayChangeDelay = iniFile.Read<int>("Display", "DisplayChangeDelay", 2000);
				Settings.Display.ShowLoadingScreens = iniFile.Read<bool>("Display", "ShowLoadingScreens", false);
				Settings.Display.LabelArrowShow = iniFile.Read<bool>("Display", "LabelArrowShow", true);
				Settings.Display.LabelArrowSize = iniFile.Read<int>("Display", "LabelArrowSize", 4);
				Settings.Display.LabelArrowColor = iniFile.Read<Color>("Display", "LabelArrowColor", Color.DarkBlue);
				Settings.Display.LabelSpotShow = iniFile.Read<bool>("Display", "LabelSpotShow", true);
				Settings.Display.LabelSpotSize = iniFile.Read<int>("Display", "LabelSpotSize", 8);
				Settings.Display.LabelSpotColor = iniFile.Read<Color>("Display", "LabelSpotColor", Color.DarkBlue);
				Settings.Display.LabelOutlineSize = iniFile.Read<int>("Display", "LabelOutlineSize", 2);
				Settings.Display.LabelOutlineColor = iniFile.Read<Color>("Display", "LabelOutlineColor", Color.Black);
				Settings.Display.AlphaFade = iniFile.Read<bool>("Display", "AlphaFade", true);
				Settings.Display.AlphaFadeValue = iniFile.Read<int>("Display", "AlphaFadeValue", 10);
				Settings.Display.ShowLayoutTopMost = iniFile.Read<bool>("Display", "ShowLayoutTopMost", true);
				Settings.Display.ShowLayoutForceForeground = iniFile.Read<bool>("Display", "ShowLayoutForceForeground", true);
				Settings.Display.ShowLayoutGiveFocus = iniFile.Read<bool>("Display", "ShowLayoutGiveFocus", true);
				Settings.Display.ShowLayoutMouseClick = iniFile.Read<bool>("Display", "ShowLayoutMouseClick", true);
				Settings.Display.ShowRetryEnable = iniFile.Read<bool>("Display", "ShowRetryEnable", true);
				Settings.Display.ShowRetryExitOnFail = iniFile.Read<bool>("Display", "ShowRetryExitOnFail", false);
				Settings.Display.ShowRetryNumRetrys = iniFile.Read<int>("Display", "ShowRetryNumRetrys", 3);
				Settings.Display.ShowRetryInterval = iniFile.Read<int>("Display", "ShowRetryInterval", 2000);
				Settings.Display.UseHighQuality = iniFile.Read<bool>("Display", "UseHighQuality", false);
				Settings.Display.HideExitMenu = iniFile.Read<bool>("Display", "HideExitMenu", false);
				Settings.Display.UseMenuBorders = iniFile.Read<bool>("Display", "UseMenuBorders", false);
				Settings.Display.MenuFont = iniFile.Read<Font>("Display", "MenuFont", new Font("Lucida Console", 40.0f));
				Settings.Display.MenuFontColor = iniFile.Read<Color>("Display", "MenuFontColor", Color.White);
				Settings.Display.MenuSelectorBarColor = iniFile.Read<Color>("Display", "MenuSelectorBarColor", Color.Yellow);
				Settings.Display.MenuBorderColor = iniFile.Read<Color>("Display", "MenuBorderColor", Color.Black);
				Settings.Display.MenuSelectorBorderColor = iniFile.Read<Color>("Display", "MenuSelectorBorderColor", Color.Black);
				Settings.Display.ShowDropShadow = iniFile.Read<bool>("Display", "ShowDropShadow", true);
				
				Settings.Data.General.ShowCPOnly = iniFile.Read<bool>("Data", "ShowCPOnly", false);
				Settings.Data.General.ExitToMenu = iniFile.Read<bool>("Data", "ExitToMenu", false);

				Settings.Data.AutoShow.ShowCPOnly = iniFile.Read<bool>("Data", "AutoShowShowCPOnly", true);
				Settings.Data.AutoShow.ExitToMenu = iniFile.Read<bool>("Data", "AutoShowExitToMenu", true);

				Settings.Data.MAME.ControlPanel = iniFile.Read<bool>("Data", "MAMECP", true);
				Settings.Data.MAME.GameInfo = iniFile.Read<bool>("Data", "GameInfo", true);
				Settings.Data.MAME.GameHistory = iniFile.Read<bool>("Data", "GameHistory", true);
				Settings.Data.MAME.MAMEInfo = iniFile.Read<bool>("Data", "MAMEInfo", true);
				Settings.Data.MAME.ControlInfo = iniFile.Read<bool>("Data", "ControlInfo", true);
				Settings.Data.MAME.HighScore = iniFile.Read<bool>("Data", "HighScore", true);
				Settings.Data.MAME.MyHighScore = iniFile.Read<bool>("Data", "MyHighScore", true);
				Settings.Data.MAME.Artwork = iniFile.Read<bool>("Data", "MAMEArtwork", true);
				Settings.Data.MAME.Manual = iniFile.Read<bool>("Data", "MAMEManual", true);
				Settings.Data.MAME.IRC = iniFile.Read<bool>("Data", "MAMEIRC", true);

				Settings.Data.Emulator.ControlPanel = iniFile.Read<bool>("Data", "EmulatorCP", true);
				Settings.Data.Emulator.Artwork = iniFile.Read<bool>("Data", "EmulatorArtwork", true);
				Settings.Data.Emulator.Manual = iniFile.Read<bool>("Data", "EmulatorManual", true);
				Settings.Data.Emulator.OperationCard = iniFile.Read<bool>("Data", "OperationCard", true);
				Settings.Data.Emulator.NFO = iniFile.Read<bool>("Data", "NFO", true);
				Settings.Data.Emulator.IRC = iniFile.Read<bool>("Data", "EmulatorIRC", true);

				Settings.Export.ExportType = iniFile.Read("Export", "ExportType", "Image");
				Settings.Export.ResolutionType = (ResolutionType)iniFile.Read<int>("Export", "ResolutionType", (int)ResolutionType.Res_LayoutSize);
				Settings.Export.DrawBackground = iniFile.Read<bool>("Export", "DrawBackground", true);
				Settings.Export.SkipClones = iniFile.Read<bool>("Export", "SkipClones", false);
				Settings.Export.IncludeVerticalBezel = iniFile.Read<bool>("Export", "IncludeVerticalBezel", true);
				Settings.Export.VerticalOrientation = iniFile.Read<bool>("Export", "VerticalOrientation", false);
				Settings.Export.Folders.Export = iniFile.Read("Export", "ExportFolder", null);

				Settings.Input.EnableExitKey = iniFile.Read<bool>("Input", "EnableExitKey", false);
				Settings.Input.BackKeyExitMenu = iniFile.Read<bool>("Input", "BackKeyExitMenu", false);
				Settings.Input.ShowKey = StringTools.FixVariables(iniFile.Read("Input", "ShowKey", "[KEYCODE_L]"));
				Settings.Input.SelectKey = StringTools.FixVariables(iniFile.Read("Input", "SelectKey", "[KEYCODE_LCONTROL]"));
				Settings.Input.BackKey = StringTools.FixVariables(iniFile.Read("Input", "BackKey", "[KEYCODE_ESC]"));
				Settings.Input.ExitKey = StringTools.FixVariables(iniFile.Read("Input", "ExitKey", "[KEYCODE_P]"));
				Settings.Input.MenuUp = StringTools.FixVariables(iniFile.Read("Input", "MenuUp", "[KEYCODE_UP]"));
				Settings.Input.MenuDown = StringTools.FixVariables(iniFile.Read("Input", "MenuDown", "[KEYCODE_DOWN]"));
				Settings.Input.MenuLeft = StringTools.FixVariables(iniFile.Read("Input", "MenuLeft", "[KEYCODE_LEFT]"));
				Settings.Input.MenuRight = StringTools.FixVariables(iniFile.Read("Input", "MenuRight", "[KEYCODE_RIGHT]"));
				Settings.Input.VolumeDown = StringTools.FixVariables(iniFile.Read("Input", "VolumeDown", "[KEYCODE_MINUS]"));
				Settings.Input.VolumeUp = StringTools.FixVariables(iniFile.Read("Input", "VolumeUp", "[KEYCODE_EQUALS]"));
				Settings.Input.ShowDesktop = StringTools.FixVariables(iniFile.Read("Input", "ShowDesktop", "[KEYCODE_HOME]"));
				Settings.Input.HideDesktop = StringTools.FixVariables(iniFile.Read("Input", "HideDesktop", "[KEYCODE_END]"));
				Settings.Input.StopBackMenu = iniFile.Read<bool>("Input", "StopBackMenu", false);
				Settings.Input.BackShowsCP = iniFile.Read<bool>("Input", "BackShowsCP", false);
				//	iniFile.Write("Input", "StopBackMenu", Settings.Input.StopBackMenu);

				Settings.IRC.Server = iniFile.Read("IRC", "Server", "irc.scifi-fans.net");
				Settings.IRC.Port = iniFile.Read<int>("IRC", "Port", 7000);
				Settings.IRC.Channel = iniFile.Read("IRC", "Channel", "#byoac");
				Settings.IRC.NickName = iniFile.Read("IRC", "NickName", System.Environment.UserName);
				Settings.IRC.UserName = iniFile.Read("IRC", "UserName", System.Environment.MachineName);
				Settings.IRC.RealName = iniFile.Read("IRC", "RealName", "CPWizard");
				Settings.IRC.IsInvisible = iniFile.Read<bool>("IRC", "IsInvisible", true);

				Settings.LastWriteTime.MAME = iniFile.Read("LastWriteTime", "MAME", null);
				Settings.LastWriteTime.ControlsDat = iniFile.Read("LastWriteTime", "ControlsDat", null);
				Settings.LastWriteTime.ColorsIni = iniFile.Read("LastWriteTime", "ColorsIni", null);
				Settings.LastWriteTime.CatVer = iniFile.Read("LastWriteTime", "CatVer", null);
				Settings.LastWriteTime.NPlayers = iniFile.Read("LastWriteTime", "NPlayers", null);
				Settings.LastWriteTime.HallOfFame = iniFile.Read("LastWriteTime", "HallOfFame", null);

				Settings.Files.ControlsDat = iniFile.Read("DataFiles", "ControlsDat", Settings.Files.ControlsDat);
				Settings.Files.ColorsIni = iniFile.Read("DataFiles", "ColorsIni", Settings.Files.ColorsIni);
				Settings.Files.CatVer = iniFile.Read("DataFiles", "CatVer", Settings.Files.CatVer);
				Settings.Files.NPlayers = iniFile.Read("DataFiles", "NPlayers", Settings.Files.NPlayers);
				Settings.Files.ListInfo = iniFile.Read("DataFiles", "ListInfo", Settings.Files.ListInfo);
				Settings.Files.MiniInfo = iniFile.Read("DataFiles", "MiniInfo", Settings.Files.MiniInfo);
				Settings.Files.CommandDat = iniFile.Read("DataFiles", "CommandDat", Settings.Files.CommandDat);
				Settings.Files.HistoryDat = iniFile.Read("DataFiles", "HistoryDat", Settings.Files.HistoryDat);
				Settings.Files.MAMEInfoDat = iniFile.Read("DataFiles", "MAMEInfoDat", Settings.Files.MAMEInfoDat);
				Settings.Files.HallOfFame = iniFile.Read("DataFiles", "HallOfFame", Settings.Files.HallOfFame);
				Settings.Files.StoryDat = iniFile.Read("DataFiles", "StoryDat", Settings.Files.StoryDat);

				Settings.Preview.LastRomSelected = iniFile.Read("Preview", "LastRomSelected", null);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ReadConfig", "ConfigIO", ex.Message, ex.StackTrace);
			}
		}

		public static void WriteConfig()
		{
			try
			{
				IniFile iniFile = new IniFile(Settings.Files.Ini);

				iniFile.Write<bool>("General", "VerboseLogging", Settings.General.VerboseLogging);
				iniFile.Write<bool>("General", "RunOnStartup", Settings.General.RunOnStartup);
				iniFile.Write<bool>("General", "VolumeControlEnable", Settings.General.VolumeControlEnable);
				iniFile.Write<bool>("General", "DynamicDataLoading", Settings.General.DynamicDataLoading);
				iniFile.Write("General", "GhostScriptExe", Settings.Files.GSExe);
				iniFile.Write<bool>("General", "AllowBracketedGameNames", Settings.General.AllowBracketedGameNames);
				iniFile.Write<bool>("General", "DisableSystemSpecs", Settings.General.DisableSystemSpecs);

				iniFile.Write<bool>("HideDesktop", "Enable", Settings.HideDesktop.Enable);
				iniFile.Write<bool>("HideDesktop", "DisableScreenSaver", Settings.HideDesktop.DisableScreenSaver);
				iniFile.Write<bool>("HideDesktop", "HideMouseCursor", Settings.HideDesktop.HideMouseCursor);
				iniFile.Write<bool>("HideDesktop", "HideDesktopUsingForms", Settings.HideDesktop.HideDesktopUsingForms);
				iniFile.Write<bool>("HideDesktop", "SetWallpaperBlack", Settings.HideDesktop.SetWallpaperBlack);
				iniFile.Write<bool>("HideDesktop", "HideDesktopIcons", Settings.HideDesktop.HideDesktopIcons);
				iniFile.Write<bool>("HideDesktop", "HideTaskbar", Settings.HideDesktop.HideTaskbar);
				iniFile.Write<bool>("HideDesktop", "MoveMouseOffscreen", Settings.HideDesktop.MoveMouseOffscreen);

				string mameVersion = null;

				MAMEXml.TryGetMAMEVersion(Settings.Files.MAME.MAMEExe, out mameVersion);

				iniFile.Write("MAME", "Version", mameVersion);
				iniFile.Write("MAME", "Exe", Settings.Files.MAME.MAMEExe);
				iniFile.Write("MAME", "Layout", Settings.MAME.Layout);
				iniFile.Write("MAMEFolders", "Cabinets", Settings.Folders.MAME.Cabinets);
				iniFile.Write("MAMEFolders", "Cfg", Settings.Folders.MAME.Cfg);
				iniFile.Write("MAMEFolders", "CPanel", Settings.Folders.MAME.CPanel);
				iniFile.Write("MAMEFolders", "Ctrlr", Settings.Folders.MAME.Ctrlr);
				iniFile.Write("MAMEFolders", "Flyers", Settings.Folders.MAME.Flyers);
				iniFile.Write("MAMEFolders", "Hi", Settings.Folders.MAME.Hi);
				iniFile.Write("MAMEFolders", "Icons", Settings.Folders.MAME.Icons);
				iniFile.Write("MAMEFolders", "Ini", Settings.Folders.MAME.Ini);
				iniFile.Write("MAMEFolders", "Manuals", Settings.Folders.MAME.Manuals);
				iniFile.Write("MAMEFolders", "Marquees", Settings.Folders.MAME.Marquees);
				iniFile.Write("MAMEFolders", "NvRam", Settings.Folders.MAME.NvRam);
				iniFile.Write("MAMEFolders", "PCB", Settings.Folders.MAME.PCB);
				iniFile.Write("MAMEFolders", "Previews", Settings.Folders.MAME.Previews);
				iniFile.Write("MAMEFolders", "Select", Settings.Folders.MAME.Select);
				iniFile.Write("MAMEFolders", "Snap", Settings.Folders.MAME.Snap);
				iniFile.Write("MAMEFolders", "Titles", Settings.Folders.MAME.Titles);

				iniFile.Write("MAME", "Layout", Settings.MAME.Layout);
				iniFile.Write("MAME", "LayoutOverride", Settings.MAME.LayoutOverride);
				iniFile.Write("MAME", "LayoutSub", Settings.MAME.LayoutSub);
				iniFile.Write("MAME", "Bak", Settings.MAME.Bak);
				iniFile.Write<bool>("MAME", "AutoShow", Settings.MAME.AutoShow);
				iniFile.Write<int>("MAME", "AutoShowDelay", Settings.MAME.AutoShowDelay);
				iniFile.Write<int>("MAME", "AutoShowTimeout", Settings.MAME.AutoShowTimeout);
				iniFile.Write<bool>("MAME", "UseMAMEOutputSystem", Settings.MAME.UseMAMEOutputSystem);
				iniFile.Write<bool>("MAME", "UseShowKey", Settings.MAME.UseShowKey);
				iniFile.Write<bool>("MAME", "Screenshot", Settings.MAME.Screenshot);
				iniFile.Write<bool>("MAME", "DetectPause", Settings.MAME.DetectPause);
				iniFile.Write<bool>("MAME", "SendPause", Settings.MAME.SendPause);
				iniFile.Write<int>("MAME", "PauseMode", ((int)Settings.MAME.PauseMode));
				iniFile.Write<bool>("MAME", "SkipDisclaimer", Settings.MAME.SkipDisclaimer);

				iniFile.Write("Layout", "LayoutName", Settings.Layout.Name);

				iniFile.Write<int>("Display", "Rotate", Settings.Display.Rotation);
				iniFile.Write<bool>("Display", "FlipX", Settings.Display.FlipX);
				iniFile.Write<bool>("Display", "FlipY", Settings.Display.FlipY);
				iniFile.Write<bool>("Display", "AutoRotate", Settings.Display.AutoRotate);
				iniFile.Write<bool>("Display", "RotateLeft", Settings.Display.RotateLeft);
				iniFile.Write("Display", "Screen", ScreenManager.ScreenNumberToName(Settings.Display.Screen));
				iniFile.Write<bool>("Display", "SubScreenEnable", Settings.Display.SubScreenEnable);
				iniFile.Write<bool>("Display", "SubScreenDisable", Settings.Display.SubScreenDisable);
				iniFile.Write<int>("Display", "SubScreenInterval", Settings.Display.SubScreenInterval);
				iniFile.Write("Display", "SubScreen", ScreenManager.ScreenNumberToName(Settings.Display.SubScreen));
				iniFile.Write<bool>("Display", "DisplayChange", Settings.Display.DisplayChange);
				iniFile.Write<int>("Display", "DisplayChangeDelay", Settings.Display.DisplayChangeDelay);
				iniFile.Write<bool>("Display", "ShowLoadingScreens", Settings.Display.ShowLoadingScreens);
				iniFile.Write<bool>("Display", "LabelArrowShow", Settings.Display.LabelArrowShow);
				iniFile.Write<int>("Display", "LabelArrowSize", Settings.Display.LabelArrowSize);
				iniFile.Write<Color>("Display", "LabelArrowColor", Settings.Display.LabelArrowColor);
				iniFile.Write<bool>("Display", "LabelSpotShow", Settings.Display.LabelSpotShow);
				iniFile.Write<int>("Display", "LabelSpotSize", Settings.Display.LabelSpotSize);
				iniFile.Write<Color>("Display", "LabelSpotColor", Settings.Display.LabelSpotColor);
				iniFile.Write<int>("Display", "LabelOutlineSize", Settings.Display.LabelOutlineSize);
				iniFile.Write<Color>("Display", "LabelOutlineColor", Settings.Display.LabelOutlineColor);
				iniFile.Write<bool>("Display", "AlphaFade", Settings.Display.AlphaFade);
				iniFile.Write<int>("Display", "AlphaFadeValue", Settings.Display.AlphaFadeValue);
				iniFile.Write<bool>("Display", "ShowLayoutTopMost", Settings.Display.ShowLayoutTopMost);
				iniFile.Write<bool>("Display", "ShowLayoutForceForeground", Settings.Display.ShowLayoutForceForeground);
				iniFile.Write<bool>("Display", "ShowLayoutGiveFocus", Settings.Display.ShowLayoutGiveFocus);
				iniFile.Write<bool>("Display", "ShowLayoutMouseClick", Settings.Display.ShowLayoutMouseClick);
				iniFile.Write<bool>("Display", "ShowRetryEnable", Settings.Display.ShowRetryEnable);
				iniFile.Write<bool>("Display", "ShowRetryExitOnFail", Settings.Display.ShowRetryExitOnFail);
				iniFile.Write<int>("Display", "ShowRetryNumRetrys", Settings.Display.ShowRetryNumRetrys);
				iniFile.Write<int>("Display", "ShowRetryInterval", Settings.Display.ShowRetryInterval);
				iniFile.Write<bool>("Display", "UseHighQuality", Settings.Display.UseHighQuality);
				iniFile.Write<bool>("Display", "HideExitMenu", Settings.Display.HideExitMenu);
				iniFile.Write<Font>("Display", "MenuFont", Settings.Display.MenuFont);
				iniFile.Write<bool>("Display", "UseMenuBorders", Settings.Display.UseMenuBorders);
				iniFile.Write<Color>("Display", "MenuFontColor", Settings.Display.MenuFontColor);
				iniFile.Write<Color>("Display", "MenuSelectorBarColor", Settings.Display.MenuSelectorBarColor);
				iniFile.Write<Color>("Display", "MenuBorderColor", Settings.Display.MenuBorderColor);
				iniFile.Write<Color>("Display", "MenuSelectorBorderColor", Settings.Display.MenuSelectorBorderColor);
				iniFile.Write<bool>("Display", "ShowDropShadow", Settings.Display.ShowDropShadow);

				iniFile.Write<bool>("Data", "ShowCPOnly", Settings.Data.General.ShowCPOnly);
				iniFile.Write<bool>("Data", "ExitToMenu", Settings.Data.General.ExitToMenu);

				iniFile.Write<bool>("Data", "AutoShowShowCPOnly", Settings.Data.AutoShow.ShowCPOnly);
				iniFile.Write<bool>("Data", "AutoShowExitToMenu", Settings.Data.AutoShow.ExitToMenu);

				iniFile.Write<bool>("Data", "MAMECP", Settings.Data.MAME.ControlPanel);
				iniFile.Write<bool>("Data", "GameInfo", Settings.Data.MAME.GameInfo);
				iniFile.Write<bool>("Data", "GameHistory", Settings.Data.MAME.GameHistory);
				iniFile.Write<bool>("Data", "MAMEInfo", Settings.Data.MAME.MAMEInfo);
				iniFile.Write<bool>("Data", "ControlInfo", Settings.Data.MAME.ControlInfo);
				iniFile.Write<bool>("Data", "HighScore", Settings.Data.MAME.HighScore);
				iniFile.Write<bool>("Data", "MyHighScore", Settings.Data.MAME.MyHighScore);
				iniFile.Write<bool>("Data", "MAMEArtwork", Settings.Data.MAME.Artwork);
				iniFile.Write<bool>("Data", "MAMEManual", Settings.Data.MAME.Manual);
				iniFile.Write<bool>("Data", "MAMEIRC", Settings.Data.MAME.IRC);

				iniFile.Write<bool>("Data", "EmulatorCP", Settings.Data.Emulator.ControlPanel);
				iniFile.Write<bool>("Data", "EmulatorArtwork", Settings.Data.Emulator.Artwork);
				iniFile.Write<bool>("Data", "EmulatorManual", Settings.Data.Emulator.Manual);
				iniFile.Write<bool>("Data", "OperationCard", Settings.Data.Emulator.OperationCard);
				iniFile.Write<bool>("Data", "NFO", Settings.Data.Emulator.NFO);
				iniFile.Write<bool>("Data", "EmulatorIRC", Settings.Data.Emulator.IRC);

				iniFile.Write("Export", "ExportType", Settings.Export.ExportType);
				iniFile.Write<int>("Export", "ResolutionType", (int)Settings.Export.ResolutionType);
				iniFile.Write<bool>("Export", "DrawBackground", Settings.Export.DrawBackground);
				iniFile.Write<bool>("Export", "SkipClones", Settings.Export.SkipClones);
				iniFile.Write<bool>("Export", "IncludeVerticalBezel", Settings.Export.IncludeVerticalBezel);
				iniFile.Write<bool>("Export", "VerticalOrientation", Settings.Export.VerticalOrientation);
				iniFile.Write("Export", "ExportFolder", Settings.Export.Folders.Export);

				iniFile.Write<bool>("Input", "EnableExitKey", Settings.Input.EnableExitKey);
				iniFile.Write<bool>("Input", "BackKeyExitMenu", Settings.Input.BackKeyExitMenu);
				iniFile.Write("Input", "ShowKey", Settings.Input.ShowKey);
				iniFile.Write("Input", "SelectKey", Settings.Input.SelectKey);
				iniFile.Write("Input", "BackKey", Settings.Input.BackKey);
				iniFile.Write("Input", "ExitKey", Settings.Input.ExitKey);
				iniFile.Write("Input", "MenuUp", Settings.Input.MenuUp);
				iniFile.Write("Input", "MenuDown", Settings.Input.MenuDown);
				iniFile.Write("Input", "MenuLeft", Settings.Input.MenuLeft);
				iniFile.Write("Input", "MenuRight", Settings.Input.MenuRight);
				iniFile.Write("Input", "VolumeDown", Settings.Input.VolumeDown);
				iniFile.Write("Input", "VolumeUp", Settings.Input.VolumeUp);
				iniFile.Write("Input", "ShowDesktop", Settings.Input.ShowDesktop);
				iniFile.Write("Input", "HideDesktop", Settings.Input.HideDesktop);
				iniFile.Write<bool>("Input", "StopBackMenu", Settings.Input.StopBackMenu);
				iniFile.Write<bool>("Input", "BackShowsCP", Settings.Input.BackShowsCP);
				
				iniFile.Write("IRC", "Server", Settings.IRC.Server);
				iniFile.Write<int>("IRC", "Port", Settings.IRC.Port);
				iniFile.Write("IRC", "Channel", Settings.IRC.Channel);
				iniFile.Write("IRC", "NickName", Settings.IRC.NickName);
				iniFile.Write("IRC", "UserName", Settings.IRC.UserName);
				iniFile.Write("IRC", "RealName", Settings.IRC.RealName);
				iniFile.Write<bool>("IRC", "IsInvisible", Settings.IRC.IsInvisible);

				iniFile.Write("LastWriteTime", "MAME", GetLastWriteTime(Settings.Files.MAME.MAMEExe));
				iniFile.Write("LastWriteTime", "ControlsDat", GetLastWriteTime(Settings.Files.ControlsDat));
				iniFile.Write("LastWriteTime", "ColorsIni", GetLastWriteTime(Settings.Files.ColorsIni));
				iniFile.Write("LastWriteTime", "CatVer", GetLastWriteTime(Settings.Files.CatVer));
				iniFile.Write("LastWriteTime", "NPlayers", GetLastWriteTime(Settings.Files.NPlayers));
				iniFile.Write("LastWriteTime", "HallOfFame", GetLastWriteTime(Settings.Files.HallOfFame));

				iniFile.Write("DataFiles", "ControlsDat", Settings.Files.ControlsDat);
				iniFile.Write("DataFiles", "ColorsIni", Settings.Files.ColorsIni);
				iniFile.Write("DataFiles", "CatVer", Settings.Files.CatVer);
				iniFile.Write("DataFiles", "NPlayers", Settings.Files.NPlayers);
				iniFile.Write("DataFiles", "ListInfo", Settings.Files.ListInfo);
				iniFile.Write("DataFiles", "MiniInfo", Settings.Files.MiniInfo);
				iniFile.Write("DataFiles", "CommandDat", Settings.Files.CommandDat);
				iniFile.Write("DataFiles", "HistoryDat", Settings.Files.HistoryDat);
				iniFile.Write("DataFiles", "MAMEInfoDat", Settings.Files.MAMEInfoDat);
				iniFile.Write("DataFiles", "HallOfFame", Settings.Files.HallOfFame);
				iniFile.Write("DataFiles", "StoryDat", Settings.Files.StoryDat);

				iniFile.Write("Preview", "LastRomSelected", Settings.Preview.LastRomSelected);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("WriteConfig", "ConfigIO", ex.Message, ex.StackTrace);
			}
		}
	}
}
