using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PMSClient.ViewModel.Model;

namespace PMSClient.ViewModel
{
    public class ProductSelectVM : BaseViewModelSelect
    {
        public ProductSelectVM()
        {
            InitializeCommands();
            InitializeProperties();
            SetPageParametersWhenConditionChange();
        }

        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }

        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);
            Select = new RelayCommand<ProductExtra>(ActionSelect);
            SelectAndSend = new RelayCommand<ProductExtra>(ActionSelectAndSend);
            GiveUp = new RelayCommand(GoBack);
            InventoryOut = new RelayCommand(ActionInventoryOut);
        }

        private void ActionInventoryOut()
        {
          
        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchProductID) && string.IsNullOrEmpty(SearchCompositionStd));
        }

        private void ActionAll()
        {
            SearchProductID = SearchCompositionStd = "";
            SetPageParametersWhenConditionChange();
        }
        private PMSViews requestView;
        public void SetRequestView(PMSViews view)
        {
            requestView = view;
        }
        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void ActionSelect(ProductExtra model)
        {

            if (model != null)
            {
                switch (requestView)
                {
                    case PMSViews.DeliveryItemEdit:
                        //PMSHelper.ViewModels.DeliveryItemEdit.SetBySelect(model.Product);
                        break;
                    default:
                        break;
                }
                GoBack();

            }
        }
        private void ActionSelectAndSend(ProductExtra model)
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定要将此产品设置为发货状态并填入发货单项目中？"))
            {
                return;
            }

            try
            {
                if (model != null)
                {
                    using (var service = new ProductServiceClient())
                    {
                        model.Product.State = PMSCommon.InventoryState.发货.ToString();
                        service.UpdateProduct(model.Product);
                    }
                    ActionSelect(model);
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                throw;
            }
        }
        private void GoBack()
        {
            NavigationService.GoTo(requestView);
        }

        private void InitializeProperties()
        {
            ProductExtras = new ObservableCollection<ProductExtra>();
            SearchCompositionStd = searchProductID = "";

        }
        private void SetPageParametersWhenConditionChange()
        {
            try
            {
                PageIndex = 1;
                PageSize = 20;
                using (var service = new ProductServiceClient())
                {
                    RecordCount = service.GetProductCount(SearchProductID, SearchCompositionStd);
                }
                ActionPaging();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                throw;
            }

        }
        private void ActionPaging()
        {
            try
            {
                int skip, take = 0;
                skip = (PageIndex - 1) * PageSize;
                take = PageSize;
                using (var service = new ProductServiceClient())
                {
                    var orders = service.GetProducts(skip, take, SearchProductID, SearchCompositionStd);
                    ProductExtras.Clear();
                    orders.ToList().ForEach(o => ProductExtras.Add(new ProductExtra { Product = o }));
                }
                CurrentSelectItem = ProductExtras.FirstOrDefault();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                throw;
            }
        }
        #region Commands
        public RelayCommand<ProductExtra> Select { get; set; }

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

        public ObservableCollection<ProductExtra> ProductExtras { get; set; }
        private ProductExtra currentSelectItem;

        public ProductExtra CurrentSelectItem
        {
            get { return currentSelectItem; }
            set { currentSelectItem = value; RaisePropertyChanged(nameof(CurrentSelectItem)); }
        }
        #endregion


        public RelayCommand<ProductExtra> SelectAndSend { get; set; }

        public RelayCommand InventoryOut { get; set; }
    }
}
