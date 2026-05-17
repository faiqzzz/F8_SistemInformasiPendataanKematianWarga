using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace SIP_KMW
{
    public partial class FormKoneksi : Form
    {
        Koneksi konn = new Koneksi();

        public FormKoneksi()
        {
            InitializeComponent();
            btnLanjut.Enabled = false; // Tombol login dimatikan dulu.
        }

        private void btnCekKoneksi_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    conn.Open();
                    lblStatus.Text = "Status: Terkoneksi ke Database!";
                    lblStatus.ForeColor = System.Drawing.Color.Green;

                    MessageBox.Show("Koneksi Sukses!", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Aktifkan tombol lanjut.
                    btnLanjut.Enabled = true;
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "Status: Koneksi Gagal!";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    MessageBox.Show("Gagal terhubung: " + ex.Message);
                }
            }
        }

        private void btnLanjut_Click(object sender, EventArgs e)
        {
            FormLogin login = new FormLogin();
            login.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormKoneksi_Load(object sender, EventArgs e)
        {

        }
    }
}


