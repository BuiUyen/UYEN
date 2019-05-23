namespace Medibox
{
    partial class FormConfigDatabase
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
            this.local_txtUser = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.local_txtServer = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtAccount = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.local_txtPassword = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtPassword = new DevComponents.DotNetBar.LabelX();
            this.local_txtDatabase = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // local_txtUser
            // 
            // 
            // 
            // 
            this.local_txtUser.Border.Class = "TextBoxBorder";
            this.local_txtUser.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.local_txtUser.FocusHighlightColor = System.Drawing.Color.LightYellow;
            this.local_txtUser.FocusHighlightEnabled = true;
            this.local_txtUser.Location = new System.Drawing.Point(10, 93);
            this.local_txtUser.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.local_txtUser.Name = "local_txtUser";
            this.local_txtUser.Size = new System.Drawing.Size(327, 23);
            this.local_txtUser.TabIndex = 1;
            // 
            // local_txtServer
            // 
            // 
            // 
            // 
            this.local_txtServer.Border.Class = "TextBoxBorder";
            this.local_txtServer.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.local_txtServer.FocusHighlightColor = System.Drawing.Color.LightYellow;
            this.local_txtServer.FocusHighlightEnabled = true;
            this.local_txtServer.Location = new System.Drawing.Point(10, 39);
            this.local_txtServer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.local_txtServer.Name = "local_txtServer";
            this.local_txtServer.Size = new System.Drawing.Size(327, 23);
            this.local_txtServer.TabIndex = 0;
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(10, 66);
            this.labelX3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(101, 23);
            this.labelX3.TabIndex = 20;
            this.labelX3.Text = "Tên đăng nhập";
            // 
            // txtAccount
            // 
            this.txtAccount.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.txtAccount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtAccount.Location = new System.Drawing.Point(10, 12);
            this.txtAccount.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.Size = new System.Drawing.Size(253, 23);
            this.txtAccount.TabIndex = 20;
            this.txtAccount.Text = "Địa chỉ máy chủ cơ sở dữ liệu";
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(10, 120);
            this.labelX2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(101, 23);
            this.labelX2.TabIndex = 19;
            this.labelX2.Text = "Mật khẩu";
            // 
            // local_txtPassword
            // 
            // 
            // 
            // 
            this.local_txtPassword.Border.Class = "TextBoxBorder";
            this.local_txtPassword.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.local_txtPassword.FocusHighlightColor = System.Drawing.Color.LightYellow;
            this.local_txtPassword.FocusHighlightEnabled = true;
            this.local_txtPassword.Location = new System.Drawing.Point(10, 147);
            this.local_txtPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.local_txtPassword.Name = "local_txtPassword";
            this.local_txtPassword.PasswordChar = '*';
            this.local_txtPassword.Size = new System.Drawing.Size(327, 23);
            this.local_txtPassword.TabIndex = 2;
            this.local_txtPassword.UseSystemPasswordChar = true;
            this.local_txtPassword.TextChanged += new System.EventHandler(this.local_txtPassword_TextChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.txtPassword.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPassword.Location = new System.Drawing.Point(10, 174);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(101, 23);
            this.txtPassword.TabIndex = 19;
            this.txtPassword.Text = "Tên cơ sở dữ liệu";
            // 
            // local_txtDatabase
            // 
            // 
            // 
            // 
            this.local_txtDatabase.Border.Class = "TextBoxBorder";
            this.local_txtDatabase.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.local_txtDatabase.FocusHighlightColor = System.Drawing.Color.LightYellow;
            this.local_txtDatabase.FocusHighlightEnabled = true;
            this.local_txtDatabase.Location = new System.Drawing.Point(10, 201);
            this.local_txtDatabase.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.local_txtDatabase.Name = "local_txtDatabase";
            this.local_txtDatabase.Size = new System.Drawing.Size(327, 23);
            this.local_txtDatabase.TabIndex = 3;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelEx1.Controls.Add(this.panelEx2);
            this.panelEx1.Controls.Add(this.local_txtUser);
            this.panelEx1.Controls.Add(this.local_txtDatabase);
            this.panelEx1.Controls.Add(this.local_txtServer);
            this.panelEx1.Controls.Add(this.labelX3);
            this.panelEx1.Controls.Add(this.txtPassword);
            this.panelEx1.Controls.Add(this.txtAccount);
            this.panelEx1.Controls.Add(this.local_txtPassword);
            this.panelEx1.Controls.Add(this.labelX2);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(349, 275);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.panelEx1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 32;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.buttonX2);
            this.panelEx2.Controls.Add(this.buttonX1);
            this.panelEx2.Controls.Add(this.btnSave);
            this.panelEx2.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx2.Location = new System.Drawing.Point(0, 235);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Padding = new System.Windows.Forms.Padding(1);
            this.panelEx2.Size = new System.Drawing.Size(349, 40);
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
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.buttonX2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonX2.Image = global::MediboxAssistant.Properties.Resources.Apply_32x32;
            this.buttonX2.Location = new System.Drawing.Point(1, 1);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(117, 38);
            this.buttonX2.TabIndex = 20;
            this.buttonX2.Text = "Kiểm tra";
            this.buttonX2.Click += new System.EventHandler(this.btnKiemTraKetNoi_Click);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.buttonX1.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonX1.Image = global::MediboxAssistant.Properties.Resources.Properties_32x32;
            this.buttonX1.Location = new System.Drawing.Point(118, 1);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(115, 38);
            this.buttonX1.TabIndex = 19;
            this.buttonX1.Text = "Nâng cấp";
            this.buttonX1.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSave.Image = global::MediboxAssistant.Properties.Resources.Save_32x32;
            this.btnSave.Location = new System.Drawing.Point(233, 1);
            this.btnSave.Name = "btnSave";
            this.btnSave.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlS);
            this.btnSave.Size = new System.Drawing.Size(115, 38);
            this.btnSave.TabIndex = 18;
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FormConfigDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 275);
            this.Controls.Add(this.panelEx1);
            this.Name = "FormConfigDatabase";
            this.Text = "Cấu Hình Cơ Sở Dữ Liệu";
            this.Load += new System.EventHandler(this.Database_Load);
            this.panelEx1.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.TextBoxX local_txtUser;
        private DevComponents.DotNetBar.Controls.TextBoxX local_txtServer;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX txtAccount;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX local_txtPassword;
        private DevComponents.DotNetBar.LabelX txtPassword;
        private DevComponents.DotNetBar.Controls.TextBoxX local_txtDatabase;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonX btnSave;
    }
}