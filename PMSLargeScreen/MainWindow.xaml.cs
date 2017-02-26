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
            LoadDisplayData();
        }

        private void LoadDisplayData()
        {
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

            SetSinglePanel(first, firstModel);
            SetSinglePanel(second, secondModel);
            SetSinglePanel(third, thirdModel);
        }

        private void SetSinglePanel(SinglePanel panel, SinglePanelModel model)
        {
            panel.SetDataContext(model);
        }

    }
}
