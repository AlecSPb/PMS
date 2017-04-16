using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.ReportsHelper
{
    public static class ReportHelper
    {
        /// <summary>
        /// 返回公共的Report模板文件夹
        /// </summary>
        public static string ReportsTemplateFolder
        {
            get
            {
                var reportsFolder = Path.Combine(Environment.CurrentDirectory, "DocTemplate", "Reports");
                return reportsFolder;
            }
        }
        /// <summary>
        /// 临时文件位置
        /// </summary>
        public static string ReportsTemplateTempFolder
        {
            get
            {
                var reportsFolder = Path.Combine(Environment.CurrentDirectory, "DocTemplate", "Reports","Temp");
                return reportsFolder;
            }
        }
        public static string DesktopFolder
        {
            get
            {
                var reportsFolder = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                return reportsFolder;
            }
        }
        public static string TimeName
        {
            get
            {
                return DateTime.Now.ToString("yyyyMMddHHmmss")+".docx";
            }
        }
        /// <summary>
        /// 复制文件，如果文件存在，先删除再复制
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="targetFile"></param>
        public static void FileCopy(string sourceFile,string targetFile)
        {
            try
            {
                if (File.Exists(targetFile))
                {
                    File.Delete(targetFile);
                }
                File.Copy(sourceFile, targetFile);
            }
            catch (Exception)
            {

            }
        }

    }
}
