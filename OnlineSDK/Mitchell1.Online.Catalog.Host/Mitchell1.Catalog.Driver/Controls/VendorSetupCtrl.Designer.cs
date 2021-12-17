namespace Mitchell1.Catalog.Driver.Controls
{
	partial class VendorSetupCtrl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Vendor", 6, 6);
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Host Data", 7, 7);
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Catalog Settings", 4, 4, new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VendorSetupCtrl));
			this.buttonVendorSetup = new System.Windows.Forms.Button();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.treeViewVendor = new System.Windows.Forms.TreeView();
			this.imageListTreeImages = new System.Windows.Forms.ImageList(this.components);
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonVendorSetup
			// 
			this.buttonVendorSetup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.buttonVendorSetup.Location = new System.Drawing.Point(12, 13);
			this.buttonVendorSetup.Name = "buttonVendorSetup";
			this.buttonVendorSetup.Size = new System.Drawing.Size(175, 28);
			this.buttonVendorSetup.TabIndex = 0;
			this.buttonVendorSetup.Text = "Setup Vendor";
			this.buttonVendorSetup.UseVisualStyleBackColor = true;
			this.buttonVendorSetup.Click += new System.EventHandler(this.buttonVendorSetup_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.buttonVendorSetup);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new System.Drawing.Size(600, 400);
			this.splitContainer1.SplitterDistance = 200;
			this.splitContainer1.TabIndex = 1;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.treeViewVendor);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.propertyGrid1);
			this.splitContainer2.Size = new System.Drawing.Size(396, 400);
			this.splitContainer2.SplitterDistance = 209;
			this.splitContainer2.TabIndex = 0;
			// 
			// treeViewVendor
			// 
			this.treeViewVendor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.treeViewVendor.Cursor = System.Windows.Forms.Cursors.Hand;
			this.treeViewVendor.ImageIndex = 0;
			this.treeViewVendor.ImageList = this.imageListTreeImages;
			this.treeViewVendor.Location = new System.Drawing.Point(3, 3);
			this.treeViewVendor.Name = "treeViewVendor";
			treeNode1.ImageIndex = 6;
			treeNode1.Name = "Vendor";
			treeNode1.SelectedImageIndex = 6;
			treeNode1.Text = "Vendor";
			treeNode2.ImageIndex = 7;
			treeNode2.Name = "HostData";
			treeNode2.SelectedImageIndex = 7;
			treeNode2.Text = "Host Data";
			treeNode3.ImageIndex = 4;
			treeNode3.Name = "CatalogSettings";
			treeNode3.SelectedImageIndex = 4;
			treeNode3.Text = "Catalog Settings";
			this.treeViewVendor.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3});
			this.treeViewVendor.SelectedImageIndex = 0;
			this.treeViewVendor.Size = new System.Drawing.Size(390, 203);
			this.treeViewVendor.TabIndex = 0;
			this.treeViewVendor.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewVendor_NodeMouseClick);
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
			// propertyGrid1
			// 
			this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.propertyGrid1.Location = new System.Drawing.Point(3, -1);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.Size = new System.Drawing.Size(390, 185);
			this.propertyGrid1.TabIndex = 0;
			// 
			// VendorSetupCtrl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "VendorSetupCtrl";
			this.Size = new System.Drawing.Size(600, 400);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			this.splitContainer2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonVendorSetup;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.TreeView treeViewVendor;
		private System.Windows.Forms.PropertyGrid propertyGrid1;
		private System.Windows.Forms.ImageList imageListTreeImages;
	}
}
