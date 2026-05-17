using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

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
            try
            {
                using (SqlConnection conn = GetConn()) // Memastikan pakai koneksi dari class ini
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error GetData: " + ex.Message);
            }
            return dt;
        }
    }

    // Class Session untuk menyimpan data user yang sedang login
    // Static artinya data ini tersimpan di memori selama aplikasi running
    public static class Session
    {
        public static string UserID;
        public static string NamaLengkap;
        public static string Role;
    }
}