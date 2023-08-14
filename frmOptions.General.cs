using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace CPWizard
{
	partial class frmOptions
	{
		private void GetGeneralOptions()
		{
			chkRunOnStartup.Checked = Settings.General.RunOnStartup;
			chkVolumeControl.Checked = Settings.General.VolumeControlEnable;

			chkHideDesktopEnable.Checked = Settings.HideDesktop.Enable;
			chkDisableScreenSaver.Checked = Settings.HideDesktop.DisableScreenSaver;
			chkHideMouseCursor.Checked = Settings.HideDesktop.HideMouseCursor;
			chkHideDesktopUsingForms.Checked = Settings.HideDesktop.HideDesktopUsingForms;
			chkSetWallpaperBlack.Checked = Settings.HideDesktop.SetWallpaperBlack;
			chkHideDesktopIcons.Checked = Settings.HideDesktop.HideDesktopIcons;
			chkHideTaskbar.Checked = Settings.HideDesktop.HideTaskbar;
			chkMoveMouseOffscreen.Checked = Settings.HideDesktop.MoveMouseOffscreen;

			txtGhostScriptExe.Text = Settings.Files.GSExe;
			chkDynamicDataLoading.Checked = Settings.General.DynamicDataLoading;

			chkVerboseLogging.Checked = Settings.General.VerboseLogging;

			chkDisableSystemSpecs.Checked = Settings.General.DisableSystemSpecs;
			chkAllowBracketedGameNames.Checked = Settings.General.AllowBracketedGameNames;
		}

		private void SetGeneralOptions()
		{
			Settings.General.RunOnStartup = chkRunOnStartup.Checked;
			Settings.General.VolumeControlEnable = chkVolumeControl.Checked;

			Settings.HideDesktop.Enable = chkHideDesktopEnable.Checked;
			Settings.HideDesktop.DisableScreenSaver = chkDisableScreenSaver.Checked;
			Settings.HideDesktop.HideMouseCursor = chkHideMouseCursor.Checked;
			Settings.HideDesktop.HideDesktopUsingForms = chkHideDesktopUsingForms.Checked;
			Settings.HideDesktop.SetWallpaperBlack = chkSetWallpaperBlack.Checked;
			Settings.HideDesktop.HideDesktopIcons = chkHideDesktopIcons.Checked;
			Settings.HideDesktop.HideTaskbar = chkHideTaskbar.Checked;
			Settings.HideDesktop.MoveMouseOffscreen = chkMoveMouseOffscreen.Checked;

			Settings.Files.GSExe = txtGhostScriptExe.Text;
			Settings.General.DynamicDataLoading = chkDynamicDataLoading.Checked;

			Settings.General.VerboseLogging = chkVerboseLogging.Checked;

			Settings.General.AllowBracketedGameNames = chkAllowBracketedGameNames.Checked;
			Settings.General.DisableSystemSpecs = chkDisableSystemSpecs.Checked;
		}

		private void butGhostScriptExe_Click(object sender, EventArgs e)
		{
			try
			{
				string ghostScriptExe = null;

				if (FileIO.TryOpenExe(this, null, out ghostScriptExe))
					txtGhostScriptExe.Text = ghostScriptExe;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("butGhostScriptExe_Click", "OptionsForm", ex.Message, ex.StackTrace);
			}
		}

		private void chkDynamicDataLoading_CheckedChanged(object sender, EventArgs e)
		{
			DoLoadMAMEData = true;
		}
	}
}
