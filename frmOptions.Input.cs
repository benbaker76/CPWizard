using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CPWizard
{
	partial class frmOptions
	{
		private void GetInputOptions()
		{
			chkEnableExitKey.Checked = Settings.Input.EnableExitKey;
			chkBackKeyExitMenu.Checked = Settings.Input.BackKeyExitMenu;
			txtShowKey.Text = Settings.Input.ShowKey;
			txtSelectKey.Text = Settings.Input.SelectKey;
			txtBackKey.Text = Settings.Input.BackKey;
			txtExitKey.Text = Settings.Input.ExitKey;
			txtMenuUp.Text = Settings.Input.MenuUp;
			txtMenuDown.Text = Settings.Input.MenuDown;
			txtMenuLeft.Text = Settings.Input.MenuLeft;
			txtMenuRight.Text = Settings.Input.MenuRight;
			txtVolumeDown.Text = Settings.Input.VolumeDown;
			txtVolumeUp.Text = Settings.Input.VolumeUp;
			txtShowDesktop.Text = Settings.Input.ShowDesktop;
			txtHideDesktop.Text = Settings.Input.HideDesktop;
			chkStopBackMenu.Checked = Settings.Input.StopBackMenu;
			chkBackShowsCP.Checked = Settings.Input.BackShowsCP;

			butShowKey.Click += new System.EventHandler(InputKeyEvent);
			butSelectKey.Click += new System.EventHandler(InputKeyEvent);
			butBackKey.Click += new System.EventHandler(InputKeyEvent);
			butExitKey.Click += new System.EventHandler(InputKeyEvent);
			butMenuUp.Click += new System.EventHandler(InputKeyEvent);
			butMenuDown.Click += new System.EventHandler(InputKeyEvent);
			butMenuLeft.Click += new System.EventHandler(InputKeyEvent);
			butMenuRight.Click += new System.EventHandler(InputKeyEvent);
			butVolumeDown.Click += new System.EventHandler(InputKeyEvent);
			butVolumeUp.Click += new System.EventHandler(InputKeyEvent);
			butShowDesktop.Click += new System.EventHandler(InputKeyEvent);
			butHideDesktop.Click += new System.EventHandler(InputKeyEvent);
			
		}

		private void SetInputOptions()
		{
			Settings.Input.EnableExitKey = chkEnableExitKey.Checked;
			Settings.Input.BackKeyExitMenu = chkBackKeyExitMenu.Checked;
			Settings.Input.ShowKey = txtShowKey.Text;
			Settings.Input.SelectKey = txtSelectKey.Text;
			Settings.Input.BackKey = txtBackKey.Text;
			Settings.Input.ExitKey = txtExitKey.Text;
			Settings.Input.MenuUp = txtMenuUp.Text;
			Settings.Input.MenuDown = txtMenuDown.Text;
			Settings.Input.MenuLeft = txtMenuLeft.Text;
			Settings.Input.MenuRight = txtMenuRight.Text;
			Settings.Input.VolumeDown = txtVolumeDown.Text;
			Settings.Input.VolumeUp = txtVolumeUp.Text;
			Settings.Input.ShowDesktop = txtShowDesktop.Text;
			Settings.Input.HideDesktop = txtHideDesktop.Text;
			Settings.Input.StopBackMenu = chkStopBackMenu.Checked;
			Settings.Input.BackShowsCP = chkBackShowsCP.Checked;

			butShowKey.Click -= new System.EventHandler(InputKeyEvent);
			butSelectKey.Click -= new System.EventHandler(InputKeyEvent);
			butBackKey.Click -= new System.EventHandler(InputKeyEvent);
			butExitKey.Click -= new System.EventHandler(InputKeyEvent);
			butMenuUp.Click -= new System.EventHandler(InputKeyEvent);
			butMenuDown.Click -= new System.EventHandler(InputKeyEvent);
			butMenuLeft.Click -= new System.EventHandler(InputKeyEvent);
			butMenuRight.Click -= new System.EventHandler(InputKeyEvent);
			butVolumeDown.Click -= new System.EventHandler(InputKeyEvent);
			butVolumeUp.Click -= new System.EventHandler(InputKeyEvent);
			butShowDesktop.Click -= new System.EventHandler(InputKeyEvent);
			butHideDesktop.Click -= new System.EventHandler(InputKeyEvent);
		}

		private void InputKeyEvent(object sender, EventArgs e)
		{
			Button button = sender as Button;

			using (frmInput InputForm = new frmInput())
			{
				TextBox textBox = null;

				if (InputForm.ShowDialog(this) == DialogResult.OK)
				{
					switch (button.Name)
					{
						case "butShowKey":
							textBox = txtShowKey;
							break;
						case "butSelectKey":
							textBox = txtSelectKey;
							break;
						case "butBackKey":
							textBox = txtBackKey;
							break;
						case "butExitKey":
							textBox = txtExitKey;
							break;
						case "butMenuUp":
							textBox = txtMenuUp;
							break;
						case "butMenuDown":
							textBox = txtMenuDown;
							break;
						case "butMenuLeft":
							textBox = txtMenuLeft;
							break;
						case "butMenuRight":
							textBox = txtMenuRight;
							break;
						case "butVolumeDown":
							textBox = txtVolumeDown;
							break;
						case "butVolumeUp":
							textBox = txtVolumeUp;
							break;
						case "butShowDesktop":
							textBox = txtShowDesktop;
							break;
						case "butHideDesktop":
							textBox = txtHideDesktop;
							break;
					}

					if (textBox != null)
					{
						if (frmInput.InputName == String.Empty)
							textBox.Text = String.Empty;
						else
							textBox.Text += (textBox.Text == String.Empty ? "" : "|") + frmInput.InputName;
					}
				}
			}
		}
	}
}
