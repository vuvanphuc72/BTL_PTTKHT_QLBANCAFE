using BTL_PTTKHT_QLBANQA.DAO;
using BTL_PTTKHT_QLBANQA.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_PTTKHT_QLBANQA
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult ret = MessageBox.Show("Đóng phần mềm!", "Thông báo", MessageBoxButtons.YesNo);
            if(ret == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string passWord = txtPassword.Text;
            if (login(userName, passWord))
            {
                QLPHANMEM f = new QLPHANMEM();
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            else
            {
                DialogResult ret = MessageBox.Show("Sai thông tin tài khoản hoặc mật khẩu!", "Thông báo");
            }

        }

        bool login(string username, string password)
        {
            return AccountDAO.Instance.login(username, password);
        }
    }
}

