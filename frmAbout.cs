using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace CPWizard
{
	public partial class frmAbout : Form
	{
		public frmAbout()
		{
			InitializeComponent();
		}

		private void frmAbout_Load(object sender, EventArgs e)
		{
			listBox1.Items[0] = listBox1.Items[0].ToString().Replace("[VERSION]", Globals.Version);
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void picPayPal_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_xclick&business=freeaccess%40iinet%2enet%2eau&item_name=CPWizard&no_shipping=0&no_note=1&tax=0&currency_code=USD&lc=AU&bn=PP%2dDonationsBF&charset=UTF%2d8");
		}
	}
}