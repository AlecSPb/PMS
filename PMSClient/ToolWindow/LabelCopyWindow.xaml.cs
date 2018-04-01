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
using System.Diagnostics;
using System.IO;

namespace PMSClient.ToolWindow
{
    /// <summary>
    /// LabelCopyWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LabelCopyWindow : Window
    {
        public LabelCopyWindow()
        {
            InitializeComponent();
            LabelInformation = "无";
            BasicInformation = "无";
            this.DataContext = this;
        }

        public string LabelInformation { get; set; }
        public string BasicInformation { get; set; }

        private void btnOpenBarTender_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var filepath = Path.Combine(Environment.CurrentDirectory, "Resource", "DocTemplate", "BarTender101", "毛坯标签" + ".btw");
                var targetpath = Path.Combine(Environment.CurrentDirectory, "Resource", "DocTemplate", "BarTender101", "毛坯标签" + "_temp.btw");
                //复制一下
                if (File.Exists(targetpath))
                {
                    File.Delete(targetpath);
                }
                File.Copy(filepath, targetpath);

                System.Diagnostics.Process.Start(targetpath);
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                NavigationService.Status(ex.Message);
            }
        }

        private void btnCloseIt_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
