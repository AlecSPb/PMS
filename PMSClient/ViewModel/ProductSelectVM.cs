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
        private void InitializeProperties()
        {
            ProductExtras = new ObservableCollection<ProductExtra>();
            SearchCompositionStd = searchProductID = "";

        }
        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);
            Send = new RelayCommand<ProductExtra>(ActionSend);
            SendSelected = new RelayCommand(ActioSendSelected);
            GiveUp = new RelayCommand(GoBack);
        }

        /// <summary>
        /// 用户视图用来刷新数据，通常使用前刷新
        /// </summary>
        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }
        /// <summary>
        /// 批量发货
        /// </summary>
        private void ActioSendSelected()
        {
            int selectedCount = ProductExtras.Where(i => i.IsSelected == true).Count();

            if (!PMSDialogService.ShowYesNo("请问", $"请问要批量出库选中的{selectedCount}条记录吗？"))
            {
                return;
            }
            try
            {
                switch (requestView)
                {
                    case PMSViews.DeliveryItemEdit:
                        //获取外键
                        Guid id = PMSHelper.ViewModels.DeliveryItemEdit.CurrentDeliveryItem.DeliveryID;
                        BatchInsertDeliveryItem(id);
                        break;
                    default:
                        break;
                }
                PMSDialogService.ShowYes("成功", "添加成功，即将返回列表视图,之后请刷新列表看到最新信息");
                NavigationService.GoTo(PMSViews.Delivery);
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                NavigationService.ShowStatusMessage(ex.Message);
            }

        }

        private void BatchInsertDeliveryItem(Guid deliveryid)
        {
            try
            {
                var serviceDelivery = new DeliveryServiceClient();
                var serviceProduct = new ProductServiceClient();
                foreach (var item in ProductExtras)
                {
                    if (item.IsSelected)
                    {
                        var model = item.Product;
                        var deliveryItem = PMSNewModelCollection.NewDeliveryItem(deliveryid);
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
                        //System.Diagnostics.Debug.Print(item.IsSelected.ToString() + item.Product.ProductID);
                        var uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                        serviceDelivery.AddDeliveryItemByUID(deliveryItem, uid);

                        item.Product.State = PMSCommon.InventoryState.发货.ToString();
                        serviceProduct.UpdateProductByUID(item.Product, uid);
                    }
                }
                serviceDelivery.Close();
                serviceProduct.Close();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                NavigationService.ShowStatusMessage(ex.Message);
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
        /// <summary>
        /// 单个发货
        /// </summary>
        /// <param name="model"></param>
        private void ActionSend(ProductExtra model)
        {

            if (!PMSDialogService.ShowYesNo("请问", "确定要将此产品设置为发货状态\r\n并填入发货项目编辑页面中？"))
            {
                return;
            }

            try
            {
                if (model != null)
                {
                    PMSHelper.ViewModels.DeliveryItemEdit.SetBySelect(model.Product);
                    using (var service = new ProductServiceClient())
                    {
                        model.Product.State = PMSCommon.InventoryState.发货.ToString();
                        service.UpdateProduct(model.Product);
                    }
                    GoBack();
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                NavigationService.ShowStatusMessage(ex.Message);
            }
        }
        private void GoBack()
        {
            NavigationService.GoTo(requestView);
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
                NavigationService.ShowStatusMessage(ex.Message);
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
                NavigationService.ShowStatusMessage(ex.Message);
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

        public ObservableCollection<ProductExtra> ProductExtras { get; set; }
        private ProductExtra currentSelectItem;
        public ProductExtra CurrentSelectItem
        {
            get { return currentSelectItem; }
            set { currentSelectItem = value; RaisePropertyChanged(nameof(CurrentSelectItem)); }
        }

        public RelayCommand<ProductExtra> Send { get; set; }
        public RelayCommand SendSelected { get; set; }
    }
}
