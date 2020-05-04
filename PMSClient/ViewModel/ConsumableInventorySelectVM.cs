using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using System.Collections.ObjectModel;
using PMSClient.Tool;
using PMSClient.ConsumableService;

namespace PMSClient.ViewModel
{
    public class ConsumableInventorySelectVM : BaseViewModelSelect
    {
        public ConsumableInventorySelectVM()
        {
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }



        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }

        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            Select = new RelayCommand<DcConsumableInventory>(ActionSelect);
            All = new RelayCommand(ActionAll);
            GiveUp = new RelayCommand(ActionGiveUp);
        }

        private void ActionGiveUp()
        {
            NavigationService.GoTo(requestView);
        }

        private PMSViews requestView;
        /// <summary>
        /// 设置请求视图的token，返回或者选择后返回用
        /// </summary>
        /// <param name="request">请求视图的token</param>
        public void SetRequestView(PMSViews request)
        {
            requestView = request;
        }

        private void ActionSelect(DcConsumableInventory model)
        {
            switch (requestView)
            {
                case PMSViews.ConsumablePurchaseEdit:
                    PMSHelper.ViewModels.ConsumablePurchaseEdit.SetBySelect(model);
                    break;
                default:
                    break;
            }
            NavigationService.GoTo(requestView);
        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchItemName));
        }

        private void ActionAll()
        {
            searchItemName = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void InitializeProperties()
        {
            ConsumableInventories
                = new ObservableCollection<DcConsumableInventory>();
            searchItemName = "";

        }
        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 30;
            using (var service = new ConsumableServiceClient())
            {
                RecordCount = service.GetConsumableInventoryCount(SearchItemName);
            }
            ActionPaging();
        }
        private void ActionPaging()
        {
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            using (var service = new ConsumableServiceClient())
            {
                var orders = service.GetConsumableInventory(skip, take, SearchItemName);
                ConsumableInventories.Clear();
                orders.ToList().ForEach(o => ConsumableInventories.Add(o));
            }
        }
        #region Commands
        private string searchItemName;
        public string SearchItemName
        {
            get { return searchItemName; }
            set
            {
                if (searchItemName == value)
                    return;
                searchItemName = value;
                RaisePropertyChanged(() => SearchItemName);
            }
        }

        public ObservableCollection<DcConsumableInventory> ConsumableInventories { get; set; }
        #endregion
        public RelayCommand<DcConsumableInventory> Select { get; set; }
    }
}
