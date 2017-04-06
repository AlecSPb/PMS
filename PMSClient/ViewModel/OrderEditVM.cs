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
        public void SetNew()
        {
            IsNew = true;
            var order = new DcOrder();
            order.ID = Guid.NewGuid();
            order.CustomerName = "Midsummer";
            order.PO = DateTime.Now.ToString("yyMMdd");
            order.PMINumber = DateTime.Now.ToString("yyMMdd");
            order.ProductType = "Target";
            order.Dimension = "230mm OD x  4mm";
            order.DimensionDetails = "None";
            order.SampleNeed = "无需样品";
            order.MinimumAcceptDefect = "通常";
            order.Reviewer = "xs.zhou";
            order.PolicyContent = "";
            order.PolicyType = "VHP";
            order.PolicyMaker = "xs.zhou";

            order.Purity = "99.99";
            order.DeadLine = DateTime.Now.AddDays(30);
            order.ReviewDate = DateTime.Now;
            order.PolicyMakeDate = DateTime.Now;
            order.State = "UnChecked";
            order.Priority = "Normal";
            order.CompositionOriginal = "CuGaSe2";
            order.CompositionStandard = "Cu25Ga25Se50";
            order.CompositionAbbr = "CuGaSe";
            order.Creator = "xs.zhou";
            order.CreateTime = DateTime.Now;
            order.ProductType = "Target";
            order.ReviewPassed = true;
            order.Quantity = 1;
            order.QuantityUnit = "片";
            CurrentOrder = order;
        }
        public void SetEdit(DcOrder order)
        {
            if (order!=null)
            {
                IsNew = false;
                CurrentOrder = order;
            }
        }

        public void SetDuplicate(DcOrder order)
        {
            if (order!=null)
            {
                IsNew = true;
                CurrentOrder = order;
            }
        }
        private void ActionGiveUp()
        {
            NavigationService.GoTo(PMSViews.Order);
        }

        private bool CanSave()
        {
            return true;
        }

        private void ActionSave()
        {
            try
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
                NavigationService.GoTo(PMSViews.Order);
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
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
