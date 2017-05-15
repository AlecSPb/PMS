using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LiveCharts;
using System.Collections.ObjectModel;

namespace PMSClient.ViewModel
{
    public class BaseViewModelStatistic:ViewModelBase
    {
        public BaseViewModelStatistic()
        {
            Years = new List<int>();
            Years.Clear();
            FirstYear = 2011;
            for (int i = 0; i < 30; i++)
            {
                Years.Add(FirstYear + i);
            }
            CurrentYear = DateTime.Now.Year;

            StatisticChartData = new SeriesCollection();
            StatisticChartLabels = new ObservableCollection<string>();

            GoToNavigation = new RelayCommand(() => NavigationService.GoTo(PMSViews.Navigation));
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
        public int FirstYear { get; set; }

        public RelayCommand GoToNavigation { get; private set; }
        public RelayCommand ByYear { get; set; }
        public RelayCommand ByMonth { get; set; }
        public RelayCommand BySeason { get; set; }



    }
}
