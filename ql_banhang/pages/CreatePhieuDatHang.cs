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
    public partial class CreatePhieuDatHang : Form
    {
        QLBanHangDataContext db = new QLBanHangDataContext();
        public CreatePhieuDatHang()
        {
            InitializeComponent();
        }

        private void CreatePhieuDatHang_Load(object sender, EventArgs e)
        {
            HienThiCbNhaCungCap();
        }
        private void HienThiCbNhaCungCap()
        {
            var danhSach = from pTu in db.NhaCungCaps
                           orderby pTu.MaNCC descending
                           select new { pTu.MaNCC, pTu.TenNCC };

            cbNCC.DataSource = danhSach;
            cbNCC.DisplayMember = "TenNCC";
            cbNCC.ValueMember = "MaNCC";
        }

        private void tbMaSP_KeyDown(object sender, KeyEventArgs e)
        {
            TimKiemThongTinSanPham();
        }

        private void tbMaSP_Leave(object sender, EventArgs e)
        {
            TimKiemThongTinSanPham();
        }
        private void TimKiemThongTinSanPham()
        {
            if (tbMaSP.Text == "")
            {
                return;
            }

            int maSP;
            try
            {
                maSP = int.Parse(tbMaSP.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Mã sản phẩm không hợp lệ", "Thông báo");
                return;
            }

            SanPham sanPham = db.SanPhams.SingleOrDefault(sp => sp.MaSP == maSP);
            if (sanPham != null)
            {
                tbTenSP.Text = sanPham.TenSP;
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin sản phẩm phù hợp", "Thông báo");
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (tbSoLuongDat.Text == "" || tbDonGiaDat.Text == "")
            {
                MessageBox.Show("Đơn giá đặt và số lượng đặt không được để trống", "Thông báo");
                return;
            }

            int soLuongDat, donGiaDat;
            try
            {
                soLuongDat = int.Parse(tbSoLuongDat.Text);
                donGiaDat = int.Parse(tbDonGiaDat.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Đơn giá đặt và số lượng đặt không hợp lệ", "Thông báo");
                return;
            }

            if (soLuongDat <= 0 || donGiaDat <= 0)
            {
                MessageBox.Show("Đơn giá đặt và số lượng đặt phải lớn hơn 0", "Thông báo");
                return;
            }

            if (grChiTietPhieuDatHang.Rows.Count > 1)
            {
                for (int i = 0; i < grChiTietPhieuDatHang.Rows.Count - 1; i++)
                {
                    DataGridViewRow hang = grChiTietPhieuDatHang.Rows[i];
                    if (hang.Cells[0].Value.ToString().Equals(tbMaSP.Text))
                    {
                        MessageBox.Show("Sản phẩm đã tồn tại", "Thông báo");
                        return;
                    }
                }
            }
            DataGridViewRow row = (DataGridViewRow)grChiTietPhieuDatHang.Rows[0].Clone();
            row.Cells[0].Value = tbMaSP.Text;
            row.Cells[1].Value = tbTenSP.Text;
            row.Cells[2].Value = tbSoLuongDat.Text;
            row.Cells[3].Value = tbDonGiaDat.Text;
            row.Cells[4].Value = "Xoá";
            grChiTietPhieuDatHang.Rows.Add(row);
        }

        private void grChiTietPhieuDatHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                var confirmMsg = MessageBox.Show("Bạn có muốn xoá?", "Thông báo", MessageBoxButtons.YesNo);

                if (confirmMsg == DialogResult.Yes)
                {
                    grChiTietPhieuDatHang.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (grChiTietPhieuDatHang.Rows.Count <= 1)
            {
                MessageBox.Show("Danh sách sản phẩm rỗng, vui lòng nhập thêm sản phẩm", "Thông báo");
                return;
            }

            PhieuDatHang phieuDatHang = new PhieuDatHang();
            phieuDatHang.NgayDatHang = DateTime.Now;
            phieuDatHang.MaNCC = int.Parse(cbNCC.SelectedValue.ToString());
            db.PhieuDatHangs.InsertOnSubmit(phieuDatHang);
            db.SubmitChanges();

            for (int i = 0; i < grChiTietPhieuDatHang.Rows.Count - 1; i++)
            {
                DataGridViewRow hang = grChiTietPhieuDatHang.Rows[i];

                ChiTietPhieuDatHang chiTietPDH = new ChiTietPhieuDatHang();
                chiTietPDH.MaPDH = phieuDatHang.MaPDH;
                chiTietPDH.MaSP = int.Parse(hang.Cells[0].Value.ToString());
                chiTietPDH.SoLuongDat = int.Parse(hang.Cells[2].Value.ToString());
                chiTietPDH.DonGiaDat = int.Parse(hang.Cells[3].Value.ToString());

                db.ChiTietPhieuDatHangs.InsertOnSubmit(chiTietPDH);
                db.SubmitChanges();
            }

            MessageBox.Show("Tạo đơn đặt hàng thành công", "Thông báo");
            btnLuu.Enabled = false;
        }

        private void grChiTietPhieuDatHang_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int soLuongDat, donGiaDat;
            try
            {
                soLuongDat = int.Parse(grChiTietPhieuDatHang.Rows[e.RowIndex].Cells[2].Value.ToString());
                donGiaDat = int.Parse(grChiTietPhieuDatHang.Rows[e.RowIndex].Cells[3].Value.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("Đơn giá đặt, số lượng đặt không hợp lệ", "Thông báo");
                return;
            }
            if (soLuongDat <= 0 || donGiaDat <= 0)
            {
                MessageBox.Show("Đơn giá đặt, số lượng đặt phải lớn hơn 0", "Thông báo");
                return;
            }
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            grChiTietPhieuDatHang.Rows.Clear();
            tbMaSP.Clear();
            tbDonGiaDat.Clear();
            tbSoLuongDat.Clear();
        }
    }
}
