namespace CPWizard
{
	partial class frmAbout
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
			this.btnOk = new System.Windows.Forms.Button();
			this.grpAbout = new System.Windows.Forms.GroupBox();
			this.picPayPal = new System.Windows.Forms.PictureBox();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.picIcon = new System.Windows.Forms.PictureBox();
			this.grpAbout.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picPayPal)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOk.Location = new System.Drawing.Point(119, 141);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(96, 32);
			this.btnOk.TabIndex = 4;
			this.btnOk.Text = "Ok";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// grpAbout
			// 
			this.grpAbout.Controls.Add(this.picPayPal);
			this.grpAbout.Controls.Add(this.listBox1);
			this.grpAbout.Controls.Add(this.picIcon);
			this.grpAbout.Location = new System.Drawing.Point(12, 10);
			this.grpAbout.Name = "grpAbout";
			this.grpAbout.Size = new System.Drawing.Size(311, 116);
			this.grpAbout.TabIndex = 5;
			this.grpAbout.TabStop = false;
			this.grpAbout.Text = "About";
			// 
			// picPayPal
			// 
			this.picPayPal.Cursor = System.Windows.Forms.Cursors.Hand;
			this.picPayPal.Image = ((System.Drawing.Image)(resources.GetObject("picPayPal.Image")));
			this.picPayPal.Location = new System.Drawing.Point(165, 71);
			this.picPayPal.Name = "picPayPal";
			this.picPayPal.Size = new System.Drawing.Size(62, 31);
			this.picPayPal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.picPayPal.TabIndex = 6;
			this.picPayPal.TabStop = false;
			this.picPayPal.Click += new System.EventHandler(this.picPayPal_Click);
			// 
			// listBox1
			// 
			this.listBox1.BackColor = System.Drawing.SystemColors.Control;
			this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listBox1.Items.AddRange(new object[] {
            "CPWizard [VERSION]",
            "",
            "© Copyright 2007 Written By Ben Baker"});
			this.listBox1.Location = new System.Drawing.Point(107, 21);
			this.listBox1.Name = "listBox1";
			this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.listBox1.Size = new System.Drawing.Size(198, 39);
			this.listBox1.TabIndex = 2;
			// 
			// picIcon
			// 
			this.picIcon.Image = ((System.Drawing.Image)(resources.GetObject("picIcon.Image")));
			this.picIcon.Location = new System.Drawing.Point(13, 21);
			this.picIcon.Name = "picIcon";
			this.picIcon.Size = new System.Drawing.Size(82, 81);
			this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.picIcon.TabIndex = 0;
			this.picIcon.TabStop = false;
			// 
			// frmAbout
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(334, 185);
			this.Controls.Add(this.grpAbout);
			this.Controls.Add(this.btnOk);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "frmAbout";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About";
			this.Load += new System.EventHandler(this.frmAbout_Load);
			this.grpAbout.ResumeLayout(false);
			this.grpAbout.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picPayPal)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		internal System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.GroupBox grpAbout;
		private System.Windows.Forms.PictureBox picIcon;
		internal System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.PictureBox picPayPal;
	}
}