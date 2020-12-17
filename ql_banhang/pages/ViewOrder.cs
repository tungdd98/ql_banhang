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
    public partial class ViewOrder : Form
    {
        QLBanHangDataContext db = new QLBanHangDataContext();
        HoaDon hoaDon = null;
        int id;
        public ViewOrder()
        {
            InitializeComponent();
        }

        private void ViewOrder_Load(object sender, EventArgs e)
        {
            id = (int)this.Tag;
            hoaDon = db.HoaDons.SingleOrDefault(hd => hd.MaHD == id);

            labelMaHD.Text = "Mã hoá đơn: " + hoaDon.MaHD;
            labelNgayLap.Text = "Ngày lập: " + hoaDon.NgayLap;
            labelTenKH.Text = "Tên khách hàng: " + hoaDon.KhachHang.TenKH;
            labelTenNV.Text = "Tên nhân viên: " + hoaDon.NhanVien.TenNV;
            labelGhiChu.Text = "Ghi chú: " + hoaDon.GhiChu;
            labelTongTien.Text = "Tổng tiền: " + hoaDon.TongTien.ToString();
            ShowDanhSachSanPham();
        }
        private void ShowDanhSachSanPham()
        {
            dataGridViewSP.Rows.Clear();
            var list = from ct in db.ChiTietHoaDons
                       where ct.MaHD == id
                       select new { ct.MaSP, ct.SanPham.TenSP, ct.SoLuongMua, ct.SanPham.DonGia };

            foreach (var item in list)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridViewSP.Rows[0].Clone();

                row.Cells[0].Value = item.MaSP;
                row.Cells[1].Value = item.TenSP;
                row.Cells[2].Value = item.SoLuongMua;
                row.Cells[3].Value = item.DonGia;
                row.Cells[4].Value = (item.SoLuongMua * item.DonGia).ToString();

                dataGridViewSP.Rows.Add(row);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
