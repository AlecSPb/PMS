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

namespace PMSClient.View
{
    /// <summary>
    /// MaterialNeedEditView.xaml 的交互逻辑
    /// </summary>
    public partial class MaterialOrderItemEditView : UserControl
    {
        public MaterialOrderItemEditView()
        {
            InitializeComponent();
        }

        private void Tool_Fill(object sender, ToolWindow.MaterialPriceToolArgs e)
        {
            try
            {
                PMSMethods.SetTextBoxAppend(TxtProvideMaterial, e.ProvideMaterial);
                double temp;
                double.TryParse(TxtMaterialPrice.Text, out temp);
                temp += e.TotalPrice;

                PMSMethods.SetTextBox(TxtMaterialPrice, temp.ToString("F2"));

            }
            catch (Exception)
            {

            }

        }

        private void BtnDefaultMemo_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBox(TxtRemark, "第1次补料");
        }

        private void BtnReOrderReason_Click(object sender, RoutedEventArgs e)
        {
            var tool = new ToolWindow.ReOrderReason();
            tool.ShowDialog();
            if (!string.IsNullOrEmpty(tool.WindowContent))
            {
                PMSMethods.SetTextBoxAppend(TxtRemark, tool.WindowContent);
            }
        }

        private void BtnProvideRawMaterial_Click(object sender, RoutedEventArgs e)
        {
            if (TxtProvideMaterial == null) return;
            var dialog = new Components.MaterialOrder.SimpleMaterialResult();
            dialog.KeyStrings = TxtProvideMaterial.Text.Trim();
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                PMSMethods.SetTextBox(TxtProvideMaterial, dialog.KeyStrings);
            }
        }

        private void BtnMaterialPrice_Click(object sender, RoutedEventArgs e)
        {
            double cost = Components.MaterialOrder.SimpleMaterialHelper.GetAllMaterialPrice(TxtProvideMaterial.Text.Trim());
            if (cost != 0)
            {
                PMSMethods.SetTextBox(TxtMaterialPrice, cost.ToString("F2"));
            }
        }

        private void BtnGetElementFromComposition_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtComposition.Text)) return;
            bool useDB = (bool)ChkUseCurrentPriceDB.IsChecked;
            string s = Components.MaterialOrder.SimpleMaterialHelper.GetMaterialStrFromComposition(TxtComposition.Text, useDB);
            if (!string.IsNullOrEmpty(s))
            {
                PMSMethods.SetTextBox(TxtProvideMaterial, s);
            }
        }
    }
}
