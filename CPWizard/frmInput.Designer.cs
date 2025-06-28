namespace CPWizard
{
	partial class frmInput
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
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblPressAKey = new System.Windows.Forms.Label();
			this.butClear = new System.Windows.Forms.Button();
			this.lblOrButton = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(23, 105);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(132, 21);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// lblPressAKey
			// 
			this.lblPressAKey.AutoSize = true;
			this.lblPressAKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblPressAKey.Location = new System.Drawing.Point(33, 20);
			this.lblPressAKey.Name = "lblPressAKey";
			this.lblPressAKey.Size = new System.Drawing.Size(117, 24);
			this.lblPressAKey.TabIndex = 0;
			this.lblPressAKey.Text = "Press a key";
			// 
			// butClear
			// 
			this.butClear.Location = new System.Drawing.Point(23, 78);
			this.butClear.Name = "butClear";
			this.butClear.Size = new System.Drawing.Size(132, 21);
			this.butClear.TabIndex = 2;
			this.butClear.Text = "Clear";
			this.butClear.UseVisualStyleBackColor = true;
			this.butClear.Click += new System.EventHandler(this.butClear_Click);
			// 
			// lblOrButton
			// 
			this.lblOrButton.AutoSize = true;
			this.lblOrButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblOrButton.Location = new System.Drawing.Point(38, 43);
			this.lblOrButton.Name = "lblOrButton";
			this.lblOrButton.Size = new System.Drawing.Size(111, 24);
			this.lblOrButton.TabIndex = 3;
			this.lblOrButton.Text = "or button...";
			// 
			// frmInput
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(180, 137);
			this.Controls.Add(this.lblOrButton);
			this.Controls.Add(this.butClear);
			this.Controls.Add(this.lblPressAKey);
			this.Controls.Add(this.btnCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.KeyPreview = true;
			this.Name = "frmInput";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Input";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lblPressAKey;
		private System.Windows.Forms.Button butClear;
		private System.Windows.Forms.Label lblOrButton;
	}
}