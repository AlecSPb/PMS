using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.XSSF;
using System.IO;

namespace PMSClient.ExcelOutputHelper
{
    public class ExcelOutputBasePageByDate
    {
        // 输出文件名
        protected string excelFileName = String.Empty;
        protected string sheetName = String.Empty;
        //分页参数
        protected int pageSize, recordCount, pageCount, pageIndex;

        //检索空字符
        protected string empty = "";


        public event EventHandler<double> UpdateProgress;

        protected void OnUpdateProgress(double progressValue)
        {
            UpdateProgress?.Invoke(this, progressValue);
        }

        protected int year_start = 0;
        protected int month_start = 0;
        protected int year_end = 0;
        protected int month_end = 0;

        public void SetParameter(int y_s, int m_s, int y_e, int m_e)
        {
            year_start = y_s;
            month_start = m_s;
            year_end = y_e;
            month_end = m_e;
        }


        protected bool openAfterCreate = true;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="excel2007FileName">不包含扩展名的文件名</param>
        /// <param name="pageSize">分页大小</param>
        public ExcelOutputBasePageByDate()
        {

        }
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="excel2007FileName"></param>
        /// <param name="sheetName"></param>
        /// <param name="pageSize"></param>
        public void Intialize(string excel2007FileName, string sheetName = "Default", int pageSize = 50, bool openAfterCreate = true)
        {
            this.sheetName = sheetName;
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string folder = Path.Combine(desktop, DateTime.Today.ToString("yyMMdd"));
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            this.excelFileName = Path.Combine(folder, excel2007FileName + ".xlsx");
            this.pageSize = pageSize;
            this.openAfterCreate = openAfterCreate;

            ResetParameters();
        }
        /// <summary>
        /// 获取分页页数
        /// </summary>
        /// <returns></returns>
        protected int GetPageCount()
        {
            return recordCount / pageSize + (recordCount % pageSize == 0 ? 0 : 1);
        }

        protected void ResetParameters()
        {
            recordCount = 0;
            pageCount = 0;
            pageIndex = 0;
        }

        /// <summary>
        /// 输出Excel，在子类中重写
        /// </summary>
        public virtual void Output()
        {

        }

        /// <summary>
        /// 判断是否在创建后打开文件
        /// </summary>
        protected void CheckOpenAfterCreate()
        {
            try
            {
                if (openAfterCreate)
                {
                    System.Diagnostics.Process.Start(excelFileName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
