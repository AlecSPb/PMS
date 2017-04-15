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
    public class ProductVM:BaseViewModelPage
    {
        public ProductVM()
        {

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
            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<DcProduct>(ActionEdit, CanEdit);
            Doc = new RelayCommand<DcProduct>(ActionDoc, CanDoc);
        }

        private bool CanDoc(DcProduct arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑产品记录");
        }

        private bool CanEdit(DcProduct arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑产品记录");
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑产品记录");
        }

        private void ActionSelectionChanged(DcProduct model)
        {
            CurrentSelectItem = model;
        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchProductID) && string.IsNullOrEmpty(SearchCompositonStd));
        }

        private void ActionAll()
        {
            SearchProductID = SearchCompositonStd = "";
            ActionPaging();
        }

        private void ActionSearch()
        {
            ActionPaging();
        }

        private void ActionDuplicate(DcProduct model)
        {
            PMSHelper.ViewModels.ProductEdit.SetNew(model);
            NavigationService.GoTo(PMSViews.ProductEdit);
        }

        private void ActionEdit(DcProduct model)
        {
            PMSHelper.ViewModels.ProductEdit.SetEdit(model);
            NavigationService.GoTo(PMSViews.ProductEdit);
        }

        private void ActionDoc(DcProduct model)
        {
            if (model != null)
            {
                //PMSHelper.ViewModels.ProductDoc.SetModel(model);
                //NavigationService.GoTo(PMSViews.ProductDoc);
            }

        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.ProductEdit.SetNew();
            NavigationService.GoTo(PMSViews.ProductEdit);
        }

        private void InitializeProperties()
        {
            RecordProducts = new ObservableCollection<DcProduct>();
            SearchCompositonStd = searchProductID = "";

        }
        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 10;
            using (var service = new RecordTestServiceClient())
            {
                RecordCount = service.GetRecordTestCountBySearchInPage(SearchProductID, SearchCompositonStd);
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
                var orders = service.GetRecordTestBySearchInPage(skip, take, SearchProductID, SearchCompositonStd);
                RecordProducts.Clear();
                orders.ToList().ForEach(o => RecordProducts.Add(o));
            }
            CurrentSelectItem = RecordProducts.FirstOrDefault();
        }
        #region Commands
        public RelayCommand Report { get; set; }
        public RelayCommand Add { get; set; }
        public RelayCommand<DcProduct> Edit { get; set; }
        public RelayCommand<DcProduct> Doc { get; set; }

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
        public string SearchCompositonStd
        {
            get { return searchCompositionStd; }
            set
            {
                if (searchCompositionStd == value)
                    return;
                searchCompositionStd = value;
                RaisePropertyChanged(() => SearchCompositonStd);
            }
        }

        public ObservableCollection<DcProduct> RecordProducts { get; set; }
        private DcProduct currentSelectItem;

        public DcProduct CurrentSelectItem
        {
            get { return currentSelectItem; }
            set { currentSelectItem = value; RaisePropertyChanged(nameof(CurrentSelectItem)); }
        }

        public RelayCommand<DcProduct> SelectionChanged { get; set; }
        #endregion

    }
}
