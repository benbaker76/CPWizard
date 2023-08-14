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
	}
}
