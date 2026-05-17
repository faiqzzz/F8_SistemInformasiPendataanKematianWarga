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

       

        void TampilkanPenyebab()
        {
            try
            {
                DataTable dtPenyebab = konn.GetData("SELECT NamaPenyebab FROM MasterPenyebab");
                cbPenyebab.Items.Clear();
                foreach (DataRow dr in dtPenyebab.Rows)
                {
                    cbPenyebab.Items.Add(dr["NamaPenyebab"].ToString());
                }
            }
            catch { /* Ignored */ }
        }

        
    }
}