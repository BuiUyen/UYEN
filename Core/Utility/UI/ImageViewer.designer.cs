namespace Sanita.Utility.UI
{
    partial class BarcodeImageViewer
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
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.picLabel = new System.Windows.Forms.PictureBox();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLabel)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.pnlContainer);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(468, 278);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(468, 303);
            this.toolStripContainer1.TabIndex = 2;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // pnlContainer
            // 
            this.pnlContainer.AutoScroll = true;
            this.pnlContainer.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pnlContainer.Controls.Add(this.picLabel);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(468, 278);
            this.pnlContainer.TabIndex = 0;
            // 
            // picLabel
            // 
            this.picLabel.Location = new System.Drawing.Point(175, 110);
            this.picLabel.Name = "picLabel";
            this.picLabel.Size = new System.Drawing.Size(100, 50);
            this.picLabel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picLabel.TabIndex = 0;
            this.picLabel.TabStop = false;
            // 
            // ImageViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "ImageViewer";
            this.Size = new System.Drawing.Size(468, 303);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.pnlContainer.ResumeLayout(false);
            this.pnlContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLabel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.PictureBox picLabel;
    }
}
