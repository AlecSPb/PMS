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
    public partial class CscanToCoa : Form
    {
        public CscanToCoa()
        {
            InitializeComponent();

            InitializeThis();
        }
        private ProcessHelper helper;
        private PhotoProcess process;

        private void InitializeThis()
        {
            this.Text = "COA报告批量追加超声照片程序 designed by xs.zhou";

            helper = new ProcessHelper();
            process = new PhotoProcess();
            process.ChangeMessage += (s, e) =>
            {
                helper.AddStatus(this, TxtStatus, e.Message);
            };

            process.ChangeProcess += (s, e) =>
            {
                helper.ChangeProgressBarValue(this, PbValue, e.Progress);
            };


            TxtCoaFolder.Text = helper.DefaultCoaFolder;
            TxtCscanFolder.Text = helper.DefaultScanFolder;

            string manual_file = System.IO.Path.Combine(Environment.CurrentDirectory, "manual1.txt");
            helper.LoadManual(TxtStatus, manual_file);
        }



        private void BtnCoaFolderSelect_Click(object sender, EventArgs e)
        {
            helper.SelectFolderPath(TxtCoaFolder, "请选择COA报告所在的文件夹，确保都是docx格式");
            helper.AddStatus(this, TxtStatus, "COA报告文件夹设置完毕！");
        }

        private void BtnScanFolderSelect_Click(object sender, EventArgs e)
        {
            helper.SelectFolderPath(TxtCscanFolder, "请选择CSCAN报告所在的文件夹，确保都是jpg格式");
            helper.AddStatus(this, TxtStatus, "CSCAN图片文件夹设置完毕！");

        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            BtnStart.Enabled = false;
            helper.ClearStatusMessage();

            process.IsToPdf = ChkToPdf.Checked;
            process.IsOpenOutputDirectory = chkOpenOutput.Checked;

            process.LoadFile(TxtCoaFolder.Text, TxtCscanFolder.Text);
            if (MessageBox.Show("载入文件完成，继续处理吗？", "请问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                == DialogResult.OK)
            {
                Task.Factory.StartNew(StartProcess);
            }
            else
            {
                BtnStart.Enabled = true;
            }


        }

        private void StartProcess()
        {
            process.ProcessDocx();
            helper.AddStatus(this, TxtStatus, "处理完毕");

            this.Invoke(new Action(() =>
            {
                this.BtnStart.Enabled = true;
            }));
        }

    }
}
