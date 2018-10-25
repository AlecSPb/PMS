using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImportTargetPhotoIntoReport
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            InitializeThis();
        }

        private void InitializeThis()
        {
            this.Text = "COA报告批量追加超声照片辅助程序 designed by xs.zhou@2018";
            pp = new PhotoProcess();
            pp.ChangeMessage += (s, arg) =>
            {
                AddStatus(arg.Message);
            };

            pp.ChangeProcess += (s, e) =>
            {
                ChangeProgressBarValue(e.Progress);
            };


            sb = new StringBuilder();
            string current_folder = Environment.CurrentDirectory;
            TxtCoaFolder.Text = System.IO.Path.Combine(current_folder, "Input", "Docx");
            TxtCscanFolder.Text = System.IO.Path.Combine(current_folder, "Input", "Cscan");

            TxtStatus.Text = System.IO.File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, "readme.txt"));
        }

        private PhotoProcess pp;


        private void SetFolderPath(TextBox txt, string message)
        {
            if (txt != null)
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = message;
                dialog.RootFolder = Environment.SpecialFolder.Desktop;
                dialog.ShowNewFolderButton = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txt.Text = dialog.SelectedPath;
                }
            }
        }

        private void BtnCoaFolderSelect_Click(object sender, EventArgs e)
        {
            SetFolderPath(TxtCoaFolder, "请选择COA报告所在的文件夹，确保都是docx格式");
            AddStatus("COA报告文件夹设置完毕！");
        }

        private void BtnScanFolderSelect_Click(object sender, EventArgs e)
        {
            SetFolderPath(TxtCscanFolder, "请选择CSCAN报告所在的文件夹，确保都是jpg格式");
            AddStatus("CSCAN图片文件夹设置完毕！");

        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            BtnStart.Enabled = false;
            sb.Clear();
            pp.LoadFile(TxtCoaFolder.Text, TxtCscanFolder.Text);
            if (MessageBox.Show("载入文件完成，继续处理吗？", "请问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                == DialogResult.OK)
            {
                Task.Factory.StartNew(StartProcess);
            }


        }

        private void StartProcess()
        {
            pp.Process();
            AddStatus("处理完毕");

            this.Invoke(new Action(() =>
            {
                this.BtnStart.Enabled = true;
            }));
        }

        private StringBuilder sb;

        private void AddStatus(string msg)
        {
            sb.Insert(0, $"{DateTime.Now.ToLongTimeString()}=>{msg}\r\n");
            this.Invoke(new Action(() =>
            {
                TxtStatus.Text = sb.ToString();
            }));
        }

        private void ChangeProgressBarValue(int value)
        {
            this.Invoke(new Action(() =>
            {
                PbValue.Value = value;
            }));
        }

        private void ChkPrintProductID_CheckedChanged(object sender, EventArgs e)
        {
            pp.HasWaterPrint = ChkPrintProductID.Checked;
        }

        private void chkOpenOutput_CheckedChanged(object sender, EventArgs e)
        {
            pp.IsOpenOutputDirectory = chkOpenOutput.Checked;
        }
    }
}
