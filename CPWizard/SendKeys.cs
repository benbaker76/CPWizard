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
using System.Text.RegularExpressions;

namespace CPWizard
{
	public class SendKeys : IDisposable
	{
		private bool m_bWait = false;
		private bool m_bUsingParens = false;
		private bool m_bShiftDown = false;
		private bool m_bAltDown = false;
		private bool m_bControlDown = false;
		private bool m_bWinDown = false;

		private int m_delayAlways = 0;
		private int m_delayNow = 0;

		[Flags]
		private enum SendKeyFlags
		{
			VKeyScanShiftOn = 0x01,
			VKeyScanCtrlOn = 0x02,
			VKeyScanAltOn = 0x04
		}

		private Win32.VirtualKeys[] ExtendedVKeys = new Win32.VirtualKeys[] {
            Win32.VirtualKeys.VK_UP,
            Win32.VirtualKeys.VK_DOWN,
            Win32.VirtualKeys.VK_LEFT,
            Win32.VirtualKeys.VK_RIGHT,
            Win32.VirtualKeys.VK_HOME,
            Win32.VirtualKeys.VK_END,
            Win32.VirtualKeys.VK_PRIOR,
            Win32.VirtualKeys.VK_NEXT,
            Win32.VirtualKeys.VK_INSERT,
            Win32.VirtualKeys.VK_DELETE,
			Win32.VirtualKeys.VK_LCONTROL,
            Win32.VirtualKeys.VK_RCONTROL
        };

		private struct KeyDesc
		{
			public string Name;
			public short VKey;

			public KeyDesc(string name, Win32.VirtualKeys vKey)
			{
				Name = name;
				VKey = (short)vKey;
			}

			public KeyDesc(string name, char vKey)
			{
				Name = name;
				VKey = Win32.VkKeyScan(vKey);
			}
		}

		private KeyDesc[] KeyNames = {
            new KeyDesc("ADD", Win32.VirtualKeys.VK_ADD),
            new KeyDesc("APPS", Win32.VirtualKeys.VK_APPS),
            new KeyDesc("AT", '@'),
            new KeyDesc("BACKSPACE", Win32.VirtualKeys.VK_BACK),
            new KeyDesc("BKSP", Win32.VirtualKeys.VK_BACK),
            new KeyDesc("BREAK", Win32.VirtualKeys.VK_CANCEL),
            new KeyDesc("BS", Win32.VirtualKeys.VK_BACK),
            new KeyDesc("CAPSLOCK", Win32.VirtualKeys.VK_CAPITAL),
            new KeyDesc("CARET", '^'),
            new KeyDesc("CLEAR", Win32.VirtualKeys.VK_CLEAR),
            new KeyDesc("DECIMAL", Win32.VirtualKeys.VK_DECIMAL), 
            new KeyDesc("DEL", Win32.VirtualKeys.VK_DELETE),
            new KeyDesc("DELETE", Win32.VirtualKeys.VK_DELETE),
            new KeyDesc("DIVIDE", Win32.VirtualKeys.VK_DIVIDE), 
            new KeyDesc("DOWN", Win32.VirtualKeys.VK_DOWN),
            new KeyDesc("END", Win32.VirtualKeys.VK_END),
            new KeyDesc("ENTER", Win32.VirtualKeys.VK_RETURN),
            new KeyDesc("ESC", Win32.VirtualKeys.VK_ESCAPE),
            new KeyDesc("ESCAPE", Win32.VirtualKeys.VK_ESCAPE),
            new KeyDesc("F1", Win32.VirtualKeys.VK_F1),
            new KeyDesc("F10", Win32.VirtualKeys.VK_F10),
            new KeyDesc("F11", Win32.VirtualKeys.VK_F11),
            new KeyDesc("F12", Win32.VirtualKeys.VK_F12),
            new KeyDesc("F13", Win32.VirtualKeys.VK_F13),
            new KeyDesc("F14", Win32.VirtualKeys.VK_F14),
            new KeyDesc("F15", Win32.VirtualKeys.VK_F15),
            new KeyDesc("F16", Win32.VirtualKeys.VK_F16),
            new KeyDesc("F2", Win32.VirtualKeys.VK_F2),
            new KeyDesc("F3", Win32.VirtualKeys.VK_F3),
            new KeyDesc("F4", Win32.VirtualKeys.VK_F4),
            new KeyDesc("F5", Win32.VirtualKeys.VK_F5),
            new KeyDesc("F6", Win32.VirtualKeys.VK_F6),
            new KeyDesc("F7", Win32.VirtualKeys.VK_F7),
            new KeyDesc("F8", Win32.VirtualKeys.VK_F8),
            new KeyDesc("F9", Win32.VirtualKeys.VK_F9),
            new KeyDesc("HELP", Win32.VirtualKeys.VK_HELP),
            new KeyDesc("HOME", Win32.VirtualKeys.VK_HOME),
            new KeyDesc("INS", Win32.VirtualKeys.VK_INSERT),
            new KeyDesc("LEFT", Win32.VirtualKeys.VK_LEFT),
            new KeyDesc("LEFTBRACE", '{'),
            new KeyDesc("LEFTPAREN", '('),
            new KeyDesc("LWIN", Win32.VirtualKeys.VK_LWIN),
            new KeyDesc("MULTIPLY", Win32.VirtualKeys.VK_MULTIPLY),
            new KeyDesc("NUMLOCK", Win32.VirtualKeys.VK_NUMLOCK),
            new KeyDesc("NUMPAD0", Win32.VirtualKeys.VK_NUMPAD0),
            new KeyDesc("NUMPAD1", Win32.VirtualKeys.VK_NUMPAD1),
            new KeyDesc("NUMPAD2", Win32.VirtualKeys.VK_NUMPAD2),
            new KeyDesc("NUMPAD3", Win32.VirtualKeys.VK_NUMPAD3),
            new KeyDesc("NUMPAD4", Win32.VirtualKeys.VK_NUMPAD4),
            new KeyDesc("NUMPAD5", Win32.VirtualKeys.VK_NUMPAD5),
            new KeyDesc("NUMPAD6", Win32.VirtualKeys.VK_NUMPAD6),
            new KeyDesc("NUMPAD7", Win32.VirtualKeys.VK_NUMPAD7),
            new KeyDesc("NUMPAD8", Win32.VirtualKeys.VK_NUMPAD8),
            new KeyDesc("NUMPAD9", Win32.VirtualKeys.VK_NUMPAD9),
            new KeyDesc("PERCENT", '%'),
            new KeyDesc("PGDN", Win32.VirtualKeys.VK_NEXT),
            new KeyDesc("PGUP", Win32.VirtualKeys.VK_PRIOR),
            new KeyDesc("PLUS", '+'),
            new KeyDesc("PRTSC", Win32.VirtualKeys.VK_PRINT),
            new KeyDesc("RIGHT", Win32.VirtualKeys.VK_RIGHT),
            new KeyDesc("RIGHTBRACE", '}'),
            new KeyDesc("RIGHTPAREN", ')'),
            new KeyDesc("RWIN", Win32.VirtualKeys.VK_RWIN),
            new KeyDesc("SCROLL", Win32.VirtualKeys.VK_SCROLL),
            new KeyDesc("SEPARATOR", Win32.VirtualKeys.VK_SEPARATOR),
            new KeyDesc("SNAPSHOT", Win32.VirtualKeys.VK_SNAPSHOT),
            new KeyDesc("SPACE", ' '),
            new KeyDesc("SUBTRACT", Win32.VirtualKeys.VK_SUBTRACT),
            new KeyDesc("TAB", Win32.VirtualKeys.VK_TAB),
            new KeyDesc("TILDE", '~'),
            new KeyDesc("UP", Win32.VirtualKeys.VK_UP),
            new KeyDesc("WIN", Win32.VirtualKeys.VK_LWIN)
        };

		public SendKeys()
		{
		}

		private bool BitSet(byte bitTable, uint bitMask)
		{
			return (bitTable & bitMask) != 0 ? true : false;
		}

		private byte HiByte(short number)
		{
			return (byte)((number >> 8) & 0xff);
		}

		private byte LoByte(short number)
		{
			return (byte)number;
		}

		private short LoWord(uint number)
		{
			return (short)(number & 0xffff);
		}

		private bool IsInteger(string str)
		{
			str = str.Trim();

			return (Regex.IsMatch(str, @"^[\+\-]?\d+$"));
		}

		private int StrToInt(string val)
		{
			if (IsInteger(val))
				return System.Convert.ToInt32(val.Trim());

			return 0;
		}

		private void KeyboardEvent(byte vKey, byte scanCode, int flags)
		{
			Win32.MSG keyboardMsg;

			Win32.keybd_event(vKey, scanCode, (uint)flags, UIntPtr.Zero);

			if (m_bWait)
			{
				while (Win32.PeekMessage(out keyboardMsg, IntPtr.Zero, Win32.WM_KEYFIRST, Win32.WM_KEYLAST, Win32.PM_REMOVE))
				{
					Win32.TranslateMessage(ref keyboardMsg);
					Win32.DispatchMessage(ref keyboardMsg);
				}
			}
		}

		private bool IsVkExtended(short vKey)
		{
			foreach (Win32.VirtualKeys key in ExtendedVKeys)
				if ((byte)key == (byte)vKey)
					return true;

			return false;
		}

		public void SendKeyUp(short vKey)
		{
			SendKeyUp(vKey, false);
		}

		public void SendKeyUp(short vKey, bool bNoDelay)
		{
			if (!bNoDelay)
				System.Threading.Thread.Sleep(100);

			byte scanCode = LoByte((short)Win32.MapVirtualKey((uint)vKey, 0));

			KeyboardEvent((byte)vKey, scanCode, (IsVkExtended(vKey) ? Win32.KEYEVENTF_EXTENDEDKEY : 0) | Win32.KEYEVENTF_KEYUP);
		}

		public void SendKeyDown(short vKey, int numTimes, bool genUpMsg)
		{
			SendKeyDown(vKey, numTimes, genUpMsg, false);
		}

		public void SendKeyDown(short vKey, int numTimes, bool genUpMsg, bool bDelay)
		{
			byte scanCode = 0;
			bool numState = false;

			if ((byte)vKey == (byte)Win32.VirtualKeys.VK_NUMLOCK)
			{
				uint version = Win32.GetVersion();

				for (int i = 1; i <= numTimes; i++)
				{
					if (bDelay)
						CarryDelay();

					// snippet based on:
					// http://www.codeproject.com/cpp/togglekeys.asp

					if (version < 0x80000000)
					{
						Win32.keybd_event((byte)vKey, 0x45, Win32.KEYEVENTF_EXTENDEDKEY, UIntPtr.Zero);
						Win32.keybd_event((byte)vKey, 0x45, Win32.KEYEVENTF_EXTENDEDKEY | Win32.KEYEVENTF_KEYUP, UIntPtr.Zero);
					}
					else
					{
						// Win98 and later
						if ((HiByte((short)LoWord(version)) >= 10))
						{
							// Define _WIN32_WINNT > 0x0400
							// to compile

							Win32.INPUT[] input = new Win32.INPUT[2];

							input[0].type = input[1].type = Win32.INPUT_KEYBOARD;
							input[0].ki.wVk = input[1].ki.wVk = vKey;
							input[1].ki.dwFlags = Win32.KEYEVENTF_KEYUP;

							Win32.SendInput(2, input, Marshal.SizeOf(typeof(Win32.INPUT)));
						}
						// Win95
						else
						{
							byte[] keyboardState = new byte[256];
							numState = (Win32.GetKeyState(Win32.VirtualKeys.VK_NUMLOCK) & 1) != 0 ? true : false;
							Win32.GetKeyboardState(keyboardState);
							if (numState)
								keyboardState[(int)Win32.VirtualKeys.VK_NUMLOCK] &= unchecked((byte)~1);
							else
								keyboardState[(int)Win32.VirtualKeys.VK_NUMLOCK] |= 1;

							Win32.SetKeyboardState(keyboardState);
						}
					}
				}

				return;
			}

			// Get scancode
			scanCode = LoByte((short)Win32.MapVirtualKey((uint)vKey, 0));

			// Send Keys
			for (int i = 1; i <= numTimes; i++)
			{
				// Carry needed delay?
				if (bDelay)
					CarryDelay();

				KeyboardEvent((byte)vKey, scanCode, IsVkExtended(vKey) ? Win32.KEYEVENTF_EXTENDEDKEY : 0);

				if (genUpMsg)
				{
					System.Threading.Thread.Sleep(100);

					KeyboardEvent((byte)vKey, scanCode, (IsVkExtended(vKey) ? Win32.KEYEVENTF_EXTENDEDKEY : 0) | Win32.KEYEVENTF_KEYUP);
				}
			}
		}

		public void SendKey(Keys key, int numTimes, bool genDownMsg)
		{
			SendKey((short)key, numTimes, genDownMsg);
		}

		public void SendKey(char ch, int numTimes, bool genDownMsg)
		{
			short vKey = Win32.VkKeyScan(ch);

			if (vKey == -1)
				SendCharUnicode(ch);
			else
				SendKey(vKey, numTimes, genDownMsg);
		}

		// Sends a single key
		public void SendKey(short vKey, int numTimes, bool genDownMsg)
		{
			if ((HiByte(vKey) & (short)SendKeyFlags.VKeyScanShiftOn) != 0)
				SendKeyDown((short)Win32.VirtualKeys.VK_SHIFT, 1, false);

			if ((HiByte(vKey) & (short)SendKeyFlags.VKeyScanCtrlOn) != 0)
				SendKeyDown((short)Win32.VirtualKeys.VK_CONTROL, 1, false);

			if ((HiByte(vKey) & (short)SendKeyFlags.VKeyScanAltOn) != 0)
				SendKeyDown((short)Win32.VirtualKeys.VK_MENU, 1, false);

			SendKeyDown(vKey, numTimes, genDownMsg, true);

			if ((HiByte(vKey) & (short)SendKeyFlags.VKeyScanShiftOn) != 0)
				SendKeyUp((short)Win32.VirtualKeys.VK_SHIFT);

			if ((HiByte(vKey) & (short)SendKeyFlags.VKeyScanCtrlOn) != 0)
				SendKeyUp((short)Win32.VirtualKeys.VK_CONTROL);

			if ((HiByte(vKey) & (short)SendKeyFlags.VKeyScanAltOn) != 0)
				SendKeyUp((short)Win32.VirtualKeys.VK_MENU);
		}

		private short StringToVK(string keyString)
		{
			foreach (KeyDesc key in KeyNames)
			{
				if (key.Name.Trim().ToLower() == keyString.Trim().ToLower())
					return key.VKey;
			}

			return 0;
		}

		// Releases all shift keys (keys that can be depressed while other keys are being pressed
		// If we are in a modifier group this function does nothing
		public void PopUpShiftKeys()
		{
			if (!m_bUsingParens)
			{
				if (m_bShiftDown)
					SendKeyUp((short)Win32.VirtualKeys.VK_SHIFT);
				if (m_bControlDown)
					SendKeyUp((short)Win32.VirtualKeys.VK_CONTROL);
				if (m_bAltDown)
					SendKeyUp((short)Win32.VirtualKeys.VK_MENU);
				if (m_bWinDown)
					SendKeyUp((short)Win32.VirtualKeys.VK_LWIN);

				m_bWinDown = m_bShiftDown = m_bControlDown = m_bControlDown = m_bAltDown = false;
			}
		}

		public bool SendKeyString(string keyString)
		{
			return SendKeyString(keyString, false);
		}

		// Sends a key string
		public bool SendKeyString(string keyString, bool wait)
		{
			m_delayAlways = 50;
			m_delayNow = 0;

			if (String.IsNullOrEmpty(keyString))
				return false;

			try
			{
				m_bWait = wait;

				m_bWinDown = m_bShiftDown = m_bControlDown = m_bAltDown = m_bUsingParens = false;

				for (int i = 0; i < keyString.Length; i++)
				{
					switch (keyString[i])
					{
						case '(': // begin modifier group
							m_bUsingParens = true;
							break;
						case ')': // end modifier group
							m_bUsingParens = false;
							PopUpShiftKeys(); // pop all shift keys when we finish a modifier group close
							break;
						case '%': // ALT key
							m_bAltDown = true;
							SendKeyDown((short)Win32.VirtualKeys.VK_MENU, 1, false);
							break;
						case '+': // SHIFT key
							m_bShiftDown = true;
							SendKeyDown((short)Win32.VirtualKeys.VK_SHIFT, 1, false);
							break;
						case '^': // CTRL key
							m_bControlDown = true;
							SendKeyDown((short)Win32.VirtualKeys.VK_CONTROL, 1, false);
							break;
						case '@': // WINKEY (Left-WinKey)
							m_bWinDown = true;
							SendKeyDown((short)Win32.VirtualKeys.VK_LWIN, 1, false);
							break;
						case '~': // enter
							SendKeyDown((short)Win32.VirtualKeys.VK_RETURN, 1, true);
							PopUpShiftKeys();
							break;
						case '{': // begin special keys
							{
								i++;

								short vKey = 0;
								int numTimes = 1;

								if (keyString.IndexOf('}', i) != -1)
								{
									string keyName = keyString.Substring(i, keyString.IndexOf('}', i) - i);

									if (keyName.StartsWith("VKEY"))
									{
										string[] parameters = keyName.Split(new char[] { ' ' }, 2);

										if (parameters.Length == 2)
											vKey = Win32.VkKeyScan((char)StrToInt(parameters[1]));
										//SendKey(Win32.VkKeyScan((char) StrToInt(parameters[1])), 1, true);
									}
									else if (keyName.StartsWith("BEEP"))
									{
										string[] parameters = keyName.Split(new char[] { ' ' }, 3);

										if (parameters.Length == 3)
											System.Console.Beep(StrToInt(parameters[1]), StrToInt(parameters[2]));
									}
									else if (keyName.StartsWith("APPACTIVATE"))
									{
										string[] parameters = keyName.Split(new char[] { ' ' }, 2);

										//if (parameters.Length == 2)
										//    FindProcess.AppActivate(parameters[1]);
									}
									else if (keyName.StartsWith("DELAY"))
									{
										string[] parameters = keyName.Split(new char[] { ' ' }, 2);

										if (parameters.Length == 2)
											m_delayNow = StrToInt(parameters[1]);

										parameters = keyName.Split(new char[] { '=' }, 2);

										if (parameters.Length == 2)
											m_delayAlways = StrToInt(parameters[1]);
									}
									else
									{
										string[] parameters = keyName.Split(new char[] { ' ' }, 2);

										if (parameters.Length == 2)
										{
											vKey = StringToVK(parameters[0]);
											numTimes = StrToInt(parameters[1]);
										}
										else
											vKey = StringToVK(keyName);
									}

									if (vKey != 0)
									{
										SendKey(vKey, numTimes, true);
										PopUpShiftKeys();
									}

									i = keyString.IndexOf('}', i);
								}
								break;
							}
						default: // a normal key was pressed
							{
								// Get the VKey from the key
								char ch = keyString[i];
								short vKey = Win32.VkKeyScan(ch);

								if (vKey == -1)
									SendCharUnicode(ch);
								else
									SendKey(vKey, 1, true);

								PopUpShiftKeys();
								break;
							}
					}
				}

				m_bUsingParens = false;
				PopUpShiftKeys();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("SendKeyString", "SendKeys", ex.Message, ex.StackTrace);
			}

			return true;
		}

		// Carries the required delay and clears the m_delayNow value
		private void CarryDelay()
		{
			// Should we delay once?
			if (m_delayNow == 0)
				m_delayNow = m_delayAlways; // should we delay always?

			if (m_delayNow != 0) // No delay specified?
				System.Threading.Thread.Sleep(m_delayNow); //::Beep(100, m_delayNow);

			// clear SleepNow
			m_delayNow = 0;
		}

		public static void SendChar(char ch)
		{
			//uint threadId = Win32.GetWindowThreadProcessId(Win32.GetForegroundWindow(), IntPtr.Zero);
			//IntPtr keyboardLayout = Win32.GetKeyboardLayout(threadId);
			IntPtr keyboardLayout = Win32.GetKeyboardLayout(0);
			short vk = Win32.VkKeyScanEx(ch, keyboardLayout);
			short scanCode = (short)Win32.MapVirtualKey((uint)vk, 0);

			unchecked
			{
				Win32.INPUT[] input = new Win32.INPUT[2];

				input[0] = new Win32.INPUT();
				input[0].type = Win32.INPUT_KEYBOARD;
				input[0].ki.wVk = vk;
				input[0].ki.wScan = (short)(scanCode & 0xff);
				input[0].ki.time = 0;
				input[0].ki.dwFlags = 0;
				input[0].ki.dwExtraInfo = Win32.GetMessageExtraInfo();

				input[1] = new Win32.INPUT();
				input[1].type = Win32.INPUT_KEYBOARD;
				input[1].ki.wVk = vk;
				input[1].ki.wScan = scanCode;
				input[1].ki.time = 0;
				input[1].ki.dwFlags = Win32.KEYEVENTF_KEYUP;
				input[1].ki.dwExtraInfo = Win32.GetMessageExtraInfo();

				Win32.SendInput(2, input, Marshal.SizeOf(typeof(Win32.INPUT)));
			}
		}

		public static void SendCharUnicode(char ch)
		{
			Win32.INPUT[] input = new Win32.INPUT[2];

			input[0] = new Win32.INPUT();
			input[0].type = Win32.INPUT_KEYBOARD;
			input[0].ki.wVk = 0;
			input[0].ki.wScan = (short)ch;
			input[0].ki.time = 0;
			input[0].ki.dwFlags = Win32.KEYEVENTF_UNICODE;
			input[0].ki.dwExtraInfo = IntPtr.Zero;

			input[1] = new Win32.INPUT();
			input[1].type = Win32.INPUT_KEYBOARD;
			input[1].ki.wVk = 0;
			input[1].ki.wScan = (short)ch;
			input[1].ki.time = 0;
			input[1].ki.dwFlags = Win32.KEYEVENTF_UNICODE | Win32.KEYEVENTF_KEYUP;
			input[1].ki.dwExtraInfo = IntPtr.Zero;

			Win32.SendInput(2, input, Marshal.SizeOf(typeof(Win32.INPUT)));
		}

		public static void SendCharUnicode(int utf32)
		{
			string unicodeString = Char.ConvertFromUtf32(utf32);
			Win32.INPUT[] input = new Win32.INPUT[unicodeString.Length];

			for (int i = 0; i < input.Length; i++)
			{
				input[i] = new Win32.INPUT();
				input[i].type = Win32.INPUT_KEYBOARD;
				input[i].ki.wVk = 0;
				input[i].ki.wScan = (short)unicodeString[i];
				input[i].ki.time = 0;
				input[i].ki.dwFlags = Win32.KEYEVENTF_UNICODE;
				input[i].ki.dwExtraInfo = IntPtr.Zero;
			}

			Win32.SendInput((uint)input.Length, input, Marshal.SizeOf(typeof(Win32.INPUT)));
		}

		public static void KeyPress(char ch, bool keyDown)
		{
			short vk = Win32.VkKeyScan(ch);
			short scanCode = (short)Win32.MapVirtualKey((uint)vk, 0);

			if (keyDown)
				KeyDown(scanCode);
			else
				KeyUp(scanCode);
		}

		public static bool KeyDown(short scanCode)
		{
			Win32.INPUT[] inputs = new Win32.INPUT[1];
			inputs[0].type = Win32.INPUT_KEYBOARD;
			inputs[0].ki.dwFlags = 0;
			inputs[0].ki.wScan = (short)(scanCode & 0xff);

			return (Win32.SendInput(1, inputs, Marshal.SizeOf(inputs[0])) == 1);
		}

		public static bool KeyUp(short scanCode)
		{
			Win32.INPUT[] inputs = new Win32.INPUT[1];
			inputs[0].type = Win32.INPUT_KEYBOARD;
			inputs[0].ki.wScan = scanCode;
			inputs[0].ki.dwFlags = Win32.KEYEVENTF_KEYUP;

			return (Win32.SendInput(1, inputs, Marshal.SizeOf(inputs[0])) == 1);
		}

		private char KeyToChar(Keys key, uint scanCode)
		{
			var buffer = new StringBuilder(256);
			var keyboardState = new byte[256];
			char ch = '\0';

			// Force the keyboard status cache to update
			Win32.GetKeyState(0);

			if (Win32.GetKeyboardState(keyboardState))
				Win32.ToUnicode((uint)key, scanCode, keyboardState, buffer, 64, 0);

			if (buffer.Length != 1)
				return ch;

			ch = buffer.ToString()[0];

			return ch;
		}

		public void SetDelay(int delay)
		{
			m_delayAlways = delay;
		}

		public bool Alt
		{
			set { m_bAltDown = value; if (value) SendKeyDown((short)Win32.VirtualKeys.VK_MENU, 1, false); else PopUpShiftKeys(); }
			get { return m_bAltDown; }
		}

		public bool Shift
		{
			set { m_bShiftDown = value; if (value) SendKeyDown((short)Win32.VirtualKeys.VK_SHIFT, 1, false); else PopUpShiftKeys(); }
			get { return m_bShiftDown; }
		}

		public bool Ctrl
		{
			set { m_bControlDown = value; if (value) SendKeyDown((short)Win32.VirtualKeys.VK_CONTROL, 1, false); else PopUpShiftKeys(); }
			get { return m_bControlDown; }
		}

		public bool LWin
		{
			set { m_bWinDown = value; if (value) SendKeyDown((short)Win32.VirtualKeys.VK_LWIN, 1, false); else PopUpShiftKeys(); }
			get { return m_bWinDown; }
		}

		public bool Enter
		{
			set { if (value) SendKeyDown((short)Win32.VirtualKeys.VK_RETURN, 1, true); PopUpShiftKeys(); }
		}

		#region IDisposable Members

		public void Dispose()
		{
		}

		#endregion
	}

	public partial class Win32
	{
		#region VirtualKeys
		public enum VirtualKeys : int
		{
			VK_LBUTTON = 0x01,
			VK_RBUTTON = 0x02,
			VK_CANCEL = 0x03,
			VK_MBUTTON = 0x04,
			//
			VK_XBUTTON1 = 0x05,
			VK_XBUTTON2 = 0x06,
			//
			VK_BACK = 0x08,
			VK_TAB = 0x09,
			//
			VK_CLEAR = 0x0C,
			VK_RETURN = 0x0D,
			//
			VK_SHIFT = 0x10,
			VK_CONTROL = 0x11,
			VK_MENU = 0x12,
			VK_PAUSE = 0x13,
			VK_CAPITAL = 0x14,
			//
			VK_KANA = 0x15,
			VK_HANGEUL = 0x15,  // old name - should be here for compatibility
			VK_HANGUL = 0x15,
			VK_JUNJA = 0x17,
			VK_FINAL = 0x18,
			VK_HANJA = 0x19,
			VK_KANJI = 0x19,
			//
			VK_ESCAPE = 0x1B,
			//
			VK_CONVERT = 0x1C,
			VK_NONCONVERT = 0x1D,
			VK_ACCEPT = 0x1E,
			VK_MODECHANGE = 0x1F,
			//
			VK_SPACE = 0x20,
			VK_PRIOR = 0x21,
			VK_NEXT = 0x22,
			VK_END = 0x23,
			VK_HOME = 0x24,
			VK_LEFT = 0x25,
			VK_UP = 0x26,
			VK_RIGHT = 0x27,
			VK_DOWN = 0x28,
			VK_SELECT = 0x29,
			VK_PRINT = 0x2A,
			VK_EXECUTE = 0x2B,
			VK_SNAPSHOT = 0x2C,
			VK_INSERT = 0x2D,
			VK_DELETE = 0x2E,
			VK_HELP = 0x2F,
			//
			VK_LWIN = 0x5B,
			VK_RWIN = 0x5C,
			VK_APPS = 0x5D,
			//
			VK_SLEEP = 0x5F,
			//
			VK_NUMPAD0 = 0x60,
			VK_NUMPAD1 = 0x61,
			VK_NUMPAD2 = 0x62,
			VK_NUMPAD3 = 0x63,
			VK_NUMPAD4 = 0x64,
			VK_NUMPAD5 = 0x65,
			VK_NUMPAD6 = 0x66,
			VK_NUMPAD7 = 0x67,
			VK_NUMPAD8 = 0x68,
			VK_NUMPAD9 = 0x69,
			VK_MULTIPLY = 0x6A,
			VK_ADD = 0x6B,
			VK_SEPARATOR = 0x6C,
			VK_SUBTRACT = 0x6D,
			VK_DECIMAL = 0x6E,
			VK_DIVIDE = 0x6F,
			VK_F1 = 0x70,
			VK_F2 = 0x71,
			VK_F3 = 0x72,
			VK_F4 = 0x73,
			VK_F5 = 0x74,
			VK_F6 = 0x75,
			VK_F7 = 0x76,
			VK_F8 = 0x77,
			VK_F9 = 0x78,
			VK_F10 = 0x79,
			VK_F11 = 0x7A,
			VK_F12 = 0x7B,
			VK_F13 = 0x7C,
			VK_F14 = 0x7D,
			VK_F15 = 0x7E,
			VK_F16 = 0x7F,
			VK_F17 = 0x80,
			VK_F18 = 0x81,
			VK_F19 = 0x82,
			VK_F20 = 0x83,
			VK_F21 = 0x84,
			VK_F22 = 0x85,
			VK_F23 = 0x86,
			VK_F24 = 0x87,
			//
			VK_NUMLOCK = 0x90,
			VK_SCROLL = 0x91,
			//
			VK_OEM_NEC_EQUAL = 0x92,   // '=' key on numpad
			//
			VK_OEM_FJ_JISHO = 0x92,   // 'Dictionary' key
			VK_OEM_FJ_MASSHOU = 0x93,   // 'Unregister word' key
			VK_OEM_FJ_TOUROKU = 0x94,   // 'Register word' key
			VK_OEM_FJ_LOYA = 0x95,   // 'Left OYAYUBI' key
			VK_OEM_FJ_ROYA = 0x96,   // 'Right OYAYUBI' key
			//
			VK_LSHIFT = 0xA0,
			VK_RSHIFT = 0xA1,
			VK_LCONTROL = 0xA2,
			VK_RCONTROL = 0xA3,
			VK_LMENU = 0xA4,
			VK_RMENU = 0xA5,
			//
			VK_BROWSER_BACK = 0xA6,
			VK_BROWSER_FORWARD = 0xA7,
			VK_BROWSER_REFRESH = 0xA8,
			VK_BROWSER_STOP = 0xA9,
			VK_BROWSER_SEARCH = 0xAA,
			VK_BROWSER_FAVORITES = 0xAB,
			VK_BROWSER_HOME = 0xAC,
			//
			VK_VOLUME_MUTE = 0xAD,
			VK_VOLUME_DOWN = 0xAE,
			VK_VOLUME_UP = 0xAF,
			VK_MEDIA_NEXT_TRACK = 0xB0,
			VK_MEDIA_PREV_TRACK = 0xB1,
			VK_MEDIA_STOP = 0xB2,
			VK_MEDIA_PLAY_PAUSE = 0xB3,
			VK_LAUNCH_MAIL = 0xB4,
			VK_LAUNCH_MEDIA_SELECT = 0xB5,
			VK_LAUNCH_APP1 = 0xB6,
			VK_LAUNCH_APP2 = 0xB7,
			//
			VK_OEM_1 = 0xBA,   // ';:' for US
			VK_OEM_PLUS = 0xBB,   // '+' any country
			VK_OEM_COMMA = 0xBC,   // ',' any country
			VK_OEM_MINUS = 0xBD,   // '-' any country
			VK_OEM_PERIOD = 0xBE,   // '.' any country
			VK_OEM_2 = 0xBF,   // '/?' for US
			VK_OEM_3 = 0xC0,   // '`~' for US
			//
			VK_OEM_4 = 0xDB,  //  '[{' for US
			VK_OEM_5 = 0xDC,  //  '\|' for US
			VK_OEM_6 = 0xDD,  //  ']}' for US
			VK_OEM_7 = 0xDE,  //  ''"' for US
			VK_OEM_8 = 0xDF,
			//
			VK_OEM_AX = 0xE1,  //  'AX' key on Japanese AX kbd
			VK_OEM_102 = 0xE2,  //  "<>" or "\|" on RT 102-key kbd.
			VK_ICO_HELP = 0xE3,  //  Help key on ICO
			VK_ICO_00 = 0xE4,  //  00 key on ICO
			//
			VK_PROCESSKEY = 0xE5,
			//
			VK_ICO_CLEAR = 0xE6,
			//
			VK_PACKET = 0xE7,
			//
			VK_OEM_RESET = 0xE9,
			VK_OEM_JUMP = 0xEA,
			VK_OEM_PA1 = 0xEB,
			VK_OEM_PA2 = 0xEC,
			VK_OEM_PA3 = 0xED,
			VK_OEM_WSCTRL = 0xEE,
			VK_OEM_CUSEL = 0xEF,
			VK_OEM_ATTN = 0xF0,
			VK_OEM_FINISH = 0xF1,
			VK_OEM_COPY = 0xF2,
			VK_OEM_AUTO = 0xF3,
			VK_OEM_ENLW = 0xF4,
			VK_OEM_BACKTAB = 0xF5
		}
		#endregion

		public const int WM_KEYFIRST = 0x100;
		public const int WM_KEYLAST = 0x108;
		public const int PM_REMOVE = 0x1;

		public const int KEYEVENTF_EXTENDEDKEY = 0x1;
		//public const int KEYEVENTF_KEYUP = 0x2;
		public const int KEYEVENTF_UNICODE = 0x0004;

		public const int INPUT_KEYBOARD = 0x1;

		[StructLayout(LayoutKind.Sequential)]
		public struct MSG
		{
			public IntPtr handle;
			public uint msg;
			public IntPtr wParam;
			public IntPtr lParam;
			public uint time;
			public System.Drawing.Point p;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct MOUSEINPUT
		{
			public int dx;
			public int dy;
			public int mouseData;
			public int dwFlags;
			public int time;
			public IntPtr dwExtraInfo;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct KEYBDINPUT
		{
			public short wVk;
			public short wScan;
			public int dwFlags;
			public int time;
			public IntPtr dwExtraInfo;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct HARDWAREINPUT
		{
			public int uMsg;
			public short wParamL;
			public short wParamH;
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct INPUT
		{
			[FieldOffset(0)]
			public int type;
			[FieldOffset(4)]
			public MOUSEINPUT mi;
			[FieldOffset(4)]
			public KEYBDINPUT ki;
			[FieldOffset(4)]
			public HARDWAREINPUT hi;
		}

		[DllImport("user32.dll")]
		public static extern short GetKeyState(VirtualKeys nVirtKey);

		[DllImport("user32.dll")]
		public static extern bool GetKeyboardState(byte[] lpKeyState);

		[DllImport("user32.dll")]
		public static extern bool SetKeyboardState(byte[] lpKeyState);

		[DllImport("user32.dll")]
		public static extern short VkKeyScan(char ch);

		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		public static extern short VkKeyScanEx(char ch, IntPtr dwhkl);

		//[DllImport("user32.dll")]
		//public static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr processId);

		[DllImport("user32.dll")]
		public static extern IntPtr GetKeyboardLayout(uint idThread);

		//[DllImport("user32.dll")]
		//public static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll", SetLastError = false)]
		public static extern IntPtr GetMessageExtraInfo();

		[DllImport("user32.dll", SetLastError = true)]
		public static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool PeekMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);

		[DllImport("user32.dll")]
		public static extern bool TranslateMessage([In] ref MSG lpMsg);

		[DllImport("user32.dll")]
		public static extern IntPtr DispatchMessage([In] ref MSG lpmsg);

		[DllImport("user32.dll")]
		public static extern uint MapVirtualKey(uint uCode, uint uMapType);

		[DllImport("kernel32.dll")]
		public static extern uint GetVersion();

		[DllImport("user32.dll")]
		public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

		//public delegate bool EnumWindowsCallback(IntPtr hWnd, IntPtr lParam);

		//[DllImport("user32.dll")]
		//public static extern int EnumWindows(EnumWindowsCallback callPtr, IntPtr lParam);

		[return: MarshalAs(UnmanagedType.Bool)]
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		//[DllImport("user32.dll")]
		//public static extern int ToUnicode(uint virtualKeyCode, uint scanCode, byte[] keyboardState, [Out, MarshalAs(UnmanagedType.LPWStr, SizeConst = 64)] StringBuilder receivingBuffer, int bufferSize, uint flags);
	}
}
