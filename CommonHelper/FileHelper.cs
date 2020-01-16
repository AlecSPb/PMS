using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace CommonHelper
{
    /// <summary>
    /// 文件处理相关的帮助类
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="filepath"></param>
        public void OpenFile(string filepath)
        {
            try
            {
                if (File.Exists(filepath))
                {
                    System.Diagnostics.Process.Start(filepath);
                }
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 创建新的文件夹在某个位置
        /// 默认在桌面上创建文件夹
        /// </summary>
        /// <param name="folderpath"></param>
        public string CreateFolder(string folder, string foldername)
        {
            if (string.IsNullOrEmpty(folder))
            {
                folder = GetDesktopPath();
            }
            string folderpath = Path.Combine(folder, foldername);
            if (Directory.Exists(folderpath)) return folderpath;
            Directory.CreateDirectory(folderpath);
            return folderpath;
        }

        /// <summary>
        /// 根据路径创建文件夹
        /// </summary>
        /// <param name="folderpath"></param>
        /// <returns></returns>
        public void CreateFolder(string folderpath)
        {
            if (Directory.Exists(folderpath)) return;
            Directory.CreateDirectory(folderpath);
        }

        /// <summary>
        /// 获取日期命名的文件夹名
        /// </summary>
        /// <returns></returns>
        public string GetNameByDate()
        {
            return DateTime.Today.ToString("yyMMdd");
        }


        /// <summary>
        /// 返回当前文件夹路径
        /// </summary>
        /// <returns></returns>
        public string GetCurrentFolderPath(string dir = "")
        {
            return Path.Combine(Environment.CurrentDirectory, dir);
        }

        /// <summary>
        /// 返回桌面路径
        /// </summary>
        /// <returns></returns>
        public string GetDesktopPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        }


        /// <summary>
        /// 获取当前时间文件名，精确到秒
        /// </summary>
        /// <param name="extensionName"></param>
        /// <returns></returns>
        public string GetDateTimeFileName(string extensionName = "docx")
        {
            return $"{DateTime.Now.ToString("yyyyMMddHHmmss")}.{extensionName}";
        }

        /// <summary>
        /// 获取完整文件名
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public string GetFullFileName(string file, string root, string folder = "")
        {
            return Path.Combine(root, folder, file);
        }

        /// <summary>
        /// 获取完整文件名-默认桌面
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public string GetFullFileName(string file)
        {
            return Path.Combine(GetDesktopPath(), file);
        }

        /// <summary>
        /// 在WinForm中获取文件夹位置
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        [Obsolete]
        public XSDialogResult ShowFolderBrowserDialog(string description)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = description;
            dialog.ShowNewFolderButton = true;
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                return new XSDialogResult { HasSelected = true, SelectPath = dialog.SelectedPath };
            }
            else
            {
                return new XSDialogResult { HasSelected = false };
            }
        }

        /// <summary>
        /// 写入文本
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="filecontent"></param>
        public void SaveText(string filename, string filecontent)
        {
            File.WriteAllText(filename, filecontent);
        }

        /// <summary>
        /// 读取文本
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public string ReadText(string filename)
        {
            if (!File.Exists(filename)) return null;
            return File.ReadAllText(filename);
        }
    }
}
