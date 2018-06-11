using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.ReportsHelperNew
{
    public class ReportBase
    {
        // 输出文件名
        protected string wordFileName = string.Empty;
        //分页参数
        protected int pageSize, recordCount, pageCount, pageIndex;
        protected string empty = string.Empty;
        protected string reportsFolder = Path.Combine(Environment.CurrentDirectory,
            "Resource", "DocTemplate", "Reports");
        public ReportBase()
        {

        }

        public void Intialize(string word2007FileName, int pageSize = 20)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string folder = Path.Combine(desktop, DateTime.Today.ToString("yyMMdd"));
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            this.wordFileName = Path.Combine(folder, word2007FileName + ".docx");
            this.pageSize = pageSize;
            ResetParameters();
        }

        protected void ResetParameters()
        {
            recordCount = 0;
            pageCount = 0;
            pageIndex = 0;
        }

        protected int GetPageCount()
        {
            return recordCount / pageSize + (recordCount % pageSize == 0 ? 0 : 1);
        }

        public virtual void Output()
        {

        }





    }
}
