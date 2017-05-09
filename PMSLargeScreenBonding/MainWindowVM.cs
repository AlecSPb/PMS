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
        private Timer _timer;
        private double IntervalRefreshData;
        public MainWindowVM()
        {
            currentDate = DateTime.Now;
            finishedCount = 0;
            status1 = status3 = "状态栏";
            status2 = "只显示最近未完成的16条记录";
            RecordBondings = new ObservableCollection<DcRecordBonding>();


            IntervalRefreshData = 10000;

            _timer = new Timer();
            _timer.Interval = IntervalRefreshData;
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();

            CenterMessage = $"准备数据中，请等待，{IntervalRefreshData / 1000}s后显示";
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                CurrentDate = DateTime.Now;

                DcRecordBonding[] result;
                using (var service = new LargeScreenServiceClient())
                {
                    result = service.GetBondingUnComplete();
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

                Status1 = $"数据刷新于{DateTime.Now.ToString("HH:mm:ss")}";
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
                return IntervalRefreshData / 1000;
            }
        }
    }
}
