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
        // Masukkan Connection String kamu di sini sekali saja
        // Ganti NAMA-LAPTOP-KAMU sesuai yang ada di SSMS atau Server Explorer
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

                    if (rd.Read()) // Kalau user ditemukan di database
                    {
                        // 1. Simpan role ke GlobalData supaya Form lain (Daftar Data) bisa tau siapa yang login
                        // Pastikan nama kolom di database kamu adalah "Role"
                        string roleValue = rd["Role"].ToString();
                        GlobalData.Role = roleValue;

                        MessageBox.Show("Selamat Datang, " + roleValue);

                        // 2. Panggil Form2 (Menu Utama kamu). 
                        // Kita pakai Form2() saja, tidak perlu (role) di dalam kurung
                        // Karena datanya sudah aman di GlobalData
                        Form2 menu = new Form2(roleValue);
                        menu.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Username atau Password salah!", "Gagal");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ada error koneksi: " + ex.Message);
                }
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            prosesLogin();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        // Nanti setelah kamu buat tombol di Design, 
        // klik dua kali tombolnya, lalu panggil fungsi: prosesLogin();
    }
}
