using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Analyser
{
	/// <summary>
	/// Summary description for MainForm.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
    #region Console Functions
    [System.Runtime.InteropServices.DllImport("Kernel32.dll")]
    static extern bool AllocConsole();

    [System.Runtime.InteropServices.DllImport("Kernel32.dll")]
    static extern bool FreeConsole();

    [System.Runtime.InteropServices.DllImport("Kernel32.dll")]
    static extern bool SetConsoleTitle(string title);
    #endregion

    bool plotOK;
    PollGAForm dlgPoll=new PollGAForm();
    Preferences preferences=new Preferences();
    private string appName="Analyser";
    private Analyser analyser=new Analyser();
    private GraphOptionsForm graphDlg=new GraphOptionsForm();
    private DockingSuite.DockPanel dockPanel1;
    private DockingSuite.DockPanel dockPanel2;
    private DockingSuite.DockPanel dockPanel3;
    private DockingSuite.DockPanel dockPanel4;
    private System.Windows.Forms.PictureBox pictureBoxGraph;
    private System.Windows.Forms.MenuItem menuItem9;
    private System.Windows.Forms.MenuItem menuItem11;
    private System.Windows.Forms.MenuItem menuPreferences;
    private System.Windows.Forms.MenuItem menuExit;
    private System.Windows.Forms.MenuItem menuSaveGraph;
    private System.Windows.Forms.MenuItem menuPrintGraph;
    private System.Windows.Forms.MenuItem menuPrintPreview;
    private System.Windows.Forms.MenuItem menuFile;
    private System.Windows.Forms.MenuItem menuView;
    private System.Windows.Forms.MenuItem menuHelp;
    private System.Windows.Forms.MenuItem menuOpen;
    private System.Windows.Forms.MenuItem menuPoll;
    private System.Windows.Forms.MenuItem menuAbout;
    private System.Windows.Forms.MenuItem menuGraphOptions;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.CheckBox checkBoxAllRuns;
    private System.Windows.Forms.ListBox listBoxRun;
    private System.Windows.Forms.CheckBox checkBoxAllIslands;
    private System.Windows.Forms.ListBox listBoxScope;
    private System.Windows.Forms.Button buttonSelectAllIslands;
    private System.Windows.Forms.TextBox textBoxFileComment;
    private System.Windows.Forms.Button buttonToolbarOpen;
    private System.Windows.Forms.Button buttonToolbarPoll;
    private System.Windows.Forms.Button buttonToolbarOption;
    private System.Windows.Forms.Button buttonToolbarPrint;
    private DockingSuite.DockControl dockControlRun;
    private DockingSuite.DockControl dockControlScope;
    private System.Drawing.Printing.PrintDocument printDocument;
    private System.Windows.Forms.MainMenu mainMenu;
    private System.Windows.Forms.PrintDialog printDialog;
    private System.Windows.Forms.MenuItem menuItem1;
    private System.Windows.Forms.MenuItem menuViewRun;
    private DockingSuite.DockHost dockHostTop;
    private DockingSuite.DockHost dockHostBottom;
    private DockingSuite.DockHost dockHostLeft;
    private DockingSuite.DockHost dockHostRight;
    private System.Windows.Forms.MenuItem menuViewOutput;
    private System.Windows.Forms.MenuItem menuViewScope;
    private System.Windows.Forms.MenuItem menuViewSummary;
    private System.Windows.Forms.MenuItem menuViewToolbar;
    private System.Windows.Forms.MenuItem menuItem2;
    private DockingSuite.DockControl dockControlToolbar;
    private DockingSuite.DockControl dockControlSummary;
    private DockingSuite.DockPanel dockPanel7;
    private DockingSuite.DockControl dockControlOutputType;
    private DockingSuite.DockPanel dockPanel6;
    private DockingSuite.DockControl dockControlFiles;
    private System.Windows.Forms.ListBox listBoxFiles;
    private System.Windows.Forms.CheckBox checkBoxShowStddev;
    private System.Windows.Forms.ListBox listBoxShowMode;
    private System.Windows.Forms.MenuItem menuConsoleWindow;
    private System.Windows.Forms.NotifyIcon notifyIcon1;
    private Furty.Windows.Forms.FolderTreeView treeView1;
    private System.Windows.Forms.Splitter splitter1;
    private System.ComponentModel.IContainer components;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
      treeView1.InitFolderTreeView();
      treeView1.DrillToFolder(Preferences.logPath+"\\");

      checkBoxShowStddev.Checked=analyser.ShowStddev;
      plotOK=false;
      UpdateUI();
      ClearUI();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      this.components = new System.ComponentModel.Container();
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
      this.dockHostTop = new DockingSuite.DockHost();
      this.dockPanel1 = new DockingSuite.DockPanel();
      this.dockControlToolbar = new DockingSuite.DockControl();
      this.buttonToolbarPrint = new System.Windows.Forms.Button();
      this.buttonToolbarOption = new System.Windows.Forms.Button();
      this.buttonToolbarPoll = new System.Windows.Forms.Button();
      this.buttonToolbarOpen = new System.Windows.Forms.Button();
      this.dockHostBottom = new DockingSuite.DockHost();
      this.dockPanel7 = new DockingSuite.DockPanel();
      this.dockControlOutputType = new DockingSuite.DockControl();
      this.listBoxShowMode = new System.Windows.Forms.ListBox();
      this.checkBoxShowStddev = new System.Windows.Forms.CheckBox();
      this.dockPanel2 = new DockingSuite.DockPanel();
      this.dockControlSummary = new DockingSuite.DockControl();
      this.textBoxFileComment = new System.Windows.Forms.TextBox();
      this.dockHostLeft = new DockingSuite.DockHost();
      this.dockPanel6 = new DockingSuite.DockPanel();
      this.dockControlFiles = new DockingSuite.DockControl();
      this.listBoxFiles = new System.Windows.Forms.ListBox();
      this.treeView1 = new Furty.Windows.Forms.FolderTreeView();
      this.dockPanel3 = new DockingSuite.DockPanel();
      this.dockControlRun = new DockingSuite.DockControl();
      this.listBoxRun = new System.Windows.Forms.ListBox();
      this.checkBoxAllRuns = new System.Windows.Forms.CheckBox();
      this.dockPanel4 = new DockingSuite.DockPanel();
      this.dockControlScope = new DockingSuite.DockControl();
      this.listBoxScope = new System.Windows.Forms.ListBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.buttonSelectAllIslands = new System.Windows.Forms.Button();
      this.checkBoxAllIslands = new System.Windows.Forms.CheckBox();
      this.dockHostRight = new DockingSuite.DockHost();
      this.pictureBoxGraph = new System.Windows.Forms.PictureBox();
      this.mainMenu = new System.Windows.Forms.MainMenu();
      this.menuFile = new System.Windows.Forms.MenuItem();
      this.menuOpen = new System.Windows.Forms.MenuItem();
      this.menuPoll = new System.Windows.Forms.MenuItem();
      this.menuSaveGraph = new System.Windows.Forms.MenuItem();
      this.menuPrintGraph = new System.Windows.Forms.MenuItem();
      this.menuPrintPreview = new System.Windows.Forms.MenuItem();
      this.menuItem9 = new System.Windows.Forms.MenuItem();
      this.menuPreferences = new System.Windows.Forms.MenuItem();
      this.menuItem11 = new System.Windows.Forms.MenuItem();
      this.menuExit = new System.Windows.Forms.MenuItem();
      this.menuView = new System.Windows.Forms.MenuItem();
      this.menuGraphOptions = new System.Windows.Forms.MenuItem();
      this.menuItem2 = new System.Windows.Forms.MenuItem();
      this.menuConsoleWindow = new System.Windows.Forms.MenuItem();
      this.menuItem1 = new System.Windows.Forms.MenuItem();
      this.menuViewRun = new System.Windows.Forms.MenuItem();
      this.menuViewOutput = new System.Windows.Forms.MenuItem();
      this.menuViewScope = new System.Windows.Forms.MenuItem();
      this.menuViewSummary = new System.Windows.Forms.MenuItem();
      this.menuViewToolbar = new System.Windows.Forms.MenuItem();
      this.menuHelp = new System.Windows.Forms.MenuItem();
      this.menuAbout = new System.Windows.Forms.MenuItem();
      this.printDocument = new System.Drawing.Printing.PrintDocument();
      this.printDialog = new System.Windows.Forms.PrintDialog();
      this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
      this.splitter1 = new System.Windows.Forms.Splitter();
      this.dockHostTop.SuspendLayout();
      this.dockPanel1.SuspendLayout();
      this.dockControlToolbar.SuspendLayout();
      this.dockHostBottom.SuspendLayout();
      this.dockPanel7.SuspendLayout();
      this.dockControlOutputType.SuspendLayout();
      this.dockPanel2.SuspendLayout();
      this.dockControlSummary.SuspendLayout();
      this.dockHostLeft.SuspendLayout();
      this.dockPanel6.SuspendLayout();
      this.dockControlFiles.SuspendLayout();
      this.dockPanel3.SuspendLayout();
      this.dockControlRun.SuspendLayout();
      this.dockPanel4.SuspendLayout();
      this.dockControlScope.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // dockHostTop
      // 
      this.dockHostTop.Controls.Add(this.dockPanel1);
      this.dockHostTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.dockHostTop.Location = new System.Drawing.Point(0, 0);
      this.dockHostTop.Name = "dockHostTop";
      this.dockHostTop.Size = new System.Drawing.Size(728, 48);
      this.dockHostTop.TabIndex = 0;
      // 
      // dockPanel1
      // 
      this.dockPanel1.AutoHide = false;
      this.dockPanel1.Controls.Add(this.dockControlToolbar);
      this.dockPanel1.DockedHeight = 300;
      this.dockPanel1.DockedWidth = 728;
      this.dockPanel1.Location = new System.Drawing.Point(0, 0);
      this.dockPanel1.Name = "dockPanel1";
      this.dockPanel1.SelectedTab = this.dockControlToolbar;
      this.dockPanel1.Size = new System.Drawing.Size(728, 44);
      this.dockPanel1.TabIndex = 0;
      this.dockPanel1.Text = "Docked Panel";
      // 
      // dockControlToolbar
      // 
      this.dockControlToolbar.Controls.Add(this.buttonToolbarPrint);
      this.dockControlToolbar.Controls.Add(this.buttonToolbarOption);
      this.dockControlToolbar.Controls.Add(this.buttonToolbarPoll);
      this.dockControlToolbar.Controls.Add(this.buttonToolbarOpen);
      this.dockControlToolbar.FloatingHeight = 50;
      this.dockControlToolbar.FloatingWidth = 300;
      this.dockControlToolbar.Guid = new System.Guid("5b76e612-7bc3-43ce-bd28-c5b1c29eeed6");
      this.dockControlToolbar.Location = new System.Drawing.Point(0, 20);
      this.dockControlToolbar.Name = "dockControlToolbar";
      this.dockControlToolbar.PrimaryControl = null;
      this.dockControlToolbar.Size = new System.Drawing.Size(728, 1);
      this.dockControlToolbar.TabImage = null;
      this.dockControlToolbar.TabIndex = 0;
      this.dockControlToolbar.Text = "Toolbar";
      // 
      // buttonToolbarPrint
      // 
      this.buttonToolbarPrint.Image = ((System.Drawing.Image)(resources.GetObject("buttonToolbarPrint.Image")));
      this.buttonToolbarPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.buttonToolbarPrint.Location = new System.Drawing.Point(224, 0);
      this.buttonToolbarPrint.Name = "buttonToolbarPrint";
      this.buttonToolbarPrint.Size = new System.Drawing.Size(64, 23);
      this.buttonToolbarPrint.TabIndex = 3;
      this.buttonToolbarPrint.Text = "Print";
      this.buttonToolbarPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.buttonToolbarPrint.Click += new System.EventHandler(this.buttonToolbarPrint_Click);
      // 
      // buttonToolbarOption
      // 
      this.buttonToolbarOption.Image = ((System.Drawing.Image)(resources.GetObject("buttonToolbarOption.Image")));
      this.buttonToolbarOption.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.buttonToolbarOption.Location = new System.Drawing.Point(152, 0);
      this.buttonToolbarOption.Name = "buttonToolbarOption";
      this.buttonToolbarOption.Size = new System.Drawing.Size(64, 23);
      this.buttonToolbarOption.TabIndex = 2;
      this.buttonToolbarOption.Text = "Option";
      this.buttonToolbarOption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.buttonToolbarOption.Click += new System.EventHandler(this.buttonToolbarOption_Click);
      // 
      // buttonToolbarPoll
      // 
      this.buttonToolbarPoll.Image = ((System.Drawing.Image)(resources.GetObject("buttonToolbarPoll.Image")));
      this.buttonToolbarPoll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.buttonToolbarPoll.Location = new System.Drawing.Point(80, 0);
      this.buttonToolbarPoll.Name = "buttonToolbarPoll";
      this.buttonToolbarPoll.Size = new System.Drawing.Size(64, 23);
      this.buttonToolbarPoll.TabIndex = 1;
      this.buttonToolbarPoll.Text = "Poll";
      this.buttonToolbarPoll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.buttonToolbarPoll.Click += new System.EventHandler(this.buttonToolbarPoll_Click);
      // 
      // buttonToolbarOpen
      // 
      this.buttonToolbarOpen.Image = ((System.Drawing.Image)(resources.GetObject("buttonToolbarOpen.Image")));
      this.buttonToolbarOpen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.buttonToolbarOpen.Location = new System.Drawing.Point(8, 0);
      this.buttonToolbarOpen.Name = "buttonToolbarOpen";
      this.buttonToolbarOpen.Size = new System.Drawing.Size(64, 23);
      this.buttonToolbarOpen.TabIndex = 0;
      this.buttonToolbarOpen.Text = "Open";
      this.buttonToolbarOpen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.buttonToolbarOpen.Click += new System.EventHandler(this.buttonToolbarOpen_Click);
      // 
      // dockHostBottom
      // 
      this.dockHostBottom.Controls.Add(this.dockPanel7);
      this.dockHostBottom.Controls.Add(this.dockPanel2);
      this.dockHostBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.dockHostBottom.Location = new System.Drawing.Point(0, 433);
      this.dockHostBottom.Name = "dockHostBottom";
      this.dockHostBottom.Size = new System.Drawing.Size(728, 144);
      this.dockHostBottom.TabIndex = 1;
      // 
      // dockPanel7
      // 
      this.dockPanel7.AutoHide = false;
      this.dockPanel7.Controls.Add(this.dockControlOutputType);
      this.dockPanel7.DockedHeight = 300;
      this.dockPanel7.DockedWidth = 146;
      this.dockPanel7.Location = new System.Drawing.Point(0, 4);
      this.dockPanel7.Name = "dockPanel7";
      this.dockPanel7.SelectedTab = this.dockControlOutputType;
      this.dockPanel7.Size = new System.Drawing.Size(143, 140);
      this.dockPanel7.TabIndex = 1;
      this.dockPanel7.Text = "Docked Panel";
      // 
      // dockControlOutputType
      // 
      this.dockControlOutputType.Controls.Add(this.listBoxShowMode);
      this.dockControlOutputType.Controls.Add(this.checkBoxShowStddev);
      this.dockControlOutputType.Guid = new System.Guid("3a9b4134-c570-4137-b60b-509ffbb94b18");
      this.dockControlOutputType.Location = new System.Drawing.Point(0, 20);
      this.dockControlOutputType.Name = "dockControlOutputType";
      this.dockControlOutputType.PrimaryControl = null;
      this.dockControlOutputType.Size = new System.Drawing.Size(143, 97);
      this.dockControlOutputType.TabImage = null;
      this.dockControlOutputType.TabIndex = 0;
      this.dockControlOutputType.Text = "Output";
      // 
      // listBoxShowMode
      // 
      this.listBoxShowMode.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listBoxShowMode.Items.AddRange(new object[] {
                                                         "Mean Fitness",
                                                         "Max Fitness",
                                                         "Min Fitness",
                                                         "Global Optima",
                                                         "Adaptation Events",
                                                         "Migration Policy Changes",
                                                         "Speed",
                                                         "Number of clusters",
                                                         "Connectivity"});
      this.listBoxShowMode.Location = new System.Drawing.Point(0, 0);
      this.listBoxShowMode.Name = "listBoxShowMode";
      this.listBoxShowMode.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
      this.listBoxShowMode.Size = new System.Drawing.Size(143, 69);
      this.listBoxShowMode.TabIndex = 1;
      this.listBoxShowMode.SelectedIndexChanged += new System.EventHandler(this.listBoxShowMode_SelectedIndexChanged);
      // 
      // checkBoxShowStddev
      // 
      this.checkBoxShowStddev.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.checkBoxShowStddev.Location = new System.Drawing.Point(0, 73);
      this.checkBoxShowStddev.Name = "checkBoxShowStddev";
      this.checkBoxShowStddev.Size = new System.Drawing.Size(143, 24);
      this.checkBoxShowStddev.TabIndex = 0;
      this.checkBoxShowStddev.Text = "Show stddev";
      this.checkBoxShowStddev.CheckedChanged += new System.EventHandler(this.checkBoxShowStddev_CheckedChanged);
      // 
      // dockPanel2
      // 
      this.dockPanel2.AutoHide = false;
      this.dockPanel2.Controls.Add(this.dockControlSummary);
      this.dockPanel2.DockedHeight = 300;
      this.dockPanel2.DockedWidth = 582;
      this.dockPanel2.Location = new System.Drawing.Point(146, 4);
      this.dockPanel2.Name = "dockPanel2";
      this.dockPanel2.SelectedTab = this.dockControlSummary;
      this.dockPanel2.Size = new System.Drawing.Size(582, 140);
      this.dockPanel2.TabIndex = 0;
      this.dockPanel2.Text = "Docked Panel";
      // 
      // dockControlSummary
      // 
      this.dockControlSummary.Controls.Add(this.textBoxFileComment);
      this.dockControlSummary.Guid = new System.Guid("10fb6867-765e-41cf-978c-f6af64a62fb7");
      this.dockControlSummary.Location = new System.Drawing.Point(0, 20);
      this.dockControlSummary.Name = "dockControlSummary";
      this.dockControlSummary.PrimaryControl = null;
      this.dockControlSummary.Size = new System.Drawing.Size(582, 97);
      this.dockControlSummary.TabImage = null;
      this.dockControlSummary.TabIndex = 0;
      this.dockControlSummary.Text = "Summary";
      // 
      // textBoxFileComment
      // 
      this.textBoxFileComment.Dock = System.Windows.Forms.DockStyle.Fill;
      this.textBoxFileComment.Location = new System.Drawing.Point(0, 0);
      this.textBoxFileComment.Multiline = true;
      this.textBoxFileComment.Name = "textBoxFileComment";
      this.textBoxFileComment.ReadOnly = true;
      this.textBoxFileComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.textBoxFileComment.Size = new System.Drawing.Size(582, 97);
      this.textBoxFileComment.TabIndex = 0;
      this.textBoxFileComment.Text = "textBox1";
      // 
      // dockHostLeft
      // 
      this.dockHostLeft.Controls.Add(this.dockPanel6);
      this.dockHostLeft.Controls.Add(this.dockPanel3);
      this.dockHostLeft.Controls.Add(this.dockPanel4);
      this.dockHostLeft.Dock = System.Windows.Forms.DockStyle.Left;
      this.dockHostLeft.Location = new System.Drawing.Point(0, 48);
      this.dockHostLeft.Name = "dockHostLeft";
      this.dockHostLeft.Size = new System.Drawing.Size(160, 385);
      this.dockHostLeft.TabIndex = 2;
      // 
      // dockPanel6
      // 
      this.dockPanel6.AutoHide = false;
      this.dockPanel6.Controls.Add(this.dockControlFiles);
      this.dockPanel6.DockedHeight = 162;
      this.dockPanel6.DockedWidth = 0;
      this.dockPanel6.Location = new System.Drawing.Point(0, 0);
      this.dockPanel6.Name = "dockPanel6";
      this.dockPanel6.SelectedTab = this.dockControlFiles;
      this.dockPanel6.Size = new System.Drawing.Size(156, 159);
      this.dockPanel6.TabIndex = 2;
      this.dockPanel6.Text = "Docked Panel";
      // 
      // dockControlFiles
      // 
      this.dockControlFiles.Controls.Add(this.splitter1);
      this.dockControlFiles.Controls.Add(this.listBoxFiles);
      this.dockControlFiles.Controls.Add(this.treeView1);
      this.dockControlFiles.Guid = new System.Guid("5f14d7bf-b0d0-45df-83e3-dbbca6f4a5c2");
      this.dockControlFiles.Location = new System.Drawing.Point(0, 20);
      this.dockControlFiles.Name = "dockControlFiles";
      this.dockControlFiles.PrimaryControl = null;
      this.dockControlFiles.Size = new System.Drawing.Size(156, 116);
      this.dockControlFiles.TabImage = null;
      this.dockControlFiles.TabIndex = 0;
      this.dockControlFiles.Text = "Files";
      // 
      // listBoxFiles
      // 
      this.listBoxFiles.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listBoxFiles.Location = new System.Drawing.Point(0, 80);
      this.listBoxFiles.Name = "listBoxFiles";
      this.listBoxFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.listBoxFiles.Size = new System.Drawing.Size(156, 30);
      this.listBoxFiles.TabIndex = 4;
      this.listBoxFiles.SelectedIndexChanged += new System.EventHandler(this.listBoxFiles_SelectedIndexChanged);
      // 
      // treeView1
      // 
      this.treeView1.Dock = System.Windows.Forms.DockStyle.Top;
      this.treeView1.ImageIndex = -1;
      this.treeView1.Location = new System.Drawing.Point(0, 0);
      this.treeView1.Name = "treeView1";
      this.treeView1.SelectedImageIndex = -1;
      this.treeView1.Size = new System.Drawing.Size(156, 80);
      this.treeView1.TabIndex = 0;
      this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
      // 
      // dockPanel3
      // 
      this.dockPanel3.AutoHide = false;
      this.dockPanel3.Controls.Add(this.dockControlRun);
      this.dockPanel3.DockedHeight = 104;
      this.dockPanel3.DockedWidth = 0;
      this.dockPanel3.Location = new System.Drawing.Point(0, 162);
      this.dockPanel3.Name = "dockPanel3";
      this.dockPanel3.SelectedTab = this.dockControlRun;
      this.dockPanel3.Size = new System.Drawing.Size(156, 101);
      this.dockPanel3.TabIndex = 0;
      this.dockPanel3.Text = "Docked Panel";
      // 
      // dockControlRun
      // 
      this.dockControlRun.Controls.Add(this.listBoxRun);
      this.dockControlRun.Controls.Add(this.checkBoxAllRuns);
      this.dockControlRun.Guid = new System.Guid("9c7aa6c0-3880-4d6b-911b-76191d9f8c82");
      this.dockControlRun.Location = new System.Drawing.Point(0, 20);
      this.dockControlRun.Name = "dockControlRun";
      this.dockControlRun.PrimaryControl = null;
      this.dockControlRun.Size = new System.Drawing.Size(156, 58);
      this.dockControlRun.TabImage = null;
      this.dockControlRun.TabIndex = 0;
      this.dockControlRun.Text = "Run";
      // 
      // listBoxRun
      // 
      this.listBoxRun.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listBoxRun.Location = new System.Drawing.Point(0, 24);
      this.listBoxRun.Name = "listBoxRun";
      this.listBoxRun.Size = new System.Drawing.Size(156, 30);
      this.listBoxRun.TabIndex = 1;
      this.listBoxRun.SelectedIndexChanged += new System.EventHandler(this.listBoxRun_SelectedIndexChanged);
      // 
      // checkBoxAllRuns
      // 
      this.checkBoxAllRuns.Dock = System.Windows.Forms.DockStyle.Top;
      this.checkBoxAllRuns.Location = new System.Drawing.Point(0, 0);
      this.checkBoxAllRuns.Name = "checkBoxAllRuns";
      this.checkBoxAllRuns.Size = new System.Drawing.Size(156, 24);
      this.checkBoxAllRuns.TabIndex = 0;
      this.checkBoxAllRuns.Text = "All Runs";
      this.checkBoxAllRuns.CheckedChanged += new System.EventHandler(this.checkBoxAllRuns_CheckedChanged);
      // 
      // dockPanel4
      // 
      this.dockPanel4.AutoHide = false;
      this.dockPanel4.Controls.Add(this.dockControlScope);
      this.dockPanel4.DockedHeight = 119;
      this.dockPanel4.DockedWidth = 0;
      this.dockPanel4.Location = new System.Drawing.Point(0, 266);
      this.dockPanel4.Name = "dockPanel4";
      this.dockPanel4.SelectedTab = this.dockControlScope;
      this.dockPanel4.Size = new System.Drawing.Size(156, 119);
      this.dockPanel4.TabIndex = 1;
      this.dockPanel4.Text = "Docked Panel";
      // 
      // dockControlScope
      // 
      this.dockControlScope.Controls.Add(this.listBoxScope);
      this.dockControlScope.Controls.Add(this.panel1);
      this.dockControlScope.Guid = new System.Guid("e6bf8dc1-514b-4c9d-8bf0-0da0935217b9");
      this.dockControlScope.Location = new System.Drawing.Point(0, 20);
      this.dockControlScope.Name = "dockControlScope";
      this.dockControlScope.PrimaryControl = null;
      this.dockControlScope.Size = new System.Drawing.Size(156, 76);
      this.dockControlScope.TabImage = null;
      this.dockControlScope.TabIndex = 0;
      this.dockControlScope.Text = "Scope";
      // 
      // listBoxScope
      // 
      this.listBoxScope.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listBoxScope.Location = new System.Drawing.Point(0, 24);
      this.listBoxScope.Name = "listBoxScope";
      this.listBoxScope.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.listBoxScope.Size = new System.Drawing.Size(156, 43);
      this.listBoxScope.TabIndex = 1;
      this.listBoxScope.SelectedIndexChanged += new System.EventHandler(this.listBoxScope_SelectedIndexChanged);
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.buttonSelectAllIslands);
      this.panel1.Controls.Add(this.checkBoxAllIslands);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(156, 24);
      this.panel1.TabIndex = 0;
      // 
      // buttonSelectAllIslands
      // 
      this.buttonSelectAllIslands.Location = new System.Drawing.Point(80, 0);
      this.buttonSelectAllIslands.Name = "buttonSelectAllIslands";
      this.buttonSelectAllIslands.TabIndex = 1;
      this.buttonSelectAllIslands.Text = "Select All";
      this.buttonSelectAllIslands.Click += new System.EventHandler(this.buttonSelectAllIslands_Click);
      // 
      // checkBoxAllIslands
      // 
      this.checkBoxAllIslands.Location = new System.Drawing.Point(0, 0);
      this.checkBoxAllIslands.Name = "checkBoxAllIslands";
      this.checkBoxAllIslands.Size = new System.Drawing.Size(80, 24);
      this.checkBoxAllIslands.TabIndex = 0;
      this.checkBoxAllIslands.Text = "All Islands";
      this.checkBoxAllIslands.CheckedChanged += new System.EventHandler(this.checkBoxAllIslands_CheckedChanged);
      // 
      // dockHostRight
      // 
      this.dockHostRight.Dock = System.Windows.Forms.DockStyle.Right;
      this.dockHostRight.Location = new System.Drawing.Point(584, 48);
      this.dockHostRight.Name = "dockHostRight";
      this.dockHostRight.Size = new System.Drawing.Size(144, 385);
      this.dockHostRight.TabIndex = 3;
      // 
      // pictureBoxGraph
      // 
      this.pictureBoxGraph.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pictureBoxGraph.Location = new System.Drawing.Point(160, 48);
      this.pictureBoxGraph.Name = "pictureBoxGraph";
      this.pictureBoxGraph.Size = new System.Drawing.Size(424, 385);
      this.pictureBoxGraph.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pictureBoxGraph.TabIndex = 4;
      this.pictureBoxGraph.TabStop = false;
      this.pictureBoxGraph.Click += new System.EventHandler(this.pictureBoxGraph_Click);
      // 
      // mainMenu
      // 
      this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                             this.menuFile,
                                                                             this.menuView,
                                                                             this.menuHelp});
      // 
      // menuFile
      // 
      this.menuFile.Index = 0;
      this.menuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                             this.menuOpen,
                                                                             this.menuPoll,
                                                                             this.menuSaveGraph,
                                                                             this.menuPrintGraph,
                                                                             this.menuPrintPreview,
                                                                             this.menuItem9,
                                                                             this.menuPreferences,
                                                                             this.menuItem11,
                                                                             this.menuExit});
      this.menuFile.Text = "&File";
      // 
      // menuOpen
      // 
      this.menuOpen.Index = 0;
      this.menuOpen.Text = "&Open File...";
      this.menuOpen.Click += new System.EventHandler(this.menuOpen_Click);
      // 
      // menuPoll
      // 
      this.menuPoll.Index = 1;
      this.menuPoll.Text = "&Poll PGA...";
      this.menuPoll.Click += new System.EventHandler(this.menuPoll_Click);
      // 
      // menuSaveGraph
      // 
      this.menuSaveGraph.Index = 2;
      this.menuSaveGraph.Text = "Save Graph...";
      this.menuSaveGraph.Click += new System.EventHandler(this.menuSaveGraph_Click);
      // 
      // menuPrintGraph
      // 
      this.menuPrintGraph.Index = 3;
      this.menuPrintGraph.Text = "Print Graph...";
      this.menuPrintGraph.Click += new System.EventHandler(this.menuPrintGraph_Click);
      // 
      // menuPrintPreview
      // 
      this.menuPrintPreview.Index = 4;
      this.menuPrintPreview.Text = "Print Preview...";
      this.menuPrintPreview.Click += new System.EventHandler(this.menuPrintPreview_Click);
      // 
      // menuItem9
      // 
      this.menuItem9.Index = 5;
      this.menuItem9.Text = "-";
      // 
      // menuPreferences
      // 
      this.menuPreferences.Index = 6;
      this.menuPreferences.Text = "Preferences";
      this.menuPreferences.Click += new System.EventHandler(this.menuPreferences_Click);
      // 
      // menuItem11
      // 
      this.menuItem11.Index = 7;
      this.menuItem11.Text = "-";
      // 
      // menuExit
      // 
      this.menuExit.Index = 8;
      this.menuExit.Text = "E&xit";
      this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
      // 
      // menuView
      // 
      this.menuView.Index = 1;
      this.menuView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                             this.menuGraphOptions,
                                                                             this.menuItem2,
                                                                             this.menuConsoleWindow,
                                                                             this.menuItem1});
      this.menuView.Text = "&View";
      // 
      // menuGraphOptions
      // 
      this.menuGraphOptions.Index = 0;
      this.menuGraphOptions.Text = "Graph &Options...";
      this.menuGraphOptions.Click += new System.EventHandler(this.menuGraphOptions_Click);
      // 
      // menuItem2
      // 
      this.menuItem2.Index = 1;
      this.menuItem2.Text = "-";
      // 
      // menuConsoleWindow
      // 
      this.menuConsoleWindow.Index = 2;
      this.menuConsoleWindow.Text = "&Console Window";
      this.menuConsoleWindow.Click += new System.EventHandler(this.menuConsoleWindow_Click);
      // 
      // menuItem1
      // 
      this.menuItem1.Index = 3;
      this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                              this.menuViewRun,
                                                                              this.menuViewOutput,
                                                                              this.menuViewScope,
                                                                              this.menuViewSummary,
                                                                              this.menuViewToolbar});
      this.menuItem1.Text = "Floating &Windows";
      // 
      // menuViewRun
      // 
      this.menuViewRun.Index = 0;
      this.menuViewRun.Text = "&Run";
      this.menuViewRun.Click += new System.EventHandler(this.menuViewRun_Click);
      // 
      // menuViewOutput
      // 
      this.menuViewOutput.Index = 1;
      this.menuViewOutput.Text = "&Output";
      this.menuViewOutput.Click += new System.EventHandler(this.menuViewOutput_Click);
      // 
      // menuViewScope
      // 
      this.menuViewScope.Index = 2;
      this.menuViewScope.Text = "&Scope";
      this.menuViewScope.Click += new System.EventHandler(this.menuViewScope_Click);
      // 
      // menuViewSummary
      // 
      this.menuViewSummary.Index = 3;
      this.menuViewSummary.Text = "&Summary";
      this.menuViewSummary.Click += new System.EventHandler(this.menuViewSummary_Click);
      // 
      // menuViewToolbar
      // 
      this.menuViewToolbar.Index = 4;
      this.menuViewToolbar.Text = "&Toolbar";
      this.menuViewToolbar.Click += new System.EventHandler(this.menuViewToolbar_Click);
      // 
      // menuHelp
      // 
      this.menuHelp.Index = 2;
      this.menuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                             this.menuAbout});
      this.menuHelp.Text = "&Help";
      // 
      // menuAbout
      // 
      this.menuAbout.Index = 0;
      this.menuAbout.Text = "&About...";
      this.menuAbout.Click += new System.EventHandler(this.menuAbout_Click);
      // 
      // printDocument
      // 
      this.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument_PrintPage_1);
      // 
      // notifyIcon1
      // 
      this.notifyIcon1.Text = "notifyIcon1";
      this.notifyIcon1.Visible = true;
      // 
      // splitter1
      // 
      this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
      this.splitter1.Location = new System.Drawing.Point(0, 80);
      this.splitter1.Name = "splitter1";
      this.splitter1.Size = new System.Drawing.Size(156, 3);
      this.splitter1.TabIndex = 5;
      this.splitter1.TabStop = false;
      // 
      // MainForm
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(728, 577);
      this.Controls.Add(this.pictureBoxGraph);
      this.Controls.Add(this.dockHostRight);
      this.Controls.Add(this.dockHostLeft);
      this.Controls.Add(this.dockHostBottom);
      this.Controls.Add(this.dockHostTop);
      this.Menu = this.mainMenu;
      this.Name = "MainForm";
      this.Text = "Analyser";
      this.dockHostTop.ResumeLayout(false);
      this.dockPanel1.ResumeLayout(false);
      this.dockControlToolbar.ResumeLayout(false);
      this.dockHostBottom.ResumeLayout(false);
      this.dockPanel7.ResumeLayout(false);
      this.dockControlOutputType.ResumeLayout(false);
      this.dockPanel2.ResumeLayout(false);
      this.dockControlSummary.ResumeLayout(false);
      this.dockHostLeft.ResumeLayout(false);
      this.dockPanel6.ResumeLayout(false);
      this.dockControlFiles.ResumeLayout(false);
      this.dockPanel3.ResumeLayout(false);
      this.dockControlRun.ResumeLayout(false);
      this.dockPanel4.ResumeLayout(false);
      this.dockControlScope.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }
		#endregion

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    //[STAThread]
    static void Main(string[] args) {
      // Redirect exception handling
      AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
      Application.ThreadException += new ThreadExceptionEventHandler(ThreadException);

      MainForm form=new MainForm();

      // Read command line arguments
      //args=new string[1]; args[0]=@"d:\test.dat";
      if(args.Length>0) {
        form.Show();
        foreach(string arg in args) {
          form.ReadFile(arg);
        }
      }

      Application.Run(form);
    }

    static void UnhandledException(object sender, UnhandledExceptionEventArgs e) {
      MessageBox.Show(e.ExceptionObject.ToString(), "Unhandled Error");
    }

    static void ThreadException(object sender, ThreadExceptionEventArgs e) {
      MessageBox.Show(e.Exception.Message+"\n\n"+e.Exception.StackTrace, "Thread Error");
    }

    private void menuOpen_Click(object sender, System.EventArgs e) {
      listBoxFiles.ClearSelected();

      OpenFileDialog dlg=new OpenFileDialog();
      dlg.InitialDirectory=Preferences.logPath;
      dlg.Filter="Data dump files (*.dat)|*.dat|All files (*.*)|*.*";
      if(dlg.ShowDialog()==DialogResult.OK) {
        ReadFile(dlg.FileName);
      }
    }

    private void menuSaveGraph_Click(object sender, System.EventArgs e) {
      SaveFileDialog dlg=new SaveFileDialog();
      dlg.InitialDirectory=Environment.CurrentDirectory;
      dlg.Filter="Bitmap format (*.png)|*.png|Vector format (*.cgm)|*.cgm|Latex format (*.tex)|*.tex";

      if(dlg.ShowDialog()==DialogResult.OK) {
        if(dlg.FileName.EndsWith("png")) {
          analyser.MakeGraphFile(dlg.FileName, Analyser.GraphType.PNG);
        } else if(dlg.FileName.EndsWith("tex")) {
          analyser.MakeGraphFile(dlg.FileName, Analyser.GraphType.TEX);
        } else {
          analyser.MakeGraphFile(dlg.FileName, Analyser.GraphType.CGM);
        }
        MessageBox.Show(this, "Graph stored as "+dlg.FileName, "Save Graph");
      }
    }

    private void menuExit_Click(object sender, System.EventArgs e) {
      Application.Exit();
    }

    private void menuAbout_Click(object sender, System.EventArgs e) {
      MessageBox.Show(this, "(c) 2004 Johan Berntsson\n\nComponents and libraries used:\nDockingSuite by Tim Dawson (http://www.divil.co.uk/net/)", appName);
    }

    private void menuGraphOptions_Click(object sender, System.EventArgs e) {
      if(graphDlg.ShowDialog()==DialogResult.OK) {
        string cmd="";
        if(graphDlg.buttonXCustom.Checked) {
          cmd+="set xrange ["+graphDlg.textBoxXMin.Text+":"+graphDlg.textBoxXMax.Text+"]\r\n";
        }
        if(graphDlg.buttonYCustom.Checked) {
          cmd+="set yrange ["+graphDlg.textBoxYMin.Text+":"+graphDlg.textBoxYMax.Text+"]\r\n";
        }
        if(graphDlg.buttonLegendOff.Checked) {
          cmd+="set key off\r\n";
        }

        cmd+=graphDlg.textBoxCommands.Text;
        analyser.CustomizeGraph(cmd);
        UpdateGraph();
      }
    }


    private void checkBoxAllIslands_CheckedChanged(object sender, System.EventArgs e) {
      analyser.ShowAllIslands=checkBoxAllIslands.Checked;
      UpdateGraph();
    }

    private void listBoxScope_SelectedIndexChanged(object sender, System.EventArgs e) {
      for(int i=0; i<analyser.NumIslands; i++) analyser.ShowIsland(i, false);
      if(listBoxScope.SelectedIndices.Count>0) {
        foreach(int index in listBoxScope.SelectedIndices) {
          analyser.ShowIsland(index, true);
        }

        if(plotOK) {
          if(checkBoxAllIslands.Checked) {
            checkBoxAllIslands.Checked=false;
          } else {
            UpdateGraph();
          }
        }
      }
    }

    private void ReadFile(string path) {
      ReadFile(path, 0);
    }

    private void ReadFile(string path, int index) {
        // read PGA data file and initialise analyser
      plotOK=false;
      ProgressForm dlg=new ProgressForm();
      dlg.Show();
      ClearUI();
      try {
        analyser.Read(path, index, dlg.progressBar);
        analyser.SetCurrentRun(0);
        for(int i=0; i<analyser.NumIslands; i++) analyser.ShowIsland(i, false);
        analyser.ShowAllIslands=true;
        dlg.Hide();
      } catch(Exception e) {
        dlg.Hide();
        //        MessageBox.Show(e.Message, "File Open Error");
        MessageBox.Show(e.Message+"\n"+e.StackTrace, "File Open Error");
      }
      UpdateUI();

      plotOK=true;
      UpdateGraph();
    }

    private void ClearUI() {
      // Called before loading a new file. Set GUI to non-loaded state
      this.Text=appName;
      textBoxFileComment.ResetText();
      listBoxScope.Items.Clear();
      listBoxRun.Items.Clear();
      pictureBoxGraph.Image=null;

      dockControlRun.Enabled=false;
      dockControlScope.Enabled=false;
      dockControlOutputType.Enabled=false;
      menuSaveGraph.Enabled=false;
      menuPrintGraph.Enabled=false;
      menuPrintPreview.Enabled=false;
      buttonToolbarPrint.Enabled=false;
      menuGraphOptions.Enabled=false;
//      menuView.Visible=false;
      buttonToolbarOption.Enabled=false;
    }

    private void UpdateUI() {
      // Called after new file loaded. Set GUI to loaded state
      if(listBoxFiles.SelectedIndices.Count<2) {
        dockControlRun.Enabled=true;
        dockControlScope.Enabled=true;
      }
      dockControlOutputType.Enabled=true;
      menuSaveGraph.Enabled=true;
      menuPrintGraph.Enabled=true;
      menuPrintPreview.Enabled=true;
      buttonToolbarPrint.Enabled=true;
      menuGraphOptions.Enabled=true;
 //      menuView.Visible=true;
      buttonToolbarOption.Enabled=true;

      listBoxShowMode.SetSelected(0, analyser.ShowMeanFitness);
      listBoxShowMode.SetSelected(1, analyser.ShowMaxFitness);
      listBoxShowMode.SetSelected(2, analyser.ShowMinFitness);
      listBoxShowMode.SetSelected(3, analyser.ShowGlobalOptima);
      listBoxShowMode.SetSelected(4, analyser.ShowAdaptationEvents);
      listBoxShowMode.SetSelected(5, analyser.ShowMigrationEvents);
      listBoxShowMode.SetSelected(6, analyser.ShowSpeed);
      listBoxShowMode.SetSelected(7, analyser.ShowClusters);
      listBoxShowMode.SetSelected(8, analyser.ShowConnectivity);
      checkBoxAllIslands.Checked=analyser.ShowAllIslands;

      if(MainForm.ActiveForm!=null) MainForm.ActiveForm.Text=appName+": "+analyser.FilePath;

      for(int i=0; i<analyser.NumRuns; i++) {
        listBoxRun.Items.Add(string.Format("Run {0}", i));
        if(analyser.ShowRun(i)) listBoxRun.SetSelected(i, true);
      }

      for(int i=0; i<analyser.NumIslands; i++) {
        listBoxScope.Items.Add(string.Format("Island {0}", i));
        listBoxScope.SetSelected(i, analyser.ShowIsland(i));
      }
    }

    private void UpdateGraph() {
      if(!plotOK) return;
      Cursor.Current=Cursors.WaitCursor;

      // Update status text
      textBoxFileComment.Text=analyser.Summary;

      // create the graph and display it in the picture box
      string picPath="gnu.png";
      analyser.MakeGraphFile(picPath, Analyser.GraphType.PNG);

      // Bitmap(url) or Image.FromFile(url) locks the picture file, making it
      // impossible to regenerate a new graph after editing the scenario file. Using
      // Image.FromStream instead solves the problem.
      using(FileStream fs=new FileStream(picPath, FileMode.Open, FileAccess.Read)) {
        try {
          pictureBoxGraph.Image=System.Drawing.Image.FromStream(fs);
        } catch(Exception) {
          pictureBoxGraph.Image=null;
        }
      }
    }

    private void pictureBoxGraph_Click(object sender, System.EventArgs e) {
      // Create a modeless window with a copy of the currently displayed picture
      if(pictureBoxGraph.Image==null) return;

      ImageWindow wnd=new ImageWindow();
      wnd.Text=this.Text;
      wnd.Size=pictureBoxGraph.Size;
      wnd.SetImage(pictureBoxGraph.Image);
      wnd.Show();
    }

    private void menuPoll_Click(object sender, System.EventArgs e) {
      if(dlgPoll.ShowDialog()==DialogResult.OK) {
        MessageBox.Show("Polling "+dlgPoll.textBoxID.Text+" isn't implemented - YET!", "Under construction");
      }
    }

    private void menuPreferences_Click(object sender, System.EventArgs e) {
      preferences.ShowDialog(this);
    }

    private void menuPrintGraph_Click(object sender, System.EventArgs e) {
      if(sender==buttonToolbarPrint) {
        printDocument.Print();
      } else {
        if(printDialog.ShowDialog()==DialogResult.OK) {
          printDocument.PrinterSettings=printDialog.PrinterSettings;
          printDocument.Print();
        }
      }
    }

    private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e) {
      e.Graphics.DrawImage(pictureBoxGraph.Image, e.MarginBounds.Left, e.MarginBounds.Top);
      Rectangle r=new Rectangle(e.MarginBounds.Left, e.MarginBounds.Top+pictureBoxGraph.Image.Height+40, e.MarginBounds.Width, e.MarginBounds.Height-pictureBoxGraph.Image.Height-40);
      e.Graphics.DrawString(analyser.Summary, SystemInformation.MenuFont, Brushes.Black, r);
      e.HasMorePages = false;
    }

    private void menuPrintPreview_Click(object sender, System.EventArgs e) {
      PrintPreviewDialog prevDlg = new PrintPreviewDialog();
      prevDlg.Document = printDocument;
      prevDlg.Size = new System.Drawing.Size(600, 329);
      prevDlg.ShowDialog();
    }

    private void listBoxRun_SelectedIndexChanged(object sender, System.EventArgs e) {
      if(listBoxRun.SelectedIndices.Count>0) {
        foreach(int index in listBoxRun.SelectedIndices) {
          analyser.SetCurrentRun(index);
        }

        if(this.plotOK) {
          if(checkBoxAllRuns.Checked) {
            checkBoxAllRuns.Checked=false;
          } else {
            UpdateGraph();
          }
        }
      }
    }

    private void checkBoxAllRuns_CheckedChanged(object sender, System.EventArgs e) {
      analyser.AllRuns=checkBoxAllRuns.Checked;
      UpdateGraph();    
    }

    private void listBoxShowMode_SelectedIndexChanged(object sender, System.EventArgs e) {
      if(!plotOK) return;

      analyser.ShowMeanFitness=false;
      analyser.ShowMinFitness=false;
      analyser.ShowMaxFitness=false;
      analyser.ShowGlobalOptima=false;
      analyser.ShowMigrationEvents=false;
      analyser.ShowAdaptationEvents=false;
      analyser.ShowSpeed=false;
      analyser.ShowClusters=false;
      analyser.ShowConnectivity=false;

      foreach(int index in listBoxShowMode.SelectedIndices) {
        if(index==0) analyser.ShowMeanFitness=true;
        if(index==1) analyser.ShowMaxFitness=true;
        if(index==2) analyser.ShowMinFitness=true;
        if(index==3) analyser.ShowGlobalOptima=true;
        if(index==4) analyser.ShowAdaptationEvents=true;
        if(index==5) analyser.ShowMigrationEvents=true;
        if(index==6) analyser.ShowSpeed=true;
        if(index==7) analyser.ShowClusters=true;
        if(index==8) analyser.ShowConnectivity=true;
      }
      UpdateGraph();
    }

    private void buttonSelectAllIslands_Click(object sender, System.EventArgs e) {
      plotOK=false;
      for(int i=0; i<listBoxScope.Items.Count; i++) listBoxScope.SetSelected(i, true);
      plotOK=true;
      UpdateGraph();
    }

    private void buttonToolbarOpen_Click(object sender, System.EventArgs e) {
#if !DEBUG
        ReadFile(@"D:\Documents and Settings\n4558146\My Documents\Visual Studio Projects\data\x14.dat");
#else
      menuOpen_Click(sender, e);
#endif    
    }

    private void buttonToolbarPoll_Click(object sender, System.EventArgs e) {
      menuPoll_Click(sender, e);
    }

    private void buttonToolbarOption_Click(object sender, System.EventArgs e) {
      menuGraphOptions_Click(sender, e);
    }

    private void buttonToolbarPrint_Click(object sender, System.EventArgs e) {
      menuPrintGraph_Click(sender, e);   
    }

    private void menuViewRun_Click(object sender, System.EventArgs e) {
      dockControlRun.EnsureVisible(dockHostLeft);
    }

    private void menuViewOutput_Click(object sender, System.EventArgs e) {
      dockControlOutputType.EnsureVisible(dockHostLeft);
    }

    private void menuViewScope_Click(object sender, System.EventArgs e) {
      dockControlScope.EnsureVisible(dockHostLeft);    
    }

    private void menuViewSummary_Click(object sender, System.EventArgs e) {
      dockControlSummary.EnsureVisible(dockHostBottom);
    }

    private void menuViewToolbar_Click(object sender, System.EventArgs e) {
      dockControlToolbar.EnsureVisible(dockHostTop);
    }

    private void treeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e) {
      listBoxFiles.Items.Clear();
      string path=treeView1.GetSelectedNodePath();
      if(path.Length==0) return;

      DirectoryInfo di = new DirectoryInfo(treeView1.GetSelectedNodePath());
      if(!di.Exists) return;

      // Add all .dat files in this directory
      FileInfo[] fi = di.GetFiles("*.dat");
      foreach (FileInfo fiTemp in fi) {
        listBoxFiles.Items.Add(fiTemp.Name);
      }
    }

    private void listBoxFiles_SelectedIndexChanged(object sender, System.EventArgs e) {
      // Assume that we add in order (TODO: fix me!)
      int cnt=listBoxFiles.SelectedIndices.Count;
      string path=treeView1.GetSelectedNodePath();

      int i=0;
      foreach(int index in listBoxFiles.SelectedIndices) {
        if(cnt<2 || i==cnt-1) {
          ReadFile(path+"\\"+listBoxFiles.Items[index], i);
        }
        ++i;
      }
    }

    private void checkBoxShowStddev_CheckedChanged(object sender, System.EventArgs e) {
      analyser.ShowStddev=checkBoxShowStddev.Checked;
      UpdateGraph();
    }

    private void menuConsoleWindow_Click(object sender, System.EventArgs e) {
      if (menuConsoleWindow.Checked)
        menuConsoleWindow.Checked = !FreeConsole();
      else {
        menuConsoleWindow.Checked = AllocConsole();
        SetConsoleTitle(appName);
        Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
      }
    }

    private void printDocument_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e) {
      e.Graphics.DrawImage(pictureBoxGraph.Image, e.MarginBounds.Left, e.MarginBounds.Top);
      Rectangle r=new Rectangle(e.MarginBounds.Left, e.MarginBounds.Top+pictureBoxGraph.Image.Height+40, e.MarginBounds.Width, e.MarginBounds.Height-pictureBoxGraph.Image.Height-40);
      e.Graphics.DrawString(analyser.Summary, SystemInformation.MenuFont, Brushes.Black, r);
      e.HasMorePages = false;    
    }

  }
}
