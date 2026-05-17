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

        
    }
}