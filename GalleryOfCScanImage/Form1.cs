using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonHelper;
using GalleryOfCScanImage.Service;

namespace GalleryOfCScanImage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Initial();
        }

        private void Initial()
        {
            DtpStart.Value = DateTime.Today.AddDays(-30);
            DtpEnd.Value = DateTime.Today;
            LoadPaths();
            UpdateStatus("默认设置加载完毕");

        }

        private void LoadPaths()
        {
            TxtImageFolder.Text = Properties.Settings.Default.PathImage;
            TxtOutputFolder.Text = Properties.Settings.Default.PathOutput;
        }

        private void SavePaths()
        {
            Properties.Settings.Default.PathImage = TxtImageFolder.Text;
            Properties.Settings.Default.PathOutput = TxtOutputFolder.Text;
            Properties.Settings.Default.Save();
        }



        private ProcessParameter parameters = new ProcessParameter();

        private BasicHelper helper = new BasicHelper();


        private void BtnSelectImageFolder_Click(object sender, EventArgs e)
        {
            PathParameter path = helper.DilaogSelectDirectoryPath("请选择超声图片所在的文件夹");
            if (path.HasSelected)
            {
                TxtImageFolder.Text = path.SelectPath;
                UpdateStatus("已设置超声图片文件夹路径");
            }

        }

        private void BtnSelectOutputFolder_Click(object sender, EventArgs e)
        {
            PathParameter path = helper.DilaogSelectDirectoryPath("请选择输出文件所在的文件夹");
            if (path.HasSelected)
            {
                TxtOutputFolder.Text = path.SelectPath;
                UpdateStatus("已设置输出图片文件夹路径");
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (!CheckFolderPath(TxtImageFolder.Text))
            {
                helper.DialogShowWarning("图片文件夹不存在");
                return;
            }
            if (!CheckFolderPath(TxtOutputFolder.Text))
            {
                helper.DialogShowWarning("输出文件夹不存在");
                return;
            }
            if (DtpStart.Value >= DtpEnd.Value)
            {
                helper.DialogShowWarning("开始日期不得大于结束日期");
                return;
            }
            parameters.OpenTheDocument = ChkIsOpen.Checked;
            parameters.ShowProcessDetails = ChkShowProcessDetails.Checked;
            parameters.ImageFolder = TxtImageFolder.Text;
            parameters.OutputFolder = TxtOutputFolder.Text;
            parameters.Start = DtpStart.Value;
            parameters.End = DtpEnd.Value;

            if (helper.DialogShowQuestion("载入文件完成，继续处理吗？"))
            {
                Task.Factory.StartNew(StartProcess);
            }

        }

        private bool CheckFolderPath(string folderpath)
        {
            return System.IO.Directory.Exists(folderpath);
        }

        private void StartProcess()
        {
            this.Invoke(new Action(() =>
            {
                BtnStart.Enabled = false;
            }));

            GalleryService gs = new GalleryService();
            gs.UpdateStatus += UpdateStatusInTask;
            gs.UpdateProgressValue += UpdateProgressValueInTask;
            gs.Parameters = parameters;

            gs.Process();

            this.Invoke(new Action(() =>
            {
                BtnStart.Enabled = true;
            }));
        }

        #region 更新状态

        private void UpdateStatus(string msg)
        {
            TxtStatus.Text = helper.InsertStatus(msg);
        }

        private void UpdateStatusInTask(object sender, string e)
        {
            this.Invoke(new Action(() =>
            {
                TxtStatus.Text = helper.InsertStatus(e);
            }));
        }

        private void UpdateProgressValueInTask(object sender, int e)
        {
            this.Invoke(new Action(() =>
            {
                TSProgressBar.Value = e;
            }));
        }
        #endregion



        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SavePaths();
        }

        private void DtpStart_ValueChanged(object sender, EventArgs e)
        {
            UpdateStatus($"开始日期已设置 {DtpStart.Value.ToShortDateString()}");
        }

        private void DtpEnd_ValueChanged(object sender, EventArgs e)
        {
            UpdateStatus($"结束日期已设置 {DtpEnd.Value.ToShortDateString()}");
        }

        private void BtnThisMonth_Click(object sender, EventArgs e)
        {
            DtpStart.Value = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
            DtpEnd.Value = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);
        }
        private void BtnLastMonth_Click(object sender, EventArgs e)
        {
            DtpStart.Value = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(-1).Date;
            DtpEnd.Value = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddDays(-1).Date;
        }
    }
}
