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
	public class InputCodes
	{
		private enum KeySeparatorType
		{
			Or,
			Not
		}

		public InputMap[] m_defaultInputMap = null;
		public InputMap[] m_analogToDigitalInputMap = null;

		public string[] m_emuCodes = null;
		public string[] m_groupCodes = null;
		public string[] m_joyCodes = null;
		public string[] m_keyCodes = null;
		public string[] m_miscCodes = null;
		public string[] m_mouseCodes = null;
		public string[] m_playerCodes = null;

		public InputCodes()
		{
			m_defaultInputMap = ReadDefaultInputMap(Settings.Files.InputCodes.MAMEDefault);
			m_analogToDigitalInputMap = ReadAnalogToDigitalInputMap(Settings.Files.InputCodes.AnalogToDigital);

			ReadInputCodes();
		}

		public void ReadInputCodes()
		{
			try
			{
				m_emuCodes = ReadInputCodeFile(Settings.Files.InputCodes.EmuCodes);
				m_groupCodes = ReadInputCodeFile(Settings.Files.InputCodes.GroupCodes);
				m_joyCodes = ReadMultiInputCodeFile(Settings.Files.InputCodes.JoyCodes);
				m_keyCodes = ReadInputCodeFile(Settings.Files.InputCodes.KeyCodes);
				m_miscCodes = ReadInputCodeFile(Settings.Files.InputCodes.MiscCodes);
				m_mouseCodes = ReadMultiInputCodeFile(Settings.Files.InputCodes.MouseCodes);
				m_playerCodes = ReadMultiInputCodeFile(Settings.Files.InputCodes.PlayerCodes);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("InputCodes", "ReadInputCodes", ex.Message, ex.StackTrace);
			}
		}

		public string[] ReadInputCodeFile(string fileName)
		{
			string[] textArray = File.ReadAllLines(fileName);
			List<string> textList = new List<string>();

			foreach (string text in textArray)
				textList.Add(String.Format("[{0}]", text));

			return textList.ToArray();
		}

		public string[] ReadMultiInputCodeFile(string fileName)
		{
			string[] textArray = File.ReadAllLines(fileName);
			List<string> textList = new List<string>();

			for (int i = 0; i < 8; i++)
				for (int j = 0; j < textArray.Length; j++)
					textList.Add(String.Format("[" + textArray[j] + "]", i + 1));

			return textList.ToArray();
		}

		public bool TryGetAnalogToDigitalCodes(string inputCode, out string[] inputCodeArray)
		{
			inputCodeArray = null;

			for (int i = 0; i < m_analogToDigitalInputMap.Length; i++)
			{
				//LogFile.WriteLine(String.Format("{0,-30}{1}", inputCode, AnalogToDigitalInputMap[i].Name));

				if (m_analogToDigitalInputMap[i].Name == inputCode)
				{
					inputCodeArray = m_analogToDigitalInputMap[i].SequenceArray;

					return true;
				}
			}

			return false;
		}

		public InputMap[] ReadAnalogToDigitalInputMap(string fileName)
		{
			List<InputMap> inputMapArray = new List<InputMap>();

			try
			{
				string[] lineArray = File.ReadAllLines(fileName);

				for (int i = 0; i < 8; i++)
				{
					for (int j = 0; j < lineArray.Length; j++)
					{
						string[] splitLine = lineArray[j].Split(new char[] { '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);

						if (splitLine.Length == 2)
						{
							string inputCode = String.Format(splitLine[0].Trim(), i + 1);
							string analogCode = String.Format("[{0}]", inputCode);

							string[] digitalSequenceArray = splitLine[1].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

							for (int k = 0; k < digitalSequenceArray.Length; k++)
							{
								inputCode = String.Format(digitalSequenceArray[k], i + 1);
								string digitalCode = String.Format("[{0}]", inputCode);
								digitalSequenceArray[k] = digitalCode;
							}

							inputMapArray.Add(new InputMap(analogCode, digitalSequenceArray));
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ReadAnalogToDigital", "MAMEManager", ex.Message, ex.StackTrace);
			}

			return inputMapArray.ToArray();
		}

		public InputMap[] ReadDefaultInputMap(string fileName)
		{
			LogFile.VerboseWriteLine(String.Format("MAME Default '{0}'", fileName));

			List<InputMap> inputMapList = new List<InputMap>();

			try
			{
				string[] lineArray = File.ReadAllLines(fileName);

				for (int i = 0; i < lineArray.Length; i++)
				{
					string[] splitLine = lineArray[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

					if (splitLine.Length == 2)
					{
						string name = splitLine[0].Trim();

						string[] sequenceArray = StringTools.SplitString(splitLine[1], new string[] { "|", "!" });
						List<string> sequenceList = new List<string>();

						KeySeparatorType separatorType = KeySeparatorType.Or;

						for (int j = 0; j < sequenceArray.Length; j++)
						{
							string sequenceItem = sequenceArray[j].Trim();
							string inputCode = String.Format("[{0}]", sequenceItem);

							switch (sequenceItem)
							{
								case "|":
									separatorType = KeySeparatorType.Or;
									break;
								case "!":
									separatorType = KeySeparatorType.Not;
									break;
								default:
									switch (separatorType)
									{
										case KeySeparatorType.Or:
											sequenceList.Add(inputCode);
											break;
										case KeySeparatorType.Not:
											sequenceList.Add("!" + inputCode);
											break;
									}
									break;
							}
						}

						LogFile.VerboseWriteLine(String.Format("{0,-30}{1}", name, String.Join("|", sequenceList.ToArray())));

						inputMapList.Add(new InputMap(name, sequenceList.ToArray()));
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ReadDefaultInputMap", "MAMEManager", ex.Message, ex.StackTrace);
			}

			return inputMapList.ToArray();
		}

		public Dictionary<string, string[]> GetDefaultInputMap()
		{
			Dictionary<string, string[]> inputDictionary = new Dictionary<string, string[]>();

			for (int i = 0; i < m_defaultInputMap.Length; i++)
			{
				if (m_defaultInputMap[i] == null)
					continue;

				inputDictionary.Add(m_defaultInputMap[i].Name, m_defaultInputMap[i].SequenceArray);
			}

			return inputDictionary;
		}

		public Dictionary<string, string[]> GetDefaultInputMap(string[] excludeArray)
		{
			Dictionary<string, string[]> inputDictionary = new Dictionary<string, string[]>();

			for (int i = 0; i < m_defaultInputMap.Length; i++)
			{
				if (m_defaultInputMap[i] == null)
					continue;

				bool exclude = false;

				for (int j = 0; j < excludeArray.Length; j++)
					if (m_defaultInputMap[i].Name.EndsWith(excludeArray[j]))
						exclude = true;

				if (!exclude)
					inputDictionary.Add(m_defaultInputMap[i].Name, m_defaultInputMap[i].SequenceArray);
			}

			return inputDictionary;
		}

		public class InputNode
		{
			public string Code = null;
			public List<string> SequenceList = null;

			public InputNode(string code, List<string> sequenceList)
			{
				Code = code;
				SequenceList = sequenceList;
			}
		}

		public class InputMap
		{
			public string Name = null;
			public string[] SequenceArray = null;

			public InputMap(string name, string[] sequenceArray)
			{
				Name = name;
				SequenceArray = sequenceArray;
			}
		}

		public string[] EmuCodes
		{
			get { return m_emuCodes; }
		}

		public string[] GroupCodes
		{
			get { return m_groupCodes; }
		}

		public string[] JoyCodes
		{
			get { return m_joyCodes; }
		}

		public string[] KeyCodes
		{
			get { return m_keyCodes; }
		}

		public string[] MiscCodes
		{
			get { return m_miscCodes; }
		}

		public string[] MouseCodes
		{
			get { return m_mouseCodes; }
		}

		public string[] PlayerCodes
		{
			get { return m_playerCodes; }
		}
	}
}
