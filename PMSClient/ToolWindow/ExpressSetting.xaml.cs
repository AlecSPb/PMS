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
using PMSClient.Express;
using PMSClient.PMSSettings;

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

            string sender = ExpressConfigService.ReadKey("sf_sender");
            string senderphone = ExpressConfigService.ReadKey("sf_sender_phone");
            TxtSender.Text = sender;
            TxtSenderPhone.Text = senderphone;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            //Properties.Settings.Default.ExpressSender = TxtSender.Text.Trim();
            //Properties.Settings.Default.ExpressSenderPhone = TxtSenderPhone.Text.Trim();
            //Properties.Settings.Default.Save();
            try
            {
                using (var s = new PMSSettingServiceClient())
                {
                    s.UpdateSettings("sf_sender", TxtSender.Text.Trim());
                    s.UpdateSettings("sf_sender_phone", TxtSenderPhone.Text.Trim());
                }
            }
            catch (Exception)
            {

            }
            this.Close();
        }
    }
}
