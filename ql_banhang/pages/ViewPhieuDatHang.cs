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
    public partial class ViewPhieuDatHang : Form
    {
        QLBanHangDataContext db = new QLBanHangDataContext();
        PhieuDatHang phieuDatHang = null;
        int id;
        public ViewPhieuDatHang()
        {
            InitializeComponent();
        }

        private void ViewPhieuDatHang_Load(object sender, EventArgs e)
        {
            id = (int)this.Tag;
            phieuDatHang = db.PhieuDatHangs.SingleOrDefault(pdh => pdh.MaPDH == id);

            labelMaPDH.Text = "Mã đơn đặt hàng: " + phieuDatHang.MaPDH.ToString();
            labelNgayDatHang.Text = "Ngày đặt hàng: " + phieuDatHang.NgayDatHang.ToString();
            labelNhaCungCap.Text = "Tên nhà cung cấp: " + phieuDatHang.NhaCungCap.TenNCC;
            HienThiDanhSachSanPham();
        }

        private void HienThiDanhSachSanPham()
        {
            grSanPham.Rows.Clear();
            var danhSach = from ptu in db.ChiTietPhieuDatHangs
                       where ptu.MaPDH == id
                       select new { ptu.MaSP, ptu.SanPham.TenSP, ptu.SoLuongDat, ptu.DonGiaDat };

            foreach (var phanTu in danhSach)
            {
                DataGridViewRow hang = (DataGridViewRow)grSanPham.Rows[0].Clone();

                hang.Cells[0].Value = phanTu.MaSP;
                hang.Cells[1].Value = phanTu.TenSP;
                hang.Cells[2].Value = phanTu.SoLuongDat;
                hang.Cells[3].Value = phanTu.DonGiaDat;

                grSanPham.Rows.Add(hang);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
