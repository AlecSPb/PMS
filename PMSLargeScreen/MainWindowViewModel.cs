using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSLargeScreen
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            Today = DateTime.Now.Date;

            StatusMessage = "准备运行";
            Models = new ObservableCollection<SinglePanelModel>();

            Models.Clear();
            #region TestData
            var firstModel = new SinglePanelModel();
            firstModel.DeviceCode = "A";
            firstModel.MoldType = "CFC";
            firstModel.MoldDiameter = 233;
            firstModel.Temperature = 500;
            firstModel.Pressure = 345;
            firstModel.Vaccum = 1E-3;
            firstModel.ProcessCode = "W1";
            firstModel.Compositions = new ObservableCollection<string>()
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
            secondModel.Compositions = new ObservableCollection<string>()
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
            thirdModel.Compositions = new ObservableCollection<string>()
            {
                "Cu22.8In20Ga7.0Se50.2"+"共1片"+"W3",
                "CuGaSe2"+"共2片"+" W3"
            };
            Models.Add(firstModel);
            Models.Add(secondModel);
            Models.Add(thirdModel);
            #endregion

        }

        public ObservableCollection<SinglePanelModel> Models { get; set; }

        private DateTime today;
        public DateTime Today
        {
            get { return today; }
            set { today = value; RaisePropertyChanged(nameof(Today)); }
        }


        private string statusMessage;
        public string StatusMessage
        {
            get { return statusMessage; }
            set { statusMessage = value; RaisePropertyChanged(nameof(StatusMessage)); }
        }

    }
}
