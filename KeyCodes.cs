// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace CPWizard
{
	public enum VKKeys : int
	{
		VK_LBUTTON = 0x01,
		VK_RBUTTON = 0x02,
		VK_CANCEL = 0x03,
		VK_MBUTTON = 0x04,
		VK_BACK = 0x08,
		VK_TAB = 0x09,
		VK_CLEAR = 0x0C,
		VK_RETURN = 0x0D,
		VK_SHIFT = 0x10,
		VK_CONTROL = 0x11,
		VK_MENU = 0x12,
		VK_PAUSE = 0x13,
		VK_CAPITAL = 0x14,
		VK_ESCAPE = 0x1B,
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
		VK_LWIN = 0x5B,
		VK_RWIN = 0x5C,
		VK_APPS = 0x5D,
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
		VK_NUMLOCK = 0x90,
		VK_SCROLL = 0x91,
		VK_LSHIFT = 0xA0,
		VK_RSHIFT = 0xA1,
		VK_LCONTROL = 0xA2,
		VK_RCONTROL = 0xA3,
		VK_LMENU = 0xA4,
		VK_RMENU = 0xA5,
		VK_PROCESSKEY = 0xE5,
		VK_ATTN = 0xF6,
		VK_CRSEL = 0xF7,
		VK_EXSEL = 0xF8,
		VK_EREOF = 0xF9,
		VK_PLAY = 0xFA,
		VK_ZOOM = 0xFB,
		VK_NONAME = 0xFC,
		VK_PA1 = 0xFD,
		VK_BROWSER_BACK = 0xA6,
		VK_BROWSER_FORWARD = 0xA7,
		VK_BROWSER_REFRESH = 0xA8,
		VK_BROWSER_STOP = 0xA9,
		VK_BROWSER_SEARCH = 0xAA,
		VK_BROWSER_FAVORITES = 0xAB,
		VK_BROWSER_HOME = 0xAC,
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
		VK_OEM_CLEAR = 0xFE,
		VK_OEM_1 = 0xBA,
		VK_OEM_PLUS = 0xBB,
		VK_OEM_COMMA = 0xBC,
		VK_OEM_MINUS = 0xBD,
		VK_OEM_PERIOD = 0xBE,
		VK_OEM_2 = 0xBF,
		VK_OEM_3 = 0xC0,
		VK_OEM_4 = 0xDB,
		VK_OEM_5 = 0xDC,
		VK_OEM_6 = 0xDD,
		VK_OEM_7 = 0xDE,
		VK_OEM_8 = 0xDF,
		VK_OEM_AX = 0xE1,
		VK_OEM_102 = 0xE1
	};

	public enum DIKeys : int
	{
		DIK_ESCAPE = 0x01,
		DIK_1 = 0x02,
		DIK_2 = 0x03,
		DIK_3 = 0x04,
		DIK_4 = 0x05,
		DIK_5 = 0x06,
		DIK_6 = 0x07,
		DIK_7 = 0x08,
		DIK_8 = 0x09,
		DIK_9 = 0x0A,
		DIK_0 = 0x0B,
		DIK_MINUS = 0x0C,    /* - on main keyboard */
		DIK_EQUALS = 0x0D,
		DIK_BACK = 0x0E,    /* backspace */
		DIK_TAB = 0x0F,
		DIK_Q = 0x10,
		DIK_W = 0x11,
		DIK_E = 0x12,
		DIK_R = 0x13,
		DIK_T = 0x14,
		DIK_Y = 0x15,
		DIK_U = 0x16,
		DIK_I = 0x17,
		DIK_O = 0x18,
		DIK_P = 0x19,
		DIK_LBRACKET = 0x1A,
		DIK_RBRACKET = 0x1B,
		DIK_RETURN = 0x1C,    /* Enter on main keyboard */
		DIK_LCONTROL = 0x1D,
		DIK_A = 0x1E,
		DIK_S = 0x1F,
		DIK_D = 0x20,
		DIK_F = 0x21,
		DIK_G = 0x22,
		DIK_H = 0x23,
		DIK_J = 0x24,
		DIK_K = 0x25,
		DIK_L = 0x26,
		DIK_SEMICOLON = 0x27,
		DIK_APOSTROPHE = 0x28,
		DIK_GRAVE = 0x29,    /* accent grave */
		DIK_LSHIFT = 0x2A,
		DIK_BACKSLASH = 0x2B,
		DIK_Z = 0x2C,
		DIK_X = 0x2D,
		DIK_C = 0x2E,
		DIK_V = 0x2F,
		DIK_B = 0x30,
		DIK_N = 0x31,
		DIK_M = 0x32,
		DIK_COMMA = 0x33,
		DIK_PERIOD = 0x34,    /* . on main keyboard */
		DIK_SLASH = 0x35,    /* / on main keyboard */
		DIK_RSHIFT = 0x36,
		DIK_MULTIPLY = 0x37,    /* * on numeric keypad */
		DIK_LMENU = 0x38,    /* left Alt */
		DIK_SPACE = 0x39,
		DIK_CAPITAL = 0x3A,
		DIK_F1 = 0x3B,
		DIK_F2 = 0x3C,
		DIK_F3 = 0x3D,
		DIK_F4 = 0x3E,
		DIK_F5 = 0x3F,
		DIK_F6 = 0x40,
		DIK_F7 = 0x41,
		DIK_F8 = 0x42,
		DIK_F9 = 0x43,
		DIK_F10 = 0x44,
		DIK_NUMLOCK = 0x45,
		DIK_SCROLL = 0x46,    /* Scroll Lock */
		DIK_NUMPAD7 = 0x47,
		DIK_NUMPAD8 = 0x48,
		DIK_NUMPAD9 = 0x49,
		DIK_SUBTRACT = 0x4A,    /* - on numeric keypad */
		DIK_NUMPAD4 = 0x4B,
		DIK_NUMPAD5 = 0x4C,
		DIK_NUMPAD6 = 0x4D,
		DIK_ADD = 0x4E,    /* + on numeric keypad */
		DIK_NUMPAD1 = 0x4F,
		DIK_NUMPAD2 = 0x50,
		DIK_NUMPAD3 = 0x51,
		DIK_NUMPAD0 = 0x52,
		DIK_DECIMAL = 0x53,    /* . on numeric keypad */
		DIK_OEM_102 = 0x56,    /* < > | on UK/Germany keyboards */
		DIK_F11 = 0x57,
		DIK_F12 = 0x58,
		DIK_F13 = 0x64,    /*                     (NEC PC98) */
		DIK_F14 = 0x65,    /*                     (NEC PC98) */
		DIK_F15 = 0x66,    /*                     (NEC PC98) */
		DIK_KANA = 0x70,    /* (Japanese keyboard)            */
		DIK_ABNT_C1 = 0x73,    /* / ? on Portugese (Brazilian) keyboards */
		DIK_CONVERT = 0x79,    /* (Japanese keyboard)            */
		DIK_NOCONVERT = 0x7B,    /* (Japanese keyboard)            */
		DIK_YEN = 0x7D,    /* (Japanese keyboard)            */
		DIK_ABNT_C2 = 0x7E,    /* Numpad . on Portugese (Brazilian) keyboards */
		DIK_NUMPADEQUALS = 0x8D,    /* = on numeric keypad (NEC PC98) */
		DIK_PREVTRACK = 0x90,    /* Previous Track (DIK_CIRCUMFLEX on Japanese keyboard) */
		DIK_AT = 0x91,    /*                     (NEC PC98) */
		DIK_COLON = 0x92,    /*                     (NEC PC98) */
		DIK_UNDERLINE = 0x93,    /*                     (NEC PC98) */
		DIK_KANJI = 0x94,    /* (Japanese keyboard)            */
		DIK_STOP = 0x95,    /*                     (NEC PC98) */
		DIK_AX = 0x96,    /*                     (Japan AX) */
		DIK_UNLABELED = 0x97,    /*                        (J3100) */
		DIK_NEXTTRACK = 0x99,    /* Next Track */
		DIK_NUMPADENTER = 0x9C,    /* Enter on numeric keypad */
		DIK_RCONTROL = 0x9D,
		DIK_MUTE = 0xA0,    /* Mute */
		DIK_CALCULATOR = 0xA1,    /* Calculator */
		DIK_PLAYPAUSE = 0xA2,    /* Play / Pause */
		DIK_MEDIASTOP = 0xA4,    /* Media Stop */
		DIK_VOLUMEDOWN = 0xAE,    /* Volume - */
		DIK_VOLUMEUP = 0xB0,    /* Volume + */
		DIK_WEBHOME = 0xB2,    /* Web home */
		DIK_NUMPADCOMMA = 0xB3,    /* , on numeric keypad (NEC PC98) */
		DIK_DIVIDE = 0xB5,    /* / on numeric keypad */
		DIK_SYSRQ = 0xB7,
		DIK_RMENU = 0xB8,    /* right Alt */
		DIK_PAUSE = 0xC5,    /* Pause */
		DIK_HOME = 0xC7,    /* Home on arrow keypad */
		DIK_UP = 0xC8,    /* UpArrow on arrow keypad */
		DIK_PRIOR = 0xC9,    /* PgUp on arrow keypad */
		DIK_LEFT = 0xCB,    /* LeftArrow on arrow keypad */
		DIK_RIGHT = 0xCD,    /* RightArrow on arrow keypad */
		DIK_END = 0xCF,    /* End on arrow keypad */
		DIK_DOWN = 0xD0,    /* DownArrow on arrow keypad */
		DIK_NEXT = 0xD1,    /* PgDn on arrow keypad */
		DIK_INSERT = 0xD2,    /* Insert on arrow keypad */
		DIK_DELETE = 0xD3,    /* Delete on arrow keypad */
		DIK_LWIN = 0xDB,    /* Left Windows key */
		DIK_RWIN = 0xDC,    /* Right Windows key */
		DIK_APPS = 0xDD,    /* AppMenu key */
		DIK_POWER = 0xDE,    /* System Power */
		DIK_SLEEP = 0xDF,    /* System Sleep */
		DIK_WAKE = 0xE3,    /* System Wake */
		DIK_WEBSEARCH = 0xE5,    /* Web Search */
		DIK_WEBFAVORITES = 0xE6,    /* Web Favorites */
		DIK_WEBREFRESH = 0xE7,    /* Web Refresh */
		DIK_WEBSTOP = 0xE8,    /* Web Stop */
		DIK_WEBFORWARD = 0xE9,    /* Web Forward */
		DIK_WEBBACK = 0xEA,    /* Web Back */
		DIK_MYCOMPUTER = 0xEB,    /* My Computer */
		DIK_MAIL = 0xEC,    /* Mail */
		DIK_MEDIASELECT = 0xED    /* Media Select */
	};

	public class KeyCodeNode
	{
		public string Name = String.Empty;
		public uint VKKey = 0;
		public uint DIKey = 0;
		public uint AsciiCode = 0;
		public int KeyRepeatCount = 0;

		public KeyCodeNode(string name, uint vkKey, uint dikKey, uint asciiCode)
		{
			Name = name;
			VKKey = vkKey;
			DIKey = dikKey;
			AsciiCode = asciiCode;
		}
	}

	public class KeyCodes
	{
		public static KeyCodeNode[] KeyCodeArray =
		{
			new KeyCodeNode("[KEYCODE_ESC]", (uint)Keys.Escape, (uint)DIKeys.DIK_ESCAPE, 27),
			new KeyCodeNode("[KEYCODE_1]", (uint)Keys.D1, (uint)DIKeys.DIK_1, 49),
			new KeyCodeNode("[KEYCODE_2]", (uint)Keys.D2, (uint)DIKeys.DIK_2, 50),
			new KeyCodeNode("[KEYCODE_3]", (uint)Keys.D3, (uint)DIKeys.DIK_3, 51),
			new KeyCodeNode("[KEYCODE_4]", (uint)Keys.D4, (uint)DIKeys.DIK_4, 52),
			new KeyCodeNode("[KEYCODE_5]", (uint)Keys.D5, (uint)DIKeys.DIK_5, 53),
			new KeyCodeNode("[KEYCODE_6]", (uint)Keys.D6, (uint)DIKeys.DIK_6, 54),
			new KeyCodeNode("[KEYCODE_7]", (uint)Keys.D7, (uint)DIKeys.DIK_7, 55),
			new KeyCodeNode("[KEYCODE_8]", (uint)Keys.D8, (uint)DIKeys.DIK_8, 56),
			new KeyCodeNode("[KEYCODE_9]", (uint)Keys.D9, (uint)DIKeys.DIK_9, 57),
			new KeyCodeNode("[KEYCODE_0]", (uint)Keys.D0, (uint)DIKeys.DIK_0, 48),
			new KeyCodeNode("[KEYCODE_MINUS]", (uint)Keys.OemMinus, (uint)DIKeys.DIK_MINUS, 45),
			new KeyCodeNode("[KEYCODE_EQUALS]", (uint)Keys.Oemplus, (uint)DIKeys.DIK_EQUALS, 61),
			new KeyCodeNode("[KEYCODE_BACKSPACE]", (uint)Keys.Back, (uint)DIKeys.DIK_BACK, 8),
			new KeyCodeNode("[KEYCODE_TAB]", (uint)Keys.Tab, (uint)DIKeys.DIK_TAB, 9),
			new KeyCodeNode("[KEYCODE_Q]", (uint)Keys.Q, (uint)DIKeys.DIK_Q, 81),
			new KeyCodeNode("[KEYCODE_W]", (uint)Keys.W, (uint)DIKeys.DIK_W, 87),
			new KeyCodeNode("[KEYCODE_E]", (uint)Keys.E, (uint)DIKeys.DIK_E, 69),
			new KeyCodeNode("[KEYCODE_R]", (uint)Keys.R, (uint)DIKeys.DIK_R, 82),
			new KeyCodeNode("[KEYCODE_T]", (uint)Keys.T, (uint)DIKeys.DIK_T, 84),
			new KeyCodeNode("[KEYCODE_Y]", (uint)Keys.Y, (uint)DIKeys.DIK_Y, 89),
			new KeyCodeNode("[KEYCODE_U]", (uint)Keys.U, (uint)DIKeys.DIK_U, 85),
			new KeyCodeNode("[KEYCODE_I]", (uint)Keys.I, (uint)DIKeys.DIK_I, 73),
			new KeyCodeNode("[KEYCODE_O]", (uint)Keys.O, (uint)DIKeys.DIK_O, 79),
			new KeyCodeNode("[KEYCODE_P]", (uint)Keys.P, (uint)DIKeys.DIK_P, 80),
			new KeyCodeNode("[KEYCODE_OPENBRACE]", (uint)Keys.OemOpenBrackets, (uint)DIKeys.DIK_LBRACKET, 91),
			new KeyCodeNode("[KEYCODE_CLOSEBRACE]", (uint)Keys.OemCloseBrackets, (uint)DIKeys.DIK_RBRACKET, 93),
			new KeyCodeNode("[KEYCODE_ENTER]", (uint)Keys.Return, (uint)DIKeys.DIK_RETURN, 13),
			new KeyCodeNode("[KEYCODE_LCONTROL]", (uint)Keys.LControlKey, (uint)DIKeys.DIK_LCONTROL, 0),
			new KeyCodeNode("[KEYCODE_A]", (uint)Keys.A, (uint)DIKeys.DIK_A, 65),
			new KeyCodeNode("[KEYCODE_S]", (uint)Keys.S, (uint)DIKeys.DIK_S, 83),
			new KeyCodeNode("[KEYCODE_D]", (uint)Keys.D, (uint)DIKeys.DIK_D, 68),
			new KeyCodeNode("[KEYCODE_F]", (uint)Keys.F, (uint)DIKeys.DIK_F, 70),
			new KeyCodeNode("[KEYCODE_G]", (uint)Keys.G, (uint)DIKeys.DIK_G, 71),
			new KeyCodeNode("[KEYCODE_H]", (uint)Keys.H, (uint)DIKeys.DIK_H, 72),
			new KeyCodeNode("[KEYCODE_J]", (uint)Keys.J, (uint)DIKeys.DIK_J, 74),
			new KeyCodeNode("[KEYCODE_K]", (uint)Keys.K, (uint)DIKeys.DIK_K, 75),
			new KeyCodeNode("[KEYCODE_L]", (uint)Keys.L, (uint)DIKeys.DIK_L, 76),
			new KeyCodeNode("[KEYCODE_COLON]", (uint)Keys.OemSemicolon, (uint)DIKeys.DIK_SEMICOLON, 59),
			new KeyCodeNode("[KEYCODE_QUOTE]", (uint)Keys.OemQuotes, (uint)DIKeys.DIK_APOSTROPHE, 39),
			new KeyCodeNode("[KEYCODE_TILDE]", (uint)Keys.Oemtilde, (uint)DIKeys.DIK_GRAVE, 96),
			new KeyCodeNode("[KEYCODE_LSHIFT]", (uint)Keys.LShiftKey, (uint)DIKeys.DIK_LSHIFT, 0),
			new KeyCodeNode("[KEYCODE_BACKSLASH]", (uint)Keys.OemPipe, (uint)DIKeys.DIK_BACKSLASH, 92),
			new KeyCodeNode("[KEYCODE_Z]", (uint)Keys.Z, (uint)DIKeys.DIK_Z, 90),
			new KeyCodeNode("[KEYCODE_X]", (uint)Keys.X, (uint)DIKeys.DIK_X, 88),
			new KeyCodeNode("[KEYCODE_C]", (uint)Keys.C, (uint)DIKeys.DIK_C, 67),
			new KeyCodeNode("[KEYCODE_V]", (uint)Keys.V, (uint)DIKeys.DIK_V, 86),
			new KeyCodeNode("[KEYCODE_B]", (uint)Keys.B, (uint)DIKeys.DIK_B, 66),
			new KeyCodeNode("[KEYCODE_N]", (uint)Keys.N, (uint)DIKeys.DIK_N, 78),
			new KeyCodeNode("[KEYCODE_M]", (uint)Keys.M, (uint)DIKeys.DIK_M, 77),
			new KeyCodeNode("[KEYCODE_COMMA]", (uint)Keys.Oemcomma, (uint)DIKeys.DIK_COMMA, 44),
			new KeyCodeNode("[KEYCODE_STOP]", (uint)Keys.OemPeriod, (uint)DIKeys.DIK_PERIOD, 46),
			new KeyCodeNode("[KEYCODE_SLASH]", (uint)Keys.OemQuestion, (uint)DIKeys.DIK_SLASH, 47),
			new KeyCodeNode("[KEYCODE_RSHIFT]", (uint)Keys.RShiftKey, (uint)DIKeys.DIK_RSHIFT, 0),
			new KeyCodeNode("[KEYCODE_ASTERISK]", (uint)Keys.Multiply, (uint)DIKeys.DIK_MULTIPLY, 42),
			new KeyCodeNode("[KEYCODE_LALT]", (uint)Keys.LMenu, (uint)DIKeys.DIK_LMENU, 0),
			new KeyCodeNode("[KEYCODE_SPACE]", (uint)Keys.Space, (uint)DIKeys.DIK_SPACE, 32),
			new KeyCodeNode("[KEYCODE_CAPSLOCK]", (uint)Keys.Capital, (uint)DIKeys.DIK_CAPITAL, 0),
			new KeyCodeNode("[KEYCODE_F1]", (uint)Keys.F1, (uint)DIKeys.DIK_F1, 0),
			new KeyCodeNode("[KEYCODE_F2]", (uint)Keys.F2, (uint)DIKeys.DIK_F2, 0),
			new KeyCodeNode("[KEYCODE_F3]", (uint)Keys.F3, (uint)DIKeys.DIK_F3, 0),
			new KeyCodeNode("[KEYCODE_F4]", (uint)Keys.F4, (uint)DIKeys.DIK_F4, 0),
			new KeyCodeNode("[KEYCODE_F5]",(uint)Keys.F5, (uint)DIKeys.DIK_F5, 0),
			new KeyCodeNode("[KEYCODE_F6]", (uint)Keys.F6, (uint)DIKeys.DIK_F6, 0),
			new KeyCodeNode("[KEYCODE_F7]", (uint)Keys.F7, (uint)DIKeys.DIK_F7, 0),
			new KeyCodeNode("[KEYCODE_F8]", (uint)Keys.F8, (uint)DIKeys.DIK_F8, 0),
			new KeyCodeNode("[KEYCODE_F9]", (uint)Keys.F9, (uint)DIKeys.DIK_F9, 0),
			new KeyCodeNode("[KEYCODE_F10]", (uint)Keys.F10, (uint)DIKeys.DIK_F10, 0),
			new KeyCodeNode("[KEYCODE_NUMLOCK]", (uint)Keys.NumLock, (uint)DIKeys.DIK_NUMLOCK, 0),
			new KeyCodeNode("[KEYCODE_SCRLOCK]", (uint)Keys.Scroll, (uint)DIKeys.DIK_SCROLL, 0),
			new KeyCodeNode("[KEYCODE_7PAD]", (uint)Keys.NumPad7, (uint)DIKeys.DIK_NUMPAD7, 0),
			new KeyCodeNode("[KEYCODE_8PAD]", (uint)Keys.NumPad8, (uint)DIKeys.DIK_NUMPAD8, 0),
			new KeyCodeNode("[KEYCODE_9PAD]", (uint)Keys.NumPad9, (uint)DIKeys.DIK_NUMPAD9, 0),
			new KeyCodeNode("[KEYCODE_MINUSPAD]", (uint)Keys.Subtract, (uint)DIKeys.DIK_SUBTRACT, 0),
			new KeyCodeNode("[KEYCODE_4PAD]", (uint)Keys.NumPad4, (uint)DIKeys.DIK_NUMPAD4, 0),
			new KeyCodeNode("[KEYCODE_5PAD]", (uint)Keys.NumPad5, (uint)DIKeys.DIK_NUMPAD5, 0),
			new KeyCodeNode("[KEYCODE_6PAD]", (uint)Keys.NumPad6, (uint)DIKeys.DIK_NUMPAD6, 0),
			new KeyCodeNode("[KEYCODE_PLUSPAD]", (uint)Keys.Add, (uint)DIKeys.DIK_ADD, 0),
			new KeyCodeNode("[KEYCODE_1PAD]", (uint)Keys.NumPad1, (uint)DIKeys.DIK_NUMPAD1, 0),
			new KeyCodeNode("[KEYCODE_2PAD]", (uint)Keys.NumPad2, (uint)DIKeys.DIK_NUMPAD2, 0),
			new KeyCodeNode("[KEYCODE_3PAD]", (uint)Keys.NumPad3, (uint)DIKeys.DIK_NUMPAD3, 0),
			new KeyCodeNode("[KEYCODE_DELPAD]", (uint)Keys.NumPad0, (uint)DIKeys.DIK_NUMPAD0, 0),
			new KeyCodeNode("[KEYCODE_DECIMAL]", (uint)Keys.Decimal, (uint)DIKeys.DIK_DECIMAL, 0),
			new KeyCodeNode("[KEYCODE_F11]", (uint)Keys.F11, (uint)DIKeys.DIK_F11, 0),
			new KeyCodeNode("[KEYCODE_F12]", (uint)Keys.F12, (uint)DIKeys.DIK_F12, 0),
			new KeyCodeNode("[KEYCODE_F13]", (uint)Keys.F13, (uint)DIKeys.DIK_F13, 0),
			new KeyCodeNode("[KEYCODE_F14]", (uint)Keys.F14, (uint)DIKeys.DIK_F14, 0),
			new KeyCodeNode("[KEYCODE_F15]", (uint)Keys.F15, (uint)DIKeys.DIK_F15, 0),
			new KeyCodeNode("[KEYCODE_ENTERPAD]", (uint)Keys.Return, (uint)DIKeys.DIK_NUMPADENTER, 0),
			new KeyCodeNode("[KEYCODE_RCONTROL]", (uint)Keys.RControlKey, (uint)DIKeys.DIK_RCONTROL, 0),
			new KeyCodeNode("[KEYCODE_SLASHPAD]", (uint)Keys.Divide, (uint)DIKeys.DIK_DIVIDE, 0),
			new KeyCodeNode("[KEYCODE_PRTSCR]", (uint)Keys.Snapshot, (uint)DIKeys.DIK_SYSRQ, 0),
			new KeyCodeNode("[KEYCODE_RALT]", (uint)Keys.RMenu, (uint)DIKeys.DIK_RMENU, 0),
			new KeyCodeNode("[KEYCODE_HOME]", (uint)Keys.Home, (uint)DIKeys.DIK_HOME, 0),
			new KeyCodeNode("[KEYCODE_UP]", (uint)Keys.Up, (uint)DIKeys.DIK_UP, 0),
			new KeyCodeNode("[KEYCODE_PGUP]", (uint)Keys.Prior, (uint)DIKeys.DIK_PRIOR, 0),
			new KeyCodeNode("[KEYCODE_LEFT]", (uint)Keys.Left, (uint)DIKeys.DIK_LEFT, 0),
			new KeyCodeNode("[KEYCODE_RIGHT]", (uint)Keys.Right, (uint)DIKeys.DIK_RIGHT, 0),
			new KeyCodeNode("[KEYCODE_END]", (uint)Keys.End, (uint)DIKeys.DIK_END, 0),
			new KeyCodeNode("[KEYCODE_DOWN]", (uint)Keys.Down, (uint)DIKeys.DIK_DOWN, 0),
			new KeyCodeNode("[KEYCODE_PGDN]", (uint)Keys.Next, (uint)DIKeys.DIK_NEXT, 0),
			new KeyCodeNode("[KEYCODE_INSERT]", (uint)Keys.Insert, (uint)DIKeys.DIK_INSERT, 0),
			new KeyCodeNode("[KEYCODE_DEL]", (uint)Keys.Delete, (uint)DIKeys.DIK_DELETE, 0),
			new KeyCodeNode("[KEYCODE_LWIN]", (uint)Keys.LWin, (uint)DIKeys.DIK_LWIN, 0),
			new KeyCodeNode("[KEYCODE_RWIN]", (uint)Keys.RWin, (uint)DIKeys.DIK_RWIN, 0),
			new KeyCodeNode("[KEYCODE_MENU]", (uint)Keys.Apps, (uint)DIKeys.DIK_APPS, 0),
			new KeyCodeNode("[KEYCODE_PAUSE]", (uint)Keys.Pause, (uint)DIKeys.DIK_PAUSE, 0),
			//new KeyCodeNode("[KEYCODE_CANCEL]", (uint)Keys.Cancel, (uint)DIKeys.DIK_CANCEL, 0)
		};

		public static KeyCodeNode GetKeyNode(uint vkKey)
		{
			for (int i = 0; i < KeyCodeArray.Length; i++)
			{
				if (KeyCodeArray[i].VKKey == vkKey)
					return KeyCodeArray[i];
			}

			return null;
		}

		public static uint VKKeyToDIKey(uint vkKey)
		{
			for (int i = 0; i < KeyCodeArray.Length; i++)
			{
				if (KeyCodeArray[i].VKKey == vkKey)
					return KeyCodeArray[i].DIKey;
			}

			return 0;
		}

		public static Keys DIKeyToVKKey(int diKey)
		{
			for (int i = 0; i < KeyCodeArray.Length; i++)
			{
				if (KeyCodeArray[i].DIKey == diKey)
					return (Keys)KeyCodeArray[i].VKKey;
			}

			return 0;
		}

		public static uint VKKeyToAscii(uint vkKey)
		{
			for (int i = 0; i < KeyCodeArray.Length; i++)
			{
				if (KeyCodeArray[i].VKKey == vkKey)
					return KeyCodeArray[i].AsciiCode;
			}

			return 0;
		}

		public static string VKKeyToString(uint vkKey)
		{
			for (int i = 0; i < KeyCodeArray.Length; i++)
			{
				if (KeyCodeArray[i].VKKey == vkKey)
					return KeyCodeArray[i].Name;
			}

			return String.Empty;
		}

		public static uint StringToVKKey(string vkKeyString)
		{
			for (int i = 0; i < KeyCodeArray.Length; i++)
			{
				if (KeyCodeArray[i].Name == vkKeyString)
					return KeyCodeArray[i].VKKey;
			}

			return 0;
		}

		public static string VKKeyToString(Keys vkKey)
		{
			for (int i = 0; i < KeyCodeArray.Length; i++)
			{
				if (KeyCodeArray[i].VKKey == (uint)vkKey)
					return KeyCodeArray[i].Name;
			}

			return String.Empty;
		}

		public static string DIKeyToString(uint diKey)
		{
			for (int i = 0; i < KeyCodeArray.Length; i++)
			{
				if (KeyCodeArray[i].DIKey == diKey)
					return KeyCodeArray[i].Name;
			}

			return String.Empty;
		}

		private static byte[] m_keyArray = new byte[256];

		public static bool TryGetKeyCodes(ref byte[] m_keyArray)
		{
			bool result = false;
			IntPtr hWndActiveWin = Win32.GetForegroundWindow();
			uint activeId = Win32.GetWindowThreadProcessId(hWndActiveWin, IntPtr.Zero);
			IntPtr hWndFocused = IntPtr.Zero;
			uint threadId = Win32.GetCurrentThreadId();

			if (Win32.AttachThreadInput(threadId, activeId, true))
			{
				Win32.GetKeyState(0);
				result = Win32.GetKeyboardState(m_keyArray);
				hWndFocused = Win32.GetFocus();
				Win32.AttachThreadInput(threadId, activeId, false);
			}
			else
			{
				result = Win32.GetKeyboardState(m_keyArray);
				hWndFocused = hWndActiveWin;
			}

			return result;
		}

		public static string GetKeyCodeString()
		{
			List<string> keyCodeList = new List<string>();

			if (!TryGetKeyCodes(ref m_keyArray))
				return String.Empty;

			for (int i = 0; i < 256; i++)
			{
				byte key = m_keyArray[i];

				if ((key & 0x80) != 0)
				{
					string keyString = VKKeyToString((uint)i);

					keyCodeList.Add(keyString);
				}
			}

			return String.Join("|", keyCodeList.ToArray());
		}
	}

	public partial class Win32
	{
		//[DllImport("user32.dll")]
		//public static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll")]
		public static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

		//[DllImport("kernel32.dll")]
		//public static extern uint GetCurrentThreadId();

		[DllImport("user32.dll")]
		public static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

		[DllImport("user32.dll")]
		public static extern IntPtr GetFocus();

		//[DllImport("user32.dll")]
		//public static extern short GetKeyState(int nVirtKey);

		//[DllImport("user32.dll", SetLastError = true)]
		//[return: MarshalAs(UnmanagedType.Bool)]
		//public static extern bool GetKeyboardState(byte[] lpKeyState);
	}
}
