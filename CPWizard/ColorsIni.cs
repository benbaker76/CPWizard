// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CPWizard
{
	public class ColorsNode
	{
		public string Control = null;
		public string Color = null;

		public ColorsNode(string control, string color)
		{
			Control = control;
			Color = color;
		}
	}

	public class ColorsIniNode
	{
		public string Name = null;
		public List<ColorsNode> Colors = null;

		public ColorsIniNode(string name)
		{
			Name = name;
			Colors = new List<ColorsNode>();
		}
	}

	public class ColorsIni : IDisposable
	{
		private Dictionary<string, ColorsIniNode> m_gameDictionary;

		public ColorsIni()
		{
			m_gameDictionary = new Dictionary<string, ColorsIniNode>();
		}

		public void ReadColorsIni(string path)
		{
			try
			{
				ColorsIniNode colorsIni = null;

				string[] lineArray = File.ReadAllLines(path);

				for (int i = 0; i < lineArray.Length; i++)
				{
					string Line = lineArray[i].Trim();

					if (Line.StartsWith("[") && Line.EndsWith("]"))
					{
						string sectionName = Line.Substring(1, Line.IndexOf(']') - 1);

						m_gameDictionary.Add(sectionName, colorsIni = new ColorsIniNode(sectionName));
					}
					else
					{
						if (colorsIni != null)
						{
							string[] valueArray = Line.Split(new char[] { '=' });

							if (valueArray.Length == 2)
								colorsIni.Colors.Add(new ColorsNode(valueArray[0], valueArray[1]));
						}
					}

					//System.Windows.Forms.Application.DoEvents();
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ReadColorsIni", "ColorsIni", ex.Message, ex.StackTrace);
			}
		}

		public Dictionary<string, ColorsIniNode> GameDictionary
		{
			get { return m_gameDictionary; }
		}

		#region IDisposable Members

		public void Dispose()
		{
			if (m_gameDictionary != null)
			{
				m_gameDictionary.Clear();
				m_gameDictionary = null;
			}
		}

		#endregion
	}
}
