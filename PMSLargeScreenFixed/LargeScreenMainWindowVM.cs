using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSLargeScreen.Models;
using PMSLargeScreen.LargeScreenService;
using System.Timers;
using System.Collections.ObjectModel;
using System.Windows;

namespace PMSLargeScreen
{
    public class LargeScreenMainWindowVM : ViewModelBase
    {
        private Timer _timerLoadData;
        public LargeScreenMainWindowVM()
        {
            InitializeAll();
            IntervalLoadData = Properties.Settings.Default.UpdateInterval;
        }

        private double IntervalLoadData = 120000;

        private void InitializeAll()
        {
            CurrentDate = DateTime.Now;
            status = "状态栏";

            errorMessage = "其他信息";

            CenterMessage = $"准备数据中，请等待，{IntervalLoadData / 1000}s后显示";

            CompositionVisibility = Visibility.Visible;
            Hide = new RelayCommand(ActionHide);

        #region 设定定时器
        _timerLoadData = new Timer();
            _timerLoadData.Interval = IntervalLoadData;
            _timerLoadData.Elapsed += _timerLoadData_Elapsed;
            _timerLoadData.Start();

            #endregion

            //首次加载数据
            GetDataFromService();
        }

        private void ActionHide()
        {
            if (CompositionVisibility == Visibility.Visible)
                CompositionVisibility = Visibility.Collapsed;
            else
                CompositionVisibility = Visibility.Visible;
        }

        private void _timerLoadData_Elapsed(object sender, ElapsedEventArgs e)
        {
            CurrentDate = DateTime.Now;
            GetDataFromService();
        }

        private void GetDataFromService()
        {
            try
            {
                //Read 6 devices
                int planLot = 1;
                using (var service = new LargeScreenServiceClient())
                {
                    //get plan statistics
                    var plan_statistic = service.GetPlanStatistic();
                    if (plan_statistic.Count() > 0)
                    {
                        FinishedPlanCount = (int)plan_statistic[0].Value;
                    }
                    else
                    {
                        FinishedPlanCount = 0;
                    }

                    string deviceCode = string.Empty;
                    DcPlanExtra[] plans = null;
                    #region 读取每个设定的设备的计划信息
                    deviceCode = Properties.Settings.Default.Device1;
                    plans = service.GetPlanByDateDeviceCode(planLot, CurrentDate.Date, deviceCode);
                    Model1 = CreateUnitModel(planLot, deviceCode, plans);

                    deviceCode = Properties.Settings.Default.Device2;
                    plans = service.GetPlanByDateDeviceCode(planLot, CurrentDate.Date, deviceCode);
                    Model2 = CreateUnitModel(planLot, deviceCode, plans);

                    deviceCode = Properties.Settings.Default.Device3;
                    plans = service.GetPlanByDateDeviceCode(planLot, CurrentDate.Date, deviceCode);
                    Model3 = CreateUnitModel(planLot, deviceCode, plans);

                    deviceCode = Properties.Settings.Default.Device4;
                    plans = service.GetPlanByDateDeviceCode(planLot, CurrentDate.Date, deviceCode);
                    Model4 = CreateUnitModel(planLot, deviceCode, plans);

                    deviceCode = Properties.Settings.Default.Device5;
                    plans = service.GetPlanByDateDeviceCode(planLot, CurrentDate.Date, deviceCode);
                    Model5 = CreateUnitModel(planLot, deviceCode, plans);

                    deviceCode = Properties.Settings.Default.Device6;
                    plans = service.GetPlanByDateDeviceCode(planLot, CurrentDate.Date, deviceCode);
                    Model6 = CreateUnitModel(planLot, deviceCode, plans);
                    #endregion

                }
                CenterMessage = string.Empty;

                if (Model1 == null && Model2 == null && Model3 == null
                    && Model4 == null && Model5 == null && Model6 == null)
                {
                    CenterMessage = $"今日没有安排热压计划";
                }
                ErrorMessage = "读取正常";
                Status = $"刷新全部数据于{DateTime.Now.ToString("HH:mm:ss")}";
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        private UnitModel CreateUnitModel(int planLot, string deviceCode, DcPlanExtra[] plans)
        {
            var model = new UnitModel();
            if (plans.Count() == 0)
                return model;
            //标题部分
            model.DeviceCode = deviceCode;
            model.PlanLot = planLot;
            model.Items.Clear();
            foreach (var item in plans)
            {
                //热压工艺必须一样
                model.MoldInnerDiameter = item.Plan.MoldDiameter;
                model.MoldType = item.Plan.MoldType;
                model.Pressure = item.Plan.Pressure;
                model.Temp = item.Plan.Temperature;
                model.Vaccum = item.Plan.Vaccum;
                model.KeepTime = item.Plan.KeepTempTime;
                model.IsLocked = item.Plan.IsLocked;
                //具体计划
                var modelItem = new UnitModelItem();
                modelItem.Composition = item.Misson.CompositionStandard;
                modelItem.PMINumber = item.Misson.PMINumber;
                modelItem.SingleWeight = item.Plan.SingleWeight;
                modelItem.PlanType = item.Plan.PlanType;
                modelItem.Quantity = item.Plan.Quantity;
                modelItem.ProcessCode = item.Plan.ProcessCode;
                modelItem.FillRequirement = item.Plan.FillingRequirement;
                modelItem.VHPRequirement = item.Plan.VHPRequirement;
                model.Items.Add(modelItem);
            }
            return model;
        }

        #region Models
        private UnitModel model1;

        public UnitModel Model1
        {
            get { return model1; }
            set { model1 = value; RaisePropertyChanged(nameof(Model1)); }
        }

        private UnitModel model2;
        public UnitModel Model2
        {
            get { return model2; }
            set { model2 = value; RaisePropertyChanged(nameof(Model2)); }
        }

        private UnitModel model3;
        public UnitModel Model3
        {
            get { return model3; }
            set { model3 = value; RaisePropertyChanged(nameof(Model3)); }
        }

        private UnitModel model4;
        public UnitModel Model4
        {
            get { return model4; }
            set { model4 = value; RaisePropertyChanged(nameof(Model4)); }
        }
        private UnitModel model5;
        public UnitModel Model5
        {
            get { return model5; }
            set { model5 = value; RaisePropertyChanged(nameof(Model5)); }
        }
        private UnitModel model6;
        public UnitModel Model6
        {
            get { return model6; }
            set { model6 = value; RaisePropertyChanged(nameof(Model6)); }
        }
        #endregion

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
        private Visibility compositionVisibility;
        public Visibility CompositionVisibility
        {
            get { return compositionVisibility; }
            set
            {
                compositionVisibility = value;
                RaisePropertyChanged(nameof(CompositionVisibility));
            }
        }

        public RelayCommand Hide { get; set; }
    }
}
