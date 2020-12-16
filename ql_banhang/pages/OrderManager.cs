﻿using System;
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
            int maSP = int.Parse(textBoxMaSP.Text);
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
            int quantity = 0;
            if (textBoxMaSP.Text == "" || textBoxSoLuongMua.Text == "")
            {
                MessageBox.Show("Mã sản phẩm, số lượng mua không được để trống", "Thông báo");
                return;
            }

            try
            {
                quantity = int.Parse(textBoxSoLuongMua.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Số lượng mua phải là số nguyên", "Thông báo");
                return;
            }
            
            if (quantity > int.Parse(textBoxSoLuongCon.Text))
            {
                MessageBox.Show("Số lượng mua vượt quá số lượng sản phẩm còn trong kho", "Thông báo");
                return;
            }

            int price = (int)(int.Parse(textBoxDonGia.Text) - (int.Parse(textBoxDonGia.Text) * float.Parse(textBoxKhuyenMai.Text) / 100));
            bool isCheckExist = false;

            if (dataGridViewSP.Rows.Count > 1)
            {
                for (int i = 0; i < dataGridViewSP.Rows.Count - 1; i++)
                {
                    DataGridViewRow r = dataGridViewSP.Rows[i];
                    if (r.Cells[0].Value.ToString().Equals(textBoxMaSP.Text))
                    {
                        r.Cells[2].Value = (int.Parse(r.Cells[2].Value.ToString()) + quantity);
                        r.Cells[5].Value = (int.Parse(r.Cells[2].Value.ToString()) * price).ToString("N0");

                        textBoxSoLuongCon.Text = (int.Parse(textBoxSoLuongCon.Text) - quantity).ToString();
                        isCheckExist = true;
                        SetTotalMoney();
                        break;
                    }
                }
            }
            if (isCheckExist)
            {
                return;
            }

            DataGridViewRow row = (DataGridViewRow)dataGridViewSP.Rows[0].Clone();

            row.Cells[0].Value = textBoxMaSP.Text;
            row.Cells[1].Value = textBoxTenSP.Text;
            row.Cells[2].Value = textBoxSoLuongMua.Text;
            row.Cells[3].Value = textBoxDonGia.Text;
            row.Cells[4].Value = (float.Parse(textBoxKhuyenMai.Text) * int.Parse(textBoxDonGia.Text) / 100).ToString("N0");
            row.Cells[5].Value = (quantity * price).ToString("N0");
            row.Cells[6].Value = "Cập nhật";
            row.Cells[7].Value = "Xoá";

            dataGridViewSP.Rows.Add(row);
            textBoxSoLuongCon.Text = (int.Parse(textBoxSoLuongCon.Text) - quantity).ToString();
            SetTotalMoney();
        }

        private void dataGridViewSP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                var confirmMsg = MessageBox.Show("Bạn có muốn xoá?", "Thông báo", MessageBoxButtons.YesNo);

                if (confirmMsg == DialogResult.Yes)
                {
                    SetTotalMoney();
                    dataGridViewSP.Rows.RemoveAt(e.RowIndex);
                }
            }

            if (e.ColumnIndex == 6)
            {
                int soLuongMua = int.Parse(dataGridViewSP.Rows[e.RowIndex].Cells[2].Value.ToString());

                if (soLuongMua > sanPham.SoLuong)
                {
                    MessageBox.Show("Số lượng mua vượt quá số lượng sản phẩm còn trong kho", "Thông báo");
                } else
                {
                    int quantity = int.Parse(dataGridViewSP.Rows[e.RowIndex].Cells[2].Value.ToString());
                    int price = int.Parse(dataGridViewSP.Rows[e.RowIndex].Cells[3].Value.ToString());
                    dataGridViewSP.Rows[e.RowIndex].Cells[5].Value = (quantity * price).ToString("N0");
                    SetTotalMoney();
                    textBoxSoLuongCon.Text = (sanPham.SoLuong - soLuongMua).ToString();
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
                int quantity = int.Parse(row.Cells[2].Value.ToString());
                int price = int.Parse(row.Cells[3].Value.ToString());

                total += quantity * price;
            }
            labelTotal.Text = total.ToString("N0");
        }
    }
}
