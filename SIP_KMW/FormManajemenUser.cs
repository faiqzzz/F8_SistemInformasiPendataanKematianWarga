using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIP_KMW
{
    public partial class FormManajemenUser : Form
    {
        public FormManajemenUser()
        {
            InitializeComponent();
        }

        public void TampilDataUser()
        {
            // Pastikan alamatDatabase sudah dideklarasikan di paling atas form ini juga
            string alamatDatabase = "Data Source=DESKTOP-DDDRHRS\\RIDHOFAIQAHMAD;Initial Catalog=DB_SIPKMW;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(alamatDatabase))
            {
                try
                {
                    conn.Open();
                    // Kita ambil data dari tabel Users yang kamu punya
                    string query = "SELECT Username, Role FROM Users";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Karena kamu bilang gak pake dgv, bagian ini bisa kamu hapus 
                    // ATAU kalau kamu berubah pikiran dan pasang dgvUser, baris ini yang ngisi datanya:
                    // dgvUser.DataSource = dt; 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal refresh data: " + ex.Message);
                }
            }
        }

        
    }
}
