namespace Mitchell1.Online.Catalog.Host
{
	partial class CatalogErrorForm
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

			if (browserControl != null)
			{
				browserControl.Dispose();
				browserControl = null;
			}
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.buttonOK = new System.Windows.Forms.Button();
			this.panelBrowser = new System.Windows.Forms.Panel();
			this.groupBox = new System.Windows.Forms.GroupBox();
			this.labelPhone = new System.Windows.Forms.Label();
			this.linkLabelSupport = new System.Windows.Forms.LinkLabel();
			this.groupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonOK.Location = new System.Drawing.Point(366, 29);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 25);
			this.buttonOK.TabIndex = 0;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// panelBrowser
			// 
			this.panelBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelBrowser.Location = new System.Drawing.Point(7, 7);
			this.panelBrowser.Name = "panelBrowser";
			this.panelBrowser.Padding = new System.Windows.Forms.Padding(3);
			this.panelBrowser.Size = new System.Drawing.Size(453, 179);
			this.panelBrowser.TabIndex = 1;
			// 
			// groupBox
			// 
			this.groupBox.Controls.Add(this.labelPhone);
			this.groupBox.Controls.Add(this.linkLabelSupport);
			this.groupBox.Controls.Add(this.buttonOK);
			this.groupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupBox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox.Location = new System.Drawing.Point(7, 186);
			this.groupBox.Name = "groupBox";
			this.groupBox.Size = new System.Drawing.Size(453, 66);
			this.groupBox.TabIndex = 0;
			this.groupBox.TabStop = false;
			this.groupBox.Text = "[Catalog Name]";
			// 
			// labelPhone
			// 
			this.labelPhone.AutoSize = true;
			this.labelPhone.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelPhone.Location = new System.Drawing.Point(25, 43);
			this.labelPhone.Name = "labelPhone";
			this.labelPhone.Size = new System.Drawing.Size(133, 14);
			this.labelPhone.TabIndex = 2;
			this.labelPhone.Text = "Support Phone #555-5555";
			// 
			// linkLabelSupport
			// 
			this.linkLabelSupport.AutoSize = true;
			this.linkLabelSupport.Location = new System.Drawing.Point(25, 22);
			this.linkLabelSupport.Name = "linkLabelSupport";
			this.linkLabelSupport.Size = new System.Drawing.Size(89, 14);
			this.linkLabelSupport.TabIndex = 2;
			this.linkLabelSupport.TabStop = true;
			this.linkLabelSupport.Text = "Online Support";
			this.linkLabelSupport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSupport_LinkClicked);
			// 
			// CatalogErrorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(467, 264);
			this.ControlBox = false;
			this.Controls.Add(this.panelBrowser);
			this.Controls.Add(this.groupBox);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MaximizeBox = false;
			this.MinimumSize = new System.Drawing.Size(483, 280);
			this.Name = "CatalogErrorForm";
			this.Padding = new System.Windows.Forms.Padding(7, 7, 7, 12);
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "CatalogErrorForm";
			this.groupBox.ResumeLayout(false);
			this.groupBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Panel panelBrowser;
		private System.Windows.Forms.GroupBox groupBox;
		private System.Windows.Forms.Label labelPhone;
		private System.Windows.Forms.LinkLabel linkLabelSupport;
	}
}