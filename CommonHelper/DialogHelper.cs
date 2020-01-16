using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonHelper
{
    public class DialogHelper
    {
        /// <summary>
        /// 在WinForm中获取文件夹位置
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
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
        /// 显示保存对话框
        /// </summary>
        /// <param name="startPath"></param>
        /// <returns></returns>
        public XSDialogResult ShowSaveDialog(string initialDirectory, string filter = "*.*", string filename = "default")
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = filename;
            dialog.InitialDirectory = initialDirectory;
            dialog.Filter = filter;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return new XSDialogResult() { HasSelected = true, SelectPath = dialog.FileName };
            }
            return new XSDialogResult() { HasSelected = false };
        }

        /// <summary>
        /// 显示打开对话框
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public XSDialogResult ShowOpenDialog(string initialDirectory, string filter = "*.*")
        {
            var dialog = new OpenFileDialog();
            dialog.InitialDirectory = initialDirectory;
            dialog.Filter = filter;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return new XSDialogResult() { HasSelected = true, SelectPath = dialog.FileName };
            }
            return new XSDialogResult() { HasSelected = false };
        }


    }
}
