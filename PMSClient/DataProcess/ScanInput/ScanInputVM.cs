using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.DataProcess;

namespace PMSClient.DataProcess.ScanInput
{
    public class ScanInputVM : ViewModelBase
    {
        public ScanInputVM()
        {
            inputText = "";

            process = new DataProcess.ScanInput.ScanInputRecordBonding();

            Process = new RelayCommand(ActionProcess);
            Check = new RelayCommand(ActionCheck);
            Lots = new ObservableCollection<LotModel>();

        }

        private DataProcess.ScanInput.ScanInputRecordBonding process;


        private void ActionCheck()
        {
            process.Intialize(InputText);

            process.Check();

            RefreshLotsStatus();
        }

        private void RefreshLotsStatus()
        {
            Lots.Clear();
            process.Lots.ForEach(i =>
            {
                Lots.Add(i);
            });
        }


        private void ActionProcess()
        {
            process.Intialize(InputText);

            process.Process();

            RefreshLotsStatus();
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
        #endregion
        public RelayCommand Process { get; set; }
        public RelayCommand Check { get; set; }
    }
}
