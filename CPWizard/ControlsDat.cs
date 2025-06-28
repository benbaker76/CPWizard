// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.Text;

namespace CPWizard
{
	public class ControlsDatLabelNode
	{
		public string Name = null;
		public string Value = null;
		public string Color = null;

		public ControlsDatLabelNode(string name, string value, string color)
		{
			Name = name;
			Value = value;
			Color = color;
		}
	}

	public class ControlsDatControlNode
	{
		public string Name = null;
		public List<string> ConstantList = null;
		public List<string> ButtonList = null;

		public ControlsDatControlNode(string name)
		{
			Name = name;
			ConstantList = new List<string>();
			ButtonList = new List<string>();
		}
	}

	public class ControlsDatPlayerNode
	{
		public int Number = 0;
		public int NumButtons = 0;
		public List<ControlsDatControlNode> ControlList;
		public List<ControlsDatLabelNode> LabelList;

		public ControlsDatPlayerNode(int number, int numbuttons)
		{
			Number = number;
			NumButtons = numbuttons;
			ControlList = new List<ControlsDatControlNode>();
			LabelList = new List<ControlsDatLabelNode>();
		}
	}

	public class ControlsDatNode
	{
		public string ROMName = null;
		public string GameName = null;
		public int NumPlayers = 0;
		public int Alternating = 0;
		public int Mirrored = 0;
		public int UsesService = 0;
		public int Tilt = 0;
		public int Cocktail = 0;
		public int Verified = 0;
		public string MiscDetails = null;
		public List<ControlsDatPlayerNode> PlayerList = null;

		public ControlsDatNode(string romName, string gameName, int numPlayers, int alternating, int mirrored, int usesService, int tilt, int cocktail)
		{
			ROMName = romName;
			GameName = gameName;
			NumPlayers = numPlayers;
			Alternating = alternating;
			Mirrored = mirrored;
			UsesService = usesService;
			Tilt = tilt;
			Cocktail = cocktail;
			PlayerList = new List<ControlsDatPlayerNode>();
		}

		public string[] GetControlsArray()
		{
			List<string> controlsList = new List<string>();

			foreach (ControlsDatPlayerNode playerNode in PlayerList)
			{
				foreach (ControlsDatControlNode controlNode in playerNode.ControlList)
					controlsList.Add(controlNode.Name);
			}

			return controlsList.ToArray();
		}

		public string[] GetConstantsArray()
		{
			List<string> constantList = new List<string>();

			foreach (ControlsDatPlayerNode playerNode in PlayerList)
			{
				foreach (ControlsDatControlNode controlNode in playerNode.ControlList)
				{
					foreach (string constant in controlNode.ConstantList)
						constantList.Add(constant);
				}
			}

			return constantList.ToArray();
		}

		public string GetControlsString()
		{
			return String.Join(" ", GetControlsArray());
		}

		public string GetConstantsString()
		{
			return String.Join(" ", GetConstantsArray());
		}
	}

	public class ControlsDat : IDisposable
	{
		public enum XmlElement
		{
			Nothing,
			Game,
			MiscDetails,
			Player,
			Control,
			Constant,
			Button,
			Label
		}

		private List<ControlsDatNode> m_gameList = null;
		private Dictionary<string, ControlsDatNode> m_gameDictionary = null;

		public ControlsDat()
		{
			m_gameList = new List<ControlsDatNode>();
			m_gameDictionary = new Dictionary<string, ControlsDatNode>();
		}

		public void ReadControlsXml(string fileName)
		{
			XmlElement currentElement = XmlElement.Nothing;
			Hashtable attribHash = new Hashtable();

			ControlsDatNode currentGame = null;
			ControlsDatPlayerNode currentPlayer = null;
			ControlsDatControlNode currentControl = null;
			ControlsDatLabelNode currentLabel = null;

			XmlReader xmlReader = null;

			try
			{
				XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
				xmlReaderSettings.ProhibitDtd = false;

				xmlReader = XmlReader.Create(fileName, xmlReaderSettings);

				while (xmlReader.Read())
				{
					switch (xmlReader.NodeType)
					{
						case XmlNodeType.Element:
							switch (xmlReader.LocalName.ToLower())
							{
								case "game":
									currentElement = XmlElement.Game;
									break;
								case "miscdetails":
									currentElement = XmlElement.MiscDetails;
									break;
								case "player":
									currentElement = XmlElement.Player;
									break;
								case "control":
									currentElement = XmlElement.Control;
									break;
								case "constant":
									currentElement = XmlElement.Constant;
									break;
								case "button":
									currentElement = XmlElement.Button;
									break;
								case "label":
									currentElement = XmlElement.Label;
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
								case XmlElement.Game:
									string romName = (string)attribHash["romname"];
									currentGame = new ControlsDatNode(romName, (string)attribHash["gamename"], StringTools.FromString<int>((string)attribHash["numplayers"]), StringTools.FromString<int>((string)attribHash["alternating"]), StringTools.FromString<int>((string)attribHash["mirrored"]), StringTools.FromString<int>((string)attribHash["usesservice"]), StringTools.FromString<int>((string)attribHash["tilt"]), StringTools.FromString<int>((string)attribHash["cocktail"]));
									m_gameList.Add(currentGame);
									m_gameDictionary.Add(romName, currentGame);
									break;
								case XmlElement.MiscDetails:
									break;
								case XmlElement.Player:
									if (currentGame != null)
									{
										currentPlayer = new ControlsDatPlayerNode(StringTools.FromString<int>((string)attribHash["number"]), StringTools.FromString<int>((string)attribHash["numbuttons"]));
										currentGame.PlayerList.Add(currentPlayer);
									}
									break;
								case XmlElement.Control:
									if (currentPlayer != null)
									{
										currentControl = new ControlsDatControlNode((string)attribHash["name"]);
										currentPlayer.ControlList.Add(currentControl);
									}
									break;
								case XmlElement.Constant:
									if (currentControl != null)
									{
										currentControl.ConstantList.Add((string)attribHash["name"]);
									}
									break;
								case XmlElement.Button:
									if (currentControl != null)
									{
										currentControl.ButtonList.Add((string)attribHash["name"]);
									}
									break;
								case XmlElement.Label:
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
								case XmlElement.MiscDetails:
									currentGame.MiscDetails = text;
									break;
								default:
									break;
							}
							break;

						case XmlNodeType.EndElement:
							switch (currentElement)
							{
								case XmlElement.Game:
									currentGame = null;
									break;
								case XmlElement.MiscDetails:
									break;
								case XmlElement.Player:
									currentPlayer = null;
									break;
								case XmlElement.Control:
									currentControl = null;
									break;
								case XmlElement.Constant:
									break;
								case XmlElement.Button:
									break;
								case XmlElement.Label:
									currentLabel = null;
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
				LogFile.WriteLine("ReadControlsXml", "ControlsDat", ex.Message, ex.StackTrace);
			}
			finally
			{
				if (xmlReader != null)
				{
					xmlReader.Close();
					xmlReader = null;
				}
			}
		}

		public void WriteControlsDatXml(string fileName)
		{
			XmlWriter xmlWriter = null;

			try
			{
				XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
				xmlWriterSettings.Encoding = Encoding.UTF8;
				xmlWriterSettings.Indent = true;
				xmlWriterSettings.NewLineHandling = NewLineHandling.Entitize;

				xmlWriter = XmlWriter.Create(fileName, xmlWriterSettings);

				xmlWriter.WriteStartDocument();
				xmlWriter.WriteStartElement("Dat");

				foreach (ControlsDatNode gameNode in m_gameList)
				{
					xmlWriter.WriteStartElement("Game");
					xmlWriter.WriteAttributeString("ROMName", gameNode.ROMName);
					xmlWriter.WriteAttributeString("GameName", gameNode.GameName);

					xmlWriter.WriteAttributeString("NumPlayers", gameNode.NumPlayers.ToString());
					xmlWriter.WriteAttributeString("Alternating", gameNode.Alternating.ToString());
					xmlWriter.WriteAttributeString("Mirrored", gameNode.Mirrored.ToString());
					xmlWriter.WriteAttributeString("UsesService", gameNode.UsesService.ToString());
					xmlWriter.WriteAttributeString("Tilt", gameNode.Tilt.ToString());
					xmlWriter.WriteAttributeString("Cocktail", gameNode.Cocktail.ToString());

					xmlWriter.WriteStartElement("MiscDetails");
					xmlWriter.WriteString(gameNode.MiscDetails);
					xmlWriter.WriteEndElement();

					foreach (ControlsDatPlayerNode player in gameNode.PlayerList)
					{
						xmlWriter.WriteStartElement("Player");
						xmlWriter.WriteAttributeString("Number", player.Number.ToString());
						xmlWriter.WriteAttributeString("NumButtons", player.NumButtons.ToString());

						xmlWriter.WriteStartElement("Controls");

						foreach (ControlsDatControlNode control in player.ControlList)
						{
							xmlWriter.WriteStartElement("Control");
							xmlWriter.WriteAttributeString("Name", control.Name);

							foreach (string constant in control.ConstantList)
							{
								xmlWriter.WriteStartElement("Constant");
								xmlWriter.WriteAttributeString("Name", constant);
								xmlWriter.WriteEndElement();
							}

							foreach (string button in control.ButtonList)
							{
								xmlWriter.WriteStartElement("Button");
								xmlWriter.WriteAttributeString("Name", button);
								xmlWriter.WriteEndElement();
							}

							xmlWriter.WriteEndElement();
						}

						xmlWriter.WriteEndElement();
						xmlWriter.WriteStartElement("Labels");

						foreach (ControlsDatLabelNode label in player.LabelList)
						{
							xmlWriter.WriteStartElement("Label");
							xmlWriter.WriteAttributeString("Name", label.Name);
							xmlWriter.WriteAttributeString("Value", label.Value);

							if (!String.IsNullOrEmpty(label.Color))
								xmlWriter.WriteAttributeString("Color", label.Color);

							xmlWriter.WriteEndElement();
						}

						xmlWriter.WriteEndElement();
						xmlWriter.WriteEndElement();
					}

					xmlWriter.WriteEndElement();
				}

				xmlWriter.WriteEndDocument();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("WriteControlsDatXml", "MAMEXml", ex.Message, ex.StackTrace);
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

		public void AddColorsIni()
		{
			foreach (ControlsDatNode controlsDat in m_gameList)
			{
				foreach (ControlsDatPlayerNode player in controlsDat.PlayerList)
				{
					foreach (ControlsDatLabelNode label in player.LabelList)
					{
						ColorsIniNode colors = null;

						if (Globals.ColorsIni.GameDictionary.TryGetValue(controlsDat.ROMName, out colors))
						{
							foreach (ColorsNode color in colors.Colors)
							{
								if (label.Name.StartsWith(color.Control))
								{
									label.Color = color.Color;
									break;
								}
							}
						}
					}
				}
			}
		}

		public List<ControlsDatNode> GameList
		{
			get { return m_gameList; }
		}

		public Dictionary<string, ControlsDatNode> GameDictionary
		{
			get { return m_gameDictionary; }
		}

		public bool IsROMSupported(string romName)
		{
			return m_gameDictionary.ContainsKey(romName);
		}

		#region IDisposable Members

		public void Dispose()
		{
			m_gameList.Clear();
			m_gameDictionary.Clear();
		}

		#endregion
	}
}