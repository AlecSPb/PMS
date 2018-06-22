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
            inputText = statusText = "";

            Process = new RelayCommand(ActionProcess);
            Check = new RelayCommand(ActionCheck);
            Lots = new ObservableCollection<LotModel>();

        }

        private StringBuilder sb = new StringBuilder();
        private DataProcess.ScanInput.ProcessScanInput process = new DataProcess.ScanInput.ProcessScanInput();


        private void ActionCheck()
        {
            process.Intialize(InputText);

            //StatusText = process.FromLotsToString();

            Lots.Clear();
            process.Lots.ForEach(i =>
            {
                Lots.Add(i);
            });
        }



        private void ActionProcess()
        {

        }


        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="s"></param>
        private void Log(string s)
        {
            if (string.IsNullOrEmpty(s))
                return;
            sb.AppendLine(s);
            StatusText = sb.ToString();
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

        private string statusText;
        public string StatusText
        {
            get
            {
                return statusText;
            }
            set
            {
                statusText = value;
                RaisePropertyChanged(nameof(statusText));
            }
        }
        #endregion
        public RelayCommand Process { get; set; }
        public RelayCommand Check { get; set; }
    }
}
