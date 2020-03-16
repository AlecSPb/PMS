using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using System.Collections.ObjectModel;

namespace PMSClient.ViewModel
{
    public class MissonSelectVM : BaseViewModelSelect
    {
        //要转到的页面

        public MissonSelectVM()
        {
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
            GiveUp = new RelayCommand(() => NavigationService.GoTo(requestView));
            Select = new RelayCommand<DcOrder>(ActionSelect);

        }
        private PMSViews requestView;

        public void RefreshData()
        {
            searchPMINumber = "";
            searchCompositionStandard = "";
            SetPageParametersWhenConditionChange();
        }
        /// <summary>
        /// 设置请求视图的token，返回或者选择后返回用
        /// </summary>
        /// <param name="request">请求视图的token</param>
        public void SetRequestView(PMSViews request)
        {
            requestView = request;
        }

        private void ActionSelect(DcOrder order)
        {
            if (order != null)
            {
                switch (requestView)
                {
                    case PMSViews.MaterialNeedEdit:
                        PMSHelper.ViewModels.MaterialNeedEdit.SetBySelect(order);
                        break;
                    case PMSViews.RecordTestEdit:
                        PMSHelper.ViewModels.RecordTestEdit.SetBySelectMisson(order);
                        break;
                    case PMSViews.SampleEdit:
                        PMSHelper.ViewModels.SampleEdit.SetBySelectMisson(order);
                        break;
                    default:
                        break;
                }
                NavigationService.GoTo(requestView);
            }
        }
        public RelayCommand<DcOrder> Select { get; set; }

        private void ActionRefresh(Object obj)
        {
            SetPageParametersWhenConditionChange();
        }

        private void InitializeProperties()
        {
            searchPMINumber = "";
            searchCompositionStandard = "";
            MainOrders = new ObservableCollection<DcOrder>();
        }
        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);
        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchPMINumber) && string.IsNullOrEmpty(SearchCompositionStandard));
        }

        private void ActionAll()
        {
            SearchPMINumber = "";
            SearchCompositionStandard = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 30;
            var service = new MissonServiceClient();
            RecordCount = service.GetMissonsCountBySearch(SearchCompositionStandard, SearchPMINumber);
            service.Close();
            ActionPaging();
        }
        /// <summary>
        /// 分页动作的时候读入数据
        /// </summary>
        private void ActionPaging()
        {

            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var service = new MissonServiceClient();
            var orders = service.GetMissonsBySearch(skip, take, SearchCompositionStandard, SearchPMINumber);
            service.Close();
            MainOrders.Clear();
            orders.ToList().ForEach(o => MainOrders.Add(o));
        }

        #region Proeperties
        private string searchPMINumber;
        public string SearchPMINumber
        {
            get { return searchPMINumber; }
            set
            {
                if (searchPMINumber == value)
                    return;
                searchPMINumber = value;
                RaisePropertyChanged(() => SearchPMINumber);
            }
        }
        private string searchCompositionStandard;
        public string SearchCompositionStandard
        {
            get { return searchCompositionStandard; }
            set
            {
                if (searchCompositionStandard == value)
                    return;
                searchCompositionStandard = value;
                RaisePropertyChanged(() => SearchCompositionStandard);
            }
        }

        private ObservableCollection<DcOrder> mainOrders;
        public ObservableCollection<DcOrder> MainOrders
        {
            get { return mainOrders; }
            set { mainOrders = value; RaisePropertyChanged(nameof(MainOrders)); }
        }

        #endregion

    }
}
