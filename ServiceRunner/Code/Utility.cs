using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DigaSystem.ServiceRunner
{
    public class Utility
    {
        public static string GetServiceName()
        {
            string filename = GetFilename();
            string name = Path.GetFileNameWithoutExtension(filename);
            return name;
        }

        public static string GetFilename()
        {
            string filename = Process.GetCurrentProcess().MainModule.FileName;
            return filename;
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static string GetInfoFile()
        {
            string path = Path.GetTempPath();
            string filename = Base64Encode(GetFilename());
            return path + "\\" + filename + ".info";
        }
    }
}
