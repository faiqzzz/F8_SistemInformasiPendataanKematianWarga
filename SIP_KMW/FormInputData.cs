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
        // 1. Inisialisasi Class Koneksi & Variabel Global
        Koneksi konn = new Koneksi();
        DataTable dt = new DataTable();
        BindingSource bs = new BindingSource();

        public FormInputData()
        {
            InitializeComponent();
        }

        private void FormInputData_Load(object sender, EventArgs e)
        {
            TampilkanData();
            TampilkanPenyebab();

            // Proteksi Role
            if (Session.Role == "Petugas")
            {
                btnHapus.Enabled = false;
                btnCetak.Visible = false;
            }
        }

        // --- POIN 2 & 4 & 5: View, Binding, & Binding Navigator ---
        void TampilkanData()
        {
            // Menggunakan VIEW (v_DataKematianLengkap)
            dt = konn.GetData("SELECT * FROM v_DataKematianLengkap");
            bs.DataSource = dt;
            dgvData.DataSource = bs;

            // Menghubungkan Navigator
            if (bindingNavigator1 != null)
            {
                bindingNavigator1.BindingSource = bs;
            }
        }

        // --- POIN 1: INSERT menggunakan STORED PROCEDURE ---
        private void btnSimpan_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_InsertDataKematian", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@nik", txtNIK.Text);
                    cmd.Parameters.AddWithValue("@nama", txtNama.Text);
                    cmd.Parameters.AddWithValue("@jk", rbLaki.Checked ? "Laki-laki" : "Perempuan");
                    cmd.Parameters.AddWithValue("@tglL", dtpLahir.Value.Date);
                    cmd.Parameters.AddWithValue("@tglW", dtpWafat.Value.Date);
                    cmd.Parameters.AddWithValue("@usia", int.Parse(txtUsia.Text));
                    cmd.Parameters.AddWithValue("@sebab", cbPenyebab.Text);
                    cmd.Parameters.AddWithValue("@alamat", txtAlamat.Text);
                    cmd.Parameters.AddWithValue("@userid", 7); // Default Admin ID

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Berhasil Disimpan!", "Sukses");
                    TampilkanData();
                    BersihkanLabel();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error SQL: " + ex.Message);
                }
            }
        }

        // --- POIN 1: UPDATE menggunakan STORED PROCEDURE ---
        private void btnUbah_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_UpdateDataKematian", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@nik", txtNIK.Text);
                    cmd.Parameters.AddWithValue("@nama", txtNama.Text);
                    cmd.Parameters.AddWithValue("@jk", rbLaki.Checked ? "Laki-laki" : "Perempuan");
                    cmd.Parameters.AddWithValue("@tglL", dtpLahir.Value.Date);
                    cmd.Parameters.AddWithValue("@tglW", dtpWafat.Value.Date);
                    cmd.Parameters.AddWithValue("@usia", int.Parse(txtUsia.Text));
                    cmd.Parameters.AddWithValue("@sebab", cbPenyebab.Text);
                    cmd.Parameters.AddWithValue("@alamat", txtAlamat.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Berhasil Diperbarui!", "Sukses");
                    TampilkanData();
                }
                catch (Exception ex) { MessageBox.Show("Error Update: " + ex.Message); }
            }
        }

        // --- POIN 1: DELETE menggunakan STORED PROCEDURE ---
        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNIK.Text)) return;

            if (MessageBox.Show("Hapus data " + txtNama.Text + "?", "Konfirmasi",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection conn = konn.GetConn())
                {
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("sp_DeleteDataKematian", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@nik", txtNIK.Text);
                        cmd.Parameters.AddWithValue("@UserID", 7);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data berhasil dihapus.");
                        TampilkanData();
                        BersihkanLabel();
                    }
                    catch (Exception ex) { MessageBox.Show("Error Hapus: " + ex.Message); }
                }
            }
        }

        // --- POIN 3: SQL INJECTION di Fitur Pencarian ---
        // Penjelasan: Menggunakan string concatenation tanpa parameter agar rentan
        private void txtCari_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_CariDataKematian", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@keyword", txtCari.Text);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dtSearch = new DataTable();
                    da.Fill(dtSearch);
                    bs.DataSource = dtSearch; // Tetap pakai BindingSource (Poin 4)
                }
                catch (Exception ex) { /* Debug */ }
            }
        }

        // --- SELEKSI DATA Grid ---
        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvData.Rows[e.RowIndex];
                txtNIK.Text = row.Cells["NIK"].Value.ToString();
                txtNama.Text = row.Cells["NamaAlmarhum"].Value.ToString();
                txtAlamat.Text = row.Cells["Alamat"].Value.ToString();
                txtUsia.Text = row.Cells["Usia"].Value.ToString();
                cbPenyebab.Text = row.Cells["Penyebab"].Value.ToString();
                dtpLahir.Value = Convert.ToDateTime(row.Cells["TanggalLahir"].Value);
                dtpWafat.Value = Convert.ToDateTime(row.Cells["TanggalWafat"].Value);

                string jk = row.Cells["JenisKelamin"].Value.ToString();
                if (jk == "Laki-laki") rbLaki.Checked = true;
                else rbPerempuan.Checked = true;
            }
        }

        // --- LOGIKA FORM & VALIDASI ---
        void BersihkanLabel()
        {
            txtNIK.Clear();
            txtNama.Clear();
            txtAlamat.Clear();
            txtUsia.Clear();
            txtNIK.Focus();
        }

        void UpdateOtomatisUsia()
        {
            int usia = dtpWafat.Value.Year - dtpLahir.Value.Year;
            if (dtpWafat.Value < dtpLahir.Value.AddYears(usia)) usia--;
            if (usia < 0) usia = 0;
            txtUsia.Text = usia.ToString();
        }

        private void dtpWafat_ValueChanged(object sender, EventArgs e) => UpdateOtomatisUsia();
        private void dtpLahir_ValueChanged(object sender, EventArgs e) => UpdateOtomatisUsia();

        private void txtNIK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true;
        }

        private void txtNama_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar)) e.Handled = true;
        }

        void TampilkanPenyebab()
        {
            try
            {
                DataTable dtPenyebab = konn.GetData("SELECT NamaPenyebab FROM MasterPenyebab");
                cbPenyebab.Items.Clear();
                foreach (DataRow dr in dtPenyebab.Rows)
                {
                    cbPenyebab.Items.Add(dr["NamaPenyebab"].ToString());
                }
            }
            catch { /* Ignored */ }
        }

        private void btnTampilkan_Click(object sender, EventArgs e)
        {
            txtCari.Clear();
            BersihkanLabel();
            TampilkanData();
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            FormMenuUtama utama = new FormMenuUtama();
            utama.Show();
            this.Close();
        }

        private void btnCetak_Click(object sender, EventArgs e)
        {
            // Kode Excel interop kamu (Sama seperti sebelumnya)
            if (dgvData.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application xcelApp = new Microsoft.Office.Interop.Excel.Application();
                xcelApp.Application.Workbooks.Add(Type.Missing);
                for (int i = 1; i < dgvData.Columns.Count + 1; i++) xcelApp.Cells[1, i] = dgvData.Columns[i - 1].HeaderText;
                for (int i = 0; i < dgvData.Rows.Count; i++)
                {
                    if (dgvData.Rows[i].IsNewRow) continue;
                    for (int j = 0; j < dgvData.Columns.Count; j++)
                    {
                        var cellValue = dgvData.Rows[i].Cells[j].Value;
                        xcelApp.Cells[i + 2, j + 1] = cellValue != null ? cellValue.ToString() : "";
                    }
                }
                xcelApp.Columns.AutoFit();
                xcelApp.Visible = true;
            }
        }
    }
}