namespace Mitchell1.Catalog.Driver.Controls
{
    partial class CatalogConfiguration
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
			this.comboBoxApiLevel = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxDescription = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxApiUrl = new System.Windows.Forms.TextBox();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPageBasic = new System.Windows.Forms.TabPage();
			this.label13 = new System.Windows.Forms.Label();
			this.dataGridViewURLs = new System.Windows.Forms.DataGridView();
			this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.textBoxIdentifier = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.textBoxSupportPhone = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textBoxSite = new System.Windows.Forms.TextBox();
			this.tabPageAdvanced = new System.Windows.Forms.TabPage();
			this.checkBoxDeliver = new System.Windows.Forms.CheckBox();
			this.checkBoxAllowsNotFoundPartsToBeOrdered = new System.Windows.Forms.CheckBox();
			this.checkBoxSupportsLocation = new System.Windows.Forms.CheckBox();
			this.checkBoxSupportsOrderMessage = new System.Windows.Forms.CheckBox();
			this.checkBoxSupportsPriceCheck = new System.Windows.Forms.CheckBox();
			this.checkBoxRequiresPriceCheck = new System.Windows.Forms.CheckBox();
			this.checkBoxSupportsAlternateLocations = new System.Windows.Forms.CheckBox();
			this.checkBoxSupportsAlternateParts = new System.Windows.Forms.CheckBox();
			this.checkBoxAllowsBlankManufacturerCode = new System.Windows.Forms.CheckBox();
			this.buttonUse = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.buttonLoad = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.linkLabelConfig = new System.Windows.Forms.LinkLabel();
			this.label6 = new System.Windows.Forms.Label();
            this.checkBoxMultiplePOs = new System.Windows.Forms.CheckBox();
			this.tabControl.SuspendLayout();
			this.tabPageBasic.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewURLs)).BeginInit();
			this.tabPageAdvanced.SuspendLayout();
			this.SuspendLayout();
			// 
			// comboBoxApiLevel
			// 
			this.comboBoxApiLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxApiLevel.FormattingEnabled = true;
			this.comboBoxApiLevel.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
			this.comboBoxApiLevel.Location = new System.Drawing.Point(108, 23);
			this.comboBoxApiLevel.Name = "comboBoxApiLevel";
			this.comboBoxApiLevel.Size = new System.Drawing.Size(200, 21);
			this.comboBoxApiLevel.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(49, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "API Level";
			// 
			// textBoxDescription
			// 
			this.textBoxDescription.Location = new System.Drawing.Point(131, 68);
			this.textBoxDescription.Name = "textBoxDescription";
			this.textBoxDescription.Size = new System.Drawing.Size(200, 20);
			this.textBoxDescription.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(51, 45);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Display Name";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(65, 71);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(60, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Description";
			// 
			// textBoxName
			// 
			this.textBoxName.Location = new System.Drawing.Point(131, 42);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(200, 20);
			this.textBoxName.TabIndex = 1;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(36, 149);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(86, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "Base Catalog Url";
			// 
			// textBoxApiUrl
			// 
			this.textBoxApiUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxApiUrl.Location = new System.Drawing.Point(131, 146);
			this.textBoxApiUrl.Name = "textBoxApiUrl";
			this.textBoxApiUrl.Size = new System.Drawing.Size(496, 20);
			this.textBoxApiUrl.TabIndex = 5;
			// 
			// tabControl
			// 
			this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl.Controls.Add(this.tabPageBasic);
			this.tabControl.Controls.Add(this.tabPageAdvanced);
			this.tabControl.Location = new System.Drawing.Point(12, 46);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(641, 376);
			this.tabControl.TabIndex = 0;
			// 
			// tabPageBasic
			// 
			this.tabPageBasic.Controls.Add(this.label13);
			this.tabPageBasic.Controls.Add(this.dataGridViewURLs);
			this.tabPageBasic.Controls.Add(this.textBoxIdentifier);
			this.tabPageBasic.Controls.Add(this.label12);
			this.tabPageBasic.Controls.Add(this.label11);
			this.tabPageBasic.Controls.Add(this.textBoxSupportPhone);
			this.tabPageBasic.Controls.Add(this.label10);
			this.tabPageBasic.Controls.Add(this.textBoxSite);
			this.tabPageBasic.Controls.Add(this.label4);
			this.tabPageBasic.Controls.Add(this.textBoxApiUrl);
			this.tabPageBasic.Controls.Add(this.textBoxName);
			this.tabPageBasic.Controls.Add(this.label3);
			this.tabPageBasic.Controls.Add(this.label2);
			this.tabPageBasic.Controls.Add(this.textBoxDescription);
			this.tabPageBasic.Location = new System.Drawing.Point(4, 22);
			this.tabPageBasic.Name = "tabPageBasic";
			this.tabPageBasic.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageBasic.Size = new System.Drawing.Size(633, 350);
			this.tabPageBasic.TabIndex = 0;
			this.tabPageBasic.Text = "Basic";
			this.tabPageBasic.UseVisualStyleBackColor = true;
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(128, 178);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(113, 13);
			this.label13.TabIndex = 28;
			this.label13.Text = "Sub URL Components";
			// 
			// dataGridViewURLs
			// 
			this.dataGridViewURLs.AllowUserToAddRows = false;
			this.dataGridViewURLs.AllowUserToDeleteRows = false;
			this.dataGridViewURLs.AllowUserToResizeColumns = false;
			this.dataGridViewURLs.AllowUserToResizeRows = false;
			this.dataGridViewURLs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewURLs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewURLs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnName,
            this.ColumnValue});
			this.dataGridViewURLs.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.dataGridViewURLs.Location = new System.Drawing.Point(131, 194);
			this.dataGridViewURLs.MultiSelect = false;
			this.dataGridViewURLs.Name = "dataGridViewURLs";
			this.dataGridViewURLs.RowHeadersVisible = false;
			this.dataGridViewURLs.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.dataGridViewURLs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewURLs.ShowCellErrors = false;
			this.dataGridViewURLs.ShowCellToolTips = false;
			this.dataGridViewURLs.ShowEditingIcon = false;
			this.dataGridViewURLs.ShowRowErrors = false;
			this.dataGridViewURLs.Size = new System.Drawing.Size(496, 150);
			this.dataGridViewURLs.TabIndex = 6;
			// 
			// ColumnName
			// 
			this.ColumnName.DataPropertyName = "Name";
			this.ColumnName.HeaderText = "Purpose";
			this.ColumnName.Name = "ColumnName";
			this.ColumnName.ReadOnly = true;
			this.ColumnName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			// 
			// ColumnValue
			// 
			this.ColumnValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColumnValue.DataPropertyName = "Value";
			this.ColumnValue.HeaderText = "URL Component";
			this.ColumnValue.Name = "ColumnValue";
			// 
			// textBoxIdentifier
			// 
			this.textBoxIdentifier.Location = new System.Drawing.Point(131, 16);
			this.textBoxIdentifier.Name = "textBoxIdentifier";
			this.textBoxIdentifier.ReadOnly = true;
			this.textBoxIdentifier.Size = new System.Drawing.Size(200, 20);
			this.textBoxIdentifier.TabIndex = 0;
			this.textBoxIdentifier.Text = "Id";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(75, 19);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(47, 13);
			this.label12.TabIndex = 25;
			this.label12.Text = "Identifier";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(47, 123);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(78, 13);
			this.label11.TabIndex = 24;
			this.label11.Text = "Support Phone";
			// 
			// textBoxSupportPhone
			// 
			this.textBoxSupportPhone.Location = new System.Drawing.Point(131, 120);
			this.textBoxSupportPhone.Name = "textBoxSupportPhone";
			this.textBoxSupportPhone.Size = new System.Drawing.Size(200, 20);
			this.textBoxSupportPhone.TabIndex = 4;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(57, 97);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(65, 13);
			this.label10.TabIndex = 22;
			this.label10.Text = "Support Site";
			// 
			// textBoxSite
			// 
			this.textBoxSite.Location = new System.Drawing.Point(131, 94);
			this.textBoxSite.Name = "textBoxSite";
			this.textBoxSite.Size = new System.Drawing.Size(200, 20);
			this.textBoxSite.TabIndex = 3;
			// 
			// tabPageAdvanced
			// 
            this.tabPageAdvanced.Controls.Add(this.checkBoxMultiplePOs);
			this.tabPageAdvanced.Controls.Add(this.checkBoxDeliver);
			this.tabPageAdvanced.Controls.Add(this.checkBoxAllowsNotFoundPartsToBeOrdered);
			this.tabPageAdvanced.Controls.Add(this.checkBoxSupportsLocation);
			this.tabPageAdvanced.Controls.Add(this.label1);
			this.tabPageAdvanced.Controls.Add(this.comboBoxApiLevel);
			this.tabPageAdvanced.Controls.Add(this.checkBoxSupportsOrderMessage);
			this.tabPageAdvanced.Controls.Add(this.checkBoxSupportsPriceCheck);
			this.tabPageAdvanced.Controls.Add(this.checkBoxRequiresPriceCheck);
			this.tabPageAdvanced.Controls.Add(this.checkBoxSupportsAlternateLocations);
			this.tabPageAdvanced.Controls.Add(this.checkBoxSupportsAlternateParts);
			this.tabPageAdvanced.Controls.Add(this.checkBoxAllowsBlankManufacturerCode);
			this.tabPageAdvanced.Location = new System.Drawing.Point(4, 22);
			this.tabPageAdvanced.Name = "tabPageAdvanced";
			this.tabPageAdvanced.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageAdvanced.Size = new System.Drawing.Size(633, 350);
			this.tabPageAdvanced.TabIndex = 1;
			this.tabPageAdvanced.Text = "Advanced";
			this.tabPageAdvanced.UseVisualStyleBackColor = true;
			// 
			// checkBoxDeliver
			// 
			this.checkBoxDeliver.AutoSize = true;
			this.checkBoxDeliver.Location = new System.Drawing.Point(332, 102);
			this.checkBoxDeliver.Name = "checkBoxDeliver";
			this.checkBoxDeliver.Size = new System.Drawing.Size(128, 17);
			this.checkBoxDeliver.TabIndex = 8;
			this.checkBoxDeliver.Text = "Show Deliver/WillCall";
			this.checkBoxDeliver.UseVisualStyleBackColor = true;
			// 
			// checkBoxAllowsNotFoundPartsToBeOrdered
			// 
			this.checkBoxAllowsNotFoundPartsToBeOrdered.AutoSize = true;
			this.checkBoxAllowsNotFoundPartsToBeOrdered.Location = new System.Drawing.Point(52, 125);
			this.checkBoxAllowsNotFoundPartsToBeOrdered.Name = "checkBoxAllowsNotFoundPartsToBeOrdered";
			this.checkBoxAllowsNotFoundPartsToBeOrdered.Size = new System.Drawing.Size(191, 17);
			this.checkBoxAllowsNotFoundPartsToBeOrdered.TabIndex = 7;
			this.checkBoxAllowsNotFoundPartsToBeOrdered.Text = "AllowsNotFoundPartsToBeOrdered";
			this.checkBoxAllowsNotFoundPartsToBeOrdered.UseVisualStyleBackColor = true;
			// 
			// checkBoxSupportsLocation
			// 
			this.checkBoxSupportsLocation.AutoSize = true;
			this.checkBoxSupportsLocation.Location = new System.Drawing.Point(52, 217);
			this.checkBoxSupportsLocation.Name = "checkBoxSupportsLocation";
			this.checkBoxSupportsLocation.Size = new System.Drawing.Size(109, 17);
			this.checkBoxSupportsLocation.TabIndex = 6;
			this.checkBoxSupportsLocation.Text = "SupportsLocation";
			this.checkBoxSupportsLocation.UseVisualStyleBackColor = true;
			// 
			// checkBoxSupportsOrderMessage
			// 
			this.checkBoxSupportsOrderMessage.AutoSize = true;
			this.checkBoxSupportsOrderMessage.Location = new System.Drawing.Point(52, 240);
			this.checkBoxSupportsOrderMessage.Name = "checkBoxSupportsOrderMessage";
			this.checkBoxSupportsOrderMessage.Size = new System.Drawing.Size(137, 17);
			this.checkBoxSupportsOrderMessage.TabIndex = 5;
			this.checkBoxSupportsOrderMessage.Text = "SupportsOrderMessage";
			this.checkBoxSupportsOrderMessage.UseVisualStyleBackColor = true;
			// 
			// checkBoxSupportsPriceCheck
			// 
			this.checkBoxSupportsPriceCheck.AutoSize = true;
			this.checkBoxSupportsPriceCheck.Checked = true;
			this.checkBoxSupportsPriceCheck.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxSupportsPriceCheck.Location = new System.Drawing.Point(52, 263);
			this.checkBoxSupportsPriceCheck.Name = "checkBoxSupportsPriceCheck";
			this.checkBoxSupportsPriceCheck.Size = new System.Drawing.Size(123, 17);
			this.checkBoxSupportsPriceCheck.TabIndex = 4;
			this.checkBoxSupportsPriceCheck.Text = "SupportsPriceCheck";
			this.checkBoxSupportsPriceCheck.UseVisualStyleBackColor = true;
			// 
			// checkBoxRequiresPriceCheck
			// 
			this.checkBoxRequiresPriceCheck.AutoSize = true;
			this.checkBoxRequiresPriceCheck.Location = new System.Drawing.Point(52, 148);
			this.checkBoxRequiresPriceCheck.Name = "checkBoxRequiresPriceCheck";
			this.checkBoxRequiresPriceCheck.Size = new System.Drawing.Size(123, 17);
			this.checkBoxRequiresPriceCheck.TabIndex = 3;
			this.checkBoxRequiresPriceCheck.Text = "RequiresPriceCheck";
			this.checkBoxRequiresPriceCheck.UseVisualStyleBackColor = true;
			// 
			// checkBoxSupportsAlternateLocations
			// 
			this.checkBoxSupportsAlternateLocations.AutoSize = true;
			this.checkBoxSupportsAlternateLocations.Location = new System.Drawing.Point(52, 171);
			this.checkBoxSupportsAlternateLocations.Name = "checkBoxSupportsAlternateLocations";
			this.checkBoxSupportsAlternateLocations.Size = new System.Drawing.Size(156, 17);
			this.checkBoxSupportsAlternateLocations.TabIndex = 2;
			this.checkBoxSupportsAlternateLocations.Text = "SupportsAlternateLocations";
			this.checkBoxSupportsAlternateLocations.UseVisualStyleBackColor = true;
			// 
			// checkBoxSupportsAlternateParts
			// 
			this.checkBoxSupportsAlternateParts.AutoSize = true;
			this.checkBoxSupportsAlternateParts.Location = new System.Drawing.Point(52, 194);
			this.checkBoxSupportsAlternateParts.Name = "checkBoxSupportsAlternateParts";
			this.checkBoxSupportsAlternateParts.Size = new System.Drawing.Size(134, 17);
			this.checkBoxSupportsAlternateParts.TabIndex = 1;
			this.checkBoxSupportsAlternateParts.Text = "SupportsAlternateParts";
			this.checkBoxSupportsAlternateParts.UseVisualStyleBackColor = true;
			// 
			// checkBoxAllowsBlankManufacturerCode
			// 
			this.checkBoxAllowsBlankManufacturerCode.AutoSize = true;
			this.checkBoxAllowsBlankManufacturerCode.Location = new System.Drawing.Point(52, 102);
			this.checkBoxAllowsBlankManufacturerCode.Name = "checkBoxAllowsBlankManufacturerCode";
			this.checkBoxAllowsBlankManufacturerCode.Size = new System.Drawing.Size(171, 17);
			this.checkBoxAllowsBlankManufacturerCode.TabIndex = 0;
			this.checkBoxAllowsBlankManufacturerCode.Text = "AllowsBlankManufacturerCode";
			this.checkBoxAllowsBlankManufacturerCode.UseVisualStyleBackColor = true;
			// 
			// buttonUse
			// 
			this.buttonUse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonUse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonUse.Location = new System.Drawing.Point(575, 428);
			this.buttonUse.Name = "buttonUse";
			this.buttonUse.Size = new System.Drawing.Size(74, 23);
			this.buttonUse.TabIndex = 3;
			this.buttonUse.Text = "Save";
			this.toolTip1.SetToolTip(this.buttonUse, "Save these settings");
			this.buttonUse.UseVisualStyleBackColor = true;
			this.buttonUse.Click += new System.EventHandler(this.buttonUse_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.Location = new System.Drawing.Point(506, 428);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(63, 23);
			this.buttonCancel.TabIndex = 2;
			this.buttonCancel.Text = "Reset";
			this.toolTip1.SetToolTip(this.buttonCancel, "Reset back to demo settings");
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonLoad
			// 
			this.buttonLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonLoad.Location = new System.Drawing.Point(437, 428);
			this.buttonLoad.Name = "buttonLoad";
			this.buttonLoad.Size = new System.Drawing.Size(63, 23);
			this.buttonLoad.TabIndex = 7;
			this.buttonLoad.Text = "Load";
			this.toolTip1.SetToolTip(this.buttonLoad, "Load alternate config file");
			this.buttonLoad.UseVisualStyleBackColor = true;
			this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(14, 433);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(71, 13);
			this.label5.TabIndex = 4;
			this.label5.Text = "Config File:";
			// 
			// linkLabelConfig
			// 
			this.linkLabelConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.linkLabelConfig.AutoSize = true;
			this.linkLabelConfig.Location = new System.Drawing.Point(91, 433);
			this.linkLabelConfig.Name = "linkLabelConfig";
			this.linkLabelConfig.Size = new System.Drawing.Size(55, 13);
			this.linkLabelConfig.TabIndex = 5;
			this.linkLabelConfig.TabStop = true;
			this.linkLabelConfig.Text = "linkLabel1";
			this.linkLabelConfig.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelConfig_LinkClicked);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(13, 15);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(304, 13);
			this.label6.TabIndex = 6;
			this.label6.Text = "Configure && Save Online Catalog to use test harness";
            // 
            // checkBoxMultiplePOs
            // 
            this.checkBoxMultiplePOs.AutoSize = true;
            this.checkBoxMultiplePOs.Location = new System.Drawing.Point(332, 125);
            this.checkBoxMultiplePOs.Name = "checkBoxMultiplePOs";
            this.checkBoxMultiplePOs.Size = new System.Drawing.Size(180, 17);
            this.checkBoxMultiplePOs.TabIndex = 9;
            this.checkBoxMultiplePOs.Text = "SupportsMultiplePurchaseOrders";
            this.checkBoxMultiplePOs.UseVisualStyleBackColor = true;
			// 
			// CatalogConfiguration
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.buttonLoad);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.linkLabelConfig);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonUse);
			this.Controls.Add(this.tabControl);
			this.Name = "CatalogConfiguration";
			this.Size = new System.Drawing.Size(668, 458);
			this.tabControl.ResumeLayout(false);
			this.tabPageBasic.ResumeLayout(false);
			this.tabPageBasic.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewURLs)).EndInit();
			this.tabPageAdvanced.ResumeLayout(false);
			this.tabPageAdvanced.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBoxApiLevel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxApiUrl;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageBasic;
        private System.Windows.Forms.TabPage tabPageAdvanced;
        private System.Windows.Forms.CheckBox checkBoxAllowsBlankManufacturerCode;
        private System.Windows.Forms.CheckBox checkBoxAllowsNotFoundPartsToBeOrdered;
        private System.Windows.Forms.CheckBox checkBoxSupportsLocation;
        private System.Windows.Forms.CheckBox checkBoxSupportsOrderMessage;
        private System.Windows.Forms.CheckBox checkBoxSupportsPriceCheck;
        private System.Windows.Forms.CheckBox checkBoxRequiresPriceCheck;
        private System.Windows.Forms.CheckBox checkBoxSupportsAlternateLocations;
        private System.Windows.Forms.CheckBox checkBoxSupportsAlternateParts;
        private System.Windows.Forms.Button buttonUse;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxSupportPhone;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxSite;
        private System.Windows.Forms.CheckBox checkBoxDeliver;
        private System.Windows.Forms.TextBox textBoxIdentifier;
        private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.DataGridView dataGridViewURLs;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnValue;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.LinkLabel linkLabelConfig;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.CheckBox checkBoxMultiplePOs;
	}
}

