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

        private void btnSave_Click(object sender, EventArgs e)
        {

            string alamatDatabase = "Data Source=DESKTOP-DDDRHRS\\RIDHOFAIQAHMAD;Initial Catalog=DB_SIPKMW;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(alamatDatabase))
            {
                try
                {
                    conn.Open();
                    // Sesuaikan dengan nama kolom di tabel 'Users' kamu
                    string query = "INSERT INTO Users (Username, Password, Role) VALUES (@user, @pass, @role)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@user", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@pass", txtPassword.Text); // Untuk tugas, password polos dulu oke lah
                    cmd.Parameters.AddWithValue("@role", cbRole.Text); // ComboBox isi: Administrator / Petugas

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User baru berhasil didaftarkan!");

                    TampilDataUser(); // Refresh tabel user
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal tambah user: " + ex.Message);
                }
            }
        }

        private void FormManajemenUser_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
