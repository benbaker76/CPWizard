using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace CPWizard
{
	public partial class frmOptions : Form
	{
		public frmOptions()
		{
			InitializeComponent();
		}

		private void OptionsForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Globals.OptionsForm = null;
		}

		private void OptionsForm_Load(object sender, EventArgs e)
		{
			GetOptions();
		}

		public void GoToMAMETab()
		{
			tabControl1.SelectedTab = tabMAMEPaths;
		}

		private void LoadMAMEData()
		{
			try
			{
				if (DoLoadMAMEData)
				{
					using (frmInfo loadingForm = new frmInfo(this, "Loading Please Wait...", false, false))
					{
						loadingForm.Show();
						Application.DoEvents();
						this.Cursor = Cursors.WaitCursor;
						butOk.Enabled = false;
						butCancel.Enabled = false;
						Globals.ProgramManager.LoadMAMEData();
						this.Cursor = Cursors.Default;
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("LoadMAMEData", "OptionsForm", ex.Message, ex.StackTrace);
			}
		}

		private void GetOptions()
		{
			try
			{
				GetGeneralOptions();
				GetLayoutOptions();
				GetMAMEOptions();
				GetMAMEFilters();
				GetMAMEPaths();
				GetProfileOptions();
				GetInputOptions();
				GetDisplayOptions();
				GetDataOptions();
				GetIRCOptions();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetOptions", "OptionsForm", ex.Message, ex.StackTrace);
			}
		}


		private void SetOptions()
		{
			try
			{
				SetGeneralOptions();
				SetLayoutOptions();
				SetMAMEOptions();
				SetMAMEFilters();
				SetMAMEPaths();
				SetProfileOptions();
				SetInputOptions();
				SetDisplayOptions();
				SetDataOptions();
				SetIRCOptions();

				Globals.ProgramManager.UpdateData();

				ConfigIO.WriteConfig();

				LoadMAMEData();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("SetOptions", "OptionsForm", ex.Message, ex.StackTrace);
			}
		}

		private void butOk_Click(object sender, EventArgs e)
		{
			SetOptions();
			this.Close();
		}

		private void butCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
			EventManager.WndProc(ref m);
		}
		
#region Profiles
		
		private void GetProfileOptions()
		{
			PopulateProfilesListView();
		}

		private void SetProfileOptions()
		{
			Globals.Profiles.WriteAllProfiles();
		}

		private void PopulateProfilesListView()
		{
			try
			{
				this.lvwProfiles.ItemChanged -= new ListViewEx.ItemChangedHandler(lvwProfile_ItemChanged);
				this.lvwProfiles.ItemChecked -= new ItemCheckedEventHandler(lvwProfile_ItemChecked);

				this.lvwProfiles.Items.Clear();

				foreach (ProfileNode profile in Globals.Profiles.ProfileList)
				{
					this.lvwProfiles.Items.Add(profile.Name);
					this.lvwProfiles.Items[this.lvwProfiles.Items.Count - 1].Checked = profile.Enabled;
					this.lvwProfiles.Items[this.lvwProfiles.Items.Count - 1].SubItems.Clear();
					this.lvwProfiles.Items[this.lvwProfiles.Items.Count - 1].SubItems.AddRange(new string[] { profile.Name, profile.Type, profile.Layout, profile.LayoutOverride, profile.LayoutSub, profile.Labels, profile.Database, profile.Executable, profile.WindowTitle, profile.WindowClass, profile.UseExe.ToString(), profile.Screenshot.ToString(), profile.Minimize.ToString(), profile.Maximize.ToString(), profile.ShowKey, profile.HideKey, profile.ShowSendKeys, profile.HideSendKeys, profile.Manuals, profile.OpCards, profile.Snaps, profile.Titles, profile.Carts, profile.Boxes, profile.Nfo });

					for (int i = 0; i < ProfileNode.CellTypes.Length; i++)
					{
						switch (ProfileNode.CellTypes[i])
						{
							case ListViewEx.CellType.ComboBox:
								this.lvwProfiles.AddComboBoxCell(-1, i, new string[] { "Emulator", "Game" });
								break;
							case ListViewEx.CellType.ComboBoxBoolean:
								this.lvwProfiles.AddComboBoxBooleanCell(-1, i);
								break;
							case ListViewEx.CellType.TextBox:
								this.lvwProfiles.AddTextBoxCell(-1, i);
								break;
							case ListViewEx.CellType.Button:
								this.lvwProfiles.AddButtonCell(-1, i, "...");
								break;
						}
					}
				}

				this.lvwProfiles.ItemChanged += new ListViewEx.ItemChangedHandler(lvwProfile_ItemChanged);
				this.lvwProfiles.ItemChecked += new ItemCheckedEventHandler(lvwProfile_ItemChecked);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("PopulateProfilesListView", "OptionsForm", ex.Message, ex.StackTrace);
			}
		}

		private void butProfileNew_Click(object sender, EventArgs e)
		{
			Globals.Profiles.ProfileList.Add(new ProfileNode());

			PopulateProfilesListView();
		}

		private void butProfileDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if (lvwProfiles.SelectedItems.Count == 1)
				{
					ProfileNode emulatorProfile = Globals.Profiles.ProfileList[lvwProfiles.SelectedItems[0].Index];
					Globals.Profiles.ProfileList.Remove(emulatorProfile);

					if (File.Exists(Path.Combine(Settings.Folders.Profiles, emulatorProfile.Name + ".xml")))
					{
						try
						{
							File.Delete(Path.Combine(Settings.Folders.Profiles, emulatorProfile.Name + ".xml"));
						}
						catch
						{
						}
					}

					PopulateProfilesListView();
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("butProfileDelete_Click", "OptionsForm", ex.Message, ex.StackTrace);
			}
		}

		void lvwProfile_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			if (e.Item.Checked == true)
				Globals.Profiles.ProfileList[e.Item.Index].Enabled = true;
			else
				Globals.Profiles.ProfileList[e.Item.Index].Enabled = false;
		}

		void lvwProfile_ItemChanged(int row, int col, string oldText, string newText)
		{
			try
			{
				ProfileNode profile = Globals.Profiles.ProfileList[row];

				switch (col)
				{
					case 0: // Enabled
						break;
					case 1: // Name
						if (newText != null)
						{
							profile.Name = newText;

							if (File.Exists(Path.Combine(Settings.Folders.Profiles, oldText + ".xml")))
							{
								try
								{
									File.Move(Path.Combine(Settings.Folders.Profiles, oldText + ".xml"), Path.Combine(Settings.Folders.Profiles, newText + ".xml"));
								}
								catch
								{
								}
							}
						}
						break;
					case 2: // Type
						if (newText != null)
							profile.Type = newText;
						break;
					case 3: // Layout
						{
							string fileName = null;

							if (FileIO.TryOpenLayout(this, Settings.Folders.Layout, profile.Layout + ".xml", out fileName))
							{
								string layoutName = Path.GetFileNameWithoutExtension(fileName);
								this.lvwProfiles.SetCellText(row, col, layoutName);
								profile.Layout = layoutName;
							}
						}
						break;
					case 4: // LayoutOverride
						{
							string folderName = null;

							if (FileIO.TryOpenFolder(this, Settings.Folders.Layout, out folderName))
							{
								string layoutFolder = FileIO.GetRelativeFolder(Settings.Folders.Layout, folderName, false);
								this.lvwProfiles.SetCellText(row, col, layoutFolder);
								profile.LayoutOverride = layoutFolder;
							}
						}
						break;
					case 5: // LayoutSub
						{
							string fileName = null;

							if (FileIO.TryOpenLayout(this, Settings.Folders.Layout, profile.LayoutSub + ".xml", out fileName))
							{
								string layoutName = Path.GetFileNameWithoutExtension(fileName);
								this.lvwProfiles.SetCellText(row, col, layoutName);
								profile.LayoutSub = layoutName;
							}
						}
						break;
					case 6: // Labels
						{
							string fileName = null;

							if (FileIO.TryOpenLabel(this, Settings.Folders.Labels, profile.Labels + ".ini", out fileName))
							{
								string labelName = Path.GetFileNameWithoutExtension(fileName);
								this.lvwProfiles.SetCellText(row, col, labelName);
								profile.Labels = labelName;
							}
						}
						break;
					case 7: // Database
						{
							string fileName = null;

							if (FileIO.TryOpenDatabase(this, Settings.Folders.Database, profile.Database + ".mdb", out fileName))
							{
								string databaseName = Path.GetFileNameWithoutExtension(fileName);
								this.lvwProfiles.SetCellText(row, col, databaseName);
								profile.Database = databaseName;
							}
						}
						break;
					case 8: // Executable
						{
							string fileName = null;

							if (FileIO.TryOpenExe(this, null, out fileName))
							{
								string exeName = Path.GetFileName(fileName);
								this.lvwProfiles.SetCellText(row, col, exeName);
								profile.Executable = exeName;
							}
						}
						break;
					case 9: // WindowTitle
						if (newText != null)
							profile.WindowTitle = newText;
						break;
					case 10: // WindowClass
						if (newText != null)
							profile.WindowClass = newText;
						break;
					case 11: // UseExe
						if (newText != null)
							profile.UseExe = StringTools.FromString<bool>(newText);
						break;
					case 12: // Screenshot
						if (newText != null)
							profile.Screenshot = StringTools.FromString<bool>(newText);
						break;
					case 13: // Minimize
						if (newText != null)
							profile.Minimize = StringTools.FromString<bool>(newText);
						break;
					case 14: // Maximize
						if (newText != null)
							profile.Maximize = StringTools.FromString<bool>(newText);
						break;
					case 15: // ShowKey
						using (frmInput inputForm = new frmInput())
						{
							if (inputForm.ShowDialog(this) == DialogResult.OK)
							{
								this.lvwProfiles.SetCellText(row, col, frmInput.InputName);

								if (frmInput.InputName == String.Empty)
									profile.ShowKey = String.Empty;
								else
									profile.ShowKey += (profile.ShowKey == String.Empty ? "" : "|") + frmInput.InputName;
							}
						}
						break;
					case 16: // HideKey
						using (frmInput inputForm = new frmInput())
						{
							if (inputForm.ShowDialog(this) == DialogResult.OK)
							{
								this.lvwProfiles.SetCellText(row, col, frmInput.InputName);
								if (frmInput.InputName == String.Empty)
									profile.HideKey = String.Empty;
								else
									profile.HideKey += (profile.HideKey == String.Empty ? "" : "|") + frmInput.InputName;
							}
						}
						break;
					case 17: // ShowSendKeys
						if (newText != null)
							profile.ShowSendKeys = newText;
						break;
					case 18: // HideSendKeys
						if (newText != null)
							profile.HideSendKeys = newText;
						break;
					case 19: // Manuals
						{
							string folderName = null;

							if (FileIO.TryOpenFolder(this, null, out folderName))
							{
								this.lvwProfiles.SetCellText(row, col, folderName);
								profile.Manuals = folderName;
							}
						}
						break;
					case 20: // OpCards
						{
							string folderName = null;

							if (FileIO.TryOpenFolder(this, null, out folderName))
							{
								this.lvwProfiles.SetCellText(row, col, folderName);
								profile.OpCards = folderName;
							}
						}
						break;
					case 21: // Snaps
						{
							string folderName = null;

							if (FileIO.TryOpenFolder(this, null, out folderName))
							{
								this.lvwProfiles.SetCellText(row, col, folderName);
								profile.Snaps = folderName;
							}
						}
						break;
					case 22: // Titles
						{
							string folderName = null;

							if (FileIO.TryOpenFolder(this, null, out folderName))
							{
								this.lvwProfiles.SetCellText(row, col, folderName);
								profile.Titles = folderName;
							}
						}
						break;
					case 23: // Carts
						{
							string folderName = null;

							if (FileIO.TryOpenFolder(this, null, out folderName))
							{
								this.lvwProfiles.SetCellText(row, col, folderName);
								profile.Carts = folderName;
							}
						}
						break;
					case 24: // Boxes
						{
							string folderName = null;

							if (FileIO.TryOpenFolder(this, null, out folderName))
							{
								this.lvwProfiles.SetCellText(row, col, folderName);
								profile.Boxes = folderName;
							}
						}
						break;
					case 25: // Nfo
						{
							string folderName = null;

							if (FileIO.TryOpenFolder(this, null, out folderName))
							{
								this.lvwProfiles.SetCellText(row, col, folderName);
								profile.Nfo = folderName;
							}
						}
						break;
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("lvwProfile_ItemChanged", "OptionsForm", ex.Message, ex.StackTrace);
			}
		}
		
#endregion

#region Data

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
		
	
#endregion

#region Display

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

#endregion

#region General

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

#endregion

#region Input

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

#endregion

#region IRC

		private void GetIRCOptions()
		{
			txtIRCServer.Text = Settings.IRC.Server;
			txtIRCPort.Text = StringTools.ToString<int>(Settings.IRC.Port);
			txtIRCChannel.Text = Settings.IRC.Channel;
			txtIRCNickName.Text = Settings.IRC.NickName;
			txtIRCUserName.Text = Settings.IRC.UserName;
			txtIRCRealName.Text = Settings.IRC.RealName;
			chkIRCIsInvisible.Checked = Settings.IRC.IsInvisible;
		}

		private void SetIRCOptions()
		{
			Settings.IRC.Server = txtIRCServer.Text;
			Settings.IRC.Port = StringTools.FromString<int>(txtIRCPort.Text);
			Settings.IRC.Channel = txtIRCChannel.Text;
			Settings.IRC.NickName = txtIRCNickName.Text;
			Settings.IRC.UserName = txtIRCUserName.Text;
			Settings.IRC.RealName = txtIRCRealName.Text;
			Settings.IRC.IsInvisible = chkIRCIsInvisible.Checked;
		}

#endregion

#region Layout

		private void GetLayoutOptions()
		{
			if (Globals.Layout == null)
				return;

			txtLayoutName.Text = Globals.Layout.Name;
			txtLayoutBackground.Text = Globals.Layout.BackgroundFileName;
			txtLayoutSub.Text = Globals.Layout.LayoutSub;
			chkShowMiniInfo.Checked = Globals.Layout.ShowMiniInfo;
			chkLayoutColorsEnable.Checked = Globals.Layout.ColorImagesEnabled;

			chkShowLayoutTopMost.Checked = Settings.Display.ShowLayoutTopMost;
			chkShowLayoutForceForeground.Checked = Settings.Display.ShowLayoutForceForeground;
			chkShowLayoutGiveFocus.Checked = Settings.Display.ShowLayoutGiveFocus;
			chkShowLayoutMouseClick.Checked = Settings.Display.ShowLayoutMouseClick;
			chkShowRetry.Checked = Settings.Display.ShowRetryEnable;
			chkShowRetryExitOnFail.Checked = Settings.Display.ShowRetryExitOnFail;
			nudShowRetryNumRetrys.Value = Settings.Display.ShowRetryNumRetrys;
			nudShowRetryInterval.Value = Settings.Display.ShowRetryInterval;

			PopulateLayoutColorsListView();
		}

		private void SetLayoutOptions()
		{
			if (Globals.Layout == null)
				return;

			if (Settings.Layout.Name != txtLayoutName.Text)
				Globals.Layout.PromptToSave = true;

			Settings.Layout.Name = txtLayoutName.Text;
			Globals.Layout.BackgroundFileName = txtLayoutBackground.Text;
			Globals.Layout.LayoutSub = txtLayoutSub.Text;
			Globals.Layout.ShowMiniInfo = chkShowMiniInfo.Checked;
			Globals.Layout.ColorImagesEnabled = chkLayoutColorsEnable.Checked;

			Settings.Display.ShowLayoutTopMost = chkShowLayoutTopMost.Checked;
			Settings.Display.ShowLayoutForceForeground = chkShowLayoutForceForeground.Checked;
			Settings.Display.ShowLayoutGiveFocus = chkShowLayoutGiveFocus.Checked;
			Settings.Display.ShowLayoutMouseClick = chkShowLayoutMouseClick.Checked;
			Settings.Display.ShowRetryEnable = chkShowRetry.Checked;
			Settings.Display.ShowRetryExitOnFail = chkShowRetryExitOnFail.Checked;
			Settings.Display.ShowRetryNumRetrys = (int)nudShowRetryNumRetrys.Value;
			Settings.Display.ShowRetryInterval = (int)nudShowRetryInterval.Value;
		}

		private void butLayoutBackground_Click(object sender, EventArgs e)
		{
			try
			{
				string imageFile = null;

				if (FileIO.TryOpenImage(this, Settings.Folders.Media, out imageFile))
				{
					string fileName = Path.GetFileName(imageFile);

					txtLayoutBackground.Text = fileName;

					if (Globals.Layout != null)
					{
						Globals.Layout.PromptToSave = true;
						Globals.Layout.BackgroundFileName = fileName;
						Globals.Layout.LoadBackground();

						Globals.MainForm.UpdateDisplay();
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("butLayoutBackground_Click", "OptionsForm", ex.Message, ex.StackTrace);
			}
		}

		private void butLayoutSub_Click(object sender, EventArgs e)
		{
			try
			{
				string layoutName = null;

				if (FileIO.TryOpenLayout(this, Settings.Folders.Layout, Globals.Layout.LayoutSub + ".xml", out layoutName))
					txtLayoutSub.Text = Path.GetFileNameWithoutExtension(layoutName);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("butLayoutSub_Click", "OptionsForm", ex.Message, ex.StackTrace);
			}
		}

		private void PopulateLayoutColorsListView()
		{
			try
			{
				this.lvwLayoutColors.ItemChanged -= new ListViewEx.ItemChangedHandler(lvwLayoutColors_ItemChanged);

				this.lvwLayoutColors.Items.Clear();

				foreach (Layout.ColorImageNode colorImage in Globals.Layout.ColorImageArray)
				{
					this.lvwLayoutColors.Items.Add(colorImage.Name);
					//this.lvwLayoutColors.Items[this.lvwLayoutColors.Items.Count - 1].SubItems.Clear();
					this.lvwLayoutColors.Items[this.lvwLayoutColors.Items.Count - 1].SubItems.AddRange(new string[] { colorImage.Image });

					this.lvwLayoutColors.AddButtonCell(-1, 1, "...");
				}

				this.lvwLayoutColors.ItemChanged += new ListViewEx.ItemChangedHandler(lvwLayoutColors_ItemChanged);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("PopulateLayoutColorsListView", "OptionsForm", ex.Message, ex.StackTrace);
			}
		}

		private void chkLayoutColorsEnable_CheckedChanged(object sender, EventArgs e)
		{
			Globals.Layout.PromptToSave = true;
		}

		void lvwLayoutColors_ItemChanged(int row, int col, string oldText, string newText)
		{
			try
			{
				if (Globals.Layout == null)
					return;

				switch (col)
				{
					case 0: // Color
						break;
					case 1: // Image
						{
							string imageFileName = null;

							if (FileIO.TryOpenImage(this, Settings.Folders.Media, out imageFileName))
							{
								Globals.Layout.PromptToSave = true;
								string fileName = Path.GetFileName(imageFileName);
								this.lvwLayoutColors.SetCellText(row, col, fileName);
								Globals.Layout.ColorImageArray[row].Image = fileName;
							}
						}
						break;
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("lvwLayoutColors_ItemChanged", "OptionsForm", ex.Message, ex.StackTrace);
			}
		}

#endregion

#region Mame

		private string[] NumPlayers =
        {
            "*",
            "0",
            "1",
            "2",
            "3", 
            "4",
            "5",
            "6",
            "7",
            "8"
        };

		private string[] Alternating =
        {
            "*",
            "Yes",
            "No"
        };

		private void GetMAMEOptions()
		{
			lblMAMEVersionValue.Text = Settings.MAME.Version;
			txtMAMELayout.Text = Settings.MAME.Layout;
			txtMAMELayoutOverride.Text = Settings.MAME.LayoutOverride;
			txtMAMELayoutSub.Text = Settings.MAME.LayoutSub;
			chkAutoShow.Checked = Settings.MAME.AutoShow;
			nudAutoShowDelay.Value = Settings.MAME.AutoShowDelay;
			nudAutoShowTimeout.Value = Settings.MAME.AutoShowTimeout;
			chkMAMEOutputSystem.Checked = Settings.MAME.UseMAMEOutputSystem;
			chkMAMEUseShowKey.Checked = Settings.MAME.UseShowKey;
			chkMAMEScreenshot.Checked = Settings.MAME.Screenshot;
			chkMAMEDetectPause.Checked = Settings.MAME.DetectPause;
			chkMAMESendPause.Checked = Settings.MAME.SendPause;

			switch (Settings.MAME.PauseMode)
			{
				case PauseMode.Msg:
					rdoMAMEPauseMsg.Checked = true;
					break;
				case PauseMode.Diff:
					rdoMAMEPauseDiff.Checked = true;
					break;
				case PauseMode.Key:
					rdoMAMEPauseKey.Checked = true;
					break;
			}

			chkMAMESkipDisclaimer.Checked = Settings.MAME.SkipDisclaimer;
			cboMAMEBak.Text = Settings.MAME.Bak;

			PopulateLayoutMapsListView();
		}

		private void SetMAMEOptions()
		{
			Settings.MAME.Layout = txtMAMELayout.Text;
			Settings.MAME.LayoutOverride = txtMAMELayoutOverride.Text;
			Settings.MAME.LayoutSub = txtMAMELayoutSub.Text;
			Settings.MAME.AutoShow = chkAutoShow.Checked;
			Settings.MAME.AutoShowDelay = (int)nudAutoShowDelay.Value;
			Settings.MAME.AutoShowTimeout = (int)nudAutoShowTimeout.Value;
			Settings.MAME.UseMAMEOutputSystem = chkMAMEOutputSystem.Checked;
			Settings.MAME.UseShowKey = chkMAMEUseShowKey.Checked;
			Settings.MAME.Screenshot = chkMAMEScreenshot.Checked;
			Settings.MAME.DetectPause = chkMAMEDetectPause.Checked;
			Settings.MAME.SendPause = chkMAMESendPause.Checked;

			if (rdoMAMEPauseMsg.Checked)
				Settings.MAME.PauseMode = PauseMode.Msg;

			if (rdoMAMEPauseDiff.Checked)
				Settings.MAME.PauseMode = PauseMode.Diff;

			if (rdoMAMEPauseKey.Checked)
				Settings.MAME.PauseMode = PauseMode.Key;

			Settings.MAME.SkipDisclaimer = chkMAMESkipDisclaimer.Checked;
			Settings.MAME.Bak = cboMAMEBak.Text;
		}

		private void PopulateLayoutMapsListView()
		{
			try
			{
				this.lvwLayoutMaps.ItemChanged -= new ListViewEx.ItemChangedHandler(lvwLayoutMaps_ItemChanged);
				this.lvwLayoutMaps.ItemChecked -= new ItemCheckedEventHandler(lvwLayoutMaps_ItemChecked);

				this.lvwLayoutMaps.Items.Clear();

				foreach (LayoutMapNode layoutMap in Globals.LayoutMaps.LayoutMapsList)
				{
					this.lvwLayoutMaps.Items.Add(layoutMap.Control);
					this.lvwLayoutMaps.Items[this.lvwLayoutMaps.Items.Count - 1].Checked = layoutMap.Enabled;
					this.lvwLayoutMaps.Items[this.lvwLayoutMaps.Items.Count - 1].SubItems.Clear();
					this.lvwLayoutMaps.Items[this.lvwLayoutMaps.Items.Count - 1].SubItems.AddRange(new string[] { layoutMap.Constant, layoutMap.Control, layoutMap.NumPlayers, layoutMap.Alternating, layoutMap.Layout });

					List<string> ConstantNames = new List<string>();
					List<string> ControlNames = new List<string>();

					ConstantNames.Add("*");
					ConstantNames.AddRange(Globals.ControlsData.GetConstantArray());
					ControlNames.Add("*");
					ControlNames.AddRange(Globals.ControlsData.GetControlArray());

					this.lvwLayoutMaps.AddComboBoxCell(-1, 1, ConstantNames.ToArray());
					this.lvwLayoutMaps.AddComboBoxCell(-1, 2, ControlNames.ToArray(), true);
					this.lvwLayoutMaps.AddComboBoxCell(-1, 3, NumPlayers);
					this.lvwLayoutMaps.AddComboBoxCell(-1, 4, Alternating);
					this.lvwLayoutMaps.AddButtonCell(-1, 5, "...");
				}

				this.lvwLayoutMaps.ItemChanged += new ListViewEx.ItemChangedHandler(lvwLayoutMaps_ItemChanged);
				this.lvwLayoutMaps.ItemChecked += new ItemCheckedEventHandler(lvwLayoutMaps_ItemChecked);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("PopulateLayoutMapsListView", "OptionsForm", ex.Message, ex.StackTrace);
			}
		}

		private void butMAMELayout_Click(object sender, EventArgs e)
		{
			try
			{
				string layoutName = null;

				if (FileIO.TryOpenLayout(this, Settings.Folders.Layout, Settings.MAME.Layout + ".xml", out layoutName))
					txtMAMELayout.Text = Path.GetFileNameWithoutExtension(layoutName);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("butMAMELayout_Click", "OptionsForm", ex.Message, ex.StackTrace);
			}
		}

		private void butMAMELayoutOverride_Click(object sender, EventArgs e)
		{
			try
			{
				string selectedPath = null;

				if (FileIO.TryOpenFolder(this, Settings.Folders.Layout, out selectedPath))
					txtMAMELayoutOverride.Text = FileIO.GetRelativeFolder(Settings.Folders.Layout, selectedPath, false);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("butMAMELayoutOverride_Click", "OptionsForm", ex.Message, ex.StackTrace);
			}
		}

		private void butMAMELayoutSub_Click(object sender, EventArgs e)
		{
			try
			{
				string layoutName = null;

				if (FileIO.TryOpenLayout(this, Settings.Folders.Layout, Settings.MAME.LayoutSub + ".xml", out layoutName))
					txtMAMELayoutSub.Text = Path.GetFileNameWithoutExtension(layoutName);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("butMAMELayoutSub_Click", "OptionsForm", ex.Message, ex.StackTrace);
			}
		}

		void lvwLayoutMaps_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			if (e.Item.Checked == true)
				Globals.LayoutMaps.LayoutMapsList[e.Item.Index].Enabled = true;
			else
				Globals.LayoutMaps.LayoutMapsList[e.Item.Index].Enabled = false;

			Globals.LayoutMaps.WriteLayoutMapsXml(Path.Combine(Settings.Folders.LayoutMaps, "MAME.xml"));
		}

		void lvwLayoutMaps_ItemChanged(int row, int col, string oldText, string newText)
		{
			try
			{
				LayoutMapNode layoutMap = Globals.LayoutMaps.LayoutMapsList[row];

				switch (col)
				{
					case 0: // Enabled
						break;
					case 1: // Constant
						if (newText != null)
						{
							layoutMap.Constant = newText;

							Globals.LayoutMaps.WriteLayoutMapsXml(Path.Combine(Settings.Folders.LayoutMaps, "MAME.xml"));
						}
						break;
					case 2: // Control
						if (newText != null)
						{
							layoutMap.Control = newText;

							Globals.LayoutMaps.WriteLayoutMapsXml(Path.Combine(Settings.Folders.LayoutMaps, "MAME.xml"));
						}
						break;
					case 3: // NumPlayers
						if (newText != null)
						{
							layoutMap.NumPlayers = newText;

							Globals.LayoutMaps.WriteLayoutMapsXml(Path.Combine(Settings.Folders.LayoutMaps, "MAME.xml"));
						}
						break;
					case 4: // Alternating
						if (newText != null)
						{
							layoutMap.Alternating = newText;

							Globals.LayoutMaps.WriteLayoutMapsXml(Path.Combine(Settings.Folders.LayoutMaps, "MAME.xml"));
						}
						break;
					case 5: // Layout
						{
							string fileName = null;

							if (FileIO.TryOpenLayout(this, Settings.Folders.Layout, layoutMap.Layout + ".xml", out fileName))
							{
								string layoutName = Path.GetFileNameWithoutExtension(fileName);
								this.lvwLayoutMaps.SetCellText(row, col, layoutName);
								layoutMap.Layout = layoutName;
							}

							Globals.LayoutMaps.WriteLayoutMapsXml(Path.Combine(Settings.Folders.LayoutMaps, "MAME.xml"));
						}
						break;
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("lvwLayoutMaps_ItemChanged", "OptionsForm", ex.Message, ex.StackTrace);
			}
		}

		private void butLayoutMapNew_Click(object sender, EventArgs e)
		{
			LayoutMapNode layoutMap = new LayoutMapNode(true, "*", "*", "*", "*", String.Empty);
			Globals.LayoutMaps.LayoutMapsList.Add(layoutMap);

			this.lvwLayoutMaps.Items.Add(layoutMap.Control);
			this.lvwLayoutMaps.Items[this.lvwLayoutMaps.Items.Count - 1].Checked = layoutMap.Enabled;
			this.lvwLayoutMaps.Items[this.lvwLayoutMaps.Items.Count - 1].SubItems.Clear();
			this.lvwLayoutMaps.Items[this.lvwLayoutMaps.Items.Count - 1].SubItems.AddRange(new string[] { layoutMap.Constant, layoutMap.Control, layoutMap.NumPlayers, layoutMap.Alternating, layoutMap.Layout });

			List<string> ConstantNames = new List<string>();
			List<string> ControlNames = new List<string>();

			ConstantNames.Add("*");
			ConstantNames.AddRange(Globals.ControlsData.GetConstantArray());
			ControlNames.Add("*");
			ControlNames.AddRange(Globals.ControlsData.GetControlArray());

			this.lvwLayoutMaps.AddComboBoxCell(-1, 1, ConstantNames.ToArray());
			this.lvwLayoutMaps.AddComboBoxCell(-1, 2, ControlNames.ToArray(), true);
			this.lvwLayoutMaps.AddComboBoxCell(-1, 3, NumPlayers);
			this.lvwLayoutMaps.AddComboBoxCell(-1, 4, Alternating);
			this.lvwLayoutMaps.AddButtonCell(-1, 5, "...");
		}

		private void butLayoutMapDelete_Click(object sender, EventArgs e)
		{
			if (lvwLayoutMaps.SelectedItems.Count == 0)
				return;

			int index = lvwLayoutMaps.SelectedItems[0].Index;

			Globals.LayoutMaps.LayoutMapsList.RemoveAt(index);
			this.lvwLayoutMaps.Items[index].Remove();
		}

		private void butLayoutMapUp_Click(object sender, EventArgs e)
		{
			if (lvwLayoutMaps.SelectedItems.Count == 0)
				return;

			int index = lvwLayoutMaps.SelectedItems[0].Index;

			if (index > 0)
			{
				LayoutMapNode layoutMap1 = Globals.LayoutMaps.LayoutMapsList[index - 1];
				LayoutMapNode layoutMap2 = Globals.LayoutMaps.LayoutMapsList[index];

				Globals.LayoutMaps.LayoutMapsList[index] = layoutMap1;
				Globals.LayoutMaps.LayoutMapsList[index - 1] = layoutMap2;

				lvwLayoutMaps.MoveListViewItem(ListViewEx.MoveDirection.Up);
			}

		}

		private void butLayoutMapDown_Click(object sender, EventArgs e)
		{
			if (lvwLayoutMaps.SelectedItems.Count == 0)
				return;

			int index = lvwLayoutMaps.SelectedItems[0].Index;

			if (index < lvwLayoutMaps.Items.Count - 1)
			{
				LayoutMapNode layoutMap1 = Globals.LayoutMaps.LayoutMapsList[index + 1];
				LayoutMapNode layoutMap2 = Globals.LayoutMaps.LayoutMapsList[index];

				Globals.LayoutMaps.LayoutMapsList[index] = layoutMap1;
				Globals.LayoutMaps.LayoutMapsList[index + 1] = layoutMap2;

				lvwLayoutMaps.MoveListViewItem(ListViewEx.MoveDirection.Down);
			}
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
			MessageBox.Show(this, File.ReadAllText(Path.Combine(Settings.Folders.Data, "ShowHelp.txt")), "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

#endregion

#region MAMEFilters

		private void GetMAMEFilters()
		{
			if (!MAMEFilter.TryLoadXml(Settings.Files.FilterXml, out Globals.MAMEFilter))
			{
				Globals.MAMEFilter = new MAMEFilter();

				MAMEFilter.TrySaveXml(Settings.Files.FilterXml, Globals.MAMEFilter);
			}

			chkNoClones.Checked = Globals.MAMEFilter.NoClones;
			chkNoBios.Checked = Globals.MAMEFilter.NoBios;
			chkNoDevice.Checked = Globals.MAMEFilter.NoDevice;
			chkNoAdult.Checked = Globals.MAMEFilter.NoAdult;
			chkNoMahjong.Checked = Globals.MAMEFilter.NoMahjong;
			chkNoGambling.Checked = Globals.MAMEFilter.NoGambling;
			chkNoCasino.Checked = Globals.MAMEFilter.NoCasino;
			chkNoMechanical.Checked = Globals.MAMEFilter.NoMechanical;
			chkNoReels.Checked = Globals.MAMEFilter.NoReels;
			chkNoUtilities.Checked = Globals.MAMEFilter.NoUtilities;
			chkNoNotClassified.Checked = Globals.MAMEFilter.NoNotClassified;
			chkRunnableOnly.Checked = Globals.MAMEFilter.RunnableOnly;
			chkArcadeOnly.Checked = Globals.MAMEFilter.ArcadeOnly;
			chkNoSystemExceptChd.Checked = Globals.MAMEFilter.NoSystemExceptChd;
			chkNoPreliminary.Checked = Globals.MAMEFilter.NoPreliminary;
			chkNoImperfect.Checked = Globals.MAMEFilter.NoImperfect;
			cboFilterRotation.Items.AddRange(MAMEFilter.FilterRotationArray);
			cboFilterRotation.SelectedIndex = (int)Globals.MAMEFilter.FilterRotation;
			txtNameIncludes.Text = GetNameFilterString(Globals.MAMEFilter.NameIncludeList);
			txtDescriptionExcludes.Text = GetNameFilterString(Globals.MAMEFilter.DescriptionExcludeList);

			this.chkNoClones.CheckedChanged += new System.EventHandler(this.chkMAMEFilters_CheckedChanged);
			this.chkNoBios.CheckedChanged += new System.EventHandler(this.chkMAMEFilters_CheckedChanged);
			this.chkNoDevice.CheckedChanged += new System.EventHandler(this.chkMAMEFilters_CheckedChanged);
			this.chkNoAdult.CheckedChanged += new System.EventHandler(this.chkMAMEFilters_CheckedChanged);
			this.chkNoMahjong.CheckedChanged += new System.EventHandler(this.chkMAMEFilters_CheckedChanged);
			this.chkNoGambling.CheckedChanged += new System.EventHandler(this.chkMAMEFilters_CheckedChanged);
			this.chkNoCasino.CheckedChanged += new System.EventHandler(this.chkMAMEFilters_CheckedChanged);
			this.chkNoMechanical.CheckedChanged += new System.EventHandler(this.chkMAMEFilters_CheckedChanged);
			this.chkNoReels.CheckedChanged += new System.EventHandler(this.chkMAMEFilters_CheckedChanged);
			this.chkNoUtilities.CheckedChanged += new System.EventHandler(this.chkMAMEFilters_CheckedChanged);
			this.chkNoNotClassified.CheckedChanged += new System.EventHandler(this.chkMAMEFilters_CheckedChanged);
			this.chkRunnableOnly.CheckedChanged += new System.EventHandler(this.chkMAMEFilters_CheckedChanged);
			this.chkArcadeOnly.CheckedChanged += new System.EventHandler(this.chkMAMEFilters_CheckedChanged);
			this.chkNoSystemExceptChd.CheckedChanged += new System.EventHandler(this.chkMAMEFilters_CheckedChanged);
			this.chkNoPreliminary.CheckedChanged += new System.EventHandler(this.chkMAMEFilters_CheckedChanged);
			this.chkNoImperfect.CheckedChanged += new System.EventHandler(this.chkMAMEFilters_CheckedChanged);
			this.cboFilterRotation.SelectedIndexChanged += new System.EventHandler(this.cboMAMEFilters_SelectedIndexChanged);
			this.txtNameIncludes.TextChanged += new System.EventHandler(this.txtMAMEFilters_TextChanged);
			this.txtDescriptionExcludes.TextChanged += new System.EventHandler(this.txtMAMEFilters_TextChanged);
		}

		private void SetMAMEFilters()
		{
			Globals.MAMEFilter.NoClones = chkNoClones.Checked;
			Globals.MAMEFilter.NoBios = chkNoBios.Checked;
			Globals.MAMEFilter.NoDevice = chkNoDevice.Checked;
			Globals.MAMEFilter.NoAdult = chkNoAdult.Checked;
			Globals.MAMEFilter.NoMahjong = chkNoMahjong.Checked;
			Globals.MAMEFilter.NoGambling = chkNoGambling.Checked;
			Globals.MAMEFilter.NoCasino = chkNoCasino.Checked;
			Globals.MAMEFilter.NoMechanical = chkNoMechanical.Checked;
			Globals.MAMEFilter.NoReels = chkNoReels.Checked;
			Globals.MAMEFilter.NoUtilities = chkNoUtilities.Checked;
			Globals.MAMEFilter.NoNotClassified = chkNoNotClassified.Checked;
			Globals.MAMEFilter.RunnableOnly = chkRunnableOnly.Checked;
			Globals.MAMEFilter.ArcadeOnly = chkArcadeOnly.Checked;
			Globals.MAMEFilter.NoSystemExceptChd = chkNoSystemExceptChd.Checked;
			Globals.MAMEFilter.NoPreliminary = chkNoPreliminary.Checked;
			Globals.MAMEFilter.NoImperfect = chkNoImperfect.Checked;
			Globals.MAMEFilter.FilterRotation = (FilterRotationOptions)cboFilterRotation.SelectedIndex;
			Globals.MAMEFilter.NameIncludeList = GetMameFilterList(txtNameIncludes.Text);
			Globals.MAMEFilter.DescriptionExcludeList = GetMameFilterList(txtDescriptionExcludes.Text);

			this.chkNoClones.CheckedChanged -= new System.EventHandler(this.chkMAMEFilters_CheckedChanged);
			this.chkNoBios.CheckedChanged -= new System.EventHandler(this.chkMAMEFilters_CheckedChanged);
			this.chkNoDevice.CheckedChanged -= new System.EventHandler(this.chkMAMEFilters_CheckedChanged);
			this.chkNoAdult.CheckedChanged -= new System.EventHandler(this.chkMAMEFilters_CheckedChanged);
			this.chkNoMahjong.CheckedChanged -= new System.EventHandler(this.chkMAMEFilters_CheckedChanged);
			this.chkNoGambling.CheckedChanged -= new System.EventHandler(this.chkMAMEFilters_CheckedChanged);
			this.chkNoCasino.CheckedChanged -= new System.EventHandler(this.chkMAMEFilters_CheckedChanged);
			this.chkNoMechanical.CheckedChanged -= new System.EventHandler(this.chkMAMEFilters_CheckedChanged);
			this.chkNoReels.CheckedChanged -= new System.EventHandler(this.chkMAMEFilters_CheckedChanged);
			this.chkNoUtilities.CheckedChanged -= new System.EventHandler(this.chkMAMEFilters_CheckedChanged);
			this.chkNoNotClassified.CheckedChanged -= new System.EventHandler(this.chkMAMEFilters_CheckedChanged);
			this.chkRunnableOnly.CheckedChanged -= new System.EventHandler(this.chkMAMEFilters_CheckedChanged);
			this.chkArcadeOnly.CheckedChanged -= new System.EventHandler(this.chkMAMEFilters_CheckedChanged);
			this.chkNoSystemExceptChd.CheckedChanged -= new System.EventHandler(this.chkMAMEFilters_CheckedChanged);
			this.chkNoPreliminary.CheckedChanged -= new System.EventHandler(this.chkMAMEFilters_CheckedChanged);
			this.chkNoImperfect.CheckedChanged -= new System.EventHandler(this.chkMAMEFilters_CheckedChanged);
			this.cboFilterRotation.SelectedIndexChanged -= new System.EventHandler(this.cboMAMEFilters_SelectedIndexChanged);
			this.txtNameIncludes.TextChanged -= new System.EventHandler(this.txtMAMEFilters_TextChanged);
			this.txtDescriptionExcludes.TextChanged -= new System.EventHandler(this.txtMAMEFilters_TextChanged);

			MAMEFilter.TrySaveXml(Settings.Files.FilterXml, Globals.MAMEFilter);
		}

		private string GetNameFilterString(List<MAMEFilterNode> mameFilterList)
		{
			if (mameFilterList == null)
				return null;

			string[] mameFilterArray = new string[mameFilterList.Count];

			for(int i=0; i<mameFilterArray.Length; i++)
				mameFilterArray[i] = mameFilterList[i].Name;
			
			return String.Join(", ", mameFilterArray);
		}

		private List<MAMEFilterNode> GetMameFilterList(string mameFilterText)
		{
			List<MAMEFilterNode> mameFilterList = new List<MAMEFilterNode>();
			string[] filterArray = mameFilterText.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

			foreach(string filter in filterArray)
			{
				MAMEFilterNode mameFilterNode = new MAMEFilterNode();
				mameFilterNode.Name = filter.Trim();

				mameFilterList.Add(mameFilterNode);
			}

			return mameFilterList;
		}

		private void chkMAMEFilters_CheckedChanged(object sender, EventArgs e)
		{
			DoLoadMAMEData = true;
		}

		private void cboMAMEFilters_SelectedIndexChanged(object sender, EventArgs e)
		{
			DoLoadMAMEData = true;
		}

		private void txtMAMEFilters_TextChanged(object sender, EventArgs e)
		{
			DoLoadMAMEData = true;
		}

#endregion

#region MamePaths

		private bool DoLoadMAMEData = false;

		private void GetMAMEPaths()
		{
			txtMAMEExe.Text = Settings.Files.MAME.MAMEExe;
			txtCabinets.Text = Settings.Folders.MAME.Cabinets;
			txtCfg.Text = Settings.Folders.MAME.Cfg;
			txtCPanel.Text = Settings.Folders.MAME.CPanel;
			txtCtrlr.Text = Settings.Folders.MAME.Ctrlr;
			txtFlyers.Text = Settings.Folders.MAME.Flyers;
			txtHi.Text = Settings.Folders.MAME.Hi;
			txtIcons.Text = Settings.Folders.MAME.Icons;
			txtIni.Text = Settings.Folders.MAME.Ini;
			txtManuals.Text = Settings.Folders.MAME.Manuals;
			txtMarquees.Text = Settings.Folders.MAME.Marquees;
			txtNvRam.Text = Settings.Folders.MAME.NvRam;
			txtPCB.Text = Settings.Folders.MAME.PCB;
			txtPreviews.Text = Settings.Folders.MAME.Previews;
			txtSelect.Text = Settings.Folders.MAME.Select;
			txtSnap.Text = Settings.Folders.MAME.Snap;
			txtTitles.Text = Settings.Folders.MAME.Titles;

			butMAMEExe.Tag = txtMAMEExe;
			butCabinets.Tag = txtCabinets;
			butCfg.Tag = txtCfg;
			butCPanel.Tag = txtCPanel;
			butCtrlr.Tag = txtCtrlr;
			butFlyers.Tag = txtFlyers;
			butHi.Tag = txtHi;
			butIcons.Tag = txtIcons;
			butIni.Tag = txtIni;
			butManuals.Tag = txtManuals;
			butMarquees.Tag = txtMarquees;
			butNvRam.Tag = txtNvRam;
			butPCB.Tag = txtPCB;
			butPreviews.Tag = txtPreviews;
			butSelect.Tag = txtSelect;
			butSnap.Tag = txtSnap;
			butTitles.Tag = txtTitles;
		}

		private void SetMAMEPaths()
		{
			Settings.Files.MAME.MAMEExe = txtMAMEExe.Text;
			Settings.Folders.MAME.Cabinets = txtCabinets.Text;
			Settings.Folders.MAME.Cfg = txtCfg.Text;
			Settings.Folders.MAME.CPanel = txtCPanel.Text;
			Settings.Folders.MAME.Ctrlr = txtCtrlr.Text;
			Settings.Folders.MAME.Flyers = txtFlyers.Text;
			Settings.Folders.MAME.Hi = txtHi.Text;
			Settings.Folders.MAME.Icons = txtIcons.Text;
			Settings.Folders.MAME.Ini = txtIni.Text;
			Settings.Folders.MAME.Manuals = txtManuals.Text;
			Settings.Folders.MAME.Marquees = txtMarquees.Text;
			Settings.Folders.MAME.NvRam = txtNvRam.Text;
			Settings.Folders.MAME.PCB = txtPCB.Text;
			Settings.Folders.MAME.Previews = txtPreviews.Text;
			Settings.Folders.MAME.Select = txtSelect.Text;
			Settings.Folders.MAME.Snap = txtSnap.Text;
			Settings.Folders.MAME.Titles = txtTitles.Text;
		}

		private void SetFolder(string mameFolder, TextBox txtFolder, string name)
		{
			string newPath = Path.Combine(mameFolder, name);

			if (Directory.Exists(txtFolder.Text))
				return;

			if (Directory.Exists(newPath))
				txtFolder.Text = newPath;
		}

		private void CalcMAMEFolders(string mameExe)
		{
			string mameFolder = Path.GetDirectoryName(mameExe);

			SetFolder(mameFolder, txtCabinets, "cabinets");
			SetFolder(mameFolder, txtCfg, "cfg");
			SetFolder(mameFolder, txtCPanel, "cpanel");
			SetFolder(mameFolder, txtCtrlr, "ctrlr");
			SetFolder(mameFolder, txtFlyers, "flyers");
			SetFolder(mameFolder, txtHi, "hi");
			SetFolder(mameFolder, txtIcons, "icons");
			SetFolder(mameFolder, txtIni, "ini");
			SetFolder(mameFolder, txtManuals, "manuals");
			SetFolder(mameFolder, txtMarquees, "marquees");
			SetFolder(mameFolder, txtNvRam, "nvram");
			SetFolder(mameFolder, txtPCB, "pcb");
			SetFolder(mameFolder, txtPreviews, "previews");
			SetFolder(mameFolder, txtSelect, "select");
			SetFolder(mameFolder, txtSnap, "snap");
			SetFolder(mameFolder, txtTitles, "titles");
		}

		private string GetPath(string fileName)
		{
			string path = null;

			if (String.IsNullOrEmpty(fileName))
				return path;

			try
			{
				path = Path.GetDirectoryName(fileName);
			}
			catch
			{
			}

			return path;
		}

		private void butMAMEExe_Click(object sender, EventArgs e)
		{
			string fileName = null;

			if (FileIO.TryOpenExe(this, GetPath(txtMAMEExe.Text), out fileName))
			{
				txtMAMEExe.Text = fileName;
				CalcMAMEFolders(fileName);
				DoLoadMAMEData = true;
			}
		}

		private void butMAMEPaths_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			TextBox textBox = (TextBox)button.Tag;

			string folder = null;

			if (FileIO.TryOpenFolder(this, textBox.Text, out folder))
				textBox.Text = folder;
		}

#endregion

	}	
}