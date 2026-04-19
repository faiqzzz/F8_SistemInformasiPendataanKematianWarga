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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // Panggil lagi fungsi narik data dari SQL
            TampilData();

            // Opsional: Bersihkan kotak pencarian kalau ada isinya
            if (txtCari != null)
            {
                txtCari.Clear();
            }

            MessageBox.Show("Data telah diperbarui!", "Refresh Berhasil");
        }

        private void txtCari_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(alamatDatabase))
            {
                try
                {
                    conn.Open();
                    // Saya tambahkan OR Alamat LIKE @cari supaya wilayah bisa dicari juga
                    string query = "SELECT * FROM Tabel_Kematian WHERE Nama LIKE @cari OR NIK LIKE @cari OR Alamat LIKE @cari";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);

                    da.SelectCommand.Parameters.AddWithValue("@cari", "%" + txtCari.Text + "%");

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvKematian.DataSource = dt;

                    // Tambahkan ini biar lblTotal kamu update otomatis setiap ngetik
                    // Sesuai deskripsi "Pengelolaan laporan dalam bentuk filter dan statistik"
                    lblTotal.Text = "Jumlah Data: " + dt.Rows.Count.ToString() + " Orang";
                }
                catch (Exception ex)
                {
                    // Tetap pakai Console biar nggak pop-up terus pas lagi ngetik
                    Console.WriteLine("Error Cari: " + ex.Message);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DialogResult konfirmasi = MessageBox.Show("Apakah Anda yakin ingin memperbarui data ini?",
                              "Konfirmasi Perubahan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (konfirmasi == DialogResult.No)

                if (dgvKematian.CurrentRow == null)
            {
                MessageBox.Show("Pilih dulu data di tabel yang mau di-update!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(alamatDatabase))
            {
                try
                {
                    conn.Open();

                    // 1. Pastikan kolom 'Umur' dan 'Penyebab' sudah ada di Query
                    string query = "UPDATE Tabel_Kematian SET Nama=@nama, Jenis_Kelamin=@jk, Tanggal_Lahir=@tglLahir, " +
                                    "Tanggal_Wafat=@tglWafat, Umur=@umur, Penyebab=@penyebab, Alamat=@alamat, Status=@status " +
                                    "WHERE NIK=@nik";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    // 2. Ambil angka umur dengan hati-hati
                    int umurTerbaru;
                    if (!int.TryParse(txtUmur.Text, out umurTerbaru))
                    {
                        MessageBox.Show("Isi umur harus angka!");
                        return;
                    }

                    cmd.Parameters.AddWithValue("@jk", cbJenisKelamin.Text);
                    cmd.Parameters.AddWithValue("@alamat", txtAlamat.Text);
                    cmd.Parameters.AddWithValue("@status", cbStatus.Text);
                    cmd.Parameters.AddWithValue("@nik", txtNik.Text);
                    cmd.Parameters.AddWithValue("@nama", txtNama.Text);
                    cmd.Parameters.AddWithValue("@tglLahir", dtpLahir.Value);
                    cmd.Parameters.AddWithValue("@tglWafat", dtpWafat.Value);
                    cmd.Parameters.AddWithValue("@umur", umurTerbaru); // Kirim angka umur terbaru
                    cmd.Parameters.AddWithValue("@penyebab", cbPenyebab.Text); // Update juga penyebabnya

                    int barisTerubah = cmd.ExecuteNonQuery();

                    if (barisTerubah > 0)
                    {
                        MessageBox.Show("Data Berhasil Diperbarui!");
                    }
                    else
                    {
                        MessageBox.Show("Data gagal diubah. Pastikan NIK tidak diotak-atik!");
                    }

                    TampilData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error pas update: " + ex.Message);
                }
            }
        }


        private void dgvKematian_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Supaya kalau klik header tabel nggak error
            if (e.RowIndex >= 0)
            {
                // Mengambil baris yang diklik
                DataGridViewRow row = dgvKematian.Rows[e.RowIndex];

                // Pindahkan data dari tabel ke TextBox yang sudah kamu buat/paste tadi
                cbJenisKelamin.Text = row.Cells["Jenis_Kelamin"].Value.ToString();
                txtAlamat.Text = row.Cells["Alamat"].Value.ToString();
                cbStatus.Text = row.Cells["Status"].Value.ToString();
                txtNik.Text = row.Cells["NIK"].Value.ToString();
                txtNama.Text = row.Cells["Nama"].Value.ToString();
                dtpLahir.Value = Convert.ToDateTime(row.Cells["Tanggal_Lahir"].Value);
                dtpWafat.Value = Convert.ToDateTime(row.Cells["Tanggal_Wafat"].Value);
                txtUmur.Text = row.Cells["Umur"].Value.ToString();

                // Kunci NIK-nya supaya nggak bisa diubah (karena itu kunci utama di database)
                txtNik.ReadOnly = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // 1. Validasi: Pastikan ada NIK yang dipilih dari tabel
            if (string.IsNullOrEmpty(txtNik.Text))
            {
                MessageBox.Show("Klik dulu baris di tabel yang mau dihapus!", "Peringatan");
                return;
            }

            // 2. Konfirmasi: Biar gak asal hapus
            DialogResult konfirmasi = MessageBox.Show("Yakin ingin menghapus data dengan NIK: " + txtNik.Text + "?",
                                                     "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (konfirmasi == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(alamatDatabase))
                {
                    try
                    {
                        conn.Open();
                        // 3. Query Delete
                        string query = "DELETE FROM Tabel_Kematian WHERE NIK=@nik";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@nik", txtNik.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data Berhasil Dihapus!", "Sukses");

                        // 4. Refresh otomatis & bersih-bersih
                        TampilData();
                        txtNik.Clear();
                        txtNama.Clear();
                        txtUmur.Clear();
                        cbPenyebab.SelectedIndex = -1;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal hapus data: " + ex.Message);
                    }
                }
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(alamatDatabase))
            {
                try
                {
                    conn.Open();
                    // Query ini fleksibel: Cari Nama tapi juga saring per Kategori
                    string query = @"SELECT * FROM Tabel_Kematian 
                             WHERE Nama LIKE @nama 
                             AND (Penyebab = @penyebab OR @penyebab = '-- Semua Kategori --')";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nama", "%" + txtCari.Text + "%");

                    // Jika user tidak milih kategori, tampilkan semua
                    string kategori = string.IsNullOrEmpty(cbFilterPenyebab.Text) ? "-- Semua Kategori --" : cbFilterPenyebab.Text;
                    cmd.Parameters.AddWithValue("@penyebab", kategori);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvKematian.DataSource = dt;

                    // FITUR STATISTIK (Sesuai deskripsi Admin)
                    lblTotal.Text = "Jumlah Data: " + dt.Rows.Count.ToString() + " Orang";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Filter Gagal: " + ex.Message);
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                dgvKematian.SelectAll();
                DataObject dataObj = dgvKematian.GetClipboardContent();
                if (dataObj != null)
                    Clipboard.SetDataObject(dataObj);

                MessageBox.Show("Data tabel sudah disalin (Copy). Silakan buka Excel dan tekan Paste (Ctrl+V).", "Ekspor Berhasil");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal ekspor: " + ex.Message);
            }
        }

        private void HitungUmurOtomatis()
        {
            try
            {
                DateTime lahir = dtpLahir.Value;
                DateTime wafat = dtpWafat.Value;
                int umur = wafat.Year - lahir.Year;
                if (wafat < lahir.AddYears(umur)) umur--;
                if (umur < 0) umur = 0;
                txtUmur.Text = umur.ToString();
            }
            catch { }
        }

        private void dgvKematian_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtUmur_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpLahir_ValueChanged(object sender, EventArgs e)
        {
            HitungUmurOtomatis();
        }

        private void dtpWafat_ValueChanged(object sender, EventArgs e)
        {
            HitungUmurOtomatis();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}