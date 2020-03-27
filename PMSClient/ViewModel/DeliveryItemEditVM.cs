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
    public class DeliveryItemEditVM : BaseViewModelEdit
    {
        public DeliveryItemEditVM()
        {
            InitializeBasicData();

            InitialCommands();
        }

        private void InitializeBasicData()
        {
            States = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.SimpleState>(States);

            ProductTypes = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.ProductType>(ProductTypes);

            GoodPositions = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.GoodPosition>(GoodPositions);

            PackNumbers = new List<int>();
            PMSBasicDataService.SetListDS(PackNumbers, 10);

            CustomerNames = new List<string>();
            PMSBasicDataService.SetListDS(BasicData.Customers, CustomerNames, i => i.CustomerName);
        }

        public void SetNew(DcDelivery delivery)
        {
            IsNew = true;
            var model = new DcDeliveryItem();
            #region 初始化
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.DeliveryID = delivery.ID;
            model.ProductType = PMSCommon.ProductType.靶材.ToString();
            model.ProductID = DateTime.Now.ToString("yyMMdd");
            model.Composition = "填写成分";
            model.Abbr = "缩写";
            model.PO = "PO";
            model.Customer = "Midsummer";
            model.Weight = "重量";
            model.DetailRecord = "细节";
            model.Remark = "无";
            model.PackNumber = 1;
            model.Position = "无";
            model.Dimension = "无";
            model.DimensionActual = "无";
            model.Defects = "无";
            model.State = PMSCommon.SimpleState.正常.ToString();
            model.OrderNumber = 0;
            #endregion
            CurrentDeliveryItem = model;
        }

        public void SetEdit(DcDeliveryItem model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentDeliveryItem = model;
            }
        }

        /// <summary>
        /// 从测试记录中获得发货信息
        /// </summary>
        /// <param name="model"></param>
        public void SetBySelect(DcRecordTest model)
        {
            if (model != null)
            {
                CurrentDeliveryItem.ProductID = model.ProductID;
                CurrentDeliveryItem.Composition = model.Composition;
                CurrentDeliveryItem.Abbr = model.CompositionAbbr;
                CurrentDeliveryItem.Customer = model.Customer;
                CurrentDeliveryItem.Weight = model.Weight;
                CurrentDeliveryItem.PO = model.PO;
                CurrentDeliveryItem.Dimension = model.Dimension;
                CurrentDeliveryItem.DimensionActual = model.DimensionActual;
                CurrentDeliveryItem.Defects = model.Defects;

                //背板编号的处理
                var platelot = Helpers.DeliveryHelper.GetBPLotFromTest(model.ProductID);
                CurrentDeliveryItem.Remark = platelot;
                //RaisePropertyChanged(nameof(CurrentDeliveryItem));
            }
        }
        /// <summary>
        /// 从产品中获取发货信息
        /// </summary>
        /// <param name="model"></param>
        public void SetBySelect(DcProduct model)
        {
            if (model != null)
            {
                CurrentDeliveryItem.ProductType = model.ProductType;
                CurrentDeliveryItem.ProductID = model.ProductID;
                CurrentDeliveryItem.Composition = model.Composition;
                CurrentDeliveryItem.Abbr = model.Abbr;
                CurrentDeliveryItem.Customer = model.Customer;
                CurrentDeliveryItem.Weight = model.Weight;
                CurrentDeliveryItem.PO = model.PO;
                CurrentDeliveryItem.Dimension = model.Dimension;
                CurrentDeliveryItem.DimensionActual = model.DimensionActual;
                CurrentDeliveryItem.Defects = model.Defects;
                //背板编号的处理
                var platelot = Helpers.DeliveryHelper.GetBPLotFromTest(model.ProductID);
                CurrentDeliveryItem.Remark = platelot;
                //RaisePropertyChanged(nameof(CurrentDeliveryItem));
            }
        }

        public void SetBySelect(DcPlate model)
        {
            if (model != null)
            {
                CurrentDeliveryItem.ProductType = PMSCommon.ProductType.背板.ToString();
                CurrentDeliveryItem.ProductID = model.PlateLot;
                CurrentDeliveryItem.Composition = $"{model.PlateMaterial}背板";
                CurrentDeliveryItem.Abbr = model.PlateMaterial;
                CurrentDeliveryItem.Customer = "无";
                CurrentDeliveryItem.Weight = "未知";
                CurrentDeliveryItem.PO = "无";
                CurrentDeliveryItem.Dimension = model.Dimension;
                CurrentDeliveryItem.DimensionActual = model.Dimension;
                CurrentDeliveryItem.Defects = model.Defects;
                //RaisePropertyChanged(nameof(CurrentDeliveryItem));
            }
        }

        public void SetBySelect(PMSClient.NewService.DcPlanExtra model)
        {
            if (model != null)
            {
                CurrentDeliveryItem.ProductType = PMSCommon.ProductType.样品.ToString();
                CurrentDeliveryItem.ProductID = model.Plan.SearchCode;
                CurrentDeliveryItem.Composition = model.Misson.CompositionStandard;
                CurrentDeliveryItem.Abbr = model.Misson.CompositionAbbr;
                CurrentDeliveryItem.Customer = "未知";
                CurrentDeliveryItem.Weight = "未知";
                CurrentDeliveryItem.PO = "无";
                CurrentDeliveryItem.Dimension = model.Misson.Dimension;
                CurrentDeliveryItem.DimensionActual = model.Misson.Dimension;
                CurrentDeliveryItem.Defects = "";
            }
        }

        private void InitialCommands()
        {
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
            SelectProduct = new RelayCommand(ActionSelectProduct);
            SelectPlate = new RelayCommand(ActionSelectPlate);
            SelectOther = new RelayCommand(ActionSelectOther);
        }

        private void ActionSelectOther()
        {
            PMSHelper.ViewModels.PlanSelect.SetRequestView(PMSViews.DeliveryItemEdit);
            PMSHelper.ViewModels.PlanSelect.RefreshData();
            NavigationService.GoTo(PMSViews.PlanSelect);
        }

        private void ActionSelectPlate()
        {
            PMSHelper.ViewModels.PlateSelect.SetRequestView(PMSViews.DeliveryItemEdit);
            PMSBatchHelper.SetPlateBatchEnable(IsNew);
            NavigationService.GoTo(PMSViews.PlateSelect);
        }

        private void ActionSelectProduct()
        {
            PMSHelper.ViewModels.ProductSelect.SetRequestView(PMSViews.DeliveryItemEdit);
            PMSHelper.ViewModels.ProductSelect.RefreshData();
            PMSBatchHelper.SetProductBatchEnable(IsNew);

            NavigationService.GoTo(PMSViews.ProductSelect);
        }

        private void GoBack()
        {
            NavigationService.GoTo(PMSViews.Delivery);
        }

        private void ActionSave()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录？"))
            {
                return;
            }
            if (CurrentDeliveryItem.State == "作废")
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定作废这条记录？"))
                {
                    return;
                }
            }
            try
            {
                if (CurrentDeliveryItem != null)
                {
                    var service = new DeliveryServiceClient();
                    if (IsNew)
                    {
                        service.AddDeliveryItem(CurrentDeliveryItem);
                    }
                    else
                    {
                        service.UpdateDeliveryItem(CurrentDeliveryItem);
                    }
                    service.Close();
                    PMSHelper.ViewModels.Delivery.RefreshDataItem();
                    GoBack();
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }

        }
        public List<string> States { get; set; }
        public List<string> ProductTypes { get; set; }
        public List<string> GoodPositions { get; set; }
        public List<int> PackNumbers { get; set; }
        public List<string> CustomerNames { get; set; }
        private DcDeliveryItem currentDeliveryItem;
        public DcDeliveryItem CurrentDeliveryItem
        {
            get
            {
                return currentDeliveryItem;
            }
            set
            {
                currentDeliveryItem = value;
                RaisePropertyChanged(nameof(CurrentDeliveryItem));
            }
        }

        public RelayCommand SelectProduct { get; set; }
        public RelayCommand SelectPlate { get; set; }
        public RelayCommand SelectOther { get; set; }
    }
}
