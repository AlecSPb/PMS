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

namespace PMSClient.ToolDialog
{
    /// <summary>
    /// EnterCSCAN.xaml 的交互逻辑
    /// </summary>
    public partial class EnterCSCAN : Window
    {
        public EnterCSCAN()
        {
            InitializeComponent();
        }

        public string ProductInformation
        {
            set { TxtProductInformation.Text = value; }
        }

        public string CSCAN
        {
            set
            {
                TxtCscan.Text = value;
            }
            get
            {
                return TxtCscan.Text;
            }
        }



        private void StackPanel_Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.OriginalSource as Button;
            string csan_str = CSCAN + " " + btn.Content.ToString();
            CSCAN = csan_str.Replace("无", "").Trim();
            e.Handled = true;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
