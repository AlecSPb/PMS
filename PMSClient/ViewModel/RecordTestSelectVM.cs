using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;
using PMSClient.ViewModel.Model;

namespace PMSClient.ViewModel
{
    public class RecordTestSelectVM : BaseViewModelSelect
    {

        public RecordTestSelectVM()
        {
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }

        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }

        private PMSViews requestView;
        public void SetRequestView(PMSViews view)
        {
            requestView = view;
        }
        private void InitializeCommands()
        {
            GiveUp = new RelayCommand(GoBack);
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);
            Select = new RelayCommand<RecordTestExtra>(ActionSelect);
            SelectBatch = new RelayCommand(ActionSelectBatch);
        }

        private void ActionSelectBatch()
        {
            try
            {
                var count = RecordTestExtras.Where(i => i.IsSelected == true).Count();
                if (!PMSDialogService.ShowYesNo("请问", $"请问要导入当前选定的{count}条记录吗？"))
                {
                    return;
                }

                switch (requestView)
                {
                    case PMSViews.ProductEdit:
                        BatchSaveProducts();
                        break;
                    case PMSViews.RecordBondingSimpleEdit:
                    case PMSViews.RecordBondingEdit:
                    case PMSViews.RecordBonding:
                        BatchSaveRecordBondings();
                        break;
                    case PMSViews.OutsideProcessEdit:
                        break;
                    default:
                        break;
                }
                PMSDialogService.Show("成功", "记录导入完成，请刷新列表");
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                NavigationService.Status(ex.Message);
            }
        }

        private void BatchSaveRecordBondings()
        {
            using (var service = new RecordBondingServiceClient())
            {
                foreach (var item in RecordTestExtras)
                {
                    if (item.IsSelected)
                    {
                        var model = item.RecordTest;
                        var temp = PMSNewModelCollection.NewRecordBonding();
                        temp.TargetProductID = model.ProductID;
                        temp.TargetCustomer = model.Customer;
                        temp.TargetComposition = model.Composition;
                        temp.TargetAbbr = model.CompositionAbbr;
                        temp.TargetPO = model.PO;
                        temp.TargetPMINumber = model.PMINumber;
                        temp.TargetWeight = model.Weight;
                        temp.TargetDimension = model.Dimension;
                        temp.TargetDimensionActual = model.DimensionActual;
                        temp.TargetDefects = model.Defects;
                        service.AddRecordBongdingByUID(temp, PMSHelper.CurrentSession.CurrentUser.UserName);
                    }
                }
                NavigationService.GoTo(PMSViews.RecordBonding);
            }
        }

        /// <summary>
        /// 批量导入到产品
        /// </summary>
        private void BatchSaveProducts()
        {
            using (var service = new ProductServiceClient())
            {
                foreach (var item in RecordTestExtras)
                {
                    if (item.IsSelected)
                    {
                        var temp = PMSNewModelCollection.NewProduct();
                        temp.ProductType = PMSCommon.ProductType.靶材.ToString();
                        temp.ProductID = item.RecordTest.ProductID;
                        temp.Customer = item.RecordTest.Customer;
                        temp.Composition = item.RecordTest.Composition;
                        temp.Abbr = item.RecordTest.CompositionAbbr;
                        temp.PO = item.RecordTest.PO;
                        temp.Weight = item.RecordTest.Weight;
                        temp.Dimension = item.RecordTest.Dimension;
                        temp.DimensionActual = item.RecordTest.DimensionActual;
                        temp.Defects = item.RecordTest.Defects;
                        temp.Remark = item.RecordTest.Remark;

                        service.AddProductByUID(temp, PMSHelper.CurrentSession.CurrentUser.UserName);
                    }
                }
                NavigationService.GoTo(PMSViews.Product);
            }

        }

        private void GoBack()
        {
            NavigationService.GoTo(requestView);
        }

        private bool CanSearch()
        {
            return !string.IsNullOrEmpty(SearchProductID) || !string.IsNullOrEmpty(SearchCompositionStd) || !string.IsNullOrEmpty(SearchPMINumber);
        }

        private void ActionAll()
        {
            SearchProductID = SearchCompositionStd = SearchPMINumber = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void ActionSelect(RecordTestExtra model)
        {
            if (model != null)
            {
                switch (requestView)
                {
                    case PMSViews.RecordBondingSimpleEdit:
                    case PMSViews.RecordBondingEdit:
                        PMSHelper.ViewModels.RecordBondingEdit.SetBySelect(model.RecordTest);
                        break;
                    case PMSViews.ProductEdit:
                        PMSHelper.ViewModels.ProductEdit.SetBySelect(model.RecordTest);
                        break;
                    case PMSViews.DeliveryItemEdit:
                        PMSHelper.ViewModels.DeliveryItemEdit.SetBySelect(model.RecordTest);
                        break;
                    case PMSViews.OutsideProcessEdit:
                        PMSHelper.ViewModels.OutsideProcessEdit.SetBySelect2(model.RecordTest);
                        break;
                    default:
                        break;
                }
                GoBack();
            }
        }

        private void InitializeProperties()
        {
            RecordTestExtras = new ObservableCollection<RecordTestExtra>();
            SearchCompositionStd = SearchProductID = SearchPMINumber = "";

        }
        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 30;
            using (var service = new RecordTestServiceClient())
            {
                RecordCount = service.GetRecordTestCountBySearch(SearchProductID, SearchCompositionStd, SearchPMINumber);
            }
            ActionPaging();
        }
        private void ActionPaging()
        {
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            using (var service = new RecordTestServiceClient())
            {
                var orders = service.GetRecordTestBySearch(skip, take, SearchProductID, SearchCompositionStd, SearchPMINumber);
                RecordTestExtras.Clear();
                orders.ToList().ForEach(o => RecordTestExtras.Add(new Model.RecordTestExtra { RecordTest = o }));
            }
        }
        #region Commands
        public RelayCommand<RecordTestExtra> Select { get; set; }

        #endregion

        #region Properties
        private string searchPMINumber;
        public string SearchPMINumber
        {
            get { return searchPMINumber; }
            set
            {
                if (searchPMINumber == value)
                    return;
                searchPMINumber = value;
                RaisePropertyChanged(() => SearchPMINumber);
            }
        }
        private string searchProductID;
        public string SearchProductID
        {
            get { return searchProductID; }
            set
            {
                if (searchProductID == value)
                    return;
                searchProductID = value;
                RaisePropertyChanged(() => SearchProductID);
            }
        }
        private string searchCompositionStd;
        public string SearchCompositionStd
        {
            get { return searchCompositionStd; }
            set
            {
                if (searchCompositionStd == value)
                    return;
                searchCompositionStd = value;
                RaisePropertyChanged(() => SearchCompositionStd);
            }
        }

        public ObservableCollection<RecordTestExtra> RecordTestExtras { get; set; }
        public RelayCommand SelectBatch { get; set; }
        #endregion
    }
}
