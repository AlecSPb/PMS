using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ImportTargetPhotoIntoReport
{
    public partial class CscanMarker : Form
    {
        public CscanMarker()
        {
            InitializeComponent();

            InitializeThis();
        }

        private ProcessHelper helper;
        private PhotoProcess process;
        private void InitializeThis()
        {
            this.Text = "Cscan图像批量标记水印程序2.0 可处理任意靶材图片 designed by xs.zhou";
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

            TxtPhotos.Text = helper.DefaultScanFolder;
            string manual_file = Path.Combine(Environment.CurrentDirectory, "manual2.txt");
            helper.LoadManual(TxtStatus, manual_file);

        }



        private void BtnStart_Click(object sender, EventArgs e)
        {
            BtnStart.Enabled = false;
            helper.ClearStatusMessage();

            process.LoadFile(TxtPhotos.Text);
            if (MessageBox.Show("载入文件完成，继续处理吗？", "请问"
                , MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
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
            process.ProcessCscanPhoto();
            helper.AddStatus(this, TxtStatus, "处理完毕");

            this.Invoke(new Action(() =>
            {
                this.BtnStart.Enabled = true;
            }));
        }


        private void BtnFolder_Click(object sender, EventArgs e)
        {
            helper.SelectFolderPath(TxtPhotos, "请选择CSCAN报告所在的文件夹，确保都是jpg格式");
            helper.AddStatus(this, TxtStatus, "CSCAN图片文件夹设置完毕！");
        }

        private void CkProductID_CheckedChanged(object sender, EventArgs e)
        {
            process.PhotoMarkerControl.HasProductID = CKComposition.Checked;
        }

        private void CKComposition_CheckedChanged(object sender, EventArgs e)
        {
            process.PhotoMarkerControl.HasComposition = CKComposition.Checked;
        }

        private void CKWeldingRation_CheckedChanged(object sender, EventArgs e)
        {
            process.PhotoMarkerControl.HasWeldingRation = CKWeldingRation.Checked;
        }

        private void chkOpenOutput_CheckedChanged(object sender, EventArgs e)
        {
            process.IsOpenOutputDirectory = chkOpenOutput.Checked;
        }

        private void TxtFontSize_TextChanged(object sender, EventArgs e)
        {
            process.PhotoMarkerControl.FontSize = float.Parse(TxtFontSize.Text.Trim());
        }

        private void TxtLogo_TextChanged(object sender, EventArgs e)
        {
            process.PhotoMarkerControl.Logo = TxtLogo.Text.Trim();
        }

        private void TxtFixedWeldingRate_TextChanged(object sender, EventArgs e)
        {
            process.PhotoMarkerControl.FixedWeldingRate = TxtFixedWeldingRate.Text;
        }
    }
}
