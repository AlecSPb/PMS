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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PMSClient.Tool
{
    /// <summary>
    /// BatchOutSourceProduct.xaml 的交互逻辑
    /// </summary>
    public partial class BatchOutSourceProduct : Window
    {
        public BatchOutSourceProduct()
        {
            InitializeComponent();
            txtFirst.Text = DateTime.Now.ToString("yyMMdd");
            List<int> numbers = new List<int>();
            numbers.Clear();
            for (int i = 0; i < 10; i++)
            {
                numbers.Add(i + 1);
            }
            this.cboLast.ItemsSource = numbers;
            cboLast.SelectedItem = 1;


            cboCustomer.ItemsSource = BasicData.Customers;
            cboCustomer.DisplayMemberPath = "CustomerName";
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
