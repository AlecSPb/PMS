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
            SearchCustomer = "";
            SearchCompositoinStandard = "";
            MainMissons = new ObservableCollection<DcOrder>();
            PlanVHPItems = new ObservableCollection<DcPlanVHP>();
        }
        private void InitializeCommands()
        {
            Navigate = new RelayCommand(() => NavigationService.NavigateTo("NavigationView"));
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch,CanSearch);
            All = new RelayCommand(ActionAll);
            GetPlans = new RelayCommand<ServiceReference.DcOrder>(ActionVHPDetails);
        }

        private void ActionVHPDetails(DcOrder obj)
        {
            if (obj!=null)
            {
                var service = new PlanVHPServiceClient();
                var plans = service.GetVHPPlansByOrderID(obj.ID).ToList();
                PlanVHPItems.Clear();
                plans.ForEach(p => PlanVHPItems.Add(p));
            }
        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchCustomer) && string.IsNullOrEmpty(SearchCompositoinStandard));
        }

        private void ActionAll()
        {
            SearchCustomer = "";
            SearchCompositoinStandard = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 20;
            var service = new MissonServiceClient();
            RecordCount = service.GetMissonCountBySearch(SearchCustomer, SearchCompositoinStandard);
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
            var orders = service.GetMissonBySearchInPage(skip, take, SearchCustomer, SearchCompositoinStandard);
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
        private string searchCustomer;
        public string SearchCustomer
        {
            get { return searchCustomer; }
            set
            {
                if (searchCustomer == value)
                    return;
                searchCustomer = value;
                RaisePropertyChanged(() => SearchCustomer);
            }
        }
        private string searchCompositionStandard;
        public string SearchCompositoinStandard
        {
            get { return searchCompositionStandard; }
            set
            {
                if (searchCompositionStandard == value)
                    return;
                searchCompositionStandard = value;
                RaisePropertyChanged(() => SearchCompositoinStandard);
            }
        }



        public ObservableCollection<DcPlanVHP> PlanVHPItems { get; set; }

        private ObservableCollection<DcOrder> mainMissons;
        public ObservableCollection<DcOrder> MainMissons
        {
            get { return mainMissons; }
            set { mainMissons = value; RaisePropertyChanged(nameof(MainMissons)); }
        }

        #endregion

        #region Commands
        public RelayCommand Navigate { get; private set; }
        public RelayCommand Search { get; private set; }
        public RelayCommand All { get; set; }
        public RelayCommand Add { get; private set; }
        public RelayCommand PageChanged { get; private set; }

        public RelayCommand<DcOrder> GetPlans { get; set; }


        #endregion
    }
}
