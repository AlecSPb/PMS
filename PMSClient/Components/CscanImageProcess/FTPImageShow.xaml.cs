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
using System.Windows.Shapes;
using System.IO;

namespace PMSClient.Components.CscanImageProcess
{
    /// <summary>
    /// FTPImageShow.xaml 的交互逻辑
    /// </summary>
    public partial class FTPImageShow : Window
    {
        public FTPImageShow()
        {
            InitializeComponent();
        }

        public string CurrentFile { get; set; }
        private void MainInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(System.IO.Path.GetDirectoryName(CurrentFile));
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
    }
}
