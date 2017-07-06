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

namespace PMSClient.CustomControls
{
    /// <summary>
    /// PlateNumber.xaml 的交互逻辑
    /// </summary>
    public partial class PlateNumber : Window
    {
        public PlateNumber()
        {
            InitializeComponent();

            int[] numbers = new int[50];
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = i + 1;
            }
            cboNumbers.ItemsSource = numbers;
            cboNumbers.SelectedItem = 1;
            CurrentPlateNumber = 1;
        }

        public int CurrentPlateNumber { get; set; }
        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            CurrentPlateNumber = (int)cboNumbers.SelectedItem;
            this.Close();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            CurrentPlateNumber = 1;
            this.Close();
        }
    }
}
