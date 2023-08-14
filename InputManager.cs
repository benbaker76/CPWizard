// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CPWizard
{
	public class InputManager : IDisposable
	{
		[DllImport("user32.dll")]
		private static extern int GetAsyncKeyState(int vKey);

		private enum KeySeparatorType
		{
			Or,
			And,
			Not
		}

		private Dictionary<string, uint> m_inputDictionary = null;

		private uint m_maxInputCount = 0;

		private const int MAX_JOYSTICKS = 8;
		private const int MAX_BUTTONS = 256;

		private const bool KEY_REPEAT = true;
		private const int KEY_REPEAT_VALUE = 4;

		public delegate void InputEventHandler(object sender, InputEventArgs e);
		public event InputEventHandler InputEvent;

		public InputManager()
		{
			m_inputDictionary = new Dictionary<string, uint>();

			AddKeyCodesToDictionary();
			AddJoyCodesToDictionary();
			AddMCECodesToDictionary();

			Globals.KeyboardHook.KeyEvent += KeyboardHook_OnGlobalKeyEvent;
			//Global.MouseHook.MouseEvent += MouseHook_OnGlobalMouseEvent;
			Globals.DirectInput.JoyEvent += DirectInput_OnJoyInput;
			Globals.MCERemote.MCEEvent += MCERemote_OnMCEInput;
		}

		private void KeyboardHook_OnGlobalKeyEvent(object sender, KeyEventArgs e)
		{
			if (InputEvent == null)
				return;

			KeyCodeNode keyNode = GetKeyNode(e.VKKey);

			if (keyNode == null)
				return;

			if (KEY_REPEAT)
			{
				if (keyNode.KeyRepeatCount > 0)
				{
					if (keyNode.KeyRepeatCount == KEY_REPEAT_VALUE)
						keyNode.KeyRepeatCount = 0;
					else
						keyNode.KeyRepeatCount++;
				}
			}

			if (!e.IsDown && keyNode.KeyRepeatCount > 0)
			{
				if (InputEvent != null)
					InputEvent(sender, new InputEventArgs(e));

				keyNode.KeyRepeatCount = 0;
			}

			if (e.IsDown && keyNode.KeyRepeatCount == 0)
			{
				if (InputEvent != null)
					InputEvent(sender, new InputEventArgs(e));

				keyNode.KeyRepeatCount = 1;
			}
		}

		/* private void MouseHook_OnMouseEvent(object sender, MouseHookEventArgs e)
		{
			if (InputEvent != null)
				InputEvent(sender, new InputEventArgs(m_inputDictionary[e.Name], e));
		} */

		private void DirectInput_OnJoyInput(object sender, JoyEventArgs e)
		{
			if (InputEvent != null)
				InputEvent(sender, new InputEventArgs(m_inputDictionary[e.Name], e));
		}

		private void MCERemote_OnMCEInput(object sender, MCECode mceCode)
		{
			if (InputEvent != null)
			{
				string name = Globals.MCERemote.MCECodeToName(mceCode);
				InputEvent(sender, new InputEventArgs(m_inputDictionary[name], mceCode));
			}
		}

		private void AddKeyCodesToDictionary()
		{
			for (int i = 0; i < KeyCodes.KeyCodeArray.Length; i++)
				m_inputDictionary.Add(KeyCodes.KeyCodeArray[i].Name, (uint)KeyCodes.KeyCodeArray[i].VKKey);

			m_maxInputCount = 0xff;
		}

		private void AddMouseCodesToDictionary()
		{
			m_inputDictionary.Add("[MOUSECODE_1_XAXIS_POS_SWITCH]", m_maxInputCount++);
			m_inputDictionary.Add("[MOUSECODE_1_YAXIS_POS_SWITCH]", m_maxInputCount++);
			m_inputDictionary.Add("[MOUSECODE_1_BUTTON1]", m_maxInputCount++);
			m_inputDictionary.Add("[MOUSECODE_1_BUTTON2]", m_maxInputCount++);
			m_inputDictionary.Add("[MOUSECODE_1_BUTTON3]", m_maxInputCount++);
		}

		private void AddJoyCodesToDictionary()
		{
			for (int i = 0; i < MAX_JOYSTICKS; i++)
			{
				m_inputDictionary.Add(String.Format("[JOYCODE_{0}_XAXIS_{1}_SWITCH]", i + 1, "LEFT"), m_maxInputCount++);
				m_inputDictionary.Add(String.Format("[JOYCODE_{0}_XAXIS_{1}_SWITCH]", i + 1, "RIGHT"), m_maxInputCount++);
				m_inputDictionary.Add(String.Format("[JOYCODE_{0}_YAXIS_{1}_SWITCH]", i + 1, "UP"), m_maxInputCount++);
				m_inputDictionary.Add(String.Format("[JOYCODE_{0}_YAXIS_{1}_SWITCH]", i + 1, "DOWN"), m_maxInputCount++);
				m_inputDictionary.Add(String.Format("[JOYCODE_{0}_ZAXIS_{1}_SWITCH]", i + 1, "NEG"), m_maxInputCount++);
				m_inputDictionary.Add(String.Format("[JOYCODE_{0}_ZAXIS_{1}_SWITCH]", i + 1, "POS"), m_maxInputCount++);
				m_inputDictionary.Add(String.Format("[JOYCODE_{0}_RXAXIS_{1}_SWITCH]", i + 1, "NEG"), m_maxInputCount++);
				m_inputDictionary.Add(String.Format("[JOYCODE_{0}_RXAXIS_{1}_SWITCH]", i + 1, "POS"), m_maxInputCount++);
				m_inputDictionary.Add(String.Format("[JOYCODE_{0}_RYAXIS_{1}_SWITCH]", i + 1, "NEG"), m_maxInputCount++);
				m_inputDictionary.Add(String.Format("[JOYCODE_{0}_RYAXIS_{1}_SWITCH]", i + 1, "POS"), m_maxInputCount++);
				m_inputDictionary.Add(String.Format("[JOYCODE_{0}_RZAXIS_{1}_SWITCH]", i + 1, "NEG"), m_maxInputCount++);
				m_inputDictionary.Add(String.Format("[JOYCODE_{0}_RZAXIS_{1}_SWITCH]", i + 1, "POS"), m_maxInputCount++);

				m_inputDictionary.Add(String.Format("[JOYCODE_{0}_DPAD{1}]", i + 1, "UP"), m_maxInputCount++);
				m_inputDictionary.Add(String.Format("[JOYCODE_{0}_DPAD{1}]", i + 1, "RIGHT"), m_maxInputCount++);
				m_inputDictionary.Add(String.Format("[JOYCODE_{0}_DPAD{1}]", i + 1, "DOWN"), m_maxInputCount++);
				m_inputDictionary.Add(String.Format("[JOYCODE_{0}_DPAD{1}]", i + 1, "LEFT"), m_maxInputCount++);

				for (int j = 0; j < MAX_BUTTONS; j++)
					m_inputDictionary.Add(String.Format("[JOYCODE_{0}_BUTTON{1}]", i + 1, j + 1), m_maxInputCount++);
			}
		}


		private void AddMCECodesToDictionary()
		{
			for (int i = 0; i < MCERemote.MCECodes.Length; i++)
				m_inputDictionary.Add(MCERemote.MCECodes[i], m_maxInputCount++);
		}

		/* public bool CheckKeys(uint key, string input)
		{
			bool retVal = false;
			int keyValue = 0;
			KeySeparatorType keySeparator = KeySeparatorType.Or;
			int andCount = 1, andMatchCount = 0;
			string[] inputs = StringTools.SplitString(input, new string[] { "|", "&" });

			for (int i = 0; i < inputs.Length; i++)
			{
				switch (inputs[i])
				{
					case "|":
						keySeparator = KeySeparatorType.Or;
						break;
					case "&":
						keySeparator = KeySeparatorType.And;
						andCount++;
						break;
					default:
						keyValue = (int)GetKey(inputs[i]);

						switch (keySeparator)
						{
							case KeySeparatorType.Or:
								if (key == keyValue)
									retVal = true;
								break;
							case KeySeparatorType.And:
								if (GetAsyncKeyState(keyValue) != 0)
									andMatchCount++;
								break;
						}
						break;
				}
			}

			return (andCount == andMatchCount || retVal);
		} */

		public bool CheckInput(uint inputCode, string inputString)
		{
			if (inputString.Contains("|"))
			{
				string[] inputs = inputString.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

				for (int i = 0; i < inputs.Length; i++)
					if (inputCode == GetInputCode(inputs[i]))
						return true;
			}
			else
				return (inputCode == GetInputCode(inputString));

			return false;
		}

		public uint GetInputCode(string inputString)
		{
			uint inputCode = 0;

			if (m_inputDictionary.TryGetValue(inputString, out inputCode))
				return inputCode;

			return 0;
		}

		public KeyCodeNode GetKeyNode(Keys keyCode)
		{
			for (int i = 0; i < KeyCodes.KeyCodeArray.Length; i++)
				if (KeyCodes.KeyCodeArray[i].VKKey == (uint)keyCode)
					return KeyCodes.KeyCodeArray[i];

			return null;
		}

		public string GetKeyName(int keyCode)
		{
			for (int i = 0; i < KeyCodes.KeyCodeArray.Length; i++)
				if (KeyCodes.KeyCodeArray[i].VKKey == keyCode)
					return KeyCodes.KeyCodeArray[i].Name;

			return null;
		}

		#region IDisposable Members

		public void Dispose()
		{
			if (m_inputDictionary != null)
			{
				m_inputDictionary.Clear();
				m_inputDictionary = null;
			}
		}

		#endregion
	}

	public enum InputType
	{
		None,
		Keyboard,
		Mouse,
		Joystick,
		MCERemote
	};

	public class InputEventArgs : EventArgs
	{
		public uint InputCode;
		public KeyEventArgs KeyEventArgs;
		public MouseEventArgs MouseEventArgs;
		public JoyEventArgs JoyEventArgs;
		public MCECode MCECode;
		public bool IsDown = false;
		public InputType Type = InputType.None;
		public bool Handled = false;

		public InputEventArgs(KeyEventArgs keyEventArgs)
			: base()
		{
			this.InputCode = (uint)keyEventArgs.VKKey;
			this.IsDown = keyEventArgs.IsDown;
			this.KeyEventArgs = keyEventArgs;
			this.Type = InputType.Keyboard;
		}

		public InputEventArgs(uint inputCode, MouseEventArgs mouseEventArgs)
			: base()
		{
			this.InputCode = inputCode;
			this.IsDown = false;
			this.MouseEventArgs = mouseEventArgs;
			this.Type = InputType.Mouse;
		}

		public InputEventArgs(uint inputCode, JoyEventArgs joyHookEventArgs)
			: base()
		{
			this.InputCode = inputCode;
			this.IsDown = joyHookEventArgs.IsDown;
			this.JoyEventArgs = joyHookEventArgs;
			this.Type = InputType.Joystick;
		}

		public InputEventArgs(uint inputCode, MCECode mceCode)
			: base()
		{
			this.InputCode = inputCode;
			this.IsDown = false;
			this.MCECode = mceCode;
			this.Type = InputType.MCERemote;
		}
	}
}
