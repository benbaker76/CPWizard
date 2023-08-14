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
	public class LabelObject : LayoutObject
	{
		//public enum TextAlignType { Left, Right, Center };
		//public enum TextStyleType { Outline, Shadow };
		//public enum TextAngleType { 0, 90, 180, 270 }

		public string Group = null;
		public List<LabelCode> Codes = null;
		public string TextAlign = "Center";
		public string TextStyle = "Outline";
		public bool Arrow = true;
		public bool Spot = true;
		public string m_text = null;
		public Color Color = Color.White;

		private Font m_font = new Font("Arial", 12, FontStyle.Bold);

		public LabelObject(string name, string group, Rectangle location, bool sizeable, bool arrow, bool spot)
			: base(name, location, sizeable, ObjectType.Label)
		{
			Text = name;
			Group = group;
			Arrow = arrow;
			Spot = spot;

			Codes = new List<LabelCode>();
		}

		private StringFormat GetStringFormat()
		{
			StringFormat stringFormat = new StringFormat(StringFormatFlags.NoClip);

			switch (TextAlign)
			{
				case "Left":
					stringFormat.Alignment = StringAlignment.Near;
					break;
				case "Right":
					stringFormat.Alignment = StringAlignment.Far;
					break;
				case "Center":
					stringFormat.Alignment = StringAlignment.Center;
					break;
			}

			if (Sizeable)
				stringFormat.Trimming = StringTrimming.Character;
			else
				stringFormat.Trimming = StringTrimming.None;

			return stringFormat;
		}

		private void DrawOutline(Graphics g)
		{
			if (String.IsNullOrEmpty(Text))
				return;

			try
			{
				using (Brush b = new SolidBrush(Color.Black))
				{
					using (StringFormat stringFormat = GetStringFormat())
					{
						g.DrawString(Text, Font, b, new RectangleF(Rect.X - 1, Rect.Y - 1, Rect.Width, Rect.Height), stringFormat);
						g.DrawString(Text, Font, b, new RectangleF(Rect.X + 1, Rect.Y + 1, Rect.Width, Rect.Height), stringFormat);
						g.DrawString(Text, Font, b, new RectangleF(Rect.X + 1, Rect.Y - 1, Rect.Width, Rect.Height), stringFormat);
						g.DrawString(Text, Font, b, new RectangleF(Rect.X - 1, Rect.Y + 1, Rect.Width, Rect.Height), stringFormat);
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawOutline", "LabelObject", ex.Message, ex.StackTrace);
			}
		}

		private void DrawOutline(Graphics g, int outlineSize)
		{
			if (String.IsNullOrEmpty(Text))
				return;

			try
			{
				g.SmoothingMode = SmoothingMode.HighQuality;

				using (Pen p = new Pen(new SolidBrush(Settings.Display.LabelOutlineColor), outlineSize))
				{
					p.LineJoin = LineJoin.Round;
					p.Alignment = PenAlignment.Center;

					using (StringFormat stringFormat = GetStringFormat())
					{
						using (GraphicsPath graphicsPath = new GraphicsPath())
						{
							graphicsPath.AddString(Text, Font.FontFamily, (int)Font.Style, g.DpiY * Font.Size / 72, new RectangleF(Rect.X, Rect.Y, Rect.Width, Rect.Height), stringFormat);

							g.DrawPath(p, graphicsPath);
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawOutline", "LabelObject", ex.Message, ex.StackTrace);
			}
		}

		private void DrawShadow(Graphics g)
		{
			if (String.IsNullOrEmpty(Text))
				return;

			try
			{
				using (Brush b = new SolidBrush(Color.Black))
				{
					using (StringFormat stringFormat = GetStringFormat())
						g.DrawString(Text, Font, b, new RectangleF(Rect.X + 1, Rect.Y + 1, Rect.Width, Rect.Height), stringFormat);
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawShadow", "LabelObject", ex.Message, ex.StackTrace);
			}
		}

		private void DrawString(Graphics g)
		{
			if (String.IsNullOrEmpty(Text))
				return;

			try
			{
				using (Brush b = new SolidBrush(Color))
				{
					using (StringFormat stringFormat = GetStringFormat())
						g.DrawString(Text, Font, b, Rect, stringFormat);
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawString", "LabelObject", ex.Message, ex.StackTrace);
			}
		}

		public void DrawLabelSpot(Graphics g, int size, Color color)
		{
			using (Brush b = new SolidBrush(color))
			{
				switch (TextAlign)
				{
					case "Left":
						g.FillEllipse(b, Left.X - (size / 2), Left.Y - (size / 2), size, size);
						break;
					case "Right":
						g.FillEllipse(b, Right.X - (size / 2), Right.Y - (size / 2), size, size);
						break;
					case "Center":
						g.FillEllipse(b, Center.X - (size / 2), Center.Y - (size / 2), size, size);
						break;
				}
			}
		}

		private void DrawRotatedString(Graphics g)
		{
			try
			{
				Matrix matrix = new Matrix();

				switch (TextAlign)
				{
					case "Left":
						matrix.RotateAt(Rotation, Left);
						break;
					case "Right":
						matrix.RotateAt(Rotation, Right);
						break;
					case "Center":
						matrix.RotateAt(Rotation, Center);
						break;
				}

				g.Transform = matrix;

				switch (TextStyle)
				{
					case "Outline":
						using (Brush b = new SolidBrush(Color.Black))
						{
							g.DrawString(Text, Font, b, -1, -1);
							g.DrawString(Text, Font, b, +1, +1);
							g.DrawString(Text, Font, b, +1, -1);
							g.DrawString(Text, Font, b, -1, +1);
						}
						break;
					case "Shadow":
						using (Brush b = new SolidBrush(Color.Black))
							g.DrawString(Text, Font, b, +1, +1);
						break;
				}

				using (Brush b = new SolidBrush(Color))
					g.DrawString(Text, Font, b, 0, 0);

				g.Transform = null;
				g.ResetTransform();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawRotatedString", "LabelObject", ex.Message, ex.StackTrace);
			}
		}

		public override void Render(Graphics g, bool drawPlaceHolder)
		{
			try
			{
				if (Rotation != 0)
					DrawRotatedString(g);
				else
				{
					if (TextStyle == "Outline")
						DrawOutline(g, Settings.Display.LabelOutlineSize);
					else
						DrawShadow(g);

					DrawString(g);
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("Render", "LabelObject", ex.Message, ex.StackTrace);
			}
		}

		private Size GetTextSize()
		{
			try
			{
				int TextWidth = 0;
				Size textSize;

				if (Text != null)
				{
					using (StringFormat stringFormat = GetStringFormat())
					{
						using (Bitmap bmp = new Bitmap(1, 1))
						{
							using (Graphics g = Graphics.FromImage(bmp))
							{
								if (Sizeable)
									TextWidth = Rect.Width;

								SizeF size = g.MeasureString(Text, Font, TextWidth, stringFormat);
								textSize = new Size((int)Math.Ceiling(size.Width), (int)Math.Ceiling(size.Height));
							}
						}
					}

					return textSize;
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetTextSize", "LabelObject", ex.Message, ex.StackTrace);
			}

			return new Size();
		}

		private void SetSize()
		{
			if (!Sizeable)
			{
				Size textSize = GetTextSize();

				switch (TextAlign)
				{
					case "Center":
						Rect.X = Rect.X + (Rect.Width / 2) - (textSize.Width / 2);
						break;
					case "Left":
						break;
					case "Right":
						Rect.X = Rect.X + Rect.Width - textSize.Width;
						break;
				}
				Rect.Width = textSize.Width;
				Rect.Height = textSize.Height;
			}
		}

		public override bool Size(LayoutObject.MouseTools tool, int x, int y)
		{
			bool retVal = base.Size(tool, x, y);

			SetSize();

			//Size textSize = GetTextSize();

			//if (Location.Width < textSize.Width)
			//    Location.Width = textSize.Width;
			//if (Location.Height < textSize.Height)
			//    Location.Height = textSize.Height;

			return retVal;
		}

		protected override LayoutObject CreateInstance()
		{
			return new LabelObject(this.Name, this.Group, this.Rect, this.Sizeable, this.Arrow, this.Spot);
		}

		protected override void CopyTo(LayoutObject target)
		{
			base.CopyTo(target);

			LabelObject targetLabel = (LabelObject)target;
			targetLabel.Group = this.Group;
			targetLabel.TextAlign = this.TextAlign;
			targetLabel.TextStyle = this.TextStyle;
			targetLabel.Arrow = this.Arrow;
			targetLabel.Spot = this.Spot;
			targetLabel.Text = this.Text;
			targetLabel.Color = this.Color;
			targetLabel.Font = (Font)this.Font.Clone();

			foreach (LabelCode code in Codes)
				targetLabel.Codes.Add(new LabelCode(code.Type, code.Value));
		}

		protected override void DisposeObjects()
		{
			if (m_font != null)
			{
				m_font.Dispose();
				m_font = null;
			}
		}

		public void RestoreText()
		{
			Text = Name;
		}

		public string Text
		{
			get { return m_text; }
			set { m_text = value; SetSize(); }
		}

		public Font Font
		{
			get { return m_font; }
			set { m_font = value; SetSize(); }
		}
	}

	public class LabelCode
	{

		public string Type = null;
		public string Value = null;

		public LabelCode(string type, string value)
		{
			Type = type;
			Value = value;
		}
	}
}
