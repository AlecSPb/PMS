using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NotificationClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            (this.FindResource("story") as Storyboard).Begin();
        }

        private void txtInformation_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtInformation.Text = msg;
        }

        string msg = "会议通知：2016-12-19 13:30 在新会议室举行上一批产品问题解决方案相关的会议 参加人员:习近平 胡锦涛 江泽民 李鹏 邓小平";

    }
}
