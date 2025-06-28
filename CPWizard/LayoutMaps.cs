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

namespace CPWizard
{
	class LayoutMapNode
	{
		public bool Enabled = false;
		public string Constant = null;
		public string Control = null;
		public string NumPlayers = null;
		public string Alternating = null;
		public string Layout = null;

		public LayoutMapNode(bool enabled, string constant, string control, string numplayers, string alternating, string layout)
		{
			Enabled = enabled;
			Constant = constant;
			Control = control;
			NumPlayers = numplayers;
			Alternating = alternating;
			Layout = layout;

			if (Constant == null)
				Constant = "*";
			if (Control == null)
				Control = "*";
			if (NumPlayers == null)
				NumPlayers = "*";
			if (Alternating == null)
				Alternating = "*";
		}
	}

	class LayoutMaps : IDisposable
	{
		private enum xmlElement
		{
			Nothing,
			LayoutMap
		}

		public List<LayoutMapNode> LayoutMapsList = null;

		public LayoutMaps()
		{
			LayoutMapsList = new List<LayoutMapNode>();

			ReadLayoutMapsXml(Path.Combine(Settings.Folders.LayoutMaps, "Mame.xml"));
		}

		public bool ReadLayoutMapsXml(string FileName)
		{
			XmlTextReader xmlTextReader = null;
			xmlElement currentElement = xmlElement.Nothing;
			Hashtable attribHash = new Hashtable();

			try
			{
				xmlTextReader = new XmlTextReader(FileName);

				xmlTextReader.Read();

				while (xmlTextReader.Read())
				{
					switch (xmlTextReader.NodeType)
					{
						case XmlNodeType.Element:
							switch (xmlTextReader.LocalName.ToLower())
							{
								case "layoutmap":
									currentElement = xmlElement.LayoutMap;
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
								case xmlElement.LayoutMap:
									LayoutMapsList.Add(new LayoutMapNode(StringTools.FromString<bool>((string)attribHash["enabled"]), (string)attribHash["constant"], (string)attribHash["control"], (string)attribHash["numplayers"], (string)attribHash["alternating"], (string)attribHash["layout"]));
									break;
								default:
									break;
							}
							xmlTextReader.MoveToElement();
							break;

						case XmlNodeType.Text:
							string text = xmlTextReader.Value.Trim();
							switch (currentElement)
							{
								default:
									break;
							}
							break;

						case XmlNodeType.EndElement:
							switch (currentElement)
							{
								default:
									break;
							}
							break;
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ReadLayoutMapsXml", "LayoutMapsXml", ex.Message, ex.StackTrace);
			}

			if (xmlTextReader != null)
				xmlTextReader.Close();

			return true;
		}

		public void WriteLayoutMapsXml(string FileName)
		{
			XmlTextWriter xmlTextWriter = null;

			try
			{
				xmlTextWriter = new XmlTextWriter(FileName, null);

				xmlTextWriter.Formatting = Formatting.Indented;

				xmlTextWriter.WriteStartDocument();

				xmlTextWriter.WriteStartElement("LayoutMaps");

				foreach (LayoutMapNode layoutMap in LayoutMapsList)
				{
					xmlTextWriter.WriteStartElement("LayoutMap");

					xmlTextWriter.WriteAttributeString("Enabled", layoutMap.Enabled.ToString());
					xmlTextWriter.WriteAttributeString("Constant", layoutMap.Constant);
					xmlTextWriter.WriteAttributeString("Control", layoutMap.Control);
					xmlTextWriter.WriteAttributeString("NumPlayers", layoutMap.NumPlayers);
					xmlTextWriter.WriteAttributeString("Alternating", layoutMap.Alternating);
					xmlTextWriter.WriteAttributeString("Layout", layoutMap.Layout);

					xmlTextWriter.WriteEndElement();
				}

				xmlTextWriter.WriteEndElement();

				xmlTextWriter.WriteEndDocument();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("WriteLayoutMapsXml", "LayoutMapsXml", ex.Message, ex.StackTrace);
			}

			if (xmlTextWriter != null)
			{
				xmlTextWriter.Flush();
				xmlTextWriter.Close();
			}
		}

		#region IDisposable Members

		public void Dispose()
		{
			LayoutMapsList.Clear();
		}

		#endregion
	}
}
