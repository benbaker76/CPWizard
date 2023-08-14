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
using System.IO;
using System.Windows.Forms;

namespace CPWizard
{
	public class LayoutManager : RenderObject, IDisposable
	{
		public LayoutManager()
		{
		}

		private void OnInputEvent(object sender, InputEventArgs e)
		{
			try
			{
				if ((Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.BackKey) && !Settings.Input.StopBackMenu ) || Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.SelectKey))
				{
					e.Handled = true;

					if (!e.IsDown)
					{
						Hide();

						if (ExitToMenu)
							Globals.MainMenu.Show();
						else
							Globals.ProgramManager.Hide();
					}
				}

				if (Settings.Input.EnableExitKey && Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.ExitKey))
				{
					e.Handled = true;

					if (!e.IsDown)
					{
						Hide();

						Globals.ProgramManager.Hide();
					}
				}

				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.MenuLeft))
				{
					e.Handled = true;

					if (!e.IsDown)
					{
						if (Globals.LayoutList != null)
						{
							if (--Globals.LayoutIndex < 0)
								Globals.LayoutIndex = Globals.LayoutList.Count - 1;

							Globals.Layout = Globals.LayoutList[Globals.LayoutIndex];

							EventManager.UpdateDisplay();
						}
					}
				}

				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.MenuRight))
				{
					e.Handled = true;

					if (!e.IsDown)
					{
						if (Globals.LayoutList != null)
						{
							if (++Globals.LayoutIndex > Globals.LayoutList.Count - 1)
								Globals.LayoutIndex = 0;

							Globals.Layout = Globals.LayoutList[Globals.LayoutIndex];

							EventManager.UpdateDisplay();
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("OnGlobalKeyEvent", "LayoutManager", ex.Message, ex.StackTrace);
			}
		}

		private bool TryLoadLayoutMap(List<string> constantList, List<string> controlList, int numPlayers, int alternating)
		{
			try
			{
				foreach (LayoutMapNode layoutMap in Globals.LayoutMaps.LayoutMapsList)
				{
					bool constantMatch = false;
					bool controlMatch = false;
					bool numPlayersMatch = false;
					bool alternatingMatch = false;

					if (layoutMap.Enabled)
					{
						if (layoutMap.Constant == "*")
							constantMatch = true;
						else
							foreach (string constant in constantList)
								if (constant == layoutMap.Constant)
									constantMatch = true;

						if (layoutMap.Control == "*")
							controlMatch = true;
						else
							foreach (string control in controlList)
								if (control.Contains(layoutMap.Control))
									controlMatch = true;

						if (layoutMap.NumPlayers == "*" || StringTools.FromString<int>(layoutMap.NumPlayers) == numPlayers)
							numPlayersMatch = true;

						if (layoutMap.Alternating == "*" || (layoutMap.Alternating == "Yes" && alternating == 1) || (layoutMap.Alternating == "No" && alternating == 0))
							alternatingMatch = true;

						if (constantMatch && controlMatch && numPlayersMatch && alternatingMatch)
						{
							Globals.ProgramManager.LoadLayout(Path.Combine(Settings.Folders.Layout, layoutMap.Layout + ".xml"), ref Globals.Layout, ref Globals.LayoutList, false);

							return true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("TryLoadLayoutMap", "LayoutManager", ex.Message, ex.StackTrace);
			}

			return false;
		}

		public void LoadEmulatorLayout(Layout layout, List<Layout> layoutList, bool layoutOverride, bool isSubLayout)
		{
			if (layoutOverride)
			{
				if (Globals.ProgramManager.LoadLayout(Path.Combine(Settings.Folders.Layout, Path.Combine(Settings.Emulator.Profile.LayoutOverride, Settings.Emulator.GameName + ".xml")), ref layout, ref layoutList, isSubLayout))
					return;

				Globals.ProgramManager.LoadLayout(Path.Combine(Settings.Folders.Layout, (isSubLayout ? Settings.Emulator.Profile.LayoutSub : Settings.Emulator.Profile.Layout) + ".xml"), ref layout, ref layoutList, isSubLayout);
			}
		}

		public void LoadMAMELayout(ref Layout layout, ref List<Layout> layoutList, List<string> constantList, List<string> controlList, MAMEMachineNode parent, int numPlayers, int alternating, bool layoutOverride, bool isSubLayout)
		{
			try
			{
				if (layoutOverride)
				{
					if (Globals.ProgramManager.LoadLayout(Path.Combine(Settings.Folders.Layout, Path.Combine(Settings.MAME.LayoutOverride, Settings.MAME.GameName + ".xml")), ref layout, ref layoutList, isSubLayout))
						return;

					if (Settings.MAME.MachineNode != null)
					{
						if (!String.IsNullOrEmpty(Settings.MAME.MachineNode.SourceFile))
						{
							if (Globals.ProgramManager.LoadLayout(Path.Combine(Settings.Folders.Layout, Path.Combine(Settings.MAME.LayoutOverride, Settings.MAME.MachineNode.SourceFile + ".xml")), ref layout, ref layoutList, isSubLayout))
								return;
						}

						if (!String.IsNullOrEmpty(Settings.MAME.MachineNode.CloneOf))
						{
							if (Globals.ProgramManager.LoadLayout(Path.Combine(Settings.Folders.Layout, Path.Combine(Settings.MAME.LayoutOverride, Settings.MAME.MachineNode.CloneOf + ".xml")), ref layout, ref layoutList, isSubLayout))
								return;
						}

						if (!String.IsNullOrEmpty(Settings.MAME.MachineNode.ROMOf))
						{
							if (Globals.ProgramManager.LoadLayout(Path.Combine(Settings.Folders.Layout, Path.Combine(Settings.MAME.LayoutOverride, Settings.MAME.MachineNode.ROMOf + ".xml")), ref layout, ref layoutList, isSubLayout))
								return;
						}

						if (parent != null)
						{
							if (Globals.ProgramManager.LoadLayout(Path.Combine(Settings.Folders.Layout, Path.Combine(Settings.MAME.LayoutOverride, parent.Name + ".xml")), ref layout, ref layoutList, isSubLayout))
								return;
						}
					}
					if (TryLoadLayoutMap(constantList, controlList, numPlayers, alternating))
						return;

					Globals.ProgramManager.LoadLayout(Path.Combine(Settings.Folders.Layout, (isSubLayout ? Settings.MAME.LayoutSub : Settings.MAME.Layout) + ".xml"), ref layout, ref layoutList, isSubLayout);
				}
				else
				{
					if (TryLoadLayoutMap(constantList, controlList, numPlayers, alternating))
						return;

					Globals.ProgramManager.LoadLayout(Path.Combine(Settings.Folders.Layout, (isSubLayout ? Settings.MAME.LayoutSub : Settings.MAME.Layout) + ".xml"), ref layout, ref layoutList, isSubLayout);
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("LoadMAMELayout", "LayoutManager", ex.Message, ex.StackTrace);
			}
		}

		public void MapEmuInputToLabels(ref Layout layout, ref List<Layout> layoutList, bool layoutOverride, bool isSubLayout)
		{
			try
			{
				if (Settings.Emulator.Profile == null)
					return;

				if (layout == null)
					return;

				LoadEmulatorLayout(layout, layoutList, layoutOverride, isSubLayout);
				layout.ClearLabels();

				for (int i = 0; i < Globals.EmulatorManager.Labels.Count; i++)
					layout.ReplaceLabel(Globals.EmulatorManager.Labels[i].Name, Globals.EmulatorManager.Labels[i].Value);

				ProcessLabelGroups(ref layout);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("MapEmuInputToLabels", "LayoutManager", ex.Message, ex.StackTrace);
			}
		}

		public void MapMAMEInputToLabels(ref Layout layout, ref List<Layout> layoutList, bool layoutOverride, bool attemptReload, bool isSubLayout)
		{
			try
			{
				if (Settings.MAME.MachineNode == null)
					return;

				MAMEMachineNode machineNode = Settings.MAME.MachineNode;
				List<MAMEInputNode> inputList = machineNode.InputList;
				ControlsDatNode controlsDat = Settings.MAME.MachineNode.ControlsDat;
				MAMEMachineNode parentMachineNode = Settings.MAME.MachineNode;
				List<ControlsDatPlayerNode> playerList = null;
				List<string> constantList = null;
				List<string> controlList = null;
				Dictionary<string, string[]> inputDictionary = Globals.InputCodes.GetDefaultInputMap();

				if (inputList.Count == 0)
					return;

				if (machineNode.NumPlayers == 0)
					return;

				string romName = machineNode.Name;
				string gameName = machineNode.Description;
				int numPlayers = machineNode.NumPlayers;
				int numButtons = machineNode.NumButtons;
				int mirrored = 0;
				int alternating = 0;
				string miscDetails = null;
				bool hasControlsDat = false;

				constantList = new List<string>();
				controlList = new List<string>();

				foreach (MAMEInputNode inputNode in Settings.MAME.MachineNode.InputList)
					foreach (MAMEControlNode controlNode in inputNode.ControlList)
						constantList.Add(controlNode.Constant);

				if (controlsDat != null) // ControlsDat entry found
				{
					hasControlsDat = true;

					playerList = controlsDat.PlayerList;
					numPlayers = controlsDat.NumPlayers;
					mirrored = controlsDat.Mirrored;
					alternating = controlsDat.Alternating;
					miscDetails = controlsDat.MiscDetails;

					constantList.Clear();

					foreach (ControlsDatPlayerNode playerNode in controlsDat.PlayerList)
					{
						foreach (ControlsDatControlNode controlNode in playerNode.ControlList)
						{
							controlList.Add(controlNode.Name);

							foreach (string constant in controlNode.ConstantList)
								constantList.Add(constant);
						}
					}
				}
				else
				{
					parentMachineNode = Globals.MAMEManager.GetParentROMControls(machineNode);
					controlsDat = parentMachineNode.ControlsDat;

					if (controlsDat != null)
					{
						hasControlsDat = true;

						playerList = controlsDat.PlayerList;
						numPlayers = controlsDat.NumPlayers;
						mirrored = controlsDat.Mirrored;
						alternating = controlsDat.Alternating;
						miscDetails = controlsDat.MiscDetails;

						constantList.Clear();

						foreach (ControlsDatPlayerNode playerNode in controlsDat.PlayerList)
						{
							foreach (ControlsDatControlNode controlNode in playerNode.ControlList)
							{
								controlList.Add(controlNode.Name);

								foreach (string constant in controlNode.ConstantList)
									constantList.Add(constant);
							}
						}
					}
				}

				if (attemptReload || layout == null)
					LoadMAMELayout(ref layout, ref layoutList, constantList, controlList, parentMachineNode, numPlayers, alternating, layoutOverride, isSubLayout);

				if (layout == null)
					return;

				layout.ClearLabels();
				LoadBitmaps(ref layout);
				GetMiscLabels(ref layout, numPlayers, alternating, miscDetails);

				MAMECfg.CfgNode cfgDefault = null;
				MAMECfg.CfgNode cfgROM = null;
				MAMECfg.CfgNode ctrlrParent = null;
				MAMECfg.CfgNode ctrlrSource = null;
				MAMECfg.CfgNode ctrlrDefault = null;
				MAMECfg.CfgNode ctrlrROM = null;

				Globals.MAMECfg.CfgDictionary.TryGetValue("default", out cfgDefault);
				Globals.MAMECfg.CfgDictionary.TryGetValue(machineNode.Name, out cfgROM);
				Globals.MAMECfg.CtrlrDictionary.TryGetValue("default", out ctrlrDefault);
				Globals.MAMECfg.CtrlrDictionary.TryGetValue(machineNode.Name, out ctrlrROM);

				if (parentMachineNode.Name != null)
					Globals.MAMECfg.CtrlrDictionary.TryGetValue(parentMachineNode.Name, out ctrlrParent);

				if (parentMachineNode.SourceFile != null)
					Globals.MAMECfg.CtrlrDictionary.TryGetValue(parentMachineNode.SourceFile, out ctrlrSource);

				// --------------------------

				if (ctrlrDefault != null)
					GetKeyCode(inputDictionary, ctrlrDefault);

				if (ctrlrSource != null)
					GetKeyCode(inputDictionary, ctrlrSource);

				if (ctrlrParent != null)
					GetKeyCode(inputDictionary, ctrlrParent);

				if (ctrlrROM != null)
					GetKeyCode(inputDictionary, ctrlrROM);

				if (cfgDefault != null)
					GetKeyCode(inputDictionary, cfgDefault);

				if (cfgROM != null)
					GetKeyCode(inputDictionary, cfgROM);

				// --------------------------

				if (Settings.General.VerboseLogging)
				{
					OutputCfg(ctrlrDefault);
					OutputCfg(ctrlrSource);
					OutputCfg(ctrlrParent);
					OutputCfg(ctrlrROM);
					OutputCfg(cfgDefault);
					OutputCfg(cfgROM);
					OutputInputDictionary(inputDictionary);
				}

				// --------------------------

				if (Settings.Display.AlphaFade)
					SetAlpha(ref layout, (alternating == 1 ? 1 : numPlayers));

				if (controlsDat == null)
					controlsDat = new ControlsDatNode(romName, gameName, numPlayers, alternating, mirrored, 0, 0, 0);

				if (playerList == null && numButtons > 0)
				{
					ControlsDatPlayerNode playerNode = new ControlsDatPlayerNode(1, numButtons);

					playerList = new List<ControlsDatPlayerNode>();
					playerList.Add(playerNode);

					playerNode.LabelList = new List<ControlsDatLabelNode>();

					for (int i = 0; i < numButtons; i++)
						playerNode.LabelList.Add(new ControlsDatLabelNode(String.Format("P1_BUTTON{0}", i + 1), (i + 1).ToString(), "White"));
				}

				if (playerList == null)
					return;

				foreach (ControlsDatPlayerNode playerNode in playerList)
				{
					List<string> playerInputList = new List<string>();

					int player = playerNode.Number;

					foreach (ControlsDatControlNode controlsDatControlNode in playerNode.ControlList)
						playerInputList.Add(controlsDatControlNode.Name);

					GetInputTypeLabels(ref layout, player, playerInputList);

					GetLabels(ref layout, player, playerNode.LabelList, inputDictionary);

					// --------------------------

					if (Settings.General.VerboseLogging)
						OutputLabelText(ref layout);

					// --------------------------

					if ((hasControlsDat ? (mirrored == 1 && alternating != 1) : true) && numPlayers > 1)
					{
						for (int i = 1; i < numPlayers; i++)
						{
							GetInputTypeLabels(ref layout, i + 1, playerInputList);
							GetLabels(ref layout, i + 1, playerNode.LabelList, inputDictionary);
						}
					}
				}

				ProcessLabelGroups(ref layout);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("MapMAMEInputToLabels", "LayoutManager", ex.Message, ex.StackTrace);
			}
		}

		private string[] ProcessMAMEModifiers(List<ControlsDatPlayerNode> playerList, MAMEIniOptions options)
		{
			List<string> controlList = new List<string>();

			if (playerList == null)
				return controlList.ToArray();

			try
			{
				foreach (MAMEControlLabelNode controlLabel in Globals.MAMEOptions.ControlLabelArray)
				{
					for (int i = 0; i < controlLabel.InputCodes.Length; i++)
						if (!controlList.Contains(controlLabel.InputCodes[i]))
							controlList.Add(controlLabel.InputCodes[i]);
				}

				//File.WriteAllLines("C:\\Exclude1.txt", controlList.ToList());

				foreach (ControlsDatPlayerNode players in playerList)
				{
					foreach (ControlsDatControlNode control in players.ControlList)
					{
						foreach (string constant in control.ConstantList)
						{
							foreach (MAMEControlLabelNode controlLabel in Globals.MAMEOptions.ControlLabelArray)
							{
								if (controlLabel.Constant == constant)
								{
									for (int i = 0; i < controlLabel.InputCodes.Length; i++)
										if (controlList.Contains(controlLabel.InputCodes[i]))
											controlList.Remove(controlLabel.InputCodes[i]);
								}
							}
						}
					}
				}

				//File.WriteAllLines("C:\\Exclude2.txt", controlList.ToList());
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ProcessModifiers", "LayoutManager", ex.Message, ex.StackTrace);
			}

			return controlList.ToArray();
		}

		public void OutputLabelText(ref Layout layout)
		{
			try
			{
				List<string> outputList = new List<string>();

				foreach (LayoutObject layoutObject in layout.LayoutObjectList)
				{
					if (layoutObject is LabelObject)
					{
						LabelObject labelObject = (LabelObject)layoutObject;

						outputList.Add(String.Format("{0}={1}", labelObject.Name, labelObject.Text));
					}
				}

				outputList.Sort();

				outputList.Insert(0, "[]====> Labels:");

				LogFile.VerboseWriteLine(String.Join(Environment.NewLine, outputList.ToArray()));
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("OutputLabelText", "LayoutManager", ex.Message, ex.StackTrace);
			}
		}

		public void OutputInputDictionary(Dictionary<string, string[]> inputDictionary)
		{
			try
			{
				List<string> outputList = new List<string>();

				IDictionaryEnumerator dictionaryEnumerator = inputDictionary.GetEnumerator();

				while (dictionaryEnumerator.MoveNext())
					outputList.Add(String.Format("{0,-30}{1}", dictionaryEnumerator.Key, String.Join("|", (string[])dictionaryEnumerator.Value)));

				outputList.Sort();

				outputList.Insert(0, "[]====> Input Dictionary:");

				LogFile.VerboseWriteLine(String.Join(Environment.NewLine, outputList.ToArray()));
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("OutputDictionaryValues", "LayoutManager", ex.Message, ex.StackTrace);
			}
		}

		public void OutputCfg(MAMECfg.CfgNode cfgNode)
		{
			if (cfgNode == null)
				return;

			List<string> outputList = new List<string>();

			foreach (MAMECfg.PortNode portNode in cfgNode.PortList)
				outputList.Add(String.Format("{0,-30}{1}", portNode.Type, String.Join("|", portNode.SequenceList.ToArray())));

			outputList.Insert(0, String.Format("[]====> Cfg '{0}'", cfgNode.Name));

			LogFile.VerboseWriteLine(String.Join(Environment.NewLine, outputList.ToArray()));
		}

		public void GetMiscLabels(ref Layout layout, int numPlayers, int alternating, string miscDetails)
		{
			if (Settings.MAME.MachineNode == null)
				return;

			try
			{
				MAMEMachineNode gameInfo = Settings.MAME.MachineNode;

				string romName = gameInfo.Name;
				string gameName = gameInfo.Description;
				string year = gameInfo.Year;
				string manufacturer = gameInfo.Manufacturer;
				string category = gameInfo.CatVer != null ? gameInfo.CatVer.Category : null;
				string verAdded = gameInfo.CatVer != null ? gameInfo.CatVer.VerAdded : null;
				string nPlayersType = gameInfo.NPlayers != null ? gameInfo.NPlayers.Type : null;
				float weightedAverage = gameInfo.HallOfFame != null ? gameInfo.HallOfFame.WeightedAverage : 0;
				string display = gameInfo.DisplayList.Count > 0 ? gameInfo.DisplayList[0].Type : "??";
				string size = gameInfo.DisplayList.Count > 0 ? String.Format("{0} x {1}", gameInfo.DisplayList[0].Width, gameInfo.DisplayList[0].Height) : "?? x ??";
				string rotate = gameInfo.DisplayList.Count > 0 ? gameInfo.DisplayList[0].Rotate.ToString() : "??";
				string refresh = gameInfo.DisplayList.Count > 0 ? gameInfo.DisplayList[0].Refresh.ToString() : "??";
				string votes = gameInfo.HallOfFame != null ? String.Format("({0} Votes)", gameInfo.HallOfFame.Votes) : null;
				string average = gameInfo.HallOfFame != null ? String.Format(" {0} {1}%", Globals.GameInfo.GetWeightedAverageName(gameInfo.HallOfFame.WeightedAverage), gameInfo.HallOfFame.WeightedAverage) : null;

				foreach (LayoutObject layoutObject in layout.LayoutObjectList)
				{
					if (layoutObject is LabelObject)
					{
						LabelObject labelObject = (LabelObject)layoutObject;

						if (labelObject.Name.StartsWith("[MISC_"))
						{
							switch (labelObject.Name)
							{
								case "[MISC_ROM_NAME]":
									labelObject.Text = romName;
									break;
								case "[MISC_GAME_NAME]":
									labelObject.Text = gameName;
									break;
								case "[MISC_YEAR]":
									labelObject.Text = year;
									break;
								case "[MISC_MANUFACTURER]":
									labelObject.Text = manufacturer;
									break;
								case "[MISC_CATEGORY]":
									labelObject.Text = category;
									break;
								case "[MISC_VER_ADDED]":
									labelObject.Text = verAdded;
									break;
								case "[MISC_NPLAYERS]":
									labelObject.Text = nPlayersType;
									break;
								case "[MISC_WEIGHTED_AVERAGE]":
									labelObject.Text = weightedAverage.ToString();
									break;
								case "[MISC_NUM_PLAYERS]":
									labelObject.Text = String.Format("Number of Players: {0}", numPlayers);
									break;
								case "[MISC_ALTERNATING]":
									labelObject.Text = (numPlayers == 1 ? "Single Player" : alternating == 1 ? "Alternating Play" : "Simultaneous Play");
									break;
								case "[MISC_MISC_DETAILS]":
									labelObject.Text = miscDetails;
									break;
								case "[MISC_DISPLAY]":
									labelObject.Text = display;
									break;
								case "[MISC_SIZE]":
									labelObject.Text = size;
									break;
								case "[MISC_ROTATE]":
									labelObject.Text = rotate;
									break;
								case "[MISC_REFRESH]":
									labelObject.Text = refresh;
									break;
								case "[MISC_VOTES]":
									labelObject.Text = votes;
									break;
								case "[MISC_AVERAGE]":
									labelObject.Text = average;
									break;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetMiscLabels", "LayoutManager", ex.Message, ex.StackTrace);
			}
		}

		public void GetInputTypeLabels(ref Layout layout, int player, List<string> inputList)
		{
			try
			{
				LabelObject[] labelArray = null;

				if (layout.TryGetLabel(String.Format("[MISC_P{0}_INPUTTYPE]", player), out labelArray))
				{
					foreach (LabelObject labelObject in labelArray)
					{
						foreach (string inputName in inputList)
						{
							if (labelObject.Text != String.Empty)
								labelObject.Text += " / ";

							labelObject.Text += inputName;
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetInputTypeLabels", "LayoutManager", ex.Message, ex.StackTrace);
			}
		}

		private class LabelUpdate
		{
			public LabelObject LabelObject = null;
			public string Text = null;

			public LabelUpdate(LabelObject labelObject, string text)
			{
				LabelObject = labelObject;
				Text = text;
			}
		}

		public void ProcessLabelGroups(ref Layout layout)
		{
			try
			{
				List<LabelObject> groupList = new List<LabelObject>();
				List<LabelUpdate> labelUpdateList = new List<LabelUpdate>();

				foreach (LayoutObject layoutObject in layout.LayoutObjectList)
				{
					if (!(layoutObject is LabelObject))
						continue;

					LabelObject labelObject = (LabelObject)layoutObject;

					if (labelObject.Name.StartsWith("[GROUP_"))
						groupList.Add(labelObject);
				}

				if (groupList.Count == 0)
					return;

				foreach (LabelObject groupLabel in groupList)
				{
					List<LabelObject> memberList = new List<LabelObject>();

					foreach (LayoutObject layoutObject in layout.LayoutObjectList)
					{
						if (!(layoutObject is LabelObject))
							continue;

						LabelObject labelObject = (LabelObject)layoutObject;

						if (groupLabel.Name == labelObject.Group)
							memberList.Add(labelObject);
					}

					if (memberList.Count > 0)
					{
						string word = null;
						int wordCount = 0;
						string labelText = memberList[0].Text;
						string[] labelTextSplit = labelText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

						for (int i = 0; i < labelTextSplit.Length; i++)
						{
							word = labelTextSplit[i];
							wordCount = 0;

							if (!StringTools.IsAlpha(word) || word.Length == 1)
								continue;

							foreach (LabelObject memberLabel in memberList)
							{
								if (memberLabel.Text.Contains(word))
									wordCount++;
								else
									break;
							}

							if (wordCount == memberList.Count)
							{
								labelUpdateList.Add(new LabelUpdate(groupLabel, word));

								foreach (LabelObject memberLabel in memberList)
									labelUpdateList.Add(new LabelUpdate(memberLabel, StringTools.ReplaceFirst(memberLabel.Text, word, "")));

								break;
							}
						}
					}
				}

				foreach (LabelUpdate labelUpdate in labelUpdateList)
				{
					labelUpdate.LabelObject.Text = labelUpdate.Text;

					if (labelUpdate.LabelObject.Text.StartsWith(" - "))
						labelUpdate.LabelObject.Text = labelUpdate.LabelObject.Text.Replace(" - ", "");
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ProcessLabelGroups", "LayoutManager", ex.Message, ex.StackTrace);
			}
		}

		public void SetAlpha(ref Layout layout, int numPlayers)
		{
			try
			{
				foreach (LayoutObject layoutObject in layout.LayoutObjectList)
				{
					if (layoutObject is ImageObject)
					{
						ImageObject imageObject = (ImageObject)layoutObject;

						if (!String.IsNullOrEmpty(imageObject.LabelLink))
						{
							if (imageObject.LabelLink.StartsWith("[PLAYER_"))
							{
								int player = StringTools.FromString<int>(imageObject.LabelLink.Substring(imageObject.LabelLink.IndexOf('_') + 1, 1));

								if (player <= numPlayers)
								{
									LabelObject[] labelArray = null;

									if (layout.TryGetLabel(imageObject.LabelLink, out labelArray))
									{
										foreach (LabelObject labelObject in labelArray)
										{
											foreach (LabelCode labelCode in labelObject.Codes)
											{
												if (labelCode.Type == "Custom Text")
													labelObject.Text = labelCode.Value;
											}
										}
									}

									continue;
								}
							}

							imageObject.Alpha = imageObject.AlphaFade ? ((float)imageObject.AlphaValue / 100f) : ((float)Settings.Display.AlphaFadeValue / 100f);
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("SetAlpha", "LayoutManager", ex.Message, ex.StackTrace);
			}
		}

		public void LoadBitmaps(ref Layout layout)
		{
			if (layout == null || Settings.MAME.GameName == null || Settings.MAME.MachineNode == null)
				return;

			try
			{
				MAMEMachineNode gameInfo = Settings.MAME.MachineNode;

				foreach (LayoutObject layoutObject in layout.LayoutObjectList)
				{
					if (layoutObject is ImageObject)
					{
						ImageObject imageObject = (ImageObject)layoutObject;

						if (imageObject.Name.StartsWith("[IMAGE_"))
						{
							switch (imageObject.Name)
							{
								case "[IMAGE_LAYOUTSUB]":
									if (!String.IsNullOrEmpty(layout.LayoutSub))
									{
										Layout layoutSub = new Layout();
										List<Layout> layoutList = new List<Layout>();

										layoutSub.LoadLayoutXml(Path.Combine(Settings.Folders.Layout, layout.LayoutSub + ".xml"));

										if (Globals.EmulatorMode == EmulatorMode.MAME)
											Globals.LayoutManager.MapMAMEInputToLabels(ref layoutSub, ref layoutList, false, false, false);
										else
											Globals.LayoutManager.MapEmuInputToLabels(ref layoutSub, ref layoutList, false, false);

										imageObject.Bitmap = Globals.LayoutManager.GetLayoutBitmap(layoutSub);
									}
									break;
								case "[IMAGE_CABINET]":
									imageObject.Bitmap = FileIO.LoadImage(Path.Combine(Settings.Folders.MAME.Cabinets, Settings.MAME.GameName + ".png"));
									break;
								case "[IMAGE_CPANEL]":
									imageObject.Bitmap = FileIO.LoadImage(Path.Combine(Settings.Folders.MAME.CPanel, Settings.MAME.GameName + ".png"));
									break;
								case "[IMAGE_FLYER]":
									imageObject.Bitmap = FileIO.LoadImage(Path.Combine(Settings.Folders.MAME.Flyers, Settings.MAME.GameName + ".png"));
									break;
								case "[IMAGE_ICON]":
									imageObject.Bitmap = Globals.MAMEManager.GetIcon(Settings.MAME.GameName, gameInfo.ROMOf);
									break;
								case "[IMAGE_MARQUEE]":
									imageObject.Bitmap = FileIO.LoadImage(Path.Combine(Settings.Folders.MAME.Marquees, Settings.MAME.GameName + ".png"));
									break;
								case "[IMAGE_PCB]":
									imageObject.Bitmap = FileIO.LoadImage(Path.Combine(Settings.Folders.MAME.PCB, Settings.MAME.GameName + ".png"));
									break;
								case "[IMAGE_PREVIEW]":
									imageObject.Bitmap = FileIO.LoadImage(Path.Combine(Settings.Folders.MAME.Previews, Settings.MAME.GameName + ".png"));
									break;
								case "[IMAGE_SELECT]":
									imageObject.Bitmap = FileIO.LoadImage(Path.Combine(Settings.Folders.MAME.Select, Settings.MAME.GameName + ".png"));
									break;
								case "[IMAGE_SNAP]":
									imageObject.Bitmap = FileIO.LoadImage(Path.Combine(Settings.Folders.MAME.Snap, Settings.MAME.GameName + ".png"));
									break;
								case "[IMAGE_TITLE]":
									imageObject.Bitmap = FileIO.LoadImage(Path.Combine(Settings.Folders.MAME.Titles, Settings.MAME.GameName + ".png"));
									break;
								case "[IMAGE_STATUS_BAR]":
									imageObject.Bitmap = Globals.GameInfo.GetPowerBarBitmap(gameInfo.Driver.Status, imageObject.Rect);
									break;
								case "[IMAGE_COLOR_BAR]":
									imageObject.Bitmap = Globals.GameInfo.GetPowerBarBitmap(gameInfo.Driver.Color, imageObject.Rect);
									break;
								case "[IMAGE_GRAPHIC_BAR]":
									imageObject.Bitmap = Globals.GameInfo.GetPowerBarBitmap(gameInfo.Driver.Graphic, imageObject.Rect);
									break;
								case "[IMAGE_EMULATION_BAR]":
									imageObject.Bitmap = Globals.GameInfo.GetPowerBarBitmap(gameInfo.Driver.Emulation, imageObject.Rect);
									break;
								case "[IMAGE_SOUND_BAR]":
									imageObject.Bitmap = Globals.GameInfo.GetPowerBarBitmap(gameInfo.Driver.Sound, imageObject.Rect);
									break;
								case "[IMAGE_SAVE_STATE]":
									imageObject.Bitmap = Globals.GameInfo.GetSaveStateBitmap(gameInfo, imageObject.Rect);
									break;
								case "[IMAGE_CPU_CHIPS]":
									imageObject.Bitmap = Globals.GameInfo.GetChipsBitmap(gameInfo, "cpu", imageObject.Rect);
									break;
								case "[IMAGE_AUDIO_CHIPS]":
									imageObject.Bitmap = Globals.GameInfo.GetChipsBitmap(gameInfo, "audio", imageObject.Rect);
									break;
								case "[IMAGE_STARS]":
									imageObject.Bitmap = Globals.GameInfo.GetStarsBitmap(gameInfo, imageObject.Rect);
									break;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("LoadBitmaps", "LayoutManager", ex.Message, ex.StackTrace);
			}
		}

		public bool TryGetLabelLinks(ref Layout layout, LabelObject labelObject, out List<ImageObject> imageList)
		{
			bool retVal = false;
			imageList = new List<ImageObject>();

			foreach (LayoutObject layoutObject in layout.LayoutObjectList)
			{
				if (layoutObject is ImageObject)
				{
					ImageObject imageObject = (ImageObject)layoutObject;

					string labelName = imageObject.LabelLink;

					int openBracketIndex = labelName.IndexOf("(");
					int closeBracketIndex = labelName.IndexOf(")");

					if (openBracketIndex < closeBracketIndex)
						labelName = labelName.Substring(0, openBracketIndex - 1);

					if (!String.IsNullOrEmpty(labelName))
					{
						if (labelObject.Name == labelName)
						{
							imageList.Add(imageObject);

							retVal = true;
						}
					}
				}
			}

			return retVal;
		}

		public void GetLabels(ref Layout layout, int player, List<ControlsDatLabelNode> labelList, Dictionary<string, string[]> inputDictionary)
		{
			try
			{
				foreach (ControlsDatLabelNode controlsDatLabelNode in labelList)
				{
					string[] inputArray = null;
					string inputCode = controlsDatLabelNode.Name.Replace("P1_", "P" + player.ToString() + "_");

					//LogFile.WriteLine(String.Format("----------->{0}", InputCode));

					if (inputDictionary.TryGetValue(inputCode, out inputArray))
					{
						for (int i = 0; i < inputArray.Length; i++)
						{
							LabelObject[] labelArray = null;

							//LogFile.WriteLine(String.Format("{0} : {1}", InputCode, inputList[i]));

							if (layout.TryGetLabel(inputArray[i], out labelArray))
							{
								int labelCount = 0;

								foreach (LabelObject labelObject in labelArray)
								{
									if (labelObject.Text != controlsDatLabelNode.Value)
									{
										if (!String.IsNullOrEmpty(labelObject.Text))
											labelObject.Text += " / ";

										labelObject.Text += controlsDatLabelNode.Value;
									}

									//System.Diagnostics.Debug.WriteLine(LabelNode.Name.Replace("P1_", "P" + Player.ToString() + "_") + ":" + InputList[i] + ": " + LabelNode.Value);

									List<ImageObject> imageList = null;

									if (TryGetLabelLinks(ref layout, labelObject, out imageList))
									{
										foreach (ImageObject imageObject in imageList)
										{
											imageObject.Alpha = 1f;

											if (layout.ColorImagesEnabled)
											{
												foreach (Layout.ColorImageNode colorImage in layout.ColorImageArray)
												{
													if (controlsDatLabelNode.Color == colorImage.Name)
													{
														imageObject.Bitmap = FileIO.LoadImage(Path.Combine(Settings.Folders.Media, colorImage.Image));
														imageObject.TempImage = true;

														//break;
													}
												}
											}
										}
									}

									labelCount++;

									//break;
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetLabels", "LayoutManager", ex.Message, ex.StackTrace);
			}
		}

		public void ResetControls(Layout layout)
		{
			try
			{
				foreach (LayoutObject layoutObject in layout.LayoutObjectList)
				{
					if (layoutObject is LabelObject)
					{
						LabelObject labelObject = (LabelObject)layoutObject;
						labelObject.RestoreText();
					}
					if (layoutObject is ImageObject)
					{
						ImageObject imageObject = (ImageObject)layoutObject;
						imageObject.Alpha = 1f;

						if (imageObject.TempImage)
						{
							imageObject.Bitmap = FileIO.LoadImage(Path.Combine(Settings.Folders.Media, imageObject.Name));
							imageObject.TempImage = false;
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ResetControls", "LayoutManager", ex.Message, ex.StackTrace);
			}
		}

		public void GetKeyCode(Dictionary<string, string[]> inputDictionary, MAMECfg.CfgNode cfgNode)
		{
			try
			{
				foreach (MAMECfg.PortNode port in cfgNode.PortList)
				{
					if (inputDictionary.ContainsKey(port.Type))
					{
						List<string> inputList = new List<string>();

						foreach (string input in port.SequenceList)
							inputList.Add(input);

						inputDictionary.Remove(port.Type);
						inputDictionary.Add(port.Type, inputList.ToArray());

						//LogFile.WriteLine(String.Format("PortType: {0}, InputList: {1}", Port.Type, String.Join(" | ", InputList.ToList())));
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetKeyCode", "LayoutManager", ex.Message, ex.StackTrace);
			}
		}

		public void DrawLabelLink(Graphics g, Layout layout, ImageObject imageObject)
		{
			if (!String.IsNullOrEmpty(imageObject.LabelLink))
			{
				LabelObject[] labelArray = null;

				if (layout.TryGetLabelLink(imageObject.LabelLink, out labelArray))
				{
					foreach (LabelObject labelObject in labelArray)
					{
						if (labelObject.Arrow && !String.IsNullOrEmpty(labelObject.Text))
							Globals.DisplayManager.DrawLabelArrow(g, labelObject.Center, imageObject.Center, Settings.Display.LabelArrowSize, Settings.Display.LabelArrowColor);
					}
				}
			}
		}

		public void DrawLayout(Graphics g, ref Layout layout, bool drawPlaceHolder)
		{
			try
			{
				lock (this)
				{
					if (layout != null)
					{
						if (layout.LayoutBitmap != null)
							g.DrawImage(layout.LayoutBitmap, 0, 0, layout.Width, layout.Height);

						foreach (LayoutObject layoutObject in layout.LayoutObjectList)
						{
							if (Settings.Display.LabelSpotShow)
							{
								if (layoutObject is LabelObject)
								{
									LabelObject labelObject = (LabelObject)layoutObject;

									if (labelObject.Spot && !String.IsNullOrEmpty(labelObject.Text))
										labelObject.DrawLabelSpot(g, Settings.Display.LabelSpotSize, Settings.Display.LabelSpotColor);
								}
							}

							layoutObject.Render(g, drawPlaceHolder);

							if (Settings.Display.LabelArrowShow)
							{
								if (layoutObject is ImageObject)
									DrawLabelLink(g, layout, (ImageObject)layoutObject);
							}
						}

						if (layout.ShowMiniInfo)
							Globals.Bezel.PaintMiniInfo(g, 0, (int)(layout.Height - (layout.Height * 0.075f)), layout.Width, (int)(layout.Height * 0.075f), Settings.MAME.MachineNode);
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawLayout", "LayoutManager", ex.Message, ex.StackTrace);
			}
		}

		public Bitmap GetLayoutBitmap(Layout layout)
		{
			Bitmap LayoutBitmap = new Bitmap(layout.Width, layout.Height);

			try
			{
				using (Graphics gSrc = Graphics.FromImage(LayoutBitmap))
				{
					Globals.DisplayManager.SetGraphicsQuality(gSrc, DisplayManager.UserGraphicsQuality());

					DrawLayout(gSrc, ref layout, false);
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetLayoutBitmap", "LayoutManager", ex.Message, ex.StackTrace);
			}

			return LayoutBitmap;
		}

		public override void Paint(Graphics g, int x, int y, int width, int height)
		{
		}

		public void Paint(Graphics g, int x, int y, int width, int height, bool isSubLayout)
		{
			try
			{
				if (isSubLayout)
				{
					using (Bitmap layoutBitmap = new Bitmap(Globals.LayoutSub.Width, Globals.LayoutSub.Height))
					{
						using (Graphics gSrc = Graphics.FromImage(layoutBitmap))
						{
							Globals.DisplayManager.SetGraphicsQuality(gSrc, DisplayManager.UserGraphicsQuality());

							DrawLayout(gSrc, ref Globals.LayoutSub, false);

							g.DrawImage(layoutBitmap, new Rectangle(x, y, width, height), 0, 0, layoutBitmap.Width, layoutBitmap.Height, GraphicsUnit.Pixel);
						}
					}
				}
				else
				{
					using (Bitmap layoutBitmap = new Bitmap(Globals.Layout.Width, Globals.Layout.Height))

					{
						using (Graphics gSrc = Graphics.FromImage(layoutBitmap))
						{
							Globals.DisplayManager.SetGraphicsQuality(gSrc, DisplayManager.UserGraphicsQuality());

							DrawLayout(gSrc, ref Globals.Layout, (Globals.DisplayMode == DisplayMode.LayoutEditor));

							g.DrawImage(layoutBitmap, new Rectangle(x, y, width, height), 0, 0, layoutBitmap.Width, layoutBitmap.Height, GraphicsUnit.Pixel);
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("Paint", "LayoutManager", ex.Message, ex.StackTrace);
			}
		}

		public override bool CheckEnabled()
		{
			return true;
		}

		public override void Reset(EmulatorMode mode)
		{
		}

		public override void AddEventHandlers()
		{
			Globals.InputManager.InputEvent += new InputManager.InputEventHandler(OnInputEvent);
		}

		public override void RemoveEventHandlers()
		{
			Globals.InputManager.InputEvent -= new InputManager.InputEventHandler(OnInputEvent);
		}

		#region IDisposable Members

		public override void Dispose()
		{
			base.Dispose();
		}

		#endregion
	}
}
