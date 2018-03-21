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
    /// CompositionSimulator.xaml 的交互逻辑
    /// </summary>
    public partial class CompositionSimulator : Window
    {
        public CompositionSimulator()
        {
            InitializeComponent();
        }

        public event EventHandler<string> FillIn;



        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string input = txtCondition.Text;
                if (string.IsNullOrEmpty(input)) return;
                CompositionSimulatorHelper helper = new CompositionSimulatorHelper();
                string csv = helper.SimulateCompositionToCsvFormat(input);
                txtCsv.Text = csv;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnFill_Click(object sender, RoutedEventArgs e)
        {
            //trigger event
            OnFillIn();
        }

        private void OnFillIn()
        {
            string csv = txtCsv.Text;
            if (string.IsNullOrEmpty(csv))
                return;
            //if (FillIn != null)
            //    FillIn(this, csv);
            FillIn?.Invoke(this, csv);
        }
    }
}
