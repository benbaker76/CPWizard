using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CPWizard
{
	public partial class frmInfo : Form
	{
		private Form m_parent = null;

		public bool Exit = false;

		public frmInfo(Form parent, string message, bool ShowCancel, bool ShowProgressBar)
		{
			InitializeComponent();

			m_parent = parent;

			lblOutput.Text = message;

			if (ShowCancel)
				butCancel.Enabled = true;
			else
				butCancel.Enabled = false;

			if (ShowProgressBar)
				progressBar1.Visible = true;
			else
				progressBar1.Visible = false;

			Exit = false;
		}

		private void LoadingForm_Load(object sender, EventArgs e)
		{
			this.StartPosition = FormStartPosition.Manual;

			int x = m_parent.Location.X + (m_parent.Width / 2) - (this.Width / 2);
			int y = m_parent.Location.Y + (m_parent.Height / 2) - (this.Height / 2);
			this.Location = new Point(x, y);
		}

		private void butCancel_Click(object sender, EventArgs e)
		{
			Exit = true;
		}

		public void SetProgressBar(int Percentage)
		{
			progressBar1.Value = Percentage;
		}
	}
}