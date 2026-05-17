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

        

        private void btnCetak_Click(object sender, EventArgs e)
        {
            // Kode Excel interop kamu (Sama seperti sebelumnya)
            if (dgvData.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application xcelApp = new Microsoft.Office.Interop.Excel.Application();
                xcelApp.Application.Workbooks.Add(Type.Missing);
                for (int i = 1; i < dgvData.Columns.Count + 1; i++) xcelApp.Cells[1, i] = dgvData.Columns[i - 1].HeaderText;
                for (int i = 0; i < dgvData.Rows.Count; i++)
                {
                    if (dgvData.Rows[i].IsNewRow) continue;
                    for (int j = 0; j < dgvData.Columns.Count; j++)
                    {
                        var cellValue = dgvData.Rows[i].Cells[j].Value;
                        xcelApp.Cells[i + 2, j + 1] = cellValue != null ? cellValue.ToString() : "";
                    }
                }
                xcelApp.Columns.AutoFit();
                xcelApp.Visible = true;
            }
        }
    }
}