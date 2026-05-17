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
            if (GlobalData.Role == "Petugas")
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

        
    }
}