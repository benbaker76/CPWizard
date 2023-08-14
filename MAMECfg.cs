// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using System.IO;

namespace CPWizard
{
	public class MAMECfg : IDisposable
	{
		public class PortNode
		{
			public string Type;
			//public int Mask;
			//public int Index;
			//public int DefValue;
			public List<string> SequenceList;

			public PortNode(string type, List<string> sequenceList)
			{
				Type = type;
				SequenceList = sequenceList;
			}
		}

		public class CfgNode
		{
			public int EntryNum;
			public string Name = null;
			public Dictionary<string, string> ReMapDictionary = null;
			public List<PortNode> PortList = null;

			public CfgNode(int entrynum, string name)
			{
				EntryNum = entrynum;
				Name = name;
				ReMapDictionary = new Dictionary<string, string>();
				PortList = new List<PortNode>();
			}
		}

		private enum XmlElement
		{
			Nothing,
			System,
			ReMap,
			Port,
			NewSeq,
		}

		private enum KeySeparatorType
		{
			Or,
			And,
			Not
		}

		private Dictionary<string, CfgNode> m_cfgDictionary = null;
		private Dictionary<string, CfgNode> m_ctrlrDictionary = null;

		public MAMECfg()
		{
			m_cfgDictionary = new Dictionary<string, CfgNode>();
			m_ctrlrDictionary = new Dictionary<string, CfgNode>();
		}

		public void ReadMAMECfg(string cfgPath, string romName)
		{
			try
			{
				if (String.IsNullOrEmpty(cfgPath))
					return;

				if (!Directory.Exists(cfgPath))
					return;

				m_cfgDictionary.Clear();

				if (romName != null)
				{
					ReadCfg(Path.Combine(cfgPath, "default.cfg"), m_cfgDictionary);
					ReadCfg(Path.Combine(cfgPath, romName + ".cfg"), m_cfgDictionary);
				}
				else
				{
					System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(cfgPath);

					System.IO.FileSystemInfo[] fileArray = directoryInfo.GetFiles("*.cfg");

					foreach (System.IO.FileSystemInfo fileInfo in fileArray)
						ReadCfg(fileInfo.FullName, m_cfgDictionary);
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ReadMAMECfg", "MAMECfg", ex.Message, ex.StackTrace);
			}
		}

		public void ReadMAMECtrlr(string fileName)
		{
			try
			{
				m_ctrlrDictionary.Clear();

				ReadCfg(fileName, m_ctrlrDictionary);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ReadMAMECtrlr", "MAMECfg", ex.Message, ex.StackTrace);
			}
		}

		public void ReadCfg(string fileName, Dictionary<string, CfgNode> cfgDictionary)
		{
			//LogFile.VerboseWriteLine(String.Format("MAME Config '{0}'", fileName));

			XmlElement currentElement = XmlElement.Nothing;
			CfgNode currentCfg = null;
			Hashtable attribHash = new Hashtable();

			int entryCount = 0;
			string name = null;
			string portType = null;
			string sequenceType = null;

			XmlReader xmlReader = null;

			if (!File.Exists(fileName))
				return;

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
								case "system":
									currentElement = XmlElement.System;
									break;
								case "remap":
									currentElement = XmlElement.ReMap;
									break;
								case "port":
									currentElement = XmlElement.Port;
									break;
								case "newseq":
									currentElement = XmlElement.NewSeq;
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
								case XmlElement.System:
									name = (string)attribHash["name"];

									currentCfg = new CfgNode(entryCount, name);
									entryCount++;
									break;
								case XmlElement.ReMap:
									if (currentCfg != null)
									{
										string origCode = String.Format("[{0}]", (string)attribHash["origcode"]);
										string newCode = String.Format("[{0}]", (string)attribHash["newcode"]);

										if (!currentCfg.ReMapDictionary.ContainsKey(origCode))
											currentCfg.ReMapDictionary.Add(origCode, newCode);
									}
									break;
								case XmlElement.Port:
									portType = (string)attribHash["type"];
									break;
								case XmlElement.NewSeq:
									sequenceType = (string)attribHash["type"];
									break;
								default:
									break;
							}
							xmlReader.MoveToElement();
							break;

						case XmlNodeType.Text:
							switch (currentElement)
							{
								case XmlElement.NewSeq:
									if (xmlReader.Value.Trim().Equals("DEFAULT"))
										break;

									string[] sequenceArray = StringTools.SplitString(xmlReader.Value.Trim(), new string[] { " ", " NOT ", " OR " });
									List<string> sequenceList = new List<string>();
									KeySeparatorType separatorType = KeySeparatorType.Or;

									for (int i = 0; i < sequenceArray.Length; i++)
									{
										string sequenceItem = sequenceArray[i].Trim();
										string inputCode = String.Format("[{0}]", sequenceItem);
										string reMapCode = null;

										if (currentCfg.ReMapDictionary.TryGetValue(inputCode, out reMapCode))
											inputCode = reMapCode;

										switch (sequenceItem)
										{
											case "":
												break;
											case "NOT":
												separatorType = KeySeparatorType.Not;
												break;
											case "OR":
												separatorType = KeySeparatorType.Or;
												break;
											default:
												string[] inputCodeArray = null;

												switch (separatorType)
												{
													case KeySeparatorType.Or:
														{
															if (Globals.InputCodes.TryGetAnalogToDigitalCodes(inputCode, out inputCodeArray))
															{
																if (inputCodeArray.Length == 2)
																{
																	inputCode = inputCodeArray[0];
																	string digitalInputCode = inputCodeArray[1];
																	string portTypeExt = String.Format("{0}_EXT", portType);

																	List<string> digitalSequenceList = new List<string>();
																	digitalSequenceList.Add(digitalInputCode);

																	//LogFile.VerboseWriteLine(String.Format("{0,-30}{1}", portType, inputCode));
																	//LogFile.VerboseWriteLine(String.Format("{0,-30}{1}", portTypeExt, String.Join("|", digitalSequenceList.ToArray())));

																	PortNode port = new PortNode(portTypeExt, digitalSequenceList);

																	currentCfg.PortList.Add(port);
																}
															}

															if (!sequenceList.Contains(inputCode))
																sequenceList.Add(inputCode);

															separatorType = KeySeparatorType.Or;

															break;
														}
													case KeySeparatorType.Not:
														{
															if (Globals.InputCodes.TryGetAnalogToDigitalCodes(inputCode, out inputCodeArray))
															{
																if (inputCodeArray.Length == 2)
																{
																	inputCode = inputCodeArray[0];
																	string digitalInputCode = inputCodeArray[1];
																	string portTypeExt = String.Format("{0}_EXT", portType);

																	List<string> digitalSequenceList = new List<string>();
																	digitalSequenceList.Add(digitalInputCode);

																	PortNode port = new PortNode(portTypeExt, digitalSequenceList);

																	currentCfg.PortList.Add(port);
																}
															}

															string notInputCode = String.Format("![{0}]", inputCode);

															if (!sequenceList.Contains(notInputCode))
																sequenceList.Add(notInputCode);

															separatorType = KeySeparatorType.Or;

															break;
														}
												}
												break;
										}
									}

									if (!cfgDictionary.ContainsKey(name))
										cfgDictionary.Add(name, currentCfg);

									if (sequenceType == "increment" || sequenceType == "decrement")
										if (sequenceList.Count > 0)
											if (sequenceList[0] == "[NONE]")
												break;

									if (sequenceType == "increment")
										portType += "_EXT";

									// AnalogToDigital

									//LogFile.VerboseWriteLine(String.Format("{0,-30}{1}", name, String.Join("|", sequenceList.ToArray())));

									currentCfg.PortList.Add(new PortNode(portType, sequenceList));
									break;
								default:
									break;
							}
							break;

						case XmlNodeType.EndElement:
							break;
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ReadCfg", "MAMECfg", ex.Message, ex.StackTrace);
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

		public Dictionary<string, CfgNode> CfgDictionary
		{
			get { return m_cfgDictionary; }
		}

		public Dictionary<string, CfgNode> CtrlrDictionary
		{
			get { return m_ctrlrDictionary; }
		}

		#region IDisposable Members

		public void Dispose()
		{
			CfgDictionary.Clear();
			CtrlrDictionary.Clear();
		}

		#endregion
	}
}