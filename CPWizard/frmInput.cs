using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CPWizard
{
	public partial class frmInput : Form
	{
		public enum InputTypes
		{
			Keyboard,
			MCE,
			Joystick
		}

		public static string InputName = null;
		public static InputTypes InputType = InputTypes.Keyboard;
		private bool keystrokeProcessed;
		private string m_inputName = null;

		public frmInput()
		{
			InitializeComponent();

			this.KeyPreview = true;
			this.KeyDown += InputForm_KeyDown;
			this.KeyPress += InputForm_KeyPress;
			Globals.DirectInput.JoyEvent += DirectInput_OnJoyInput;
			Globals.MCERemote.MCEEvent += MCERemote_OnMCEEvent;
		}

		private void MCERemote_OnMCEEvent(object sender, MCECode mceCode)
		{
			Globals.MCERemote.MCEEvent -= MCERemote_OnMCEEvent;
			InputName = Globals.MCERemote.MCECodeToName(mceCode);
			InputType = InputTypes.MCE;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void DirectInput_OnJoyInput(object sender, JoyEventArgs e)
		{
			Globals.DirectInput.JoyEvent -= DirectInput_OnJoyInput;
			InputName = e.Name;
			InputType = InputTypes.Joystick;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void butClear_Click(object sender, EventArgs e)
		{
			InputName = String.Empty;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			InputName = null;
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void InputForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (!keystrokeProcessed)
				m_inputName = Globals.InputManager.GetKeyName((int)e.KeyData);

			e.Handled = true;

			InputName = m_inputName;
			InputType = InputTypes.Keyboard;

			this.Invalidate();
			this.Update();
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		void InputForm_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				InputForm_KeyDown(this, new System.Windows.Forms.KeyEventArgs(Keys.Return));
				e.Handled = true;
			}
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			InputForm_KeyDown(this, new System.Windows.Forms.KeyEventArgs(keyData));

			return base.ProcessCmdKey(ref msg, keyData);
		}

		protected override bool ProcessDialogKey(Keys keyData)
		{
			keystrokeProcessed = true;
			m_inputName = Globals.InputManager.GetKeyName((int)keyData);

			if ((Win32.GetKeyState((int)Keys.LShiftKey) & 0xF0000000) != 0)
				m_inputName = "[KEYCODE_LSHIFT]";
			else if ((Win32.GetKeyState((int)Keys.RShiftKey) & 0xF0000000) != 0)
				m_inputName = "[KEYCODE_RSHIFT]";
			else if ((Win32.GetKeyState((int)Keys.LControlKey) & 0xF0000000) != 0)
				m_inputName = "[KEYCODE_LCONTROL]";
			else if ((Win32.GetKeyState((int)Keys.RControlKey) & 0xF0000000) != 0)
				m_inputName = "[KEYCODE_RCONTROL]";
			else if ((Win32.GetKeyState((int)Keys.LMenu) & 0xF0000000) != 0)
				m_inputName = "[KEYCODE_LALT]";
			else if ((Win32.GetKeyState((int)Keys.RMenu) & 0xF0000000) != 0)
				m_inputName = "[KEYCODE_RALT]";
			else
				InputForm_KeyDown(this, new System.Windows.Forms.KeyEventArgs(keyData));

			this.Invalidate();
			this.Update();

			return base.ProcessDialogKey(keyData);
		}
	}
}