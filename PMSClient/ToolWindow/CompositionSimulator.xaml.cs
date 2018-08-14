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

        private void KeepTop_Click(object sender, RoutedEventArgs e)
        {
            this.Topmost = (bool)KeepTop.IsChecked;
        }

        private void StackPanel_Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.OriginalSource as Button;
            if (btn == null) return;
            switch (btn.Content.ToString())
            {
                case "CIGS":
                    txtCondition.Text = "5\r\nCu+22.8\r\nIn+20\r\nGa+7\r\nSe+50.2";
                    break;
                case "InSe":
                    txtCondition.Text = "5\r\nIn+2\r\nSe+3";
                    break;
                case "CuGaSe":
                    txtCondition.Text = "5\r\nCu+1\r\nGa+1\r\nSe+2";
                    break;
                case "BiTeSe":
                    txtCondition.Text = "5\r\nBi+39\r\nTe+59\r\nSe+2";
                    break;
                case "BiSbTe":
                    txtCondition.Text = "5\r\nBi+9\r\nSb+31\r\nTe+60";
                    break;
                case "SeAsGe":
                    txtCondition.Text = "5\r\nSe+44\r\nAs+33\r\nGe+22";
                    break;
                default:
                    break;
            }
        }
    }
}
