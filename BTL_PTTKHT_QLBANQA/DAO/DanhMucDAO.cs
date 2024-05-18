using BTL_PTTKHT_QLBANQA.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_PTTKHT_QLBANQA.DAO
{
    public class DanhMucDAO
    {
        private static DanhMucDAO instance;

        public static DanhMucDAO Instance 
        {
            get { if (instance == null) return instance = new DanhMucDAO(); return DanhMucDAO.instance; }
            private set { DanhMucDAO.instance = value; }
        }

        private DanhMucDAO() { }

        public List<DanhMucDTO> GetDanhMuc()
        {
            List<DanhMucDTO> listMenu = new List<DanhMucDTO>();

            string query = "select * from dbo.DanhMucDoUong";
            DataTable data = DataProvider.Instance.executeQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DanhMucDTO dm = new DanhMucDTO(item);
                listMenu.Add(dm);
            }
            return listMenu;
        }

        public DanhMucDTO getDMbyID(int id)
        {
            DanhMucDTO dm = null;
            string query = "select * from DanhMucDoUong where maDM = " + id;
            DataTable data = DataProvider.Instance.executeQuery(query);
            foreach (DataRow item in data.Rows)
            {
                dm = new DanhMucDTO(item);
                return dm;
            }
            return dm;
        }

        public bool insertDanhMuc(string name)
        {
            string query = string.Format("insert into dbo.DanhMucDoUong (tenDM) values (N'{0}')", name);
            int result = DataProvider.Instance.executeNonQuery(query);
            return result > 0;
        }

        public bool updateDanhMuc(string name, int id )
        {
            string query = string.Format("update DanhMucDoUong set tenDM = N'{0}' where maDM = {1}", name, id);
            int result = DataProvider.Instance.executeNonQuery(query);
            return result > 0;
        }

        public bool deleteDanhMuc(int id)
        {
            string query = "delete DanhMucDoUong where maDM = " + id;
            int result = DataProvider.Instance.executeNonQuery(query);
            return result > 0;
        }
    }
}
