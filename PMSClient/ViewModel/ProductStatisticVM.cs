using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.StatisticService;
using LiveCharts.Wpf;
using LiveCharts;

namespace PMSClient.ViewModel
{
    public class ProductStatisticVM:BaseViewModelStatistic
    {
        public ProductStatisticVM()
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
            ByProductType = new RelayCommand(ActionByProductType);
        }

        private void ActionByProductType()
        {
            try
            {
                StatisticChartData.Clear();
                StatisticChartLabels.Clear();
                AxisXTitle = $"{CurrentYear}的产品类型";
                AxisYTitle = "数量";
                using (var service = new MainStatisticServiceClient())
                {
                    var result = service.GetProductStatisticByProductType(CurrentYear);
                    if (result.Count() == 0)
                    {
                        PMSDialogService.Show("没有记录");
                        return;
                    }
                    var orderByYear = new ChartValues<int>();
                    var labelByYear = new List<string>();
                    var sb = new StringBuilder();
                    sb.AppendLine("按产品类型统计结果:");
                    orderByYear.Clear();
                    labelByYear.Clear();
                    result.ToList().ForEach(i =>
                    {
                        labelByYear.Add(i.Key);
                        orderByYear.Add((int)i.Value);

                        sb.AppendLine($"[{CurrentYear}-{i.Key}]，共有{i.Value}个产品");

                    });
                    var seriesColumn = new ColumnSeries();
                    seriesColumn.Title = "产品数-柱状";
                    seriesColumn.Values = orderByYear;
                    StatisticChartData.Add(seriesColumn);

                    labelByYear.ForEach(i => StatisticChartLabels.Add(i));
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
                AxisYTitle = "数量";
                using (var service = new MainStatisticServiceClient())
                {
                    var result = service.GetProductStatisticBySeason(CurrentYear);
                    if (result.Count() == 0)
                    {
                        PMSDialogService.Show("没有记录");
                        return;
                    }
                    var orderByYear = new ChartValues<int>();
                    var labelByYear = new List<string>();
                    var sb = new StringBuilder();
                    sb.AppendLine("按季度统计结果:");
                    orderByYear.Clear();
                    labelByYear.Clear();
                    result.ToList().ForEach(i =>
                    {
                        labelByYear.Add(i.Key);
                        orderByYear.Add((int)i.Value);

                        sb.AppendLine($"[{CurrentYear}，第{i.Key}季度]，共有{i.Value}个产品");

                    });
                    var seriesColumn = new ColumnSeries();
                    seriesColumn.Title = "产品数-柱状";
                    seriesColumn.Values = orderByYear;
                    StatisticChartData.Add(seriesColumn);

                    labelByYear.ForEach(i => StatisticChartLabels.Add(i));
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
                AxisYTitle = "数量";
                using (var service = new MainStatisticServiceClient())
                {
                    var result = service.GetProductStatisticByMonth(CurrentYear);
                    if (result.Count() == 0)
                    {
                        PMSDialogService.Show("没有记录");
                        return;
                    }
                    var orderByYear = new ChartValues<int>();
                    var labelByYear = new List<string>();
                    var sb = new StringBuilder();
                    sb.AppendLine("按月份统计结果:");
                    orderByYear.Clear();
                    labelByYear.Clear();
                    result.ToList().ForEach(i =>
                    {
                        labelByYear.Add(i.Key);
                        orderByYear.Add((int)i.Value);

                        sb.AppendLine($"[{CurrentYear}-{i.Key}]，共有{i.Value}个产品");

                    });
                    var seriesColumn = new ColumnSeries();
                    seriesColumn.Title = "产品数-柱状";
                    seriesColumn.Values = orderByYear;
                    StatisticChartData.Add(seriesColumn);

                    labelByYear.ForEach(i => StatisticChartLabels.Add(i));
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
                AxisYTitle = "数量";
                using (var service = new MainStatisticServiceClient())
                {
                    var result = service.GetProductStatisticByYear();
                    if (result.Count() == 0)
                    {
                        PMSDialogService.Show("没有记录");
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

                        sb.AppendLine($"[{i.Key}]年，共有{i.Value}个产品");

                    });
                    var seriesColumn = new ColumnSeries();
                    seriesColumn.Title = "产品数-柱状";
                    seriesColumn.Values = orderByYear;
                    StatisticChartData.Add(seriesColumn);

                    labelByYear.ForEach(i => StatisticChartLabels.Add(i));
                    StatisticTextData = sb.ToString();
                }

            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        public RelayCommand ByProductType { get; set; }
    }
}
