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
        public OrderEditVM(DcOrder order,bool isAdd)
        {
            CurrentOrder = order;
            isNew = isAdd;
            Save = new RelayCommand(ActionSave, CanSave);
            GiveUp = new RelayCommand(ActionGiveUp);

        }

        private void ActionGiveUp()
        {
            NavigationService.GoTo("OrderView");
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
            NavigationService.GoTo("OrderView");
            Messenger.Default.Send<Object>("", "RefreshOrder");
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
