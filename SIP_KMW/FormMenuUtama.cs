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
        // Panggil class koneksi
        Koneksi konn = new Koneksi();

        public FormMenuUtama()
        {
            InitializeComponent();
        }

        
    }
}