using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PMSClient.ReportsHelper
{
    public class ReportBase
    {
        public virtual void Output() { }
        protected string sourceFile;
        protected string targetFile;
        protected string tempFile;
        protected string targetDir;

        public void CreateFolderOnDesktop()
        {
            targetDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                                                                         DateTime.Now.ToString("yyMMdd"));
            if (!Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }
        }
    }
}
