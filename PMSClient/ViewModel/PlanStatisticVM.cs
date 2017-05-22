using GalaSoft.MvvmLight.CommandWpf;
using LiveCharts;
using LiveCharts.Wpf;
using PMSClient.StatisticService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.ViewModel
{
    public class PlanStatisticVM : BaseViewModelStatistic
    {
        public PlanStatisticVM()
        {
            Initialize();
            //ActionByYear();
            ActionByMonth();
        }

        private void Initialize()
        {
            ByYear = new RelayCommand(ActionByYear);
            ByMonth = new RelayCommand(ActionByMonth);
            BySeason = new RelayCommand(ActionBySeason);
            ByDevice = new RelayCommand(ActionByDevice);
        }

        private void ActionByDevice()
        {
            try
            {
                StatisticChartData.Clear();
                StatisticChartLabels.Clear();
                AxisXTitle = "设备代码";
                AxisYTitle = "热压次数";
                using (var service = new MainStatisticServiceClient())
                {
                    var result = service.GetPlanStatisticByDevice(CurrentYear);
                    if (result.Count() == 0)
                    {
                        PMSDialogService.ShowYes("没有记录");
                        return;
                    }
                    var tempValues = new ChartValues<int>();
                    var tempLabels = new List<string>();
                    var sb = new StringBuilder();
                    sb.AppendLine($"{CurrentYear}-按设备统计结果:");
                    tempValues.Clear();
                    tempLabels.Clear();
                    result.ToList().ForEach(i =>
                    {
                        tempLabels.Add(i.Key);
                        tempValues.Add((int)i.Value);

                        sb.AppendLine($"[{CurrentYear}-设备{i.Key}]，共热压{i.Value}次");

                    });
                    var series = new ColumnSeries();
                    series.Title = "热压次数";
                    series.Values = tempValues;
                    StatisticChartData.Add(series);
                    tempLabels.ForEach(i => StatisticChartLabels.Add(i));

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
            try
            {
                StatisticChartData.Clear();
                StatisticChartLabels.Clear();
                AxisXTitle = $"{CurrentYear}的季度";
                AxisYTitle = "热压次数";
                using (var service = new MainStatisticServiceClient())
                {
                    var result = service.GetPlanStatisticBySeason(CurrentYear);
                    if (result.Count() == 0)
                    {
                        PMSDialogService.ShowYes("没有记录");
                        return;
                    }
                    var tempValues = new ChartValues<int>();
                    var tempLabels = new List<string>();
                    var sb = new StringBuilder();
                    sb.AppendLine("按季度统计结果:");
                    tempValues.Clear();
                    tempLabels.Clear();
                    result.ToList().ForEach(i =>
                    {
                        tempLabels.Add(i.Key);
                        tempValues.Add((int)i.Value);

                        sb.AppendLine($"[{CurrentYear}第{i.Key}季度]，共热压{i.Value}次");

                    });
                    var series = new ColumnSeries();
                    series.Title = "热压次数";
                    series.Values = tempValues;
                    StatisticChartData.Add(series);
                    tempLabels.ForEach(i => StatisticChartLabels.Add(i));

                    StatisticTextData = sb.ToString();
                }

            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private void ActionByMonth()
        {
            try
            {
                StatisticChartData.Clear();
                StatisticChartLabels.Clear();
                AxisXTitle = $"{CurrentYear}的月份";
                AxisYTitle = "热压次数";
                using (var service = new MainStatisticServiceClient())
                {
                    var result = service.GetPlanStatisticByMonth(CurrentYear);
                    if (result.Count() == 0)
                    {
                        PMSDialogService.ShowYes("没有记录");
                        return;
                    }
                    var tempValues = new ChartValues<int>();
                    var tempLabels = new List<string>();
                    var sb = new StringBuilder();
                    sb.AppendLine($"{CurrentYear}-按月份统计结果:");
                    tempValues.Clear();
                    tempLabels.Clear();
                    result.ToList().ForEach(i =>
                    {
                        tempLabels.Add(i.Key);
                        tempValues.Add((int)i.Value);

                        sb.AppendLine($"[{CurrentYear}-{i.Key}]，共热压{i.Value}次");

                    });
                    var series = new ColumnSeries();
                    series.Title = "热压次数";
                    series.Values = tempValues;
                    StatisticChartData.Add(series);
                    tempLabels.ForEach(i => StatisticChartLabels.Add(i));

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
                AxisXTitle = $"全部年份,开始于{FirstYear}";
                AxisYTitle = "热压次数";
                using (var service = new MainStatisticServiceClient())
                {
                    var result = service.GetPlanStatisticByYear();
                    if (result.Count() == 0)
                    {
                        PMSDialogService.ShowYes("没有记录");
                        return;
                    }
                    var tempValues = new ChartValues<int>();
                    var tempLabels = new List<string>();
                    var sb = new StringBuilder();
                    sb.AppendLine("全部按年份统计结果:");
                    tempValues.Clear();
                    tempLabels.Clear();
                    result.ToList().ForEach(i =>
                    {
                        tempLabels.Add(i.Key);
                        tempValues.Add((int)i.Value);

                        sb.AppendLine($"[{i.Key}]，共热压{i.Value}次");

                    });
                    var series = new ColumnSeries();
                    series.Title = "热压次数";
                    series.Values = tempValues;
                    StatisticChartData.Add(series);
                    tempLabels.ForEach(i => StatisticChartLabels.Add(i));

                    StatisticTextData = sb.ToString();
                }

            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }

        }



        public RelayCommand ByDevice { get; set; }
    }
}
