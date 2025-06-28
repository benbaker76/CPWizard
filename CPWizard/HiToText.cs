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
using System.Diagnostics;

namespace CPWizard
{
	class ColumnNode
	{
		public int ColumnWidth = 0;
		public List<string> Items = null;

		public ColumnNode()
		{
			Items = new List<string>();
		}
	}

	class HiToText : RenderObject, IDisposable
	{
		private Font m_font = new Font("Lucida Console", 18, FontStyle.Bold);

		public Dictionary<string, string> RomsSupportedHash = null;

		private int m_visibleLines = 20;
		private int m_pageOffset = 0;
		private int m_totalLines = 0;

		private SizeF m_fontSize = SizeF.Empty;
		private Size m_paddingSize = Size.Empty;
		private Size m_backSize = Size.Empty;

		private ColumnNode[] m_hiScoreTable = null;
		private string[] m_hiScoreString = null;

		private const int MAX_LINE_COLS = 37;

		public HiToText()
		{
			try
			{
				RomsSupportedHash = new Dictionary<string, string>();

				GetRomsSupported();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("StoryDat", "StoryDat", ex.Message, ex.StackTrace);
			}
		}

		private void OnInputEvent(object sender, InputEventArgs e)
		{
			try
			{
				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.MenuUp))
				{
					e.Handled = true;

					if (e.IsDown)
					{
						if (m_pageOffset > 0)
							m_pageOffset--;
						//else
						//    if (m_totalLines > m_visibleLines)
						//        m_pageOffset = m_totalLines - m_visibleLines;

						EventManager.UpdateDisplay();
					}
				}

				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.MenuDown))
				{
					e.Handled = true;

					if (e.IsDown)
					{
						if (m_pageOffset + m_visibleLines < m_totalLines)
							m_pageOffset++;
						//else
						//    m_pageOffset = 0;

						EventManager.UpdateDisplay();
					}
				}

				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.MenuLeft))
				{
					e.Handled = true;

					if (e.IsDown)
					{
						m_pageOffset -= m_visibleLines - 1;

						if (m_pageOffset < 0)
							m_pageOffset = 0;

						EventManager.UpdateDisplay();
					}

					return;
				}

				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.MenuRight))
				{
					e.Handled = true;

					if (e.IsDown)
					{
						m_pageOffset += m_visibleLines - 1;

						if (m_pageOffset + m_visibleLines > m_totalLines)
							m_pageOffset = m_totalLines - m_visibleLines;

						if (m_pageOffset < 0)
							m_pageOffset = 0;

						EventManager.UpdateDisplay();
					}
				}

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
				LogFile.WriteLine("OnGlobalKeyEvent", "HiToText", ex.Message, ex.StackTrace);
			}
		}

		private bool GetRomsSupported()
		{
			try
			{
				Process HiToTextProcess = new Process();
				HiToTextProcess.StartInfo.FileName = Settings.Files.HiToTextExe;
				HiToTextProcess.StartInfo.Arguments = "-l";
				HiToTextProcess.StartInfo.UseShellExecute = false;
				HiToTextProcess.StartInfo.RedirectStandardOutput = true;
				HiToTextProcess.StartInfo.CreateNoWindow = true;
				HiToTextProcess.Start();

				string supportedGamesString = HiToTextProcess.StandardOutput.ReadToEnd();
				HiToTextProcess.WaitForExit();

				string[] supportedGamesArray = supportedGamesString.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

				for (int i = 0; i < supportedGamesArray.Length; i++)
				{
					if (!RomsSupportedHash.ContainsKey(supportedGamesArray[i]))
						RomsSupportedHash.Add(supportedGamesArray[i], null);
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetHiToTextSupportedRoms", "HiToText", ex.Message, ex.StackTrace);
			}

			return false;
		}

		private bool CreateHiToText(string RomName)
		{
			try
			{
				Process HiToTextProcess = new Process();
				HiToTextProcess.StartInfo.FileName = Settings.Files.HiToTextExe;
				HiToTextProcess.StartInfo.Arguments = String.Format("-r {0}.hi", Path.Combine(Settings.Folders.MAME.Hi, RomName));
				HiToTextProcess.StartInfo.UseShellExecute = false;
				HiToTextProcess.StartInfo.RedirectStandardOutput = true;
				HiToTextProcess.StartInfo.CreateNoWindow = true;
				HiToTextProcess.Start();

				string hiscoreTableString = HiToTextProcess.StandardOutput.ReadToEnd();
				HiToTextProcess.WaitForExit();

				if (hiscoreTableString.StartsWith("Error:"))
					return false;

				string[] hiscoreTableArray = hiscoreTableString.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

				if (hiscoreTableArray.Length < 1)
					return false;

				int columnCount = hiscoreTableArray[0].Split('|').Length;
				m_hiScoreTable = new ColumnNode[columnCount];

				for (int x = 0; x < m_hiScoreTable.Length; x++)
				{
					m_hiScoreTable[x] = new ColumnNode();
					m_hiScoreTable[x].ColumnWidth = MAX_LINE_COLS / m_hiScoreTable.Length;
				}

				for (int y = 0; y < hiscoreTableArray.Length; y++)
				{
					string[] columnArray = hiscoreTableArray[y].Split('|');

					for (int x = 0; x < columnArray.Length; x++)
					{
						m_hiScoreTable[x].Items.Add(columnArray[x]);

						if (columnArray[x].Length + 2 > m_hiScoreTable[x].ColumnWidth)
							m_hiScoreTable[x].ColumnWidth = columnArray[x].Length + 2;
					}
				}

				return true;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("CreateHiToText", "HiToText", ex.Message, ex.StackTrace);
			}

			return false;
		}

		private void MeasureText()
		{
			try
			{
				using (Bitmap measureBmp = new Bitmap(1, 1))
				{
					using (Graphics g = Graphics.FromImage(measureBmp))
					{
						int totalWidth = 0;

						for (int x = 0; x < m_hiScoreTable.Length; x++)
							totalWidth += m_hiScoreTable[x].ColumnWidth;

						m_fontSize = g.MeasureString(StringTools.StrFillChar('*', totalWidth), m_font, 0, StringFormat.GenericTypographic);
						m_paddingSize = new Size((int)(m_fontSize.Width * 0.02f), (int)((m_fontSize.Height * m_visibleLines) * 0.04f));
						m_backSize = new Size((int)m_fontSize.Width, (int)(m_fontSize.Height * m_visibleLines));
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("MeasureText", "HiToText", ex.Message, ex.StackTrace);
			}
		}

		private bool GetHiToText()
		{
			try
			{
				if (!RomsSupportedHash.ContainsKey(Settings.MAME.GameName))
					return false;

				if (CreateHiToText(Settings.MAME.GameName))
				{
					m_totalLines = m_hiScoreTable[0].Items.Count;
					m_hiScoreString = new string[m_totalLines];

					for (int x = 0; x < m_hiScoreTable.Length; x++)
					{
						string formatString = "{0,-" + m_hiScoreTable[x].ColumnWidth.ToString() + "}";

						for (int y = 0; y < m_hiScoreTable[x].Items.Count; y++)
							m_hiScoreString[y] += String.Format(formatString, m_hiScoreTable[x].Items[y]);
					}

					MeasureText();

					return true;
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetHiToText", "HiToText", ex.Message, ex.StackTrace);
			}

			return false;
		}

		private void DrawScrollBar(Graphics g, Rectangle rect)
		{
			try
			{
				using (Brush backBrush = new SolidBrush(Color.Black))
				{
					using (Brush markBrush = new SolidBrush(Color.MidnightBlue))
					{
						RectangleF ScrollBarRect = new RectangleF(rect.Width * 0.98f, rect.Height * 0.04f, rect.Width * 0.01f, rect.Height * 0.92f);

						float markOffset = (ScrollBarRect.Height * (float)m_pageOffset / (float)m_totalLines);
						float markHeight = (ScrollBarRect.Height * (float)m_visibleLines / (float)m_totalLines);

						if (markHeight > ScrollBarRect.Height)
							markHeight = ScrollBarRect.Height;

						RectangleF MarkerRect = new RectangleF(ScrollBarRect.Left, ScrollBarRect.Y + markOffset, ScrollBarRect.Width, markHeight);

						g.FillRectangle(backBrush, ScrollBarRect);
						g.FillRectangle(markBrush, MarkerRect);
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawScrollBar", "HiToText", ex.Message, ex.StackTrace);
			}
		}

		public Bitmap GetHiToTextBitmap(string RomName)
		{
			try
			{
				if (m_hiScoreString == null)
					return null;

				Bitmap storyTextBmp = new Bitmap(m_backSize.Width, (int)(m_fontSize.Height * m_visibleLines));

				using (Graphics g = Graphics.FromImage(storyTextBmp))
				{
					Globals.DisplayManager.SetGraphicsQuality(g, DisplayManager.UserGraphicsQuality());

					int y = 0;

					for (int i = m_pageOffset; i < m_hiScoreString.Length; i++)
					{
						using (Brush backBrush = new SolidBrush(Color.Black))
						{
							Brush foreBrush = null;

							if (i == 0)
								foreBrush = new SolidBrush(Color.Red);
							else
								foreBrush = new SolidBrush(Color.White);

							g.DrawString(m_hiScoreString[i], m_font, backBrush, 1, y + 1, StringFormat.GenericTypographic);
							g.DrawString(m_hiScoreString[i], m_font, foreBrush, 0, y, StringFormat.GenericTypographic);

							backBrush.Dispose();
						}

						y += (int)m_fontSize.Height;
					}
				}

				return storyTextBmp;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetHiToTextBitmap", "HiToText", ex.Message, ex.StackTrace);
			}

			return null;
		}

		public override void Paint(Graphics g, int x, int y, int width, int height)
		{
			try
			{
				if (Settings.MAME.GameName == null)
					return;

				using (Bitmap hiToTextBmp = GetHiToTextBitmap(Settings.MAME.GameName))
				{
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

									//DrawSelectionBar(srcGraphics);

									srcGraphics.DrawImage(hiToTextBmp, new Rectangle(0, 0, m_backSize.Width, m_backSize.Height), 0, 0, hiToTextBmp.Width, (int)(m_visibleLines * m_fontSize.Height), GraphicsUnit.Pixel);
									destGraphics.DrawImage(srcBmp, new Rectangle(m_paddingSize.Width, m_paddingSize.Height, m_backSize.Width - (m_paddingSize.Width * 2), m_backSize.Height - (m_paddingSize.Height * 2)), 0, 0, srcBmp.Width, srcBmp.Height, GraphicsUnit.Pixel);

									g.DrawImage(destBmp, new Rectangle(x, y, width, height), 0, 0, destBmp.Width, destBmp.Height, GraphicsUnit.Pixel);

									DrawScrollBar(g, new Rectangle(x, y, width, height));
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("Paint", "HiToText", ex.Message, ex.StackTrace);
			}
		}

		public override bool CheckEnabled()
		{
			try
			{
				if (String.IsNullOrEmpty(Settings.MAME.GameName) ||
					String.IsNullOrEmpty(Settings.Files.HiToTextExe) ||
					String.IsNullOrEmpty(Settings.Folders.MAME.Hi))
					return false;

				if (!File.Exists(Settings.Files.HiToTextExe))
					return false;

				if (!Directory.Exists(Settings.Folders.MAME.Hi))
					return false;

				if (!GetHiToText())
					return false;

				return true;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("CheckEnabled", "HiToText", ex.Message, ex.StackTrace);
			}

			return false;
		}

		public override void Reset(EmulatorMode mode)
		{
			//m_currentLine = 0;
			m_pageOffset = 0;
			m_totalLines = 0;
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
