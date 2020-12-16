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
    public partial class EditLoaiSP : Form
    {
        QLBanHangDataContext db = new QLBanHangDataContext();
        LoaiSanPham item = null;
        int id;
        public EditLoaiSP()
        {
            InitializeComponent();
        }
        private void EditLoaiSP_Load(object sender, EventArgs e)
        {
            id = (int)this.Tag;
            item = db.LoaiSanPhams.SingleOrDefault(i => i.MaLSP == id);

            textBoxMaLSP.Text = item.MaLSP.ToString();
            textBoxTenLSP.Text = item.TenLSP;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            string tenLSP = textBoxTenLSP.Text;

            if (tenLSP == "")
            {
                MessageBox.Show("Tên loại sản phẩm không được để trống");
                return;
            }

            item.TenLSP = tenLSP;
            db.SubmitChanges();
            this.Close();
        }
    }
}
