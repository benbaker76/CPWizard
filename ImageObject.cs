// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CPWizard
{
	public class ImageObject : LayoutObject
	{
		public Bitmap m_bitmap = null;
		public float Alpha = 1;
		public float Hue = 0;
		public float Saturation = 1;
		public float Brightness = 1;
		public bool AlphaFade = true;
		public int AlphaValue = 10;
		public string LabelLink = "";
		public bool TempImage = false;

		private Font m_font = null;

		public ImageObject(string name, Rectangle location, bool sizeable, string labelLink, bool alphaFade, int alphaValue)
			: base(name, location, sizeable, ObjectType.Image)
		{
			LabelLink = labelLink;
			AlphaFade = alphaFade;
			AlphaValue = alphaValue;

			m_font = new Font("Arial", 12, FontStyle.Bold);
		}

		public ColorMatrix GetAlphaMatrix(float value)
		{
			ColorMatrix colorMatrix = new ColorMatrix(new float[][] { 
            new float[] { 1.0f, 0.0f, 0.0f, 0.0f, 0.0f },
            new float[] { 0.0f, 1.0f, 0.0f, 0.0f, 0.0f },
            new float[] { 0.0f, 0.0f, 1.0f, 0.0f, 0.0f },
            new float[] { 0.0f, 0.0f, 0.0f, value, 0.0f },
            new float[] { 0.0f, 0.0f, 0.0f, 0.0f, 1.0f }
            });

			return colorMatrix;
		}

		private void DrawOverlay(Graphics g)
		{
			using (StringFormat stringFormat = new StringFormat(StringFormatFlags.NoWrap))
			{
				stringFormat.Alignment = StringAlignment.Near;

				g.DrawString(Name, m_font, Brushes.White, Rect, stringFormat);
			}
		}

		public override void Render(Graphics g, bool drawPlaceHolder)
		{
			try
			{
				if (Bitmap == null)
				{
					if (drawPlaceHolder)
					{
						DrawPlaceHolder(g);

						if (Name.StartsWith("[IMAGE_"))
							DrawOverlay(g);
					}

					return;
				}

				if (Hue != 0f || Saturation != 1f || Brightness != 1f || Alpha != 1f)
				{
					using (ImageAttributes imageAttr = new ImageAttributes())
					{
						ColorMatrix colorMatrix = new ColorMatrix();

						if (Hue != 0f)
							ColorProcessing.HueRotateMatrix(ref colorMatrix, Hue);
						if (Saturation != 1f)
							ColorProcessing.SaturateMatrix(ref colorMatrix, Saturation);
						if (Brightness != 1f)
							ColorProcessing.ColourScaleMatrix(ref colorMatrix, Brightness, Brightness, Brightness);

						if (Alpha != 1f)
						{
							ColorMatrix alphaMatrix = GetAlphaMatrix(Alpha);
							colorMatrix = ColorProcessing.Multiply(alphaMatrix, colorMatrix);
						}

						imageAttr.SetColorMatrix(colorMatrix);

						g.DrawImage(Bitmap, Rect, 0, 0, Bitmap.Width, Bitmap.Height, GraphicsUnit.Pixel, imageAttr);
					}
				}
				else
					g.DrawImage(Bitmap, Rect, 0, 0, Bitmap.Width, Bitmap.Height, GraphicsUnit.Pixel);

				if (drawPlaceHolder && Name.StartsWith("[IMAGE_"))
				{
					DrawPlaceHolder(g);
					DrawOverlay(g);
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("Render", "ImageObject", ex.Message, ex.StackTrace);
			}
		}

		private bool ThumbnailCallback()
		{
			return true;
		}

		public override MouseTools IsHit(int xp, int yp)
		{
			MouseTools MouseTool = base.IsHit(xp, yp);

			if (MouseTool == MouseTools.MoveObject)
			{
				if (Bitmap != null && !Name.StartsWith("[IMAGE_"))
				{
					Color color = GetPixel(xp - Rect.X, yp - Rect.Y);

					if (color.A == 0)
						return MouseTools.NoTool;
				}

				return MouseTools.MoveObject;
			}

			return MouseTool;
		}

		private Color GetPixel(int x, int y)
		{
			try
			{
				Rectangle OldLocation = Rect;
				Color col = Color.Empty;
				using (Bitmap bmp = new Bitmap(Rect.Width, Rect.Height))
				{
					using (Graphics g = Graphics.FromImage(bmp))
					{
						Rect.X = 0;
						Rect.Y = 0;

						Render(g, false);

						col = bmp.GetPixel(x, y);
					}
				}

				Rect = OldLocation;

				return col;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetPixel", "ImageObject", ex.Message, ex.StackTrace);
			}

			return Color.Empty;
		}

		/* public override bool IsHit(int x, int y)
		{
			if (Location.Contains(x, y))
			{
				if (Bitmap != null)
				{
					Color color = Bitmap.GetPixel(x - Location.X, y - Location.Y);
					if (color.A == 255)
						return true;
				}
				else
					return true;
			}

			return false;
		} */

		protected override LayoutObject CreateInstance()
		{
			return new ImageObject(this.Name, this.Rect, this.Sizeable, this.LabelLink, this.AlphaFade, this.AlphaValue);
		}

		protected override void CopyTo(LayoutObject target)
		{
			base.CopyTo(target);

			ImageObject targetObject = (ImageObject)target;
			if (this.Bitmap != null)
				targetObject.Bitmap = (Bitmap)this.Bitmap.Clone();
			targetObject.Hue = this.Hue;
			targetObject.Saturation = this.Saturation;
			targetObject.Brightness = this.Brightness;
			targetObject.AlphaFade = this.AlphaFade;
			targetObject.AlphaValue = this.AlphaValue;
		}

		protected override void DisposeObjects()
		{
			if (m_bitmap != null)
			{
				m_bitmap.Dispose();
				m_bitmap = null;
			}

			if (m_font != null)
			{
				m_font.Dispose();
				m_font = null;
			}
		}

		public Bitmap Bitmap
		{
			get { return m_bitmap; }
			set
			{
				if (m_bitmap != null)
				{
					m_bitmap.Dispose();
					m_bitmap = null;
				}

				m_bitmap = value;
			}
		}
	}
}
