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
            GiveUp = new RelayCommand(GoBack);
        }

        public void InitializeProperties()
        {
            OrderStates = new List<string>();
            var states = PMSBasicData.OrderStates;
            states.ToList().ForEach(s => OrderStates.Add(s));

            OrderPriorities = new List<string>();
            var priorities = PMSBasicData.OrderPriorities;
            priorities.ToList().ForEach(p => OrderPriorities.Add(p));

            PolicyTypes = new List<string>();
            var policyTypes = PMSBasicData.OrderPolicyTypes;
            policyTypes.ToList().ForEach(p => PolicyTypes.Add(p));

            CustomerNames = new List<string>();
            var customerNames = PMSBasicData.Customers;
            customerNames.ToList().ForEach(c => CustomerNames.Add(c.CustomerName));

            ProductTypes = new List<string>();
            var productTypes = PMSBasicData.OrderProductTypes;
            productTypes.ToList().ForEach(p => ProductTypes.Add(p));


            OrderUnits = new List<string>();
            PMSBasicData.OrderUnits.ToList().ForEach(i => OrderUnits.Add(i));
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
            #region 初始化order
            order.ID = Guid.NewGuid();
            order.CustomerName = "客户名称";
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
            order.CompositionOriginal = "原始成分写法";
            order.CompositionStandard = "标准成分写法";
            order.CompositionAbbr = "成分缩写";
            order.Creator = "xs.zhou";
            order.CreateTime = DateTime.Now;
            order.ProductType = "Target";
            order.ReviewPassed = true;
            order.Quantity = 1;
            order.QuantityUnit = "片";
            #endregion
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
                PMSHelper.ViewModels.Order.RefreshData();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.Order);
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

        public List<string> OrderStates { get; set; }
        public List<string> OrderPriorities { get; set; }
        public List<string> PolicyTypes { get; set; }
        public List<string> CustomerNames { get; set; }
        public List<string> ProductTypes { get; set; }

        public List<string> OrderUnits { get; set; }

    }
}
