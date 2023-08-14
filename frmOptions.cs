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
	}
}