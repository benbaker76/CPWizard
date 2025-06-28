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
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CPWizard
{
	abstract class TextViewer : RenderObject, IDisposable
	{
		private Font m_font = new Font("Lucida Console", 14, FontStyle.Bold);

		//private int m_currentLine = 0;
		private int m_visibleLines = 20;
		private int m_pageOffset = 0;
		private int m_totalLines = 0;

		public List<string> Lines = null;
		private RectangleF MainRect;
		private SizeF BackSize;
		private SizeF FontSize;

		private string GameName = null;

		private const int MAX_LINE_COLS = 66;

		public TextViewer()
		{
			try
			{
				Lines = new List<string>();

				GetSize();

				MainRect = new RectangleF(BackSize.Width * 0.03f, BackSize.Height * 0.04f, BackSize.Width * 0.94f, BackSize.Height * 0.92f);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("TextViewer", "TextViewer", ex.Message, ex.StackTrace);
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
						//    if(m_totalLines > m_visibleLines)
						//        m_pageOffset = m_totalLines - m_visibleLines;

						EventManager.UpdateDisplay();
					}

					return;
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
				LogFile.WriteLine("OnGlobalKeyEvent", "TextViewer", ex.Message, ex.StackTrace);
			}
		}

		public void GetSize()
		{
			try
			{
				using (Bitmap backBmp = new Bitmap(1, 1))
				{
					using (Graphics g = Graphics.FromImage(backBmp))
					{
						FontSize = g.MeasureString(StringTools.StrFillChar('*', MAX_LINE_COLS), m_font, 1024, StringFormat.GenericTypographic);
						BackSize = new SizeF(FontSize.Width, (int)FontSize.Height * m_visibleLines);
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetSize", "TextViewer", ex.Message, ex.StackTrace);
			}
		}

		private void DrawScrollBar(Graphics g, Rectangle rect)
		{
			try
			{
				Brush backBrush = new SolidBrush(Color.Black);
				Brush markBrush = new SolidBrush(Color.MidnightBlue);

				RectangleF ScrollBarRect = new RectangleF(rect.Width * 0.98f, rect.Height * 0.04f, rect.Width * 0.01f, rect.Height * 0.92f);

				float markOffset = (ScrollBarRect.Height * (float)m_pageOffset / (float)m_totalLines);
				float markHeight = (ScrollBarRect.Height * (float)m_visibleLines / (float)m_totalLines);

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

		private void DrawShadow(Graphics g, string text, Font font, RectangleF rect)
		{
			try
			{
				Brush b = new SolidBrush(Color.Black);

				g.DrawString(text, font, b, new RectangleF(rect.X + 1, rect.Y + 1, rect.Width, rect.Height), StringFormat.GenericTypographic);

				b.Dispose();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawShadow", "TextViewer", ex.Message, ex.StackTrace);
			}
		}

		private void DrawString(Graphics g, string text, Font font, Color color, RectangleF rect)
		{
			try
			{
				SolidBrush b = new SolidBrush(color);

				g.DrawString(text, font, b, rect, StringFormat.GenericTypographic);

				b.Dispose();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawString", "TextViewer", ex.Message, ex.StackTrace);
			}
		}

		/* private void DrawSelectionBar(Graphics g)
		{
			try
			{
				Rectangle rect = new Rectangle(0, (int)(m_currentLine * (int)FontSize.Height), (int)FontSize.Width, (int)FontSize.Height);

				//Color[] color = { Color.FromArgb(0, 0, 0, 0), Color.DeepSkyBlue, Color.FromArgb(0, 0, 0, 0) };
				Color[] color = { Color.FromArgb(0, 0, 0, 0), Color.Yellow, Color.FromArgb(0, 0, 0, 0) };
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
				LogFile.WriteLine("DrawSelectionBar", "TextViewer", ex.Message, ex.StackTrace);
			}
		} */

		private void DrawLines(Graphics g)
		{
			try
			{
				Bitmap bmpText = new Bitmap((int)BackSize.Width, (int)BackSize.Height);
				Graphics gText = Graphics.FromImage(bmpText);

				Globals.DisplayManager.SetGraphicsQuality(gText, DisplayManager.UserGraphicsQuality());

				int yOffset = 0;
				int LineCount = 0;

				//DrawSelectionBar(gText);

				for (int i = m_pageOffset; i < Lines.Count; i++)
				{
					DrawShadow(gText, Lines[i], m_font, new RectangleF(0, yOffset, 0, 0));
					DrawString(gText, Lines[i], m_font, Color.White, new RectangleF(0, yOffset, 0, 0));

					yOffset += (int)FontSize.Height;

					if (LineCount == m_visibleLines - 1)
						break;
					else
						LineCount++;
				}

				g.DrawImage(bmpText, MainRect.X, MainRect.Y, MainRect.Width, MainRect.Height);

				bmpText.Dispose();
				gText.Dispose();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawLines", "TextViewer", ex.Message, ex.StackTrace);
			}
		}

		public void AddLine(string text)
		{
			List<string> lines = GetLines(text);

			foreach (string line in lines)
				Lines.Add(line);

			m_totalLines = Lines.Count;
		}

		private List<string> GetLines(string text)
		{
			try
			{
				List<string> lines = new List<string>();

				using (Bitmap bmp = new Bitmap(1, 1))
				{
					using (Graphics g = Graphics.FromImage(bmp))
					{
						SizeF fontSize;
						int xOffset = 0;
						int LineCount = 0;

						string currentLine = null;

						fontSize = g.MeasureString(text, m_font, 0, StringFormat.GenericTypographic);

						if (fontSize.Width < BackSize.Width)
						{
							lines.Add(text);

							LineCount++;
						}
						else
						{
							string[] splitString = StringTools.SplitString(text, new string[] { " " });

							for (int i = 0; i < splitString.Length; i++)
							{
								int Characters = 0, Lines = 0;

								g.MeasureString(splitString[i].Replace(' ', '*'), m_font, new SizeF(BackSize.Width - xOffset, FontSize.Height), StringFormat.GenericTypographic, out Characters, out Lines);
								fontSize = g.MeasureString(splitString[i].Replace(' ', '*'), m_font, 0, StringFormat.GenericTypographic);

								if (Lines == 0)
								{
									xOffset = 0;
									LineCount++;
									lines.Add(currentLine);
									currentLine = null;

									if (splitString[i] == " ")
										continue;
								}

								currentLine += splitString[i];

								xOffset += (int)Math.Ceiling(fontSize.Width);
							}
						}

						if (currentLine != null)
						{
							lines.Add(currentLine);

							LineCount++;
						}
					}
				}

				return lines;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetLines", "TextViewer", ex.Message, ex.StackTrace);
			}

			return null;
		}

		public virtual void GetLineData()
		{
			//m_currentLine = 0;
			m_pageOffset = 0;
		}

		public override void Paint(Graphics g, int x, int y, int width, int height)
		{
			try
			{
				if (GameName != Settings.MAME.GameName)
				{
					GetLineData();
					GameName = Settings.MAME.GameName;
				}

				using (Bitmap bmp = new Bitmap((int)BackSize.Width, (int)(BackSize.Height)))
				{
					using (Graphics gSrc = Graphics.FromImage(bmp))
					{
						DrawLines(gSrc);

						g.DrawImage(bmp, new Rectangle(x, y, width, height), 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel);

						DrawScrollBar(g, new Rectangle(x, y, width, height));
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("Paint", "TextViewer", ex.Message, ex.StackTrace);
			}
		}

		public override bool CheckEnabled()
		{
			return false;
		}

		public override void Reset(EmulatorMode mode)
		{
			//m_currentLine = 0;
			m_pageOffset = 0;
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
