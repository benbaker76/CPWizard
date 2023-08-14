// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;

namespace CPWizard
{
	partial class Win32
	{
		public const int SRCCOPY = 13369376;

		[DllImport("gdi32.dll", EntryPoint = "BitBlt")]
		public static extern bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest, IntPtr hdcSource, int xSrc, int ySrc, int RasterOp);

		[DllImport("user32.dll", EntryPoint = "GetDC")]
		public static extern IntPtr GetDC(IntPtr hWnd);

		[DllImport("user32.dll", EntryPoint = "ReleaseDC")]
		public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);

		[DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
		public static extern IntPtr DeleteDC(IntPtr hDc);

		[DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
		public static extern IntPtr DeleteObject(IntPtr hDc);

		[DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap")]
		public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

		[DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC")]
		public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

		[DllImport("gdi32.dll", EntryPoint = "SelectObject")]
		public static extern IntPtr SelectObject(IntPtr hdc, IntPtr bmp);

		[StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;
		}

		[DllImport("user32.dll")]
		public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);
	}

	class ScreenShotNode
	{
		public Rectangle Rect;
		public Bitmap Bitmap = null;

		public ScreenShotNode(Rectangle rect, Bitmap bmp)
		{
			Rect = rect;
			Bitmap = bmp;
		}
	}

	class WindowScreenshot
	{
		public static Size GetClientSize(IntPtr WindowHandle)
		{
			Win32.RECT rect = new Win32.RECT();
			Win32.GetClientRect(WindowHandle, out rect);
			return new Size(rect.Right - rect.Left, rect.Bottom - rect.Top);
		}

		public static Rectangle GetClientRect(IntPtr WindowHandle)
		{
			Win32.RECT rect = new Win32.RECT();
			Win32.GetClientRect(WindowHandle, out rect);
			return new Rectangle(rect.Top, rect.Left, rect.Right - rect.Left, rect.Bottom - rect.Top);
		}

		public static Bitmap TakeWindowScreenshot(IntPtr windowHandle)
		{
			try
			{
				Bitmap bmp = null;

				Size clientSize = GetClientSize(windowHandle);

				// get device context of the window...
				IntPtr hdcFrom = Win32.GetDC(windowHandle);

				// create dc that we can draw to...
				IntPtr hdcTo = Win32.CreateCompatibleDC(hdcFrom);
				IntPtr ptrBitmap = Win32.CreateCompatibleBitmap(hdcFrom, clientSize.Width, clientSize.Height);

				//  validate...
				if (!ptrBitmap.Equals(IntPtr.Zero))
				{
					// copy...
					IntPtr hcdLocal = (IntPtr)(Win32.SelectObject(hdcTo, ptrBitmap));
					Win32.BitBlt(hdcTo, 0, 0, clientSize.Width, clientSize.Height, hdcFrom, 0, 0, Win32.SRCCOPY);
					Win32.SelectObject(hdcTo, hcdLocal);
					//  create bitmap for window image...
					bmp = System.Drawing.Image.FromHbitmap(ptrBitmap);
				}

				//  release ...
				Win32.ReleaseDC(windowHandle, hdcFrom);
				Win32.DeleteDC(hdcTo);
				Win32.DeleteObject(ptrBitmap);

				//  return...
				return bmp;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("TakeWindowScreenshot", "WindowScreenshot", ex.Message, ex.StackTrace);
			}

			return null;
		}
	}
}
