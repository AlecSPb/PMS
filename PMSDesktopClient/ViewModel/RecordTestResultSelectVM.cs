using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSDesktopClient.PMSMainService;
using System.Collections.ObjectModel;

namespace PMSDesktopClient.ViewModel
{
    public class RecordTestResultTestVM : ViewModelBase
    {
        public RecordTestResultTestVM()
        {
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }

        private void InitializeCommands()
        {
            GiveUp = new RelayCommand(() => NavigationService.GoTo(VT.RecordDelivery.ToString()));
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);
            Select = new RelayCommand<DcRecordTestResult>(ActionSelect);
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

        private void ActionSelect(DcRecordTestResult obj)
        {
            MsgObject msg = new PMSDesktopClient.MsgObject();
            msg.GoToToken = VT.RecordTestResultEdit.ToString();
            msg.Model = new PMSDesktopClient.ModelObject() { IsNew = false, Model = obj };

            NavigationService.GoToWithParameter(msg);
        }

        private void InitializeProperties()
        {
            RecordProducts = new ObservableCollection<DcRecordTestResult>();
            SearchCompositonStd = searchProductID = "";

        }
        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 10;
            var service = new RecordTestResultServiceClient();
            RecordCount = service.GetRecordTestResultCountBySearchInPage(SearchProductID, SearchCompositonStd);
            ActionPaging();
        }
        private void ActionPaging()
        {
            var service = new RecordTestResultServiceClient();
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var orders = service.GetRecordTestResultBySearchInPage(skip, take, SearchProductID, SearchCompositonStd);
            RecordProducts.Clear();
            orders.ToList<DcRecordTestResult>().ForEach(o => RecordProducts.Add(o));
        }
        #region Commands
        public RelayCommand GiveUp { get; set; }
        public RelayCommand Search { get; set; }
        public RelayCommand All { get; set; }
        public RelayCommand<DcRecordTestResult> Select { get; set; }
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

        public ObservableCollection<DcRecordTestResult> RecordProducts { get; set; }
        #endregion
    }
}
