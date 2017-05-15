using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using f = System.Windows.Forms;

namespace PMSClient.View
{
    /// <summary>
    /// ProductEditView.xaml 的交互逻辑
    /// </summary>
    public partial class RecordTestDocView : UserControl
    {
        public RecordTestDocView()
        {
            InitializeComponent();
        }

        private void txtBrowse_Click(object sender, RoutedEventArgs e)
        {
            f.FolderBrowserDialog fbd = new f.FolderBrowserDialog();
            fbd.Description = "请选择文档存放路径";
            fbd.ShowNewFolderButton = true;
            fbd.RootFolder = Environment.SpecialFolder.DesktopDirectory;
            if (fbd.ShowDialog() == f.DialogResult.OK)
            {
                PMSMethods.SetTextBox(txtCurrentFolder, fbd.SelectedPath);
            }
        }
    }
}
