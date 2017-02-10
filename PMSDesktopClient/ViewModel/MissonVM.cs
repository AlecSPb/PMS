using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSCommon;
using PMSDesktopClient.ServiceReference;
using System.Collections.ObjectModel;

namespace PMSDesktopClient.ViewModel
{
    public class MissonVM : ViewModelBase
    {
        public MissonVM()
        {
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }

        private void InitializeProperties()
        {
            MainMissons = new ObservableCollection<DcOrder>();
        }
        private void InitializeCommands()
        {
            Navigate = new RelayCommand(() => NavigationService.GoTo("NavigationView"));
            GoToPlan = new RelayCommand(() => NavigationService.GoTo("PlanView"));


            PageChanged = new RelayCommand(ActionPaging);
            AddNewPlan = new RelayCommand<ServiceReference.DcOrder>(ActionVHPDetails);
        }

        private void ActionVHPDetails(DcOrder obj)
        {

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
            var service = new MissonServiceClient();
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var orders = service.GetMissonBySearchInPage(skip, take);
            MainMissons.Clear();
            orders.ToList<DcOrder>().ForEach(o => MainMissons.Add(o));
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
        #endregion

        #region Proeperties

        private ObservableCollection<DcOrder> mainMissons;
        public ObservableCollection<DcOrder> MainMissons
        {
            get { return mainMissons; }
            set { mainMissons = value; RaisePropertyChanged(nameof(MainMissons)); }
        }

        #endregion

        #region Commands
        public RelayCommand Navigate { get; private set; }
        public RelayCommand GoToPlan { get; private set; }
        public RelayCommand Add { get; private set; }
        public RelayCommand PageChanged { get; private set; }

        public RelayCommand<DcOrder> AddNewPlan { get; set; }


        #endregion
    }
}
