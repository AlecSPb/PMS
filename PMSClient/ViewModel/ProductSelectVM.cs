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

        public Guid ForeignKey { get; set; }
        private void ActionInventoryOut()
        {
            int selectedCount = 0;
            foreach (var item in ProductExtras)
            {
                if (item.IsSelected)
                {
                    selectedCount++;
                }
            }

            if (!PMSDialogService.ShowYesNo("请问",$"请问要批量添加选中的{selectedCount}条记录吗？"))
            {
                return;
            }
            try
            {
                switch (requestView)
                {
                    case PMSViews.DeliveryItemEdit:
                        BatchInsertDeliveryItem(ForeignKey);
                        break;
                    default:
                        break;
                }
                PMSDialogService.ShowYes("成功", "添加成功，返回列表视图,请刷新");
                NavigationService.GoTo(PMSViews.Delivery);
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }

        }

        private void BatchInsertDeliveryItem(Guid deliveryid)
        {
            using (var service = new DeliveryServiceClient())
            {
                foreach (var item in ProductExtras)
                {
                    if (item.IsSelected)
                    {
                        var model = item.Product;
                        //System.Diagnostics.Debug.Print(item.IsSelected.ToString() + item.Product.ProductID);
                        var deliveryItem = NewModelCollection.NewDeliveryItem(deliveryid);
                        deliveryItem.ProductType = PMSCommon.ProductType.靶材.ToString();
                        deliveryItem.ProductID = model.ProductID;
                        deliveryItem.Composition = model.Composition;
                        deliveryItem.Abbr = model.Abbr;
                        deliveryItem.Customer = model.Customer;
                        deliveryItem.Weight = model.Weight;
                        deliveryItem.PO = model.PO;
                        deliveryItem.Dimension = model.Dimension;
                        deliveryItem.DimensionActual = model.DimensionActual;
                        deliveryItem.Defects = model.Defects;

                        service.AddDeliveryItem(deliveryItem);
                    }
                }
            }
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
                        PMSHelper.ViewModels.DeliveryItemEdit.SetBySelect(model.Product);
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
