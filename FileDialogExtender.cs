// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CPWizard
{
	public class FileDialogExtender
	{
		#region Enum DialogViewTypes

		public enum DialogViewTypes
		{
			Details = 0x704b,
			Tiles = 0x704c,
			ExtraLargeIcons = 0x704d,
			LargeIcons = 0x704f,
			MediumIcons = 0x704e,
			SmallIcons = 0x7050,
			List = 0x7051,            
			Content = 0x7052
		}

		#endregion

		#region Fields

		private const uint WM_COMMAND = 0x0111;
		private IntPtr m_lastDialogHandle = IntPtr.Zero;
		private DialogViewTypes m_viewType;
		private bool m_enabled;

		#endregion

		#region DllImports

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern IntPtr SendMessage(IntPtr Hdc, uint Msg_Const, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern IntPtr GetForegroundWindow();


		#endregion

		#region Constructors & Destructors

		public FileDialogExtender() : this(DialogViewTypes.List, false) { }

		public FileDialogExtender(DialogViewTypes viewType) : this(viewType, false) { }

		public FileDialogExtender(DialogViewTypes viewType, bool enabled)
		{
			m_viewType = viewType;
			Enabled = enabled;
		}

		#endregion

		#region Properties

		public DialogViewTypes DialogViewType
		{
			get { return m_viewType; }
			set { m_viewType = value; }
		}

		public bool Enabled
		{
			get { return m_enabled; }
			set { m_enabled = value; }
		}

		#endregion

		#region Methods

		/// <summary>
		/// Hash to be called from the overriden WndProc in the parent window of the dialog
		/// </summary>
		/// <param name="m"></param>
		public void WndProc(ref Message m)
		{
			if (!m_enabled)
				return;

			if (m.Msg == 289) //Notify of message loop
			{
				IntPtr dialogHandle = m.LParam;	//handle of the file dialog

				if (dialogHandle != m_lastDialogHandle) //only when not already changed
				{
					//get handle of the listview
					IntPtr listviewHandle = WindowTools.FindChildWindow(dialogHandle, IntPtr.Zero, "SHELLDLL_DefView", null);

					//send message to listview
					SendMessage(listviewHandle, WM_COMMAND, (IntPtr)m_viewType, IntPtr.Zero);

					//remember last handle
					m_lastDialogHandle = dialogHandle;
				}
			}
		}

		#endregion
	}
}
