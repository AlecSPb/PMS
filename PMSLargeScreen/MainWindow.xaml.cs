using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PMSLargeScreen.PMSMainService;
using System.Timers;

namespace PMSLargeScreen
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Models = new List<SinglePanelModel>();

            LoadDisplayData();

            //Timer timer = new Timer();
            //timer.Interval = 2000;
            //timer.Elapsed += Timer_Elapsed;
            //timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            LoadDisplayData();
        }

        private SinglePanelModel GetModel(List<DcMissonWithPlan> models)
        {
            var single = new SinglePanelModel();
            if (models.Count > 0)
            {
                var commonState = models[0];
                single.DeviceCode = commonState.VHPDeviceCode;
                single.Temperature = commonState.Temperature;
                single.Pressure = commonState.PrePressure;
                single.Vaccum = commonState.Vaccum;
                single.MoldType = commonState.MoldType;
                single.MoldDiameter = commonState.MoldDiameter;
                single.PrePressure = commonState.PrePressure;
                single.PreTemperature = commonState.PreTemperature;
                single.Compositions = new List<string>();
                foreach (var item in models)
                {
                    single.Compositions.Add(item.CompositionStandard + " " + item.ProcessCode);
                }

            }
            return single;
        }

        public void AddIntoModel(List<DcMissonWithPlan> todayList, string device)
        {
            if (todayList.Count > 0)
            {
                var plans = todayList.Where(i => i.VHPDeviceCode.Contains(device)).ToList();
                if (plans.Count > 0)
                {
                    Models.Add(GetModel(plans));
                }
            }
        }
        private void LoadDisplayData()
        {
            #region TestData
            var firstModel = new SinglePanelModel();
            firstModel.DeviceCode = "A";
            firstModel.MoldType = "CFC";
            firstModel.MoldDiameter = 233;
            firstModel.Temperature = 500;
            firstModel.Pressure = 345;
            firstModel.Vaccum = 1E-3;
            firstModel.ProcessCode = "W1";
            firstModel.Compositions = new List<string>()
            {
                "CuGaSe2"+"   共2片"+" W1",
                "Cu22.8In20Ga7.0Se50.2"+"   共1片"+" W3"
            };

            var secondModel = new SinglePanelModel();
            secondModel.DeviceCode = "B";
            secondModel.MoldType = "CFC";
            secondModel.MoldDiameter = 233;
            secondModel.Temperature = 500;
            secondModel.Pressure = 345;
            secondModel.Vaccum = 1E-3;
            secondModel.ProcessCode = "W1";
            secondModel.Compositions = new List<string>()
            {
                "Cu22.8In20Ga7.0Se50.2"+" 共1片"+" W2",
                "CuGaSe2"+" 共2片"+" W2"
            };

            var thirdModel = new SinglePanelModel();
            thirdModel.DeviceCode = "C";
            thirdModel.MoldType = "CFC";
            thirdModel.MoldDiameter = 233;
            thirdModel.Temperature = 500;
            thirdModel.Pressure = 345;
            thirdModel.Vaccum = 1E-3;
            thirdModel.ProcessCode = "W3";
            thirdModel.Compositions = new List<string>()
            {
                "Cu22.8In20Ga7.0Se50.2"+"共1片"+"W3",
                "CuGaSe2"+"共2片"+" W3"
            };
            #endregion

            //var todayList = LargeScreenService.GetTodayMissonWithPlan();

            //AddIntoModel(todayList, "A");
            //AddIntoModel(todayList, "B");
            //AddIntoModel(todayList, "C"); 
            Models.Add(firstModel);
            Models.Add(secondModel);
            Models.Add(thirdModel);


            if (Models.Count > 0)
            {
                SetSinglePanel(first, Models[0]);
            }
            if (Models.Count > 1)
            {
                SetSinglePanel(second, Models[1]);
            }
            if (Models.Count > 2)
            {
                SetSinglePanel(third, Models[2]);
            }

        }

        private List<SinglePanelModel> Models;

        private void SetSinglePanel(SinglePanel panel, SinglePanelModel model)
        {
            panel.SetDataContext(model);
        }

    }
}
