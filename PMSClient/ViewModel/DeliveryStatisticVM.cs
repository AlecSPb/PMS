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
    public class DeliveryStatisticVM : BaseViewModelStatistic
    {
        public DeliveryStatisticVM()
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
            ByCustomer = new RelayCommand(ActionByCustomer);
            ByProductType = new RelayCommand(ActionByProductType);
        }

        private void ActionByProductType()
        {
            try
            {
                StatisticChartData.Clear();
                StatisticChartLabels.Clear();
                AxisXTitle = $"{CurrentYear}的产品类型";
                AxisYTitle = "发货片数";
                using (var service = new MainStatisticServiceClient())
                {
                    var result = service.GetDeliveryStatisticByProductType(CurrentYear);
                    if (result.Count() == 0)
                    {
                        PMSDialogService.Show("没有记录");
                        return;
                    }
                    var tempValues = new ChartValues<int>();
                    var tempLabels = new List<string>();
                    var sb = new StringBuilder();
                    sb.AppendLine("按产品类型统计结果:");
                    tempValues.Clear();
                    tempLabels.Clear();
                    result.ToList().ForEach(i =>
                    {
                        tempLabels.Add(i.Key);
                        tempValues.Add((int)i.Value);

                        sb.AppendLine($"[{CurrentYear}-{i.Key}]，共{i.Value}个");

                    });
                    var series = new ColumnSeries();
                    series.Title = "发货片数";
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

        private void ActionByCustomer()
        {
            try
            {
                StatisticChartData.Clear();
                StatisticChartLabels.Clear();
                AxisXTitle = $"{CurrentYear}的客户";
                AxisYTitle = "发货片数";
                using (var service = new MainStatisticServiceClient())
                {
                    var result = service.GetDeliveryStatisticByCustomer(CurrentYear);
                    if (result.Count() == 0)
                    {
                        PMSDialogService.Show("没有记录");
                        return;
                    }
                    var tempValues = new ChartValues<int>();
                    var tempLabels = new List<string>();
                    var sb = new StringBuilder();
                    sb.AppendLine("按客户统计结果:");
                    tempValues.Clear();
                    tempLabels.Clear();
                    result.ToList().ForEach(i =>
                    {
                        tempLabels.Add(i.Key);
                        tempValues.Add((int)i.Value);

                        sb.AppendLine($"[{CurrentYear}-{i.Key}]，共{i.Value}个");

                    });
                    var series = new ColumnSeries();
                    series.Title = "发货片数";
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
                AxisYTitle = "发货片数";
                using (var service = new MainStatisticServiceClient())
                {
                    var result = service.GetDeliveryStatisticBySeason(CurrentYear);
                    if (result.Count() == 0)
                    {
                        PMSDialogService.Show("没有记录");
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

                        sb.AppendLine($"[{CurrentYear}，第{i.Key}季度]，共{i.Value}个");

                    });
                    var series = new ColumnSeries();
                    series.Title = "发货片数";
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
                AxisYTitle = "发货片数";
                using (var service = new MainStatisticServiceClient())
                {
                    var result = service.GetDeliveryStatisticByMonth(CurrentYear);
                    if (result.Count() == 0)
                    {
                        PMSDialogService.Show("没有记录");
                        return;
                    }
                    var tempValues = new ChartValues<int>();
                    var tempLabels = new List<string>();
                    var sb = new StringBuilder();
                    sb.AppendLine("按月份统计结果:");
                    tempValues.Clear();
                    tempLabels.Clear();
                    result.ToList().ForEach(i =>
                    {
                        tempLabels.Add(i.Key);
                        tempValues.Add((int)i.Value);

                        sb.AppendLine($"[{CurrentYear}-{i.Key}]，共{i.Value}个");

                    });
                    var series = new ColumnSeries();
                    series.Title = "发货片数";
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
                AxisYTitle = "发货片数";
                using (var service = new MainStatisticServiceClient())
                {
                    var result = service.GetDeliveryStatisticByYear();
                    if (result.Count() == 0)
                    {
                        PMSDialogService.Show("没有记录");
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

                        sb.AppendLine($"[{i.Key}]，共{i.Value}个");

                    });
                    var series = new ColumnSeries();
                    series.Title = "发货片数";
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

        public RelayCommand ByProductType { get; set; }
        public RelayCommand ByCustomer { get; set; }


    }
}
