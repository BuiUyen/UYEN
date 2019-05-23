namespace Medibox
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem4 = new DevComponents.DotNetBar.ButtonItem();
            this.btnFileCanLamSangGroup_List = new DevComponents.DotNetBar.ButtonItem();
            this.btnFileCanLamSangGroup_Import = new DevComponents.DotNetBar.ButtonItem();
            this.btnFile_MaiKetQua_DanhSach = new DevComponents.DotNetBar.ButtonItem();
            this.CurrentTimer = new System.Windows.Forms.Timer(this.components);
            this.styleManager = new DevComponents.DotNetBar.StyleManager(this.components);
            this.NOW_Timer = new System.Windows.Forms.Timer(this.components);
            this.ribbonPanel1 = new System.Windows.Forms.RibbonPanel();
            this.tabControl_Menu = new System.Windows.Forms.TabControl();
            this.tabPageHeThong = new System.Windows.Forms.TabPage();
            this.btnQLTaiKhoan = new Medibox.Utility.UI.SanitaButton();
            this.btnDoiMK = new Medibox.Utility.UI.SanitaButton();
            this.btnDangXuat = new Medibox.Utility.UI.SanitaButton();
            this.tabPageQuanLi = new System.Windows.Forms.TabPage();
            this.btnKhachHang = new Medibox.Utility.UI.SanitaButton();
            this.btnTraPhong = new System.Windows.Forms.Button();
            this.btnDangKi = new System.Windows.Forms.Button();
            this.btnPhong = new System.Windows.Forms.Button();
            this.btnQLNhanVien = new Medibox.Utility.UI.SanitaButton();
            this.tabPageDanhMuc = new System.Windows.Forms.TabPage();
            this.btnTCKH = new Medibox.Utility.UI.SanitaButton();
            this.btnQLDichVu = new Medibox.Utility.UI.SanitaButton();
            this.btnLoaiPhong = new Medibox.Utility.UI.SanitaButton();
            this.tabPageThongKe = new System.Windows.Forms.TabPage();
            this.tabPageTroGiup = new System.Windows.Forms.TabPage();
            this.ribbonButtonList1 = new System.Windows.Forms.RibbonButtonList();
            this.tabControl_Menu.SuspendLayout();
            this.tabPageHeThong.SuspendLayout();
            this.tabPageQuanLi.SuspendLayout();
            this.tabPageDanhMuc.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonItem2
            // 
            this.buttonItem2.BeginGroup = true;
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem3,
            this.buttonItem4});
            this.buttonItem2.Text = "Nhóm XN Gõ Tắt";
            // 
            // buttonItem3
            // 
            this.buttonItem3.Name = "buttonItem3";
            this.buttonItem3.Text = "Danh Sách";
            // 
            // buttonItem4
            // 
            this.buttonItem4.BeginGroup = true;
            this.buttonItem4.Name = "buttonItem4";
            this.buttonItem4.Text = "Nhập Danh Sách Sẵn Có";
            // 
            // btnFileCanLamSangGroup_List
            // 
            this.btnFileCanLamSangGroup_List.Name = "btnFileCanLamSangGroup_List";
            this.btnFileCanLamSangGroup_List.Text = "Danh Sách";
            // 
            // btnFileCanLamSangGroup_Import
            // 
            this.btnFileCanLamSangGroup_Import.BeginGroup = true;
            this.btnFileCanLamSangGroup_Import.Name = "btnFileCanLamSangGroup_Import";
            this.btnFileCanLamSangGroup_Import.Text = "Nhập Danh Sách Sẵn Có";
            // 
            // btnFile_MaiKetQua_DanhSach
            // 
            this.btnFile_MaiKetQua_DanhSach.Name = "btnFile_MaiKetQua_DanhSach";
            this.btnFile_MaiKetQua_DanhSach.Text = "Danh Sách";
            // 
            // CurrentTimer
            // 
            this.CurrentTimer.Interval = 1000;
            this.CurrentTimer.Tick += new System.EventHandler(this.CurrentTimer_Tick);
            // 
            // styleManager
            // 
            this.styleManager.ManagerColorTint = System.Drawing.Color.Gray;
            this.styleManager.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Silver;
            this.styleManager.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.Gainsboro, System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60))))));
            // 
            // NOW_Timer
            // 
            this.NOW_Timer.Enabled = true;
            this.NOW_Timer.Interval = 10;
            this.NOW_Timer.Tick += new System.EventHandler(this.NOW_Timer_Tick);
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.Tag = null;
            this.ribbonPanel1.Text = null;
            // 
            // tabControl_Menu
            // 
            this.tabControl_Menu.Controls.Add(this.tabPageHeThong);
            this.tabControl_Menu.Controls.Add(this.tabPageQuanLi);
            this.tabControl_Menu.Controls.Add(this.tabPageDanhMuc);
            this.tabControl_Menu.Controls.Add(this.tabPageThongKe);
            this.tabControl_Menu.Controls.Add(this.tabPageTroGiup);
            this.tabControl_Menu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_Menu.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl_Menu.ItemSize = new System.Drawing.Size(100, 50);
            this.tabControl_Menu.Location = new System.Drawing.Point(0, 0);
            this.tabControl_Menu.Name = "tabControl_Menu";
            this.tabControl_Menu.SelectedIndex = 0;
            this.tabControl_Menu.Size = new System.Drawing.Size(653, 284);
            this.tabControl_Menu.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl_Menu.TabIndex = 0;
            // 
            // tabPageHeThong
            // 
            this.tabPageHeThong.Controls.Add(this.btnQLTaiKhoan);
            this.tabPageHeThong.Controls.Add(this.btnDoiMK);
            this.tabPageHeThong.Controls.Add(this.btnDangXuat);
            this.tabPageHeThong.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageHeThong.Location = new System.Drawing.Point(4, 54);
            this.tabPageHeThong.Name = "tabPageHeThong";
            this.tabPageHeThong.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHeThong.Size = new System.Drawing.Size(645, 226);
            this.tabPageHeThong.TabIndex = 0;
            this.tabPageHeThong.Text = "Hệ Thống";
            this.tabPageHeThong.UseVisualStyleBackColor = true;
            // 
            // btnQLTaiKhoan
            // 
            this.btnQLTaiKhoan.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnQLTaiKhoan.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnQLTaiKhoan.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQLTaiKhoan.Location = new System.Drawing.Point(219, 6);
            this.btnQLTaiKhoan.Name = "btnQLTaiKhoan";
            this.btnQLTaiKhoan.Size = new System.Drawing.Size(207, 47);
            this.btnQLTaiKhoan.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnQLTaiKhoan.Symbol = "59376";
            this.btnQLTaiKhoan.SymbolColor = System.Drawing.Color.Red;
            this.btnQLTaiKhoan.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material;
            this.btnQLTaiKhoan.TabIndex = 2;
            this.btnQLTaiKhoan.Text = "Quản Lí Tài Khoản";
            this.btnQLTaiKhoan.Click += new System.EventHandler(this.btnQLTaiKhoan_Click);
            // 
            // btnDoiMK
            // 
            this.btnDoiMK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDoiMK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDoiMK.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDoiMK.Location = new System.Drawing.Point(6, 6);
            this.btnDoiMK.Name = "btnDoiMK";
            this.btnDoiMK.Size = new System.Drawing.Size(207, 47);
            this.btnDoiMK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDoiMK.Symbol = "";
            this.btnDoiMK.SymbolColor = System.Drawing.Color.Red;
            this.btnDoiMK.TabIndex = 1;
            this.btnDoiMK.Text = "Đổi Mật Khẩu";
            this.btnDoiMK.Click += new System.EventHandler(this.btnDoiMK_Click);
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDangXuat.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDangXuat.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDangXuat.Location = new System.Drawing.Point(432, 6);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Size = new System.Drawing.Size(207, 47);
            this.btnDangXuat.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDangXuat.Symbol = "";
            this.btnDangXuat.SymbolColor = System.Drawing.Color.Red;
            this.btnDangXuat.TabIndex = 0;
            this.btnDangXuat.Text = "Đăng Xuất";
            this.btnDangXuat.Click += new System.EventHandler(this.btnDangXuat_Click);
            // 
            // tabPageQuanLi
            // 
            this.tabPageQuanLi.Controls.Add(this.btnKhachHang);
            this.tabPageQuanLi.Controls.Add(this.btnTraPhong);
            this.tabPageQuanLi.Controls.Add(this.btnDangKi);
            this.tabPageQuanLi.Controls.Add(this.btnPhong);
            this.tabPageQuanLi.Controls.Add(this.btnQLNhanVien);
            this.tabPageQuanLi.Location = new System.Drawing.Point(4, 54);
            this.tabPageQuanLi.Name = "tabPageQuanLi";
            this.tabPageQuanLi.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageQuanLi.Size = new System.Drawing.Size(645, 226);
            this.tabPageQuanLi.TabIndex = 1;
            this.tabPageQuanLi.Text = "Quản Lí";
            this.tabPageQuanLi.UseVisualStyleBackColor = true;
            // 
            // btnKhachHang
            // 
            this.btnKhachHang.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnKhachHang.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground;
            this.btnKhachHang.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKhachHang.Location = new System.Drawing.Point(330, 6);
            this.btnKhachHang.Name = "btnKhachHang";
            this.btnKhachHang.Size = new System.Drawing.Size(283, 48);
            this.btnKhachHang.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnKhachHang.Symbol = "";
            this.btnKhachHang.SymbolColor = System.Drawing.Color.Red;
            this.btnKhachHang.TabIndex = 7;
            this.btnKhachHang.Text = "Khách Hàng Hiện Tại";
            this.btnKhachHang.Click += new System.EventHandler(this.btnKhachHang_Click);
            // 
            // btnTraPhong
            // 
            this.btnTraPhong.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnTraPhong.BackgroundImage = global::MediboxAssistant.Properties.Resources.Restore;
            this.btnTraPhong.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnTraPhong.Location = new System.Drawing.Point(444, 76);
            this.btnTraPhong.Name = "btnTraPhong";
            this.btnTraPhong.Size = new System.Drawing.Size(169, 95);
            this.btnTraPhong.TabIndex = 5;
            this.btnTraPhong.Text = "Trả Phòng";
            this.btnTraPhong.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnTraPhong.UseVisualStyleBackColor = false;
            this.btnTraPhong.Click += new System.EventHandler(this.btnTraPhong_Click);
            // 
            // btnDangKi
            // 
            this.btnDangKi.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnDangKi.BackgroundImage = global::MediboxAssistant.Properties.Resources.paper_pencil_48;
            this.btnDangKi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDangKi.Location = new System.Drawing.Point(242, 76);
            this.btnDangKi.Name = "btnDangKi";
            this.btnDangKi.Size = new System.Drawing.Size(169, 95);
            this.btnDangKi.TabIndex = 4;
            this.btnDangKi.Text = "Đăng Kí";
            this.btnDangKi.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDangKi.UseVisualStyleBackColor = false;
            this.btnDangKi.Click += new System.EventHandler(this.btnDangKi_Click);
            // 
            // btnPhong
            // 
            this.btnPhong.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnPhong.BackgroundImage = global::MediboxAssistant.Properties.Resources.home_481;
            this.btnPhong.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPhong.Location = new System.Drawing.Point(34, 76);
            this.btnPhong.Name = "btnPhong";
            this.btnPhong.Size = new System.Drawing.Size(169, 95);
            this.btnPhong.TabIndex = 3;
            this.btnPhong.Text = "Phòng";
            this.btnPhong.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPhong.UseVisualStyleBackColor = false;
            this.btnPhong.Click += new System.EventHandler(this.btnPhong_Click);
            // 
            // btnQLNhanVien
            // 
            this.btnQLNhanVien.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnQLNhanVien.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground;
            this.btnQLNhanVien.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQLNhanVien.Location = new System.Drawing.Point(20, 6);
            this.btnQLNhanVien.Name = "btnQLNhanVien";
            this.btnQLNhanVien.Size = new System.Drawing.Size(283, 48);
            this.btnQLNhanVien.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnQLNhanVien.Symbol = "59375";
            this.btnQLNhanVien.SymbolColor = System.Drawing.Color.Red;
            this.btnQLNhanVien.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material;
            this.btnQLNhanVien.TabIndex = 2;
            this.btnQLNhanVien.Text = "Quản Lí Nhân Viên";
            this.btnQLNhanVien.Click += new System.EventHandler(this.btnQLNhanVien_Click);
            // 
            // tabPageDanhMuc
            // 
            this.tabPageDanhMuc.Controls.Add(this.btnTCKH);
            this.tabPageDanhMuc.Controls.Add(this.btnQLDichVu);
            this.tabPageDanhMuc.Controls.Add(this.btnLoaiPhong);
            this.tabPageDanhMuc.Location = new System.Drawing.Point(4, 54);
            this.tabPageDanhMuc.Name = "tabPageDanhMuc";
            this.tabPageDanhMuc.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDanhMuc.Size = new System.Drawing.Size(645, 226);
            this.tabPageDanhMuc.TabIndex = 2;
            this.tabPageDanhMuc.Text = "Danh Mục";
            this.tabPageDanhMuc.UseVisualStyleBackColor = true;
            // 
            // btnTCKH
            // 
            this.btnTCKH.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTCKH.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground;
            this.btnTCKH.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTCKH.Location = new System.Drawing.Point(402, 154);
            this.btnTCKH.Name = "btnTCKH";
            this.btnTCKH.Size = new System.Drawing.Size(235, 64);
            this.btnTCKH.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnTCKH.Symbol = "";
            this.btnTCKH.SymbolColor = System.Drawing.Color.Red;
            this.btnTCKH.TabIndex = 11;
            this.btnTCKH.Text = "Tất Cả Khách Hàng";
            this.btnTCKH.Click += new System.EventHandler(this.btnTCKH_Click);
            // 
            // btnQLDichVu
            // 
            this.btnQLDichVu.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnQLDichVu.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground;
            this.btnQLDichVu.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQLDichVu.Location = new System.Drawing.Point(408, 6);
            this.btnQLDichVu.Name = "btnQLDichVu";
            this.btnQLDichVu.Size = new System.Drawing.Size(229, 64);
            this.btnQLDichVu.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnQLDichVu.Symbol = "";
            this.btnQLDichVu.SymbolColor = System.Drawing.Color.Red;
            this.btnQLDichVu.TabIndex = 10;
            this.btnQLDichVu.Text = "Quản Lí Dịch Vụ";
            this.btnQLDichVu.Click += new System.EventHandler(this.btnQLDichVu_Click);
            // 
            // btnLoaiPhong
            // 
            this.btnLoaiPhong.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLoaiPhong.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground;
            this.btnLoaiPhong.Location = new System.Drawing.Point(6, 6);
            this.btnLoaiPhong.Name = "btnLoaiPhong";
            this.btnLoaiPhong.Size = new System.Drawing.Size(235, 64);
            this.btnLoaiPhong.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnLoaiPhong.Symbol = "";
            this.btnLoaiPhong.SymbolColor = System.Drawing.Color.Red;
            this.btnLoaiPhong.TabIndex = 9;
            this.btnLoaiPhong.Text = "Loại Phòng";
            this.btnLoaiPhong.Click += new System.EventHandler(this.btnLoaiPhong_Click);
            // 
            // tabPageThongKe
            // 
            this.tabPageThongKe.Location = new System.Drawing.Point(4, 54);
            this.tabPageThongKe.Name = "tabPageThongKe";
            this.tabPageThongKe.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageThongKe.Size = new System.Drawing.Size(645, 226);
            this.tabPageThongKe.TabIndex = 3;
            this.tabPageThongKe.Text = "Thống Kê";
            this.tabPageThongKe.UseVisualStyleBackColor = true;
            // 
            // tabPageTroGiup
            // 
            this.tabPageTroGiup.Location = new System.Drawing.Point(4, 54);
            this.tabPageTroGiup.Name = "tabPageTroGiup";
            this.tabPageTroGiup.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTroGiup.Size = new System.Drawing.Size(645, 226);
            this.tabPageTroGiup.TabIndex = 4;
            this.tabPageTroGiup.Text = "Trợ Giúp";
            this.tabPageTroGiup.UseVisualStyleBackColor = true;
            // 
            // ribbonButtonList1
            // 
            this.ribbonButtonList1.AltKey = null;
            this.ribbonButtonList1.ButtonsSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large;
            this.ribbonButtonList1.CheckedGroup = null;
            this.ribbonButtonList1.FlowToBottom = false;
            this.ribbonButtonList1.Image = null;
            this.ribbonButtonList1.ItemsSizeInDropwDownMode = new System.Drawing.Size(7, 5);
            this.ribbonButtonList1.Tag = null;
            this.ribbonButtonList1.Text = null;
            this.ribbonButtonList1.ToolTip = null;
            this.ribbonButtonList1.ToolTipTitle = null;
            this.ribbonButtonList1.Value = null;
            // 
            // FormMain
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(653, 284);
            this.Controls.Add(this.tabControl_Menu);
            this.MinimizeBox = true;
            this.Name = "FormMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Quản Lí Khách Sạn";
            this.Load += new System.EventHandler(this.FormMedibox_Load);
            this.tabControl_Menu.ResumeLayout(false);
            this.tabPageHeThong.ResumeLayout(false);
            this.tabPageQuanLi.ResumeLayout(false);
            this.tabPageDanhMuc.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem buttonItem2;
        private DevComponents.DotNetBar.ButtonItem buttonItem3;
        private DevComponents.DotNetBar.ButtonItem buttonItem4;
        private DevComponents.DotNetBar.ButtonItem btnFileCanLamSangGroup_List;
        private DevComponents.DotNetBar.ButtonItem btnFileCanLamSangGroup_Import;
        private DevComponents.DotNetBar.ButtonItem btnFile_MaiKetQua_DanhSach;
        private System.Windows.Forms.Timer CurrentTimer;
        private DevComponents.DotNetBar.StyleManager styleManager;
        private System.Windows.Forms.Timer NOW_Timer;
        private System.Windows.Forms.RibbonPanel ribbonPanel1;
        private System.Windows.Forms.TabControl tabControl_Menu;
        private System.Windows.Forms.TabPage tabPageHeThong;
        private System.Windows.Forms.TabPage tabPageQuanLi;
        private System.Windows.Forms.TabPage tabPageDanhMuc;
        private System.Windows.Forms.TabPage tabPageThongKe;
        private System.Windows.Forms.TabPage tabPageTroGiup;
        private System.Windows.Forms.RibbonButtonList ribbonButtonList1;
        private Utility.UI.SanitaButton btnDoiMK;
        private Utility.UI.SanitaButton btnDangXuat;
        private Utility.UI.SanitaButton btnQLTaiKhoan;
        private Utility.UI.SanitaButton btnQLNhanVien;
        private System.Windows.Forms.Button btnPhong;
        private System.Windows.Forms.Button btnDangKi;
        private System.Windows.Forms.Button btnTraPhong;
        private Utility.UI.SanitaButton btnKhachHang;
        private Utility.UI.SanitaButton btnLoaiPhong;
        private Utility.UI.SanitaButton btnQLDichVu;
        private Utility.UI.SanitaButton btnTCKH;
    }
}