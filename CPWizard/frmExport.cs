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
	public enum ResolutionType
	{
		Res_LayoutSize = 0,
		Res_320x240 = 1,
		Res_640x480 = 2,
		Res_1024x768 = 3,
		Res_1280x960 = 4,
		Res_1280x1024 = 5
	}

	public partial class frmExport : Form
	{
		private string[] ResolutionNames =
        {
            "Layout Size",
            "320 x 240",
            "640 x 480",
            "1024 x 768",
            "1280 x 960",
            "1280 x 1024"
        };

		private Bitmap m_bitmap = null;

		public frmExport()
		{
			InitializeComponent();
		}

		private void GetData()
		{
			cboExportType.Items.Clear();

			cboExportType.Items.Add("Image");
			cboExportType.Items.Add("Bezel");

			cboExportType.Text = Settings.Export.ExportType;
			cboResolution.SelectedIndex = (int)Settings.Export.ResolutionType;
			chkDrawBackground.Checked = Settings.Export.DrawBackground;
			chkSkipClones.Checked = Settings.Export.SkipClones;
			chkIncludeVerticalBezel.Checked = Settings.Export.IncludeVerticalBezel;
			chkVerticalOrientation.Checked = Settings.Export.VerticalOrientation;
			txtOutputFolder.Text = Settings.Export.Folders.Export;
		}

		private void SetData()
		{
			Settings.Export.ExportType = cboExportType.Text;
			Settings.Export.ResolutionType = (ResolutionType)cboResolution.SelectedIndex;
			Settings.Export.DrawBackground = chkDrawBackground.Checked;
			Settings.Export.SkipClones = chkSkipClones.Checked;
			Settings.Export.IncludeVerticalBezel = chkIncludeVerticalBezel.Checked;
			Settings.Export.VerticalOrientation = chkVerticalOrientation.Checked;
			Settings.Export.Folders.Export = txtOutputFolder.Text;
		}

		private void frmExportBatch_Load(object sender, EventArgs e)
		{
			GetData();
		}

		private void frmExportBatch_FormClosing(object sender, FormClosingEventArgs e)
		{
			SetData();
		}

		private void butOutputFolder_Click(object sender, EventArgs e)
		{
			string Folder = null;

			if (FileIO.TryOpenFolder(this, String.Empty, out Folder))
				txtOutputFolder.Text = Folder;
		}

		private void butGo_Click(object sender, EventArgs e)
		{
			try
			{
				if (!Directory.Exists(txtOutputFolder.Text))
					Directory.CreateDirectory(txtOutputFolder.Text);

				if (!Directory.Exists(txtOutputFolder.Text))
					return;

				if (Globals.MAMEXml == null)
					return;

				Globals.EmulatorMode = EmulatorMode.MAME;
				Globals.ProgramManager.ReadAllGameData();

				if (cboExportType.Text == "Image")
				{
					if (rdoLayout.Checked)
						Globals.DisplayMode = DisplayMode.Layout;
					else
						Globals.DisplayMode = DisplayMode.GameInfo;
				}
				else
				{
					Globals.DisplayMode = DisplayMode.Layout;
				}

				Globals.ProgramManager.ShowScreen(false);

				if (m_bitmap != null)
				{
					m_bitmap.Dispose();
					m_bitmap = null;
				}

				switch ((ResolutionType)cboResolution.SelectedIndex)
				{
					case ResolutionType.Res_LayoutSize:
						m_bitmap = new Bitmap(Globals.Layout.Width, Globals.Layout.Height);
						break;
					case ResolutionType.Res_320x240:
						m_bitmap = new Bitmap(320, 240);
						break;
					case ResolutionType.Res_640x480:
						m_bitmap = new Bitmap(640, 480);
						break;
					case ResolutionType.Res_1024x768:
						m_bitmap = new Bitmap(1024, 768);
						break;
					case ResolutionType.Res_1280x960:
						m_bitmap = new Bitmap(1280, 960);
						break;
					case ResolutionType.Res_1280x1024:
						m_bitmap = new Bitmap(1280, 1024);
						break;
					default:
						m_bitmap = new Bitmap(Globals.Layout.Width, Globals.Layout.Height);
						break;
				}

				using (frmInfo loadingForm = new frmInfo(this, "Processing Please Wait...", true, true))
				{
					loadingForm.Show();
					Application.DoEvents();
					this.Cursor = Cursors.WaitCursor;
					butGo.Enabled = false;
					butCancel.Enabled = false;

					int Count = 0;

					foreach (MAMEMachineNode gameNode in Globals.MAMEXml.GameList)
					{
						if (chkSkipClones.Checked)
						{
							if (!String.IsNullOrEmpty(gameNode.CloneOf) || !String.IsNullOrEmpty(gameNode.ROMOf))
							{
								loadingForm.SetProgressBar((int)(((float)Count++ / (float)Globals.MAMEXml.GameList.Count) * 100f));

								continue;
							}
						}

						string pngFileName = Path.Combine(txtOutputFolder.Text, gameNode.Name + ".png");
						string zipFileName = Path.Combine(txtOutputFolder.Text, gameNode.Name + ".zip");

						if (chkSkipExisting.Checked)
						{
							if (cboExportType.Text == "Image")
							{
								if (File.Exists(pngFileName))
								{
									loadingForm.SetProgressBar((int)(((float)Count++ / (float)Globals.MAMEXml.GameList.Count) * 100f));

									continue;
								}
							}
							else
							{
								if (File.Exists(zipFileName))
								{
									loadingForm.SetProgressBar((int)(((float)Count++ / (float)Globals.MAMEXml.GameList.Count) * 100f));

									continue;
								}
							}
						}

						Settings.MAME.GameName = gameNode.Name;

						if (rdoLayout.Checked)
							Globals.MAMEManager.GetGameDetails(true, true, false);
						else
							Globals.MAMEManager.GetGameDetails(false, false, false);

						if (cboExportType.Text == "Image")
							ExportImage(pngFileName);
						else
							ExportBezel(gameNode);

						if (rdoLayout.Checked)
							Globals.LayoutManager.ResetControls(Globals.Layout);

						loadingForm.SetProgressBar((int)(((float)Count++ / (float)Globals.MAMEXml.GameList.Count) * 100f));

						Application.DoEvents();

						if (loadingForm.Exit)
							break;
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("butGo_Click", "frmExportBatch", ex.Message, ex.StackTrace);
			}

			butGo.Enabled = true;
			butCancel.Enabled = true;

			this.Cursor = Cursors.Default;

			Globals.DisplayMode = DisplayMode.LayoutEditor;
			Globals.MainForm.UpdateDisplay();

			this.Close();
		}

		private void butCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void cboExportType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cboExportType.Text == "Image")
			{
				cboResolution.Items.Clear();

				cboResolution.Items.AddRange(ResolutionNames);

				cboResolution.SelectedIndex = (int)ResolutionType.Res_LayoutSize;

				rdoLayout.Checked = true;

				chkDrawBackground.Checked = true;

				chkIncludeVerticalBezel.Enabled = false;
				chkVerticalOrientation.Enabled = false;
			}
			else
			{
				cboResolution.Items.Clear();

				cboResolution.Items.AddRange(ResolutionNames);

				cboResolution.SelectedIndex = (int)ResolutionType.Res_640x480;

				chkDrawBackground.Checked = false;

				chkIncludeVerticalBezel.Enabled = true;
				chkVerticalOrientation.Enabled = true;
			}
		}

		private void chkIncludeBezel_CheckedChanged(object sender, EventArgs e)
		{
			if (chkIncludeVerticalBezel.Checked)
				chkVerticalOrientation.Checked = false;
		}

		private void chkVerticalOrientation_CheckedChanged(object sender, EventArgs e)
		{
			if (chkVerticalOrientation.Checked)
				chkIncludeVerticalBezel.Checked = false;
		}

		private void ExportImage(string fileName)
		{
			try
			{
				if (cboResolution.Text == "Layout Size" && Globals.Layout != null)
				{
					if (m_bitmap.Width != Globals.Layout.Width || m_bitmap.Height != Globals.Layout.Height)
					{
						if (m_bitmap != null)
						{
							m_bitmap.Dispose();
							m_bitmap = null;
						}

						m_bitmap = new Bitmap(Globals.Layout.Width, Globals.Layout.Height);
					}
				}

				Globals.DisplayManager.Paint(m_bitmap, BakType.Default, chkDrawBackground.Checked, false, Settings.Display.Rotation, Settings.Display.FlipX, Settings.Display.FlipY);

				Globals.DisplayManager.DrawWatermark(m_bitmap);

				m_bitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ExportImage", "frmExportBatch", ex.Message, ex.StackTrace);
			}
		}

		private void ExportBezel(MAMEMachineNode gameNode)
		{
			try
			{
				string Category = null;
				string Type = null;
				float WeightedAverage = 0f;
				bool Vertical = false;
				bool IncludeVertical = false;
				Size ScreenSize;

				double fourThreeRatio = 4.0 / 3.0;
				int sizeOffset = 0;
				int sizeRemaining = 0;

				Rectangle ScreenRect = Rectangle.Empty;
				Rectangle BezelRect = Rectangle.Empty;

				if (rdoLayout.Checked)
				{
					Globals.DisplayManager.Paint(m_bitmap, BakType.Default, chkDrawBackground.Checked, false, Settings.Display.Rotation, Settings.Display.FlipX, Settings.Display.FlipY);

					Globals.DisplayManager.DrawWatermark(m_bitmap);
				}

				MAMEBezel.BezelType BezelType = MAMEBezel.BezelType.Layout;

				if (rdoGameInfo.Checked)
					BezelType = MAMEBezel.BezelType.GameInfo;

				GetGameDetails(gameNode, out Category, out Type, out WeightedAverage, out Vertical);

				if (Vertical && chkIncludeVerticalBezel.Checked)
					IncludeVertical = true;

				if (chkVerticalOrientation.Checked)
				{
					if ((ResolutionType)cboResolution.SelectedIndex == ResolutionType.Res_LayoutSize)
						ScreenSize = new Size(m_bitmap.Width, m_bitmap.Height);
					else
						ScreenSize = new Size(m_bitmap.Height, m_bitmap.Width);

					ScreenRect = new Rectangle(0, 0, ScreenSize.Width, ScreenSize.Height);

					if (Vertical)
					{
						sizeRemaining = ScreenSize.Width - (int)Math.Round((double)ScreenSize.Height / fourThreeRatio);
						sizeOffset = sizeRemaining / 2;

						ScreenRect = new Rectangle(sizeOffset, 0, ScreenSize.Width - sizeRemaining, ScreenSize.Height);
					}
					else
					{
						sizeRemaining = ScreenSize.Height - (int)Math.Round((double)ScreenSize.Width / fourThreeRatio);
						sizeOffset = sizeRemaining / 2;

						ScreenRect = new Rectangle(0, sizeOffset, ScreenSize.Width, ScreenSize.Height - sizeRemaining);
					}
				}
				else
				{
					ScreenSize = new Size(m_bitmap.Width, m_bitmap.Height);
					ScreenRect = new Rectangle(0, 0, ScreenSize.Width, ScreenSize.Height);

					if (Vertical)
					{
						sizeRemaining = ScreenSize.Width - (int)Math.Round((double)ScreenSize.Height / fourThreeRatio);
						sizeOffset = sizeRemaining / 2;

						ScreenRect = new Rectangle(sizeOffset, 0, ScreenSize.Width - sizeRemaining, ScreenSize.Height);
					}
					else
					{
						sizeRemaining = ScreenSize.Height - (int)Math.Round((double)ScreenSize.Width / fourThreeRatio);
						sizeOffset = sizeRemaining / 2;

						ScreenRect = new Rectangle(0, sizeOffset, ScreenSize.Width, ScreenSize.Height - sizeRemaining);
					}
				}

				BezelRect = new Rectangle(0, 0, ScreenSize.Width, ScreenSize.Height);

				Globals.Bezel.GenerateBezelV2(m_bitmap, txtOutputFolder.Text, ScreenSize, ScreenRect, BezelRect, gameNode.Name, gameNode.Description, gameNode.Year, gameNode.Manufacturer, Category, Type, WeightedAverage, IncludeVertical, BezelType);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ExportBezel", "frmExportBatch", ex.Message, ex.StackTrace);
			}
		}

		private void GetGameDetails(MAMEMachineNode gameNode, out string category, out string type, out float weightedAverage, out bool vertical)
		{
			category = null;
			type = null;
			weightedAverage = 0f;
			vertical = false;

			try
			{
				if (gameNode.CatVer != null)
					category = gameNode.CatVer.Category;

				if (gameNode.NPlayers != null)
					type = gameNode.NPlayers.Type;

				if (gameNode.HallOfFame != null)
					weightedAverage = gameNode.HallOfFame.WeightedAverage;

				if (gameNode.DisplayList != null)
					vertical = gameNode.IsVertical;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetGameDetails", "frmExportBatch", ex.Message, ex.StackTrace);
			}
		}
	}
}