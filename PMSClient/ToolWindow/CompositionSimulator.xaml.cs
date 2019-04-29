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
            txtCondition.Text = point + composition_cigs;
        }

        public void SetForBridgeLine()
        {
            point = "13\r\n";
            txtCondition.Text = point + composition_sag;
        }

        public event EventHandler<string> FillIn;

        private string point= "5\r\n";
        private string composition_cigs = "Cu+22.8\r\nIn+20\r\nGa+7\r\nSe+50.2";
        private string composition_inse = "In+2\r\nSe+3";
        private string composition_cgs = "Cu+1\r\nGa+1\r\nSe+2";
        private string composition_sag = "Se+44\r\nAs+33\r\nGe+22";
        private string composition_bitese = "Bi+39\r\nTe+59\r\nSe+2";
        private string composition_bisbte = "Bi+9\r\nSb+31\r\nTe+60";

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
                    txtCondition.Text = point+composition_cigs;
                    break;
                case "InSe":
                    txtCondition.Text = point+composition_inse;
                    break;
                case "CuGaSe":
                    txtCondition.Text = point+composition_cgs;
                    break;
                case "BiTeSe":
                    txtCondition.Text = point+composition_bitese;
                    break;
                case "BiSbTe":
                    txtCondition.Text = point+composition_bisbte;
                    break;
                case "SeAsGe":
                    txtCondition.Text = point+composition_sag;
                    break;
                default:
                    break;
            }
        }
    }
}
