using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_PTTKHT_QLBANQA.DTO
{
    public class Table
    {
        private int maBan;
        public int MaBan { get => maBan; set => maBan = value; }
        public string TenBan { get => tenBan; set => tenBan = value; }
        public string TrangThai { get => trangThai; set => trangThai = value; }

        private string tenBan;

        private string trangThai;

        public Table(int id, string name, string status) {
            this.MaBan = id;
            this.TenBan = name;
            this.TrangThai = status;
        }

        public Table(DataRow row) 
        {
            this.MaBan = (int)row["maBan"];
            this.TrangThai = row["trangThai"].ToString();
            this.TenBan = row["tenBan"].ToString();
        }
    }
}
