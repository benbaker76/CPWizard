using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CPWizard
{
	public partial class frmLoading : Form
	{
		private int m_id = 0;

		public frmLoading()
		{
			InitializeComponent();
		}

		public void ShowLoading(int id, bool showScreenshot)
		{
			try
			{
				m_id = id;

				if (showScreenshot)
					picOutput.Image = Globals.ScreenshotList[id];
				else
					picOutput.Image = null;

				this.Bounds = System.Windows.Forms.Screen.AllScreens[id].Bounds;

				//LogFile.WriteLine(String.Format("Screen {0}: Bounds: {1}", id, System.Windows.Forms.Screen.AllScreens[id].Bounds));

				this.Show();
				this.TopMost = true;
				this.TopLevel = true;
				//this.Activate();
				//this.Focus();

				//Win32.SetWindowPos(this.Handle, Win32.HWND_BOTTOM, 0, 0, 0, 0, Win32.SWP_NOMOVE | Win32.SWP_NOSIZE);
				//Win32.SetWindowPos(this.Handle, Win32.HWND_TOP, 0, 0, 0, 0, Win32.SWP_NOMOVE | Win32.SWP_NOSIZE);

				HideDesktop.AllowForceForegroundWindow();
				HideDesktop.ForceForegroundWindow(this);

				this.Invalidate();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("Show", "fmrLoading", ex.Message, ex.StackTrace);
			}
		}

		public void OnDisplaySettingsChanged()
		{
			this.Bounds = System.Windows.Forms.Screen.AllScreens[m_id].Bounds;
			picOutput.Invalidate();
		}

		private void frmLoading_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		protected override bool ShowWithoutActivation
		{
			get { return true; }
		}
	}
}