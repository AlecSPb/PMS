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

    public class OrderStatisticVM:ViewModelBase
    {
        public OrderStatisticVM()
        {
            OrderSeries = new SeriesCollection();
            GetStatisticData();
        }

        private void GetStatisticData()
        {
            try
            {
                OrderSeries.Clear();
                using (var service=new MainStatisticServiceClient())
                {
                    var result = service.GetOrderStatisticByYear();
                   var ordeByYear = new ChartValues<double>();
                    var labelByYear = new List<string>();
                    ordeByYear.Clear();
                    labelByYear.Clear();
                    result.ToList().ForEach(i =>
                    {
                        ordeByYear.Add(i.Value);
                        labelByYear.Add(i.Key);
                    });
                    var colum = new ColumnSeries();
                    colum.Title = "Order by Year";
                    colum.Values = ordeByYear;
                    OrderSeries.Add(colum);
                    Labels = labelByYear.ToArray();
                }

            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        public SeriesCollection OrderSeries { get; set; }
        public string[] Labels { get; set; }
    }
}
