using BTL_PTTKHT_QLBANQA.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BTL_PTTKHT_QLBANQA.DAO
{
    internal class BillInforDAO
    {
        private static BillInforDAO instance;

        public static BillInforDAO Instance
        {
            get { if (instance == null) return instance = new BillInforDAO(); return BillInforDAO.instance; }
            private set {BillInforDAO.instance = value;}
        }

        public BillInforDAO() { }

        public List<CTHD> getListBillInfor(int id)
        {
            List<CTHD> listCTHD = new List<CTHD>();
            DataTable dataTable = DataProvider.Instance.executeQuery("select * from CTHD where maHD = "+ id);
            foreach (DataRow dr in dataTable.Rows)
            {
                CTHD cthd = new CTHD(dr);
                listCTHD.Add(cthd);
            }
            return listCTHD;
        }

        public void insertBillInfor(int maHD, int maDU, int sl)
        {
            DataProvider.Instance.executeNonQuery("USP_insertBillInfor @maHD , @maDU , @soLuong ", new object[] { maHD, maDU, sl });
        }

        public void deleteCTHDbyIDDU(int idDU)
        {
            DataTable dataTable = DataProvider.Instance.executeQuery("delete CTHD where maDoUong = " + idDU);
        }

        public void deleteCTHDbyHD(int id)
        {
            DataTable dataTable = DataProvider.Instance.executeQuery("delete CTHD where maHD = " + id);
        }
    }
}
