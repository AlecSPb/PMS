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
    /// DeliverySheetChooseDialog.xaml 的交互逻辑
    /// </summary>
    public partial class DeliverySheetChooseDialog : Window
    {
        public DeliverySheetChooseDialog()
        {
            InitializeComponent();
            DeliverySheetType = "Chinese";
        }

        public string DeliverySheetType { get; set; }
        private void BtnChinese_Click(object sender, RoutedEventArgs e)
        {
            DeliverySheetType = "Chinese";
            this.DialogResult = true;
            Close();
        }

        private void BtnEnglish_Click(object sender, RoutedEventArgs e)
        {
            DeliverySheetType = "English";
            this.DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
