namespace Mitchell1.Online.Catalog.Host
{
	sealed partial class CatalogHostingForm
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
            this.catalogHostingControl = new Mitchell1.Online.Catalog.Host.CatalogHostingControl();
            this.SuspendLayout();
            // 
            // catalogHostingControl
            // 
            this.catalogHostingControl.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.catalogHostingControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.catalogHostingControl.Location = new System.Drawing.Point(0, 0);
            this.catalogHostingControl.Name = "catalogHostingControl";
            this.catalogHostingControl.OnlineCatalogInformation = null;
            this.catalogHostingControl.Size = new System.Drawing.Size(730, 592);
            this.catalogHostingControl.TabIndex = 0;
            this.catalogHostingControl.Text = "catalogHostingControl";
            // 
            // CatalogHostingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 592);
            this.Controls.Add(this.catalogHostingControl);
            this.MinimizeBox = false;
            this.Name = "CatalogHostingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Catalog";
            this.ResumeLayout(false);

        }

        #endregion

        private CatalogHostingControl catalogHostingControl;
    }
}

