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
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace CPWizard
{
	class CommandDat : RenderObject, IDisposable
	{
		public class CommandNode
		{
			public string Name = null;
			public string[] TextArray = null;

			public CommandNode(string name)
			{
				Name = name;
			}
		}

		public class CommandDatNode
		{
			public string Name = null;
			public List<CommandNode> CommandList = null;

			public CommandDatNode(string name)
			{
				Name = name;
				CommandList = new List<CommandNode>();
			}
		}

		public class CmdDatSymbolMap
		{
			public string Name = null;
			public string Source = null;
			public Bitmap Image = null;

			public CmdDatSymbolMap(string name, string source)
			{
				Name = name;
				Source = source;
			}
		}

		public class Page
		{
			public int CurrentLine = 0;
			public int VisibleLines = 20;
			public int PageOffset = 0;
			public int TotalLines = 0;

			public int TotalOffset
			{
				get { return PageOffset + CurrentLine; }
			}
		}

		private enum CmdDatElement
		{
			Nothing,
			Comment,
			Info,
			Cmd,
			Section,
			End
		}

		private Dictionary<string, CommandDatNode> m_gameDictionary = null;
		private Font m_font = new Font("Lucida Console", 18, FontStyle.Bold);
		private Page m_menuPage = null;
		private Page[] m_pages = null;

		private bool m_showingMenu = true;

		private SizeF m_fontSize = SizeF.Empty;
		private Size m_paddingSize = Size.Empty;
		private Size m_backSize = Size.Empty;

		private int m_pagepos = 0;

		private TextMeasure m_textMeasure = null;

		public CommandDat()
		{
			try
			{
				m_textMeasure = new TextMeasure(m_font);
				m_menuPage = new Page();
				m_pages = new Page[100];

				for (int i = 0; i < 100; i++)
					m_pages[i] = new Page();

				m_gameDictionary = new Dictionary<string, CommandDatNode>();

				LoadCmdDatSymbols();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("CommandDat", "CommandDat", ex.Message, ex.StackTrace);
			}
		}

		private void OnInputEvent(object sender, InputEventArgs e)
		{
			try
			{
				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.SelectKey))
				{
					e.Handled = true;

					if (!e.IsDown)
					{
						if (m_showingMenu)
						{
							m_showingMenu = false;

							m_pagepos = m_menuPage.TotalOffset;
						}
						else
							m_showingMenu = true;

						EventManager.UpdateDisplay();
					}
				}

				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.MenuUp))
				{
					e.Handled = true;

					if (e.IsDown)
					{
						Page page = null;

						if (m_showingMenu)
						{
							page = m_menuPage;

							if (page.CurrentLine > 0)
								page.CurrentLine--;
							else
								if (page.PageOffset > 0)
									page.PageOffset--;
								else
								{
									if (page.TotalLines < page.VisibleLines)
										page.CurrentLine = page.TotalLines - 1;
									else
									{
										page.CurrentLine = page.VisibleLines - 1;
										page.PageOffset = page.TotalLines - page.VisibleLines;
									}
								}
						}
						else
						{
							page = m_pages[m_pagepos];

							if (page.PageOffset > 0)
								page.PageOffset--;
							/* else
								if (page.TotalLines > page.VisibleLines)
									page.PageOffset = page.TotalLines - page.VisibleLines; */
						}

						EventManager.UpdateDisplay();
					}
				}

				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.MenuDown))
				{
					e.Handled = true;

					if (e.IsDown)
					{
						Page page = null;

						if (m_showingMenu)
						{
							page = m_menuPage;

							if (page.CurrentLine + page.PageOffset < page.TotalLines - 1)
							{
								if (page.CurrentLine < page.VisibleLines - 1)
									page.CurrentLine++;
								else
									page.PageOffset++;
							}
							else
							{
								page.CurrentLine = 0;
								page.PageOffset = 0;
							}
						}
						else
						{
							page = m_pages[m_pagepos];

							if (page.PageOffset + page.VisibleLines < page.TotalLines)
								page.PageOffset++;
							/* else
								page.PageOffset = 0; */
						}

						EventManager.UpdateDisplay();
					}
				}

				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.MenuLeft))
				{
					e.Handled = true;

					if (e.IsDown)
					{
						Page page = null;

						if (m_showingMenu)
							page = m_menuPage;
						else
							page = m_pages[m_pagepos];

						page.PageOffset -= page.VisibleLines - 1;

						if (page.PageOffset < 0)
							page.PageOffset = 0;

						EventManager.UpdateDisplay();
					}

					return;
				}

				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.MenuRight))
				{
					e.Handled = true;

					if (e.IsDown)
					{
						Page page = null;

						if (m_showingMenu)
							page = m_menuPage;
						else
							page = m_pages[m_pagepos];

						page.PageOffset += page.VisibleLines - 1;

						if (page.PageOffset + page.VisibleLines > page.TotalLines)
							page.PageOffset = page.TotalLines - page.VisibleLines;

						if (page.PageOffset < 0)
							page.PageOffset = 0;

						EventManager.UpdateDisplay();
					}
				}

				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.BackKey))
				{
					e.Handled = true;

					if (!e.IsDown)
					{
						if (m_showingMenu)
						{
							Hide();

							if (ExitToMenu)
								Globals.MainMenu.Show();
							else
								Globals.ProgramManager.Hide();
						}
						else
						{
							m_showingMenu = true;

							m_pagepos = m_menuPage.TotalOffset;

							EventManager.UpdateDisplay();
						}
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
				LogFile.WriteLine("OnGlobalKeyEvent", "CommandDat", ex.Message, ex.StackTrace);
			}
		}

		private void LoadCmdDatSymbols()
		{
			try
			{
				for (int i = 0; i < CmdDatSymbolMaps.Length; i++)
				{
					string fileName = Path.Combine(Settings.Folders.CommandDat, CmdDatSymbolMaps[i].Source);

					CmdDatSymbolMaps[i].Image = FileIO.LoadImage(fileName);
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("LoadCmdDatSymbols", "CommandDat", ex.Message, ex.StackTrace);
			}
		}

		private void DisposeCommandDatSymbols()
		{
			try
			{
				for (int i = 0; i < CmdDatSymbolMaps.Length; i++)
				{
					if (CmdDatSymbolMaps[i].Image != null)
						CmdDatSymbolMaps[i].Image.Dispose();
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DisposeCommandDatSymbols", "CommandDat", ex.Message, ex.StackTrace);
			}
		}

		public CmdDatSymbolMap[] CmdDatSymbolMaps =
		{
			new CmdDatSymbolMap("_a", "[ICONS_COMMANDDAT_#A].png"),
            new CmdDatSymbolMap("_b", "[ICONS_COMMANDDAT_#B].png"),
            new CmdDatSymbolMap("_c", "[ICONS_COMMANDDAT_#C].png"),
            new CmdDatSymbolMap("_d", "[ICONS_COMMANDDAT_#D].png"),
            new CmdDatSymbolMap("_e", "[ICONS_COMMANDDAT_#E].png"),
            new CmdDatSymbolMap("_f", "[ICONS_COMMANDDAT_#F].png"),
            new CmdDatSymbolMap("_g", "[ICONS_COMMANDDAT_#G].png"),
            new CmdDatSymbolMap("_h", "[ICONS_COMMANDDAT_#H].png"),
            new CmdDatSymbolMap("_i", "[ICONS_COMMANDDAT_#I].png"),
            new CmdDatSymbolMap("_j", "[ICONS_COMMANDDAT_#J].png"),
            new CmdDatSymbolMap("_k", "[ICONS_COMMANDDAT_#K].png"),
            new CmdDatSymbolMap("_l", "[ICONS_COMMANDDAT_#L].png"),
            new CmdDatSymbolMap("_m", "[ICONS_COMMANDDAT_#M].png"),
            new CmdDatSymbolMap("_n", "[ICONS_COMMANDDAT_#N].png"),
            new CmdDatSymbolMap("_o", "[ICONS_COMMANDDAT_#O].png"),
            new CmdDatSymbolMap("_p", "[ICONS_COMMANDDAT_#P].png"),
            new CmdDatSymbolMap("_q", "[ICONS_COMMANDDAT_#Q].png"),
            new CmdDatSymbolMap("_r", "[ICONS_COMMANDDAT_#R].png"),
            new CmdDatSymbolMap("_s", "[ICONS_COMMANDDAT_#S].png"),
            new CmdDatSymbolMap("_t", "[ICONS_COMMANDDAT_#T].png"),
            new CmdDatSymbolMap("_u", "[ICONS_COMMANDDAT_#U].png"),
            new CmdDatSymbolMap("_v", "[ICONS_COMMANDDAT_#V].png"),
            new CmdDatSymbolMap("_w", "[ICONS_COMMANDDAT_#W].png"),
            new CmdDatSymbolMap("_x", "[ICONS_COMMANDDAT_#X].png"),
            new CmdDatSymbolMap("_y", "[ICONS_COMMANDDAT_#Y].png"),
            new CmdDatSymbolMap("_z", "[ICONS_COMMANDDAT_#Z].png"),
            new CmdDatSymbolMap("_A", "[ICONS_COMMANDDAT__A].png"),
            new CmdDatSymbolMap("_B", "[ICONS_COMMANDDAT__B].png"),
            new CmdDatSymbolMap("_C", "[ICONS_COMMANDDAT__C].png"),
            new CmdDatSymbolMap("_D", "[ICONS_COMMANDDAT__D].png"),
            new CmdDatSymbolMap("_P", "[ICONS_COMMANDDAT__P].png"),
            new CmdDatSymbolMap("_K", "[ICONS_COMMANDDAT__K].png"),
            new CmdDatSymbolMap("^s", "[ICONS_COMMANDDAT_^S].png"),
            new CmdDatSymbolMap("_G", "[ICONS_COMMANDDAT__G].png"),
            new CmdDatSymbolMap("_H", "[ICONS_COMMANDDAT__H].png"),
            new CmdDatSymbolMap("_Z", "[ICONS_COMMANDDAT__Z].png"),
            new CmdDatSymbolMap("^E", "[ICONS_COMMANDDAT_^E].png"),
            new CmdDatSymbolMap("^F", "[ICONS_COMMANDDAT_^F].png"),
            new CmdDatSymbolMap("^G", "[ICONS_COMMANDDAT_^G].png"),
            new CmdDatSymbolMap("^H", "[ICONS_COMMANDDAT_^H].png"),
            new CmdDatSymbolMap("^I", "[ICONS_COMMANDDAT_^I].png"),
            new CmdDatSymbolMap("^J", "[ICONS_COMMANDDAT_^J].png"),
            new CmdDatSymbolMap("^T", "[ICONS_COMMANDDAT_^T].png"),
            new CmdDatSymbolMap("^U", "[ICONS_COMMANDDAT_^U].png"),
            new CmdDatSymbolMap("^V", "[ICONS_COMMANDDAT_^V].png"),
            new CmdDatSymbolMap("^W", "[ICONS_COMMANDDAT_^W].png"),
            new CmdDatSymbolMap("_S", "[ICONS_COMMANDDAT__S].png"),
            new CmdDatSymbolMap("^S", "[ICONS_COMMANDDAT_^S].png"),
            new CmdDatSymbolMap("_1", "[ICONS_COMMANDDAT__1].png"),
            new CmdDatSymbolMap("_2", "[ICONS_COMMANDDAT__2].png"),
            new CmdDatSymbolMap("_3", "[ICONS_COMMANDDAT__3].png"),
            new CmdDatSymbolMap("_4", "[ICONS_COMMANDDAT__4].png"),
            new CmdDatSymbolMap("_5", "[ICONS_COMMANDDAT__5].png"),
            new CmdDatSymbolMap("_6", "[ICONS_COMMANDDAT__6].png"),
            new CmdDatSymbolMap("_7", "[ICONS_COMMANDDAT__7].png"),
            new CmdDatSymbolMap("_8", "[ICONS_COMMANDDAT__8].png"),
            new CmdDatSymbolMap("_9", "[ICONS_COMMANDDAT__9].png"),
            new CmdDatSymbolMap("_N", "[ICONS_COMMANDDAT__N].png"),
            new CmdDatSymbolMap("_+", "[ICONS_COMMANDDAT__+].png"),
            new CmdDatSymbolMap("_.", "[ICONS_COMMANDDAT__.].png"),
            new CmdDatSymbolMap("^1", "[ICONS_COMMANDDAT_^1].png"),
            new CmdDatSymbolMap("^2", "[ICONS_COMMANDDAT_^2].png"),
            new CmdDatSymbolMap("^3", "[ICONS_COMMANDDAT_^3].png"),
            new CmdDatSymbolMap("^4", "[ICONS_COMMANDDAT_^4].png"),
            new CmdDatSymbolMap("^6", "[ICONS_COMMANDDAT_^6].png"),
            new CmdDatSymbolMap("^7", "[ICONS_COMMANDDAT_^7].png"),
            new CmdDatSymbolMap("^8", "[ICONS_COMMANDDAT_^8].png"),
            new CmdDatSymbolMap("^9", "[ICONS_COMMANDDAT_^9].png"),
            new CmdDatSymbolMap("^!", "[ICONS_COMMANDDAT_^!].png"),
            new CmdDatSymbolMap("_!", "[ICONS_COMMANDDAT__!].png"),
            new CmdDatSymbolMap("_L", "[ICONS_COMMANDDAT__L].png"),
            new CmdDatSymbolMap("_M", "[ICONS_COMMANDDAT__M].png"),
            new CmdDatSymbolMap("_Q", "[ICONS_COMMANDDAT__Q].png"),
            new CmdDatSymbolMap("_R", "[ICONS_COMMANDDAT__R].png"),
            new CmdDatSymbolMap("_^", "[ICONS_COMMANDDAT__^].png"),
            new CmdDatSymbolMap("_?", "[ICONS_COMMANDDAT__QU].png"),
            new CmdDatSymbolMap("_X", "[ICONS_COMMANDDAT__X].png"),
            new CmdDatSymbolMap("^M", "[ICONS_COMMANDDAT_^M].png"),
            new CmdDatSymbolMap("_|", "[ICONS_COMMANDDAT__PI].png"),
            new CmdDatSymbolMap("_O", "[ICONS_COMMANDDAT__O].png"),
            new CmdDatSymbolMap("_-", "[ICONS_COMMANDDAT__-].png"),
            new CmdDatSymbolMap("_=", "[ICONS_COMMANDDAT__=].png"),
            new CmdDatSymbolMap("^-", "[ICONS_COMMANDDAT_^-].png"),
            new CmdDatSymbolMap("^=", "[ICONS_COMMANDDAT_^=].png"),
            new CmdDatSymbolMap("_~", "[ICONS_COMMANDDAT__TI].png"),
            new CmdDatSymbolMap("^*", "[ICONS_COMMANDDAT_^AS].png"),
            new CmdDatSymbolMap("^?", "[ICONS_COMMANDDAT_^QU].png"),
            new CmdDatSymbolMap("_`", "[ICONS_COMMANDDAT__`].png"),
            new CmdDatSymbolMap("_�", "[ICONS_COMMANDDAT__CO].png"),
            new CmdDatSymbolMap("_)", "[ICONS_COMMANDDAT__BR].png"),
            new CmdDatSymbolMap("_(", "[ICONS_COMMANDDAT__BL].png"),
            new CmdDatSymbolMap("_*", "[ICONS_COMMANDDAT__AS].png"),
            new CmdDatSymbolMap("_&", "[ICONS_COMMANDDAT__AN].png"),
            new CmdDatSymbolMap("_%", "[ICONS_COMMANDDAT__PE].png"),
            new CmdDatSymbolMap("_$", "[ICONS_COMMANDDAT__DO].png"),
            new CmdDatSymbolMap("_#", "[ICONS_COMMANDDAT__HA].png"),
            new CmdDatSymbolMap("_]", "[ICONS_COMMANDDAT__SR].png"),
            new CmdDatSymbolMap("_[", "[ICONS_COMMANDDAT__SL].png"),
            new CmdDatSymbolMap("_{", "[ICONS_COMMANDDAT__CL].png"),
            new CmdDatSymbolMap("_}", "[ICONS_COMMANDDAT__CR].png"),
            new CmdDatSymbolMap("_<", "[ICONS_COMMANDDAT__LT].png"),
            new CmdDatSymbolMap("_>", "[ICONS_COMMANDDAT__GT].png"),
			new CmdDatSymbolMap("block", "[ICONS_COMMANDDAT_BLOCK].png")
        };

		public bool IsCommand(string s)
		{
			for (int i = 0; i < CmdDatSymbolMaps.Length; i++)
				if (CmdDatSymbolMaps[i].Name == s)
					return true;

			return false;
		}

		public Bitmap GetCommandImage(string s)
		{
			for (int i = 0; i < CmdDatSymbolMaps.Length; i++)
				if (CmdDatSymbolMaps[i].Name == s)
					return CmdDatSymbolMaps[i].Image;

			return null;
		}

		private void MeasureText()
		{
			try
			{
				CommandDatNode commandDat = null;

				if (Settings.MAME.GameName == null)
					return;

				if (!m_gameDictionary.TryGetValue(Settings.MAME.GameName, out commandDat))
				{
					if (Settings.MAME.MachineNode.ROMOf == null)
						return;

					if (!m_gameDictionary.TryGetValue(Settings.MAME.MachineNode.ROMOf, out commandDat))
						return;
				}

				Page page = null;

				if (m_showingMenu)
					page = m_menuPage;
				else
					page = m_pages[m_pagepos];

				page.TotalLines = commandDat.CommandList[m_pagepos].TextArray.Length + 1;

				using (Bitmap measureBmp = new Bitmap(1, 1))
				{
					using (Graphics g = Graphics.FromImage(measureBmp))
					{
						m_fontSize = new Size(0, 0);

						for (int i = 0; i < commandDat.CommandList[m_pagepos].TextArray.Length; i++)
						{
							//SizeF maxFontSize = g.MeasureString(CommandDat.Commands[m_pagepos].TextArray[i], m_font, 0, StringFormat.GenericTypographic);
							SizeF maxFontSize = m_textMeasure.MeasureString(commandDat.CommandList[m_pagepos].TextArray[i]);

							if (maxFontSize.Width > m_fontSize.Width)
								m_fontSize.Width = maxFontSize.Width;
							if (maxFontSize.Height > m_fontSize.Height)
								m_fontSize.Height = maxFontSize.Height;
						}

						m_paddingSize = new Size((int)((float)m_fontSize.Width * 0.03f), (int)(((float)m_fontSize.Height * (float)page.VisibleLines) * 0.04f));
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("MeasureText", "CommandDat", ex.Message, ex.StackTrace);
			}
		}

		private string[] GetMenu()
		{
			try
			{
				List<string> menuText = new List<string>();

				CommandDatNode commandDat = null;

				if (Settings.MAME.GameName == null)
					return null;

				if (!m_gameDictionary.TryGetValue(Settings.MAME.GameName, out commandDat))
				{
					if (Settings.MAME.MachineNode.ROMOf == null)
						return null;

					if (!m_gameDictionary.TryGetValue(Settings.MAME.MachineNode.ROMOf, out commandDat))
						return null;
				}

				for (int i = 0; i < commandDat.CommandList.Count; i++)
					menuText.Add(commandDat.CommandList[i].Name);

				m_menuPage.TotalLines = commandDat.CommandList.Count;

				return (string[])menuText.ToArray();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetMenu", "CommandDat", ex.Message, ex.StackTrace);
			}

			return null;
		}

		private string[] GetCmdDatPage()
		{
			try
			{
				CommandDatNode commandDat = null;

				if (Settings.MAME.GameName == null)
					return null;

				if (!m_gameDictionary.TryGetValue(Settings.MAME.GameName, out commandDat))
				{
					if (Settings.MAME.MachineNode.ROMOf == null)
						return null;

					if (!m_gameDictionary.TryGetValue(Settings.MAME.MachineNode.ROMOf, out commandDat))
						return null;
				}

				string[] CmdDatString = new string[commandDat.CommandList[m_pagepos].TextArray.Length + 1];
				CmdDatString[0] = commandDat.CommandList[m_pagepos].Name;
				Array.Copy(commandDat.CommandList[m_pagepos].TextArray, 0, CmdDatString, 1, commandDat.CommandList[m_pagepos].TextArray.Length);

				return CmdDatString;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetCmdDatPage", "CommandDat", ex.Message, ex.StackTrace);
			}

			return null;
		}

		private void DrawScrollBar(Graphics g, Page page, Rectangle rect)
		{
			try
			{
				Brush backBrush = new SolidBrush(Color.Black);
				Brush markBrush = new SolidBrush(Color.MidnightBlue);

				RectangleF ScrollBarRect = new RectangleF(rect.Width * 0.98f, rect.Height * 0.04f, rect.Width * 0.01f, rect.Height * 0.92f);

				float markOffset = (ScrollBarRect.Height * (float)page.PageOffset / (float)page.TotalLines);
				float markHeight = (ScrollBarRect.Height * (float)page.VisibleLines / (float)page.TotalLines);

				if (markHeight > ScrollBarRect.Height)
					markHeight = ScrollBarRect.Height;

				RectangleF MarkerRect = new RectangleF(ScrollBarRect.Left, ScrollBarRect.Y + markOffset, ScrollBarRect.Width, markHeight);

				g.FillRectangle(backBrush, ScrollBarRect);
				g.FillRectangle(markBrush, MarkerRect);

				backBrush.Dispose();
				markBrush.Dispose();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawScrollBar", "TextViewer", ex.Message, ex.StackTrace);
			}
		}

		private void DrawSelectionBar(Graphics g)
		{
			try
			{
				Page page = null;

				if (m_showingMenu)
					page = m_menuPage;
				else
					page = m_pages[m_pagepos];

				Rectangle rect = new Rectangle(0, (int)(page.CurrentLine * m_fontSize.Height), (int)m_fontSize.Width, (int)m_fontSize.Height);

				//Color[] color = { Color.FromArgb(0, 0, 0, 0), Color.DeepSkyBlue, Color.FromArgb(0, 0, 0, 0) };
				Color[] color = { Color.FromArgb(0, 0, 0, 0), Color.DarkRed, Color.FromArgb(0, 0, 0, 0) };
				float[] colorPositions = { 0.0f, .5f, 1.0f };

				ColorBlend blend = new ColorBlend();
				blend.Colors = color;
				blend.Positions = colorPositions;

				LinearGradientBrush b = new LinearGradientBrush(rect, Color.Empty, Color.Empty, LinearGradientMode.Horizontal);

				b.InterpolationColors = blend;

				g.FillRectangle(b, rect);

				b.Dispose();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawSelectionBar", "CommandDat", ex.Message, ex.StackTrace);
			}
		}

		public Bitmap GetCmdDatTextBitmap()
		{
			try
			{
				MeasureText();

				string[] cmdDatString = null;

				Page page = null;

				if (m_showingMenu)
				{
					page = m_menuPage;
					cmdDatString = GetMenu();
				}
				else
				{
					page = m_pages[m_pagepos];
					cmdDatString = GetCmdDatPage();
				}

				if (cmdDatString == null)
					return null;

				m_backSize = new Size((int)m_fontSize.Width, (int)(m_fontSize.Height * page.VisibleLines));

				Bitmap cmdTextBmp = new Bitmap(m_backSize.Width, (int)(cmdDatString.Length * m_fontSize.Height));

				using (Graphics g = Graphics.FromImage(cmdTextBmp))
				{
					Globals.DisplayManager.SetGraphicsQuality(g, DisplayManager.UserGraphicsQuality());

					float x = 0.0f, y = 0.0f;

					for (int i = 0; i < cmdDatString.Length; i++)
					{
						string[] cmdDatSymbols = new string[CmdDatSymbolMaps.Length];

						for (int j = 0; j < CmdDatSymbolMaps.Length; j++)
							cmdDatSymbols[j] = CmdDatSymbolMaps[j].Name;

						string[] cmdSections = StringTools.SplitString(cmdDatString[i], cmdDatSymbols);

						x = 0;

						for (int j = 0; j < cmdSections.Length; j++)
						{
							SizeF measureString = SizeF.Empty;

							//measureString = g.MeasureString(CmdSections[j].Replace(' ', '*'), m_font, 1024, StringFormat.GenericTypographic);
							//measureString = TextRenderer.MeasureText(g, cmdSections[j], m_font, Size.Empty, TextFormatFlags.NoPadding);
							measureString = m_textMeasure.MeasureString(cmdSections[j]);

							if (IsCommand(cmdSections[j]))
							{
								Bitmap cmdBmp = GetCommandImage(cmdSections[j]);
								g.DrawImage(cmdBmp, new RectangleF(x, y, measureString.Width, measureString.Height), new RectangleF(0, 0, cmdBmp.Width, cmdBmp.Height), GraphicsUnit.Pixel);
							}

							x += measureString.Width;
						}

						y += m_fontSize.Height;
					}

					for (int i = 0; i < cmdDatString.Length; i++)
						for (int j = 0; j < CmdDatSymbolMaps.Length; j++)
							cmdDatString[i] = cmdDatString[i].Replace(CmdDatSymbolMaps[j].Name, "  ");

					x = 0;
					y = 0;

					for (int i = 0; i < cmdDatString.Length; i++)
					{
						Brush backBrush = new SolidBrush(Color.Black);
						Brush foreBrush = null;

						if (cmdDatString[i].StartsWith("[") && cmdDatString[i].EndsWith("["))
							foreBrush = new SolidBrush(Color.Red);
						else if (cmdDatString[i].StartsWith("�") && cmdDatString[i].EndsWith("�"))
							foreBrush = new SolidBrush(Color.Red);
						else if (cmdDatString[i].StartsWith("*") && cmdDatString[i].EndsWith("*"))
							foreBrush = new SolidBrush(Color.Yellow);
						else if (cmdDatString[i].StartsWith(":") && cmdDatString[i].EndsWith(":"))
							foreBrush = new SolidBrush(Color.Yellow);
						else if (cmdDatString[i].StartsWith("[") && cmdDatString[i].EndsWith("]"))
							foreBrush = new SolidBrush(Color.Yellow);
						else if (StringTools.StrContainsOnlyChar(cmdDatString[i], '-'))
							foreBrush = new SolidBrush(Color.Yellow);
						else
							foreBrush = new SolidBrush(Color.White);

						g.DrawString(cmdDatString[i], m_font, backBrush, x + 1, y + 1, StringFormat.GenericTypographic);
						g.DrawString(cmdDatString[i], m_font, foreBrush, x, y, StringFormat.GenericTypographic);

						backBrush.Dispose();
						foreBrush.Dispose();

						y += (int)m_fontSize.Height;
					}
				}

				return cmdTextBmp;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetCmdDatTextBitmap", "CommandDat", ex.Message, ex.StackTrace);
			}

			return null;
		}

		public override void Paint(Graphics g, int x, int y, int width, int height)
		{
			try
			{
				using (Bitmap cmdTextBmp = GetCmdDatTextBitmap())
				{
					if (cmdTextBmp == null)
						return;

					using (Bitmap srcBmp = new Bitmap(m_backSize.Width, m_backSize.Height))
					{
						using (Bitmap destBmp = new Bitmap(m_backSize.Width, m_backSize.Height))
						{
							using (Graphics srcGraphics = Graphics.FromImage(srcBmp))
							{
								using (Graphics destGraphics = Graphics.FromImage(destBmp))
								{
									Globals.DisplayManager.SetGraphicsQuality(srcGraphics, DisplayManager.UserGraphicsQuality());
									Globals.DisplayManager.SetGraphicsQuality(destGraphics, DisplayManager.UserGraphicsQuality());

									if (m_showingMenu)
										DrawSelectionBar(srcGraphics);

									Page page = null;

									if (m_showingMenu)
										page = m_menuPage;
									else
										page = m_pages[m_pagepos];

									srcGraphics.DrawImage(cmdTextBmp, new Rectangle(0, 0, m_backSize.Width, m_backSize.Height), 0, (int)(page.PageOffset * m_fontSize.Height), cmdTextBmp.Width, (int)(page.VisibleLines * m_fontSize.Height), GraphicsUnit.Pixel);
									destGraphics.DrawImage(srcBmp, new Rectangle(m_paddingSize.Width, m_paddingSize.Height, m_backSize.Width - (m_paddingSize.Width * 2), m_backSize.Height - (m_paddingSize.Height * 2)), 0, 0, srcBmp.Width, srcBmp.Height, GraphicsUnit.Pixel);
									g.DrawImage(destBmp, new Rectangle(x, y, width, height), 0, 0, destBmp.Width, destBmp.Height, GraphicsUnit.Pixel);

									DrawScrollBar(g, page, new Rectangle(x, y, width, height));
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("Paint", "CommandDat", ex.Message, ex.StackTrace);
			}
		}

		public void ReadCommandDat(string fileName)
		{
			ReadCommandDat(fileName, null);
		}

		public void ReadCommandDat(string fileName, string romName)
		{
			try
			{
				List<string> commandText = new List<string>();
				CommandDatNode currentCommandDat = null;
				CommandNode currentCommand = null;
				bool found = false;

				m_gameDictionary.Clear();

				string[] lineArray = File.ReadAllLines(fileName, Encoding.ASCII);

				for (int i = 0; i < lineArray.Length; i++)
				{
					if (found)
						break;

					string lineString = lineArray[i];

					if (lineString.StartsWith("#"))
						continue;

					if (lineString.StartsWith("$info"))
					{
						currentCommandDat = null;
						currentCommand = null;
						commandText.Clear();

						string[] valueArray = lineString.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

						if (valueArray.Length != 2)
							continue;

						string[] nameArray = valueArray[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

						currentCommandDat = new CommandDatNode(valueArray[1]);

						for (int j = 0; j < nameArray.Length; j++)
						{
							string name = nameArray[j];

							m_gameDictionary.Add(name, currentCommandDat);

							if (romName == name)
								found = true;
						}

						continue;
					}

					if (lineString.StartsWith("$cmd"))
					{
						if (currentCommandDat != null)
							currentCommandDat.CommandList.Add(currentCommand = new CommandNode("*---INFO---*"));

						continue;
					}

					if (lineString.StartsWith("�") || lineString.StartsWith("$end"))
					{
						if (currentCommand != null)
						{
							currentCommand.TextArray = (string[])commandText.ToArray();
							currentCommand = null;
							commandText.Clear();
						}

						if (!lineString.StartsWith("$end") && currentCommandDat != null)
						{
							currentCommandDat.CommandList.Add(currentCommand = new CommandNode(lineString));

							continue;
						}
					}

					if (currentCommand != null)
					{
						lineString = lineString.Replace((char)0x3f, '-');
						lineString = lineString.Replace("@R-button", "_L"); // Run?

						commandText.Add(lineString);
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ReadCommandDat", "CommandDat", ex.Message, ex.StackTrace);
			}
		}

		public override bool CheckEnabled()
		{
			if (Settings.MAME.GameName == null)
				return false;

			if (!m_gameDictionary.ContainsKey(Settings.MAME.GameName))
			{
				if (Settings.MAME.MachineNode == null)
					return false;

				if (Settings.MAME.MachineNode.ROMOf == null)
					return false;

				if (!m_gameDictionary.ContainsKey(Settings.MAME.MachineNode.ROMOf))
					return false;
			}

			return true;
		}

		public override void Reset(EmulatorMode mode)
		{
			m_showingMenu = true;
			m_pagepos = 0;

			m_menuPage.CurrentLine = 0;
			m_menuPage.PageOffset = 0;
			m_menuPage.TotalLines = 0;

			for (int i = 0; i < 100; i++)
			{
				m_pages[i].CurrentLine = 0;
				m_pages[i].PageOffset = 0;
				m_pages[i].TotalLines = 0;
			}
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
