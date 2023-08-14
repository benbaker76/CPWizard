namespace CPWizard
{
	partial class frmOptions
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOptions));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.chkAllowBracketedGameNames = new System.Windows.Forms.CheckBox();
			this.grpVerboseLogging = new System.Windows.Forms.GroupBox();
			this.chkDisableSystemSpecs = new System.Windows.Forms.CheckBox();
			this.chkVerboseLogging = new System.Windows.Forms.CheckBox();
			this.grpDynamicDataLoading = new System.Windows.Forms.GroupBox();
			this.chkDynamicDataLoading = new System.Windows.Forms.CheckBox();
			this.grpGhostScriptExe = new System.Windows.Forms.GroupBox();
			this.butGhostScriptExe = new System.Windows.Forms.Button();
			this.txtGhostScriptExe = new System.Windows.Forms.TextBox();
			this.grpVolumeControl = new System.Windows.Forms.GroupBox();
			this.chkVolumeControl = new System.Windows.Forms.CheckBox();
			this.grpHideDesktopOptions = new System.Windows.Forms.GroupBox();
			this.chkDisableScreenSaver = new System.Windows.Forms.CheckBox();
			this.chkMoveMouseOffscreen = new System.Windows.Forms.CheckBox();
			this.chkHideTaskbar = new System.Windows.Forms.CheckBox();
			this.chkHideDesktopUsingForms = new System.Windows.Forms.CheckBox();
			this.chkHideDesktopIcons = new System.Windows.Forms.CheckBox();
			this.chkHideMouseCursor = new System.Windows.Forms.CheckBox();
			this.chkHideDesktopEnable = new System.Windows.Forms.CheckBox();
			this.chkSetWallpaperBlack = new System.Windows.Forms.CheckBox();
			this.grpStartupOptions = new System.Windows.Forms.GroupBox();
			this.chkRunOnStartup = new System.Windows.Forms.CheckBox();
			this.tabLayout = new System.Windows.Forms.TabPage();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chkShowLayoutMouseClick = new System.Windows.Forms.CheckBox();
			this.chkShowLayoutGiveFocus = new System.Windows.Forms.CheckBox();
			this.chkShowLayoutForceForeground = new System.Windows.Forms.CheckBox();
			this.chkShowLayoutTopMost = new System.Windows.Forms.CheckBox();
			this.grpShowRetry = new System.Windows.Forms.GroupBox();
			this.chkShowRetryExitOnFail = new System.Windows.Forms.CheckBox();
			this.nudShowRetryNumRetrys = new System.Windows.Forms.NumericUpDown();
			this.lblInterval = new System.Windows.Forms.Label();
			this.nudShowRetryInterval = new System.Windows.Forms.NumericUpDown();
			this.lblNumRetrys = new System.Windows.Forms.Label();
			this.chkShowRetry = new System.Windows.Forms.CheckBox();
			this.grpLayoutSub = new System.Windows.Forms.GroupBox();
			this.butLayoutSub = new System.Windows.Forms.Button();
			this.txtLayoutSub = new System.Windows.Forms.TextBox();
			this.grpLayoutOptions = new System.Windows.Forms.GroupBox();
			this.chkShowMiniInfo = new System.Windows.Forms.CheckBox();
			this.grpLayoutColors = new System.Windows.Forms.GroupBox();
			this.chkLayoutColorsEnable = new System.Windows.Forms.CheckBox();
			this.lvwLayoutColors = new CPWizard.ListViewEx(this.components);
			this.colColor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colImage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.grpLayoutBackground = new System.Windows.Forms.GroupBox();
			this.butLayoutBackground = new System.Windows.Forms.Button();
			this.txtLayoutBackground = new System.Windows.Forms.TextBox();
			this.grpLayoutName = new System.Windows.Forms.GroupBox();
			this.txtLayoutName = new System.Windows.Forms.TextBox();
			this.tabMAME = new System.Windows.Forms.TabPage();
			this.grpMAMEBak = new System.Windows.Forms.GroupBox();
			this.cboMAMEBak = new System.Windows.Forms.ComboBox();
			this.lblMAMEBak = new System.Windows.Forms.Label();
			this.lblMAMEVersionValue = new System.Windows.Forms.Label();
			this.grpAutoShow = new System.Windows.Forms.GroupBox();
			this.nudAutoShowTimeout = new System.Windows.Forms.NumericUpDown();
			this.lblAutoShowTimeout = new System.Windows.Forms.Label();
			this.nudAutoShowDelay = new System.Windows.Forms.NumericUpDown();
			this.lblAutoShowDelay = new System.Windows.Forms.Label();
			this.chkAutoShow = new System.Windows.Forms.CheckBox();
			this.lblMAMEVersion = new System.Windows.Forms.Label();
			this.grpMAMELayoutSub = new System.Windows.Forms.GroupBox();
			this.butMAMELayoutSub = new System.Windows.Forms.Button();
			this.txtMAMELayoutSub = new System.Windows.Forms.TextBox();
			this.grpMAMELayoutOverride = new System.Windows.Forms.GroupBox();
			this.butMAMELayoutOverride = new System.Windows.Forms.Button();
			this.txtMAMELayoutOverride = new System.Windows.Forms.TextBox();
			this.grpPauseOptions = new System.Windows.Forms.GroupBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.chkMAMEDetectPause = new System.Windows.Forms.CheckBox();
			this.rdoMAMEPauseDiff = new System.Windows.Forms.RadioButton();
			this.chkMAMESendPause = new System.Windows.Forms.CheckBox();
			this.rdoMAMEPauseKey = new System.Windows.Forms.RadioButton();
			this.rdoMAMEPauseMsg = new System.Windows.Forms.RadioButton();
			this.grpLayoutMaps = new System.Windows.Forms.GroupBox();
			this.butLayoutMapDown = new System.Windows.Forms.Button();
			this.butLayoutMapUp = new System.Windows.Forms.Button();
			this.butLayoutMapDelete = new System.Windows.Forms.Button();
			this.butLayoutMapNew = new System.Windows.Forms.Button();
			this.lvwLayoutMaps = new CPWizard.ListViewEx(this.components);
			this.colLayoutMapEnabled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colLayoutMapConstant = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colLayoutMapControl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colLayoutMapNumPlayers = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colLayoutMapAlternating = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colLayoutMapLayout = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.grpMAMELayout = new System.Windows.Forms.GroupBox();
			this.butMAMELayout = new System.Windows.Forms.Button();
			this.txtMAMELayout = new System.Windows.Forms.TextBox();
			this.grpMAMEOptions = new System.Windows.Forms.GroupBox();
			this.chkMAMESkipDisclaimer = new System.Windows.Forms.CheckBox();
			this.chkMAMEScreenshot = new System.Windows.Forms.CheckBox();
			this.chkMAMEUseShowKey = new System.Windows.Forms.CheckBox();
			this.chkMAMEOutputSystem = new System.Windows.Forms.CheckBox();
			this.tabMAMEFilters = new System.Windows.Forms.TabPage();
			this.grpMAMEFilters = new System.Windows.Forms.GroupBox();
			this.lblFilterRotation = new System.Windows.Forms.Label();
			this.cboFilterRotation = new System.Windows.Forms.ComboBox();
			this.chkNoImperfect = new System.Windows.Forms.CheckBox();
			this.chkNoPreliminary = new System.Windows.Forms.CheckBox();
			this.chkNoSystemExceptChd = new System.Windows.Forms.CheckBox();
			this.chkArcadeOnly = new System.Windows.Forms.CheckBox();
			this.chkRunnableOnly = new System.Windows.Forms.CheckBox();
			this.chkNoNotClassified = new System.Windows.Forms.CheckBox();
			this.chkNoUtilities = new System.Windows.Forms.CheckBox();
			this.chkNoReels = new System.Windows.Forms.CheckBox();
			this.chkNoMechanical = new System.Windows.Forms.CheckBox();
			this.chkNoCasino = new System.Windows.Forms.CheckBox();
			this.chkNoGambling = new System.Windows.Forms.CheckBox();
			this.chkNoMahjong = new System.Windows.Forms.CheckBox();
			this.chkNoBios = new System.Windows.Forms.CheckBox();
			this.chkNoAdult = new System.Windows.Forms.CheckBox();
			this.chkNoClones = new System.Windows.Forms.CheckBox();
			this.chkNoDevice = new System.Windows.Forms.CheckBox();
			this.lblDescriptionExcludes = new System.Windows.Forms.Label();
			this.lblNameIncludes = new System.Windows.Forms.Label();
			this.txtDescriptionExcludes = new System.Windows.Forms.TextBox();
			this.txtNameIncludes = new System.Windows.Forms.TextBox();
			this.tabMAMEPaths = new System.Windows.Forms.TabPage();
			this.grpMAMEPaths = new System.Windows.Forms.GroupBox();
			this.lblNvRam = new System.Windows.Forms.Label();
			this.butNvRam = new System.Windows.Forms.Button();
			this.txtNvRam = new System.Windows.Forms.TextBox();
			this.lblHi = new System.Windows.Forms.Label();
			this.butHi = new System.Windows.Forms.Button();
			this.txtHi = new System.Windows.Forms.TextBox();
			this.butSelect = new System.Windows.Forms.Button();
			this.lblSelect = new System.Windows.Forms.Label();
			this.txtSelect = new System.Windows.Forms.TextBox();
			this.butPreviews = new System.Windows.Forms.Button();
			this.lblPreviews = new System.Windows.Forms.Label();
			this.txtPreviews = new System.Windows.Forms.TextBox();
			this.lblPCB = new System.Windows.Forms.Label();
			this.butPCB = new System.Windows.Forms.Button();
			this.txtPCB = new System.Windows.Forms.TextBox();
			this.lblCfg = new System.Windows.Forms.Label();
			this.butCfg = new System.Windows.Forms.Button();
			this.txtCfg = new System.Windows.Forms.TextBox();
			this.lblTitles = new System.Windows.Forms.Label();
			this.butTitles = new System.Windows.Forms.Button();
			this.txtTitles = new System.Windows.Forms.TextBox();
			this.lblSnap = new System.Windows.Forms.Label();
			this.butSnap = new System.Windows.Forms.Button();
			this.txtSnap = new System.Windows.Forms.TextBox();
			this.lblMarquees = new System.Windows.Forms.Label();
			this.butMarquees = new System.Windows.Forms.Button();
			this.txtMarquees = new System.Windows.Forms.TextBox();
			this.lblManuals = new System.Windows.Forms.Label();
			this.butManuals = new System.Windows.Forms.Button();
			this.txtManuals = new System.Windows.Forms.TextBox();
			this.lblIni = new System.Windows.Forms.Label();
			this.butIni = new System.Windows.Forms.Button();
			this.txtIni = new System.Windows.Forms.TextBox();
			this.lblIcons = new System.Windows.Forms.Label();
			this.butIcons = new System.Windows.Forms.Button();
			this.txtIcons = new System.Windows.Forms.TextBox();
			this.lblFlyers = new System.Windows.Forms.Label();
			this.butFlyers = new System.Windows.Forms.Button();
			this.txtFlyers = new System.Windows.Forms.TextBox();
			this.lblCtrlr = new System.Windows.Forms.Label();
			this.butCtrlr = new System.Windows.Forms.Button();
			this.txtCtrlr = new System.Windows.Forms.TextBox();
			this.lblCPanel = new System.Windows.Forms.Label();
			this.butCPanel = new System.Windows.Forms.Button();
			this.txtCPanel = new System.Windows.Forms.TextBox();
			this.lblCabinets = new System.Windows.Forms.Label();
			this.butCabinets = new System.Windows.Forms.Button();
			this.txtCabinets = new System.Windows.Forms.TextBox();
			this.lblMAMEExe = new System.Windows.Forms.Label();
			this.butMAMEExe = new System.Windows.Forms.Button();
			this.txtMAMEExe = new System.Windows.Forms.TextBox();
			this.tabProfiles = new System.Windows.Forms.TabPage();
			this.grpProfiles = new System.Windows.Forms.GroupBox();
			this.butProfileDelete = new System.Windows.Forms.Button();
			this.butProfileNew = new System.Windows.Forms.Button();
			this.lvwProfiles = new CPWizard.ListViewEx(this.components);
			this.colProfileEnabled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colProfileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colProfileType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colProfileLayout = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colProfileLayoutOverride = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colProfileLayoutSub = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colProfileLabels = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colProfileDatabase = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colProfileExecutable = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colProfileWindowTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colProfileWindowClass = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colProfileUseExe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colProfileScreenshot = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colProfileMinimize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colProfileMaximize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colProfileShowKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colProfileHideKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colProfileShowSendKeys = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colProfileHideSendKeys = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colProfileManuals = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colProfileOpCards = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colProfileSnaps = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colProfileTitles = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colProfileCarts = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colProfileBoxes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tabInput = new System.Windows.Forms.TabPage();
			this.grpInput = new System.Windows.Forms.GroupBox();
			this.chkBackShowsCP = new System.Windows.Forms.CheckBox();
			this.chkStopBackMenu = new System.Windows.Forms.CheckBox();
			this.chkEnableExitKey = new System.Windows.Forms.CheckBox();
			this.lblExitKey = new System.Windows.Forms.Label();
			this.butExitKey = new System.Windows.Forms.Button();
			this.txtExitKey = new System.Windows.Forms.TextBox();
			this.chkBackKeyExitMenu = new System.Windows.Forms.CheckBox();
			this.lblHideDesktop = new System.Windows.Forms.Label();
			this.lblVolumeUp = new System.Windows.Forms.Label();
			this.lblVolumeDown = new System.Windows.Forms.Label();
			this.lblMenuRight = new System.Windows.Forms.Label();
			this.lblMenuLeft = new System.Windows.Forms.Label();
			this.lblMenuDown = new System.Windows.Forms.Label();
			this.lblMenuUp = new System.Windows.Forms.Label();
			this.lblSelectKey = new System.Windows.Forms.Label();
			this.lblBackKey = new System.Windows.Forms.Label();
			this.butShowDesktop = new System.Windows.Forms.Button();
			this.butHideDesktop = new System.Windows.Forms.Button();
			this.txtShowDesktop = new System.Windows.Forms.TextBox();
			this.butVolumeUp = new System.Windows.Forms.Button();
			this.txtHideDesktop = new System.Windows.Forms.TextBox();
			this.butVolumeDown = new System.Windows.Forms.Button();
			this.txtVolumeUp = new System.Windows.Forms.TextBox();
			this.butMenuRight = new System.Windows.Forms.Button();
			this.txtVolumeDown = new System.Windows.Forms.TextBox();
			this.butMenuLeft = new System.Windows.Forms.Button();
			this.txtMenuRight = new System.Windows.Forms.TextBox();
			this.butMenuDown = new System.Windows.Forms.Button();
			this.txtMenuLeft = new System.Windows.Forms.TextBox();
			this.butMenuUp = new System.Windows.Forms.Button();
			this.txtMenuDown = new System.Windows.Forms.TextBox();
			this.butSelectKey = new System.Windows.Forms.Button();
			this.txtMenuUp = new System.Windows.Forms.TextBox();
			this.butBackKey = new System.Windows.Forms.Button();
			this.txtSelectKey = new System.Windows.Forms.TextBox();
			this.lblShowKey = new System.Windows.Forms.Label();
			this.txtBackKey = new System.Windows.Forms.TextBox();
			this.butShowKey = new System.Windows.Forms.Button();
			this.txtShowKey = new System.Windows.Forms.TextBox();
			this.lblShowDesktop = new System.Windows.Forms.Label();
			this.tabDisplay = new System.Windows.Forms.TabPage();
			this.grpMenuOptions = new System.Windows.Forms.GroupBox();
			this.lblMenuFont = new System.Windows.Forms.Label();
			this.butChangeMenuBak = new System.Windows.Forms.Button();
			this.chkShowDropShadow = new System.Windows.Forms.CheckBox();
			this.butChooseMenuFont = new System.Windows.Forms.Button();
			this.chkUseMenuBorders = new System.Windows.Forms.CheckBox();
			this.chkHideExitMenu = new System.Windows.Forms.CheckBox();
			this.grpChangeBackgrounds = new System.Windows.Forms.GroupBox();
			this.butChangeDefaultBak = new System.Windows.Forms.Button();
			this.butChangeMainMenuBak = new System.Windows.Forms.Button();
			this.butChangeInfoBak = new System.Windows.Forms.Button();
			this.grpMenuColors = new System.Windows.Forms.GroupBox();
			this.lblMenuBorderColor = new System.Windows.Forms.Label();
			this.butMenuSelectorBorderColor = new System.Windows.Forms.Button();
			this.butMenuBorderColor = new System.Windows.Forms.Button();
			this.lblSelectorBorderColor = new System.Windows.Forms.Label();
			this.lblSelectorBarColor = new System.Windows.Forms.Label();
			this.butMenuFontColor = new System.Windows.Forms.Button();
			this.lblMenuFontColor = new System.Windows.Forms.Label();
			this.butMenuSelectorBarColor = new System.Windows.Forms.Button();
			this.grpGraphicsQuality = new System.Windows.Forms.GroupBox();
			this.chkUseHighQuality = new System.Windows.Forms.CheckBox();
			this.grpLabelOutline = new System.Windows.Forms.GroupBox();
			this.butLabelOutlineColor = new System.Windows.Forms.Button();
			this.lblLabelOutlineSize = new System.Windows.Forms.Label();
			this.nudLabelOutlineSize = new System.Windows.Forms.NumericUpDown();
			this.grpSubScreen = new System.Windows.Forms.GroupBox();
			this.chkSubScreenDisable = new System.Windows.Forms.CheckBox();
			this.nudSubScreen = new System.Windows.Forms.NumericUpDown();
			this.lblSubScreenDelay = new System.Windows.Forms.Label();
			this.chkSubScreen = new System.Windows.Forms.CheckBox();
			this.cboSubScreen = new System.Windows.Forms.ComboBox();
			this.grpAutoRotation = new System.Windows.Forms.GroupBox();
			this.rdoRotateRight = new System.Windows.Forms.RadioButton();
			this.rdoRotateLeft = new System.Windows.Forms.RadioButton();
			this.chkAutoRotate = new System.Windows.Forms.CheckBox();
			this.grpLoadingScreens = new System.Windows.Forms.GroupBox();
			this.chkShowLoadingScreens = new System.Windows.Forms.CheckBox();
			this.grpDisplayChangeDelay = new System.Windows.Forms.GroupBox();
			this.chkDisplayChange = new System.Windows.Forms.CheckBox();
			this.nudDisplayChangeDelay = new System.Windows.Forms.NumericUpDown();
			this.grpAlphaFade = new System.Windows.Forms.GroupBox();
			this.lblAlphaFadeValue = new System.Windows.Forms.Label();
			this.nudAlphaFadeValue = new System.Windows.Forms.NumericUpDown();
			this.chkAlphaFade = new System.Windows.Forms.CheckBox();
			this.grpLabelSpot = new System.Windows.Forms.GroupBox();
			this.butLabelSpotColor = new System.Windows.Forms.Button();
			this.lblLabelSpotSize = new System.Windows.Forms.Label();
			this.nudLabelSpotSize = new System.Windows.Forms.NumericUpDown();
			this.chkLabelSpotShow = new System.Windows.Forms.CheckBox();
			this.grpLabelArrow = new System.Windows.Forms.GroupBox();
			this.butLabelArrowColor = new System.Windows.Forms.Button();
			this.lblLabelArrowSize = new System.Windows.Forms.Label();
			this.nudLabelArrowSize = new System.Windows.Forms.NumericUpDown();
			this.chkLabelArrowShow = new System.Windows.Forms.CheckBox();
			this.grpScreen = new System.Windows.Forms.GroupBox();
			this.cboDisplayScreen = new System.Windows.Forms.ComboBox();
			this.grpRotation = new System.Windows.Forms.GroupBox();
			this.chkFlipY = new System.Windows.Forms.CheckBox();
			this.chkFlipX = new System.Windows.Forms.CheckBox();
			this.cboDisplayRotation = new System.Windows.Forms.ComboBox();
			this.tabData = new System.Windows.Forms.TabPage();
			this.grpAutoShowDataOptions = new System.Windows.Forms.GroupBox();
			this.chkAutoShowExitToMenu = new System.Windows.Forms.CheckBox();
			this.chkAutoShowShowCPOnly = new System.Windows.Forms.CheckBox();
			this.grpEmulatorDataOptions = new System.Windows.Forms.GroupBox();
			this.chkEmulatorIRC = new System.Windows.Forms.CheckBox();
			this.chkNFO = new System.Windows.Forms.CheckBox();
			this.chkOperationCard = new System.Windows.Forms.CheckBox();
			this.chkEmulatorManual = new System.Windows.Forms.CheckBox();
			this.chkEmulatorArtwork = new System.Windows.Forms.CheckBox();
			this.chkEmulatorCP = new System.Windows.Forms.CheckBox();
			this.grpMAMEDataOptions = new System.Windows.Forms.GroupBox();
			this.chkMyHighScore = new System.Windows.Forms.CheckBox();
			this.chkMAMEIRC = new System.Windows.Forms.CheckBox();
			this.chkMAMEManual = new System.Windows.Forms.CheckBox();
			this.chkMAMEArtwork = new System.Windows.Forms.CheckBox();
			this.chkHighScore = new System.Windows.Forms.CheckBox();
			this.chkControlInfo = new System.Windows.Forms.CheckBox();
			this.chkMAMEInfo = new System.Windows.Forms.CheckBox();
			this.chkGameHistory = new System.Windows.Forms.CheckBox();
			this.chkGameInfo = new System.Windows.Forms.CheckBox();
			this.chkMAMECP = new System.Windows.Forms.CheckBox();
			this.grpGeneralDataOptions = new System.Windows.Forms.GroupBox();
			this.chkExitToMenu = new System.Windows.Forms.CheckBox();
			this.chkShowCPOnly = new System.Windows.Forms.CheckBox();
			this.tabIRC = new System.Windows.Forms.TabPage();
			this.grpIRCIsInvisible = new System.Windows.Forms.GroupBox();
			this.chkIRCIsInvisible = new System.Windows.Forms.CheckBox();
			this.grpIRCRealName = new System.Windows.Forms.GroupBox();
			this.txtIRCRealName = new System.Windows.Forms.TextBox();
			this.grpIRCUserName = new System.Windows.Forms.GroupBox();
			this.txtIRCUserName = new System.Windows.Forms.TextBox();
			this.grpIRCNickName = new System.Windows.Forms.GroupBox();
			this.txtIRCNickName = new System.Windows.Forms.TextBox();
			this.grpIRCChannel = new System.Windows.Forms.GroupBox();
			this.txtIRCChannel = new System.Windows.Forms.TextBox();
			this.grpIRCPort = new System.Windows.Forms.GroupBox();
			this.txtIRCPort = new System.Windows.Forms.TextBox();
			this.grpIRCServer = new System.Windows.Forms.GroupBox();
			this.txtIRCServer = new System.Windows.Forms.TextBox();
			this.butOk = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button2 = new System.Windows.Forms.Button();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.butCancel = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.tabControl1.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.grpVerboseLogging.SuspendLayout();
			this.grpDynamicDataLoading.SuspendLayout();
			this.grpGhostScriptExe.SuspendLayout();
			this.grpVolumeControl.SuspendLayout();
			this.grpHideDesktopOptions.SuspendLayout();
			this.grpStartupOptions.SuspendLayout();
			this.tabLayout.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.grpShowRetry.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudShowRetryNumRetrys)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudShowRetryInterval)).BeginInit();
			this.grpLayoutSub.SuspendLayout();
			this.grpLayoutOptions.SuspendLayout();
			this.grpLayoutColors.SuspendLayout();
			this.grpLayoutBackground.SuspendLayout();
			this.grpLayoutName.SuspendLayout();
			this.tabMAME.SuspendLayout();
			this.grpMAMEBak.SuspendLayout();
			this.grpAutoShow.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudAutoShowTimeout)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudAutoShowDelay)).BeginInit();
			this.grpMAMELayoutSub.SuspendLayout();
			this.grpMAMELayoutOverride.SuspendLayout();
			this.grpPauseOptions.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.grpLayoutMaps.SuspendLayout();
			this.grpMAMELayout.SuspendLayout();
			this.grpMAMEOptions.SuspendLayout();
			this.tabMAMEFilters.SuspendLayout();
			this.grpMAMEFilters.SuspendLayout();
			this.tabMAMEPaths.SuspendLayout();
			this.grpMAMEPaths.SuspendLayout();
			this.tabProfiles.SuspendLayout();
			this.grpProfiles.SuspendLayout();
			this.tabInput.SuspendLayout();
			this.grpInput.SuspendLayout();
			this.tabDisplay.SuspendLayout();
			this.grpMenuOptions.SuspendLayout();
			this.grpChangeBackgrounds.SuspendLayout();
			this.grpMenuColors.SuspendLayout();
			this.grpGraphicsQuality.SuspendLayout();
			this.grpLabelOutline.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudLabelOutlineSize)).BeginInit();
			this.grpSubScreen.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudSubScreen)).BeginInit();
			this.grpAutoRotation.SuspendLayout();
			this.grpLoadingScreens.SuspendLayout();
			this.grpDisplayChangeDelay.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudDisplayChangeDelay)).BeginInit();
			this.grpAlphaFade.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudAlphaFadeValue)).BeginInit();
			this.grpLabelSpot.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudLabelSpotSize)).BeginInit();
			this.grpLabelArrow.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudLabelArrowSize)).BeginInit();
			this.grpScreen.SuspendLayout();
			this.grpRotation.SuspendLayout();
			this.tabData.SuspendLayout();
			this.grpAutoShowDataOptions.SuspendLayout();
			this.grpEmulatorDataOptions.SuspendLayout();
			this.grpMAMEDataOptions.SuspendLayout();
			this.grpGeneralDataOptions.SuspendLayout();
			this.tabIRC.SuspendLayout();
			this.grpIRCIsInvisible.SuspendLayout();
			this.grpIRCRealName.SuspendLayout();
			this.grpIRCUserName.SuspendLayout();
			this.grpIRCNickName.SuspendLayout();
			this.grpIRCChannel.SuspendLayout();
			this.grpIRCPort.SuspendLayout();
			this.grpIRCServer.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabGeneral);
			this.tabControl1.Controls.Add(this.tabLayout);
			this.tabControl1.Controls.Add(this.tabMAME);
			this.tabControl1.Controls.Add(this.tabMAMEFilters);
			this.tabControl1.Controls.Add(this.tabMAMEPaths);
			this.tabControl1.Controls.Add(this.tabProfiles);
			this.tabControl1.Controls.Add(this.tabInput);
			this.tabControl1.Controls.Add(this.tabDisplay);
			this.tabControl1.Controls.Add(this.tabData);
			this.tabControl1.Controls.Add(this.tabIRC);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Multiline = true;
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(522, 536);
			this.tabControl1.TabIndex = 0;
			// 
			// tabGeneral
			// 
			this.tabGeneral.Controls.Add(this.groupBox2);
			this.tabGeneral.Controls.Add(this.grpVerboseLogging);
			this.tabGeneral.Controls.Add(this.grpDynamicDataLoading);
			this.tabGeneral.Controls.Add(this.grpGhostScriptExe);
			this.tabGeneral.Controls.Add(this.grpVolumeControl);
			this.tabGeneral.Controls.Add(this.grpHideDesktopOptions);
			this.tabGeneral.Controls.Add(this.grpStartupOptions);
			this.tabGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Size = new System.Drawing.Size(514, 510);
			this.tabGeneral.TabIndex = 5;
			this.tabGeneral.Text = "General";
			this.tabGeneral.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.chkAllowBracketedGameNames);
			this.groupBox2.Location = new System.Drawing.Point(11, 157);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(240, 48);
			this.groupBox2.TabIndex = 9;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Emulator Options";
			// 
			// chkAllowBracketedGameNames
			// 
			this.chkAllowBracketedGameNames.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkAllowBracketedGameNames.AutoSize = true;
			this.chkAllowBracketedGameNames.Location = new System.Drawing.Point(12, 19);
			this.chkAllowBracketedGameNames.Name = "chkAllowBracketedGameNames";
			this.chkAllowBracketedGameNames.Size = new System.Drawing.Size(170, 17);
			this.chkAllowBracketedGameNames.TabIndex = 0;
			this.chkAllowBracketedGameNames.Text = "Allow Bracketed Game Names";
			this.toolTip1.SetToolTip(this.chkAllowBracketedGameNames, "Accommodates any gamenames with \r\nbrackets (e.g. \"Addams Fmialy (USA)\")");
			this.chkAllowBracketedGameNames.UseVisualStyleBackColor = true;
			// 
			// grpVerboseLogging
			// 
			this.grpVerboseLogging.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.grpVerboseLogging.Controls.Add(this.chkDisableSystemSpecs);
			this.grpVerboseLogging.Controls.Add(this.chkVerboseLogging);
			this.grpVerboseLogging.Location = new System.Drawing.Point(265, 214);
			this.grpVerboseLogging.Name = "grpVerboseLogging";
			this.grpVerboseLogging.Size = new System.Drawing.Size(240, 65);
			this.grpVerboseLogging.TabIndex = 5;
			this.grpVerboseLogging.TabStop = false;
			this.grpVerboseLogging.Text = "Logging";
			// 
			// chkDisableSystemSpecs
			// 
			this.chkDisableSystemSpecs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkDisableSystemSpecs.AutoSize = true;
			this.chkDisableSystemSpecs.Location = new System.Drawing.Point(12, 36);
			this.chkDisableSystemSpecs.Name = "chkDisableSystemSpecs";
			this.chkDisableSystemSpecs.Size = new System.Drawing.Size(210, 17);
			this.chkDisableSystemSpecs.TabIndex = 1;
			this.chkDisableSystemSpecs.Text = "Disable System Specs Logging (Faster)";
			this.toolTip1.SetToolTip(this.chkDisableSystemSpecs, "Speeds up startup");
			this.chkDisableSystemSpecs.UseVisualStyleBackColor = true;
			// 
			// chkVerboseLogging
			// 
			this.chkVerboseLogging.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkVerboseLogging.AutoSize = true;
			this.chkVerboseLogging.Location = new System.Drawing.Point(12, 19);
			this.chkVerboseLogging.Name = "chkVerboseLogging";
			this.chkVerboseLogging.Size = new System.Drawing.Size(142, 17);
			this.chkVerboseLogging.TabIndex = 0;
			this.chkVerboseLogging.Text = "Enable Verbose (Slower)";
			this.toolTip1.SetToolTip(this.chkVerboseLogging, "Increases Layout display times\r\nsignificantly");
			this.chkVerboseLogging.UseVisualStyleBackColor = true;
			// 
			// grpDynamicDataLoading
			// 
			this.grpDynamicDataLoading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.grpDynamicDataLoading.Controls.Add(this.chkDynamicDataLoading);
			this.grpDynamicDataLoading.Location = new System.Drawing.Point(265, 157);
			this.grpDynamicDataLoading.Name = "grpDynamicDataLoading";
			this.grpDynamicDataLoading.Size = new System.Drawing.Size(240, 48);
			this.grpDynamicDataLoading.TabIndex = 4;
			this.grpDynamicDataLoading.TabStop = false;
			this.grpDynamicDataLoading.Text = "Dynamic Data Loading";
			// 
			// chkDynamicDataLoading
			// 
			this.chkDynamicDataLoading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkDynamicDataLoading.AutoSize = true;
			this.chkDynamicDataLoading.Location = new System.Drawing.Point(12, 19);
			this.chkDynamicDataLoading.Name = "chkDynamicDataLoading";
			this.chkDynamicDataLoading.Size = new System.Drawing.Size(59, 17);
			this.chkDynamicDataLoading.TabIndex = 0;
			this.chkDynamicDataLoading.Text = "Enable";
			this.chkDynamicDataLoading.UseVisualStyleBackColor = true;
			this.chkDynamicDataLoading.CheckedChanged += new System.EventHandler(this.chkDynamicDataLoading_CheckedChanged);
			// 
			// grpGhostScriptExe
			// 
			this.grpGhostScriptExe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.grpGhostScriptExe.Controls.Add(this.butGhostScriptExe);
			this.grpGhostScriptExe.Controls.Add(this.txtGhostScriptExe);
			this.grpGhostScriptExe.Location = new System.Drawing.Point(265, 285);
			this.grpGhostScriptExe.Name = "grpGhostScriptExe";
			this.grpGhostScriptExe.Size = new System.Drawing.Size(240, 48);
			this.grpGhostScriptExe.TabIndex = 3;
			this.grpGhostScriptExe.TabStop = false;
			this.grpGhostScriptExe.Text = "GhostScript Exe";
			// 
			// butGhostScriptExe
			// 
			this.butGhostScriptExe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butGhostScriptExe.Location = new System.Drawing.Point(201, 18);
			this.butGhostScriptExe.Name = "butGhostScriptExe";
			this.butGhostScriptExe.Size = new System.Drawing.Size(27, 21);
			this.butGhostScriptExe.TabIndex = 1;
			this.butGhostScriptExe.Text = "...";
			this.butGhostScriptExe.UseVisualStyleBackColor = true;
			this.butGhostScriptExe.Click += new System.EventHandler(this.butGhostScriptExe_Click);
			// 
			// txtGhostScriptExe
			// 
			this.txtGhostScriptExe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtGhostScriptExe.Location = new System.Drawing.Point(12, 18);
			this.txtGhostScriptExe.Name = "txtGhostScriptExe";
			this.txtGhostScriptExe.Size = new System.Drawing.Size(183, 20);
			this.txtGhostScriptExe.TabIndex = 0;
			// 
			// grpVolumeControl
			// 
			this.grpVolumeControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.grpVolumeControl.Controls.Add(this.chkVolumeControl);
			this.grpVolumeControl.Location = new System.Drawing.Point(265, 6);
			this.grpVolumeControl.Name = "grpVolumeControl";
			this.grpVolumeControl.Size = new System.Drawing.Size(240, 48);
			this.grpVolumeControl.TabIndex = 1;
			this.grpVolumeControl.TabStop = false;
			this.grpVolumeControl.Text = "Volume Control";
			// 
			// chkVolumeControl
			// 
			this.chkVolumeControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkVolumeControl.AutoSize = true;
			this.chkVolumeControl.Location = new System.Drawing.Point(12, 19);
			this.chkVolumeControl.Name = "chkVolumeControl";
			this.chkVolumeControl.Size = new System.Drawing.Size(59, 17);
			this.chkVolumeControl.TabIndex = 0;
			this.chkVolumeControl.Text = "Enable";
			this.chkVolumeControl.UseVisualStyleBackColor = true;
			// 
			// grpHideDesktopOptions
			// 
			this.grpHideDesktopOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpHideDesktopOptions.Controls.Add(this.chkDisableScreenSaver);
			this.grpHideDesktopOptions.Controls.Add(this.chkMoveMouseOffscreen);
			this.grpHideDesktopOptions.Controls.Add(this.chkHideTaskbar);
			this.grpHideDesktopOptions.Controls.Add(this.chkHideDesktopUsingForms);
			this.grpHideDesktopOptions.Controls.Add(this.chkHideDesktopIcons);
			this.grpHideDesktopOptions.Controls.Add(this.chkHideMouseCursor);
			this.grpHideDesktopOptions.Controls.Add(this.chkHideDesktopEnable);
			this.grpHideDesktopOptions.Controls.Add(this.chkSetWallpaperBlack);
			this.grpHideDesktopOptions.Location = new System.Drawing.Point(11, 60);
			this.grpHideDesktopOptions.Name = "grpHideDesktopOptions";
			this.grpHideDesktopOptions.Size = new System.Drawing.Size(494, 91);
			this.grpHideDesktopOptions.TabIndex = 2;
			this.grpHideDesktopOptions.TabStop = false;
			this.grpHideDesktopOptions.Text = "Hide Desktop Options";
			// 
			// chkDisableScreenSaver
			// 
			this.chkDisableScreenSaver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkDisableScreenSaver.AutoSize = true;
			this.chkDisableScreenSaver.Location = new System.Drawing.Point(266, 67);
			this.chkDisableScreenSaver.Name = "chkDisableScreenSaver";
			this.chkDisableScreenSaver.Size = new System.Drawing.Size(126, 17);
			this.chkDisableScreenSaver.TabIndex = 7;
			this.chkDisableScreenSaver.Text = "Disable ScreenSaver";
			this.chkDisableScreenSaver.UseVisualStyleBackColor = true;
			// 
			// chkMoveMouseOffscreen
			// 
			this.chkMoveMouseOffscreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkMoveMouseOffscreen.AutoSize = true;
			this.chkMoveMouseOffscreen.Location = new System.Drawing.Point(266, 51);
			this.chkMoveMouseOffscreen.Name = "chkMoveMouseOffscreen";
			this.chkMoveMouseOffscreen.Size = new System.Drawing.Size(137, 17);
			this.chkMoveMouseOffscreen.TabIndex = 5;
			this.chkMoveMouseOffscreen.Text = "Move Mouse Offscreen";
			this.chkMoveMouseOffscreen.UseVisualStyleBackColor = true;
			// 
			// chkHideTaskbar
			// 
			this.chkHideTaskbar.AutoSize = true;
			this.chkHideTaskbar.Location = new System.Drawing.Point(12, 67);
			this.chkHideTaskbar.Name = "chkHideTaskbar";
			this.chkHideTaskbar.Size = new System.Drawing.Size(90, 17);
			this.chkHideTaskbar.TabIndex = 6;
			this.chkHideTaskbar.Text = "Hide Taskbar";
			this.chkHideTaskbar.UseVisualStyleBackColor = true;
			// 
			// chkHideDesktopUsingForms
			// 
			this.chkHideDesktopUsingForms.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkHideDesktopUsingForms.AutoSize = true;
			this.chkHideDesktopUsingForms.Location = new System.Drawing.Point(266, 19);
			this.chkHideDesktopUsingForms.Name = "chkHideDesktopUsingForms";
			this.chkHideDesktopUsingForms.Size = new System.Drawing.Size(152, 17);
			this.chkHideDesktopUsingForms.TabIndex = 1;
			this.chkHideDesktopUsingForms.Text = "Hide Desktop Using Forms";
			this.chkHideDesktopUsingForms.UseVisualStyleBackColor = true;
			// 
			// chkHideDesktopIcons
			// 
			this.chkHideDesktopIcons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkHideDesktopIcons.AutoSize = true;
			this.chkHideDesktopIcons.Location = new System.Drawing.Point(266, 35);
			this.chkHideDesktopIcons.Name = "chkHideDesktopIcons";
			this.chkHideDesktopIcons.Size = new System.Drawing.Size(120, 17);
			this.chkHideDesktopIcons.TabIndex = 3;
			this.chkHideDesktopIcons.Text = "Hide Desktop Icons";
			this.chkHideDesktopIcons.UseVisualStyleBackColor = true;
			// 
			// chkHideMouseCursor
			// 
			this.chkHideMouseCursor.AutoSize = true;
			this.chkHideMouseCursor.Location = new System.Drawing.Point(12, 35);
			this.chkHideMouseCursor.Name = "chkHideMouseCursor";
			this.chkHideMouseCursor.Size = new System.Drawing.Size(116, 17);
			this.chkHideMouseCursor.TabIndex = 2;
			this.chkHideMouseCursor.Text = "Hide Mouse Cursor";
			this.chkHideMouseCursor.UseVisualStyleBackColor = true;
			// 
			// chkHideDesktopEnable
			// 
			this.chkHideDesktopEnable.AutoSize = true;
			this.chkHideDesktopEnable.Location = new System.Drawing.Point(12, 19);
			this.chkHideDesktopEnable.Name = "chkHideDesktopEnable";
			this.chkHideDesktopEnable.Size = new System.Drawing.Size(59, 17);
			this.chkHideDesktopEnable.TabIndex = 0;
			this.chkHideDesktopEnable.Text = "Enable";
			this.chkHideDesktopEnable.UseVisualStyleBackColor = true;
			// 
			// chkSetWallpaperBlack
			// 
			this.chkSetWallpaperBlack.AutoSize = true;
			this.chkSetWallpaperBlack.Location = new System.Drawing.Point(12, 51);
			this.chkSetWallpaperBlack.Name = "chkSetWallpaperBlack";
			this.chkSetWallpaperBlack.Size = new System.Drawing.Size(123, 17);
			this.chkSetWallpaperBlack.TabIndex = 4;
			this.chkSetWallpaperBlack.Text = "Set Wallpaper Black";
			this.chkSetWallpaperBlack.UseVisualStyleBackColor = true;
			// 
			// grpStartupOptions
			// 
			this.grpStartupOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpStartupOptions.Controls.Add(this.chkRunOnStartup);
			this.grpStartupOptions.Location = new System.Drawing.Point(11, 6);
			this.grpStartupOptions.Name = "grpStartupOptions";
			this.grpStartupOptions.Size = new System.Drawing.Size(240, 48);
			this.grpStartupOptions.TabIndex = 0;
			this.grpStartupOptions.TabStop = false;
			this.grpStartupOptions.Text = "Startup Options";
			// 
			// chkRunOnStartup
			// 
			this.chkRunOnStartup.AutoSize = true;
			this.chkRunOnStartup.Location = new System.Drawing.Point(12, 19);
			this.chkRunOnStartup.Name = "chkRunOnStartup";
			this.chkRunOnStartup.Size = new System.Drawing.Size(98, 17);
			this.chkRunOnStartup.TabIndex = 0;
			this.chkRunOnStartup.Text = "Run on Startup";
			this.chkRunOnStartup.UseVisualStyleBackColor = true;
			// 
			// tabLayout
			// 
			this.tabLayout.Controls.Add(this.groupBox1);
			this.tabLayout.Controls.Add(this.grpShowRetry);
			this.tabLayout.Controls.Add(this.grpLayoutSub);
			this.tabLayout.Controls.Add(this.grpLayoutOptions);
			this.tabLayout.Controls.Add(this.grpLayoutColors);
			this.tabLayout.Controls.Add(this.grpLayoutBackground);
			this.tabLayout.Controls.Add(this.grpLayoutName);
			this.tabLayout.Location = new System.Drawing.Point(4, 22);
			this.tabLayout.Name = "tabLayout";
			this.tabLayout.Size = new System.Drawing.Size(514, 510);
			this.tabLayout.TabIndex = 3;
			this.tabLayout.Text = "Layout";
			this.tabLayout.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.chkShowLayoutMouseClick);
			this.groupBox1.Controls.Add(this.chkShowLayoutGiveFocus);
			this.groupBox1.Controls.Add(this.chkShowLayoutForceForeground);
			this.groupBox1.Controls.Add(this.chkShowLayoutTopMost);
			this.groupBox1.Location = new System.Drawing.Point(11, 368);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(494, 47);
			this.groupBox1.TabIndex = 39;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Show Layout";
			// 
			// chkShowLayoutMouseClick
			// 
			this.chkShowLayoutMouseClick.AutoSize = true;
			this.chkShowLayoutMouseClick.Location = new System.Drawing.Point(314, 20);
			this.chkShowLayoutMouseClick.Name = "chkShowLayoutMouseClick";
			this.chkShowLayoutMouseClick.Size = new System.Drawing.Size(84, 17);
			this.chkShowLayoutMouseClick.TabIndex = 5;
			this.chkShowLayoutMouseClick.Text = "Mouse Click";
			this.chkShowLayoutMouseClick.UseVisualStyleBackColor = true;
			// 
			// chkShowLayoutGiveFocus
			// 
			this.chkShowLayoutGiveFocus.AutoSize = true;
			this.chkShowLayoutGiveFocus.Location = new System.Drawing.Point(213, 20);
			this.chkShowLayoutGiveFocus.Name = "chkShowLayoutGiveFocus";
			this.chkShowLayoutGiveFocus.Size = new System.Drawing.Size(80, 17);
			this.chkShowLayoutGiveFocus.TabIndex = 4;
			this.chkShowLayoutGiveFocus.Text = "Give Focus";
			this.chkShowLayoutGiveFocus.UseVisualStyleBackColor = true;
			// 
			// chkShowLayoutForceForeground
			// 
			this.chkShowLayoutForceForeground.AutoSize = true;
			this.chkShowLayoutForceForeground.Location = new System.Drawing.Point(92, 20);
			this.chkShowLayoutForceForeground.Name = "chkShowLayoutForceForeground";
			this.chkShowLayoutForceForeground.Size = new System.Drawing.Size(110, 17);
			this.chkShowLayoutForceForeground.TabIndex = 3;
			this.chkShowLayoutForceForeground.Text = "Force Foreground";
			this.chkShowLayoutForceForeground.UseVisualStyleBackColor = true;
			// 
			// chkShowLayoutTopMost
			// 
			this.chkShowLayoutTopMost.AutoSize = true;
			this.chkShowLayoutTopMost.Location = new System.Drawing.Point(11, 20);
			this.chkShowLayoutTopMost.Name = "chkShowLayoutTopMost";
			this.chkShowLayoutTopMost.Size = new System.Drawing.Size(71, 17);
			this.chkShowLayoutTopMost.TabIndex = 2;
			this.chkShowLayoutTopMost.Text = "Top Most";
			this.chkShowLayoutTopMost.UseVisualStyleBackColor = true;
			// 
			// grpShowRetry
			// 
			this.grpShowRetry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpShowRetry.Controls.Add(this.chkShowRetryExitOnFail);
			this.grpShowRetry.Controls.Add(this.nudShowRetryNumRetrys);
			this.grpShowRetry.Controls.Add(this.lblInterval);
			this.grpShowRetry.Controls.Add(this.nudShowRetryInterval);
			this.grpShowRetry.Controls.Add(this.lblNumRetrys);
			this.grpShowRetry.Controls.Add(this.chkShowRetry);
			this.grpShowRetry.Location = new System.Drawing.Point(11, 421);
			this.grpShowRetry.Name = "grpShowRetry";
			this.grpShowRetry.Size = new System.Drawing.Size(494, 47);
			this.grpShowRetry.TabIndex = 38;
			this.grpShowRetry.TabStop = false;
			this.grpShowRetry.Text = "Show Retry";
			// 
			// chkShowRetryExitOnFail
			// 
			this.chkShowRetryExitOnFail.AutoSize = true;
			this.chkShowRetryExitOnFail.Location = new System.Drawing.Point(314, 20);
			this.chkShowRetryExitOnFail.Name = "chkShowRetryExitOnFail";
			this.chkShowRetryExitOnFail.Size = new System.Drawing.Size(79, 17);
			this.chkShowRetryExitOnFail.TabIndex = 6;
			this.chkShowRetryExitOnFail.Text = "Exit On Fail";
			this.chkShowRetryExitOnFail.UseVisualStyleBackColor = true;
			// 
			// nudShowRetryNumRetrys
			// 
			this.nudShowRetryNumRetrys.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.nudShowRetryNumRetrys.Location = new System.Drawing.Point(116, 17);
			this.nudShowRetryNumRetrys.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.nudShowRetryNumRetrys.Name = "nudShowRetryNumRetrys";
			this.nudShowRetryNumRetrys.ReadOnly = true;
			this.nudShowRetryNumRetrys.Size = new System.Drawing.Size(52, 20);
			this.nudShowRetryNumRetrys.TabIndex = 1;
			// 
			// lblInterval
			// 
			this.lblInterval.AutoSize = true;
			this.lblInterval.Location = new System.Drawing.Point(174, 21);
			this.lblInterval.Name = "lblInterval";
			this.lblInterval.Size = new System.Drawing.Size(67, 13);
			this.lblInterval.TabIndex = 5;
			this.lblInterval.Text = "Interval (ms):";
			// 
			// nudShowRetryInterval
			// 
			this.nudShowRetryInterval.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.nudShowRetryInterval.Location = new System.Drawing.Point(246, 17);
			this.nudShowRetryInterval.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.nudShowRetryInterval.Name = "nudShowRetryInterval";
			this.nudShowRetryInterval.ReadOnly = true;
			this.nudShowRetryInterval.Size = new System.Drawing.Size(59, 20);
			this.nudShowRetryInterval.TabIndex = 4;
			// 
			// lblNumRetrys
			// 
			this.lblNumRetrys.AutoSize = true;
			this.lblNumRetrys.Location = new System.Drawing.Point(72, 21);
			this.lblNumRetrys.Name = "lblNumRetrys";
			this.lblNumRetrys.Size = new System.Drawing.Size(42, 13);
			this.lblNumRetrys.TabIndex = 3;
			this.lblNumRetrys.Text = "Retry\'s:";
			// 
			// chkShowRetry
			// 
			this.chkShowRetry.AutoSize = true;
			this.chkShowRetry.Location = new System.Drawing.Point(11, 20);
			this.chkShowRetry.Name = "chkShowRetry";
			this.chkShowRetry.Size = new System.Drawing.Size(59, 17);
			this.chkShowRetry.TabIndex = 2;
			this.chkShowRetry.Text = "Enable";
			this.chkShowRetry.UseVisualStyleBackColor = true;
			// 
			// grpLayoutSub
			// 
			this.grpLayoutSub.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpLayoutSub.Controls.Add(this.butLayoutSub);
			this.grpLayoutSub.Controls.Add(this.txtLayoutSub);
			this.grpLayoutSub.Location = new System.Drawing.Point(11, 62);
			this.grpLayoutSub.Name = "grpLayoutSub";
			this.grpLayoutSub.Size = new System.Drawing.Size(240, 51);
			this.grpLayoutSub.TabIndex = 27;
			this.grpLayoutSub.TabStop = false;
			this.grpLayoutSub.Text = "Layout Sub";
			// 
			// butLayoutSub
			// 
			this.butLayoutSub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butLayoutSub.Location = new System.Drawing.Point(203, 20);
			this.butLayoutSub.Name = "butLayoutSub";
			this.butLayoutSub.Size = new System.Drawing.Size(27, 21);
			this.butLayoutSub.TabIndex = 1;
			this.butLayoutSub.Text = "...";
			this.butLayoutSub.UseVisualStyleBackColor = true;
			this.butLayoutSub.Click += new System.EventHandler(this.butLayoutSub_Click);
			// 
			// txtLayoutSub
			// 
			this.txtLayoutSub.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtLayoutSub.Location = new System.Drawing.Point(12, 20);
			this.txtLayoutSub.Name = "txtLayoutSub";
			this.txtLayoutSub.Size = new System.Drawing.Size(185, 20);
			this.txtLayoutSub.TabIndex = 0;
			// 
			// grpLayoutOptions
			// 
			this.grpLayoutOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.grpLayoutOptions.Controls.Add(this.chkShowMiniInfo);
			this.grpLayoutOptions.Location = new System.Drawing.Point(265, 63);
			this.grpLayoutOptions.Name = "grpLayoutOptions";
			this.grpLayoutOptions.Size = new System.Drawing.Size(240, 50);
			this.grpLayoutOptions.TabIndex = 26;
			this.grpLayoutOptions.TabStop = false;
			this.grpLayoutOptions.Text = "Layout Options";
			// 
			// chkShowMiniInfo
			// 
			this.chkShowMiniInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkShowMiniInfo.AutoSize = true;
			this.chkShowMiniInfo.Location = new System.Drawing.Point(12, 21);
			this.chkShowMiniInfo.Name = "chkShowMiniInfo";
			this.chkShowMiniInfo.Size = new System.Drawing.Size(96, 17);
			this.chkShowMiniInfo.TabIndex = 4;
			this.chkShowMiniInfo.Text = "Show Mini Info";
			this.chkShowMiniInfo.UseVisualStyleBackColor = true;
			// 
			// grpLayoutColors
			// 
			this.grpLayoutColors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpLayoutColors.Controls.Add(this.chkLayoutColorsEnable);
			this.grpLayoutColors.Controls.Add(this.lvwLayoutColors);
			this.grpLayoutColors.Location = new System.Drawing.Point(11, 119);
			this.grpLayoutColors.Name = "grpLayoutColors";
			this.grpLayoutColors.Size = new System.Drawing.Size(494, 243);
			this.grpLayoutColors.TabIndex = 25;
			this.grpLayoutColors.TabStop = false;
			this.grpLayoutColors.Text = "Color Images";
			// 
			// chkLayoutColorsEnable
			// 
			this.chkLayoutColorsEnable.AutoSize = true;
			this.chkLayoutColorsEnable.Location = new System.Drawing.Point(18, 20);
			this.chkLayoutColorsEnable.Name = "chkLayoutColorsEnable";
			this.chkLayoutColorsEnable.Size = new System.Drawing.Size(59, 17);
			this.chkLayoutColorsEnable.TabIndex = 1;
			this.chkLayoutColorsEnable.Text = "Enable";
			this.chkLayoutColorsEnable.UseVisualStyleBackColor = true;
			this.chkLayoutColorsEnable.CheckedChanged += new System.EventHandler(this.chkLayoutColorsEnable_CheckedChanged);
			// 
			// lvwLayoutColors
			// 
			this.lvwLayoutColors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvwLayoutColors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colColor,
            this.colImage});
			this.lvwLayoutColors.DoubleClickDoesCheck = false;
			this.lvwLayoutColors.FullRowSelect = true;
			this.lvwLayoutColors.GridLines = true;
			this.lvwLayoutColors.Location = new System.Drawing.Point(16, 43);
			this.lvwLayoutColors.MultiSelect = false;
			this.lvwLayoutColors.Name = "lvwLayoutColors";
			this.lvwLayoutColors.Size = new System.Drawing.Size(464, 183);
			this.lvwLayoutColors.TabIndex = 0;
			this.lvwLayoutColors.UseCompatibleStateImageBehavior = false;
			this.lvwLayoutColors.View = System.Windows.Forms.View.Details;
			// 
			// colColor
			// 
			this.colColor.Text = "Color";
			this.colColor.Width = 143;
			// 
			// colImage
			// 
			this.colImage.Text = "Image";
			this.colImage.Width = 220;
			// 
			// grpLayoutBackground
			// 
			this.grpLayoutBackground.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.grpLayoutBackground.Controls.Add(this.butLayoutBackground);
			this.grpLayoutBackground.Controls.Add(this.txtLayoutBackground);
			this.grpLayoutBackground.Location = new System.Drawing.Point(265, 6);
			this.grpLayoutBackground.Name = "grpLayoutBackground";
			this.grpLayoutBackground.Size = new System.Drawing.Size(240, 51);
			this.grpLayoutBackground.TabIndex = 24;
			this.grpLayoutBackground.TabStop = false;
			this.grpLayoutBackground.Text = "Background";
			// 
			// butLayoutBackground
			// 
			this.butLayoutBackground.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butLayoutBackground.Location = new System.Drawing.Point(203, 19);
			this.butLayoutBackground.Name = "butLayoutBackground";
			this.butLayoutBackground.Size = new System.Drawing.Size(27, 21);
			this.butLayoutBackground.TabIndex = 1;
			this.butLayoutBackground.Text = "...";
			this.butLayoutBackground.UseVisualStyleBackColor = true;
			this.butLayoutBackground.Click += new System.EventHandler(this.butLayoutBackground_Click);
			// 
			// txtLayoutBackground
			// 
			this.txtLayoutBackground.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtLayoutBackground.Location = new System.Drawing.Point(12, 20);
			this.txtLayoutBackground.Name = "txtLayoutBackground";
			this.txtLayoutBackground.Size = new System.Drawing.Size(185, 20);
			this.txtLayoutBackground.TabIndex = 0;
			// 
			// grpLayoutName
			// 
			this.grpLayoutName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpLayoutName.Controls.Add(this.txtLayoutName);
			this.grpLayoutName.Location = new System.Drawing.Point(11, 6);
			this.grpLayoutName.Name = "grpLayoutName";
			this.grpLayoutName.Size = new System.Drawing.Size(240, 51);
			this.grpLayoutName.TabIndex = 22;
			this.grpLayoutName.TabStop = false;
			this.grpLayoutName.Text = "Name";
			// 
			// txtLayoutName
			// 
			this.txtLayoutName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtLayoutName.Location = new System.Drawing.Point(12, 20);
			this.txtLayoutName.Name = "txtLayoutName";
			this.txtLayoutName.Size = new System.Drawing.Size(218, 20);
			this.txtLayoutName.TabIndex = 0;
			// 
			// tabMAME
			// 
			this.tabMAME.Controls.Add(this.grpMAMEBak);
			this.tabMAME.Controls.Add(this.lblMAMEVersionValue);
			this.tabMAME.Controls.Add(this.grpAutoShow);
			this.tabMAME.Controls.Add(this.lblMAMEVersion);
			this.tabMAME.Controls.Add(this.grpMAMELayoutSub);
			this.tabMAME.Controls.Add(this.grpMAMELayoutOverride);
			this.tabMAME.Controls.Add(this.grpPauseOptions);
			this.tabMAME.Controls.Add(this.grpLayoutMaps);
			this.tabMAME.Controls.Add(this.grpMAMELayout);
			this.tabMAME.Controls.Add(this.grpMAMEOptions);
			this.tabMAME.Location = new System.Drawing.Point(4, 22);
			this.tabMAME.Name = "tabMAME";
			this.tabMAME.Size = new System.Drawing.Size(514, 510);
			this.tabMAME.TabIndex = 9;
			this.tabMAME.Text = "MAME";
			this.tabMAME.UseVisualStyleBackColor = true;
			// 
			// grpMAMEBak
			// 
			this.grpMAMEBak.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpMAMEBak.Controls.Add(this.cboMAMEBak);
			this.grpMAMEBak.Controls.Add(this.lblMAMEBak);
			this.grpMAMEBak.Location = new System.Drawing.Point(11, 301);
			this.grpMAMEBak.Name = "grpMAMEBak";
			this.grpMAMEBak.Size = new System.Drawing.Size(240, 54);
			this.grpMAMEBak.TabIndex = 36;
			this.grpMAMEBak.TabStop = false;
			this.grpMAMEBak.Text = "MAME Background";
			// 
			// cboMAMEBak
			// 
			this.cboMAMEBak.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMAMEBak.FormattingEnabled = true;
			this.cboMAMEBak.Items.AddRange(new object[] {
            "Default",
            "CPanel",
            "Flyer",
            "Marquee",
            "PCB",
            "Snap",
            "Title"});
			this.cboMAMEBak.Location = new System.Drawing.Point(83, 19);
			this.cboMAMEBak.Name = "cboMAMEBak";
			this.cboMAMEBak.Size = new System.Drawing.Size(146, 21);
			this.cboMAMEBak.TabIndex = 36;
			this.toolTip1.SetToolTip(this.cboMAMEBak, "Choose the MAME background");
			// 
			// lblMAMEBak
			// 
			this.lblMAMEBak.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblMAMEBak.AutoSize = true;
			this.lblMAMEBak.Location = new System.Drawing.Point(9, 23);
			this.lblMAMEBak.Name = "lblMAMEBak";
			this.lblMAMEBak.Size = new System.Drawing.Size(68, 13);
			this.lblMAMEBak.TabIndex = 43;
			this.lblMAMEBak.Text = "Background:";
			// 
			// lblMAMEVersionValue
			// 
			this.lblMAMEVersionValue.AutoSize = true;
			this.lblMAMEVersionValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMAMEVersionValue.ForeColor = System.Drawing.Color.Maroon;
			this.lblMAMEVersionValue.Location = new System.Drawing.Point(466, 461);
			this.lblMAMEVersionValue.Name = "lblMAMEVersionValue";
			this.lblMAMEVersionValue.Size = new System.Drawing.Size(14, 13);
			this.lblMAMEVersionValue.TabIndex = 41;
			this.lblMAMEVersionValue.Text = "?";
			// 
			// grpAutoShow
			// 
			this.grpAutoShow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpAutoShow.Controls.Add(this.nudAutoShowTimeout);
			this.grpAutoShow.Controls.Add(this.lblAutoShowTimeout);
			this.grpAutoShow.Controls.Add(this.nudAutoShowDelay);
			this.grpAutoShow.Controls.Add(this.lblAutoShowDelay);
			this.grpAutoShow.Controls.Add(this.chkAutoShow);
			this.grpAutoShow.Location = new System.Drawing.Point(11, 63);
			this.grpAutoShow.Name = "grpAutoShow";
			this.grpAutoShow.Size = new System.Drawing.Size(493, 51);
			this.grpAutoShow.TabIndex = 40;
			this.grpAutoShow.TabStop = false;
			this.grpAutoShow.Text = "Auto Show (On Game Start)";
			// 
			// nudAutoShowTimeout
			// 
			this.nudAutoShowTimeout.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.nudAutoShowTimeout.Location = new System.Drawing.Point(319, 20);
			this.nudAutoShowTimeout.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.nudAutoShowTimeout.Name = "nudAutoShowTimeout";
			this.nudAutoShowTimeout.ReadOnly = true;
			this.nudAutoShowTimeout.Size = new System.Drawing.Size(71, 20);
			this.nudAutoShowTimeout.TabIndex = 15;
			// 
			// lblAutoShowTimeout
			// 
			this.lblAutoShowTimeout.AutoSize = true;
			this.lblAutoShowTimeout.Location = new System.Drawing.Point(243, 23);
			this.lblAutoShowTimeout.Name = "lblAutoShowTimeout";
			this.lblAutoShowTimeout.Size = new System.Drawing.Size(70, 13);
			this.lblAutoShowTimeout.TabIndex = 14;
			this.lblAutoShowTimeout.Text = "Timeout (ms):";
			// 
			// nudAutoShowDelay
			// 
			this.nudAutoShowDelay.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.nudAutoShowDelay.Location = new System.Drawing.Point(166, 19);
			this.nudAutoShowDelay.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.nudAutoShowDelay.Name = "nudAutoShowDelay";
			this.nudAutoShowDelay.ReadOnly = true;
			this.nudAutoShowDelay.Size = new System.Drawing.Size(71, 20);
			this.nudAutoShowDelay.TabIndex = 13;
			// 
			// lblAutoShowDelay
			// 
			this.lblAutoShowDelay.AutoSize = true;
			this.lblAutoShowDelay.Location = new System.Drawing.Point(71, 23);
			this.lblAutoShowDelay.Name = "lblAutoShowDelay";
			this.lblAutoShowDelay.Size = new System.Drawing.Size(89, 13);
			this.lblAutoShowDelay.TabIndex = 12;
			this.lblAutoShowDelay.Text = "Show Delay (ms):";
			// 
			// chkAutoShow
			// 
			this.chkAutoShow.AutoSize = true;
			this.chkAutoShow.Location = new System.Drawing.Point(12, 22);
			this.chkAutoShow.Name = "chkAutoShow";
			this.chkAutoShow.Size = new System.Drawing.Size(59, 17);
			this.chkAutoShow.TabIndex = 0;
			this.chkAutoShow.Text = "Enable";
			this.chkAutoShow.UseVisualStyleBackColor = true;
			// 
			// lblMAMEVersion
			// 
			this.lblMAMEVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblMAMEVersion.AutoSize = true;
			this.lblMAMEVersion.Location = new System.Drawing.Point(386, 461);
			this.lblMAMEVersion.Name = "lblMAMEVersion";
			this.lblMAMEVersion.Size = new System.Drawing.Size(80, 13);
			this.lblMAMEVersion.TabIndex = 40;
			this.lblMAMEVersion.Text = "MAME Version:";
			// 
			// grpMAMELayoutSub
			// 
			this.grpMAMELayoutSub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.grpMAMELayoutSub.Controls.Add(this.butMAMELayoutSub);
			this.grpMAMELayoutSub.Controls.Add(this.txtMAMELayoutSub);
			this.grpMAMELayoutSub.Location = new System.Drawing.Point(344, 6);
			this.grpMAMELayoutSub.Name = "grpMAMELayoutSub";
			this.grpMAMELayoutSub.Size = new System.Drawing.Size(160, 51);
			this.grpMAMELayoutSub.TabIndex = 39;
			this.grpMAMELayoutSub.TabStop = false;
			this.grpMAMELayoutSub.Text = "MAME Layout Sub";
			// 
			// butMAMELayoutSub
			// 
			this.butMAMELayoutSub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butMAMELayoutSub.Location = new System.Drawing.Point(127, 19);
			this.butMAMELayoutSub.Name = "butMAMELayoutSub";
			this.butMAMELayoutSub.Size = new System.Drawing.Size(27, 21);
			this.butMAMELayoutSub.TabIndex = 3;
			this.butMAMELayoutSub.Text = "...";
			this.butMAMELayoutSub.UseVisualStyleBackColor = true;
			this.butMAMELayoutSub.Click += new System.EventHandler(this.butMAMELayoutSub_Click);
			// 
			// txtMAMELayoutSub
			// 
			this.txtMAMELayoutSub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtMAMELayoutSub.Location = new System.Drawing.Point(12, 20);
			this.txtMAMELayoutSub.Name = "txtMAMELayoutSub";
			this.txtMAMELayoutSub.Size = new System.Drawing.Size(109, 20);
			this.txtMAMELayoutSub.TabIndex = 2;
			// 
			// grpMAMELayoutOverride
			// 
			this.grpMAMELayoutOverride.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.grpMAMELayoutOverride.Controls.Add(this.butMAMELayoutOverride);
			this.grpMAMELayoutOverride.Controls.Add(this.txtMAMELayoutOverride);
			this.grpMAMELayoutOverride.Location = new System.Drawing.Point(178, 6);
			this.grpMAMELayoutOverride.Name = "grpMAMELayoutOverride";
			this.grpMAMELayoutOverride.Size = new System.Drawing.Size(160, 51);
			this.grpMAMELayoutOverride.TabIndex = 36;
			this.grpMAMELayoutOverride.TabStop = false;
			this.grpMAMELayoutOverride.Text = "MAME Layout Override";
			// 
			// butMAMELayoutOverride
			// 
			this.butMAMELayoutOverride.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butMAMELayoutOverride.Location = new System.Drawing.Point(127, 20);
			this.butMAMELayoutOverride.Name = "butMAMELayoutOverride";
			this.butMAMELayoutOverride.Size = new System.Drawing.Size(27, 21);
			this.butMAMELayoutOverride.TabIndex = 1;
			this.butMAMELayoutOverride.Text = "...";
			this.butMAMELayoutOverride.UseVisualStyleBackColor = true;
			this.butMAMELayoutOverride.Click += new System.EventHandler(this.butMAMELayoutOverride_Click);
			// 
			// txtMAMELayoutOverride
			// 
			this.txtMAMELayoutOverride.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtMAMELayoutOverride.Location = new System.Drawing.Point(12, 20);
			this.txtMAMELayoutOverride.Name = "txtMAMELayoutOverride";
			this.txtMAMELayoutOverride.Size = new System.Drawing.Size(109, 20);
			this.txtMAMELayoutOverride.TabIndex = 0;
			// 
			// grpPauseOptions
			// 
			this.grpPauseOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.grpPauseOptions.Controls.Add(this.pictureBox1);
			this.grpPauseOptions.Controls.Add(this.chkMAMEDetectPause);
			this.grpPauseOptions.Controls.Add(this.rdoMAMEPauseDiff);
			this.grpPauseOptions.Controls.Add(this.chkMAMESendPause);
			this.grpPauseOptions.Controls.Add(this.rdoMAMEPauseKey);
			this.grpPauseOptions.Controls.Add(this.rdoMAMEPauseMsg);
			this.grpPauseOptions.Location = new System.Drawing.Point(11, 361);
			this.grpPauseOptions.Name = "grpPauseOptions";
			this.grpPauseOptions.Size = new System.Drawing.Size(240, 112);
			this.grpPauseOptions.TabIndex = 38;
			this.grpPauseOptions.TabStop = false;
			this.grpPauseOptions.Text = "Pause Options";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(199, 19);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(27, 19);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 42;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
			// 
			// chkMAMEDetectPause
			// 
			this.chkMAMEDetectPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkMAMEDetectPause.AutoSize = true;
			this.chkMAMEDetectPause.Location = new System.Drawing.Point(13, 89);
			this.chkMAMEDetectPause.Name = "chkMAMEDetectPause";
			this.chkMAMEDetectPause.Size = new System.Drawing.Size(91, 17);
			this.chkMAMEDetectPause.TabIndex = 39;
			this.chkMAMEDetectPause.Text = "Detect Pause";
			this.chkMAMEDetectPause.UseVisualStyleBackColor = true;
			// 
			// rdoMAMEPauseDiff
			// 
			this.rdoMAMEPauseDiff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.rdoMAMEPauseDiff.AutoSize = true;
			this.rdoMAMEPauseDiff.Location = new System.Drawing.Point(13, 53);
			this.rdoMAMEPauseDiff.Name = "rdoMAMEPauseDiff";
			this.rdoMAMEPauseDiff.Size = new System.Drawing.Size(111, 17);
			this.rdoMAMEPauseDiff.TabIndex = 38;
			this.rdoMAMEPauseDiff.TabStop = true;
			this.rdoMAMEPauseDiff.Text = "Diff Patch Method";
			this.rdoMAMEPauseDiff.UseVisualStyleBackColor = true;
			// 
			// chkMAMESendPause
			// 
			this.chkMAMESendPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkMAMESendPause.AutoSize = true;
			this.chkMAMESendPause.Location = new System.Drawing.Point(13, 19);
			this.chkMAMESendPause.Name = "chkMAMESendPause";
			this.chkMAMESendPause.Size = new System.Drawing.Size(87, 17);
			this.chkMAMESendPause.TabIndex = 34;
			this.chkMAMESendPause.Text = "Send Pause:";
			this.chkMAMESendPause.UseVisualStyleBackColor = true;
			// 
			// rdoMAMEPauseKey
			// 
			this.rdoMAMEPauseKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.rdoMAMEPauseKey.AutoSize = true;
			this.rdoMAMEPauseKey.Location = new System.Drawing.Point(13, 69);
			this.rdoMAMEPauseKey.Name = "rdoMAMEPauseKey";
			this.rdoMAMEPauseKey.Size = new System.Drawing.Size(76, 17);
			this.rdoMAMEPauseKey.TabIndex = 36;
			this.rdoMAMEPauseKey.TabStop = true;
			this.rdoMAMEPauseKey.Text = "Send Keys";
			this.rdoMAMEPauseKey.UseVisualStyleBackColor = true;
			// 
			// rdoMAMEPauseMsg
			// 
			this.rdoMAMEPauseMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.rdoMAMEPauseMsg.AutoSize = true;
			this.rdoMAMEPauseMsg.Location = new System.Drawing.Point(13, 37);
			this.rdoMAMEPauseMsg.Name = "rdoMAMEPauseMsg";
			this.rdoMAMEPauseMsg.Size = new System.Drawing.Size(105, 17);
			this.rdoMAMEPauseMsg.TabIndex = 37;
			this.rdoMAMEPauseMsg.TabStop = true;
			this.rdoMAMEPauseMsg.Text = "Message System";
			this.rdoMAMEPauseMsg.UseVisualStyleBackColor = true;
			// 
			// grpLayoutMaps
			// 
			this.grpLayoutMaps.Controls.Add(this.butLayoutMapDown);
			this.grpLayoutMaps.Controls.Add(this.butLayoutMapUp);
			this.grpLayoutMaps.Controls.Add(this.butLayoutMapDelete);
			this.grpLayoutMaps.Controls.Add(this.butLayoutMapNew);
			this.grpLayoutMaps.Controls.Add(this.lvwLayoutMaps);
			this.grpLayoutMaps.Location = new System.Drawing.Point(11, 122);
			this.grpLayoutMaps.Name = "grpLayoutMaps";
			this.grpLayoutMaps.Size = new System.Drawing.Size(494, 172);
			this.grpLayoutMaps.TabIndex = 37;
			this.grpLayoutMaps.TabStop = false;
			this.grpLayoutMaps.Text = "Layout Maps";
			// 
			// butLayoutMapDown
			// 
			this.butLayoutMapDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butLayoutMapDown.Location = new System.Drawing.Point(414, 137);
			this.butLayoutMapDown.Name = "butLayoutMapDown";
			this.butLayoutMapDown.Size = new System.Drawing.Size(66, 26);
			this.butLayoutMapDown.TabIndex = 18;
			this.butLayoutMapDown.Text = "Down";
			this.butLayoutMapDown.UseVisualStyleBackColor = true;
			this.butLayoutMapDown.Click += new System.EventHandler(this.butLayoutMapDown_Click);
			// 
			// butLayoutMapUp
			// 
			this.butLayoutMapUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butLayoutMapUp.Location = new System.Drawing.Point(342, 137);
			this.butLayoutMapUp.Name = "butLayoutMapUp";
			this.butLayoutMapUp.Size = new System.Drawing.Size(66, 26);
			this.butLayoutMapUp.TabIndex = 17;
			this.butLayoutMapUp.Text = "Up";
			this.butLayoutMapUp.UseVisualStyleBackColor = true;
			this.butLayoutMapUp.Click += new System.EventHandler(this.butLayoutMapUp_Click);
			// 
			// butLayoutMapDelete
			// 
			this.butLayoutMapDelete.Location = new System.Drawing.Point(86, 137);
			this.butLayoutMapDelete.Name = "butLayoutMapDelete";
			this.butLayoutMapDelete.Size = new System.Drawing.Size(66, 26);
			this.butLayoutMapDelete.TabIndex = 16;
			this.butLayoutMapDelete.Text = "Delete";
			this.butLayoutMapDelete.UseVisualStyleBackColor = true;
			this.butLayoutMapDelete.Click += new System.EventHandler(this.butLayoutMapDelete_Click);
			// 
			// butLayoutMapNew
			// 
			this.butLayoutMapNew.Location = new System.Drawing.Point(14, 137);
			this.butLayoutMapNew.Name = "butLayoutMapNew";
			this.butLayoutMapNew.Size = new System.Drawing.Size(66, 26);
			this.butLayoutMapNew.TabIndex = 15;
			this.butLayoutMapNew.Text = "New";
			this.butLayoutMapNew.UseVisualStyleBackColor = true;
			this.butLayoutMapNew.Click += new System.EventHandler(this.butLayoutMapNew_Click);
			// 
			// lvwLayoutMaps
			// 
			this.lvwLayoutMaps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvwLayoutMaps.CheckBoxes = true;
			this.lvwLayoutMaps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colLayoutMapEnabled,
            this.colLayoutMapConstant,
            this.colLayoutMapControl,
            this.colLayoutMapNumPlayers,
            this.colLayoutMapAlternating,
            this.colLayoutMapLayout});
			this.lvwLayoutMaps.DoubleClickDoesCheck = false;
			this.lvwLayoutMaps.FullRowSelect = true;
			this.lvwLayoutMaps.GridLines = true;
			this.lvwLayoutMaps.Location = new System.Drawing.Point(12, 19);
			this.lvwLayoutMaps.MultiSelect = false;
			this.lvwLayoutMaps.Name = "lvwLayoutMaps";
			this.lvwLayoutMaps.Size = new System.Drawing.Size(468, 112);
			this.lvwLayoutMaps.TabIndex = 0;
			this.lvwLayoutMaps.UseCompatibleStateImageBehavior = false;
			this.lvwLayoutMaps.View = System.Windows.Forms.View.Details;
			// 
			// colLayoutMapEnabled
			// 
			this.colLayoutMapEnabled.Text = "";
			this.colLayoutMapEnabled.Width = 32;
			// 
			// colLayoutMapConstant
			// 
			this.colLayoutMapConstant.Text = "Constant";
			this.colLayoutMapConstant.Width = 100;
			// 
			// colLayoutMapControl
			// 
			this.colLayoutMapControl.Text = "Control";
			this.colLayoutMapControl.Width = 100;
			// 
			// colLayoutMapNumPlayers
			// 
			this.colLayoutMapNumPlayers.Text = "NumPlayers";
			this.colLayoutMapNumPlayers.Width = 100;
			// 
			// colLayoutMapAlternating
			// 
			this.colLayoutMapAlternating.Text = "Alternating";
			this.colLayoutMapAlternating.Width = 100;
			// 
			// colLayoutMapLayout
			// 
			this.colLayoutMapLayout.Text = "Layout";
			this.colLayoutMapLayout.Width = 100;
			// 
			// grpMAMELayout
			// 
			this.grpMAMELayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpMAMELayout.Controls.Add(this.butMAMELayout);
			this.grpMAMELayout.Controls.Add(this.txtMAMELayout);
			this.grpMAMELayout.Location = new System.Drawing.Point(11, 6);
			this.grpMAMELayout.Name = "grpMAMELayout";
			this.grpMAMELayout.Size = new System.Drawing.Size(160, 51);
			this.grpMAMELayout.TabIndex = 33;
			this.grpMAMELayout.TabStop = false;
			this.grpMAMELayout.Text = "MAME Layout";
			// 
			// butMAMELayout
			// 
			this.butMAMELayout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butMAMELayout.Location = new System.Drawing.Point(127, 19);
			this.butMAMELayout.Name = "butMAMELayout";
			this.butMAMELayout.Size = new System.Drawing.Size(27, 21);
			this.butMAMELayout.TabIndex = 1;
			this.butMAMELayout.Text = "...";
			this.butMAMELayout.UseVisualStyleBackColor = true;
			this.butMAMELayout.Click += new System.EventHandler(this.butMAMELayout_Click);
			// 
			// txtMAMELayout
			// 
			this.txtMAMELayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtMAMELayout.Location = new System.Drawing.Point(12, 19);
			this.txtMAMELayout.Name = "txtMAMELayout";
			this.txtMAMELayout.Size = new System.Drawing.Size(109, 20);
			this.txtMAMELayout.TabIndex = 0;
			// 
			// grpMAMEOptions
			// 
			this.grpMAMEOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpMAMEOptions.Controls.Add(this.chkMAMESkipDisclaimer);
			this.grpMAMEOptions.Controls.Add(this.chkMAMEScreenshot);
			this.grpMAMEOptions.Controls.Add(this.chkMAMEUseShowKey);
			this.grpMAMEOptions.Controls.Add(this.chkMAMEOutputSystem);
			this.grpMAMEOptions.Location = new System.Drawing.Point(264, 301);
			this.grpMAMEOptions.Name = "grpMAMEOptions";
			this.grpMAMEOptions.Size = new System.Drawing.Size(240, 98);
			this.grpMAMEOptions.TabIndex = 32;
			this.grpMAMEOptions.TabStop = false;
			this.grpMAMEOptions.Text = "MAME Options";
			// 
			// chkMAMESkipDisclaimer
			// 
			this.chkMAMESkipDisclaimer.AutoSize = true;
			this.chkMAMESkipDisclaimer.Location = new System.Drawing.Point(13, 69);
			this.chkMAMESkipDisclaimer.Name = "chkMAMESkipDisclaimer";
			this.chkMAMESkipDisclaimer.Size = new System.Drawing.Size(98, 17);
			this.chkMAMESkipDisclaimer.TabIndex = 1;
			this.chkMAMESkipDisclaimer.Text = "Skip Disclaimer";
			this.chkMAMESkipDisclaimer.UseVisualStyleBackColor = true;
			// 
			// chkMAMEScreenshot
			// 
			this.chkMAMEScreenshot.AutoSize = true;
			this.chkMAMEScreenshot.Location = new System.Drawing.Point(13, 53);
			this.chkMAMEScreenshot.Name = "chkMAMEScreenshot";
			this.chkMAMEScreenshot.Size = new System.Drawing.Size(80, 17);
			this.chkMAMEScreenshot.TabIndex = 31;
			this.chkMAMEScreenshot.Text = "Screenshot";
			this.chkMAMEScreenshot.UseVisualStyleBackColor = true;
			// 
			// chkMAMEUseShowKey
			// 
			this.chkMAMEUseShowKey.AutoSize = true;
			this.chkMAMEUseShowKey.Location = new System.Drawing.Point(13, 37);
			this.chkMAMEUseShowKey.Name = "chkMAMEUseShowKey";
			this.chkMAMEUseShowKey.Size = new System.Drawing.Size(96, 17);
			this.chkMAMEUseShowKey.TabIndex = 35;
			this.chkMAMEUseShowKey.Text = "Use Show Key";
			this.chkMAMEUseShowKey.UseVisualStyleBackColor = true;
			// 
			// chkMAMEOutputSystem
			// 
			this.chkMAMEOutputSystem.AutoSize = true;
			this.chkMAMEOutputSystem.Location = new System.Drawing.Point(13, 21);
			this.chkMAMEOutputSystem.Name = "chkMAMEOutputSystem";
			this.chkMAMEOutputSystem.Size = new System.Drawing.Size(130, 17);
			this.chkMAMEOutputSystem.TabIndex = 29;
			this.chkMAMEOutputSystem.Text = "MAME Output System";
			this.chkMAMEOutputSystem.UseVisualStyleBackColor = true;
			// 
			// tabMAMEFilters
			// 
			this.tabMAMEFilters.Controls.Add(this.grpMAMEFilters);
			this.tabMAMEFilters.Location = new System.Drawing.Point(4, 22);
			this.tabMAMEFilters.Name = "tabMAMEFilters";
			this.tabMAMEFilters.Size = new System.Drawing.Size(514, 510);
			this.tabMAMEFilters.TabIndex = 11;
			this.tabMAMEFilters.Text = "MAME Filters";
			this.tabMAMEFilters.UseVisualStyleBackColor = true;
			// 
			// grpMAMEFilters
			// 
			this.grpMAMEFilters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpMAMEFilters.Controls.Add(this.lblFilterRotation);
			this.grpMAMEFilters.Controls.Add(this.cboFilterRotation);
			this.grpMAMEFilters.Controls.Add(this.chkNoImperfect);
			this.grpMAMEFilters.Controls.Add(this.chkNoPreliminary);
			this.grpMAMEFilters.Controls.Add(this.chkNoSystemExceptChd);
			this.grpMAMEFilters.Controls.Add(this.chkArcadeOnly);
			this.grpMAMEFilters.Controls.Add(this.chkRunnableOnly);
			this.grpMAMEFilters.Controls.Add(this.chkNoNotClassified);
			this.grpMAMEFilters.Controls.Add(this.chkNoUtilities);
			this.grpMAMEFilters.Controls.Add(this.chkNoReels);
			this.grpMAMEFilters.Controls.Add(this.chkNoMechanical);
			this.grpMAMEFilters.Controls.Add(this.chkNoCasino);
			this.grpMAMEFilters.Controls.Add(this.chkNoGambling);
			this.grpMAMEFilters.Controls.Add(this.chkNoMahjong);
			this.grpMAMEFilters.Controls.Add(this.chkNoBios);
			this.grpMAMEFilters.Controls.Add(this.chkNoAdult);
			this.grpMAMEFilters.Controls.Add(this.chkNoClones);
			this.grpMAMEFilters.Controls.Add(this.chkNoDevice);
			this.grpMAMEFilters.Controls.Add(this.lblDescriptionExcludes);
			this.grpMAMEFilters.Controls.Add(this.lblNameIncludes);
			this.grpMAMEFilters.Controls.Add(this.txtDescriptionExcludes);
			this.grpMAMEFilters.Controls.Add(this.txtNameIncludes);
			this.grpMAMEFilters.Location = new System.Drawing.Point(11, 6);
			this.grpMAMEFilters.Name = "grpMAMEFilters";
			this.grpMAMEFilters.Size = new System.Drawing.Size(494, 238);
			this.grpMAMEFilters.TabIndex = 35;
			this.grpMAMEFilters.TabStop = false;
			this.grpMAMEFilters.Text = "MAME Filters";
			// 
			// lblFilterRotation
			// 
			this.lblFilterRotation.AutoSize = true;
			this.lblFilterRotation.Location = new System.Drawing.Point(7, 158);
			this.lblFilterRotation.Name = "lblFilterRotation";
			this.lblFilterRotation.Size = new System.Drawing.Size(75, 13);
			this.lblFilterRotation.TabIndex = 68;
			this.lblFilterRotation.Text = "Filter Rotation:";
			// 
			// cboFilterRotation
			// 
			this.cboFilterRotation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboFilterRotation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboFilterRotation.FormattingEnabled = true;
			this.cboFilterRotation.Location = new System.Drawing.Point(121, 155);
			this.cboFilterRotation.Name = "cboFilterRotation";
			this.cboFilterRotation.Size = new System.Drawing.Size(358, 21);
			this.cboFilterRotation.TabIndex = 67;
			// 
			// chkNoImperfect
			// 
			this.chkNoImperfect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkNoImperfect.AutoSize = true;
			this.chkNoImperfect.Location = new System.Drawing.Point(260, 131);
			this.chkNoImperfect.Name = "chkNoImperfect";
			this.chkNoImperfect.Size = new System.Drawing.Size(87, 17);
			this.chkNoImperfect.TabIndex = 66;
			this.chkNoImperfect.Text = "No Imperfect";
			this.chkNoImperfect.UseVisualStyleBackColor = true;
			// 
			// chkNoPreliminary
			// 
			this.chkNoPreliminary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkNoPreliminary.AutoSize = true;
			this.chkNoPreliminary.Location = new System.Drawing.Point(260, 115);
			this.chkNoPreliminary.Name = "chkNoPreliminary";
			this.chkNoPreliminary.Size = new System.Drawing.Size(93, 17);
			this.chkNoPreliminary.TabIndex = 65;
			this.chkNoPreliminary.Text = "No Preliminary";
			this.chkNoPreliminary.UseVisualStyleBackColor = true;
			// 
			// chkNoSystemExceptChd
			// 
			this.chkNoSystemExceptChd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkNoSystemExceptChd.AutoSize = true;
			this.chkNoSystemExceptChd.Location = new System.Drawing.Point(260, 99);
			this.chkNoSystemExceptChd.Name = "chkNoSystemExceptChd";
			this.chkNoSystemExceptChd.Size = new System.Drawing.Size(135, 17);
			this.chkNoSystemExceptChd.TabIndex = 64;
			this.chkNoSystemExceptChd.Text = "No System Except Chd";
			this.chkNoSystemExceptChd.UseVisualStyleBackColor = true;
			// 
			// chkArcadeOnly
			// 
			this.chkArcadeOnly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkArcadeOnly.AutoSize = true;
			this.chkArcadeOnly.Location = new System.Drawing.Point(260, 83);
			this.chkArcadeOnly.Name = "chkArcadeOnly";
			this.chkArcadeOnly.Size = new System.Drawing.Size(84, 17);
			this.chkArcadeOnly.TabIndex = 63;
			this.chkArcadeOnly.Text = "Arcade Only";
			this.chkArcadeOnly.UseVisualStyleBackColor = true;
			// 
			// chkRunnableOnly
			// 
			this.chkRunnableOnly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkRunnableOnly.AutoSize = true;
			this.chkRunnableOnly.Location = new System.Drawing.Point(260, 67);
			this.chkRunnableOnly.Name = "chkRunnableOnly";
			this.chkRunnableOnly.Size = new System.Drawing.Size(96, 17);
			this.chkRunnableOnly.TabIndex = 62;
			this.chkRunnableOnly.Text = "Runnable Only";
			this.chkRunnableOnly.UseVisualStyleBackColor = true;
			// 
			// chkNoNotClassified
			// 
			this.chkNoNotClassified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkNoNotClassified.AutoSize = true;
			this.chkNoNotClassified.Location = new System.Drawing.Point(260, 51);
			this.chkNoNotClassified.Name = "chkNoNotClassified";
			this.chkNoNotClassified.Size = new System.Drawing.Size(107, 17);
			this.chkNoNotClassified.TabIndex = 61;
			this.chkNoNotClassified.Text = "No Not Classified";
			this.chkNoNotClassified.UseVisualStyleBackColor = true;
			// 
			// chkNoUtilities
			// 
			this.chkNoUtilities.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkNoUtilities.AutoSize = true;
			this.chkNoUtilities.Location = new System.Drawing.Point(260, 35);
			this.chkNoUtilities.Name = "chkNoUtilities";
			this.chkNoUtilities.Size = new System.Drawing.Size(76, 17);
			this.chkNoUtilities.TabIndex = 60;
			this.chkNoUtilities.Text = "No Utilities";
			this.chkNoUtilities.UseVisualStyleBackColor = true;
			// 
			// chkNoReels
			// 
			this.chkNoReels.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkNoReels.AutoSize = true;
			this.chkNoReels.Location = new System.Drawing.Point(260, 19);
			this.chkNoReels.Name = "chkNoReels";
			this.chkNoReels.Size = new System.Drawing.Size(70, 17);
			this.chkNoReels.TabIndex = 59;
			this.chkNoReels.Text = "No Reels";
			this.chkNoReels.UseVisualStyleBackColor = true;
			// 
			// chkNoMechanical
			// 
			this.chkNoMechanical.AutoSize = true;
			this.chkNoMechanical.Location = new System.Drawing.Point(10, 131);
			this.chkNoMechanical.Name = "chkNoMechanical";
			this.chkNoMechanical.Size = new System.Drawing.Size(98, 17);
			this.chkNoMechanical.TabIndex = 58;
			this.chkNoMechanical.Text = "No Mechanical";
			this.chkNoMechanical.UseVisualStyleBackColor = true;
			// 
			// chkNoCasino
			// 
			this.chkNoCasino.AutoSize = true;
			this.chkNoCasino.Location = new System.Drawing.Point(10, 115);
			this.chkNoCasino.Name = "chkNoCasino";
			this.chkNoCasino.Size = new System.Drawing.Size(75, 17);
			this.chkNoCasino.TabIndex = 57;
			this.chkNoCasino.Text = "No Casino";
			this.chkNoCasino.UseVisualStyleBackColor = true;
			// 
			// chkNoGambling
			// 
			this.chkNoGambling.AutoSize = true;
			this.chkNoGambling.Location = new System.Drawing.Point(10, 99);
			this.chkNoGambling.Name = "chkNoGambling";
			this.chkNoGambling.Size = new System.Drawing.Size(87, 17);
			this.chkNoGambling.TabIndex = 56;
			this.chkNoGambling.Text = "No Gambling";
			this.chkNoGambling.UseVisualStyleBackColor = true;
			// 
			// chkNoMahjong
			// 
			this.chkNoMahjong.AutoSize = true;
			this.chkNoMahjong.Location = new System.Drawing.Point(10, 83);
			this.chkNoMahjong.Name = "chkNoMahjong";
			this.chkNoMahjong.Size = new System.Drawing.Size(84, 17);
			this.chkNoMahjong.TabIndex = 55;
			this.chkNoMahjong.Text = "No Mahjong";
			this.chkNoMahjong.UseVisualStyleBackColor = true;
			// 
			// chkNoBios
			// 
			this.chkNoBios.AutoSize = true;
			this.chkNoBios.Location = new System.Drawing.Point(10, 35);
			this.chkNoBios.Name = "chkNoBios";
			this.chkNoBios.Size = new System.Drawing.Size(63, 17);
			this.chkNoBios.TabIndex = 54;
			this.chkNoBios.Text = "No Bios";
			this.chkNoBios.UseVisualStyleBackColor = true;
			// 
			// chkNoAdult
			// 
			this.chkNoAdult.AutoSize = true;
			this.chkNoAdult.Location = new System.Drawing.Point(10, 67);
			this.chkNoAdult.Name = "chkNoAdult";
			this.chkNoAdult.Size = new System.Drawing.Size(67, 17);
			this.chkNoAdult.TabIndex = 53;
			this.chkNoAdult.Text = "No Adult";
			this.chkNoAdult.UseVisualStyleBackColor = true;
			// 
			// chkNoClones
			// 
			this.chkNoClones.AutoSize = true;
			this.chkNoClones.Location = new System.Drawing.Point(10, 19);
			this.chkNoClones.Name = "chkNoClones";
			this.chkNoClones.Size = new System.Drawing.Size(75, 17);
			this.chkNoClones.TabIndex = 52;
			this.chkNoClones.Text = "No Clones";
			this.chkNoClones.UseVisualStyleBackColor = true;
			// 
			// chkNoDevice
			// 
			this.chkNoDevice.AutoSize = true;
			this.chkNoDevice.Location = new System.Drawing.Point(10, 51);
			this.chkNoDevice.Name = "chkNoDevice";
			this.chkNoDevice.Size = new System.Drawing.Size(77, 17);
			this.chkNoDevice.TabIndex = 48;
			this.chkNoDevice.Text = "No Device";
			this.chkNoDevice.UseVisualStyleBackColor = true;
			// 
			// lblDescriptionExcludes
			// 
			this.lblDescriptionExcludes.AutoSize = true;
			this.lblDescriptionExcludes.Location = new System.Drawing.Point(6, 209);
			this.lblDescriptionExcludes.Name = "lblDescriptionExcludes";
			this.lblDescriptionExcludes.Size = new System.Drawing.Size(109, 13);
			this.lblDescriptionExcludes.TabIndex = 32;
			this.lblDescriptionExcludes.Text = "Description Excludes:";
			// 
			// lblNameIncludes
			// 
			this.lblNameIncludes.AutoSize = true;
			this.lblNameIncludes.Location = new System.Drawing.Point(6, 184);
			this.lblNameIncludes.Name = "lblNameIncludes";
			this.lblNameIncludes.Size = new System.Drawing.Size(81, 13);
			this.lblNameIncludes.TabIndex = 31;
			this.lblNameIncludes.Text = "Name Includes:";
			// 
			// txtDescriptionExcludes
			// 
			this.txtDescriptionExcludes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtDescriptionExcludes.Location = new System.Drawing.Point(121, 206);
			this.txtDescriptionExcludes.Name = "txtDescriptionExcludes";
			this.txtDescriptionExcludes.Size = new System.Drawing.Size(358, 20);
			this.txtDescriptionExcludes.TabIndex = 0;
			// 
			// txtNameIncludes
			// 
			this.txtNameIncludes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtNameIncludes.Location = new System.Drawing.Point(121, 181);
			this.txtNameIncludes.Name = "txtNameIncludes";
			this.txtNameIncludes.Size = new System.Drawing.Size(358, 20);
			this.txtNameIncludes.TabIndex = 0;
			// 
			// tabMAMEPaths
			// 
			this.tabMAMEPaths.Controls.Add(this.grpMAMEPaths);
			this.tabMAMEPaths.Location = new System.Drawing.Point(4, 22);
			this.tabMAMEPaths.Name = "tabMAMEPaths";
			this.tabMAMEPaths.Padding = new System.Windows.Forms.Padding(3);
			this.tabMAMEPaths.Size = new System.Drawing.Size(514, 510);
			this.tabMAMEPaths.TabIndex = 1;
			this.tabMAMEPaths.Text = "MAME Paths";
			this.tabMAMEPaths.UseVisualStyleBackColor = true;
			// 
			// grpMAMEPaths
			// 
			this.grpMAMEPaths.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpMAMEPaths.Controls.Add(this.lblNvRam);
			this.grpMAMEPaths.Controls.Add(this.butNvRam);
			this.grpMAMEPaths.Controls.Add(this.txtNvRam);
			this.grpMAMEPaths.Controls.Add(this.lblHi);
			this.grpMAMEPaths.Controls.Add(this.butHi);
			this.grpMAMEPaths.Controls.Add(this.txtHi);
			this.grpMAMEPaths.Controls.Add(this.butSelect);
			this.grpMAMEPaths.Controls.Add(this.lblSelect);
			this.grpMAMEPaths.Controls.Add(this.txtSelect);
			this.grpMAMEPaths.Controls.Add(this.butPreviews);
			this.grpMAMEPaths.Controls.Add(this.lblPreviews);
			this.grpMAMEPaths.Controls.Add(this.txtPreviews);
			this.grpMAMEPaths.Controls.Add(this.lblPCB);
			this.grpMAMEPaths.Controls.Add(this.butPCB);
			this.grpMAMEPaths.Controls.Add(this.txtPCB);
			this.grpMAMEPaths.Controls.Add(this.lblCfg);
			this.grpMAMEPaths.Controls.Add(this.butCfg);
			this.grpMAMEPaths.Controls.Add(this.txtCfg);
			this.grpMAMEPaths.Controls.Add(this.lblTitles);
			this.grpMAMEPaths.Controls.Add(this.butTitles);
			this.grpMAMEPaths.Controls.Add(this.txtTitles);
			this.grpMAMEPaths.Controls.Add(this.lblSnap);
			this.grpMAMEPaths.Controls.Add(this.butSnap);
			this.grpMAMEPaths.Controls.Add(this.txtSnap);
			this.grpMAMEPaths.Controls.Add(this.lblMarquees);
			this.grpMAMEPaths.Controls.Add(this.butMarquees);
			this.grpMAMEPaths.Controls.Add(this.txtMarquees);
			this.grpMAMEPaths.Controls.Add(this.lblManuals);
			this.grpMAMEPaths.Controls.Add(this.butManuals);
			this.grpMAMEPaths.Controls.Add(this.txtManuals);
			this.grpMAMEPaths.Controls.Add(this.lblIni);
			this.grpMAMEPaths.Controls.Add(this.butIni);
			this.grpMAMEPaths.Controls.Add(this.txtIni);
			this.grpMAMEPaths.Controls.Add(this.lblIcons);
			this.grpMAMEPaths.Controls.Add(this.butIcons);
			this.grpMAMEPaths.Controls.Add(this.txtIcons);
			this.grpMAMEPaths.Controls.Add(this.lblFlyers);
			this.grpMAMEPaths.Controls.Add(this.butFlyers);
			this.grpMAMEPaths.Controls.Add(this.txtFlyers);
			this.grpMAMEPaths.Controls.Add(this.lblCtrlr);
			this.grpMAMEPaths.Controls.Add(this.butCtrlr);
			this.grpMAMEPaths.Controls.Add(this.txtCtrlr);
			this.grpMAMEPaths.Controls.Add(this.lblCPanel);
			this.grpMAMEPaths.Controls.Add(this.butCPanel);
			this.grpMAMEPaths.Controls.Add(this.txtCPanel);
			this.grpMAMEPaths.Controls.Add(this.lblCabinets);
			this.grpMAMEPaths.Controls.Add(this.butCabinets);
			this.grpMAMEPaths.Controls.Add(this.txtCabinets);
			this.grpMAMEPaths.Controls.Add(this.lblMAMEExe);
			this.grpMAMEPaths.Controls.Add(this.butMAMEExe);
			this.grpMAMEPaths.Controls.Add(this.txtMAMEExe);
			this.grpMAMEPaths.Location = new System.Drawing.Point(11, 6);
			this.grpMAMEPaths.Name = "grpMAMEPaths";
			this.grpMAMEPaths.Size = new System.Drawing.Size(491, 387);
			this.grpMAMEPaths.TabIndex = 0;
			this.grpMAMEPaths.TabStop = false;
			this.grpMAMEPaths.Text = "MAME Paths";
			// 
			// lblNvRam
			// 
			this.lblNvRam.AutoSize = true;
			this.lblNvRam.Location = new System.Drawing.Point(10, 253);
			this.lblNvRam.Name = "lblNvRam";
			this.lblNvRam.Size = new System.Drawing.Size(46, 13);
			this.lblNvRam.TabIndex = 49;
			this.lblNvRam.Text = "NvRam:";
			// 
			// butNvRam
			// 
			this.butNvRam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butNvRam.Location = new System.Drawing.Point(450, 249);
			this.butNvRam.Name = "butNvRam";
			this.butNvRam.Size = new System.Drawing.Size(27, 21);
			this.butNvRam.TabIndex = 48;
			this.butNvRam.Text = "...";
			this.butNvRam.UseVisualStyleBackColor = true;
			this.butNvRam.Click += new System.EventHandler(this.butMAMEPaths_Click);
			// 
			// txtNvRam
			// 
			this.txtNvRam.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtNvRam.Location = new System.Drawing.Point(73, 250);
			this.txtNvRam.Name = "txtNvRam";
			this.txtNvRam.Size = new System.Drawing.Size(371, 20);
			this.txtNvRam.TabIndex = 47;
			// 
			// lblHi
			// 
			this.lblHi.AutoSize = true;
			this.lblHi.Location = new System.Drawing.Point(10, 148);
			this.lblHi.Name = "lblHi";
			this.lblHi.Size = new System.Drawing.Size(20, 13);
			this.lblHi.TabIndex = 46;
			this.lblHi.Text = "Hi:";
			// 
			// butHi
			// 
			this.butHi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butHi.Location = new System.Drawing.Point(450, 144);
			this.butHi.Name = "butHi";
			this.butHi.Size = new System.Drawing.Size(27, 21);
			this.butHi.TabIndex = 45;
			this.butHi.Text = "...";
			this.butHi.UseVisualStyleBackColor = true;
			this.butHi.Click += new System.EventHandler(this.butMAMEPaths_Click);
			// 
			// txtHi
			// 
			this.txtHi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtHi.Location = new System.Drawing.Point(73, 145);
			this.txtHi.Name = "txtHi";
			this.txtHi.Size = new System.Drawing.Size(371, 20);
			this.txtHi.TabIndex = 44;
			// 
			// butSelect
			// 
			this.butSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butSelect.Location = new System.Drawing.Point(450, 312);
			this.butSelect.Name = "butSelect";
			this.butSelect.Size = new System.Drawing.Size(27, 21);
			this.butSelect.TabIndex = 40;
			this.butSelect.Text = "...";
			this.butSelect.UseVisualStyleBackColor = true;
			this.butSelect.Click += new System.EventHandler(this.butMAMEPaths_Click);
			// 
			// lblSelect
			// 
			this.lblSelect.AutoSize = true;
			this.lblSelect.Location = new System.Drawing.Point(10, 316);
			this.lblSelect.Name = "lblSelect";
			this.lblSelect.Size = new System.Drawing.Size(40, 13);
			this.lblSelect.TabIndex = 43;
			this.lblSelect.Text = "Select:";
			// 
			// txtSelect
			// 
			this.txtSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSelect.Location = new System.Drawing.Point(73, 313);
			this.txtSelect.Name = "txtSelect";
			this.txtSelect.Size = new System.Drawing.Size(371, 20);
			this.txtSelect.TabIndex = 42;
			// 
			// butPreviews
			// 
			this.butPreviews.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butPreviews.Location = new System.Drawing.Point(450, 291);
			this.butPreviews.Name = "butPreviews";
			this.butPreviews.Size = new System.Drawing.Size(27, 21);
			this.butPreviews.TabIndex = 39;
			this.butPreviews.Text = "...";
			this.butPreviews.UseVisualStyleBackColor = true;
			this.butPreviews.Click += new System.EventHandler(this.butMAMEPaths_Click);
			// 
			// lblPreviews
			// 
			this.lblPreviews.AutoSize = true;
			this.lblPreviews.Location = new System.Drawing.Point(10, 295);
			this.lblPreviews.Name = "lblPreviews";
			this.lblPreviews.Size = new System.Drawing.Size(53, 13);
			this.lblPreviews.TabIndex = 41;
			this.lblPreviews.Text = "Previews:";
			// 
			// txtPreviews
			// 
			this.txtPreviews.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPreviews.Location = new System.Drawing.Point(73, 292);
			this.txtPreviews.Name = "txtPreviews";
			this.txtPreviews.Size = new System.Drawing.Size(371, 20);
			this.txtPreviews.TabIndex = 40;
			// 
			// lblPCB
			// 
			this.lblPCB.AutoSize = true;
			this.lblPCB.Location = new System.Drawing.Point(10, 274);
			this.lblPCB.Name = "lblPCB";
			this.lblPCB.Size = new System.Drawing.Size(31, 13);
			this.lblPCB.TabIndex = 39;
			this.lblPCB.Text = "PCB:";
			// 
			// butPCB
			// 
			this.butPCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butPCB.Location = new System.Drawing.Point(450, 270);
			this.butPCB.Name = "butPCB";
			this.butPCB.Size = new System.Drawing.Size(27, 21);
			this.butPCB.TabIndex = 38;
			this.butPCB.Text = "...";
			this.butPCB.UseVisualStyleBackColor = true;
			this.butPCB.Click += new System.EventHandler(this.butMAMEPaths_Click);
			// 
			// txtPCB
			// 
			this.txtPCB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPCB.Location = new System.Drawing.Point(73, 271);
			this.txtPCB.Name = "txtPCB";
			this.txtPCB.Size = new System.Drawing.Size(371, 20);
			this.txtPCB.TabIndex = 37;
			// 
			// lblCfg
			// 
			this.lblCfg.AutoSize = true;
			this.lblCfg.Location = new System.Drawing.Point(10, 64);
			this.lblCfg.Name = "lblCfg";
			this.lblCfg.Size = new System.Drawing.Size(26, 13);
			this.lblCfg.TabIndex = 36;
			this.lblCfg.Text = "Cfg:";
			// 
			// butCfg
			// 
			this.butCfg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butCfg.Location = new System.Drawing.Point(450, 60);
			this.butCfg.Name = "butCfg";
			this.butCfg.Size = new System.Drawing.Size(27, 21);
			this.butCfg.TabIndex = 35;
			this.butCfg.Text = "...";
			this.butCfg.UseVisualStyleBackColor = true;
			this.butCfg.Click += new System.EventHandler(this.butMAMEPaths_Click);
			// 
			// txtCfg
			// 
			this.txtCfg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtCfg.Location = new System.Drawing.Point(73, 61);
			this.txtCfg.Name = "txtCfg";
			this.txtCfg.Size = new System.Drawing.Size(371, 20);
			this.txtCfg.TabIndex = 34;
			// 
			// lblTitles
			// 
			this.lblTitles.AutoSize = true;
			this.lblTitles.Location = new System.Drawing.Point(10, 358);
			this.lblTitles.Name = "lblTitles";
			this.lblTitles.Size = new System.Drawing.Size(35, 13);
			this.lblTitles.TabIndex = 33;
			this.lblTitles.Text = "Titles:";
			// 
			// butTitles
			// 
			this.butTitles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butTitles.Location = new System.Drawing.Point(450, 354);
			this.butTitles.Name = "butTitles";
			this.butTitles.Size = new System.Drawing.Size(27, 21);
			this.butTitles.TabIndex = 32;
			this.butTitles.Text = "...";
			this.butTitles.UseVisualStyleBackColor = true;
			this.butTitles.Click += new System.EventHandler(this.butMAMEPaths_Click);
			// 
			// txtTitles
			// 
			this.txtTitles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtTitles.Location = new System.Drawing.Point(73, 355);
			this.txtTitles.Name = "txtTitles";
			this.txtTitles.Size = new System.Drawing.Size(371, 20);
			this.txtTitles.TabIndex = 31;
			// 
			// lblSnap
			// 
			this.lblSnap.AutoSize = true;
			this.lblSnap.Location = new System.Drawing.Point(10, 337);
			this.lblSnap.Name = "lblSnap";
			this.lblSnap.Size = new System.Drawing.Size(35, 13);
			this.lblSnap.TabIndex = 30;
			this.lblSnap.Text = "Snap:";
			// 
			// butSnap
			// 
			this.butSnap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butSnap.Location = new System.Drawing.Point(450, 333);
			this.butSnap.Name = "butSnap";
			this.butSnap.Size = new System.Drawing.Size(27, 21);
			this.butSnap.TabIndex = 29;
			this.butSnap.Text = "...";
			this.butSnap.UseVisualStyleBackColor = true;
			this.butSnap.Click += new System.EventHandler(this.butMAMEPaths_Click);
			// 
			// txtSnap
			// 
			this.txtSnap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSnap.Location = new System.Drawing.Point(73, 334);
			this.txtSnap.Name = "txtSnap";
			this.txtSnap.Size = new System.Drawing.Size(371, 20);
			this.txtSnap.TabIndex = 28;
			// 
			// lblMarquees
			// 
			this.lblMarquees.AutoSize = true;
			this.lblMarquees.Location = new System.Drawing.Point(10, 232);
			this.lblMarquees.Name = "lblMarquees";
			this.lblMarquees.Size = new System.Drawing.Size(57, 13);
			this.lblMarquees.TabIndex = 27;
			this.lblMarquees.Text = "Marquees:";
			// 
			// butMarquees
			// 
			this.butMarquees.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butMarquees.Location = new System.Drawing.Point(450, 228);
			this.butMarquees.Name = "butMarquees";
			this.butMarquees.Size = new System.Drawing.Size(27, 21);
			this.butMarquees.TabIndex = 26;
			this.butMarquees.Text = "...";
			this.butMarquees.UseVisualStyleBackColor = true;
			this.butMarquees.Click += new System.EventHandler(this.butMAMEPaths_Click);
			// 
			// txtMarquees
			// 
			this.txtMarquees.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtMarquees.Location = new System.Drawing.Point(73, 229);
			this.txtMarquees.Name = "txtMarquees";
			this.txtMarquees.Size = new System.Drawing.Size(371, 20);
			this.txtMarquees.TabIndex = 25;
			// 
			// lblManuals
			// 
			this.lblManuals.AutoSize = true;
			this.lblManuals.Location = new System.Drawing.Point(10, 211);
			this.lblManuals.Name = "lblManuals";
			this.lblManuals.Size = new System.Drawing.Size(50, 13);
			this.lblManuals.TabIndex = 24;
			this.lblManuals.Text = "Manuals:";
			// 
			// butManuals
			// 
			this.butManuals.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butManuals.Location = new System.Drawing.Point(450, 207);
			this.butManuals.Name = "butManuals";
			this.butManuals.Size = new System.Drawing.Size(27, 21);
			this.butManuals.TabIndex = 23;
			this.butManuals.Text = "...";
			this.butManuals.UseVisualStyleBackColor = true;
			this.butManuals.Click += new System.EventHandler(this.butMAMEPaths_Click);
			// 
			// txtManuals
			// 
			this.txtManuals.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtManuals.Location = new System.Drawing.Point(73, 208);
			this.txtManuals.Name = "txtManuals";
			this.txtManuals.Size = new System.Drawing.Size(371, 20);
			this.txtManuals.TabIndex = 22;
			// 
			// lblIni
			// 
			this.lblIni.AutoSize = true;
			this.lblIni.Location = new System.Drawing.Point(10, 190);
			this.lblIni.Name = "lblIni";
			this.lblIni.Size = new System.Drawing.Size(21, 13);
			this.lblIni.TabIndex = 21;
			this.lblIni.Text = "Ini:";
			// 
			// butIni
			// 
			this.butIni.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butIni.Location = new System.Drawing.Point(450, 186);
			this.butIni.Name = "butIni";
			this.butIni.Size = new System.Drawing.Size(27, 21);
			this.butIni.TabIndex = 20;
			this.butIni.Text = "...";
			this.butIni.UseVisualStyleBackColor = true;
			this.butIni.Click += new System.EventHandler(this.butMAMEPaths_Click);
			// 
			// txtIni
			// 
			this.txtIni.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtIni.Location = new System.Drawing.Point(73, 187);
			this.txtIni.Name = "txtIni";
			this.txtIni.Size = new System.Drawing.Size(371, 20);
			this.txtIni.TabIndex = 19;
			// 
			// lblIcons
			// 
			this.lblIcons.AutoSize = true;
			this.lblIcons.Location = new System.Drawing.Point(10, 169);
			this.lblIcons.Name = "lblIcons";
			this.lblIcons.Size = new System.Drawing.Size(36, 13);
			this.lblIcons.TabIndex = 18;
			this.lblIcons.Text = "Icons:";
			// 
			// butIcons
			// 
			this.butIcons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butIcons.Location = new System.Drawing.Point(450, 165);
			this.butIcons.Name = "butIcons";
			this.butIcons.Size = new System.Drawing.Size(27, 21);
			this.butIcons.TabIndex = 17;
			this.butIcons.Text = "...";
			this.butIcons.UseVisualStyleBackColor = true;
			this.butIcons.Click += new System.EventHandler(this.butMAMEPaths_Click);
			// 
			// txtIcons
			// 
			this.txtIcons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtIcons.Location = new System.Drawing.Point(73, 166);
			this.txtIcons.Name = "txtIcons";
			this.txtIcons.Size = new System.Drawing.Size(371, 20);
			this.txtIcons.TabIndex = 16;
			// 
			// lblFlyers
			// 
			this.lblFlyers.AutoSize = true;
			this.lblFlyers.Location = new System.Drawing.Point(10, 127);
			this.lblFlyers.Name = "lblFlyers";
			this.lblFlyers.Size = new System.Drawing.Size(37, 13);
			this.lblFlyers.TabIndex = 15;
			this.lblFlyers.Text = "Flyers:";
			// 
			// butFlyers
			// 
			this.butFlyers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butFlyers.Location = new System.Drawing.Point(450, 123);
			this.butFlyers.Name = "butFlyers";
			this.butFlyers.Size = new System.Drawing.Size(27, 21);
			this.butFlyers.TabIndex = 14;
			this.butFlyers.Text = "...";
			this.butFlyers.UseVisualStyleBackColor = true;
			this.butFlyers.Click += new System.EventHandler(this.butMAMEPaths_Click);
			// 
			// txtFlyers
			// 
			this.txtFlyers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtFlyers.Location = new System.Drawing.Point(73, 124);
			this.txtFlyers.Name = "txtFlyers";
			this.txtFlyers.Size = new System.Drawing.Size(371, 20);
			this.txtFlyers.TabIndex = 13;
			// 
			// lblCtrlr
			// 
			this.lblCtrlr.AutoSize = true;
			this.lblCtrlr.Location = new System.Drawing.Point(10, 106);
			this.lblCtrlr.Name = "lblCtrlr";
			this.lblCtrlr.Size = new System.Drawing.Size(28, 13);
			this.lblCtrlr.TabIndex = 12;
			this.lblCtrlr.Text = "Ctrlr:";
			// 
			// butCtrlr
			// 
			this.butCtrlr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butCtrlr.Location = new System.Drawing.Point(450, 102);
			this.butCtrlr.Name = "butCtrlr";
			this.butCtrlr.Size = new System.Drawing.Size(27, 21);
			this.butCtrlr.TabIndex = 11;
			this.butCtrlr.Text = "...";
			this.butCtrlr.UseVisualStyleBackColor = true;
			this.butCtrlr.Click += new System.EventHandler(this.butMAMEPaths_Click);
			// 
			// txtCtrlr
			// 
			this.txtCtrlr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtCtrlr.Location = new System.Drawing.Point(73, 103);
			this.txtCtrlr.Name = "txtCtrlr";
			this.txtCtrlr.Size = new System.Drawing.Size(371, 20);
			this.txtCtrlr.TabIndex = 10;
			// 
			// lblCPanel
			// 
			this.lblCPanel.AutoSize = true;
			this.lblCPanel.Location = new System.Drawing.Point(10, 85);
			this.lblCPanel.Name = "lblCPanel";
			this.lblCPanel.Size = new System.Drawing.Size(44, 13);
			this.lblCPanel.TabIndex = 9;
			this.lblCPanel.Text = "CPanel:";
			// 
			// butCPanel
			// 
			this.butCPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butCPanel.Location = new System.Drawing.Point(450, 81);
			this.butCPanel.Name = "butCPanel";
			this.butCPanel.Size = new System.Drawing.Size(27, 21);
			this.butCPanel.TabIndex = 8;
			this.butCPanel.Text = "...";
			this.butCPanel.UseVisualStyleBackColor = true;
			this.butCPanel.Click += new System.EventHandler(this.butMAMEPaths_Click);
			// 
			// txtCPanel
			// 
			this.txtCPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtCPanel.Location = new System.Drawing.Point(73, 82);
			this.txtCPanel.Name = "txtCPanel";
			this.txtCPanel.Size = new System.Drawing.Size(371, 20);
			this.txtCPanel.TabIndex = 7;
			// 
			// lblCabinets
			// 
			this.lblCabinets.AutoSize = true;
			this.lblCabinets.Location = new System.Drawing.Point(10, 43);
			this.lblCabinets.Name = "lblCabinets";
			this.lblCabinets.Size = new System.Drawing.Size(51, 13);
			this.lblCabinets.TabIndex = 6;
			this.lblCabinets.Text = "Cabinets:";
			// 
			// butCabinets
			// 
			this.butCabinets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butCabinets.Location = new System.Drawing.Point(450, 39);
			this.butCabinets.Name = "butCabinets";
			this.butCabinets.Size = new System.Drawing.Size(27, 21);
			this.butCabinets.TabIndex = 5;
			this.butCabinets.Text = "...";
			this.butCabinets.UseVisualStyleBackColor = true;
			this.butCabinets.Click += new System.EventHandler(this.butMAMEPaths_Click);
			// 
			// txtCabinets
			// 
			this.txtCabinets.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtCabinets.Location = new System.Drawing.Point(73, 40);
			this.txtCabinets.Name = "txtCabinets";
			this.txtCabinets.Size = new System.Drawing.Size(371, 20);
			this.txtCabinets.TabIndex = 4;
			// 
			// lblMAMEExe
			// 
			this.lblMAMEExe.AutoSize = true;
			this.lblMAMEExe.Location = new System.Drawing.Point(10, 22);
			this.lblMAMEExe.Name = "lblMAMEExe";
			this.lblMAMEExe.Size = new System.Drawing.Size(63, 13);
			this.lblMAMEExe.TabIndex = 3;
			this.lblMAMEExe.Text = "MAME Exe:";
			// 
			// butMAMEExe
			// 
			this.butMAMEExe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butMAMEExe.Location = new System.Drawing.Point(450, 18);
			this.butMAMEExe.Name = "butMAMEExe";
			this.butMAMEExe.Size = new System.Drawing.Size(27, 21);
			this.butMAMEExe.TabIndex = 2;
			this.butMAMEExe.Text = "...";
			this.butMAMEExe.UseVisualStyleBackColor = true;
			this.butMAMEExe.Click += new System.EventHandler(this.butMAMEExe_Click);
			// 
			// txtMAMEExe
			// 
			this.txtMAMEExe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtMAMEExe.Location = new System.Drawing.Point(73, 19);
			this.txtMAMEExe.Name = "txtMAMEExe";
			this.txtMAMEExe.Size = new System.Drawing.Size(371, 20);
			this.txtMAMEExe.TabIndex = 0;
			// 
			// tabProfiles
			// 
			this.tabProfiles.Controls.Add(this.grpProfiles);
			this.tabProfiles.Location = new System.Drawing.Point(4, 22);
			this.tabProfiles.Name = "tabProfiles";
			this.tabProfiles.Size = new System.Drawing.Size(514, 510);
			this.tabProfiles.TabIndex = 8;
			this.tabProfiles.Text = "Profiles";
			this.tabProfiles.UseVisualStyleBackColor = true;
			// 
			// grpProfiles
			// 
			this.grpProfiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpProfiles.Controls.Add(this.butProfileDelete);
			this.grpProfiles.Controls.Add(this.butProfileNew);
			this.grpProfiles.Controls.Add(this.lvwProfiles);
			this.grpProfiles.Location = new System.Drawing.Point(11, 6);
			this.grpProfiles.Name = "grpProfiles";
			this.grpProfiles.Size = new System.Drawing.Size(494, 381);
			this.grpProfiles.TabIndex = 32;
			this.grpProfiles.TabStop = false;
			this.grpProfiles.Text = "Profiles";
			// 
			// butProfileDelete
			// 
			this.butProfileDelete.Location = new System.Drawing.Point(99, 338);
			this.butProfileDelete.Name = "butProfileDelete";
			this.butProfileDelete.Size = new System.Drawing.Size(78, 26);
			this.butProfileDelete.TabIndex = 14;
			this.butProfileDelete.Text = "Delete";
			this.butProfileDelete.UseVisualStyleBackColor = true;
			this.butProfileDelete.Click += new System.EventHandler(this.butProfileDelete_Click);
			// 
			// butProfileNew
			// 
			this.butProfileNew.Location = new System.Drawing.Point(15, 338);
			this.butProfileNew.Name = "butProfileNew";
			this.butProfileNew.Size = new System.Drawing.Size(78, 26);
			this.butProfileNew.TabIndex = 13;
			this.butProfileNew.Text = "New";
			this.butProfileNew.UseVisualStyleBackColor = true;
			this.butProfileNew.Click += new System.EventHandler(this.butProfileNew_Click);
			// 
			// lvwProfiles
			// 
			this.lvwProfiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvwProfiles.CheckBoxes = true;
			this.lvwProfiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colProfileEnabled,
            this.colProfileName,
            this.colProfileType,
            this.colProfileLayout,
            this.colProfileLayoutOverride,
            this.colProfileLayoutSub,
            this.colProfileLabels,
            this.colProfileDatabase,
            this.colProfileExecutable,
            this.colProfileWindowTitle,
            this.colProfileWindowClass,
            this.colProfileUseExe,
            this.colProfileScreenshot,
            this.colProfileMinimize,
            this.colProfileMaximize,
            this.colProfileShowKey,
            this.colProfileHideKey,
            this.colProfileShowSendKeys,
            this.colProfileHideSendKeys,
            this.colProfileManuals,
            this.colProfileOpCards,
            this.colProfileSnaps,
            this.colProfileTitles,
            this.colProfileCarts,
            this.colProfileBoxes});
			this.lvwProfiles.DoubleClickDoesCheck = false;
			this.lvwProfiles.FullRowSelect = true;
			this.lvwProfiles.GridLines = true;
			this.lvwProfiles.Location = new System.Drawing.Point(15, 23);
			this.lvwProfiles.MultiSelect = false;
			this.lvwProfiles.Name = "lvwProfiles";
			this.lvwProfiles.Size = new System.Drawing.Size(463, 309);
			this.lvwProfiles.TabIndex = 12;
			this.lvwProfiles.UseCompatibleStateImageBehavior = false;
			this.lvwProfiles.View = System.Windows.Forms.View.Details;
			// 
			// colProfileEnabled
			// 
			this.colProfileEnabled.Text = "";
			this.colProfileEnabled.Width = 30;
			// 
			// colProfileName
			// 
			this.colProfileName.Text = "Name";
			this.colProfileName.Width = 100;
			// 
			// colProfileType
			// 
			this.colProfileType.Text = "Type";
			this.colProfileType.Width = 100;
			// 
			// colProfileLayout
			// 
			this.colProfileLayout.Text = "Layout";
			this.colProfileLayout.Width = 100;
			// 
			// colProfileLayoutOverride
			// 
			this.colProfileLayoutOverride.Text = "Layout Override";
			this.colProfileLayoutOverride.Width = 100;
			// 
			// colProfileLayoutSub
			// 
			this.colProfileLayoutSub.Text = "Layout Sub";
			this.colProfileLayoutSub.Width = 100;
			// 
			// colProfileLabels
			// 
			this.colProfileLabels.Text = "Labels";
			this.colProfileLabels.Width = 100;
			// 
			// colProfileDatabase
			// 
			this.colProfileDatabase.Text = "Database";
			this.colProfileDatabase.Width = 100;
			// 
			// colProfileExecutable
			// 
			this.colProfileExecutable.Text = "Executable";
			this.colProfileExecutable.Width = 100;
			// 
			// colProfileWindowTitle
			// 
			this.colProfileWindowTitle.Text = "Window Title";
			this.colProfileWindowTitle.Width = 100;
			// 
			// colProfileWindowClass
			// 
			this.colProfileWindowClass.Text = "Window Class";
			this.colProfileWindowClass.Width = 100;
			// 
			// colProfileUseExe
			// 
			this.colProfileUseExe.Text = "Use Exe";
			this.colProfileUseExe.Width = 100;
			// 
			// colProfileScreenshot
			// 
			this.colProfileScreenshot.Text = "Take Screenshot";
			this.colProfileScreenshot.Width = 100;
			// 
			// colProfileMinimize
			// 
			this.colProfileMinimize.Text = "Minimize";
			this.colProfileMinimize.Width = 100;
			// 
			// colProfileMaximize
			// 
			this.colProfileMaximize.Text = "Maximize";
			this.colProfileMaximize.Width = 100;
			// 
			// colProfileShowKey
			// 
			this.colProfileShowKey.Text = "Show Key";
			this.colProfileShowKey.Width = 100;
			// 
			// colProfileHideKey
			// 
			this.colProfileHideKey.Text = "Hide Key";
			this.colProfileHideKey.Width = 100;
			// 
			// colProfileShowSendKeys
			// 
			this.colProfileShowSendKeys.Text = "Show SendKeys";
			this.colProfileShowSendKeys.Width = 100;
			// 
			// colProfileHideSendKeys
			// 
			this.colProfileHideSendKeys.Text = "Hide SendKeys";
			this.colProfileHideSendKeys.Width = 100;
			// 
			// colProfileManuals
			// 
			this.colProfileManuals.Text = "Manuals Path";
			this.colProfileManuals.Width = 100;
			// 
			// colProfileOpCards
			// 
			this.colProfileOpCards.Text = "Op Cards Path";
			this.colProfileOpCards.Width = 100;
			// 
			// colProfileSnaps
			// 
			this.colProfileSnaps.Text = "Snaps Path";
			this.colProfileSnaps.Width = 100;
			// 
			// colProfileTitles
			// 
			this.colProfileTitles.Text = "Titles Folder";
			this.colProfileTitles.Width = 100;
			// 
			// colProfileCarts
			// 
			this.colProfileCarts.Text = "Carts Folder";
			this.colProfileCarts.Width = 100;
			// 
			// colProfileBoxes
			// 
			this.colProfileBoxes.Text = "Boxes Folder";
			this.colProfileBoxes.Width = 100;
			// 
			// tabInput
			// 
			this.tabInput.Controls.Add(this.grpInput);
			this.tabInput.Location = new System.Drawing.Point(4, 22);
			this.tabInput.Name = "tabInput";
			this.tabInput.Size = new System.Drawing.Size(514, 510);
			this.tabInput.TabIndex = 6;
			this.tabInput.Text = "Input";
			this.tabInput.UseVisualStyleBackColor = true;
			// 
			// grpInput
			// 
			this.grpInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpInput.Controls.Add(this.chkBackShowsCP);
			this.grpInput.Controls.Add(this.chkStopBackMenu);
			this.grpInput.Controls.Add(this.chkEnableExitKey);
			this.grpInput.Controls.Add(this.lblExitKey);
			this.grpInput.Controls.Add(this.butExitKey);
			this.grpInput.Controls.Add(this.txtExitKey);
			this.grpInput.Controls.Add(this.chkBackKeyExitMenu);
			this.grpInput.Controls.Add(this.lblHideDesktop);
			this.grpInput.Controls.Add(this.lblVolumeUp);
			this.grpInput.Controls.Add(this.lblVolumeDown);
			this.grpInput.Controls.Add(this.lblMenuRight);
			this.grpInput.Controls.Add(this.lblMenuLeft);
			this.grpInput.Controls.Add(this.lblMenuDown);
			this.grpInput.Controls.Add(this.lblMenuUp);
			this.grpInput.Controls.Add(this.lblSelectKey);
			this.grpInput.Controls.Add(this.lblBackKey);
			this.grpInput.Controls.Add(this.butShowDesktop);
			this.grpInput.Controls.Add(this.butHideDesktop);
			this.grpInput.Controls.Add(this.txtShowDesktop);
			this.grpInput.Controls.Add(this.butVolumeUp);
			this.grpInput.Controls.Add(this.txtHideDesktop);
			this.grpInput.Controls.Add(this.butVolumeDown);
			this.grpInput.Controls.Add(this.txtVolumeUp);
			this.grpInput.Controls.Add(this.butMenuRight);
			this.grpInput.Controls.Add(this.txtVolumeDown);
			this.grpInput.Controls.Add(this.butMenuLeft);
			this.grpInput.Controls.Add(this.txtMenuRight);
			this.grpInput.Controls.Add(this.butMenuDown);
			this.grpInput.Controls.Add(this.txtMenuLeft);
			this.grpInput.Controls.Add(this.butMenuUp);
			this.grpInput.Controls.Add(this.txtMenuDown);
			this.grpInput.Controls.Add(this.butSelectKey);
			this.grpInput.Controls.Add(this.txtMenuUp);
			this.grpInput.Controls.Add(this.butBackKey);
			this.grpInput.Controls.Add(this.txtSelectKey);
			this.grpInput.Controls.Add(this.lblShowKey);
			this.grpInput.Controls.Add(this.txtBackKey);
			this.grpInput.Controls.Add(this.butShowKey);
			this.grpInput.Controls.Add(this.txtShowKey);
			this.grpInput.Controls.Add(this.lblShowDesktop);
			this.grpInput.Location = new System.Drawing.Point(11, 6);
			this.grpInput.Name = "grpInput";
			this.grpInput.Size = new System.Drawing.Size(494, 358);
			this.grpInput.TabIndex = 32;
			this.grpInput.TabStop = false;
			this.grpInput.Text = "Input";
			// 
			// chkBackShowsCP
			// 
			this.chkBackShowsCP.AutoSize = true;
			this.chkBackShowsCP.Location = new System.Drawing.Point(92, 297);
			this.chkBackShowsCP.Name = "chkBackShowsCP";
			this.chkBackShowsCP.Size = new System.Drawing.Size(166, 17);
			this.chkBackShowsCP.TabIndex = 54;
			this.chkBackShowsCP.Text = "Back Key In Menu Shows CP";
			this.chkBackShowsCP.UseVisualStyleBackColor = true;
			// 
			// chkStopBackMenu
			// 
			this.chkStopBackMenu.AutoSize = true;
			this.chkStopBackMenu.Location = new System.Drawing.Point(92, 329);
			this.chkStopBackMenu.Name = "chkStopBackMenu";
			this.chkStopBackMenu.Size = new System.Drawing.Size(193, 17);
			this.chkStopBackMenu.TabIndex = 53;
			this.chkStopBackMenu.Text = "Stop Back Showing Menu From CP";
			this.chkStopBackMenu.UseVisualStyleBackColor = true;
			// 
			// chkEnableExitKey
			// 
			this.chkEnableExitKey.AutoSize = true;
			this.chkEnableExitKey.Location = new System.Drawing.Point(92, 281);
			this.chkEnableExitKey.Name = "chkEnableExitKey";
			this.chkEnableExitKey.Size = new System.Drawing.Size(100, 17);
			this.chkEnableExitKey.TabIndex = 52;
			this.chkEnableExitKey.Text = "Enable Exit Key";
			this.chkEnableExitKey.UseVisualStyleBackColor = true;
			// 
			// lblExitKey
			// 
			this.lblExitKey.AutoSize = true;
			this.lblExitKey.Location = new System.Drawing.Point(6, 90);
			this.lblExitKey.Name = "lblExitKey";
			this.lblExitKey.Size = new System.Drawing.Size(48, 13);
			this.lblExitKey.TabIndex = 51;
			this.lblExitKey.Text = "Exit Key:";
			// 
			// butExitKey
			// 
			this.butExitKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butExitKey.Location = new System.Drawing.Point(454, 86);
			this.butExitKey.Name = "butExitKey";
			this.butExitKey.Size = new System.Drawing.Size(27, 21);
			this.butExitKey.TabIndex = 50;
			this.butExitKey.Text = "...";
			this.butExitKey.UseVisualStyleBackColor = true;
			// 
			// txtExitKey
			// 
			this.txtExitKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtExitKey.Location = new System.Drawing.Point(85, 87);
			this.txtExitKey.Name = "txtExitKey";
			this.txtExitKey.Size = new System.Drawing.Size(363, 20);
			this.txtExitKey.TabIndex = 49;
			// 
			// chkBackKeyExitMenu
			// 
			this.chkBackKeyExitMenu.AutoSize = true;
			this.chkBackKeyExitMenu.Location = new System.Drawing.Point(92, 313);
			this.chkBackKeyExitMenu.Name = "chkBackKeyExitMenu";
			this.chkBackKeyExitMenu.Size = new System.Drawing.Size(142, 17);
			this.chkBackKeyExitMenu.TabIndex = 48;
			this.chkBackKeyExitMenu.Text = "Back Key Will Exit Menu";
			this.chkBackKeyExitMenu.UseVisualStyleBackColor = true;
			// 
			// lblHideDesktop
			// 
			this.lblHideDesktop.AutoSize = true;
			this.lblHideDesktop.Location = new System.Drawing.Point(6, 237);
			this.lblHideDesktop.Name = "lblHideDesktop";
			this.lblHideDesktop.Size = new System.Drawing.Size(75, 13);
			this.lblHideDesktop.TabIndex = 40;
			this.lblHideDesktop.Text = "Hide Desktop:";
			// 
			// lblVolumeUp
			// 
			this.lblVolumeUp.AutoSize = true;
			this.lblVolumeUp.Location = new System.Drawing.Point(6, 216);
			this.lblVolumeUp.Name = "lblVolumeUp";
			this.lblVolumeUp.Size = new System.Drawing.Size(62, 13);
			this.lblVolumeUp.TabIndex = 39;
			this.lblVolumeUp.Text = "Volume Up:";
			// 
			// lblVolumeDown
			// 
			this.lblVolumeDown.AutoSize = true;
			this.lblVolumeDown.Location = new System.Drawing.Point(6, 195);
			this.lblVolumeDown.Name = "lblVolumeDown";
			this.lblVolumeDown.Size = new System.Drawing.Size(76, 13);
			this.lblVolumeDown.TabIndex = 38;
			this.lblVolumeDown.Text = "Volume Down:";
			// 
			// lblMenuRight
			// 
			this.lblMenuRight.AutoSize = true;
			this.lblMenuRight.Location = new System.Drawing.Point(6, 174);
			this.lblMenuRight.Name = "lblMenuRight";
			this.lblMenuRight.Size = new System.Drawing.Size(65, 13);
			this.lblMenuRight.TabIndex = 37;
			this.lblMenuRight.Text = "Menu Right:";
			// 
			// lblMenuLeft
			// 
			this.lblMenuLeft.AutoSize = true;
			this.lblMenuLeft.Location = new System.Drawing.Point(6, 153);
			this.lblMenuLeft.Name = "lblMenuLeft";
			this.lblMenuLeft.Size = new System.Drawing.Size(58, 13);
			this.lblMenuLeft.TabIndex = 36;
			this.lblMenuLeft.Text = "Menu Left:";
			// 
			// lblMenuDown
			// 
			this.lblMenuDown.AutoSize = true;
			this.lblMenuDown.Location = new System.Drawing.Point(6, 132);
			this.lblMenuDown.Name = "lblMenuDown";
			this.lblMenuDown.Size = new System.Drawing.Size(68, 13);
			this.lblMenuDown.TabIndex = 35;
			this.lblMenuDown.Text = "Menu Down:";
			// 
			// lblMenuUp
			// 
			this.lblMenuUp.AutoSize = true;
			this.lblMenuUp.Location = new System.Drawing.Point(6, 111);
			this.lblMenuUp.Name = "lblMenuUp";
			this.lblMenuUp.Size = new System.Drawing.Size(54, 13);
			this.lblMenuUp.TabIndex = 34;
			this.lblMenuUp.Text = "Menu Up:";
			// 
			// lblSelectKey
			// 
			this.lblSelectKey.AutoSize = true;
			this.lblSelectKey.Location = new System.Drawing.Point(6, 69);
			this.lblSelectKey.Name = "lblSelectKey";
			this.lblSelectKey.Size = new System.Drawing.Size(61, 13);
			this.lblSelectKey.TabIndex = 33;
			this.lblSelectKey.Text = "Select Key:";
			// 
			// lblBackKey
			// 
			this.lblBackKey.AutoSize = true;
			this.lblBackKey.Location = new System.Drawing.Point(6, 48);
			this.lblBackKey.Name = "lblBackKey";
			this.lblBackKey.Size = new System.Drawing.Size(56, 13);
			this.lblBackKey.TabIndex = 32;
			this.lblBackKey.Text = "Back Key:";
			// 
			// butShowDesktop
			// 
			this.butShowDesktop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butShowDesktop.Location = new System.Drawing.Point(454, 254);
			this.butShowDesktop.Name = "butShowDesktop";
			this.butShowDesktop.Size = new System.Drawing.Size(27, 21);
			this.butShowDesktop.TabIndex = 1;
			this.butShowDesktop.Text = "...";
			this.butShowDesktop.UseVisualStyleBackColor = true;
			// 
			// butHideDesktop
			// 
			this.butHideDesktop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butHideDesktop.Location = new System.Drawing.Point(454, 233);
			this.butHideDesktop.Name = "butHideDesktop";
			this.butHideDesktop.Size = new System.Drawing.Size(27, 21);
			this.butHideDesktop.TabIndex = 1;
			this.butHideDesktop.Text = "...";
			this.butHideDesktop.UseVisualStyleBackColor = true;
			// 
			// txtShowDesktop
			// 
			this.txtShowDesktop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtShowDesktop.Location = new System.Drawing.Point(85, 255);
			this.txtShowDesktop.Name = "txtShowDesktop";
			this.txtShowDesktop.Size = new System.Drawing.Size(363, 20);
			this.txtShowDesktop.TabIndex = 0;
			// 
			// butVolumeUp
			// 
			this.butVolumeUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butVolumeUp.Location = new System.Drawing.Point(454, 212);
			this.butVolumeUp.Name = "butVolumeUp";
			this.butVolumeUp.Size = new System.Drawing.Size(27, 21);
			this.butVolumeUp.TabIndex = 1;
			this.butVolumeUp.Text = "...";
			this.butVolumeUp.UseVisualStyleBackColor = true;
			// 
			// txtHideDesktop
			// 
			this.txtHideDesktop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtHideDesktop.Location = new System.Drawing.Point(85, 234);
			this.txtHideDesktop.Name = "txtHideDesktop";
			this.txtHideDesktop.Size = new System.Drawing.Size(363, 20);
			this.txtHideDesktop.TabIndex = 0;
			// 
			// butVolumeDown
			// 
			this.butVolumeDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butVolumeDown.Location = new System.Drawing.Point(454, 191);
			this.butVolumeDown.Name = "butVolumeDown";
			this.butVolumeDown.Size = new System.Drawing.Size(27, 21);
			this.butVolumeDown.TabIndex = 1;
			this.butVolumeDown.Text = "...";
			this.butVolumeDown.UseVisualStyleBackColor = true;
			// 
			// txtVolumeUp
			// 
			this.txtVolumeUp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtVolumeUp.Location = new System.Drawing.Point(85, 213);
			this.txtVolumeUp.Name = "txtVolumeUp";
			this.txtVolumeUp.Size = new System.Drawing.Size(363, 20);
			this.txtVolumeUp.TabIndex = 0;
			// 
			// butMenuRight
			// 
			this.butMenuRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butMenuRight.Location = new System.Drawing.Point(454, 170);
			this.butMenuRight.Name = "butMenuRight";
			this.butMenuRight.Size = new System.Drawing.Size(27, 21);
			this.butMenuRight.TabIndex = 1;
			this.butMenuRight.Text = "...";
			this.butMenuRight.UseVisualStyleBackColor = true;
			// 
			// txtVolumeDown
			// 
			this.txtVolumeDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtVolumeDown.Location = new System.Drawing.Point(85, 192);
			this.txtVolumeDown.Name = "txtVolumeDown";
			this.txtVolumeDown.Size = new System.Drawing.Size(363, 20);
			this.txtVolumeDown.TabIndex = 0;
			// 
			// butMenuLeft
			// 
			this.butMenuLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butMenuLeft.Location = new System.Drawing.Point(454, 149);
			this.butMenuLeft.Name = "butMenuLeft";
			this.butMenuLeft.Size = new System.Drawing.Size(27, 21);
			this.butMenuLeft.TabIndex = 1;
			this.butMenuLeft.Text = "...";
			this.butMenuLeft.UseVisualStyleBackColor = true;
			// 
			// txtMenuRight
			// 
			this.txtMenuRight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtMenuRight.Location = new System.Drawing.Point(85, 171);
			this.txtMenuRight.Name = "txtMenuRight";
			this.txtMenuRight.Size = new System.Drawing.Size(363, 20);
			this.txtMenuRight.TabIndex = 0;
			// 
			// butMenuDown
			// 
			this.butMenuDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butMenuDown.Location = new System.Drawing.Point(454, 128);
			this.butMenuDown.Name = "butMenuDown";
			this.butMenuDown.Size = new System.Drawing.Size(27, 21);
			this.butMenuDown.TabIndex = 1;
			this.butMenuDown.Text = "...";
			this.butMenuDown.UseVisualStyleBackColor = true;
			// 
			// txtMenuLeft
			// 
			this.txtMenuLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtMenuLeft.Location = new System.Drawing.Point(85, 150);
			this.txtMenuLeft.Name = "txtMenuLeft";
			this.txtMenuLeft.Size = new System.Drawing.Size(363, 20);
			this.txtMenuLeft.TabIndex = 0;
			// 
			// butMenuUp
			// 
			this.butMenuUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butMenuUp.Location = new System.Drawing.Point(454, 107);
			this.butMenuUp.Name = "butMenuUp";
			this.butMenuUp.Size = new System.Drawing.Size(27, 21);
			this.butMenuUp.TabIndex = 1;
			this.butMenuUp.Text = "...";
			this.butMenuUp.UseVisualStyleBackColor = true;
			// 
			// txtMenuDown
			// 
			this.txtMenuDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtMenuDown.Location = new System.Drawing.Point(85, 129);
			this.txtMenuDown.Name = "txtMenuDown";
			this.txtMenuDown.Size = new System.Drawing.Size(363, 20);
			this.txtMenuDown.TabIndex = 0;
			// 
			// butSelectKey
			// 
			this.butSelectKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butSelectKey.Location = new System.Drawing.Point(454, 65);
			this.butSelectKey.Name = "butSelectKey";
			this.butSelectKey.Size = new System.Drawing.Size(27, 21);
			this.butSelectKey.TabIndex = 1;
			this.butSelectKey.Text = "...";
			this.butSelectKey.UseVisualStyleBackColor = true;
			// 
			// txtMenuUp
			// 
			this.txtMenuUp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtMenuUp.Location = new System.Drawing.Point(85, 108);
			this.txtMenuUp.Name = "txtMenuUp";
			this.txtMenuUp.Size = new System.Drawing.Size(363, 20);
			this.txtMenuUp.TabIndex = 0;
			// 
			// butBackKey
			// 
			this.butBackKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butBackKey.Location = new System.Drawing.Point(454, 44);
			this.butBackKey.Name = "butBackKey";
			this.butBackKey.Size = new System.Drawing.Size(27, 21);
			this.butBackKey.TabIndex = 1;
			this.butBackKey.Text = "...";
			this.butBackKey.UseVisualStyleBackColor = true;
			// 
			// txtSelectKey
			// 
			this.txtSelectKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSelectKey.Location = new System.Drawing.Point(85, 66);
			this.txtSelectKey.Name = "txtSelectKey";
			this.txtSelectKey.Size = new System.Drawing.Size(363, 20);
			this.txtSelectKey.TabIndex = 0;
			// 
			// lblShowKey
			// 
			this.lblShowKey.AutoSize = true;
			this.lblShowKey.Location = new System.Drawing.Point(6, 27);
			this.lblShowKey.Name = "lblShowKey";
			this.lblShowKey.Size = new System.Drawing.Size(58, 13);
			this.lblShowKey.TabIndex = 31;
			this.lblShowKey.Text = "Show Key:";
			// 
			// txtBackKey
			// 
			this.txtBackKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtBackKey.Location = new System.Drawing.Point(85, 45);
			this.txtBackKey.Name = "txtBackKey";
			this.txtBackKey.Size = new System.Drawing.Size(363, 20);
			this.txtBackKey.TabIndex = 0;
			// 
			// butShowKey
			// 
			this.butShowKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butShowKey.Location = new System.Drawing.Point(454, 23);
			this.butShowKey.Name = "butShowKey";
			this.butShowKey.Size = new System.Drawing.Size(27, 21);
			this.butShowKey.TabIndex = 1;
			this.butShowKey.Text = "...";
			this.butShowKey.UseVisualStyleBackColor = true;
			// 
			// txtShowKey
			// 
			this.txtShowKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtShowKey.Location = new System.Drawing.Point(85, 24);
			this.txtShowKey.Name = "txtShowKey";
			this.txtShowKey.Size = new System.Drawing.Size(363, 20);
			this.txtShowKey.TabIndex = 0;
			// 
			// lblShowDesktop
			// 
			this.lblShowDesktop.AutoSize = true;
			this.lblShowDesktop.Location = new System.Drawing.Point(6, 258);
			this.lblShowDesktop.Name = "lblShowDesktop";
			this.lblShowDesktop.Size = new System.Drawing.Size(80, 13);
			this.lblShowDesktop.TabIndex = 41;
			this.lblShowDesktop.Text = "Show Desktop:";
			// 
			// tabDisplay
			// 
			this.tabDisplay.Controls.Add(this.grpMenuOptions);
			this.tabDisplay.Controls.Add(this.grpChangeBackgrounds);
			this.tabDisplay.Controls.Add(this.grpMenuColors);
			this.tabDisplay.Controls.Add(this.grpGraphicsQuality);
			this.tabDisplay.Controls.Add(this.grpLabelOutline);
			this.tabDisplay.Controls.Add(this.grpSubScreen);
			this.tabDisplay.Controls.Add(this.grpAutoRotation);
			this.tabDisplay.Controls.Add(this.grpLoadingScreens);
			this.tabDisplay.Controls.Add(this.grpDisplayChangeDelay);
			this.tabDisplay.Controls.Add(this.grpAlphaFade);
			this.tabDisplay.Controls.Add(this.grpLabelSpot);
			this.tabDisplay.Controls.Add(this.grpLabelArrow);
			this.tabDisplay.Controls.Add(this.grpScreen);
			this.tabDisplay.Controls.Add(this.grpRotation);
			this.tabDisplay.Location = new System.Drawing.Point(4, 22);
			this.tabDisplay.Name = "tabDisplay";
			this.tabDisplay.Padding = new System.Windows.Forms.Padding(3);
			this.tabDisplay.Size = new System.Drawing.Size(514, 510);
			this.tabDisplay.TabIndex = 0;
			this.tabDisplay.Text = "Display";
			this.tabDisplay.UseVisualStyleBackColor = true;
			// 
			// grpMenuOptions
			// 
			this.grpMenuOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpMenuOptions.Controls.Add(this.lblMenuFont);
			this.grpMenuOptions.Controls.Add(this.butChangeMenuBak);
			this.grpMenuOptions.Controls.Add(this.chkShowDropShadow);
			this.grpMenuOptions.Controls.Add(this.butChooseMenuFont);
			this.grpMenuOptions.Controls.Add(this.chkUseMenuBorders);
			this.grpMenuOptions.Controls.Add(this.chkHideExitMenu);
			this.grpMenuOptions.Location = new System.Drawing.Point(262, 347);
			this.grpMenuOptions.Name = "grpMenuOptions";
			this.grpMenuOptions.Size = new System.Drawing.Size(240, 104);
			this.grpMenuOptions.TabIndex = 42;
			this.grpMenuOptions.TabStop = false;
			this.grpMenuOptions.Text = "Menu Options";
			// 
			// lblMenuFont
			// 
			this.lblMenuFont.AutoEllipsis = true;
			this.lblMenuFont.Location = new System.Drawing.Point(13, 73);
			this.lblMenuFont.Name = "lblMenuFont";
			this.lblMenuFont.Size = new System.Drawing.Size(178, 18);
			this.lblMenuFont.TabIndex = 4;
			this.lblMenuFont.Text = "...";
			// 
			// butChangeMenuBak
			// 
			this.butChangeMenuBak.Location = new System.Drawing.Point(151, 19);
			this.butChangeMenuBak.Name = "butChangeMenuBak";
			this.butChangeMenuBak.Size = new System.Drawing.Size(75, 23);
			this.butChangeMenuBak.TabIndex = 1;
			this.butChangeMenuBak.Text = "Menu Image";
			this.toolTip1.SetToolTip(this.butChangeMenuBak, "Choose the background image for the menu");
			this.butChangeMenuBak.UseVisualStyleBackColor = true;
			this.butChangeMenuBak.Click += new System.EventHandler(this.butChangeMenuBak_Click);
			// 
			// chkShowDropShadow
			// 
			this.chkShowDropShadow.AutoSize = true;
			this.chkShowDropShadow.Location = new System.Drawing.Point(12, 51);
			this.chkShowDropShadow.Name = "chkShowDropShadow";
			this.chkShowDropShadow.Size = new System.Drawing.Size(121, 17);
			this.chkShowDropShadow.TabIndex = 2;
			this.chkShowDropShadow.Text = "Show Drop Shadow";
			this.chkShowDropShadow.UseVisualStyleBackColor = true;
			// 
			// butChooseMenuFont
			// 
			this.butChooseMenuFont.Location = new System.Drawing.Point(201, 70);
			this.butChooseMenuFont.Name = "butChooseMenuFont";
			this.butChooseMenuFont.Size = new System.Drawing.Size(25, 23);
			this.butChooseMenuFont.TabIndex = 5;
			this.butChooseMenuFont.Text = "...";
			this.butChooseMenuFont.UseVisualStyleBackColor = true;
			this.butChooseMenuFont.Click += new System.EventHandler(this.butChooseMenuFont_Click);
			// 
			// chkUseMenuBorders
			// 
			this.chkUseMenuBorders.AutoSize = true;
			this.chkUseMenuBorders.Location = new System.Drawing.Point(12, 35);
			this.chkUseMenuBorders.Name = "chkUseMenuBorders";
			this.chkUseMenuBorders.Size = new System.Drawing.Size(122, 17);
			this.chkUseMenuBorders.TabIndex = 1;
			this.chkUseMenuBorders.Text = "Show Menu Borders";
			this.chkUseMenuBorders.UseVisualStyleBackColor = true;
			// 
			// chkHideExitMenu
			// 
			this.chkHideExitMenu.AutoSize = true;
			this.chkHideExitMenu.Location = new System.Drawing.Point(12, 19);
			this.chkHideExitMenu.Name = "chkHideExitMenu";
			this.chkHideExitMenu.Size = new System.Drawing.Size(68, 17);
			this.chkHideExitMenu.TabIndex = 0;
			this.chkHideExitMenu.Text = "Hide Exit";
			this.chkHideExitMenu.UseVisualStyleBackColor = true;
			// 
			// grpChangeBackgrounds
			// 
			this.grpChangeBackgrounds.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpChangeBackgrounds.Controls.Add(this.butChangeDefaultBak);
			this.grpChangeBackgrounds.Controls.Add(this.butChangeMainMenuBak);
			this.grpChangeBackgrounds.Controls.Add(this.butChangeInfoBak);
			this.grpChangeBackgrounds.Location = new System.Drawing.Point(11, 421);
			this.grpChangeBackgrounds.Name = "grpChangeBackgrounds";
			this.grpChangeBackgrounds.Size = new System.Drawing.Size(240, 51);
			this.grpChangeBackgrounds.TabIndex = 41;
			this.grpChangeBackgrounds.TabStop = false;
			this.grpChangeBackgrounds.Text = "Backgrounds";
			// 
			// butChangeDefaultBak
			// 
			this.butChangeDefaultBak.Location = new System.Drawing.Point(8, 16);
			this.butChangeDefaultBak.Name = "butChangeDefaultBak";
			this.butChangeDefaultBak.Size = new System.Drawing.Size(70, 24);
			this.butChangeDefaultBak.TabIndex = 4;
			this.butChangeDefaultBak.Text = "Default";
			this.toolTip1.SetToolTip(this.butChangeDefaultBak, "Changes the default background\r\nimage behind controls. Ensure\r\ntransparency of la" +
        "yout to see.");
			this.butChangeDefaultBak.UseVisualStyleBackColor = true;
			this.butChangeDefaultBak.Click += new System.EventHandler(this.butChangeDefaultBak_Click);
			// 
			// butChangeMainMenuBak
			// 
			this.butChangeMainMenuBak.Location = new System.Drawing.Point(84, 16);
			this.butChangeMainMenuBak.Name = "butChangeMainMenuBak";
			this.butChangeMainMenuBak.Size = new System.Drawing.Size(70, 24);
			this.butChangeMainMenuBak.TabIndex = 5;
			this.butChangeMainMenuBak.Text = "Main Menu";
			this.toolTip1.SetToolTip(this.butChangeMainMenuBak, "Changes the default background\r\nbehind the main menu.");
			this.butChangeMainMenuBak.UseVisualStyleBackColor = true;
			this.butChangeMainMenuBak.Click += new System.EventHandler(this.butChangeMainMenuBak_Click);
			// 
			// butChangeInfoBak
			// 
			this.butChangeInfoBak.Location = new System.Drawing.Point(160, 16);
			this.butChangeInfoBak.Name = "butChangeInfoBak";
			this.butChangeInfoBak.Size = new System.Drawing.Size(70, 24);
			this.butChangeInfoBak.TabIndex = 6;
			this.butChangeInfoBak.Text = "Info";
			this.toolTip1.SetToolTip(this.butChangeInfoBak, "Changes the default background\r\nimage behind the menu information\r\nscreens.");
			this.butChangeInfoBak.UseVisualStyleBackColor = true;
			this.butChangeInfoBak.Click += new System.EventHandler(this.butChangeInfoBak_Click);
			// 
			// grpMenuColors
			// 
			this.grpMenuColors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpMenuColors.Controls.Add(this.lblMenuBorderColor);
			this.grpMenuColors.Controls.Add(this.butMenuSelectorBorderColor);
			this.grpMenuColors.Controls.Add(this.butMenuBorderColor);
			this.grpMenuColors.Controls.Add(this.lblSelectorBorderColor);
			this.grpMenuColors.Controls.Add(this.lblSelectorBarColor);
			this.grpMenuColors.Controls.Add(this.butMenuFontColor);
			this.grpMenuColors.Controls.Add(this.lblMenuFontColor);
			this.grpMenuColors.Controls.Add(this.butMenuSelectorBarColor);
			this.grpMenuColors.Location = new System.Drawing.Point(11, 347);
			this.grpMenuColors.Name = "grpMenuColors";
			this.grpMenuColors.Size = new System.Drawing.Size(240, 68);
			this.grpMenuColors.TabIndex = 40;
			this.grpMenuColors.TabStop = false;
			this.grpMenuColors.Text = "Menu Colors";
			// 
			// lblMenuBorderColor
			// 
			this.lblMenuBorderColor.AutoSize = true;
			this.lblMenuBorderColor.Location = new System.Drawing.Point(157, 21);
			this.lblMenuBorderColor.Name = "lblMenuBorderColor";
			this.lblMenuBorderColor.Size = new System.Drawing.Size(71, 13);
			this.lblMenuBorderColor.TabIndex = 3;
			this.lblMenuBorderColor.Text = "Menu Border ";
			// 
			// butMenuSelectorBorderColor
			// 
			this.butMenuSelectorBorderColor.Location = new System.Drawing.Point(124, 37);
			this.butMenuSelectorBorderColor.Name = "butMenuSelectorBorderColor";
			this.butMenuSelectorBorderColor.Size = new System.Drawing.Size(32, 20);
			this.butMenuSelectorBorderColor.TabIndex = 6;
			this.butMenuSelectorBorderColor.UseVisualStyleBackColor = true;
			this.butMenuSelectorBorderColor.Click += new System.EventHandler(this.butMenuSelectorBorderColor_Click);
			// 
			// butMenuBorderColor
			// 
			this.butMenuBorderColor.Location = new System.Drawing.Point(124, 16);
			this.butMenuBorderColor.Name = "butMenuBorderColor";
			this.butMenuBorderColor.Size = new System.Drawing.Size(32, 20);
			this.butMenuBorderColor.TabIndex = 2;
			this.butMenuBorderColor.UseVisualStyleBackColor = true;
			this.butMenuBorderColor.Click += new System.EventHandler(this.butMenuBorderColor_Click);
			// 
			// lblSelectorBorderColor
			// 
			this.lblSelectorBorderColor.AutoSize = true;
			this.lblSelectorBorderColor.Location = new System.Drawing.Point(157, 42);
			this.lblSelectorBorderColor.Name = "lblSelectorBorderColor";
			this.lblSelectorBorderColor.Size = new System.Drawing.Size(80, 13);
			this.lblSelectorBorderColor.TabIndex = 7;
			this.lblSelectorBorderColor.Text = "Selector Border";
			// 
			// lblSelectorBarColor
			// 
			this.lblSelectorBarColor.AutoSize = true;
			this.lblSelectorBarColor.Location = new System.Drawing.Point(45, 42);
			this.lblSelectorBarColor.Name = "lblSelectorBarColor";
			this.lblSelectorBarColor.Size = new System.Drawing.Size(65, 13);
			this.lblSelectorBarColor.TabIndex = 5;
			this.lblSelectorBarColor.Text = "Selector Bar";
			// 
			// butMenuFontColor
			// 
			this.butMenuFontColor.Location = new System.Drawing.Point(12, 16);
			this.butMenuFontColor.Name = "butMenuFontColor";
			this.butMenuFontColor.Size = new System.Drawing.Size(32, 20);
			this.butMenuFontColor.TabIndex = 0;
			this.butMenuFontColor.UseVisualStyleBackColor = true;
			this.butMenuFontColor.Click += new System.EventHandler(this.butMenuFontColor_Click);
			// 
			// lblMenuFontColor
			// 
			this.lblMenuFontColor.AutoSize = true;
			this.lblMenuFontColor.Location = new System.Drawing.Point(45, 21);
			this.lblMenuFontColor.Name = "lblMenuFontColor";
			this.lblMenuFontColor.Size = new System.Drawing.Size(58, 13);
			this.lblMenuFontColor.TabIndex = 1;
			this.lblMenuFontColor.Text = "Menu Font";
			// 
			// butMenuSelectorBarColor
			// 
			this.butMenuSelectorBarColor.Location = new System.Drawing.Point(12, 37);
			this.butMenuSelectorBarColor.Name = "butMenuSelectorBarColor";
			this.butMenuSelectorBarColor.Size = new System.Drawing.Size(32, 20);
			this.butMenuSelectorBarColor.TabIndex = 4;
			this.butMenuSelectorBarColor.UseVisualStyleBackColor = true;
			this.butMenuSelectorBarColor.Click += new System.EventHandler(this.butMenuSelectorBarColor_Click);
			// 
			// grpGraphicsQuality
			// 
			this.grpGraphicsQuality.Controls.Add(this.chkUseHighQuality);
			this.grpGraphicsQuality.Location = new System.Drawing.Point(11, 135);
			this.grpGraphicsQuality.Name = "grpGraphicsQuality";
			this.grpGraphicsQuality.Size = new System.Drawing.Size(240, 47);
			this.grpGraphicsQuality.TabIndex = 39;
			this.grpGraphicsQuality.TabStop = false;
			this.grpGraphicsQuality.Text = "Graphics Quality";
			// 
			// chkUseHighQuality
			// 
			this.chkUseHighQuality.AutoSize = true;
			this.chkUseHighQuality.Location = new System.Drawing.Point(11, 19);
			this.chkUseHighQuality.Name = "chkUseHighQuality";
			this.chkUseHighQuality.Size = new System.Drawing.Size(105, 17);
			this.chkUseHighQuality.TabIndex = 0;
			this.chkUseHighQuality.Text = "Use High Quality";
			this.toolTip1.SetToolTip(this.chkUseHighQuality, "Renders any text in high quality");
			this.chkUseHighQuality.UseVisualStyleBackColor = true;
			// 
			// grpLabelOutline
			// 
			this.grpLabelOutline.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpLabelOutline.Controls.Add(this.butLabelOutlineColor);
			this.grpLabelOutline.Controls.Add(this.lblLabelOutlineSize);
			this.grpLabelOutline.Controls.Add(this.nudLabelOutlineSize);
			this.grpLabelOutline.Location = new System.Drawing.Point(262, 294);
			this.grpLabelOutline.Name = "grpLabelOutline";
			this.grpLabelOutline.Size = new System.Drawing.Size(240, 47);
			this.grpLabelOutline.TabIndex = 4;
			this.grpLabelOutline.TabStop = false;
			this.grpLabelOutline.Text = "Label Outline";
			// 
			// butLabelOutlineColor
			// 
			this.butLabelOutlineColor.Location = new System.Drawing.Point(88, 16);
			this.butLabelOutlineColor.Name = "butLabelOutlineColor";
			this.butLabelOutlineColor.Size = new System.Drawing.Size(32, 20);
			this.butLabelOutlineColor.TabIndex = 2;
			this.butLabelOutlineColor.UseVisualStyleBackColor = true;
			this.butLabelOutlineColor.Click += new System.EventHandler(this.butLabelOutlineColor_Click);
			// 
			// lblLabelOutlineSize
			// 
			this.lblLabelOutlineSize.AutoSize = true;
			this.lblLabelOutlineSize.Location = new System.Drawing.Point(131, 20);
			this.lblLabelOutlineSize.Name = "lblLabelOutlineSize";
			this.lblLabelOutlineSize.Size = new System.Drawing.Size(30, 13);
			this.lblLabelOutlineSize.TabIndex = 1;
			this.lblLabelOutlineSize.Text = "Size:";
			// 
			// nudLabelOutlineSize
			// 
			this.nudLabelOutlineSize.Location = new System.Drawing.Point(169, 16);
			this.nudLabelOutlineSize.Name = "nudLabelOutlineSize";
			this.nudLabelOutlineSize.ReadOnly = true;
			this.nudLabelOutlineSize.Size = new System.Drawing.Size(52, 20);
			this.nudLabelOutlineSize.TabIndex = 0;
			// 
			// grpSubScreen
			// 
			this.grpSubScreen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpSubScreen.Controls.Add(this.chkSubScreenDisable);
			this.grpSubScreen.Controls.Add(this.nudSubScreen);
			this.grpSubScreen.Controls.Add(this.lblSubScreenDelay);
			this.grpSubScreen.Controls.Add(this.chkSubScreen);
			this.grpSubScreen.Controls.Add(this.cboSubScreen);
			this.grpSubScreen.Location = new System.Drawing.Point(11, 59);
			this.grpSubScreen.Name = "grpSubScreen";
			this.grpSubScreen.Size = new System.Drawing.Size(491, 70);
			this.grpSubScreen.TabIndex = 38;
			this.grpSubScreen.TabStop = false;
			this.grpSubScreen.Text = "Sub Screen";
			// 
			// chkSubScreenDisable
			// 
			this.chkSubScreenDisable.AutoSize = true;
			this.chkSubScreenDisable.Location = new System.Drawing.Point(11, 40);
			this.chkSubScreenDisable.Name = "chkSubScreenDisable";
			this.chkSubScreenDisable.Size = new System.Drawing.Size(177, 17);
			this.chkSubScreenDisable.TabIndex = 12;
			this.chkSubScreenDisable.Text = "Disable For Dual Screen Games";
			this.chkSubScreenDisable.UseVisualStyleBackColor = true;
			// 
			// nudSubScreen
			// 
			this.nudSubScreen.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.nudSubScreen.Location = new System.Drawing.Point(166, 20);
			this.nudSubScreen.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.nudSubScreen.Name = "nudSubScreen";
			this.nudSubScreen.ReadOnly = true;
			this.nudSubScreen.Size = new System.Drawing.Size(71, 20);
			this.nudSubScreen.TabIndex = 10;
			// 
			// lblSubScreenDelay
			// 
			this.lblSubScreenDelay.AutoSize = true;
			this.lblSubScreenDelay.Location = new System.Drawing.Point(72, 24);
			this.lblSubScreenDelay.Name = "lblSubScreenDelay";
			this.lblSubScreenDelay.Size = new System.Drawing.Size(94, 13);
			this.lblSubScreenDelay.TabIndex = 11;
			this.lblSubScreenDelay.Text = "Rotate Delay (ms):";
			// 
			// chkSubScreen
			// 
			this.chkSubScreen.AutoSize = true;
			this.chkSubScreen.Location = new System.Drawing.Point(11, 23);
			this.chkSubScreen.Name = "chkSubScreen";
			this.chkSubScreen.Size = new System.Drawing.Size(59, 17);
			this.chkSubScreen.TabIndex = 9;
			this.chkSubScreen.Text = "Enable";
			this.chkSubScreen.UseVisualStyleBackColor = true;
			// 
			// cboSubScreen
			// 
			this.cboSubScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cboSubScreen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSubScreen.FormattingEnabled = true;
			this.cboSubScreen.Location = new System.Drawing.Point(261, 19);
			this.cboSubScreen.Name = "cboSubScreen";
			this.cboSubScreen.Size = new System.Drawing.Size(218, 21);
			this.cboSubScreen.TabIndex = 0;
			this.cboSubScreen.SelectedIndexChanged += new System.EventHandler(this.cboSubScreen_SelectedIndexChanged);
			// 
			// grpAutoRotation
			// 
			this.grpAutoRotation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpAutoRotation.Controls.Add(this.rdoRotateRight);
			this.grpAutoRotation.Controls.Add(this.rdoRotateLeft);
			this.grpAutoRotation.Controls.Add(this.chkAutoRotate);
			this.grpAutoRotation.Location = new System.Drawing.Point(262, 188);
			this.grpAutoRotation.Name = "grpAutoRotation";
			this.grpAutoRotation.Size = new System.Drawing.Size(240, 47);
			this.grpAutoRotation.TabIndex = 37;
			this.grpAutoRotation.TabStop = false;
			this.grpAutoRotation.Text = "Auto Rotation";
			// 
			// rdoRotateRight
			// 
			this.rdoRotateRight.AutoSize = true;
			this.rdoRotateRight.Location = new System.Drawing.Point(171, 19);
			this.rdoRotateRight.Name = "rdoRotateRight";
			this.rdoRotateRight.Size = new System.Drawing.Size(50, 17);
			this.rdoRotateRight.TabIndex = 10;
			this.rdoRotateRight.Text = "Right";
			this.rdoRotateRight.UseVisualStyleBackColor = true;
			// 
			// rdoRotateLeft
			// 
			this.rdoRotateLeft.AutoSize = true;
			this.rdoRotateLeft.Location = new System.Drawing.Point(118, 19);
			this.rdoRotateLeft.Name = "rdoRotateLeft";
			this.rdoRotateLeft.Size = new System.Drawing.Size(43, 17);
			this.rdoRotateLeft.TabIndex = 9;
			this.rdoRotateLeft.Text = "Left";
			this.rdoRotateLeft.UseVisualStyleBackColor = true;
			// 
			// chkAutoRotate
			// 
			this.chkAutoRotate.AutoSize = true;
			this.chkAutoRotate.Location = new System.Drawing.Point(11, 20);
			this.chkAutoRotate.Name = "chkAutoRotate";
			this.chkAutoRotate.Size = new System.Drawing.Size(59, 17);
			this.chkAutoRotate.TabIndex = 8;
			this.chkAutoRotate.Text = "Enable";
			this.chkAutoRotate.UseVisualStyleBackColor = true;
			// 
			// grpLoadingScreens
			// 
			this.grpLoadingScreens.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.grpLoadingScreens.Controls.Add(this.chkShowLoadingScreens);
			this.grpLoadingScreens.Location = new System.Drawing.Point(262, 135);
			this.grpLoadingScreens.Name = "grpLoadingScreens";
			this.grpLoadingScreens.Size = new System.Drawing.Size(240, 47);
			this.grpLoadingScreens.TabIndex = 35;
			this.grpLoadingScreens.TabStop = false;
			this.grpLoadingScreens.Text = "Loading Screens";
			// 
			// chkShowLoadingScreens
			// 
			this.chkShowLoadingScreens.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkShowLoadingScreens.AutoSize = true;
			this.chkShowLoadingScreens.Location = new System.Drawing.Point(11, 20);
			this.chkShowLoadingScreens.Name = "chkShowLoadingScreens";
			this.chkShowLoadingScreens.Size = new System.Drawing.Size(136, 17);
			this.chkShowLoadingScreens.TabIndex = 0;
			this.chkShowLoadingScreens.Text = "Show Loading Screens";
			this.chkShowLoadingScreens.UseVisualStyleBackColor = true;
			// 
			// grpDisplayChangeDelay
			// 
			this.grpDisplayChangeDelay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpDisplayChangeDelay.Controls.Add(this.chkDisplayChange);
			this.grpDisplayChangeDelay.Controls.Add(this.nudDisplayChangeDelay);
			this.grpDisplayChangeDelay.Location = new System.Drawing.Point(11, 188);
			this.grpDisplayChangeDelay.Name = "grpDisplayChangeDelay";
			this.grpDisplayChangeDelay.Size = new System.Drawing.Size(240, 47);
			this.grpDisplayChangeDelay.TabIndex = 34;
			this.grpDisplayChangeDelay.TabStop = false;
			this.grpDisplayChangeDelay.Text = "Display Change Delay (ms)";
			// 
			// chkDisplayChange
			// 
			this.chkDisplayChange.AutoSize = true;
			this.chkDisplayChange.Location = new System.Drawing.Point(11, 21);
			this.chkDisplayChange.Name = "chkDisplayChange";
			this.chkDisplayChange.Size = new System.Drawing.Size(59, 17);
			this.chkDisplayChange.TabIndex = 2;
			this.chkDisplayChange.Text = "Enable";
			this.chkDisplayChange.UseVisualStyleBackColor = true;
			// 
			// nudDisplayChangeDelay
			// 
			this.nudDisplayChangeDelay.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.nudDisplayChangeDelay.Location = new System.Drawing.Point(133, 17);
			this.nudDisplayChangeDelay.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.nudDisplayChangeDelay.Name = "nudDisplayChangeDelay";
			this.nudDisplayChangeDelay.ReadOnly = true;
			this.nudDisplayChangeDelay.Size = new System.Drawing.Size(88, 20);
			this.nudDisplayChangeDelay.TabIndex = 1;
			this.nudDisplayChangeDelay.ValueChanged += new System.EventHandler(this.nudDisplayChangeDelay_ValueChanged);
			// 
			// grpAlphaFade
			// 
			this.grpAlphaFade.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.grpAlphaFade.Controls.Add(this.lblAlphaFadeValue);
			this.grpAlphaFade.Controls.Add(this.nudAlphaFadeValue);
			this.grpAlphaFade.Controls.Add(this.chkAlphaFade);
			this.grpAlphaFade.Location = new System.Drawing.Point(11, 241);
			this.grpAlphaFade.Name = "grpAlphaFade";
			this.grpAlphaFade.Size = new System.Drawing.Size(240, 47);
			this.grpAlphaFade.TabIndex = 33;
			this.grpAlphaFade.TabStop = false;
			this.grpAlphaFade.Text = "Alpha Fade";
			// 
			// lblAlphaFadeValue
			// 
			this.lblAlphaFadeValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblAlphaFadeValue.AutoSize = true;
			this.lblAlphaFadeValue.Location = new System.Drawing.Point(122, 21);
			this.lblAlphaFadeValue.Name = "lblAlphaFadeValue";
			this.lblAlphaFadeValue.Size = new System.Drawing.Size(37, 13);
			this.lblAlphaFadeValue.TabIndex = 2;
			this.lblAlphaFadeValue.Text = "Value:";
			// 
			// nudAlphaFadeValue
			// 
			this.nudAlphaFadeValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.nudAlphaFadeValue.Location = new System.Drawing.Point(167, 18);
			this.nudAlphaFadeValue.Name = "nudAlphaFadeValue";
			this.nudAlphaFadeValue.ReadOnly = true;
			this.nudAlphaFadeValue.Size = new System.Drawing.Size(52, 20);
			this.nudAlphaFadeValue.TabIndex = 1;
			// 
			// chkAlphaFade
			// 
			this.chkAlphaFade.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkAlphaFade.AutoSize = true;
			this.chkAlphaFade.Location = new System.Drawing.Point(11, 21);
			this.chkAlphaFade.Name = "chkAlphaFade";
			this.chkAlphaFade.Size = new System.Drawing.Size(59, 17);
			this.chkAlphaFade.TabIndex = 0;
			this.chkAlphaFade.Text = "Enable";
			this.chkAlphaFade.UseVisualStyleBackColor = true;
			// 
			// grpLabelSpot
			// 
			this.grpLabelSpot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.grpLabelSpot.Controls.Add(this.butLabelSpotColor);
			this.grpLabelSpot.Controls.Add(this.lblLabelSpotSize);
			this.grpLabelSpot.Controls.Add(this.nudLabelSpotSize);
			this.grpLabelSpot.Controls.Add(this.chkLabelSpotShow);
			this.grpLabelSpot.Location = new System.Drawing.Point(11, 294);
			this.grpLabelSpot.Name = "grpLabelSpot";
			this.grpLabelSpot.Size = new System.Drawing.Size(240, 47);
			this.grpLabelSpot.TabIndex = 32;
			this.grpLabelSpot.TabStop = false;
			this.grpLabelSpot.Text = "Label Spot";
			// 
			// butLabelSpotColor
			// 
			this.butLabelSpotColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butLabelSpotColor.Location = new System.Drawing.Point(84, 17);
			this.butLabelSpotColor.Name = "butLabelSpotColor";
			this.butLabelSpotColor.Size = new System.Drawing.Size(32, 20);
			this.butLabelSpotColor.TabIndex = 4;
			this.butLabelSpotColor.UseVisualStyleBackColor = true;
			this.butLabelSpotColor.Click += new System.EventHandler(this.butLabelSpotColor_Click);
			// 
			// lblLabelSpotSize
			// 
			this.lblLabelSpotSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblLabelSpotSize.AutoSize = true;
			this.lblLabelSpotSize.Location = new System.Drawing.Point(129, 20);
			this.lblLabelSpotSize.Name = "lblLabelSpotSize";
			this.lblLabelSpotSize.Size = new System.Drawing.Size(30, 13);
			this.lblLabelSpotSize.TabIndex = 2;
			this.lblLabelSpotSize.Text = "Size:";
			// 
			// nudLabelSpotSize
			// 
			this.nudLabelSpotSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.nudLabelSpotSize.Location = new System.Drawing.Point(167, 17);
			this.nudLabelSpotSize.Name = "nudLabelSpotSize";
			this.nudLabelSpotSize.ReadOnly = true;
			this.nudLabelSpotSize.Size = new System.Drawing.Size(52, 20);
			this.nudLabelSpotSize.TabIndex = 1;
			// 
			// chkLabelSpotShow
			// 
			this.chkLabelSpotShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkLabelSpotShow.AutoSize = true;
			this.chkLabelSpotShow.Location = new System.Drawing.Point(11, 20);
			this.chkLabelSpotShow.Name = "chkLabelSpotShow";
			this.chkLabelSpotShow.Size = new System.Drawing.Size(53, 17);
			this.chkLabelSpotShow.TabIndex = 0;
			this.chkLabelSpotShow.Text = "Show";
			this.chkLabelSpotShow.UseVisualStyleBackColor = true;
			// 
			// grpLabelArrow
			// 
			this.grpLabelArrow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpLabelArrow.Controls.Add(this.butLabelArrowColor);
			this.grpLabelArrow.Controls.Add(this.lblLabelArrowSize);
			this.grpLabelArrow.Controls.Add(this.nudLabelArrowSize);
			this.grpLabelArrow.Controls.Add(this.chkLabelArrowShow);
			this.grpLabelArrow.Location = new System.Drawing.Point(262, 241);
			this.grpLabelArrow.Name = "grpLabelArrow";
			this.grpLabelArrow.Size = new System.Drawing.Size(240, 47);
			this.grpLabelArrow.TabIndex = 31;
			this.grpLabelArrow.TabStop = false;
			this.grpLabelArrow.Text = "Label Arrow";
			// 
			// butLabelArrowColor
			// 
			this.butLabelArrowColor.Location = new System.Drawing.Point(88, 16);
			this.butLabelArrowColor.Name = "butLabelArrowColor";
			this.butLabelArrowColor.Size = new System.Drawing.Size(32, 20);
			this.butLabelArrowColor.TabIndex = 3;
			this.butLabelArrowColor.UseVisualStyleBackColor = true;
			this.butLabelArrowColor.Click += new System.EventHandler(this.butLabelLinkColor_Click);
			// 
			// lblLabelArrowSize
			// 
			this.lblLabelArrowSize.AutoSize = true;
			this.lblLabelArrowSize.Location = new System.Drawing.Point(131, 20);
			this.lblLabelArrowSize.Name = "lblLabelArrowSize";
			this.lblLabelArrowSize.Size = new System.Drawing.Size(30, 13);
			this.lblLabelArrowSize.TabIndex = 2;
			this.lblLabelArrowSize.Text = "Size:";
			// 
			// nudLabelArrowSize
			// 
			this.nudLabelArrowSize.Location = new System.Drawing.Point(169, 17);
			this.nudLabelArrowSize.Name = "nudLabelArrowSize";
			this.nudLabelArrowSize.ReadOnly = true;
			this.nudLabelArrowSize.Size = new System.Drawing.Size(52, 20);
			this.nudLabelArrowSize.TabIndex = 1;
			// 
			// chkLabelArrowShow
			// 
			this.chkLabelArrowShow.AutoSize = true;
			this.chkLabelArrowShow.Location = new System.Drawing.Point(11, 20);
			this.chkLabelArrowShow.Name = "chkLabelArrowShow";
			this.chkLabelArrowShow.Size = new System.Drawing.Size(53, 17);
			this.chkLabelArrowShow.TabIndex = 0;
			this.chkLabelArrowShow.Text = "Show";
			this.chkLabelArrowShow.UseVisualStyleBackColor = true;
			// 
			// grpScreen
			// 
			this.grpScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.grpScreen.Controls.Add(this.cboDisplayScreen);
			this.grpScreen.Location = new System.Drawing.Point(262, 6);
			this.grpScreen.Name = "grpScreen";
			this.grpScreen.Size = new System.Drawing.Size(240, 47);
			this.grpScreen.TabIndex = 1;
			this.grpScreen.TabStop = false;
			this.grpScreen.Text = "Screen";
			// 
			// cboDisplayScreen
			// 
			this.cboDisplayScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cboDisplayScreen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboDisplayScreen.FormattingEnabled = true;
			this.cboDisplayScreen.Location = new System.Drawing.Point(10, 16);
			this.cboDisplayScreen.Name = "cboDisplayScreen";
			this.cboDisplayScreen.Size = new System.Drawing.Size(218, 21);
			this.cboDisplayScreen.TabIndex = 0;
			this.cboDisplayScreen.SelectedIndexChanged += new System.EventHandler(this.cboDisplayScreen_SelectedIndexChanged);
			// 
			// grpRotation
			// 
			this.grpRotation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpRotation.Controls.Add(this.chkFlipY);
			this.grpRotation.Controls.Add(this.chkFlipX);
			this.grpRotation.Controls.Add(this.cboDisplayRotation);
			this.grpRotation.Location = new System.Drawing.Point(11, 6);
			this.grpRotation.Name = "grpRotation";
			this.grpRotation.Size = new System.Drawing.Size(240, 47);
			this.grpRotation.TabIndex = 0;
			this.grpRotation.TabStop = false;
			this.grpRotation.Text = "Rotation";
			// 
			// chkFlipY
			// 
			this.chkFlipY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkFlipY.AutoSize = true;
			this.chkFlipY.Location = new System.Drawing.Point(180, 19);
			this.chkFlipY.Name = "chkFlipY";
			this.chkFlipY.Size = new System.Drawing.Size(49, 17);
			this.chkFlipY.TabIndex = 2;
			this.chkFlipY.Text = "FlipY";
			this.chkFlipY.UseVisualStyleBackColor = true;
			// 
			// chkFlipX
			// 
			this.chkFlipX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkFlipX.AutoSize = true;
			this.chkFlipX.Location = new System.Drawing.Point(124, 19);
			this.chkFlipX.Name = "chkFlipX";
			this.chkFlipX.Size = new System.Drawing.Size(49, 17);
			this.chkFlipX.TabIndex = 1;
			this.chkFlipX.Text = "FlipX";
			this.chkFlipX.UseVisualStyleBackColor = true;
			// 
			// cboDisplayRotation
			// 
			this.cboDisplayRotation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboDisplayRotation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboDisplayRotation.FormattingEnabled = true;
			this.cboDisplayRotation.Items.AddRange(new object[] {
            "No Rotation",
            "Rotate 90°",
            "Rotate 180°",
            "Rotate 270°"});
			this.cboDisplayRotation.Location = new System.Drawing.Point(12, 16);
			this.cboDisplayRotation.Name = "cboDisplayRotation";
			this.cboDisplayRotation.Size = new System.Drawing.Size(99, 21);
			this.cboDisplayRotation.TabIndex = 0;
			// 
			// tabData
			// 
			this.tabData.Controls.Add(this.grpAutoShowDataOptions);
			this.tabData.Controls.Add(this.grpEmulatorDataOptions);
			this.tabData.Controls.Add(this.grpMAMEDataOptions);
			this.tabData.Controls.Add(this.grpGeneralDataOptions);
			this.tabData.Location = new System.Drawing.Point(4, 22);
			this.tabData.Name = "tabData";
			this.tabData.Size = new System.Drawing.Size(514, 510);
			this.tabData.TabIndex = 10;
			this.tabData.Text = "Data";
			this.tabData.UseVisualStyleBackColor = true;
			// 
			// grpAutoShowDataOptions
			// 
			this.grpAutoShowDataOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpAutoShowDataOptions.Controls.Add(this.chkAutoShowExitToMenu);
			this.grpAutoShowDataOptions.Controls.Add(this.chkAutoShowShowCPOnly);
			this.grpAutoShowDataOptions.Location = new System.Drawing.Point(11, 64);
			this.grpAutoShowDataOptions.Name = "grpAutoShowDataOptions";
			this.grpAutoShowDataOptions.Size = new System.Drawing.Size(494, 52);
			this.grpAutoShowDataOptions.TabIndex = 29;
			this.grpAutoShowDataOptions.TabStop = false;
			this.grpAutoShowDataOptions.Text = "Auto Show Data Options";
			// 
			// chkAutoShowExitToMenu
			// 
			this.chkAutoShowExitToMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkAutoShowExitToMenu.AutoSize = true;
			this.chkAutoShowExitToMenu.Location = new System.Drawing.Point(276, 22);
			this.chkAutoShowExitToMenu.Name = "chkAutoShowExitToMenu";
			this.chkAutoShowExitToMenu.Size = new System.Drawing.Size(89, 17);
			this.chkAutoShowExitToMenu.TabIndex = 2;
			this.chkAutoShowExitToMenu.Text = "Exit To Menu";
			this.chkAutoShowExitToMenu.UseVisualStyleBackColor = true;
			// 
			// chkAutoShowShowCPOnly
			// 
			this.chkAutoShowShowCPOnly.AutoSize = true;
			this.chkAutoShowShowCPOnly.Location = new System.Drawing.Point(35, 22);
			this.chkAutoShowShowCPOnly.Name = "chkAutoShowShowCPOnly";
			this.chkAutoShowShowCPOnly.Size = new System.Drawing.Size(94, 17);
			this.chkAutoShowShowCPOnly.TabIndex = 1;
			this.chkAutoShowShowCPOnly.Text = "Show CP Only";
			this.chkAutoShowShowCPOnly.UseVisualStyleBackColor = true;
			// 
			// grpEmulatorDataOptions
			// 
			this.grpEmulatorDataOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpEmulatorDataOptions.Controls.Add(this.chkEmulatorIRC);
			this.grpEmulatorDataOptions.Controls.Add(this.chkNFO);
			this.grpEmulatorDataOptions.Controls.Add(this.chkOperationCard);
			this.grpEmulatorDataOptions.Controls.Add(this.chkEmulatorManual);
			this.grpEmulatorDataOptions.Controls.Add(this.chkEmulatorArtwork);
			this.grpEmulatorDataOptions.Controls.Add(this.chkEmulatorCP);
			this.grpEmulatorDataOptions.Location = new System.Drawing.Point(11, 238);
			this.grpEmulatorDataOptions.Name = "grpEmulatorDataOptions";
			this.grpEmulatorDataOptions.Size = new System.Drawing.Size(494, 111);
			this.grpEmulatorDataOptions.TabIndex = 28;
			this.grpEmulatorDataOptions.TabStop = false;
			this.grpEmulatorDataOptions.Text = "Emulator Data Options";
			// 
			// chkEmulatorIRC
			// 
			this.chkEmulatorIRC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkEmulatorIRC.AutoSize = true;
			this.chkEmulatorIRC.Location = new System.Drawing.Point(276, 22);
			this.chkEmulatorIRC.Name = "chkEmulatorIRC";
			this.chkEmulatorIRC.Size = new System.Drawing.Size(44, 17);
			this.chkEmulatorIRC.TabIndex = 12;
			this.chkEmulatorIRC.Text = "IRC";
			this.chkEmulatorIRC.UseVisualStyleBackColor = true;
			// 
			// chkNFO
			// 
			this.chkNFO.AutoSize = true;
			this.chkNFO.Location = new System.Drawing.Point(35, 86);
			this.chkNFO.Name = "chkNFO";
			this.chkNFO.Size = new System.Drawing.Size(48, 17);
			this.chkNFO.TabIndex = 11;
			this.chkNFO.Text = "NFO";
			this.chkNFO.UseVisualStyleBackColor = true;
			// 
			// chkOperationCard
			// 
			this.chkOperationCard.AutoSize = true;
			this.chkOperationCard.Location = new System.Drawing.Point(35, 70);
			this.chkOperationCard.Name = "chkOperationCard";
			this.chkOperationCard.Size = new System.Drawing.Size(97, 17);
			this.chkOperationCard.TabIndex = 10;
			this.chkOperationCard.Text = "Operation Card";
			this.chkOperationCard.UseVisualStyleBackColor = true;
			// 
			// chkEmulatorManual
			// 
			this.chkEmulatorManual.AutoSize = true;
			this.chkEmulatorManual.Location = new System.Drawing.Point(35, 54);
			this.chkEmulatorManual.Name = "chkEmulatorManual";
			this.chkEmulatorManual.Size = new System.Drawing.Size(61, 17);
			this.chkEmulatorManual.TabIndex = 9;
			this.chkEmulatorManual.Text = "Manual";
			this.chkEmulatorManual.UseVisualStyleBackColor = true;
			// 
			// chkEmulatorArtwork
			// 
			this.chkEmulatorArtwork.AutoSize = true;
			this.chkEmulatorArtwork.Location = new System.Drawing.Point(35, 38);
			this.chkEmulatorArtwork.Name = "chkEmulatorArtwork";
			this.chkEmulatorArtwork.Size = new System.Drawing.Size(62, 17);
			this.chkEmulatorArtwork.TabIndex = 8;
			this.chkEmulatorArtwork.Text = "Artwork";
			this.chkEmulatorArtwork.UseVisualStyleBackColor = true;
			// 
			// chkEmulatorCP
			// 
			this.chkEmulatorCP.AutoSize = true;
			this.chkEmulatorCP.Location = new System.Drawing.Point(35, 22);
			this.chkEmulatorCP.Name = "chkEmulatorCP";
			this.chkEmulatorCP.Size = new System.Drawing.Size(89, 17);
			this.chkEmulatorCP.TabIndex = 2;
			this.chkEmulatorCP.Text = "Control Panel";
			this.chkEmulatorCP.UseVisualStyleBackColor = true;
			// 
			// grpMAMEDataOptions
			// 
			this.grpMAMEDataOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpMAMEDataOptions.Controls.Add(this.chkMyHighScore);
			this.grpMAMEDataOptions.Controls.Add(this.chkMAMEIRC);
			this.grpMAMEDataOptions.Controls.Add(this.chkMAMEManual);
			this.grpMAMEDataOptions.Controls.Add(this.chkMAMEArtwork);
			this.grpMAMEDataOptions.Controls.Add(this.chkHighScore);
			this.grpMAMEDataOptions.Controls.Add(this.chkControlInfo);
			this.grpMAMEDataOptions.Controls.Add(this.chkMAMEInfo);
			this.grpMAMEDataOptions.Controls.Add(this.chkGameHistory);
			this.grpMAMEDataOptions.Controls.Add(this.chkGameInfo);
			this.grpMAMEDataOptions.Controls.Add(this.chkMAMECP);
			this.grpMAMEDataOptions.Location = new System.Drawing.Point(11, 122);
			this.grpMAMEDataOptions.Name = "grpMAMEDataOptions";
			this.grpMAMEDataOptions.Size = new System.Drawing.Size(494, 110);
			this.grpMAMEDataOptions.TabIndex = 27;
			this.grpMAMEDataOptions.TabStop = false;
			this.grpMAMEDataOptions.Text = "MAME Data Options";
			// 
			// chkMyHighScore
			// 
			this.chkMyHighScore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkMyHighScore.AutoSize = true;
			this.chkMyHighScore.Location = new System.Drawing.Point(276, 39);
			this.chkMyHighScore.Name = "chkMyHighScore";
			this.chkMyHighScore.Size = new System.Drawing.Size(101, 17);
			this.chkMyHighScore.TabIndex = 10;
			this.chkMyHighScore.Text = "My High Scores";
			this.chkMyHighScore.UseVisualStyleBackColor = true;
			// 
			// chkMAMEIRC
			// 
			this.chkMAMEIRC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkMAMEIRC.AutoSize = true;
			this.chkMAMEIRC.Location = new System.Drawing.Point(276, 87);
			this.chkMAMEIRC.Name = "chkMAMEIRC";
			this.chkMAMEIRC.Size = new System.Drawing.Size(44, 17);
			this.chkMAMEIRC.TabIndex = 9;
			this.chkMAMEIRC.Text = "IRC";
			this.chkMAMEIRC.UseVisualStyleBackColor = true;
			// 
			// chkMAMEManual
			// 
			this.chkMAMEManual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkMAMEManual.AutoSize = true;
			this.chkMAMEManual.Location = new System.Drawing.Point(276, 71);
			this.chkMAMEManual.Name = "chkMAMEManual";
			this.chkMAMEManual.Size = new System.Drawing.Size(61, 17);
			this.chkMAMEManual.TabIndex = 8;
			this.chkMAMEManual.Text = "Manual";
			this.chkMAMEManual.UseVisualStyleBackColor = true;
			// 
			// chkMAMEArtwork
			// 
			this.chkMAMEArtwork.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkMAMEArtwork.AutoSize = true;
			this.chkMAMEArtwork.Location = new System.Drawing.Point(276, 55);
			this.chkMAMEArtwork.Name = "chkMAMEArtwork";
			this.chkMAMEArtwork.Size = new System.Drawing.Size(62, 17);
			this.chkMAMEArtwork.TabIndex = 7;
			this.chkMAMEArtwork.Text = "Artwork";
			this.chkMAMEArtwork.UseVisualStyleBackColor = true;
			// 
			// chkHighScore
			// 
			this.chkHighScore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkHighScore.AutoSize = true;
			this.chkHighScore.Location = new System.Drawing.Point(276, 23);
			this.chkHighScore.Name = "chkHighScore";
			this.chkHighScore.Size = new System.Drawing.Size(84, 17);
			this.chkHighScore.TabIndex = 6;
			this.chkHighScore.Text = "High Scores";
			this.chkHighScore.UseVisualStyleBackColor = true;
			// 
			// chkControlInfo
			// 
			this.chkControlInfo.AutoSize = true;
			this.chkControlInfo.Location = new System.Drawing.Point(35, 87);
			this.chkControlInfo.Name = "chkControlInfo";
			this.chkControlInfo.Size = new System.Drawing.Size(80, 17);
			this.chkControlInfo.TabIndex = 5;
			this.chkControlInfo.Text = "Control Info";
			this.chkControlInfo.UseVisualStyleBackColor = true;
			// 
			// chkMAMEInfo
			// 
			this.chkMAMEInfo.AutoSize = true;
			this.chkMAMEInfo.Location = new System.Drawing.Point(35, 71);
			this.chkMAMEInfo.Name = "chkMAMEInfo";
			this.chkMAMEInfo.Size = new System.Drawing.Size(79, 17);
			this.chkMAMEInfo.TabIndex = 4;
			this.chkMAMEInfo.Text = "MAME Info";
			this.chkMAMEInfo.UseVisualStyleBackColor = true;
			// 
			// chkGameHistory
			// 
			this.chkGameHistory.AutoSize = true;
			this.chkGameHistory.Location = new System.Drawing.Point(35, 55);
			this.chkGameHistory.Name = "chkGameHistory";
			this.chkGameHistory.Size = new System.Drawing.Size(89, 17);
			this.chkGameHistory.TabIndex = 3;
			this.chkGameHistory.Text = "Game History";
			this.chkGameHistory.UseVisualStyleBackColor = true;
			// 
			// chkGameInfo
			// 
			this.chkGameInfo.AutoSize = true;
			this.chkGameInfo.Location = new System.Drawing.Point(35, 39);
			this.chkGameInfo.Name = "chkGameInfo";
			this.chkGameInfo.Size = new System.Drawing.Size(75, 17);
			this.chkGameInfo.TabIndex = 2;
			this.chkGameInfo.Text = "Game Info";
			this.chkGameInfo.UseVisualStyleBackColor = true;
			// 
			// chkMAMECP
			// 
			this.chkMAMECP.AutoSize = true;
			this.chkMAMECP.Location = new System.Drawing.Point(35, 23);
			this.chkMAMECP.Name = "chkMAMECP";
			this.chkMAMECP.Size = new System.Drawing.Size(89, 17);
			this.chkMAMECP.TabIndex = 1;
			this.chkMAMECP.Text = "Control Panel";
			this.chkMAMECP.UseVisualStyleBackColor = true;
			// 
			// grpGeneralDataOptions
			// 
			this.grpGeneralDataOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpGeneralDataOptions.Controls.Add(this.chkExitToMenu);
			this.grpGeneralDataOptions.Controls.Add(this.chkShowCPOnly);
			this.grpGeneralDataOptions.Location = new System.Drawing.Point(11, 6);
			this.grpGeneralDataOptions.Name = "grpGeneralDataOptions";
			this.grpGeneralDataOptions.Size = new System.Drawing.Size(494, 52);
			this.grpGeneralDataOptions.TabIndex = 26;
			this.grpGeneralDataOptions.TabStop = false;
			this.grpGeneralDataOptions.Text = "General Data Options";
			// 
			// chkExitToMenu
			// 
			this.chkExitToMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkExitToMenu.AutoSize = true;
			this.chkExitToMenu.Location = new System.Drawing.Point(276, 22);
			this.chkExitToMenu.Name = "chkExitToMenu";
			this.chkExitToMenu.Size = new System.Drawing.Size(89, 17);
			this.chkExitToMenu.TabIndex = 2;
			this.chkExitToMenu.Text = "Exit To Menu";
			this.chkExitToMenu.UseVisualStyleBackColor = true;
			// 
			// chkShowCPOnly
			// 
			this.chkShowCPOnly.AutoSize = true;
			this.chkShowCPOnly.Location = new System.Drawing.Point(35, 22);
			this.chkShowCPOnly.Name = "chkShowCPOnly";
			this.chkShowCPOnly.Size = new System.Drawing.Size(94, 17);
			this.chkShowCPOnly.TabIndex = 1;
			this.chkShowCPOnly.Text = "Show CP Only";
			this.chkShowCPOnly.UseVisualStyleBackColor = true;
			// 
			// tabIRC
			// 
			this.tabIRC.Controls.Add(this.grpIRCIsInvisible);
			this.tabIRC.Controls.Add(this.grpIRCRealName);
			this.tabIRC.Controls.Add(this.grpIRCUserName);
			this.tabIRC.Controls.Add(this.grpIRCNickName);
			this.tabIRC.Controls.Add(this.grpIRCChannel);
			this.tabIRC.Controls.Add(this.grpIRCPort);
			this.tabIRC.Controls.Add(this.grpIRCServer);
			this.tabIRC.Location = new System.Drawing.Point(4, 22);
			this.tabIRC.Name = "tabIRC";
			this.tabIRC.Padding = new System.Windows.Forms.Padding(3);
			this.tabIRC.Size = new System.Drawing.Size(514, 510);
			this.tabIRC.TabIndex = 7;
			this.tabIRC.Text = "IRC";
			this.tabIRC.UseVisualStyleBackColor = true;
			// 
			// grpIRCIsInvisible
			// 
			this.grpIRCIsInvisible.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.grpIRCIsInvisible.Controls.Add(this.chkIRCIsInvisible);
			this.grpIRCIsInvisible.Location = new System.Drawing.Point(262, 177);
			this.grpIRCIsInvisible.Name = "grpIRCIsInvisible";
			this.grpIRCIsInvisible.Size = new System.Drawing.Size(240, 51);
			this.grpIRCIsInvisible.TabIndex = 27;
			this.grpIRCIsInvisible.TabStop = false;
			this.grpIRCIsInvisible.Text = "Visibility";
			// 
			// chkIRCIsInvisible
			// 
			this.chkIRCIsInvisible.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkIRCIsInvisible.AutoSize = true;
			this.chkIRCIsInvisible.Location = new System.Drawing.Point(19, 23);
			this.chkIRCIsInvisible.Name = "chkIRCIsInvisible";
			this.chkIRCIsInvisible.Size = new System.Drawing.Size(75, 17);
			this.chkIRCIsInvisible.TabIndex = 0;
			this.chkIRCIsInvisible.Text = "Is Invisible";
			this.chkIRCIsInvisible.UseVisualStyleBackColor = true;
			// 
			// grpIRCRealName
			// 
			this.grpIRCRealName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpIRCRealName.Controls.Add(this.txtIRCRealName);
			this.grpIRCRealName.Location = new System.Drawing.Point(11, 177);
			this.grpIRCRealName.Name = "grpIRCRealName";
			this.grpIRCRealName.Size = new System.Drawing.Size(240, 51);
			this.grpIRCRealName.TabIndex = 26;
			this.grpIRCRealName.TabStop = false;
			this.grpIRCRealName.Text = "RealName";
			// 
			// txtIRCRealName
			// 
			this.txtIRCRealName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtIRCRealName.Location = new System.Drawing.Point(12, 20);
			this.txtIRCRealName.Name = "txtIRCRealName";
			this.txtIRCRealName.Size = new System.Drawing.Size(218, 20);
			this.txtIRCRealName.TabIndex = 0;
			// 
			// grpIRCUserName
			// 
			this.grpIRCUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.grpIRCUserName.Controls.Add(this.txtIRCUserName);
			this.grpIRCUserName.Location = new System.Drawing.Point(262, 120);
			this.grpIRCUserName.Name = "grpIRCUserName";
			this.grpIRCUserName.Size = new System.Drawing.Size(240, 51);
			this.grpIRCUserName.TabIndex = 25;
			this.grpIRCUserName.TabStop = false;
			this.grpIRCUserName.Text = "UserName";
			// 
			// txtIRCUserName
			// 
			this.txtIRCUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtIRCUserName.Location = new System.Drawing.Point(12, 20);
			this.txtIRCUserName.Name = "txtIRCUserName";
			this.txtIRCUserName.Size = new System.Drawing.Size(218, 20);
			this.txtIRCUserName.TabIndex = 0;
			// 
			// grpIRCNickName
			// 
			this.grpIRCNickName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpIRCNickName.Controls.Add(this.txtIRCNickName);
			this.grpIRCNickName.Location = new System.Drawing.Point(11, 120);
			this.grpIRCNickName.Name = "grpIRCNickName";
			this.grpIRCNickName.Size = new System.Drawing.Size(240, 51);
			this.grpIRCNickName.TabIndex = 24;
			this.grpIRCNickName.TabStop = false;
			this.grpIRCNickName.Text = "NickName";
			// 
			// txtIRCNickName
			// 
			this.txtIRCNickName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtIRCNickName.Location = new System.Drawing.Point(12, 20);
			this.txtIRCNickName.Name = "txtIRCNickName";
			this.txtIRCNickName.Size = new System.Drawing.Size(218, 20);
			this.txtIRCNickName.TabIndex = 0;
			// 
			// grpIRCChannel
			// 
			this.grpIRCChannel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpIRCChannel.Controls.Add(this.txtIRCChannel);
			this.grpIRCChannel.Location = new System.Drawing.Point(11, 63);
			this.grpIRCChannel.Name = "grpIRCChannel";
			this.grpIRCChannel.Size = new System.Drawing.Size(240, 51);
			this.grpIRCChannel.TabIndex = 23;
			this.grpIRCChannel.TabStop = false;
			this.grpIRCChannel.Text = "Channel";
			// 
			// txtIRCChannel
			// 
			this.txtIRCChannel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtIRCChannel.Location = new System.Drawing.Point(12, 20);
			this.txtIRCChannel.Name = "txtIRCChannel";
			this.txtIRCChannel.Size = new System.Drawing.Size(218, 20);
			this.txtIRCChannel.TabIndex = 0;
			// 
			// grpIRCPort
			// 
			this.grpIRCPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.grpIRCPort.Controls.Add(this.txtIRCPort);
			this.grpIRCPort.Location = new System.Drawing.Point(262, 6);
			this.grpIRCPort.Name = "grpIRCPort";
			this.grpIRCPort.Size = new System.Drawing.Size(240, 51);
			this.grpIRCPort.TabIndex = 22;
			this.grpIRCPort.TabStop = false;
			this.grpIRCPort.Text = "Port";
			// 
			// txtIRCPort
			// 
			this.txtIRCPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtIRCPort.Location = new System.Drawing.Point(12, 20);
			this.txtIRCPort.Name = "txtIRCPort";
			this.txtIRCPort.Size = new System.Drawing.Size(218, 20);
			this.txtIRCPort.TabIndex = 0;
			// 
			// grpIRCServer
			// 
			this.grpIRCServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpIRCServer.Controls.Add(this.txtIRCServer);
			this.grpIRCServer.Location = new System.Drawing.Point(11, 6);
			this.grpIRCServer.Name = "grpIRCServer";
			this.grpIRCServer.Size = new System.Drawing.Size(240, 51);
			this.grpIRCServer.TabIndex = 21;
			this.grpIRCServer.TabStop = false;
			this.grpIRCServer.Text = "Server";
			// 
			// txtIRCServer
			// 
			this.txtIRCServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtIRCServer.Location = new System.Drawing.Point(12, 20);
			this.txtIRCServer.Name = "txtIRCServer";
			this.txtIRCServer.Size = new System.Drawing.Size(218, 20);
			this.txtIRCServer.TabIndex = 0;
			// 
			// butOk
			// 
			this.butOk.Location = new System.Drawing.Point(3, 3);
			this.butOk.Name = "butOk";
			this.butOk.Size = new System.Drawing.Size(102, 28);
			this.butOk.TabIndex = 1;
			this.butOk.Text = "OK";
			this.butOk.UseVisualStyleBackColor = true;
			this.butOk.Click += new System.EventHandler(this.butOk_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(137, 19);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(27, 21);
			this.button1.TabIndex = 1;
			this.button1.Text = "...";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// textBox1
			// 
			this.textBox1.Enabled = false;
			this.textBox1.Location = new System.Drawing.Point(12, 20);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(124, 20);
			this.textBox1.TabIndex = 0;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(137, 19);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(27, 21);
			this.button2.TabIndex = 1;
			this.button2.Text = "...";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// textBox2
			// 
			this.textBox2.Enabled = false;
			this.textBox2.Location = new System.Drawing.Point(12, 20);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(124, 20);
			this.textBox2.TabIndex = 0;
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(11, 19);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(151, 21);
			this.comboBox1.TabIndex = 0;
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(12, 21);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(146, 17);
			this.checkBox1.TabIndex = 29;
			this.checkBox1.Text = "Search Using Executable";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// checkBox2
			// 
			this.checkBox2.AutoSize = true;
			this.checkBox2.Location = new System.Drawing.Point(193, 21);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(108, 17);
			this.checkBox2.TabIndex = 1;
			this.checkBox2.Text = "Take Screenshot";
			this.checkBox2.UseVisualStyleBackColor = true;
			// 
			// butCancel
			// 
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Location = new System.Drawing.Point(417, 3);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(102, 28);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "Cancel";
			this.butCancel.UseVisualStyleBackColor = true;
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.butOk);
			this.panel1.Controls.Add(this.butCancel);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 501);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(522, 35);
			this.panel1.TabIndex = 3;
			// 
			// frmOptions
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(522, 536);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.tabControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmOptions";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Options";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OptionsForm_FormClosed);
			this.Load += new System.EventHandler(this.OptionsForm_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.grpVerboseLogging.ResumeLayout(false);
			this.grpVerboseLogging.PerformLayout();
			this.grpDynamicDataLoading.ResumeLayout(false);
			this.grpDynamicDataLoading.PerformLayout();
			this.grpGhostScriptExe.ResumeLayout(false);
			this.grpGhostScriptExe.PerformLayout();
			this.grpVolumeControl.ResumeLayout(false);
			this.grpVolumeControl.PerformLayout();
			this.grpHideDesktopOptions.ResumeLayout(false);
			this.grpHideDesktopOptions.PerformLayout();
			this.grpStartupOptions.ResumeLayout(false);
			this.grpStartupOptions.PerformLayout();
			this.tabLayout.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.grpShowRetry.ResumeLayout(false);
			this.grpShowRetry.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudShowRetryNumRetrys)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudShowRetryInterval)).EndInit();
			this.grpLayoutSub.ResumeLayout(false);
			this.grpLayoutSub.PerformLayout();
			this.grpLayoutOptions.ResumeLayout(false);
			this.grpLayoutOptions.PerformLayout();
			this.grpLayoutColors.ResumeLayout(false);
			this.grpLayoutColors.PerformLayout();
			this.grpLayoutBackground.ResumeLayout(false);
			this.grpLayoutBackground.PerformLayout();
			this.grpLayoutName.ResumeLayout(false);
			this.grpLayoutName.PerformLayout();
			this.tabMAME.ResumeLayout(false);
			this.tabMAME.PerformLayout();
			this.grpMAMEBak.ResumeLayout(false);
			this.grpMAMEBak.PerformLayout();
			this.grpAutoShow.ResumeLayout(false);
			this.grpAutoShow.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudAutoShowTimeout)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudAutoShowDelay)).EndInit();
			this.grpMAMELayoutSub.ResumeLayout(false);
			this.grpMAMELayoutSub.PerformLayout();
			this.grpMAMELayoutOverride.ResumeLayout(false);
			this.grpMAMELayoutOverride.PerformLayout();
			this.grpPauseOptions.ResumeLayout(false);
			this.grpPauseOptions.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.grpLayoutMaps.ResumeLayout(false);
			this.grpMAMELayout.ResumeLayout(false);
			this.grpMAMELayout.PerformLayout();
			this.grpMAMEOptions.ResumeLayout(false);
			this.grpMAMEOptions.PerformLayout();
			this.tabMAMEFilters.ResumeLayout(false);
			this.grpMAMEFilters.ResumeLayout(false);
			this.grpMAMEFilters.PerformLayout();
			this.tabMAMEPaths.ResumeLayout(false);
			this.grpMAMEPaths.ResumeLayout(false);
			this.grpMAMEPaths.PerformLayout();
			this.tabProfiles.ResumeLayout(false);
			this.grpProfiles.ResumeLayout(false);
			this.tabInput.ResumeLayout(false);
			this.grpInput.ResumeLayout(false);
			this.grpInput.PerformLayout();
			this.tabDisplay.ResumeLayout(false);
			this.grpMenuOptions.ResumeLayout(false);
			this.grpMenuOptions.PerformLayout();
			this.grpChangeBackgrounds.ResumeLayout(false);
			this.grpMenuColors.ResumeLayout(false);
			this.grpMenuColors.PerformLayout();
			this.grpGraphicsQuality.ResumeLayout(false);
			this.grpGraphicsQuality.PerformLayout();
			this.grpLabelOutline.ResumeLayout(false);
			this.grpLabelOutline.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudLabelOutlineSize)).EndInit();
			this.grpSubScreen.ResumeLayout(false);
			this.grpSubScreen.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudSubScreen)).EndInit();
			this.grpAutoRotation.ResumeLayout(false);
			this.grpAutoRotation.PerformLayout();
			this.grpLoadingScreens.ResumeLayout(false);
			this.grpLoadingScreens.PerformLayout();
			this.grpDisplayChangeDelay.ResumeLayout(false);
			this.grpDisplayChangeDelay.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudDisplayChangeDelay)).EndInit();
			this.grpAlphaFade.ResumeLayout(false);
			this.grpAlphaFade.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudAlphaFadeValue)).EndInit();
			this.grpLabelSpot.ResumeLayout(false);
			this.grpLabelSpot.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudLabelSpotSize)).EndInit();
			this.grpLabelArrow.ResumeLayout(false);
			this.grpLabelArrow.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudLabelArrowSize)).EndInit();
			this.grpScreen.ResumeLayout(false);
			this.grpRotation.ResumeLayout(false);
			this.grpRotation.PerformLayout();
			this.tabData.ResumeLayout(false);
			this.grpAutoShowDataOptions.ResumeLayout(false);
			this.grpAutoShowDataOptions.PerformLayout();
			this.grpEmulatorDataOptions.ResumeLayout(false);
			this.grpEmulatorDataOptions.PerformLayout();
			this.grpMAMEDataOptions.ResumeLayout(false);
			this.grpMAMEDataOptions.PerformLayout();
			this.grpGeneralDataOptions.ResumeLayout(false);
			this.grpGeneralDataOptions.PerformLayout();
			this.tabIRC.ResumeLayout(false);
			this.grpIRCIsInvisible.ResumeLayout(false);
			this.grpIRCIsInvisible.PerformLayout();
			this.grpIRCRealName.ResumeLayout(false);
			this.grpIRCRealName.PerformLayout();
			this.grpIRCUserName.ResumeLayout(false);
			this.grpIRCUserName.PerformLayout();
			this.grpIRCNickName.ResumeLayout(false);
			this.grpIRCNickName.PerformLayout();
			this.grpIRCChannel.ResumeLayout(false);
			this.grpIRCChannel.PerformLayout();
			this.grpIRCPort.ResumeLayout(false);
			this.grpIRCPort.PerformLayout();
			this.grpIRCServer.ResumeLayout(false);
			this.grpIRCServer.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabDisplay;
		private System.Windows.Forms.TabPage tabMAMEPaths;
		private System.Windows.Forms.TabPage tabLayout;
		private System.Windows.Forms.GroupBox grpRotation;
		private System.Windows.Forms.ComboBox cboDisplayRotation;
		private System.Windows.Forms.GroupBox grpScreen;
		private System.Windows.Forms.ComboBox cboDisplayScreen;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.CheckBox chkRunOnStartup;
		private System.Windows.Forms.GroupBox grpMAMEPaths;
		private System.Windows.Forms.Button butMAMEExe;
		private System.Windows.Forms.TextBox txtMAMEExe;
		private System.Windows.Forms.Label lblTitles;
		private System.Windows.Forms.Button butTitles;
		private System.Windows.Forms.TextBox txtTitles;
		private System.Windows.Forms.Label lblSnap;
		private System.Windows.Forms.Button butSnap;
		private System.Windows.Forms.TextBox txtSnap;
		private System.Windows.Forms.Label lblMarquees;
		private System.Windows.Forms.Button butMarquees;
		private System.Windows.Forms.TextBox txtMarquees;
		private System.Windows.Forms.Label lblManuals;
		private System.Windows.Forms.Button butManuals;
		private System.Windows.Forms.TextBox txtManuals;
		private System.Windows.Forms.Label lblIni;
		private System.Windows.Forms.Button butIni;
		private System.Windows.Forms.TextBox txtIni;
		private System.Windows.Forms.Label lblIcons;
		private System.Windows.Forms.Button butIcons;
		private System.Windows.Forms.TextBox txtIcons;
		private System.Windows.Forms.Label lblFlyers;
		private System.Windows.Forms.Button butFlyers;
		private System.Windows.Forms.TextBox txtFlyers;
		private System.Windows.Forms.Label lblCtrlr;
		private System.Windows.Forms.Button butCtrlr;
		private System.Windows.Forms.TextBox txtCtrlr;
		private System.Windows.Forms.Label lblCPanel;
		private System.Windows.Forms.Button butCPanel;
		private System.Windows.Forms.TextBox txtCPanel;
		private System.Windows.Forms.Label lblCabinets;
		private System.Windows.Forms.Button butCabinets;
		private System.Windows.Forms.TextBox txtCabinets;
		private System.Windows.Forms.Label lblMAMEExe;
		private System.Windows.Forms.Button butOk;
		private System.Windows.Forms.Label lblCfg;
		private System.Windows.Forms.Button butCfg;
		private System.Windows.Forms.TextBox txtCfg;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TabPage tabInput;
		private System.Windows.Forms.Button butMenuRight;
		private System.Windows.Forms.TextBox txtMenuRight;
		private System.Windows.Forms.Button butMenuLeft;
		private System.Windows.Forms.TextBox txtMenuLeft;
		private System.Windows.Forms.Button butMenuDown;
		private System.Windows.Forms.TextBox txtMenuDown;
		private System.Windows.Forms.Button butMenuUp;
		private System.Windows.Forms.TextBox txtMenuUp;
		private System.Windows.Forms.Button butBackKey;
		private System.Windows.Forms.TextBox txtBackKey;
		private System.Windows.Forms.Button butSelectKey;
		private System.Windows.Forms.TextBox txtSelectKey;
		private System.Windows.Forms.Button butShowKey;
		private System.Windows.Forms.TextBox txtShowKey;
		private System.Windows.Forms.TabPage tabIRC;
		private System.Windows.Forms.GroupBox grpIRCPort;
		private System.Windows.Forms.TextBox txtIRCPort;
		private System.Windows.Forms.GroupBox grpIRCServer;
		private System.Windows.Forms.TextBox txtIRCServer;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.GroupBox grpIRCNickName;
		private System.Windows.Forms.TextBox txtIRCNickName;
		private System.Windows.Forms.GroupBox grpIRCChannel;
		private System.Windows.Forms.TextBox txtIRCChannel;
		private System.Windows.Forms.GroupBox grpIRCIsInvisible;
		private System.Windows.Forms.CheckBox chkIRCIsInvisible;
		private System.Windows.Forms.GroupBox grpIRCRealName;
		private System.Windows.Forms.TextBox txtIRCRealName;
		private System.Windows.Forms.GroupBox grpIRCUserName;
		private System.Windows.Forms.TextBox txtIRCUserName;
		private System.Windows.Forms.Button butVolumeUp;
		private System.Windows.Forms.TextBox txtVolumeUp;
		private System.Windows.Forms.Button butVolumeDown;
		private System.Windows.Forms.TextBox txtVolumeDown;
		private System.Windows.Forms.GroupBox grpLayoutBackground;
		private System.Windows.Forms.Button butLayoutBackground;
		private System.Windows.Forms.TextBox txtLayoutBackground;
		private System.Windows.Forms.GroupBox grpLayoutName;
		private System.Windows.Forms.TextBox txtLayoutName;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.GroupBox grpStartupOptions;
		private System.Windows.Forms.Label lblPCB;
		private System.Windows.Forms.Button butPCB;
		private System.Windows.Forms.TextBox txtPCB;
		private System.Windows.Forms.TabPage tabProfiles;
		private System.Windows.Forms.GroupBox grpProfiles;
		private System.Windows.Forms.TabPage tabMAME;
		private System.Windows.Forms.GroupBox grpMAMELayout;
		private System.Windows.Forms.Button butMAMELayout;
		private System.Windows.Forms.TextBox txtMAMELayout;
		private ListViewEx lvwProfiles;
		private System.Windows.Forms.ColumnHeader colProfileExecutable;
		private System.Windows.Forms.ColumnHeader colProfileWindowTitle;
		private System.Windows.Forms.ColumnHeader colProfileWindowClass;
		private System.Windows.Forms.ColumnHeader colProfileUseExe;
		private System.Windows.Forms.ColumnHeader colProfileScreenshot;
		private System.Windows.Forms.ColumnHeader colProfileLayoutOverride;
		private System.Windows.Forms.ColumnHeader colProfileShowKey;
		private System.Windows.Forms.ColumnHeader colProfileHideKey;
		private System.Windows.Forms.Button butProfileDelete;
		private System.Windows.Forms.Button butProfileNew;
		private System.Windows.Forms.ColumnHeader colProfileEnabled;
		private System.Windows.Forms.ColumnHeader colProfileName;
		private System.Windows.Forms.ColumnHeader colProfileLayout;
		private System.Windows.Forms.ColumnHeader colProfileMinimize;
		private System.Windows.Forms.ColumnHeader colProfileMaximize;
		private System.Windows.Forms.ColumnHeader colProfileShowSendKeys;
		private System.Windows.Forms.ColumnHeader colProfileHideSendKeys;
		private System.Windows.Forms.GroupBox grpMAMELayoutOverride;
		private System.Windows.Forms.Button butMAMELayoutOverride;
		private System.Windows.Forms.TextBox txtMAMELayoutOverride;
		private System.Windows.Forms.ColumnHeader colProfileLabels;
		private System.Windows.Forms.ColumnHeader colProfileDatabase;
		private System.Windows.Forms.GroupBox grpLayoutMaps;
		private System.Windows.Forms.CheckBox chkMAMESendPause;
		private System.Windows.Forms.GroupBox grpHideDesktopOptions;
		private System.Windows.Forms.CheckBox chkHideDesktopUsingForms;
		private System.Windows.Forms.CheckBox chkHideDesktopIcons;
		private System.Windows.Forms.CheckBox chkHideMouseCursor;
		private System.Windows.Forms.CheckBox chkHideDesktopEnable;
		private System.Windows.Forms.CheckBox chkSetWallpaperBlack;
		private System.Windows.Forms.Button butShowDesktop;
		private System.Windows.Forms.TextBox txtShowDesktop;
		private System.Windows.Forms.Button butHideDesktop;
		private System.Windows.Forms.TextBox txtHideDesktop;
		private System.Windows.Forms.GroupBox grpVolumeControl;
		private System.Windows.Forms.CheckBox chkVolumeControl;
		private System.Windows.Forms.CheckBox chkMoveMouseOffscreen;
		private System.Windows.Forms.CheckBox chkHideTaskbar;
		private System.Windows.Forms.CheckBox chkDisableScreenSaver;
		private System.Windows.Forms.Button butSelect;
		private System.Windows.Forms.Label lblSelect;
		private System.Windows.Forms.TextBox txtSelect;
		private System.Windows.Forms.Button butPreviews;
		private System.Windows.Forms.Label lblPreviews;
		private System.Windows.Forms.TextBox txtPreviews;
		private System.Windows.Forms.ColumnHeader colProfileManuals;
		private System.Windows.Forms.ColumnHeader colProfileOpCards;
		private System.Windows.Forms.ColumnHeader colProfileSnaps;
		private System.Windows.Forms.ColumnHeader colProfileTitles;
		private System.Windows.Forms.ColumnHeader colProfileCarts;
		private System.Windows.Forms.ColumnHeader colProfileBoxes;
		private System.Windows.Forms.GroupBox grpLayoutColors;
		private ListViewEx lvwLayoutColors;
		private System.Windows.Forms.ColumnHeader colColor;
		private System.Windows.Forms.ColumnHeader colImage;
		private System.Windows.Forms.GroupBox grpGhostScriptExe;
		private System.Windows.Forms.Button butGhostScriptExe;
		private System.Windows.Forms.TextBox txtGhostScriptExe;
		private ListViewEx lvwLayoutMaps;
		private System.Windows.Forms.ColumnHeader colLayoutMapControl;
		private System.Windows.Forms.ColumnHeader colLayoutMapLayout;
		private System.Windows.Forms.Button butLayoutMapDelete;
		private System.Windows.Forms.Button butLayoutMapNew;
		private System.Windows.Forms.CheckBox chkLayoutColorsEnable;
		private System.Windows.Forms.GroupBox grpInput;
		private System.Windows.Forms.Label lblShowKey;
		private System.Windows.Forms.Label lblHideDesktop;
		private System.Windows.Forms.Label lblVolumeUp;
		private System.Windows.Forms.Label lblVolumeDown;
		private System.Windows.Forms.Label lblMenuRight;
		private System.Windows.Forms.Label lblMenuLeft;
		private System.Windows.Forms.Label lblMenuDown;
		private System.Windows.Forms.Label lblMenuUp;
		private System.Windows.Forms.Label lblSelectKey;
		private System.Windows.Forms.Label lblBackKey;
		private System.Windows.Forms.Label lblShowDesktop;
		private System.Windows.Forms.ColumnHeader colLayoutMapEnabled;
		private System.Windows.Forms.GroupBox grpLabelArrow;
		private System.Windows.Forms.CheckBox chkLabelArrowShow;
		private System.Windows.Forms.Label lblLabelArrowSize;
		private System.Windows.Forms.NumericUpDown nudLabelArrowSize;
		private System.Windows.Forms.TabPage tabData;
		private System.Windows.Forms.GroupBox grpEmulatorDataOptions;
		private System.Windows.Forms.GroupBox grpMAMEDataOptions;
		private System.Windows.Forms.CheckBox chkMAMECP;
		private System.Windows.Forms.GroupBox grpGeneralDataOptions;
		private System.Windows.Forms.CheckBox chkShowCPOnly;
		private System.Windows.Forms.CheckBox chkMAMEManual;
		private System.Windows.Forms.CheckBox chkMAMEArtwork;
		private System.Windows.Forms.CheckBox chkHighScore;
		private System.Windows.Forms.CheckBox chkControlInfo;
		private System.Windows.Forms.CheckBox chkMAMEInfo;
		private System.Windows.Forms.CheckBox chkGameHistory;
		private System.Windows.Forms.CheckBox chkGameInfo;
		private System.Windows.Forms.CheckBox chkMAMEIRC;
		private System.Windows.Forms.CheckBox chkEmulatorIRC;
		private System.Windows.Forms.CheckBox chkNFO;
		private System.Windows.Forms.CheckBox chkOperationCard;
		private System.Windows.Forms.CheckBox chkEmulatorManual;
		private System.Windows.Forms.CheckBox chkEmulatorArtwork;
		private System.Windows.Forms.CheckBox chkEmulatorCP;
		private System.Windows.Forms.CheckBox chkBackKeyExitMenu;
		private System.Windows.Forms.ColumnHeader colProfileType;
		private System.Windows.Forms.GroupBox grpLabelSpot;
		private System.Windows.Forms.Label lblLabelSpotSize;
		private System.Windows.Forms.NumericUpDown nudLabelSpotSize;
		private System.Windows.Forms.CheckBox chkLabelSpotShow;
		private System.Windows.Forms.GroupBox grpAlphaFade;
		private System.Windows.Forms.Label lblAlphaFadeValue;
		private System.Windows.Forms.NumericUpDown nudAlphaFadeValue;
		private System.Windows.Forms.CheckBox chkAlphaFade;
		private System.Windows.Forms.Button butLabelArrowColor;
		private System.Windows.Forms.Button butLabelSpotColor;
		private System.Windows.Forms.ColumnHeader colLayoutMapNumPlayers;
		private System.Windows.Forms.ColumnHeader colLayoutMapAlternating;
		private System.Windows.Forms.ColumnHeader colLayoutMapConstant;
		private System.Windows.Forms.Button butLayoutMapDown;
		private System.Windows.Forms.Button butLayoutMapUp;
		private System.Windows.Forms.CheckBox chkExitToMenu;
		private System.Windows.Forms.Label lblExitKey;
		private System.Windows.Forms.Button butExitKey;
		private System.Windows.Forms.TextBox txtExitKey;
		private System.Windows.Forms.GroupBox grpVerboseLogging;
		private System.Windows.Forms.CheckBox chkVerboseLogging;
		private System.Windows.Forms.CheckBox chkEnableExitKey;
		private System.Windows.Forms.GroupBox grpDisplayChangeDelay;
		private System.Windows.Forms.NumericUpDown nudDisplayChangeDelay;
		private System.Windows.Forms.GroupBox grpDynamicDataLoading;
		private System.Windows.Forms.CheckBox chkDynamicDataLoading;
		private System.Windows.Forms.GroupBox grpLoadingScreens;
		private System.Windows.Forms.CheckBox chkShowLoadingScreens;
		private System.Windows.Forms.Label lblHi;
		private System.Windows.Forms.Button butHi;
		private System.Windows.Forms.TextBox txtHi;
		private System.Windows.Forms.Label lblNvRam;
		private System.Windows.Forms.Button butNvRam;
		private System.Windows.Forms.TextBox txtNvRam;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.CheckBox chkMyHighScore;
		private System.Windows.Forms.CheckBox chkAutoRotate;
		private System.Windows.Forms.GroupBox grpAutoRotation;
		private System.Windows.Forms.RadioButton rdoRotateRight;
		private System.Windows.Forms.RadioButton rdoRotateLeft;
		private System.Windows.Forms.RadioButton rdoMAMEPauseDiff;
		private System.Windows.Forms.RadioButton rdoMAMEPauseMsg;
		private System.Windows.Forms.RadioButton rdoMAMEPauseKey;
		private System.Windows.Forms.GroupBox grpPauseOptions;
		private System.Windows.Forms.CheckBox chkMAMEDetectPause;
		private System.Windows.Forms.GroupBox grpMAMELayoutSub;
		private System.Windows.Forms.CheckBox chkAutoShow;
		private System.Windows.Forms.GroupBox grpSubScreen;
		private System.Windows.Forms.ComboBox cboSubScreen;
		private System.Windows.Forms.NumericUpDown nudSubScreen;
		private System.Windows.Forms.CheckBox chkSubScreen;
		private System.Windows.Forms.Button butMAMELayoutSub;
		private System.Windows.Forms.TextBox txtMAMELayoutSub;
		private System.Windows.Forms.GroupBox grpAutoShow;
		private System.Windows.Forms.Label lblAutoShowDelay;
		private System.Windows.Forms.Label lblSubScreenDelay;
		private System.Windows.Forms.NumericUpDown nudAutoShowDelay;
		private System.Windows.Forms.NumericUpDown nudAutoShowTimeout;
		private System.Windows.Forms.Label lblAutoShowTimeout;
		private System.Windows.Forms.Label lblMAMEVersionValue;
		private System.Windows.Forms.Label lblMAMEVersion;
		private System.Windows.Forms.GroupBox grpLayoutOptions;
		private System.Windows.Forms.CheckBox chkShowMiniInfo;
		private System.Windows.Forms.GroupBox grpAutoShowDataOptions;
		private System.Windows.Forms.CheckBox chkAutoShowExitToMenu;
		private System.Windows.Forms.CheckBox chkAutoShowShowCPOnly;
		private System.Windows.Forms.CheckBox chkSubScreenDisable;
		private System.Windows.Forms.CheckBox chkDisplayChange;
		private System.Windows.Forms.GroupBox grpLayoutSub;
		private System.Windows.Forms.Button butLayoutSub;
		private System.Windows.Forms.TextBox txtLayoutSub;
		private System.Windows.Forms.ColumnHeader colProfileLayoutSub;
		private System.Windows.Forms.CheckBox chkFlipX;
		private System.Windows.Forms.CheckBox chkFlipY;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.CheckBox chkStopBackMenu;
		private System.Windows.Forms.GroupBox grpLabelOutline;
		private System.Windows.Forms.NumericUpDown nudLabelOutlineSize;
		private System.Windows.Forms.Label lblLabelOutlineSize;
		private System.Windows.Forms.Button butLabelOutlineColor;
		private System.Windows.Forms.CheckBox chkBackShowsCP;
		private System.Windows.Forms.TabPage tabMAMEFilters;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox chkShowLayoutMouseClick;
		private System.Windows.Forms.CheckBox chkShowLayoutGiveFocus;
		private System.Windows.Forms.CheckBox chkShowLayoutForceForeground;
		private System.Windows.Forms.CheckBox chkShowLayoutTopMost;
		private System.Windows.Forms.GroupBox grpShowRetry;
		private System.Windows.Forms.CheckBox chkShowRetryExitOnFail;
		private System.Windows.Forms.NumericUpDown nudShowRetryNumRetrys;
		private System.Windows.Forms.Label lblInterval;
		private System.Windows.Forms.NumericUpDown nudShowRetryInterval;
		private System.Windows.Forms.Label lblNumRetrys;
		private System.Windows.Forms.CheckBox chkShowRetry;
		private System.Windows.Forms.GroupBox grpMAMEFilters;
		private System.Windows.Forms.Label lblFilterRotation;
		private System.Windows.Forms.ComboBox cboFilterRotation;
		private System.Windows.Forms.CheckBox chkNoImperfect;
		private System.Windows.Forms.CheckBox chkNoPreliminary;
		private System.Windows.Forms.CheckBox chkNoSystemExceptChd;
		private System.Windows.Forms.CheckBox chkArcadeOnly;
		private System.Windows.Forms.CheckBox chkRunnableOnly;
		private System.Windows.Forms.CheckBox chkNoNotClassified;
		private System.Windows.Forms.CheckBox chkNoUtilities;
		private System.Windows.Forms.CheckBox chkNoReels;
		private System.Windows.Forms.CheckBox chkNoMechanical;
		private System.Windows.Forms.CheckBox chkNoCasino;
		private System.Windows.Forms.CheckBox chkNoGambling;
		private System.Windows.Forms.CheckBox chkNoMahjong;
		private System.Windows.Forms.CheckBox chkNoBios;
		private System.Windows.Forms.CheckBox chkNoAdult;
		private System.Windows.Forms.CheckBox chkNoClones;
		private System.Windows.Forms.CheckBox chkNoDevice;
		private System.Windows.Forms.Label lblDescriptionExcludes;
		private System.Windows.Forms.Label lblNameIncludes;
		private System.Windows.Forms.TextBox txtDescriptionExcludes;
		private System.Windows.Forms.TextBox txtNameIncludes;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkAllowBracketedGameNames;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox chkDisableSystemSpecs;
		private System.Windows.Forms.GroupBox grpGraphicsQuality;
		private System.Windows.Forms.CheckBox chkUseHighQuality;
		private System.Windows.Forms.GroupBox grpMAMEBak;
		private System.Windows.Forms.ComboBox cboMAMEBak;
		private System.Windows.Forms.Label lblMAMEBak;
		private System.Windows.Forms.GroupBox grpMAMEOptions;
		private System.Windows.Forms.CheckBox chkMAMESkipDisclaimer;
		private System.Windows.Forms.CheckBox chkMAMEScreenshot;
		private System.Windows.Forms.CheckBox chkMAMEUseShowKey;
		private System.Windows.Forms.CheckBox chkMAMEOutputSystem;
		private System.Windows.Forms.GroupBox grpChangeBackgrounds;
		private System.Windows.Forms.Button butChangeDefaultBak;
		private System.Windows.Forms.Button butChangeMainMenuBak;
		private System.Windows.Forms.Button butChangeInfoBak;
		private System.Windows.Forms.GroupBox grpMenuColors;
		private System.Windows.Forms.Label lblMenuBorderColor;
		private System.Windows.Forms.Button butMenuSelectorBorderColor;
		private System.Windows.Forms.Button butMenuBorderColor;
		private System.Windows.Forms.Label lblSelectorBorderColor;
		private System.Windows.Forms.Label lblSelectorBarColor;
		private System.Windows.Forms.Button butMenuFontColor;
		private System.Windows.Forms.Label lblMenuFontColor;
		private System.Windows.Forms.Button butMenuSelectorBarColor;
		private System.Windows.Forms.GroupBox grpMenuOptions;
		private System.Windows.Forms.Label lblMenuFont;
		private System.Windows.Forms.Button butChangeMenuBak;
		private System.Windows.Forms.CheckBox chkShowDropShadow;
		private System.Windows.Forms.Button butChooseMenuFont;
		private System.Windows.Forms.CheckBox chkUseMenuBorders;
		private System.Windows.Forms.CheckBox chkHideExitMenu;
	}
}