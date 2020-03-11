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
using PMSClient.ToolWindow;
using PMSCommon;

namespace PMSClient.View
{
    /// <summary>
    /// FailureEditView.xaml 的交互逻辑
    /// </summary>
    public partial class RawMaterialSheetEditView : UserControl
    {
        public RawMaterialSheetEditView()
        {
            InitializeComponent();
        }

        private SingleStringSelector selector;

        private void BtnSelectScrapProblem_Click(object sender, RoutedEventArgs e)
        {
            selector = new SingleStringSelector();
            List<string> ds = new List<string>();
            PMSBasicDataService.SetListDS<string>(CustomData.FailureProblem,ds);
            string stage = CboStages.SelectedItem.ToString();
            //if (!string.IsNullOrEmpty(stage))
            //{
            //    selector.SetDataSource(ds, stage);
            //}
            //else
            //{
            //    selector.SetDataSource(ds);
            //}
            selector.SetDataSource(ds);
            if (selector.ShowDialog()==true){
                PMSMethods.SetTextBox(TxtProblem, selector.SelectedString);
            }
        }

        private void BtnSelectScrapProcess_Click(object sender, RoutedEventArgs e)
        {
            selector = new SingleStringSelector();
            List<string> ds = new List<string>();
            PMSBasicDataService.SetListDS<string>(CustomData.FailureProcess, ds);
            selector.SetDataSource(ds);
            if (selector.ShowDialog() == true)
            {
                PMSMethods.SetTextBox(TxtProcess, selector.SelectedString);
            }
        }
    }
}
