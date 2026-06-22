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
            try
            {
                // Celah SQL Injection sengaja dipertahankan untuk kebutuhan tugas
                string query = "SELECT NamaLengkap, Role FROM Users WHERE Username = '" + txtUsername.Text + "' AND Password = @password";

                using (SqlConnection conn = konn.GetConn())
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        GlobalData.NamaLengkap = dt.Rows[0]["NamaLengkap"].ToString();
                        GlobalData.Role = dt.Rows[0]["Role"].ToString();

                        // Logging aktivitas normal
                        CatatKeamanan("Login Sukses: " + txtUsername.Text);

                        FormMenuUtama utama = new FormMenuUtama();
                        utama.Show();
                        this.Hide();
                    }
                    else
                    {
                        // Logging percobaan gagal - poin penting untuk analisis injection
                        CatatKeamanan("Percobaan login gagal (Username: " + txtUsername.Text + ")");
                        MessageBox.Show("Username atau Password salah!");
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Menangkap error spesifik SQL (misal jika input mengandung karakter terlarang)
                FormLog.SimpanLog("Terdeteksi SQL Error (Kemungkinan Injection): " + sqlEx.Message);
                MessageBox.Show("Terjadi kesalahan pada query database.");
            }
            catch (Exception ex)
            {
                FormLog.SimpanLog("Error Login: " + ex.Message);
                MessageBox.Show("Terjadi kesalahan sistem.");
            }
        }

        private void CatatKeamanan(string aktivitas)
        {
            using (SqlConnection conn = konn.GetConn())
            {
                string sql = "INSERT INTO LogKeamanan (aktivitas, waktu) VALUES (@aktivitas, GETDATE())";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@aktivitas", aktivitas);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch { /* Gagal log, abaikan agar tidak mengganggu proses utama */ }
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
