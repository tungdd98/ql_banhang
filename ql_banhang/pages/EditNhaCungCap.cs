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
    public partial class EditNhaCungCap : Form
    {
        QLBanHangDataContext db = new QLBanHangDataContext();
        NhaCungCap item = null;
        int id;
        public EditNhaCungCap()
        {
            InitializeComponent();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (textBoxTenNCC.Text == "")
            {
                MessageBox.Show("Tên nhà cung cấp không được để trống", "Thông báo");
                return;
            }

            item.TenNCC = textBoxTenNCC.Text;
            item.DiaChi = textBoxDiaChiNCC.Text;
            item.SoDienThoai = textBoxSoDienThoaiNCC.Text;

            db.SubmitChanges();
            this.Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EditNhaCungCap_Load(object sender, EventArgs e)
        {
            id = (int)this.Tag;
            item = db.NhaCungCaps.SingleOrDefault(i => i.MaNCC == id);

            textBoxMaNCC.Text = item.MaNCC.ToString();
            textBoxTenNCC.Text = item.TenNCC;
            textBoxDiaChiNCC.Text = item.DiaChi;
            textBoxSoDienThoaiNCC.Text = item.SoDienThoai;
        }
    }
}
