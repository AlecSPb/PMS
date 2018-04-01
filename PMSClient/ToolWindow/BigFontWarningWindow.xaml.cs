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
    /// BigFontWarningWindow.xaml 的交互逻辑
    /// </summary>
    public partial class BigFontWarningWindow : Window
    {
        public BigFontWarningWindow()
        {
            InitializeComponent();
        }

        public string BasicInformation
        {
            set
            {
                TxtBasic.Text = value;
            }
        }
        public string WarningText
        {
            set
            {
                TxtWarning.Text = value;
            }
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
