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
            GetFinishedPlanCount();
            ShowModels = new ObservableCollection<UnitModel>();
            AllModels = new List<UnitModel>();
            status1 = "状态栏1";
            status2 = "状态栏2";
            errorMessage = "其他信息";

            CenterMessage = $"准备数据中，请等待，{IntervalLoadData/ 1000}s后显示";

            #region 设定定时器
            _timerLoadData = new Timer();
            _timerLoadData.Interval = IntervalLoadData;
            _timerLoadData.Elapsed += _timerLoadData_Elapsed;
            _timerLoadData.Start();

            #endregion

            //首次加载数据
            GetDataFromService();
        }
   
        private int counter = 0;
        /// <summary>
        /// 显示方式ABC,BCD,ABC
        /// </summary>
        private void ShowDataOneByOne()
        {
            ShowModels.Clear();

            if (AllModels.Count == 0)
            {
                Model1 = null;
                Model2 = null;
                Model3 = null;
                Model4 = null;
                Model5 = null;
                Model6 = null;

                CenterMessage = "没有计划";
                return;
            }

            CenterMessage = "";

            if (AllModels.Count == 1)
            {
                Model1 = AllModels[0];
                Model2 = null;
                Model3 = null;
                Model4 = null;
                Model5 = null;
                Model6 = null;
            }

            if (AllModels.Count == 2)
            {
                Model1 = AllModels[0];
                Model2 = AllModels[1];
                Model3 = null;
                Model4 = null;
                Model5 = null;
                Model6 = null;
            }

            if (AllModels.Count == 3)
            {
                Model1 = AllModels[0];
                Model2 = AllModels[1];
                Model3 = AllModels[2];
                Model4 = null;
                Model5 = null;
                Model6 = null;
            }

            if (AllModels.Count == 4)
            {
                Model1 = AllModels[0];
                Model2 = AllModels[1];
                Model3 = AllModels[2];
                Model4 = AllModels[3];
                Model5 = null;
                Model6 = null;
            }

            if (AllModels.Count == 5)
            {
                Model1 = AllModels[0];
                Model2 = AllModels[1];
                Model3 = AllModels[2];
                Model4 = AllModels[3];
                Model5 = AllModels[4];
                Model6 = null;
            }
            if (AllModels.Count >= 6)
            {
                Model1 = AllModels[0];
                Model2 = AllModels[1];
                Model3 = AllModels[2];
                Model4 = AllModels[3];
                Model5 = AllModels[4];
                Model6 = AllModels[5];
            }
        }

        private void _timerLoadData_Elapsed(object sender, ElapsedEventArgs e)
        {
            CurrentDate = DateTime.Now;
            GetFinishedPlanCount();
            GetDataFromService();
        }

        private void GetDataFromService()
        {
            try
            {
                  #region 读取规范化数据
                DcPlanExtra[] result;
                using (var service = new LargeScreenServiceClient())
                {
                    result = service.GetPlanByDate(CurrentDate.Date);
                }
                ErrorMessage = "其他信息";

                AllModels.Clear();
                //按批次分组
                var query1 = from i in result
                             group i by i.Plan.PlanLot into g
                             orderby g.Key
                             select g;
                foreach (var group1 in query1)
                {
                    //再次按照设备号分组
                    var query2 = from j in group1
                                 group j by j.Plan.VHPDeviceCode into g
                                 orderby g.Key
                                 select g;
                    //循环读取设备数据
                    foreach (var group2 in query2)
                    {
                        var model = new UnitModel();
                        model.PlanLot = group1.Key;
                        model.DeviceCode = group2.Key;
                        model.Items.Clear();
                        //循环读取同一个设备的热压和材料参数
                        foreach (var item in group2)
                        {
                            model.MoldInnerDiameter = item.Plan.MoldDiameter;
                            model.MoldType = item.Plan.MoldType;
                            model.Pressure = item.Plan.Pressure;
                            model.Temp = item.Plan.Temperature;
                            model.Vaccum = item.Plan.Vaccum;
                            model.KeepTime = item.Plan.KeepTempTime;

                            var modelItem = new UnitModelItem();
                            modelItem.Composition = item.Misson.CompositionStandard;
                            modelItem.PMINumber = item.Misson.PMINumber;
                            modelItem.SingleWeight = item.Plan.SingleWeight;
                            modelItem.PlanType = item.Plan.PlanType;
                            modelItem.Quantity = item.Plan.Quantity;
                            modelItem.ProcessCode = item.Plan.ProcessCode;
                            modelItem.FillRequirement = item.Plan.FillingRequirement;

                            model.Items.Add(modelItem);
                        }
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            AllModels.Add(model);
                        });
                    }
                }
                #endregion
                Status1 = $"刷新全部数据于{DateTime.Now.ToString("HH:mm:ss")},今日共有{AllModels.Count}个计划安排";
                counter = 0;
                ShowDataOneByOne();
               }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        /// <summary>
        /// 获取已完成计划数量
        /// </summary>
        private void GetFinishedPlanCount()
        {
            try
            {
                using (var service = new LargeScreenServiceClient())
                {
                    var result = service.GetPlanStatistic();
                    if (result.Count() > 0)
                    {
                        FinishedPlanCount = (int)result[0].Value;
                    }
                    else
                    {
                        FinishedPlanCount = 0;
                    }
                }
                ErrorMessage = "其他信息";
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public ObservableCollection<UnitModel> ShowModels { get; set; }

        private List<UnitModel> AllModels { get; set; }

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

    }
}
