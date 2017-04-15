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

            ProductTypes = new List<string>();
            PMSMethods.SetListDS<PMSCommon.OrderProductType>(ProductTypes);

            OrderUnits = new List<string>();
            PMSMethods.SetListDS<PMSCommon.OrderUnit>(OrderUnits);


            CustomerNames = new List<string>();
            var customerNames = PMSBasicData.Customers;
            customerNames.ToList().ForEach(c => CustomerNames.Add(c.CustomerName));
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
            order.CustomerName = CustomerNames.FirstOrDefault();
            order.PO = DateTime.Now.ToString("yyMMdd");
            order.CompositionOriginal = "原始成分写法";
            order.CompositionStandard = "标准成分写法";
            order.CompositionAbbr = "成分缩写";
            order.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            order.CreateTime = DateTime.Now;
            order.PMINumber = DateTime.Now.ToString("yyMMdd");
            order.ProductType = PMSCommon.OrderProductType.Target.ToString();
            order.Purity = "99.99";
            order.Quantity = 1;
            order.QuantityUnit = PMSCommon.OrderUnit.片.ToString();
            order.Dimension = "230mm OD x  4mm";
            order.DimensionDetails = "None";
            order.SampleNeed = "无需样品";
            order.MinimumAcceptDefect = "通常";
            order.DeadLine = DateTime.Now.AddDays(30);
            order.State = PMSCommon.OrderState.未核验.ToString();
            order.Priority =PMSCommon.OrderPriority.通常.ToString();
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
        public List<string> CustomerNames { get; set; }
        public List<string> ProductTypes { get; set; }
        public List<string> OrderUnits { get; set; }

    }
}
