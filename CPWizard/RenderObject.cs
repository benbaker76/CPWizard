// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace CPWizard
{
	public abstract class RenderObject : IDisposable
	{
		private bool m_visible = false;
		private bool m_exitToMenu = true;
		private Rectangle m_location = Rectangle.Empty;

		public abstract void AddEventHandlers();

		public abstract void RemoveEventHandlers();

		public abstract bool CheckEnabled();

		public abstract void Reset(EmulatorMode mode);

		public abstract void Paint(Graphics g, int x, int y, int width, int height);

		public virtual void Show()
		{
			m_visible = true;
			AddEventHandlers();
			EventManager.UpdateDisplay();
		}

		public virtual void Hide()
		{
			m_visible = false;
			RemoveEventHandlers();
		}

		public bool Enabled
		{
			get { return CheckEnabled(); }
		}

		public bool Visible
		{
			get { return m_visible; }
			set { m_visible = value; }
		}

		public bool ExitToMenu
		{
			get { return m_exitToMenu; }
			set { m_exitToMenu = value; }
		}

		public virtual void Dispose()
		{
			RemoveEventHandlers();
		}
	}
}
