using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace PMSClient.ToolWindow
{
    public class ScanInputVM : ViewModelBase
    {
        public ScanInputVM()
        {
            inputText = statusText = "";
            TargetTable = "";

            Process = new RelayCommand(ActionProcess);
            Check = new RelayCommand(ActionCheck);
        }

        private StringBuilder sb = new StringBuilder();
        private DataProcess.ProcessScanInput process = new DataProcess.ProcessScanInput();
        public string TargetTable { get; set; }



        private void ActionCheck()
        {
            var result = process.CheckAll(InputText);
            StatusText = result;
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
