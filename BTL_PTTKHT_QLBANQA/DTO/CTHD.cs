using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_PTTKHT_QLBANQA.DTO
{
    public class CTHD
    {
        private int billID;
        private int maDU;
        private int soLuong;

        public CTHD(int billID, int maDU, int soLuong)
        {
            BillID = billID;
            MaDU = maDU;
            SoLuong = soLuong;
        }


        public CTHD(DataRow row) {
            BillID = (int)row["maHD"];
            MaDU = (int)row["maDoUong"];
            SoLuong = (int)row["soLuong"];
        }

        public int BillID { get => billID; set => billID = value; }
        public int MaDU { get => maDU; set => maDU = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
    }
}
