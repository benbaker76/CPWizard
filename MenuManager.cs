// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace CPWizard
{
	public delegate void MenuItemSelectHandler(string item);

	public class MenuManager : RenderObject, IDisposable
	{
		private int m_currentLine = 0;
		private int m_visibleLines = 8;
		private int m_pageOffset = 0;

		private RectangleF m_mainRect;
		private Size m_backSize = Size.Empty;
		private SizeF m_fontSize = SizeF.Empty;
		//private SizeF m_paddingSize = SizeF.Empty;

		private List<string> m_items = null;

		public event MenuItemSelectHandler MenuItemSelect = null;

		public MenuManager()
		{
			try
			{
				m_items = new List<string>();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("MenuManager", "MenuManager", ex.Message, ex.StackTrace);
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
						if (MenuItemSelect != null)
							MenuItemSelect(m_items[m_pageOffset + m_currentLine]);
					}
				}

				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.BackKey))
				{
					e.Handled = true;

					if (!e.IsDown)
					{
						if (MenuItemSelect != null)
							MenuItemSelect("!");
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

				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.MenuUp))
				{
					e.Handled = true;

					if (e.IsDown)
					{
						if (m_currentLine > 0)
							m_currentLine--;
						else
							if (m_pageOffset > 0)
								m_pageOffset--;
							else
							{
								if (m_items.Count < m_visibleLines)
									m_currentLine = m_items.Count - 1;
								else
								{
									m_currentLine = m_visibleLines - 1;
									m_pageOffset = m_items.Count - m_visibleLines;
								}
							}

						EventManager.UpdateDisplay();
					}
				}

				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.MenuDown))
				{
					e.Handled = true;

					if (e.IsDown)
					{
						if (m_currentLine + m_pageOffset < m_items.Count - 1)
						{
							if (m_currentLine < m_visibleLines - 1)
								m_currentLine++;
							else
								m_pageOffset++;
						}
						else
						{
							m_currentLine = 0;
							m_pageOffset = 0;
						}

						EventManager.UpdateDisplay();
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("OnGlobalKeyEvent", "MenuManager", ex.Message, ex.StackTrace);
			}
		}

		private void DrawShadow(Graphics g, string text, Font font, RectangleF rect)
		{
			try
			{
				using (Brush b = new SolidBrush(Color.Black))
					g.DrawString(text, font, b, new RectangleF(rect.X + 3, rect.Y + 3, rect.Width, rect.Height), StringFormat.GenericTypographic);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawShadow", "MenuManager", ex.Message, ex.StackTrace);
			}
		}

		private void DrawString(Graphics g, string text, Font font, Color color, RectangleF rect)
		{
			try
			{
				using (SolidBrush b = new SolidBrush(color))
					g.DrawString(text, font, b, rect, StringFormat.GenericTypographic);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawString", "MenuManager", ex.Message, ex.StackTrace);
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
						m_fontSize = g.MeasureString(StringTools.StrFillChar('*', 16), Settings.Display.MenuFont, 0, StringFormat.GenericTypographic);

						foreach (string item in m_items)
						{
							SizeF maxFontSize = g.MeasureString(item, Settings.Display.MenuFont, 0, StringFormat.GenericTypographic);

							if (maxFontSize.Width > m_fontSize.Width)
								m_fontSize.Width = maxFontSize.Width;
							if (maxFontSize.Height > m_fontSize.Height)
								m_fontSize.Height = maxFontSize.Height;
						}

						m_fontSize.Height *= 1.04f;

						m_backSize = new Size((int)m_fontSize.Width, (int)(m_fontSize.Height * m_visibleLines));
						m_mainRect = new RectangleF(m_backSize.Width * 0.03f, m_backSize.Height * 0.04f, m_backSize.Width * 0.94f, m_backSize.Height * 0.92f);
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("MeasureText", "MenuManager", ex.Message, ex.StackTrace);
			}
		}

		private void DrawSelectionBar(Graphics g)
		{
			try
			{
				Rectangle rect = new Rectangle(0, (int)((float)m_currentLine * (float)m_fontSize.Height), (int)m_fontSize.Width, (int)(m_fontSize.Height * 1.04f));

				//Color[] color = { Color.FromArgb(0, 0, 0, 0), Color.DeepSkyBlue, Color.FromArgb(0, 0, 0, 0) };
				Color[] color = { Color.FromArgb(0, 0, 0, 0), Settings.Display.MenuSelectorBarColor, Color.FromArgb(0, 0, 0, 0) };
				float[] colorPositions = { 0.0f, .5f, 1.0f };

				ColorBlend colorBlend = new ColorBlend();
				colorBlend.Colors = color;
				colorBlend.Positions = colorPositions;
				
				using (LinearGradientBrush b = new LinearGradientBrush(rect, Color.Empty, Color.Empty, LinearGradientMode.Horizontal))
				{
					b.InterpolationColors = colorBlend;

					g.FillRectangle(b, rect);	
				}

				if (Settings.Display.UseMenuBorders)
				{
					using (Pen pen = new Pen(Settings.Display.MenuSelectorBorderColor, 1))
					g.DrawRectangle(pen, new Rectangle(new Point(rect.X-1, rect.Y), new Size(rect.Width + 2, rect.Height)));
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawSelectionBar", "MenuManager", ex.Message, ex.StackTrace);
			}
		}

		public void DrawMenuList(Graphics g)
		{
			try
			{
				using (Bitmap bmpText = new Bitmap((int)m_backSize.Width, (int)m_backSize.Height))
				{
					using (Graphics gText = Graphics.FromImage(bmpText))
					{
						Globals.DisplayManager.SetGraphicsQuality(gText, DisplayManager.UserGraphicsQuality());

						float yOffset = 0;
						int lineCount = 0;
						
						DrawSelectionBar(gText);

						for (int i = m_pageOffset; i < m_items.Count; i++)
						{
							DrawShadow(gText, m_items[i], Settings.Display.MenuFont, new RectangleF(0, yOffset, 0, 0));
							DrawString(gText, m_items[i], Settings.Display.MenuFont, Settings.Display.MenuFontColor, new RectangleF(0, yOffset, 0, 0));

							yOffset += m_fontSize.Height;

							lineCount++;

							if (lineCount == m_visibleLines)
								break;
						}

						g.DrawImage(bmpText, m_mainRect);
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawMenuList", "MenuManager", ex.Message, ex.StackTrace);
			}
		}

		public override void Paint(Graphics g, int x, int y, int width, int height)
		{
			try
			{
				lock (this)
				{
					MeasureText();

					using (Bitmap srcBmp = new Bitmap(m_backSize.Width, m_backSize.Height))
					{
						using (Graphics srcGraphics = Graphics.FromImage(srcBmp))
						{
							Bitmap menuBakBitmap = Globals.DisplayManager.BakArray[(int)BakType.Menu];

							srcGraphics.DrawImage(menuBakBitmap, new Rectangle(0, 0, m_backSize.Width, m_backSize.Height), 0, 0, menuBakBitmap.Width, menuBakBitmap.Height, GraphicsUnit.Pixel);

							DrawMenuList(srcGraphics);

							g.DrawImage(srcBmp, new Rectangle(x, y, width, height), 0, 0, m_backSize.Width, m_backSize.Height, GraphicsUnit.Pixel);

							if (Settings.Display.UseMenuBorders)
							{
								using (Pen pen = new Pen(Settings.Display.MenuBorderColor, 1))
								{
									pen.Alignment = PenAlignment.Outset;
									g.DrawRectangle(pen, new Rectangle(x, y, width, height));

								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("Paint", "MenuManager", ex.Message, ex.StackTrace);
			}
		}

		public override void Reset(EmulatorMode mode)
		{
			m_currentLine = 0;
			m_pageOffset = 0;
		}

		/* void OnMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Size displaySize = Global.LayoutForm.ClientSize;
			Size bitmapSize = Global.MainBitmap.Size;
			SizeF ratioSize = new SizeF((float)bitmapSize.Width / (float)displaySize.Width, (float)bitmapSize.Height / (float)displaySize.Height);
			Point mousePoint = new Point((int)((float)e.X * ratioSize.Width), (int)((float)e.Y * ratioSize.Height));
			SizeF fontSize = new Size((int)((float)m_fontSize.Width * ratioSize.Width), (int)((float)m_fontSize.Height * ratioSize.Height));

			if (Location.Contains(mousePoint))
			{
				int yOffset = 0;
				int LineCount = 0;

				for (int i = m_pageOffset; i < m_items.Count; i++)
				{
					Rectangle rect = new Rectangle(Location.X, (int)(Location.Y + (float)LineCount * (float) fontSize.Height), Location.Width, (int) fontSize.Height);

					if (rect.Contains(mousePoint))
					{
						if (m_currentLine != i - m_pageOffset)
						{
							m_currentLine = i - m_pageOffset;
							EventManager.UpdateDisplay();
						}
                        
						return;
					}

					yOffset += (int)m_fontSize.Height;

					LineCount++;

					if (LineCount == m_totalLines)
						break;
				}
			}
		}

		void OnMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Size displaySize = Global.LayoutForm.ClientSize;
			Size bitmapSize = Global.MainBitmap.Size;
			Point mousePoint = new Point((int)((float)e.X * ((float)bitmapSize.Width / (float)displaySize.Width)), (int)((float)e.Y * ((float)bitmapSize.Height / (float)displaySize.Height)));
	   } */

		public override void AddEventHandlers()
		{
			Globals.InputManager.InputEvent += new InputManager.InputEventHandler(OnInputEvent);
			//EventHandler.OnMouseMove += new EventHandler.MouseHandler(OnMouseMove);
			//EventHandler.OnMouseUp += new EventHandler.MouseHandler(OnMouseUp);
		}

		public override void RemoveEventHandlers()
		{
			Globals.InputManager.InputEvent -= new InputManager.InputEventHandler(OnInputEvent);
			//EventHandler.OnMouseMove -= new EventHandler.MouseHandler(OnMouseMove);
			//EventHandler.OnMouseUp -= new EventHandler.MouseHandler(OnMouseUp);
		}

		public override bool CheckEnabled()
		{
			return true;
		}

		public List<string> Items
		{
			get { return m_items; }
			set { m_items = value; }
		}

		#region IDisposable Members

		public override void Dispose()
		{
			base.Dispose();

			RemoveEventHandlers();
		}

		#endregion
	}
}
