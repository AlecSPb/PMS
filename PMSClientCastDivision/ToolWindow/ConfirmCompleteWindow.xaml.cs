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
    /// ConfirmCompleteWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ConfirmCompleteWindow : Window
    {
        public ConfirmCompleteWindow()
        {
            InitializeComponent();
        }

        public ConfirmModel Model
        {
            get
            {
                ConfirmModel model = new ConfirmModel()
                {
                    MaterialItemLot = txtMaterialLot.Text,
                    Composition = txtComposition.Text,
                    PMINumber = txtPMINumber.Text,
                    Weight = double.Parse(txtWeight.Text),
                    ActualWeight = double.Parse(txtActualWeight.Text),
                    MeltingPoint = txtMeltingPoint.Text,
                    Remark = txtRemark.Text,
                    MaterialSource = TxtMaterialSource.Text,
                    SupplierPO = txtSupplierPO.Text

                };
                return model;
            }
            set
            {
                txtMaterialLot.Text = value.MaterialItemLot;
                txtComposition.Text = value.Composition;
                txtPMINumber.Text = value.PMINumber;
                txtWeight.Text = value.Weight.ToString();
                txtActualWeight.Text = value.ActualWeight.ToString();
                txtMeltingPoint.Text = value.MeltingPoint;
                txtRemark.Text = value.Remark;
                TxtMaterialSource.Text = value.MaterialSource;
                txtSupplierPO.Text = value.SupplierPO;
            }

        }
        public string SureType { get; set; } = "All";
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnSurePart_Click(object sender, RoutedEventArgs e)
        {
            SureType = "Part";
            this.DialogResult = true;
            this.Close();
        }
        private void btnSureAll_Click(object sender, RoutedEventArgs e)
        {
            SureType = "All";
            this.DialogResult = true;
            this.Close();
        }

        private void BtnMaterialSource_Click(object sender, RoutedEventArgs e)
        {
            //string composition = Model.Composition.Replace("pcs", "").Replace("(", "");
            //var elements = Helpers.CompositionHelper.GetElements(composition);
            //if (elements.Count > 0)
            //{
            //    StringBuilder sb = new StringBuilder();
            //    foreach (var item in elements)
            //    {
            //        sb.Append($"{item}=来源;");
            //    }
            //    TxtMaterialSource.Text = $"{TxtMaterialSource.Text}{sb.ToString()}"; ;

            //}
            //else
            //{
            //    TxtMaterialSource.Text = $"{TxtMaterialSource.Text}组分=来源;";
            //}

            var win = new View.RawMaterialSheetWindow();
            double x = this.Left + this.Width;
            double y = this.Top;
            win.Left = x;
            win.Top = y;


            var vm = win.DataContext as ViewModel.RawMaterialSheetWindowVM;
            vm.FillData += Vm_FillData;

            win.ShowDialog();

            vm.FillData -= Vm_FillData;

        }

        private void Vm_FillData(object sender, string e)
        {
            PMSMethods.SetTextBoxInsert(TxtMaterialSource, e);
        }

        private void StackPanel_Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.OriginalSource as Button;
            if (btn != null)
            {
                PMSMethods.SetTextBoxInsert(txtRemark, btn.Content.ToString() + ";");
            }
        }
    }
}
