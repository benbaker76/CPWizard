using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CPWizard
{
	partial class frmOptions
	{
		private void GetDisplayOptions()
		{
			cboDisplayRotation.SelectedIndex = Settings.Display.Rotation / 90;
			chkFlipX.Checked = Settings.Display.FlipX;
			chkFlipY.Checked = Settings.Display.FlipY;

			chkAutoRotate.Checked = Settings.Display.AutoRotate;

			if (Settings.Display.RotateLeft)
				rdoRotateLeft.Checked = true;
			else
				rdoRotateRight.Checked = true;

			this.cboDisplayScreen.SelectedIndexChanged -= new System.EventHandler(this.cboDisplayScreen_SelectedIndexChanged);
			this.cboSubScreen.SelectedIndexChanged -= new System.EventHandler(this.cboSubScreen_SelectedIndexChanged);

			cboDisplayScreen.Items.AddRange(ScreenManager.GetScreenArray());
			cboDisplayScreen.SelectedIndex = cboDisplayScreen.FindString(ScreenManager.GetScreenName(Settings.Display.Screen));

			chkSubScreen.Checked = Settings.Display.SubScreenEnable;
			chkSubScreenDisable.Checked = Settings.Display.SubScreenDisable;
			nudSubScreen.Value = Settings.Display.SubScreenInterval;
			cboSubScreen.Items.AddRange(ScreenManager.GetScreenArray());
			cboSubScreen.SelectedIndex = cboDisplayScreen.FindString(ScreenManager.GetScreenName(Settings.Display.SubScreen));

			this.cboDisplayScreen.SelectedIndexChanged += new System.EventHandler(this.cboDisplayScreen_SelectedIndexChanged);
			this.cboSubScreen.SelectedIndexChanged += new System.EventHandler(this.cboSubScreen_SelectedIndexChanged);

			chkDisplayChange.Checked = Settings.Display.DisplayChange;
			nudDisplayChangeDelay.Value = Settings.Display.DisplayChangeDelay;
			chkShowLoadingScreens.Checked = Settings.Display.ShowLoadingScreens;
			chkLabelArrowShow.Checked = Settings.Display.LabelArrowShow;
			nudLabelArrowSize.Value = Settings.Display.LabelArrowSize;
			butLabelArrowColor.BackColor = Settings.Display.LabelArrowColor;
			chkLabelSpotShow.Checked = Settings.Display.LabelSpotShow;
			nudLabelSpotSize.Value = Settings.Display.LabelSpotSize;
			butLabelSpotColor.BackColor = Settings.Display.LabelSpotColor;
			nudLabelOutlineSize.Value = Settings.Display.LabelOutlineSize;
			butLabelOutlineColor.BackColor = Settings.Display.LabelOutlineColor;
			chkAlphaFade.Checked = Settings.Display.AlphaFade;
			nudAlphaFadeValue.Value = Settings.Display.AlphaFadeValue;

			chkHideExitMenu.Checked = Settings.Display.HideExitMenu;

			lblMenuFont.Text = Settings.Display.MenuFont.Name;
			lblMenuFont.Font = new Font(Settings.Display.MenuFont.FontFamily, 12);
			lblMenuFont.Tag = Settings.Display.MenuFont;

			chkUseMenuBorders.Checked = Settings.Display.UseMenuBorders;
			butMenuFontColor.BackColor = Settings.Display.MenuFontColor;
			butMenuSelectorBarColor.BackColor = Settings.Display.MenuSelectorBarColor;
			butMenuBorderColor.BackColor = Settings.Display.MenuBorderColor;
			butMenuSelectorBorderColor.BackColor = Settings.Display.MenuSelectorBorderColor;
			chkShowDropShadow.Checked = Settings.Display.ShowDropShadow;
		}

		private void SetDisplayOptions()
		{
			Settings.Display.Rotation = cboDisplayRotation.SelectedIndex * 90;
			Settings.Display.FlipX = chkFlipX.Checked;
			Settings.Display.FlipY = chkFlipY.Checked;
			Settings.Display.AutoRotate = chkAutoRotate.Checked;
			Settings.Display.RotateLeft = rdoRotateLeft.Checked;
			Settings.Display.Screen = cboDisplayScreen.SelectedIndex;
			Settings.Display.SubScreenEnable = chkSubScreen.Checked;
			Settings.Display.SubScreenDisable = chkSubScreenDisable.Checked;
			Settings.Display.SubScreenInterval = (int)nudSubScreen.Value;
			Settings.Display.SubScreen = cboSubScreen.SelectedIndex;
			Settings.Display.DisplayChange = chkDisplayChange.Checked;
			Settings.Display.DisplayChangeDelay = (int)nudDisplayChangeDelay.Value;
			Settings.Display.ShowLoadingScreens = chkShowLoadingScreens.Checked;
			Settings.Display.LabelArrowShow = chkLabelArrowShow.Checked;
			Settings.Display.LabelArrowSize = (int)nudLabelArrowSize.Value;
			Settings.Display.LabelArrowColor = butLabelArrowColor.BackColor;
			Settings.Display.LabelSpotShow = chkLabelSpotShow.Checked;
			Settings.Display.LabelSpotSize = (int)nudLabelSpotSize.Value;
			Settings.Display.LabelSpotColor = butLabelSpotColor.BackColor;
			Settings.Display.LabelOutlineSize = (int)nudLabelOutlineSize.Value;
			Settings.Display.LabelOutlineColor = butLabelOutlineColor.BackColor;
			Settings.Display.AlphaFade = chkAlphaFade.Checked;
			Settings.Display.AlphaFadeValue = (int)nudAlphaFadeValue.Value;
			Settings.Display.HideExitMenu = chkHideExitMenu.Checked;
			Settings.Display.MenuFont = (Font)lblMenuFont.Tag;
			Settings.Display.UseMenuBorders = chkUseMenuBorders.Checked;
			Settings.Display.MenuFontColor = butMenuFontColor.BackColor;
			Settings.Display.MenuSelectorBarColor = butMenuSelectorBarColor.BackColor;
			Settings.Display.MenuBorderColor = butMenuBorderColor.BackColor;
			Settings.Display.MenuSelectorBorderColor = butMenuSelectorBorderColor.BackColor;
			Settings.Display.ShowDropShadow = chkShowDropShadow.Checked;
		}

		private void butLabelLinkColor_Click(object sender, EventArgs e)
		{
			ColorDialog colorDialog = new ColorDialog();

			colorDialog.Color = butLabelArrowColor.BackColor;

			if (colorDialog.ShowDialog(this) == DialogResult.OK)
				butLabelArrowColor.BackColor = colorDialog.Color;
		}

		private void butLabelSpotColor_Click(object sender, EventArgs e)
		{
			ColorDialog colorDialog = new ColorDialog();

			colorDialog.Color = butLabelSpotColor.BackColor;

			if (colorDialog.ShowDialog(this) == DialogResult.OK)
				butLabelSpotColor.BackColor = colorDialog.Color;
		}

		private void butLabelOutlineColor_Click(object sender, EventArgs e)
		{
			ColorDialog colorDialog = new ColorDialog();

			colorDialog.Color = butLabelOutlineColor.BackColor;

			if (colorDialog.ShowDialog(this) == DialogResult.OK)
				butLabelOutlineColor.BackColor = colorDialog.Color;
		}

		private void cboSubScreen_SelectedIndexChanged(object sender, EventArgs e)
		{
			Globals.ProgramManager.CreateBitmaps();
		}

		private void cboDisplayScreen_SelectedIndexChanged(object sender, EventArgs e)
		{
			Globals.ProgramManager.CreateBitmaps();
		}

		private void nudDisplayChangeDelay_ValueChanged(object sender, EventArgs e)
		{
			Globals.ProgramManager.DisplaySettingsChangedTimer.Interval = (int)nudDisplayChangeDelay.Value;
		}

		private void butMenuFontColor_Click(object sender, EventArgs e)
		{
			ColorDialog colorDialog = new ColorDialog();

			colorDialog.Color = butMenuFontColor.BackColor;

			if (colorDialog.ShowDialog(this) == DialogResult.OK)
				butMenuFontColor.BackColor = colorDialog.Color;
		}

		private void butMenuSelectorBarColor_Click(object sender, EventArgs e)
		{
			ColorDialog colorDialog = new ColorDialog();

			colorDialog.Color = butMenuSelectorBarColor.BackColor;

			if (colorDialog.ShowDialog(this) == DialogResult.OK)
				butMenuSelectorBarColor.BackColor = colorDialog.Color;
		}

		private void butMenuBorderColor_Click(object sender, EventArgs e)
		{
			ColorDialog colorDialog = new ColorDialog();

			colorDialog.Color = butMenuBorderColor.BackColor;

			if (colorDialog.ShowDialog(this) == DialogResult.OK)
				butMenuBorderColor.BackColor = colorDialog.Color;
		}

		private void butMenuSelectorBorderColor_Click(object sender, EventArgs e)
		{
			ColorDialog colorDialog = new ColorDialog();

			colorDialog.Color = butMenuSelectorBorderColor.BackColor;

			if (colorDialog.ShowDialog(this) == DialogResult.OK)
				butMenuSelectorBorderColor.BackColor = colorDialog.Color;
		}

		private void butChangeMenuBak_Click(object sender, EventArgs e)
		{
			TryOpenBak(BakType.Menu);
		}

		private void butChooseMenuFont_Click(object sender, EventArgs e)
		{
			FontDialog fontDialog = new FontDialog();

			fontDialog.Font = (Font)lblMenuFont.Tag;
			fontDialog.MinSize = 30;  // this ensures menu displayed in hi-res - not sure if effects performance?

			if (fontDialog.ShowDialog(this) == DialogResult.OK)
			{
				lblMenuFont.Text = fontDialog.Font.Name;
				lblMenuFont.Font = new Font(fontDialog.Font.FontFamily, 12);
				lblMenuFont.Tag = fontDialog.Font;
			}
		}

		private void butChangeDefaultBak_Click(object sender, EventArgs e)
		{
			TryOpenBak(BakType.Default);
		}

		private void butChangeMainMenuBak_Click(object sender, EventArgs e)
		{
			TryOpenBak(BakType.MainMenu);
		}

		private void butChangeInfoBak_Click(object sender, EventArgs e)
		{
			TryOpenBak(BakType.Info);
		}

		private void TryOpenBak(BakType bakType)
		{
			string fileName = null;

			if (FileIO.TryOpenImage(this, Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), out fileName))
			{
				File.Copy(fileName, Path.Combine(Settings.Folders.Media, DisplayManager.BakName[(int)bakType]), true);

				Globals.DisplayManager.LoadBak(bakType);
			}
		}
	}
}
