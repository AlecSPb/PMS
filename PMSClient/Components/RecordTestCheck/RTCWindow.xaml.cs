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
using PMSClient.PMSSettings;

namespace PMSClient.Components.RecordTestCheck
{
    /// <summary>
    /// RTCWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RTCWindow : Window
    {
        public RTCWindow()
        {
            InitializeComponent();
            Read();
        }

        private void BtnRead_Click(object sender, RoutedEventArgs e)
        {

            if (XSHelper.XS.MessageBox.ShowYesNo("确定要从服务器读取并替代当前产品ID输入？"))
            {
                Read();
            }
        }

        private void Read()
        {
            try
            {
                using (var s = new PMSSettingServiceClient())
                {
                    var str = s.GetValueByKey("record_check_ids");
                    TxtInput.Text = str;
                }
                UpdateStatus("读取成功");
            }
            catch (Exception)
            {
            }
        }

        private void Save()
        {
            try
            {
                using (var s = new PMSSettingServiceClient())
                {
                    string str = TxtInput.Text;
                    s.UpdateSettings("record_check_ids", str);
                }
                UpdateStatus("保存成功");

            }
            catch (Exception)
            {
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
            XSHelper.XS.MessageBox.ShowInfo("当前输入的产品ID已保存到服务器，可以从其他客户端查看");
        }

        private void BtnGenerate_Click(object sender, RoutedEventArgs e)
        {
            var service = new RTCService();
            service.UpdateStatus += AppendStatus;

            service.Generate(TxtInput.Text.Trim());

        }


        private void UpdateStatus(string msg)
        {
            TxtStatus.Text = msg;
        }

        private void AppendStatus(string msg)
        {
            TxtStatus.Text = $"{msg}\r\n{TxtStatus.Text}";
        }

        private void AppendStatus(object sender, string msg)
        {
            AppendStatus(msg);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Save();
        }
    }
}
