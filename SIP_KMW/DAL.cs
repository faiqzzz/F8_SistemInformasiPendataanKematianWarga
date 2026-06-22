using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SIP_KMW
{
    internal class DAL
    {
        string connString = "Data Source=DESKTOP-DDDRHRS\\RIDHOFAIQAHMAD;Initial Catalog=DB_SIPKMW;User ID=sa;Password=sa";

        public void InsertData(string nik, string nama, string jk, DateTime tglLahir, DateTime tglWafat, int usia, string sebab, string alamat, int userId)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("sp_InsertDataKematian", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nik", nik);
                cmd.Parameters.AddWithValue("@nama", nama);
                cmd.Parameters.AddWithValue("@jk", jk);
                cmd.Parameters.AddWithValue("@tglL", tglLahir);
                cmd.Parameters.AddWithValue("@tglW", tglWafat);
                cmd.Parameters.AddWithValue("@usia", usia);
                cmd.Parameters.AddWithValue("@sebab", sebab);
                cmd.Parameters.AddWithValue("@alamat", alamat);
                // TAMBAHKAN BARIS INI:
                cmd.Parameters.AddWithValue("@userid", userId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void LoadGrafikData(System.Windows.Forms.DataVisualization.Charting.Chart chart)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                // 1. Reset chart dulu
                chart.Series.Clear();
                chart.ChartAreas.Clear();
                chart.ChartAreas.Add("AreaUtama");
                // 2. Buat series baru
                var series = chart.Series.Add("Statistik");
                series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
                // Biar ada efek 3D
                chart.ChartAreas["AreaUtama"].Area3DStyle.Enable3D = true;
                series.IsValueShownAsLabel = true; // Biar angka muncul di potongan pie
                                                   // 1. Transparent-kan Chart utama
                chart.BackColor = Color.Transparent;

                // 2. Transparent-kan Area tempat gambar grafik
                chart.ChartAreas[0].BackColor = Color.Transparent;

                // 3. Hapus garis tepi (Border)
                chart.BorderlineColor = Color.Transparent;

                // 3. Ambil data dari database (Gunakan Query Group By Penyebab)
                string sql = "SELECT Penyebab, COUNT(*) as Total FROM v_DataKematianLengkap GROUP BY Penyebab";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // 4. Masukkan data dari database ke grafik
                foreach (DataRow row in dt.Rows)
                {
                    string label = row["Penyebab"].ToString();
                    double total = Convert.ToDouble(row["Total"]);

                    // Ganti baris penambahan data menjadi seperti ini:
                    int index = series.Points.AddXY(label, total);
                    DataPoint point = series.Points[index];

                    // Sekarang 'point' sudah benar-benar objek DataPoint
                    point.Label = "#VALX\n#PERCENT{P0}";
                }
                foreach (var legend in chart.Legends)
                {
                    legend.BackColor = Color.Transparent;
                }
            }
        }

        public void LoadGrafikJK(System.Windows.Forms.DataVisualization.Charting.Chart chart)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                chart.Series.Clear();
                chart.ChartAreas.Clear();
                chart.ChartAreas.Add("AreaJK");

                var series = chart.Series.Add("JenisKelamin");
                series.ChartType = SeriesChartType.Doughnut;
                chart.ChartAreas["AreaJK"].Area3DStyle.Enable3D = true;
                series.IsValueShownAsLabel = true;
                // 1. Transparent-kan Chart utama
                chart.BackColor = Color.Transparent;

                // 2. Transparent-kan Area tempat gambar grafik
                chart.ChartAreas[0].BackColor = Color.Transparent;

                // 3. Hapus garis tepi (Border)
                chart.BorderlineColor = Color.Transparent;

                // Gunakan nama kolom yang benar: 'JenisKelamin'
                string sql = "SELECT JenisKelamin, COUNT(*) as Total FROM v_DataKematianLengkap GROUP BY JenisKelamin";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    // Pastikan key-nya juga 'JenisKelamin'
                    string label = row["JenisKelamin"].ToString();
                    double total = Convert.ToDouble(row["Total"]);

                    int index = series.Points.AddXY(label, total);
                    DataPoint point = series.Points[index];
                    point.Label = "#VALX\n#PERCENT{P0}";
                }
                foreach (var legend in chart.Legends)
                {
                    legend.BackColor = Color.Transparent;
                }
            }
        }
    }
}