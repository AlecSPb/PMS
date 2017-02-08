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
            }

            GiveUp = new RelayCommand(ActionGiveup);
            Save = new RelayCommand(CanSave);
        }

        private void CanSave()
        {
            throw new NotImplementedException();
        }

        private void ActionGiveup()
        {
            NavigationService.NavigateTo("PlanView");
        }

        public DcPlanVHP CurrentPlan { get; set; }

        public RelayCommand GiveUp { get; set; }
        public RelayCommand Save { get; set; }

    }
}
