
namespace ql_banhang.pages
{
    partial class CreatePhieuDatHang
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
            this.grChiTietPhieuDatHang = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewLinkColumn12 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbNCC = new System.Windows.Forms.ComboBox();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.lbPDH_NgayDatHang = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbDonGiaDat = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.tbTenSP = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.btnThem = new System.Windows.Forms.Button();
            this.tbSoLuongDat = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.tbMaSP = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnTaoMoi = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grChiTietPhieuDatHang)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grChiTietPhieuDatHang
            // 
            this.grChiTietPhieuDatHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grChiTietPhieuDatHang.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn19,
            this.dataGridViewTextBoxColumn20,
            this.dataGridViewTextBoxColumn21,
            this.Column1,
            this.dataGridViewLinkColumn12});
            this.grChiTietPhieuDatHang.Location = new System.Drawing.Point(12, 81);
            this.grChiTietPhieuDatHang.Name = "grChiTietPhieuDatHang";
            this.grChiTietPhieuDatHang.Size = new System.Drawing.Size(753, 466);
            this.grChiTietPhieuDatHang.TabIndex = 2;
            this.grChiTietPhieuDatHang.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grChiTietPhieuDatHang_CellContentClick);
            this.grChiTietPhieuDatHang.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grChiTietPhieuDatHang_CellEndEdit);
            // 
            // dataGridViewTextBoxColumn19
            // 
            this.dataGridViewTextBoxColumn19.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn19.HeaderText = "Mã sản phẩm";
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            this.dataGridViewTextBoxColumn19.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn20
            // 
            this.dataGridViewTextBoxColumn20.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn20.HeaderText = "Tên sản phẩm";
            this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            this.dataGridViewTextBoxColumn20.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn21
            // 
            this.dataGridViewTextBoxColumn21.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn21.HeaderText = "Đơn giá đặt";
            this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "Số lượng đặt";
            this.Column1.Name = "Column1";
            // 
            // dataGridViewLinkColumn12
            // 
            this.dataGridViewLinkColumn12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewLinkColumn12.HeaderText = "";
            this.dataGridViewLinkColumn12.Name = "dataGridViewLinkColumn12";
            this.dataGridViewLinkColumn12.ReadOnly = true;
            this.dataGridViewLinkColumn12.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbNCC);
            this.groupBox3.Controls.Add(this.label42);
            this.groupBox3.Controls.Add(this.label43);
            this.groupBox3.Controls.Add(this.lbPDH_NgayDatHang);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(772, 81);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(366, 110);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Thông tin phiếu đặt hàng";
            // 
            // cbNCC
            // 
            this.cbNCC.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbNCC.FormattingEnabled = true;
            this.cbNCC.Location = new System.Drawing.Point(108, 63);
            this.cbNCC.Name = "cbNCC";
            this.cbNCC.Size = new System.Drawing.Size(252, 25);
            this.cbNCC.TabIndex = 7;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(13, 37);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(102, 17);
            this.label42.TabIndex = 2;
            this.label42.Text = "Ngày đặt hàng: ";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(13, 67);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(92, 17);
            this.label43.TabIndex = 5;
            this.label43.Text = "Nhà cung cấp:";
            // 
            // lbPDH_NgayDatHang
            // 
            this.lbPDH_NgayDatHang.AutoSize = true;
            this.lbPDH_NgayDatHang.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPDH_NgayDatHang.Location = new System.Drawing.Point(104, 37);
            this.lbPDH_NgayDatHang.Name = "lbPDH_NgayDatHang";
            this.lbPDH_NgayDatHang.Size = new System.Drawing.Size(74, 17);
            this.lbPDH_NgayDatHang.TabIndex = 3;
            this.lbPDH_NgayDatHang.Text = "12/15/2020";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbDonGiaDat);
            this.groupBox2.Controls.Add(this.label35);
            this.groupBox2.Controls.Add(this.tbTenSP);
            this.groupBox2.Controls.Add(this.label36);
            this.groupBox2.Controls.Add(this.btnThem);
            this.groupBox2.Controls.Add(this.tbSoLuongDat);
            this.groupBox2.Controls.Add(this.label37);
            this.groupBox2.Controls.Add(this.tbMaSP);
            this.groupBox2.Controls.Add(this.label38);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(772, 197);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(366, 240);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sản phẩm đặt";
            // 
            // tbDonGiaDat
            // 
            this.tbDonGiaDat.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDonGiaDat.Location = new System.Drawing.Point(159, 96);
            this.tbDonGiaDat.Name = "tbDonGiaDat";
            this.tbDonGiaDat.Size = new System.Drawing.Size(181, 25);
            this.tbDonGiaDat.TabIndex = 16;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(156, 76);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(80, 17);
            this.label35.TabIndex = 15;
            this.label35.Text = "Đơn giá đặt:";
            // 
            // tbTenSP
            // 
            this.tbTenSP.Enabled = false;
            this.tbTenSP.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTenSP.Location = new System.Drawing.Point(159, 45);
            this.tbTenSP.Name = "tbTenSP";
            this.tbTenSP.Size = new System.Drawing.Size(181, 25);
            this.tbTenSP.TabIndex = 14;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(156, 25);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(49, 17);
            this.label36.TabIndex = 13;
            this.label36.Text = "Tên SP:";
            // 
            // btnThem
            // 
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.Location = new System.Drawing.Point(15, 143);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(104, 33);
            this.btnThem.TabIndex = 12;
            this.btnThem.Text = "Thêm ";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // tbSoLuongDat
            // 
            this.tbSoLuongDat.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSoLuongDat.Location = new System.Drawing.Point(15, 96);
            this.tbSoLuongDat.Name = "tbSoLuongDat";
            this.tbSoLuongDat.Size = new System.Drawing.Size(104, 25);
            this.tbSoLuongDat.TabIndex = 11;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(13, 76);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(87, 17);
            this.label37.TabIndex = 10;
            this.label37.Text = "Số lượng đặt:";
            // 
            // tbMaSP
            // 
            this.tbMaSP.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMaSP.Location = new System.Drawing.Point(15, 45);
            this.tbMaSP.Name = "tbMaSP";
            this.tbMaSP.Size = new System.Drawing.Size(104, 25);
            this.tbMaSP.TabIndex = 9;
            this.tbMaSP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbMaSP_KeyDown);
            this.tbMaSP.Leave += new System.EventHandler(this.tbMaSP_Leave);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(13, 25);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(48, 17);
            this.label38.TabIndex = 9;
            this.label38.Text = "Mã SP:";
            // 
            // btnLuu
            // 
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.Location = new System.Drawing.Point(956, 509);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(181, 38);
            this.btnLuu.TabIndex = 29;
            this.btnLuu.Text = "Lưu đơn đặt hàng";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnTaoMoi
            // 
            this.btnTaoMoi.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTaoMoi.Location = new System.Drawing.Point(771, 509);
            this.btnTaoMoi.Name = "btnTaoMoi";
            this.btnTaoMoi.Size = new System.Drawing.Size(179, 38);
            this.btnTaoMoi.TabIndex = 28;
            this.btnTaoMoi.Text = "Tạo mới đơn đặt hàng";
            this.btnTaoMoi.UseVisualStyleBackColor = true;
            this.btnTaoMoi.Click += new System.EventHandler(this.btnTaoMoi_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(471, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(228, 30);
            this.label1.TabIndex = 8;
            this.label1.Text = "Tạo mới đơn đặt hàng";
            // 
            // CreatePhieuDatHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1147, 565);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnTaoMoi);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.grChiTietPhieuDatHang);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "CreatePhieuDatHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm mới phiếu đặt hàng";
            this.Load += new System.EventHandler(this.CreatePhieuDatHang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grChiTietPhieuDatHang)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grChiTietPhieuDatHang;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cbNCC;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label lbPDH_NgayDatHang;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbDonGiaDat;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.TextBox tbTenSP;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.TextBox tbSoLuongDat;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.TextBox tbMaSP;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnTaoMoi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewLinkColumn dataGridViewLinkColumn12;
    }
}