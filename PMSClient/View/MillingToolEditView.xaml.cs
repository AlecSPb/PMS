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
using PMSClient.ToolService;

namespace PMSClient.View
{
    /// <summary>
    /// FillingToolEditView.xaml 的交互逻辑
    /// </summary>
    public partial class MillingToolEditView : UserControl
    {
        public MillingToolEditView()
        {
            InitializeComponent();
        }

        private void BtnGetMaxSieve_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string s = "";
                
                using (var service = new ToolSieveServiceClient())
                {
                    s = service.GetMaxToolSieveNumber();
                }

                XSHelper.XS.MessageBox.ShowInfo($"筛号从大到小排序为 [{s}]");
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
    }
}
