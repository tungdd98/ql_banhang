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
    public partial class EditPhieuNhap : Form
    {
        QLBanHangDataContext db = new QLBanHangDataContext();
        PhieuNhap phieuNhap = null;
        int id;
        public EditPhieuNhap()
        {
            InitializeComponent();
        }

        private void EditPhieuNhap_Load(object sender, EventArgs e)
        {
            // Lấy id phiếu nhập
            id = (int)this.Tag;
            phieuNhap = db.PhieuNhaps.SingleOrDefault(pn => pn.MaPN == id);

            labelMaPN.Text = phieuNhap.MaPN.ToString();
            labelNgayNhap.Text = phieuNhap.NgayLap.ToString();
            labelNhaCC.Text = phieuNhap.NhaCungCap.TenNCC;
            ShowDanhSach();
        }
        // Hiển thị danh sách
        private void ShowDanhSach()
        {
            dataGridViewPN.Rows.Clear();
            var list = from ct in db.ChiTietPhieuNhaps
                       where ct.MaPN == id
                       select new { ct.MaSP, ct.SanPham.TenSP, ct.DonGiaNhap, ct.SoLuongNhap };

            foreach (var item in list)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridViewPN.Rows[0].Clone();

                row.Cells[0].Value = item.MaSP;
                row.Cells[1].Value = item.TenSP;
                row.Cells[2].Value = item.DonGiaNhap;
                row.Cells[3].Value = item.SoLuongNhap;
                row.Cells[4].Value = "Cập nhật";
                row.Cells[5].Value = "Xoá";

                dataGridViewPN.Rows.Add(row);
            }
        }

        private void buttonHuyBo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewPN_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Lấy id trên gridview
            int idSanPham = int.Parse(dataGridViewPN.Rows[e.RowIndex].Cells[0].Value.ToString());
            // Lấy thông tin sản phẩm và ctpn tương ứng
            SanPham sanPham = db.SanPhams.SingleOrDefault(item => item.MaSP == idSanPham);
            ChiTietPhieuNhap ctpnDangXet = db.ChiTietPhieuNhaps.SingleOrDefault(item => item.MaPN == id && item.MaSP == idSanPham);

            // Xoá
            if (e.ColumnIndex == 5)
            {
                var confirmMsg = MessageBox.Show("Bạn có muốn xoá?", "Thông báo", MessageBoxButtons.YesNo);

                if (confirmMsg == DialogResult.Yes)
                {
                    // Giảm số lượng sản phẩm tương ứng
                    sanPham.SoLuong = sanPham.SoLuong - ctpnDangXet.SoLuongNhap;

                    // Xoá phiếu nhập
                    db.ChiTietPhieuNhaps.DeleteOnSubmit(ctpnDangXet);
                    db.SubmitChanges();
                    ShowDanhSach();
                }
            }

            // Cập nhật
            if (e.ColumnIndex == 4)
            {
                // Lấy số lượng cập nhật hiện tại và thay đổi
                try
                {
                    int soLuongCapNhat = int.Parse(dataGridViewPN.Rows[e.RowIndex].Cells[3].Value.ToString());
                    if (soLuongCapNhat <= 0)
                    {
                        MessageBox.Show("Số lượng phải lớn hơn 0", "Thông báo");
                        return;
                    }
                    sanPham.SoLuong = sanPham.SoLuong - ctpnDangXet.SoLuongNhap + soLuongCapNhat;
                    ctpnDangXet.SoLuongNhap = soLuongCapNhat;
                }
                catch (Exception)
                {
                    MessageBox.Show("Số lượng phải là số nguyên", "Thông báo");
                    return;
                }
                // Thay đổi đơn giá
                try
                {
                    int donGiaCapNhat = int.Parse(dataGridViewPN.Rows[e.RowIndex].Cells[2].Value.ToString());
                    if (donGiaCapNhat <= 0)
                    {
                        MessageBox.Show("Đơn giá phải lớn hơn 0", "Thông báo");
                        return;
                    }
                    ctpnDangXet.DonGiaNhap = donGiaCapNhat;
                }
                catch (Exception)
                {
                    MessageBox.Show("Đơn giá phải là số nguyên", "Thông báo");
                    return;
                }
                

                db.SubmitChanges();
                MessageBox.Show("Cập nhật thành công", "Thông báo");
            }
        }
    }
}
