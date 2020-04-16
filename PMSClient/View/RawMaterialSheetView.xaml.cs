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
using PMSClient.Other;

namespace PMSClient.View
{
    /// <summary>
    /// FailureView.xaml 的交互逻辑
    /// </summary>
    public partial class RawMaterialSheetView : UserControl
    {
        public RawMaterialSheetView()
        {
            InitializeComponent();
        }

        private void dg_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var model = (DcRawMaterialSheet)e.Row.DataContext;
            if (model != null)
            {
                switch (model.State)
                {
                    case "耗尽":
                        e.Row.Background = new SolidColorBrush(Colors.LightGray);
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
