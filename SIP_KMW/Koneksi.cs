using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIP_KMW
{
    // Class untuk mengatur koneksi ke Database
    class Koneksi
    {
        public SqlConnection GetConn()
        {
            // String koneksi kamu sudah benar mengarah ke PC Ridho
            string stringKoneksi = @"Data Source=DESKTOP-DDDRHRS\RIDHOFAIQAHMAD;Initial Catalog=DB_SIPKMW;Integrated Security=True";
            SqlConnection conn = new SqlConnection(stringKoneksi);
            return conn;
        }

        public DataTable GetData(string query)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = GetConn(); // Pastikan namanya GetConn atau sesuai fungsi koneksimu
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
            return dt;
        }


    }


}