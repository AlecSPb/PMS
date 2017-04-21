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
            Select = new RelayCommand<DcProduct>(ActionSelect);
            SelectAndSend = new RelayCommand<DcProduct>(ActionSelectAndSend);
            GiveUp = new RelayCommand(GoBack);
        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchProductID) && string.IsNullOrEmpty(SearchCompositionStd));
        }

        private void ActionAll()
        {
            SearchProductID = SearchCompositionStd = "";
            ActionPaging();
        }
        private PMSViews requestView;
        public void SetRequestView(PMSViews view)
        {
            requestView = view;
        }
        private void ActionSearch()
        {
            ActionPaging();
        }

        private void ActionEdit(DcProduct model)
        {
            PMSHelper.ViewModels.ProductEdit.SetEdit(model);
            NavigationService.GoTo(PMSViews.ProductEdit);
        }

        private void ActionSelect(DcProduct model)
        {

            if (model != null)
            {
                switch (requestView)
                {
                    case PMSViews.DeliveryItemEdit:
                        PMSHelper.ViewModels.DeliveryItemEdit.SetBySelect(model);
                        break;
                    default:
                        break;
                }
                GoBack();

            }
        }
        private void ActionSelectAndSend(DcProduct model)
        {
            if (MessageBox.Show("确定要将此产品设置为发货状态并填入发货单项目中？", "请问", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                return;
            }

            if (model != null)
            {
                using (var service = new ProductServiceClient())
                {
                    model.State = PMSCommon.ProductState.发货.ToString();
                    service.UpdateProduct(model);
                }
                ActionSelect(model);
            }
        }
        private void GoBack()
        {
            NavigationService.GoTo(requestView);
        }

        private void InitializeProperties()
        {
            Products = new ObservableCollection<DcProduct>();
            SearchCompositionStd = searchProductID = "";

        }
        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 20;
            using (var service = new ProductServiceClient())
            {
                RecordCount = service.GetProductCount(SearchProductID, SearchCompositionStd);
            }
            ActionPaging();
        }
        private void ActionPaging()
        {
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            using (var service = new ProductServiceClient())
            {
                var orders = service.GetProducts(skip, take, SearchProductID, SearchCompositionStd);
                Products.Clear();
                orders.ToList().ForEach(o => Products.Add(o));
            }
            CurrentSelectItem = Products.FirstOrDefault();
        }
        #region Commands
        public RelayCommand<DcProduct> Select { get; set; }

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

        public ObservableCollection<DcProduct> Products { get; set; }
        private DcProduct currentSelectItem;

        public DcProduct CurrentSelectItem
        {
            get { return currentSelectItem; }
            set { currentSelectItem = value; RaisePropertyChanged(nameof(CurrentSelectItem)); }
        }
        #endregion


        public RelayCommand<DcProduct> SelectAndSend { get; set; }
    }
}
