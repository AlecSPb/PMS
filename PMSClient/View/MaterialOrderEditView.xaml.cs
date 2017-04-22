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
using PMSClient.BasicService;


namespace PMSClient.View
{
    /// <summary>
    /// MaterialOrderEditView.xaml 的交互逻辑
    /// </summary>
    public partial class MaterialOrderEditView : UserControl
    {
        public MaterialOrderEditView()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectSupplier = cboSuppliers.SelectedItem as DcBDSupplier;
            if (selectSupplier != null)
            {
                PMSMethods.SetTextBox(txtSupplier, selectSupplier.SupplierName);
                PMSMethods.SetTextBox(txtAbbr, selectSupplier.Abbr);
                PMSMethods.SetTextBox(txtReceiver, selectSupplier.ContactPerson);
                PMSMethods.SetTextBox(txtEmail, selectSupplier.Email);
                PMSMethods.SetTextBox(txtAddress, selectSupplier.Address);
            }
        }
    }
}
