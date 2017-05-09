using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using PMSLargeScreenBonding.LargeScreenService;

namespace PMSLargeScreenBonding
{
    public class MainWindowVM : ViewModelBase
    {
        public MainWindowVM()
        {
            currentDate = DateTime.Now;
            finishedCount = 1000;
            status1 = status2 = status3 = "状态栏";
            RecordBondings = new ObservableCollection<DcRecordBonding>();
            using (var service = new LargeScreenServiceClient())
            {
                var result = service.GetBondingUnComplete();
                RecordBondings.Clear();
                result.ToList().ForEach(i => RecordBondings.Add(i));
            }
        }





        public ObservableCollection<DcRecordBonding> RecordBondings { get; set; }


        private DateTime currentDate;

        public DateTime CurrentDate
        {
            get { return currentDate; }
            set { currentDate = value; RaisePropertyChanged(nameof(CurrentDate)); }
        }

        private int finishedCount;

        public int FinishedCount
        {
            get { return finishedCount; }
            set { finishedCount = value; RaisePropertyChanged(nameof(FinishedCount)); }
        }

        private string status1;
        public string Status1
        {
            get { return status1; }
            set { status1 = value; RaisePropertyChanged(nameof(Status1)); }
        }

        private string status2;
        public string Status2
        {
            get { return status2; }
            set { status2 = value; RaisePropertyChanged(nameof(Status2)); }
        }

        private string status3;
        public string Status3
        {
            get { return status3; }
            set { status3 = value; RaisePropertyChanged(nameof(Status3)); }
        }

    }
}
