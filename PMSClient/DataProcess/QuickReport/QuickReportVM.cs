using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.DataProcess.ScanInput;

namespace PMSClient.DataProcess.QuickReport
{
    public class QuickReportVM : ViewModelBase
    {
        public QuickReportVM()
        {
            inputText = "";
            progressValue = 0;
            sourceTarget = "测试记录 => 特定文件夹";
            process = new ProcessReport();

            ReportTypes = new List<string>();
            ReportTypes.Add("TEST");
            ReportTypes.Add("COA");
            ReportTypes.Add("COA-BL");
            CurrentReportType = ReportTypes[0];

            Process = new RelayCommand(ActionProcess);
            Check = new RelayCommand(ActionCheck);
            Lots = new ObservableCollection<LotModel>();

        }

        private ProcessReport process;

        private void ActionCheck()
        {
            Task task = new Task(() =>
            {
                process.Intialize(InputText);

                process.Check(i =>
                {
                    ProgressValue = i;
                });
                App.Current.Dispatcher.Invoke(() =>
                {
                    RefreshLotsStatus();

                });
            });
            task.Start();
        }

        private void ActionProcess()
        {
            if (PMSDialogService.ShowYesNo("请问", "确定继续吗？") == false)
                return;

            Task task = new Task(() =>
            {
                process.Intialize(InputText);
                process.CurrentReportType = CurrentReportType;

                process.Process(i =>
                {
                    ProgressValue = i;
                });
                App.Current.Dispatcher.Invoke(() =>
                {
                    RefreshLotsStatus();

                });
            });

            task.Start();
        }

        private void RefreshLotsStatus()
        {
            Lots.Clear();
            process.Lots.ForEach(i =>
            {
                Lots.Add(i);
            });

            PMSDialogService.Show("结束", "处理结束");

        }

        public List<string> ReportTypes { get; set; }


        public ObservableCollection<LotModel> Lots { get; set; }

        #region 属性
        private string currentReportType;
        public string CurrentReportType
        {
            get
            {
                return currentReportType;
            }
            set
            {
                currentReportType = value;
                RaisePropertyChanged(nameof(CurrentReportType));
            }
        }
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
        #endregion
        public RelayCommand Process { get; set; }
        public RelayCommand Check { get; set; }

    }
}
