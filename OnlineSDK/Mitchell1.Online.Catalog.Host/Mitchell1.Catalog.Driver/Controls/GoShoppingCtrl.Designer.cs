namespace Mitchell1.Catalog.Driver.Controls
{
    partial class GoShoppingCtrl
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
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Part Items (0)", 18, 18);
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Labor Items (0)", 11, 11);
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Note Items (0)", 12, 12);
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Cart (0)", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
			System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("ICartOrder\'s (0)");
			System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Vendor", 6, 6);
			System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Vehicle", 10, 10);
			System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Host Data", 15, 15);
			System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Catalog Settings", 4, 4, new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7,
            treeNode8});
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GoShoppingCtrl));
			this.buttonDemoImage = new System.Windows.Forms.Button();
			this.buttonGoShopping = new System.Windows.Forms.Button();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.buttonClearCart = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.treeViewCart = new System.Windows.Forms.TreeView();
			this.imageListTreeImages = new System.Windows.Forms.ImageList(this.components);
			this.propertyGridCartItems = new System.Windows.Forms.PropertyGrid();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonDemoImage
			// 
			this.buttonDemoImage.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.buttonDemoImage.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.buttonDemoImage.FlatAppearance.BorderSize = 0;
			this.buttonDemoImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonDemoImage.Location = new System.Drawing.Point(57, 28);
			this.buttonDemoImage.Margin = new System.Windows.Forms.Padding(0);
			this.buttonDemoImage.Name = "buttonDemoImage";
			this.buttonDemoImage.Size = new System.Drawing.Size(59, 29);
			this.buttonDemoImage.TabIndex = 0;
			this.buttonDemoImage.UseVisualStyleBackColor = false;
			this.buttonDemoImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonDemoImage_MouseDown);
			this.buttonDemoImage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonDemoImage_MouseUp);
			// 
			// buttonGoShopping
			// 
			this.buttonGoShopping.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonGoShopping.Location = new System.Drawing.Point(11, 11);
			this.buttonGoShopping.Margin = new System.Windows.Forms.Padding(2);
			this.buttonGoShopping.Name = "buttonGoShopping";
			this.buttonGoShopping.Size = new System.Drawing.Size(175, 28);
			this.buttonGoShopping.TabIndex = 1;
			this.buttonGoShopping.Text = "Go Shopping";
			this.buttonGoShopping.UseVisualStyleBackColor = true;
			this.buttonGoShopping.Click += new System.EventHandler(this.buttonGoShopping_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.buttonClearCart);
			this.splitContainer1.Panel1.Controls.Add(this.buttonGoShopping);
			this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new System.Drawing.Size(600, 400);
			this.splitContainer1.SplitterDistance = 200;
			this.splitContainer1.SplitterWidth = 3;
			this.splitContainer1.TabIndex = 2;
			// 
			// buttonClearCart
			// 
			this.buttonClearCart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClearCart.Location = new System.Drawing.Point(11, 360);
			this.buttonClearCart.Margin = new System.Windows.Forms.Padding(2);
			this.buttonClearCart.Name = "buttonClearCart";
			this.buttonClearCart.Size = new System.Drawing.Size(175, 28);
			this.buttonClearCart.TabIndex = 3;
			this.buttonClearCart.Text = "Empty Cart";
			this.buttonClearCart.UseVisualStyleBackColor = true;
			this.buttonClearCart.Click += new System.EventHandler(this.buttonClearCart_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.buttonDemoImage);
			this.groupBox1.Location = new System.Drawing.Point(11, 44);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(175, 77);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Button Image Preview";
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Margin = new System.Windows.Forms.Padding(2);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.treeViewCart);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.propertyGridCartItems);
			this.splitContainer2.Size = new System.Drawing.Size(397, 400);
			this.splitContainer2.SplitterDistance = 209;
			this.splitContainer2.SplitterWidth = 3;
			this.splitContainer2.TabIndex = 2;
			// 
			// treeViewCart
			// 
			this.treeViewCart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.treeViewCart.Cursor = System.Windows.Forms.Cursors.Hand;
			this.treeViewCart.ImageIndex = 0;
			this.treeViewCart.ImageList = this.imageListTreeImages;
			this.treeViewCart.Location = new System.Drawing.Point(2, 2);
			this.treeViewCart.Margin = new System.Windows.Forms.Padding(2);
			this.treeViewCart.Name = "treeViewCart";
			treeNode1.ImageIndex = 18;
			treeNode1.Name = "PartItems";
			treeNode1.SelectedImageIndex = 18;
			treeNode1.Text = "Part Items (0)";
			treeNode2.ImageIndex = 11;
			treeNode2.Name = "LaborItems";
			treeNode2.SelectedImageIndex = 11;
			treeNode2.Text = "Labor Items (0)";
			treeNode3.ImageIndex = 12;
			treeNode3.Name = "NoteItems";
			treeNode3.SelectedImageIndex = 12;
			treeNode3.Text = "Note Items (0)";
			treeNode4.Name = "Cart";
			treeNode4.Text = "Cart (0)";
			treeNode5.Name = "OrderCart";
			treeNode5.Text = "ICartOrder\'s (0)";
			treeNode6.ImageIndex = 6;
			treeNode6.Name = "Vendor";
			treeNode6.SelectedImageIndex = 6;
			treeNode6.Text = "Vendor";
			treeNode7.ImageIndex = 10;
			treeNode7.Name = "Vehicle";
			treeNode7.SelectedImageIndex = 10;
			treeNode7.Text = "Vehicle";
			treeNode8.ImageIndex = 15;
			treeNode8.Name = "HostData";
			treeNode8.SelectedImageIndex = 15;
			treeNode8.Text = "Host Data";
			treeNode9.ImageIndex = 4;
			treeNode9.Name = "CatalogSettings";
			treeNode9.SelectedImageIndex = 4;
			treeNode9.Text = "Catalog Settings";
			this.treeViewCart.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5,
            treeNode9});
			this.treeViewCart.SelectedImageIndex = 0;
			this.treeViewCart.Size = new System.Drawing.Size(393, 205);
			this.treeViewCart.TabIndex = 0;
			this.treeViewCart.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewCart_NodeMouseClick);
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
			// propertyGridCartItems
			// 
			this.propertyGridCartItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.propertyGridCartItems.Location = new System.Drawing.Point(2, 2);
			this.propertyGridCartItems.Margin = new System.Windows.Forms.Padding(2);
			this.propertyGridCartItems.Name = "propertyGridCartItems";
			this.propertyGridCartItems.Size = new System.Drawing.Size(393, 184);
			this.propertyGridCartItems.TabIndex = 0;
			// 
			// GoShoppingCtrl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "GoShoppingCtrl";
			this.Size = new System.Drawing.Size(600, 400);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonDemoImage;
        private System.Windows.Forms.Button buttonGoShopping;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeViewCart;
        private System.Windows.Forms.ImageList imageListTreeImages;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.PropertyGrid propertyGridCartItems;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button buttonClearCart;
    }
}
