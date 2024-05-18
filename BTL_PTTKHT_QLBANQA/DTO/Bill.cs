using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_PTTKHT_QLBANQA.DTO
{
    public class Bill
    {
        public Bill(int id, DateTime? datecheckin, DateTime? datecheckout, int status, int maban, float tongtien) {
            this.MaHD = id;
            this.VaoBan = datecheckin;
            this.RoiBan = datecheckout;
            this.ThanhToan = status;
            this.MaBan = maban;
            this.TongTien = tongtien;
        }

        public Bill(DataRow row) 
        {
            this.MaHD = (int)row["maHD"];
            this.VaoBan = (DateTime?)row["tGianVao"];
            var tGianRaTemp = row["tGianRa"];
            if(tGianRaTemp.ToString() != "" )
                this.RoiBan = (DateTime?)tGianRaTemp;
            this.ThanhToan = (int)row["thanhToan"];
            this.MaBan = (int)row["maBan"];
            this.TongTien = (float)Convert.ToDouble(row["tongTien"]);
            //this.TongTien = (float)row["tongTien"];
        }

        private int maHD;
        private DateTime? vaoBan;
        private DateTime? roiBan;
        private int thanhToan;
        private int maBan;
        private float tongTien;
        public DateTime? VaoBan { get => vaoBan; set => vaoBan = value; }
        public DateTime? RoiBan { get => roiBan; set => roiBan = value; }
        public int ThanhToan { get => thanhToan; set => thanhToan = value; }
        public int MaHD { get => maHD; set => maHD = value; }
        public int MaBan { get => maBan; set => maBan = value; }
        public float TongTien { get => tongTien; set => tongTien = value; }
    }
}
