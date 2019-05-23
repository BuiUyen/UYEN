namespace Sanita.Utility.SplashScreen
{
    partial class FormSplashScreen
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lbStatusInfo = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.Label();
            this.txtLogo = new System.Windows.Forms.PictureBox();
            this.txtProgress = new DevComponents.DotNetBar.Controls.CircularProgress();
            this.btnSave = new System.Windows.Forms.Button();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.txtHeader = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txtLogo)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbStatusInfo
            // 
            this.lbStatusInfo.AutoSize = true;
            this.lbStatusInfo.BackColor = System.Drawing.Color.Transparent;
            this.lbStatusInfo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatusInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.lbStatusInfo.Location = new System.Drawing.Point(12, 274);
            this.lbStatusInfo.Name = "lbStatusInfo";
            this.lbStatusInfo.Size = new System.Drawing.Size(72, 18);
            this.lbStatusInfo.TabIndex = 0;
            this.lbStatusInfo.Text = "Loading...";
            this.lbStatusInfo.Visible = false;
            // 
            // txtTitle
            // 
            this.txtTitle.BackColor = System.Drawing.Color.Transparent;
            this.txtTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTitle.ForeColor = System.Drawing.Color.White;
            this.txtTitle.Location = new System.Drawing.Point(10, 1);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(566, 130);
            this.txtTitle.TabIndex = 0;
            this.txtTitle.Text = "Clinic Information System\r\n1.0";
            this.txtTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.txtTitle.Visible = false;
            // 
            // txtLogo
            // 
            this.txtLogo.BackColor = System.Drawing.Color.Transparent;            
            this.txtLogo.Location = new System.Drawing.Point(0, 1);
            this.txtLogo.Name = "txtLogo";
            this.txtLogo.Size = new System.Drawing.Size(84, 66);
            this.txtLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.txtLogo.TabIndex = 1;
            this.txtLogo.TabStop = false;
            this.txtLogo.Visible = false;
            // 
            // txtProgress
            // 
            this.txtProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(144)))), ((int)(((byte)(164)))));
            // 
            // 
            // 
            this.txtProgress.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtProgress.Location = new System.Drawing.Point(275, 215);
            this.txtProgress.Name = "txtProgress";
            this.txtProgress.ProgressBarType = DevComponents.DotNetBar.eCircularProgressType.Dot;
            this.txtProgress.ProgressColor = System.Drawing.Color.White;
            this.txtProgress.ProgressTextColor = System.Drawing.Color.White;
            this.txtProgress.ProgressTextFormat = "";
            this.txtProgress.ProgressTextVisible = true;
            this.txtProgress.Size = new System.Drawing.Size(50, 50);
            this.txtProgress.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.txtProgress.TabIndex = 204;
            this.txtProgress.Value = 100;
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.Location = new System.Drawing.Point(259, 118);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(11, 12);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Lưu (Ctrl+S)";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.txtTitle);
            this.panelEx2.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx2.Location = new System.Drawing.Point(12, 69);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Padding = new System.Windows.Forms.Padding(10, 1, 10, 1);
            this.panelEx2.Size = new System.Drawing.Size(586, 132);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 237;
            // 
            // txtHeader
            // 
            this.txtHeader.AutoSize = true;
            this.txtHeader.BackColor = System.Drawing.Color.Transparent;
            this.txtHeader.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHeader.ForeColor = System.Drawing.Color.White;
            this.txtHeader.Location = new System.Drawing.Point(101, 23);
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.Size = new System.Drawing.Size(473, 23);
            this.txtHeader.TabIndex = 241;
            this.txtHeader.Text = "Công Ty TNHH Giải Pháp Phần Mềm MediBox Việt Nam";
            this.txtHeader.Visible = false;
            // 
            // FormSplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(144)))), ((int)(((byte)(164)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(600, 300);
            this.Controls.Add(this.txtHeader);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.txtProgress);
            this.Controls.Add(this.txtLogo);
            this.Controls.Add(this.lbStatusInfo);
            this.Controls.Add(this.btnSave);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormSplashScreen";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormSplashScreen";
            ((System.ComponentModel.ISupportInitialize)(this.txtLogo)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbStatusInfo;
        private System.Windows.Forms.Label txtTitle;
        private System.Windows.Forms.PictureBox txtLogo;
        private DevComponents.DotNetBar.Controls.CircularProgress txtProgress;
        private System.Windows.Forms.Button btnSave;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private System.Windows.Forms.Label txtHeader;
    }
}