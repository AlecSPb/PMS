using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.CommandWpf;
using PMSDesktopClient.ServiceReference;

namespace PMSDesktopClient.ViewModel
{
    public class OrderSelectVM : OrderVM
    {
        public OrderSelectVM()
        {
            SelectEmpty = new RelayCommand(ActionSelectEmpty);
            SelectOrder = new RelayCommand<ServiceReference.DcOrder>(ActionSelectOrder);
        }

        private void ActionSelectOrder(DcOrder obj)
        {
            if (obj != null)
            {
                var plan = new DcPlanVHP();
                plan.ID = Guid.NewGuid();
                plan.PlanDate = DateTime.Now;
                plan.MoldDiameter = 230;
                plan.CurrentMold = "GQ230";
                plan.Quantity = 1;
                plan.VHPDeviceCode = "A";
                plan.OrderID = obj.ID;

                var nModel = new MessageObject();
                nModel.ViewName = "PlanEditView";
                nModel.ModelObject = plan;
                NavigationService.GoToWithParameter(nModel);
            }
        }

        private void ActionSelectEmpty()
        {
            var nModel = new MessageObject();
            nModel.ViewName = "PlanEditView";
            nModel.ModelObject = null;
            NavigationService.GoToWithParameter(nModel);
        }

        public RelayCommand SelectEmpty { get; set; }
        public RelayCommand<DcOrder> SelectOrder { get; set; }

    }
}
