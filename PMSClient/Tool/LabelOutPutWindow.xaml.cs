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

namespace PMSClient.Tool
{
    /// <summary>
    /// LabelOutPutView.xaml 的交互逻辑
    /// </summary>
    public partial class LabelOutPutWindow :Window
    {
        public LabelOutPutWindow()
        {
            InitializeComponent();
        }

        private void BtnLabelTemplate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var filepath = System.IO.Path.Combine(Environment.CurrentDirectory, "Resource", "DocTemplate", "BarTender101", "毛坯标签" + ".btw");
                var targetpath = System.IO.Path.Combine(Environment.CurrentDirectory, "Resource", "DocTemplate", "BarTender101", "毛坯标签" + "_temp.btw");
                //复制一下
                if (System.IO.File.Exists(targetpath))
                {
                    System.IO.File.Delete(targetpath);
                }
                System.IO.File.Copy(filepath, targetpath);

                System.Diagnostics.Process.Start(targetpath);
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                NavigationService.Status(ex.Message);
            }
        }
    }
}
