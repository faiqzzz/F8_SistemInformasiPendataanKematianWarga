using SIP_KMW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIP_KMW
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Inilah baris yang menentukan form mana yang muncul pertama kali.
            // Kita arahkan ke FormKoneksi agar user harus cek koneksi dulu.
            Application.Run(new FormKoneksi());
        }
    }
}
