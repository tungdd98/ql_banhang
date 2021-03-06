﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ql_banhang.pages
{
    public partial class AdminManager : Form
    {
        QLBanHangDataContext db = new QLBanHangDataContext();
        int tabIndex = 0;
        public AdminManager()
        {
            InitializeComponent();
        }
        private void AdminManager_Load(object sender, EventArgs e)
        {
            ShowLoaiSP();
        }

        private void AdminManager_Activated(object sender, EventArgs e)
        {
            selectedTab(tabIndex);
        }

        public void selectedTab(int tabIndex)
        {
            switch (tabIndex)
            {
                case 0:
                    ShowLoaiSP();
                    break;
                case 1:
                    ShowSanPham();
                    ShowComboboxLoaiSP();
                    break;
                case 2:
                    ShowNhaCungCap();
                    break;
                case 3:
                    ShowCustomer();
                    break;
                case 4:
                    HienThiNhanVien();
                    break;
                case 6:
                    ShowOrder();
                    break;
                case 7:
                    HienThiPhieuDatHang();
                    break;
                case 8:
                    HienThiPhieuNhapHang();
                    break;
                default:
                    ShowLoaiSP();
                    break;
            }
        }

        private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            selectedTab(e.TabPageIndex);
        }

        /*
            Manager LoaiSanPham
            @author: Dang Duc Tung
         */
        private void ShowLoaiSP()
        {
            dataGridViewLoaiSP.Rows.Clear();
            var list = from item in db.LoaiSanPhams
                       orderby item.MaLSP descending
                       select new { item.MaLSP, item.TenLSP };

            foreach (var item in list)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridViewLoaiSP.Rows[0].Clone();

                row.Cells[0].Value = item.MaLSP;
                row.Cells[1].Value = item.TenLSP;
                row.Cells[2].Value = "Cập nhật";
                row.Cells[3].Value = "Xoá";

                dataGridViewLoaiSP.Rows.Add(row);
            }
        }

        private void buttonClearCategory_Click(object sender, EventArgs e)
        {
            textBoxTenLSP.Clear();
        }

        private void buttonAddLoaiSP_Click(object sender, EventArgs e)
        {
            string tenLSP = textBoxTenLSP.Text;
            if (tenLSP == "")
            {
                MessageBox.Show("Tên loại sản phẩm không được để trống");
                return;
            }
            LoaiSanPham newItem = new LoaiSanPham();
            newItem.TenLSP = tenLSP;

            db.LoaiSanPhams.InsertOnSubmit(newItem);
            db.SubmitChanges();
            ShowLoaiSP();
        }

        private void dataGridViewLoaiSP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = int.Parse(dataGridViewLoaiSP.Rows[e.RowIndex].Cells[0].Value.ToString());
            LoaiSanPham itemChange = db.LoaiSanPhams.SingleOrDefault(item => item.MaLSP == id);

            if (e.ColumnIndex == 3)
            {
                var confirmMsg = MessageBox.Show("Bạn có muốn xoá?", "Thông báo", MessageBoxButtons.YesNo);

                if (confirmMsg == DialogResult.Yes)
                {
                    db.LoaiSanPhams.DeleteOnSubmit(itemChange);
                    db.SubmitChanges();
                    ShowLoaiSP();
                }
            }

            if (e.ColumnIndex == 2)
            {
                EditLoaiSP f = new EditLoaiSP();
                f.Tag = id;
                tabIndex = 0;
                f.ShowDialog();
            }
        }

        private void buttonSearchLoaiSP_Click(object sender, EventArgs e)
        {
            SearchLoaiSP();
        }

        private void SearchLoaiSP()
        {
            dataGridViewLoaiSP.Rows.Clear();
            string search = textBoxSearchLoaiSP.Text;

            if (search == "")
            {
                ShowLoaiSP();
                return;
            }

            var listSearch = from lsp in db.LoaiSanPhams
                             where lsp.TenLSP.Contains(search)
                             orderby lsp.MaLSP descending
                             select lsp;

            if (listSearch.Count() == 0)
            {
                MessageBox.Show("Không tìm thấy loại sản phẩm với từ khoá " + search);
            }
            else
            {
                foreach (var item in listSearch)
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridViewLoaiSP.Rows[0].Clone();

                    row.Cells[0].Value = item.MaLSP;
                    row.Cells[1].Value = item.TenLSP;
                    row.Cells[2].Value = "Cập nhật";
                    row.Cells[3].Value = "Xoá";

                    dataGridViewLoaiSP.Rows.Add(row);
                }
            }
        }

        private void buttonClearSearchLoaiSP_Click(object sender, EventArgs e)
        {
            textBoxSearchLoaiSP.Clear();
            ShowLoaiSP();
        }

        private void textBoxSearchLoaiSP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SearchLoaiSP();
            }
        }

        /*
            SanPham
            @author: Dang Duc Tung
         */
        private void ShowSanPham()
        {
            dataGridViewSanPham.Rows.Clear();
            var list = from item in db.SanPhams
                       orderby item.MaSP descending
                       select new { item.MaSP, item.TenSP, item.LoaiSanPham.TenLSP, item.DonGia, item.KhuyenMai, item.SoLuong };

            foreach (var item in list)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridViewSanPham.Rows[0].Clone();

                row.Cells[0].Value = item.MaSP;
                row.Cells[1].Value = item.TenSP;
                row.Cells[2].Value = item.TenLSP;
                row.Cells[3].Value = item.DonGia.ToString();
                row.Cells[4].Value = item.KhuyenMai;
                row.Cells[5].Value = item.SoLuong;

                row.Cells[6].Value = "Cập nhật";
                row.Cells[7].Value = "Xoá";

                dataGridViewSanPham.Rows.Add(row);
            }
        }

        private void ShowComboboxLoaiSP()
        {
            var list = from item in db.LoaiSanPhams
                       orderby item.MaLSP descending
                       select new { item.MaLSP, item.TenLSP };

            comboBoxLoaiSP.DataSource = list;
            comboBoxLoaiSP.DisplayMember = "TenLSP";
            comboBoxLoaiSP.ValueMember = "MaLSP";
        }

        private void buttonAddSP_Click(object sender, EventArgs e)
        {
            int donGia, soLuong, maLSP;
            float khuyenMai = 0;
            string tenSP;
            SanPham newItem = new SanPham();

            if (textBoxTenSP.Text == "" || textBoxDonGiaSP.Text == "" || textBoxSoLuongSP.Text == "")
            {
                MessageBox.Show("Tên sản phẩm, đơn giá, số lượng không được để trống");
                return;
            }

            tenSP = textBoxTenSP.Text;
            newItem.TenSP = tenSP;

            maLSP = int.Parse(comboBoxLoaiSP.SelectedValue.ToString());
            newItem.MaLSP = maLSP;
            try
            {
                donGia = int.Parse(textBoxDonGiaSP.Text);
                if (donGia < 0)
                {
                    MessageBox.Show("Đơn giá phải là 1 số nguyên dương");
                    return;
                }
                newItem.DonGia = donGia;
            }
            catch (Exception)
            {
                MessageBox.Show("Đơn giá phải là 1 số nguyên dương");
                return;
            }

            try
            {
                soLuong = int.Parse(textBoxSoLuongSP.Text);
                if (soLuong < 0)
                {
                    MessageBox.Show("Số lượng phải là 1 số nguyên dương");
                    return;
                }
                newItem.SoLuong = soLuong;
            }
            catch (Exception)
            {
                MessageBox.Show("Số lượng phải là 1 số nguyên");
                return;
            }

            try
            {
                khuyenMai = float.Parse(textBoxKhuyenMaiSP.Text);
                if (khuyenMai < 0 || khuyenMai > 100)
                {
                    MessageBox.Show("Khuyến mãi nằm trong khoảng từ 0 - 100");
                    return;
                }
                newItem.KhuyenMai = khuyenMai;
            }
            catch (Exception)
            {
                MessageBox.Show("Khuyến mãi phải là 1 số");
                return;
            }

            db.SanPhams.InsertOnSubmit(newItem);
            db.SubmitChanges();
            ShowSanPham();
        }

        private void buttonClearSP_Click(object sender, EventArgs e)
        {
            textBoxTenSP.Clear();
            textBoxDonGiaSP.Clear();
            textBoxKhuyenMaiSP.Clear();
            textBoxSoLuongSP.Clear();
            comboBoxLoaiSP.SelectedIndex = 0;
        }

        private void dataGridViewSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = int.Parse(dataGridViewSanPham.Rows[e.RowIndex].Cells[0].Value.ToString());
            SanPham itemChange = db.SanPhams.SingleOrDefault(item => item.MaSP == id);

            if (e.ColumnIndex == 7)
            {
                var confirmMsg = MessageBox.Show("Bạn có muốn xoá?", "Thông báo", MessageBoxButtons.YesNo);

                if (confirmMsg == DialogResult.Yes)
                {
                    db.SanPhams.DeleteOnSubmit(itemChange);
                    db.SubmitChanges();
                    ShowSanPham();
                }
            }

            if (e.ColumnIndex == 6)
            {
                EditSanPham f = new EditSanPham();
                f.Tag = id;
                tabIndex = 1;
                f.ShowDialog();
            }
        }

        private void SearchSanPham()
        {
            dataGridViewSanPham.Rows.Clear();
            string search = textBoxSearchSanPham.Text;

            if (search == "")
            {
                ShowSanPham();
                return;
            }

            var listSearch = from sp in db.SanPhams
                             where sp.TenSP.Contains(search)
                             orderby sp.MaLSP descending
                             select new { sp.MaSP, sp.TenSP, sp.LoaiSanPham.TenLSP, sp.DonGia, sp.KhuyenMai, sp.SoLuong };

            if (listSearch.Count() == 0)
            {
                MessageBox.Show("Không tìm thấy sản phẩm với từ khoá " + search);
            }
            else
            {
                foreach (var item in listSearch)
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridViewSanPham.Rows[0].Clone();

                    row.Cells[0].Value = item.MaSP;
                    row.Cells[1].Value = item.TenSP;
                    row.Cells[2].Value = item.TenLSP;
                    row.Cells[3].Value = item.DonGia;
                    row.Cells[4].Value = item.KhuyenMai;
                    row.Cells[5].Value = item.SoLuong;

                    row.Cells[6].Value = "Cập nhật";
                    row.Cells[7].Value = "Xoá";

                    dataGridViewSanPham.Rows.Add(row);
                }
            }
        }

        private void buttonSearchSanPham_Click(object sender, EventArgs e)
        {
            SearchSanPham();
        }

        private void buttonClearSearchSanPham_Click(object sender, EventArgs e)
        {
            textBoxSearchSanPham.Clear();
            ShowSanPham();
        }

        private void textBoxSearchSanPham_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SearchSanPham();
            }
        }

        /*
            NhaCungCap
            @author: Duong Van Duc
         */
        private void ShowNhaCungCap()
        {
            dataGridViewNCC.Rows.Clear();
            var list = from item in db.NhaCungCaps
                       orderby item.MaNCC descending
                       select new { item.MaNCC, item.TenNCC, item.DiaChi, item.SoDienThoai };

            foreach (var item in list)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridViewNCC.Rows[0].Clone();

                row.Cells[0].Value = item.MaNCC;
                row.Cells[1].Value = item.TenNCC;
                row.Cells[2].Value = item.DiaChi;
                row.Cells[3].Value = item.SoDienThoai;

                row.Cells[4].Value = "Cập nhật";
                row.Cells[5].Value = "Xoá";

                dataGridViewNCC.Rows.Add(row);
            }
        }

        private void buttonAddNCC_Click(object sender, EventArgs e)
        {
            if (textBoxTenNCC.Text == "")
            {
                MessageBox.Show("Tên nhà cung cấp không được để trống", "Thông báo");
                return;
            }

            NhaCungCap ncc = new NhaCungCap();
            ncc.TenNCC = textBoxTenNCC.Text;
            ncc.DiaChi = textBoxDiaChiNCC.Text;
            ncc.SoDienThoai = textBoxSoDienThoaiNCC.Text;

            db.NhaCungCaps.InsertOnSubmit(ncc);
            db.SubmitChanges();
            ShowNhaCungCap();
        }

        private void buttonClearNCC_Click(object sender, EventArgs e)
        {
            textBoxTenNCC.Clear();
            textBoxDiaChiNCC.Clear();
            textBoxSoDienThoaiNCC.Clear();
        }

        private void dataGridViewNCC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = int.Parse(dataGridViewNCC.Rows[e.RowIndex].Cells[0].Value.ToString());
            NhaCungCap itemChange = db.NhaCungCaps.SingleOrDefault(item => item.MaNCC == id);

            if (e.ColumnIndex == 5)
            {
                var confirmMsg = MessageBox.Show("Bạn có muốn xoá?", "Thông báo", MessageBoxButtons.YesNo);

                if (confirmMsg == DialogResult.Yes)
                {
                    db.NhaCungCaps.DeleteOnSubmit(itemChange);
                    db.SubmitChanges();
                    ShowNhaCungCap();
                }
            }

            if (e.ColumnIndex == 4)
            {
                EditNhaCungCap f = new EditNhaCungCap();
                f.Tag = id;
                tabIndex = 2;
                f.ShowDialog();
            }
        }

        private void SearchNhaCungCap()
        {
            dataGridViewNCC.Rows.Clear();
            string search = textBoxSearchNCC.Text;

            if (search == "")
            {
                ShowNhaCungCap();
                return;
            }

            var listSearch = from item in db.NhaCungCaps
                             where item.TenNCC.Contains(search)
                             orderby item.MaNCC descending
                             select new { item.MaNCC, item.TenNCC, item.DiaChi, item.SoDienThoai };

            if (listSearch.Count() == 0)
            {
                MessageBox.Show("Không tìm thấy nhà cung cấp với từ khoá " + search);
            }
            else
            {
                foreach (var item in listSearch)
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridViewNCC.Rows[0].Clone();

                    row.Cells[0].Value = item.MaNCC;
                    row.Cells[1].Value = item.TenNCC;
                    row.Cells[2].Value = item.DiaChi;
                    row.Cells[3].Value = item.SoDienThoai;

                    row.Cells[4].Value = "Cập nhật";
                    row.Cells[5].Value = "Xoá";

                    dataGridViewNCC.Rows.Add(row);
                }
            }
        }

        private void textBoxSearchNCC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SearchNhaCungCap();
            }
        }

        private void buttonSearchNCC_Click(object sender, EventArgs e)
        {
            SearchNhaCungCap();
        }

        private void buttonClearSearchNCC_Click(object sender, EventArgs e)
        {
            textBoxSearchNCC.Clear();
            SearchNhaCungCap();
        }

        /*
            PhieuNhap
            @author: Duong Van Duc
         */

        private void btnPNH_Them_Click(object sender, EventArgs e)
        {
            CreatePhieuNhapHang f = new CreatePhieuNhapHang();
            tabIndex = 8;
            f.ShowDialog();
        }
        private void grPhieuNhapHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = int.Parse(grPhieuNhapHang.Rows[e.RowIndex].Cells[0].Value.ToString());
            if (e.ColumnIndex == 3)
            {
                ViewPhieuNhapHang f = new ViewPhieuNhapHang();
                f.Tag = id;
                f.ShowDialog();
            }
        }
        private void HienThiPhieuNhapHang()
        {
            grPhieuNhapHang.Rows.Clear();
            var danhSach = from ptu in db.PhieuNhaps
                           orderby ptu.MaPN descending
                           select new { ptu.MaPN, ptu.NgayNhapHang, ptu.MaPDH };

            foreach (var phanTu in danhSach)
            {
                DataGridViewRow hang = (DataGridViewRow)grPhieuNhapHang.Rows[0].Clone();

                hang.Cells[0].Value = phanTu.MaPN;
                hang.Cells[1].Value = phanTu.NgayNhapHang;
                hang.Cells[2].Value = phanTu.MaPDH;
                hang.Cells[3].Value = "Xem chi tiết";

                grPhieuNhapHang.Rows.Add(hang);
            }
        }

        /*
            PhieuDatHang - PDH
            @author: Dang Duc Tung
         */

        private void HienThiPhieuDatHang()
        {
            grPhieuDatHang.Rows.Clear();
            var danhSach = from ptu in db.PhieuDatHangs
                       orderby ptu.MaPDH descending
                       select new { ptu.MaPDH, ptu.NgayDatHang, ptu.NhaCungCap.TenNCC };

            foreach (var phanTu in danhSach)
            {
                DataGridViewRow hang = (DataGridViewRow)grPhieuDatHang.Rows[0].Clone();

                hang.Cells[0].Value = phanTu.MaPDH;
                hang.Cells[1].Value = phanTu.NgayDatHang;
                hang.Cells[2].Value = phanTu.TenNCC;
                hang.Cells[3].Value = "Xem chi tiết";

                grPhieuDatHang.Rows.Add(hang);
            }
        }
        private void btnPDH_Them_Click(object sender, EventArgs e)
        {
            CreatePhieuDatHang f = new CreatePhieuDatHang();
            tabIndex = 7;
            f.ShowDialog();
        }
        private void grPhieuDatHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = int.Parse(grPhieuDatHang.Rows[e.RowIndex].Cells[0].Value.ToString());
            if (e.ColumnIndex == 3)
            {
                ViewPhieuDatHang f = new ViewPhieuDatHang();
                f.Tag = id;
                f.ShowDialog();
            }
        }

        /*
            DonHang
            @author: Dang Duc Tung
         */
        private void ShowOrder()
        {
            dataGridViewOrder.Rows.Clear();
            var list = from item in db.HoaDons
                       orderby item.MaHD descending
                       select new { item.MaHD, item.NgayLap, item.KhachHang.TenKH, item.NhanVien.TenNV, item.GhiChu };

            foreach (var item in list)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridViewOrder.Rows[0].Clone();

                row.Cells[0].Value = item.MaHD;
                row.Cells[1].Value = item.NgayLap;
                row.Cells[2].Value = item.TenKH;
                row.Cells[3].Value = item.TenNV;
                row.Cells[4].Value = item.GhiChu;
                row.Cells[5].Value = "Xem chi tiết";

                dataGridViewOrder.Rows.Add(row);
            }
        }

        private void dataGridViewOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = int.Parse(dataGridViewOrder.Rows[e.RowIndex].Cells[0].Value.ToString());
            if (e.ColumnIndex == 5)
            {
                ViewOrder f = new ViewOrder();
                f.Tag = id;
                f.ShowDialog();
            }
        }
        
        /*
            KhachHang
            @author
         */
        private void ShowCustomer()
        {
            dataGridViewKH.Rows.Clear();
            var list = from item in db.KhachHangs
                       orderby item.MaKH descending
                       select new { item.MaKH, item.TenKH, item.SoDienThoai, item.DiemTichLuy };

            foreach (var item in list)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridViewKH.Rows[0].Clone();

                row.Cells[0].Value = item.MaKH;
                row.Cells[1].Value = item.TenKH;
                row.Cells[2].Value = item.SoDienThoai;
                row.Cells[3].Value = item.DiemTichLuy;
                row.Cells[4].Value = "Cập nhật";
                row.Cells[5].Value = "Xoá";

                dataGridViewKH.Rows.Add(row);
            }
        }

        private void buttonAddKH_Click(object sender, EventArgs e)
        {
            if (textBoxTenKH.Text == "" || textBoxSoDienThoaiKH.Text == "")
            {
                MessageBox.Show("Tên khách hàng, số điện thoại khách hàng không được để trống", "Thông báo");
                return;
            }

            KhachHang item = new KhachHang();
            item.TenKH = textBoxTenKH.Text;
            item.SoDienThoai = textBoxSoDienThoaiKH.Text;

            db.KhachHangs.InsertOnSubmit(item);
            db.SubmitChanges();
            ShowCustomer();
        }

        private void buttonClearFormKH_Click(object sender, EventArgs e)
        {
            textBoxTenKH.Clear();
            textBoxSoDienThoaiKH.Clear();
        }

        private void SearchCustomer()
        {
            dataGridViewKH.Rows.Clear();
            string search = textBoxSearchKH.Text;

            if (search == "")
            {
                ShowCustomer();
                return;
            }

            var listSearch = from item in db.KhachHangs
                             where item.TenKH.Contains(search) || item.SoDienThoai.Contains(search)
                             orderby item.MaKH descending
                             select new { item.MaKH, item.TenKH, item.SoDienThoai, item.DiemTichLuy };

            if (listSearch.Count() == 0)
            {
                MessageBox.Show("Không tìm thấy khách hàng với từ khoá " + search);
            }
            else
            {
                foreach (var item in listSearch)
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridViewKH.Rows[0].Clone();

                    row.Cells[0].Value = item.MaKH;
                    row.Cells[1].Value = item.TenKH;
                    row.Cells[2].Value = item.SoDienThoai;
                    row.Cells[3].Value = item.DiemTichLuy;
                    row.Cells[4].Value = "Cập nhật";
                    row.Cells[5].Value = "Xoá";

                    dataGridViewKH.Rows.Add(row);
                }
            }
        }

        private void textBoxSearchKH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchCustomer();
            }
        }

        private void textBoxSearchKH_Leave(object sender, EventArgs e)
        {
            SearchCustomer();
        }

        private void buttonSearchKH_Click(object sender, EventArgs e)
        {
            SearchCustomer();
        }

        private void buttonClearSearchKH_Click(object sender, EventArgs e)
        {
            textBoxSearchKH.Clear();
            ShowCustomer();
        }

        private void dataGridViewKH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = int.Parse(dataGridViewKH.Rows[e.RowIndex].Cells[0].Value.ToString());
            KhachHang itemChange = db.KhachHangs.SingleOrDefault(item => item.MaKH == id);

            if (e.ColumnIndex == 5)
            {
                var confirmMsg = MessageBox.Show("Bạn có muốn xoá?", "Thông báo", MessageBoxButtons.YesNo);

                if (confirmMsg == DialogResult.Yes)
                {
                    db.KhachHangs.DeleteOnSubmit(itemChange);
                    db.SubmitChanges();
                    ShowCustomer();
                }
            }

            if (e.ColumnIndex == 4)
            {
                
            }
        }

        /*
         * NhanVien
         * @author
         */

        private void HienThiNhanVien()
        {
            dataGridViewNhanVien.Rows.Clear();
            var list = from item in db.NhanViens
                       orderby item.MaNV descending
                       select new { item.MaNV, item.Username, item.TenNV, item.SoDienThoai, item.NgaySinh, item.QueQuan, item.ChucVu };

            foreach (var item in list)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridViewNhanVien.Rows[0].Clone();

                row.Cells[0].Value = item.MaNV;
                row.Cells[1].Value = item.TenNV;
                row.Cells[2].Value = item.Username;
                row.Cells[3].Value = item.SoDienThoai;
                row.Cells[4].Value = item.NgaySinh;
                row.Cells[5].Value = item.QueQuan;
                row.Cells[6].Value = item.ChucVu == 1 ? "Quản lý" : "Nhân viên";
                row.Cells[7].Value = "Cập nhật";
                row.Cells[8].Value = "Xoá";

                dataGridViewNhanVien.Rows.Add(row);
            }
        }

        private void dataGridViewNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
