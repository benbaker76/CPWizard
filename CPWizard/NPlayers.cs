// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CPWizard
{
	public class NPlayersNode
	{
		public string Name = null;
		public string Type = null;

		public NPlayersNode(string name, string type)
		{
			Name = name;
			Type = type;
		}
	}

	public class NPlayers : IDisposable
	{
		private Dictionary<string, NPlayersNode> m_gameDictionary = null;

		public NPlayers()
		{
			m_gameDictionary = new Dictionary<string, NPlayersNode>();
		}

		public void ReadNPlayersIni(string path)
		{
			try
			{
				string[] lineArray = File.ReadAllLines(path);

				for (int i = 0; i < lineArray.Length; i++)
				{
					string lineString = lineArray[i].Trim();

					if (!lineString.StartsWith("["))
					{
						string[] valueArray = lineString.Split(new char[] { '=' });

						if (valueArray.Length == 2)
						{
							if (!m_gameDictionary.ContainsKey(valueArray[0]))
								m_gameDictionary.Add(valueArray[0], new NPlayersNode(valueArray[0], valueArray[1]));
						}
					}

					//System.Windows.Forms.Application.DoEvents();
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ReadNPlayersIni", "NPlayers", ex.Message, ex.StackTrace);
			}
		}

		public Dictionary<string, NPlayersNode> GameDictionary
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
