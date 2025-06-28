// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CPWizard
{
	public class HallOfFameNode
	{
		public string ROMName = null;
		//public string Description = null;
		//public string Manufacturer = null;
		public float WeightedAverage = 0f;
		public int Votes = 0;
		//public string Category = null;
		//public string Genre = null;

		public HallOfFameNode(string romName, float weightedAverage, int votes)
		{
			ROMName = romName;
			WeightedAverage = weightedAverage;
			Votes = votes;
		}

		/* public HallOfFameNode(string romName, string description, string manufacturer, float weightedAverage, int votes, string category, string genre)
		{
			ROMName = romName;
			Description = description;
			Manufacturer = manufacturer;
			WeightedAverage = weightedAverage;
			Votes = votes;
			Category = category;
			Genre = genre;
		} */
	}

	public class HallOfFame : IDisposable
	{
		public enum XmlElement
		{
			Nothing,
			Game
		}

		//private List<HallOfFameNode> m_gameArray = null;
		private Dictionary<string, HallOfFameNode> m_gameDictionary = null;

		public HallOfFame()
		{
			//m_gameArray = new List<HallOfFameNode>();
			m_gameDictionary = new Dictionary<string, HallOfFameNode>();
		}

		public void ReadHallOfFameXml(string fileName)
		{
			XmlElement currentElement = XmlElement.Nothing;
			Hashtable attribHash = new Hashtable();

			HallOfFameNode currentGame = null;

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
									string romName = (string)attribHash["name"];
									//currentGame = new HallOfFameNode(ROMName, (string)AttribHash["description"], (string)AttribHash["manufacturer"], StringTools.FromString<float>((string)AttribHash["weightedaverage"]), StringTools.FromString<int>((string)AttribHash["votes"]), (string)AttribHash["category"], (string)AttribHash["genre"]);
									currentGame = new HallOfFameNode(romName, StringTools.FromString<float>((string)attribHash["weightedaverage"]), StringTools.FromString<int>((string)attribHash["votes"]));
									m_gameDictionary.Add(romName, currentGame);
									break;
								default:
									break;
							}
							xmlReader.MoveToElement();
							break;

						case XmlNodeType.EndElement:
							switch (currentElement)
							{
								case XmlElement.Game:
									currentGame = null;
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
				LogFile.WriteLine("ReadHallOfFameXml", "HallOfFameXml", ex.Message, ex.StackTrace);
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

		public Dictionary<string, HallOfFameNode> GameDictionary
		{
			get { return m_gameDictionary; }
		}

		#region IDisposable Members

		public void Dispose()
		{
			m_gameDictionary.Clear();
		}

		#endregion
	}
}
