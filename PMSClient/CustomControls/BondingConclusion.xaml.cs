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
    /// BondingConclusion.xaml 的交互逻辑
    /// </summary>
    public partial class BondingConclusion : Window
    {
        public BondingConclusion()
        {
            InitializeComponent();
            var ds = new string[] { "完成", "暂停", "失败" };
            cboState.ItemsSource = ds;
            cboState.SelectedIndex = 0;
        }
        public string State { get { return cboState.SelectedItem.ToString(); } }
        public string PlateNumber { get { return txtPlateNumber.Text; } }
        public string Defects { get { return txtDefects.Text; } }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {

            this.DialogResult = true;
            this.Close();
        }
    }
}
