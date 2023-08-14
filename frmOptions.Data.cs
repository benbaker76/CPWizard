using System;
using System.Collections.Generic;
using System.Text;

namespace CPWizard
{
	partial class frmOptions
	{
		private void GetDataOptions()
		{
			chkShowCPOnly.Checked = Settings.Data.General.ShowCPOnly;
			chkExitToMenu.Checked = Settings.Data.General.ExitToMenu;

			chkAutoShowShowCPOnly.Checked = Settings.Data.AutoShow.ShowCPOnly;
			chkAutoShowExitToMenu.Checked = Settings.Data.AutoShow.ExitToMenu;

			chkMAMECP.Checked = Settings.Data.MAME.ControlPanel;
			chkGameInfo.Checked = Settings.Data.MAME.GameInfo;
			chkGameHistory.Checked = Settings.Data.MAME.GameHistory;
			chkMAMEInfo.Checked = Settings.Data.MAME.MAMEInfo;
			chkControlInfo.Checked = Settings.Data.MAME.ControlInfo;
			chkHighScore.Checked = Settings.Data.MAME.HighScore;
			chkMyHighScore.Checked = Settings.Data.MAME.MyHighScore;
			chkMAMEArtwork.Checked = Settings.Data.MAME.Artwork;
			chkMAMEManual.Checked = Settings.Data.MAME.Manual;
			chkMAMEIRC.Checked = Settings.Data.MAME.IRC;

			chkEmulatorCP.Checked = Settings.Data.Emulator.ControlPanel;
			chkEmulatorArtwork.Checked = Settings.Data.Emulator.Artwork;
			chkEmulatorManual.Checked = Settings.Data.Emulator.Manual;
			chkOperationCard.Checked = Settings.Data.Emulator.OperationCard;
			chkNFO.Checked = Settings.Data.Emulator.NFO;
			chkEmulatorIRC.Checked = Settings.Data.Emulator.IRC;

			this.chkGameHistory.CheckedChanged += new System.EventHandler(this.chkGameHistory_CheckedChanged);
			this.chkMAMEInfo.CheckedChanged += new System.EventHandler(this.chkMAMEInfo_CheckedChanged);
			this.chkControlInfo.CheckedChanged += new System.EventHandler(this.chkControlInfo_CheckedChanged);
			this.chkHighScore.CheckedChanged += new System.EventHandler(this.chkHighScore_CheckedChanged);
		}

		private void SetDataOptions()
		{
			Settings.Data.General.ShowCPOnly = chkShowCPOnly.Checked;
			Settings.Data.General.ExitToMenu = chkExitToMenu.Checked;

			Settings.Data.AutoShow.ShowCPOnly = chkAutoShowShowCPOnly.Checked;
			Settings.Data.AutoShow.ExitToMenu = chkAutoShowExitToMenu.Checked;

			Settings.Data.MAME.ControlPanel = chkMAMECP.Checked;
			Settings.Data.MAME.GameInfo = chkGameInfo.Checked;
			Settings.Data.MAME.GameHistory = chkGameHistory.Checked;
			Settings.Data.MAME.MAMEInfo = chkMAMEInfo.Checked;
			Settings.Data.MAME.ControlInfo = chkControlInfo.Checked;
			Settings.Data.MAME.HighScore = chkHighScore.Checked;
			Settings.Data.MAME.MyHighScore = chkMyHighScore.Checked;
			Settings.Data.MAME.Artwork = chkMAMEArtwork.Checked;
			Settings.Data.MAME.Manual = chkMAMEManual.Checked;
			Settings.Data.MAME.IRC = chkMAMEIRC.Checked;

			Settings.Data.Emulator.ControlPanel = chkEmulatorCP.Checked;
			Settings.Data.Emulator.Artwork = chkEmulatorArtwork.Checked;
			Settings.Data.Emulator.Manual = chkEmulatorManual.Checked;
			Settings.Data.Emulator.OperationCard = chkOperationCard.Checked;
			Settings.Data.Emulator.NFO = chkNFO.Checked;
			Settings.Data.Emulator.IRC = chkEmulatorIRC.Checked;

			this.chkGameHistory.CheckedChanged -= new System.EventHandler(this.chkGameHistory_CheckedChanged);
			this.chkMAMEInfo.CheckedChanged -= new System.EventHandler(this.chkMAMEInfo_CheckedChanged);
			this.chkControlInfo.CheckedChanged -= new System.EventHandler(this.chkControlInfo_CheckedChanged);
			this.chkHighScore.CheckedChanged -= new System.EventHandler(this.chkHighScore_CheckedChanged);
		}

		private void chkGameHistory_CheckedChanged(object sender, EventArgs e)
		{
			DoLoadMAMEData = true;
		}

		private void chkMAMEInfo_CheckedChanged(object sender, EventArgs e)
		{
			DoLoadMAMEData = true;
		}

		private void chkControlInfo_CheckedChanged(object sender, EventArgs e)
		{
			DoLoadMAMEData = true;
		}

		private void chkHighScore_CheckedChanged(object sender, EventArgs e)
		{
			DoLoadMAMEData = true;
		}
	}
}
