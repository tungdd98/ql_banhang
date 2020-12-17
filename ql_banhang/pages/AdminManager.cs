using System;
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
                case 6:
                    ShowPhieuNhap();
                    ShowComboboxNhaCungCap();
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
        private void ShowComboboxNhaCungCap()
        {
            var list = from item in db.NhaCungCaps
                       orderby item.MaNCC descending
                       select new { item.MaNCC, item.TenNCC };

            comboBoxNhaCC.DataSource = list;
            comboBoxNhaCC.DisplayMember = "TenNCC";
            comboBoxNhaCC.ValueMember = "MaNCC";
        }

        private void ShowPhieuNhap()
        {
            dataGridViewPN.Rows.Clear();
            var list = from item in db.PhieuNhaps
                       orderby item.MaPN descending
                       select new { item.MaPN, item.NgayLap, item.NhaCungCap.TenNCC };

            foreach (var item in list)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridViewPN.Rows[0].Clone();

                row.Cells[0].Value = item.MaPN;
                row.Cells[1].Value = item.NgayLap;
                row.Cells[2].Value = item.TenNCC;
                row.Cells[3].Value = "Cập nhật";
                row.Cells[4].Value = "Xoá";

                dataGridViewPN.Rows.Add(row);
            }
        }

        private void buttonThemCTPN_Click(object sender, EventArgs e)
        {
            if (textBoxMaSPPN.Text == "" || textBoxSoLuongNhap.Text == "" || textBoxDonGiaNhap.Text == "")
            {
                MessageBox.Show("Mã sản phẩm, số lượng nhập, đơn giá nhập không được để trống", "Thông báo");
                return;
            }

            int soLuongNhap = 0, donGiaNhap = 0;

            try
            {
                soLuongNhap = int.Parse(textBoxSoLuongNhap.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Số lượng nhập phải là số nguyên", "Thông báo");
                return;
            }

            try
            {
                donGiaNhap = int.Parse(textBoxDonGiaNhap.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Đơn giá nhập phải là số nguyên", "Thông báo");
                return;
            }

            bool isCheckExist = false;
            if (dataGridViewCTPN.Rows.Count > 1)
            {
                for (int i = 0; i < dataGridViewCTPN.Rows.Count - 1; i++)
                {
                    DataGridViewRow r = dataGridViewCTPN.Rows[i];
                    if (r.Cells[0].Value.ToString().Equals(textBoxMaSPPN.Text))
                    {
                        MessageBox.Show("Đã tồn tại sản phẩm trong danh sách", "Thông báo");
                        isCheckExist = true;
                        break;
                    }
                }
            }
            if (isCheckExist)
            {
                return;
            }

            DataGridViewRow row = (DataGridViewRow)dataGridViewCTPN.Rows[0].Clone();
            row.Cells[0].Value = textBoxMaSPPN.Text;
            row.Cells[1].Value = donGiaNhap;
            row.Cells[2].Value = soLuongNhap;
            row.Cells[3].Value = "Xoá";

            dataGridViewCTPN.Rows.Add(row);
        }

        private void buttonLuuPN_Click(object sender, EventArgs e)
        {
            if (dataGridViewCTPN.Rows.Count <= 1)
            {
                MessageBox.Show("Danh sách rỗng, vui lòng nhập thêm sản phẩm", "Thông báo");
                return;
            }

            PhieuNhap phieuNhap = new PhieuNhap();
            phieuNhap.NgayLap = dateTimePickerNgayNhap.Value;
            phieuNhap.MaNCC = int.Parse(comboBoxNhaCC.SelectedValue.ToString());

            db.PhieuNhaps.InsertOnSubmit(phieuNhap);
            db.SubmitChanges();

            for (int i = 0; i < dataGridViewCTPN.Rows.Count - 1; i++)
            {
                DataGridViewRow row = dataGridViewCTPN.Rows[i];

                ChiTietPhieuNhap ctpn = new ChiTietPhieuNhap();
                ctpn.MaPN = phieuNhap.MaPN;
                ctpn.MaSP = int.Parse(row.Cells[0].Value.ToString());
                ctpn.DonGiaNhap = int.Parse(row.Cells[1].Value.ToString());
                ctpn.SoLuongNhap = int.Parse(row.Cells[2].Value.ToString());

                SanPham sanPham = db.SanPhams.SingleOrDefault(sp => sp.MaSP == ctpn.MaSP); 
                sanPham.SoLuong = sanPham.SoLuong + ctpn.SoLuongNhap;

                db.ChiTietPhieuNhaps.InsertOnSubmit(ctpn);
                db.SubmitChanges();
            }

            MessageBox.Show("Tạo phiếu nhập thành công", "Thông báo");
            ShowPhieuNhap();
            buttonLuuPN.Enabled = false;
        }

        private void textBoxMaSPPN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TimKiemThongTinSanPham();
            }
        }
        private void TimKiemThongTinSanPham()
        {
            if (textBoxMaSPPN.Text == "")
            {
                return;
            }
            int maSP = int.Parse(textBoxMaSPPN.Text);
            var sanPham = db.SanPhams.SingleOrDefault(sp => sp.MaSP == maSP);

            if (sanPham != null)
            {
                textBoxTenSPPN.Text = sanPham.TenSP;
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin sản phẩm phù hợp", "Thông báo");
            }
        }

        private void textBoxMaSPPN_Leave(object sender, EventArgs e)
        {
            TimKiemThongTinSanPham();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxTenSPPN.Clear();
            textBoxDonGiaNhap.Clear();
            textBoxSoLuongNhap.Clear();
            textBoxTenSPPN.Clear();
            textBoxMaSPPN.Clear();
        }

        private void buttonTaoMoiPN_Click(object sender, EventArgs e)
        {
            buttonLuuPN.Enabled = true;
            dateTimePickerNgayNhap.Value = DateTime.Now;
            comboBoxNhaCC.SelectedIndex = 0;
            dataGridViewCTPN.Rows.Clear();
            textBoxMaSPPN.Clear();
            textBoxTenSPPN.Clear();
            textBoxDonGiaNhap.Clear();
            textBoxSoLuongNhap.Clear();
            textBoxTenSPPN.Clear();
        }

        private void dataGridViewCTPN_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                var confirmMsg = MessageBox.Show("Bạn có muốn xoá?", "Thông báo", MessageBoxButtons.YesNo);

                if (confirmMsg == DialogResult.Yes)
                {
                    dataGridViewCTPN.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void dataGridViewPN_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = int.Parse(dataGridViewPN.Rows[e.RowIndex].Cells[0].Value.ToString());
            PhieuNhap itemChange = db.PhieuNhaps.SingleOrDefault(item => item.MaPN == id);

            if (e.ColumnIndex == 4)
            {
                var confirmMsg = MessageBox.Show("Bạn có muốn xoá?", "Thông báo", MessageBoxButtons.YesNo);

                if (confirmMsg == DialogResult.Yes)
                {
                    // Lấy danh sách chi tiết phiếu nhập
                    var listCTPN = from ct in db.ChiTietPhieuNhaps
                                   where ct.MaPN == id
                                   select new { ct.MaSP, ct.SoLuongNhap };

                    // Giảm số lượng sản phẩm tương ứng
                    foreach(var ct in listCTPN)
                    {
                        SanPham sanPham = db.SanPhams.SingleOrDefault(sp => sp.MaSP == ct.MaSP);
                        sanPham.SoLuong = sanPham.SoLuong - ct.SoLuongNhap;
                        db.SubmitChanges();
                    }
                    db.PhieuNhaps.DeleteOnSubmit(itemChange);
                    db.SubmitChanges();
                    ShowPhieuNhap();
                }
            }

            if (e.ColumnIndex == 3)
            {
                EditPhieuNhap f = new EditPhieuNhap();
                f.Tag = id;
                f.ShowDialog();
            }
        }
    }
}
