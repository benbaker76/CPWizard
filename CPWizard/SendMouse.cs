// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;

namespace CPWizard
{
	public class SendMouse
	{
		public static void DoMouseClick(int x, int y)
		{
			Win32.mouse_event((int)Win32.MouseEventFlags.LEFTDOWN | (int)Win32.MouseEventFlags.LEFTUP, x, y, 0, 0);
		}

		public static void SetLeftMouseButton(Point p)
		{
			Win32.mouse_event((int)Win32.MouseEventFlags.LEFTDOWN, p.X, p.Y, 0, 0);
			Win32.mouse_event((int)Win32.MouseEventFlags.LEFTUP, p.X, p.Y, 0, 0);
		}

		public static void SetLeftMouseButtonDown(Point p)
		{
			Win32.mouse_event((int)Win32.MouseEventFlags.LEFTDOWN, p.X, p.Y, 0, 0);
		}

		public static void SetLeftMouseButtonUp(Point p)
		{
			Win32.mouse_event((int)Win32.MouseEventFlags.LEFTUP, p.X, p.Y, 0, 0);
		}

		public static void SetRightMouseButton(Point p)
		{
			Win32.mouse_event((int)Win32.MouseEventFlags.RIGHTDOWN, p.X, p.Y, 0, 0);
			Win32.mouse_event((int)Win32.MouseEventFlags.RIGHTUP, p.X, p.Y, 0, 0);
		}

		public static void SetRightMouseButtonDown(Point p)
		{
			Win32.mouse_event((int)Win32.MouseEventFlags.RIGHTDOWN, p.X, p.Y, 0, 0);
		}

		public static void SetRightMouseButtonUp(Point p)
		{
			Win32.mouse_event((int)Win32.MouseEventFlags.RIGHTUP, p.X, p.Y, 0, 0);
		}

		public static void SetMousePos(Point p)
		{
			Win32.SetCursorPos(p.X, p.Y);
		}

		public static Point GetMousePos()
		{
			Point p = Point.Empty;
			Win32.GetCursorPos(out p);

			return p;
		}

		public static void UnmanagedMoveCursorOverButton(Button button)
		{
			IntPtr handle = IntPtr.Zero;
			Point point;
			int x = 0;
			int y = 0;
			int width = 0;
			int height = 0;
			bool coordinatesFound = false;

			if (button != null)
			{
				width = button.Size.Width;
				height = button.Size.Height;
				handle = button.Handle;
			}

			if ((width > 0) && (height > 0))
			{
				point = new Point();
				x = (width / 2);
				y = (height / 2);

				coordinatesFound = Win32.ClientToScreen(handle, ref point);

				if (coordinatesFound == true)
					Win32.SetCursorPos(point.X + x, point.Y + y);
			}
		}

		public static void ManagedMoveCursorOverButton(Button button)
		{
			IntPtr handle = IntPtr.Zero;
			Point point;
			int x = 0;
			int y = 0;
			int width = 0;
			int height = 0;
			bool coordinatesFound = false;

			if (button != null)
			{
				width = button.Size.Width;
				height = button.Size.Height;
				handle = button.Handle;
			}

			if ((width > 0) && (height > 0))
			{
				point = new Point();
				x = (width / 2);
				y = (height / 2);

				coordinatesFound = Win32.ClientToScreen(handle, ref point);

				if (coordinatesFound == true)
				{
					point.X += x;
					point.Y += y;
					System.Windows.Forms.Cursor.Position = point;
				}
			}
		}
	}

	public partial class Win32
	{
		[Flags]
		public enum MouseEventFlags
		{
			LEFTDOWN = 0x00000002,
			LEFTUP = 0x00000004,
			MIDDLEDOWN = 0x00000020,
			MIDDLEUP = 0x00000040,
			MOVE = 0x00000001,
			ABSOLUTE = 0x00008000,
			RIGHTDOWN = 0x00000008,
			RIGHTUP = 0x00000010
		}

		[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
		public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);

		[DllImport("user32.dll")]
		public static extern bool SetCursorPos(int X, int Y);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetCursorPos(out Point lpPoint);

		[DllImport("user32.dll")]
		public static extern bool ClientToScreen(IntPtr hwnd, ref Point lpPoint);
	}
}
