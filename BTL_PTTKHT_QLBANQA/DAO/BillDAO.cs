using BTL_PTTKHT_QLBANQA.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BTL_PTTKHT_QLBANQA.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return BillDAO.instance; }
            private set { BillDAO.instance = value; }
        }

        private BillDAO() { }

        public int getUnCheckBillIByTableID(int id)
        {
            DataTable data = DataProvider.Instance.executeQuery("select * from dbo.HoaDon where maBan = "+ id +" and thanhToan = 0");
            if (data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.MaHD;
            }
            return -1;
        }

        public void insertBill(int id)
        {
            DataProvider.Instance.executeNonQuery("exec USP_insertBill @maBan", new object[]{id});
        }

        public void CheckOut(int id, float tongTien)
        {
            string query = "update HoaDon set tGianRa = GETDATE(), thanhToan = 1, tongTien = "+ tongTien +" where maHD =" + id;
            DataProvider.Instance.executeNonQuery(query);
        }

        public DataTable GetBillListByDate(DateTime checkIn, DateTime checkOut) 
        {
            return DataProvider.Instance.executeQuery("exec usp_getListBillByDate @checkIn , @checkOut", new Object[] {checkIn, checkOut});
        }

        public int getMaxIDBill()
        {
            try
            {
                return (int)DataProvider.Instance.executeScalar("select  max(maHD) from dbo.HoaDon");
            }
            catch
            {
                return 1;
            }
            
        }

        public List<Bill> getListHoaDon()
        {
            List<Bill> listMenu = new List<Bill>();

            string query = "select * from HoaDon";
            DataTable data = DataProvider.Instance.executeQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Bill hd = new Bill(item);
                listMenu.Add(hd);
            }
            return listMenu;
        }

        public bool deleteHoaDon(int id)
        {
            BillInforDAO.Instance.deleteCTHDbyHD(id); 
            string query = "delete HoaDon where maHD = " + id;
            int result = DataProvider.Instance.executeNonQuery(query);
            return result > 0;
        }
    }
}
