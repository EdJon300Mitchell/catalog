namespace Mitchell1.Catalog.Driver.Controls
{
    partial class PriceCheckCtrl
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
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Parts (0)", 18, 18);
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PriceCheckCtrl));
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Vendor", 6, 6);
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Vehicle", 10, 10);
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Host Data", 15, 15);
			System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Catalog Settings", 4, 4, new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4});
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.buttonOrderTracking = new System.Windows.Forms.Button();
			this.deliveryMethodLabel = new System.Windows.Forms.GroupBox();
			this.radioButtonDeliver = new System.Windows.Forms.RadioButton();
			this.radioButtonWillCall = new System.Windows.Forms.RadioButton();
			this.orderMessageLabel = new System.Windows.Forms.Label();
			this.orderMessage = new System.Windows.Forms.TextBox();
			this.buttonOrderParts = new System.Windows.Forms.Button();
			this.buttonPriceCheck = new System.Windows.Forms.Button();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.treeViewCart = new System.Windows.Forms.TreeView();
			this.imageListTreeImages = new System.Windows.Forms.ImageList(this.components);
			this.treeViewAppSettings = new System.Windows.Forms.TreeView();
			this.propertyGridCartItems = new System.Windows.Forms.PropertyGrid();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textBoxPONumber = new System.Windows.Forms.TextBox();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.deliveryMethodLabel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
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
			this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
			this.splitContainer1.Panel1.Controls.Add(this.buttonOrderTracking);
			this.splitContainer1.Panel1.Controls.Add(this.deliveryMethodLabel);
			this.splitContainer1.Panel1.Controls.Add(this.orderMessageLabel);
			this.splitContainer1.Panel1.Controls.Add(this.orderMessage);
			this.splitContainer1.Panel1.Controls.Add(this.buttonOrderParts);
			this.splitContainer1.Panel1.Controls.Add(this.buttonPriceCheck);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new System.Drawing.Size(659, 521);
			this.splitContainer1.SplitterDistance = 219;
			this.splitContainer1.SplitterWidth = 3;
			this.splitContainer1.TabIndex = 3;
			// 
			// buttonOrderTracking
			// 
			this.buttonOrderTracking.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOrderTracking.Location = new System.Drawing.Point(14, 478);
			this.buttonOrderTracking.Margin = new System.Windows.Forms.Padding(2);
			this.buttonOrderTracking.Name = "buttonOrderTracking";
			this.buttonOrderTracking.Size = new System.Drawing.Size(194, 28);
			this.buttonOrderTracking.TabIndex = 7;
			this.buttonOrderTracking.Text = "Track Order...";
			this.buttonOrderTracking.UseVisualStyleBackColor = true;
			this.buttonOrderTracking.Click += new System.EventHandler(this.buttonOrderTracking_Click);
			// 
			// deliveryMethodLabel
			// 
			this.deliveryMethodLabel.AutoSize = true;
			this.deliveryMethodLabel.Controls.Add(this.radioButtonDeliver);
			this.deliveryMethodLabel.Controls.Add(this.radioButtonWillCall);
			this.deliveryMethodLabel.Location = new System.Drawing.Point(14, 201);
			this.deliveryMethodLabel.Name = "deliveryMethodLabel";
			this.deliveryMethodLabel.Size = new System.Drawing.Size(175, 87);
			this.deliveryMethodLabel.TabIndex = 6;
			this.deliveryMethodLabel.TabStop = false;
			this.deliveryMethodLabel.Text = "Delivery Method";
			this.deliveryMethodLabel.Visible = false;
			// 
			// radioButtonDeliver
			// 
			this.radioButtonDeliver.AutoSize = true;
			this.radioButtonDeliver.Location = new System.Drawing.Point(25, 28);
			this.radioButtonDeliver.Name = "radioButtonDeliver";
			this.radioButtonDeliver.Size = new System.Drawing.Size(58, 17);
			this.radioButtonDeliver.TabIndex = 8;
			this.radioButtonDeliver.TabStop = true;
			this.radioButtonDeliver.Tag = "1";
			this.radioButtonDeliver.Text = "Deliver";
			this.radioButtonDeliver.UseVisualStyleBackColor = true;
			// 
			// radioButtonWillCall
			// 
			this.radioButtonWillCall.AutoSize = true;
			this.radioButtonWillCall.Location = new System.Drawing.Point(25, 51);
			this.radioButtonWillCall.Name = "radioButtonWillCall";
			this.radioButtonWillCall.Size = new System.Drawing.Size(59, 17);
			this.radioButtonWillCall.TabIndex = 7;
			this.radioButtonWillCall.TabStop = true;
			this.radioButtonWillCall.Tag = "0";
			this.radioButtonWillCall.Text = "WillCall";
			this.radioButtonWillCall.UseVisualStyleBackColor = true;
			// 
			// orderMessageLabel
			// 
			this.orderMessageLabel.AutoSize = true;
			this.orderMessageLabel.Location = new System.Drawing.Point(11, 76);
			this.orderMessageLabel.Name = "orderMessageLabel";
			this.orderMessageLabel.Size = new System.Drawing.Size(152, 13);
			this.orderMessageLabel.TabIndex = 4;
			this.orderMessageLabel.Text = "Order Message (Sent w/Order)";
			this.orderMessageLabel.Visible = false;
			// 
			// orderMessage
			// 
			this.orderMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.orderMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.orderMessage.Location = new System.Drawing.Point(14, 92);
			this.orderMessage.Multiline = true;
			this.orderMessage.Name = "orderMessage";
			this.orderMessage.Size = new System.Drawing.Size(194, 102);
			this.orderMessage.TabIndex = 3;
			this.orderMessage.Visible = false;
			// 
			// buttonOrderParts
			// 
			this.buttonOrderParts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOrderParts.Location = new System.Drawing.Point(14, 43);
			this.buttonOrderParts.Margin = new System.Windows.Forms.Padding(2);
			this.buttonOrderParts.Name = "buttonOrderParts";
			this.buttonOrderParts.Size = new System.Drawing.Size(194, 28);
			this.buttonOrderParts.TabIndex = 2;
			this.buttonOrderParts.Text = "Order Parts";
			this.buttonOrderParts.UseVisualStyleBackColor = true;
			this.buttonOrderParts.Click += new System.EventHandler(this.buttonOrderParts_Click);
			// 
			// buttonPriceCheck
			// 
			this.buttonPriceCheck.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonPriceCheck.Location = new System.Drawing.Point(14, 11);
			this.buttonPriceCheck.Margin = new System.Windows.Forms.Padding(2);
			this.buttonPriceCheck.Name = "buttonPriceCheck";
			this.buttonPriceCheck.Size = new System.Drawing.Size(194, 28);
			this.buttonPriceCheck.TabIndex = 1;
			this.buttonPriceCheck.Text = "Price Check";
			this.buttonPriceCheck.UseVisualStyleBackColor = true;
			this.buttonPriceCheck.Click += new System.EventHandler(this.buttonPriceCheck_Click);
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
			this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.propertyGridCartItems);
			this.splitContainer2.Size = new System.Drawing.Size(437, 521);
			this.splitContainer2.SplitterDistance = 273;
			this.splitContainer2.SplitterWidth = 3;
			this.splitContainer2.TabIndex = 2;
			// 
			// splitContainer3
			// 
			this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer3.Location = new System.Drawing.Point(0, 0);
			this.splitContainer3.Name = "splitContainer3";
			this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.treeViewCart);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.treeViewAppSettings);
			this.splitContainer3.Size = new System.Drawing.Size(437, 273);
			this.splitContainer3.SplitterDistance = 187;
			this.splitContainer3.TabIndex = 1;
			// 
			// treeViewCart
			// 
			this.treeViewCart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.treeViewCart.CheckBoxes = true;
			this.treeViewCart.Cursor = System.Windows.Forms.Cursors.Hand;
			this.treeViewCart.ImageIndex = 0;
			this.treeViewCart.ImageList = this.imageListTreeImages;
			this.treeViewCart.Location = new System.Drawing.Point(2, 2);
			this.treeViewCart.Margin = new System.Windows.Forms.Padding(2);
			this.treeViewCart.Name = "treeViewCart";
			treeNode1.ImageIndex = 18;
			treeNode1.Name = "Parts";
			treeNode1.SelectedImageIndex = 18;
			treeNode1.Text = "Parts (0)";
			this.treeViewCart.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
			this.treeViewCart.SelectedImageIndex = 0;
			this.treeViewCart.Size = new System.Drawing.Size(433, 183);
			this.treeViewCart.TabIndex = 0;
			this.treeViewCart.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewCart_BeforeCheck);
			this.treeViewCart.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewCart_AfterCheck);
			this.treeViewCart.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewCart_NodeMouseClick);
			this.treeViewCart.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewCart_NodeMouseDoubleClick);
			this.treeViewCart.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.treeViewCart_KeyPress);
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
			// treeViewAppSettings
			// 
			this.treeViewAppSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.treeViewAppSettings.Cursor = System.Windows.Forms.Cursors.Hand;
			this.treeViewAppSettings.ImageIndex = 0;
			this.treeViewAppSettings.ImageList = this.imageListTreeImages;
			this.treeViewAppSettings.Location = new System.Drawing.Point(2, 2);
			this.treeViewAppSettings.Margin = new System.Windows.Forms.Padding(2);
			this.treeViewAppSettings.Name = "treeViewAppSettings";
			treeNode2.ImageIndex = 6;
			treeNode2.Name = "Vendor";
			treeNode2.SelectedImageIndex = 6;
			treeNode2.Text = "Vendor";
			treeNode3.ImageIndex = 10;
			treeNode3.Name = "Vehicle";
			treeNode3.SelectedImageIndex = 10;
			treeNode3.Text = "Vehicle";
			treeNode4.ImageIndex = 15;
			treeNode4.Name = "HostData";
			treeNode4.SelectedImageIndex = 15;
			treeNode4.Text = "Host Data";
			treeNode5.ImageIndex = 4;
			treeNode5.Name = "CatalogSettings";
			treeNode5.SelectedImageIndex = 4;
			treeNode5.Text = "Catalog Settings";
			this.treeViewAppSettings.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5});
			this.treeViewAppSettings.SelectedImageIndex = 0;
			this.treeViewAppSettings.Size = new System.Drawing.Size(433, 78);
			this.treeViewAppSettings.TabIndex = 1;
			this.treeViewAppSettings.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewCart_NodeMouseClick);
			// 
			// propertyGridCartItems
			// 
			this.propertyGridCartItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.propertyGridCartItems.Location = new System.Drawing.Point(2, 2);
			this.propertyGridCartItems.Margin = new System.Windows.Forms.Padding(2);
			this.propertyGridCartItems.Name = "propertyGridCartItems";
			this.propertyGridCartItems.Size = new System.Drawing.Size(433, 244);
			this.propertyGridCartItems.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(3, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(188, 81);
			this.label1.TabIndex = 8;
			this.label1.Text = "String value. Generated by calling app. Typically always unique; however, same PO" +
    " # can be used to order multiple times when manually restocking parts";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.textBoxPONumber);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(14, 308);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(194, 120);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Purchase Order Number";
			// 
			// textBoxPONumber
			// 
			this.textBoxPONumber.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.textBoxPONumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxPONumber.Location = new System.Drawing.Point(3, 97);
			this.textBoxPONumber.Name = "textBoxPONumber";
			this.textBoxPONumber.Size = new System.Drawing.Size(188, 20);
			this.textBoxPONumber.TabIndex = 9;
			this.textBoxPONumber.Text = "1000";
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
			// 
			// PriceCheckCtrl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "PriceCheckCtrl";
			this.Size = new System.Drawing.Size(659, 521);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.deliveryMethodLabel.ResumeLayout(false);
			this.deliveryMethodLabel.PerformLayout();
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
			this.splitContainer3.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button buttonPriceCheck;
        private System.Windows.Forms.PropertyGrid propertyGridCartItems;
        private System.Windows.Forms.TreeView treeViewCart;
        private System.Windows.Forms.Button buttonOrderParts;
        private System.Windows.Forms.ImageList imageListTreeImages;
		private System.Windows.Forms.SplitContainer splitContainer3;
		private System.Windows.Forms.TreeView treeViewAppSettings;
		private System.Windows.Forms.Label orderMessageLabel;
		private System.Windows.Forms.TextBox orderMessage;
		private System.Windows.Forms.GroupBox deliveryMethodLabel;
        private System.Windows.Forms.RadioButton radioButtonDeliver;
        private System.Windows.Forms.RadioButton radioButtonWillCall;
		private System.Windows.Forms.Button buttonOrderTracking;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxPONumber;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
	}
}
