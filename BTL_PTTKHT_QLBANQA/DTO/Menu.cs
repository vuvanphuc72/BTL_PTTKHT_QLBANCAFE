using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_PTTKHT_QLBANQA.DTO
{
    public class MMenu
    {
        private string tenDU;
        private int soLuong;
        private float giaTien;
        private float tongTien;


        public string TenDU { get => tenDU; set => tenDU = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public float GiaTien { get => giaTien; set => giaTien = value; }
        public float TongTien { get => tongTien; set => tongTien = value; }

        public MMenu(string tenDU, int soLuong, float giaTien, float tongTien = 0)
        {
            TenDU = tenDU;
            SoLuong = soLuong;
            GiaTien = giaTien;
            TongTien = tongTien;
        }

        public MMenu(DataRow row)
        {
            TenDU = row["tenDoUong"].ToString();
            SoLuong = (int)row["soLuong"];
            GiaTien = (float)Convert.ToDouble(row["gia"]);
            TongTien = (float)Convert.ToDouble(row["tongGia"]);
        }
    }
}
