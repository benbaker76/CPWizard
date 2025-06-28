namespace CPWizard
{
	partial class frmObject
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
			this.components = new System.ComponentModel.Container();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabImage = new System.Windows.Forms.TabPage();
			this.grpImageDisplay = new System.Windows.Forms.GroupBox();
			this.lblAlphaFadeValue = new System.Windows.Forms.Label();
			this.nudAlphaFadeValue = new System.Windows.Forms.NumericUpDown();
			this.chkAlphaFade = new System.Windows.Forms.CheckBox();
			this.grpImageLink = new System.Windows.Forms.GroupBox();
			this.cboLabelLink = new System.Windows.Forms.ComboBox();
			this.grpImageTransform = new System.Windows.Forms.GroupBox();
			this.chkImageSizeable = new System.Windows.Forms.CheckBox();
			this.grpColor = new System.Windows.Forms.GroupBox();
			this.trkBrightness = new System.Windows.Forms.TrackBar();
			this.trkSaturation = new System.Windows.Forms.TrackBar();
			this.lblBrightness = new System.Windows.Forms.Label();
			this.lblSaturation = new System.Windows.Forms.Label();
			this.lblHue = new System.Windows.Forms.Label();
			this.trkHue = new System.Windows.Forms.TrackBar();
			this.grpImageName = new System.Windows.Forms.GroupBox();
			this.cboImageName = new System.Windows.Forms.ComboBox();
			this.butImageBrowse = new System.Windows.Forms.Button();
			this.grpImagePreview = new System.Windows.Forms.GroupBox();
			this.picImagePreview = new System.Windows.Forms.PictureBox();
			this.tabLabel = new System.Windows.Forms.TabPage();
			this.grpLabelDisplay = new System.Windows.Forms.GroupBox();
			this.chkLabelSpot = new System.Windows.Forms.CheckBox();
			this.chkLabelArrow = new System.Windows.Forms.CheckBox();
			this.grpLabelGroup = new System.Windows.Forms.GroupBox();
			this.cboLabelGroup = new System.Windows.Forms.ComboBox();
			this.grpInputCodes = new System.Windows.Forms.GroupBox();
			this.butInputCodeDelete = new System.Windows.Forms.Button();
			this.butInputCodeNew = new System.Windows.Forms.Button();
			this.lvwInputCodes = new CPWizard.ListViewEx(this.components);
			this.colShowLabel = new System.Windows.Forms.ColumnHeader();
			this.colCodeType = new System.Windows.Forms.ColumnHeader();
			this.colCodeValue = new System.Windows.Forms.ColumnHeader();
			this.grpLabelTransform = new System.Windows.Forms.GroupBox();
			this.chkLabelSizeable = new System.Windows.Forms.CheckBox();
			this.grpLabelPreview = new System.Windows.Forms.GroupBox();
			this.picLabelPreview = new System.Windows.Forms.PictureBox();
			this.grpLabelTextProperies = new System.Windows.Forms.GroupBox();
			this.rdoOutlineStyle = new System.Windows.Forms.RadioButton();
			this.cboTextAlign = new System.Windows.Forms.ComboBox();
			this.rdoShadowStyle = new System.Windows.Forms.RadioButton();
			this.txtLabelFont = new System.Windows.Forms.TextBox();
			this.butLabelFont = new System.Windows.Forms.Button();
			this.butLabelColor = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabImage.SuspendLayout();
			this.grpImageDisplay.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudAlphaFadeValue)).BeginInit();
			this.grpImageLink.SuspendLayout();
			this.grpImageTransform.SuspendLayout();
			this.grpColor.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trkBrightness)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trkSaturation)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trkHue)).BeginInit();
			this.grpImageName.SuspendLayout();
			this.grpImagePreview.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picImagePreview)).BeginInit();
			this.tabLabel.SuspendLayout();
			this.grpLabelDisplay.SuspendLayout();
			this.grpLabelGroup.SuspendLayout();
			this.grpInputCodes.SuspendLayout();
			this.grpLabelTransform.SuspendLayout();
			this.grpLabelPreview.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picLabelPreview)).BeginInit();
			this.grpLabelTextProperies.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabImage);
			this.tabControl1.Controls.Add(this.tabLabel);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(334, 388);
			this.tabControl1.TabIndex = 0;
			// 
			// tabImage
			// 
			this.tabImage.Controls.Add(this.grpImageDisplay);
			this.tabImage.Controls.Add(this.grpImageLink);
			this.tabImage.Controls.Add(this.grpImageTransform);
			this.tabImage.Controls.Add(this.grpColor);
			this.tabImage.Controls.Add(this.grpImageName);
			this.tabImage.Controls.Add(this.grpImagePreview);
			this.tabImage.Location = new System.Drawing.Point(4, 22);
			this.tabImage.Name = "tabImage";
			this.tabImage.Padding = new System.Windows.Forms.Padding(3);
			this.tabImage.Size = new System.Drawing.Size(326, 362);
			this.tabImage.TabIndex = 0;
			this.tabImage.Text = "Image";
			this.tabImage.UseVisualStyleBackColor = true;
			// 
			// grpImageDisplay
			// 
			this.grpImageDisplay.Controls.Add(this.lblAlphaFadeValue);
			this.grpImageDisplay.Controls.Add(this.nudAlphaFadeValue);
			this.grpImageDisplay.Controls.Add(this.chkAlphaFade);
			this.grpImageDisplay.Location = new System.Drawing.Point(192, 186);
			this.grpImageDisplay.Name = "grpImageDisplay";
			this.grpImageDisplay.Size = new System.Drawing.Size(126, 90);
			this.grpImageDisplay.TabIndex = 18;
			this.grpImageDisplay.TabStop = false;
			this.grpImageDisplay.Text = "Display";
			// 
			// lblAlphaFadeValue
			// 
			this.lblAlphaFadeValue.AutoSize = true;
			this.lblAlphaFadeValue.Location = new System.Drawing.Point(16, 58);
			this.lblAlphaFadeValue.Name = "lblAlphaFadeValue";
			this.lblAlphaFadeValue.Size = new System.Drawing.Size(37, 13);
			this.lblAlphaFadeValue.TabIndex = 22;
			this.lblAlphaFadeValue.Text = "Value:";
			// 
			// nudAlphaFadeValue
			// 
			this.nudAlphaFadeValue.Location = new System.Drawing.Point(55, 55);
			this.nudAlphaFadeValue.Name = "nudAlphaFadeValue";
			this.nudAlphaFadeValue.ReadOnly = true;
			this.nudAlphaFadeValue.Size = new System.Drawing.Size(52, 20);
			this.nudAlphaFadeValue.TabIndex = 21;
			this.nudAlphaFadeValue.ValueChanged += new System.EventHandler(this.nudAlphaFadeValue_ValueChanged);
			// 
			// chkAlphaFade
			// 
			this.chkAlphaFade.AutoSize = true;
			this.chkAlphaFade.Location = new System.Drawing.Point(22, 32);
			this.chkAlphaFade.Name = "chkAlphaFade";
			this.chkAlphaFade.Size = new System.Drawing.Size(80, 17);
			this.chkAlphaFade.TabIndex = 19;
			this.chkAlphaFade.Text = "Alpha Fade";
			this.chkAlphaFade.UseVisualStyleBackColor = true;
			this.chkAlphaFade.CheckedChanged += new System.EventHandler(this.chkAlphaFade_CheckedChanged);
			// 
			// grpImageLink
			// 
			this.grpImageLink.Controls.Add(this.cboLabelLink);
			this.grpImageLink.Location = new System.Drawing.Point(105, 282);
			this.grpImageLink.Name = "grpImageLink";
			this.grpImageLink.Size = new System.Drawing.Size(215, 52);
			this.grpImageLink.TabIndex = 17;
			this.grpImageLink.TabStop = false;
			this.grpImageLink.Text = "Label Link";
			// 
			// cboLabelLink
			// 
			this.cboLabelLink.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboLabelLink.FormattingEnabled = true;
			this.cboLabelLink.Location = new System.Drawing.Point(13, 18);
			this.cboLabelLink.Name = "cboLabelLink";
			this.cboLabelLink.Size = new System.Drawing.Size(189, 21);
			this.cboLabelLink.TabIndex = 0;
			this.cboLabelLink.SelectedIndexChanged += new System.EventHandler(this.cboLabelLink_SelectedIndexChanged);
			// 
			// grpImageTransform
			// 
			this.grpImageTransform.Controls.Add(this.chkImageSizeable);
			this.grpImageTransform.Location = new System.Drawing.Point(5, 282);
			this.grpImageTransform.Name = "grpImageTransform";
			this.grpImageTransform.Size = new System.Drawing.Size(93, 52);
			this.grpImageTransform.TabIndex = 12;
			this.grpImageTransform.TabStop = false;
			this.grpImageTransform.Text = "Transform";
			// 
			// chkImageSizeable
			// 
			this.chkImageSizeable.AutoSize = true;
			this.chkImageSizeable.Location = new System.Drawing.Point(15, 22);
			this.chkImageSizeable.Name = "chkImageSizeable";
			this.chkImageSizeable.Size = new System.Drawing.Size(66, 17);
			this.chkImageSizeable.TabIndex = 1;
			this.chkImageSizeable.Text = "Sizeable";
			this.chkImageSizeable.UseVisualStyleBackColor = true;
			this.chkImageSizeable.CheckedChanged += new System.EventHandler(this.chkImageSizeable_CheckedChanged);
			// 
			// grpColor
			// 
			this.grpColor.Controls.Add(this.trkBrightness);
			this.grpColor.Controls.Add(this.trkSaturation);
			this.grpColor.Controls.Add(this.lblBrightness);
			this.grpColor.Controls.Add(this.lblSaturation);
			this.grpColor.Controls.Add(this.lblHue);
			this.grpColor.Controls.Add(this.trkHue);
			this.grpColor.Location = new System.Drawing.Point(5, 186);
			this.grpColor.Name = "grpColor";
			this.grpColor.Size = new System.Drawing.Size(181, 90);
			this.grpColor.TabIndex = 6;
			this.grpColor.TabStop = false;
			this.grpColor.Text = "Color";
			// 
			// trkBrightness
			// 
			this.trkBrightness.AutoSize = false;
			this.trkBrightness.BackColor = System.Drawing.SystemColors.Window;
			this.trkBrightness.Location = new System.Drawing.Point(69, 57);
			this.trkBrightness.Maximum = 100;
			this.trkBrightness.Name = "trkBrightness";
			this.trkBrightness.Size = new System.Drawing.Size(103, 16);
			this.trkBrightness.SmallChange = 5;
			this.trkBrightness.TabIndex = 5;
			this.trkBrightness.TickFrequency = 5;
			this.trkBrightness.ValueChanged += new System.EventHandler(this.trkBrightness_ValueChanged);
			// 
			// trkSaturation
			// 
			this.trkSaturation.AutoSize = false;
			this.trkSaturation.BackColor = System.Drawing.SystemColors.Window;
			this.trkSaturation.Location = new System.Drawing.Point(69, 39);
			this.trkSaturation.Maximum = 100;
			this.trkSaturation.Name = "trkSaturation";
			this.trkSaturation.Size = new System.Drawing.Size(103, 16);
			this.trkSaturation.SmallChange = 5;
			this.trkSaturation.TabIndex = 4;
			this.trkSaturation.TickFrequency = 5;
			this.trkSaturation.ValueChanged += new System.EventHandler(this.trkSaturation_ValueChanged);
			// 
			// lblBrightness
			// 
			this.lblBrightness.AutoSize = true;
			this.lblBrightness.Location = new System.Drawing.Point(11, 57);
			this.lblBrightness.Name = "lblBrightness";
			this.lblBrightness.Size = new System.Drawing.Size(59, 13);
			this.lblBrightness.TabIndex = 3;
			this.lblBrightness.Text = "Brightness:";
			// 
			// lblSaturation
			// 
			this.lblSaturation.AutoSize = true;
			this.lblSaturation.Location = new System.Drawing.Point(11, 39);
			this.lblSaturation.Name = "lblSaturation";
			this.lblSaturation.Size = new System.Drawing.Size(58, 13);
			this.lblSaturation.TabIndex = 2;
			this.lblSaturation.Text = "Saturation:";
			// 
			// lblHue
			// 
			this.lblHue.AutoSize = true;
			this.lblHue.Location = new System.Drawing.Point(11, 20);
			this.lblHue.Name = "lblHue";
			this.lblHue.Size = new System.Drawing.Size(30, 13);
			this.lblHue.TabIndex = 1;
			this.lblHue.Text = "Hue:";
			// 
			// trkHue
			// 
			this.trkHue.AutoSize = false;
			this.trkHue.BackColor = System.Drawing.SystemColors.Window;
			this.trkHue.Location = new System.Drawing.Point(69, 20);
			this.trkHue.Maximum = 360;
			this.trkHue.Name = "trkHue";
			this.trkHue.Size = new System.Drawing.Size(103, 16);
			this.trkHue.SmallChange = 5;
			this.trkHue.TabIndex = 0;
			this.trkHue.TickFrequency = 15;
			this.trkHue.ValueChanged += new System.EventHandler(this.trkHue_ValueChanged);
			// 
			// grpImageName
			// 
			this.grpImageName.Controls.Add(this.cboImageName);
			this.grpImageName.Controls.Add(this.butImageBrowse);
			this.grpImageName.Location = new System.Drawing.Point(5, 130);
			this.grpImageName.Name = "grpImageName";
			this.grpImageName.Size = new System.Drawing.Size(315, 50);
			this.grpImageName.TabIndex = 5;
			this.grpImageName.TabStop = false;
			this.grpImageName.Text = "Name";
			// 
			// cboImageName
			// 
			this.cboImageName.FormattingEnabled = true;
			this.cboImageName.Location = new System.Drawing.Point(15, 18);
			this.cboImageName.Name = "cboImageName";
			this.cboImageName.Size = new System.Drawing.Size(254, 21);
			this.cboImageName.TabIndex = 5;
			this.cboImageName.SelectedIndexChanged += new System.EventHandler(this.cboDynamicImage_SelectedIndexChanged);
			// 
			// butImageBrowse
			// 
			this.butImageBrowse.Location = new System.Drawing.Point(275, 18);
			this.butImageBrowse.Name = "butImageBrowse";
			this.butImageBrowse.Size = new System.Drawing.Size(27, 21);
			this.butImageBrowse.TabIndex = 3;
			this.butImageBrowse.Text = "...";
			this.butImageBrowse.UseVisualStyleBackColor = true;
			this.butImageBrowse.Click += new System.EventHandler(this.butImageBrowse_Click);
			// 
			// grpImagePreview
			// 
			this.grpImagePreview.Controls.Add(this.picImagePreview);
			this.grpImagePreview.Location = new System.Drawing.Point(5, 3);
			this.grpImagePreview.Name = "grpImagePreview";
			this.grpImagePreview.Size = new System.Drawing.Size(315, 124);
			this.grpImagePreview.TabIndex = 4;
			this.grpImagePreview.TabStop = false;
			this.grpImagePreview.Text = "Preview";
			// 
			// picImagePreview
			// 
			this.picImagePreview.BackColor = System.Drawing.Color.Silver;
			this.picImagePreview.Location = new System.Drawing.Point(13, 18);
			this.picImagePreview.Name = "picImagePreview";
			this.picImagePreview.Size = new System.Drawing.Size(289, 93);
			this.picImagePreview.TabIndex = 0;
			this.picImagePreview.TabStop = false;
			this.picImagePreview.Paint += new System.Windows.Forms.PaintEventHandler(this.picImagePreview_Paint);
			// 
			// tabLabel
			// 
			this.tabLabel.Controls.Add(this.grpLabelDisplay);
			this.tabLabel.Controls.Add(this.grpLabelGroup);
			this.tabLabel.Controls.Add(this.grpInputCodes);
			this.tabLabel.Controls.Add(this.grpLabelTransform);
			this.tabLabel.Controls.Add(this.grpLabelPreview);
			this.tabLabel.Controls.Add(this.grpLabelTextProperies);
			this.tabLabel.Location = new System.Drawing.Point(4, 22);
			this.tabLabel.Name = "tabLabel";
			this.tabLabel.Padding = new System.Windows.Forms.Padding(3);
			this.tabLabel.Size = new System.Drawing.Size(326, 362);
			this.tabLabel.TabIndex = 1;
			this.tabLabel.Text = "Label";
			this.tabLabel.UseVisualStyleBackColor = true;
			// 
			// grpLabelDisplay
			// 
			this.grpLabelDisplay.Controls.Add(this.chkLabelSpot);
			this.grpLabelDisplay.Controls.Add(this.chkLabelArrow);
			this.grpLabelDisplay.Location = new System.Drawing.Point(209, 207);
			this.grpLabelDisplay.Name = "grpLabelDisplay";
			this.grpLabelDisplay.Size = new System.Drawing.Size(111, 85);
			this.grpLabelDisplay.TabIndex = 19;
			this.grpLabelDisplay.TabStop = false;
			this.grpLabelDisplay.Text = "Display";
			// 
			// chkLabelSpot
			// 
			this.chkLabelSpot.AutoSize = true;
			this.chkLabelSpot.Location = new System.Drawing.Point(16, 47);
			this.chkLabelSpot.Name = "chkLabelSpot";
			this.chkLabelSpot.Size = new System.Drawing.Size(77, 17);
			this.chkLabelSpot.TabIndex = 21;
			this.chkLabelSpot.Text = "Label Spot";
			this.chkLabelSpot.UseVisualStyleBackColor = true;
			this.chkLabelSpot.CheckedChanged += new System.EventHandler(this.chkLabelSpot_CheckedChanged);
			// 
			// chkLabelArrow
			// 
			this.chkLabelArrow.AutoSize = true;
			this.chkLabelArrow.Location = new System.Drawing.Point(16, 30);
			this.chkLabelArrow.Name = "chkLabelArrow";
			this.chkLabelArrow.Size = new System.Drawing.Size(82, 17);
			this.chkLabelArrow.TabIndex = 20;
			this.chkLabelArrow.Text = "Label Arrow";
			this.chkLabelArrow.UseVisualStyleBackColor = true;
			this.chkLabelArrow.CheckedChanged += new System.EventHandler(this.chkLabelArrow_CheckedChanged);
			// 
			// grpLabelGroup
			// 
			this.grpLabelGroup.Controls.Add(this.cboLabelGroup);
			this.grpLabelGroup.Location = new System.Drawing.Point(105, 298);
			this.grpLabelGroup.Name = "grpLabelGroup";
			this.grpLabelGroup.Size = new System.Drawing.Size(215, 52);
			this.grpLabelGroup.TabIndex = 15;
			this.grpLabelGroup.TabStop = false;
			this.grpLabelGroup.Text = "Label Group";
			// 
			// cboLabelGroup
			// 
			this.cboLabelGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboLabelGroup.FormattingEnabled = true;
			this.cboLabelGroup.Items.AddRange(new object[] {
            "Left",
            "Right",
            "Center"});
			this.cboLabelGroup.Location = new System.Drawing.Point(13, 18);
			this.cboLabelGroup.Name = "cboLabelGroup";
			this.cboLabelGroup.Size = new System.Drawing.Size(190, 21);
			this.cboLabelGroup.TabIndex = 3;
			this.cboLabelGroup.SelectedIndexChanged += new System.EventHandler(this.cboLabelGroup_SelectedIndexChanged);
			// 
			// grpInputCodes
			// 
			this.grpInputCodes.Controls.Add(this.butInputCodeDelete);
			this.grpInputCodes.Controls.Add(this.butInputCodeNew);
			this.grpInputCodes.Controls.Add(this.lvwInputCodes);
			this.grpInputCodes.Location = new System.Drawing.Point(6, 68);
			this.grpInputCodes.Name = "grpInputCodes";
			this.grpInputCodes.Size = new System.Drawing.Size(314, 133);
			this.grpInputCodes.TabIndex = 14;
			this.grpInputCodes.TabStop = false;
			this.grpInputCodes.Text = "Input Codes";
			// 
			// butInputCodeDelete
			// 
			this.butInputCodeDelete.Location = new System.Drawing.Point(90, 101);
			this.butInputCodeDelete.Name = "butInputCodeDelete";
			this.butInputCodeDelete.Size = new System.Drawing.Size(78, 26);
			this.butInputCodeDelete.TabIndex = 18;
			this.butInputCodeDelete.Text = "Delete";
			this.butInputCodeDelete.UseVisualStyleBackColor = true;
			this.butInputCodeDelete.Click += new System.EventHandler(this.butInputCodeDelete_Click);
			// 
			// butInputCodeNew
			// 
			this.butInputCodeNew.Location = new System.Drawing.Point(6, 101);
			this.butInputCodeNew.Name = "butInputCodeNew";
			this.butInputCodeNew.Size = new System.Drawing.Size(78, 26);
			this.butInputCodeNew.TabIndex = 17;
			this.butInputCodeNew.Text = "New";
			this.butInputCodeNew.UseVisualStyleBackColor = true;
			this.butInputCodeNew.Click += new System.EventHandler(this.butInputCodeNew_Click);
			// 
			// lvwInputCodes
			// 
			this.lvwInputCodes.CheckBoxes = true;
			this.lvwInputCodes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colShowLabel,
            this.colCodeType,
            this.colCodeValue});
			this.lvwInputCodes.DoubleClickDoesCheck = false;
			this.lvwInputCodes.FullRowSelect = true;
			this.lvwInputCodes.GridLines = true;
			this.lvwInputCodes.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvwInputCodes.Location = new System.Drawing.Point(10, 17);
			this.lvwInputCodes.MultiSelect = false;
			this.lvwInputCodes.Name = "lvwInputCodes";
			this.lvwInputCodes.Size = new System.Drawing.Size(291, 78);
			this.lvwInputCodes.TabIndex = 15;
			this.lvwInputCodes.UseCompatibleStateImageBehavior = false;
			this.lvwInputCodes.View = System.Windows.Forms.View.Details;
			// 
			// colShowLabel
			// 
			this.colShowLabel.Text = "";
			this.colShowLabel.Width = 24;
			// 
			// colCodeType
			// 
			this.colCodeType.Text = "Code Type";
			this.colCodeType.Width = 93;
			// 
			// colCodeValue
			// 
			this.colCodeValue.Text = "Code Value";
			this.colCodeValue.Width = 144;
			// 
			// grpLabelTransform
			// 
			this.grpLabelTransform.Controls.Add(this.chkLabelSizeable);
			this.grpLabelTransform.Location = new System.Drawing.Point(6, 298);
			this.grpLabelTransform.Name = "grpLabelTransform";
			this.grpLabelTransform.Size = new System.Drawing.Size(93, 52);
			this.grpLabelTransform.TabIndex = 13;
			this.grpLabelTransform.TabStop = false;
			this.grpLabelTransform.Text = "Transform";
			// 
			// chkLabelSizeable
			// 
			this.chkLabelSizeable.AutoSize = true;
			this.chkLabelSizeable.Location = new System.Drawing.Point(15, 22);
			this.chkLabelSizeable.Name = "chkLabelSizeable";
			this.chkLabelSizeable.Size = new System.Drawing.Size(66, 17);
			this.chkLabelSizeable.TabIndex = 0;
			this.chkLabelSizeable.Text = "Sizeable";
			this.chkLabelSizeable.UseVisualStyleBackColor = true;
			this.chkLabelSizeable.CheckedChanged += new System.EventHandler(this.chkLabelSizeable_CheckedChanged);
			// 
			// grpLabelPreview
			// 
			this.grpLabelPreview.Controls.Add(this.picLabelPreview);
			this.grpLabelPreview.Location = new System.Drawing.Point(5, 3);
			this.grpLabelPreview.Name = "grpLabelPreview";
			this.grpLabelPreview.Size = new System.Drawing.Size(315, 59);
			this.grpLabelPreview.TabIndex = 10;
			this.grpLabelPreview.TabStop = false;
			this.grpLabelPreview.Text = "Preview";
			// 
			// picLabelPreview
			// 
			this.picLabelPreview.BackColor = System.Drawing.Color.Silver;
			this.picLabelPreview.Location = new System.Drawing.Point(13, 18);
			this.picLabelPreview.Name = "picLabelPreview";
			this.picLabelPreview.Size = new System.Drawing.Size(289, 32);
			this.picLabelPreview.TabIndex = 0;
			this.picLabelPreview.TabStop = false;
			this.picLabelPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.picLabelPreview_Paint);
			// 
			// grpLabelTextProperies
			// 
			this.grpLabelTextProperies.Controls.Add(this.rdoOutlineStyle);
			this.grpLabelTextProperies.Controls.Add(this.cboTextAlign);
			this.grpLabelTextProperies.Controls.Add(this.rdoShadowStyle);
			this.grpLabelTextProperies.Controls.Add(this.txtLabelFont);
			this.grpLabelTextProperies.Controls.Add(this.butLabelFont);
			this.grpLabelTextProperies.Controls.Add(this.butLabelColor);
			this.grpLabelTextProperies.Location = new System.Drawing.Point(6, 207);
			this.grpLabelTextProperies.Name = "grpLabelTextProperies";
			this.grpLabelTextProperies.Size = new System.Drawing.Size(197, 85);
			this.grpLabelTextProperies.TabIndex = 8;
			this.grpLabelTextProperies.TabStop = false;
			this.grpLabelTextProperies.Text = "Text Properties";
			// 
			// rdoOutlineStyle
			// 
			this.rdoOutlineStyle.AutoSize = true;
			this.rdoOutlineStyle.Checked = true;
			this.rdoOutlineStyle.Location = new System.Drawing.Point(131, 44);
			this.rdoOutlineStyle.Name = "rdoOutlineStyle";
			this.rdoOutlineStyle.Size = new System.Drawing.Size(58, 17);
			this.rdoOutlineStyle.TabIndex = 0;
			this.rdoOutlineStyle.TabStop = true;
			this.rdoOutlineStyle.Text = "Outline";
			this.rdoOutlineStyle.UseVisualStyleBackColor = true;
			this.rdoOutlineStyle.CheckedChanged += new System.EventHandler(this.rdoTextStyle_CheckedChanged);
			// 
			// cboTextAlign
			// 
			this.cboTextAlign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTextAlign.FormattingEnabled = true;
			this.cboTextAlign.Items.AddRange(new object[] {
            "Left",
            "Right",
            "Center"});
			this.cboTextAlign.Location = new System.Drawing.Point(12, 47);
			this.cboTextAlign.Name = "cboTextAlign";
			this.cboTextAlign.Size = new System.Drawing.Size(113, 21);
			this.cboTextAlign.TabIndex = 2;
			this.cboTextAlign.SelectedIndexChanged += new System.EventHandler(this.cboTextAlign_SelectedIndexChanged);
			// 
			// rdoShadowStyle
			// 
			this.rdoShadowStyle.AutoSize = true;
			this.rdoShadowStyle.Location = new System.Drawing.Point(131, 61);
			this.rdoShadowStyle.Name = "rdoShadowStyle";
			this.rdoShadowStyle.Size = new System.Drawing.Size(64, 17);
			this.rdoShadowStyle.TabIndex = 1;
			this.rdoShadowStyle.Text = "Shadow";
			this.rdoShadowStyle.UseVisualStyleBackColor = true;
			this.rdoShadowStyle.CheckedChanged += new System.EventHandler(this.rdoTextStyle_CheckedChanged);
			// 
			// txtLabelFont
			// 
			this.txtLabelFont.Location = new System.Drawing.Point(48, 19);
			this.txtLabelFont.Name = "txtLabelFont";
			this.txtLabelFont.Size = new System.Drawing.Size(108, 20);
			this.txtLabelFont.TabIndex = 4;
			// 
			// butLabelFont
			// 
			this.butLabelFont.Location = new System.Drawing.Point(162, 19);
			this.butLabelFont.Name = "butLabelFont";
			this.butLabelFont.Size = new System.Drawing.Size(27, 22);
			this.butLabelFont.TabIndex = 5;
			this.butLabelFont.Text = "...";
			this.butLabelFont.UseVisualStyleBackColor = true;
			this.butLabelFont.Click += new System.EventHandler(this.butLabelFont_Click);
			// 
			// butLabelColor
			// 
			this.butLabelColor.Location = new System.Drawing.Point(12, 16);
			this.butLabelColor.Name = "butLabelColor";
			this.butLabelColor.Size = new System.Drawing.Size(30, 25);
			this.butLabelColor.TabIndex = 0;
			this.butLabelColor.UseVisualStyleBackColor = true;
			this.butLabelColor.Click += new System.EventHandler(this.butLabelColor_Click);
			// 
			// frmObject
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(334, 388);
			this.Controls.Add(this.tabControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "frmObject";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Object Properties";
			this.Load += new System.EventHandler(this.ObjectForm_Load);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ObjectForm_FormClosed);
			this.tabControl1.ResumeLayout(false);
			this.tabImage.ResumeLayout(false);
			this.grpImageDisplay.ResumeLayout(false);
			this.grpImageDisplay.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudAlphaFadeValue)).EndInit();
			this.grpImageLink.ResumeLayout(false);
			this.grpImageTransform.ResumeLayout(false);
			this.grpImageTransform.PerformLayout();
			this.grpColor.ResumeLayout(false);
			this.grpColor.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trkBrightness)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trkSaturation)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trkHue)).EndInit();
			this.grpImageName.ResumeLayout(false);
			this.grpImagePreview.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.picImagePreview)).EndInit();
			this.tabLabel.ResumeLayout(false);
			this.grpLabelDisplay.ResumeLayout(false);
			this.grpLabelDisplay.PerformLayout();
			this.grpLabelGroup.ResumeLayout(false);
			this.grpInputCodes.ResumeLayout(false);
			this.grpLabelTransform.ResumeLayout(false);
			this.grpLabelTransform.PerformLayout();
			this.grpLabelPreview.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.picLabelPreview)).EndInit();
			this.grpLabelTextProperies.ResumeLayout(false);
			this.grpLabelTextProperies.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabImage;
		private System.Windows.Forms.TabPage tabLabel;
		private System.Windows.Forms.Button butImageBrowse;
		private System.Windows.Forms.Button butLabelColor;
		private System.Windows.Forms.Button butLabelFont;
		private System.Windows.Forms.TextBox txtLabelFont;
		private System.Windows.Forms.GroupBox grpLabelTextProperies;
		private System.Windows.Forms.GroupBox grpLabelPreview;
		private System.Windows.Forms.PictureBox picLabelPreview;
		private System.Windows.Forms.RadioButton rdoShadowStyle;
		private System.Windows.Forms.RadioButton rdoOutlineStyle;
		private System.Windows.Forms.ComboBox cboTextAlign;
		private System.Windows.Forms.GroupBox grpColor;
		private System.Windows.Forms.TrackBar trkHue;
		private System.Windows.Forms.GroupBox grpImageName;
		private System.Windows.Forms.GroupBox grpImagePreview;
		private System.Windows.Forms.PictureBox picImagePreview;
		private System.Windows.Forms.Label lblHue;
		private System.Windows.Forms.TrackBar trkBrightness;
		private System.Windows.Forms.TrackBar trkSaturation;
		private System.Windows.Forms.Label lblBrightness;
		private System.Windows.Forms.Label lblSaturation;
		private System.Windows.Forms.GroupBox grpImageTransform;
		private System.Windows.Forms.GroupBox grpLabelTransform;
		private System.Windows.Forms.CheckBox chkImageSizeable;
		private System.Windows.Forms.CheckBox chkLabelSizeable;
		private ListViewEx lvwInputCodes;
		private System.Windows.Forms.ColumnHeader colShowLabel;
		private System.Windows.Forms.ColumnHeader colCodeType;
		private System.Windows.Forms.ColumnHeader colCodeValue;
		private System.Windows.Forms.GroupBox grpInputCodes;
		private System.Windows.Forms.GroupBox grpLabelGroup;
		private System.Windows.Forms.ComboBox cboLabelGroup;
		private System.Windows.Forms.Button butInputCodeDelete;
		private System.Windows.Forms.Button butInputCodeNew;
		private System.Windows.Forms.GroupBox grpImageLink;
		private System.Windows.Forms.ComboBox cboLabelLink;
		private System.Windows.Forms.ComboBox cboImageName;
		private System.Windows.Forms.GroupBox grpImageDisplay;
		private System.Windows.Forms.CheckBox chkAlphaFade;
		private System.Windows.Forms.GroupBox grpLabelDisplay;
		private System.Windows.Forms.CheckBox chkLabelSpot;
		private System.Windows.Forms.CheckBox chkLabelArrow;
		private System.Windows.Forms.Label lblAlphaFadeValue;
		private System.Windows.Forms.NumericUpDown nudAlphaFadeValue;
	}
}