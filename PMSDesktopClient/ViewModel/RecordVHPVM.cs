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
    public class RecordVHPVM:ViewModelBase
    {
        public RecordVHPVM()
        {
            InitializeCommands();
            IntializeProperties();
        }

        private void IntializeProperties()
        {
            SearchVHPID = "";
            RecordVHPs = new ObservableCollection<PMSMainService.DcRecordVHP>();
            SetPageParametersWhenConditionChange();
        }

        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 20;
            var service = new RecordVHPServiceClient();
            RecordCount = service.GetRecordVHPCount(SearchVHPID);
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
            var orders = service.GetRecordVHP(skip, take, SearchVHPID);
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
            set { searchVHPID = value;RaisePropertyChanged(nameof(SearchVHPID)); }
        }

        public ObservableCollection<DcRecordVHP> RecordVHPs { get; set; }
        #endregion
    }
}
