using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace PMSClient.DataProcess.ScanInput
{
    public class ScanInputVMBase : ViewModelBase
    {
        public ScanInputVMBase()
        {
            inputText = "";
            progressValue = 0;
            sourceTarget = "无";

            Numbers = new List<int>();
            for (int i = 1; i < 100; i++)
            {
                Numbers.Add(i);
            }
            CurrentNumber = 1;

            Plates = new List<string>();
            Plates.Clear();
            PMSBasicDataService.SetListDS(PMSCommon.CustomData.PlateTypes, Plates);

            CurrentPlate = Plates[0];

        }
        public ObservableCollection<LotModel> Lots { get; set; }

        #region 属性
        private string inputText;
        public string InputText
        {
            get
            {
                return inputText;
            }
            set
            {
                inputText = value;
                RaisePropertyChanged(nameof(inputText));
            }
        }

        private double progressValue;
        public double ProgressValue
        {
            get
            {
                return progressValue;
            }
            set
            {
                progressValue = value;
                RaisePropertyChanged(nameof(ProgressValue));
            }
        }

        private string sourceTarget;
        public string SourceTarget
        {
            get
            {
                return sourceTarget;
            }
            set
            {
                sourceTarget = value;
                RaisePropertyChanged(nameof(SourceTarget));
            }
        }

        protected bool canClick=true;


        public List<int> Numbers { get; set; }

        public List<string> Plates { get; set; }


        private int currentNumber;
        public int CurrentNumber
        {
            get
            {
                return currentNumber;
            }
            set
            {
                currentNumber = value;
                RaisePropertyChanged(nameof(CurrentNumber));
            }
        }

        private string currentPlate;
        public string CurrentPlate
        {
            get
            {
                return currentPlate;
            }
            set
            {
                currentPlate = value;
                RaisePropertyChanged(nameof(CurrentPlate));
            }
        }


        #endregion
        public RelayCommand Process { get; set; }
        public RelayCommand Check { get; set; }

    }
}
