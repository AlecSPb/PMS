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

           //默认数字
            Values = new List<int>();
            for (int i = 1; i < 100; i++)
            {
                Values.Add(i);
            }
            CurrentValue = 1;

            //默认背板类型
            Texts = new List<string>();
            Texts.Clear();
            PMSBasicDataService.SetListDS(PMSCommon.CustomData.PlateTypes, Texts);

            CurrentText = Texts[0];

            currentCheck = false;
            currentCheck2 = false;

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


        public List<int> Values { get; set; }

        public List<string> Texts { get; set; }


        private int currentNumber;
        public int CurrentValue
        {
            get
            {
                return currentNumber;
            }
            set
            {
                currentNumber = value;
                RaisePropertyChanged(nameof(CurrentValue));
            }
        }

        private string currentPlate;
        public string CurrentText
        {
            get
            {
                return currentPlate;
            }
            set
            {
                currentPlate = value;
                RaisePropertyChanged(nameof(CurrentText));
            }
        }

        private bool currentCheck;
        public bool CurrentCheck
        {
            get
            {
                return currentCheck;
            }
            set
            {
                currentCheck = value;
                RaisePropertyChanged(nameof(CurrentCheck));
            }
        }

        private bool currentCheck2;
        public bool CurrentCheck2
        {
            get
            {
                return currentCheck2;
            }
            set
            {
                currentCheck2 = value;
                RaisePropertyChanged(nameof(CurrentCheck2));
            }
        }

        #endregion
        public RelayCommand Process { get; set; }
        public RelayCommand Check { get; set; }

    }
}
