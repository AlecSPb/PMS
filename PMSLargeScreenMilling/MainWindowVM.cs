using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSLargeScreen.LargeScreenService;

namespace PMSLargeScreen
{
    public class MainWindowVM : ViewModelBase
    {
        public MainWindowVM()
        {
            Millings = new ObservableCollection<DcRecordMilling>();
            PlanExtras = new ObservableCollection<DcPlanExtra>();
            CenterMessage = $"准备数据中，请等待，{IntervalLoadData / 1000}s后显示";
            ErrorMessage = "";
            Status = "初始化中";
            LoadData();
            #region 设定定时器
            _timerLoadData = new Timer();
            _timerLoadData.Interval = IntervalLoadData;
            _timerLoadData.Elapsed += _timerLoadData_Elapsed; ;
            _timerLoadData.Start();

            #endregion
        }

        private void _timerLoadData_Elapsed(object sender, ElapsedEventArgs e)
        {
            LoadData();
        }

        private Timer _timerLoadData;
        private double IntervalLoadData = 60000;
        private void LoadData()
        {
            CurrentDate = DateTime.Now;
            try
            {
                using (var service = new LargeScreenServiceClient())
                {
                    var plan_statistic = service.GetPlanStatistic();
                    if (plan_statistic.Count() > 0)
                    {
                        FinishedPlanCount = (int)plan_statistic[0].Value;
                    }
                    else
                    {
                        FinishedPlanCount = 0;
                    }

                    var r_millings = from m in service.GetRecordMillings(CurrentDate.Date)
                                     orderby m.PlanBatchNumber, m.VHPPlanLot
                                     select m;
                    var r_plans = from p in service.GetPlanByDate(CurrentDate.Date)
                                  orderby p.Plan.PlanDate.Date, p.Plan.PlanLot, p.Plan.VHPDeviceCode
                                  select p;
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        Millings.Clear();
                        r_millings.ToList().ForEach(i => Millings.Add(i));
                        PlanExtras.Clear();
                        r_plans.ToList().ForEach(i => PlanExtras.Add(i));
                    });
                }
                CenterMessage = string.Empty;
                Status = $"读取数据正常{CurrentDate.ToShortTimeString()}";
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }




        public ObservableCollection<DcRecordMilling> Millings { get; set; }
        public ObservableCollection<DcPlanExtra> PlanExtras { get; set; }


        #region 其他属性
        private DateTime currentDate;
        public DateTime CurrentDate
        {
            get { return currentDate; }
            set { currentDate = value; RaisePropertyChanged(nameof(CurrentDate)); }
        }


        private int finishedPlanCount;
        public int FinishedPlanCount
        {
            get { return finishedPlanCount; }
            set { finishedPlanCount = value; RaisePropertyChanged(nameof(FinishedPlanCount)); }
        }

        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; RaisePropertyChanged(nameof(Status)); }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; RaisePropertyChanged(nameof(ErrorMessage)); }
        }

        private string centerMessage;
        public string CenterMessage
        {
            get { return centerMessage; }
            set { centerMessage = value; RaisePropertyChanged(nameof(CenterMessage)); }
        }

        public double IntervalRefreshLoadData
        {
            get
            {
                return IntervalLoadData / 1000;
            }
        }
        #endregion


    }
}
