using BTL_PTTKHT_QLBANQA.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_PTTKHT_QLBANQA.DAO
{
    public class MenuDAO
    {
        private static MenuDAO instance;

        public static MenuDAO Instance 
        {
            get { if (instance == null) return instance = new MenuDAO(); return MenuDAO.instance; }
            private set { MenuDAO.instance = value; }
        }
        private MenuDAO() { }

        public List<MMenu> GetListMenuByTable(int id)
        {
            List<MMenu> listMenu = new List<MMenu>();

            string query = "select du.tenDoUong, bi.soLuong, du.gia, du.gia*bi.soLuong as tongGia from CTHD as bi, dbo.HoaDon as b, dbo.DoUong as du where bi.maHD = b.maHD and bi.maDoUong = du.maDoUong and b.thanhToan = 0 and b.maBan = " + id;
            DataTable data = DataProvider.Instance.executeQuery(query);
            foreach (DataRow item in data.Rows)
            {
                MMenu menu = new MMenu(item);
                listMenu.Add(menu);
            }
            return listMenu;
        }

    }
}
