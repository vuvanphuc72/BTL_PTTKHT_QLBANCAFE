using BTL_PTTKHT_QLBANQA.DAO;
using BTL_PTTKHT_QLBANQA.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_PTTKHT_QLBANQA
{
    public partial class QLPHANMEM : Form
    {


        public QLPHANMEM()
        {
            InitializeComponent();
            loadTable();
            loadDM();          
        }

        #region Method


        void loadDM()
        {
            List<DanhMucDTO> listDM = DanhMucDAO.Instance.GetDanhMuc();
            cboDanhMuc.DataSource = listDM;
            cboDanhMuc.DisplayMember = "tenDM";
        }

        void loadDUBymaDM(int id)
        {
            List<DoUongDTO> listDU = DoUongDAO.Instance.GetDoUong(id);
            cboDoUong.DataSource = listDU;
            cboDoUong.DisplayMember = "tenDU";
        }

        void loadTable()
        {
            flpTable.Controls.Clear();
            List<Table> tableList = TableDAO.Instance.LoadTableList();
            foreach (Table item in tableList)
            {
                Button btn = new Button() { Width = TableDAO.dai, Height = TableDAO.rong};
                btn.Text = item.TenBan + "\n" + item.TrangThai;
                int newSize = 20;
                btn.Font = new Font(btn.Font.FontFamily, newSize);
                btn.Click += btn_Click;
                btn.Tag = item;
                if (item.TrangThai == "Trống")
                    btn.BackColor = Color.PeachPuff;
                else btn.BackColor = Color.SaddleBrown;
                flpTable.Controls.Add(btn);

            }
        }
        void showBill(int id)
        {
            lsvDanhSach.Items.Clear();
            double tongTien = 0;
            List<MMenu> listCTHD = MenuDAO.Instance.GetListMenuByTable(id);
            foreach (MMenu item in listCTHD)
            {
                ListViewItem lsvItem = new ListViewItem(item.TenDU.ToString());
                lsvItem.SubItems.Add(item.SoLuong.ToString());
                lsvItem.SubItems.Add(item.GiaTien.ToString());
                lsvItem.SubItems.Add(item.TongTien.ToString());
                tongTien += Convert.ToDouble(item.TongTien);
                lsvDanhSach.Items.Add(lsvItem);
            }
            CultureInfo culture = new CultureInfo("vi-VN");
            txtTongTien.Text = tongTien.ToString("C", culture);
        }

        #endregion 

        #region Events

        void btn_Click(object sender, EventArgs e)
        {
            int idTable = ((sender as Button).Tag as Table).MaBan;
            lsvDanhSach.Tag = (sender as Button).Tag;
            showBill(idTable);
        }


        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fQuanLy fql = new fQuanLy();
            fql.ShowDialog();
        }


        private void cboDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null)
                return;
            DanhMucDTO selected = cb.SelectedItem as DanhMucDTO;
            id = selected.MaDM;
            loadDUBymaDM(id);
                
        }

        private void btnThemDU_Click(object sender, EventArgs e)
        {
            Table table = lsvDanhSach.Tag as Table;

            int idBill = BillDAO.Instance.getUnCheckBillIByTableID(table.MaBan);
            int maDuu = (cboDoUong.SelectedItem as DoUongDTO).MaDu;
            int sluong = (int)nudCountDU.Value;

            if(idBill == -1)
            {
                BillDAO.Instance.insertBill(table.MaBan);
                BillInforDAO.Instance.insertBillInfor(BillDAO.Instance.getMaxIDBill(), maDuu, sluong);
            }
            else
            {
                BillInforDAO.Instance.insertBillInfor(idBill, maDuu, sluong);
            }
            showBill(table.MaBan);
            loadTable();
        }
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            Table table = lsvDanhSach.Tag as Table;
            int idbBill = BillDAO.Instance.getUnCheckBillIByTableID(table.MaBan);
            if(idbBill != -1)
            {
                if(MessageBox.Show("Thanh toán hoá đơn cho bàn " + table.TenBan, "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK) 
                {
                    float tongTien = (float)Convert.ToDouble(txtTongTien.Text.Split(',')[0].Replace(".", ""));
                    BillDAO.Instance.CheckOut(idbBill, tongTien);
                    showBill(table.MaBan );
                    loadTable();
                }
            }
        }


        #endregion

        private void exitPM(object sender, EventArgs e)
        {
            DialogResult ret = MessageBox.Show("Đóng phần mềm!", "Thông báo", MessageBoxButtons.YesNo);
            if (ret == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
