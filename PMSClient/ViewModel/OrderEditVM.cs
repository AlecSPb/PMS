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
using PMSClient.BasicService;


namespace PMSClient.ViewModel
{
    public class OrderEditVM : BaseViewModelEdit
    {
        public OrderEditVM()
        {
            InitializeProperties();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            Save = new RelayCommand(ActionSave, CanSave);
            GiveUp = new RelayCommand(GoBack);
            CheckPMINumber = new RelayCommand(ActionCheckPMINumber);
        }

        private void ActionCheckPMINumber()
        {
            if (CurrentOrder!=null)
            {
                using (var service=new OrderServiceClient())
                {
                    CanUseThisPMINumber = service.CheckPMINumberExisit(CurrentOrder.PMINumber) ? "被占用" : "可以用";
                }
            }
        }

        public void InitializeProperties()
        {
            canUseThisPMINumber = "";

            ProductTypes = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.OrderProductType>(ProductTypes);

            OrderUnits = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.OrderUnit>(OrderUnits);

            SampleNeeds = new List<string>();
            PMSBasicDataService.SetListDS(PMSCommon.CustomData.OrderSampleNeeds, SampleNeeds);

            CustomerNames = new List<string>();
            //BasicData.Customers.ToList().ForEach(c => CustomerNames.Add(c.CustomerName));
            PMSBasicDataService.SetListDS(BasicData.Customers, CustomerNames, i => i.CustomerName);
        }

        public void SetNew()
        {
            IsNew = true;
            var order = new DcOrder();
            #region 初始化order
            order.ID = Guid.NewGuid();
            order.CustomerName = CustomerNames.FirstOrDefault();
            order.PO = DateTime.Now.ToString("yyMMdd");
            order.CompositionOriginal = "CuInGaSe";
            order.CompositionStandard = "";
            order.CompositionAbbr = "";
            order.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            order.CreateTime = DateTime.Now;
            order.PMINumber = UsefulPackage.PMSTranslate.PMINumber();
            order.ProductType = PMSCommon.OrderProductType.靶材.ToString();
            order.Purity = "99.99";
            order.Quantity = 1;
            order.QuantityUnit = PMSCommon.OrderUnit.片.ToString();
            order.Dimension = "230mm OD x  4mm";
            order.DimensionDetails = "无";
            order.SampleNeed = PMSCommon.CustomData.OrderSampleNeeds[0];
            order.MinimumAcceptDefect = "通常";
            order.DeadLine = DateTime.Now.AddDays(30);
            order.PolicyType = PMSCommon.OrderPolicyType.VHP.ToString();
            order.State = PMSCommon.OrderState.未核验.ToString();
            order.Priority = PMSCommon.OrderPriority.普通.ToString();
            order.Reviewer = "";
            order.ReviewTime = DateTime.Now;
            #endregion
            CurrentOrder = order;
        }
        public void SetEdit(DcOrder order)
        {
            if (order != null)
            {
                IsNew = false;
                CurrentOrder = order;
            }
        }

        public void SetDuplicate(DcOrder order)
        {
            if (order != null)
            {
                IsNew = true;
                CurrentOrder = order;
                CurrentOrder.ID = Guid.NewGuid();
                CurrentOrder.State = PMSCommon.OrderState.未核验.ToString();
                currentOrder.CreateTime = DateTime.Now;
                CurrentOrder.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            }
        }

        private bool CanSave()
        {
            return true;
        }

        private void ActionSave()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录？"))
            {
                return;
            }
            try
            {
                string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                var service = new OrderServiceClient();
                if (IsNew)
                {
                    service.AddOrderByUID(CurrentOrder, uid);
                }
                else
                {
                    service.UpdateOrderByUID(CurrentOrder, uid);
                }
                service.Close();
                //PMSHelper.ViewModels.Order.RefreshData();
                GoBack();
                NavigationService.Status("保存成功，请刷新列表");

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
        private string canUseThisPMINumber;

        public string CanUseThisPMINumber
        {
            get { return canUseThisPMINumber; }
            set { canUseThisPMINumber = value; RaisePropertyChanged(nameof(CanUseThisPMINumber)); }
        }

        public RelayCommand CheckPMINumber { get; set; }


        public List<string> CustomerNames { get; set; }
        public List<string> ProductTypes { get; set; }
        public List<string> OrderUnits { get; set; }
        public List<string> SampleNeeds { get; set; }
    }
}
