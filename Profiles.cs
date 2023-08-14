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
using System.Xml;
using System.IO;
using System.Timers;
using System.Runtime.InteropServices;

namespace CPWizard
{
	public class ProfileNode
	{
		public static ListViewEx.CellType[] CellTypes =
        {
            ListViewEx.CellType.Normal, // Enabled
            ListViewEx.CellType.TextBox, // Name
            ListViewEx.CellType.ComboBox, // Type
            ListViewEx.CellType.Button, // Layout
            ListViewEx.CellType.Button, // LayoutOverride
            ListViewEx.CellType.Button, // LayoutSub
            ListViewEx.CellType.Button, // Labels
            ListViewEx.CellType.Button, // Database
            ListViewEx.CellType.Button, // Executable
            ListViewEx.CellType.TextBox, // WindowTitle
            ListViewEx.CellType.TextBox, // WindowClass
            ListViewEx.CellType.ComboBoxBoolean, // UseExe
            ListViewEx.CellType.ComboBoxBoolean, // Screenshot
            ListViewEx.CellType.ComboBoxBoolean, // Minimize
            ListViewEx.CellType.ComboBoxBoolean, // Maximize
            ListViewEx.CellType.Button, // ShowKey
            ListViewEx.CellType.Button, // HideKey
            ListViewEx.CellType.TextBox, // ShowSendKeys
            ListViewEx.CellType.TextBox, // HideSendKeys
            //ListViewEx.CellType.ComboBoxBoolean, // UseOverlay
            ListViewEx.CellType.Button, // Manuals
            ListViewEx.CellType.Button, // OpCards
            ListViewEx.CellType.Button, // Snaps
            ListViewEx.CellType.Button, // Titles
            ListViewEx.CellType.Button, // Carts
            ListViewEx.CellType.Button, // Boxes
            ListViewEx.CellType.Button, // Nfo
        };

		public bool Enabled = false;
		public string Name = String.Empty;
		public string Type = String.Empty;
		public string Layout = String.Empty;
		public string LayoutOverride = String.Empty;
		public string LayoutSub = String.Empty;
		public string Labels = String.Empty;
		public string Database = String.Empty;
		public string Executable = String.Empty;
		public string WindowTitle = String.Empty;
		public string WindowClass = String.Empty;
		public bool UseExe = true;
		public bool Screenshot = false;
		public bool Minimize = true;
		public bool Maximize = true;
		public string ShowKey = String.Empty;
		public string HideKey = String.Empty;
		public string ShowSendKeys = String.Empty;
		public string HideSendKeys = String.Empty;
		//public bool UseOverlay = false;
		public string Manuals = String.Empty;
		public string OpCards = String.Empty;
		public string Snaps = String.Empty;
		public string Titles = String.Empty;
		public string Carts = String.Empty;
		public string Boxes = String.Empty;
		public string Nfo = String.Empty;

		public string FileName
		{
			get { return Path.Combine(Settings.Folders.Profiles, Name + ".xml"); }
		}

		public ProfileNode()
		{
		}
	}

	class Profiles : IDisposable
	{
		private enum xmlElement
		{
			Nothing,
			Profile,
			Enabled,
			Name,
			Type,
			Layout,
			LayoutOverride,
			LayoutSub,
			Labels,
			Database,
			Executable,
			WindowTitle,
			WindowClass,
			UseExe,
			Screenshot,
			Minimize,
			Maximize,
			ShowKey,
			HideKey,
			ShowSendKeys,
			HideSendKeys,
			//UseOverlay,
			Manuals,
			OpCards,
			Snaps,
			Titles,
			Carts,
			Boxes,
			Nfo
		}

		public List<ProfileNode> ProfileList = null;
		private System.Timers.Timer ProfileCheckTimer = null;

		public event EventHandler<MAMEEventArgs> MAMEStart = null;
		public event EventHandler<EventArgs> MAMEStop = null;

		public event EventHandler<MAMEEventArgs> EmuStart = null;
		public event EventHandler<EventArgs> EmuStop = null;

		public Profiles()
		{
			ProfileList = new List<ProfileNode>();

			ReadAllProfiles(Settings.Folders.Profiles);

			ProfileCheckTimer = new System.Timers.Timer(500);
			ProfileCheckTimer.SynchronizingObject = Globals.MainForm;
			ProfileCheckTimer.Elapsed += new ElapsedEventHandler(OnProfileCheck);
			ProfileCheckTimer.Enabled = true;
		}

		private void OnProfileCheck(object sender, ElapsedEventArgs e)
		{
			try
			{
				if (!Settings.General.Minimized)
					return;

				if (Globals.LayoutForm != null)
					return;

				if (!Settings.MAME.UseMAMEOutputSystem)
				{
					ProcessTools.TryFindWindows("MAME:", "MAME", false, out Settings.MAME.WindowInfoList);

					if (Settings.MAME.WindowInfoList.Count == 0)
					{
						if (!Settings.MAME.Running)
						{
							Settings.MAME.RectList = new List<Rectangle>();

							foreach (WindowInfo windowInfo in Settings.MAME.WindowInfoList)
							{
								LogFile.VerboseWriteLine("OnProfileCheck", "Profiles", String.Format("MAME Running hWnd: 0x{0:x8}", windowInfo.Handle));

								HandleRef handleRef = new HandleRef(Globals.MainForm, windowInfo.Handle);
								Rectangle rect = Win32.GetWindowRectangle(handleRef);
								Settings.MAME.RectList.Add(rect);

								LogFile.VerboseWriteLine("OnProfileCheck", "Profiles", "Window Rect: " + rect.ToString());
							}

							Settings.MAME.Running = true;

							ProcessTools.TryGetCommandLineFromHwnd(Settings.MAME.WindowInfoList, out Settings.MAME.CommandLine);

							LogFile.VerboseWriteLine("OnProfileCheck", "Profiles", "Command Line: " + Settings.MAME.CommandLine);

							string GameName = Globals.MAMEManager.GetMAMEROMName();

							if (GameName != Settings.MAME.GameName)
							{
								LogFile.VerboseWriteLine("OnProfileCheck", "Profiles", "GameName: " + GameName);

								Settings.MAME.GameName = GameName;

								if (Settings.MAME.SkipDisclaimer)
									Globals.MAMEManager.SkipDisclaimer();

								if (MAMEStart != null)
									MAMEStart(this, new MAMEEventArgs(Settings.MAME.GameName));
							}
						}
					}
					else
					{
						if (Settings.MAME.Running)
						{
							LogFile.VerboseWriteLine("OnProfileCheck", "Profiles", "MAME Stopped");
							Settings.MAME.Running = false;
							Settings.MAME.GameName = null;

							if (MAMEStop != null)
								MAMEStop(this, EventArgs.Empty);
						}
					}
				}

				foreach (ProfileNode profile in ProfileList)
				{
					if (!profile.Enabled)
						continue;

					List<WindowInfo> windowInfoList = null;

					if (profile.UseExe)
						ProcessTools.TryGetWindowInfoFromExe(profile.Executable, false, out windowInfoList);
					else
						ProcessTools.TryFindWindows(profile.WindowTitle, profile.WindowClass, false, out windowInfoList);

					if (windowInfoList != null && windowInfoList.Count > 0)
					{
						if (Settings.Emulator.Profile != null)
						{
							if (Settings.Emulator.Profile.Name == profile.Name)
								break;
						}

						Settings.Emulator.Hwnd = windowInfoList[0].Handle;

						LogFile.VerboseWriteLine("OnProfileCheck", "Profiles", "Emulator Running hWnd: " + String.Format("0x{0:x8}", Settings.Emulator.Hwnd));

						HandleRef handleRef = new HandleRef(Globals.MainForm, Settings.Emulator.Hwnd);
						Settings.Emulator.Rect = Win32.GetWindowRectangle(handleRef);

						LogFile.VerboseWriteLine("OnProfileCheck", "Profiles", "Window Rect: " + Settings.Emulator.Rect.ToString());

						Globals.EmulatorMode = EmulatorMode.Emulator;

						Settings.Emulator.Running = true;

						ProcessTools.TryGetCommandLineFromHwnd(Settings.Emulator.Hwnd, out Settings.Emulator.CommandLine);

						LogFile.VerboseWriteLine("OnProfileCheck", "Profiles", "Command Line: " + Settings.Emulator.CommandLine);

						if (profile.Type == "Game")
						{
							Settings.Emulator.GameName = profile.Name;

							LogFile.VerboseWriteLine("OnProfileCheck", "Profiles", "GameName: " + Settings.Emulator.GameName);
						}

						Settings.Emulator.Profile = profile;

						if (EmuStart != null)
							EmuStart(this, new MAMEEventArgs(Settings.Emulator.GameName));
					}
					else
					{
						if (Settings.Emulator.Profile != null)
						{
							if (Settings.Emulator.Profile.Name == profile.Name)
							{
								LogFile.VerboseWriteLine("OnProfileCheck", "Profiles", "Emulator Stopped");

								Settings.Emulator.Running = false;
								Settings.Emulator.Profile = null;
								Settings.Emulator.GameName = null;
								Settings.Emulator.Hwnd = IntPtr.Zero;

								if (EmuStop != null)
									EmuStop(this, EventArgs.Empty);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("OnProfileCheck", "Profiles", ex.Message, ex.StackTrace);
			}
		}

		public void ReadAllProfiles(string Path)
		{
			try
			{
				ProfileList.Clear();

				System.IO.DirectoryInfo Dir = new System.IO.DirectoryInfo(Path);

				System.IO.FileSystemInfo[] Files = Dir.GetFiles("*.xml");

				foreach (System.IO.FileSystemInfo fi in Files)
				{
					ProfileNode profile = new ProfileNode();

					if (TryReadProfileXml(fi.FullName, ref profile))
						ProfileList.Add(profile);
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ReadAllProfiles", "Profiles", ex.Message, ex.StackTrace);
			}
		}

		public void WriteAllProfiles()
		{
			foreach (ProfileNode emulatorProfile in ProfileList)
				WriteProfileXml(emulatorProfile);
		}

		public bool TryReadProfileXml(ref ProfileNode profile)
		{
			if (profile == null)
				return false;

			return TryReadProfileXml(profile.FileName, ref profile);
		}

		public bool TryReadProfileXml(string fileName, ref ProfileNode profile)
		{
			XmlTextReader xmlTextReader = null;
			xmlElement currentElement = xmlElement.Nothing;
			Hashtable attribHash = new Hashtable();

			try
			{
				xmlTextReader = new XmlTextReader(fileName);

				xmlTextReader.Read();

				while (xmlTextReader.Read())
				{
					switch (xmlTextReader.NodeType)
					{
						case XmlNodeType.Element:
							switch (xmlTextReader.LocalName.ToLower())
							{
								case "profile":
									currentElement = xmlElement.Profile;
									break;
								case "enabled":
									currentElement = xmlElement.Enabled;
									break;
								case "name":
									currentElement = xmlElement.Name;
									break;
								case "type":
									currentElement = xmlElement.Type;
									break;
								case "layout":
									currentElement = xmlElement.Layout;
									break;
								case "layoutoverride":
									currentElement = xmlElement.LayoutOverride;
									break;
								case "layoutsub":
									currentElement = xmlElement.LayoutSub;
									break;
								case "labels":
									currentElement = xmlElement.Labels;
									break;
								case "database":
									currentElement = xmlElement.Database;
									break;
								case "executable":
									currentElement = xmlElement.Executable;
									break;
								case "windowtitle":
									currentElement = xmlElement.WindowTitle;
									break;
								case "windowclass":
									currentElement = xmlElement.WindowClass;
									break;
								case "useexe":
									currentElement = xmlElement.UseExe;
									break;
								case "screenshot":
									currentElement = xmlElement.Screenshot;
									break;
								case "minimize":
									currentElement = xmlElement.Minimize;
									break;
								case "maximize":
									currentElement = xmlElement.Maximize;
									break;
								case "showkey":
									currentElement = xmlElement.ShowKey;
									break;
								case "hidekey":
									currentElement = xmlElement.HideKey;
									break;
								case "showsendkeys":
									currentElement = xmlElement.ShowSendKeys;
									break;
								case "hidesendkeys":
									currentElement = xmlElement.HideSendKeys;
									break;
								case "manuals":
									currentElement = xmlElement.Manuals;
									break;
								case "opcards":
									currentElement = xmlElement.OpCards;
									break;
								case "snaps":
									currentElement = xmlElement.Snaps;
									break;
								case "titles":
									currentElement = xmlElement.Titles;
									break;
								case "carts":
									currentElement = xmlElement.Carts;
									break;
								case "boxes":
									currentElement = xmlElement.Boxes;
									break;
								case "nfo":
									currentElement = xmlElement.Nfo;
									break;
								default:
									currentElement = xmlElement.Nothing;
									break;
							}
							if (xmlTextReader.HasAttributes)
							{
								attribHash.Clear();
								while (xmlTextReader.MoveToNextAttribute())
								{
									attribHash.Add(xmlTextReader.Name.ToLower(), xmlTextReader.Value);
								}
							}
							switch (currentElement)
							{
								case xmlElement.Profile:
									break;
								default:
									break;
							}
							xmlTextReader.MoveToElement();
							break;

						case XmlNodeType.Text:
							string text = xmlTextReader.Value.Trim();
							if (profile != null)
							{
								switch (currentElement)
								{
									case xmlElement.Enabled:
										profile.Enabled = StringTools.FromString<bool>(text);
										break;
									case xmlElement.Name:
										profile.Name = text;
										break;
									case xmlElement.Type:
										profile.Type = text;
										break;
									case xmlElement.Layout:
										profile.Layout = text;
										break;
									case xmlElement.LayoutOverride:
										profile.LayoutOverride = text;
										break;
									case xmlElement.LayoutSub:
										profile.LayoutSub = text;
										break;
									case xmlElement.Labels:
										profile.Labels = text;
										break;
									case xmlElement.Database:
										profile.Database = text;
										break;
									case xmlElement.Executable:
										profile.Executable = text;
										break;
									case xmlElement.WindowTitle:
										profile.WindowTitle = text;
										break;
									case xmlElement.WindowClass:
										profile.WindowClass = text;
										break;
									case xmlElement.UseExe:
										profile.UseExe = StringTools.FromString<bool>(text);
										break;
									case xmlElement.Screenshot:
										profile.Screenshot = StringTools.FromString<bool>(text);
										break;
									case xmlElement.Minimize:
										profile.Minimize = StringTools.FromString<bool>(text);
										break;
									case xmlElement.Maximize:
										profile.Maximize = StringTools.FromString<bool>(text);
										break;
									case xmlElement.ShowKey:
										profile.ShowKey = text;
										break;
									case xmlElement.HideKey:
										profile.HideKey = text;
										break;
									case xmlElement.ShowSendKeys:
										profile.ShowSendKeys = text;
										break;
									case xmlElement.HideSendKeys:
										profile.HideSendKeys = text;
										break;
									case xmlElement.Manuals:
										profile.Manuals = text;
										break;
									case xmlElement.OpCards:
										profile.OpCards = text;
										break;
									case xmlElement.Snaps:
										profile.Snaps = text;
										break;
									case xmlElement.Titles:
										profile.Titles = text;
										break;
									case xmlElement.Carts:
										profile.Carts = text;
										break;
									case xmlElement.Boxes:
										profile.Boxes = text;
										break;
									case xmlElement.Nfo:
										profile.Nfo = text;
										break;
									default:
										break;
								}
							}
							break;

						case XmlNodeType.EndElement:
							switch (currentElement)
							{
								case xmlElement.Profile:
									profile = null;
									break;
								default:
									break;
							}
							break;
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ReadProfileXml", "Profiles", ex.Message, ex.StackTrace);

				return false;
			}
			finally
			{
				if (xmlTextReader != null)
					xmlTextReader.Close();
			}

			return true;
		}

		public void WriteProfileXml(ProfileNode profile)
		{
			XmlTextWriter xmlTextWriter = null;

			try
			{
				xmlTextWriter = new XmlTextWriter(profile.FileName, null);

				xmlTextWriter.Formatting = Formatting.Indented;

				xmlTextWriter.WriteStartDocument();

				xmlTextWriter.WriteStartElement("Profile");

				xmlTextWriter.WriteStartElement("Enabled");
				xmlTextWriter.WriteString(profile.Enabled.ToString());
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteStartElement("Name");
				xmlTextWriter.WriteString(profile.Name);
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteStartElement("Type");
				xmlTextWriter.WriteString(profile.Type);
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteStartElement("Layout");
				xmlTextWriter.WriteString(profile.Layout);
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteStartElement("LayoutOverride");
				xmlTextWriter.WriteString(profile.LayoutOverride);
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteStartElement("LayoutSub");
				xmlTextWriter.WriteString(profile.LayoutSub);
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteStartElement("Labels");
				xmlTextWriter.WriteString(profile.Labels);
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteStartElement("Database");
				xmlTextWriter.WriteString(profile.Database);
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteStartElement("Executable");
				xmlTextWriter.WriteString(profile.Executable);
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteStartElement("WindowTitle");
				xmlTextWriter.WriteString(profile.WindowTitle);
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteStartElement("WindowClass");
				xmlTextWriter.WriteString(profile.WindowClass);
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteStartElement("UseExe");
				xmlTextWriter.WriteString(profile.UseExe.ToString());
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteStartElement("Screenshot");
				xmlTextWriter.WriteString(profile.Screenshot.ToString());
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteStartElement("Maximize");
				xmlTextWriter.WriteString(profile.Maximize.ToString());
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteStartElement("Minimize");
				xmlTextWriter.WriteString(profile.Minimize.ToString());
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteStartElement("ShowKey");
				xmlTextWriter.WriteString(profile.ShowKey);
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteStartElement("HideKey");
				xmlTextWriter.WriteString(profile.HideKey);
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteStartElement("ShowSendKeys");
				xmlTextWriter.WriteString(profile.ShowSendKeys);
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteStartElement("HideSendKeys");
				xmlTextWriter.WriteString(profile.HideSendKeys);
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteStartElement("Manuals");
				xmlTextWriter.WriteString(profile.Manuals);
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteStartElement("OpCards");
				xmlTextWriter.WriteString(profile.OpCards);
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteStartElement("Snaps");
				xmlTextWriter.WriteString(profile.Snaps);
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteStartElement("Titles");
				xmlTextWriter.WriteString(profile.Titles);
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteStartElement("Carts");
				xmlTextWriter.WriteString(profile.Carts);
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteStartElement("Boxes");
				xmlTextWriter.WriteString(profile.Boxes);
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteStartElement("Nfo");
				xmlTextWriter.WriteString(profile.Nfo);
				xmlTextWriter.WriteEndElement();

				xmlTextWriter.WriteEndElement();

				xmlTextWriter.WriteEndDocument();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("WriteProfileXml", "Profiles", ex.Message, ex.StackTrace);
			}
			finally
			{
				if (xmlTextWriter != null)
				{
					xmlTextWriter.Flush();
					xmlTextWriter.Close();
				}
			}
		}

		#region IDisposable Members

		public void Dispose()
		{
			ProfileList.Clear();
		}

		#endregion
	}
}
