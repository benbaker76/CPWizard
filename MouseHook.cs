// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;
using System.Diagnostics;

namespace CPWizard
{
	public class MouseHook : IDisposable
	{
		private bool m_isDisposed = false;

		private object m_tag = null;

		private Win32.HookType m_hookType = Win32.HookType.WH_MOUSE_LL;
		private IntPtr m_hookHandle = IntPtr.Zero;
		private Win32.HookProc m_hookFunction = null;

		public event EventHandler<MouseEventArgs> MouseEvent = null;

		public MouseHook(object tag)
		{
			m_tag = tag;
			m_hookFunction = new Win32.HookProc(HookCallback);
			InstallHook();
		}

		~MouseHook()
		{
			Dispose(false);
		}

		private int HookCallback(int code, IntPtr wParam, IntPtr lParam)
		{
			if (code < 0 || MouseEvent == null)
				return Win32.CallNextHookEx(m_hookHandle, code, wParam, lParam);

			Win32.MOUSELLHOOKSTRUCT mouseData = (Win32.MOUSELLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(Win32.MOUSELLHOOKSTRUCT));

			MouseButtons button = MouseButtons.None;

			short mouseDelta = 0;

			switch (wParam.ToInt32())
			{
				case Win32.WM_LBUTTONDOWN:
					button = MouseButtons.Left;
					break;
				case Win32.WM_RBUTTONDOWN:
					button = MouseButtons.Right;
					break;
				case Win32.WM_MOUSEWHEEL:
					mouseDelta = (short)((mouseData.mouseData >> 16) & 0xffff);
					break;
			}

			int clickCount = 0;

			if (button != MouseButtons.None)
				clickCount = (wParam.ToInt32() == Win32.WM_LBUTTONDBLCLK || wParam.ToInt32() == Win32.WM_RBUTTONDBLCLK) ? 2 : 1;

			MouseEventArgs mouseEventArgs = new MouseEventArgs(button, clickCount, mouseData.pt.X, mouseData.pt.Y, mouseDelta, m_tag);

			if (MouseEvent != null)
				MouseEvent(this, mouseEventArgs);

			return (mouseEventArgs.Handled ? 1 : Win32.CallNextHookEx(m_hookHandle, code, wParam, lParam));
		}

		private void InstallHook()
		{
			if (m_hookHandle != IntPtr.Zero)
				return;

			//m_hookHandle = SetWindowsHookEx(m_hookType, m_hookFunction, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), 0);
			m_hookHandle = Win32.SetWindowsHookEx(m_hookType, m_hookFunction, Process.GetCurrentProcess().MainModule.BaseAddress, 0);
		}

		private void UnInstallHook()
		{
			if (m_hookHandle != IntPtr.Zero)
			{
				Win32.UnhookWindowsHookEx(m_hookHandle);
				m_hookHandle = IntPtr.Zero;
			}
		}

		#region IDisposable Members

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (m_isDisposed)
				return;

			if (disposing)
			{
				// Dispose managed resources
			}

			// Dispose unmanaged resources
			UnInstallHook();

			m_isDisposed = true;
		}

		#endregion
	}

	public class MouseEventArgs : EventArgs
	{
		public MouseButtons Button;
		public int Clicks;
		public int X;
		public int Y;
		public int Delta;
		public object Tag;
		public bool Handled = false;

		public MouseEventArgs(MouseButtons button, int clicks, int x, int y, int delta, object tag)
		{
			Button = button;
			Clicks = clicks;
			X = x;
			Y = y;
			Delta = delta;
			Tag = tag;
		}

		public Point Location
		{
			get { return new Point(X, Y); }
		}
	}

	public partial class Win32
	{
		public const int WM_MOUSEMOVE = 0x200;
		public const int WM_LBUTTONDOWN = 0x201;
		public const int WM_RBUTTONDOWN = 0x204;
		public const int WM_MBUTTONDOWN = 0x207;
		public const int WM_LBUTTONUP = 0x202;
		public const int WM_RBUTTONUP = 0x205;
		public const int WM_MBUTTONUP = 0x208;
		public const int WM_LBUTTONDBLCLK = 0x203;
		public const int WM_RBUTTONDBLCLK = 0x206;
		public const int WM_MBUTTONDBLCLK = 0x209;
		public const int WM_MOUSEWHEEL = 0x020A;

		/* public enum HookType : int
		{
			WH_JOURNALRECORD = 0,
			WH_JOURNALPLAYBACK = 1,
			WH_KEYBOARD = 2,
			WH_GETMESSAGE = 3,
			WH_CALLWNDPROC = 4,
			WH_CBT = 5,
			WH_SYSMSGFILTER = 6,
			WH_MOUSE = 7,
			WH_HARDWARE = 8,
			WH_DEBUG = 9,
			WH_SHELL = 10,
			WH_FOREGROUNDIDLE = 11,
			WH_CALLWNDPROCRET = 12,
			WH_KEYBOARD_LL = 13,
			WH_MOUSE_LL = 14
		} */

		[StructLayout(LayoutKind.Sequential)]
		public class POINT
		{
			public int X;
			public int Y;
		}

		[StructLayout(LayoutKind.Sequential)]
		public class MOUSELLHOOKSTRUCT
		{
			public POINT pt;
			public uint mouseData;
			public uint flags;
			public uint time;
			public UIntPtr dwExtraInfo;
		}

		/* public delegate int HookProc(int code, int wParam, IntPtr lParam);

		[DllImport("user32.dll")]
		public static extern IntPtr SetWindowsHookEx(HookType code, HookProc func, IntPtr instance, int threadID);

		[DllImport("user32.dll")]
		public static extern int UnhookWindowsHookEx(IntPtr hook); */

		[DllImport("user32.dll")]
		public static extern int CallNextHookEx(IntPtr hook, int code, int wParam, IntPtr lParam);
	}
}