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
	}
}
