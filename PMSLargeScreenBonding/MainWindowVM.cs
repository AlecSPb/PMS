using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;



namespace PMSLargeScreenBonding
{
    public class MainWindowVM : ViewModelBase
    {
        public MainWindowVM()
        {
            currentDate = DateTime.Now;
            finishedCount = 1000;
        }

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



    }
}
