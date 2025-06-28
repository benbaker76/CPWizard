namespace CPWizard
{
	partial class frmPreview
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPreview));
			this.lvwGameList = new System.Windows.Forms.ListView();
			this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colROM = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colSourceFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colCloneOf = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colRomOf = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colParent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colConstants = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colControls = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colNumPlayers = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colAlternating = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.launchGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsbSave = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbViewCPWizard = new System.Windows.Forms.ToolStripButton();
			this.tsbLaunchGame = new System.Windows.Forms.ToolStripButton();
			this.statusStrip1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lvwGameList
			// 
			this.lvwGameList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colROM,
            this.colSourceFile,
            this.colCloneOf,
            this.colRomOf,
            this.colParent,
            this.colConstants,
            this.colControls,
            this.colNumPlayers,
            this.colAlternating});
			this.lvwGameList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvwGameList.FullRowSelect = true;
			this.lvwGameList.GridLines = true;
			this.lvwGameList.Location = new System.Drawing.Point(0, 49);
			this.lvwGameList.MultiSelect = false;
			this.lvwGameList.Name = "lvwGameList";
			this.lvwGameList.Size = new System.Drawing.Size(374, 230);
			this.lvwGameList.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lvwGameList.TabIndex = 1;
			this.lvwGameList.UseCompatibleStateImageBehavior = false;
			this.lvwGameList.View = System.Windows.Forms.View.Details;
			this.lvwGameList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwGameList_ColumnClick);
			this.lvwGameList.SelectedIndexChanged += new System.EventHandler(this.lvwGameList_SelectedIndexChanged);
			this.lvwGameList.DoubleClick += new System.EventHandler(this.lvwGameList_DoubleClick);
			// 
			// colName
			// 
			this.colName.Text = "Name";
			this.colName.Width = 200;
			// 
			// colROM
			// 
			this.colROM.Text = "ROM";
			this.colROM.Width = 100;
			// 
			// colSourceFile
			// 
			this.colSourceFile.Text = "SourceFile";
			this.colSourceFile.Width = 100;
			// 
			// colCloneOf
			// 
			this.colCloneOf.Text = "CloneOf";
			this.colCloneOf.Width = 100;
			// 
			// colRomOf
			// 
			this.colRomOf.Text = "RomOf";
			this.colRomOf.Width = 100;
			// 
			// colParent
			// 
			this.colParent.Text = "Parent";
			this.colParent.Width = 100;
			// 
			// colConstants
			// 
			this.colConstants.Text = "Constants";
			this.colConstants.Width = 100;
			// 
			// colControls
			// 
			this.colControls.Text = "Controls";
			this.colControls.Width = 200;
			// 
			// colNumPlayers
			// 
			this.colNumPlayers.Text = "NumPlayers";
			this.colNumPlayers.Width = 100;
			// 
			// colAlternating
			// 
			this.colAlternating.Text = "Alternating";
			this.colAlternating.Width = 100;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 279);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(374, 22);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(374, 24);
			this.menuStrip1.TabIndex = 3;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAsToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
			this.saveAsToolStripMenuItem.Text = "Save As...";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(120, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.launchGameToolStripMenuItem});
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.viewToolStripMenuItem.Text = "View";
			// 
			// showToolStripMenuItem
			// 
			this.showToolStripMenuItem.Name = "showToolStripMenuItem";
			this.showToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.showToolStripMenuItem.Text = "CP Wizard";
			this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
			// 
			// launchGameToolStripMenuItem
			// 
			this.launchGameToolStripMenuItem.Name = "launchGameToolStripMenuItem";
			this.launchGameToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.launchGameToolStripMenuItem.Text = "Launch Game";
			this.launchGameToolStripMenuItem.Click += new System.EventHandler(this.launchGameToolStripMenuItem_Click);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSave,
            this.toolStripSeparator1,
            this.tsbViewCPWizard,
            this.tsbLaunchGame});
			this.toolStrip1.Location = new System.Drawing.Point(0, 24);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(374, 25);
			this.toolStrip1.TabIndex = 4;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsbSave
			// 
			this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
			this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSave.Name = "tsbSave";
			this.tsbSave.Size = new System.Drawing.Size(23, 22);
			this.tsbSave.Text = "toolStripButton1";
			this.tsbSave.ToolTipText = "Save";
			this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbViewCPWizard
			// 
			this.tsbViewCPWizard.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbViewCPWizard.Image = ((System.Drawing.Image)(resources.GetObject("tsbViewCPWizard.Image")));
			this.tsbViewCPWizard.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbViewCPWizard.Name = "tsbViewCPWizard";
			this.tsbViewCPWizard.Size = new System.Drawing.Size(23, 22);
			this.tsbViewCPWizard.Text = "toolStripButton2";
			this.tsbViewCPWizard.ToolTipText = "View CPWizard";
			this.tsbViewCPWizard.Click += new System.EventHandler(this.tsbViewCPWizard_Click);
			// 
			// tsbLaunchGame
			// 
			this.tsbLaunchGame.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbLaunchGame.Image = ((System.Drawing.Image)(resources.GetObject("tsbLaunchGame.Image")));
			this.tsbLaunchGame.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbLaunchGame.Name = "tsbLaunchGame";
			this.tsbLaunchGame.Size = new System.Drawing.Size(23, 22);
			this.tsbLaunchGame.Text = "toolStripButton3";
			this.tsbLaunchGame.ToolTipText = "Launch Game";
			this.tsbLaunchGame.Click += new System.EventHandler(this.tsbLaunchGame_Click);
			// 
			// frmPreview
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(374, 301);
			this.Controls.Add(this.lvwGameList);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "frmPreview";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Preview";
			this.Load += new System.EventHandler(this.PreviewForm_Load);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView lvwGameList;
		private System.Windows.Forms.ColumnHeader colName;
		private System.Windows.Forms.ColumnHeader colROM;
		private System.Windows.Forms.ColumnHeader colSourceFile;
		private System.Windows.Forms.ColumnHeader colCloneOf;
		private System.Windows.Forms.ColumnHeader colRomOf;
		private System.Windows.Forms.ColumnHeader colControls;
		private System.Windows.Forms.ColumnHeader colNumPlayers;
		private System.Windows.Forms.ColumnHeader colAlternating;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ColumnHeader colParent;
		private System.Windows.Forms.ColumnHeader colConstants;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem launchGameToolStripMenuItem;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsbSave;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tsbViewCPWizard;
		private System.Windows.Forms.ToolStripButton tsbLaunchGame;

	}
}