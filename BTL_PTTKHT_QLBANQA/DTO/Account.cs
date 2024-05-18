using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_PTTKHT_QLBANQA.DTO
{
    public class Account
    {
        public Account(string tk, string mk, string ht) 
        { 
            this.UserName = tk;
            this.Password = mk;
            this.HoTen = ht;
        }

        public Account(DataRow row)
        {
            this.UserName = row["maNV"].ToString();
            this.Password = row["matKhau"].ToString();
            this.HoTen = row["tenNV"].ToString();
        }


        private string userName;
        private string password;
        private string hoTen;

        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public string HoTen { get => hoTen; set => hoTen = value; }
    }
}
