// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace CPWizard
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			// Create new stopwatch.
			Stopwatch stopwatch = new Stopwatch();

			// Begin timing.
			stopwatch.Start();

			try
			{
				Settings.Folders.App = Application.StartupPath;
				Settings.Folders.Media = Path.Combine(Settings.Folders.App, "Media");
				Settings.Folders.Data = Path.Combine(Settings.Folders.App, "Data");
				Settings.Folders.Database = Path.Combine(Settings.Folders.Data, "Database");
				Settings.Folders.Labels = Path.Combine(Settings.Folders.Data, "Labels");
				Settings.Folders.Profiles = Path.Combine(Settings.Folders.Data, "Profiles");
				Settings.Folders.LayoutMaps = Path.Combine(Settings.Folders.Data, "LayoutMaps");
				Settings.Folders.Layout = Path.Combine(Settings.Folders.App, "Layout");
				Settings.Files.LogFile = Path.Combine(Settings.Folders.App, "CPWizard.log");
				Settings.Files.HelpFile = Path.Combine(Settings.Folders.App, "CPWizard.chm");
				Settings.Files.Ini = Path.Combine(Settings.Folders.App, "CPWizard.ini");
				Settings.Files.Uza = Path.Combine(Settings.Folders.App, "uza.exe");
				Settings.Files.FilterXml = Path.Combine(Settings.Folders.Data, "[LIST_MAME_FILTER].xml");
				Settings.Files.ControlsDat = Path.Combine(Settings.Folders.Data, "controls.xml");
				Settings.Files.ColorsIni = Path.Combine(Settings.Folders.Data, "colors.ini");
				Settings.Files.CatVer = Path.Combine(Settings.Folders.Data, "catver.ini");
				Settings.Files.NPlayers = Path.Combine(Settings.Folders.Data, "nplayers.ini");
				Settings.Files.ListInfo = Path.Combine(Settings.Folders.Data, "[LIST_MAME].xml");
				Settings.Files.MiniInfo = Path.Combine(Settings.Folders.Data, "[LIST_MAME_MINI].xml");
				Settings.Files.CommandDat = Path.Combine(Settings.Folders.Data, "Command.dat");
				Settings.Files.HistoryDat = Path.Combine(Settings.Folders.Data, "History.dat");
				Settings.Files.MAMEInfoDat = Path.Combine(Settings.Folders.Data, "MAMEInfo.dat");
				Settings.Files.HallOfFame = Path.Combine(Settings.Folders.Data, "HallOfFame.xml");
				Settings.Files.StoryDat = Path.Combine(Settings.Folders.Data, "Story.dat");
				Settings.Folders.CommandDat = Path.Combine(Settings.Folders.Media, "CommandDat");
				Settings.Folders.Logos = Path.Combine(Settings.Folders.Media, "Logos");
				Settings.Folders.Manufacturers = Path.Combine(Settings.Folders.Media, "Manufacturers");
				Settings.Folders.Smilies = Path.Combine(Settings.Folders.Media, "Smilies");
				Settings.Folders.Flags = Path.Combine(Settings.Folders.Media, "Flags");
				Settings.Folders.Icons = Path.Combine(Settings.Folders.Media, "Icons");
				Settings.Folders.Temp = Path.GetTempPath();

				Settings.Folders.InputCodes = Path.Combine(Settings.Folders.Data, "InputCodes");

				Settings.Files.InputCodes.MAMEDefault = Path.Combine(Settings.Folders.InputCodes, "MAMEDefault.txt");
				Settings.Files.InputCodes.ControlsToLabels = Path.Combine(Settings.Folders.InputCodes, "FEDEV_controls_to_labels.txt");
				Settings.Files.InputCodes.DescriptionsToControls = Path.Combine(Settings.Folders.InputCodes, "FEDEV_descriptions_to_controls.txt");
				Settings.Files.InputCodes.EmuCodes = Path.Combine(Settings.Folders.InputCodes, "EmuCodes.txt");
				Settings.Files.InputCodes.GroupCodes = Path.Combine(Settings.Folders.InputCodes, "GroupCodes.txt");
				Settings.Files.InputCodes.JoyCodes = Path.Combine(Settings.Folders.InputCodes, "JoyCodes.txt");
				Settings.Files.InputCodes.KeyCodes = Path.Combine(Settings.Folders.InputCodes, "KeyCodes.txt");
				Settings.Files.InputCodes.MiscCodes = Path.Combine(Settings.Folders.InputCodes, "MiscCodes.txt");
				Settings.Files.InputCodes.MouseCodes = Path.Combine(Settings.Folders.InputCodes, "MouseCodes.txt");
				Settings.Files.InputCodes.PlayerCodes = Path.Combine(Settings.Folders.InputCodes, "PlayerCodes.txt");
				Settings.Files.InputCodes.AnalogToDigital = Path.Combine(Settings.Folders.InputCodes, "AnalogToDigital.txt");

				LogFile.FileName = Settings.Files.LogFile;

				Globals.InterComm = new frmInterComm();
				Globals.InterComm.MessageReceived += new frmInterComm.MessageReceivedDelegate(OnMessageReceived);

				if (Globals.InterComm.CommMode == frmInterComm.InterCommMode.Client)
				{
					Globals.InterComm.SendMessage((int)DataMode.FirstRun, String.Empty);
					Globals.InterComm.SendMessage((int)DataMode.CmdLine, String.Join("\n", args));

					while (!Settings.InterComm.Exit)
					{
						System.Threading.Thread.Sleep(100);
						Application.DoEvents();
					}

					return;
				}

				ConfigIO.ReadScreen();

				Settings.General.MainScreenSize = System.Windows.Forms.Screen.AllScreens[Settings.Display.Screen].Bounds.Size;
				Settings.General.SubScreenSize = System.Windows.Forms.Screen.AllScreens[Settings.Display.SubScreen].Bounds.Size;

                var version = Assembly.GetExecutingAssembly().GetName().Version;

                LogFile.ClearLog();
				LogFile.WriteLine("CPWizard " + version.ToString(3));
				LogFile.WriteLine("Main Load Process Starting");

				//Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

				using (Globals.MainForm = new frmMain())
				{
					LogFile.WriteLine("Initializing Display Manager");
					Globals.DisplayManager = new DisplayManager();
					LogFile.WriteLine("Initializing Program Manager");
					Globals.ProgramManager = new ProgramManager(); // this is where settings get read in
					EventManager.CmdArgsChanged(CmdLineToHash(args));

					//Log System Specs if set in settings
					if (!Settings.General.DisableSystemSpecs)
					{
						LogFile.WriteLine("System Specs:");
						LogFile.OutputSystemConfiguration();
					}
					else
					{
						LogFile.WriteLine("Disable System Specs logging set in settings. Skipping.");
					}

					if (Globals.MainForm != null)
					{
						Globals.ProgramManager.StartMAMEInterop();

						if (Settings.General.Minimized)
							Globals.MainForm.WindowState = FormWindowState.Minimized;

						// Get the elapsed time as a TimeSpan value.
						TimeSpan ts = stopwatch.Elapsed;

						// Format and display the TimeSpan value.
						string elapsedTime = String.Format("{0:00}.{1:00}",
							ts.Seconds,
							ts.Milliseconds / 10);

						LogFile.WriteLine("Time taken to show layout: " + elapsedTime + " (s)");


						Application.Run(Globals.MainForm);
					}


				}

			}
			catch (Exception ex)
			{
				if (IsCritical(ex))
					LogFile.WriteLine("Main (Critical Exception)");
				LogFile.WriteLine("Main", "Program", ex.Message, ex.StackTrace);
				if (ex.InnerException != null)
					LogFile.WriteLine("Main (Inner Exception)", "Program", ex.InnerException.Message, ex.InnerException.StackTrace);
			}

			Exit();
		}

		static void OnMessageReceived(int id, string str)
		{
			switch (id)
			{
				case (int)DataMode.CmdLine:
					string[] args = str.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

					EventManager.CmdArgsChanged(CmdLineToHash(args));
					break;
				case (int)DataMode.FirstRun:
					Settings.InterComm.FirstRun = false;
					break;
				case (int)DataMode.Exit:
					Settings.InterComm.Exit = true;
					break;
			}
		}

		public static Dictionary<string, string> CmdLineToHash(string[] args)
		{
			try
			{
				if (args == null)
					return null;

				Dictionary<string, string> argHash = new Dictionary<string, string>();

				for (int i = 0; i < args.Length; i++)
				{
					if (args[i].StartsWith("-"))
					{
						string Value = null;

						if (i + 1 < args.Length)
							Value = args[i + 1];

						LogFile.VerboseWriteLine(String.Format("CmdArg: {0} Value: {1}", args[i].ToLower(), Value));

						argHash.Add(args[i].ToLower(), Value);
					}
				}

				return argHash;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("CmdLineToHash", "Program", ex.Message, ex.StackTrace);
			}

			return null;
		}

		public static void Exit()
		{
			try
			{
				LogFile.WriteLine("Shutting Down Program Manager");

				if (Globals.ProgramManager != null)
				{
					Globals.ProgramManager.Dispose();
					Globals.ProgramManager = null;
				}

				LogFile.WriteLine("Writing Config");

				ConfigIO.WriteConfig();

				if (Settings.HideDesktop.Enable)
				{
					LogFile.WriteLine("Showing Desktop");

					HideDesktop.ShowAll();
				}

				LogFile.WriteLine("Exiting");

				Application.Exit();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
			}
		}

		public static bool IsCritical(Exception ex)
		{
			if (ex is OutOfMemoryException) return true;
			if (ex is AppDomainUnloadedException) return true;
			if (ex is BadImageFormatException) return true;
			if (ex is CannotUnloadAppDomainException) return true;
			if (ex is ExecutionEngineException) return true;
			if (ex is InvalidProgramException) return true;
			if (ex is System.Threading.ThreadAbortException)
				return true;
			return false;
		}

		static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			LogFile.WriteLine("ThreadException", "Program", e.Exception.Message, e.Exception.StackTrace);
			if (e.Exception.InnerException != null)
				LogFile.WriteLine("ThreadException (Inner Exception)", "Program", e.Exception.InnerException.Message, e.Exception.InnerException.StackTrace);
			Exit();
		}
	}
}