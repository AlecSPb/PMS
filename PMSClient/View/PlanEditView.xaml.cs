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
    /// PlanEditView.xaml 的交互逻辑
    /// </summary>
    public partial class PlanEditView : UserControl
    {
        public PlanEditView()
        {
            InitializeComponent();
        }

        //private void CboCompounds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (cboCompounds.SelectedItem is DcBDCompound selectedCompound)
        //    {
        //        PMSMethods.SetTextBox(txtCalculationDensity, selectedCompound.Density.ToString());
        //    }
        //}


        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double diameter = double.Parse(cboMoldDiameter.Text.Trim());
                double thickness = double.Parse(txtThickness.Text);
                double quantity = double.Parse(cboQuantity.SelectedItem.ToString());
                double density = double.Parse(txtCalculationDensity.Text);

                double singleWeight = Math.PI * diameter * diameter * thickness / 4 / 1000 * density;

                PMSMethods.SetTextBox(txtSingleWeight, singleWeight.ToString("F3"));
                PMSMethods.SetTextBox(txtAllWeight, (singleWeight * quantity).ToString("F3"));
            }
            catch (Exception)
            {
            }
        }

        private void BtnPressureTool_Click(object sender, RoutedEventArgs e)
        {
            PMSClient.ToolWindow.PressureChangeTool tool = new ToolWindow.PressureChangeTool();
            tool.ShowDialog();
        }

        private void TxtCompound_Click(object sender, RoutedEventArgs e)
        {
            CompoundWindow win = new View.CompoundWindow();
            win.ShowDialog();
        }

        private void BtnMillingSample_Click(object sender, RoutedEventArgs e)
        {
            CboMillingRequirements.Text += " 取粉末样品;";
        }

        private void BtnNumber_Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.OriginalSource as Button;
            if (btn != null)
            {
                if (btn.Name == "BtnNumber")
                {
                    CboMillingRequirements.Text += " 第片;";
                }
                else
                {
                    CboFillingRequirements.Text += " 第片;";
                }
            }
        }
    }
}
