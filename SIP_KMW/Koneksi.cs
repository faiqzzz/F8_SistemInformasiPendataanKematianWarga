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

        
    }
}