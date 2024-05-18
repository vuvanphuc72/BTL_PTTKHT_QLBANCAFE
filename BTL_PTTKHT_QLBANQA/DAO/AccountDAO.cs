using BTL_PTTKHT_QLBANQA.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_PTTKHT_QLBANQA.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }

        private AccountDAO() { }
        public bool login(string username, string password)
        {
            string query = "select * from dbo.NhanVien where maNV = N'" + username + "' and matKhau = N'" + password + "'";
            DataTable result = DataProvider.Instance.executeQuery(query);
            if (result.Rows.Count > 0) { return true; }
            return false;
        }

        public Account getAccountByUserName(string username)
        {
            DataTable data =  DataProvider.Instance.executeQuery("select * from NhanVien where maNV = '" + username+ "'");
            foreach(DataRow item in data.Rows)
            {
                return new Account(item);
            }
            return null;
        }

        public List<Account> getListAccount()
        {
            List<Account> listA = new List<Account>();

            string query = "select * from NhanVien";
            DataTable data = DataProvider.Instance.executeQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Account tk = new Account(item);
                listA.Add(tk);
            }
            return listA;
        }

        public bool insertTaiKhoan(string ten, string tk, string mk)
        {
            string query = string.Format("insert into dbo.NhanVien (maNV, tenNV, sdt, ngayNhanViec, matKhau, luong) values (N'{0}', N'{1}', 0, getdate(), N'{2}', 6000000)", tk, ten, mk);
            int result = DataProvider.Instance.executeNonQuery(query);
            return result > 0;
        }

        public bool updateTaiKhoan(string ten, string tk, string mk)
        {
            string query = string.Format("update NhanVien set tenNV = N'{0}', matKhau = N'{1}' where  maNV = N'{2}'", ten, mk, tk);
            int result = DataProvider.Instance.executeNonQuery(query);
            return result > 0;
        }

        public bool deleteTaiKhoan(string tk)
        {
            string query = "delete NhanVien where maNV = N'" + tk + "'";
            int result = DataProvider.Instance.executeNonQuery(query);
            return result > 0;
        }

    }
}
