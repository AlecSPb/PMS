using PMSDesktopClient.PMSMainService;
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

namespace PMSDesktopClient.View
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
            var selectSupplier = cboSuppliers.SelectedItem as DcBDSupplier;
            if (selectSupplier!=null)
            {
                txtSupplier.Text = selectSupplier.SupplierName;
                txtAbbr.Text = selectSupplier.Abbr;
                txtReceiver.Text = selectSupplier.ContactPerson;
                txtEmail.Text = selectSupplier.Email;
                txtAddress.Text = selectSupplier.Address;
            }
        }
    }
}
