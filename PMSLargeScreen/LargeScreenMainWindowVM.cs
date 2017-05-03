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
        private Timer _timer;
        public LargeScreenMainWindowVM()
        {
            InitializeAll();
        }

        private void InitializeAll()
        {
            CurrentDate = DateTime.Now;
            SetFinishedPlanCount();
            ModelList = new ObservableCollection<UnitModel>();
            #region 设定定时器
            _timer = new Timer();
            _timer.Interval = 20000;
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();
            #endregion

            //首次加载数据
            LoadData();
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            CurrentDate = DateTime.Now;
            SetFinishedPlanCount();
            LoadData();
        }

        private void SetFinishedPlanCount()
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
        }

        private void LoadData()
        {
            DcPlanExtra[] result;
            using (var service = new LargeScreenServiceClient())
            {
                result = service.GetPlanByDate(CurrentDate.Date);
            }

            ModelList.Clear();
            //按批次分组
            var query1 = from i in result
                         group i by i.Plan.PlanLot into g
                         orderby g.Key
                         select g;
            try
            {
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
                            model.FillRequirement = item.Plan.FillingRequirement;

                            var modelItem = new UnitModelItem();
                            modelItem.Composition = item.Misson.CompositionStandard;
                            modelItem.SingleWeight = item.Plan.SingleWeight;
                            modelItem.Quantity = item.Plan.Quantity;
                            modelItem.ProcessCode = item.Plan.ProcessCode;

                            model.Items.Add(modelItem);
                        }
                        ModelList.Add(model);
                    }
                }



            }
            catch (Exception ex)
            {
                throw;
            }

        }


        public ObservableCollection<UnitModel> ModelList { get; set; }


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

    }
}
