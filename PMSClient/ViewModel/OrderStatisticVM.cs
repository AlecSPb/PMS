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
            StatisticChartData = new SeriesCollection();
            Refresh = new RelayCommand(ActionRefresh);
            GetOrderStatisticByYear();
        }

        private void ActionRefresh()
        {
            GetOrderStatisticByYear();
        }

        private void GetOrderStatisticByYear()
        {
            try
            {
                StatisticChartData.Clear();
                using (var service = new MainStatisticServiceClient())
                {
                    var result = service.GetOrderStatisticByYear();
                    var ordeByYear = new ChartValues<double>();
                    var labelByYear = new List<string>();
                    var sb = new StringBuilder();
                    ordeByYear.Clear();
                    labelByYear.Clear();
                    result.ToList().ForEach(i =>
                    {
                        labelByYear.Add(i.Key);
                        ordeByYear.Add(i.Value);

                        sb.AppendLine($"[{i.Key}]年，共有订单{i.Value}个");

                    });
                    var series = new ColumnSeries();
                    series.Title = "订单数";
                    series.Values = ordeByYear;
                    StatisticChartData.Add(series);
                    StatisticChartLabels = labelByYear.ToArray();

                    StatisticTextData = sb.ToString();
                }

            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        public SeriesCollection StatisticChartData { get; set; }
        public string[] StatisticChartLabels { get; set; }

        private string statisticTextData;

        public string StatisticTextData
        {
            get { return statisticTextData; }
            set { statisticTextData = value; RaisePropertyChanged(nameof(StatisticTextData)); }
        }
        public RelayCommand Refresh { get; set; }



    }
}
