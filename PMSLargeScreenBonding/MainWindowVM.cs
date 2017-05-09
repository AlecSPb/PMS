using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using PMSLargeScreenBonding.LargeScreenService;
using System.Timers;

namespace PMSLargeScreenBonding
{
    public class MainWindowVM : ViewModelBase
    {
        private Timer _Loadtimer;

        private double IntervalLoadDataTime;
        public MainWindowVM()
        {
            currentDate = DateTime.Now;
            finishedCount = 0;
            status1 = status3 = "状态栏";
            status2 = "只显示最近未完成的16条记录";
            RecordBondings = new ObservableCollection<DcRecordBonding>();


            IntervalLoadDataTime = 60000;
            _Loadtimer = new Timer();
            _Loadtimer.Interval = IntervalLoadDataTime;
            _Loadtimer.Elapsed += _Loadtimer_Elapsed;
            _Loadtimer.Start();


            CenterMessage = $"准备数据中，请等待，{IntervalLoadDataTime / 1000}s后显示";
        }

        private int pageIndex = 0;
        private int PageSize = 8;
        private int dataCount = 0;
        private void _Loadtimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                using (var service=new LargeScreenServiceClient())
                {
                    dataCount = service.GetBondingUnCompleteCount();
                }
                int skip = pageIndex * PageSize;
                int take = PageSize;

                Status2 = $"每次显示{PageSize}条，这是第{pageIndex+1}页，共{dataCount}条数据";
                LoadData(skip,take);

                pageIndex++;
                if (pageIndex*PageSize>dataCount)
                {
                    pageIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Status3 = ex.Message;
            }
        }

        private void LoadData(int skip,int take)
        {
            try
            {
                CurrentDate = DateTime.Now;
                DcRecordBonding[] result;
                using (var service = new LargeScreenServiceClient())
                {
                    result = service.GetBondingUnComplete(skip, take);
                    FinishedCount = (int)service.GetBondingCompleteStatistic()[0].Value;
                }
                if (result.Count() == 0)
                {
                    CenterMessage = "今日没有绑定计划";
                    return;
                }
                CenterMessage = "";
                App.Current.Dispatcher.Invoke(() =>
                {
                    RecordBondings.Clear();
                    result.ToList().ForEach(i => RecordBondings.Add(i));
                });

                Status1 = $"全部数据刷新于{DateTime.Now.ToString("HH:mm:ss")}";
            }
            catch (Exception ex)
            {
                Status3 = ex.Message;
            }
        }

        public ObservableCollection<DcRecordBonding> RecordBondings { get; set; }
        private DateTime currentDate;

        public DateTime CurrentDate
        {
            get { return currentDate; }
            set { currentDate = value; RaisePropertyChanged(nameof(CurrentDate)); }
        }

        private int finishedCount;

        public int FinishedCount
        {
            get { return finishedCount; }
            set { finishedCount = value; RaisePropertyChanged(nameof(FinishedCount)); }
        }

        private string centerMessage;

        public string CenterMessage
        {
            get { return centerMessage; }
            set { centerMessage = value; RaisePropertyChanged(nameof(CenterMessage)); }
        }



        private string status1;
        public string Status1
        {
            get { return status1; }
            set { status1 = value; RaisePropertyChanged(nameof(Status1)); }
        }

        private string status2;
        public string Status2
        {
            get { return status2; }
            set { status2 = value; RaisePropertyChanged(nameof(Status2)); }
        }

        private string status3;
        public string Status3
        {
            get { return status3; }
            set { status3 = value; RaisePropertyChanged(nameof(Status3)); }
        }
        public double IntervalRefreshDataTime
        {
            get
            {
                return IntervalLoadDataTime / 1000;
            }
        }
    }
}
