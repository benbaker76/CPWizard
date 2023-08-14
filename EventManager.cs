// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace CPWizard
{
	public class EventManager
	{
		//public static event GlobalKeyHandlerGlobalKeyDown;
		//public static event GlobalKeyHandlerGlobalKeyUp;
		public static event EmptyHandler OnUpdateDisplay;
		//public static event EmptyHandler OnDisplaySettingsChanged;
		public static event KeyHandler OnKeyDown;
		public static event KeyHandler OnKeyUp;
		public static event MouseHandler OnMouseMove;
		public static event MouseHandler OnMouseDown;
		public static event MouseHandler OnMouseUp;
		public static event PaintHandler OnPaint;
		public static event CmdArgsChangedHandler OnCmdArgsChanged;
		public static event WndProcHandler OnWndProc;

		public delegate void EmptyHandler();
		public delegate bool GlobalKeyHandler(int VKKey);
		public delegate void KeyHandler(object sender, System.Windows.Forms.KeyEventArgs e);
		public delegate void MouseHandler(object sender, System.Windows.Forms.MouseEventArgs e);
		public delegate void PaintHandler(object sender, PaintEventArgs e);
		public delegate void CmdArgsChangedHandler(Dictionary<string, string> args);
		public delegate void WndProcHandler(ref Message msg);

		public static void UpdateDisplay()
		{
			if (OnUpdateDisplay != null)
				OnUpdateDisplay();
		}

		/* public static bool GlobalKeyDown(int vkkey)
		{
			if (OnGlobalKeyDown != null)
				return OnGlobalKeyDown(vkkey);

			return false;
		}

		public static bool GlobalKeyUp(int vkkey)
		{
			if (OnGlobalKeyUp != null)
				return OnGlobalKeyUp(vkkey);

			return false;
		} */

		public static void KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (OnKeyDown != null)
				OnKeyDown(sender, e);
		}

		public static void KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (OnKeyUp != null)
				OnKeyUp(sender, e);
		}

		public static void MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (OnMouseMove != null)
				OnMouseMove(sender, e);
		}

		public static void MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (OnMouseDown != null)
				OnMouseDown(sender, e);
		}

		public static void MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (OnMouseUp != null)
				OnMouseUp(sender, e);
		}

		public static void Paint(object sender, PaintEventArgs e)
		{
			if (OnPaint != null)
				OnPaint(sender, e);
		}

		public static void CmdArgsChanged(Dictionary<string, string> args)
		{
			if (OnCmdArgsChanged != null)
				OnCmdArgsChanged(args);
		}

		public static void WndProc(ref Message msg)
		{
			if (OnWndProc != null)
				OnWndProc(ref msg);
		}
	}
}
