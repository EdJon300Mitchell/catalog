namespace Mitchell1.Online.Catalog.Host
{
    partial class CatalogHostingForm
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
            this.catalogHostingControl1 = new Mitchell1.Online.Catalog.Host.CatalogHostingControl();
            this.SuspendLayout();
            // 
            // catalogHostingControl1
            // 
            this.catalogHostingControl1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.catalogHostingControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.catalogHostingControl1.Location = new System.Drawing.Point(0, 0);
            this.catalogHostingControl1.Name = "catalogHostingControl1";
            this.catalogHostingControl1.Size = new System.Drawing.Size(707, 414);
            this.catalogHostingControl1.TabIndex = 0;
            this.catalogHostingControl1.Text = "catalogHostingControl1";
            // 
            // CatalogHostingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 414);
            this.Controls.Add(this.catalogHostingControl1);
            this.MinimizeBox = false;
            this.Name = "CatalogHostingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Catalog";
            this.ResumeLayout(false);

        }

        #endregion

        private CatalogHostingControl catalogHostingControl1;
    }
}

