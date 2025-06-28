namespace CPWizard
{
	partial class frmExport
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
			this.grpExportType = new System.Windows.Forms.GroupBox();
			this.lblOutputType = new System.Windows.Forms.Label();
			this.cboExportType = new System.Windows.Forms.ComboBox();
			this.rdoGameInfo = new System.Windows.Forms.RadioButton();
			this.rdoLayout = new System.Windows.Forms.RadioButton();
			this.grpImageProperties = new System.Windows.Forms.GroupBox();
			this.chkSkipClones = new System.Windows.Forms.CheckBox();
			this.chkVerticalOrientation = new System.Windows.Forms.CheckBox();
			this.chkIncludeVerticalBezel = new System.Windows.Forms.CheckBox();
			this.chkSkipExisting = new System.Windows.Forms.CheckBox();
			this.chkDrawBackground = new System.Windows.Forms.CheckBox();
			this.lblResolution = new System.Windows.Forms.Label();
			this.cboResolution = new System.Windows.Forms.ComboBox();
			this.grpOutputFolder = new System.Windows.Forms.GroupBox();
			this.butOutputFolder = new System.Windows.Forms.Button();
			this.txtOutputFolder = new System.Windows.Forms.TextBox();
			this.butGo = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.grpExportType.SuspendLayout();
			this.grpImageProperties.SuspendLayout();
			this.grpOutputFolder.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpExportType
			// 
			this.grpExportType.Controls.Add(this.lblOutputType);
			this.grpExportType.Controls.Add(this.cboExportType);
			this.grpExportType.Controls.Add(this.rdoGameInfo);
			this.grpExportType.Controls.Add(this.rdoLayout);
			this.grpExportType.Location = new System.Drawing.Point(12, 12);
			this.grpExportType.Name = "grpExportType";
			this.grpExportType.Size = new System.Drawing.Size(178, 153);
			this.grpExportType.TabIndex = 0;
			this.grpExportType.TabStop = false;
			this.grpExportType.Text = "Export Type";
			// 
			// lblOutputType
			// 
			this.lblOutputType.AutoSize = true;
			this.lblOutputType.Location = new System.Drawing.Point(15, 18);
			this.lblOutputType.Name = "lblOutputType";
			this.lblOutputType.Size = new System.Drawing.Size(69, 13);
			this.lblOutputType.TabIndex = 0;
			this.lblOutputType.Text = "Output Type:";
			// 
			// cboExportType
			// 
			this.cboExportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboExportType.FormattingEnabled = true;
			this.cboExportType.Location = new System.Drawing.Point(18, 35);
			this.cboExportType.Name = "cboExportType";
			this.cboExportType.Size = new System.Drawing.Size(144, 21);
			this.cboExportType.TabIndex = 1;
			this.cboExportType.SelectedIndexChanged += new System.EventHandler(this.cboExportType_SelectedIndexChanged);
			// 
			// rdoGameInfo
			// 
			this.rdoGameInfo.AutoSize = true;
			this.rdoGameInfo.Location = new System.Drawing.Point(18, 93);
			this.rdoGameInfo.Name = "rdoGameInfo";
			this.rdoGameInfo.Size = new System.Drawing.Size(74, 17);
			this.rdoGameInfo.TabIndex = 3;
			this.rdoGameInfo.Text = "Game Info";
			this.rdoGameInfo.UseVisualStyleBackColor = true;
			// 
			// rdoLayout
			// 
			this.rdoLayout.AutoSize = true;
			this.rdoLayout.Checked = true;
			this.rdoLayout.Location = new System.Drawing.Point(18, 75);
			this.rdoLayout.Name = "rdoLayout";
			this.rdoLayout.Size = new System.Drawing.Size(57, 17);
			this.rdoLayout.TabIndex = 2;
			this.rdoLayout.TabStop = true;
			this.rdoLayout.Text = "Layout";
			this.rdoLayout.UseVisualStyleBackColor = true;
			// 
			// grpImageProperties
			// 
			this.grpImageProperties.Controls.Add(this.chkSkipClones);
			this.grpImageProperties.Controls.Add(this.chkVerticalOrientation);
			this.grpImageProperties.Controls.Add(this.chkIncludeVerticalBezel);
			this.grpImageProperties.Controls.Add(this.chkSkipExisting);
			this.grpImageProperties.Controls.Add(this.chkDrawBackground);
			this.grpImageProperties.Controls.Add(this.lblResolution);
			this.grpImageProperties.Controls.Add(this.cboResolution);
			this.grpImageProperties.Location = new System.Drawing.Point(197, 12);
			this.grpImageProperties.Name = "grpImageProperties";
			this.grpImageProperties.Size = new System.Drawing.Size(178, 153);
			this.grpImageProperties.TabIndex = 1;
			this.grpImageProperties.TabStop = false;
			this.grpImageProperties.Text = "Image Properties";
			// 
			// chkSkipClones
			// 
			this.chkSkipClones.AutoSize = true;
			this.chkSkipClones.Location = new System.Drawing.Point(16, 95);
			this.chkSkipClones.Name = "chkSkipClones";
			this.chkSkipClones.Size = new System.Drawing.Size(82, 17);
			this.chkSkipClones.TabIndex = 4;
			this.chkSkipClones.Text = "Skip Clones";
			this.chkSkipClones.UseVisualStyleBackColor = true;
			// 
			// chkVerticalOrientation
			// 
			this.chkVerticalOrientation.AutoSize = true;
			this.chkVerticalOrientation.Location = new System.Drawing.Point(16, 129);
			this.chkVerticalOrientation.Name = "chkVerticalOrientation";
			this.chkVerticalOrientation.Size = new System.Drawing.Size(115, 17);
			this.chkVerticalOrientation.TabIndex = 6;
			this.chkVerticalOrientation.Text = "Vertical Orientation";
			this.chkVerticalOrientation.UseVisualStyleBackColor = true;
			this.chkVerticalOrientation.CheckedChanged += new System.EventHandler(this.chkVerticalOrientation_CheckedChanged);
			// 
			// chkIncludeVerticalBezel
			// 
			this.chkIncludeVerticalBezel.AutoSize = true;
			this.chkIncludeVerticalBezel.Checked = true;
			this.chkIncludeVerticalBezel.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkIncludeVerticalBezel.Location = new System.Drawing.Point(16, 112);
			this.chkIncludeVerticalBezel.Name = "chkIncludeVerticalBezel";
			this.chkIncludeVerticalBezel.Size = new System.Drawing.Size(128, 17);
			this.chkIncludeVerticalBezel.TabIndex = 5;
			this.chkIncludeVerticalBezel.Text = "Include Vertical Bezel";
			this.chkIncludeVerticalBezel.UseVisualStyleBackColor = true;
			this.chkIncludeVerticalBezel.CheckedChanged += new System.EventHandler(this.chkIncludeBezel_CheckedChanged);
			// 
			// chkSkipExisting
			// 
			this.chkSkipExisting.AutoSize = true;
			this.chkSkipExisting.Location = new System.Drawing.Point(16, 78);
			this.chkSkipExisting.Name = "chkSkipExisting";
			this.chkSkipExisting.Size = new System.Drawing.Size(86, 17);
			this.chkSkipExisting.TabIndex = 3;
			this.chkSkipExisting.Text = "Skip Existing";
			this.chkSkipExisting.UseVisualStyleBackColor = true;
			// 
			// chkDrawBackground
			// 
			this.chkDrawBackground.AutoSize = true;
			this.chkDrawBackground.Location = new System.Drawing.Point(16, 61);
			this.chkDrawBackground.Name = "chkDrawBackground";
			this.chkDrawBackground.Size = new System.Drawing.Size(112, 17);
			this.chkDrawBackground.TabIndex = 2;
			this.chkDrawBackground.Text = "Draw Background";
			this.chkDrawBackground.UseVisualStyleBackColor = true;
			// 
			// lblResolution
			// 
			this.lblResolution.AutoSize = true;
			this.lblResolution.Location = new System.Drawing.Point(13, 18);
			this.lblResolution.Name = "lblResolution";
			this.lblResolution.Size = new System.Drawing.Size(60, 13);
			this.lblResolution.TabIndex = 0;
			this.lblResolution.Text = "Resolution:";
			// 
			// cboResolution
			// 
			this.cboResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboResolution.FormattingEnabled = true;
			this.cboResolution.Location = new System.Drawing.Point(16, 35);
			this.cboResolution.Name = "cboResolution";
			this.cboResolution.Size = new System.Drawing.Size(147, 21);
			this.cboResolution.TabIndex = 1;
			// 
			// grpOutputFolder
			// 
			this.grpOutputFolder.Controls.Add(this.butOutputFolder);
			this.grpOutputFolder.Controls.Add(this.txtOutputFolder);
			this.grpOutputFolder.Location = new System.Drawing.Point(12, 171);
			this.grpOutputFolder.Name = "grpOutputFolder";
			this.grpOutputFolder.Size = new System.Drawing.Size(363, 62);
			this.grpOutputFolder.TabIndex = 2;
			this.grpOutputFolder.TabStop = false;
			this.grpOutputFolder.Text = "Output Folder";
			// 
			// butOutputFolder
			// 
			this.butOutputFolder.Location = new System.Drawing.Point(314, 25);
			this.butOutputFolder.Name = "butOutputFolder";
			this.butOutputFolder.Size = new System.Drawing.Size(34, 20);
			this.butOutputFolder.TabIndex = 1;
			this.butOutputFolder.Text = "...";
			this.butOutputFolder.UseVisualStyleBackColor = true;
			this.butOutputFolder.Click += new System.EventHandler(this.butOutputFolder_Click);
			// 
			// txtOutputFolder
			// 
			this.txtOutputFolder.Location = new System.Drawing.Point(14, 25);
			this.txtOutputFolder.Name = "txtOutputFolder";
			this.txtOutputFolder.Size = new System.Drawing.Size(294, 20);
			this.txtOutputFolder.TabIndex = 0;
			// 
			// butGo
			// 
			this.butGo.Location = new System.Drawing.Point(12, 245);
			this.butGo.Name = "butGo";
			this.butGo.Size = new System.Drawing.Size(94, 33);
			this.butGo.TabIndex = 3;
			this.butGo.Text = "GO!";
			this.butGo.UseVisualStyleBackColor = true;
			this.butGo.Click += new System.EventHandler(this.butGo_Click);
			// 
			// butCancel
			// 
			this.butCancel.Location = new System.Drawing.Point(283, 245);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(94, 33);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "Cancel";
			this.butCancel.UseVisualStyleBackColor = true;
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// frmExport
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(389, 296);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butGo);
			this.Controls.Add(this.grpOutputFolder);
			this.Controls.Add(this.grpImageProperties);
			this.Controls.Add(this.grpExportType);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "frmExport";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Export";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmExportBatch_FormClosing);
			this.Load += new System.EventHandler(this.frmExportBatch_Load);
			this.grpExportType.ResumeLayout(false);
			this.grpExportType.PerformLayout();
			this.grpImageProperties.ResumeLayout(false);
			this.grpImageProperties.PerformLayout();
			this.grpOutputFolder.ResumeLayout(false);
			this.grpOutputFolder.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox grpExportType;
		private System.Windows.Forms.RadioButton rdoLayout;
		private System.Windows.Forms.GroupBox grpImageProperties;
		private System.Windows.Forms.GroupBox grpOutputFolder;
		private System.Windows.Forms.Button butGo;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.RadioButton rdoGameInfo;
		private System.Windows.Forms.CheckBox chkDrawBackground;
		private System.Windows.Forms.Label lblResolution;
		private System.Windows.Forms.ComboBox cboResolution;
		private System.Windows.Forms.Button butOutputFolder;
		private System.Windows.Forms.TextBox txtOutputFolder;
		private System.Windows.Forms.CheckBox chkSkipExisting;
		private System.Windows.Forms.ComboBox cboExportType;
		private System.Windows.Forms.CheckBox chkIncludeVerticalBezel;
		private System.Windows.Forms.Label lblOutputType;
		private System.Windows.Forms.CheckBox chkVerticalOrientation;
		private System.Windows.Forms.CheckBox chkSkipClones;
	}
}