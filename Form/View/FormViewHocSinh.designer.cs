namespace Medibox
{
    partial class FormViewHocSinh
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
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.mListViewData = new Sanita.Utility.UI.ObjectListView();
            this.ID_HocSinh = ((Sanita.Utility.UI.OLVColumn)(new Sanita.Utility.UI.OLVColumn()));
            this.HovaTen_HocSinh = ((Sanita.Utility.UI.OLVColumn)(new Sanita.Utility.UI.OLVColumn()));
            this.GioiTinh_HocSinh = ((Sanita.Utility.UI.OLVColumn)(new Sanita.Utility.UI.OLVColumn()));
            this.txtSearch = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.btnAddHocSinh = new DevComponents.DotNetBar.ButtonX();
            this.btnRefresh = new DevComponents.DotNetBar.ButtonX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.lblBenhAn = new DevComponents.DotNetBar.LabelX();
            this.DichVuMenuBar = new DevComponents.DotNetBar.ContextMenuBar();
            this.DichVuMenu = new DevComponents.DotNetBar.ButtonItem();
            this.labelItem2 = new DevComponents.DotNetBar.LabelItem();
            this.btnXoa = new DevComponents.DotNetBar.ButtonItem();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.ID = ((Sanita.Utility.UI.OLVColumn)(new Sanita.Utility.UI.OLVColumn()));
            this.HovaTen = ((Sanita.Utility.UI.OLVColumn)(new Sanita.Utility.UI.OLVColumn()));
            this.GioiTinh = ((Sanita.Utility.UI.OLVColumn)(new Sanita.Utility.UI.OLVColumn()));
            this.Tuoi = ((Sanita.Utility.UI.OLVColumn)(new Sanita.Utility.UI.OLVColumn()));
            this.DataProgress = new DevComponents.DotNetBar.Controls.CircularProgress();
            this.controlContainerItem1 = new DevComponents.DotNetBar.ControlContainerItem();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mListViewData)).BeginInit();
            this.panelEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DichVuMenuBar)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelEx1.Controls.Add(this.mListViewData);
            this.panelEx1.Controls.Add(this.txtSearch);
            this.panelEx1.Controls.Add(this.panelEx3);
            this.panelEx1.Controls.Add(this.panelEx2);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1290, 690);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.panelEx1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 32;
            // 
            // mListViewData
            // 
            this.mListViewData.AllColumns.Add(this.ID_HocSinh);
            this.mListViewData.AllColumns.Add(this.HovaTen_HocSinh);
            this.mListViewData.AllColumns.Add(this.GioiTinh_HocSinh);
            this.mListViewData.AlternateRowBackColor = System.Drawing.Color.WhiteSmoke;
            this.mListViewData.AutoArrange = false;
            this.mListViewData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.mListViewData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mListViewData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID_HocSinh,
            this.HovaTen_HocSinh,
            this.GioiTinh_HocSinh});
            this.DichVuMenuBar.SetContextMenuEx(this.mListViewData, this.DichVuMenu);
            this.mListViewData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mListViewData.FullRowSelect = true;
            this.mListViewData.GridLines = true;
            this.mListViewData.HideSelection = false;
            this.mListViewData.Location = new System.Drawing.Point(0, 87);
            this.mListViewData.MenuLabelSortAscending = "Sắp xếp tăng dần theo \'{0}\'";
            this.mListViewData.MultiSelect = false;
            this.mListViewData.Name = "mListViewData";
            this.mListViewData.ShowGroups = false;
            this.mListViewData.Size = new System.Drawing.Size(1290, 563);
            this.mListViewData.SortGroupItemsByPrimaryColumn = false;
            this.mListViewData.TabIndex = 222;
            this.mListViewData.UnfocusedHighlightBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.mListViewData.UnfocusedHighlightForegroundColor = System.Drawing.Color.White;
            this.mListViewData.UseAlternatingBackColors = true;
            this.mListViewData.UseCompatibleStateImageBehavior = false;
            this.mListViewData.UseCustomSelectionColors = true;
            this.mListViewData.UseFiltering = true;
            this.mListViewData.View = System.Windows.Forms.View.Details;
            this.mListViewData.CellClick += new System.EventHandler<Sanita.Utility.UI.CellClickEventArgs>(this.mListViewData_CellClick);
            // 
            // ID_HocSinh
            // 
            this.ID_HocSinh.AspectName = "HocSinhID";
            this.ID_HocSinh.CellPadding = null;
            this.ID_HocSinh.Text = "ID";
            this.ID_HocSinh.Width = 64;
            // 
            // HovaTen_HocSinh
            // 
            this.HovaTen_HocSinh.AspectName = "HovaTen";
            this.HovaTen_HocSinh.CellPadding = null;
            this.HovaTen_HocSinh.Text = "Ho va ten";
            this.HovaTen_HocSinh.Width = 363;
            // 
            // GioiTinh_HocSinh
            // 
            this.GioiTinh_HocSinh.AspectName = "GioiTinh";
            this.GioiTinh_HocSinh.CellPadding = null;
            this.GioiTinh_HocSinh.Text = "Gioi Tinh";
            this.GioiTinh_HocSinh.Width = 112;
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            // 
            // 
            // 
            this.txtSearch.Border.BorderLeftWidth = -1;
            this.txtSearch.Border.BorderRightWidth = -1;
            this.txtSearch.Border.Class = "TextBoxBorder";
            this.txtSearch.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSearch.ButtonCustom.Visible = true;
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSearch.FocusHighlightEnabled = true;
            this.txtSearch.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(0, 60);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PreventEnterBeep = true;
            this.txtSearch.Size = new System.Drawing.Size(1290, 27);
            this.txtSearch.TabIndex = 221;
            this.txtSearch.WatermarkColor = System.Drawing.Color.DimGray;
            this.txtSearch.WatermarkText = "Tìm kiếm (Ctrl+F)";
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.btnAddHocSinh);
            this.panelEx3.Controls.Add(this.btnRefresh);
            this.panelEx3.Controls.Add(this.buttonX1);
            this.panelEx3.Controls.Add(this.lblBenhAn);
            this.panelEx3.Controls.Add(this.DichVuMenuBar);
            this.panelEx3.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx3.Location = new System.Drawing.Point(0, 0);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Padding = new System.Windows.Forms.Padding(1);
            this.panelEx3.Size = new System.Drawing.Size(1290, 60);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor2.Color = System.Drawing.SystemColors.Control;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.Top;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 220;
            // 
            // btnAddHocSinh
            // 
            this.btnAddHocSinh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddHocSinh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.btnAddHocSinh.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnAddHocSinh.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnAddHocSinh.Image = global::MediboxAssistant.Properties.Resources.Add_32x32;
            this.btnAddHocSinh.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnAddHocSinh.Location = new System.Drawing.Point(147, 1);
            this.btnAddHocSinh.Name = "btnAddHocSinh";
            this.btnAddHocSinh.Size = new System.Drawing.Size(70, 58);
            this.btnAddHocSinh.TabIndex = 147;
            this.btnAddHocSinh.Text = "Thêm";
            this.btnAddHocSinh.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.btnRefresh.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRefresh.Image = global::MediboxAssistant.Properties.Resources.Refresh_32__1_;
            this.btnRefresh.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnRefresh.Location = new System.Drawing.Point(74, 1);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(73, 58);
            this.btnRefresh.TabIndex = 148;
            this.btnRefresh.Text = "Làm Mới";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.AutoExpandOnClick = true;
            this.buttonX1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.buttonX1.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonX1.Image = global::MediboxAssistant.Properties.Resources.BAD_SAMPLE;
            this.buttonX1.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonX1.Location = new System.Drawing.Point(1, 1);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(73, 58);
            this.buttonX1.TabIndex = 146;
            this.buttonX1.Text = "Đóng";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // lblBenhAn
            // 
            // 
            // 
            // 
            this.lblBenhAn.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblBenhAn.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblBenhAn.Font = new System.Drawing.Font("Tahoma", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblBenhAn.ForeColor = System.Drawing.Color.Maroon;
            this.lblBenhAn.Location = new System.Drawing.Point(960, 1);
            this.lblBenhAn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblBenhAn.Name = "lblBenhAn";
            this.lblBenhAn.Size = new System.Drawing.Size(329, 58);
            this.lblBenhAn.TabIndex = 9;
            this.lblBenhAn.Text = "DANH SÁCH HỌC SINH";
            this.lblBenhAn.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // DichVuMenuBar
            // 
            this.DichVuMenuBar.AntiAlias = true;
            this.DichVuMenuBar.DockSide = DevComponents.DotNetBar.eDockSide.Top;
            this.DichVuMenuBar.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DichVuMenuBar.IsMaximized = false;
            this.DichVuMenuBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.DichVuMenu});
            this.DichVuMenuBar.Location = new System.Drawing.Point(346, 12);
            this.DichVuMenuBar.Name = "DichVuMenuBar";
            this.DichVuMenuBar.Size = new System.Drawing.Size(82, 25);
            this.DichVuMenuBar.Stretch = true;
            this.DichVuMenuBar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.DichVuMenuBar.TabIndex = 224;
            this.DichVuMenuBar.TabStop = false;
            // 
            // DichVuMenu
            // 
            this.DichVuMenu.AutoExpandOnClick = true;
            this.DichVuMenu.Name = "DichVuMenu";
            this.DichVuMenu.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem2,
            this.btnXoa});
            this.DichVuMenu.Text = "Thao Tác";
            // 
            // labelItem2
            // 
            this.labelItem2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labelItem2.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom;
            this.labelItem2.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.labelItem2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(21)))), ((int)(((byte)(110)))));
            this.labelItem2.Name = "labelItem2";
            this.labelItem2.PaddingBottom = 1;
            this.labelItem2.PaddingLeft = 10;
            this.labelItem2.PaddingTop = 1;
            this.labelItem2.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.labelItem2.Text = "Học Sinh";
            // 
            // btnXoa
            // 
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.btnSave);
            this.panelEx2.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx2.Location = new System.Drawing.Point(0, 650);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Padding = new System.Windows.Forms.Padding(1);
            this.panelEx2.Size = new System.Drawing.Size(1290, 40);
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
            this.btnSave.Image = global::MediboxAssistant.Properties.Resources.Save_32x32;
            this.btnSave.Location = new System.Drawing.Point(1174, 1);
            this.btnSave.Name = "btnSave";
            this.btnSave.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlS);
            this.btnSave.Size = new System.Drawing.Size(115, 38);
            this.btnSave.TabIndex = 18;
            this.btnSave.Text = "Lưu";
            // 
            // ID
            // 
            this.ID.AspectName = "MediboxTextID";
            this.ID.CellPadding = null;
            this.ID.DisplayIndex = 0;
            this.ID.Text = "ID";
            this.ID.Width = 41;
            // 
            // HovaTen
            // 
            this.HovaTen.AspectName = "DeviceName";
            this.HovaTen.CellPadding = null;
            this.HovaTen.DisplayIndex = 1;
            this.HovaTen.Text = "Họ và Tên";
            this.HovaTen.Width = 100;
            // 
            // GioiTinh
            // 
            this.GioiTinh.AspectName = "English";
            this.GioiTinh.CellPadding = null;
            this.GioiTinh.DisplayIndex = 2;
            this.GioiTinh.Text = "Giới Tính";
            this.GioiTinh.Width = 352;
            // 
            // Tuoi
            // 
            this.Tuoi.AspectName = "Japanese";
            this.Tuoi.CellPadding = null;
            this.Tuoi.DisplayIndex = 3;
            this.Tuoi.Text = "Tuổi";
            this.Tuoi.Width = 795;
            // 
            // DataProgress
            // 
            this.DataProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            // 
            // 
            // 
            this.DataProgress.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.DataProgress.Location = new System.Drawing.Point(3, 54);
            this.DataProgress.Name = "DataProgress";
            this.DataProgress.ProgressBarType = DevComponents.DotNetBar.eCircularProgressType.Dot;
            this.DataProgress.ProgressColor = System.Drawing.Color.Maroon;
            this.DataProgress.ProgressTextColor = System.Drawing.Color.Maroon;
            this.DataProgress.ProgressTextFormat = "";
            this.DataProgress.ProgressTextVisible = true;
            this.DataProgress.Size = new System.Drawing.Size(50, 50);
            this.DataProgress.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.DataProgress.TabIndex = 223;
            this.DataProgress.Value = 100;
            // 
            // controlContainerItem1
            // 
            this.controlContainerItem1.AllowItemResize = true;
            this.controlContainerItem1.Control = this.DataProgress;
            this.controlContainerItem1.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem1.Name = "controlContainerItem1";
            // 
            // FormViewHocSinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1290, 690);
            this.Controls.Add(this.panelEx1);
            this.Name = "FormViewHocSinh";
            this.Text = "Danh Sách Học Sinh";
            this.Load += new System.EventHandler(this.Database_Load);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mListViewData)).EndInit();
            this.panelEx3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DichVuMenuBar)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSearch;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.ButtonX btnAddHocSinh;
        private DevComponents.DotNetBar.ButtonX btnRefresh;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.LabelX lblBenhAn;
        private DevComponents.DotNetBar.ContextMenuBar DichVuMenuBar;
        private DevComponents.DotNetBar.ButtonItem DichVuMenu;
        private DevComponents.DotNetBar.ButtonItem btnXoa;
        private DevComponents.DotNetBar.Controls.CircularProgress DataProgress;
        private Sanita.Utility.UI.ObjectListView mListViewData;
        private Sanita.Utility.UI.OLVColumn ID;
        private Sanita.Utility.UI.OLVColumn HovaTen;
        private Sanita.Utility.UI.OLVColumn GioiTinh;
        private Sanita.Utility.UI.OLVColumn Tuoi;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem1;
        private Sanita.Utility.UI.OLVColumn ID_HocSinh;
        private Sanita.Utility.UI.OLVColumn HovaTen_HocSinh;
        private Sanita.Utility.UI.OLVColumn GioiTinh_HocSinh;
        private DevComponents.DotNetBar.LabelItem labelItem2;
    }
}