using System;
using System.Collections.Generic;
using System.IO;
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
                return Path.Combine(Environment.CurrentDirectory, "Documents");
            }
        }
        public static string Roots
        {
            get
            {
                return Environment.CurrentDirectory;
            }
        }

        public static string DocxReportTemplate
        {
            get
            {
                return Path.Combine(Environment.CurrentDirectory, "Resource", "DocTemplate", "Reports");
            }
        }

    }
}
