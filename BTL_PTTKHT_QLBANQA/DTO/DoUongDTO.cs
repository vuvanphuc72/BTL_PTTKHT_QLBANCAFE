using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_PTTKHT_QLBANQA.DTO
{
    public class DoUongDTO
    {

        public DoUongDTO(int maDu, string tenDu, int maDm, float gia)
        {
            this.MaDu = maDu;
            this.TenDu = tenDu;
            this.MaDm = maDm;
            this.Gia = gia;
        }

        public DoUongDTO(DataRow row)
        {
            this.MaDu = (int)row["maDoUong"];
            this.TenDu = row["tenDoUong"].ToString();
            this.MaDm = (int)row["maDM"];
            this.Gia = (float)Convert.ToDouble(row["gia"].ToString());
        }

        private int maDu;
        private string tenDu;
        private int maDm;
        private float gia;

        public int MaDu { get => maDu; set => maDu = value; }
        public string TenDu { get => tenDu; set => tenDu = value; }
        public int MaDm { get => maDm; set => maDm = value; }
        public float Gia { get => gia; set => gia = value; }
    }
}
