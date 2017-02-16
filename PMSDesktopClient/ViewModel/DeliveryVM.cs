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
    public class DeliveryVM:ViewModelBase
    {
        public DeliveryVM()
        {
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }

        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
        }

        private void InitializeProperties()
        {
            RecordDeliveries = new ObservableCollection<PMSMainService.DcRecordDelivery>();
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 20;
            var service = new RecordDeliveryServiceClient();
            RecordCount = service.GetDeliveryCount();
            ActionPaging();
        }
        /// <summary>
        /// 分页动作的时候读入数据
        /// </summary>
        private void ActionPaging()
        {
            var service = new RecordDeliveryServiceClient();
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var orders = service.GetDelivery(skip, take);
            RecordDeliveries.Clear();
            orders.ToList<DcRecordDelivery>().ForEach(o => RecordDeliveries.Add(o));
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


        #region Propeties
        public ObservableCollection<DcRecordDelivery> RecordDeliveries { get; set; }
        #endregion
        #region Commands
        public RelayCommand AddDelivery { get; set; }
        public RelayCommand UpdateDelivery { get; set; }
        public RelayCommand DeleteDelivery { get; set; }
        public RelayCommand AddDeliveryItem { get; set; }
        public RelayCommand UpdateDeliveryItem { get; set; }
        public RelayCommand DeleteDeliveryItem { get; set; }
        #endregion
    }
}
