namespace Medibox
{
    partial class FormConfigSchema
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
            this.panel_login = new DevComponents.DotNetBar.PanelEx();
            this.printProgress = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.txtStatus = new DevComponents.DotNetBar.LabelX();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.panel_login.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_login
            // 
            this.panel_login.CanvasColor = System.Drawing.SystemColors.Control;
            this.panel_login.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panel_login.Controls.Add(this.panelEx2);
            this.panel_login.Controls.Add(this.printProgress);
            this.panel_login.Controls.Add(this.txtStatus);
            this.panel_login.DisabledBackColor = System.Drawing.Color.Empty;
            this.panel_login.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_login.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel_login.Location = new System.Drawing.Point(0, 0);
            this.panel_login.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel_login.Name = "panel_login";
            this.panel_login.Size = new System.Drawing.Size(494, 95);
            this.panel_login.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panel_login.Style.BackColor1.Color = System.Drawing.SystemColors.Control;
            this.panel_login.Style.BackColor2.Color = System.Drawing.SystemColors.Control;
            this.panel_login.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panel_login.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panel_login.Style.GradientAngle = 90;
            this.panel_login.TabIndex = 11;
            // 
            // printProgress
            // 
            // 
            // 
            // 
            this.printProgress.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.printProgress.Location = new System.Drawing.Point(12, 12);
            this.printProgress.Name = "printProgress";
            this.printProgress.Size = new System.Drawing.Size(470, 23);
            this.printProgress.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.printProgress.TabIndex = 28;
            // 
            // txtStatus
            // 
            // 
            // 
            // 
            this.txtStatus.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtStatus.Location = new System.Drawing.Point(12, 48);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(470, 23);
            this.txtStatus.TabIndex = 7;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.btnSave);
            this.panelEx2.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx2.Location = new System.Drawing.Point(0, 55);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Padding = new System.Windows.Forms.Padding(1);
            this.panelEx2.Size = new System.Drawing.Size(494, 40);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.panelEx2.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.Top;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 52;
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSave.Location = new System.Drawing.Point(368, 1);
            this.btnSave.Name = "btnSave";
            this.btnSave.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlS);
            this.btnSave.Size = new System.Drawing.Size(125, 38);
            this.btnSave.TabIndex = 39;
            this.btnSave.Text = "Hủy Bỏ";
            this.btnSave.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // FormConfigSchema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 95);
            this.Controls.Add(this.panel_login);
            this.Name = "FormConfigSchema";
            this.Text = "Nâng Cấp Cơ Sở Dữ Liệu";
            this.Load += new System.EventHandler(this.FormPrintProgress_Load);
            this.panel_login.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panel_login;
        private DevComponents.DotNetBar.LabelX txtStatus;
        private DevComponents.DotNetBar.Controls.ProgressBarX printProgress;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.ButtonX btnSave;
    }
}