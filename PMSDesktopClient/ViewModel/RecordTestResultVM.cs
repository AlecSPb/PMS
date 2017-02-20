using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSDesktopClient.PMSMainService;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;

namespace PMSDesktopClient.ViewModel
{
    public class RecordTestResultVM : ViewModelBase
    {
        public RecordTestResultVM()
        {
            Messenger.Default.Register<MsgObject>(this, VToken.RecordTestResultRefresh, ActionRefresh);
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }

        private void ActionRefresh(MsgObject obj)
        {
            SetPageParametersWhenConditionChange();
        }
        public override void Cleanup()
        {
            Messenger.Default.Unregister(this);
            base.Cleanup();
        }
        private void InitializeCommands()
        {
            GoToNavigation = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VToken.Navigation }));
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);
            Add = new RelayCommand(ActionAdd);
            Edit = new RelayCommand<DcRecordTestResult>(ActionEdit);
            Doc = new RelayCommand<DcRecordTestResult>(ActionDoc);
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

        private void ActionEdit(DcRecordTestResult obj)
        {
            MsgObject msg = new PMSDesktopClient.MsgObject();
            msg.MsgToken = VToken.RecordTestResultEdit;
            msg.MsgModel = new PMSDesktopClient.ModelObject() { IsNew = false, Model = obj };

            NavigationService.GoTo(msg);
        }

        private void ActionDoc(DcRecordTestResult obj)
        {
            //TODO:这里添加生成doc的代码
        }

        private void ActionAdd()
        {
            NavigationService.GoTo(new MsgObject() { MsgToken = VToken.PlanSelect });
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
        public RelayCommand GoToNavigation { get; set; }
        public RelayCommand Search { get; set; }
        public RelayCommand All { get; set; }
        public RelayCommand Report { get; set; }
        public RelayCommand Add { get; set; }
        public RelayCommand<DcRecordTestResult> Edit { get; set; }
        public RelayCommand<DcRecordTestResult> Doc { get; set; }
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
