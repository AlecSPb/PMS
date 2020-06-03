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
using PMSClient.Components.CscanImageGallery;

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
                excel.UpdateButtonEnable += Excel_UpdateButtonEnable;

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
            }
        }

        private void Excel_UpdateButtonEnable(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(() => BtnChaozhou.IsEnabled = true);
        }

        private void Excel_UpdateProgress(object sender, double e)
        {
            if (e >= 0 && e <= 100)
            {
                this.Dispatcher.Invoke(() =>
                {
                    Pg1.Value = e;
                });
            }
        }

        private void BtnCSCAN_Click(object sender, RoutedEventArgs e)
        {
            XSHelper.XS.MessageBox.ShowInfo("图片本地有缓存会比较快，没有就会到服务器找，速度会慢一些，耐心等待");
            if (!PMSDialogService.ShowYesNo("请问", $"确定要导出230mm靶材绑定面图片集合到桌面吗？"))
            {
                return;
            }
            try
            {

                //年月选择对话框
                //年月选择对话框
                var dialog = new WPFControls.YearDateDailog(-1);
                if (dialog.ShowDialog() == false)
                {
                    return;
                }
                int year_start = dialog.YearStart;
                int month_start = dialog.MonthStart;
                int year_end = dialog.YearEnd;
                int month_end = dialog.MonthEnd;

                var gs = new GalleryService();
                gs.SetParameters(year_start, month_start, year_end, month_end);
                gs.UpdateProgress += Gs_UpdateProgress;
                gs.UpdateButtonEnable += Gs_UpdateButtonEnable;

                var task = new Task(gs.Output);
                task.Start();
                BtnCSCAN.IsEnabled = false;

            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                PMSDialogService.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void Gs_UpdateButtonEnable(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(() => BtnCSCAN.IsEnabled = true);
        }

        private void Gs_UpdateProgress(object sender, double e)
        {
            if (e >= 0 && e <= 100)
            {
                this.Dispatcher.Invoke(() =>
                {
                    Pg2.Value = e;
                });
            }
        }
    }
}
