using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace ImportTargetPhotoIntoReport
{
    public class ProcessHelper
    {
        public ProcessHelper()
        {
            string current_folder = Environment.CurrentDirectory;
            DefaultScanFolder = Path.Combine(current_folder, "Input", "Cscan");
            DefaultCoaFolder = Path.Combine(current_folder, "Input", "Docx");
            sb = new StringBuilder();

        }


        public string DefaultScanFolder { get; set; }
        public string DefaultCoaFolder { get; set; }

        /// <summary>
        /// 弹出选择路径对话框
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public void SelectFolderPath(TextBox txt, string msg)
        {
            if (txt != null)
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = msg;
                dialog.RootFolder = Environment.SpecialFolder.Desktop;
                dialog.ShowNewFolderButton = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txt.Text = dialog.SelectedPath;
                }

            }

        }

        /// <summary>
        /// 加载手册到文本框
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="filePath"></param>
        public void LoadManual(TextBox txt, string filePath)
        {
            if (txt != null && File.Exists(filePath))
            {
                txt.Text = File.ReadAllText(filePath);
            }
        }

        private StringBuilder sb;
        public void ClearStatusMessage()
        {
            sb.Clear();
        }
        /// <summary>
        /// 添加状态
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="msg"></param>
        public void AddStatus(Form form, TextBox txt, string msg)
        {
            if (form != null && txt != null)
            {
                sb.Insert(0, $"{DateTime.Now.ToLongTimeString()}=>{msg}\r\n");
                form.Invoke(new Action(() =>
                {
                    txt.Text = sb.ToString();
                }));
            }
        }


        public void ChangeProgressBarValue(Form form, ProgressBar pb, int value)
        {

            if (form != null && pb != null)
            {
                form.Invoke(new Action(() =>
                {
                    pb.Value = value;
                }));
            }

        }



    }
}
