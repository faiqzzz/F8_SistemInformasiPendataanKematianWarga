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
    public partial class FormLog : Form
    {
        Koneksi konn = new Koneksi();

        public FormLog()
        {
            InitializeComponent();
        }

        private void cmbPilihLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tabel = "";
            string kolomID = "";

            if (cmbPilihLog.Text == "Error") { tabel = "LogError"; kolomID = "id_log"; }
            else if (cmbPilihLog.Text == "Aktivitas") { tabel = "LogAktivitas"; kolomID = "LogID"; }
            else if (cmbPilihLog.Text == "Keamanan") { tabel = "LogKeamanan"; kolomID = "id_log"; }

            if (!string.IsNullOrEmpty(tabel))
            {
                string sql = "SELECT * FROM " + tabel + " ORDER BY " + kolomID + " DESC";

                try
                {
                    // Gunakan method GetConn() dari kelas Koneksi
                    using (SqlConnection conn = konn.GetConn())
                    {
                        SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvPilihLog.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat log: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public static void SimpanLog(string pesanError)
        {
            // Karena static, kita buat instance baru di dalam
            Koneksi tempKonn = new Koneksi();
            using (SqlConnection conn = tempKonn.GetConn())
            {
                try
                {
                    conn.Open();
                    string sql = "INSERT INTO LogError (waktu, pesan_error) VALUES (GETDATE(), @pesan)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@pesan", pesanError);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Gagal menyimpan log: " + ex.Message);
                }
            }
        }

        private void TampilkanDataLog()
        {
            string tabel = "";
            string kolomID = "";

            if (cmbPilihLog.Text == "Error") { tabel = "LogError"; kolomID = "id_log"; }
            else if (cmbPilihLog.Text == "Aktivitas") { tabel = "LogAktivitas"; kolomID = "LogID"; }
            else if (cmbPilihLog.Text == "Keamanan") { tabel = "LogKeamanan"; kolomID = "id_log"; }

            if (!string.IsNullOrEmpty(tabel))
            {
                string sql = "SELECT * FROM " + tabel + " ORDER BY " + kolomID + " DESC";

                try
                {
                    // Menggunakan fungsi GetData dari Koneksi.cs yang sudah kamu buat
                    DataTable dt = konn.GetData(sql);
                    dgvPilihLog.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat log: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FormLog_Load(object sender, EventArgs e)
        {
            // Menyiapkan pilihan log ke ComboBox
            if (cmbPilihLog.Items.Count == 0)
            {
                cmbPilihLog.Items.Add("Error");
                cmbPilihLog.Items.Add("Aktivitas");
                cmbPilihLog.Items.Add("Keamanan");
            }
            cmbPilihLog.SelectedIndex = 0;
        }

        private void dgcLog_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
