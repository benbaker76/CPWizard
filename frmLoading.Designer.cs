namespace CPWizard
{
	partial class frmLoading
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLoading));
			this.picOutput = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.picOutput)).BeginInit();
			this.SuspendLayout();
			// 
			// picOutput
			// 
			this.picOutput.BackColor = System.Drawing.Color.Black;
			this.picOutput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.picOutput.Location = new System.Drawing.Point(0, 0);
			this.picOutput.Name = "picOutput";
			this.picOutput.Size = new System.Drawing.Size(256, 256);
			this.picOutput.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picOutput.TabIndex = 1;
			this.picOutput.TabStop = false;
			// 
			// frmLoading
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(256, 256);
			this.ControlBox = false;
			this.Controls.Add(this.picOutput);
			this.Enabled = false;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmLoading";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "frmLoading";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLoading_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.picOutput)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox picOutput;

	}
}