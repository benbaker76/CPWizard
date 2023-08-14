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
	public class MAMEIni : IDisposable
	{
		private Dictionary<string, string> m_mameIniDictionary = null;
		private Dictionary<string, Dictionary<string, string>> m_gameIniDictionary = null;

		public MAMEIni()
		{
			m_mameIniDictionary = new Dictionary<string, string>();
			m_gameIniDictionary = new Dictionary<string, Dictionary<string, string>>();
		}

		public void ReadMAMEIni(string path)
		{
			m_mameIniDictionary.Clear();

			if (!File.Exists(path))
				return;

			m_mameIniDictionary = ReadIni(path);
		}

		public void ReadGameIni(string iniPath, string romName)
		{
			try
			{
				m_gameIniDictionary.Clear();

				if (!Directory.Exists(iniPath))
					return;

				if (romName != null)
				{
					m_gameIniDictionary.Add(romName, ReadIni(Path.Combine(iniPath, romName + ".ini")));
				}
				else
				{
					DirectoryInfo directoryInfo = new DirectoryInfo(iniPath);

					FileSystemInfo[] fileSystemInfoArray = directoryInfo.GetFiles("*.ini");

					foreach (System.IO.FileSystemInfo fileSystemInfo in fileSystemInfoArray)
					{
						string iniName = Path.GetFileNameWithoutExtension(fileSystemInfo.FullName);

						m_gameIniDictionary.Add(iniName, ReadIni(fileSystemInfo.FullName));
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ReadGameIni", "MAMEIni", ex.Message, ex.StackTrace);
			}
		}

		private Dictionary<string, string> ReadIni(string path)
		{
			Dictionary<string, string> iniDictionary = new Dictionary<string, string>();

			try
			{
				string[] lineArray = File.ReadAllLines(path);

				for (int i = 0; i < lineArray.Length; i++)
				{
					string lineString = lineArray[i].Trim();

					if (lineString != String.Empty)
					{
						if (!lineString.StartsWith("###"))
						{
							string[] valueArray = lineString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

							if (valueArray.Length == 2)
							{
								string name = valueArray[0].Trim();
								string value = valueArray[1].Trim();

								iniDictionary.Add(name, value);
							}
						}
					}

					//System.Windows.Forms.Application.DoEvents();

				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ReadIni", "MAMEIni", ex.Message, ex.StackTrace);
			}

			return iniDictionary;
		}

		public Dictionary<string, string> MAMEIniDictionary
		{
			get { return m_mameIniDictionary; }
		}

		public Dictionary<string, Dictionary<string, string>> GameIniDictionary
		{
			get { return m_gameIniDictionary; }
		}

		#region IDisposable Members

		public void Dispose()
		{
		}

		#endregion
	}
}
