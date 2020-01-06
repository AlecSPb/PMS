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
            helper.UpdateTextBox += (s, e) =>
            {
                TxtStatus.Text = e;
            };
            helper.InsertStatus("默认设置加载完毕");

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
            TxtImageFolder.Text = helper.GetDirectoryPath("请选择超声图片所在的文件夹");
            helper.InsertStatus("已设置超声图片文件夹路径");
        }

        private void BtnSelectOutputFolder_Click(object sender, EventArgs e)
        {
            TxtOutputFolder.Text = helper.GetDirectoryPath("请选择输出文件所在的文件夹");
            helper.InsertStatus("已设置输出图片文件夹路径");
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            helper.InsertStatus("开始处理，耐心等待");
            parameters.OpenAfterCreated = ChkIsOpen.Checked;


        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SavePaths();
        }

        private void DtpStart_ValueChanged(object sender, EventArgs e)
        {
            helper.InsertStatus($"开始日期已设置 {DtpStart.Value.ToShortDateString()}");

        }

        private void DtpEnd_ValueChanged(object sender, EventArgs e)
        {
            helper.InsertStatus($"结束日期已设置 {DtpEnd.Value.ToShortDateString()}");
        }
    }
}
