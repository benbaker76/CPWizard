// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace CPWizard
{
	class GameInfo : RenderObject, IDisposable
	{
		private enum StarType { Black, Bronze, Silver, Gold };

		private Bitmap m_powerBarBitmap = null;
		private Bitmap m_trueBitmap = null;
		private Bitmap m_falseBitmap = null;
		private Bitmap m_chipBitmap = null;

		private Font m_font = new Font("Lucida Console", 14, FontStyle.Regular);
		private Font m_chipFont = new Font("Lucida Console", 14, FontStyle.Regular);
		private Bitmap[] m_starBitmapArray = null;

		public GameInfo()
		{
			try
			{
				m_powerBarBitmap = FileIO.LoadImage(Path.Combine(Settings.Folders.Media, "PowerBar.png"));
				m_trueBitmap = FileIO.LoadImage(Path.Combine(Settings.Folders.Media, "True.png"));
				m_falseBitmap = FileIO.LoadImage(Path.Combine(Settings.Folders.Media, "False.png"));
				m_chipBitmap = FileIO.LoadImage(Path.Combine(Settings.Folders.Media, "Chip.png"));

				m_starBitmapArray = new Bitmap[4];

				m_starBitmapArray[(int)StarType.Black] = FileIO.LoadImage(Path.Combine(Settings.Folders.Media, "StarBlack.png"));
				m_starBitmapArray[(int)StarType.Bronze] = FileIO.LoadImage(Path.Combine(Settings.Folders.Media, "StarBronze.png"));
				m_starBitmapArray[(int)StarType.Silver] = FileIO.LoadImage(Path.Combine(Settings.Folders.Media, "StarSilver.png"));
				m_starBitmapArray[(int)StarType.Gold] = FileIO.LoadImage(Path.Combine(Settings.Folders.Media, "StarGold.png"));
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GameInfo", "GameInfo", ex.Message, ex.StackTrace);
			}
		}

		private void OnInputEvent(object sender, InputEventArgs e)
		{
			try
			{
				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.BackKey) ||
					Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.SelectKey))
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
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("OnGlobalKeyEvent", "GameInfo", ex.Message, ex.StackTrace);
			}
		}

		public Bitmap GetPowerBarBitmap(MAMEDriverStatus driverStatus, Rectangle rect)
		{
			try
			{
				Bitmap mainBitmap = new Bitmap(rect.Width, rect.Height);

				using (Graphics g = Graphics.FromImage(mainBitmap))
					g.DrawImage(m_powerBarBitmap, new Rectangle(0, 0, (int)(StatusToPower(driverStatus) * rect.Width), rect.Height), 0, 0, (int)(StatusToPower(driverStatus) * m_powerBarBitmap.Width), m_powerBarBitmap.Height, GraphicsUnit.Pixel);

				return mainBitmap;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetPowerBarBitmap", "GameInfo", ex.Message, ex.StackTrace);
			}

			return null;
		}

		public Bitmap GetChipsBitmap(MAMEMachineNode gameInfo, string chipType, Rectangle rect)
		{
			try
			{
				Bitmap mainBitmap = new Bitmap(rect.Width, rect.Height);

				using (Graphics g = Graphics.FromImage(mainBitmap))
				{
					int x = 0, y = 0;
					int width = rect.Width / 4;
					int height = rect.Height / 2;
					int chipCount = 0;

					for (int i = 0; i < gameInfo.ChipList.Count; i++)
					{
						if (gameInfo.ChipList[i].Type == chipType)
						{
							using (Bitmap chipBitmap = GetChipBitmap(gameInfo.ChipList[i].Name))
								g.DrawImage(chipBitmap, new Rectangle(x * width, y * height, width, height), 0, 0, chipBitmap.Width, chipBitmap.Height, GraphicsUnit.Pixel);

							if (++x % 4 == 0)
							{
								x = 0;
								y++;
							}

							chipCount++;
						}
					}
				}

				return mainBitmap;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetChipsBitmap", "GameInfo", ex.Message, ex.StackTrace);
			}

			return null;
		}

		public Bitmap GetStarsBitmap(MAMEMachineNode gameInfo, Rectangle rect)
		{
			try
			{
				Bitmap mainBitmap = new Bitmap(rect.Width, rect.Height);

				using (Graphics g = Graphics.FromImage(mainBitmap))
				{
					int width = rect.Width / 10;

					for (int i = 0; i < 10; i++)
					{
						Bitmap starBitmap = m_starBitmapArray[GetStarType(i, gameInfo.HallOfFame.WeightedAverage)];

						g.DrawImage(starBitmap, new Rectangle(i * width, 0, width, rect.Height), 0, 0, starBitmap.Width, starBitmap.Height, GraphicsUnit.Pixel);
					}
				}

				return mainBitmap;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetStarsBitmap", "GameInfo", ex.Message, ex.StackTrace);
			}

			return null;
		}

		public Bitmap GetSaveStateBitmap(MAMEMachineNode gameInfo, Rectangle rect)
		{
			try
			{
				Bitmap mainBitmap = new Bitmap(rect.Width, rect.Height);

				using (Graphics g = Graphics.FromImage(mainBitmap))
				{
					if (gameInfo.Driver.SaveState == MAMESaveState.Supported)
						g.DrawImage(m_trueBitmap, new Rectangle(0, 0, rect.Width, rect.Height), 0, 0, m_trueBitmap.Width, m_trueBitmap.Height, GraphicsUnit.Pixel);
					else
						g.DrawImage(m_falseBitmap, new Rectangle(0, 0, rect.Width, rect.Height), 0, 0, m_falseBitmap.Width, m_falseBitmap.Height, GraphicsUnit.Pixel);
				}

				return mainBitmap;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetSaveStateBitmap", "GameInfo", ex.Message, ex.StackTrace);
			}

			return null;
		}

		private Bitmap GetChipBitmap(string str)
		{
			try
			{
				Bitmap chipBitmap = new Bitmap(m_chipBitmap);

				using (Graphics g = Graphics.FromImage(chipBitmap))
				{
					using (StringFormat sf = new StringFormat())
					{
						sf.Alignment = StringAlignment.Center;
						sf.LineAlignment = StringAlignment.Center;
						sf.FormatFlags = StringFormatFlags.NoWrap;

						using (Brush foreBrush = new SolidBrush(Color.White))
							g.DrawString(str, m_chipFont, foreBrush, new Rectangle(0, 0, chipBitmap.Width, chipBitmap.Height), sf);
					}
				}

				return chipBitmap;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetChipBitmap", "GameInfo", ex.Message, ex.StackTrace);
			}

			return null;
		}

		public float StatusToPower(MAMEDriverStatus driverStatus)
		{
			switch (driverStatus)
			{
				case MAMEDriverStatus.Preliminary:
					return 0.2f;
				case MAMEDriverStatus.Imperfect:
					return 0.5f;
				case MAMEDriverStatus.Good:
					return 1f;
			}

			return 0f;
		}

		public string GetWeightedAverageName(float weightedAverage)
		{
			if (weightedAverage <= 20)
				return "Rubbish";
			else if (weightedAverage <= 70)
				return "Average";
			else if (weightedAverage <= 100)
				return "Perfect";

			return "Unknown";
		}

		public int GetStarType(int count, float weightedAverage)
		{
			if (count * 10 <= weightedAverage)
			{
				if (weightedAverage <= 20)
					return 1;
				else if (weightedAverage <= 70)
					return 2;
				else if (weightedAverage <= 100)
					return 3;
			}

			return 0;
		}

		public override void Paint(Graphics g, int x, int y, int width, int height)
		{
			try
			{
				MAMEMachineNode gameInfo = null;

				if (Settings.MAME.GameName == null || Globals.MAMEXml == null)
					return;

				if (!Globals.MAMEXml.GameDictionary.TryGetValue(Settings.MAME.GameName, out gameInfo))
					return;

				Color[] gameInfoColor = new Color[19] { Color.White, Color.Yellow, Color.Yellow, Color.Red, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.Red, Color.Red, Color.Yellow, Color.Yellow, Color.White, Color.White, Color.White };
				string[] gameInfoString = new string[19];
				string[] textReplace = new string[] { "[Statu]", "[Emula]", "[Color]", "[Sound]", "[Graph]", "[SaveS]", "[0]", "[1]", "[2]", "[3]", "[4]", "[5]", "[6]", "[7]", "[8]", "[9]", "[A]", "[B]", "[C]", "[D]", "[E]", "[F]", "[]" };

				gameInfoString[0] = String.Format("{0,12}{1,-35}", "", "");
				gameInfoString[1] = String.Format("{0,12}{1,-35}", "", gameInfo.Name);
				gameInfoString[2] = String.Format("{0,12}{1,-35}", "", StringTools.SubString(gameInfo.Description, 35));
				gameInfoString[3] = String.Format("{0,12}{1,-35}", "", StringTools.SubString(gameInfo.Year + " (" + gameInfo.Manufacturer + ")", 35));
				gameInfoString[4] = String.Format("{0,12}{1,-35}", "", StringTools.SubString(gameInfo.CatVer != null ? gameInfo.CatVer.Category : null, 35));
				gameInfoString[5] = String.Format("{0,12}{1,-12}{2,23}", "", "Ver Added:", gameInfo.CatVer != null ? gameInfo.CatVer.VerAdded : null);
				gameInfoString[6] = String.Format("{0,12}{1,-12}{2,23}", "", "NPlayers:", gameInfo.NPlayers != null ? gameInfo.NPlayers.Type : null);
				gameInfoString[7] = String.Format("{0,12}{1,-12}{2,23}", "", "", "");
				gameInfoString[8] = String.Format("{0,12}{1,-12}{2,12}{3,-12}", "Status: ", "[Statu]", "Emulation: ", "[Emula]");
				gameInfoString[9] = String.Format("{0,12}{1,-12}{2,12}{3,-12}", "Color: ", "[Color]", "Sound: ", "[Sound]");
				gameInfoString[10] = String.Format("{0,12}{1,-12}{2,12}{3,-12}", "Graphic: ", "[Graph]", "SaveState: ", "[SaveS]");
				gameInfoString[11] = String.Format("{0,12}{1,-12}{2,12}{3,-12}", "", "", "", "");
				gameInfoString[12] = String.Format("{0,12}{1,-12}{2,12}{3,-12}", "CPU: ", "[0][1][2][3]", "Audio: ", "[8][9][A][B]");
				gameInfoString[13] = String.Format("{0,12}{1,-12}{2,12}{3,-12}", "     ", "[4][5][6][7]", "       ", "[C][D][E][F]");

				if (gameInfo.DisplayList.Count > 0)
				{
					gameInfoString[14] = String.Format("{0,12}{1,-12}{2,12}{3,-12}", "Display: ", gameInfo.DisplayList[0].Type, "Rotate: ", gameInfo.DisplayList[0].Rotate);
					gameInfoString[15] = String.Format("{0,12}{1,-12}{2,12}{3,-12}", "Size: ", gameInfo.DisplayList[0].Width + " x " + gameInfo.DisplayList[0].Height, "Refresh: ", gameInfo.DisplayList[0].Refresh.ToString());
				}
				else
				{
					gameInfoString[14] = String.Format("{0,12}{1,-12}{2,12}{3,-12}", "Display: ", "??", "Rotate: ", "??");
					gameInfoString[15] = String.Format("{0,12}{1,-12}{2,12}{3,-12}", "Size: ", "?? x ??", "Refresh: ", "??");
				}
				gameInfoString[16] = String.Format("{0,12}{1,-12}{2,12}{3,-12}", "", "", "", "");

				if (gameInfo.HallOfFame != null)
					gameInfoString[17] = String.Format("{0,-20}{1,-12}{2,-12}", "[][][][][][][][][][]", String.Format(" ({0} Votes)", gameInfo.HallOfFame.Votes), String.Format(" {0} {1}%", GetWeightedAverageName(gameInfo.HallOfFame.WeightedAverage), gameInfo.HallOfFame.WeightedAverage));
				else
					gameInfoString[17] = "";

				gameInfoString[18] = String.Format("{0,12}{1,-12}{2,12}{3,-12}", "", "", "", "");

				SizeF fontSize = g.MeasureString(gameInfoString[0], m_font, 1024, StringFormat.GenericTypographic);

				for (int i = 0; i < gameInfoString.Length; i++)
				{
					SizeF maxFontSize = g.MeasureString(gameInfoString[i], m_font, 1024, StringFormat.GenericTypographic);
					if (maxFontSize.Width > fontSize.Width)
						fontSize.Width = maxFontSize.Width;
					if (maxFontSize.Height > fontSize.Height)
						fontSize.Height = maxFontSize.Height;
				}

				Size paddingSize = new Size((int)(fontSize.Width * 0.03f), (int)((fontSize.Height * gameInfoString.Length) * 0.03f));
				Size backSize = new Size((int)fontSize.Width + (paddingSize.Width * 2), (int)(fontSize.Height * gameInfoString.Length) + (paddingSize.Height * 2));

				using (Bitmap cmdBitmap = new Bitmap(backSize.Width, backSize.Height))
				{
					using (Graphics gSrc = Graphics.FromImage(cmdBitmap))
					{
						int xPos = paddingSize.Width, yPos = paddingSize.Height;

						Globals.DisplayManager.SetGraphicsQuality(gSrc, DisplayManager.UserGraphicsQuality());
						gSrc.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

						using (Bitmap mameIcon = Globals.MAMEManager.GetIcon(Settings.MAME.GameName, gameInfo.ROMOf))
						{
							if (mameIcon != null)
								gSrc.DrawImage(mameIcon, new Rectangle(xPos, yPos + (int)fontSize.Height, (int)(backSize.Width * 0.2f), (int)(backSize.Height * 0.3f)), 0, 0, mameIcon.Width, mameIcon.Height, GraphicsUnit.Pixel);
						}

						//Global.DisplayManager.SetGraphicsQuality(gSrc, DisplayManager.GraphicsQuality.Low);
						Globals.DisplayManager.SetGraphicsQuality(gSrc, DisplayManager.UserGraphicsQuality());

						List<MAMEChipNode> cpuChipList = new List<MAMEChipNode>();
						List<MAMEChipNode> audioChipList = new List<MAMEChipNode>();

						for (int i = 0; i < gameInfo.ChipList.Count; i++)
						{
							if (gameInfo.ChipList[i].Type == "cpu")
								cpuChipList.Add(gameInfo.ChipList[i]);
							else if (gameInfo.ChipList[i].Type == "audio")
								audioChipList.Add(gameInfo.ChipList[i]);
						}

						for (int i = 0; i < gameInfoString.Length; i++)
						{
							string[] strSplit = StringTools.SplitString(gameInfoString[i], textReplace); //TextReplace[j]);

							xPos = paddingSize.Width;
							int count = 0;

							for (int k = 0; k < strSplit.Length; k++)
							{
								SizeF strSize = g.MeasureString(strSplit[k].Replace(' ', '*'), m_font, 0, StringFormat.GenericTypographic);

								bool chipTest = false;

								for (int j = 0; j < 8; j++)
								{
									if (strSplit[k] == String.Format("[{0:X}]", j))
									{
										chipTest = true;

										if (j < cpuChipList.Count)
										{
											using (Bitmap chipBitmap = GetChipBitmap(cpuChipList[j].Name))
												gSrc.DrawImage(chipBitmap, new Rectangle(xPos, yPos, (int)strSize.Width, (int)strSize.Height), 0, 0, chipBitmap.Width, chipBitmap.Height, GraphicsUnit.Pixel);
										}
									}
								}

								for (int j = 0; j < 8; j++)
								{
									if (strSplit[k] == String.Format("[{0:X}]", j + 8))
									{
										chipTest = true;
										if (j < audioChipList.Count)
										{
											using (Bitmap chipBitmap = GetChipBitmap(audioChipList[j].Name))
												gSrc.DrawImage(chipBitmap, new Rectangle(xPos, yPos, (int)strSize.Width, (int)strSize.Height), 0, 0, chipBitmap.Width, chipBitmap.Height, GraphicsUnit.Pixel);
										}
									}
								}

								if (chipTest) { }
								else if (strSplit[k] == "[Statu]")
									gSrc.DrawImage(m_powerBarBitmap, new Rectangle(xPos, yPos + (int)(strSize.Height * 0.25f), (int)(StatusToPower(gameInfo.Driver.Status) * strSize.Width), (int)(strSize.Height * 0.5f)), 0, 0, (int)(StatusToPower(gameInfo.Driver.Status) * m_powerBarBitmap.Width), m_powerBarBitmap.Height, GraphicsUnit.Pixel);
								else if (strSplit[k] == "[Emula]")
									gSrc.DrawImage(m_powerBarBitmap, new Rectangle(xPos, yPos + (int)(strSize.Height * 0.25f), (int)(StatusToPower(gameInfo.Driver.Emulation) * strSize.Width), (int)(strSize.Height * 0.5f)), 0, 0, (int)(StatusToPower(gameInfo.Driver.Emulation) * m_powerBarBitmap.Width), m_powerBarBitmap.Height, GraphicsUnit.Pixel);
								else if (strSplit[k] == "[Color]")
									gSrc.DrawImage(m_powerBarBitmap, new Rectangle(xPos, yPos + (int)(strSize.Height * 0.25f), (int)(StatusToPower(gameInfo.Driver.Color) * strSize.Width), (int)(strSize.Height * 0.5f)), 0, 0, (int)(StatusToPower(gameInfo.Driver.Color) * m_powerBarBitmap.Width), m_powerBarBitmap.Height, GraphicsUnit.Pixel);
								else if (strSplit[k] == "[Sound]")
									gSrc.DrawImage(m_powerBarBitmap, new Rectangle(xPos, yPos + (int)(strSize.Height * 0.25f), (int)(StatusToPower(gameInfo.Driver.Sound) * strSize.Width), (int)(strSize.Height * 0.5f)), 0, 0, (int)(StatusToPower(gameInfo.Driver.Sound) * m_powerBarBitmap.Width), m_powerBarBitmap.Height, GraphicsUnit.Pixel);
								else if (strSplit[k] == "[Graph]")
									gSrc.DrawImage(m_powerBarBitmap, new Rectangle(xPos, yPos + (int)(strSize.Height * 0.25f), (int)(StatusToPower(gameInfo.Driver.Graphic) * strSize.Width), (int)(strSize.Height * 0.5f)), 0, 0, (int)(StatusToPower(gameInfo.Driver.Graphic) * m_powerBarBitmap.Width), m_powerBarBitmap.Height, GraphicsUnit.Pixel);
								else if (strSplit[k] == "[]")
								{
									Bitmap starBitmap = m_starBitmapArray[GetStarType(count, gameInfo.HallOfFame.WeightedAverage)];

									gSrc.DrawImage(starBitmap, new Rectangle(xPos, yPos, (int)strSize.Width, (int)strSize.Height), 0, 0, starBitmap.Width, starBitmap.Height, GraphicsUnit.Pixel);

									count++;
								}
								else if (strSplit[k] == "[SaveS]")
								{
									if (gameInfo.Driver.SaveState == MAMESaveState.Supported)
										gSrc.DrawImage(m_trueBitmap, new Rectangle(xPos, yPos, (int)(strSize.Width * 0.3f), (int)strSize.Height), 0, 0, m_trueBitmap.Width, m_trueBitmap.Height, GraphicsUnit.Pixel);
									else
										gSrc.DrawImage(m_falseBitmap, new Rectangle(xPos, yPos, (int)(strSize.Width * 0.3f), (int)strSize.Height), 0, 0, m_falseBitmap.Width, m_falseBitmap.Height, GraphicsUnit.Pixel);
								}
								else
								{
									using (Brush backBrush = new SolidBrush(Color.Black))
									{
										using (Brush foreBrush = new SolidBrush(gameInfoColor[i]))
										{
											gSrc.DrawString(strSplit[k], m_font, backBrush, xPos + 1, yPos + 1, StringFormat.GenericTypographic);
											gSrc.DrawString(strSplit[k], m_font, foreBrush, xPos, yPos, StringFormat.GenericTypographic);
										}
									}
								}

								xPos += (int)strSize.Width;
							}

							yPos += (int)fontSize.Height;
						}

						g.DrawImage(cmdBitmap, new Rectangle(x, y, width, height), 0, 0, backSize.Width, backSize.Height, GraphicsUnit.Pixel);
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("Paint", "GameInfo", ex.Message, ex.StackTrace);
			}
		}

		public override bool CheckEnabled()
		{
			try
			{
				if (Settings.MAME.GameName == null)
					return false;

				if (Globals.MAMEXml == null)
					return false;

				if (!Globals.MAMEXml.GameDictionary.ContainsKey(Settings.MAME.GameName))
					return false;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("CheckEnabled", "GameInfo", ex.Message, ex.StackTrace);
			}

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
	}
}
