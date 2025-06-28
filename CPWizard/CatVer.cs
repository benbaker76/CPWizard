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
	public class CatVerNode
	{
		public string Name = null;
		public string Genre = null;
		public string Category = null;
		public string VerAdded = null;
		public bool IsMature = false;

		public CatVerNode()
		{
		}

		public CatVerNode(string name, string genre, string category, string verAdded, bool isMature)
		{
			Name = name;
			Genre = genre;
			Category = category;
			VerAdded = verAdded;
			IsMature = isMature;
		}

		public string GenreCategory
		{
			get
			{
				return Genre + (Category != null ? String.Format(" / {0}", Category) : "");
			}
		}
	}

	public class CatVer : IDisposable
	{
		private List<CatVerNode> m_gameList;
		private Dictionary<string, CatVerNode> m_gameDictionary;

		public enum CatVerType
		{
			None,
			Category,
			VerAdded
		}

		public CatVer()
		{
			m_gameList = new List<CatVerNode>();
			m_gameDictionary = new Dictionary<string, CatVerNode>();
		}

		public void ReadCatVerIni(string fileName)
		{
			try
			{
				CatVerNode catVer = null;
				CatVerType currentType = CatVerType.None;

				string[] lineArray = File.ReadAllLines(fileName);

				for (int i = 0; i < lineArray.Length; i++)
				{
					string lineString = lineArray[i].Trim();

					if (lineString.StartsWith("[") && lineString.EndsWith("]"))
					{
						string sectionName = lineString.Substring(1, lineString.LastIndexOf(']') - 1);

						if (sectionName != null)
						{
							switch (sectionName.ToLower())
							{
								case "category":
									currentType = CatVerType.Category;
									break;
								case "veradded":
									currentType = CatVerType.VerAdded;
									break;
							}
						}

						continue;
					}

					string[] valueArray = lineString.Split(new char[] { '=' });

					if (valueArray.Length != 2)
						continue;

					string name = valueArray[0].Trim();
					string value = valueArray[1].Trim();

					switch (currentType)
					{
						case CatVerType.Category:
							if (!m_gameDictionary.TryGetValue(name, out catVer))
							{
								catVer = new CatVerNode();
								catVer.Name = name;

								m_gameList.Add(catVer);
								m_gameDictionary.Add(name, catVer);
							}

							if (value.Contains(" * Mature *"))
							{
								catVer.IsMature = true;
								value = value.Replace(" * Mature *", "");
							}

							string[] valueSplitArray = value.Split(new string[] { " / " }, StringSplitOptions.RemoveEmptyEntries);

							catVer.Genre = (valueSplitArray.Length > 0 ? valueSplitArray[0] : null);
							catVer.Category = (valueSplitArray.Length > 1 ? valueSplitArray[1] : null);
							break;
						case CatVerType.VerAdded:
							if (!m_gameDictionary.TryGetValue(name, out catVer))
							{
								catVer = new CatVerNode();
								catVer.Name = name;

								m_gameList.Add(catVer);
								m_gameDictionary.Add(name, catVer);
							}

							catVer.VerAdded = value;
							break;
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ReadCatVerIni", "CatVer", ex.Message, ex.StackTrace);
			}
		}

		public List<CatVerNode> GameList
		{
			get { return m_gameList; }
		}

		public Dictionary<string, CatVerNode> GameDictionary
		{
			get { return m_gameDictionary; }
		}

		#region IDisposable Members

		public void Dispose()
		{
			if (m_gameList != null)
			{
				m_gameList.Clear();
				m_gameList = null;
			}

			if (m_gameDictionary != null)
			{
				m_gameDictionary.Clear();
				m_gameDictionary = null;
			}
		}

		#endregion
	}
}
