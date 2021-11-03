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
using PMSQuotation.Models;

namespace PMSQuotation
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowVM();
        }

        private void mainDg_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            Quotation model = (Quotation)e.Row.DataContext;
            if (model != null)
            {
                switch (model.State)
                {
                    case "Deleted":
                        e.Row.Background = Brushes.Gray;
                        break;
                    case "UnFinished":
                        e.Row.Background = Brushes.Yellow;
                        break;
                    case "Ordered":
                        e.Row.Background = Brushes.LightGreen;
                        break;
                    default:
                        break;
                }
            }
        }

        private void BtnCalculator_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("calc.exe");
            }
            catch (Exception)
            {

            }
        }

        private void BtnNotepad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("notepad.exe");
            }
            catch (Exception)
            {

            }
        }
    }
}
