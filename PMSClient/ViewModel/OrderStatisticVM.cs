using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.StatisticService;
using System.Collections.ObjectModel;
using LiveCharts;
using LiveCharts.Wpf;

namespace PMSClient.ViewModel
{

    public class OrderStatisticVM : BaseViewModelStatistic
    {
        public OrderStatisticVM()
        {
            Initialize();
            ActionByYear();
        }

        private void Initialize()
        {
            ByYear = new RelayCommand(ActionByYear);
            ByMonth = new RelayCommand(ActionByMonth);
            BySeason = new RelayCommand(ActionBySeason);
            ByCustomer = new RelayCommand(ActionByCustomer);
        }

        private void ActionByCustomer()
        {
            try
            {
                StatisticChartData.Clear();
                StatisticChartLabels.Clear();
                AxisXTitle = $"{CurrentYear}-客户";
                AxisYTitle = "数量";
                using (var service = new MainStatisticServiceClient())
                {
                    var result = service.GetOrderStatisticByCustomer(CurrentYear);
                    if (result.Count() == 0)
                    {
                        PMSDialogService.ShowYes("该年没有记录");
                        return;
                    }
                    var ordeByCustomer = new ChartValues<int>();
                    var labelByCustomer = new List<string>();
                    var sb = new StringBuilder();
                    sb.AppendLine("按客户统计结果:");
                    ordeByCustomer.Clear();
                    labelByCustomer.Clear();
                    result.ToList().ForEach(i =>
                    {
                        labelByCustomer.Add(i.Key);
                        ordeByCustomer.Add((int)i.Value);

                        sb.AppendLine($"[{CurrentYear}-{i.Key}]，共有{i.Value}个订单");

                    });
                    var series = new ColumnSeries();
                    series.Title = "订单数";
                    series.Values = ordeByCustomer;
                    StatisticChartData.Add(series);
                    labelByCustomer.ForEach(i => StatisticChartLabels.Add(i));

                    StatisticTextData = sb.ToString();
                }

            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }

        }

        private void ActionBySeason()
        {

        }

        private void ActionByMonth()
        {
            try
            {
                StatisticChartData.Clear();
                StatisticChartLabels.Clear();
                AxisXTitle = $"{CurrentYear}的月份";
                AxisYTitle = "数量";
                using (var service = new MainStatisticServiceClient())
                {
                    var result = service.GetOrderStatisticByMonth(CurrentYear);
                    if (result.Count() == 0)
                    {
                        PMSDialogService.ShowYes("该年没有记录");
                        return;
                    }
                    var orderByMonth = new ChartValues<int>();
                    var labelByMonth = new List<string>();
                    var sb = new StringBuilder();
                    sb.AppendLine("按月份统计结果:");
                    orderByMonth.Clear();
                    labelByMonth.Clear();
                    result.ToList().ForEach(i =>
                    {
                        labelByMonth.Add(i.Key);
                        orderByMonth.Add((int)i.Value);

                        sb.AppendLine($"[{CurrentYear}-{i.Key}]，共有{i.Value}个订单");

                    });
                    var series = new ColumnSeries();
                    series.Title = "订单数-柱状";
                    series.Values = orderByMonth;
                    StatisticChartData.Add(series);

                    var seriesLine = new LineSeries();
                    seriesLine.Title = "订单数-曲线";
                    seriesLine.Values = orderByMonth;
                    StatisticChartData.Add(seriesLine);

                    labelByMonth.ForEach(i => StatisticChartLabels.Add(i));

                    StatisticTextData = sb.ToString();
                }

            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private void ActionByYear()
        {
            try
            {
                StatisticChartData.Clear();
                StatisticChartLabels.Clear();
                AxisXTitle = $"年份,开始于{FirstYear}";
                AxisYTitle = "数量";
                using (var service = new MainStatisticServiceClient())
                {
                    var result = service.GetOrderStatisticByYear();
                    if (result.Count() == 0)
                    {
                        PMSDialogService.ShowYes("没有记录");
                        return;
                    }
                    var orderByYear = new ChartValues<int>();
                    var labelByYear = new List<string>();
                    var sb = new StringBuilder();
                    sb.AppendLine("全部按年份统计结果:");
                    orderByYear.Clear();
                    labelByYear.Clear();
                    result.ToList().ForEach(i =>
                    {
                        labelByYear.Add(i.Key);
                        orderByYear.Add((int)i.Value);

                        sb.AppendLine($"[{i.Key}]年，共有{i.Value}个订单");

                    });
                    var seriesColumn = new ColumnSeries();
                    seriesColumn.Title = "订单数-柱状";
                    seriesColumn.Values = orderByYear;
                    StatisticChartData.Add(seriesColumn);

                    var seriesLine = new LineSeries();
                    seriesLine.Title = "订单数-曲线";
                    seriesLine.Values = orderByYear;
                    StatisticChartData.Add(seriesLine);

                    labelByYear.ForEach(i => StatisticChartLabels.Add(i));
                    StatisticTextData = sb.ToString();
                }

            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        public RelayCommand ByCustomer { get; set; }
    }
}
