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
    public class OrderEditVM : ViewModelBase
    {
        public OrderEditVM()
        {

        }
        public OrderEditVM(DcOrder order)
        {
            if (order != null)
            {
                CurrentOrder = order;
                isNew = false;
            }
            else
            {
                var dcOrder = new DcOrder();
                dcOrder.ID = Guid.NewGuid();
                dcOrder.CustomerName = "Midsummer";
                dcOrder.Purity = "99.99";
                dcOrder.CreateTime = DateTime.Now;
                dcOrder.DeadLine = DateTime.Now.AddDays(30);
                dcOrder.ReviewDate = DateTime.Now;
                dcOrder.PolicyMakeDate = DateTime.Now;
                dcOrder.State = 1;
                dcOrder.Priority = 1;


                CurrentOrder = dcOrder;
                isNew = true;
            }

            Save = new RelayCommand(ActionSave, CanSave);
            GiveUp = new RelayCommand(ActionGiveUp);

        }

        private void ActionGiveUp()
        {
            NavigationService.NavigateTo("OrderView");
        }

        private bool CanSave()
        {
            return true;
        }

        private void ActionSave()
        {
            var service = new OrderServiceClient();
            if (isNew)
            {
                service.AddOrder(CurrentOrder);
            }
            else
            {
                service.UpdateOrder(CurrentOrder);
            }
            NavigationService.NavigateTo("OrderView");
        }

        private bool isNew;


        private DcOrder currentOrder;

        public DcOrder CurrentOrder
        {
            get { return currentOrder; }
            set
            {
                currentOrder = value;
                RaisePropertyChanged(nameof(CurrentOrder));
            }
        }


        public RelayCommand Save { get; set; }
        public RelayCommand GiveUp { get; set; }

    }
}
