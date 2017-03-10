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
    public class RecordVHPVM : ViewModelBase
    {
        public RecordVHPVM()
        {
            InitializeCommands();
            IntializeProperties();
            SetPageParametersWhenConditionChange();
            LoadRecordVHPItemsByRecordVHPID(RecordVHPs.FirstOrDefault());
        }

        private void IntializeProperties()
        {
            SearchVHPID = "";
            RecordVHPs = new ObservableCollection<PMSMainService.DcRecordVHP>();
            RecordVHPItems = new ObservableCollection<PMSMainService.DcRecordVHPItem>();


            GoToNavigation = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VToken.Navigation }));
            All = new RelayCommand(ActionAll);
            Add = new RelayCommand(ActionAdd);
            Edit = new RelayCommand<PMSMainService.DcRecordVHP>(ActionEdit);
            QuickEdit= new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VToken.RecordVHPQuickEdit }));
            SelectionChanged = new RelayCommand<PMSMainService.DcRecordVHP>(ActionSelectionChanged);
        }

        private void ActionSelectionChanged(DcRecordVHP obj)
        {
            if (obj!=null)
            {
                LoadRecordVHPItemsByRecordVHPID(obj);
            }
        }

        public void LoadRecordVHPItemsByRecordVHPID(DcRecordVHP model)
        {
            var result = (new RecordVHPServiceClient()).GetRecordVHPItemsByRecrodVHPID(model.ID);
            RecordVHPItems.Clear();
            result.ToList().ForEach(i => RecordVHPItems.Add(i));
        }

        private void ActionEdit(DcRecordVHP obj)
        {
            MsgObject msg = new PMSDesktopClient.MsgObject();
            msg.MsgToken = VToken.RecordVHPEdit;
            msg.MsgModel = new ModelObject() { IsNew = false, Model = obj };
            NavigationService.GoTo(msg);
        }

        private void ActionAdd()
        {
            NavigationService.GoTo(new MsgObject() { MsgToken = VToken.PlanSelectForVHP });
        }

        private void ActionAll()
        {
            SetPageParametersWhenConditionChange();
            LoadRecordVHPItemsByRecordVHPID(RecordVHPs.FirstOrDefault());
        }

        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 10;
            var service = new RecordVHPServiceClient();
            RecordCount = service.GetRecordVHPCount();
            ActionPaging();
        }
        /// <summary>
        /// 分页动作的时候读入数据
        /// </summary>
        private void ActionPaging()
        {
            var service = new RecordVHPServiceClient();
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var orders = service.GetRecordVHP(skip, take);
            RecordVHPs.Clear();
            orders.ToList<DcRecordVHP>().ForEach(o => RecordVHPs.Add(o));
        }


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
        public RelayCommand PageChanged { get; private set; }
        #endregion

        #region Properties
        private string searchVHPID;

        public string SearchVHPID
        {
            get { return searchVHPID; }
            set { searchVHPID = value; RaisePropertyChanged(nameof(SearchVHPID)); }
        }

        public ObservableCollection<DcRecordVHP> RecordVHPs { get; set; }
        public ObservableCollection<DcRecordVHPItem> RecordVHPItems { get; set; }
        #endregion

        #region Commands
        public RelayCommand GoToNavigation { get; set; }
        public RelayCommand All { get; set; }
        public RelayCommand Add { get; set; }

        public RelayCommand <DcRecordVHP> SelectionChanged { get; set; }
        public RelayCommand<DcRecordVHP> Edit { get; set; }
        public RelayCommand<DcRecordVHP> Doc { get; set; }


        public RelayCommand QuickEdit { get; set; }
        #endregion
    }
}
