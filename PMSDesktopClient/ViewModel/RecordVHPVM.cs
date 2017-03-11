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
            LoadRecordVHPsByRecordVHPID(MissonWithPlans.FirstOrDefault());
        }

        private void IntializeProperties()
        {
            SearchVHPID = "";
            MissonWithPlans = new ObservableCollection<DcMissonWithPlan>();
            RecordVHPs = new ObservableCollection<DcRecordVHP>();


            GoToNavigation = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VToken.Navigation }));
            All = new RelayCommand(ActionAll);
            Add = new RelayCommand(ActionAdd);
            Edit = new RelayCommand<DcMissonWithPlan>(ActionEdit);
            QuickEdit = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VToken.RecordVHPQuickEdit }));
            SelectionChanged = new RelayCommand<DcMissonWithPlan>(ActionSelectionChanged);
        }

        private void ActionSelectionChanged(DcMissonWithPlan obj)
        {
            if (obj != null)
            {
                LoadRecordVHPsByRecordVHPID(obj);
            }
        }

        public void LoadRecordVHPsByRecordVHPID(DcMissonWithPlan model)
        {
            var result = (new RecordVHPServiceClient()).GetRecordVHP(model.PlanID);
            RecordVHPs.Clear();
            result.ToList().ForEach(i => RecordVHPs.Add(i));
        }

        private void ActionEdit(DcMissonWithPlan obj)
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
            LoadRecordVHPsByRecordVHPID(MissonWithPlans.FirstOrDefault());
        }

        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 10;
            var service = new MissonServiceClient();
            RecordCount = service.GetMissonCountBySearch();
            ActionPaging();
        }
        /// <summary>
        /// 分页动作的时候读入数据
        /// </summary>
        private void ActionPaging()
        {
            var service = new MissonWithPlanServiceClient();
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var orders = service.GetMissonWithPlan(skip, take);
            MissonWithPlans.Clear();
            orders.ToList().ForEach(o => MissonWithPlans.Add(o));
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

        public ObservableCollection<DcMissonWithPlan> MissonWithPlans { get; set; }
        public ObservableCollection<DcRecordVHP> RecordVHPs { get; set; }
        #endregion

        #region Commands
        public RelayCommand GoToNavigation { get; set; }
        public RelayCommand All { get; set; }
        public RelayCommand Add { get; set; }

        public RelayCommand<DcMissonWithPlan> SelectionChanged { get; set; }
        public RelayCommand<DcMissonWithPlan> Edit { get; set; }
        public RelayCommand<DcMissonWithPlan> Doc { get; set; }


        public RelayCommand QuickEdit { get; set; }
        #endregion
    }
}
