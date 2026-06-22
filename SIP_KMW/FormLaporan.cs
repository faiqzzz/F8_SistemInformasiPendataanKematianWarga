using CrystalDecisions.CrystalReports.Engine;
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

namespace SIP_KMW
{
    public partial class FormLaporan : Form
    {
        Koneksi konn = new Koneksi();
        public FormLaporan()
        {
            InitializeComponent();
        }

        private void FormLaporan_Load(object sender, EventArgs e)
        {
            try
            {
                // Mengambil lokasi file report relatif dari folder aplikasi
                string namaFileReport = "rptDataKematian.rpt";
                string reportPath = Path.Combine(Application.StartupPath, "Reports", namaFileReport);

                // Cek apakah file benar-benar ada
                if (!File.Exists(reportPath))
                {
                    MessageBox.Show("File laporan tidak ditemukan di: " + reportPath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Ambil Data dengan pola yang konsisten
                DataSet ds = new DataSet();
                using (SqlConnection conn = konn.GetConn())
                {
                    // Pastikan koneksi terbuka jika diperlukan oleh adapter
                    string sql = "SELECT * FROM v_DataKematianLengkap";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.Fill(ds, "v_DataKematianLengkap");
                }

                // Load Report
                ReportDocument cryRpt = new ReportDocument();
                cryRpt.Load(reportPath);
                cryRpt.SetDataSource(ds.Tables["v_DataKematianLengkap"]);

                // Tampilkan
                crystalReportViewer1.ReportSource = cryRpt;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat laporan: " + ex.Message, "Error Laporan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}

