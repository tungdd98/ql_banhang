using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ql_banhang.pages;

namespace ql_banhang
{
    public partial class OrderManager : Form
    {
        QLBanHangDataContext db = new QLBanHangDataContext();
        NhanVien nhanVien;
        KhachHang khachHang;
        SanPham sanPham;
        static int total = 0;
        public OrderManager()
        {
            InitializeComponent();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void managerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdminManager f = new AdminManager();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void OrderManager_Load(object sender, EventArgs e)
        {
            nhanVien = (NhanVien)this.Tag;
            labelTenNV.Text = nhanVien.TenNV;
            labelNgayLap.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void textBoxSoDienThoaiKH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchInfoKhachHang();
            }
        }
        private void SearchInfoKhachHang()
        {
            string sdtKH = textBoxSoDienThoaiKH.Text;
            if (sdtKH == "")
            {
                return;
            }

            khachHang = db.KhachHangs.SingleOrDefault(kh => kh.SoDienThoai == sdtKH);
            if (khachHang != null)
            {
                textBoxTenKH.Text = khachHang.TenKH;
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin khách hàng phù hợp", "Thông báo");
            }
        }

        private void textBoxSoDienThoaiKH_Leave(object sender, EventArgs e)
        {
            SearchInfoKhachHang();
        }

        private void textBoxMaSP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchInfoSanPham();
            }
        }
        private void SearchInfoSanPham()
        {
            if (textBoxMaSP.Text == "")
            {
                return;
            }
            int maSP;
            try
            {
                maSP = int.Parse(textBoxMaSP.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Mã sản phẩm không đúng định dạng", "Thông báo");
                return;
            }

            sanPham = db.SanPhams.SingleOrDefault(sp => sp.MaSP == maSP);
            if (sanPham != null)
            {
                textBoxTenSP.Text = sanPham.TenSP;
                textBoxSoLuongCon.Text = sanPham.SoLuong.ToString();
                textBoxDonGia.Text = sanPham.DonGia.ToString();
                textBoxKhuyenMai.Text = sanPham.KhuyenMai.ToString();
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin sản phẩm phù hợp", "Thông báo");
            }
        }

        private void textBoxMaSP_Leave(object sender, EventArgs e)
        {
            SearchInfoSanPham();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBoxMaSP.Text == "" || textBoxSoLuongMua.Text == "")
            {
                MessageBox.Show("Mã sản phẩm, số lượng mua không được để trống", "Thông báo");
                return;
            }

            int quantity;
            try
            {
                quantity = int.Parse(textBoxSoLuongMua.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Số lượng mua không đúng định dạng", "Thông báo");
                return;
            }

            if (quantity <= 0)
            {
                MessageBox.Show("Số lượng mua phải lớn hơn 0", "Thông báo");
                return;
            }
            
            if (quantity > int.Parse(textBoxSoLuongCon.Text))
            {
                MessageBox.Show("Số lượng mua vượt quá số lượng sản phẩm còn trong kho", "Thông báo");
                return;
            }

            int price = (int)(int.Parse(textBoxDonGia.Text) - (int.Parse(textBoxDonGia.Text) * float.Parse(textBoxKhuyenMai.Text) / 100));

            if (dataGridViewSP.Rows.Count > 1)
            {
                for (int i = 0; i < dataGridViewSP.Rows.Count - 1; i++)
                {
                    DataGridViewRow r = dataGridViewSP.Rows[i];
                    if (r.Cells[0].Value.ToString().Equals(textBoxMaSP.Text))
                    {
                        MessageBox.Show("Sản phẩm đã tồn tại", "Thông báo");
                        return;
                    }
                }
            }

            DataGridViewRow row = (DataGridViewRow)dataGridViewSP.Rows[0].Clone();

            row.Cells[0].Value = textBoxMaSP.Text;
            row.Cells[1].Value = textBoxTenSP.Text;
            row.Cells[2].Value = textBoxSoLuongMua.Text;
            row.Cells[3].Value = textBoxDonGia.Text;
            row.Cells[4].Value = (float.Parse(textBoxKhuyenMai.Text) * int.Parse(textBoxDonGia.Text) / 100).ToString();
            row.Cells[5].Value = (quantity * price).ToString();
            row.Cells[6].Value = "Xoá";

            dataGridViewSP.Rows.Add(row);
            textBoxSoLuongCon.Text = (int.Parse(textBoxSoLuongCon.Text) - quantity).ToString();
            SetTotalMoney();
        }

        private void dataGridViewSP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                var confirmMsg = MessageBox.Show("Bạn có muốn xoá?", "Thông báo", MessageBoxButtons.YesNo);

                if (confirmMsg == DialogResult.Yes)
                {
                    try
                    {
                        int soLuongMua = int.Parse(dataGridViewSP.Rows[e.RowIndex].Cells[2].Value.ToString());
                        SetTotalMoney();
                        dataGridViewSP.Rows.RemoveAt(e.RowIndex);
                        textBoxSoLuongCon.Text = (int.Parse(textBoxSoLuongCon.Text) + soLuongMua).ToString();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Số lượng mua không đúng định dạng", "Thông báo");
                        return;
                    }
                }
            }
        }

        private void buttonSaveOrder_Click(object sender, EventArgs e)
        {
            if (khachHang == null)
            {
                MessageBox.Show("Vui lòng nhập thông tin khách hàng", "Thông báo");
                return;
            }

            if (dataGridViewSP.Rows.Count <= 1)
            {
                MessageBox.Show("Danh sách sản phẩm rỗng, vui lòng nhập thêm sản phẩm", "Thông báo");
                return;
            }

            HoaDon hoaDon = new HoaDon();
            hoaDon.NgayLap = DateTime.Now;
            hoaDon.MaNV = nhanVien.MaNV;
            hoaDon.MaKH = khachHang.MaKH;
            hoaDon.GhiChu = textBoxNote.Text;
            hoaDon.TongTien = total;

            khachHang.DiemTichLuy = (int)Math.Round((double)(total / 100000), 2, MidpointRounding.ToEven);

            db.HoaDons.InsertOnSubmit(hoaDon);
            db.SubmitChanges();

            for (int i = 0; i < dataGridViewSP.Rows.Count - 1; i++)
            {
                DataGridViewRow row = dataGridViewSP.Rows[i];

                ChiTietHoaDon cthd = new ChiTietHoaDon();
                cthd.MaHD = hoaDon.MaHD;
                cthd.MaSP = int.Parse(row.Cells[0].Value.ToString());
                cthd.SoLuongMua = int.Parse(row.Cells[2].Value.ToString());

                SanPham sanPham = db.SanPhams.SingleOrDefault(sp => sp.MaSP == int.Parse(row.Cells[0].Value.ToString()));
                sanPham.SoLuong = sanPham.SoLuong - int.Parse(row.Cells[2].Value.ToString());

                db.ChiTietHoaDons.InsertOnSubmit(cthd);
                db.SubmitChanges();
            }

            MessageBox.Show("Tạo hoá đơn thành công", "Thông báo");
            buttonSaveOrder.Enabled = false;
        }

        private void buttonNewOrder_Click(object sender, EventArgs e)
        {
            dataGridViewSP.Rows.Clear();
            buttonSaveOrder.Enabled = true;

            textBoxSoDienThoaiKH.Clear();
            textBoxTenKH.Clear();
            textBoxNote.Clear();

            textBoxMaSP.Clear();
            textBoxSoLuongMua.Clear();
            textBoxTenSP.Clear();
            textBoxDonGia.Clear();
            textBoxSoLuongCon.Clear();
            textBoxKhuyenMai.Clear();

            total = 0;
            labelTotal.Text = total.ToString("N0");
        }
        
        private void SetTotalMoney()
        {
            total = 0;
            for (int i = 0; i < dataGridViewSP.Rows.Count - 1; i++)
            {
                DataGridViewRow row = dataGridViewSP.Rows[i];
                int price;
                try
                {
                    price = int.Parse(row.Cells[5].Value.ToString());
                }
                catch (Exception)
                {
                    return;
                }

                total += price;
            }
            labelTotal.Text = total.ToString("N0");
        }

        private void dataGridViewSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != dataGridViewSP.Rows.Count - 1)
            {
                int id = int.Parse(dataGridViewSP.Rows[e.RowIndex].Cells[0].Value.ToString());
                int quantity = int.Parse(dataGridViewSP.Rows[e.RowIndex].Cells[2].Value.ToString());

                SanPham spChoose = db.SanPhams.SingleOrDefault(sp => sp.MaSP == id);

                textBoxMaSP.Text = spChoose.MaSP.ToString();
                textBoxTenSP.Text = spChoose.TenSP;
                textBoxDonGia.Text = spChoose.DonGia.ToString();
                textBoxSoLuongCon.Text = (spChoose.SoLuong - quantity).ToString();
            }
        }

        private void dataGridViewSP_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            int soLuongMua;
            int soLuongCon = int.Parse(textBoxSoLuongCon.Text);
            try
            {
                soLuongMua = int.Parse(dataGridViewSP.Rows[e.RowIndex].Cells[2].Value.ToString());
            }
            catch (Exception)
            {
                return;
            }
            if (soLuongMua > soLuongCon)
            {
                return;
            }
            textBoxSoLuongCon.Text = (soLuongCon + soLuongMua).ToString();
        }

        private void dataGridViewSP_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int soLuongMua;
            int soLuongCon = int.Parse(textBoxSoLuongCon.Text);
            try
            {
                soLuongMua = int.Parse(dataGridViewSP.Rows[e.RowIndex].Cells[2].Value.ToString());
            }
            catch (Exception)
            {
                dataGridViewSP.Rows[e.RowIndex].Cells[2].Value = 1;
                textBoxSoLuongCon.Text = (soLuongCon - 1).ToString();
                MessageBox.Show("Số lượng mua không đúng định dạng", "Thông báo");
                return;
            }
            if (soLuongMua > soLuongCon)
            {
                dataGridViewSP.Rows[e.RowIndex].Cells[2].Value = 1;
                textBoxSoLuongCon.Text = (soLuongCon - 1).ToString();
                MessageBox.Show("Số lượng mua vượt quá số lượng còn trong kho", "Thông báo");
                return;
            }
            int quantity = int.Parse(dataGridViewSP.Rows[e.RowIndex].Cells[2].Value.ToString());
            int price = int.Parse(dataGridViewSP.Rows[e.RowIndex].Cells[3].Value.ToString());
            dataGridViewSP.Rows[e.RowIndex].Cells[5].Value = (quantity * price).ToString();
            SetTotalMoney();
            textBoxSoLuongCon.Text = (sanPham.SoLuong - soLuongMua).ToString();
        }
    }
}
