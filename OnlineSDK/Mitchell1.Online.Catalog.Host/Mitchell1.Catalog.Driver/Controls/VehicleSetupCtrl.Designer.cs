namespace Mitchell1.Catalog.Driver.Controls
{
	partial class VehicleSetupCtrl
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
            this.textBoxDriveType = new System.Windows.Forms.TextBox();
            this.textBoxBody = new System.Windows.Forms.TextBox();
            this.checkBoxBody = new System.Windows.Forms.CheckBox();
            this.checkBoxDriveType = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxVehicle = new System.Windows.Forms.ComboBox();
            this.textBoxEngine = new System.Windows.Forms.TextBox();
            this.textBoxGVW = new System.Windows.Forms.TextBox();
            this.textBoxBrakes = new System.Windows.Forms.TextBox();
            this.textBoxTransmission = new System.Windows.Forms.TextBox();
            this.textBoxSubModel = new System.Windows.Forms.TextBox();
            this.checkBoxEngine = new System.Windows.Forms.CheckBox();
            this.checkBoxTransmission = new System.Windows.Forms.CheckBox();
            this.checkBoxBrakes = new System.Windows.Forms.CheckBox();
            this.checkBoxGVW = new System.Windows.Forms.CheckBox();
            this.checkBoxACES = new System.Windows.Forms.CheckBox();
            this.checkBoxAAIA = new System.Windows.Forms.CheckBox();
            this.checkBoxYear = new System.Windows.Forms.CheckBox();
            this.checkBoxMake = new System.Windows.Forms.CheckBox();
            this.checkBoxModel = new System.Windows.Forms.CheckBox();
            this.checkBoxSubModel = new System.Windows.Forms.CheckBox();
            this.checkBoxVIN = new System.Windows.Forms.CheckBox();
            this.textBoxVIN = new System.Windows.Forms.TextBox();
            this.textBoxMake = new System.Windows.Forms.TextBox();
            this.textBoxModel = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxACES = new System.Windows.Forms.MaskedTextBox();
            this.textBoxAAIA = new System.Windows.Forms.MaskedTextBox();
            this.textBoxYear = new System.Windows.Forms.MaskedTextBox();
            this.checkBoxACESBaseId = new System.Windows.Forms.CheckBox();
            this.textBoxACESBaseId = new System.Windows.Forms.MaskedTextBox();
            this.textBoxACESSubModelId = new System.Windows.Forms.MaskedTextBox();
            this.checkBoxACESSubModelID = new System.Windows.Forms.CheckBox();
            this.textBoxACESEngineCfgId = new System.Windows.Forms.MaskedTextBox();
            this.checkBoxACESEngineConfigID = new System.Windows.Forms.CheckBox();
            this.textBoxACESEngineid = new System.Windows.Forms.MaskedTextBox();
            this.checkBoxACESEngineId = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // textBoxDriveType
            // 
            this.textBoxDriveType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDriveType.Location = new System.Drawing.Point(126, 332);
            this.textBoxDriveType.Name = "textBoxDriveType";
            this.textBoxDriveType.Size = new System.Drawing.Size(281, 20);
            this.textBoxDriveType.TabIndex = 26;
            this.textBoxDriveType.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // textBoxBody
            // 
            this.textBoxBody.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBody.Location = new System.Drawing.Point(126, 308);
            this.textBoxBody.Name = "textBoxBody";
            this.textBoxBody.Size = new System.Drawing.Size(281, 20);
            this.textBoxBody.TabIndex = 24;
            this.textBoxBody.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // checkBoxBody
            // 
            this.checkBoxBody.AutoSize = true;
            this.checkBoxBody.Location = new System.Drawing.Point(29, 310);
            this.checkBoxBody.Name = "checkBoxBody";
            this.checkBoxBody.Size = new System.Drawing.Size(50, 17);
            this.checkBoxBody.TabIndex = 23;
            this.checkBoxBody.Text = "Body";
            this.checkBoxBody.UseVisualStyleBackColor = true;
            // 
            // checkBoxDriveType
            // 
            this.checkBoxDriveType.AutoSize = true;
            this.checkBoxDriveType.Location = new System.Drawing.Point(29, 334);
            this.checkBoxDriveType.Name = "checkBoxDriveType";
            this.checkBoxDriveType.Size = new System.Drawing.Size(78, 17);
            this.checkBoxDriveType.TabIndex = 25;
            this.checkBoxDriveType.Text = "Drive Type";
            this.checkBoxDriveType.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Configured Vehicles";
            // 
            // comboBoxVehicle
            // 
            this.comboBoxVehicle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxVehicle.FormattingEnabled = true;
            this.comboBoxVehicle.Items.AddRange(new object[] {
            "2000 Chrysler Town & Country",
            "1991 Ford LTD Crown Victoria Country Squire",
            "2004 Dodge Cab & Chassis R3500",
            "1999 Cadillac DeVille d\'Elegance"});
            this.comboBoxVehicle.Location = new System.Drawing.Point(126, 14);
            this.comboBoxVehicle.Name = "comboBoxVehicle";
            this.comboBoxVehicle.Size = new System.Drawing.Size(281, 21);
            this.comboBoxVehicle.TabIndex = 0;
            this.comboBoxVehicle.SelectedIndexChanged += new System.EventHandler(this.comboBoxVehicle_SelectedIndexChanged);
            // 
            // textBoxEngine
            // 
            this.textBoxEngine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxEngine.Location = new System.Drawing.Point(126, 212);
            this.textBoxEngine.Name = "textBoxEngine";
            this.textBoxEngine.Size = new System.Drawing.Size(281, 20);
            this.textBoxEngine.TabIndex = 16;
            this.textBoxEngine.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // textBoxGVW
            // 
            this.textBoxGVW.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxGVW.Location = new System.Drawing.Point(126, 284);
            this.textBoxGVW.Name = "textBoxGVW";
            this.textBoxGVW.Size = new System.Drawing.Size(281, 20);
            this.textBoxGVW.TabIndex = 22;
            this.textBoxGVW.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // textBoxBrakes
            // 
            this.textBoxBrakes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBrakes.Location = new System.Drawing.Point(126, 260);
            this.textBoxBrakes.Name = "textBoxBrakes";
            this.textBoxBrakes.Size = new System.Drawing.Size(281, 20);
            this.textBoxBrakes.TabIndex = 20;
            this.textBoxBrakes.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // textBoxTransmission
            // 
            this.textBoxTransmission.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTransmission.Location = new System.Drawing.Point(126, 236);
            this.textBoxTransmission.Name = "textBoxTransmission";
            this.textBoxTransmission.Size = new System.Drawing.Size(281, 20);
            this.textBoxTransmission.TabIndex = 18;
            this.textBoxTransmission.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // textBoxSubModel
            // 
            this.textBoxSubModel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSubModel.Location = new System.Drawing.Point(126, 188);
            this.textBoxSubModel.Name = "textBoxSubModel";
            this.textBoxSubModel.Size = new System.Drawing.Size(281, 20);
            this.textBoxSubModel.TabIndex = 14;
            this.textBoxSubModel.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // checkBoxEngine
            // 
            this.checkBoxEngine.AutoSize = true;
            this.checkBoxEngine.Location = new System.Drawing.Point(29, 214);
            this.checkBoxEngine.Name = "checkBoxEngine";
            this.checkBoxEngine.Size = new System.Drawing.Size(59, 17);
            this.checkBoxEngine.TabIndex = 15;
            this.checkBoxEngine.Text = "Engine";
            this.checkBoxEngine.UseVisualStyleBackColor = true;
            // 
            // checkBoxTransmission
            // 
            this.checkBoxTransmission.AutoSize = true;
            this.checkBoxTransmission.Location = new System.Drawing.Point(29, 238);
            this.checkBoxTransmission.Name = "checkBoxTransmission";
            this.checkBoxTransmission.Size = new System.Drawing.Size(87, 17);
            this.checkBoxTransmission.TabIndex = 17;
            this.checkBoxTransmission.Text = "Transmission";
            this.checkBoxTransmission.UseVisualStyleBackColor = true;
            // 
            // checkBoxBrakes
            // 
            this.checkBoxBrakes.AutoSize = true;
            this.checkBoxBrakes.Location = new System.Drawing.Point(29, 262);
            this.checkBoxBrakes.Name = "checkBoxBrakes";
            this.checkBoxBrakes.Size = new System.Drawing.Size(59, 17);
            this.checkBoxBrakes.TabIndex = 19;
            this.checkBoxBrakes.Text = "Brakes";
            this.checkBoxBrakes.UseVisualStyleBackColor = true;
            // 
            // checkBoxGVW
            // 
            this.checkBoxGVW.AutoSize = true;
            this.checkBoxGVW.Location = new System.Drawing.Point(29, 286);
            this.checkBoxGVW.Name = "checkBoxGVW";
            this.checkBoxGVW.Size = new System.Drawing.Size(52, 17);
            this.checkBoxGVW.TabIndex = 21;
            this.checkBoxGVW.Text = "GVW";
            this.checkBoxGVW.UseVisualStyleBackColor = true;
            // 
            // checkBoxACES
            // 
            this.checkBoxACES.AutoSize = true;
            this.checkBoxACES.Location = new System.Drawing.Point(29, 70);
            this.checkBoxACES.Name = "checkBoxACES";
            this.checkBoxACES.Size = new System.Drawing.Size(54, 17);
            this.checkBoxACES.TabIndex = 3;
            this.checkBoxACES.Text = "ACES";
            this.checkBoxACES.UseVisualStyleBackColor = true;
            // 
            // checkBoxAAIA
            // 
            this.checkBoxAAIA.AutoSize = true;
            this.checkBoxAAIA.Location = new System.Drawing.Point(29, 94);
            this.checkBoxAAIA.Name = "checkBoxAAIA";
            this.checkBoxAAIA.Size = new System.Drawing.Size(50, 17);
            this.checkBoxAAIA.TabIndex = 5;
            this.checkBoxAAIA.Text = "AAIA";
            this.checkBoxAAIA.UseVisualStyleBackColor = true;
            // 
            // checkBoxYear
            // 
            this.checkBoxYear.AutoSize = true;
            this.checkBoxYear.Location = new System.Drawing.Point(29, 118);
            this.checkBoxYear.Name = "checkBoxYear";
            this.checkBoxYear.Size = new System.Drawing.Size(48, 17);
            this.checkBoxYear.TabIndex = 7;
            this.checkBoxYear.Text = "Year";
            this.checkBoxYear.UseVisualStyleBackColor = true;
            // 
            // checkBoxMake
            // 
            this.checkBoxMake.AutoSize = true;
            this.checkBoxMake.Location = new System.Drawing.Point(29, 142);
            this.checkBoxMake.Name = "checkBoxMake";
            this.checkBoxMake.Size = new System.Drawing.Size(53, 17);
            this.checkBoxMake.TabIndex = 9;
            this.checkBoxMake.Text = "Make";
            this.checkBoxMake.UseVisualStyleBackColor = true;
            // 
            // checkBoxModel
            // 
            this.checkBoxModel.AutoSize = true;
            this.checkBoxModel.Location = new System.Drawing.Point(29, 166);
            this.checkBoxModel.Name = "checkBoxModel";
            this.checkBoxModel.Size = new System.Drawing.Size(55, 17);
            this.checkBoxModel.TabIndex = 11;
            this.checkBoxModel.Text = "Model";
            this.checkBoxModel.UseVisualStyleBackColor = true;
            // 
            // checkBoxSubModel
            // 
            this.checkBoxSubModel.AutoSize = true;
            this.checkBoxSubModel.Location = new System.Drawing.Point(29, 190);
            this.checkBoxSubModel.Name = "checkBoxSubModel";
            this.checkBoxSubModel.Size = new System.Drawing.Size(74, 17);
            this.checkBoxSubModel.TabIndex = 13;
            this.checkBoxSubModel.Text = "SubModel";
            this.checkBoxSubModel.UseVisualStyleBackColor = true;
            // 
            // checkBoxVIN
            // 
            this.checkBoxVIN.AutoSize = true;
            this.checkBoxVIN.Location = new System.Drawing.Point(29, 46);
            this.checkBoxVIN.Name = "checkBoxVIN";
            this.checkBoxVIN.Size = new System.Drawing.Size(44, 17);
            this.checkBoxVIN.TabIndex = 1;
            this.checkBoxVIN.Text = "VIN";
            this.checkBoxVIN.UseVisualStyleBackColor = true;
            // 
            // textBoxVIN
            // 
            this.textBoxVIN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxVIN.Location = new System.Drawing.Point(126, 44);
            this.textBoxVIN.Name = "textBoxVIN";
            this.textBoxVIN.Size = new System.Drawing.Size(281, 20);
            this.textBoxVIN.TabIndex = 2;
            this.textBoxVIN.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // textBoxMake
            // 
            this.textBoxMake.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMake.Location = new System.Drawing.Point(126, 140);
            this.textBoxMake.Name = "textBoxMake";
            this.textBoxMake.Size = new System.Drawing.Size(281, 20);
            this.textBoxMake.TabIndex = 10;
            this.textBoxMake.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // textBoxModel
            // 
            this.textBoxModel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxModel.Location = new System.Drawing.Point(126, 164);
            this.textBoxModel.Name = "textBoxModel";
            this.textBoxModel.Size = new System.Drawing.Size(281, 20);
            this.textBoxModel.TabIndex = 12;
            this.textBoxModel.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(29, 473);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(378, 23);
            this.buttonSave.TabIndex = 29;
            this.buttonSave.Text = "Set Vehicle";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxACES
            // 
            this.textBoxACES.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxACES.Location = new System.Drawing.Point(126, 68);
            this.textBoxACES.Mask = "#####";
            this.textBoxACES.Name = "textBoxACES";
            this.textBoxACES.PromptChar = ' ';
            this.textBoxACES.Size = new System.Drawing.Size(281, 20);
            this.textBoxACES.TabIndex = 4;
            this.textBoxACES.TextChanged += new System.EventHandler(this.MaskedTextBox_TextChanged);
            // 
            // textBoxAAIA
            // 
            this.textBoxAAIA.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAAIA.Location = new System.Drawing.Point(126, 92);
            this.textBoxAAIA.Mask = "#######";
            this.textBoxAAIA.Name = "textBoxAAIA";
            this.textBoxAAIA.PromptChar = ' ';
            this.textBoxAAIA.Size = new System.Drawing.Size(281, 20);
            this.textBoxAAIA.TabIndex = 6;
            this.textBoxAAIA.TextChanged += new System.EventHandler(this.MaskedTextBox_TextChanged);
            // 
            // textBoxYear
            // 
            this.textBoxYear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxYear.Location = new System.Drawing.Point(126, 116);
            this.textBoxYear.Mask = "####";
            this.textBoxYear.Name = "textBoxYear";
            this.textBoxYear.PromptChar = ' ';
            this.textBoxYear.Size = new System.Drawing.Size(281, 20);
            this.textBoxYear.TabIndex = 8;
            this.textBoxYear.TextChanged += new System.EventHandler(this.MaskedTextBox_TextChanged);
            // 
            // checkBoxACESBaseId
            // 
            this.checkBoxACESBaseId.AutoSize = true;
            this.checkBoxACESBaseId.Location = new System.Drawing.Point(29, 357);
            this.checkBoxACESBaseId.Name = "checkBoxACESBaseId";
            this.checkBoxACESBaseId.Size = new System.Drawing.Size(93, 17);
            this.checkBoxACESBaseId.TabIndex = 27;
            this.checkBoxACESBaseId.Text = "ACES Base Id";
            this.checkBoxACESBaseId.UseVisualStyleBackColor = true;
            // 
            // textBoxACESBaseId
            // 
            this.textBoxACESBaseId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxACESBaseId.Location = new System.Drawing.Point(126, 356);
            this.textBoxACESBaseId.Mask = "#######";
            this.textBoxACESBaseId.Name = "textBoxACESBaseId";
            this.textBoxACESBaseId.PromptChar = ' ';
            this.textBoxACESBaseId.Size = new System.Drawing.Size(281, 20);
            this.textBoxACESBaseId.TabIndex = 28;
            this.textBoxACESBaseId.TextChanged += new System.EventHandler(this.MaskedTextBox_TextChanged);
            // 
            // textBoxACESSubModelId
            // 
            this.textBoxACESSubModelId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxACESSubModelId.Location = new System.Drawing.Point(126, 405);
            this.textBoxACESSubModelId.Mask = "#######";
            this.textBoxACESSubModelId.Name = "textBoxACESSubModelId";
            this.textBoxACESSubModelId.PromptChar = ' ';
            this.textBoxACESSubModelId.Size = new System.Drawing.Size(281, 20);
            this.textBoxACESSubModelId.TabIndex = 32;
            this.textBoxACESSubModelId.TextChanged += new System.EventHandler(this.MaskedTextBox_TextChanged);
            // 
            // checkBoxACESSubModelID
            // 
            this.checkBoxACESSubModelID.AutoSize = true;
            this.checkBoxACESSubModelID.Location = new System.Drawing.Point(29, 406);
            this.checkBoxACESSubModelID.Name = "checkBoxACESSubModelID";
            this.checkBoxACESSubModelID.Size = new System.Drawing.Size(103, 17);
            this.checkBoxACESSubModelID.TabIndex = 31;
            this.checkBoxACESSubModelID.Text = "ACESSubModId";
            this.checkBoxACESSubModelID.UseVisualStyleBackColor = true;
            // 
            // textBoxACESEngineCfgId
            // 
            this.textBoxACESEngineCfgId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxACESEngineCfgId.Location = new System.Drawing.Point(126, 379);
            this.textBoxACESEngineCfgId.Mask = "#######";
            this.textBoxACESEngineCfgId.Name = "textBoxACESEngineCfgId";
            this.textBoxACESEngineCfgId.PromptChar = ' ';
            this.textBoxACESEngineCfgId.Size = new System.Drawing.Size(281, 20);
            this.textBoxACESEngineCfgId.TabIndex = 34;
            this.textBoxACESEngineCfgId.TextChanged += new System.EventHandler(this.MaskedTextBox_TextChanged);
            // 
            // checkBoxACESEngineConfigID
            // 
            this.checkBoxACESEngineConfigID.AutoSize = true;
            this.checkBoxACESEngineConfigID.Location = new System.Drawing.Point(29, 380);
            this.checkBoxACESEngineConfigID.Name = "checkBoxACESEngineConfigID";
            this.checkBoxACESEngineConfigID.Size = new System.Drawing.Size(98, 17);
            this.checkBoxACESEngineConfigID.TabIndex = 33;
            this.checkBoxACESEngineConfigID.Text = "ACESEngCfgId";
            this.checkBoxACESEngineConfigID.UseVisualStyleBackColor = true;
            // 
            // textBoxACESEngineid
            // 
            this.textBoxACESEngineid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxACESEngineid.Location = new System.Drawing.Point(126, 430);
            this.textBoxACESEngineid.Mask = "#######";
            this.textBoxACESEngineid.Name = "textBoxACESEngineid";
            this.textBoxACESEngineid.PromptChar = ' ';
            this.textBoxACESEngineid.Size = new System.Drawing.Size(281, 20);
            this.textBoxACESEngineid.TabIndex = 36;
            this.textBoxACESEngineid.TextChanged += new System.EventHandler(this.MaskedTextBox_TextChanged);
            // 
            // checkBoxAcesEngineId
            // 
            this.checkBoxACESEngineId.AutoSize = true;
            this.checkBoxACESEngineId.Location = new System.Drawing.Point(29, 431);
            this.checkBoxACESEngineId.Name = "checkBoxACESEngineId";
            this.checkBoxACESEngineId.Size = new System.Drawing.Size(82, 17);
            this.checkBoxACESEngineId.TabIndex = 35;
            this.checkBoxACESEngineId.Text = "ACESEngId";
            this.checkBoxACESEngineId.UseVisualStyleBackColor = true;
            // 
            // VehicleSetupCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxACESEngineid);
            this.Controls.Add(this.checkBoxACESEngineId);
            this.Controls.Add(this.textBoxACESEngineCfgId);
            this.Controls.Add(this.checkBoxACESEngineConfigID);
            this.Controls.Add(this.textBoxACESSubModelId);
            this.Controls.Add(this.checkBoxACESSubModelID);
            this.Controls.Add(this.textBoxACESBaseId);
            this.Controls.Add(this.checkBoxACESBaseId);
            this.Controls.Add(this.textBoxYear);
            this.Controls.Add(this.textBoxAAIA);
            this.Controls.Add(this.textBoxACES);
            this.Controls.Add(this.textBoxDriveType);
            this.Controls.Add(this.textBoxBody);
            this.Controls.Add(this.checkBoxBody);
            this.Controls.Add(this.checkBoxDriveType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxVehicle);
            this.Controls.Add(this.textBoxEngine);
            this.Controls.Add(this.textBoxGVW);
            this.Controls.Add(this.textBoxBrakes);
            this.Controls.Add(this.textBoxTransmission);
            this.Controls.Add(this.textBoxSubModel);
            this.Controls.Add(this.checkBoxEngine);
            this.Controls.Add(this.checkBoxTransmission);
            this.Controls.Add(this.checkBoxBrakes);
            this.Controls.Add(this.checkBoxGVW);
            this.Controls.Add(this.checkBoxACES);
            this.Controls.Add(this.checkBoxAAIA);
            this.Controls.Add(this.checkBoxYear);
            this.Controls.Add(this.checkBoxMake);
            this.Controls.Add(this.checkBoxModel);
            this.Controls.Add(this.checkBoxSubModel);
            this.Controls.Add(this.checkBoxVIN);
            this.Controls.Add(this.textBoxVIN);
            this.Controls.Add(this.textBoxMake);
            this.Controls.Add(this.textBoxModel);
            this.Controls.Add(this.buttonSave);
            this.Name = "VehicleSetupCtrl";
            this.Size = new System.Drawing.Size(425, 509);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxDriveType;
		private System.Windows.Forms.TextBox textBoxBody;
		private System.Windows.Forms.CheckBox checkBoxBody;
		private System.Windows.Forms.CheckBox checkBoxDriveType;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBoxVehicle;
		private System.Windows.Forms.TextBox textBoxEngine;
		private System.Windows.Forms.TextBox textBoxGVW;
		private System.Windows.Forms.TextBox textBoxBrakes;
		private System.Windows.Forms.TextBox textBoxTransmission;
		private System.Windows.Forms.TextBox textBoxSubModel;
		private System.Windows.Forms.CheckBox checkBoxEngine;
		private System.Windows.Forms.CheckBox checkBoxTransmission;
		private System.Windows.Forms.CheckBox checkBoxBrakes;
		private System.Windows.Forms.CheckBox checkBoxGVW;
		private System.Windows.Forms.CheckBox checkBoxACES;
		private System.Windows.Forms.CheckBox checkBoxAAIA;
		private System.Windows.Forms.CheckBox checkBoxYear;
		private System.Windows.Forms.CheckBox checkBoxMake;
		private System.Windows.Forms.CheckBox checkBoxModel;
		private System.Windows.Forms.CheckBox checkBoxSubModel;
		private System.Windows.Forms.CheckBox checkBoxVIN;
		private System.Windows.Forms.TextBox textBoxVIN;
		private System.Windows.Forms.TextBox textBoxMake;
		private System.Windows.Forms.TextBox textBoxModel;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.MaskedTextBox textBoxACES;
		private System.Windows.Forms.MaskedTextBox textBoxAAIA;
		private System.Windows.Forms.MaskedTextBox textBoxYear;
		private System.Windows.Forms.CheckBox checkBoxACESBaseId;
		private System.Windows.Forms.MaskedTextBox textBoxACESBaseId;
        private System.Windows.Forms.MaskedTextBox textBoxACESSubModelId;
        private System.Windows.Forms.CheckBox checkBoxACESSubModelID;
        private System.Windows.Forms.MaskedTextBox textBoxACESEngineCfgId;
        private System.Windows.Forms.CheckBox checkBoxACESEngineConfigID;
        private System.Windows.Forms.MaskedTextBox textBoxACESEngineid;
        private System.Windows.Forms.CheckBox checkBoxACESEngineId;
	}
}
