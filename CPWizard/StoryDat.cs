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

namespace CPWizard
{
	class StoryDat : RenderObject, IDisposable
	{
		public class StoryDatNode
		{
			public string Name = null;
			public List<string> Text = null;

			public StoryDatNode(string name)
			{
				Name = name;
				Text = new List<string>();
			}
		}

		private enum StoryDatElement
		{
			Nothing,
			Comment,
			Info,
			Story,
			End
		}

		public Dictionary<string, StoryDatNode> StoryDatHash = null;

		private Font m_font = new Font("Lucida Console", 18, FontStyle.Bold);

		//private int m_currentLine = 0;
		private int m_visibleLines = 20;
		private int m_pageOffset = 0;
		private int m_totalLines = 0;

		private SizeF m_fontSize = SizeF.Empty;
		private Size m_paddingSize = Size.Empty;
		private Size m_backSize = Size.Empty;

		private const int MAX_LINE_COLS = 37;

		public StoryDat()
		{
			try
			{
				StoryDatHash = new Dictionary<string, StoryDatNode>();

				MeasureText();
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
				LogFile.WriteLine("OnGlobalKeyEvent", "StoryDat", ex.Message, ex.StackTrace);
			}
		}

		public void ReadStoryDat(string path)
		{
			ReadStoryDat(path, null);
		}

		public void ReadStoryDat(string path, string RomName)
		{
			try
			{
				StoryDatElement CurrentElement = StoryDatElement.Nothing;
				List<string> ROMNames = new List<string>();
				StoryDatNode CurrentStoryDat = null;
				bool Found = false;

				StoryDatHash.Clear();
				//StoryDatArray.Clear();

				string[] Lines = File.ReadAllLines(path, Encoding.ASCII);

				for (int i = 0; i < Lines.Length; i++)
				{
					string Line = Lines[i];

					if (Line.StartsWith("#"))
					{
						CurrentElement = StoryDatElement.Comment;

						continue;
					}

					if (Line.StartsWith("$info"))
					{
						CurrentElement = StoryDatElement.Info;

						if (Found)
							return;

						CurrentStoryDat = null;

						string[] Values = Line.Split('=');

						if (Values.Length == 2)
						{
							if (RomName != null)
							{
								if (Values[1] == RomName)
								{
									StoryDatHash.Add(Values[1], CurrentStoryDat = new StoryDatNode(Values[1]));
									//StoryDatArray.Add(CurrentStoryDat);

									Found = true;
								}
							}
							else
							{
								StoryDatHash.Add(Values[1], CurrentStoryDat = new StoryDatNode(Values[1]));
								//StoryDatArray.Add(CurrentStoryDat);
							}
						}

						continue;
					}

					if (Line.StartsWith("$story"))
					{
						CurrentElement = StoryDatElement.Story;

						continue;
					}

					if (Line.StartsWith("$end"))
					{
						CurrentElement = StoryDatElement.End;

						CurrentStoryDat = null;

						continue;
					}

					switch (CurrentElement)
					{
						case StoryDatElement.Story:
							if (CurrentStoryDat != null)
								if (Line.Length > MAX_LINE_COLS)
									CurrentStoryDat.Text.Add(Line.Substring(0, MAX_LINE_COLS));
								else
									CurrentStoryDat.Text.Add(Line);
							break;
					}

					//System.Windows.Forms.Application.DoEvents();
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ReadStoryDat", "StoryDat", ex.Message, ex.StackTrace);
			}
		}

		private void MeasureText()
		{
			try
			{
				using (Bitmap measureBmp = new Bitmap(1, 1))
				{
					using (Graphics g = Graphics.FromImage(measureBmp))
					{
						m_fontSize = g.MeasureString(StringTools.StrFillChar('*', MAX_LINE_COLS), m_font, 0, StringFormat.GenericTypographic);
						m_paddingSize = new Size((int)(m_fontSize.Width * 0.04f), (int)((m_fontSize.Height * m_visibleLines) * 0.04f));
						m_backSize = new Size((int)m_fontSize.Width, (int)(m_fontSize.Height * m_visibleLines));
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("MeasureText", "StoryDat", ex.Message, ex.StackTrace);
			}
		}

		private string[] GetStoryDatPage()
		{
			try
			{
				StoryDatNode StoryDat = null;

				if (!StoryDatHash.TryGetValue(Settings.MAME.GameName, out StoryDat))
				{
					if (Settings.MAME.GetParent() == null)
						return null;

					if (!StoryDatHash.TryGetValue(Settings.MAME.GetParent(), out StoryDat))
						return null;
				}

				string[] StoryDatString = (string[])StoryDat.Text.ToArray();

				m_totalLines = StoryDatString.Length;

				return StoryDatString;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetStoryDatPage", "StoryDat", ex.Message, ex.StackTrace);
			}

			return null;
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
				LogFile.WriteLine("DrawScrollBar", "StoryDat", ex.Message, ex.StackTrace);
			}
		}

		public Bitmap GetStoryDatTextBitmap()
		{
			try
			{
				string[] StoryDatString = GetStoryDatPage();

				if (StoryDatString == null)
					return null;

				Bitmap storyTextBmp = new Bitmap(m_backSize.Width, (int)(m_fontSize.Height * m_visibleLines));

				using (Graphics g = Graphics.FromImage(storyTextBmp))
				{
					Globals.DisplayManager.SetGraphicsQuality(g, DisplayManager.UserGraphicsQuality());

					int y = 0;

					for (int i = m_pageOffset; i < StoryDatString.Length; i++)
					{
						Brush backBrush = new SolidBrush(Color.Black);
						Brush foreBrush = new SolidBrush(Color.White);

						g.DrawString(StoryDatString[i], m_font, backBrush, 1, y + 1, StringFormat.GenericTypographic);
						g.DrawString(StoryDatString[i], m_font, foreBrush, 0, y, StringFormat.GenericTypographic);

						backBrush.Dispose();
						foreBrush.Dispose();

						y += (int)m_fontSize.Height;
					}
				}

				return storyTextBmp;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetStoryDatTextBitmap", "StoryDat", ex.Message, ex.StackTrace);
			}

			return null;
		}

		public override void Paint(Graphics g, int x, int y, int width, int height)
		{
			try
			{
				if (Settings.MAME.GameName == null)
					return;

				using (Bitmap storyTextBmp = GetStoryDatTextBitmap())
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

									srcGraphics.DrawImage(storyTextBmp, new Rectangle(0, 0, m_backSize.Width, m_backSize.Height), 0, 0, storyTextBmp.Width, (int)(m_visibleLines * m_fontSize.Height), GraphicsUnit.Pixel);
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
				LogFile.WriteLine("Paint", "StoryDat", ex.Message, ex.StackTrace);
			}
		}

		public override bool CheckEnabled()
		{
			if (Settings.MAME.GameName == null)
				return false;

			if (!StoryDatHash.ContainsKey(Settings.MAME.GameName))
			{
				if (Settings.MAME.GetParent() == null)
					return false;

				if (!StoryDatHash.ContainsKey(Settings.MAME.GetParent()))
					return false;
			}

			return true;
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
