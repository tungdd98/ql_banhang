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
            f.ShowDialog();
        }
    }
}
