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

namespace PMSClient.Components.EOrder
{
    /// <summary>
    /// TextWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TextWindow : Window
    {
        public TextWindow()
        {
            InitializeComponent();
        }

        private List<Order> currentOrders;
        public List<Order> CurrentOrders
        {
            get { return currentOrders; }
            set
            {
                currentOrders = value;
                MIChinse_Click(this, null);
            }
        }

        private void ChkTopMost_Click(object sender, RoutedEventArgs e)
        {
            this.Topmost = !this.Topmost;
        }

        private void MIChinse_Click(object sender, RoutedEventArgs e)
        {
            MainText.Text = "";
            foreach (var item in CurrentOrders)
            {
                var order_str = TextService.GetChineseOrderText(item);
                this.MainText.Text += order_str;
            }

        }

        private void MIEnglish_Click(object sender, RoutedEventArgs e)
        {
            MainText.Text = "";
            foreach (var item in CurrentOrders)
            {
                var order_str = TextService.GetEnglishOrderText(item);
                this.MainText.Text += order_str;
            }

        }
    }
}
