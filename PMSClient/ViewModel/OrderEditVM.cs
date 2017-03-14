using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSClient.PMSMainService;
using System.Collections.ObjectModel;

namespace PMSClient.ViewModel
{
    public class OrderEditVM : ViewModelBase
    {
        public OrderEditVM()
        {
            InitializeProperties();
        }

        public OrderEditVM(ModelObject model)
        {
            CurrentOrder = model.Model as DcOrder;
            isNew = model.IsNew;

            InitializeCommands();
            InitializeProperties();
        }

        private void InitializeCommands()
        {
            Save = new RelayCommand(ActionSave, CanSave);
            GiveUp = new RelayCommand(ActionGiveUp);
        }

        public void InitializeProperties()
        {
            OrderStates = new ObservableCollection<string>();
            var states = BDInstance.OrderStates;
            states.ToList().ForEach(s => OrderStates.Add(s));

            OrderPriorities = new ObservableCollection<string>();
            var priorities = BDInstance.OrderPriorities;
            priorities.ToList().ForEach(p => OrderPriorities.Add(p));

            PolicyTypes = new ObservableCollection<string>();
            var policyTypes = BDInstance.OrderPolicyTypes;
            policyTypes.ToList().ForEach(p => PolicyTypes.Add(p));

            CustomerNames = new ObservableCollection<string>();
            var customerNames = BDInstance.CustomerNames;
            customerNames.ToList().ForEach(c => CustomerNames.Add(c.CustomerName));

            ProductTypes = new ObservableCollection<string>();
            var productTypes = BDInstance.ProductTypes;
            productTypes.ToList().ForEach(p => ProductTypes.Add(p));

        }


        private void ActionGiveUp()
        {
            NavigationService.GoTo(new MsgObject() { MsgToken = VToken.Order });
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
            NavigationService.GoTo(new MsgObject() { MsgToken = VToken.Order });
            NavigationService.Refresh(VToken.OrderRefresh);
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

        public ObservableCollection<string> OrderStates { get; set; }
        public ObservableCollection<string> OrderPriorities { get; set; }
        public ObservableCollection<string> PolicyTypes { get; set; }
        public ObservableCollection<string> CustomerNames { get; set; }
        public ObservableCollection<string> ProductTypes { get; set; }
        public RelayCommand Save { get; set; }
        public RelayCommand GiveUp { get; set; }

    }
}
