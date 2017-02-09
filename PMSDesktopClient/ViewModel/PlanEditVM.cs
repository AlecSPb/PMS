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
    public class PlanEditVM:ViewModelBase
    {
        public PlanEditVM(DcPlanVHP plan)
        {
            if (plan!=null)
            {
                CurrentPlan = plan;
                isNew = false;
            }
            else
            {
                var plan1 = new DcPlanVHP();
                plan1.ID = Guid.NewGuid();
                plan1.PlanDate = DateTime.Now;
                plan1.MoldDiameter = 230;
                plan1.CurrentMold = "GQ230";
                plan1.Quantity = 1;
                plan1.VHPDeviceCode = "A";
                plan1.OrderID = Guid.Empty;

                CurrentPlan = plan1;
                isNew = true;
            }
            GiveUp = new RelayCommand(ActionGiveup);
            Save = new RelayCommand(CanSave);
        }
        private bool isNew;
        private void CanSave()
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

            NavigationService.GoTo("PlanView");
            Messenger.Default.Send<string>(null, "PlanVHPRefresh");
        }

        private void ActionGiveup()
        {
            NavigationService.GoTo("PlanView");
        }

        public DcPlanVHP CurrentPlan { get; set; }

        public RelayCommand GiveUp { get; set; }
        public RelayCommand Save { get; set; }

    }
}
