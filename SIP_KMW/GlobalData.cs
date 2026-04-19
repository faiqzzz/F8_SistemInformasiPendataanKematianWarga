using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIP_KMW
{
    public static class GlobalData
    {
        // static artinya variabel ini "nempel" di aplikasi, bukan di form
        // jadi datanya gak bakal hilang pas pindah-pindah form
        public static string Role = "Admin";
        public static string NamaUser;
    }
}