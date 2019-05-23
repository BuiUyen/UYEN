namespace Medibox.Utility.UI
{
    partial class GalleryView
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
            System.Windows.Forms.ColumnHeader colName;
            this.mListView = new System.Windows.Forms.ListView();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // colName
            // 
            colName.Text = "Name";
            // 
            // mListView
            // 
            this.mListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            colName});
            this.mListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mListView.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.mListView.Location = new System.Drawing.Point(0, 0);
            this.mListView.Name = "mListView";
            this.mListView.Size = new System.Drawing.Size(179, 481);
            this.mListView.TabIndex = 2;
            this.mListView.UseCompatibleStateImageBehavior = false;
            this.mListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mListView_MouseDoubleClick);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.mListView);
            this.panelEx2.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.panelEx2.Size = new System.Drawing.Size(180, 481);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.panelEx2.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.Right;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 21;
            // 
            // GalleryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelEx2);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "GalleryView";
            this.Size = new System.Drawing.Size(180, 481);
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView mListView;
        private DevComponents.DotNetBar.PanelEx panelEx2;

    }
}
