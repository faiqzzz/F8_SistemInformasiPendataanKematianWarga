using CrystalDecisions.CrystalReports.Engine;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace SIP_KMW
{
    public partial class FormInputData : Form
    {
        string lokasiFotoAwal = "";
        const long MAX_FILE_SIZE_BYTES = 2 * 1024 * 1024;
        DAL dal = new DAL();
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
            if (GlobalData.Role == "Petugas")
            {
                btnHapus.Enabled = false;
                btnCetak.Visible = false;
            }
        }

        // --- POIN 2 & 4 & 5: View, Binding, & Binding Navigator ---
        void TampilkanData()
        {
            using (SqlConnection conn = konn.GetConn())
            {
                SqlDataAdapter da = new SqlDataAdapter("sp_GetAllDataKematian", conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);
                bs.DataSource = dt;
                dgvData.DataSource = bs;
            }
        }


        // --- POIN 1: INSERT menggunakan STORED PROCEDURE ---
        private void btnSimpan_Click(object sender, EventArgs e)
        {
            string detailData = $"NIK: {txtNIK.Text}, Nama: {txtNama.Text}, JK: {(rbLaki.Checked ? "Laki-laki" : "Perempuan")}, Lahir: {dtpLahir.Value.ToShortDateString()}, Wafat: {dtpWafat.Value.ToShortDateString()}, Usia: {txtUsia.Text}";

            if (string.IsNullOrWhiteSpace(txtNIK.Text.Trim()) || txtNIK.Text.Trim().Length != 16)
            {
                SimpanLogError("Validasi Input", detailData, "NIK harus 16 digit");
                MessageBox.Show("NIK harus 16 digit.", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = konn.GetConn())
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                string pathKeDatabase = "";
                string pathTujuan = "";

                try
                {
                    if (!string.IsNullOrEmpty(lokasiFotoAwal))
                    {
                        string folderTarget = Path.Combine(Application.StartupPath, "FotoWarga");
                        if (!Directory.Exists(folderTarget)) Directory.CreateDirectory(folderTarget);
                        pathTujuan = Path.Combine(folderTarget, txtNIK.Text.Trim() + ".jpg");
                        pathKeDatabase = "FotoWarga\\" + txtNIK.Text.Trim() + ".jpg";
                        File.Copy(lokasiFotoAwal, pathTujuan, true);
                    }

                    SqlCommand cmd = new SqlCommand("sp_InsertDataKematian", conn, trans);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nik", txtNIK.Text.Trim());
                    cmd.Parameters.AddWithValue("@nama", txtNama.Text);
                    cmd.Parameters.AddWithValue("@jk", rbLaki.Checked ? "Laki-laki" : "Perempuan");
                    cmd.Parameters.AddWithValue("@tglL", dtpLahir.Value.Date);
                    cmd.Parameters.AddWithValue("@tglW", dtpWafat.Value.Date);
                    cmd.Parameters.AddWithValue("@usia", int.Parse(txtUsia.Text));
                    cmd.Parameters.AddWithValue("@FotoPath", pathKeDatabase);
                    cmd.ExecuteNonQuery();
                    trans.Commit();

                    MessageBox.Show("Data berhasil disimpan.");
                    TampilkanData();
                    BersihkanLabel();
                    pbFoto.Image = null;
                    lokasiFotoAwal = "";
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    if (!string.IsNullOrEmpty(pathTujuan) && File.Exists(pathTujuan)) File.Delete(pathTujuan);
                    SimpanLogError("INSERT Data Kematian", detailData, ex.Message);
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (conn.State == ConnectionState.Closed) conn.Open();
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
            if (MessageBox.Show("Hapus data " + txtNama.Text + "?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection conn = konn.GetConn())
                {
                    try
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
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
                    bs.DataSource = dtSearch;
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            }
        }


        // --- SELEKSI DATA Grid ---
        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvData.Rows[e.RowIndex];
                txtNIK.Text = row.Cells["NIK"].Value.ToString();
                txtNIK.ReadOnly = true;
                txtNIK.BackColor = Color.White;
                txtNIK.ForeColor = Color.Black;
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
            txtNIK.ReadOnly = false;
            txtNIK.Enabled = true;
            txtNama.Clear();
            txtAlamat.Clear();
            txtUsia.Clear();
            txtNIK.Focus();
            rbLaki.Checked = false;
            rbPerempuan.Checked = false;
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
            using (SqlConnection conn = konn.GetConn())
            {
                SqlCommand cmd = new SqlCommand("sp_GetMasterPenyebab", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dtPenyebab = new DataTable();
                da.Fill(dtPenyebab);
                cbPenyebab.Items.Clear();
                foreach (DataRow dr in dtPenyebab.Rows) cbPenyebab.Items.Add(dr["NamaPenyebab"].ToString());
            }
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
            // Cukup buka form-nya saja
            FormLaporan frm = new FormLaporan();
            frm.ShowDialog();
        }

        void InsertDataFromExcel(DataRow row)
        {
            using (SqlConnection conn = konn.GetConn())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_InsertDataKematian", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Sesuaikan dengan nama kolom di Excel kamu
                cmd.Parameters.AddWithValue("@nik", row["NIK"].ToString());
                cmd.Parameters.AddWithValue("@nama", row["NamaAlmarhum"].ToString());
                cmd.Parameters.AddWithValue("@jk", row["JenisKelamin"].ToString());
                cmd.Parameters.AddWithValue("@tglL", Convert.ToDateTime(row["TanggalLahir"]));
                cmd.Parameters.AddWithValue("@tglW", Convert.ToDateTime(row["TanggalWafat"]));
                cmd.Parameters.AddWithValue("@usia", Convert.ToInt32(row["Usia"]));
                cmd.Parameters.AddWithValue("@sebab", row["Penyebab"].ToString());
                cmd.Parameters.AddWithValue("@alamat", row["Alamat"].ToString());
                cmd.Parameters.AddWithValue("@userid", 7); // Default Admin

                cmd.ExecuteNonQuery();
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xlsx;*.xls";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                {
                                    UseHeaderRow = true
                                }
                            });

                            DataTable dtExcel = result.Tables[0];
                            int berhasil = 0;
                            int gagal = 0;

                            foreach (DataRow row in dtExcel.Rows)
                            {
                                // Lewati jika NIK kosong (baris kosong)
                                if (string.IsNullOrWhiteSpace(row[0].ToString())) continue;

                                try
                                {
                                    // 1. Bersihkan NIK (hapus spasi, titik, koma biar gak error)
                                    string nik = row[0].ToString().Replace(" ", "").Replace(".", "").Replace(",", "");

                                    // Validasi NIK (Opsional: hapus if ini jika ingin bypass validasi)
                                    if (nik.Length != 16) { throw new Exception("NIK harus 16 digit!"); }

                                    // 2. Insert Data
                                    dal.InsertData(
                                        nik,
                                        row[1].ToString(), // Nama
                                        row[2].ToString(), // Jenis Kelamin
                                        Convert.ToDateTime(row[3]), // Tanggal Lahir
                                        Convert.ToDateTime(row[4]), // Tanggal Wafat
                                        Convert.ToInt32(row[5]),    // Usia
                                        row[6].ToString(),          // Penyebab
                                        row[7].ToString(),          // Alamat
                                        1
                                    );
                                    berhasil++;
                                }
                                catch (Exception ex)
                                {
                                    // ERROR INI AKAN KASIH TAHU PENYEBABNYA
                                    // Cek pesan ini, kalau "String was not recognized..." berarti tanggal salah!
                                    MessageBox.Show("Error pada baris dengan NIK " + row[0].ToString() + "\nPesan: " + ex.Message);
                                    gagal++;
                                }
                            }

                            MessageBox.Show($"Import selesai!\nBerhasil: {berhasil}\nGagal: {gagal}", "Info");
                            TampilkanData();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Fatal: " + ex.Message);
                }
            }
        }
        private void btnDownloadTemplate_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Excel Files|*.xlsx";
            saveDialog.FileName = "Template_Import.xlsx";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook wb = xlApp.Workbooks.Add();
                Microsoft.Office.Interop.Excel.Worksheet ws = wb.Sheets[1];

                // Header - INI HARUS SAMA PERSIS DENGAN KODE IMPORT NANTI
                string[] headers = { "NIK", "NamaAlmarhum", "JenisKelamin", "TanggalLahir", "TanggalWafat", "Usia", "Penyebab", "Alamat" };

                for (int i = 0; i < headers.Length; i++)
                {
                    ws.Cells[1, i + 1] = headers[i];
                }

                // Simpan
                wb.SaveAs(saveDialog.FileName);
                wb.Close();
                xlApp.Quit();
                MessageBox.Show("Template berhasil disimpan!");
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtNIK.Clear();
            txtNama.Clear();
            txtUsia.Clear();
            cbPenyebab.SelectedIndex = -1;
            txtAlamat.Clear();
            dtpLahir.Value = DateTime.Now;
            dtpWafat.Value = DateTime.Now;
            txtNIK.Focus(); // Fokus kursor balik ke NIK
            string jenisKelamin = "";
            rbLaki.Checked = false;
            rbPerempuan.Checked = false;

            txtNIK.Focus();
        }

        private void btnPilihFoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FileInfo fileInfo = new FileInfo(ofd.FileName);
                if (fileInfo.Length > MAX_FILE_SIZE_BYTES)
                {
                    MessageBox.Show("Ukuran foto melebihi batas 2 MB.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                lokasiFotoAwal = ofd.FileName;
                pbFoto.ImageLocation = lokasiFotoAwal;
                pbFoto.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void SimpanLogError(string aksi, string detailData, string pesanError)
        {
            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    string pesanLengkap = $"Aksi: {aksi} | Data: {detailData} | Error: {pesanError}";
                    string sql = "INSERT INTO LogError (waktu, pesan_error) VALUES (GETDATE(), @pesan)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@pesan", pesanLengkap);
                    cmd.ExecuteNonQuery();
                }
                catch { }
            }
        }

        private void pbFoto_Click(object sender, EventArgs e)
        {

        }
    }
}