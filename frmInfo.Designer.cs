namespace CPWizard
{
	partial class frmInfo
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
			this.lblOutput = new System.Windows.Forms.Label();
			this.butCancel = new System.Windows.Forms.Button();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// lblOutput
			// 
			this.lblOutput.Cursor = System.Windows.Forms.Cursors.WaitCursor;
			this.lblOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblOutput.Location = new System.Drawing.Point(12, 9);
			this.lblOutput.Name = "lblOutput";
			this.lblOutput.Size = new System.Drawing.Size(268, 58);
			this.lblOutput.TabIndex = 0;
			this.lblOutput.Text = "Output";
			this.lblOutput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// butCancel
			// 
			this.butCancel.Cursor = System.Windows.Forms.Cursors.Default;
			this.butCancel.Location = new System.Drawing.Point(15, 104);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(262, 32);
			this.butCancel.TabIndex = 1;
			this.butCancel.Text = "Cancel";
			this.butCancel.UseVisualStyleBackColor = true;
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(15, 79);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(261, 16);
			this.progressBar1.TabIndex = 2;
			// 
			// frmInfo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 147);
			this.ControlBox = false;
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.lblOutput);
			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "frmInfo";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Information";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.LoadingForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lblOutput;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.ProgressBar progressBar1;
	}
}