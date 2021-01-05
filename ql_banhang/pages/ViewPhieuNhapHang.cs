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
    public partial class ViewPhieuNhapHang : Form
    {
        QLBanHangDataContext db = new QLBanHangDataContext();
        PhieuNhap phieuNhap = null;
        int id;
        public ViewPhieuNhapHang()
        {
            InitializeComponent();
        }

        private void ViewPhieuNhapHang_Load(object sender, EventArgs e)
        {
            id = (int)this.Tag;
            phieuNhap = db.PhieuNhaps.SingleOrDefault(pn => pn.MaPN == id);

            labelMaPN.Text = "Mã phiếu nhập: " + phieuNhap.MaPN.ToString();
            labelNgayNhapHang.Text = "Ngày nhập hàng: " + phieuNhap.NgayNhapHang.ToString();
            labelMaPDH.Text = "Mã phiếu đặt hàng: " + phieuNhap.MaPDH;
            HienThiDanhSachSanPham();
        }
        private void HienThiDanhSachSanPham()
        {
            grSanPham.Rows.Clear();
            var danhSach = from ptu in db.ChiTietPhieuNhaps
                           where ptu.MaPN == id
                           select new { ptu.MaSP, ptu.SanPham.TenSP, ptu.SoLuongNhap, ptu.DonGiaNhap };

            foreach (var phanTu in danhSach)
            {
                DataGridViewRow hang = (DataGridViewRow)grSanPham.Rows[0].Clone();

                hang.Cells[0].Value = phanTu.MaSP;
                hang.Cells[1].Value = phanTu.TenSP;
                hang.Cells[2].Value = phanTu.SoLuongNhap;
                hang.Cells[3].Value = phanTu.DonGiaNhap;

                grSanPham.Rows.Add(hang);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
