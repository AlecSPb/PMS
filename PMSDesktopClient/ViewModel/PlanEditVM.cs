using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSDesktopClient.ServiceReference;
using System.Collections.ObjectModel;

namespace PMSDesktopClient.ViewModel
{
    public class PlanEditVM : ViewModelBase
    {
        public PlanEditVM(DcPlanVHP plan, bool isnew)
        {

            CurrentPlan = plan;
            isNew = isnew;
            InitializeProperties();
            GiveUp = new RelayCommand(ActionGiveUp);
            Save = new RelayCommand(ActionSave);
        }

        private void InitializeProperties()
        {
            Molds = new ObservableCollection<ServiceReference.DcBDVHPMold>();
            var service = new VHPMoldServiceClient();
            var molds = service.GetVHPMold();
            molds.ToList().ForEach(m => Molds.Add(m));

            States = new ObservableCollection<string>();
            var states = Enum.GetNames(typeof(PMSCommon.NonOrderState));
            states.ToList().ForEach(s => States.Add(s));

            ProcessCodes = new ObservableCollection<string>();
            var service2 = new VHPProcessServiceClient();
            var processCodes = service2.GetVHPProcess();
            processCodes.ToList().ForEach(p => ProcessCodes.Add(p.CodeName));
        }

        private bool isNew;
        private void ActionSave()
        {
            var service = new PlanVHPServiceClient();
            if (isNew)
            {
                service.AddVHPPlan(CurrentPlan);
            }
            else
            {
                service.UpdateVHPPlan(CurrentPlan);
            }

            NavigationService.GoTo("MissonView");
            //Messenger.Default.Send<string>(null, "PlanVHPRefresh");
        }

        private void ActionGiveUp()
        {
            NavigationService.GoTo("PlanView");
        }
        public ObservableCollection<DcBDVHPMold> Molds { get; set; }
        public ObservableCollection<string> States { get; set; }
        public ObservableCollection<string> ProcessCodes { get; set; }

        public DcPlanVHP CurrentPlan { get; set; }

        public RelayCommand GiveUp { get; set; }
        public RelayCommand Save { get; set; }

    }
}
