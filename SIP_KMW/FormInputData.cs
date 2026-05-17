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

        
    }
}