// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.InteropServices;

namespace CPWizard
{
	public enum DisplayType
	{
		Preview,
		Show
	}

	public enum PauseMode : int
	{
		Msg,
		Diff,
		Key
	}

	public enum DataMode : int
	{
		CmdLine,
		FirstRun,
		Exit
	}

	public enum DisplayMode
	{
		LayoutEditor,
		MainMenu,
		Layout,
		GameInfo,
		HistoryDat,
		MAMEInfoDat,
		CommandDat,
		StoryDat,
		HiToText,
		Artwork,
		Manual,
		OpCard,
		Nfo,
		IRC
	}

	public enum EmulatorMode
	{
		MAME,
		Emulator
	}

	class Globals
	{
		public static string[] DataModeString =
        {
            "CmdLine",
            "FirstRun",
            "Exit"
        };

		public static string[] DisplayModeString =
        {
            "LayoutEditor",
            "MainMenu",
            "Layout",
            "GameInfo",
            "HistoryDat",
            "MAMEInfoDat",
            "CommandDat",
            "StoryDat",
            "HiToText",
            "Artwork",
            "Manual",
            "OpCard",
            "Nfo",
            "IRC"
        };

		public static frmInterComm InterComm = null;

		public static DisplayMode DisplayMode = DisplayMode.LayoutEditor;
		public static EmulatorMode EmulatorMode = EmulatorMode.MAME;
		public static Bitmap MainBitmap = null;
		public static List<Bitmap> SubBitmapList = null;
		public static MAMEInterop MAMEInterop = null;
		public static SendKeys SendKeys = null;

		public static Dictionary<string, int> KeyCodes = null;

		public static List<Bitmap> ScreenshotList = null;

		public static int LayoutIndex = 0;
		public static Layout Layout = null;
		public static List<Layout> LayoutList = null;

		public static int LayoutSubIndex = 0;
		public static Layout LayoutSub = null;
		public static List<Layout> LayoutSubList = null;

		public static ControlsDat ControlsDat = null;
		public static ControlsData ControlsData = null;
		public static InputCodes InputCodes = null;
		public static LayoutMaps LayoutMaps = null;
		public static ColorsIni ColorsIni = null;
		public static CatVer CatVer = null;
		public static NPlayers NPlayers = null;
		public static MAMEXml MAMEXml = null;
		public static MAMEFilter MAMEFilter = null;
		public static MAMEIni MAMEIni = null;
		public static MAMECfg MAMECfg = null;
		public static MAMEOptions MAMEOptions = null;
		public static HallOfFame HallOfFameXml = null;

		public static MainMenu MainMenu = null;
		public static LayoutManager LayoutManager = null;
		public static HistoryDat HistoryDat = null;
		public static GameInfo GameInfo = null;
		public static CommandDat CommandDat = null;
		public static MAMEInfoDat MAMEInfoDat = null;
		public static StoryDat StoryDat = null;
		public static HiToText HiToText = null;
		public static ArtworkManager ArtworkManager = null;
		public static PDFManager MAMEManual = null;
		public static PDFManager EmulatorManual = null;
		public static PDFManager EmulatorOpCard = null;
		public static NFOViewer NFOViewer = null;
		public static IRC IRC = null;

		public static frmMain MainForm = null;
		public static frmObject ObjectForm = null;
		public static frmOptions OptionsForm = null;
		public static frmLayout LayoutForm = null;
		public static frmLayout LayoutSubForm = null;
		public static bool LayoutShowing = false;
		public static bool LayoutSubShowing = false;
		public static List<frmLoading> LoadingFormList = null;
		public static frmInput InputForm = null;
		public static bool MenuJustShown = false;

		public static List<LayoutObject> SelectedObjectList = null;
		public static List<LayoutObject> ClipboardObjectList = null;

		public static ProgramManager ProgramManager = null;
		public static DisplayManager DisplayManager = null;
		public static MAMEManager MAMEManager = null;
		public static EmulatorManager EmulatorManager = null;
		public static Profiles Profiles = null;

		public static MAMEBezel Bezel = null;

		public static KeyboardHook KeyboardHook = null;
		//public static MouseHook MouseHook = null;

		public static DirectInput DirectInput = null;
		public static MCERemote MCERemote = null;
		public static InputManager InputManager = null;

		public static Bitmap WatermarkBitmap = null;
		public static Rectangle VisibleRect;
	}

	public class Settings
	{
		public class InterComm
		{
			public static bool FirstRun = true;
			public static bool Exit = false;
		}

		public class General
		{
			public static bool VerboseLogging = false;
			public static bool FirstRun = false;
			public static bool RunOnStartup = false;
			public static bool Minimized = false;
			public static Size MainScreenSize = Size.Empty;
			public static Size SubScreenSize = Size.Empty;
			//public static bool BackgroundMode = true;
			public static bool VolumeControlEnable = false;
			public static bool DynamicDataLoading = true;
			public static bool AllowBracketedGameNames = false;
			public static bool DisableSystemSpecs = false;
		}

		public class HideDesktop
		{
			public static bool Enable = false;
			public static bool DisableScreenSaver = false;
			public static bool HideMouseCursor = false;
			public static bool HideDesktopUsingForms = false;
			public static bool SetWallpaperBlack = false;
			public static bool HideDesktopIcons = false;
			public static bool HideTaskbar = false;
			public static bool MoveMouseOffscreen = false;
		}

		public class Emulator
		{
			public static ProfileNode Profile = null;
			public static bool Running = false;
			public static IntPtr Hwnd = IntPtr.Zero;
			public static Rectangle Rect;
			public static string CommandLine = null;
			//public static bool GameChange = false;
			public static string GameName = null;
		}

		public class MAME
		{
			public static string Version = null;
			public static bool AutoShow = false;
			public static int AutoShowDelay = 2000;
			public static int AutoShowTimeout = 10000;
			public static string Layout = null;
			public static string LayoutOverride = null;
			public static string LayoutSub = null;
			public static string Bak = "Default";
			public static bool UseMAMEOutputSystem = true;
			public static bool UseShowKey = true;
			public static bool Screenshot = true;
			public static bool DetectPause = false;
			public static bool SendPause = true;
			public static PauseMode PauseMode = PauseMode.Msg;
			public static bool SkipDisclaimer = false;
			public static string GameName = null;
			public static MAMEMachineNode MachineNode = null;
			public static List<WindowInfo> WindowInfoList = null;
			public static IntPtr hWndConsole = IntPtr.Zero;
			public static List<Rectangle> RectList = null;
			public static string CommandLine = null;
			public static bool Running = false;
			public static bool Paused = false;

			public static string GetParent()
			{
				if (MachineNode != null)
					if (MachineNode.ROMOf != null)
						return MachineNode.CloneOf;

				return null;
			}
		}

		public class Data
		{
			public class General
			{
				public static bool ShowCPOnly = false;
				public static bool ExitToMenu = false;
			}

			public class AutoShow
			{
				public static bool ShowCPOnly = false;
				public static bool ExitToMenu = false;
			}

			public class MAME
			{
				public static bool ControlPanel = true;
				public static bool GameInfo = true;
				public static bool GameHistory = true;
				public static bool MAMEInfo = true;
				public static bool ControlInfo = true;
				public static bool HighScore = true;
				public static bool MyHighScore = true;
				public static bool Artwork = true;
				public static bool Manual = true;
				public static bool IRC = true;
			}
			public class Emulator
			{
				public static bool ControlPanel = true;
				public static bool Artwork = true;
				public static bool Manual = true;
				public static bool OperationCard = true;
				public static bool NFO = true;
				public static bool IRC = true;
			}
		}

		public class Input
		{
			public static bool EnableExitKey = false;
			public static bool BackKeyExitMenu = false;
			public static string ShowKey = null;
			public static string BackKey = null;
			public static string SelectKey = null;
			public static string ExitKey = null;
			public static string MenuUp = null;
			public static string MenuDown = null;
			public static string MenuLeft = null;
			public static string MenuRight = null;
			public static string VolumeDown = null;
			public static string VolumeUp = null;
			public static string HideDesktop = null;
			public static string ShowDesktop = null;
			public static bool StopBackMenu = false;
			public static bool BackShowsCP = false;
		}

		public class Layout
		{
			public static string Name = null;
		}

		public class Display
		{
			public static int Rotation = 0;
			public static bool FlipX = false;
			public static bool FlipY = false;
			public static bool AutoRotate = false;
			public static bool RotateLeft = false;
			public static int Screen = 0;
			public static bool SubScreenEnable = false;
			public static bool SubScreenDisable = true;
			public static int SubScreenInterval = 10000;
			public static int SubScreen = 0;
			public static bool ShowGrid = true;
			public static bool SnapToGrid = true;
			public static Size GridSize = new Size(8, 8);
			public static float Scale = 1;
			public static bool DisplayChange = true;
			public static int DisplayChangeDelay = 2000;
			public static bool ShowLoadingScreens = true;
			public static bool LabelArrowShow = true;
			public static int LabelArrowSize = 4;
			public static Color LabelArrowColor = Color.DarkBlue;
			public static bool LabelSpotShow = true;
			public static int LabelSpotSize = 8;
			public static Color LabelSpotColor = Color.DarkBlue;
			public static int LabelOutlineSize = 2;
			public static Color LabelOutlineColor = Color.Black;
			public static bool AlphaFade = true;
			public static int AlphaFadeValue = 10;
			public static bool ShowScreenshot = false;
			//public static bool DisplayChanging = false;
			public static bool ShowLayoutTopMost = true;
			public static bool ShowLayoutForceForeground = true;
			public static bool ShowLayoutGiveFocus = true;
			public static bool ShowLayoutMouseClick = true;
			public static bool ShowRetryEnable = true;
			public static bool ShowRetryExitOnFail = false;
			public static int ShowRetryNumRetrys = 3;
			public static int ShowRetryInterval = 2000;
			public static bool UseHighQuality = false;
			public static bool HideExitMenu = false;
			public static bool UseMenuBorders = false;
			public static Font MenuFont = new Font("Times new Roman", 40, FontStyle.Regular);
			public static Color MenuFontColor = Color.White;
			public static Color MenuSelectorBarColor = Color.Yellow;
			public static Color MenuBorderColor = Color.Black;
			public static Color MenuSelectorBorderColor = Color.Black;
			public static bool ShowDropShadow = true;
		}

		public class CommandDat
		{
		}

		public class LastWriteTime
		{
			public static string MAME = null;
			public static string ControlsDat = null;
			public static string ColorsIni = null;
			public static string CatVer = null;
			public static string NPlayers = null;
			public static string HallOfFame = null;
		}

		public class Files
		{
			public static string LogFile = null;
			public static string HelpFile = null;
			public static string Ctrlr = null;
			public static string Ini = null;
			public static string FilterXml = null;
			public static string ControlsDat = null;
			public static string ColorsIni = null;
			public static string CatVer = null;
			public static string NPlayers = null;
			public static string ListInfo = null;
			public static string MiniInfo = null;
			public static string CommandDat = null;
			public static string StoryDat = null;
			public static string HistoryDat = null;
			public static string MAMEInfoDat = null;
			public static string HallOfFame = null;
			public static string GSExe = null;
			public static string HiToTextExe = null;
			public static string Uza = null;

			public class InputCodes
			{
				public static string MAMEDefault = null;
				public static string ControlsToLabels = null;
				public static string DescriptionsToControls = null;
				public static string EmuCodes = null;
				public static string GroupCodes = null;
				public static string JoyCodes = null;
				public static string KeyCodes = null;
				public static string MiscCodes = null;
				public static string MouseCodes = null;
				public static string PlayerCodes = null;
				public static string AnalogToDigital = null;
			}

			public class MAME
			{
				public static string MAMEExe = null;
				public static string MAMEIni = null;
			}
		}

		public class Folders
		{
			public static string App = null;
            public static string UserData = null;
            public static string Media = null;
			public static string Data = null;
            public static string Layout = null;
            public static string Database = null;
			public static string Labels = null;
			public static string Profiles = null;
			public static string LayoutMaps = null;
			public static string CommandDat = null;
			public static string Logos = null;
			public static string Manufacturers = null;
			public static string Smilies = null;
			public static string Icons = null;
			public static string Flags = null;
			public static string Temp = null;
			public static string InputCodes = null;

			public class MAME
			{
				public static string Root = null;
				public static string Cabinets = null;
				public static string Cfg = null;
				public static string CPanel = null;
				public static string Ctrlr = null;
				public static string Flyers = null;
				public static string Hi = null;
				public static string Icons = null;
				public static string Ini = null;
				public static string Manuals = null;
				public static string Marquees = null;
				public static string NvRam = null;
				public static string PCB = null;
				public static string Previews = null;
				public static string Select = null;
				public static string Snap = null;
				public static string Titles = null;
			}
		}

		public class IRC
		{
			public static string Server = null;
			public static int Port = 0;
			public static string Channel = null;
			public static string NickName = null;
			public static string UserName = null;
			public static string RealName = null;
			public static bool IsInvisible = false;
		}

		public class Preview
		{
			public static string LastRomSelected = null;
		}

		public class Export
		{
			public static string ExportType = null;
			public static ResolutionType ResolutionType = ResolutionType.Res_LayoutSize;
			public static bool DrawBackground = true;
			public static bool SkipClones = false;
			public static bool IncludeVerticalBezel = true;
			public static bool VerticalOrientation = false;

			public class Folders
			{
				public static string Export = null;
			}
		}
	}
}
