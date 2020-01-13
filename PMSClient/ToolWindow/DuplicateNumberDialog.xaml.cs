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
    /// DuplicateNumberDialog.xaml 的交互逻辑
    /// </summary>
    public partial class DuplicateNumberDialog : Window
    {
        public DuplicateNumberDialog()
        {
            InitializeComponent();

            List<int> quantity = new List<int>();
            for (int i = 1; i <= 10; i++)
            {
                quantity.Add(i);
            }
            CboQuantity.ItemsSource = quantity;
            CboQuantity.SelectedIndex = 0;
        }

        public int Quantity { get; set; } = 0;

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            Quantity = (int)CboQuantity.SelectedItem;
            this.Close();
        }
    }
}
