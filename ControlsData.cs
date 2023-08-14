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
	public class LabelMapNode
	{
		public string Name = null;
		public string Label = null;

		public LabelMapNode(string name, string label)
		{
			Name = name;
			Label = label;
		}
	}

	public class ControlLabelMapNode : IComparable<ControlLabelMapNode>
	{
		public string Name = null;
		public List<LabelMapNode> LabelList = null;

		public ControlLabelMapNode(string name)
		{
			Name = name;
			LabelList = new List<LabelMapNode>();
		}

		#region IComparable<ControlLabelMapNode> Members

		public int CompareTo(ControlLabelMapNode other)
		{
			return this.Name.CompareTo(other.Name);
		}

		#endregion
	}

	public class DescriptionControlMapNode : IComparable<DescriptionControlMapNode>
	{
		public string Name = null;
		public List<string> CodeList = null;

		public DescriptionControlMapNode(string name, string code)
		{
			Name = name;
			CodeList = new List<string>();
			CodeList.Add(code);
		}

		#region IComparable<DescriptionControlMapNode> Members

		public int CompareTo(DescriptionControlMapNode other)
		{
			return this.Name.CompareTo(other.Name);
		}

		#endregion
	}

	public class ControlsData
	{
		private static List<ControlLabelMapNode> m_controlsToLabelsList = null;
		private static List<DescriptionControlMapNode> m_descriptionsToControlsList = null;

		public ControlsData()
		{
			m_controlsToLabelsList = new List<ControlLabelMapNode>();
			m_descriptionsToControlsList = new List<DescriptionControlMapNode>();

			ReadControlsToLabels(Settings.Files.InputCodes.ControlsToLabels);
			ReadDescriptionsToControls(Settings.Files.InputCodes.DescriptionsToControls);
		}

		public void ReadControlsToLabels(string fileName)
		{
			try
			{
				m_controlsToLabelsList.Clear();

				string[] lineArray = File.ReadAllLines(fileName);
				Dictionary<string, ControlLabelMapNode> controlDictionary = new Dictionary<string, ControlLabelMapNode>();

				for (int i = 0; i < lineArray.Length; i++)
				{
					string[] lineSplit = lineArray[i].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

					if (lineSplit.Length == 3)
					{
						ControlLabelMapNode controlLabelNode = null;

						if (controlDictionary.TryGetValue(lineSplit[0], out controlLabelNode))
							controlLabelNode.LabelList.Add(new LabelMapNode(lineSplit[1], lineSplit[2]));
						else
						{
							controlLabelNode = new ControlLabelMapNode(lineSplit[0]);
							controlLabelNode.LabelList.Add(new LabelMapNode(lineSplit[1], lineSplit[2]));
							m_controlsToLabelsList.Add(controlLabelNode);
							controlDictionary.Add(lineSplit[0], controlLabelNode);
						}
					}
				}

				m_controlsToLabelsList.Sort();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ReadControlsToLabels", "ControlsData", ex.Message, ex.StackTrace);
			}
		}

		public void ReadDescriptionsToControls(string fileName)
		{
			try
			{
				m_descriptionsToControlsList.Clear();

				string[] strLines = File.ReadAllLines(fileName);
				Dictionary<string, DescriptionControlMapNode> controlHash = new Dictionary<string, DescriptionControlMapNode>();

				for (int i = 0; i < strLines.Length; i++)
				{
					string[] strSplit = strLines[i].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

					if (strSplit.Length == 2)
					{
						DescriptionControlMapNode descriptionControlNode = null;

						if (controlHash.TryGetValue(strSplit[0], out descriptionControlNode))
							descriptionControlNode.CodeList.Add(strSplit[1]);
						else
						{
							descriptionControlNode = new DescriptionControlMapNode(strSplit[0], strSplit[1]);
							m_descriptionsToControlsList.Add(descriptionControlNode);
							controlHash.Add(strSplit[0], descriptionControlNode);
						}
					}
				}

				m_descriptionsToControlsList.Sort();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ReadDescriptionsToControls", "ControlsData", ex.Message, ex.StackTrace);
			}
		}

		public string[] GetControlArray()
		{
			List<string> controlList = new List<string>();
			Dictionary<string, string> constantDictionary = new Dictionary<string, string>();

			foreach (DescriptionControlMapNode descriptionControlNode in m_descriptionsToControlsList)
			{
				if (!constantDictionary.ContainsKey(descriptionControlNode.Name))
				{
					controlList.Add(descriptionControlNode.Name);
					constantDictionary.Add(descriptionControlNode.Name, null);
				}
			}

			controlList.Sort();

			return controlList.ToArray();
		}

		public string[] GetConstantArray()
		{
			List<string> constantList = new List<string>();
			Dictionary<string, string> constantDictionary = new Dictionary<string, string>();

			foreach (DescriptionControlMapNode descriptionControlNode in m_descriptionsToControlsList)
			{
				foreach (string code in descriptionControlNode.CodeList)
				{
					if (!constantDictionary.ContainsKey(code))
					{
						constantList.Add(code);
						constantDictionary.Add(code, null);
					}
				}
			}

			constantList.Sort();

			return constantList.ToArray();
		}

		public string[] GetConstantArray(string controlName)
		{
			List<string> constantList = new List<string>();

			foreach (DescriptionControlMapNode descriptionControlNode in m_descriptionsToControlsList)
			{
				if (descriptionControlNode.Name == controlName)
					constantList.AddRange(descriptionControlNode.CodeList);
			}

			return constantList.ToArray();
		}

		public string[] GetButtonArray()
		{
			List<string> buttonList = new List<string>();

			for (int i = 0; i < 16; i++)
				buttonList.Add(String.Format("P1_BUTTON{0}", (i + 1)));

			return buttonList.ToArray();
		}

		public void GetControlLabelArray(string constantName, out string[] nameArray, out string[] labelArray)
		{
			List<string> nameList = new List<string>();
			List<string> labelList = new List<string>();

			foreach (ControlLabelMapNode controlLabelNode in m_controlsToLabelsList)
			{
				if (constantName == controlLabelNode.Name)
				{
					foreach (LabelMapNode labelMapNode in controlLabelNode.LabelList)
					{
						nameList.Add(labelMapNode.Name);
						labelList.Add(labelMapNode.Label);
					}
				}
			}

			nameArray = nameList.ToArray();
			labelArray = labelList.ToArray();
		}

		public string[] GetControlCodeArray()
		{
			List<string> controlList = new List<string>();
			Dictionary<string, string> controlDictionary = new Dictionary<string, string>();

			foreach (ControlLabelMapNode controlLabelNode in m_controlsToLabelsList)
			{
				foreach (LabelMapNode labelMapNode in controlLabelNode.LabelList)
				{
					if (labelMapNode.Name == "BUTTON")
					{
						for (int i = 0; i < 16; i++)
						{
							string labelName = String.Format("P1_{0}{1}", labelMapNode.Name, i + 1);

							if (!controlDictionary.ContainsKey(labelName))
							{
								controlList.Add(labelName);
								controlDictionary.Add(labelName, null);
							}
						}
					}
					else
					{
						string labelName = String.Format("P1_{0}", labelMapNode.Name);

						if (!controlDictionary.ContainsKey(labelName))
						{
							controlList.Add(labelName);
							controlDictionary.Add(labelName, null);
						}
					}
				}
			}

			controlList.Sort();

			return controlList.ToArray();
		}

		public List<ControlLabelMapNode> ControlsToLabelsList
		{
			get { return m_controlsToLabelsList; }
		}

		public List<DescriptionControlMapNode> DescriptionsToControlsList
		{
			get { return m_descriptionsToControlsList; }
		}
	}
}
