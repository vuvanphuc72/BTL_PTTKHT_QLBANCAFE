using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_PTTKHT_QLBANQA.DTO
{
    public class DanhMucDTO
    {
        public DanhMucDTO(int id, string tenDM) {
            this.MaDM = id;
            this.TenDm = tenDM;
        }

        public DanhMucDTO(DataRow row) {
            this.MaDM = (int)row["maDM"];
            this.TenDm = row["tenDM"].ToString();
        } 

        private int maDM;
        private string tenDm;

        public int MaDM { get => maDM; set => maDM = value; }
        public string TenDm { get => tenDm; set => tenDm = value; }
    }
}
