using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTL_PTTKHT_QLBANQA.DTO;

namespace BTL_PTTKHT_QLBANQA.DAO
{
    public class TableDAO
    {
        private static TableDAO instance;

        public static TableDAO Instance 
        {
            get{ if (instance == null) instance = new TableDAO();  return TableDAO.instance; }
            private set { TableDAO.instance = value; }
        }

        private TableDAO() { }

        public static int dai = 200;
        public static int rong = 200;

        public List<Table> LoadTableList()
        {
            {
                List<Table> tablelist = new List<Table>();
                DataTable data = DataProvider.Instance.executeQuery("USP_GetTableList");
                foreach (DataRow row in data.Rows)
                {
                    Table tb = new Table(row);
                    tablelist.Add(tb);
                }
                return tablelist;
            }
        }

        public List<Table> getListBan()
        {
            List<Table> listMenu = new List<Table>();

            string query = "select * from Ban";
            DataTable data = DataProvider.Instance.executeQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Table ban = new Table(item);
                listMenu.Add(ban);
            }
            return listMenu;
        }

        public bool insertBan(string name)
        {
            string query = string.Format("insert into dbo.Ban (tenBan) values (N'{0}')", name);
            int result = DataProvider.Instance.executeNonQuery(query);
            return result > 0;
        }
    }
}
