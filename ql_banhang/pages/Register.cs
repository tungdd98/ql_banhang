using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ql_banhang
{
    public partial class Register : Form
    {
        QLBanHangDataContext db = new QLBanHangDataContext();
        public Register()
        {
            InitializeComponent();
        }

        private void buttonDangKy_Click(object sender, EventArgs e)
        {
            if (textBoxTenTaiKhoan.Text == "" || textBoxMatKhau.Text == "")
            {
                MessageBox.Show("Tên tài khoản và mật khẩu không được để trống", "Thông báo");
                return;
            }

            if (textBoxMatKhau.Text != textBoxNhapLaiMatKhau.Text)
            {
                MessageBox.Show("Mật khẩu nhập lại không đúng", "Thông báo");
                return;
            }

            NhanVien kiemTraNhanVien = db.NhanViens.SingleOrDefault(nv => nv.Username == textBoxTenTaiKhoan.Text);

            if (kiemTraNhanVien != null)
            {
                MessageBox.Show("Tài khoản đã tồn tại", "Thông báo");
                return;
            }

            NhanVien nhanVien = new NhanVien();
            nhanVien.Username = textBoxTenTaiKhoan.Text;
            nhanVien.Password = textBoxMatKhau.Text;
            nhanVien.TenNV = textBoxTenNV.Text != "" ? textBoxTenNV.Text : "Nhanvien";
            nhanVien.QueQuan = textBoxQueQuan.Text;
            nhanVien.SoDienThoai = textBoxSoDienThoai.Text;
            nhanVien.NgaySinh = dateTimePickerNgaySinh.Value;
            nhanVien.ChucVu = 0;

            db.NhanViens.InsertOnSubmit(nhanVien);
            db.SubmitChanges();
            MessageBox.Show("Đăng ký tài khoản thành công", "Thông báo");

            this.Close();
        }
    }
}
