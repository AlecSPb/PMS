using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSLargeScreen.Models;
using PMSLargeScreen.LargeScreenService;
using System.Timers;

namespace PMSLargeScreen
{
    public class LargeScreenMainWindowVM : ViewModelBase
    {
        private Timer _timer;
        public LargeScreenMainWindowVM()
        {
            InitializeAll();
        }

        private void InitializeAll()
        {
            currentDate = DateTime.Now;
            finishedPlanCount = 10000;
            model1 = new UnitModel();
            model2 = new UnitModel();
            model3 = new UnitModel();

            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            using (var service=new LargeScreenServiceClient())
            {
                var result = service.GetPlanByDate(CurrentDate.Date);

            }
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
