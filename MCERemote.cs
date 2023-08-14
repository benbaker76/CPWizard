// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CPWizard
{
	public class MCERemote
	{
	public static string[] MCECodes =
	{
		"[MCECODE_UNKNOWN]",
	    //-- WM_KEYDOWN type buttons (easy)
	    "[MCECODE_OK]",
		"[MCECODE_MOVEUP]",
		"[MCECODE_MOVELEFT]",
		"[MCECODE_MOVERIGHT]",
		"[MCECODE_MOVEDOWN]",
		"[MCECODE_BACK]",
		"[MCECODE_NUMBER1]",
		"[MCECODE_NUMBER2]",
		"[MCECODE_NUMBER3]",
		"[MCECODE_NUMBER4]",
		"[MCECODE_NUMBER5]",
		"[MCECODE_NUMBER6]",
		"[MCECODE_NUMBER7]",
		"[MCECODE_NUMBER8]",
		"[MCECODE_NUMBER9]",
		"[MCECODE_NUMBER0]",
		"[MCECODE_CLEAR]",
		"[MCECODE_ENTER]",
	    //-- WM_APPCOMMAND type buttons
	    "[MCECODE_PAUSE]",
		"[MCECODE_PLAY]",
		"[MCECODE_RECORD]",
		"[MCECODE_REPLAY]",
		"[MCECODE_REWIND]",
		"[MCECODE_FORWARD]",
		"[MCECODE_SKIP]",
		"[MCECODE_CHANNELDOWN]",
		"[MCECODE_CHANNELUP]",
		"[MCECODE_STOP]",
	    //-- WM_INPUT (HID) type buttons
	    "[MCECODE_START]",
		"[MCECODE_MOREINFO]",
		"[MCECODE_DVDANGLE]",
		"[MCECODE_DVDAUDIO]",
		"[MCECODE_DVDMENU]",
		"[MCECODE_DVDSUBTITLE]",
		"[MCECODE_TELETEXT]",
		"[MCECODE_RED]",
		"[MCECODE_GREEN]",
		"[MCECODE_YELLOW]",
		"[MCECODE_BLUE]",
		"[MCECODE_TVPOWER]",
		"[MCECODE_GUIDE]",
		"[MCECODE_MUTE]",
		"[MCECODE_MYTV]",
		"[MCECODE_MYMUSIC]",
		"[MCECODE_MYPICTURES]",
		"[MCECODE_MYVIDEOS]",
		"[MCECODE_RECORDEDTV]",
		"[MCECODE_OEM1]",
		"[MCECODE_OEM2]",
		"[MCECODE_LIVETV]",
		"[MCECODE_VOLUMEDOWN]",
		"[MCECODE_VOLUMEUP]"
	};

	private bool m_isDisposed = false;

	public delegate void MCEButtonDownHandler(object sender, MCECode mceCode);

	public event MCEButtonDownHandler MCEEvent = null;

	private Control m_control = null;

	public MCERemote(Control control)
	{
		m_control = control;
	}

	~MCERemote()
	{
		Dispose(false);
	}

	public string MCECodeToName(MCECode mceCode)
	{
		return MCECodes[(int)mceCode];
	}

	public MCECode MCENameToCode(string buttonName)
	{
		for (int i = 0; i < MCECodes.Length; i++)
			if (buttonName == MCECodes[i])
				return (MCECode)i;

		return MCECode.MCECODE_UNKNOWN;
	}

	private MCECode LookupKey(int intKeyCode)
	{
		switch (intKeyCode)
		{
			case 13:
				return MCECode.MCECODE_OK;
			case 27:
				return MCECode.MCECODE_CLEAR;
			case 37:
				return MCECode.MCECODE_MOVELEFT;
			case 38:
				return MCECode.MCECODE_MOVEUP;
			case 39:
				return MCECode.MCECODE_MOVERIGHT;
			case 40:
				return MCECode.MCECODE_MOVEDOWN;
			case 48:
				return MCECode.MCECODE_NUMBER0;
			case 49:
				return MCECode.MCECODE_NUMBER1;
			case 50:
				return MCECode.MCECODE_NUMBER2;
			case 51:
				return MCECode.MCECODE_NUMBER3;
			case 52:
				return MCECode.MCECODE_NUMBER4;
			case 53:
				return MCECode.MCECODE_NUMBER5;
			case 54:
				return MCECode.MCECODE_NUMBER6;
			case 55:
				return MCECode.MCECODE_NUMBER7;
			case 56:
				return MCECode.MCECODE_NUMBER8;
			case 57:
				return MCECode.MCECODE_NUMBER9;
			case 58:
				return MCECode.MCECODE_NUMBER0;
			default:
				return MCECode.MCECODE_UNKNOWN;
		}
	}

	private MCECode LookupRaw(byte intRawByte1, byte intRawByte2)
	{
		switch (intRawByte2)
		{
			case 9:
				return MCECode.MCECODE_MOREINFO;
			case 13:
				return MCECode.MCECODE_START;
			case 36:
				switch (intRawByte1)
				{
					case 2:
						return MCECode.MCECODE_BACK;
					case 3:
						return MCECode.MCECODE_DVDMENU;
					default:
						return MCECode.MCECODE_UNKNOWN;
				}
			case 37:
				return MCECode.MCECODE_LIVETV;
			case 70:
				return MCECode.MCECODE_MYTV;
			case 71:
				return MCECode.MCECODE_MYMUSIC;
			case 72:
				return MCECode.MCECODE_RECORDEDTV;
			case 73:
				return MCECode.MCECODE_MYPICTURES;
			case 74:
				return MCECode.MCECODE_MYVIDEOS;
			case 75:
				return MCECode.MCECODE_DVDANGLE;
			case 76:
				return MCECode.MCECODE_DVDAUDIO;
			case 77:
				return MCECode.MCECODE_DVDSUBTITLE;
			case 90:
				return MCECode.MCECODE_TELETEXT;
			case 91:
				return MCECode.MCECODE_RED;
			case 92:
				return MCECode.MCECODE_GREEN;
			case 93:
				return MCECode.MCECODE_YELLOW;
			case 94:
				return MCECode.MCECODE_BLUE;
			case 101:
				return MCECode.MCECODE_TVPOWER;
			case 128:
				return MCECode.MCECODE_OEM1;
			case 129:
				return MCECode.MCECODE_OEM2;
			case 141:
				return MCECode.MCECODE_GUIDE;
			case 156:
				return MCECode.MCECODE_CHANNELUP;
			case 157:
				return MCECode.MCECODE_CHANNELDOWN;
			case 176:
				return MCECode.MCECODE_PLAY;
			case 177:
				return MCECode.MCECODE_PAUSE;
			case 178:
				return MCECode.MCECODE_RECORD;
			case 179:
				return MCECode.MCECODE_FORWARD;
			case 180:
				return MCECode.MCECODE_REWIND;
			case 181:
				return MCECode.MCECODE_SKIP;
			case 182:
				return MCECode.MCECODE_REPLAY;
			case 183:
				return MCECode.MCECODE_STOP;
			case 226:
				return MCECode.MCECODE_MUTE;
			case 233:
				return MCECode.MCECODE_VOLUMEUP;
			case 234:
				return MCECode.MCECODE_VOLUMEDOWN;
			default:
				return MCECode.MCECODE_UNKNOWN;
		}
	}

	public void Initialize()
	{
		Win32.RawInputDevice[] objRID = new Win32.RawInputDevice[2];

		objRID[0].usUsagePage = 0xFFBC;
		objRID[0].usUsage = 0x88;
		objRID[0].dwFlags = Win32.RIDEV_INPUTSINK;
		objRID[0].hwndTarget = m_control.Handle;

		objRID[1].usUsagePage = 0xC;
		objRID[1].usUsage = 0x1;
		objRID[1].dwFlags = Win32.RIDEV_INPUTSINK;
		objRID[1].hwndTarget = m_control.Handle;

		if (Win32.RegisterRawInputDevices(objRID, objRID.Length, Marshal.SizeOf(objRID[0])))
			System.Diagnostics.Debug.WriteLine("Raw Input Devices Registered OK");
		else
			System.Diagnostics.Debug.WriteLine("Error Registering Raw Input Devices!");
	}

	public void WndProc(ref Message msg)
	{
		switch (msg.Msg)
		{
			case Win32.WM_KEYDOWN:
				{
					MCECode mceCode = LookupKey(msg.WParam.ToInt32());

					if (mceCode != MCECode.MCECODE_UNKNOWN)
					{
						if (MCEEvent != null)
							MCEEvent(this, mceCode);
					}
				}
				break;
			case Win32.WM_INPUT:
				{
					IntPtr buffer = IntPtr.Zero;
					int pcbSize = 0;

					try
					{
						Win32.GetRawInputData(msg.LParam, Win32.RID_INPUT, IntPtr.Zero, ref pcbSize, Marshal.SizeOf(typeof(Win32.RawInputHeader)));

						buffer = Marshal.AllocHGlobal(pcbSize);
						Win32.GetRawInputData(msg.LParam, Win32.RID_INPUT, buffer, ref pcbSize, Marshal.SizeOf(typeof(Win32.RawInputHeader)));

						Win32.RawInput rawInput = (Win32.RawInput)Marshal.PtrToStructure(buffer, typeof(Win32.RawInput));

						MCECode mceCode = MCECode.MCECODE_UNKNOWN;

						if (rawInput.Header.dwType == Win32.RIM_TYPEHID)
							mceCode = LookupRaw(rawInput.Data.MCE.Byte0, rawInput.Data.MCE.Byte1);

						if (mceCode != MCECode.MCECODE_UNKNOWN)
						{
							if (MCEEvent != null)
								MCEEvent(this, mceCode);
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

public enum MCECode
{
	MCECODE_UNKNOWN,
	//-- WM_KEYDOWN type buttons (easy)
	MCECODE_OK,
	MCECODE_MOVEUP,
	MCECODE_MOVELEFT,
	MCECODE_MOVERIGHT,
	MCECODE_MOVEDOWN,
	MCECODE_BACK,
	MCECODE_NUMBER1,
	MCECODE_NUMBER2,
	MCECODE_NUMBER3,
	MCECODE_NUMBER4,
	MCECODE_NUMBER5,
	MCECODE_NUMBER6,
	MCECODE_NUMBER7,
	MCECODE_NUMBER8,
	MCECODE_NUMBER9,
	MCECODE_NUMBER0,
	MCECODE_CLEAR,
	MCECODE_ENTER,
	//-- WM_APPCOMMAND type buttons
	MCECODE_PAUSE,
	MCECODE_PLAY,
	MCECODE_RECORD,
	MCECODE_REPLAY,
	MCECODE_REWIND,
	MCECODE_FORWARD,
	MCECODE_SKIP,
	MCECODE_CHANNELDOWN,
	MCECODE_CHANNELUP,
	MCECODE_STOP,
	//-- WM_INPUT (HID) type buttons
	MCECODE_START,
	MCECODE_MOREINFO,
	MCECODE_DVDANGLE,
	MCECODE_DVDAUDIO,
	MCECODE_DVDMENU,
	MCECODE_DVDSUBTITLE,
	MCECODE_TELETEXT,
	MCECODE_RED,
	MCECODE_GREEN,
	MCECODE_YELLOW,
	MCECODE_BLUE,
	MCECODE_TVPOWER,
	MCECODE_GUIDE,
	MCECODE_MUTE,
	MCECODE_MYTV,
	MCECODE_MYMUSIC,
	MCECODE_MYPICTURES,
	MCECODE_MYVIDEOS,
	MCECODE_RECORDEDTV,
	MCECODE_OEM1,
	MCECODE_OEM2,
	MCECODE_LIVETV,
	MCECODE_VOLUMEDOWN,
	MCECODE_VOLUMEUP
};

	public partial class Win32
	{
		/* public const int WM_KEYDOWN = 256;
		public const int WM_APPCOMMAND = 793;
		public const int WM_INPUT = 255;
		public const int RID_INPUT = 268435459;
		public const int RIM_TYPEMOUSE = 0;
		public const int RIM_TYPEKEYBOARD = 1;
		public const int RIM_TYPEHID = 2;
		public const int RIDEV_INPUTSINK = 0x00000100;

		[DllImportAttribute("user32.dll")]
		public static extern bool RegisterRawInputDevices(RAWINPUTDEVICE[] RIDs, int uiNumDevices, int cbSize);

		[DllImportAttribute("user32.dll")]
		public static extern int GetRawInputData(IntPtr hRawInput, int uiCommand, IntPtr pData, ref int byRefpcbSize, int cbSizeHeader);

		[StructLayout(LayoutKind.Sequential)]
		public struct RAWINPUTDEVICE
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
		public struct RAWINPUT
		{
			public RAWINPUTHEADER header;
			public RAWHID hid;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct RAWINPUTHEADER
		{
			[MarshalAs(UnmanagedType.U4)]
			public int dwType;
			[MarshalAs(UnmanagedType.U4)]
			public int dwSize;
			public IntPtr hDevice;
			[MarshalAs(UnmanagedType.U4)]
			public int wParam;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct RAWHID
		{
			public int dwSizeHid;
			public int dwCount;
		} */
	}
}