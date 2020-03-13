using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient
{
    public static class PMSFolderPath
    {
        public static string Documents
        {
            get
            {
                return System.IO.Path.Combine(Environment.CurrentDirectory, "Documents");
            }
        }
        public static string Roots
        {
            get
            {
                return Environment.CurrentDirectory;
            }
        }
    }
}
