using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSLargeScreen.PMSMainService;
using System.Timers;
using System.Windows.Threading;

namespace PMSLargeScreen
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Dispatcher dispatcher;
        public MainWindowViewModel( Dispatcher dis)
        {
            dispatcher = dis;
            IntializeAll();
        }

        private void IntializeAll()
        {
            Today = DateTime.Now.Date;
            StatusMessage = "准备运行";
            NoPlan = System.Windows.Visibility.Hidden;

            Models = new ObservableCollection<SinglePanelModel>();

            Models.Clear();
            #region TestData
            //var firstModel = new SinglePanelModel();
            //firstModel.DeviceCode = "A";
            //firstModel.MoldType = "CFC";
            //firstModel.MoldDiameter = 233;
            //firstModel.Temperature = 500;
            //firstModel.Pressure = 345;
            //firstModel.Vaccum = 1E-3;
            //firstModel.ProcessCode = "W1";
            //firstModel.Compositions = new ObservableCollection<string>()
            //{
            //    "CuGaSe2"+"   共2片"+" W1",
            //    "Cu22.8In20Ga7.0Se50.2"+"   共1片"+" W3"
            //};

            //var secondModel = new SinglePanelModel();
            //secondModel.DeviceCode = "B";
            //secondModel.MoldType = "CFC";
            //secondModel.MoldDiameter = 233;
            //secondModel.Temperature = 500;
            //secondModel.Pressure = 345;
            //secondModel.Vaccum = 1E-3;
            //secondModel.ProcessCode = "W1";
            //secondModel.Compositions = new ObservableCollection<string>()
            //{
            //    "Cu22.8In20Ga7.0Se50.2"+" 共1片"+" W2",
            //    "CuGaSe2"+" 共2片"+" W2"
            //};

            //var thirdModel = new SinglePanelModel();
            //thirdModel.DeviceCode = "C";
            //thirdModel.MoldType = "CFC";
            //thirdModel.MoldDiameter = 233;
            //thirdModel.Temperature = 500;
            //thirdModel.Pressure = 345;
            //thirdModel.Vaccum = 1E-3;
            //thirdModel.ProcessCode = "W3";
            //thirdModel.Compositions = new ObservableCollection<string>()
            //{
            //    "Cu22.8In20Ga7.0Se50.2"+"共1片"+"W3",
            //    "CuGaSe2"+"共2片"+" W3"
            //};
            //Models.Add(firstModel);
            //Models.Add(secondModel);
            //Models.Add(thirdModel);
            #endregion

            Timer timer = new Timer()
            {
                Interval = 5000
            };
            timer.Elapsed += (s, e) =>
            {
                dispatcher.Invoke(() =>
                {
                    RefreshData();
                    StatusMessage = $"刷新于{DateTime.Now.ToLongTimeString()}";
                });
            };

            timer.Start();
            StatusMessage = "正在初始化，请等待……";

        }

        private void RefreshData()
        {
            var todayList = GetTodayMissonWithPlan();
            if (todayList.Count > 0)
            {
                Models.Clear();
                AddIntoModel(todayList, "A");
                AddIntoModel(todayList, "B");
                AddIntoModel(todayList, "C");
            }
            else
            {
                NoPlan = System.Windows.Visibility.Visible;
                StatusMessage = "今日暂时没有计划，请等待安排";
            }
        }
        #region ProcessHelper
        private List<DcMissonWithPlan> GetTodayMissonWithPlan()
        {

            var today = DateTime.Now;
            using (var service = new MissonWithPlanServiceClient())
            {
                var result = service.GetMissonWithPlanByDate(today);
                return result.ToList();
            }
        }
        private void AddIntoModel(List<DcMissonWithPlan> todayList, string device)
        {
            if (todayList.Count > 0)
            {
                var plans = todayList.Where(i => i.VHPDeviceCode.Contains(device)).ToList();
                if (plans.Count > 0)
                {
                    Models.Add(GetSpecialModel(plans));
                }
            }
        }

        private SinglePanelModel GetSpecialModel(List<DcMissonWithPlan> models)
        {
            var single = new SinglePanelModel();
            if (models.Count > 0)
            {
                var commonState = models[0];
                single.DeviceCode = commonState.VHPDeviceCode;
                single.Temperature = commonState.Temperature;
                single.Pressure = commonState.Pressure;
                single.Vaccum = commonState.Vaccum;
                single.MoldType = commonState.MoldType;
                single.MoldDiameter = commonState.MoldDiameter;
                single.PrePressure = commonState.PrePressure;
                single.PreTemperature = commonState.PreTemperature;
                single.Compositions = new List<string>();
                single.Compositions.Clear();
                models.ForEach(m => single.Compositions.Add(m.CompositionStandard));

            }
            return single;
        }
        #endregion



        public ObservableCollection<SinglePanelModel> Models { get; set; }

        private DateTime today;
        public DateTime Today
        {
            get { return today; }
            set { today = value; RaisePropertyChanged(nameof(Today)); }
        }

        private System.Windows.Visibility noPlan;
        public System.Windows.Visibility NoPlan
        {
            get { return noPlan; }
            set
            {
                noPlan = value; RaisePropertyChanged(nameof(NoPlan));
            }
        }



        private string statusMessage;
        public string StatusMessage
        {
            get { return statusMessage; }
            set { statusMessage = value; RaisePropertyChanged(nameof(StatusMessage)); }
        }

    }
}
