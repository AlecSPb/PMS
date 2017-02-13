using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSDesktopClient.ServiceReference;
using System.Collections.ObjectModel;

namespace PMSDesktopClient.ViewModel
{
    public class RecordProductVM : ViewModelBase
    {
        public RecordProductVM()
        {
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }

        private void InitializeCommands()
        {
            Navigate = new RelayCommand(() => NavigationService.GoTo("NavigationView"));
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);
            Add = new RelayCommand<DcRecordProduct>(ActionAdd);
            Update = new RelayCommand<DcRecordProduct>(ActionUpdate);
            Delete = new RelayCommand<DcRecordProduct>(ActionDelete);
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

        private void ActionUpdate(DcRecordProduct obj)
        {
            throw new NotImplementedException();
        }

        private void ActionDelete(DcRecordProduct obj)
        {
            throw new NotImplementedException();
        }

        private void ActionAdd(DcRecordProduct obj)
        {
            throw new NotImplementedException();
        }

        private void InitializeProperties()
        {
            RecordProducts = new ObservableCollection<DcRecordProduct>();
            SearchCompositonStd = searchProductID = "";

        }
        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 10;
            var service = new RecordProductServiceClient();
            RecordCount = service.GetRecordProductCountBySearchInPage(SearchProductID, SearchCompositonStd);
            ActionPaging();
        }
        private void ActionPaging()
        {
            var service = new RecordProductServiceClient();
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var orders = service.GetRecordProductBySearchInPage(skip, take, SearchProductID, SearchCompositonStd);
            RecordProducts.Clear();
            orders.ToList<DcRecordProduct>().ForEach(o => RecordProducts.Add(o));
        }
        #region Commands
        public RelayCommand Navigate { get; set; }
        public RelayCommand Search { get; set; }
        public RelayCommand All { get; set; }
        public RelayCommand Report { get; set; }
        public RelayCommand<DcRecordProduct> Add { get; set; }
        public RelayCommand<DcRecordProduct> Update { get; set; }
        public RelayCommand<DcRecordProduct> Delete { get; set; }
        public RelayCommand PageChanged { get; private set; }
        #endregion
        #region PagingProperties
        private int pageIndex;
        public int PageIndex
        {
            get { return pageIndex; }
            set
            {
                pageIndex = value;
                RaisePropertyChanged(nameof(PageIndex));
            }
        }

        private int pageSize;
        public int PageSize
        {
            get { return pageSize; }
            set
            {
                pageSize = value;
                RaisePropertyChanged(nameof(PageSize));
            }
        }

        private int recordCount;
        public int RecordCount
        {
            get { return recordCount; }
            set
            {
                recordCount = value;
                RaisePropertyChanged(nameof(RecordCount));
            }
        }
        #endregion
        #region Properties
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

        public ObservableCollection<DcRecordProduct> RecordProducts { get; set; }
        #endregion
    }
}
