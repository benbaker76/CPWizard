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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.mnuMain = new System.Windows.Forms.MenuStrip();
			this.tsMenuFile = new System.Windows.Forms.ToolStripMenuItem();
			this.tsNew = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsMenuOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.tsMenuSave = new System.Windows.Forms.ToolStripMenuItem();
			this.tsMenuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsImport = new System.Windows.Forms.ToolStripMenuItem();
			this.tsImportLayout = new System.Windows.Forms.ToolStripMenuItem();
			this.tsExport = new System.Windows.Forms.ToolStripMenuItem();
			this.tsExportLayout = new System.Windows.Forms.ToolStripMenuItem();
			this.tsExportBatch = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
			this.tsMenuPrint = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsMenuExit = new System.Windows.Forms.ToolStripMenuItem();
			this.tsMenuEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.tsMenuCut = new System.Windows.Forms.ToolStripMenuItem();
			this.tsMenuCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.tsMenuPaste = new System.Windows.Forms.ToolStripMenuItem();
			this.tsDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsMenuOptions = new System.Windows.Forms.ToolStripMenuItem();
			this.tsMenuObject = new System.Windows.Forms.ToolStripMenuItem();
			this.tsMenuAddImage = new System.Windows.Forms.ToolStripMenuItem();
			this.tsMenuAddLabel = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
			this.tsMenuProperties = new System.Windows.Forms.ToolStripMenuItem();
			this.tsMenuView = new System.Windows.Forms.ToolStripMenuItem();
			this.tsPreview = new System.Windows.Forms.ToolStripMenuItem();
			this.tsMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.tsHelpFileMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.tsMenuAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tbMain = new System.Windows.Forms.ToolStrip();
			this.tsButNew = new System.Windows.Forms.ToolStripButton();
			this.tsButOpen = new System.Windows.Forms.ToolStripButton();
			this.tsButSave = new System.Windows.Forms.ToolStripButton();
			this.tsButPrint = new System.Windows.Forms.ToolStripButton();
			this.tsButAddImage = new System.Windows.Forms.ToolStripButton();
			this.tsButAddLabel = new System.Windows.Forms.ToolStripButton();
			this.tsButProperties = new System.Windows.Forms.ToolStripButton();
			this.tsButCut = new System.Windows.Forms.ToolStripButton();
			this.tsButCopy = new System.Windows.Forms.ToolStripButton();
			this.tsButPaste = new System.Windows.Forms.ToolStripButton();
			this.tsButDelete = new System.Windows.Forms.ToolStripButton();
			this.tsButToggleGrid = new System.Windows.Forms.ToolStripButton();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.tsslHeadsoftLogo = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsslInfo1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsslInfo2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsslInfo3 = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsslInfo4 = new System.Windows.Forms.ToolStripStatusLabel();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.cmMenuAddImage = new System.Windows.Forms.ToolStripMenuItem();
			this.cmMenuAddLabel = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.cmMenuCut = new System.Windows.Forms.ToolStripMenuItem();
			this.cmMenuCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.cmMenuPaste = new System.Windows.Forms.ToolStripMenuItem();
			this.cmMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
			this.cmMenuProperties = new System.Windows.Forms.ToolStripMenuItem();
			this.printDocument1 = new System.Drawing.Printing.PrintDocument();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.mnuMain.SuspendLayout();
			this.tbMain.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// mnuMain
			// 
			this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuFile,
            this.tsMenuEdit,
            this.tsMenuObject,
            this.tsMenuView,
            this.tsMenuHelp});
			this.mnuMain.Location = new System.Drawing.Point(0, 0);
			this.mnuMain.Name = "mnuMain";
			this.mnuMain.Size = new System.Drawing.Size(659, 24);
			this.mnuMain.TabIndex = 0;
			this.mnuMain.Text = "menuStrip1";
			// 
			// tsMenuFile
			// 
			this.tsMenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsNew,
            this.toolStripMenuItem3,
            this.tsMenuOpen,
            this.tsMenuSave,
            this.tsMenuSaveAs,
            this.toolStripMenuItem2,
            this.tsImport,
            this.tsExport,
            this.toolStripMenuItem5,
            this.tsMenuPrint,
            this.toolStripMenuItem1,
            this.tsMenuExit});
			this.tsMenuFile.Name = "tsMenuFile";
			this.tsMenuFile.Size = new System.Drawing.Size(37, 20);
			this.tsMenuFile.Text = "File";
			// 
			// tsNew
			// 
			this.tsNew.Name = "tsNew";
			this.tsNew.Size = new System.Drawing.Size(123, 22);
			this.tsNew.Text = "New";
			this.tsNew.Click += new System.EventHandler(this.tsNew_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(120, 6);
			// 
			// tsMenuOpen
			// 
			this.tsMenuOpen.Name = "tsMenuOpen";
			this.tsMenuOpen.Size = new System.Drawing.Size(123, 22);
			this.tsMenuOpen.Text = "Open...";
			this.tsMenuOpen.Click += new System.EventHandler(this.tsMenuOpen_Click);
			// 
			// tsMenuSave
			// 
			this.tsMenuSave.Name = "tsMenuSave";
			this.tsMenuSave.Size = new System.Drawing.Size(123, 22);
			this.tsMenuSave.Text = "Save";
			this.tsMenuSave.Click += new System.EventHandler(this.tsMenuSave_Click);
			// 
			// tsMenuSaveAs
			// 
			this.tsMenuSaveAs.Name = "tsMenuSaveAs";
			this.tsMenuSaveAs.Size = new System.Drawing.Size(123, 22);
			this.tsMenuSaveAs.Text = "Save As...";
			this.tsMenuSaveAs.Click += new System.EventHandler(this.tsMenuSaveAs_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(120, 6);
			// 
			// tsImport
			// 
			this.tsImport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsImportLayout});
			this.tsImport.Name = "tsImport";
			this.tsImport.Size = new System.Drawing.Size(123, 22);
			this.tsImport.Text = "Import";
			// 
			// tsImportLayout
			// 
			this.tsImportLayout.Name = "tsImportLayout";
			this.tsImportLayout.Size = new System.Drawing.Size(110, 22);
			this.tsImportLayout.Text = "Layout";
			this.tsImportLayout.Click += new System.EventHandler(this.tsImportLayout_Click);
			// 
			// tsExport
			// 
			this.tsExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsExportLayout,
            this.tsExportBatch});
			this.tsExport.Name = "tsExport";
			this.tsExport.Size = new System.Drawing.Size(123, 22);
			this.tsExport.Text = "Export";
			// 
			// tsExportLayout
			// 
			this.tsExportLayout.Name = "tsExportLayout";
			this.tsExportLayout.Size = new System.Drawing.Size(110, 22);
			this.tsExportLayout.Text = "Layout";
			this.tsExportLayout.Click += new System.EventHandler(this.tsExportLayout_Click);
			// 
			// tsExportBatch
			// 
			this.tsExportBatch.Name = "tsExportBatch";
			this.tsExportBatch.Size = new System.Drawing.Size(110, 22);
			this.tsExportBatch.Text = "Batch";
			this.tsExportBatch.Click += new System.EventHandler(this.tsExportBatch_Click);
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(120, 6);
			// 
			// tsMenuPrint
			// 
			this.tsMenuPrint.Name = "tsMenuPrint";
			this.tsMenuPrint.Size = new System.Drawing.Size(123, 22);
			this.tsMenuPrint.Text = "Print...";
			this.tsMenuPrint.Click += new System.EventHandler(this.tsMenuPrint_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(120, 6);
			// 
			// tsMenuExit
			// 
			this.tsMenuExit.Name = "tsMenuExit";
			this.tsMenuExit.Size = new System.Drawing.Size(123, 22);
			this.tsMenuExit.Text = "Exit";
			this.tsMenuExit.Click += new System.EventHandler(this.tsMenuExit_Click);
			// 
			// tsMenuEdit
			// 
			this.tsMenuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuCut,
            this.tsMenuCopy,
            this.tsMenuPaste,
            this.tsDelete,
            this.toolStripMenuItem4,
            this.tsMenuOptions});
			this.tsMenuEdit.Name = "tsMenuEdit";
			this.tsMenuEdit.Size = new System.Drawing.Size(39, 20);
			this.tsMenuEdit.Text = "Edit";
			// 
			// tsMenuCut
			// 
			this.tsMenuCut.Name = "tsMenuCut";
			this.tsMenuCut.ShortcutKeyDisplayString = "Ctrl+X";
			this.tsMenuCut.Size = new System.Drawing.Size(144, 22);
			this.tsMenuCut.Text = "Cut";
			this.tsMenuCut.Click += new System.EventHandler(this.tsMenuCut_Click);
			// 
			// tsMenuCopy
			// 
			this.tsMenuCopy.Name = "tsMenuCopy";
			this.tsMenuCopy.ShortcutKeyDisplayString = "Ctrl+C";
			this.tsMenuCopy.Size = new System.Drawing.Size(144, 22);
			this.tsMenuCopy.Text = "Copy";
			this.tsMenuCopy.Click += new System.EventHandler(this.tsMenuCopy_Click);
			// 
			// tsMenuPaste
			// 
			this.tsMenuPaste.Name = "tsMenuPaste";
			this.tsMenuPaste.ShortcutKeyDisplayString = "Ctrl+V";
			this.tsMenuPaste.Size = new System.Drawing.Size(144, 22);
			this.tsMenuPaste.Text = "Paste";
			this.tsMenuPaste.Click += new System.EventHandler(this.tsMenuPaste_Click);
			// 
			// tsDelete
			// 
			this.tsDelete.Name = "tsDelete";
			this.tsDelete.ShortcutKeyDisplayString = "Del";
			this.tsDelete.Size = new System.Drawing.Size(144, 22);
			this.tsDelete.Text = "Delete";
			this.tsDelete.Click += new System.EventHandler(this.tsMenuDelete_Click);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(141, 6);
			// 
			// tsMenuOptions
			// 
			this.tsMenuOptions.Name = "tsMenuOptions";
			this.tsMenuOptions.Size = new System.Drawing.Size(144, 22);
			this.tsMenuOptions.Text = "Options...";
			this.tsMenuOptions.Click += new System.EventHandler(this.tsMenuOptions_Click);
			// 
			// tsMenuObject
			// 
			this.tsMenuObject.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuAddImage,
            this.tsMenuAddLabel,
            this.toolStripMenuItem7,
            this.tsMenuProperties});
			this.tsMenuObject.Name = "tsMenuObject";
			this.tsMenuObject.Size = new System.Drawing.Size(54, 20);
			this.tsMenuObject.Text = "Object";
			// 
			// tsMenuAddImage
			// 
			this.tsMenuAddImage.Name = "tsMenuAddImage";
			this.tsMenuAddImage.Size = new System.Drawing.Size(141, 22);
			this.tsMenuAddImage.Text = "Add Image...";
			this.tsMenuAddImage.Click += new System.EventHandler(this.tsMenuAddImage_Click);
			// 
			// tsMenuAddLabel
			// 
			this.tsMenuAddLabel.Name = "tsMenuAddLabel";
			this.tsMenuAddLabel.Size = new System.Drawing.Size(141, 22);
			this.tsMenuAddLabel.Text = "Add Label...";
			this.tsMenuAddLabel.Click += new System.EventHandler(this.tsMenuAddLabel_Click);
			// 
			// toolStripMenuItem7
			// 
			this.toolStripMenuItem7.Name = "toolStripMenuItem7";
			this.toolStripMenuItem7.Size = new System.Drawing.Size(138, 6);
			// 
			// tsMenuProperties
			// 
			this.tsMenuProperties.Name = "tsMenuProperties";
			this.tsMenuProperties.Size = new System.Drawing.Size(141, 22);
			this.tsMenuProperties.Text = "Properties...";
			this.tsMenuProperties.Click += new System.EventHandler(this.tsMenuProperties_Click);
			// 
			// tsMenuView
			// 
			this.tsMenuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsPreview});
			this.tsMenuView.Name = "tsMenuView";
			this.tsMenuView.Size = new System.Drawing.Size(44, 20);
			this.tsMenuView.Text = "View";
			// 
			// tsPreview
			// 
			this.tsPreview.Name = "tsPreview";
			this.tsPreview.Size = new System.Drawing.Size(115, 22);
			this.tsPreview.Text = "Preview";
			this.tsPreview.Click += new System.EventHandler(this.tsPreview_Click);
			// 
			// tsMenuHelp
			// 
			this.tsMenuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsHelpFileMenu,
            this.tsMenuAbout});
			this.tsMenuHelp.Name = "tsMenuHelp";
			this.tsMenuHelp.Size = new System.Drawing.Size(44, 20);
			this.tsMenuHelp.Text = "Help";
			// 
			// tsHelpFileMenu
			// 
			this.tsHelpFileMenu.Name = "tsHelpFileMenu";
			this.tsHelpFileMenu.Size = new System.Drawing.Size(107, 22);
			this.tsHelpFileMenu.Text = "Help";
			this.tsHelpFileMenu.Click += new System.EventHandler(this.tsHelpFileMenu_Click);
			// 
			// tsMenuAbout
			// 
			this.tsMenuAbout.Name = "tsMenuAbout";
			this.tsMenuAbout.Size = new System.Drawing.Size(107, 22);
			this.tsMenuAbout.Text = "About";
			this.tsMenuAbout.Click += new System.EventHandler(this.tsMenuAbout_Click);
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Text = "CPWizard";
			this.notifyIcon1.Visible = true;
			this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// tbMain
			// 
			this.tbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsButNew,
            this.tsButOpen,
            this.tsButSave,
            this.toolStripSeparator1,
            this.tsButPrint,
            this.toolStripSeparator4,
            this.tsButAddImage,
            this.tsButAddLabel,
            this.tsButProperties,
            this.toolStripSeparator2,
            this.tsButCut,
            this.tsButCopy,
            this.tsButPaste,
            this.tsButDelete,
            this.toolStripSeparator3,
            this.tsButToggleGrid});
			this.tbMain.Location = new System.Drawing.Point(0, 24);
			this.tbMain.Name = "tbMain";
			this.tbMain.Size = new System.Drawing.Size(659, 25);
			this.tbMain.TabIndex = 1;
			this.tbMain.Text = "toolStrip1";
			// 
			// tsButNew
			// 
			this.tsButNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsButNew.Image = ((System.Drawing.Image)(resources.GetObject("tsButNew.Image")));
			this.tsButNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsButNew.Name = "tsButNew";
			this.tsButNew.Size = new System.Drawing.Size(23, 22);
			this.tsButNew.Text = "New";
			this.tsButNew.Click += new System.EventHandler(this.tsButNew_Click);
			// 
			// tsButOpen
			// 
			this.tsButOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsButOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsButOpen.Image")));
			this.tsButOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsButOpen.Name = "tsButOpen";
			this.tsButOpen.Size = new System.Drawing.Size(23, 22);
			this.tsButOpen.Text = "Open";
			this.tsButOpen.Click += new System.EventHandler(this.tsButOpen_Click);
			// 
			// tsButSave
			// 
			this.tsButSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsButSave.Image = ((System.Drawing.Image)(resources.GetObject("tsButSave.Image")));
			this.tsButSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsButSave.Name = "tsButSave";
			this.tsButSave.Size = new System.Drawing.Size(23, 22);
			this.tsButSave.Text = "Save";
			this.tsButSave.Click += new System.EventHandler(this.tsButSave_Click);
			// 
			// tsButPrint
			// 
			this.tsButPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsButPrint.Image = ((System.Drawing.Image)(resources.GetObject("tsButPrint.Image")));
			this.tsButPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsButPrint.Name = "tsButPrint";
			this.tsButPrint.Size = new System.Drawing.Size(23, 22);
			this.tsButPrint.Text = "Print";
			this.tsButPrint.Click += new System.EventHandler(this.tsButPrint_Click);
			// 
			// tsButAddImage
			// 
			this.tsButAddImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsButAddImage.Image = ((System.Drawing.Image)(resources.GetObject("tsButAddImage.Image")));
			this.tsButAddImage.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsButAddImage.Name = "tsButAddImage";
			this.tsButAddImage.Size = new System.Drawing.Size(23, 22);
			this.tsButAddImage.Text = "Add Image";
			this.tsButAddImage.Click += new System.EventHandler(this.tsButAddImage_Click);
			// 
			// tsButAddLabel
			// 
			this.tsButAddLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsButAddLabel.Image = ((System.Drawing.Image)(resources.GetObject("tsButAddLabel.Image")));
			this.tsButAddLabel.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsButAddLabel.Name = "tsButAddLabel";
			this.tsButAddLabel.Size = new System.Drawing.Size(23, 22);
			this.tsButAddLabel.Text = "Add Label";
			this.tsButAddLabel.Click += new System.EventHandler(this.tsButAddLabel_Click);
			// 
			// tsButProperties
			// 
			this.tsButProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsButProperties.Image = ((System.Drawing.Image)(resources.GetObject("tsButProperties.Image")));
			this.tsButProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsButProperties.Name = "tsButProperties";
			this.tsButProperties.Size = new System.Drawing.Size(23, 22);
			this.tsButProperties.Text = "Properties";
			this.tsButProperties.Click += new System.EventHandler(this.tsButProperties_Click);
			// 
			// tsButCut
			// 
			this.tsButCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsButCut.Image = ((System.Drawing.Image)(resources.GetObject("tsButCut.Image")));
			this.tsButCut.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsButCut.Name = "tsButCut";
			this.tsButCut.Size = new System.Drawing.Size(23, 22);
			this.tsButCut.Text = "Cut";
			this.tsButCut.Click += new System.EventHandler(this.tsButCut_Click);
			// 
			// tsButCopy
			// 
			this.tsButCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsButCopy.Image = ((System.Drawing.Image)(resources.GetObject("tsButCopy.Image")));
			this.tsButCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsButCopy.Name = "tsButCopy";
			this.tsButCopy.Size = new System.Drawing.Size(23, 22);
			this.tsButCopy.Text = "Copy";
			this.tsButCopy.Click += new System.EventHandler(this.tsButCopy_Click);
			// 
			// tsButPaste
			// 
			this.tsButPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsButPaste.Image = ((System.Drawing.Image)(resources.GetObject("tsButPaste.Image")));
			this.tsButPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsButPaste.Name = "tsButPaste";
			this.tsButPaste.Size = new System.Drawing.Size(23, 22);
			this.tsButPaste.Text = "Paste";
			this.tsButPaste.Click += new System.EventHandler(this.tsButPaste_Click);
			// 
			// tsButDelete
			// 
			this.tsButDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsButDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsButDelete.Image")));
			this.tsButDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsButDelete.Name = "tsButDelete";
			this.tsButDelete.Size = new System.Drawing.Size(23, 22);
			this.tsButDelete.Text = "Delete";
			this.tsButDelete.Click += new System.EventHandler(this.tsButDelete_Click);
			// 
			// tsButToggleGrid
			// 
			this.tsButToggleGrid.Checked = true;
			this.tsButToggleGrid.CheckOnClick = true;
			this.tsButToggleGrid.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsButToggleGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsButToggleGrid.Image = ((System.Drawing.Image)(resources.GetObject("tsButToggleGrid.Image")));
			this.tsButToggleGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsButToggleGrid.Name = "tsButToggleGrid";
			this.tsButToggleGrid.Size = new System.Drawing.Size(23, 22);
			this.tsButToggleGrid.Text = "Toggle Grid";
			this.tsButToggleGrid.Click += new System.EventHandler(this.tsButToggleGrid_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslHeadsoftLogo,
            this.tsslInfo1,
            this.tsslInfo2,
            this.tsslInfo3,
            this.tsslInfo4});
			this.statusStrip1.Location = new System.Drawing.Point(0, 445);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(659, 22);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// tsslHeadsoftLogo
			// 
			this.tsslHeadsoftLogo.AutoSize = false;
			this.tsslHeadsoftLogo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsslHeadsoftLogo.Image = ((System.Drawing.Image)(resources.GetObject("tsslHeadsoftLogo.Image")));
			this.tsslHeadsoftLogo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.tsslHeadsoftLogo.IsLink = true;
			this.tsslHeadsoftLogo.Name = "tsslHeadsoftLogo";
			this.tsslHeadsoftLogo.Size = new System.Drawing.Size(148, 17);
			this.tsslHeadsoftLogo.Click += new System.EventHandler(this.tsslHeadsoftLogo_Click);
			// 
			// tsslInfo1
			// 
			this.tsslInfo1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.tsslInfo1.Name = "tsslInfo1";
			this.tsslInfo1.Size = new System.Drawing.Size(4, 17);
			// 
			// tsslInfo2
			// 
			this.tsslInfo2.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.tsslInfo2.Name = "tsslInfo2";
			this.tsslInfo2.Size = new System.Drawing.Size(4, 17);
			// 
			// tsslInfo3
			// 
			this.tsslInfo3.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.tsslInfo3.Name = "tsslInfo3";
			this.tsslInfo3.Size = new System.Drawing.Size(4, 17);
			// 
			// tsslInfo4
			// 
			this.tsslInfo4.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.tsslInfo4.Name = "tsslInfo4";
			this.tsslInfo4.Size = new System.Drawing.Size(4, 17);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmMenuAddImage,
            this.cmMenuAddLabel,
            this.toolStripSeparator5,
            this.cmMenuCut,
            this.cmMenuCopy,
            this.cmMenuPaste,
            this.cmMenuDelete,
            this.toolStripMenuItem8,
            this.cmMenuProperties});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(145, 170);
			// 
			// cmMenuAddImage
			// 
			this.cmMenuAddImage.Name = "cmMenuAddImage";
			this.cmMenuAddImage.Size = new System.Drawing.Size(144, 22);
			this.cmMenuAddImage.Text = "Add Image...";
			this.cmMenuAddImage.BackColorChanged += new System.EventHandler(this.cmMenuAddImage_Click);
			this.cmMenuAddImage.Click += new System.EventHandler(this.cmMenuAddImage_Click);
			// 
			// cmMenuAddLabel
			// 
			this.cmMenuAddLabel.Name = "cmMenuAddLabel";
			this.cmMenuAddLabel.Size = new System.Drawing.Size(144, 22);
			this.cmMenuAddLabel.Text = "Add Label...";
			this.cmMenuAddLabel.Click += new System.EventHandler(this.cmMenuAddLabel_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(141, 6);
			// 
			// cmMenuCut
			// 
			this.cmMenuCut.Name = "cmMenuCut";
			this.cmMenuCut.ShortcutKeyDisplayString = "Ctrl+X";
			this.cmMenuCut.Size = new System.Drawing.Size(144, 22);
			this.cmMenuCut.Text = "Cut";
			this.cmMenuCut.Click += new System.EventHandler(this.cmMenuCut_Click);
			// 
			// cmMenuCopy
			// 
			this.cmMenuCopy.Name = "cmMenuCopy";
			this.cmMenuCopy.ShortcutKeyDisplayString = "Ctrl+C";
			this.cmMenuCopy.Size = new System.Drawing.Size(144, 22);
			this.cmMenuCopy.Text = "Copy";
			this.cmMenuCopy.Click += new System.EventHandler(this.cmMenuCopy_Click);
			// 
			// cmMenuPaste
			// 
			this.cmMenuPaste.Name = "cmMenuPaste";
			this.cmMenuPaste.ShortcutKeyDisplayString = "Ctrl+V";
			this.cmMenuPaste.Size = new System.Drawing.Size(144, 22);
			this.cmMenuPaste.Text = "Paste";
			this.cmMenuPaste.Click += new System.EventHandler(this.cmMenuPaste_Click);
			// 
			// cmMenuDelete
			// 
			this.cmMenuDelete.Name = "cmMenuDelete";
			this.cmMenuDelete.ShortcutKeyDisplayString = "Del";
			this.cmMenuDelete.Size = new System.Drawing.Size(144, 22);
			this.cmMenuDelete.Text = "Delete";
			this.cmMenuDelete.Click += new System.EventHandler(this.cmMenuDelete_Click);
			// 
			// toolStripMenuItem8
			// 
			this.toolStripMenuItem8.Name = "toolStripMenuItem8";
			this.toolStripMenuItem8.Size = new System.Drawing.Size(141, 6);
			// 
			// cmMenuProperties
			// 
			this.cmMenuProperties.Name = "cmMenuProperties";
			this.cmMenuProperties.Size = new System.Drawing.Size(144, 22);
			this.cmMenuProperties.Text = "Properties...";
			this.cmMenuProperties.Click += new System.EventHandler(this.cmMenuProperties_Click);
			// 
			// printDocument1
			// 
			this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
			// 
			// panel1
			// 
			this.panel1.AutoScroll = true;
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 49);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(659, 396);
			this.panel1.TabIndex = 3;
			// 
			// pictureBox1
			// 
			this.pictureBox1.ContextMenuStrip = this.contextMenuStrip1;
			this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Cross;
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(640, 480);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
			this.pictureBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDoubleClick);
			this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
			this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
			this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(659, 467);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.tbMain);
			this.Controls.Add(this.mnuMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MainMenuStrip = this.mnuMain;
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
			this.Resize += new System.EventHandler(this.MainForm_Resize);
			this.mnuMain.ResumeLayout(false);
			this.mnuMain.PerformLayout();
			this.tbMain.ResumeLayout(false);
			this.tbMain.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

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

