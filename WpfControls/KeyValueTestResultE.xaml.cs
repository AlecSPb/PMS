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

namespace WPFControls
{
    /// <summary>
    /// KeyValueTestResult.xaml 的交互逻辑
    /// </summary>
    public partial class KeyValueTestResultE : Window
    {
        public KeyValueTestResultE()
        {
            InitializeComponent();
        }

        public string KeyStrings
        {
            get
            {
                return KeyValueHelper.KeyValuesToStr(DgKeyValues.ItemsSource as List<KeyValue>);
            }
            set
            {
                DgKeyValues.ItemsSource = KeyValueHelper.StrToKeyValues(value);
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            this.Focus();
            this.DialogResult = true;
            this.Close();
        }

        
    }
}
