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
    /// DataOutputWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DataOutputWindow : Window
    {
        public DataOutputWindow()
        {
            InitializeComponent();
        }

        private void BtnChaozhou_Click(object sender, RoutedEventArgs e)
        {
            if (!PMSDialogService.ShowYesNo("请问", $"确定要导出【{BtnChaozhou.Content}】数据吗？"))
            {
                return;
            }
            try
            {
                var excel = new ExcelOutputHelper.ExcelOutputSpecialFor230();

                excel.Intialize("瑞典潮州230靶材 发货+测试+绑定数据", "Data");//每页数据设置小一点，防止通信出错
                excel.Output();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
    }
}
