using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIP_KMW
{
    public partial class Form2 : Form
    {
        // 1. Tambahkan variabel untuk menampung Role
        string roleUser;

        // 2. Tambahkan 'string role' di dalam kurung ini
        public Form2(string role)
        {
            InitializeComponent();
            roleUser = role; // Simpan kiriman dari Form Login ke variabel ini
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Tambahkan baris ini buat ngetes:
            MessageBox.Show("Role yang terbaca adalah: " + roleUser);

            if (roleUser == "Petugas")
            {
                BtnManajemenUser.Visible = false;
            }
        }

        private void BtnManajemenUser_Click (object sender, EventArgs e)
        {
            // Buka jendela baru khusus buat ngatur user
            FormManajemenUser fu = new FormManajemenUser();
            fu.ShowDialog();
        }

        private void btnInputData_Click(object sender, EventArgs e)
        {
            FormInputData frm = new FormInputData();
            frm.ShowDialog(); // ShowDialog supaya user fokus selesaikan input dulu
        }

        
    }
}