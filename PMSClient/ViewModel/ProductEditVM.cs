using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.ViewModel
{
    public class ProductEditVM : BaseViewModelEdit
    {
        public ProductEditVM()
        {
            States = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.InventoryState>(States);

            ProductTypes = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.ProductType>(ProductTypes);

            GoodPositions = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.GoodPosition>(GoodPositions);

            CustomerNames = new List<string>();
            PMSBasicDataService.SetListDS(BasicData.Customers, CustomerNames, i => i.CustomerName);

            InitializeCommands();
        }

        private void InitializeCommands()
        {
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
            SelectTest = new RelayCommand(ActionSelectTest);
            SelectBonding = new RelayCommand(ActionSelectBonding);
            SelectOutSource = new RelayCommand(ActionSelectOutSource);
            SelectOther = new RelayCommand(ActionSelectOther);
        }

        public void SetNew()
        {
            IsNew = true;
            var model = new DcProduct();
            #region 初始化
            model.ID = Guid.NewGuid();
            model.ProductID = UsefulPackage.PMSTranslate.PlanLot();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.Composition = "成分";
            model.Abbr = "缩写";
            model.Weight = "";
            model.PO = "TBD";
            model.Customer = "Midsummer";
            model.Position = "无";
            model.ProductType = PMSCommon.ProductType.靶材.ToString();
            model.State = PMSCommon.InventoryState.库存.ToString();
            model.Remark = "";

            model.Dimension = "尺寸";
            model.DimensionActual = "实际尺寸";
            model.Defects = "无";



            #endregion
            CurrentProduct = model;
        }
        public void SetEdit(DcProduct model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentProduct = model;
            }
        }
        public void SetDuplicate(DcProduct model)
        {
            if (model != null)
            {
                IsNew = true;
                CurrentProduct = model;
                CurrentProduct.ID = Guid.NewGuid();
                CurrentProduct.CreateTime = DateTime.Now;
                CurrentProduct.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                CurrentProduct.State = PMSCommon.InventoryState.库存.ToString();
            }
        }
        public void SetBySelect(DcRecordTest model)
        {
            if (model != null)
            {
                CurrentProduct.ProductType = PMSCommon.ProductType.靶材.ToString();
                CurrentProduct.ProductID = model.ProductID;
                CurrentProduct.Customer = model.Customer;
                CurrentProduct.Composition = model.Composition;
                CurrentProduct.Abbr = model.CompositionAbbr;
                CurrentProduct.PO = model.PO;
                CurrentProduct.Weight = model.Weight;
                CurrentProduct.Dimension = model.Dimension;
                CurrentProduct.DimensionActual = model.DimensionActual;
                CurrentProduct.Defects = model.Defects;
                CurrentProduct.Remark = model.Remark;
                //RaisePropertyChanged(nameof(CurrentProduct));
            }
        }
        public void SetBySelect(DcRecordBonding model)
        {
            if (model != null)
            {
                CurrentProduct.ProductType = PMSCommon.ProductType.绑定.ToString();
                CurrentProduct.ProductID = model.TargetProductID;
                CurrentProduct.Customer = model.TargetCustomer;
                CurrentProduct.Composition = model.TargetComposition;
                CurrentProduct.Abbr = model.TargetAbbr;
                CurrentProduct.PO = model.TargetPO;
                CurrentProduct.Weight = model.TargetWeight;
                CurrentProduct.Dimension = model.TargetDimension;
                CurrentProduct.DimensionActual = model.TargetDimensionActual;
                CurrentProduct.Defects = model.TargetDefects;
                CurrentProduct.Remark = $"背板编号:{model.PlateLot}";

                //RaisePropertyChanged(nameof(CurrentProduct));
            }
        }

        public void SetBySelect(DcPlanWithMisson model)
        {
            if (model != null)
            {
                CurrentProduct.ProductType = PMSCommon.ProductType.样品.ToString();
                CurrentProduct.ProductID = model.Plan.SearchCode;
                CurrentProduct.Customer = model.Misson.CustomerName;
                CurrentProduct.Composition = model.Misson.CompositionStandard;
                CurrentProduct.Abbr = model.Misson.CompositionAbbr;
                CurrentProduct.PO = model.Misson.PO;
                CurrentProduct.Weight = "未知";
                CurrentProduct.Dimension = model.Misson.Dimension;
                CurrentProduct.DimensionActual = model.Misson.Dimension;
                CurrentProduct.Defects = "无";
                CurrentProduct.Remark = "";
            }
        }


        private void ActionSelectOutSource()
        {
            var vm = PMSHelper.ViewModels.OutSourceSelect;
            vm.RequestView = PMSViews.ProductEdit;
            vm.RefreshData();
            NavigationService.GoTo(PMSViews.OutSourceSelect);
        }
        private void ActionSelectTest()
        {
            var vm = PMSHelper.ViewModels.RecordTestSelect;
            vm.SetRequestView(PMSViews.ProductEdit);
            vm.RefreshData();
            PMSBatchHelper.SetRecordTestBatchEnable(IsNew);
            NavigationService.GoTo(PMSViews.RecordTestSelect);
        }
        private void ActionSelectBonding()
        {
            PMSHelper.ViewModels.RecordBondingSelect.SetRequestView(PMSViews.ProductEdit);
            PMSHelper.ViewModels.RecordBondingSelect.RefreshData();
            PMSBatchHelper.SetRecordBondingBatchEnable(IsNew);
            NavigationService.GoTo(PMSViews.RecordBondingSelect);
        }

        private void ActionSelectOther()
        {
            PMSHelper.ViewModels.PlanSelect.SetRequestView(PMSViews.ProductEdit);
            PMSHelper.ViewModels.PlanSelect.RefreshData();
            NavigationService.GoTo(PMSViews.PlanSelect);
        }
        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.Product);
        }

        private void ActionSave()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录？"))
            {
                return;
            }
            if (CurrentProduct.State == "作废")
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定要作废吗？"))
                {
                    return;
                }
            }
            try
            {
                string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                var service = new ProductServiceClient();
                if (IsNew)
                {
                    service.AddProductByUID(CurrentProduct, uid);
                }
                else
                {
                    service.UpdateProductByUID(CurrentProduct, uid);
                }
                service.Close();
                PMSHelper.ViewModels.Product.RefreshData();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
        public List<string> ProductTypes { get; set; }
        public List<string> States { get; set; }

        public List<string> GoodPositions { get; set; }

        public List<string> CustomerNames { get; set; }

        private DcProduct currentProduct;
        public DcProduct CurrentProduct
        {
            get { return currentProduct; }
            set
            {
                currentProduct = value;
                RaisePropertyChanged(nameof(CurrentProduct));
            }
        }
        public RelayCommand SelectTest { get; set; }
        public RelayCommand SelectBonding { get; set; }
        public RelayCommand SelectOutSource { get; set; }
        public RelayCommand SelectOther { get; set; }
    }
}
