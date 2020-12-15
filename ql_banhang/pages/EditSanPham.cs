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
    public partial class EditSanPham : Form
    {
        QLBanHangDataContext db = new QLBanHangDataContext();
        SanPham item = null;
        int id;
        public EditSanPham()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EditSanPham_Load(object sender, EventArgs e)
        {
            id = (int)this.Tag;
            item = db.SanPhams.SingleOrDefault(i => i.MaSP == id);
            ShowComboboxLoaiSP();

            textBoxMaSP.Text = item.MaSP.ToString();
            textBoxTenSP.Text = item.TenSP;
            textBoxDonGiaSP.Text = item.DonGia.ToString();
            textBoxSoLuongSP.Text = item.SoLuong.ToString();
            textBoxKhuyenMaiSP.Text = item.KhuyenMai.ToString();
            comboBoxLoaiSP.SelectedValue = item.MaLSP;
        }
        private void ShowComboboxLoaiSP()
        {
            var list = from item in db.LoaiSanPhams
                       orderby item.MaLSP descending
                       select item;

            comboBoxLoaiSP.DataSource = list;
            comboBoxLoaiSP.DisplayMember = "TenLSP";
            comboBoxLoaiSP.ValueMember = "MaLSP";
        }
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            int donGia, soLuong, maLSP;
            float khuyenMai = 0;
            string tenSP;

            if (textBoxTenSP.Text == "" || textBoxDonGiaSP.Text == "" || textBoxSoLuongSP.Text == "")
            {
                MessageBox.Show("Tên sản phẩm, đơn giá, số lượng không được để trống");
                return;
            }

            tenSP = textBoxTenSP.Text;
            item.TenSP = tenSP;

            maLSP = int.Parse(comboBoxLoaiSP.SelectedValue.ToString());
            item.MaLSP = maLSP;
            try
            {
                donGia = int.Parse(textBoxDonGiaSP.Text);
                if (donGia < 0)
                {
                    MessageBox.Show("Đơn giá phải là 1 số nguyên dương");
                    return;
                }
                item.DonGia = donGia;
            }
            catch (Exception)
            {
                MessageBox.Show("Đơn giá phải là 1 số nguyên");
                return;
            }

            try
            {
                soLuong = int.Parse(textBoxSoLuongSP.Text);
                if (soLuong < 0)
                {
                    MessageBox.Show("Số lượng phải là 1 số nguyên dương");
                    return;
                }
                item.SoLuong = soLuong;
            }
            catch (Exception)
            {
                MessageBox.Show("Số lượng phải là 1 số nguyên");
                return;
            }

            try
            {
                khuyenMai = float.Parse(textBoxKhuyenMaiSP.Text);
                if (khuyenMai < 0 || khuyenMai > 100)
                {
                    MessageBox.Show("Khuyến mãi nằm trong khoảng từ 0 - 100");
                    return;
                }
                item.KhuyenMai = khuyenMai;
            }
            catch (Exception)
            {
                MessageBox.Show("Khuyến mãi phải là 1 số");
                return;
            }

            db.SubmitChanges();
            this.Close();
        }
    }
}
