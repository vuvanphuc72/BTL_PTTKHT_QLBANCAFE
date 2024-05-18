using BTL_PTTKHT_QLBANQA.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_PTTKHT_QLBANQA.DAO
{
    public class DoUongDAO
    {
        private static DoUongDAO instance;

        public static DoUongDAO Instance 
        {
            get { if (instance == null) return instance = new DoUongDAO(); return DoUongDAO.instance; }
            private set { DoUongDAO.instance = value; }
        }

        private DoUongDAO() { }

        public List<DoUongDTO> GetDoUong(int id)
        {
            List<DoUongDTO> listMenu = new List<DoUongDTO>();

            string query = "select * from dbo.DoUong where maDM = " + id;
            DataTable data = DataProvider.Instance.executeQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DoUongDTO du = new DoUongDTO(item);
                listMenu.Add(du);
            }
            return listMenu;
        }

        public List<DoUongDTO> getListDoUong()
        {
            List<DoUongDTO> listMenu = new List<DoUongDTO>();

            string query = "select * from DoUong";
            //string query = "select maDoUong as [Mã đồ uống], tenDoUong as [Tên đồ uống], maDM as [Mã danh mục], gia as [Giá] from DoUong";
            DataTable data = DataProvider.Instance.executeQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DoUongDTO du = new DoUongDTO(item);
                listMenu.Add(du);
            }
            return listMenu;
        }

        public bool insertDoUong(string name, int id, float price)
        {
            string query = string.Format("insert into dbo.DoUong (tenDoUong, maDM, gia) values (N'{0}', {1},  {2})", name, id, price);
            int result = DataProvider.Instance.executeNonQuery(query);
            return result > 0;
        }

        public bool updateDoUong(int idDU, string name, int id, float price)
        {
            string query = string.Format("update DoUong set tenDoUong = N'{0}', maDM = {1}, gia = {2} where maDoUong = {3}", name, id, price, idDU);
            int result = DataProvider.Instance.executeNonQuery(query);
            return result > 0;
        }

        public bool deleteDoUong(int idDU)
        {
            BillInforDAO.Instance.deleteCTHDbyIDDU(idDU);
            string query = "delete DoUong where maDoUong = " + idDU;
            int result = DataProvider.Instance.executeNonQuery(query);
            return result > 0;
        }

    }
}
