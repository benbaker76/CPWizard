namespace CPWizard
{
	partial class frmMain
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            mnuMain = new MenuStrip();
            tsMenuFile = new ToolStripMenuItem();
            tsNew = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripSeparator();
            tsMenuOpen = new ToolStripMenuItem();
            tsMenuSave = new ToolStripMenuItem();
            tsMenuSaveAs = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripSeparator();
            tsImport = new ToolStripMenuItem();
            tsImportLayout = new ToolStripMenuItem();
            tsExport = new ToolStripMenuItem();
            tsExportLayout = new ToolStripMenuItem();
            tsExportBatch = new ToolStripMenuItem();
            toolStripMenuItem5 = new ToolStripSeparator();
            tsMenuPrint = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            tsMenuExit = new ToolStripMenuItem();
            tsMenuEdit = new ToolStripMenuItem();
            tsMenuCut = new ToolStripMenuItem();
            tsMenuCopy = new ToolStripMenuItem();
            tsMenuPaste = new ToolStripMenuItem();
            tsDelete = new ToolStripMenuItem();
            toolStripMenuItem4 = new ToolStripSeparator();
            tsMenuOptions = new ToolStripMenuItem();
            tsMenuObject = new ToolStripMenuItem();
            tsMenuAddImage = new ToolStripMenuItem();
            tsMenuAddLabel = new ToolStripMenuItem();
            toolStripMenuItem7 = new ToolStripSeparator();
            tsMenuProperties = new ToolStripMenuItem();
            tsMenuView = new ToolStripMenuItem();
            tsPreview = new ToolStripMenuItem();
            tsMenuHelp = new ToolStripMenuItem();
            tsHelpFileMenu = new ToolStripMenuItem();
            tsMenuAbout = new ToolStripMenuItem();
            notifyIcon1 = new NotifyIcon(components);
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripSeparator4 = new ToolStripSeparator();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripSeparator3 = new ToolStripSeparator();
            tbMain = new ToolStrip();
            tsButNew = new ToolStripButton();
            tsButOpen = new ToolStripButton();
            tsButSave = new ToolStripButton();
            tsButPrint = new ToolStripButton();
            tsButAddImage = new ToolStripButton();
            tsButAddLabel = new ToolStripButton();
            tsButProperties = new ToolStripButton();
            tsButCut = new ToolStripButton();
            tsButCopy = new ToolStripButton();
            tsButPaste = new ToolStripButton();
            tsButDelete = new ToolStripButton();
            tsButToggleGrid = new ToolStripButton();
            statusStrip1 = new StatusStrip();
            tsslHeadsoftLogo = new ToolStripStatusLabel();
            tsslInfo1 = new ToolStripStatusLabel();
            tsslInfo2 = new ToolStripStatusLabel();
            tsslInfo3 = new ToolStripStatusLabel();
            tsslInfo4 = new ToolStripStatusLabel();
            contextMenuStrip1 = new ContextMenuStrip(components);
            cmMenuAddImage = new ToolStripMenuItem();
            cmMenuAddLabel = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            cmMenuCut = new ToolStripMenuItem();
            cmMenuCopy = new ToolStripMenuItem();
            cmMenuPaste = new ToolStripMenuItem();
            cmMenuDelete = new ToolStripMenuItem();
            toolStripMenuItem8 = new ToolStripSeparator();
            cmMenuProperties = new ToolStripMenuItem();
            printDocument1 = new System.Drawing.Printing.PrintDocument();
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            mnuMain.SuspendLayout();
            tbMain.SuspendLayout();
            statusStrip1.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // mnuMain
            // 
            mnuMain.Items.AddRange(new ToolStripItem[] { tsMenuFile, tsMenuEdit, tsMenuObject, tsMenuView, tsMenuHelp });
            mnuMain.Location = new Point(0, 0);
            mnuMain.Name = "mnuMain";
            mnuMain.Padding = new Padding(7, 2, 0, 2);
            mnuMain.Size = new Size(769, 24);
            mnuMain.TabIndex = 0;
            mnuMain.Text = "menuStrip1";
            // 
            // tsMenuFile
            // 
            tsMenuFile.DropDownItems.AddRange(new ToolStripItem[] { tsNew, toolStripMenuItem3, tsMenuOpen, tsMenuSave, tsMenuSaveAs, toolStripMenuItem2, tsImport, tsExport, toolStripMenuItem5, tsMenuPrint, toolStripMenuItem1, tsMenuExit });
            tsMenuFile.Name = "tsMenuFile";
            tsMenuFile.Size = new Size(37, 20);
            tsMenuFile.Text = "File";
            // 
            // tsNew
            // 
            tsNew.Name = "tsNew";
            tsNew.Size = new Size(123, 22);
            tsNew.Text = "New";
            tsNew.Click += tsNew_Click;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(120, 6);
            // 
            // tsMenuOpen
            // 
            tsMenuOpen.Name = "tsMenuOpen";
            tsMenuOpen.Size = new Size(123, 22);
            tsMenuOpen.Text = "Open...";
            tsMenuOpen.Click += tsMenuOpen_Click;
            // 
            // tsMenuSave
            // 
            tsMenuSave.Name = "tsMenuSave";
            tsMenuSave.Size = new Size(123, 22);
            tsMenuSave.Text = "Save";
            tsMenuSave.Click += tsMenuSave_Click;
            // 
            // tsMenuSaveAs
            // 
            tsMenuSaveAs.Name = "tsMenuSaveAs";
            tsMenuSaveAs.Size = new Size(123, 22);
            tsMenuSaveAs.Text = "Save As...";
            tsMenuSaveAs.Click += tsMenuSaveAs_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(120, 6);
            // 
            // tsImport
            // 
            tsImport.DropDownItems.AddRange(new ToolStripItem[] { tsImportLayout });
            tsImport.Name = "tsImport";
            tsImport.Size = new Size(123, 22);
            tsImport.Text = "Import";
            // 
            // tsImportLayout
            // 
            tsImportLayout.Name = "tsImportLayout";
            tsImportLayout.Size = new Size(110, 22);
            tsImportLayout.Text = "Layout";
            tsImportLayout.Click += tsImportLayout_Click;
            // 
            // tsExport
            // 
            tsExport.DropDownItems.AddRange(new ToolStripItem[] { tsExportLayout, tsExportBatch });
            tsExport.Name = "tsExport";
            tsExport.Size = new Size(123, 22);
            tsExport.Text = "Export";
            // 
            // tsExportLayout
            // 
            tsExportLayout.Name = "tsExportLayout";
            tsExportLayout.Size = new Size(110, 22);
            tsExportLayout.Text = "Layout";
            tsExportLayout.Click += tsExportLayout_Click;
            // 
            // tsExportBatch
            // 
            tsExportBatch.Name = "tsExportBatch";
            tsExportBatch.Size = new Size(110, 22);
            tsExportBatch.Text = "Batch";
            tsExportBatch.Click += tsExportBatch_Click;
            // 
            // toolStripMenuItem5
            // 
            toolStripMenuItem5.Name = "toolStripMenuItem5";
            toolStripMenuItem5.Size = new Size(120, 6);
            // 
            // tsMenuPrint
            // 
            tsMenuPrint.Name = "tsMenuPrint";
            tsMenuPrint.Size = new Size(123, 22);
            tsMenuPrint.Text = "Print...";
            tsMenuPrint.Click += tsMenuPrint_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(120, 6);
            // 
            // tsMenuExit
            // 
            tsMenuExit.Name = "tsMenuExit";
            tsMenuExit.Size = new Size(123, 22);
            tsMenuExit.Text = "Exit";
            tsMenuExit.Click += tsMenuExit_Click;
            // 
            // tsMenuEdit
            // 
            tsMenuEdit.DropDownItems.AddRange(new ToolStripItem[] { tsMenuCut, tsMenuCopy, tsMenuPaste, tsDelete, toolStripMenuItem4, tsMenuOptions });
            tsMenuEdit.Name = "tsMenuEdit";
            tsMenuEdit.Size = new Size(39, 20);
            tsMenuEdit.Text = "Edit";
            // 
            // tsMenuCut
            // 
            tsMenuCut.Name = "tsMenuCut";
            tsMenuCut.ShortcutKeyDisplayString = "Ctrl+X";
            tsMenuCut.Size = new Size(144, 22);
            tsMenuCut.Text = "Cut";
            tsMenuCut.Click += tsMenuCut_Click;
            // 
            // tsMenuCopy
            // 
            tsMenuCopy.Name = "tsMenuCopy";
            tsMenuCopy.ShortcutKeyDisplayString = "Ctrl+C";
            tsMenuCopy.Size = new Size(144, 22);
            tsMenuCopy.Text = "Copy";
            tsMenuCopy.Click += tsMenuCopy_Click;
            // 
            // tsMenuPaste
            // 
            tsMenuPaste.Name = "tsMenuPaste";
            tsMenuPaste.ShortcutKeyDisplayString = "Ctrl+V";
            tsMenuPaste.Size = new Size(144, 22);
            tsMenuPaste.Text = "Paste";
            tsMenuPaste.Click += tsMenuPaste_Click;
            // 
            // tsDelete
            // 
            tsDelete.Name = "tsDelete";
            tsDelete.ShortcutKeyDisplayString = "Del";
            tsDelete.Size = new Size(144, 22);
            tsDelete.Text = "Delete";
            tsDelete.Click += tsMenuDelete_Click;
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new Size(141, 6);
            // 
            // tsMenuOptions
            // 
            tsMenuOptions.Name = "tsMenuOptions";
            tsMenuOptions.Size = new Size(144, 22);
            tsMenuOptions.Text = "Options...";
            tsMenuOptions.Click += tsMenuOptions_Click;
            // 
            // tsMenuObject
            // 
            tsMenuObject.DropDownItems.AddRange(new ToolStripItem[] { tsMenuAddImage, tsMenuAddLabel, toolStripMenuItem7, tsMenuProperties });
            tsMenuObject.Name = "tsMenuObject";
            tsMenuObject.Size = new Size(54, 20);
            tsMenuObject.Text = "Object";
            // 
            // tsMenuAddImage
            // 
            tsMenuAddImage.Name = "tsMenuAddImage";
            tsMenuAddImage.Size = new Size(141, 22);
            tsMenuAddImage.Text = "Add Image...";
            tsMenuAddImage.Click += tsMenuAddImage_Click;
            // 
            // tsMenuAddLabel
            // 
            tsMenuAddLabel.Name = "tsMenuAddLabel";
            tsMenuAddLabel.Size = new Size(141, 22);
            tsMenuAddLabel.Text = "Add Label...";
            tsMenuAddLabel.Click += tsMenuAddLabel_Click;
            // 
            // toolStripMenuItem7
            // 
            toolStripMenuItem7.Name = "toolStripMenuItem7";
            toolStripMenuItem7.Size = new Size(138, 6);
            // 
            // tsMenuProperties
            // 
            tsMenuProperties.Name = "tsMenuProperties";
            tsMenuProperties.Size = new Size(141, 22);
            tsMenuProperties.Text = "Properties...";
            tsMenuProperties.Click += tsMenuProperties_Click;
            // 
            // tsMenuView
            // 
            tsMenuView.DropDownItems.AddRange(new ToolStripItem[] { tsPreview });
            tsMenuView.Name = "tsMenuView";
            tsMenuView.Size = new Size(44, 20);
            tsMenuView.Text = "View";
            // 
            // tsPreview
            // 
            tsPreview.Name = "tsPreview";
            tsPreview.Size = new Size(115, 22);
            tsPreview.Text = "Preview";
            tsPreview.Click += tsPreview_Click;
            // 
            // tsMenuHelp
            // 
            tsMenuHelp.DropDownItems.AddRange(new ToolStripItem[] { tsHelpFileMenu, tsMenuAbout });
            tsMenuHelp.Name = "tsMenuHelp";
            tsMenuHelp.Size = new Size(44, 20);
            tsMenuHelp.Text = "Help";
            // 
            // tsHelpFileMenu
            // 
            tsHelpFileMenu.Name = "tsHelpFileMenu";
            tsHelpFileMenu.Size = new Size(107, 22);
            tsHelpFileMenu.Text = "Help";
            tsHelpFileMenu.Click += tsHelpFileMenu_Click;
            // 
            // tsMenuAbout
            // 
            tsMenuAbout.Name = "tsMenuAbout";
            tsMenuAbout.Size = new Size(107, 22);
            tsMenuAbout.Text = "About";
            tsMenuAbout.Click += tsMenuAbout_Click;
            // 
            // notifyIcon1
            // 
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "CPWizard";
            notifyIcon1.Visible = true;
            notifyIcon1.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 25);
            // 
            // tbMain
            // 
            tbMain.Items.AddRange(new ToolStripItem[] { tsButNew, tsButOpen, tsButSave, toolStripSeparator1, tsButPrint, toolStripSeparator4, tsButAddImage, tsButAddLabel, tsButProperties, toolStripSeparator2, tsButCut, tsButCopy, tsButPaste, tsButDelete, toolStripSeparator3, tsButToggleGrid });
            tbMain.Location = new Point(0, 24);
            tbMain.Name = "tbMain";
            tbMain.Size = new Size(769, 25);
            tbMain.TabIndex = 1;
            tbMain.Text = "toolStrip1";
            // 
            // tsButNew
            // 
            tsButNew.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsButNew.Image = (Image)resources.GetObject("tsButNew.Image");
            tsButNew.ImageTransparentColor = Color.Magenta;
            tsButNew.Name = "tsButNew";
            tsButNew.Size = new Size(23, 22);
            tsButNew.Text = "New";
            tsButNew.Click += tsButNew_Click;
            // 
            // tsButOpen
            // 
            tsButOpen.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsButOpen.Image = (Image)resources.GetObject("tsButOpen.Image");
            tsButOpen.ImageTransparentColor = Color.Magenta;
            tsButOpen.Name = "tsButOpen";
            tsButOpen.Size = new Size(23, 22);
            tsButOpen.Text = "Open";
            tsButOpen.Click += tsButOpen_Click;
            // 
            // tsButSave
            // 
            tsButSave.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsButSave.Image = (Image)resources.GetObject("tsButSave.Image");
            tsButSave.ImageTransparentColor = Color.Magenta;
            tsButSave.Name = "tsButSave";
            tsButSave.Size = new Size(23, 22);
            tsButSave.Text = "Save";
            tsButSave.Click += tsButSave_Click;
            // 
            // tsButPrint
            // 
            tsButPrint.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsButPrint.Image = (Image)resources.GetObject("tsButPrint.Image");
            tsButPrint.ImageTransparentColor = Color.Magenta;
            tsButPrint.Name = "tsButPrint";
            tsButPrint.Size = new Size(23, 22);
            tsButPrint.Text = "Print";
            tsButPrint.Click += tsButPrint_Click;
            // 
            // tsButAddImage
            // 
            tsButAddImage.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsButAddImage.Image = (Image)resources.GetObject("tsButAddImage.Image");
            tsButAddImage.ImageTransparentColor = Color.Magenta;
            tsButAddImage.Name = "tsButAddImage";
            tsButAddImage.Size = new Size(23, 22);
            tsButAddImage.Text = "Add Image";
            tsButAddImage.Click += tsButAddImage_Click;
            // 
            // tsButAddLabel
            // 
            tsButAddLabel.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsButAddLabel.Image = (Image)resources.GetObject("tsButAddLabel.Image");
            tsButAddLabel.ImageTransparentColor = Color.Magenta;
            tsButAddLabel.Name = "tsButAddLabel";
            tsButAddLabel.Size = new Size(23, 22);
            tsButAddLabel.Text = "Add Label";
            tsButAddLabel.Click += tsButAddLabel_Click;
            // 
            // tsButProperties
            // 
            tsButProperties.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsButProperties.Image = (Image)resources.GetObject("tsButProperties.Image");
            tsButProperties.ImageTransparentColor = Color.Magenta;
            tsButProperties.Name = "tsButProperties";
            tsButProperties.Size = new Size(23, 22);
            tsButProperties.Text = "Properties";
            tsButProperties.Click += tsButProperties_Click;
            // 
            // tsButCut
            // 
            tsButCut.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsButCut.Image = (Image)resources.GetObject("tsButCut.Image");
            tsButCut.ImageTransparentColor = Color.Magenta;
            tsButCut.Name = "tsButCut";
            tsButCut.Size = new Size(23, 22);
            tsButCut.Text = "Cut";
            tsButCut.Click += tsButCut_Click;
            // 
            // tsButCopy
            // 
            tsButCopy.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsButCopy.Image = (Image)resources.GetObject("tsButCopy.Image");
            tsButCopy.ImageTransparentColor = Color.Magenta;
            tsButCopy.Name = "tsButCopy";
            tsButCopy.Size = new Size(23, 22);
            tsButCopy.Text = "Copy";
            tsButCopy.Click += tsButCopy_Click;
            // 
            // tsButPaste
            // 
            tsButPaste.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsButPaste.Image = (Image)resources.GetObject("tsButPaste.Image");
            tsButPaste.ImageTransparentColor = Color.Magenta;
            tsButPaste.Name = "tsButPaste";
            tsButPaste.Size = new Size(23, 22);
            tsButPaste.Text = "Paste";
            tsButPaste.Click += tsButPaste_Click;
            // 
            // tsButDelete
            // 
            tsButDelete.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsButDelete.Image = (Image)resources.GetObject("tsButDelete.Image");
            tsButDelete.ImageTransparentColor = Color.Magenta;
            tsButDelete.Name = "tsButDelete";
            tsButDelete.Size = new Size(23, 22);
            tsButDelete.Text = "Delete";
            tsButDelete.Click += tsButDelete_Click;
            // 
            // tsButToggleGrid
            // 
            tsButToggleGrid.Checked = true;
            tsButToggleGrid.CheckOnClick = true;
            tsButToggleGrid.CheckState = CheckState.Checked;
            tsButToggleGrid.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsButToggleGrid.Image = (Image)resources.GetObject("tsButToggleGrid.Image");
            tsButToggleGrid.ImageTransparentColor = Color.Magenta;
            tsButToggleGrid.Name = "tsButToggleGrid";
            tsButToggleGrid.Size = new Size(23, 22);
            tsButToggleGrid.Text = "Toggle Grid";
            tsButToggleGrid.Click += tsButToggleGrid_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { tsslHeadsoftLogo, tsslInfo1, tsslInfo2, tsslInfo3, tsslInfo4 });
            statusStrip1.Location = new Point(0, 517);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 16, 0);
            statusStrip1.Size = new Size(769, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // tsslHeadsoftLogo
            // 
            tsslHeadsoftLogo.AutoSize = false;
            tsslHeadsoftLogo.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsslHeadsoftLogo.Image = (Image)resources.GetObject("tsslHeadsoftLogo.Image");
            tsslHeadsoftLogo.ImageScaling = ToolStripItemImageScaling.None;
            tsslHeadsoftLogo.IsLink = true;
            tsslHeadsoftLogo.Name = "tsslHeadsoftLogo";
            tsslHeadsoftLogo.Size = new Size(148, 17);
            tsslHeadsoftLogo.Click += tsslHeadsoftLogo_Click;
            // 
            // tsslInfo1
            // 
            tsslInfo1.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top | ToolStripStatusLabelBorderSides.Right | ToolStripStatusLabelBorderSides.Bottom;
            tsslInfo1.Name = "tsslInfo1";
            tsslInfo1.Size = new Size(4, 17);
            // 
            // tsslInfo2
            // 
            tsslInfo2.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top | ToolStripStatusLabelBorderSides.Right | ToolStripStatusLabelBorderSides.Bottom;
            tsslInfo2.Name = "tsslInfo2";
            tsslInfo2.Size = new Size(4, 17);
            // 
            // tsslInfo3
            // 
            tsslInfo3.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top | ToolStripStatusLabelBorderSides.Right | ToolStripStatusLabelBorderSides.Bottom;
            tsslInfo3.Name = "tsslInfo3";
            tsslInfo3.Size = new Size(4, 17);
            // 
            // tsslInfo4
            // 
            tsslInfo4.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top | ToolStripStatusLabelBorderSides.Right | ToolStripStatusLabelBorderSides.Bottom;
            tsslInfo4.Name = "tsslInfo4";
            tsslInfo4.Size = new Size(4, 17);
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { cmMenuAddImage, cmMenuAddLabel, toolStripSeparator5, cmMenuCut, cmMenuCopy, cmMenuPaste, cmMenuDelete, toolStripMenuItem8, cmMenuProperties });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(145, 170);
            // 
            // cmMenuAddImage
            // 
            cmMenuAddImage.Name = "cmMenuAddImage";
            cmMenuAddImage.Size = new Size(144, 22);
            cmMenuAddImage.Text = "Add Image...";
            cmMenuAddImage.BackColorChanged += cmMenuAddImage_Click;
            cmMenuAddImage.Click += cmMenuAddImage_Click;
            // 
            // cmMenuAddLabel
            // 
            cmMenuAddLabel.Name = "cmMenuAddLabel";
            cmMenuAddLabel.Size = new Size(144, 22);
            cmMenuAddLabel.Text = "Add Label...";
            cmMenuAddLabel.Click += cmMenuAddLabel_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(141, 6);
            // 
            // cmMenuCut
            // 
            cmMenuCut.Name = "cmMenuCut";
            cmMenuCut.ShortcutKeyDisplayString = "Ctrl+X";
            cmMenuCut.Size = new Size(144, 22);
            cmMenuCut.Text = "Cut";
            cmMenuCut.Click += cmMenuCut_Click;
            // 
            // cmMenuCopy
            // 
            cmMenuCopy.Name = "cmMenuCopy";
            cmMenuCopy.ShortcutKeyDisplayString = "Ctrl+C";
            cmMenuCopy.Size = new Size(144, 22);
            cmMenuCopy.Text = "Copy";
            cmMenuCopy.Click += cmMenuCopy_Click;
            // 
            // cmMenuPaste
            // 
            cmMenuPaste.Name = "cmMenuPaste";
            cmMenuPaste.ShortcutKeyDisplayString = "Ctrl+V";
            cmMenuPaste.Size = new Size(144, 22);
            cmMenuPaste.Text = "Paste";
            cmMenuPaste.Click += cmMenuPaste_Click;
            // 
            // cmMenuDelete
            // 
            cmMenuDelete.Name = "cmMenuDelete";
            cmMenuDelete.ShortcutKeyDisplayString = "Del";
            cmMenuDelete.Size = new Size(144, 22);
            cmMenuDelete.Text = "Delete";
            cmMenuDelete.Click += cmMenuDelete_Click;
            // 
            // toolStripMenuItem8
            // 
            toolStripMenuItem8.Name = "toolStripMenuItem8";
            toolStripMenuItem8.Size = new Size(141, 6);
            // 
            // cmMenuProperties
            // 
            cmMenuProperties.Name = "cmMenuProperties";
            cmMenuProperties.Size = new Size(144, 22);
            cmMenuProperties.Text = "Properties...";
            cmMenuProperties.Click += cmMenuProperties_Click;
            // 
            // printDocument1
            // 
            printDocument1.PrintPage += printDocument1_PrintPage;
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Controls.Add(pictureBox1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 49);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(769, 468);
            panel1.TabIndex = 3;
            // 
            // pictureBox1
            // 
            pictureBox1.ContextMenuStrip = contextMenuStrip1;
            pictureBox1.Cursor = Cursors.Cross;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Margin = new Padding(4, 3, 4, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(640, 480);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Paint += pictureBox1_Paint;
            pictureBox1.MouseDoubleClick += pictureBox1_MouseDoubleClick;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(769, 539);
            Controls.Add(panel1);
            Controls.Add(statusStrip1);
            Controls.Add(tbMain);
            Controls.Add(mnuMain);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MainMenuStrip = mnuMain;
            Margin = new Padding(4, 3, 4, 3);
            Name = "frmMain";
            StartPosition = FormStartPosition.CenterScreen;
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            Shown += MainForm_Shown;
            KeyDown += MainForm_KeyDown;
            KeyUp += MainForm_KeyUp;
            Resize += MainForm_Resize;
            mnuMain.ResumeLayout(false);
            mnuMain.PerformLayout();
            tbMain.ResumeLayout(false);
            tbMain.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            contextMenuStrip1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
		private System.Windows.Forms.ToolStripMenuItem tsMenuFile;
		private System.Windows.Forms.ToolStripMenuItem tsMenuOpen;
		private System.Windows.Forms.ToolStripMenuItem tsMenuSave;
		private System.Windows.Forms.ToolStripMenuItem tsMenuSaveAs;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem tsMenuPrint;
		private System.Windows.Forms.ToolStripMenuItem tsMenuExit;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem tsMenuEdit;
		private System.Windows.Forms.ToolStripMenuItem tsMenuOptions;
		private System.Windows.Forms.ToolStripMenuItem tsMenuCopy;
		private System.Windows.Forms.ToolStripMenuItem tsMenuPaste;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
		private System.Windows.Forms.ToolStripMenuItem tsMenuObject;
		private System.Windows.Forms.ToolStripMenuItem tsMenuHelp;
		private System.Windows.Forms.ToolStripMenuItem tsMenuAbout;
		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private System.Windows.Forms.ToolStripButton tsButNew;
		private System.Windows.Forms.ToolStripButton tsButOpen;
		private System.Windows.Forms.ToolStripButton tsButSave;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tsButPrint;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripButton tsButAddImage;
		private System.Windows.Forms.ToolStripButton tsButAddLabel;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton tsButCopy;
		private System.Windows.Forms.ToolStripButton tsButPaste;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton tsButToggleGrid;
		private System.Windows.Forms.ToolStrip tbMain;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripButton tsButCut;
		private System.Windows.Forms.ToolStripMenuItem tsMenuCut;
		private System.Windows.Forms.ToolStripMenuItem tsMenuAddImage;
		private System.Windows.Forms.ToolStripMenuItem tsMenuAddLabel;
		private System.Windows.Forms.ToolStripButton tsButDelete;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem cmMenuAddImage;
		private System.Windows.Forms.ToolStripMenuItem cmMenuAddLabel;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem cmMenuCut;
		private System.Windows.Forms.ToolStripMenuItem cmMenuCopy;
		private System.Windows.Forms.ToolStripMenuItem cmMenuPaste;
		private System.Windows.Forms.ToolStripMenuItem cmMenuDelete;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
		private System.Windows.Forms.ToolStripMenuItem tsMenuProperties;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
		private System.Windows.Forms.ToolStripMenuItem cmMenuProperties;
		private System.Windows.Forms.ToolStripButton tsButProperties;
		private System.Drawing.Printing.PrintDocument printDocument1;
		private System.Windows.Forms.ToolStripMenuItem tsNew;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem tsImport;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
		private System.Windows.Forms.ToolStripMenuItem tsExport;
		private System.Windows.Forms.ToolStripMenuItem tsMenuView;
		private System.Windows.Forms.ToolStripMenuItem tsPreview;
		private System.Windows.Forms.ToolStripMenuItem tsDelete;
		private System.Windows.Forms.ToolStripMenuItem tsHelpFileMenu;
		private System.Windows.Forms.ToolStripStatusLabel tsslInfo1;
		private System.Windows.Forms.ToolStripStatusLabel tsslInfo2;
		private System.Windows.Forms.ToolStripStatusLabel tsslInfo3;
		private System.Windows.Forms.ToolStripStatusLabel tsslInfo4;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.ToolStripMenuItem tsExportLayout;
		private System.Windows.Forms.ToolStripMenuItem tsExportBatch;
		private System.Windows.Forms.ToolStripMenuItem tsImportLayout;
		private System.Windows.Forms.ToolStripStatusLabel tsslHeadsoftLogo;
	}
}

