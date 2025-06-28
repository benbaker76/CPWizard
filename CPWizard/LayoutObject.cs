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
	public abstract class LayoutObject : IDisposable, IComparable, ICloneable
	{
		public enum MouseTools
		{
			NoTool,
			MoveObject,
			SizeUp,
			SizeUpLeft,
			SizeUpRight,
			SizeDown,
			SizeDownLeft,
			SizeDownRight,
			SizeLeft,
			SizeRight
		};

		public enum ObjectType { Image, Label };
		public string Name = null;
		public Point StartPoint = Point.Empty;
		public Rectangle Rect = Rectangle.Empty;
		public ObjectType Type = ObjectType.Image;
		public int Rotation = 0;
		public bool Sizeable = false;

		public LayoutObject(string name, Rectangle rect, bool sizeable, ObjectType type)
		{
			Name = name;
			Rect = rect;
			Type = type;
			Sizeable = sizeable;
		}

		public abstract void Render(Graphics g, bool drawPlaceHolder);

		/* public virtual bool IsHit(int x, int y)
		{
			return Location.Contains(x, y);
		} */

		public virtual MouseTools IsHit(int xp, int yp)
		{
			if (Sizeable)
			{
				if (xp >= Rect.X && xp <= Rect.X + Rect.Width && yp >= Rect.Y && yp <= Rect.Y + Rect.Height)
				{
					if (xp >= Rect.X + 4 && xp <= Rect.X + Rect.Width - 4 && yp >= Rect.Y + 4 && yp <= Rect.Y + Rect.Height - 4)
						return MouseTools.MoveObject;
					else
					{
						if (yp >= Rect.Y + Rect.Height - 4)
						{
							if (xp >= Rect.X + 4 && xp <= Rect.X + Rect.Width - 4)
								return MouseTools.SizeDown;
							else if (xp <= Rect.X + 4)
								return MouseTools.SizeDownLeft;
							else if (xp >= Rect.X + Rect.Width - 4)
								return MouseTools.SizeDownRight;
						}
						if (yp <= Rect.Y + 4)
						{
							if (xp >= Rect.X + 4 && xp <= Rect.X + Rect.Width - 4)
								return MouseTools.SizeUp;
							else if (xp <= Rect.X + 4)
								return MouseTools.SizeUpLeft;
							else if (xp >= Rect.X + Rect.Width - 4)
								return MouseTools.SizeUpRight;
						}
						if (xp >= Rect.X + Rect.Width - 4)
						{
							if (yp >= Rect.Y + 4 && yp <= Rect.Y + Rect.Height - 4)
								return MouseTools.SizeRight;
							else if (yp <= Rect.Y + 4)
								return MouseTools.SizeUpRight;
							else if (yp >= Rect.Y + Rect.Height - 4)
								return (MouseTools.SizeDownRight);
						}
						if (xp <= Rect.X + 4)
						{
							if (yp >= Rect.Y + 4 && yp <= Rect.Y + Rect.Height - 4)
								return MouseTools.SizeLeft;
							else if (yp <= Rect.Y + 4)
								return MouseTools.SizeUpLeft;
							else if (yp >= Rect.Y + Rect.Height - 4)
								return MouseTools.SizeDownRight;
						}
					}
				}
			}
			else
			{
				if (Rect.Contains(xp, yp))
					return MouseTools.MoveObject;
			}
			return MouseTools.NoTool;
		}

		public bool Move(int x, int y)
		{
			bool LocationChanged = false;

			if (Rect.X != x || Rect.Y != y)
				LocationChanged = true;

			Rect.X = x;
			Rect.Y = y;

			return LocationChanged;
		}

		public virtual bool Size(LayoutObject.MouseTools tool, int x, int y)
		{
			bool locationChanged = false;

			if (x != 0 || y != 0)
				locationChanged = true;

			Point Offset = new Point(Rect.X - x, Rect.Y - y);

			switch (tool)
			{
				case LayoutObject.MouseTools.SizeUp:
					Rect.Y -= Offset.Y;
					Rect.Height += Offset.Y;
					break;
				case LayoutObject.MouseTools.SizeUpLeft:
					Rect.X -= Offset.X;
					Rect.Y -= Offset.Y;
					Rect.Width += Offset.X;
					Rect.Height += Offset.Y;
					break;
				case LayoutObject.MouseTools.SizeUpRight:
					Rect.Y -= Offset.Y;
					Rect.Width = -Offset.X;
					Rect.Height += Offset.Y;
					break;
				case LayoutObject.MouseTools.SizeDown:
					Rect.Height = -Offset.Y;
					break;
				case LayoutObject.MouseTools.SizeDownLeft:
					Rect.X -= Offset.X;
					Rect.Width += Offset.X;
					Rect.Height = -Offset.Y;
					break;
				case LayoutObject.MouseTools.SizeDownRight:
					Rect.Width = -Offset.X;
					Rect.Height = -Offset.Y;
					break;
				case LayoutObject.MouseTools.SizeLeft:
					Rect.X -= Offset.X;
					Rect.Width += Offset.X;
					break;
				case LayoutObject.MouseTools.SizeRight:
					Rect.Width = -Offset.X;
					break;
			}

			if (Rect.Width < 8)
				Rect.Width = 8;
			if (Rect.Height < 8)
				Rect.Height = 8;

			return locationChanged;
		}

		public void DrawPlaceHolder(Graphics g)
		{
			try
			{
				using (Pen p = new Pen(Color.White))
				{
					g.DrawRectangle(p, Rect.X, Rect.Y, Rect.Width, Rect.Height);
					g.DrawLine(p, Rect.X, Rect.Y, Rect.X + Rect.Width, Rect.Y + Rect.Height);
					g.DrawLine(p, Rect.X + Rect.Width, Rect.Y, Rect.X, Rect.Y + Rect.Height);
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawPlaceHolder", "Object", ex.Message, ex.StackTrace);
			}
		}

		public void DrawSizeHandles(Graphics g)
		{
			try
			{
				using (SolidBrush sb = new SolidBrush(Color.Red))
				{
					g.FillRectangle(sb, Rect.X, Rect.Y, 4, 4);
					g.FillRectangle(sb, Rect.X + (Rect.Width / 2) - 2, Rect.Y, 4, 4);
					g.FillRectangle(sb, Rect.X + Rect.Width - 4, Rect.Y, 4, 4);
					g.FillRectangle(sb, Rect.X, Rect.Y + (Rect.Height / 2) - 2, 4, 4);
					g.FillRectangle(sb, Rect.X + Rect.Width - 4, (Rect.Y + (Rect.Height / 2) - 2), 4, 4);
					g.FillRectangle(sb, Rect.X, Rect.Y + Rect.Height - 4, 4, 4);
					g.FillRectangle(sb, Rect.X + (Rect.Width / 2) - 2, Rect.Y + Rect.Height - 4, 4, 4);
					g.FillRectangle(sb, Rect.X + Rect.Width - 4, Rect.Y + Rect.Height - 4, 4, 4);
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawSizeHandles", "Object", ex.Message, ex.StackTrace);
			}
		}

		public void DrawBoundingBox(Graphics g)
		{
			try
			{
				using (Pen p = new Pen(Color.Red))
				{
					p.DashPattern = new float[] { 4, 4 };
					g.DrawRectangle(p, Rect);

					if (Sizeable)
						DrawSizeHandles(g);
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawBoundingBox", "Object", ex.Message, ex.StackTrace);
			}
		}

		public void AlignLeft()
		{
			Rect.X = 0;
		}

		public void HorizontalCenter(int maxWidth)
		{
			Rect.X = (int)(((float)maxWidth / 2f) - ((float)Rect.Width / 2f));
		}

		public void AlignRight(int maxWidth)
		{
			Rect.X = maxWidth - Rect.Width;
		}

		public void AlignTop()
		{
			Rect.Y = 0;
		}

		public void VerticalCenter(int maxHeight)
		{
			Rect.Y = (int)(((float)maxHeight / 2f) - ((float)Rect.Height / 2f));
		}

		public void AlignBotton(int maxHeight)
		{
			Rect.Y = maxHeight - Rect.Height;
		}

		public Point Left
		{
			get { return new Point(Rect.X, Rect.Y + (Rect.Height / 2)); }
		}

		public Point Right
		{
			get { return new Point(Rect.X + Rect.Width, Rect.Y + (Rect.Height / 2)); }
		}

		public Point Center
		{
			get { return new Point(Rect.X + (Rect.Width / 2), Rect.Y + (Rect.Height / 2)); }
		}

		#region IComparable Members

		public int CompareTo(object obj)
		{
			LayoutObject temp = (LayoutObject)obj;

			/* foreach (LayoutObject layoutObject in Global.SelectedObjects)
				if (this == layoutObject)
					return 1;

			foreach (LayoutObject layoutObject in Global.SelectedObjects)
				if (temp == layoutObject)
					return -1; */

			if (this.Type == ObjectType.Label && temp.Type == ObjectType.Image)
				return 1;

			if (this.Type == ObjectType.Image && temp.Type == ObjectType.Label)
				return -1;

			return 0;
		}

		#endregion

		#region ICloneable Members

		public object Clone()
		{
			LayoutObject clone = CreateInstance();
			CopyTo(clone);
			return clone;
		}

		protected abstract LayoutObject CreateInstance();

		protected virtual void CopyTo(LayoutObject target)
		{
			target.Name = this.Name;
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			DisposeObjects();
		}

		protected abstract void DisposeObjects();

		#endregion
	}
}
