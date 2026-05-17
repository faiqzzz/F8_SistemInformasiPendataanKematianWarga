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
    public partial class FormMenuUtama : Form
    {
        // Panggil class koneksi
        Koneksi konn = new Koneksi();

        public FormMenuUtama()
        {
            InitializeComponent();
        }

        // Event Activated: Supaya kalau kamu habis input data dan balik ke menu, angkanya update otomatis
        private void FormMenuUtama_Activated(object sender, EventArgs e)
        {
            TampilkanStatistik();
        }

        private void FormMenuUtama_Load(object sender, EventArgs e)
        {
            // Menampilkan info Session
            lblWelcome.Text = "Halo, " + Session.NamaLengkap;
            lblRole.Text = "Akses: " + Session.Role;

            // Memanggil angka statistik
            TampilkanStatistik();

            // Proteksi Fitur: Hanya SuperAdmin yang bisa lihat tombol Manajemen User
            btnManajemenUser.Visible = (Session.Role == "SuperAdmin");

            btnBukaInput.Visible = true;
        }

        // POIN 2: Implementasi ExecuteScalar dengan VIEW
        void TampilkanStatistik()
        {
            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    conn.Open();
                    // Pakai VIEW (v_DataKematianLengkap) agar poin 2 terpenuhi di semua form
                    string sql = "SELECT COUNT(*) FROM v_DataKematianLengkap";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    // ExecuteScalar digunakan untuk mengambil 1 nilai tunggal (jumlah data)
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        lblTotalData.Text = "TOTAL WARGA TERDATA MENINGGAL : " + result.ToString();
                    }
                }
                catch (Exception ex)
                {
                    // Minimal tampilkan di output debug jika ada error koneksi
                    System.Diagnostics.Debug.WriteLine("Error Statistik: " + ex.Message);
                }
            }
        }

        private void btnBukaInput_Click(object sender, EventArgs e)
        {
            FormInputData frmInput = new FormInputData();
            frmInput.Show();
            this.Hide();
        }

        private void btnManajemenUser_Click(object sender, EventArgs e)
        {
            FormManajemenUser mng = new FormManajemenUser();
            mng.ShowDialog(); // ShowDialog agar user fokus selesaikan manajemen user dulu
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Bagian F: Konfirmasi Logout (Lebih User Friendly)
            DialogResult dialog = MessageBox.Show("Apakah Anda yakin ingin Logout?", "Konfirmasi",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialog == DialogResult.Yes)
            {
                FormLogin login = new FormLogin();
                login.Show();
                this.Close();
            }
        }

        // Hapus event kosong atau double yang tidak terpakai
        private void lblTotalData_Click(object sender, EventArgs e) { }
    }
}