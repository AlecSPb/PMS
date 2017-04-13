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
    public partial class RecordDeliveryItemEditView : UserControl
    {
        public RecordDeliveryItemEditView()
        {
            InitializeComponent();
        }

        private void cboProductTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cbo = sender as ComboBox;
            if (cbo.SelectedItem != null)
            {
                PMSMethods.SetTextBox(txtProductType,cbo.SelectedItem.ToString());
            }
        }

        private void cboGoodPositions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cbo = sender as ComboBox;
            if (cbo.SelectedItem != null)
            {
                PMSMethods.SetTextBox(txtPosition, cbo.SelectedItem.ToString());
            }

        }
    }
}
