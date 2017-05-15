using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.ReportsHelper
{
    public class ReportBase
    {
        public virtual void Output() { }
        protected string sourceFile;
        protected string targetFile;
        protected string tempFile;
    }
}
