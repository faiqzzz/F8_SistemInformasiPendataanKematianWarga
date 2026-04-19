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
    public partial class FormInputData : Form
    {
        public FormInputData()
        {
            InitializeComponent();
        }

        private void dtpWafat_ValueChanged(object sender, EventArgs e)
        {
            HitungUmurOtomatis();
        }

        private void HitungUmurOtomatis()
        {
            try
            {
                DateTime lahir = dtpLahir.Value;
                DateTime wafat = dtpWafat.Value;

                // Rumus: Tahun Wafat - Tahun Lahir
                int umur = wafat.Year - lahir.Year;

                // Koreksi: Jika belum sampai tanggal ulang tahunnya di tahun wafat tersebut, umur kurangi 1
                if (wafat < lahir.AddYears(umur))
                {
                    umur--;
                }

                // Pastikan umur tidak negatif (antisipasi salah pilih tanggal)
                if (umur < 0) umur = 0;

                txtUmur.Text = umur.ToString();
            }
            catch
            {
                // Biarkan kosong agar tidak ganggu pas ganti tanggal
            }
        }

        private void FormInputData_Load(object sender, EventArgs e)
        {
            // Ini biar pas dibuka, ComboBox-nya otomatis milih pilihan pertama kamu
            // Kalau di Items kamu baris pertamanya "-- Pilih Penyebab --", maka itu yang muncul
            cbPenyebab.SelectedIndex = 0;
        }

        private void dtpLahir_ValueChanged(object sender, EventArgs e)
        {
            HitungUmurOtomatis();
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            string alamatDatabase = "Data Source=DESKTOP-DDDRHRS\\RIDHOFAIQAHMAD;Initial Catalog=DB_SIPKMW;Integrated Security=True";

            // 1. Cek NIK, Nama, dan pastikan ComboBox sudah dipilih
            if (string.IsNullOrEmpty(txtNik.Text) || string.IsNullOrEmpty(txtNama.Text) || cbPenyebab.SelectedIndex == -1)
            {
                MessageBox.Show("NIK, Nama, dan Penyebab harus diisi/dipilih ya!", "Peringatan");
                return;
            }

            using (SqlConnection conn = new SqlConnection(alamatDatabase))
            {
                try
                {
                    conn.Open();
                    // 2. Query SQL ditambah kolom Penyebab
                    string query = "INSERT INTO Tabel_Kematian (NIK, Nama, Jenis_Kelamin, Tanggal_Lahir, Tanggal_Wafat, Umur, Penyebab, Alamat, Status) " +
                                    "VALUES (@nik, @nama, @jk, @tglLahir, @tglWafat, @umur, @penyebab, @alamat, @status)";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    // 3. Masukkan data ke parameter SQL
                    cmd.Parameters.AddWithValue("@jk", cbJenisKelamin.Text); // Tambahan JK
                    cmd.Parameters.AddWithValue("@alamat", txtAlamat.Text);
                    cmd.Parameters.AddWithValue("@status", "Belum Verifikasi"); // Default untuk input baru
                    cmd.Parameters.AddWithValue("@nik", txtNik.Text);
                    cmd.Parameters.AddWithValue("@nama", txtNama.Text);
                    cmd.Parameters.AddWithValue("@tglLahir", dtpLahir.Value);
                    cmd.Parameters.AddWithValue("@tglWafat", dtpWafat.Value);
                    cmd.Parameters.AddWithValue("@umur", int.Parse(txtUmur.Text));
                    cmd.Parameters.AddWithValue("@penyebab", cbPenyebab.Text); // INI DIA COMBOBOX-NYA

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Data Kematian Berhasil Disimpan!", "Sukses");

                    // 4. Kosongkan form setelah simpan
                    txtNik.Clear();
                    txtNama.Clear();
                    txtUmur.Clear();
                    cbPenyebab.SelectedIndex = -1; // ComboBox balik jadi kosong lagi
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Waduh, Error pas simpan: " + ex.Message);
                }
            }
        }

        private void txtNik_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtNik_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Hanya mengizinkan Angka (Digit) dan tombol Backspace (Control)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // "Menolak" input jika bukan angka
            }
        }

        private void txtNama_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUmur_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbPenyebab_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtAlamat_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbJenisKelamin_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
