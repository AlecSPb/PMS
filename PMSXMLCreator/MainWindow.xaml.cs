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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PMSXMLCreator
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void BtnGDMS_Click(object sender, RoutedEventArgs e)
        {
            SetKeyValue(TxtGDMS);
        }

        private void SetKeyValue(TextBox textBox)
        {
            if (textBox == null) return;
            var dialog = new WPFControls.KeyValueTestResult();
            dialog.KeyStrings =textBox.Text.Trim();
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                Service.PMSMethods.SetTextBox(textBox, dialog.KeyStrings);
            }
        }
        private void BtnVPI_Click(object sender, RoutedEventArgs e)
        {
            SetKeyValue(TxtVPI);

        }

        private void BtnXRF_Click(object sender, RoutedEventArgs e)
        {
            SetKeyValue(TxtXRF);
        }

        private void BtnMoreSpec_Click(object sender, RoutedEventArgs e)
        {
            SetKeyValue(TxtMoreSpec);
        }
    }
}
