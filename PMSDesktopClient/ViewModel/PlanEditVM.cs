using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSDesktopClient.ServiceReference;


namespace PMSDesktopClient.ViewModel
{
    public class PlanEditVM : ViewModelBase
    {
        public PlanEditVM(DcPlanVHP plan, bool isnew)
        {

            CurrentPlan = plan;
            isNew = isnew;

            GiveUp = new RelayCommand(ActionGiveUp);
            Save = new RelayCommand(ActionSave);
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

        public DcPlanVHP CurrentPlan { get; set; }

        public RelayCommand GiveUp { get; set; }
        public RelayCommand Save { get; set; }

    }
}
