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
    public partial class FormLogin : Form
    {
        // Tetap panggil class Koneksi untuk keperluan login nanti.
        Koneksi konn = new Koneksi();

        public FormLogin()
        {
            InitializeComponent();
        }

        // --- BAGIAN FormLogin_Load SUDAH DIHAPUS ---

        private void btnLogin_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    conn.Open();

                    // --- POIN 3: SQL INJECTION (VULNERABLE CODE) ---
                    // Kita pakai teknik tambah string (+) secara langsung. 
                    // Ini berbahaya karena user bisa masukin tanda kutip (') buat manipulasi query.
                    string query = "SELECT * FROM Users WHERE Username = '" + txtUsername.Text + "' AND Password = '" + txtPassword.Text + "'";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Login Berhasil!");

                        // Simpan session (opsional).
                        Session.NamaLengkap = dt.Rows[0]["NamaLengkap"].ToString();
                        Session.Role = dt.Rows[0]["Role"].ToString();

                        FormMenuUtama utama = new FormMenuUtama();
                        utama.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Username atau Password salah!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
