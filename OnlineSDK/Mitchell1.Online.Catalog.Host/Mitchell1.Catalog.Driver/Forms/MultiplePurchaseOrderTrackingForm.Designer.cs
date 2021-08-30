
namespace Mitchell1.Catalog.Driver.Forms
{
    partial class MultiplePurchaseOrderTrackingForm
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.purchaseOrdersGrid = new System.Windows.Forms.DataGridView();
			this.requestTrackingDataButton = new System.Windows.Forms.Button();
			this.ConfirmationNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.TrackingNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Url = new System.Windows.Forms.DataGridViewLinkColumn();
			this.Parts = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.WasRetrieved = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.purchaseOrdersGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.purchaseOrdersGrid, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.requestTrackingDataButton, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// purchaseOrdersGrid
			// 
			this.purchaseOrdersGrid.AllowUserToAddRows = false;
			this.purchaseOrdersGrid.AllowUserToDeleteRows = false;
			this.purchaseOrdersGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.purchaseOrdersGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.purchaseOrdersGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.purchaseOrdersGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ConfirmationNumber,
            this.TrackingNumber,
            this.Status,
            this.Url,
            this.Parts,
            this.WasRetrieved});
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.purchaseOrdersGrid.DefaultCellStyle = dataGridViewCellStyle2;
			this.purchaseOrdersGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.purchaseOrdersGrid.Location = new System.Drawing.Point(3, 3);
			this.purchaseOrdersGrid.Name = "purchaseOrdersGrid";
			this.purchaseOrdersGrid.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.purchaseOrdersGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.purchaseOrdersGrid.Size = new System.Drawing.Size(794, 399);
			this.purchaseOrdersGrid.TabIndex = 0;
			// 
			// requestTrackingDataButton
			// 
			this.requestTrackingDataButton.BackColor = System.Drawing.Color.Blue;
			this.requestTrackingDataButton.Dock = System.Windows.Forms.DockStyle.Right;
			this.requestTrackingDataButton.ForeColor = System.Drawing.Color.White;
			this.requestTrackingDataButton.Location = new System.Drawing.Point(634, 408);
			this.requestTrackingDataButton.Name = "requestTrackingDataButton";
			this.requestTrackingDataButton.Size = new System.Drawing.Size(163, 39);
			this.requestTrackingDataButton.TabIndex = 1;
			this.requestTrackingDataButton.Text = "Request Tracking Data";
			this.requestTrackingDataButton.UseVisualStyleBackColor = false;
			this.requestTrackingDataButton.Click += new System.EventHandler(this.requestTrackingDataButton_Click);
			// 
			// ConfirmationNumber
			// 
			this.ConfirmationNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.ConfirmationNumber.DataPropertyName = "ConfirmationNumber";
			this.ConfirmationNumber.FillWeight = 99.49239F;
			this.ConfirmationNumber.HeaderText = "ConfirmationNumber";
			this.ConfirmationNumber.Name = "ConfirmationNumber";
			this.ConfirmationNumber.ReadOnly = true;
			this.ConfirmationNumber.Width = 150;
			// 
			// TrackingNumber
			// 
			this.TrackingNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.TrackingNumber.DataPropertyName = "TrackingNumber";
			this.TrackingNumber.FillWeight = 101.5228F;
			this.TrackingNumber.HeaderText = "TrackingNumber";
			this.TrackingNumber.Name = "TrackingNumber";
			this.TrackingNumber.ReadOnly = true;
			this.TrackingNumber.Width = 110;
			// 
			// Status
			// 
			this.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.Status.DataPropertyName = "Status";
			this.Status.FillWeight = 99.49239F;
			this.Status.HeaderText = "Status";
			this.Status.Name = "Status";
			this.Status.ReadOnly = true;
			// 
			// Url
			// 
			this.Url.DataPropertyName = "Url";
			this.Url.FillWeight = 99.49239F;
			this.Url.HeaderText = "URL";
			this.Url.Name = "Url";
			this.Url.ReadOnly = true;
			// 
			// Parts
			// 
			this.Parts.DataPropertyName = "Parts";
			this.Parts.HeaderText = "Parts";
			this.Parts.Name = "Parts";
			this.Parts.ReadOnly = true;
			this.Parts.Visible = false;
			// 
			// WasRetrieved
			// 
			this.WasRetrieved.DataPropertyName = "WasRetrieved";
			this.WasRetrieved.HeaderText = "WasRetrieved";
			this.WasRetrieved.Name = "WasRetrieved";
			this.WasRetrieved.ReadOnly = true;
			this.WasRetrieved.Visible = false;
			// 
			// MultiplePurchaseOrderTrackingForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.tableLayoutPanel1);
			this.MinimumSize = new System.Drawing.Size(800, 450);
			this.Name = "MultiplePurchaseOrderTrackingForm";
			this.Text = "Track Orders";
			this.tableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.purchaseOrdersGrid)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView purchaseOrdersGrid;
        private System.Windows.Forms.Button requestTrackingDataButton;
		private System.Windows.Forms.DataGridViewTextBoxColumn ConfirmationNumber;
		private System.Windows.Forms.DataGridViewTextBoxColumn TrackingNumber;
		private System.Windows.Forms.DataGridViewTextBoxColumn Status;
		private System.Windows.Forms.DataGridViewLinkColumn Url;
		private System.Windows.Forms.DataGridViewTextBoxColumn Parts;
		private System.Windows.Forms.DataGridViewTextBoxColumn WasRetrieved;
	}
}