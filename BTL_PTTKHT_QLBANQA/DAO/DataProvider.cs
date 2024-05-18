using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_PTTKHT_QLBANQA.DAO
{
    public class DataProvider
    {   
        private static DataProvider instance;
        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
            private set { DataProvider.instance = value; }
        }

        //private string conStr = @"Data Source=LAPTOP-LC0R2A7M\SQLEXPRESS;Initial Catalog=BTL_QuanLyQuanCafe;Integrated Security=True";
        private string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;


        public DataTable executeQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(conStr))
            {
                sqlCon.Open();

                SqlCommand cmd = new SqlCommand(query, sqlCon);

                if(parameter != null )
                {
                    string[] listPar = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPar )
                    {
                        if (item.Contains('@'))
                        {
                            cmd.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }                                                       
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);

                sqlCon.Close();
            }

            return data;

        }

        public int executeNonQuery(string query, object[] parameter = null)
        {
            int data = 0;
            using (SqlConnection sqlCon = new SqlConnection(conStr))
            {
                sqlCon.Open();

                SqlCommand cmd = new SqlCommand(query, sqlCon);

                if (parameter != null)
                {
                    string[] listPar = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPar)
                    {
                        if (item.Contains('@'))
                        {
                            cmd.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                data = cmd.ExecuteNonQuery();

                sqlCon.Close();
            }

            return data;

        }

        public object executeScalar(string query, object[] parameter = null)
        {
            object data = 0;
            using (SqlConnection sqlCon = new SqlConnection(conStr))
            {
                sqlCon.Open();

                SqlCommand cmd = new SqlCommand(query, sqlCon);

                if (parameter != null)
                {
                    string[] listPar = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPar)
                    {
                        if (item.Contains('@'))
                        {
                            cmd.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = cmd.ExecuteScalar();

                sqlCon.Close();
            }

            return data;

        }

    }
}
