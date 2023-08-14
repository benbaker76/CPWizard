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
	}
}
