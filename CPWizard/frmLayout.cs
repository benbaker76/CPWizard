using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CPWizard
{
	public partial class frmLayout : Form
	{
		private MainMenu mainMenu = null;
		private System.Windows.Forms.Timer showTimer = null;
		private System.Windows.Forms.Timer subTimer = null;
		private int showCountDown = 0;
		private bool m_sub = false;

		public frmLayout()
		{
			InitializeComponent();

			this.Deactivate += new System.EventHandler(frmLayout_Deactivate);

			showTimer = new System.Windows.Forms.Timer();
			showTimer.Interval = Settings.Display.ShowRetryInterval;
			showTimer.Tick += new System.EventHandler(showTimer_Tick);

			subTimer = new System.Windows.Forms.Timer();
			subTimer.Tick += new System.EventHandler(subTimer_Tick);
		}

		private void frmLayout_Deactivate(object sender, EventArgs e)
		{
			if (Settings.Display.ShowLayoutGiveFocus)
			{
				LogFile.VerboseWriteLine("frmLayout_Deactivate", "frmLayout", "Activate");

				this.Activate();
			}
		}

		public void Show(DisplayType displayType, int width, int height, bool sub)
		{
			m_sub = sub;

			lock (picOutput)
			{
				switch (displayType)
				{
					case DisplayType.Preview:
						picOutput.SizeMode = PictureBoxSizeMode.StretchImage;
						this.ShowInTaskbar = true;
						this.StartPosition = FormStartPosition.CenterScreen;
						this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
						this.MaximizeBox = true;
						this.MinimizeBox = true;
						this.Size = new Size(width, height);
						CreateMenu();

						if (Globals.LayoutList != null)
							Globals.Layout = Globals.LayoutList[0];

						Globals.LayoutIndex = 0;

						picOutput.Image = Globals.MainBitmap;
						break;
					case DisplayType.Show:
						if (m_sub)
						{
							if (Settings.Display.Screen == Settings.Display.SubScreen)
							{
								Settings.Display.SubScreenEnable = false;

								this.Close();
								return;
							}

							picOutput.SizeMode = PictureBoxSizeMode.StretchImage;
							this.ShowInTaskbar = false;
							this.StartPosition = FormStartPosition.Manual;
							this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
							this.MaximizeBox = false;
							this.MinimizeBox = false;
							this.Bounds = System.Windows.Forms.Screen.AllScreens[Settings.Display.SubScreen].Bounds;

							Globals.LayoutSubIndex = 0;

							if (Globals.SubBitmapList != null)
							{
								for (int i = 0; i < Globals.SubBitmapList.Count; i++)
								{
									Globals.SubBitmapList[i].Dispose();
									Globals.SubBitmapList[i] = null;
								}

								Globals.SubBitmapList = null;
							}

							Globals.SubBitmapList = new List<Bitmap>();

							if (Globals.LayoutSubList != null)
							{
								for (int i = 0; i < Globals.LayoutSubList.Count; i++)
								{
									Bitmap bmp = new Bitmap(Settings.General.SubScreenSize.Width, Settings.General.SubScreenSize.Height);

									Globals.LayoutSub = Globals.LayoutSubList[i];
									Globals.DisplayManager.PaintSub(bmp);
									Globals.SubBitmapList.Add(bmp);
								}
							}

							if (Globals.SubBitmapList.Count > 0)
							{
								picOutput.Image = Globals.SubBitmapList[0];
								picOutput.Invalidate();

								subTimer.Interval = Settings.Display.SubScreenInterval;
								subTimer.Start();
							}
							else
							{
								this.Close();
								return;
							}
						}
						else
						{
							picOutput.SizeMode = PictureBoxSizeMode.StretchImage;
							this.ShowInTaskbar = false;
							this.StartPosition = FormStartPosition.Manual;
							this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
							this.MaximizeBox = false;
							this.MinimizeBox = false;
							this.Bounds = System.Windows.Forms.Screen.AllScreens[Settings.Display.Screen].Bounds;


							if (Globals.LayoutList != null)
								Globals.Layout = Globals.LayoutList[0];

							Globals.LayoutIndex = 0;
							picOutput.Image = Globals.MainBitmap;

						}
						break;
				}
			}

			LogFile.VerboseWriteLine("Show", "frmLayout", "Showing Layout");

			this.Show();

			if (Settings.Display.ShowLayoutTopMost)
			{
				LogFile.VerboseWriteLine("Show", "frmLayout", "Setting Top Most");

				//this.TopMost = true;
				//this.TopLevel = true;

				Win32.SetWindowPos(this.Handle, Win32.HWND_TOPMOST, 0, 0, 0, 0, Win32.SWP_NOMOVE | Win32.SWP_NOSIZE | Win32.SWP_NOACTIVATE);
			}

			if (m_sub)
			{
				Globals.LayoutSubShowing = true;
			}
			else
			{
				LogFile.VerboseWriteLine("Show", "frmLayout", "frmLayout hWnd: " + String.Format("0x{0:x8}", this.Handle));

				//Win32.SetWindowPos(this.Handle, Win32.HWND_BOTTOM, 0, 0, 0, 0, Win32.SWP_NOMOVE | Win32.SWP_NOSIZE);
				//Win32.SetWindowPos(this.Handle, Win32.HWND_TOP, 0, 0, 0, 0, Win32.SWP_NOMOVE | Win32.SWP_NOSIZE);

				if (Settings.Display.ShowLayoutForceForeground)
				{
					LogFile.VerboseWriteLine("Show", "frmLayout", "Forcing Foreground");

					HideDesktop.AllowForceForegroundWindow();
					HideDesktop.ForceForegroundWindow(this);
				}

				LogFile.VerboseWriteLine("Show", "frmLayout", "Invalidate");

				if (Settings.Display.ShowLayoutGiveFocus)
				{
					LogFile.VerboseWriteLine("Show", "frmLayout", "Giving Focus");

					this.Activate();
					this.Focus();
				}

				if (Settings.Display.ShowLayoutMouseClick)
				{
					LogFile.VerboseWriteLine("Show", "frmLayout", String.Format("Sending Mouse Click X: {0} Y: {1} ", this.Bounds.X, this.Bounds.Y));

					SendMouse.DoMouseClick(this.Bounds.X, this.Bounds.Y);
				}

				IntPtr hWndForeground = Win32.GetForegroundWindow();
				string windowTitle = null;

				ProcessTools.TryGetWindowTitle(hWndForeground, out windowTitle);

				if (hWndForeground == this.Handle)
					LogFile.VerboseWriteLine("Show", "frmLayout", "Success! Foreground hWnd: " + String.Format("0x{0:x8} ({1})", hWndForeground, windowTitle));
				else
				{
					LogFile.VerboseWriteLine("Show", "frmLayout", "Failed! Foreground hWnd: " + String.Format("0x{0:x8} ({1})", hWndForeground, windowTitle));

					if (Settings.Display.ShowRetryEnable)
					{
						LogFile.VerboseWriteLine("Show", "frmLayout", "Starting Retry timer...");
						showCountDown = Settings.Display.ShowRetryNumRetrys;
						showTimer.Start();
					}
				}

				EventManager.OnUpdateDisplay -= new EventManager.EmptyHandler(OnUpdateDisplay);
				EventManager.OnUpdateDisplay += new EventManager.EmptyHandler(OnUpdateDisplay);

				OnUpdateDisplay();

				Globals.LayoutShowing = true;
			}
		}

		private void showTimer_Tick(object sender, EventArgs e)
		{
			if (this.IsDisposed)
			{
				showTimer.Stop();
				return;
			}

			LogFile.VerboseWriteLine("showTimer_Tick", "frmLayout", "frmLayout hWnd: " + String.Format("0x{0:x8}", this.Handle));
			LogFile.VerboseWriteLine("showTimer_Tick", "frmLayout", String.Format("Attempting Try {0}", showCountDown));

			showCountDown--;

			if (Settings.Display.ShowLayoutForceForeground)
			{
				LogFile.VerboseWriteLine("showTimer_Tick", "frmLayout", "Forcing Foreground");

				HideDesktop.AllowForceForegroundWindow();
				HideDesktop.ForceForegroundWindow(this);
			}

			IntPtr hWndForeground = Win32.GetForegroundWindow();
			string windowTitle = null;

			ProcessTools.TryGetWindowTitle(hWndForeground, out windowTitle);

			if (hWndForeground == this.Handle)
			{
				LogFile.VerboseWriteLine("showTimer_Tick", "frmLayout", "Success! Foreground hWnd: " + String.Format("0x{0:x8} ({1})", hWndForeground, windowTitle));

				showTimer.Stop();
			}
			else
			{
				LogFile.VerboseWriteLine("showTimer_Tick", "frmLayout", "Failed! Foreground hWnd: " + String.Format("0x{0:x8} ({1})", hWndForeground, windowTitle));

				if (showCountDown <= 0)
				{
					showTimer.Stop();

					if (Settings.Display.ShowRetryExitOnFail)
					{
						LogFile.VerboseWriteLine("showTimer_Tick", "frmLayout", String.Format("Failed Show Retry {0} Times. Closing Down.", Settings.Display.ShowRetryNumRetrys));

						Globals.ProgramManager.Hide();
					}
					else
						LogFile.VerboseWriteLine("showTimer_Tick", "frmLayout", String.Format("Failed Show Retry {0} Times. Giving Up.", Settings.Display.ShowRetryNumRetrys));
				}
			}
		}

		private void subTimer_Tick(object sender, EventArgs e)
		{
			if (Globals.LayoutSubList == null)
				return;

			lock (picOutput)
			{
				if (++Globals.LayoutSubIndex > Globals.LayoutSubList.Count - 1)
					Globals.LayoutSubIndex = 0;

				picOutput.Image = Globals.SubBitmapList[Globals.LayoutSubIndex];
				picOutput.Invalidate();
			}
		}

		public void OnDisplaySettingsChanged()
		{

			this.Bounds = System.Windows.Forms.Screen.AllScreens[Settings.Display.Screen].Bounds;
			OnUpdateDisplay();

		}

		private void CPForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (m_sub)
			{
				subTimer.Stop();

				Globals.LayoutSubForm = null;
				Globals.LayoutSubShowing = false;
			}
			else
			{
				showTimer.Stop();

				Globals.DisplayMode = DisplayMode.LayoutEditor;
				Settings.Layout.Name = Globals.Layout.Name;
				Globals.LayoutManager.ResetControls(Globals.Layout);
				Globals.MainForm.SetTitleBar();

				Globals.MainMenu.Hide();

				EventManager.OnUpdateDisplay -= new EventManager.EmptyHandler(OnUpdateDisplay);

				Globals.LayoutForm = null;
				Globals.LayoutShowing = false;
			}

			LogFile.VerboseWriteLine("CPForm_FormClosing", "frmLayout", "Closing");
		}

		private void OnUpdateDisplay()
		{
			lock (picOutput)
			{
				Globals.DisplayManager.Paint(Globals.MainBitmap);
				picOutput.Invalidate();
			}
		}

        private void CreateMenu()
        {
            MenuStrip menuStrip = new MenuStrip();
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("&File");

            ToolStripMenuItem saveItem = new ToolStripMenuItem("&Save", null, saveMenu_Click);
            ToolStripMenuItem exitItem = new ToolStripMenuItem("E&xit", null, exitMenu_Click);

            fileMenu.DropDownItems.Add(saveItem);
            fileMenu.DropDownItems.Add(exitItem);

            menuStrip.Items.Add(fileMenu);
            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);
        }

        private void saveMenu_Click(object sender, EventArgs e)
		{
			lock (picOutput)
			{
				string fileName = null;

				if (FileIO.TrySaveImage(this, out fileName))
					picOutput.Image.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
			}
		}

		private void exitMenu_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void frmLayout_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			EventManager.MouseMove(this, e);
		}

		private void frmLayout_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			EventManager.MouseUp(this, e);
		}

		protected override bool ShowWithoutActivation
		{
			get { return true; }
		}

        private void frmLayout_Shown(object sender, EventArgs e)
        {
            LogFile.WriteLine("Layout shown.");

        }
    }
}