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
    public partial class CreatePhieuNhapHang : Form
    {
        QLBanHangDataContext db = new QLBanHangDataContext();
        static int maPDH;
        SanPham sanPham = null;
        public CreatePhieuNhapHang()
        {
            InitializeComponent();
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            grChiTietPhieuNhapHang.Rows.Clear();
            tbDonGiaDat.Clear();
            tbSoLuongDat.Clear();
            tbSoLuongNhap.Clear();
        }

        private void CreatePhieuNhapHang_Load(object sender, EventArgs e)
        {
            HienThiCbPhieuDatHang();
            HienThiCbSanPham();
            HienThiThongTinSanPhamTuongUng();
        }
        private void HienThiThongTinSanPhamTuongUng()
        {
            if (cbSanPham.Items.Count > 0)
            {
                try
                {
                    int maSP = int.Parse(cbSanPham.SelectedValue.ToString());
                    sanPham = db.SanPhams.SingleOrDefault(sp => sp.MaSP == maSP);
                    var timKiem = db.ChiTietPhieuDatHangs.SingleOrDefault(ptu => ptu.MaPDH == maPDH && ptu.MaSP == maSP);

                    tbDonGiaDat.Text = timKiem.DonGiaDat.ToString();
                    tbSoLuongDat.Text = timKiem.SoLuongDat.ToString();
                }
                catch (Exception)
                {
                    tbDonGiaDat.Clear();
                    tbSoLuongDat.Clear();
                    return;
                }
            }
        }
        private void HienThiCbPhieuDatHang()
        {
            var danhSach = from pTu in db.PhieuDatHangs
                           orderby pTu.MaPDH descending
                           select new { pTu.MaPDH };

            cbPhieuDatHang.DataSource = danhSach;
            cbPhieuDatHang.DisplayMember = "MaPDH";
            cbPhieuDatHang.ValueMember = "MaPDH";
            maPDH = int.Parse(cbPhieuDatHang.SelectedValue.ToString());
        }
        private void HienThiCbSanPham()
        {
            var danhSach = from ptu in db.ChiTietPhieuDatHangs
                           where ptu.MaPDH == maPDH
                           select new { ptu.SanPham.MaSP, ptu.SanPham.TenSP };

            if (danhSach.Count() > 0)
            {
                cbSanPham.DataSource = danhSach;
                cbSanPham.DisplayMember = "TenSP";
                cbSanPham.ValueMember = "MaSP";
            } else
            {
                cbSanPham.DataSource = null;
            }
        }

        private void cbSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSanPham.DataSource != null)
            {
                HienThiThongTinSanPhamTuongUng();
            } else
            {
                tbDonGiaDat.Clear();
                tbSoLuongDat.Clear();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (grChiTietPhieuNhapHang.Rows.Count > 1)
            {
                for (int i = 0; i < grChiTietPhieuNhapHang.Rows.Count - 1; i++)
                {
                    DataGridViewRow hang = grChiTietPhieuNhapHang.Rows[i];
                    if (hang.Cells[0].Value.ToString().Equals(sanPham.MaSP.ToString()))
                    {
                        MessageBox.Show("Sản phẩm đã tồn tại", "Thông báo");
                        return;
                    }
                }
            }
            if (tbSoLuongNhap.Text == "")
            {
                MessageBox.Show("Số lượng nhập không được để trống", "Thông báo");
                return;
            }
            int soLuongNhap;
            int soLuongDat = int.Parse(tbSoLuongDat.Text);
            try
            {
                soLuongNhap = int.Parse(tbSoLuongNhap.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Số lượng nhập không đúng định dạng", "Thông báo");
                return;
            }
            if (soLuongNhap <= 0)
            {
                MessageBox.Show("Số lượng nhập phải lớn hơn 0", "Thông báo");
                return;
            }
            if (soLuongNhap > soLuongDat)
            {
                MessageBox.Show("Số lượng nhập phải nhỏ hơn số lượng đặt", "Thông báo");
                return;
            }

            DataGridViewRow row = (DataGridViewRow)grChiTietPhieuNhapHang.Rows[0].Clone();
            row.Cells[0].Value = sanPham.MaSP.ToString();
            row.Cells[1].Value = sanPham.TenSP;
            row.Cells[2].Value = tbDonGiaDat.Text;
            row.Cells[3].Value = tbSoLuongNhap.Text;
            row.Cells[4].Value = "Xoá";
            grChiTietPhieuNhapHang.Rows.Add(row);
            tbSoLuongDat.Text = (soLuongDat - soLuongNhap).ToString();
        }

        private void cbPhieuDatHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                maPDH = int.Parse(cbPhieuDatHang.SelectedValue.ToString());
                HienThiCbSanPham();
                grChiTietPhieuNhapHang.Rows.Clear();
            }
            catch (Exception)
            {
                return;
            }
        }

        private void grChiTietPhieuNhapHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                var confirmMsg = MessageBox.Show("Bạn có muốn xoá?", "Thông báo", MessageBoxButtons.YesNo);

                if (confirmMsg == DialogResult.Yes)
                {
                    int soLuongNhapDangXet = int.Parse(grChiTietPhieuNhapHang.Rows[e.RowIndex].Cells[3].Value.ToString());
                    int soLuongDatHienCo = int.Parse(tbSoLuongDat.Text);
                    tbSoLuongDat.Text = (soLuongDatHienCo + soLuongNhapDangXet).ToString();
                    grChiTietPhieuNhapHang.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void grChiTietPhieuNhapHang_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int soLuongNhap;
            int soLuongDatHienCo = int.Parse(tbSoLuongDat.Text);
            try
            {
                soLuongNhap = int.Parse(grChiTietPhieuNhapHang.Rows[e.RowIndex].Cells[3].Value.ToString());
            }
            catch (Exception)
            {
                grChiTietPhieuNhapHang.Rows[e.RowIndex].Cells[3].Value = 1;
                tbSoLuongDat.Text = (soLuongDatHienCo - 1).ToString();
                MessageBox.Show("Đơn giá nhập không hợp lệ", "Thông báo");
                return;
            }
            if (soLuongNhap > soLuongDatHienCo)
            {
                grChiTietPhieuNhapHang.Rows[e.RowIndex].Cells[3].Value = 1;
                tbSoLuongDat.Text = (soLuongDatHienCo - 1).ToString();
                MessageBox.Show("Số lượng nhập phải nhỏ hơn số lượng đặt", "Thông báo");
                return;
            }
            tbSoLuongDat.Text = (soLuongDatHienCo - soLuongNhap).ToString();
        }

        private void grChiTietPhieuNhapHang_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            int soLuongNhap;
            int soLuongDatHienCo = int.Parse(tbSoLuongDat.Text);
            try
            {
                soLuongNhap = int.Parse(grChiTietPhieuNhapHang.Rows[e.RowIndex].Cells[3].Value.ToString());
            }
            catch (Exception)
            {
                return;
            }
            if (soLuongNhap > soLuongDatHienCo)
            {
                return;
            }
            tbSoLuongDat.Text = (soLuongDatHienCo + soLuongNhap).ToString();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (grChiTietPhieuNhapHang.Rows.Count <= 1)
            {
                MessageBox.Show("Danh sách sản phẩm rỗng, vui lòng nhập thêm sản phẩm", "Thông báo");
                return;
            }

            PhieuNhap phieuNhap = new PhieuNhap();
            phieuNhap.NgayNhapHang = DateTime.Now;
            phieuNhap.MaPDH = int.Parse(cbPhieuDatHang.SelectedValue.ToString());
            db.PhieuNhaps.InsertOnSubmit(phieuNhap);
            db.SubmitChanges();

            for (int i = 0; i < grChiTietPhieuNhapHang.Rows.Count - 1; i++)
            {
                DataGridViewRow hang = grChiTietPhieuNhapHang.Rows[i];

                ChiTietPhieuNhap chiTietPN = new ChiTietPhieuNhap();
                chiTietPN.MaPN = phieuNhap.MaPN;
                chiTietPN.MaSP = int.Parse(hang.Cells[0].Value.ToString());
                chiTietPN.SoLuongNhap = int.Parse(hang.Cells[3].Value.ToString());

                var sanPham = db.SanPhams.SingleOrDefault(sp => sp.MaSP == chiTietPN.MaSP);
                sanPham.SoLuong = sanPham.SoLuong + chiTietPN.SoLuongNhap;

                db.ChiTietPhieuNhaps.InsertOnSubmit(chiTietPN);
                db.SubmitChanges();
            }

            MessageBox.Show("Tạo đơn nhập hàng thành công", "Thông báo");
            btnLuu.Enabled = false;
        }
    }
}
