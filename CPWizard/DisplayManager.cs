// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace CPWizard
{
	public enum BakType
	{
		Default,
		Blank,
		Menu,
		MainMenu,
		Info,
		MAME,
		BakCount
	};

	public class DisplayManager : IDisposable
	{
		public enum GraphicsQuality { Low, High };

		public static string[] BakName =
		{
			"Bak.png",
			"BlankBak.png",
			"MenuBak.png",
			"MainMenuBak.png",
			"InfoBak.png",
			""
		};

		private LayoutObject.MouseTools CurrentTool = LayoutObject.MouseTools.NoTool;

		private Font RegularFont = new Font("Lucida Console", 14, FontStyle.Bold);

		public Rectangle MultiSelectRect = Rectangle.Empty;
		private Point MultiSelectOffset = Point.Empty;

		//private Bitmap m_logo = null;
		private BakType m_bakType = BakType.Default;
		private Bitmap[] m_bakArray = null;

		public DisplayManager()
		{
			//m_logo = FileIO.LoadImage(Path.Combine(Settings.Folders.Media, "CPWizard.png"));

			m_bakArray = new Bitmap[(int)BakType.BakCount];

			for (int i = 0; i < (int)BakType.BakCount; i++)
				LoadBak((BakType)i);

			Globals.SelectedObjectList = new List<LayoutObject>();
			Globals.ClipboardObjectList = new List<LayoutObject>();

			EventManager.OnMouseMove += new EventManager.MouseHandler(OnMouseMove);
			EventManager.OnMouseDown += new EventManager.MouseHandler(OnMouseDown);
			EventManager.OnMouseUp += new EventManager.MouseHandler(OnMouseUp);
			EventManager.OnPaint += new EventManager.PaintHandler(OnPaint);
		}

		public void SetGraphicsQuality(Graphics g, GraphicsQuality quality)
		{
			switch (quality)
			{
				case GraphicsQuality.Low:
					g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
					g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
					g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;
					g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
					g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
					break;
				case GraphicsQuality.High:
					g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
					g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
					g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
					g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
					g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
					break;
			}
		}

		private void LoadMAMEBak()
		{
			if (m_bakArray[(int)BakType.MAME] != null)
			{
				m_bakArray[(int)BakType.MAME].Dispose();
				m_bakArray[(int)BakType.MAME] = null;
			}

			switch (Settings.MAME.Bak)
			{
				case "Default":
					break;
				case "CPanel":
					m_bakArray[(int)BakType.MAME] = FileIO.LoadImage(Path.Combine(Settings.Folders.MAME.CPanel, Settings.MAME.GameName + ".png"));
					break;
				case "Flyer":
					m_bakArray[(int)BakType.MAME] = FileIO.LoadImage(Path.Combine(Settings.Folders.MAME.Flyers, Settings.MAME.GameName + ".png"));
					break;
				case "Marquee":
					m_bakArray[(int)BakType.MAME] = FileIO.LoadImage(Path.Combine(Settings.Folders.MAME.Marquees, Settings.MAME.GameName + ".png"));
					break;
				case "PCB":
					m_bakArray[(int)BakType.MAME] = FileIO.LoadImage(Path.Combine(Settings.Folders.MAME.PCB, Settings.MAME.GameName + ".png"));
					break;
				case "Snap":
					m_bakArray[(int)BakType.MAME] = FileIO.LoadImage(Path.Combine(Settings.Folders.MAME.Snap, Settings.MAME.GameName + ".png"));
					break;
				case "Title":
					m_bakArray[(int)BakType.MAME] = FileIO.LoadImage(Path.Combine(Settings.Folders.MAME.Titles, Settings.MAME.GameName + ".png"));
					break;
			}
		}

		public void LoadBak(BakType bakType)
		{
			if (m_bakArray[(int)bakType] != null)
			{
				m_bakArray[(int)bakType].Dispose();
				m_bakArray[(int)bakType] = null;
			}

			m_bakArray[(int)bakType] = FileIO.LoadImage(Path.Combine(Settings.Folders.Media, BakName[(int)bakType]));
		}

		private Point GetSizePoint(int x, int y)
		{
			try
			{
				Point MousePoint = new Point(x, y);
				Point NewPoint = Point.Empty;

				if (Settings.Display.SnapToGrid)
				{
					Point SnapPoint = SnapToGrid(MousePoint, Settings.Display.GridSize);
					NewPoint = new Point(SnapPoint.X, SnapPoint.Y);
				}
				else
					NewPoint = new Point(MousePoint.X, MousePoint.Y);

				return NewPoint;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetSizePoint", "DisplayManager", ex.Message, ex.StackTrace);
			}

			return Point.Empty;
		}

		private Point GetMovePoint(Point StartPoint, int x, int y)
		{
			try
			{
				Point MousePoint = new Point(x, y);
				Point NewPoint = Point.Empty;

				if (Settings.Display.SnapToGrid)
				{
					Point SnapPoint = SnapToGrid(new Point(MousePoint.X - StartPoint.X, MousePoint.Y - StartPoint.Y), Settings.Display.GridSize);
					NewPoint = new Point(SnapPoint.X, SnapPoint.Y);
				}
				else
					NewPoint = new Point(MousePoint.X - StartPoint.X, MousePoint.Y - StartPoint.Y);

				return NewPoint;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetMovePoint", "DisplayManager", ex.Message, ex.StackTrace);
			}

			return Point.Empty;
		}

		private void OnMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (!Globals.MainForm.Focused)
				return;

			try
			{
				if (e.Button == MouseButtons.Left)
				{
					if (Globals.SelectedObjectList.Count > 0)
					{
						if (CurrentTool == LayoutObject.MouseTools.MoveObject)
						{
							foreach (LayoutObject obj in Globals.SelectedObjectList)
							{
								Point MousePoint = GetMovePoint(obj.StartPoint, e.X, e.Y);

								if (obj.Move(MousePoint.X, MousePoint.Y))
									Globals.Layout.PromptToSave = true;
							}
						}
						else
						{
							Point MousePoint = GetSizePoint(e.X, e.Y);

							foreach (LayoutObject obj in Globals.SelectedObjectList)
							{
								if (obj.Size(CurrentTool, MousePoint.X, MousePoint.Y))
									Globals.Layout.PromptToSave = true;
							}
						}
					}
					else
					{
						Point MousePoint = new Point(e.X, e.Y);

						Point PointOffset = new Point(MousePoint.X - MultiSelectOffset.X, MousePoint.Y - MultiSelectOffset.Y);

						if (PointOffset.X > 0)
						{
							MultiSelectRect.X = MultiSelectOffset.X;
							MultiSelectRect.Width = PointOffset.X;
						}
						else
						{
							MultiSelectRect.X = MousePoint.X;
							MultiSelectRect.Width = Math.Abs(PointOffset.X);
						}

						if (PointOffset.Y > 0)
						{
							MultiSelectRect.Y = MultiSelectOffset.Y;
							MultiSelectRect.Height = PointOffset.Y;
						}
						else
						{
							MultiSelectRect.Y = MousePoint.Y;
							MultiSelectRect.Height = Math.Abs(PointOffset.Y);
						}
					}
				}
				else
				{
					Point MousePoint = new Point(e.X, e.Y);
					SetMouseCursor(MousePoint.X, MousePoint.Y);
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("OnMouseMove", "DisplayManager", ex.Message, ex.StackTrace);
			}
		}

		private void OnMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (Globals.Layout == null)
				return;

			if (!Globals.MainForm.Focused)
				return;

			try
			{
				if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
				{
					Point MousePoint = new Point(e.X, e.Y);

					foreach (LayoutObject layoutObject in Globals.Layout.LayoutObjectList)
					{
						layoutObject.StartPoint.X = MousePoint.X - layoutObject.Rect.X;
						layoutObject.StartPoint.Y = MousePoint.Y - layoutObject.Rect.Y;
					}

					LayoutObject selectedLayoutObject = null;

					if (TryGetSelectedControl(MousePoint.X, MousePoint.Y, out selectedLayoutObject))
					{
						bool ObjectSelected = false;

						foreach (LayoutObject layoutObject in Globals.SelectedObjectList)
						{
							if (layoutObject == selectedLayoutObject)
							{
								ObjectSelected = true;
								break;
							}
						}

						if (!ObjectSelected)
						{
							if (!Globals.MainForm.ShiftDown)
								Globals.SelectedObjectList.Clear();

							Globals.SelectedObjectList.Insert(0, selectedLayoutObject);

							SetMouseCursor(MousePoint.X, MousePoint.Y);
						}
					}
					else
					{
						Globals.SelectedObjectList.Clear();

						MultiSelectOffset = MousePoint;
						MultiSelectRect = Rectangle.Empty;
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("OnMouseDown", "DisplayManager", ex.Message, ex.StackTrace);
			}
		}

		private void OnMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (!Globals.MainForm.Focused)
				return;

			if (e.Button == MouseButtons.Left)
			{
				if (CurrentTool != LayoutObject.MouseTools.MoveObject)
				{
					if (!MultiSelectRect.IsEmpty)
					{
						Globals.SelectedObjectList.Clear();

						foreach (LayoutObject layoutObject in Globals.Layout.LayoutObjectList)
						{
							if (MultiSelectRect.IntersectsWith(layoutObject.Rect))
								Globals.SelectedObjectList.Add(layoutObject);
						}
					}
				}
			}
		}

		private void RotateScreen(Graphics g, int width, int height, int rotation, bool flipX, bool flipY)
		{
			if (flipX || flipY)
			{
				g.ScaleTransform(flipX ? -1f : 1f, flipY ? -1f : 1f);
				g.TranslateTransform(flipX ? -(float)width : 0f, flipY ? -(float)height : 0f);
			}

			switch (rotation)
			{
				case 0:
					break;
				case 90:
					g.TranslateTransform(width, 0);
					g.ScaleTransform((float)width / (float)height, (float)height / (float)width);
					g.RotateTransform(90);
					break;
				case 180:
					g.TranslateTransform(width, height);
					g.RotateTransform(180);
					break;
				case 270:
					g.TranslateTransform(0, height);
					g.ScaleTransform((float)width / (float)height, (float)height / (float)width);
					g.RotateTransform(270);
					break;
			}
		}

		private void DrawShadow(Graphics g, string text, Font font, RectangleF rect)
		{
			try
			{
				using (Brush b = new SolidBrush(Color.Black))
					g.DrawString(text, font, b, new RectangleF(rect.X + 1, rect.Y + 1, rect.Width, rect.Height), StringFormat.GenericTypographic);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawShadow", "DisplayManager", ex.Message, ex.StackTrace);
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
				LogFile.WriteLine("DrawString", "DisplayManager", ex.Message, ex.StackTrace);
			}
		}

		private void DrawShadow(Graphics g, Rectangle rect)
		{
			try
			{
				if (Globals.MenuJustShown)
				{
					using (SolidBrush b = new SolidBrush(Color.FromArgb(64, 0, 0, 0)))
						g.FillRectangle(b, new Rectangle(rect.X + 16, rect.Y + 16, rect.Width + 16, rect.Height + 16));
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawShadow", "DisplayManager", ex.Message, ex.StackTrace);
			}
		}

		private void DrawBackground(Graphics g, BakType bakType, int width, int height, float alpha)
		{
			try
			{
				if (Globals.EmulatorMode == EmulatorMode.MAME)
				{
					bakType = BakType.MAME;

					LoadMAMEBak();
				}

				if (m_bakArray[(int)bakType] == null)
					bakType = BakType.Default;

				m_bakType = bakType;
			
				RectangleF srcRectF = new RectangleF(0, 0, m_bakArray[(int)bakType].Width, m_bakArray[(int)bakType].Height);
				Rectangle destRect = new Rectangle(0, 0, width, height);
				RectangleF destRectF = new RectangleF(-0.5f, -0.5f, width + 1, height + 1);

				if (alpha != 1.0f)
				{
					g.Clear(Color.Black);

					ColorMatrix colorMatrix = new ColorMatrix();
					colorMatrix.Matrix33 = alpha;

					using (ImageAttributes imageAttributes = new ImageAttributes())
					{
						imageAttributes.SetColorMatrix(colorMatrix);
						g.DrawImage(m_bakArray[(int)bakType], destRect, srcRectF.X, srcRectF.Y, srcRectF.Width, srcRectF.Height, GraphicsUnit.Pixel, imageAttributes);
					}
				}
				else
					g.DrawImage(m_bakArray[(int)bakType], destRectF, srcRectF, GraphicsUnit.Pixel);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawBackground", "DisplayManager", ex.Message, ex.StackTrace);
			}
		}

		/* private void DrawLogo(Graphics g, int width, int height)
		{
			try
			{
				g.DrawImage(m_logo, (width / 2) - (m_logo.Width / 2), height * 0.08f);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawLogo", "DisplayManager", ex.Message, ex.StackTrace);
			}
		} */

		private void DrawInfo(Graphics g, string text, int width, int height)
		{
			try
			{
				SizeF textSize = g.MeasureString(text, RegularFont, 0, StringFormat.GenericTypographic);

				DrawShadow(g, text, RegularFont, new RectangleF((width / 2) - (textSize.Width / 2) + 1, (height * 0.005f) + 1, 0, 0));
				DrawString(g, text, RegularFont, Color.White, new RectangleF((width / 2) - (textSize.Width / 2), (height * 0.005f), 0, 0));
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawInfo", "DisplayManager", ex.Message, ex.StackTrace);
			}
		}

		public void DrawScreenshot(Graphics g, int width, int height, bool showScreenshot, float alpha)
		{
			try
			{
				lock (this)
				{
					if (showScreenshot && Globals.ScreenshotList.Count > Settings.Display.Screen)
					{
						RectangleF srcRectF = new RectangleF(0, 0, Globals.ScreenshotList[Settings.Display.Screen].Width, Globals.ScreenshotList[Settings.Display.Screen].Height);
						Rectangle destRect = new Rectangle(0, 0, width, height);
						RectangleF destRectF = new RectangleF(-0.5f, -0.5f, width + 1, height + 1);

						if (alpha != 1.0f)
						{
							g.Clear(Color.Black);

							ColorMatrix colorMatrix = new ColorMatrix();
							colorMatrix.Matrix33 = alpha;

							using (ImageAttributes imageAttributes = new ImageAttributes())
							{
								imageAttributes.SetColorMatrix(colorMatrix);
								g.DrawImage(Globals.ScreenshotList[Settings.Display.Screen], destRect, srcRectF.X, srcRectF.Y, srcRectF.Width, srcRectF.Height, GraphicsUnit.Pixel, imageAttributes);
							}
						}
						else
							g.DrawImage(Globals.ScreenshotList[Settings.Display.Screen], destRectF, srcRectF, GraphicsUnit.Pixel);
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawScreenshot", "DisplayManager", ex.Message, ex.StackTrace);
			}
		}

		public void Paint(Bitmap bmp)
		{
			Paint(bmp, m_bakType, true, Settings.Display.ShowScreenshot, Settings.Display.Rotation, Settings.Display.FlipX, Settings.Display.FlipY);
		}

		public void Paint(Bitmap bmp, BakType bakType)
		{
			Paint(bmp, bakType, true, Settings.Display.ShowScreenshot, Settings.Display.Rotation, Settings.Display.FlipX, Settings.Display.FlipY);
		}

		public void PaintSub(Bitmap bmp)
		{
			PaintSub(bmp, true, Settings.Display.ShowScreenshot, Settings.Display.Rotation, Settings.Display.FlipX, Settings.Display.FlipY);
		}

		private ImageAttributes SetAlpha(float value)
		{
			ColorMatrix colorMatrix = new ColorMatrix(new float[][] {
			new float[] { 1.0f, 0.0f, 0.0f, 0.0f, 0.0f },
			new float[] { 0.0f, 1.0f, 0.0f, 0.0f, 0.0f },
			new float[] { 0.0f, 0.0f, 1.0f, 0.0f, 0.0f },
			new float[] { 0.0f, 0.0f, 0.0f, value, 0.0f },
			new float[] { 0.0f, 0.0f, 0.0f, 0.0f, 1.0f }
			});

			ImageAttributes imageAttr = new ImageAttributes();

			imageAttr.SetColorMatrix(colorMatrix);

			return imageAttr;
		}

		public void DrawBackground(Graphics g, BakType bakType, int width, int height, bool showBackground, bool showScreenshot, int displayRotation, bool flipX, bool flipY, float alpha)
		{
			//LogFile.WriteLine("DrawBG: {0}, {1}", showBackground, Globals.MenuJustShown);
			try
			{
				if (showBackground)
				{
					if (showScreenshot && Globals.ScreenshotList.Count > 0)
					{
						DrawScreenshot(g, width, height, true, alpha);

						RotateScreen(g, width, height, displayRotation, flipX, flipY);
					}
					else
					{
						RotateScreen(g, width, height, displayRotation, flipX, flipY);

						//g.Clear(Color.Black);
						DrawBackground(g, bakType, width, height, alpha);
					}
				}
				else
				{
					g.Clear(Color.FromArgb(0, 0, 0, 0));

					RotateScreen(g, width, height, displayRotation, flipX, flipY);
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawBackground", "LayoutManager", ex.Message, ex.StackTrace);
			}
		}

		public void Paint(Bitmap bmp, BakType bakType, bool showBackground, bool showScreenshot, int displayRotation, bool flipX, bool flipY)
		{
			try
			{
				using (Graphics g = Graphics.FromImage(bmp))
				{
					SetGraphicsQuality(g, UserGraphicsQuality());

					if (Globals.MainMenu.Visible)
					{
						Globals.DisplayMode = DisplayMode.MainMenu;

						if (Globals.MenuJustShown) //prevents opacity value doubling by overlay on each menu item change
						{
							DrawBackground(g, BakType.MainMenu, bmp.Width, bmp.Height, showBackground, showScreenshot, displayRotation, flipX, flipY, 0.5f);

							if (Settings.Display.ShowDropShadow)
								DrawShadow(g, new Rectangle((int)(bmp.Width / 4), (int)(bmp.Height / 4), (int)(bmp.Width / 2), (int)(bmp.Height / 2)));

                            var version = Assembly.GetExecutingAssembly().GetName().Version;

                            DrawInfo(g, String.Format("CPWizard {0} - By Ben Baker", version.ToString(3)), bmp.Width, bmp.Height);
							
							Globals.MenuJustShown = false;
						}
						
						Globals.MainMenu.Paint(g, (int)(bmp.Width / 4), (int)(bmp.Height / 4), (int)(bmp.Width / 2), (int)(bmp.Height / 2));

						//DrawLogo(destGraphics, Global.MainBitmap.Width, Global.MainBitmap.Height);
					}
					else if (Globals.LayoutManager.Visible)
					{
						Globals.DisplayMode = DisplayMode.Layout;

						DrawBackground(g, BakType.Default, bmp.Width, bmp.Height, showBackground, showScreenshot, displayRotation, flipX, flipY, 0.5f);

						Globals.LayoutManager.Paint(g, 0, 0, bmp.Width, bmp.Height, false);
					}
					else if (Globals.GameInfo.Visible)
					{
						Globals.DisplayMode = DisplayMode.GameInfo;

						//g.Clear(Color.Transparent);

						DrawBackground(g, BakType.Info, bmp.Width, bmp.Height, showBackground, false, displayRotation, flipX, flipY, 0.5f);

						Globals.GameInfo.Paint(g, 0, 0, bmp.Width, bmp.Height);

						DrawInfo(g, "Game Info", bmp.Width, bmp.Height);
					}
					else if (Globals.HistoryDat.Visible)
					{
						Globals.DisplayMode = DisplayMode.HistoryDat;

						DrawBackground(g, BakType.Info, bmp.Width, bmp.Height, showBackground, false, displayRotation, flipX, flipY, 0.5f);

						Globals.HistoryDat.Paint(g, 0, 0, bmp.Width, bmp.Height);

						DrawInfo(g, "History.Dat", bmp.Width, bmp.Height);
					}
					else if (Globals.MAMEInfoDat.Visible)
					{
						Globals.DisplayMode = DisplayMode.MAMEInfoDat;

						DrawBackground(g, BakType.Info, bmp.Width, bmp.Height, showBackground, false, displayRotation, flipX, flipY, 0.5f);

						Globals.MAMEInfoDat.Paint(g, 0, 0, bmp.Width, bmp.Height);

						DrawInfo(g, "MAMEInfo.Dat", bmp.Width, bmp.Height);
					}
					else if (Globals.CommandDat.Visible)
					{
						Globals.DisplayMode = DisplayMode.CommandDat;

						DrawBackground(g, BakType.Info, bmp.Width, bmp.Height, showBackground, false, displayRotation, flipX, flipY, 0.5f);

						Globals.CommandDat.Paint(g, 0, 0, bmp.Width, bmp.Height);

						DrawInfo(g, "Command.Dat", bmp.Width, bmp.Height);
					}
					else if (Globals.StoryDat.Visible)
					{
						Globals.DisplayMode = DisplayMode.StoryDat;

						DrawBackground(g, BakType.Info, bmp.Width, bmp.Height, showBackground, false, displayRotation, flipX, flipY, 0.5f);

						Globals.StoryDat.Paint(g, 0, 0, bmp.Width, bmp.Height);

						DrawInfo(g, "Story.Dat", bmp.Width, bmp.Height);
					}
					else if (Globals.HiToText.Visible)
					{
						Globals.DisplayMode = DisplayMode.HiToText;

						DrawBackground(g, BakType.Info, bmp.Width, bmp.Height, showBackground, false, displayRotation, flipX, flipY, 0.5f);

						Globals.HiToText.Paint(g, 0, 0, bmp.Width, bmp.Height);

						DrawInfo(g, "HiToText", bmp.Width, bmp.Height);
					}
					else if (Globals.ArtworkManager.Visible)
					{
						Globals.DisplayMode = DisplayMode.Artwork;

						DrawBackground(g, BakType.Info, bmp.Width, bmp.Height, showBackground, false, displayRotation, flipX, flipY, 0.5f);

						DrawInfo(g, "Artwork", bmp.Width, bmp.Height);

						Globals.ArtworkManager.Paint(g, 0, 0, bmp.Width, bmp.Height);
					}
					else if (Globals.MAMEManual.Visible)
					{
						Globals.DisplayMode = DisplayMode.Manual;

						DrawBackground(g, BakType.Info, bmp.Width, bmp.Height, showBackground, false, displayRotation, flipX, flipY, 0.5f);

						Globals.MAMEManual.Paint(g, 0, 0, bmp.Width, bmp.Height);
					}
					else if (Globals.EmulatorManual.Visible)
					{
						Globals.DisplayMode = DisplayMode.Manual;

						DrawBackground(g, BakType.Info, bmp.Width, bmp.Height, showBackground, false, displayRotation, flipX, flipY, 0.5f);

						Globals.EmulatorManual.Paint(g, 0, 0, bmp.Width, bmp.Height);
					}
					else if (Globals.EmulatorOpCard.Visible)
					{
						Globals.DisplayMode = DisplayMode.Manual;

						DrawBackground(g, BakType.Info, bmp.Width, bmp.Height, showBackground, false, displayRotation, flipX, flipY, 0.5f);

						Globals.EmulatorOpCard.Paint(g, 0, 0, bmp.Width, bmp.Height);
					}
					else if (Globals.NFOViewer.Visible)
					{
						Globals.DisplayMode = DisplayMode.Nfo;

						DrawBackground(g, BakType.Info, bmp.Width, bmp.Height, false, false, displayRotation, flipX, flipY, 0.5f);

						Globals.NFOViewer.Paint(g, 0, 0, bmp.Width, bmp.Height);
					}
					else if (Globals.IRC.Visible)
					{
						Globals.DisplayMode = DisplayMode.IRC;

						DrawBackground(g, BakType.Info, bmp.Width, bmp.Height, false, false, displayRotation, flipX, flipY, 0.5f);

						Globals.IRC.Paint(g, 0, 0, bmp.Width, bmp.Height);

						DrawInfo(g, "IRC", bmp.Width, bmp.Height);
					}
				}
			}
			catch (Exception ex)
			{
				Globals.DisplayMode = DisplayMode.MainMenu;

				LogFile.WriteLine("Paint", "DisplayManager", ex.Message, ex.StackTrace);
			}
		}

		public void PaintSub(Bitmap bmp, bool showBackground, bool showScreenshot, int displayRotation, bool flipX, bool flipY)
		{
			try
			{
				using (Graphics g = Graphics.FromImage(bmp))
				{
					DrawBackground(g, BakType.Default, bmp.Width, bmp.Height, showBackground, showScreenshot, displayRotation, flipX, flipY, 0.5f);

					Globals.LayoutManager.Paint(g, 0, 0, bmp.Width, bmp.Height, true);
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("PaintSub", "DisplayManager", ex.Message, ex.StackTrace);
			}
		}

		private Rectangle CalcUnion(List<LayoutObject> objArray)
		{
			Rectangle union = objArray[0].Rect;

			foreach (LayoutObject obj in objArray)
				union = Rectangle.Union(union, obj.Rect);

			return union;
		}

		private void OnPaint(object sender, PaintEventArgs e)
		{
			try
			{
				SetGraphicsQuality(e.Graphics, UserGraphicsQuality());

				//e.Graphics.ScaleTransform(Settings.Display.Scale, Settings.Display.Scale);

				//e.Graphics.Clear(Color.FromArgb(0, 0, 0, 0));

				if (Globals.LayoutManager != null)
					Globals.LayoutManager.DrawLayout(e.Graphics, ref Globals.Layout, (Globals.DisplayMode == DisplayMode.LayoutEditor));

				if (Globals.Layout != null)
				{
					if (Settings.Display.ShowGrid)
						DrawGrid(e.Graphics, Globals.Layout.Width, Globals.Layout.Height);

					DrawBoundingBox(e.Graphics, 0, 0, Globals.Layout.Width, Globals.Layout.Height, Color.Red);

					if (Globals.SelectedObjectList != null)
					{
						if (Globals.SelectedObjectList.Count > 0)
						{
							Rectangle unionRect = Globals.SelectedObjectList[0].Rect;

							foreach (LayoutObject obj in Globals.SelectedObjectList)
							{
								obj.DrawBoundingBox(e.Graphics);
								unionRect = Rectangle.Union(unionRect, obj.Rect);
							}

							DrawGuides(e.Graphics, unionRect, Globals.Layout.Width, Globals.Layout.Height);

							DrawBoundingBox(e.Graphics, unionRect.X, unionRect.Y, unionRect.Width, unionRect.Height, Color.Red);
						}
						else
							DrawMultiSelectBox(e.Graphics);
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("OnPaint", "DisplayManager", ex.Message, ex.StackTrace);
			}
		}

		public void DrawLabelArrow(Graphics g, Point pt1, Point pt2, int size, Color col)
		{
			try
			{
				Pen p = new Pen(col, size);
				Brush b = new SolidBrush(col);

				p.StartCap = LineCap.Round;
				p.EndCap = LineCap.ArrowAnchor;

				g.DrawLine(p, pt1, pt2);
				//g.FillEllipse(b, pt1.X - 4, pt1.Y - 4, 8, 8);
				//g.FillEllipse(b, pt2.X - 4, pt2.Y - 4, 8, 8);

				p.Dispose();
				b.Dispose();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawLabelLink", "DisplayManager", ex.Message, ex.StackTrace);
			}
		}

		public void DrawBoundingBox(Graphics g, int x, int y, int width, int height, Color col)
		{
			try
			{
				using (Pen p = new Pen(col))
				{
					p.DashPattern = new float[] { 4, 4 };
					g.DrawRectangle(p, x, y, width - 1, height - 1);
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawBoundingBox", "DisplayManager", ex.Message, ex.StackTrace);
			}
		}

		private void DrawGrid(Graphics g, int width, int height)
		{
			try
			{
				int i = 0;
				Size grid = Settings.Display.GridSize;

				if (grid.Width < 8 || grid.Height < 8)
					return;

				using (Pen p = new Pen(Color.FromArgb(64, Color.DarkGray)))
				{
					if (Globals.Layout != null)
					{
						for (i = 0; i < width; i += grid.Width)
							g.DrawLine(p, i, 0, i, height);
						for (i = 0; i < height; i += grid.Height)
							g.DrawLine(p, 0, i, width, i);
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawGrid", "DisplayManager", ex.Message, ex.StackTrace);
			}
		}

		private void DrawGuides(Graphics g, Rectangle rect, int width, int height)
		{
			try
			{
				g.DrawLine(Pens.Yellow, 0, rect.Y + (rect.Height / 2), width, rect.Y + (rect.Height / 2));
				g.DrawLine(Pens.Yellow, rect.X + (rect.Width / 2), 0, rect.X + (rect.Width / 2), height);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawGuides", "DisplayManager", ex.Message, ex.StackTrace);
			}
		}

		private void DrawCanvas(Graphics g)
		{
			try
			{
				Brush b = null;

				for (int y = 0; y < Globals.Layout.Height; y += 8)
				{
					for (int x = 0; x < Globals.Layout.Width; x += 8)
					{
						if (((int)x / 8) % 2 == 0)
						{
							if (((int)y / 8) % 2 == 0)
								b = Brushes.Silver;
							else
								b = Brushes.LightGray;
						}
						else
						{
							if (((int)y / 8) % 2 == 0)
								b = Brushes.LightGray;
							else
								b = Brushes.Silver;
						}
						g.FillRectangle(b, x, y, 8, 8);
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawCanvas", "DisplayManager", ex.Message, ex.StackTrace);
			}
		}

		public static Point SnapToGrid(Point p, Size snapSize)
		{
			int px, py;

			px = (int)(snapSize.Width * Math.Round((double)p.X / (double)snapSize.Width));
			py = (int)(snapSize.Height * Math.Round((double)p.Y / (double)snapSize.Height));

			return new Point(px, py);
		}

		private void DrawMultiSelectBox(Graphics g)
		{
			try
			{
				using (Pen p = new Pen(Color.Red))
				{
					p.DashPattern = new float[] { 4, 4 };
					g.DrawRectangle(p, MultiSelectRect);
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawMultiSelectBox", "DisplayManager", ex.Message, ex.StackTrace);
			}
		}

		private bool TryGetSelectedControl(int x, int y, out LayoutObject selectedLayoutObject)
		{
			selectedLayoutObject = null;

			if (Globals.Layout == null)
				return false;

			try
			{
				for (int i = Globals.Layout.LayoutObjectList.Count - 1; i >= 0; i--)
				{
					LayoutObject layoutObject = (LayoutObject)Globals.Layout.LayoutObjectList[i];
					CurrentTool = layoutObject.IsHit(x, y);

					if (CurrentTool != LayoutObject.MouseTools.NoTool)
					{
						selectedLayoutObject = layoutObject;
						return true;
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("TryGetSelectedControl", "DisplayManager", ex.Message, ex.StackTrace);
			}

			return false;
		}

		private void SetMouseCursor(int x, int y)
		{
			try
			{
				if (Globals.Layout != null)
				{
					if (Globals.SelectedObjectList.Count == 1)
					{
						LayoutObject.MouseTools CursorType = Globals.SelectedObjectList[0].IsHit(x, y);

						switch (CursorType)
						{
							case LayoutObject.MouseTools.NoTool:
								Cursor.Current = Cursors.Default;
								break;
							case LayoutObject.MouseTools.MoveObject:
								Cursor.Current = Cursors.SizeAll;
								break;
							case LayoutObject.MouseTools.SizeUp:
								Cursor.Current = Cursors.SizeNS;
								break;
							case LayoutObject.MouseTools.SizeUpLeft:
								Cursor.Current = Cursors.SizeNWSE;
								break;
							case LayoutObject.MouseTools.SizeUpRight:
								Cursor.Current = Cursors.SizeNESW;
								break;
							case LayoutObject.MouseTools.SizeDown:
								Cursor.Current = Cursors.SizeNS;
								break;
							case LayoutObject.MouseTools.SizeDownLeft:
								Cursor.Current = Cursors.SizeNESW;
								break;
							case LayoutObject.MouseTools.SizeDownRight:
								Cursor.Current = Cursors.SizeNWSE;
								break;
							case LayoutObject.MouseTools.SizeLeft:
								Cursor.Current = Cursors.SizeWE;
								break;
							case LayoutObject.MouseTools.SizeRight:
								Cursor.Current = Cursors.SizeWE;
								break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("SetMouseCursor", "DisplayManager", ex.Message, ex.StackTrace);
			}
		}

		public Point GetCenterDisplay()
		{
			try
			{
				return new Point(Globals.VisibleRect.X + Globals.VisibleRect.Width / 2, Globals.VisibleRect.Y + Globals.VisibleRect.Height / 2);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetCenterDisplay", "DisplayManager", ex.Message, ex.StackTrace);
			}

			return Point.Empty;
		}

		public void DrawWatermark(Bitmap bmp)
		{
			using (Graphics g = Graphics.FromImage(bmp))
				g.DrawImage(Globals.WatermarkBitmap, 1, 1);
		}

		public static DisplayManager.GraphicsQuality UserGraphicsQuality()
		{

			if (Settings.Display.UseHighQuality)
				return GraphicsQuality.High;
			else
				return GraphicsQuality.Low;
		}

		public Bitmap[] BakArray
		{
			get { return m_bakArray; }
		}

		#region IDisposable Members

		public void Dispose()
		{
		}

		#endregion
	}
}
