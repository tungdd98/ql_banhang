﻿using System;
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
    public partial class AdminManager : Form
    {
        QLBanHangDataContext db = new QLBanHangDataContext();
        public AdminManager()
        {
            InitializeComponent();
        }
        private void AdminManager_Load(object sender, EventArgs e)
        {
            ShowLoaiSP();
        }

        private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            switch (e.TabPageIndex)
            {
                case 0:
                    ShowLoaiSP();
                    break;
                case 1:
                    ShowSanPham();
                    ShowComboboxLoaiSP();
                    break;
                default:

                    break;
            }
        }

        /*
            Manager LoaiSanPham
            @author: Dang Duc Tung
         */
        public void ShowLoaiSP()
        {
            dataGridViewLoaiSP.Rows.Clear();
            var list = from item in db.LoaiSanPhams
                       orderby item.MaLSP descending
                       select item;

            foreach (var item in list)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridViewLoaiSP.Rows[0].Clone();

                row.Cells[0].Value = item.MaLSP;
                row.Cells[1].Value = item.TenLSP;
                row.Cells[2].Value = "Cập nhật";
                row.Cells[3].Value = "Xoá";

                dataGridViewLoaiSP.Rows.Add(row);
            }
        }

        private void buttonClearCategory_Click(object sender, EventArgs e)
        {
            textBoxTenLSP.Clear();
        }

        private void buttonAddLoaiSP_Click(object sender, EventArgs e)
        {
            string tenLSP = textBoxTenLSP.Text;
            if (tenLSP == "")
            {
                MessageBox.Show("Tên loại sản phẩm không được để trống");
                return;
            }
            LoaiSanPham newItem = new LoaiSanPham();
            newItem.TenLSP = tenLSP;

            db.LoaiSanPhams.InsertOnSubmit(newItem);
            db.SubmitChanges();
            ShowLoaiSP();
        }

        private void dataGridViewLoaiSP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = int.Parse(dataGridViewLoaiSP.Rows[e.RowIndex].Cells[0].Value.ToString());
            LoaiSanPham itemChange = db.LoaiSanPhams.SingleOrDefault(item => item.MaLSP == id);

            if (e.ColumnIndex == 3)
            {
                var confirmMsg = MessageBox.Show("Bạn có muốn xoá?", "Thông báo", MessageBoxButtons.YesNo);

                if (confirmMsg == DialogResult.Yes)
                {
                    db.LoaiSanPhams.DeleteOnSubmit(itemChange);
                    db.SubmitChanges();
                    ShowLoaiSP();
                }
            }

            if (e.ColumnIndex == 2)
            {
                EditLoaiSP f = new EditLoaiSP();
                f.Tag = id;
                f.ShowDialog();
            }
        }

        private void buttonSearchLoaiSP_Click(object sender, EventArgs e)
        {
            SearchLoaiSP();
        }

        private void SearchLoaiSP()
        {
            dataGridViewLoaiSP.Rows.Clear();
            string search = textBoxSearchLoaiSP.Text;

            if (search == "")
            {
                ShowLoaiSP();
                return;
            }

            var listSearch = from lsp in db.LoaiSanPhams
                             where lsp.TenLSP.Contains(search)
                             orderby lsp.MaLSP descending
                             select lsp;

            if (listSearch.Count() == 0)
            {
                MessageBox.Show("Không tìm thấy loại sản phẩm với từ khoá " + search);
            }
            else
            {
                foreach (var item in listSearch)
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridViewLoaiSP.Rows[0].Clone();

                    row.Cells[0].Value = item.MaLSP;
                    row.Cells[1].Value = item.TenLSP;
                    row.Cells[2].Value = "Cập nhật";
                    row.Cells[3].Value = "Xoá";

                    dataGridViewLoaiSP.Rows.Add(row);
                }
            }
        }

        private void buttonClearSearchLoaiSP_Click(object sender, EventArgs e)
        {
            textBoxSearchLoaiSP.Clear();
            ShowLoaiSP();
        }

        private void textBoxSearchLoaiSP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SearchLoaiSP();
            }
        }

        /*
            SanPham
            @author: Dang Duc Tung
         */
        public void ShowSanPham()
        {
            dataGridViewSanPham.Rows.Clear();
            var list = from item in db.SanPhams
                       orderby item.MaSP descending
                       select new { item.MaSP, item.TenSP, item.LoaiSanPham.TenLSP, item.DonGia, item.KhuyenMai, item.SoLuong };

            foreach (var item in list)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridViewSanPham.Rows[0].Clone();

                row.Cells[0].Value = item.MaSP;
                row.Cells[1].Value = item.TenSP;
                row.Cells[2].Value = item.TenLSP;
                row.Cells[3].Value = item.DonGia;
                row.Cells[4].Value = item.KhuyenMai;
                row.Cells[5].Value = item.SoLuong;

                row.Cells[6].Value = "Cập nhật";
                row.Cells[7].Value = "Xoá";

                dataGridViewSanPham.Rows.Add(row);
            }
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

        private void buttonAddSP_Click(object sender, EventArgs e)
        {
            int donGia, soLuong, maLSP;
            float khuyenMai = 0;
            string tenSP;
            SanPham newItem = new SanPham();

            if (textBoxTenSP.Text == "" || textBoxDonGiaSP.Text == "" || textBoxSoLuongSP.Text == "")
            {
                MessageBox.Show("Tên sản phẩm, đơn giá, số lượng không được để trống");
                return;
            }

            tenSP = textBoxTenSP.Text;
            newItem.TenSP = tenSP;

            maLSP = int.Parse(comboBoxLoaiSP.SelectedValue.ToString());
            newItem.MaLSP = maLSP;
            try
            {
                donGia = int.Parse(textBoxDonGiaSP.Text);
                if (donGia < 0)
                {
                    MessageBox.Show("Đơn giá phải là 1 số nguyên dương");
                    return;
                }
                newItem.DonGia = donGia;
            }
            catch (Exception)
            {
                MessageBox.Show("Đơn giá phải là 1 số nguyên dương");
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
                newItem.SoLuong = soLuong;
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
                newItem.KhuyenMai = khuyenMai;
            }
            catch (Exception)
            {
                MessageBox.Show("Khuyến mãi phải là 1 số");
                return;
            }

            db.SanPhams.InsertOnSubmit(newItem);
            db.SubmitChanges();
            ShowSanPham();
        }

        private void buttonClearSP_Click(object sender, EventArgs e)
        {
            textBoxTenSP.Clear();
            textBoxDonGiaSP.Clear();
            textBoxKhuyenMaiSP.Clear();
            textBoxSoLuongSP.Clear();
            comboBoxLoaiSP.SelectedIndex = 0;
        }

        private void dataGridViewSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = int.Parse(dataGridViewSanPham.Rows[e.RowIndex].Cells[0].Value.ToString());
            SanPham itemChange = db.SanPhams.SingleOrDefault(item => item.MaSP == id);

            if (e.ColumnIndex == 7)
            {
                var confirmMsg = MessageBox.Show("Bạn có muốn xoá?", "Thông báo", MessageBoxButtons.YesNo);

                if (confirmMsg == DialogResult.Yes)
                {
                    db.SanPhams.DeleteOnSubmit(itemChange);
                    db.SubmitChanges();
                    ShowSanPham();
                }
            }

            if (e.ColumnIndex == 6)
            {
                EditSanPham f = new EditSanPham();
                f.Tag = id;
                f.ShowDialog();
            }
        }

        private void SearchSanPham()
        {
            dataGridViewSanPham.Rows.Clear();
            string search = textBoxSearchSanPham.Text;

            if (search == "")
            {
                ShowSanPham();
                return;
            }

            var listSearch = from sp in db.SanPhams
                             where sp.TenSP.Contains(search)
                             orderby sp.MaLSP descending
                             select new { sp.MaSP, sp.TenSP, sp.LoaiSanPham.TenLSP, sp.DonGia, sp.KhuyenMai, sp.SoLuong };

            if (listSearch.Count() == 0)
            {
                MessageBox.Show("Không tìm thấy sản phẩm với từ khoá " + search);
            }
            else
            {
                foreach (var item in listSearch)
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridViewSanPham.Rows[0].Clone();

                    row.Cells[0].Value = item.MaSP;
                    row.Cells[1].Value = item.TenSP;
                    row.Cells[2].Value = item.TenLSP;
                    row.Cells[3].Value = item.DonGia;
                    row.Cells[4].Value = item.KhuyenMai;
                    row.Cells[5].Value = item.SoLuong;

                    row.Cells[6].Value = "Cập nhật";
                    row.Cells[7].Value = "Xoá";

                    dataGridViewSanPham.Rows.Add(row);
                }
            }
        }

        private void buttonSearchSanPham_Click(object sender, EventArgs e)
        {
            SearchSanPham();
        }

        private void buttonClearSearchSanPham_Click(object sender, EventArgs e)
        {
            textBoxSearchSanPham.Clear();
            ShowSanPham();
        }

        private void textBoxSearchSanPham_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SearchSanPham();
            }
        }
    }
}
