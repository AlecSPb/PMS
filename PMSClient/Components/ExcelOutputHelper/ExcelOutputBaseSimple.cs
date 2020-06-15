using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.ExcelOutputHelper
{
    public class ExcelOutputBaseSimple
    {
        // 输出文件名
        protected string excelFileName = String.Empty;
        protected string sheetName = String.Empty;

        //检索空字符
        protected string empty = "";

        protected bool openAfterCreate = true;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="excel2007FileName">不包含扩展名的文件名</param>
        /// <param name="pageSize">分页大小</param>
        public ExcelOutputBaseSimple()
        {

        }
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="excel2007FileName"></param>
        /// <param name="sheetName"></param>
        /// <param name="pageSize"></param>
        public void Intialize(string excel2007FileName, string sheetName = "Default", bool openAfterCreate = true)
        {
            this.sheetName = sheetName;
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string folder = Path.Combine(desktop, DateTime.Today.ToString("yyMMdd"));
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            this.excelFileName = Path.Combine(folder, excel2007FileName + ".xlsx");
            this.openAfterCreate = openAfterCreate;

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
                                PMSHelper.CurrentLog.Error(ex);
            }
        }

    }
}
