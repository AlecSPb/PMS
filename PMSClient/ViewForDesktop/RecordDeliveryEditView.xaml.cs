using PMSClient.MainService;
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

namespace PMSClient.ViewForDesktop
{
    /// <summary>
    /// MaterialOrderEditView.xaml 的交互逻辑
    /// </summary>
    public partial class RecordDeliveryEditView : UserControl
    {
        public RecordDeliveryEditView()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cbo = sender as ComboBox;
            if (cbo.SelectedItem!=null)
            {
                txtCountry.Text = cbo.SelectedItem.ToString();
            }
        }

        private void cboPackageTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cbo = sender as ComboBox;
            if (cbo.SelectedItem!=null)
            {
                txtPackageType.Text = cbo.SelectedItem.ToString();
            }
        }
    }
}
