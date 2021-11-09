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

        private void DgItems_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            QuotationItem model = (QuotationItem)e.Row.DataContext;
            if (model != null)
            {
                switch (model.State)
                {
                    case "Deleted":
                        e.Row.Background = Brushes.Gray;
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

        private void BtnTargetCutter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("TargetCutterSimulator.exe");
            }
            catch (Exception)
            {

            }
        }

        private void BtnMaterialNeedCalculator_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var win = new Tools.MaterialNeedCalculationWindow();
                win.Show();
            }
            catch (Exception)
            {

            }
        }

        private void BtnWeightedDensityCalculator_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string wd = XSHelper.XS.File.GetCurrentFolderPath("WDC");
                var process = new System.Diagnostics.Process();
                process.StartInfo.FileName = System.IO.Path.Combine(wd, "WeightedDensityCalculator.exe");
                process.StartInfo.WorkingDirectory = wd;
                var result=process.Start();
            }
            catch (Exception ex)
            {
                XSHelper.XS.MessageBox.ShowError(ex.Message);
            }
        }


    }
}
