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
        public static string GetServerIP()
        {
            // Ganti ke "." saat coding di laptop sendiri,
            // Ganti ke "192.168.1.5" saat akan di-deploy ke teman
            return @".\RIDHOFAIQAHMAD";
        }

        public SqlConnection GetConn()
        {
            string ip = "192.168.1.5";
            // Pastikan Initial Catalog sama persis dengan nama database di SSMS
            string connString = $"Data Source={ip};Initial Catalog=DB_SIPKMW;User ID=sa;Password=Password123;TrustServerCertificate=True;Connect Timeout=15;";

            return new SqlConnection(connString);
        }

        public DataTable GetData(string query)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = GetConn())
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal membuka database: " + ex.Message, "Error Koneksi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return dt;
        }


    }


}