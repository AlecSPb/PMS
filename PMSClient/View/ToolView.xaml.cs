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

namespace PMSClient.View
{
    /// <summary>
    /// OutputView.xaml 的交互逻辑
    /// </summary>
    public partial class ToolView : UserControl
    {
        public ToolView()
        {
            InitializeComponent();
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoTo(PMSViews.Navigation);
        }

        private void btnCompositionToOneWindow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CompositionToOne window = new CompositionToOne();
                window.Show();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private void btnPressureTransmissionWindow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PressureChangeTool window = new PressureChangeTool();
                window.Show();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }


    }
}
