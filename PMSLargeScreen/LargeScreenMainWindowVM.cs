using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSLargeScreen.Models;

namespace PMSLargeScreen
{
    public class LargeScreenMainWindowVM : ViewModelBase
    {
        public LargeScreenMainWindowVM()
        {
            InitializeAll();
        }

        private void InitializeAll()
        {
            currentDate = DateTime.Now;
            finishedPlanCount = 1000;
        }

        private DateTime currentDate;

        public DateTime CurrentDate
        {
            get { return currentDate; }
            set { currentDate = value; RaisePropertyChanged(nameof(CurrentDate)); }
        }


        private int finishedPlanCount;
        public int FinishedPlanCount
        {
            get { return finishedPlanCount; }
            set { finishedPlanCount = value; RaisePropertyChanged(nameof(FinishedPlanCount)); }
        }


        private UnitModel model1;
        public UnitModel Model1
        {
            get { return model1; }
            set
            {
                model1 = value;
                RaisePropertyChanged(nameof(Model1));
            }
        }

        private UnitModel model2;
        public UnitModel Model2
        {
            get { return model2; }
            set
            {
                model2 = value;
                RaisePropertyChanged(nameof(Model2));
            }
        }

        private UnitModel model3;
        public UnitModel Model3
        {
            get { return model3; }
            set
            {
                model3 = value;
                RaisePropertyChanged(nameof(Model3));
            }
        }

    }
}
