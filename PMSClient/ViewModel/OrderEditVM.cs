using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSClient.MainService;
using System.Collections.ObjectModel;

namespace PMSClient.ViewModel
{
    public class OrderEditVM : BaseViewModelEdit
    {
        public OrderEditVM()
        {
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
            var states = BasicDataInstance.OrderStates;
            states.ToList().ForEach(s => OrderStates.Add(s));

            OrderPriorities = new ObservableCollection<string>();
            var priorities = BasicDataInstance.OrderPriorities;
            priorities.ToList().ForEach(p => OrderPriorities.Add(p));

            PolicyTypes = new ObservableCollection<string>();
            var policyTypes = BasicDataInstance.OrderPolicyTypes;
            policyTypes.ToList().ForEach(p => PolicyTypes.Add(p));

            CustomerNames = new ObservableCollection<string>();
            var customerNames = BasicDataInstance.CustomerNames;
            customerNames.ToList().ForEach(c => CustomerNames.Add(c.CustomerName));

            ProductTypes = new ObservableCollection<string>();
            var productTypes = BasicDataInstance.ProductTypes;
            productTypes.ToList().ForEach(p => ProductTypes.Add(p));

        }
        /// <summary>
        /// 直接更改属性
        /// </summary>
        /// <param name="isnew"></param>
        /// <param name="current"></param>
        public void SetKeyProperties(ModelObject model)
        {
            IsNew = model.IsNew;
            CurrentOrder = model.Model as DcOrder;
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
            if (IsNew)
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

    }
}
