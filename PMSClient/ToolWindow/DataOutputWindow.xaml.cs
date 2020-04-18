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

                //年月选择对话框
                //年月选择对话框
                var dialog = new WPFControls.YearDateDailog(-3);
                if (dialog.ShowDialog() == false)
                {
                    return;
                }
                int year_start = dialog.YearStart;
                int month_start = dialog.MonthStart;
                int year_end = dialog.YearEnd;
                int month_end = dialog.MonthEnd;


                var excel = new ExcelOutputHelper.ExcelOutputSpecialFor230();
                excel.Intialize("瑞典潮州_230靶材_发货+测试+绑定数据", "Data", 50);
                excel.SetParameter(year_start, month_start, year_end, month_end);
                excel.UpdateProgress += Excel_UpdateProgress;

                var task = new Task(excel.Output);
                task.Start();
                BtnChaozhou.IsEnabled = false;
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                PMSDialogService.Show(ex.Message);
            }
            finally
            {
                BtnChaozhou.IsEnabled = true;
            }
        }

        private void Excel_UpdateProgress(object sender, double e)
        {
            if (e >= 0 && e <= 100)
            {
                this.Dispatcher.Invoke(() =>
                {
                    Pg.Value = e;
                });
            }
        }
    }
}
