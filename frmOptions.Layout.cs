using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace CPWizard
{
	partial class frmOptions
	{
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
	}
}
