// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CPWizard
{
	public class MAMEXml : IDisposable
	{
		private enum XmlElement
		{
			Nothing,
			MAME,
			Machine,
			Description,
			Year,
			Manufacturer,
			ROM,
			DeviceRef,
			Chip,
			Display,
			Input,
			DipSwitch,
			DipValue,
			Control,
			Driver,
			Instance,
			Extension,
			Device,
			SoftwareList,
			ControlsDat,
			CatVer,
			NPlayers,
			HallOfFame,
			AAMARating
		}

		private string m_mameConfig = String.Empty;

		private List<MAMEMachineNode> m_machineList = null;
		private List<MAMEMachineNode> m_gameList = null;

		private Dictionary<string, MAMEMachineNode> m_machineDictionary = null;
		private Dictionary<string, MAMEMachineNode> m_gameDictionary = null;

		public MAMEXml()
		{
			m_machineList = new List<MAMEMachineNode>();
			m_gameList = new List<MAMEMachineNode>();
			m_machineDictionary = new Dictionary<string, MAMEMachineNode>();
			m_gameDictionary = new Dictionary<string, MAMEMachineNode>();
		}

		public bool CreateListInfoXml(string mameFileName, string xmlPath)
		{
			return FileIO.LaunchFileCaptureOutput(mameFileName, "-listxml", xmlPath);
		}

		public bool CreateMediaTxt(string mameFileName, string txtPath)
		{
			return FileIO.LaunchFileCaptureOutput(mameFileName, "-listmedia", txtPath);
		}

		public static bool TryGetMAMEVersion(string mameFileName, out string versionString)
		{
			versionString = null;

			try
			{
				if (!FileIO.TryLaunchFileCaptureOutput(mameFileName, "-help", out versionString))
					return false;

				versionString = versionString.Substring(versionString.IndexOf("v"));
				versionString = versionString.Substring(1, Math.Min(versionString.IndexOf(" "), versionString.IndexOf("\r\n")) - 1);

				return true;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("TryGetMAMEVersion", "MAMEXml", ex.Message, ex.StackTrace);
			}

			return false;
		}

		public static bool TryGetMAMEROMInfoXml(string mameFileName, string romName, out MemoryStream memoryStream)
		{
			memoryStream = null;

			try
			{
				string outputString = null;

				if (!FileIO.TryLaunchFileCaptureOutput(mameFileName, String.Format("-listxml {0}", romName), out outputString))
					return false;

				memoryStream = new MemoryStream(StringTools.FromString<byte[]>(outputString));

				return true;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetMAMEROMInfoXml", "MAMEXml", ex.Message, ex.StackTrace);
			}

			return false;
		}

		public static bool TryGetMAMEROMClones(string mameFileName, string romName, out string outputString)
		{
			return FileIO.TryLaunchFileCaptureOutput(mameFileName, String.Format("{0} -listclones", romName), out outputString);
		}

		public static bool TryGetMAMEROMDescription(string mameFileName, string romName, out string outputString)
		{
			return FileIO.TryLaunchFileCaptureOutput(mameFileName, String.Format("{0} -ll", romName), out outputString);
		}

		public static bool TryGetMAMEROMDriver(string mameFileName, string romName, out string outputString)
		{
			return FileIO.TryLaunchFileCaptureOutput(mameFileName, String.Format("{0} -ls", romName), out outputString);
		}

		public void ReadControlsXml(XmlReader xmlReader, MAMEMachineNode machineNode)
		{
			ControlsDat.XmlElement currentElement = ControlsDat.XmlElement.Nothing;
			Hashtable attribHash = new Hashtable();

			ControlsDatNode currentGame = null;
			ControlsDatPlayerNode currentPlayer = null;
			ControlsDatControlNode currentControl = null;
			ControlsDatLabelNode currentLabel = null;

			try
			{
				do
				{
					switch (xmlReader.NodeType)
					{
						case XmlNodeType.Element:
							switch (xmlReader.LocalName.ToLower())
							{
								case "controlsdat":
									currentElement = ControlsDat.XmlElement.Game;
									break;
								case "miscdetails":
									currentElement = ControlsDat.XmlElement.MiscDetails;
									break;
								case "player":
									currentElement = ControlsDat.XmlElement.Player;
									break;
								case "control":
									currentElement = ControlsDat.XmlElement.Control;
									break;
								case "constant":
									currentElement = ControlsDat.XmlElement.Constant;
									break;
								case "button":
									currentElement = ControlsDat.XmlElement.Button;
									break;
								case "label":
									currentElement = ControlsDat.XmlElement.Label;
									break;
								default:
									currentElement = ControlsDat.XmlElement.Nothing;
									break;
							}
							if (xmlReader.HasAttributes)
							{
								attribHash.Clear();

								while (xmlReader.MoveToNextAttribute())
									attribHash.Add(xmlReader.Name.ToLower(), xmlReader.Value);
							}
							switch (currentElement)
							{
								case ControlsDat.XmlElement.Game:
									machineNode.ControlsDat = currentGame = new ControlsDatNode((string)attribHash["romname"], (string)attribHash["gamename"], StringTools.FromString<int>((string)attribHash["numplayers"]), StringTools.FromString<int>((string)attribHash["alternating"]), StringTools.FromString<int>((string)attribHash["mirrored"]), StringTools.FromString<int>((string)attribHash["usesservice"]), StringTools.FromString<int>((string)attribHash["tilt"]), StringTools.FromString<int>((string)attribHash["cocktail"]));
									break;
								case ControlsDat.XmlElement.MiscDetails:
									break;
								case ControlsDat.XmlElement.Player:
									if (currentGame != null)
									{
										currentPlayer = new ControlsDatPlayerNode(StringTools.FromString<int>((string)attribHash["number"]), StringTools.FromString<int>((string)attribHash["numbuttons"]));
										currentGame.PlayerList.Add(currentPlayer);
									}
									break;
								case ControlsDat.XmlElement.Control:
									if (currentPlayer != null)
									{
										currentControl = new ControlsDatControlNode((string)attribHash["name"]);
										currentPlayer.ControlList.Add(currentControl);
									}
									break;
								case ControlsDat.XmlElement.Constant:
									if (currentControl != null)
									{
										currentControl.ConstantList.Add((string)attribHash["name"]);
									}
									break;
								case ControlsDat.XmlElement.Button:
									if (currentControl != null)
									{
										currentControl.ButtonList.Add((string)attribHash["name"]);
									}
									break;
								case ControlsDat.XmlElement.Label:
									if (currentPlayer != null)
									{
										currentLabel = new ControlsDatLabelNode((string)attribHash["name"], (string)attribHash["value"], (string)attribHash["color"]);
										currentPlayer.LabelList.Add(currentLabel);
									}
									break;
								default:
									break;
							}

							xmlReader.MoveToElement();
							break;

						case XmlNodeType.Text:
							string text = xmlReader.Value.Trim();

							switch (currentElement)
							{
								case ControlsDat.XmlElement.MiscDetails:
									currentGame.MiscDetails = text;
									break;
								default:
									break;
							}
							break;

						case XmlNodeType.EndElement:
							if (xmlReader.LocalName.ToLower() == "controlsdat")
								return;

							switch (currentElement)
							{
								case ControlsDat.XmlElement.Game:
									currentGame = null;
									break;
								case ControlsDat.XmlElement.MiscDetails:
									break;
								case ControlsDat.XmlElement.Player:
									currentPlayer = null;
									break;
								case ControlsDat.XmlElement.Control:
									currentControl = null;
									break;
								case ControlsDat.XmlElement.Constant:
									break;
								case ControlsDat.XmlElement.Button:
									break;
								case ControlsDat.XmlElement.Label:
									currentLabel = null;
									break;
								default:
									break;
							}
							break;
					}
				} while (xmlReader.Read());
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ReadControlsXml", "MAMEXml", ex.Message, ex.StackTrace);
			}
		}

		/* public MachineNode GetParentROM(MachineNode machineNode)
		{
			MachineNode parentMachineNode = machineNode;

			try
			{
				do
				{
					if (parentMachineNode.CloneOf != null) // Game has a clone?
						m_machineDictionary.TryGetValue(parentMachineNode.CloneOf, out parentMachineNode);

					if (parentMachineNode.ROMOf != null)
						m_machineDictionary.TryGetValue(parentMachineNode.ROMOf, out parentMachineNode);
				}
				while (parentMachineNode.ROMOf != null);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetParentROM", "MAMEXml", ex.Message, ex.StackTrace);
			}

			return (machineNode != parentMachineNode ? parentMachineNode : null);
		} */

		public bool TryGetParentROM(MAMEMachineNode machineNode, out MAMEMachineNode parentMachineNode)
		{
			parentMachineNode = null;

			MAMEMachineNode findParentMachineNode = machineNode;

			do
			{
				if (findParentMachineNode.CloneOf != null)
					if (m_machineDictionary.TryGetValue(findParentMachineNode.CloneOf, out parentMachineNode))
						findParentMachineNode = parentMachineNode;

				if (findParentMachineNode.ROMOf != null)
					if (m_machineDictionary.TryGetValue(findParentMachineNode.ROMOf, out parentMachineNode))
						findParentMachineNode = parentMachineNode;

				if (findParentMachineNode.SampleOf != null)
					if (m_machineDictionary.TryGetValue(findParentMachineNode.SampleOf, out parentMachineNode))
						findParentMachineNode = parentMachineNode;
			}
			while (findParentMachineNode.CloneOf != null || findParentMachineNode.ROMOf != null || findParentMachineNode.SampleOf != null);

			return true;
		}

		public void GetParentROMs()
		{
			foreach (MAMEMachineNode machineNode in m_machineList)
			{
				if (machineNode.CloneOf != null)
					m_machineDictionary.TryGetValue(machineNode.CloneOf, out machineNode.Clone);

				if (machineNode.ROMOf != null)
					m_machineDictionary.TryGetValue(machineNode.ROMOf, out machineNode.ROM);

				if (machineNode.SampleOf != null)
					m_machineDictionary.TryGetValue(machineNode.SampleOf, out machineNode.Sample);

				TryGetParentROM(machineNode, out machineNode.Parent);
			}
		}

		public void CreateGameList()
		{
			m_gameList.Clear();
			m_gameDictionary.Clear();

			foreach (MAMEMachineNode machineNode in m_machineList)
			{
				if (Globals.MAMEFilter.ShouldRemove(machineNode, false))
					continue;

				m_gameList.Add(machineNode);
				m_gameDictionary.Add(machineNode.Name, machineNode);
			}
		}

		public void AddData()
		{
			string mameDataPath = Path.Combine(Settings.Folders.Data, "MAME");
			//Dictionary<string, AAMARating> aamaRatingDictionary = Ratings.GetRatingsDictionary(Path.Combine(mameDataPath, "Ratings.ini"));

			foreach (MAMEMachineNode machineNode in m_machineList)
			{
				ControlsDatNode controlsDat = null;
				CatVerNode catVer = null;
				NPlayersNode nPlayers = null;
				HallOfFameNode hallOfFame = null;
				//HistoryDatNode historyDat = null;
				//MAMEInfoDatNode mameInfoDat = null;
				//CommandDatNode commandDat = null;
				//StoryDatNode storyDat = null;
				//AAMARating aamaRating = AAMARating.None;

				if (Globals.ControlsDat != null && Globals.ControlsDat.GameDictionary.TryGetValue(machineNode.Name, out controlsDat))
					machineNode.ControlsDat = controlsDat;

				if (Globals.CatVer != null && Globals.CatVer.GameDictionary.TryGetValue(machineNode.Name, out catVer))
					machineNode.CatVer = catVer;

				if (Globals.NPlayers != null && Globals.NPlayers.GameDictionary.TryGetValue(machineNode.Name, out nPlayers))
					machineNode.NPlayers = nPlayers;

				if (Globals.HallOfFameXml != null && Globals.HallOfFameXml.GameDictionary.TryGetValue(machineNode.Name, out hallOfFame))
					machineNode.HallOfFame = hallOfFame;

				// -------------------------------------------------------------------------------------

				/* if (Globals.HistoryDat != null && Globals.HistoryDat.SystemDictionary.TryGetValue("mame", out historyDat))
				{
					HistoryDatGameNode historyDatGameNode = null;

					if (historyDat.GameDictionary.TryGetValue(machineNode.Name, out historyDatGameNode))
						machineNode.HistoryDatList = historyDatGameNode.HistoryDatBiographyList;
				}

				if (Globals.MAMEInfoDat != null && Globals.MAMEInfoDat.GameDictionary.TryGetValue(machineNode.Name, out mameInfoDat))
					machineNode.MAMEInfoDat = mameInfoDat;

				if (Globals.CommandDat != null && Globals.CommandDat.GameDictionary.TryGetValue(machineNode.Name, out commandDat))
					machineNode.CommandDat = commandDat;

				if (Globals.StoryDat != null && Globals.StoryDat.GameDictionary.TryGetValue(machineNode.Name, out storyDat))
					machineNode.StoryDat = storyDat;

				if (aamaRatingDictionary.TryGetValue(machineNode.Name, out aamaRating))
					machineNode.AAMARating = aamaRating; */
			}
		}

		/* private void GetListInfoXmlVersion(string xmlPath)
		{
			XmlReader xmlReader = null;
			Dictionary<string, string> attribDictionary = new Dictionary<string, string>();
			XmlDocument xmlDocument = new XmlDocument();

			if (!File.Exists(xmlPath))
				return;

			try
			{
				xmlReader = new XmlTextReader(xmlPath);

				xmlDocument.Load(xmlPath);

				XmlNode xmlNode = xmlDocument.DocumentElement.SelectSingleNode("mame");

				attribDictionary.Clear();

				foreach (XmlNode xmlAttributes in xmlNode.Attributes)
					attribDictionary.Add(xmlAttributes.Name.ToLower(), xmlAttributes.Value);

				System.Windows.Forms.MessageBox.Show(attribDictionary["build"]);

				xmlReader.Close();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("FindROMData", "MAMEXml", ex.Message, ex.StackTrace);
			}
		} */

		private void FindROMData(ref XmlReader xmlReader, ref XmlWriter xmlWriter, ref MemoryStream memoryStream, string xmlPath, string romName)
		{
			Dictionary<string, string> attribDictionary = new Dictionary<string, string>();
			XmlDocument xmlDocument = new XmlDocument();
			memoryStream = new MemoryStream();

			try
			{
				xmlDocument.Load(xmlPath);

				xmlWriter = new XmlTextWriter(memoryStream, System.Text.Encoding.Default);

				xmlWriter.WriteStartDocument();
				xmlWriter.WriteStartElement("Games");

				XmlNode XmlNode = xmlDocument.DocumentElement.SelectSingleNode(string.Format("Game[@Name='{0}']", romName));

				if (XmlNode != null)
				{
					XmlNode.WriteTo(xmlWriter);

					foreach (XmlNode xmlNode in XmlNode.Attributes)
						attribDictionary.Add(xmlNode.Name.ToLower(), xmlNode.Value);
				}

				string ParentName = null;

				do
				{
					if (attribDictionary.TryGetValue("cloneof", out ParentName))
					{
						XmlNode = xmlDocument.DocumentElement.SelectSingleNode(string.Format("Game[@Name='{0}']", ParentName));

						if (XmlNode != null)
							XmlNode.WriteTo(xmlWriter);

						attribDictionary.Clear();

						foreach (XmlNode xmlNode in XmlNode.Attributes)
							attribDictionary.Add(xmlNode.Name.ToLower(), xmlNode.Value);
					}

					if (attribDictionary.TryGetValue("romof", out ParentName))
					{
						XmlNode = xmlDocument.DocumentElement.SelectSingleNode(string.Format("Game[@Name='{0}']", ParentName));

						if (XmlNode != null)
							XmlNode.WriteTo(xmlWriter);

						attribDictionary.Clear();

						foreach (XmlNode xmlNode in XmlNode.Attributes)
							attribDictionary.Add(xmlNode.Name.ToLower(), xmlNode.Value);
					}
				}
				while (attribDictionary.TryGetValue("romof", out ParentName));

				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndDocument();
				xmlWriter.Flush();

				xmlReader.Close();

				memoryStream.Position = 0;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("FindROMData", "MAMEXml", ex.Message, ex.StackTrace);
			}
		}

		public void ReadListInfoXml(string mameFileName, string xmlPath)
		{
			ReadListInfoXml(mameFileName, xmlPath, null);
		}

		public void ReadListInfoXml(string mameFileName, string xmlPath, string romName)
		{
			XmlElement currentElement = XmlElement.Nothing;
			Hashtable attribHash = new Hashtable();

			MAMEMachineNode currentMachine = null;
			MAMEDisplayNode currentDisplay = null;
			MAMEInputNode currentInput = null;
			MAMEDipSwitchNode currentDipSwitch = null;
			MAMEControlNode currentControl = null;
			MAMEDeviceNode currentDevice = null;

			XmlReader xmlReader = null;
			XmlWriter xmlWriter = null;
			MemoryStream memoryStream = null;

			m_machineList.Clear();
			m_machineDictionary.Clear();

			if (!File.Exists(xmlPath))
				return;

			FileInfo fileInfo = new FileInfo(xmlPath);

			if (fileInfo.Length == 0)
				return;

			try
			{
				xmlReader = new XmlTextReader(xmlPath);

				if (romName != null)
				{
					FindROMData(ref xmlReader, ref xmlWriter, ref memoryStream, xmlPath, romName);
					xmlReader = new XmlTextReader(memoryStream);
				}

				xmlReader.Read();

				while (xmlReader.Read())
				{
					switch (xmlReader.NodeType)
					{
						case XmlNodeType.Element:
							switch (xmlReader.LocalName.ToLower())
							{
								case "mame":
									currentElement = XmlElement.MAME;
									break;
								case "game": // backward compatibility for mameconfig < 10
								case "machine":
									currentElement = XmlElement.Machine;
									break;
								case "description":
									currentElement = XmlElement.Description;
									break;
								case "year":
									currentElement = XmlElement.Year;
									break;
								case "manufacturer":
									currentElement = XmlElement.Manufacturer;
									break;
								case "rom":
									currentElement = XmlElement.ROM;
									break;
								case "device_ref":
									currentElement = XmlElement.DeviceRef;
									break;
								case "chip":
									currentElement = XmlElement.Chip;
									break;
								case "display":
									currentElement = XmlElement.Display;
									break;
								case "input":
									currentElement = XmlElement.Input;
									break;
								case "dipswitch":
									currentElement = XmlElement.DipSwitch;
									break;
								case "dipvalue":
									currentElement = XmlElement.DipValue;
									break;
								case "control":
									currentElement = XmlElement.Control;
									break;
								case "driver":
									currentElement = XmlElement.Driver;
									break;
								case "instance":
									currentElement = XmlElement.Instance;
									break;
								case "extension":
									currentElement = XmlElement.Extension;
									break;
								case "device":
									currentElement = XmlElement.Device;
									break;
								case "softwarelist":
									currentElement = XmlElement.SoftwareList;
									break;
								case "controlsdat":
									currentElement = XmlElement.ControlsDat;
									ReadControlsXml(xmlReader, currentMachine);
									break;
								case "catver":
									currentElement = XmlElement.CatVer;
									break;
								case "nplayers":
									currentElement = XmlElement.NPlayers;
									break;
								case "halloffame":
									currentElement = XmlElement.HallOfFame;
									break;
								case "aamarating":
									currentElement = XmlElement.AAMARating;
									break;
								default:
									currentElement = XmlElement.Nothing;
									break;
							}
							if (xmlReader.HasAttributes)
							{
								attribHash.Clear();

								while (xmlReader.MoveToNextAttribute())
									attribHash.Add(xmlReader.Name.ToLower(), xmlReader.Value);
							}
							switch (currentElement)
							{
								case XmlElement.MAME:
									m_mameConfig = (string)attribHash["mameconfig"];
									break;
								case XmlElement.Machine:
									string name = (string)attribHash["name"];
									bool runnable = (attribHash.ContainsKey("runnable") ? StringTools.FromString<bool>((string)attribHash["runnable"]) : true);

									currentMachine = new MAMEMachineNode(name, (string)attribHash["sourcefile"], (string)attribHash["cloneof"], (string)attribHash["romof"], (string)attribHash["samepleof"], StringTools.FromString<bool>((string)attribHash["isbios"]), StringTools.FromString<bool>((string)attribHash["ismechanical"]), StringTools.FromString<bool>((string)attribHash["isdevice"]), runnable);

									m_machineList.Add(currentMachine);

									if (!m_machineDictionary.ContainsKey(name))
										m_machineDictionary.Add(name, currentMachine);
									break;
								case XmlElement.ROM:
									if (currentMachine != null)
										currentMachine.ROMList.Add(new MAMEROMNode((string)attribHash["name"], (string)attribHash["merge"], (string)attribHash["bios"], StringTools.FromString<ulong>((string)attribHash["size"]), StringTools.FromHexToLong((string)attribHash["crc"]), (string)attribHash["sha1"], (string)attribHash["region"], (string)attribHash["offset"], StringTools.FromString<MAMEROMStatus>((string)attribHash["status"]), StringTools.FromString<bool>((string)attribHash["optional"])));
									break;
								case XmlElement.DeviceRef:
									if (currentMachine != null)
										currentMachine.DeviceRefList.Add(new MAMEDeviceRefNode((string)attribHash["name"]));
									break;
								case XmlElement.Chip:
									if (currentMachine != null)
										currentMachine.ChipList.Add(new MAMEChipNode((string)attribHash["type"], (string)attribHash["name"], (string)attribHash["clock"]));
									break;
								case XmlElement.Display:
									if (currentMachine != null)
										currentMachine.DisplayList.Add(currentDisplay = new MAMEDisplayNode((string)attribHash["type"], StringTools.FromString<int>((string)attribHash["rotate"]), StringTools.FromString<int>((string)attribHash["width"]), StringTools.FromString<int>((string)attribHash["height"]), StringTools.FromString<float>((string)attribHash["refresh"])));
									break;
								case XmlElement.Input:
									if (currentMachine != null)
										currentMachine.InputList.Add(currentInput = new MAMEInputNode(StringTools.FromString<int>((string)attribHash["players"]), StringTools.FromString<int>((string)attribHash["coins"]), StringTools.FromString<bool>((string)attribHash["service"]), StringTools.FromString<bool>((string)attribHash["tilt"])));
									break;
								case XmlElement.DipSwitch:
									if (currentMachine != null)
										currentMachine.DipSwitchList.Add(currentDipSwitch = new MAMEDipSwitchNode((string)attribHash["name"]));
									break;
								case XmlElement.DipValue:
									if (currentDipSwitch != null)
										currentDipSwitch.DipValueList.Add(new MAMEDipValueNode((string)attribHash["name"], (string)attribHash["value"], (string)attribHash["default"]));
									break;
								case XmlElement.Control:
									if (currentInput != null)
										currentInput.ControlList.Add(currentControl = new MAMEControlNode((string)attribHash["type"], StringTools.FromString<int>((string)attribHash["player"]), StringTools.FromString<int>((string)attribHash["buttons"]), (string)attribHash["ways"], (string)attribHash["ways2"], (string)attribHash["ways3"], StringTools.FromString<int>((string)attribHash["minimum"]), StringTools.FromString<int>((string)attribHash["maximum"]), StringTools.FromString<int>((string)attribHash["sensitivity"]), StringTools.FromString<int>((string)attribHash["keydelta"]), StringTools.FromString<bool>((string)attribHash["reverse"])));
									break;
								case XmlElement.Driver:
									if (currentMachine != null)
										currentMachine.Driver = new MAMEDriverNode(StringTools.FromString<MAMEDriverStatus>((string)attribHash["status"]), StringTools.FromString<MAMEDriverStatus>((string)attribHash["emulation"]), StringTools.FromString<MAMEDriverStatus>((string)attribHash["color"]), StringTools.FromString<MAMEDriverStatus>((string)attribHash["sound"]), StringTools.FromString<MAMEDriverStatus>((string)attribHash["graphic"]), StringTools.FromString<MAMEDriverStatus>((string)attribHash["cocktail"]), StringTools.FromString<MAMEDriverStatus>((string)attribHash["protection"]), StringTools.FromString<MAMESaveState>((string)attribHash["savestate"]), StringTools.FromString<int>((string)attribHash["palettesize"]));
									break;
								case XmlElement.Instance:
									if (currentDevice != null)
										currentDevice.Instance = new MAMEInstanceNode((string)attribHash["name"], (string)attribHash["briefname"]);
									break;
								case XmlElement.Extension:
									if (currentDevice != null)
										currentDevice.ExtensionList.Add(new MAMEExtensionNode((string)attribHash["name"]));
									break;
								case XmlElement.Device:
									if (currentMachine != null)
										currentMachine.DeviceList.Add(currentDevice = new MAMEDeviceNode((string)attribHash["type"], (string)attribHash["tag"], (string)attribHash["mandatory"], (string)attribHash["interface"]));
									break;
								case XmlElement.SoftwareList:
									if (currentMachine != null)
										currentMachine.SoftwareList.Add(new MAMESoftwareListNode((string)attribHash["name"], (string)attribHash["status"], (string)attribHash["filter"]));
									break;
								case XmlElement.CatVer:
									if (currentMachine != null)
										currentMachine.CatVer = new CatVerNode((string)attribHash["name"], (string)attribHash["genre"], (string)attribHash["category"], (string)attribHash["veradded"], StringTools.FromString<bool>((string)attribHash["ismature"]));
									break;
								case XmlElement.NPlayers:
									if (currentMachine != null)
										currentMachine.NPlayers = new NPlayersNode((string)attribHash["name"], (string)attribHash["type"]);
									break;
								case XmlElement.HallOfFame:
									if (currentMachine != null)
										currentMachine.HallOfFame = new HallOfFameNode((string)attribHash["name"], StringTools.FromString<float>((string)attribHash["weightedaverage"]), StringTools.FromString<int>((string)attribHash["votes"]));
									break;
								default:
									break;
							}
							xmlReader.MoveToElement();
							break;

						case XmlNodeType.Text:
							string text = xmlReader.Value.Trim();

							switch (currentElement)
							{
								case XmlElement.Description:
									if (currentMachine != null)
										currentMachine.Description = text;
									break;
								case XmlElement.Year:
									if (currentMachine != null)
										currentMachine.Year = text;
									break;
								case XmlElement.Manufacturer:
									if (currentMachine != null)
										currentMachine.Manufacturer = text;
									break;
								//case XmlElement.AAMARating:
								//	if (currentMachine != null)
								//		currentMachine.AAMARating = StringTools.FromString<AAMARating>(text);
								//	break;
								default:
									break;
							}
							break;

						case XmlNodeType.EndElement:
							switch (currentElement)
							{
								case XmlElement.MAME:
									break;
								case XmlElement.Machine:
									currentMachine = null;
									break;
								case XmlElement.Display:
									currentDisplay = null;
									break;
								case XmlElement.Input:
									currentInput = null;
									break;
								case XmlElement.DipSwitch:
									currentDipSwitch = null;
									break;
								case XmlElement.Control:
									currentControl = null;
									break;
								case XmlElement.Device:
									currentDevice = null;
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
				LogFile.WriteLine("ReadListInfoXml", "MAMEXml", ex.Message, ex.StackTrace);
			}
			finally
			{
				if (xmlReader != null)
				{
					xmlReader.Close();
					xmlReader = null;
				}
				if (xmlWriter != null)
				{
					xmlWriter.Close();
					xmlWriter = null;
				}
				if (memoryStream != null)
				{
					memoryStream.Close();
					memoryStream.Dispose();
					memoryStream = null;
				}
			}
		}

		public void WriteNPlayersXml(XmlWriter xmlWriter, NPlayersNode nPlayers)
		{
			try
			{
				xmlWriter.WriteStartElement("nplayers");

				xmlWriter.WriteAttributeString("type", nPlayers.Type);

				xmlWriter.WriteEndElement();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("WriteNPlayersXml", "MAMEXml", ex.Message, ex.StackTrace);
			}
		}

		public void WriteCatVerXml(XmlWriter xmlWriter, CatVerNode catVer)
		{
			try
			{
				xmlWriter.WriteStartElement("catver");

				xmlWriter.WriteAttributeString("genre", catVer.Genre);

				if (catVer.Category != null)
					xmlWriter.WriteAttributeString("category", catVer.Category);

				xmlWriter.WriteAttributeString("veradded", catVer.VerAdded);
				xmlWriter.WriteAttributeString("ismature", catVer.IsMature.ToString());

				xmlWriter.WriteEndElement();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("WriteCatVerXml", "MAMEXml", ex.Message, ex.StackTrace);
			}
		}

		public void WriteControlsDatXml(XmlWriter xmlWriter, ControlsDatNode controlsDat)
		{
			try
			{
				xmlWriter.WriteStartElement("controlsdat");

				xmlWriter.WriteAttributeString("numplayers", controlsDat.NumPlayers.ToString());
				xmlWriter.WriteAttributeString("alternating", controlsDat.Alternating.ToString());
				xmlWriter.WriteAttributeString("mirrored", controlsDat.Mirrored.ToString());
				xmlWriter.WriteAttributeString("usesservice", controlsDat.UsesService.ToString());
				xmlWriter.WriteAttributeString("tilt", controlsDat.Tilt.ToString());
				xmlWriter.WriteAttributeString("cocktail", controlsDat.Cocktail.ToString());

				xmlWriter.WriteStartElement("miscdetails");
				xmlWriter.WriteString(controlsDat.MiscDetails);
				xmlWriter.WriteEndElement();

				foreach (ControlsDatPlayerNode player in controlsDat.PlayerList)
				{
					xmlWriter.WriteStartElement("player");
					xmlWriter.WriteAttributeString("number", player.Number.ToString());
					xmlWriter.WriteAttributeString("numbuttons", player.NumButtons.ToString());

					xmlWriter.WriteStartElement("controls");

					foreach (ControlsDatControlNode control in player.ControlList)
					{
						xmlWriter.WriteStartElement("control");
						xmlWriter.WriteAttributeString("name", control.Name);

						foreach (string constant in control.ConstantList)
						{
							xmlWriter.WriteStartElement("constant");
							xmlWriter.WriteAttributeString("name", constant);
							xmlWriter.WriteEndElement();
						}

						foreach (string button in control.ButtonList)
						{
							xmlWriter.WriteStartElement("button");
							xmlWriter.WriteAttributeString("name", button);
							xmlWriter.WriteEndElement();
						}

						xmlWriter.WriteEndElement();
					}

					xmlWriter.WriteEndElement();
					xmlWriter.WriteStartElement("labels");

					foreach (ControlsDatLabelNode label in player.LabelList)
					{
						xmlWriter.WriteStartElement("label");
						xmlWriter.WriteAttributeString("name", label.Name);
						xmlWriter.WriteAttributeString("value", label.Value);

						if (!String.IsNullOrEmpty(label.Color))
							xmlWriter.WriteAttributeString("color", label.Color);

						xmlWriter.WriteEndElement();
					}

					xmlWriter.WriteEndElement();
					xmlWriter.WriteEndElement();
				}

				xmlWriter.WriteEndElement();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("WriteControlsDatXml", "MAMEXml", ex.Message, ex.StackTrace);
			}
		}

		public void WriteHallOfFameXml(XmlWriter xmlWriter, HallOfFameNode hallOfFame)
		{
			try
			{
				xmlWriter.WriteStartElement("halloffame");

				xmlWriter.WriteAttributeString("weightedaverage", hallOfFame.WeightedAverage.ToString());
				xmlWriter.WriteAttributeString("votes", hallOfFame.Votes.ToString());

				xmlWriter.WriteEndElement();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("WriteHallOfFameXml", "MAMEXml", ex.Message, ex.StackTrace);
			}
		}

		public void WriteMiniInfoXml(string fileName)
		{
			XmlWriter xmlWriter = null;

			try
			{
				XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
				xmlWriterSettings.Encoding = Encoding.UTF8;
				xmlWriterSettings.Indent = true;
				xmlWriterSettings.NewLineHandling = NewLineHandling.None; // NewLineHandling.Entitize;

				xmlWriter = XmlWriter.Create(fileName, xmlWriterSettings);

				xmlWriter.WriteStartDocument();
				xmlWriter.WriteStartElement("mame");

				foreach (MAMEMachineNode machineNode in m_machineList)
				{
					xmlWriter.WriteStartElement("machine");
					xmlWriter.WriteAttributeString("name", machineNode.Name);

					if (machineNode.SourceFile != null)
						xmlWriter.WriteAttributeString("sourcefile", machineNode.SourceFile);

					if (machineNode.CloneOf != null)
						xmlWriter.WriteAttributeString("cloneof", machineNode.CloneOf);

					if (machineNode.ROMOf != null)
						xmlWriter.WriteAttributeString("romof", machineNode.ROMOf);

					if (machineNode.SampleOf != null)
						xmlWriter.WriteAttributeString("samepleof", machineNode.SampleOf);

					xmlWriter.WriteAttributeString("isbios", machineNode.IsBios.ToString());
					xmlWriter.WriteAttributeString("ismechanical", machineNode.IsMechanical.ToString());
					xmlWriter.WriteAttributeString("isdevice", machineNode.IsDevice.ToString());
					xmlWriter.WriteAttributeString("runnable", machineNode.Runnable.ToString());

					xmlWriter.WriteStartElement("description");
					xmlWriter.WriteString(machineNode.Description);
					xmlWriter.WriteEndElement();

					xmlWriter.WriteStartElement("year");
					xmlWriter.WriteString(machineNode.Year);
					xmlWriter.WriteEndElement();

					xmlWriter.WriteStartElement("manufacturer");
					xmlWriter.WriteString(machineNode.Manufacturer);
					xmlWriter.WriteEndElement();

					/* foreach (MAMEROMNode rom in machineNode.ROMList)
					{
						xmlWriter.WriteStartElement("rom");
						xmlWriter.WriteAttributeString("name", rom.Name);
						if (rom.Merge != null)
							xmlWriter.WriteAttributeString("merge", rom.Merge);
						//xmlWriter.WriteAttributeString("bios", rom.Bios);
						xmlWriter.WriteAttributeString("size", rom.Size.ToString());
						xmlWriter.WriteAttributeString("crc", rom.Crc);
						//xmlWriter.WriteAttributeString("sha1", rom.Sha1);
						//xmlWriter.WriteAttributeString("region", rom.Region);
						//xmlWriter.WriteAttributeString("offset", rom.Offset);
						xmlWriter.WriteAttributeString("status", rom.Status.ToString());
						xmlWriter.WriteAttributeString("optional", rom.Optional.ToString());
						xmlWriter.WriteEndElement();
					} */

					foreach (MAMEDeviceRefNode deviceRef in machineNode.DeviceRefList)
					{
						xmlWriter.WriteStartElement("device_ref");
						xmlWriter.WriteAttributeString("name", deviceRef.Name);
						xmlWriter.WriteEndElement();
					}

					foreach (MAMEChipNode chip in machineNode.ChipList)
					{
						xmlWriter.WriteStartElement("chip");
						xmlWriter.WriteAttributeString("type", chip.Type);
						xmlWriter.WriteAttributeString("name", chip.Name);
						xmlWriter.WriteAttributeString("clock", chip.Clock);
						xmlWriter.WriteEndElement();
					}

					foreach (MAMEDisplayNode display in machineNode.DisplayList)
					{
						xmlWriter.WriteStartElement("display");
						xmlWriter.WriteAttributeString("type", display.Type);
						xmlWriter.WriteAttributeString("rotate", display.Rotate.ToString());
						xmlWriter.WriteAttributeString("width", display.Width.ToString());
						xmlWriter.WriteAttributeString("height", display.Height.ToString());
						xmlWriter.WriteAttributeString("refresh", display.Refresh.ToString());
						xmlWriter.WriteEndElement();
					}

					foreach (MAMEInputNode input in machineNode.InputList)
					{
						xmlWriter.WriteStartElement("input");
						xmlWriter.WriteAttributeString("players", input.Players.ToString());
						//xmlWriter.WriteAttributeString("buttons", input.Buttons.ToString());
						xmlWriter.WriteAttributeString("coins", input.Coins.ToString());
						xmlWriter.WriteAttributeString("service", input.Service.ToString());
						xmlWriter.WriteAttributeString("tilt", input.Tilt.ToString());

						foreach (MAMEControlNode control in input.ControlList)
						{
							xmlWriter.WriteStartElement("control");
							xmlWriter.WriteAttributeString("type", control.Type);
							xmlWriter.WriteAttributeString("player", control.Player.ToString());
							xmlWriter.WriteAttributeString("buttons", control.Buttons.ToString());
							xmlWriter.WriteAttributeString("ways", control.Ways);
							xmlWriter.WriteAttributeString("ways2", control.Ways2);
							xmlWriter.WriteAttributeString("ways3", control.Ways3);
							xmlWriter.WriteAttributeString("minimum", control.Minimum.ToString());
							xmlWriter.WriteAttributeString("maximum", control.Maximum.ToString());
							xmlWriter.WriteAttributeString("sensitivity", control.Sensitivity.ToString());
							xmlWriter.WriteAttributeString("keydelta", control.KeyDelta.ToString());
							xmlWriter.WriteAttributeString("reverse", control.Reverse.ToString());
							xmlWriter.WriteEndElement();
						}

						xmlWriter.WriteEndElement();
					}

					/* foreach (MAMEDipSwitchNode dipSwitch in machineNode.DipSwitchList)
					{
						xmlWriter.WriteStartElement("dipswitch");
						xmlWriter.WriteAttributeString("name", dipSwitch.Name);

						foreach (MAMEDipValueNode dipValue in dipSwitch.DipValueList)
						{
							xmlWriter.WriteStartElement("dipvalue");
							xmlWriter.WriteAttributeString("name", dipValue.Name);
							xmlWriter.WriteAttributeString("value", dipValue.Value);

							if (dipValue.Default != null)
								xmlWriter.WriteAttributeString("default", dipValue.Default);

							xmlWriter.WriteEndElement();
						}

						xmlWriter.WriteEndElement();
					} */

					if (machineNode.Driver != null)
					{
						xmlWriter.WriteStartElement("driver");
						xmlWriter.WriteAttributeString("status", machineNode.Driver.Status.ToString());
						xmlWriter.WriteAttributeString("emulation", machineNode.Driver.Emulation.ToString());
						xmlWriter.WriteAttributeString("color", machineNode.Driver.Color.ToString());
						xmlWriter.WriteAttributeString("sound", machineNode.Driver.Sound.ToString());
						xmlWriter.WriteAttributeString("graphic", machineNode.Driver.Graphic.ToString());
						xmlWriter.WriteAttributeString("cocktail", machineNode.Driver.Cocktail.ToString());
						xmlWriter.WriteAttributeString("protection", machineNode.Driver.Protection.ToString());
						xmlWriter.WriteAttributeString("savestate", machineNode.Driver.SaveState.ToString());
						xmlWriter.WriteAttributeString("palettesize", machineNode.Driver.PaletteSize.ToString());
						xmlWriter.WriteEndElement();
					}

					foreach (MAMEDeviceNode device in machineNode.DeviceList)
					{
						xmlWriter.WriteStartElement("device");
						xmlWriter.WriteAttributeString("type", device.Type);
						xmlWriter.WriteAttributeString("tag", device.Tag);
						xmlWriter.WriteAttributeString("mandatory", device.Mandatory);
						xmlWriter.WriteAttributeString("interface", device.Interface);

						if (device.Instance != null)
						{
							xmlWriter.WriteStartElement("instance");
							xmlWriter.WriteAttributeString("name", device.Instance.Name);
							xmlWriter.WriteAttributeString("briefname", device.Instance.BriefName);
							xmlWriter.WriteEndElement();
						}

						foreach (MAMEExtensionNode extensionNode in device.ExtensionList)
						{
							xmlWriter.WriteStartElement("extension");
							xmlWriter.WriteAttributeString("name", extensionNode.Name);
							xmlWriter.WriteEndElement();
						}

						xmlWriter.WriteEndElement();
					}

					foreach (MAMESoftwareListNode softwareList in machineNode.SoftwareList)
					{
						xmlWriter.WriteStartElement("softwarelist");
						xmlWriter.WriteAttributeString("name", softwareList.Name);
						xmlWriter.WriteAttributeString("status", softwareList.Status);
						xmlWriter.WriteAttributeString("filter", softwareList.Filter);
						xmlWriter.WriteEndElement();
					}

					if (machineNode.ControlsDat != null)
						WriteControlsDatXml(xmlWriter, machineNode.ControlsDat);

					if (machineNode.CatVer != null)
						WriteCatVerXml(xmlWriter, machineNode.CatVer);

					if (machineNode.NPlayers != null)
						WriteNPlayersXml(xmlWriter, machineNode.NPlayers);

					if (machineNode.HallOfFame != null)
						WriteHallOfFameXml(xmlWriter, machineNode.HallOfFame);

					//xmlWriter.WriteElementString("aamarating", machineNode.AAMARating.ToString());

					xmlWriter.WriteEndElement();
				}

				xmlWriter.WriteEndElement();

				xmlWriter.WriteEndDocument();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("WriteMiniInfoXml", "MAMEXml", ex.Message, ex.StackTrace);
			}
			finally
			{
				if (xmlWriter != null)
				{
					xmlWriter.Flush();
					xmlWriter.Close();
				}
			}
		}

		public List<MAMEMachineNode> MachineList
		{
			get { return m_machineList; }
		}

		public List<MAMEMachineNode> GameList
		{
			get { return m_gameList; }
		}

		public Dictionary<string, MAMEMachineNode> MachineDictionary
		{
			get { return m_machineDictionary; }
		}

		public Dictionary<string, MAMEMachineNode> GameDictionary
		{
			get { return m_gameDictionary; }
		}

		public List<string> DescriptionList
		{
			get
			{
				List<string> descriptionList = new List<string>();

				foreach (MAMEMachineNode machineNode in m_gameList)
					descriptionList.Add(machineNode.Description);

				return descriptionList;
			}
		}

		#region IDisposable Members

		public void Dispose()
		{
			m_machineList.Clear();
			m_machineDictionary.Clear();
			m_gameList.Clear();
			m_gameDictionary.Clear();
		}

		#endregion
	}
}