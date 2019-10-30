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

namespace PMSClient.ToolWindow
{
    /// <summary>
    /// ExpressSetting.xaml 的交互逻辑
    /// </summary>
    public partial class ExpressSetting : Window
    {
        public ExpressSetting()
        {
            InitializeComponent();

            string sender = Properties.Settings.Default.ExpressSender;
            string senderphone = Properties.Settings.Default.ExpressSenderPhone;
            TxtSender.Text = sender;
            TxtSenderPhone.Text = senderphone;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ExpressSender = TxtSender.Text.Trim();
            Properties.Settings.Default.ExpressSenderPhone = TxtSenderPhone.Text.Trim();
            Properties.Settings.Default.Save();

            this.Close();
        }
    }
}
