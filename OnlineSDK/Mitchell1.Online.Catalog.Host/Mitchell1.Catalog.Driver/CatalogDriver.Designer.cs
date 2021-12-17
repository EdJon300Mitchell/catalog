namespace Mitchell1.Catalog.Driver
{
    partial class CatalogDriver
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
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Configure Catalog", 1, 1);
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Setup Vehicle", 10, 10);
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Catalog Setup", 4, 4, new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Setup Vendor", 6, 6);
			System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Go Shopping", 0, 0);
			System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Price Check", 5, 5);
			System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Order Parts", 16, 16);
			System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Catalog Methods", 1, 1, new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7});
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CatalogDriver));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.clearDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.vendorQualifierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.setupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.vehicleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.browserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.hTML5TestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.playgroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.whatIsMyUserAgentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.licensesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openFileDialogCatalogDll = new System.Windows.Forms.OpenFileDialog();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.treeViewCatalogMethods = new System.Windows.Forms.TreeView();
			this.imageListTreeImages = new System.Windows.Forms.ImageList(this.components);
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabelCatalogName = new System.Windows.Forms.ToolStripStatusLabel();
			this.openFileDialogSettings = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialogSettings = new System.Windows.Forms.SaveFileDialog();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.setupToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.browserToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
			this.menuStrip1.Size = new System.Drawing.Size(792, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// settingsToolStripMenuItem
			// 
			this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearDataToolStripMenuItem});
			this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
			this.settingsToolStripMenuItem.Text = "&Settings";
			// 
			// clearDataToolStripMenuItem
			// 
			this.clearDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vendorQualifierToolStripMenuItem,
            this.cartToolStripMenuItem});
			this.clearDataToolStripMenuItem.Name = "clearDataToolStripMenuItem";
			this.clearDataToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.clearDataToolStripMenuItem.Text = "&Clear";
			// 
			// vendorQualifierToolStripMenuItem
			// 
			this.vendorQualifierToolStripMenuItem.Name = "vendorQualifierToolStripMenuItem";
			this.vendorQualifierToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.vendorQualifierToolStripMenuItem.Text = "Vendor Qualifier";
			this.vendorQualifierToolStripMenuItem.Click += new System.EventHandler(this.vendorQualifierToolStripMenuItem_Click);
			// 
			// cartToolStripMenuItem
			// 
			this.cartToolStripMenuItem.Name = "cartToolStripMenuItem";
			this.cartToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.cartToolStripMenuItem.Text = "Cart";
			this.cartToolStripMenuItem.Click += new System.EventHandler(this.cartToolStripMenuItem_Click);
			// 
			// setupToolStripMenuItem
			// 
			this.setupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vehicleToolStripMenuItem});
			this.setupToolStripMenuItem.Name = "setupToolStripMenuItem";
			this.setupToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
			this.setupToolStripMenuItem.Text = "&Configure";
			// 
			// vehicleToolStripMenuItem
			// 
			this.vehicleToolStripMenuItem.Name = "vehicleToolStripMenuItem";
			this.vehicleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.vehicleToolStripMenuItem.Text = "&Vehicle";
			this.vehicleToolStripMenuItem.Click += new System.EventHandler(this.vehicleToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(12, 20);
			// 
			// browserToolStripMenuItem
			// 
			this.browserToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hTML5TestToolStripMenuItem,
            this.playgroundToolStripMenuItem,
            this.whatIsMyUserAgentToolStripMenuItem,
            this.licensesToolStripMenuItem});
			this.browserToolStripMenuItem.Name = "browserToolStripMenuItem";
			this.browserToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
			this.browserToolStripMenuItem.Text = "&Browser";
			// 
			// hTML5TestToolStripMenuItem
			// 
			this.hTML5TestToolStripMenuItem.Name = "hTML5TestToolStripMenuItem";
			this.hTML5TestToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
			this.hTML5TestToolStripMenuItem.Text = "&HTML5 Test";
			this.hTML5TestToolStripMenuItem.Click += new System.EventHandler(this.hTML5TestToolStripMenuItem_Click);
			// 
			// playgroundToolStripMenuItem
			// 
			this.playgroundToolStripMenuItem.Name = "playgroundToolStripMenuItem";
			this.playgroundToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
			this.playgroundToolStripMenuItem.Text = "&Playground";
			this.playgroundToolStripMenuItem.Click += new System.EventHandler(this.playgroundToolStripMenuItem_Click);
			// 
			// whatIsMyUserAgentToolStripMenuItem
			// 
			this.whatIsMyUserAgentToolStripMenuItem.Name = "whatIsMyUserAgentToolStripMenuItem";
			this.whatIsMyUserAgentToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
			this.whatIsMyUserAgentToolStripMenuItem.Text = "&What is my User Agent";
			this.whatIsMyUserAgentToolStripMenuItem.Click += new System.EventHandler(this.whatIsMyUserAgentToolStripMenuItem_Click);
			// 
			// licensesToolStripMenuItem
			// 
			this.licensesToolStripMenuItem.Name = "licensesToolStripMenuItem";
			this.licensesToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
			this.licensesToolStripMenuItem.Text = "&Licenses";
			this.licensesToolStripMenuItem.Click += new System.EventHandler(this.licensesToolStripMenuItem_Click);
			// 
			// openFileDialogCatalogDll
			// 
			this.openFileDialogCatalogDll.DefaultExt = "dll";
			this.openFileDialogCatalogDll.FileName = "sample.dll";
			this.openFileDialogCatalogDll.Filter = "Catalogs|*.dll";
			this.openFileDialogCatalogDll.InitialDirectory = ".\\";
			this.openFileDialogCatalogDll.Title = "Load Catalog";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point(0, 26);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.treeViewCatalogMethods);
			this.splitContainer1.Panel1MinSize = 0;
			this.splitContainer1.Panel2MinSize = 0;
			this.splitContainer1.Size = new System.Drawing.Size(792, 557);
			this.splitContainer1.SplitterDistance = 210;
			this.splitContainer1.TabIndex = 12;
			// 
			// treeViewCatalogMethods
			// 
			this.treeViewCatalogMethods.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.treeViewCatalogMethods.BackColor = System.Drawing.SystemColors.Control;
			this.treeViewCatalogMethods.Cursor = System.Windows.Forms.Cursors.Hand;
			this.treeViewCatalogMethods.FullRowSelect = true;
			this.treeViewCatalogMethods.HideSelection = false;
			this.treeViewCatalogMethods.ImageIndex = 0;
			this.treeViewCatalogMethods.ImageList = this.imageListTreeImages;
			this.treeViewCatalogMethods.Indent = 20;
			this.treeViewCatalogMethods.ItemHeight = 18;
			this.treeViewCatalogMethods.Location = new System.Drawing.Point(3, 3);
			this.treeViewCatalogMethods.Name = "treeViewCatalogMethods";
			treeNode1.ImageIndex = 1;
			treeNode1.Name = "SelectCatalog";
			treeNode1.SelectedImageIndex = 1;
			treeNode1.Text = "Configure Catalog";
			treeNode2.ImageIndex = 10;
			treeNode2.Name = "SetupVehicle";
			treeNode2.SelectedImageIndex = 10;
			treeNode2.Text = "Setup Vehicle";
			treeNode3.BackColor = System.Drawing.SystemColors.ScrollBar;
			treeNode3.ImageIndex = 4;
			treeNode3.Name = "CatalogSetup";
			treeNode3.SelectedImageIndex = 4;
			treeNode3.Text = "Catalog Setup";
			treeNode4.ImageIndex = 6;
			treeNode4.Name = "SetupVendor";
			treeNode4.SelectedImageIndex = 6;
			treeNode4.Text = "Setup Vendor";
			treeNode5.ImageIndex = 0;
			treeNode5.Name = "GoShopping";
			treeNode5.SelectedImageIndex = 0;
			treeNode5.Text = "Go Shopping";
			treeNode6.ImageIndex = 5;
			treeNode6.Name = "PriceCheck";
			treeNode6.SelectedImageIndex = 5;
			treeNode6.Text = "Price Check";
			treeNode7.ImageIndex = 16;
			treeNode7.Name = "OrderParts";
			treeNode7.SelectedImageIndex = 16;
			treeNode7.Text = "Order Parts";
			treeNode8.BackColor = System.Drawing.SystemColors.ScrollBar;
			treeNode8.ImageIndex = 1;
			treeNode8.Name = "CatalogMethods";
			treeNode8.SelectedImageIndex = 1;
			treeNode8.Text = "Catalog Methods";
			this.treeViewCatalogMethods.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode8});
			this.treeViewCatalogMethods.SelectedImageIndex = 0;
			this.treeViewCatalogMethods.ShowLines = false;
			this.treeViewCatalogMethods.ShowPlusMinus = false;
			this.treeViewCatalogMethods.Size = new System.Drawing.Size(204, 550);
			this.treeViewCatalogMethods.TabIndex = 5;
			this.treeViewCatalogMethods.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_AfterSelect);
			// 
			// imageListTreeImages
			// 
			this.imageListTreeImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTreeImages.ImageStream")));
			this.imageListTreeImages.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListTreeImages.Images.SetKeyName(0, "Cart");
			this.imageListTreeImages.Images.SetKeyName(1, "Catalog");
			this.imageListTreeImages.Images.SetKeyName(2, "Order");
			this.imageListTreeImages.Images.SetKeyName(3, "Disk");
			this.imageListTreeImages.Images.SetKeyName(4, "Settings");
			this.imageListTreeImages.Images.SetKeyName(5, "PriceCheck");
			this.imageListTreeImages.Images.SetKeyName(6, "Vendor");
			this.imageListTreeImages.Images.SetKeyName(7, "HostData");
			this.imageListTreeImages.Images.SetKeyName(8, "LaborRate");
			this.imageListTreeImages.Images.SetKeyName(9, "Part");
			this.imageListTreeImages.Images.SetKeyName(10, "Vehicle");
			this.imageListTreeImages.Images.SetKeyName(11, "Labor");
			this.imageListTreeImages.Images.SetKeyName(12, "Note");
			this.imageListTreeImages.Images.SetKeyName(13, "connection");
			this.imageListTreeImages.Images.SetKeyName(14, "Help");
			this.imageListTreeImages.Images.SetKeyName(15, "Location");
			this.imageListTreeImages.Images.SetKeyName(16, "Money");
			this.imageListTreeImages.Images.SetKeyName(17, "AltPart");
			this.imageListTreeImages.Images.SetKeyName(18, "Wrench");
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelCatalogName});
			this.statusStrip1.Location = new System.Drawing.Point(0, 588);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(792, 22);
			this.statusStrip1.TabIndex = 13;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabelCatalogName
			// 
			this.toolStripStatusLabelCatalogName.Name = "toolStripStatusLabelCatalogName";
			this.toolStripStatusLabelCatalogName.Size = new System.Drawing.Size(86, 17);
			this.toolStripStatusLabelCatalogName.Text = "Catalog : None";
			// 
			// openFileDialogSettings
			// 
			this.openFileDialogSettings.Filter = "Catalog Driver Settings|*.xml";
			this.openFileDialogSettings.Title = "Load Catalog Driver Settings";
			// 
			// saveFileDialogSettings
			// 
			this.saveFileDialogSettings.Filter = "Catalog Driver Settings|*.xml";
			this.saveFileDialogSettings.Title = "Save Catalog Driver Settings";
			// 
			// CatalogDriver
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(792, 610);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.menuStrip1);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MinimumSize = new System.Drawing.Size(800, 643);
			this.Name = "CatalogDriver";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CatalogDriver_FormClosing);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem setupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vehicleToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog openFileDialogCatalogDll;
		private System.Windows.Forms.SplitContainer splitContainer1;
		//private System.Windows.Forms.SplitContainer splitContainerMain;
		private System.Windows.Forms.TreeView treeViewCatalogMethods;
        private System.Windows.Forms.ImageList imageListTreeImages;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCatalogName;
		private System.Windows.Forms.ToolStripMenuItem vendorQualifierToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cartToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.OpenFileDialog openFileDialogSettings;
		private System.Windows.Forms.SaveFileDialog saveFileDialogSettings;
		private System.Windows.Forms.ToolStripMenuItem browserToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem hTML5TestToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem playgroundToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem whatIsMyUserAgentToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem licensesToolStripMenuItem;
    }
}

