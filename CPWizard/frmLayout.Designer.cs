namespace CPWizard
{
	partial class frmLayout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLayout));
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
            this.picOutput.TabIndex = 0;
            this.picOutput.TabStop = false;
            // 
            // frmLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(256, 256);
            this.Controls.Add(this.picOutput);
            this.Enabled = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLayout";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Layout";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CPForm_FormClosing);
            this.Shown += new System.EventHandler(this.frmLayout_Shown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmLayout_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmLayout_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.picOutput)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox picOutput;
	}
}