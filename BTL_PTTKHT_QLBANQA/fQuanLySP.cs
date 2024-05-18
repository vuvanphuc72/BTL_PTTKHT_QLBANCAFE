using BTL_PTTKHT_QLBANQA.DAO;
using BTL_PTTKHT_QLBANQA.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BTL_PTTKHT_QLBANQA
{
    public partial class fQuanLy : Form
    {
        public fQuanLy()
        {
            InitializeComponent();
            LoadMeThods();
        }

        BindingSource DoUongList = new BindingSource();
        BindingSource DanhMucList = new BindingSource();
        BindingSource TaiKhoanList = new BindingSource();
        BindingSource HoaDonList = new BindingSource();
        BindingSource BanList = new BindingSource();

        void LoadMeThods()
        {
            dtgvDoUong.DataSource = DoUongList;
            dtgvDanhMuc.DataSource = DanhMucList;
            dtgvTaiKhoan.DataSource = TaiKhoanList;
            dtgvHoaDon.DataSource = HoaDonList;
            dtgvBan.DataSource = BanList;

            loadListDoUong();
            addFoodBinding();

            loadListDanhMuc();
            DanhMucBinding();

            loadListTaiKhoan();
            TaiKhoanBinding();

            loadListHoaDon();
            HoaDonBinding();

            loadListBan();
            BanBinding();
        }

        #region methods
        void loadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
                dtgvDoanhThu.DataSource = BillDAO.Instance.GetBillListByDate(checkIn, checkOut);
        }

        void addFoodBinding()
        {
            txtTenDU.DataBindings.Add(new Binding("Text", dtgvDoUong.DataSource, "maDU", true, DataSourceUpdateMode.Never));
            txtMaDU.DataBindings.Add(new Binding("Text", dtgvDoUong.DataSource, "tenDU", true, DataSourceUpdateMode.Never));
            nuGiaTien.DataBindings.Add(new Binding("Value", dtgvDoUong.DataSource, "gia", true, DataSourceUpdateMode.Never));
            txtMaDMforDU.DataBindings.Add(new Binding("Text", dtgvDoUong.DataSource, "maDM", true, DataSourceUpdateMode.Never));
        }

        void loadListDoUong()
        {
            DoUongList.DataSource = DoUongDAO.Instance.getListDoUong();
        }

        void loadListDanhMuc()
        {
            DanhMucList.DataSource = DanhMucDAO.Instance.GetDanhMuc();
        }

        void DanhMucBinding()
        {
            txtMaDM.DataBindings.Add(new Binding("Text", dtgvDanhMuc.DataSource, "MaDM", true, DataSourceUpdateMode.Never));
            txtTenDM.DataBindings.Add(new Binding("Text", dtgvDanhMuc.DataSource, "tenDm", true, DataSourceUpdateMode.Never));
        }

        void loadListHoaDon()
        {
            HoaDonList.DataSource = BillDAO.Instance.getListHoaDon();
        }

        void HoaDonBinding()
        {
            txtMaHD.DataBindings.Add(new Binding("Text", dtgvHoaDon.DataSource, "MaHD", true, DataSourceUpdateMode.Never));
            txtThanhToan.DataBindings.Add(new Binding("Text", dtgvHoaDon.DataSource, "ThanhToan", true, DataSourceUpdateMode.Never));
            nuTienHoaDon.DataBindings.Add(new Binding("Value", dtgvHoaDon.DataSource, "TongTien", true, DataSourceUpdateMode.Never));
            txtMaBan.DataBindings.Add(new Binding("Text", dtgvHoaDon.DataSource, "MaBan", true, DataSourceUpdateMode.Never));
            txtDateIn.DataBindings.Add(new Binding("Text", dtgvHoaDon.DataSource, "VaoBan", true, DataSourceUpdateMode.Never));
            txtDateOut.DataBindings.Add(new Binding("Text", dtgvHoaDon.DataSource, "RoiBan", true, DataSourceUpdateMode.Never));
        }

        void loadListBan()
        {
            BanList.DataSource = TableDAO.Instance.getListBan();
        }

        void BanBinding()
        {
            txtMB.DataBindings.Add(new Binding("Text", dtgvBan.DataSource, "MaBan", true, DataSourceUpdateMode.Never));
            txtTenBan.DataBindings.Add(new Binding("Text", dtgvBan.DataSource, "TenBan", true, DataSourceUpdateMode.Never));
            txtTrangThaiBan.DataBindings.Add(new Binding("Text", dtgvBan.DataSource, "TrangThai", true, DataSourceUpdateMode.Never));
        }

        #endregion

        private void btnThongKeDoanhThu_Click(object sender, EventArgs e)
        {
            loadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }

        private void btnThemDoUong_Click(object sender, EventArgs e)
        {
            string name = txtTenDU.Text;
            int maDM = int.Parse(txtMaDMforDU.Text);
            float price = (float)nuGiaTien.Value;
            if(DoUongDAO.Instance.insertDoUong(name, maDM, price))
            {
                MessageBox.Show("Thêm đồ uống thành công!");
                loadListDoUong();
            }
            else
                MessageBox.Show("Thêm đồ uống thất bại!");
        }

        private void btnSuaDoUong_Click(object sender, EventArgs e)
        {
            string name = txtTenDU.Text;
            int maDM = int.Parse(txtMaDMforDU.Text);
            float price = (float)nuGiaTien.Value;
            int idDU = Convert.ToInt32(txtMaDU.Text);
            if (DoUongDAO.Instance.updateDoUong(idDU, name, maDM, price))
            {
                MessageBox.Show("Sửa đồ uống thành công!");
                loadListDoUong();
            }
            else
                MessageBox.Show("Sửa đồ uống thất bại!");
        }

        private void btnXoaDoUong_Click(object sender, EventArgs e)
        {
            if (checkBanTrong())
            {
                MessageBox.Show("Vui lòng chỉ xoá đồ uống khi tất cả bàn đều trống");
                return;
            }
            int idDU = Convert.ToInt32(txtMaDU.Text);
            if (DoUongDAO.Instance.deleteDoUong(idDU))
            {
                MessageBox.Show("Xoá 1 đồ uống thành công!");
                loadListDoUong();
            }
            else
                MessageBox.Show("Xoá đồ uống thất bại!");
        }

        bool checkBanTrong()
        {
            string qr = "select * from Ban where trangThai = N'Có người'";
            int n = -1;
            n = Convert.ToInt32((DataProvider.Instance.executeScalar(qr)));
            if (n > 0)
                return true;
            return false;
        }

        private void btnThemDM_Click(object sender, EventArgs e)
        {
            string name = txtTenDM.Text;
            if (DanhMucDAO.Instance.insertDanhMuc(name))
            {
                MessageBox.Show("Thêm danh mục đồ uống thành công!");
                loadListDanhMuc();
            }
            else
                MessageBox.Show("Thêm danh mục đồ uống thất bại!");
        }

        private void btnSuaDM_Click(object sender, EventArgs e)
        {
            string name = txtTenDM.Text;
            int maDM = int.Parse(txtMaDM.Text);
            if (DanhMucDAO.Instance.updateDanhMuc(name, maDM))
            {
                MessageBox.Show("Sửa danh mục thành công!");
                loadListDanhMuc();
            }
            else
                MessageBox.Show("Sửa danh mục thất bại!");
        }

        private void btnXoaDM_Click(object sender, EventArgs e)
        {
            int idDM = Convert.ToInt32(txtMaDM.Text);

            if (checkBanTrong() || checkDUofDM(idDM))
            {
                MessageBox.Show("Vui lòng chỉ xoá danh mục đồ uống khi không có đồ uống thuộc danh mục này");
                return;
            }

            if (DanhMucDAO.Instance.deleteDanhMuc(idDM))
            {
                MessageBox.Show("Xoá 1 danh mục thành công!");
                loadListDanhMuc();
                loadListDoUong();
            }
            else
                MessageBox.Show("Xoá danh mục thất bại!");
        }

        bool checkDUofDM(int maDM)
        {
            string qr = "select * from DoUong where maDM = " + maDM;
            int n = -1;
            n = Convert.ToInt32((DataProvider.Instance.executeScalar(qr)));
            if (n > 0)
                return true;
            return false;
        }

        void loadListTaiKhoan()
        {
            TaiKhoanList.DataSource = AccountDAO.Instance.getListAccount();
        }

        void TaiKhoanBinding()
        {
            txtHoTenNV.DataBindings.Add(new Binding("Text", dtgvTaiKhoan.DataSource, "HoTen", true, DataSourceUpdateMode.Never));
            txtPasswordNV.DataBindings.Add(new Binding("Text", dtgvTaiKhoan.DataSource, "PassWord", true, DataSourceUpdateMode.Never));
            txtMaNV.DataBindings.Add(new Binding("Text", dtgvTaiKhoan.DataSource, "UserName", true, DataSourceUpdateMode.Never));
        }

        private void btnThemTK_Click(object sender, EventArgs e)
        {
            string name = txtHoTenNV.Text;
            string tk = txtMaNV.Text;
            string mk = txtPasswordNV.Text;

            if (AccountDAO.Instance.insertTaiKhoan(name, tk, mk))
            {
                MessageBox.Show("Thêm tài khoản thành công!");
                loadListTaiKhoan();
            }
            else
                MessageBox.Show("Thêm tài khoản thất bại!");
        }

        private void btnSuaTK_Click(object sender, EventArgs e)
        {
            string name = txtHoTenNV.Text;
            string tk = txtMaNV.Text;
            string mk = txtPasswordNV.Text;

            if (AccountDAO.Instance.updateTaiKhoan(name, tk, mk))
            {
                MessageBox.Show("Sửa tài khoản thành công!");
                loadListTaiKhoan();
            }
            else
                MessageBox.Show("Sửa tài khoản thất bại!");
        }

        private void btnXoaTK_Click(object sender, EventArgs e)
        {
            string tk = txtMaNV.Text;
            if (AccountDAO.Instance.deleteTaiKhoan(tk))
            {
                MessageBox.Show("Xoá 1 tài khoản thành công!");
                loadListTaiKhoan();
            }
            else
                MessageBox.Show("Xoá tài khoản thất bại!");
        }

        private void btnXoaHD_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtMaHD.Text);
            int tt = Convert.ToInt32(txtThanhToan.Text);
            if (tt == 0)
            {
                MessageBox.Show("Hoá đơn này chưa thanh toán", "Thông báo");
                return;
            }
            if (BillDAO.Instance.deleteHoaDon(id))
            {
                MessageBox.Show("Xoá 1 hoá đơn thành công!");
                loadListHoaDon();
            }
            else
                MessageBox.Show("Xoá hoá đơn thất bại!");
        }

        private void btnThemBan_Click(object sender, EventArgs e)
        {
            string tenBan = txtTenBan.Text;

            if (TableDAO.Instance.insertBan(tenBan))
            {
                MessageBox.Show("Thêm bàn thành công!");
                loadListBan();
            }
            else
                MessageBox.Show("Thêm bàn thất bại!");
        }
    }
}
