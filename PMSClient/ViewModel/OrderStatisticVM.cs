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

    public class OrderStatisticVM : BaseViewModel
    {
        public OrderStatisticVM()
        {
            Initialize();
            GetOrderStatisticByYear();
        }

        private int firstYear;

        private void Initialize()
        {
            Years = new List<int>();
            Years.Clear();
            firstYear = 2011;
            for (int i = 0; i < 30; i++)
            {
                Years.Add(firstYear + i);
            }
            CurrentYear = DateTime.Now.Year;

            StatisticChartData = new SeriesCollection();
            StatisticChartLabels = new ObservableCollection<string>();
            AxisXTitle = $"年份,开始于{firstYear}";
            AxisYTitle = "数量";

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
                    var ordeByMonth = new ChartValues<int>();
                    var labelByMonth = new List<string>();
                    var sb = new StringBuilder();
                    ordeByMonth.Clear();
                    labelByMonth.Clear();
                    result.ToList().ForEach(i =>
                    {
                        labelByMonth.Add(i.Key);
                        ordeByMonth.Add((int)i.Value);

                        sb.AppendLine($"[{CurrentYear}-{i.Key}]，共有{i.Value}个订单");

                    });
                    var series = new ColumnSeries();
                    series.Title = "订单数";
                    series.Values = ordeByMonth;
                    StatisticChartData.Add(series);
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
            GetOrderStatisticByYear();
        }

        private void GetOrderStatisticByYear()
        {
            try
            {
                StatisticChartData.Clear();
                StatisticChartLabels.Clear();
                AxisXTitle = $"年份,开始于{firstYear}";
                AxisYTitle = "数量";
                using (var service = new MainStatisticServiceClient())
                {
                    var result = service.GetOrderStatisticByYear();
                    if (result.Count() == 0)
                    {
                        PMSDialogService.ShowYes("没有记录");
                        return;
                    }
                    var ordeByYear = new ChartValues<int>();
                    var labelByYear = new List<string>();
                    var sb = new StringBuilder();
                    ordeByYear.Clear();
                    labelByYear.Clear();
                    result.ToList().ForEach(i =>
                    {
                        labelByYear.Add(i.Key);
                        ordeByYear.Add((int)i.Value);

                        sb.AppendLine($"[{i.Key}]年，共有{i.Value}个订单");

                    });
                    var series = new ColumnSeries();
                    series.Title = "订单数";
                    series.Values = ordeByYear;
                    StatisticChartData.Add(series);
                    labelByYear.ForEach(i => StatisticChartLabels.Add(i));
                    StatisticTextData = sb.ToString();
                }

            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        public List<int> Years { get; set; }
        private int currentYear;

        public int CurrentYear
        {
            get { return currentYear; }
            set { currentYear = value; RaisePropertyChanged(nameof(CurrentYear)); }
        }

        private string axisXTitle;

        public string AxisXTitle
        {
            get { return axisXTitle; }
            set { axisXTitle = value; RaisePropertyChanged(nameof(AxisXTitle)); }
        }

        private string axisYTitle;
        public string AxisYTitle
        {
            get { return axisYTitle; }
            set { axisYTitle = value; RaisePropertyChanged(nameof(AxisYTitle)); }
        }

        public SeriesCollection StatisticChartData { get; set; }
        public ObservableCollection<string> StatisticChartLabels { get; set; }

        private string statisticTextData;

        public string StatisticTextData
        {
            get { return statisticTextData; }
            set { statisticTextData = value; RaisePropertyChanged(nameof(StatisticTextData)); }
        }
        public RelayCommand ByYear { get; set; }
        public RelayCommand ByMonth { get; set; }
        public RelayCommand BySeason { get; set; }
        public RelayCommand ByCustomer { get; set; }
    }
}
