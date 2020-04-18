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
    /// PlainTextWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PlainTextWindow : Window
    {
        public PlainTextWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 内容设置
        /// </summary>
        public string ContentText
        {
            set
            {
                RtxtContent.Text = value;
            }
        }


        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
