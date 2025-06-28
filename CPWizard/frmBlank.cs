using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CPWizard
{
	public partial class frmBlank : Form
	{
		public frmBlank()
		{
			InitializeComponent();
		}

		protected override bool ShowWithoutActivation
		{
			get { return true; }
		}
	}
}