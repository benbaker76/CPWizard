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
	}
}
