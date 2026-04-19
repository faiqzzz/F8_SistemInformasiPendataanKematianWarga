using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; // Ini kunci buat koneksi ke SQL

namespace SIP_KMW
{
    public partial class Form1 : Form
    {
        
        string alamatDatabase = @"Data Source=DESKTOP-DDDRHRS\RIDHOFAIQAHMAD;Initial Catalog=DB_SIPKMW;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Bisa dikosongkan
        }

        // Ini fungsi login yang nanti kita sambungkan ke tombol
        private void prosesLogin()
        {
            using (SqlConnection conn = new SqlConnection(alamatDatabase))
            {
                try
                {
                    conn.Open();
                    // Query untuk cek user
                    string query = "SELECT Role FROM Users WHERE Username=@user AND Password=@pass";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@user", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@pass", txtPassword.Text);

                    SqlDataReader rd = cmd.ExecuteReader();

                }
                }
        }

    }
}
