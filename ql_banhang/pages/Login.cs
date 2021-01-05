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
    public partial class Login : Form
    {
        QLBanHangDataContext db = new QLBanHangDataContext();
        public Login()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn thoát chương trình?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string username = tbUsername.Text;
            string password = tbPassword.Text;
            if (username == "" || password == "")
            {
                MessageBox.Show("Username và password không được để trống");
                return;
            }
            NhanVien nhanVien = db.NhanViens.SingleOrDefault(nv => nv.Username == username && nv.Password == password);

            if (nhanVien != null)
            {
                OrderManager f = new OrderManager();
                f.Tag = nhanVien;
                this.Hide();
                f.ShowDialog();
                this.Show();
                tbPassword.Clear();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register f = new Register();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }
    }
}
