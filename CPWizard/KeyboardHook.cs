// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;
using System.Reflection;
using System.Drawing;

namespace CPWizard
{
	public class KeyboardHook : IDisposable
	{
		private bool m_isDisposed = false;

		private object m_tag = null;

		private Win32.HookType m_hookType = Win32.HookType.WH_KEYBOARD_LL;
		private IntPtr m_hookHandle = IntPtr.Zero;
		private Win32.HookProc m_hookFunction = null;

		public event EventHandler<KeyEventArgs> KeyEvent = null;

		public KeyboardHook(object tag)
		{
			m_tag = tag;
			m_hookFunction = new Win32.HookProc(HookCallback);

			InstallHook();
		}

		~KeyboardHook()
		{
			Dispose(false);
		}

		// hook function called by system
		private int HookCallback(int code, IntPtr wParam, IntPtr lParam)
		{
			if (code < 0 || code != Win32.HC_ACTION || KeyEvent == null)
				return Win32.CallNextHookEx(m_hookHandle, code, wParam, lParam);

			Win32.KBDLLHOOKSTRUCT KeyboardData = (Win32.KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(Win32.KBDLLHOOKSTRUCT));

			bool isInjected = ((KeyboardData.flags & Win32.LLKHF_INJECTED) != 0);
			bool isKeyDown = ((KeyboardData.flags & Win32.LLKHF_UP) == 0);
			bool isAltDown = ((KeyboardData.flags & Win32.LLKHF_ALTDOWN) != 0);
			bool isControlDown = (Win32.GetAsyncKeyState(Win32.VK_CONTROL) != 0);
			bool isShiftDown = (Win32.GetAsyncKeyState(Win32.VK_SHIFT) != 0);
			bool isCapsLockDown = (Win32.GetAsyncKeyState(Win32.VK_CAPITAL) != 0);

			if (isInjected)
				return Win32.CallNextHookEx(m_hookHandle, code, wParam, lParam);

			byte[] keyState = new byte[256];
			byte[] inBuffer = new byte[2];
			char ch = '\0';

			Win32.GetKeyboardState(keyState);

			if (Win32.ToAscii(KeyboardData.vkCode, KeyboardData.scanCode, keyState, inBuffer, KeyboardData.flags) == 1)
			{
				ch = (char)inBuffer[0];

				if ((isCapsLockDown ^ isShiftDown) && Char.IsLetter(ch))
					ch = Char.ToUpper(ch);
			}

			Keys keys = (Keys)KeyboardData.vkCode;
			keys |= (isAltDown ? Keys.Alt : Keys.None);
			keys |= (isControlDown ? Keys.Control : Keys.None);
			keys |= (isShiftDown ? Keys.Shift : Keys.None);

			KeyEventArgs keyEventArgs = new KeyEventArgs(keys, ch, isKeyDown, isCapsLockDown, m_tag);

			if (KeyEvent != null)
				KeyEvent(this, keyEventArgs);

			//Console.WriteLine("{0}: VKCode:{1} ScanCode:{2} Flags:{3}", isKeyDown ? "KEYDOWN" : "KEYUP", KeyboardData.vkCode, KeyboardData.scanCode, KeyboardData.flags);

			return (keyEventArgs.Handled ? 1 : Win32.CallNextHookEx(m_hookHandle, code, wParam, lParam));
		}

		private void InstallHook()
		{
			// make sure not already installed
			if (m_hookHandle != IntPtr.Zero)
				return;

			// install system-wide hook
			//m_hookHandle = SetWindowsHookEx(m_hookType, m_hookFunction, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), 0);
			m_hookHandle = Win32.SetWindowsHookEx(m_hookType, m_hookFunction, Process.GetCurrentProcess().MainModule.BaseAddress, 0);
		}

		private void UnInstallHook()
		{
			if (m_hookHandle != IntPtr.Zero)
			{
				// uninstall system-wide hook
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

	public partial class Win32
	{
		public const int WM_KEYDOWN = 0x100;
		public const int WM_KEYUP = 0x101;
		public const int WM_SYSKEYDOWN = 0x104;

		public const int HC_ACTION = 0;

		public const int LLKHF_UP = 0x80;
		public const int LLKHF_DOWN = 0x81;
		public const int LLKHF_INJECTED = 0x10;
		public const int LLKHF_ALTDOWN = 0x20;

		public const int VK_SHIFT = 0x10;
		public const int VK_CONTROL = 0x11;
		public const int VK_CAPITAL = 0x14;
		public const int VK_NUMLOCK = 0x90;

		public const int KEYEVENTF_KEYUP = 0x0002;

		public enum HookType : int
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
		}

		public struct KBDLLHOOKSTRUCT
		{
			public UInt32 vkCode;
			public UInt32 scanCode;
			public UInt32 flags;
			public UInt32 time;
			public IntPtr extraInfo;
		}

		// hook method called by system
		public delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll")]
		public static extern IntPtr SetWindowsHookEx(HookType code, HookProc func, IntPtr instance, int threadID);

		[DllImport("user32.dll")]
		public static extern int UnhookWindowsHookEx(IntPtr hook);

		[DllImport("user32.dll")]
		public static extern int CallNextHookEx(IntPtr hook, int code, IntPtr wParam, IntPtr lParam);

		//[DllImport("user32.dll")]
		//public static extern int GetKeyboardState(byte[] pbKeyState);

		[DllImport("user32.dll")]
		public static extern short GetKeyState(int vKey);

		[DllImport("user32.dll")]
		public static extern short GetAsyncKeyState(int vKey);

		[DllImport("user32.dll")]
		public static extern int ToAscii(uint uVirtKey, uint uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, uint fuState);
	}
}