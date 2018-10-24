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
        }

        private void SetFolderPath(TextBox txt,string message)
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
        }

        private void BtnScanFolderSelect_Click(object sender, EventArgs e)
        {
            SetFolderPath(TxtCscanFolder, "请选择CSCAN报告所在的文件夹，确保都是jpg格式");            
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            var pp = new PhotoProcess();
            string sourceFolder = Environment.CurrentDirectory;
            string targetFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string docxFile = System.IO.Path.Combine(sourceFolder,"Input","Docx", "PMI_COA_Chaozhou_CIGS_181008_CA_1.docx");
            string jpegFile = System.IO.Path.Combine(sourceFolder, "Input", "Cscan", "181008-CA-1.jpg");

            pp.AppendJPGIntoDocx(docxFile,jpegFile,targetFolder);


            MessageBox.Show("DONE");

        }





    }
}
