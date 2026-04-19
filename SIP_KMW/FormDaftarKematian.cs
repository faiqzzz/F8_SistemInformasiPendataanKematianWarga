using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; // Pastikan ini ada

namespace SIP_KMW
{
    public partial class FormDaftarKematian : Form
    {

        string alamatDatabase = @"Data Source=DESKTOP-DDDRHRS\RIDHOFAIQAHMAD;Initial Catalog=DB_SIPKMW;Integrated Security=True";

        public FormDaftarKematian()
        {
            InitializeComponent();
        }

        // Fungsi untuk menarik data dari SQL
        public void TampilData()
        {
            // Kita pakai alamatDatabase yang sudah kamu tulis di paling atas (Class Level)
            using (SqlConnection conn = new SqlConnection(alamatDatabase))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Tabel_Kematian";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    // Bersihkan DataGridView sebelum isi ulang
                    dgvKematian.DataSource = null;
                    dgvKematian.Columns.Clear();

                    // Isi data
                    dgvKematian.DataSource = dt;

                    // JANGAN tulis manual HeaderText kalau belum yakin nama kolomnya. 
                    // Biarkan muncul otomatis dulu biar kita tau datanya masuk atau tidak.
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal nampilin data: " + ex.Message);
                }
            }
        }

        private void FormDaftarKematian_Load(object sender, EventArgs e)
        {
            TampilData();

            if (GlobalData.Role == "Administrator")
            {
                btnExport.Visible = true;
                btnDelete.Visible = true; // Admin bisa hapus
                cbStatus.Enabled = true;   // Admin bisa verifikasi
            }
            else
            {
                btnExport.Visible = false;
                btnDelete.Visible = false; // Petugas dilarang hapus data
                cbStatus.Enabled = false;  // Petugas tidak bisa verifikasi
            }
        }

        
    }
}