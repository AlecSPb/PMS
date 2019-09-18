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

        private void BtnAsist_Click(object sender, RoutedEventArgs e)
        {
            var tool = new ToolWindow.MaterialPriceTool();
            tool.Fill += Tool_Fill;
            tool.Show();
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
    }
}
