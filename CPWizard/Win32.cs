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

namespace CPWizard
{
	partial class Win32
	{
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetWindowRect(HandleRef hWnd, out RECT lpRect);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

		[DllImport("kernel32.dll")]
		public static extern void OutputDebugString(string lpOutputString);

		/* [DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool PeekMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);

		[DllImport("user32.dll")]
		public static extern bool TranslateMessage([In] ref MSG lpMsg);

		[DllImport("user32.dll")]
		public static extern IntPtr DispatchMessage([In] ref MSG lpmsg); */

		[DllImport("kernel32.dll")]
		public static extern uint GetCurrentThreadId();

		[DllImport("user32.dll", SetLastError = true)]
		public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

		[return: MarshalAs(UnmanagedType.Bool)]
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool PostThreadMessage(uint threadId, uint msg, UIntPtr wParam, IntPtr lParam);

		public const int PM_NOREMOVE = 0x0000;

		public const int WM_CLOSE = 0x10;
		public const int WM_MOVE = 0x3;
		public const int WM_SIZE = 0x5;
		public const int WM_SYSCOMMAND = 0x112;
		public const int WM_PAINT = 0x000F;
		public const int WM_NCPAINT = 0x0085;
		public const int WM_ENTERSIZEMOVE = 0x0231;
		public const int WM_EXITSIZEMOVE = 0x0232;
		public const int WM_EXITMENULOOP = 0x0212;
		public const int WM_SIZING = 0x0214;
		public const int WM_USER_REDRAW = (WM_USER + 2);
		public const int WM_USER_SET_FULLSCREEN = (WM_USER + 3);
		public const int WM_USER_SET_MAXSIZE = (WM_USER + 4);
		public const int WM_USER_SET_MINSIZE = (WM_USER + 5);
		public const int WM_USER_UI_TEMP_PAUSE = (WM_USER + 6);

		public static IntPtr MakeLParam(int LoWord, int HiWord)
		{
			return new IntPtr((HiWord << 16) | (LoWord & 0xffff));
		}

		public static Rectangle GetWindowRectangle(HandleRef hWnd)
		{
			RECT rect;

			GetWindowRect(hWnd, out rect);

			return new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
		}

		public static void ClearMessageQueue()
		{
			Win32.MSG msg;

			while (Win32.PeekMessage(out msg, IntPtr.Zero, 0, 0, Win32.PM_NOREMOVE)) // Check for messages
			{
				if (Win32.GetMessage(out msg, IntPtr.Zero, 0, 0)) // Get the message
				{
					Win32.TranslateMessage(ref msg); // Translate
					Win32.DispatchMessage(ref msg); // Dispatch
				}
			}
		}
	}
}
