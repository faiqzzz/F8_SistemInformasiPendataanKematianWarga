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
        Koneksi konn = new Koneksi();
        DAL dal = new DAL();

        public FormMenuUtama()
        {
            InitializeComponent();
        }

        private void btnBukaInput_Click(object sender, EventArgs e)
        {
            FormInputData frmInput = new FormInputData();
            frmInput.Show();
            this.Hide();
        }

        private void btnManajemenUser_Click(object sender, EventArgs e)
        {
            FormManajemenUser frm = new FormManajemenUser();
            frm.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Apakah Anda yakin ingin Logout?", "Konfirmasi",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialog == DialogResult.Yes)
            {
                FormLogin login = new FormLogin();
                login.Show();
                this.Close();
            }
        }


        private void lblTotalData_Click(object sender, EventArgs e)
        {

        }

        private void lblWelcome_Click(object sender, EventArgs e)
        {

        }

        private void lblRole_Click(object sender, EventArgs e)
        {

        }

        private void chartJK_Click(object sender, EventArgs e)
        {

        }

        private void chartKematian_Click(object sender, EventArgs e)
        {

        }

        private void FormMenuUtama_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = "Halo, " + GlobalData.NamaLengkap;
            lblRole.Text = "Akses: " + GlobalData.Role;

            TampilkanStatistik();

            // Logika Akses Tombol:
            // 1. button1 (Manajemen User): HANYA SuperAdmin
            // 2. btnBukaLog (Sistem Log): SuperAdmin DAN Admin
            bool isSA = (GlobalData.Role == "SuperAdmin");
            bool isAdminOrSA = (GlobalData.Role == "Admin" || GlobalData.Role == "Administrator" || GlobalData.Role == "SuperAdmin");

            button1.Visible = isSA;
            btnBukaLog.Visible = isAdminOrSA;

            btnBukaInput.Visible = true;

            dal.LoadGrafikData(chartKematian);
            dal.LoadGrafikJK(chartJK);
        }

        void TampilkanStatistik()
        {
            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT COUNT(*) FROM v_DataKematianLengkap";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        lblTotalData.Text = "TOTAL WARGA TERDATA MENINGGAL : " + result.ToString();
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error Statistik: " + ex.Message);
                }
            }
        }

        private void FormMenuUtama_Activated(object sender, EventArgs e)
        {
            TampilkanStatistik();
        }


        private void btnBukaLog_Click_1(object sender, EventArgs e)
        {
            FormLog frm = new FormLog();
            frm.ShowDialog();
        }
    }
}
