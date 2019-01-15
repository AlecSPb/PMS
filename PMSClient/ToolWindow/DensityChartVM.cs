using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LiveCharts;
using LiveCharts.Wpf;
using PMSClient.MainService;
using PMSClient.ViewModel.Model;

namespace PMSClient.ToolWindow
{
    public class DensityChartVM : ViewModelBase
    {
        public DensityChartVM()
        {
            YMin = 5.4;
            YMax = 5.8;
            ReferenceDensity = 5.65;
            XAxisTitle = "Target";
            YAxisTitle = "Density";



            Refresh = new RelayCommand(ActionRefresh);
            DataSeries = new SeriesCollection();
        }

        private void ActionRefresh()
        {
            Show();
        }

        private List<double> densities = new List<double>();
        private List<string> labels = new List<string>();

        public void DisplayRecord(List<RecordTestExtra> tests)
        {
            densities.Clear();
            labels.Clear();
            double temp;
            foreach (var item in tests)
            {
                labels.Add(item.RecordTest.ProductID);
                if (double.TryParse(item.RecordTest.Density, out temp))
                {
                    densities.Add(temp)
;
                }
                else
                {
                    densities.Add(0);
                }
            }

            Show();
        }


        private void Show()
        {
            List<double> average_line = new List<double>();
            ChartValues<double> DensityData = new ChartValues<double>();
            DensityData.AddRange(densities);

            ChartValues<double> ReferenceDensityData = new ChartValues<double>();
            int count = densities.Count();
            for (int i = 0; i < count; i++)
            {
                ReferenceDensityData.Add(ReferenceDensity);
            }

            var line_series = new LineSeries();
            line_series.Title = "参考";
            line_series.Values = ReferenceDensityData;
            line_series.PointGeometry = null;

            var column_series = new ColumnSeries();
            column_series.Title = "实际";
            column_series.Values = DensityData;
            column_series.DataLabels = true;
            column_series.LabelPoint = p => p.Y.ToString("F2");

            DataSeries.Clear();
            DataSeries.Add(column_series);
            DataSeries.Add(line_series);

            XLabels = labels.ToArray();
        }

        public SeriesCollection DataSeries { get; set; }
        public string XAxisTitle { get; set; }
        public string YAxisTitle { get; set; }
        public string[] XLabels { get; set; }

        private double referenceDensity;
        public double ReferenceDensity
        {
            get
            {
                return referenceDensity;
            }
            set
            {
                referenceDensity = value;
                RaisePropertyChanged(nameof(ReferenceDensity));
            }
        }
        private double yMin;
        public double YMin
        {
            get
            {
                return yMin;
            }
            set
            {
                yMin = value;
                RaisePropertyChanged(nameof(YMin));
            }
        }

        private double yMax;
        public double YMax
        {
            get
            {
                return yMax;
            }
            set
            {
                yMax = value;
                RaisePropertyChanged(nameof(YMax));
            }
        }


        public RelayCommand Refresh { get; set; }
    }
}
