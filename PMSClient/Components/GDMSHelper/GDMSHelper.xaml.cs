using System;
using System.Collections.Generic;
using System.IO;
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

namespace PMSClient.Components.GDMSHelper
{
    /// <summary>
    /// GDMSHelper.xaml 的交互逻辑
    /// </summary>
    public partial class GDMSHelper : Window
    {
        public GDMSHelper()
        {
            InitializeComponent();
            Read();
        }

        public string GDMS
        {
            get
            {
                return TxtOutput.Text.Trim();
            }
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void BtnAnlysis_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var service = new GDMSAnlysis();
                TxtOutput.Text = service.Run(TxtInput.Text.Trim());
            }
            catch (Exception ex)
            {
                PMSDialogService.ShowWarning("处理出错，格式有问题,请修改后再试");
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Write();
        }

        private string filePath = System.IO.Path.Combine(Environment.CurrentDirectory,
                                    "Temp", "GDMSHelper.txt");
        private void Read()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string txt = File.ReadAllText(filePath);
                    TxtInput.Text = txt;
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private void Write()
        {
            try
            {

                File.WriteAllText(filePath, TxtInput.Text.Trim());
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
    }
}
