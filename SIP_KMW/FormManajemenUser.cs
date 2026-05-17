using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace SIP_KMW
{
    public partial class FormManajemenUser : Form
    {
        Koneksi konn = new Koneksi();
        DataTable dt = new DataTable();

        public FormManajemenUser()
        {
            InitializeComponent();
        }

        private void FormManajemenUser_Load(object sender, EventArgs e)
        {
            TampilkanUser();
        }

        void TampilkanUser()
        {
            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    conn.Open();
                    // Di sini tetap pakai query manual / View karena cuma SELECT
                    string sql = "SELECT Username, NamaLengkap, Role FROM Users WHERE Username != 'sa'";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    dt = new DataTable();
                    da.Fill(dt);
                    dgvUser.DataSource = dt;

                    if (dgvUser.Columns.Count > 0)
                    {
                        dgvUser.Columns[1].Width = 200;
                    }
                }
                catch (Exception ex) { MessageBox.Show("Gagal Tampil User: " + ex.Message); }
            }
        }

        // --- POIN 1: INSERT USER MENGGUNAKAN STORED PROCEDURE ---
        private void btnSimpan_Click(object sender, EventArgs e)
        {
            // Validasi Input (Satpam)
            // Filter: Hanya boleh huruf (a-z, A-Z) dan spasi (\s)
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtNamaLengkap.Text, @"^[a-zA-Z\s]+$"))
            {
                MessageBox.Show("Nama Lengkap hanya boleh berisi huruf dan spasi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Username dan Password wajib diisi!");
                return;
            }

            if (!Regex.IsMatch(txtUsername.Text, @"^[a-zA-Z0-9]+$"))
            {
                MessageBox.Show("Username hanya boleh huruf dan angka!");
                return;
            }

            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    conn.Open();
                    // GANTI KE SP: sp_InsertUser (Pastikan kamu sudah buat SP ini di SQL)
                    SqlCommand cmd = new SqlCommand("sp_InsertUser", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@user", txtUsername.Text.ToLower());
                    cmd.Parameters.AddWithValue("@pass", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@nama", txtNamaLengkap.Text);
                    cmd.Parameters.AddWithValue("@role", cbRole.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User berhasil ditambahkan!");
                    TampilkanUser();
                    BersihkanInput();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal Simpan: Username mungkin sudah ada atau SP belum dibuat.\nDetail: " + ex.Message);
                }
            }
        }

        // --- POIN 1: UPDATE USER MENGGUNAKAN STORED PROCEDURE ---
        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text)) return;

            // Filter: Hanya boleh huruf (a-z, A-Z) dan spasi (\s)
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtNamaLengkap.Text, @"^[a-zA-Z\s]+$"))
            {
                MessageBox.Show("Nama Lengkap hanya boleh berisi huruf dan spasi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    conn.Open();
                    // GANTI KE SP: sp_UpdateUser
                    SqlCommand cmd = new SqlCommand("sp_UpdateUser", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@user", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@pass", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@nama", txtNamaLengkap.Text);
                    cmd.Parameters.AddWithValue("@role", cbRole.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data user berhasil diperbarui!");
                    TampilkanUser();
                    BersihkanInput();
                }
                catch (Exception ex) { MessageBox.Show("Error Update: " + ex.Message); }
            }
        }

        // --- POIN 1: DELETE USER MENGGUNAKAN STORED PROCEDURE ---
        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "sa")
            {
                MessageBox.Show("Akun System Admin tidak bisa dihapus!");
                return;
            }

            if (MessageBox.Show("Hapus user " + txtUsername.Text + "?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (SqlConnection conn = konn.GetConn())
                {
                    try
                    {
                        conn.Open();
                        // GANTI KE SP: sp_DeleteUser
                        SqlCommand cmd = new SqlCommand("sp_DeleteUser", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@user", txtUsername.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("User berhasil dihapus!");
                        TampilkanUser();
                        BersihkanInput();
                    }
                    catch (Exception ex) { MessageBox.Show("Error Hapus: " + ex.Message); }
                }
            }
        }

        private void dgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvUser.Rows[e.RowIndex];
                txtUsername.Text = row.Cells["Username"].Value.ToString();
                txtNamaLengkap.Text = row.Cells["NamaLengkap"].Value.ToString();
                cbRole.Text = row.Cells["Role"].Value.ToString();

                txtUsername.ReadOnly = true; // Username jangan boleh diganti pas update
            }
        }

        void BersihkanInput()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtNamaLengkap.Clear();
            cbRole.SelectedIndex = -1;
            txtUsername.ReadOnly = false;
        }

        private void btnBersih_Click(object sender, EventArgs e) => BersihkanInput();

        private void btnKembali_Click(object sender, EventArgs e) => this.Close();

        private void txtNamaLengkap_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Hanya izinkan huruf, spasi, dan tombol Backspace (kontrol)
            if (!char.IsLetter(e.KeyChar) && !char.IsSeparator(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Tolak input selain itu
            }
        }
    }
}