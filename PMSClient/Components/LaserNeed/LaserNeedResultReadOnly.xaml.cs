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

namespace PMSClient.Components.LaserNeed
{
    /// <summary>
    /// KeyValueTestResult.xaml 的交互逻辑
    /// </summary>
    public partial class LaserNeedResultReadOnly : Window
    {
        public LaserNeedResultReadOnly()
        {
            InitializeComponent();
        }

        public string KeyStrings
        {
            get
            {
                return LaserNeedHelper.LaserNeedToStr(DgKeyValues.ItemsSource as List<LaserNeedModel>);
            }
            set
            {
                DgKeyValues.ItemsSource = LaserNeedHelper.StrToLaserNeed(value);
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
