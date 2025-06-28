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
using System.Drawing;
using Microsoft.Win32;

namespace CPWizard
{
	public class RawInput : IDisposable
	{
		public static string[] KeyCodes =
        {
            "[KEYCODE_NONE]",
            "[KEYCODE_LBUTTON]",
            "[KEYCODE_RBUTTON]",
            "[KEYCODE_CANCEL]",
            "[KEYCODE_MBUTTON]",
            "[KEYCODE_XBUTTON1]",
            "[KEYCODE_XBUTTON2]",
            "[KEYCODE_LBUTTON]|[KEYCODE_XBUTTON2]",
            "[KEYCODE_BACK]",
            "[KEYCODE_TAB]",
            "[KEYCODE_LINEFEED]",
            "[KEYCODE_LBUTTON]|[KEYCODE_LINEFEED]",
            "[KEYCODE_CLEAR]",
            "[KEYCODE_RETURN]",
            "[KEYCODE_RBUTTON]|[KEYCODE_CLEAR]",
            "[KEYCODE_RBUTTON]|[KEYCODE_RETURN]",
            "[KEYCODE_SHIFTKEY]",
            "[KEYCODE_CONTROLKEY]",
            "[KEYCODE_MENU]",
            "[KEYCODE_PAUSE]",
            "[KEYCODE_CAPITAL]",
            "[KEYCODE_KANAMODE]",
            "[KEYCODE_RBUTTON]|[KEYCODE_CAPITAL]",
            "[KEYCODE_JUNJAMODE]",
            "[KEYCODE_FINALMODE]",
            "[KEYCODE_HANJAMODE]",
            "[KEYCODE_RBUTTON]|[KEYCODE_FINALMODE]",
            "[KEYCODE_ESCAPE]",
            "[KEYCODE_IMECONVERT]",
            "[KEYCODE_IMENONCONVERT]",
            "[KEYCODE_IMEACEEPT]",
            "[KEYCODE_IMEMODECHANGE]",
            "[KEYCODE_SPACE]",
            "[KEYCODE_PAGEUP]",
            "[KEYCODE_NEXT]",
            "[KEYCODE_END]",
            "[KEYCODE_HOME]",
            "[KEYCODE_LEFT]",
            "[KEYCODE_UP]",
            "[KEYCODE_RIGHT]",
            "[KEYCODE_DOWN]",
            "[KEYCODE_SELECT]",
            "[KEYCODE_PRINT]",
            "[KEYCODE_EXECUTE]",
            "[KEYCODE_PRINTSCREEN]",
            "[KEYCODE_INSERT]",
            "[KEYCODE_DELETE]",
            "[KEYCODE_HELP]",
            "[KEYCODE_D0]",
            "[KEYCODE_D1]",
            "[KEYCODE_D2]",
            "[KEYCODE_D3]",
            "[KEYCODE_D4]",
            "[KEYCODE_D5]",
            "[KEYCODE_D6]",
            "[KEYCODE_D7]",
            "[KEYCODE_D8]",
            "[KEYCODE_D9]",
            "[KEYCODE_RBUTTON]|[KEYCODE_D8]",
            "[KEYCODE_RBUTTON]|[KEYCODE_D9]",
            "[KEYCODE_MBUTTON]|[KEYCODE_D8]",
            "[KEYCODE_MBUTTON]|[KEYCODE_D9]",
            "[KEYCODE_XBUTTON2]|[KEYCODE_D8]",
            "[KEYCODE_XBUTTON2]|[KEYCODE_D9]",
            "[KEYCODE_64]",
            "[KEYCODE_A]",
            "[KEYCODE_B]",
            "[KEYCODE_C]",
            "[KEYCODE_D]",
            "[KEYCODE_E]",
            "[KEYCODE_F]",
            "[KEYCODE_G]",
            "[KEYCODE_H]",
            "[KEYCODE_I]",
            "[KEYCODE_J]",
            "[KEYCODE_K]",
            "[KEYCODE_L]",
            "[KEYCODE_M]",
            "[KEYCODE_N]",
            "[KEYCODE_O]",
            "[KEYCODE_P]",
            "[KEYCODE_Q]",
            "[KEYCODE_R]",
            "[KEYCODE_S]",
            "[KEYCODE_T]",
            "[KEYCODE_U]",
            "[KEYCODE_V]",
            "[KEYCODE_W]",
            "[KEYCODE_X]",
            "[KEYCODE_Y]",
            "[KEYCODE_Z]",
            "[KEYCODE_LWIN]",
            "[KEYCODE_RWIN]",
            "[KEYCODE_APPS]",
            "[KEYCODE_RBUTTON]|[KEYCODE_RWIN]",
            "[KEYCODE_SLEEP]",
            "[KEYCODE_NUMPAD0]",
            "[KEYCODE_NUMPAD1]",
            "[KEYCODE_NUMPAD2]",
            "[KEYCODE_NUMPAD3]",
            "[KEYCODE_NUMPAD4]",
            "[KEYCODE_NUMPAD5]",
            "[KEYCODE_NUMPAD6]",
            "[KEYCODE_NUMPAD7]",
            "[KEYCODE_NUMPAD8]",
            "[KEYCODE_NUMPAD9]",
            "[KEYCODE_MULTIPLY]",
            "[KEYCODE_ADD]",
            "[KEYCODE_SEPARATOR]",
            "[KEYCODE_SUBTRACT]",
            "[KEYCODE_DECIMAL]",
            "[KEYCODE_DIVIDE]",
            "[KEYCODE_F1]",
            "[KEYCODE_F2]",
            "[KEYCODE_F3]",
            "[KEYCODE_F4]",
            "[KEYCODE_F5]",
            "[KEYCODE_F6]",
            "[KEYCODE_F7]",
            "[KEYCODE_F8]",
            "[KEYCODE_F9]",
            "[KEYCODE_F10]",
            "[KEYCODE_F11]",
            "[KEYCODE_F12]",
            "[KEYCODE_F13]",
            "[KEYCODE_F14]",
            "[KEYCODE_F15]",
            "[KEYCODE_F16]",
            "[KEYCODE_F17]",
            "[KEYCODE_F18]",
            "[KEYCODE_F19]",
            "[KEYCODE_F20]",
            "[KEYCODE_F21]",
            "[KEYCODE_F22]",
            "[KEYCODE_F23]",
            "[KEYCODE_F24]",
            "[KEYCODE_BACK]|[KEYCODE_F17]",
            "[KEYCODE_BACK]|[KEYCODE_F18]",
            "[KEYCODE_BACK]|[KEYCODE_F19]",
            "[KEYCODE_BACK]|[KEYCODE_F20]",
            "[KEYCODE_BACK]|[KEYCODE_F21]",
            "[KEYCODE_BACK]|[KEYCODE_F22]",
            "[KEYCODE_BACK]|[KEYCODE_F23]",
            "[KEYCODE_BACK]|[KEYCODE_F24]",
            "[KEYCODE_NUMLOCK]",
            "[KEYCODE_SCROLL]",
            "[KEYCODE_RBUTTON]|[KEYCODE_NUMLOCK]",
            "[KEYCODE_RBUTTON]|[KEYCODE_SCROLL]",
            "[KEYCODE_MBUTTON]|[KEYCODE_NUMLOCK]",
            "[KEYCODE_MBUTTON]|[KEYCODE_SCROLL]",
            "[KEYCODE_XBUTTON2]|[KEYCODE_NUMLOCK]",
            "[KEYCODE_XBUTTON2]|[KEYCODE_SCROLL]",
            "[KEYCODE_BACK]|[KEYCODE_NUMLOCK]",
            "[KEYCODE_BACK]|[KEYCODE_SCROLL]",
            "[KEYCODE_LINEFEED]|[KEYCODE_NUMLOCK]",
            "[KEYCODE_LINEFEED]|[KEYCODE_SCROLL]",
            "[KEYCODE_CLEAR]|[KEYCODE_NUMLOCK]",
            "[KEYCODE_CLEAR]|[KEYCODE_SCROLL]",
            "[KEYCODE_RBUTTON]|[KEYCODE_CLEAR]|[KEYCODE_NUMLOCK]",
            "[KEYCODE_RBUTTON]|[KEYCODE_CLEAR]|[KEYCODE_SCROLL]",
            "[KEYCODE_LSHIFTKEY]",
            "[KEYCODE_RSHIFTKEY]",
            "[KEYCODE_LCONTROLKEY]",
            "[KEYCODE_RCONTROLKEY]",
            "[KEYCODE_LMENU]",
            "[KEYCODE_RMENU]",
            "[KEYCODE_BROWSERBACK]",
            "[KEYCODE_BROWSERFORWARD]",
            "[KEYCODE_BROWSERREFRESH]",
            "[KEYCODE_BROWSERSTOP]",
            "[KEYCODE_BROWSERSEARCH]",
            "[KEYCODE_BROWSERFAVORITES]",
            "[KEYCODE_BROWSERHOME]",
            "[KEYCODE_VOLUMEMUTE]",
            "[KEYCODE_VOLUMEDOWN]",
            "[KEYCODE_VOLUMEUP]",
            "[KEYCODE_MEDIANEXTTRACK]",
            "[KEYCODE_MEDIAPREVIOUSTRACK]",
            "[KEYCODE_MEDIASTOP]",
            "[KEYCODE_MEDIAPLAYPAUSE]",
            "[KEYCODE_LAUNCHMAIL]",
            "[KEYCODE_SELECTMEDIA]",
            "[KEYCODE_LAUNCHAPPLICATION1]",
            "[KEYCODE_LAUNCHAPPLICATION2]",
            "[KEYCODE_BACK]|[KEYCODE_MEDIANEXTTRACK]",
            "[KEYCODE_BACK]|[KEYCODE_MEDIAPREVIOUSTRACK]",
            "[KEYCODE_OEM1]",
            "[KEYCODE_OEMPLUS]",
            "[KEYCODE_OEMCOMMA]",
            "[KEYCODE_OEMMINUS]",
            "[KEYCODE_OEMPERIOD]",
            "[KEYCODE_OEMQUESTION]",
            "[KEYCODE_OEMTILDE]",
            "[KEYCODE_LBUTTON]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_RBUTTON]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_CANCEL]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_MBUTTON]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_XBUTTON1]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_XBUTTON2]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_LBUTTON]|[KEYCODE_XBUTTON2]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_BACK]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_TAB]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_LINEFEED]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_LBUTTON]|[KEYCODE_LINEFEED]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_CLEAR]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_RETURN]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_RBUTTON]|[KEYCODE_CLEAR]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_RBUTTON]|[KEYCODE_RETURN]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_SHIFTKEY]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_CONTROLKEY]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_MENU]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_PAUSE]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_CAPITAL]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_KANAMODE]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_RBUTTON]|[KEYCODE_CAPITAL]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_JUNJAMODE]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_FINALMODE]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_HANJAMODE]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_RBUTTON]|[KEYCODE_FINALMODE]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_OEMOPENBRACKETS]",
            "[KEYCODE_OEM5]",
            "[KEYCODE_OEM6]",
            "[KEYCODE_OEM7]",
            "[KEYCODE_OEM8]",
            "[KEYCODE_SPACE]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_PAGEUP]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_OEMBACKSLASH]",
            "[KEYCODE_LBUTTON]|[KEYCODE_OEMBACKSLASH]",
            "[KEYCODE_HOME]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_PROCESSKEY]",
            "[KEYCODE_MBUTTON]|[KEYCODE_OEMBACKSLASH]",
            "[KEYCODE_PACKET]",
            "[KEYCODE_DOWN]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_SELECT]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_BACK]|[KEYCODE_OEMBACKSLASH]",
            "[KEYCODE_TAB]|[KEYCODE_OEMBACKSLASH]",
            "[KEYCODE_PRINTSCREEN]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_BACK]|[KEYCODE_PROCESSKEY]",
            "[KEYCODE_CLEAR]|[KEYCODE_OEMBACKSLASH]",
            "[KEYCODE_BACK]|[KEYCODE_PACKET]",
            "[KEYCODE_D0]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_D1]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_SHIFTKEY]|[KEYCODE_OEMBACKSLASH]",
            "[KEYCODE_CONTROLKEY]|[KEYCODE_OEMBACKSLASH]",
            "[KEYCODE_D4]|[KEYCODE_OEMTILDE]",
            "[KEYCODE_SHIFTKEY]|[KEYCODE_PROCESSKEY]",
            "[KEYCODE_ATTN]",
            "[KEYCODE_CRSEL]",
            "[KEYCODE_EXSEL]",
            "[KEYCODE_ERASEEOF]",
            "[KEYCODE_PLAY]",
            "[KEYCODE_ZOOM]",
            "[KEYCODE_NONAME]",
            "[KEYCODE_PA1]",
            "[KEYCODE_OEMCLEAR]",
            "[KEYCODE_LBUTTON]|[KEYCODE_OEMCLEAR]"
       };

		public static string[] MCECodes =
        {
            "[MCE_UNKNOWN]",
            "[MCE_OK]",
            "[MCE_CLEAR]",
            "[MCE_UP]",
            "[MCE_DOWN]",
            "[MCE_LEFT]",
            "[MCE_RIGHT]",
            "[MCE_DIGIT0]",
            "[MCE_DIGIT1]",
            "[MCE_DIGIT2]",
            "[MCE_DIGIT3]",
            "[MCE_DIGIT4]",
            "[MCE_DIGIT5]",
            "[MCE_DIGIT6]",
            "[MCE_DIGIT7]",
            "[MCE_DIGIT8]",
            "[MCE_DIGIT9]",
            "[MCE_SHIFT]",
            "[MCE_PREVIOUSTRACK]",
            "[MCE_NEXTTRACK]",
            "[MCE_MOREINFO]",
            "[MCE_START]",
            "[MCE_BACK]",
            "[MCE_DVDMENU]",
            "[MCE_LIVETV]",
            "[MCE_MYTV]",
            "[MCE_MYMUSIC]",
            "[MCE_RECORDEDTV]",
            "[MCE_MYPICTURES]",
            "[MCE_MYVIDEOS]",
            "[MCE_DVDANGLE]",
            "[MCE_DVDAUDIO]",
            "[MCE_DVDSUBTITLE]",
            "[MCE_TELETEXT]",
            "[MCE_RED]",
            "[MCE_GREEN]",
            "[MCE_YELLOW]",
            "[MCE_BLUE]",
            "[MCE_TVPOWER]",
            "[MCE_OEM1]",
            "[MCE_OEM2]",
            "[MCE_STANDBY]",
            "[MCE_GUIDE]",
            "[MCE_CHANNELUP]",
            "[MCE_CHANNELDOWN]",
            "[MCE_PLAY]",
            "[MCE_PAUSE]",
            "[MCE_RECORD]",
            "[MCE_FORWARD]",
            "[MCE_REWIND]",
            "[MCE_SKIP]",
            "[MCE_REPLAY]",
            "[MCE_STOP]",
            "[MCE_MUTE]",
            "[MCE_VOLUMEUP]",
            "[MCE_VOLUMEDOWN]"
        };

		private bool m_isDisposed = false;

		private Control m_control = null;

		private bool m_focused = false;
		private bool m_isActive = false;

		private List<RawDevice> m_rawDeviceList = null;
		private Dictionary<IntPtr, RawDevice> m_rawDeviceDictionary = null;

		public delegate void RawKeyHandler(object sender, RawKeyEventArgs e);
		public delegate void RawMouseHandler(object sender, RawMouseEventArgs e);

		public event RawKeyHandler RawKey = null;
		public event RawMouseHandler RawMouse = null;

		public RawInput(Control control)
		{
			// The usUsagePage is a value for the type of device. Normally we will use 1 here as that stands for "generic desktop controls" and covers all the usual input devices (see the tables below).
			// The usUsage value specifies the device within the "generic desktop controls" group. 2 is mouse, 4 is joystick, 6 is keyboard (for more see tables below)
			// dwFlags allows various flags to be specified. One useful one is RIDEV_NOLEGACY. If you specify this for the keyboard your program will no longer receive any messages like WM_KEYDOWN.
			// If you specify it for the mouse you will no longer get messages like WM_LBUTTONDOWN. This falg can be useful when writing a game running full screen however if you are testing in a
			// window I would suggest not to use them otherwise all menus etc. will not work.
			// Another potentially useful flag is RIDEV_DEVNOTIFY that provides notifications to your window (via WM_INPUT_DEVICE_CHANGE messages) when a device is hot plugged / removed.
			// hwndTarget can be used to restrict messages to a particular window. If this is NULL then the current window focus is used.

			// It was a simple case of adding the flag RIDEV_INPUTSINK to dwFlags.
			// My application isn't in the foreground - and this flag enables you to receive
			// system-wide input (your application need not be in focus to receive the input).

			m_control = control;

			//UsbNotification.RegisterUsbDeviceNotification(control.Handle);

			m_rawDeviceList = GetRawInputDeviceList();
			m_rawDeviceDictionary = new Dictionary<IntPtr, RawDevice>();

			foreach (RawDevice rawDevice in m_rawDeviceList)
				m_rawDeviceDictionary.Add(rawDevice.hDevice, rawDevice);

			for (int i = 0; i < m_rawDeviceList.Count; i++)
				Console.WriteLine("hDevice:0x{0:x} Name:{1} FriendlyName:{2} DeviceType:{3} IsRemoteDesktop:{4}", m_rawDeviceList[i].hDevice, m_rawDeviceList[i].Name, m_rawDeviceList[i].FriendlyName, m_rawDeviceList[i].DeviceType, m_rawDeviceList[i].IsRemoteDesktop);
		}

		~RawInput()
		{
			Dispose(false);
		}

		public void WndProc(object sender, WndProcEventArgs e)
		{
			switch (e.Message.Msg)
			{
				case Win32.WM_CREATE:
					/* Win32.RAWINPUTDEVICE[] rid = new Win32.RAWINPUTDEVICE[5];

					rid[0].usUsagePage = 0xFFBC;      // adds HID remote control
					rid[0].usUsage = 0x88;
					rid[0].dwFlags = Win32.RIDEV_INPUTSINK;
					rid[0].hwndTarget = m_control.Handle;

					rid[1].usUsagePage = 0x0C;      // adds HID remote control
					rid[1].usUsage = 0x01;
					rid[0].dwFlags = Win32.RIDEV_INPUTSINK;
					rid[0].hwndTarget = m_control.Handle;

					rid[2].usUsagePage = 0x0C;      // adds HID remote control
					rid[2].usUsage = 0x80;
					rid[0].dwFlags = Win32.RIDEV_INPUTSINK;
					rid[0].hwndTarget = m_control.Handle;

					rid[3].usUsagePage = 0x01;      // adds HID mouse with no legacy messages
					rid[3].usUsage = 0x02;
					rid[3].dwFlags = Win32.RIDEV_NOLEGACY;

					rid[4].usUsagePage = 0x01;      // adds HID keyboard with no legacy message
					rid[4].usUsage = 0x06;
					rid[4].dwFlags = Win32.RIDEV_NOLEGACY; */

					//{0x01,  0x04, 0, 0}, // Joystick
					//{0x01,  0x05, 0, 0}, // Gamepad
					//{0x01,  0x80, 0, 0}, // System Control
					//{0x0C,  0x01, 0, 0} // Consumer Audio Control

					// http://msdn.microsoft.com/en-us/library/ms996387.aspx
					// http://www.codeproject.com/Articles/185522/Using-the-Raw-Input-API-to-Process-Joystick-Input
					// http://msdn.microsoft.com/en-us/library/windows/desktop/bb417079.aspx

					Win32.RawInputDevice[] rid = new Win32.RawInputDevice[5];

					// MCE Remote
					rid[0].usUsagePage = 0xFFBC;
					rid[0].usUsage = 0x88;
					rid[0].dwFlags = Win32.RIDEV_INPUTSINK | Win32.RIDEV_DEVNOTIFY;
					rid[0].hwndTarget = m_control.Handle;

					rid[1].usUsagePage = 0x0C;
					rid[1].usUsage = 0x01;
					rid[1].dwFlags = Win32.RIDEV_INPUTSINK | Win32.RIDEV_DEVNOTIFY;
					rid[1].hwndTarget = m_control.Handle;

					rid[2].usUsagePage = 0x0C;
					rid[2].usUsage = 0x80;
					rid[2].dwFlags = Win32.RIDEV_INPUTSINK | Win32.RIDEV_DEVNOTIFY;
					rid[2].hwndTarget = m_control.Handle;

					// Keyboard
					rid[3].usUsagePage = Win32.HID_USAGE_PAGE_GENERIC;
					rid[3].usUsage = Win32.HID_USAGE_GENERIC_KEYBOARD;
					rid[3].dwFlags = Win32.RIDEV_INPUTSINK | Win32.RIDEV_DEVNOTIFY; // Win32.RIDEV_NOLEGACY;
					rid[3].hwndTarget = m_control.Handle;

					// Mouse
					rid[4].usUsagePage = Win32.HID_USAGE_PAGE_GENERIC;
					rid[4].usUsage = Win32.HID_USAGE_GENERIC_MOUSE;
					rid[4].dwFlags = Win32.RIDEV_INPUTSINK | Win32.RIDEV_DEVNOTIFY; // Win32.RIDEV_NOLEGACY;
					rid[4].hwndTarget = m_control.Handle;

					// Joystick
					//rid[5].usUsagePage = Win32.HID_USAGE_PAGE_GENERIC;
					//rid[5].usUsage = Win32.HID_USAGE_GENERIC_JOYSTICK;
					//rid[5].dwFlags = Win32.RIDEV_INPUTSINK | Win32.RIDEV_DEVNOTIFY; // Win32.RIDEV_NOLEGACY;
					//rid[5].hwndTarget = m_control.Handle;

					// Gamepad
					//rid[6].usUsagePage = Win32.HID_USAGE_PAGE_GENERIC;
					//rid[6].usUsage = Win32.HID_USAGE_GENERIC_GAMEPAD;
					//rid[6].dwFlags = Win32.RIDEV_INPUTSINK | Win32.RIDEV_DEVNOTIFY; // Win32.RIDEV_NOLEGACY;
					//rid[6].hwndTarget = m_control.Handle;

					if (Win32.RegisterRawInputDevices(rid, rid.Length, Marshal.SizeOf(rid[0])))
						Console.WriteLine("Raw Input Devices Registered OK");
					else
						Console.WriteLine("Error Registering Raw Input Devices!");
					break;
				case Win32.WM_SETFOCUS:
					{
						m_focused = true;
						break;
					}
				case Win32.WM_KILLFOCUS:
					{
						m_focused = false;
						break;
					}
				case Win32.WM_ACTIVATE:
				case Win32.WM_ACTIVATEAPP:
				case Win32.WM_NCACTIVATE:
					{
						int wParam = e.Message.WParam.ToInt32();
						m_isActive = (Win32.LOWORD(wParam) != Win32.WA_INACTIVE);
						//e.Message.Result = IntPtr.Zero;
						break;
					}
				case Win32.WM_INPUT_DEVICE_CHANGE:
					switch ((int)e.Message.WParam)
					{
						case Win32.GIDC_REMOVAL:
							//Console.WriteLine("GIDC_REMOVAL");

							m_rawDeviceList = GetRawInputDeviceList();

							m_rawDeviceDictionary = new Dictionary<IntPtr, RawDevice>();

							foreach (RawDevice rawDevice in m_rawDeviceList)
								m_rawDeviceDictionary.Add(rawDevice.hDevice, rawDevice);
							break;
						case Win32.GIDC_ARRIVAL:
							//Console.WriteLine("GIDC_ARRIVAL");

							m_rawDeviceList = GetRawInputDeviceList();

							m_rawDeviceDictionary = new Dictionary<IntPtr, RawDevice>();

							foreach (RawDevice rawDevice in m_rawDeviceList)
								m_rawDeviceDictionary.Add(rawDevice.hDevice, rawDevice);
							break;
					}
					break;
				/* case Win32.WM_DEVICECHANGE:
					switch ((int)e.Message.WParam)
					{
						case Win32.DBT_DEVICEREMOVECOMPLETE:
							//Output.WriteLine("DBT_DEVICEREMOVECOMPLETE");
							break;
						case Win32.DBT_DEVICEARRIVAL:
							//Output.WriteLine("DBT_DEVICEARRIVAL");
							break;
					}
					break; */
				case Win32.WM_APPCOMMAND:
					{
						int lParam = e.Message.LParam.ToInt32();

						switch ((lParam >> 16) & ~0xF000)
						{
							case 0x8:
							case 0x9:
							case 0xA:
								e.Handled = true;
								break;
						}
						/* int lParam = e.Message.LParam.ToInt32();
						RawDeviceType rawDeviceType = GetRawDeviceType(lParam);
						RemoteControlButton rcb = RemoteControlButton.Unknown;
						bool isForeground = (Win32.GetForegroundWindow() == m_control.Handle);

						if (TryProcessAppCommand(lParam, out rcb))
						{
							Output.WriteLine("WM_APPCOMMAND: {0}", rcb);

							string name = MCECodes[(int)rcb];

							if (RawKey != null)
								RawKey(this, new RawKeyEventArgs(null, name, rcb, false, m_hasFocus, m_isActive, isForeground));
						} */
						break;
					}
				/* case Win32.WM_KEYDOWN:
				case Win32.WM_KEYUP:
					{
						int wParam = e.Message.WParam.ToInt32();
						RawDeviceType rawDeviceType = GetRawDeviceType(wParam);
						RemoteControlButton rcb = RemoteControlButton.Unknown;
						bool isForeground = (Win32.GetForegroundWindow() == m_control.Handle);
						bool isDown = (e.Message.Msg == Win32.WM_KEYDOWN);

						if (TryProcessKey(wParam, out rcb))
						{
							string name = MCECodes[(int)rcb];

							Output.WriteLine("{0}: {1}", isDown ? "WM_KEYDOWN" : "WM_KEYUP", name);

							if (RawKey != null)
								RawKey(this, new RawKeyEventArgs(null, name, rcb, isDown, m_hasFocus, m_isActive, isForeground));
						}
						break;
					} */
				case Win32.WM_MOUSEMOVE:
					{
						//Point point = new Point(msg.LParam.ToInt32());

						break;
					}
				case Win32.WM_INPUT:
					{
						IntPtr hRawInput = e.Message.LParam;
						IntPtr buffer = IntPtr.Zero;
						int lParam = e.Message.LParam.ToInt32();
						int wParam = e.Message.WParam.ToInt32();
						int pcbSize = 0;
						//bool isForeground = (msg.WParam.ToInt32() == Win32.RIM_INPUT);
						bool isForeground = (Win32.GetForegroundWindow() == m_control.Handle);

						try
						{
							Win32.GetRawInputData(hRawInput, Win32.RID_INPUT, IntPtr.Zero, ref pcbSize, Marshal.SizeOf(typeof(Win32.RawInputHeader)));

							buffer = Marshal.AllocHGlobal(pcbSize);

							if (buffer == IntPtr.Zero)
								break;

							if (Win32.GetRawInputData(hRawInput, Win32.RID_INPUT, buffer, ref pcbSize, Marshal.SizeOf(typeof(Win32.RawInputHeader))) != pcbSize)
								break;

							if (pcbSize > Marshal.SizeOf(typeof(Win32.RawInput)))
								break;

							//Output.WriteLine("RAWINPUT: " + pcbSize +":"+ Marshal.SizeOf(typeof(Win32.RAWINPUT)));

							//if (pcbSize != Marshal.SizeOf(typeof(Win32.RAWINPUT)))
							//    break;

							Win32.RawInput rawInput = (Win32.RawInput)Marshal.PtrToStructure(buffer, typeof(Win32.RawInput));

							switch (rawInput.Header.dwType)
							{
								case Win32.RIM_TYPEHID:
									{
										RawDevice device = null;

										if (!m_rawDeviceDictionary.TryGetValue(rawInput.Header.hDevice, out device))
											break;

										//RawDeviceType deviceType = RawDeviceType.MCERemote;
										RemoteControlButton rcb = RemoteControlButton.Unknown;
										//int rawData = rawInput.Data.MCE.Byte0 | rawInput.Data.MCE.Byte1 << 8;

										if (TryProcessRawInput(rawInput.Data.MCE.Byte0, rawInput.Data.MCE.Byte1, out rcb))
										{
											//Console.WriteLine("WM_INPUT: {0}", rcb);

											string name = MCECodes[(int)rcb];

											if (RawKey != null)
												RawKey(this, new RawKeyEventArgs(device, name, rcb, false, m_focused, m_isActive, isForeground));
										}
										break;
									}
								case Win32.RIM_TYPEKEYBOARD:
									{
										RawDevice device = null;

										if (!m_rawDeviceDictionary.TryGetValue(rawInput.Header.hDevice, out device))
											break;

										Win32.RawKeyboard rawKeyboard = rawInput.Data.Keyboard;

										//RawDeviceType deviceType = RawDeviceType.Keyboard;
										bool keyUp = (rawKeyboard.Message == Win32.WM_KEYUP || rawKeyboard.Message == Win32.WM_SYSKEYUP);
										bool keyDown = (rawKeyboard.Message == Win32.WM_KEYDOWN || rawKeyboard.Message == Win32.WM_SYSKEYDOWN);
										//bool keyUp = ((rawKeyboard.Flags & Win32.RI_KEY_BREAK) == Win32.RI_KEY_BREAK);
										//bool keyDown = !((rawKeyboard.Flags & Win32.RI_KEY_BREAK) == Win32.RI_KEY_BREAK);
										bool isBreakBitSet = ((rawKeyboard.Flags & Win32.RI_KEY_BREAK) == Win32.RI_KEY_BREAK);
										bool isE0BitSet = ((rawKeyboard.Flags & Win32.RI_KEY_E0) == Win32.RI_KEY_E0);
										//bool keyHeld = (rawKeyboard.VirtualKey == keyDown);
										Keys keyCode = (Keys)rawKeyboard.VirtualKey;
										//bool isAltDown = (((ushort)Win32.GetAsyncKeyState((int)Keys.Menu) & 0x8000) != 0);
										//bool isControlDown = (((ushort)Win32.GetAsyncKeyState((int)Keys.ControlKey) & 0x8000) != 0);
										//bool isShiftDown = (((ushort)Win32.GetAsyncKeyState((int)Keys.ShiftKey) & 0x8000) != 0);
										bool isAltDown = device.Alt;
										bool isControlDown = device.Control;
										bool isShiftDown = device.Shift;
										bool isCapsLockDown = device.CapsLock;
										//bool isCapsLockDown = ((((ushort)Win32.GetAsyncKeyState((int)Keys.CapsLock)) & 0x8000) != 0);
										//bool isCapsLockDown = ((Win32.GetKeyState((int)Keys.CapsLock) & 0x0001) != 0);
										//bool isCapsLockDown = Control.IsKeyLocked(Keys.CapsLock);
										//char ch = (char)KeyCodes.VKKeyToAscii((uint)keyCode);
										char ch = KeyToChar(keyCode, rawKeyboard.MakeCode);
										//uint vkCode = Win32.MapVirtualKey(rawKeyboard.MakeCode, Win32.MAPVK_VSC_TO_VK_EX);

										if (keyCode > Keys.OemClear)
											break;

										if (rawInput.Header.hDevice == IntPtr.Zero)
										{
											if (keyCode == Keys.ControlKey)
												keyCode = Keys.Zoom;
										}
										else
										{
											switch (keyCode)
											{
												case Keys.ControlKey:
													isControlDown = keyDown;
													keyCode = (isE0BitSet ? Keys.RControlKey : Keys.LControlKey);
													break;
												case Keys.Menu:
													isAltDown = keyDown;
													keyCode = (isE0BitSet ? Keys.RMenu : Keys.LMenu);
													break;
												case Keys.ShiftKey:
													isShiftDown = keyDown;
													keyCode = (rawKeyboard.MakeCode == Win32.SC_SHIFT_R ? Keys.RShiftKey : Keys.LShiftKey);
													break;
												case Keys.CapsLock:
													isCapsLockDown = keyDown;
													break;
												default:
													break;
											}
										}

										device.Alt = isAltDown;
										device.Control = isControlDown;
										device.Shift = isShiftDown;
										device.CapsLock = isCapsLockDown;

										//Output.WriteLine(rawKeyboard.ToString());

										//if (OnRawInput != null)
										//    OnRawInput(this, KeyCodes.KeyToString(keyData));

										//string name = KeyCodes[(int)e.KeyCode];
										//string name = String.Format("KEYCODE_{0}", e.KeyCode.ToString().ToUpper());
										string name = GetKeyName(keyCode);

										/* if (keyDown)
										{
											Output.WriteLine("[KeyDown] KeyCode:{0} Alt:{1} Control:{2} Shift:{3} Char:'{4}'", name, isAltDown, isControlDown, isShiftDown, ch == '\0' ? '?' : ch);
											//Output.WriteLine("KeyDown: {0}", KeyCodes.KeyToString(keyData));
										}
										else if (keyUp)
										{
											Output.WriteLine("[KeyUp]   KeyCode:{0} Alt:{1} Control:{2} Shift:{3} Char:'{4}'", name, isAltDown, isControlDown, isShiftDown, ch == '\0' ? '?' : ch);
											//Output.WriteLine("KeyUp: {0}", KeyCodes.KeyToString(keyData));
										} */

										if (RawKey != null)
											RawKey(this, new RawKeyEventArgs(device, name, keyCode, isAltDown, isControlDown, isShiftDown, isCapsLockDown, ch, keyDown, m_focused, m_isActive, isForeground));

										break;
									}
								case Win32.RIM_TYPEMOUSE:
									{
										RawDevice device = null;

										if (!m_rawDeviceDictionary.TryGetValue(rawInput.Header.hDevice, out device))
											break;

										Win32.RawMouse rawMouse = rawInput.Data.Mouse;

										//RawDeviceType deviceType = RawDeviceType.Mouse;
										RawMouseEventType mouseEvent = RawMouseEventType.None;
										int deviceIndex = GetRawDeviceIndex(device);
										List<string> nameList = new List<string>();
										Point mousePoint = new Point();
										MouseButtons buttons = device.MouseButtons;
										Point lastPoint = new Point();
										int delta = 0;
										Point screenPoint = Cursor.Position;
										Point clientPoint = m_control.PointToClient(screenPoint);
										Screen screen = Screen.FromPoint(screenPoint);
										bool isDown = false;
										bool isAbsolute = ((rawMouse.Flags & Win32.RawMouseFlags.MoveAbsolute) != 0);

										// Movement
										if (rawMouse.Data.ButtonFlags == Win32.RawMouseButtons.None)
										{
											if (isAbsolute)
											{
												int lastX = rawMouse.LastX - device.MousePoint.X;
												int lastY = rawMouse.LastY - device.MousePoint.Y;

												if (lastX != 0)
												{
													nameList.Add(String.Format("[MOUSECODE_{0}_XAXIS_{1}_SWITCH]", deviceIndex + 1, lastX < 0 ? "NEG" : "POS"));

													mouseEvent = RawMouseEventType.MouseMove;

													isDown = true;
												}

												if (lastY != 0)
												{
													nameList.Add(String.Format("[MOUSECODE_{0}_YAXIS_{1}_SWITCH]", deviceIndex + 1, lastY < 0 ? "NEG" : "POS"));

													mouseEvent = RawMouseEventType.MouseMove;

													isDown = true;
												}

												mousePoint = new Point(lastX, lastY);
												lastPoint = new Point(rawMouse.LastX, rawMouse.LastY);

												device.MousePoint = lastPoint;
											}
											else
											{
												int lastX = device.MousePoint.X + rawMouse.LastX;
												int lastY = device.MousePoint.Y + rawMouse.LastY;

												if (rawMouse.LastX != 0)
												{
													nameList.Add(String.Format("[MOUSECODE_{0}_XAXIS_{1}_SWITCH]", deviceIndex + 1, rawMouse.LastX < 0 ? "NEG" : "POS"));

													mouseEvent = RawMouseEventType.MouseMove;
												}

												if (rawMouse.LastY != 0)
												{
													nameList.Add(String.Format("[MOUSECODE_{0}_YAXIS_{1}_SWITCH]", deviceIndex + 1, rawMouse.LastX < 0 ? "NEG" : "POS"));

													mouseEvent = RawMouseEventType.MouseMove;
												}

												mousePoint = new Point(lastX, lastY);
												lastPoint = new Point(rawMouse.LastX, rawMouse.LastY);

												device.MousePoint = lastPoint;
											}
										}
										else
										{
											// Left Button
											if ((rawMouse.Data.ButtonFlags & Win32.RawMouseButtons.LeftDown) != 0)
											{ nameList.Add(String.Format("[MOUSECODE_{0}_BUTTON1]", deviceIndex + 1)); buttons |= MouseButtons.Left; mouseEvent = RawMouseEventType.MouseButton; isDown = true; }
											else if ((rawMouse.Data.ButtonFlags & Win32.RawMouseButtons.LeftUp) != 0)
											{ nameList.Add(String.Format("[MOUSECODE_{0}_BUTTON1]", deviceIndex + 1)); buttons |= MouseButtons.Left; mouseEvent = RawMouseEventType.MouseButton; isDown = false; }
											// Right Button
											else if ((rawMouse.Data.ButtonFlags & Win32.RawMouseButtons.RightDown) != 0)
											{ nameList.Add(String.Format("[MOUSECODE_{0}_BUTTON2]", deviceIndex + 1)); buttons |= MouseButtons.Right; mouseEvent = RawMouseEventType.MouseButton; isDown = true; }
											else if ((rawMouse.Data.ButtonFlags & Win32.RawMouseButtons.RightUp) != 0)
											{ nameList.Add(String.Format("[MOUSECODE_{0}_BUTTON2]", deviceIndex + 1)); buttons |= MouseButtons.Right; mouseEvent = RawMouseEventType.MouseButton; isDown = false; }
											// Middle Butotn
											else if ((rawMouse.Data.ButtonFlags & Win32.RawMouseButtons.MiddleDown) != 0)
											{ nameList.Add(String.Format("[MOUSECODE_{0}_BUTTON3]", deviceIndex + 1)); buttons |= MouseButtons.Middle; mouseEvent = RawMouseEventType.MouseButton; isDown = true; }
											else if ((rawMouse.Data.ButtonFlags & Win32.RawMouseButtons.MiddleUp) != 0)
											{ nameList.Add(String.Format("[MOUSECODE_{0}_BUTTON3]", deviceIndex + 1)); buttons |= MouseButtons.Middle; mouseEvent = RawMouseEventType.MouseButton; isDown = false; }
											// Button 4
											else if ((rawMouse.Data.ButtonFlags & Win32.RawMouseButtons.Button4Down) != 0)
											{ nameList.Add(String.Format("[MOUSECODE_{0}_BUTTON4]", deviceIndex + 1)); buttons |= MouseButtons.XButton1; mouseEvent = RawMouseEventType.MouseButton; isDown = true; }
											else if ((rawMouse.Data.ButtonFlags & Win32.RawMouseButtons.Button4Up) != 0)
											{ nameList.Add(String.Format("[MOUSECODE_{0}_BUTTON4]", deviceIndex + 1)); buttons |= MouseButtons.XButton1; mouseEvent = RawMouseEventType.MouseButton; isDown = false; }
											// Button 5
											else if ((rawMouse.Data.ButtonFlags & Win32.RawMouseButtons.Button5Down) != 0)
											{ nameList.Add(String.Format("[MOUSECODE_{0}_BUTTON5]", deviceIndex + 1)); buttons |= MouseButtons.XButton2; mouseEvent = RawMouseEventType.MouseButton; isDown = true; }
											else if ((rawMouse.Data.ButtonFlags & Win32.RawMouseButtons.Button5Up) != 0)
											{ nameList.Add(String.Format("[MOUSECODE_{0}_BUTTON5]", deviceIndex + 1)); buttons |= MouseButtons.XButton2; mouseEvent = RawMouseEventType.MouseButton; isDown = false; }
											// Mouse Wheel
											else if ((rawMouse.Data.ButtonFlags & Win32.RawMouseButtons.MouseWheel) != 0)
											{
												if ((short)rawMouse.Data.ButtonData > 0)
												{
													nameList.Add(String.Format("[MOUSECODE_{0}_ZAXIS_POS_SWITCH]", deviceIndex + 1));

													mouseEvent = RawMouseEventType.MouseWheel;

													delta++;

													isDown = true;
												}

												if ((short)rawMouse.Data.ButtonData < 0)
												{
													nameList.Add(String.Format("[MOUSECODE_{0}_ZAXIS_NEG_SWITCH]", deviceIndex + 1));

													mouseEvent = RawMouseEventType.MouseWheel;

													delta--;

													isDown = true;
												}

												device.MouseDelta = (short)rawMouse.Data.ButtonData;
											}
										}

										bool isVirtualDesktop = ((rawMouse.Flags & Win32.RawMouseFlags.VirtualDesktop) != 0);

										m_rawDeviceDictionary[rawInput.Header.hDevice] = device;
										m_rawDeviceList[m_rawDeviceList.IndexOf(device)] = device;

										string name = String.Join("|", nameList.ToArray());

										if (RawMouse != null)
											RawMouse(this, new RawMouseEventArgs(device, deviceIndex, name, mousePoint, delta, buttons, lastPoint, screenPoint, clientPoint, screen, isDown, isAbsolute, isVirtualDesktop, m_focused, m_isActive, isForeground, mouseEvent));

										break;
									}
							}
						}
						finally
						{
							if (buffer != IntPtr.Zero)
								Marshal.FreeHGlobal(buffer);
						}
					}
					break;
			}
		}

		public List<RawDevice> GetRawInputDeviceList()
		{
			List<RawDevice> deviceList = new List<RawDevice>();
			IntPtr pRawInputDeviceList = IntPtr.Zero;
			IntPtr pData = IntPtr.Zero;

			try
			{
				uint deviceCount = 0;
				int dwSize = (Marshal.SizeOf(typeof(Win32.RawInputDeviceList)));

				if (Win32.GetRawInputDeviceList(IntPtr.Zero, ref deviceCount, (uint)dwSize) != 0)
					return deviceList;

				pRawInputDeviceList = Marshal.AllocHGlobal((int)(dwSize * deviceCount));

				Win32.GetRawInputDeviceList(pRawInputDeviceList, ref deviceCount, (uint)dwSize);

				for (int i = 0; i < deviceCount; i++)
				{
					string deviceName = String.Empty, friendlyName = String.Empty;
					RawDeviceType deviceType = RawDeviceType.Keyboard;
					bool isRemoteDesktop = false;
					bool isKeyboard = false;
					Win32.RawInputDeviceList rid = (Win32.RawInputDeviceList)Marshal.PtrToStructure(new IntPtr((pRawInputDeviceList.ToInt64() + (dwSize * i))), typeof(Win32.RawInputDeviceList));

					deviceType = (RawDeviceType)rid.dwType;
					uint pcbSize = 0;

					Win32.GetRawInputDeviceInfo(rid.hDevice, Win32.RIDI_DEVICENAME, IntPtr.Zero, ref pcbSize);

					pData = Marshal.AllocHGlobal((int)pcbSize);

					uint retVal = Win32.GetRawInputDeviceInfo(rid.hDevice, Win32.RIDI_DEVICENAME, pData, ref pcbSize);

					if ((int)retVal != -1)
					{
						deviceName = (string)Marshal.PtrToStringAnsi(pData);

						if (deviceName.Contains("RDP_MOU") || deviceName.Contains("RDP_KBD"))
							isRemoteDesktop = true;

						string item = deviceName.Substring(4);
						string[] splitArray = item.Split('#');

						if (splitArray.Length < 3)
							continue;

						string id1 = splitArray[0];    // ACPI (Class code)
						string id2 = splitArray[1];    // PNP0303 (SubClass code)
						string id3 = splitArray[2];    // 3&13c0b0c5&0 (Protocol code)

						// @keyboard.inf,%*pnp0303.devicedesc%;Standard PS/2 Keyboard
						// http://nate.deepcreek.org.au/svn/KeyboardRedirector/trunk/KeyboardRedirector/InputDevice.cs

						// {4D36E96F-E325-11CE-BFC1-08002BE10318} Mouse
						// {4D36E96B-E325-11CE-BFC1-08002BE10318} Keyboard

						RegistryKey RegKey = Registry.LocalMachine.OpenSubKey(String.Format(@"System\CurrentControlSet\Enum\{0}\{1}\{2}", id1, id2, id3));
						friendlyName = (string)RegKey.GetValue("DeviceDesc", "");
						string classGUID = (string)RegKey.GetValue("ClassGUID", "");
						//string deviceClass = (string)RegKey.GetValue("Class", "");
						RegKey.Close();

						friendlyName = friendlyName.Split(';')[1];
						//isKeyboard = deviceClass.ToUpper().Equals("KEYBOARD");
						isKeyboard = classGUID.ToUpper().Equals("{4D36E96B-E325-11CE-BFC1-08002BE10318}");
					}
					else
					{
						if (System.Windows.Forms.SystemInformation.TerminalServerSession)
						{
							isRemoteDesktop = true;
							if (deviceType == RawDeviceType.Keyboard)
								friendlyName = "Terminal Server Keyboard Driver";
							else if (deviceType == RawDeviceType.Mouse)
								friendlyName = "Terminal Server Mouse Driver";
						}
					}

					//if (!deviceName.ToUpper().Contains("ROOT")) // Terminal services
					deviceList.Add(new RawDevice(rid.hDevice, deviceName, friendlyName, deviceType, isRemoteDesktop));

					if (pData != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(pData);
						pData = IntPtr.Zero;
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetRawInputDeviceName", "RawInput", ex.Message, ex.StackTrace);
			}
			finally
			{
				if (pData != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(pData);
					pData = IntPtr.Zero;
				}

				if (pRawInputDeviceList != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(pRawInputDeviceList);
					pRawInputDeviceList = IntPtr.Zero;
				}
			}

			return deviceList;
		}

		public string GetKeyName(Keys key)
		{
			string keyString = key.ToString();
			string[] splitArray = keyString.Split(new string[] { ", " }, StringSplitOptions.None);

			for (int i = 0; i < splitArray.Length; i++)
				splitArray[i] = String.Format("[KEYCODE_{0}]", splitArray[i].ToUpper());

			return String.Join("|", splitArray);
		}

		private char KeyToChar(Keys key, uint scanCode)
		{
			var buffer = new StringBuilder(256);
			var keyboardState = new byte[256];
			char ch = '\0';

			// Force the keyboard status cache to update
			Win32.GetKeyState((int)Keys.None);

			if (Win32.GetKeyboardState(keyboardState))
				Win32.ToUnicode((uint)key, scanCode, keyboardState, buffer, 64, 0);

			if (buffer.Length != 1)
				return ch;

			ch = buffer.ToString()[0];

			return ch;
		}

		/* private char KeyToChar(Keys key, bool isShiftDown, bool isAltDown, bool isCapsLockDown)
		{
			var buffer = new StringBuilder(256);
			var keyboardState = new byte[256];
			char ch = '\0';

			if (isShiftDown)
				keyboardState[(int)Keys.ShiftKey] = 0xFF;

			if (isAltDown)
			{
				keyboardState[(int)Keys.ControlKey] = 0xFF;
				keyboardState[(int)Keys.Menu] = 0xFF;
			}

			Win32.ToUnicode((uint)key, 0, keyboardState, buffer, 256, 0);

			if (buffer.Length != 1)
				return ch;

			ch = buffer.ToString()[0];

			if (isCapsLockDown && Char.IsLetter(ch))
				ch = Char.ToUpper(ch);

			return ch;
		} */

		private RawDeviceType GetRawDeviceType(int param)
		{
			RawDeviceType rawDeviceType;

			switch ((int)(((ushort)(param >> 16)) & Win32.FAPPCOMMAND_MASK))
			{
				case Win32.FAPPCOMMAND_MOUSE:
					rawDeviceType = RawDeviceType.Mouse;
					break;
				case Win32.FAPPCOMMAND_KEY:
					rawDeviceType = RawDeviceType.Keyboard;
					break;
				default:
					rawDeviceType = RawDeviceType.HID;
					break;
			}

			return rawDeviceType;
		}

		private int GetRawDeviceIndex(RawDevice rawDevice)
		{
			int rawDeviceIndex = 0;

			for (int i = 0; i < m_rawDeviceList.Count; i++)
			{
				if (m_rawDeviceList[i].DeviceType != rawDevice.DeviceType)
					continue;

				if (m_rawDeviceList[i].hDevice == rawDevice.hDevice)
					break;

				rawDeviceIndex++;
			}

			return rawDeviceIndex;
		}

		private bool TryProcessKey(int param, out RemoteControlButton rcb)
		{
			rcb = RemoteControlButton.Unknown;

			switch (param)
			{
				case (int)Keys.Enter:
					rcb = RemoteControlButton.OK;
					break;
				case (int)Keys.Escape:
					rcb = RemoteControlButton.Clear;
					break;
				case (int)Keys.Up:
					rcb = RemoteControlButton.Up;
					break;
				case (int)Keys.Down:
					rcb = RemoteControlButton.Down;
					break;
				case (int)Keys.Left:
					rcb = RemoteControlButton.Left;
					break;
				case (int)Keys.Right:
					rcb = RemoteControlButton.Right;
					break;
				case (int)Keys.D0:
					rcb = RemoteControlButton.Digit0;
					break;
				case (int)Keys.D1:
					rcb = RemoteControlButton.Digit1;
					break;
				case (int)Keys.D2:
					rcb = RemoteControlButton.Digit2;
					break;
				case (int)Keys.D3:
					rcb = RemoteControlButton.Digit3;
					break;
				case (int)Keys.D4:
					rcb = RemoteControlButton.Digit4;
					break;
				case (int)Keys.D5:
					rcb = RemoteControlButton.Digit5;
					break;
				case (int)Keys.D6:
					rcb = RemoteControlButton.Digit6;
					break;
				case (int)Keys.D7:
					rcb = RemoteControlButton.Digit7;
					break;
				case (int)Keys.D8:
					rcb = RemoteControlButton.Digit8;
					break;
				case (int)Keys.D9:
					rcb = RemoteControlButton.Digit9;
					break;
				case (int)Keys.ShiftKey:
					rcb = RemoteControlButton.Shift;
					break;
			}

			if (rcb == RemoteControlButton.Unknown)
				return false;

			return true;
		}

		private bool TryProcessAppCommand(int param, out RemoteControlButton rcb)
		{
			rcb = RemoteControlButton.Unknown;

			int cmd = (int)(((ushort)(param >> 16)) & ~Win32.FAPPCOMMAND_MASK);

			switch (cmd)
			{
				case Win32.APPCOMMAND_BROWSER_BACKWARD:
					rcb = RemoteControlButton.Back;
					break;
				case Win32.APPCOMMAND_MEDIA_CHANNEL_DOWN:
					rcb = RemoteControlButton.ChannelDown;
					break;
				case Win32.APPCOMMAND_MEDIA_CHANNEL_UP:
					rcb = RemoteControlButton.ChannelUp;
					break;
				case Win32.APPCOMMAND_MEDIA_FAST_FORWARD:
					rcb = RemoteControlButton.Forward;
					break;
				case Win32.APPCOMMAND_VOLUME_MUTE:
					rcb = RemoteControlButton.Mute;
					break;
				case Win32.APPCOMMAND_MEDIA_PAUSE:
					rcb = RemoteControlButton.Pause;
					break;
				case Win32.APPCOMMAND_MEDIA_PLAY:
					rcb = RemoteControlButton.Play;
					break;
				case Win32.APPCOMMAND_MEDIA_RECORD:
					rcb = RemoteControlButton.Record;
					break;
				case Win32.APPCOMMAND_MEDIA_PREVIOUSTRACK:
					rcb = RemoteControlButton.PreviousTrack;
					break;
				case Win32.APPCOMMAND_MEDIA_REWIND:
					rcb = RemoteControlButton.Rewind;
					break;
				case Win32.APPCOMMAND_MEDIA_NEXTTRACK:
					rcb = RemoteControlButton.NextTrack;
					break;
				case Win32.APPCOMMAND_MEDIA_STOP:
					rcb = RemoteControlButton.Stop;
					break;
				case Win32.APPCOMMAND_VOLUME_DOWN:
					rcb = RemoteControlButton.VolumeDown;
					break;
				case Win32.APPCOMMAND_VOLUME_UP:
					rcb = RemoteControlButton.VolumeUp;
					break;
			}

			if (rcb == RemoteControlButton.Unknown)
				return false;

			return true;
		}

		private bool TryProcessRawInput(byte byte0, byte byte1, out RemoteControlButton rcb)
		{
			rcb = RemoteControlButton.Unknown;

			switch (byte1)
			{
				case Win32.RAWINPUT_MOREINFO:
					rcb = RemoteControlButton.MoreInfo;
					break;
				case Win32.RAWINPUT_START:
					rcb = RemoteControlButton.Start;
					break;
				case 0x24:
					switch (byte0)
					{
						case Win32.RAWINPUT_BACK:
							rcb = RemoteControlButton.Back;
							break;
						case Win32.RAWINPUT_DVDMENU:
							rcb = RemoteControlButton.DVDMenu;
							break;
						default:
							rcb = RemoteControlButton.Unknown;
							break;
					}
					break;
				case Win32.RAWINPUT_LIVETV:
					rcb = RemoteControlButton.LiveTV;
					break;
				case Win32.RAWINPUT_MYTV:
					rcb = RemoteControlButton.MyTV;
					break;
				case Win32.RAWINPUT_MYMUSIC:
					rcb = RemoteControlButton.MyMusic;
					break;
				case Win32.RAWINPUT_RECORDEDTV:
					rcb = RemoteControlButton.RecordedTV;
					break;
				case Win32.RAWINPUT_MYPICTURES:
					rcb = RemoteControlButton.MyPictures;
					break;
				case Win32.RAWINPUT_MYVIDEOS:
					rcb = RemoteControlButton.MyVideos;
					break;
				case Win32.RAWINPUT_DVDANGLE:
					rcb = RemoteControlButton.DVDAngle;
					break;
				case Win32.RAWINPUT_DVDAUDIO:
					rcb = RemoteControlButton.DVDAudio;
					break;
				case Win32.RAWINPUT_DVDSUBTITLE:
					rcb = RemoteControlButton.DVDSubtitle;
					break;
				case Win32.RAWINPUT_TELETEXT:
					rcb = RemoteControlButton.TeleText;
					break;
				case Win32.RAWINPUT_RED:
					rcb = RemoteControlButton.Red;
					break;
				case Win32.RAWINPUT_GREEN:
					rcb = RemoteControlButton.Green;
					break;
				case Win32.RAWINPUT_YELLOW:
					rcb = RemoteControlButton.Yellow;
					break;
				case Win32.RAWINPUT_BLUE:
					rcb = RemoteControlButton.Blue;
					break;
				case Win32.RAWINPUT_TVPOWER:
					rcb = RemoteControlButton.TVPower;
					break;
				case Win32.RAWINPUT_OEM1:
					rcb = RemoteControlButton.OEM1;
					break;
				case Win32.RAWINPUT_OEM2:
					rcb = RemoteControlButton.OEM2;
					break;
				case Win32.RAWINPUT_STANDBY:
					rcb = RemoteControlButton.StandBy;
					break;
				case Win32.RAWINPUT_GUIDE:
					rcb = RemoteControlButton.Guide;
					break;
				case Win32.RAWINPUT_CHANNELUP:
					rcb = RemoteControlButton.ChannelUp;
					break;
				case Win32.RAWINPUT_CHANNELDOWN:
					rcb = RemoteControlButton.ChannelDown;
					break;
				case Win32.RAWINPUT_PLAY:
					rcb = RemoteControlButton.Play;
					break;
				case Win32.RAWINPUT_PAUSE:
					rcb = RemoteControlButton.Pause;
					break;
				case Win32.RAWINPUT_RECORD:
					rcb = RemoteControlButton.Record;
					break;
				case Win32.RAWINPUT_FORWARD:
					rcb = RemoteControlButton.Forward;
					break;
				case Win32.RAWINPUT_REWIND:
					rcb = RemoteControlButton.Rewind;
					break;
				case Win32.RAWINPUT_SKIP:
					rcb = RemoteControlButton.Skip;
					break;
				case Win32.RAWINPUT_REPLAY:
					rcb = RemoteControlButton.Replay;
					break;
				case Win32.RAWINPUT_STOP:
					rcb = RemoteControlButton.Stop;
					break;
				case Win32.RAWINPUT_MUTE:
					rcb = RemoteControlButton.Mute;
					break;
				case Win32.RAWINPUT_VOLUMEUP:
					rcb = RemoteControlButton.VolumeUp;
					break;
				case Win32.RAWINPUT_VOLUMEDOWN:
					rcb = RemoteControlButton.VolumeDown;
					break;
				default:
					rcb = RemoteControlButton.Unknown;
					break;
			}

			if (rcb == RemoteControlButton.Unknown)
				return false;

			return true;
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

			m_isDisposed = true;
		}

		#endregion
	}

	public class WndProcEventArgs : EventArgs
	{
		public Message Message { get; set; }
		public bool Handled { get; set; }

		public WndProcEventArgs(Message message)
		{
			Message = message;
			Handled = false;
		}
	}

	public enum RawDeviceType
	{
		Mouse,
		Keyboard,
		Joystick,
		HID,
		MCERemote
	};

	public enum RemoteControlButton
	{
		Unknown,

		OK,     // Enter
		Clear,
		Up,
		Down,
		Left,
		Right,
		Digit0,
		Digit1,
		Digit2,
		Digit3,
		Digit4,
		Digit5,
		Digit6,
		Digit7,
		Digit8,
		Digit9,
		Shift, // (Used for * / # which is Shift+Digit3 / Shift+Digit3)

		PreviousTrack,
		NextTrack,

		MoreInfo,
		Start,
		Back,
		DVDMenu,
		LiveTV,
		MyTV,
		MyMusic,
		RecordedTV,
		MyPictures,
		MyVideos,
		DVDAngle,
		DVDAudio,
		DVDSubtitle,
		TeleText,
		Red,
		Green,
		Yellow,
		Blue,
		TVPower,
		OEM1,
		OEM2,
		StandBy,
		Guide,
		ChannelUp,
		ChannelDown,
		Play,
		Pause,
		Record,
		Forward,
		Rewind,
		Skip,
		Replay,
		Stop,
		Mute,
		VolumeUp,
		VolumeDown
	}

	public class RawDevice
	{
		public IntPtr hDevice;
		public string Name;
		public string FriendlyName;
		public RawDeviceType DeviceType;
		public bool IsRemoteDesktop;

		// Keep track of values
		public Point MousePoint = new Point();
		public int MouseDelta = 0;
		public MouseButtons MouseButtons;
		public bool Control = false;
		public bool Alt = false;
		public bool Shift = false;
		public bool CapsLock = false;

		public RawDevice(IntPtr device, string name, string friendlyName, RawDeviceType deviceType, bool isRemoteDesktop)
		{
			hDevice = device;
			Name = name;
			FriendlyName = friendlyName;
			DeviceType = deviceType;
			IsRemoteDesktop = isRemoteDesktop;
		}
	}

	/* public class RawKeyEventArgs : EventArgs
	{
		private bool m_handled;
		private readonly Keys m_keyData;
		private bool m_suppressKeyPress;

		public RawKeyEventArgs(Keys keyData)
		{
			this.m_keyData = keyData;
		}

		public Keys KeyCode
		{
			get
			{
				Keys keys = this.m_keyData & Keys.KeyCode;

				if (!Enum.IsDefined(typeof(Keys), (int)keys))
				{
					return Keys.None;
				}

				return keys;
			}
		}
		public Keys KeyData { get { return this.m_keyData; } }
		public int KeyValue { get { return (((int)this.m_keyData) & 0xffff); } }
		public Keys Modifiers { get { return (this.m_keyData & ~Keys.KeyCode); } }
		public virtual bool Shift { get { return ((this.m_keyData & Keys.Shift) == Keys.Shift); } }
		public virtual bool Alt { get { return ((this.m_keyData & Keys.Alt) == Keys.Alt); } }
		public bool Control { get { return ((this.m_keyData & Keys.Control) == Keys.Control); } }
		public bool Handled { get { return this.m_handled; } set { this.m_handled = value; } }
		public bool SuppressKeyPress
		{
			get
			{
				return this.m_suppressKeyPress;
			}
			set
			{
				this.m_suppressKeyPress = value;
				this.m_handled = value;
			}
		}
	}

	public class RawMouseEventArgs : EventArgs
	{
		private readonly MouseButtons m_button;
		private readonly int m_clicks;
		private readonly int m_delta;
		private readonly int m_x;
		private readonly int m_y;

		public RawMouseEventArgs(MouseButtons button, int clicks, int x, int y, int delta)
		{
			this.m_button = button;
			this.m_clicks = clicks;
			this.m_x = x;
			this.m_y = y;
			this.m_delta = delta;
		}

		public MouseButtons Button { get { return this.m_button; } }
		public int Clicks { get { return this.m_clicks; } }
		public int Delta { get { return this.m_delta; } }
		public Point Location { get { return new Point(this.m_x, this.m_y); } }
		public int X { get { return this.m_x; } }
		public int Y { get { return this.m_y; } }
	} */

	public class RawKeyEventArgs : EventArgs
	{
		public RawDevice Device;
		public string Name;
		public uint InputCode;
		public Keys KeyCode;
		public RemoteControlButton RemoteButton;
		public bool Alt;
		public bool Control;
		public bool Shift;
		public bool CapsLock;
		public char Char;
		public bool IsDown;
		public bool Focused = false;
		public bool IsActive = false;
		public bool IsForeground = false;
		public bool Handled = false;

		public RawKeyEventArgs(RawDevice device, string name, RemoteControlButton remoteButton, bool isDown, bool focused, bool isActive, bool isForeground)
		{
			Device = device;
			Name = name;
			RemoteButton = remoteButton;
			IsDown = isDown;
			Focused = focused;
			IsActive = isActive;
			IsForeground = isForeground;
		}

		public RawKeyEventArgs(RawDevice device, string name, Keys keyCode, bool alt, bool control, bool shift, bool capsLock, char ch, bool isDown, bool focused, bool isActive, bool isForeground)
		{
			Device = device;
			Name = name;
			KeyCode = keyCode;
			Alt = alt;
			Control = control;
			Shift = shift;
			CapsLock = capsLock;
			Char = ch;
			IsDown = isDown;
			Focused = focused;
			IsActive = isActive;
			IsForeground = isForeground;
		}

		public override string ToString()
		{
			return String.Format("{0} Device: {1} KeyCode:{2} Alt:{3} Control:{4} Shift:{5} Char:'{6}'", IsDown ? "[DOWN]" : "[  UP]", Device.FriendlyName, Name, Alt, Control, Shift, Char == '\0' ? '?' : Char);
		}
	}

	[Flags]
	public enum RawMouseEventType
	{
		None = 0,
		MouseButton,
		MouseMove,
		MouseWheel
	};

	public class RawMouseEventArgs : EventArgs
	{
		public RawDevice Device;
		public int DeviceIndex;
		public string Name;
		public Point MousePoint = new Point();
		public int Delta = 0;
		public MouseButtons Buttons;
		public Point LastPoint = new Point();
		public Point ScreenPoint = new Point();
		public Point ClientPoint = new Point();
		public Screen Screen = null;
		public bool IsDown = false;
		public bool IsAbsolute = false;
		public bool IsVirtualDesktop = false;
		public bool Focused = false;
		public bool IsActive = false;
		public bool IsForeground = false;
		public bool Handled = false;
		public RawMouseEventType EventType = RawMouseEventType.None;

		public RawMouseEventArgs(RawDevice device, int deviceIndex, string name, Point mousePoint, int delta, MouseButtons buttons, Point lastPoint, Point screenPoint, Point clientPoint, Screen screen, bool isDown, bool isAbsolute, bool isVirtualDesktop, bool focused, bool isActive, bool isForeground, RawMouseEventType eventType)
		{
			Device = device;
			DeviceIndex = deviceIndex;
			Name = name;
			MousePoint = mousePoint;
			Delta = delta;
			Buttons = buttons;
			LastPoint = lastPoint;
			ScreenPoint = screenPoint;
			ClientPoint = clientPoint;
			Screen = screen;
			IsDown = isDown;
			IsAbsolute = isAbsolute;
			IsVirtualDesktop = isVirtualDesktop;
			Focused = focused;
			IsActive = isActive;
			IsForeground = isForeground;
			EventType = eventType;
		}
	}

	public partial class Win32
	{
		public const int WM_CREATE = 0x0001;
		public const int WM_DESTROY = 0x0002;
		public const int WM_ACTIVATE = 0x0006;
		public const int WM_SETFOCUS = 0x0007;
		public const int WM_KILLFOCUS = 0x0008;
		//public const int WM_PAINT = 0x000F;
		//public const int WM_CLOSE = 0x0010;
		public const int WM_SHOWWINDOW = 0x0018;
		public const int WM_COMMAND = 0x0111;

		public const int WM_ACTIVATEAPP = 0x001C;
		public const int WM_NCACTIVATE = 0x0086;

		public const int WA_INACTIVE = 0x0;
		public const int WA_ACTIVE = 0x1;
		public const int WA_CLICKACTIVE = 0x2;

		//public const int WM_KEYDOWN = 0x100;
		//public const int WM_KEYUP = 0x0101;
		//public const int WM_SYSKEYDOWN = 0x104;
		public const int WM_SYSKEYUP = 0x105;

		public const int WM_LEFTBUTTONDOWN = 0x0201;
		public const int WM_LEFTBUTTONUP = 0x0202;
		//public const int WM_MOUSEMOVE = 0x0200;
		//public const int WM_MOUSEWHEEL = 0x020A;
		public const int WM_RIGHTBUTTONDOWN = 0x0204;
		public const int WM_RIGHTBUTTONUP = 0x0205;

		public const int WM_APPCOMMAND = 0x319;
		public const int WM_INPUT = 0x00FF;

		public const int WM_INPUT_DEVICE_CHANGE = 0x00FE;

		public const int APPCOMMAND_BROWSER_BACKWARD = 1;
		public const int APPCOMMAND_VOLUME_MUTE = 8;
		public const int APPCOMMAND_VOLUME_DOWN = 9;
		public const int APPCOMMAND_VOLUME_UP = 10;
		public const int APPCOMMAND_MEDIA_NEXTTRACK = 11;
		public const int APPCOMMAND_MEDIA_PREVIOUSTRACK = 12;
		public const int APPCOMMAND_MEDIA_STOP = 13;
		public const int APPCOMMAND_MEDIA_PLAY_PAUSE = 14;
		public const int APPCOMMAND_MEDIA_PLAY = 46;
		public const int APPCOMMAND_MEDIA_PAUSE = 47;
		public const int APPCOMMAND_MEDIA_RECORD = 48;
		public const int APPCOMMAND_MEDIA_FAST_FORWARD = 49;
		public const int APPCOMMAND_MEDIA_REWIND = 50;
		public const int APPCOMMAND_MEDIA_CHANNEL_UP = 51;
		public const int APPCOMMAND_MEDIA_CHANNEL_DOWN = 52;

		/* public const int RAWINPUT_DETAILS = 0x209;
		public const int RAWINPUT_GUIDE = 0x8D;
		public const int RAWINPUT_TVJUMP = 0x25;
		public const int RAWINPUT_STANDBY = 0x82;
		public const int RAWINPUT_OEM1 = 0x80;
		public const int RAWINPUT_OEM2 = 0x81;
		public const int RAWINPUT_MYTV = 0x46;
		public const int RAWINPUT_MYVIDEOS = 0x4A;
		public const int RAWINPUT_MYPICTURES = 0x49;
		public const int RAWINPUT_MYMUSIC = 0x47;
		public const int RAWINPUT_RECORDEDTV = 0x48;
		public const int RAWINPUT_DVDANGLE = 0x4B;
		public const int RAWINPUT_DVDAUDIO = 0x4C;
		public const int RAWINPUT_DVDMENU = 0x24;
		public const int RAWINPUT_DVDSUBTITLE = 0x4D; */

		public const int RAWINPUT_MOREINFO = 0x9; //0x209;     // RAWINPUT_DETAILS (0x9)
		public const int RAWINPUT_START = 0xD;
		public const int RAWINPUT_BACK = 0x2; //0x224;
		public const int RAWINPUT_DVDMENU = 0x3; //0x324;
		public const int RAWINPUT_LIVETV = 0x25;        // RAWINPUT_TVJUMP
		public const int RAWINPUT_MYTV = 0x46;
		public const int RAWINPUT_MYMUSIC = 0x47;
		public const int RAWINPUT_RECORDEDTV = 0x48;
		public const int RAWINPUT_MYPICTURES = 0x49;
		public const int RAWINPUT_MYVIDEOS = 0x4A;
		public const int RAWINPUT_DVDANGLE = 0x4B;
		public const int RAWINPUT_DVDAUDIO = 0x4C;
		public const int RAWINPUT_DVDSUBTITLE = 0x4D;
		public const int RAWINPUT_TELETEXT = 0x5A;
		public const int RAWINPUT_RED = 0x5B;
		public const int RAWINPUT_GREEN = 0x5C;
		public const int RAWINPUT_YELLOW = 0x5D;
		public const int RAWINPUT_BLUE = 0x5E;
		public const int RAWINPUT_TVPOWER = 0x65;
		public const int RAWINPUT_OEM1 = 0x80;
		public const int RAWINPUT_OEM2 = 0x81;
		public const int RAWINPUT_STANDBY = 0x82;
		public const int RAWINPUT_GUIDE = 0x8D;
		public const int RAWINPUT_CHANNELUP = 0x9C;
		public const int RAWINPUT_CHANNELDOWN = 0x9D;
		public const int RAWINPUT_PLAY = 0xB0;
		public const int RAWINPUT_PAUSE = 0xB1;
		public const int RAWINPUT_RECORD = 0xB2;
		public const int RAWINPUT_FORWARD = 0xB3;
		public const int RAWINPUT_REWIND = 0xB4;
		public const int RAWINPUT_SKIP = 0xB5;
		public const int RAWINPUT_REPLAY = 0xB6;
		public const int RAWINPUT_STOP = 0xB7;
		public const int RAWINPUT_MUTE = 0xE2;
		public const int RAWINPUT_VOLUMEUP = 0xE9;
		public const int RAWINPUT_VOLUMEDOWN = 0xEA;

		public const int GIDC_ARRIVAL = 1;
		public const int GIDC_REMOVAL = 2;

		public const int RIDI_DEVICENAME = 0x20000007;
		public const int RIDI_DEVICEINFO = 0x2000000B;

		public const int RIM_INPUT = 0;

		public const int RIM_TYPEMOUSE = 0x0;
		public const int RIM_TYPEKEYBOARD = 0x1;
		public const int RIM_TYPEHID = 0x2;

		public const int RI_KEY_MAKE = 0x00;    // Key Down
		public const int RI_KEY_BREAK = 0x01;   // Key Up
		public const int RI_KEY_E0 = 0x02;      // Left version of the key
		public const int RI_KEY_E1 = 0x04;      // Right version of the key. Only seems to be set for the Pause/Break key.

		public const int FAPPCOMMAND_MASK = 0xF000;
		public const int FAPPCOMMAND_MOUSE = 0x8000;
		public const int FAPPCOMMAND_KEY = 0;
		public const int FAPPCOMMAND_OEM = 0x1000;

		public const int SC_SHIFT_R = 0x36;

		public const int RID_INPUT = 0x10000003;

		public const int RIDEV_NOLEGACY = 0x00000030;
		public const int RIDEV_INPUTSINK = 0x00000100;
		public const int RIDEV_DEVNOTIFY = 0x00002000;
		public const int RIDEV_REMOVE = 0x00000001;

		public const int HID_USAGE_PAGE_GENERIC = 0x01;

		public const int HID_USAGE_GENERIC_POINTER = 0x01;
		public const int HID_USAGE_GENERIC_MOUSE = 0x02;
		public const int HID_USAGE_GENERIC_JOYSTICK = 0x04;
		public const int HID_USAGE_GENERIC_GAMEPAD = 0x05;
		public const int HID_USAGE_GENERIC_KEYBOARD = 0x06;
		public const int HID_USAGE_GENERIC_KEYPAD = 0x07;
		public const int HID_USAGE_GENERIC_SYSTEM_CTL = 0x80;

		public const int HID_USAGE_GENERIC_X = 0x30;
		public const int HID_USAGE_GENERIC_Y = 0x31;
		public const int HID_USAGE_GENERIC_Z = 0x32;
		public const int HID_USAGE_GENERIC_RX = 0x33;
		public const int HID_USAGE_GENERIC_RY = 0x34;
		public const int HID_USAGE_GENERIC_RZ = 0x35;
		public const int HID_USAGE_GENERIC_SLIDER = 0x36;
		public const int HID_USAGE_GENERIC_DIAL = 0x37;
		public const int HID_USAGE_GENERIC_WHEEL = 0x38;
		public const int HID_USAGE_GENERIC_HATSWITCH = 0x39;
		public const int HID_USAGE_GENERIC_COUNTED_BUFFER = 0x3A;
		public const int HID_USAGE_GENERIC_BYTE_COUNT = 0x3B;
		public const int HID_USAGE_GENERIC_MOTION_WAKEUP = 0x3C;
		public const int HID_USAGE_GENERIC_VX = 0x40;
		public const int HID_USAGE_GENERIC_VY = 0x41;
		public const int HID_USAGE_GENERIC_VZ = 0x42;
		public const int HID_USAGE_GENERIC_VBRX = 0x43;
		public const int HID_USAGE_GENERIC_VBRY = 0x44;
		public const int HID_USAGE_GENERIC_VBRZ = 0x45;
		public const int HID_USAGE_GENERIC_VNO = 0x46;

		public const uint MAPVK_VK_TO_VSC = 0x00;
		public const uint MAPVK_VSC_TO_VK_EX = 0x03;

		public static short LOWORD(int n) { return (short)n; }
		public static short HIWORD(int n) { return (short)(n >> 16); }

		[Flags()]
		public enum RawMouseFlags : ushort
		{
			/// <summary>Relative to the last position.</summary>
			MoveRelative = 0,
			/// <summary>Absolute positioning.</summary>
			MoveAbsolute = 1,
			/// <summary>Coordinate data is mapped to a virtual desktop.</summary>
			VirtualDesktop = 2,
			/// <summary>Attributes for the mouse have changed.</summary>
			AttributesChanged = 4
		}

		[Flags()]
		public enum RawMouseButtons : ushort
		{
			/// <summary>No button.</summary>
			None = 0,
			/// <summary>Left (button 1) down.</summary>
			LeftDown = 0x0001,
			/// <summary>Left (button 1) up.</summary>
			LeftUp = 0x0002,
			/// <summary>Right (button 2) down.</summary>
			RightDown = 0x0004,
			/// <summary>Right (button 2) up.</summary>
			RightUp = 0x0008,
			/// <summary>Middle (button 3) down.</summary>
			MiddleDown = 0x0010,
			/// <summary>Middle (button 3) up.</summary>
			MiddleUp = 0x0020,
			/// <summary>Button 4 down.</summary>
			Button4Down = 0x0040,
			/// <summary>Button 4 up.</summary>
			Button4Up = 0x0080,
			/// <summary>Button 5 down.</summary>
			Button5Down = 0x0100,
			/// <summary>Button 5 up.</summary>
			Button5Up = 0x0200,
			/// <summary>Mouse wheel moved.</summary>
			MouseWheel = 0x0400
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct RawInputDevice
		{
			[MarshalAs(UnmanagedType.U2)]
			public ushort usUsagePage;
			[MarshalAs(UnmanagedType.U2)]
			public short usUsage;
			[MarshalAs(UnmanagedType.U4)]
			public int dwFlags;
			public IntPtr hwndTarget;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct RawKeyboard
		{
			/// <summary>Scan code for key depression.</summary>
			public ushort MakeCode;
			/// <summary>Scan code information.</summary>
			public ushort Flags;
			/// <summary>Reserved.</summary>
			public ushort Reserved;
			/// <summary>Virtual key code.</summary>
			public ushort VirtualKey;
			/// <summary>Corresponding window message.</summary>
			public uint Message;
			/// <summary>Extra information.</summary>
			public uint ExtraInformation;

			public override string ToString()
			{
				return string.Format("RAWKEYBOARD MakeCode: {0} Makecode(hex) : {0:X} Flags: {1} Reserved: {2} VKeyName: {3} Message: {4} ExtraInformation {5}", MakeCode, Flags, Reserved, VirtualKey, Message, ExtraInformation);
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct RawHID
		{
			public int dwSizeHid;
			public int dwCount;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct RawMCE
		{
			public byte Byte0;
			public byte Byte1;
			public byte Byte2;
		}

		/// <summary>
		/// Contains information about the state of the mouse.
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct RawMouse
		{
			/// <summary>
			/// The mouse state.
			/// </summary>
			public RawMouseFlags Flags;

			[StructLayout(LayoutKind.Explicit)]
			public struct MouseData
			{
				[FieldOffset(0)]
				public uint Buttons;
				/// <summary>
				/// If the mouse wheel is moved, this will contain the delta amount.
				/// </summary>
				[FieldOffset(2)]
				public ushort ButtonData;
				/// <summary>
				/// Flags for the event.
				/// </summary>
				[FieldOffset(0)]
				public RawMouseButtons ButtonFlags;
			}

			public MouseData Data;

			/// <summary>
			/// Raw button data.
			/// </summary>
			public uint RawButtons;
			/// <summary>
			/// The motion in the X direction. This is signed relative motion or
			/// absolute motion, depending on the value of usFlags.
			/// </summary>
			public int LastX;
			/// <summary>
			/// The motion in the Y direction. This is signed relative motion or absolute motion,
			/// depending on the value of usFlags.
			/// </summary>
			public int LastY;
			/// <summary>
			/// The device-specific additional information for the event.
			/// </summary>
			public uint ExtraInformation;
		}

		/// <summary>
		/// Contains the raw input from a device.
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct RawInput
		{
			/// <summary>
			/// Header for the data.
			/// </summary>
			public RawInputHeader Header;
			public Union Data;
			[StructLayout(LayoutKind.Explicit)]
			public struct Union
			{
				/// <summary>
				/// Mouse raw input data.
				/// </summary>
				[FieldOffset(0)]
				public RawMouse Mouse;
				/// <summary>
				/// Keyboard raw input data.
				/// </summary>
				[FieldOffset(0)]
				public RawKeyboard Keyboard;
				/// <summary>
				/// HID raw input data.
				/// </summary>
				[FieldOffset(0)]
				public RawHID HID;
				/// <summary>
				/// MCE raw input data.
				/// </summary>
				[FieldOffset(8)]
				public RawMCE MCE;
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct RawInputHeader
		{
			public uint dwType;			// Type of raw input (RIM_TYPEHID 2, RIM_TYPEKEYBOARD 1, RIM_TYPEMOUSE 0)
			public uint dwSize;			// Size in bytes of the entire input packet of data. This includes RAWINPUT plus possible extra input reports in the RAWHID variable length array. 
			public IntPtr hDevice;		// A handle to the device generating the raw input data. 
			public IntPtr wParam;		// RIM_INPUT 0 if input occurred while application was in the foreground else RIM_INPUTSINK 1 if it was not.

			public override string ToString()
			{
				return string.Format("RawInputHeader\n dwType : {0}\n dwSize : {1}\n hDevice : {2}\n wParam : {3}", dwType,
					dwSize, hDevice, wParam);
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct RawInputDeviceList
		{
			public IntPtr hDevice;
			[MarshalAs(UnmanagedType.U4)]
			public int dwType;
		}

		//[DllImport("user32.dll")]
		//public static extern IntPtr GetForegroundWindow();

		[DllImportAttribute("user32.dll")]
		public static extern bool RegisterRawInputDevices(RawInputDevice[] RIDs, int uiNumDevices, int cbSize);

		[DllImportAttribute("user32.dll")]
		public static extern int GetRawInputData(IntPtr hRawInput, int uiCommand, IntPtr pData, ref int byRefpcbSize, int cbSizeHeader);

		[DllImport("User32.dll")]
		public extern static uint GetRawInputDeviceList(IntPtr pRawInputDeviceList, ref uint uiNumDevices, uint cbSize);

		[DllImport("User32.dll")]
		public extern static uint GetRawInputDeviceInfo(IntPtr hDevice, uint uiCommand, IntPtr pData, ref uint pcbSize);

		/* [DllImport("user32.dll")]
		public static extern short GetKeyState(int vKey);

		[DllImport("user32.dll")]
		public static extern bool GetKeyboardState(byte[] lpKeyState);

		[DllImport("user32.dll")]
		public static extern short GetAsyncKeyState(int vKey); */

		[DllImport("user32.dll")]
		public static extern int GetKeyNameText(uint lParam, [Out] StringBuilder lpString, int nSize);

		[DllImport("user32.dll")]
		public static extern int ToUnicode(uint virtualKeyCode, uint scanCode, byte[] keyboardState, [Out, MarshalAs(UnmanagedType.LPWStr, SizeConst = 64)] StringBuilder receivingBuffer, int bufferSize, uint flags);
	}
}
