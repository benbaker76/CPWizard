using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace CPWizard
{
	public partial class frmPreview : Form
	{
		private ListViewColumnSorter lvwColumnSorter;

		public frmPreview()
		{
			InitializeComponent();
		}

		private void PreviewForm_Load(object sender, EventArgs e)
		{
			if (Globals.MAMEXml == null)
				return;

			int lastRomSelectedIndex = 0;

			try
			{
				lvwColumnSorter = new ListViewColumnSorter();
				lvwGameList.ListViewItemSorter = lvwColumnSorter;

				lvwGameList.Items.Clear();

				ListViewItem[] lvwItems = new ListViewItem[Globals.MAMEXml.GameList.Count];

				for (int i = 0; i < lvwItems.Length; i++)
				{
					MAMEMachineNode gameNode = Globals.MAMEXml.GameList[i];
					MAMEMachineNode parentNode = null;
					
					Globals.MAMEXml.TryGetParentROM(gameNode, out parentNode);

					string name = gameNode.Description;
					string rom = gameNode.Name;
					string sourceFile = gameNode.SourceFile;
					string cloneOf = gameNode.CloneOf;
					string romOf = gameNode.ROMOf;
					string parent = (gameNode.CloneOf == null && gameNode.ROMOf == null ? String.Empty : parentNode.Name);
					string constants = String.Empty;
					string controls = String.Empty;
					string numPlayers = (gameNode.InputList != null ? (gameNode.InputList.Count > 0 ? gameNode.InputList[0].Players.ToString() : String.Empty) : String.Empty);
					string alternating = String.Empty;

					foreach (MAMEInputNode intputNode in gameNode.InputList)
						foreach (MAMEControlNode controlNode in intputNode.ControlList)
							constants += controlNode.Constant + " ";

					if (gameNode.ControlsDat != null)
					{
						constants = gameNode.ControlsDat.GetConstantsString();
						controls = gameNode.ControlsDat.GetControlsString();
						numPlayers = gameNode.ControlsDat.NumPlayers.ToString();
						alternating = (gameNode.ControlsDat.Alternating == 1 ? "Yes" : "No");
					}
					else if (parentNode != null)
					{
						if (parentNode.ControlsDat != null)
						{
							constants = parentNode.ControlsDat.GetConstantsString();
							controls = parentNode.ControlsDat.GetControlsString();
							numPlayers = parentNode.ControlsDat.NumPlayers.ToString();
							alternating = (parentNode.ControlsDat.Alternating == 1 ? "Yes" : "No");
						}
					}

					if (rom == Settings.Preview.LastRomSelected)
						lastRomSelectedIndex = i;

					lvwItems[i] = new ListViewItem(new string[] { name, rom, sourceFile, cloneOf, romOf, parent, constants.Trim(), controls.Trim(), numPlayers, alternating });
				}

				lvwGameList.Items.AddRange(lvwItems);

				if (lvwGameList.Items.Count > 0)
				{
					lvwGameList.SelectedIndices.Add(lastRomSelectedIndex);
					lvwGameList.SelectedItems[0].EnsureVisible();
				}

				toolStripStatusLabel1.Text = String.Format("{0} Games.", Globals.MAMEXml.GameList.Count);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("PreviewForm_Load", "frmPreview", ex.Message, ex.StackTrace);
			}
		}

		private void ShowCPWizard()
		{
			if (Globals.MAMEXml == null)
				return;

			if (lvwGameList.SelectedIndices.Count == 0)
				return;

			try
			{
				Globals.EmulatorMode = EmulatorMode.MAME;
				Settings.Display.ShowScreenshot = false;
				Globals.ProgramManager.ReadAllGameData();

				Settings.MAME.GameName = lvwGameList.Items[lvwGameList.SelectedIndices[0]].SubItems[1].Text;

				if (Globals.LayoutForm != null)
					Globals.LayoutForm.Close();

				Globals.LayoutForm = new frmLayout();

				if (Settings.General.DynamicDataLoading)
					Globals.ProgramManager.LoadDataDynamically(Settings.MAME.GameName);

				Globals.MAMEManager.GetGameDetails(true, true, false);

				Globals.LayoutForm.Show(DisplayType.Preview, 640, 480, false);

				if (Settings.Data.General.ShowCPOnly)
				{
					Globals.DisplayMode = DisplayMode.Layout;
					Globals.ProgramManager.ShowScreen(Settings.Data.General.ExitToMenu);
				}
				else
				{
					Globals.DisplayMode = DisplayMode.MainMenu;
					Globals.ProgramManager.ShowScreen(true);
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ShowCPWizard", "frmPreview", ex.Message, ex.StackTrace);
			}
		}

		private void LaunchGame()
		{
			if (lvwGameList.SelectedIndices.Count == 0)
				return;

			try
			{
				Process process = new Process();
				process.StartInfo.WorkingDirectory = Settings.Folders.MAME.Root;
				process.StartInfo.FileName = Settings.Files.MAME.MAMEExe;
				process.StartInfo.Arguments = lvwGameList.SelectedItems[0].SubItems[1].Text;
				process.StartInfo.CreateNoWindow = true;
				process.EnableRaisingEvents = true;
				process.SynchronizingObject = this;
				process.Exited += new System.EventHandler(MAMEExited);

				process.Start();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("LaunchGame", "frmPreview", ex.Message, ex.StackTrace);
			}
		}

		void MAMEExited(object sender, EventArgs e)
		{
			Activate();
		}

		private void SaveTextFile()
		{
			try
			{
				string fileName = null;

				if (FileIO.TrySaveText(this, out fileName))
				{
					StringBuilder listViewContent = new StringBuilder();

					foreach (ListViewItem item in lvwGameList.Items)
					{
						listViewContent.Append(item.Text);

						foreach (ListViewItem.ListViewSubItem subItem in item.SubItems)
							listViewContent.Append(String.Format("{0}{1}", ((char)9), subItem.Text));

						listViewContent.Append(Environment.NewLine);
					}

					TextWriter tw = new StreamWriter(fileName);

					tw.WriteLine(listViewContent.ToString());

					tw.Close();
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("SaveTextFile", "frmPreview", ex.Message, ex.StackTrace);
			}
		}

		private void lvwGameList_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			// Determine if clicked column is already the column that is being sorted.
			if (e.Column == lvwColumnSorter.SortColumn)
			{
				// Reverse the current sort direction for this column.
				if (lvwColumnSorter.Order == SortOrder.Ascending)
				{
					lvwColumnSorter.Order = SortOrder.Descending;
				}
				else
				{
					lvwColumnSorter.Order = SortOrder.Ascending;
				}
			}
			else
			{
				// Set the column number that is to be sorted; default to ascending.
				lvwColumnSorter.SortColumn = e.Column;
				lvwColumnSorter.Order = SortOrder.Ascending;
			}

			// Perform the sort with these new sort options.
			lvwGameList.Sort();
		}

		private void lvwGameList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lvwGameList.SelectedIndices.Count == 0)
				return;

			Settings.Preview.LastRomSelected = lvwGameList.Items[lvwGameList.SelectedIndices[0]].SubItems[1].Text;
		}

		private void lvwGameList_DoubleClick(object sender, EventArgs e)
		{
			ShowCPWizard();
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveTextFile();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void showToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowCPWizard();
		}

		private void launchGameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LaunchGame();
		}

		private void tsbSave_Click(object sender, EventArgs e)
		{
			SaveTextFile();
		}

		private void tsbViewCPWizard_Click(object sender, EventArgs e)
		{
			ShowCPWizard();
		}

		private void tsbLaunchGame_Click(object sender, EventArgs e)
		{
			LaunchGame();
		}

	}
}