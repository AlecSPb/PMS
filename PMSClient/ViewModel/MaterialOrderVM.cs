using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSCommon;
using PMSClient.MainService;
using System.Collections.ObjectModel;
using AutoMapper;
using System.Windows;
using PMSClient;
using Novacode;
using PMSClient.ReportsHelper;

namespace PMSClient.ViewModel
{
    public class MaterialOrderVM : BaseViewModelPage
    {
        public MaterialOrderVM()
        {
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }
        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }
        public void RefreshDataItem()
        {
            ActionSelectionChanged(CurrentSelectItem); 
        }

        private void InitializeProperties()
        {
            SearchOrderPO = "";
            SearchSupplier = "";
            MaterialOrders = new ObservableCollection<DcMaterialOrder>();
            MaterialOrderItems = new ObservableCollection<DcMaterialOrderItem>();
        }
        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);
            Doc = new RelayCommand<DcMaterialOrder>(ActionGenerateDoc);

            Add = new RelayCommand(ActionAdd,CanAdd);
            Edit = new RelayCommand<DcMaterialOrder>(ActionEdit,CanEdit);

            AddItem = new RelayCommand<DcMaterialOrder>(ActionAddItem,CanAddItem);
            EditItem = new RelayCommand<DcMaterialOrderItem>(ActionEditItem,CanEditItem);

            SelectionChanged = new RelayCommand<DcMaterialOrder>(ActionSelectionChanged);

        }

        private bool CanEditItem(DcMaterialOrderItem arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑原料订单");
        }

        private bool CanAddItem(DcMaterialOrder arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑原料订单");

        }

        private bool CanEdit(DcMaterialOrder arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑原料订单");
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑原料订单");
        }

        private void ActionSelectionChanged(DcMaterialOrder model)
        {
            if (model != null)
            {
                using (var service = new MaterialOrderServiceClient())
                {
                    var result = service.GetMaterialOrderItembyMaterialID(model.ID);
                    MaterialOrderItems.Clear();
                    result.ToList().ForEach(i => MaterialOrderItems.Add(i));
                    CurrentSelectItem = model;
                }
            }
        }

        private void ActionEditItem(DcMaterialOrderItem item)
        {
            if (item != null)
            {
                PMSHelper.ViewModels.MaterialOrderItemEdit.SetEdit(item);
                NavigationService.GoTo(PMSViews.MaterialOrderItemEdit);
            }
        }

        private void ActionAddItem(DcMaterialOrder order)
        {
            if (order != null)
            {
                PMSHelper.ViewModels.MaterialOrderItemEdit.SetNew(order);
                NavigationService.GoTo(PMSViews.MaterialOrderItemEdit);
            }
        }

        private void ActionEdit(DcMaterialOrder order)
        {
            if (order != null)
            {
                PMSHelper.ViewModels.MaterialOrderEdit.SetEdit(order);
                NavigationService.GoTo(PMSViews.MaterialOrderEdit);
            }
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.MaterialOrderEdit.SetNew();
            NavigationService.GoTo(PMSViews.MaterialOrderEdit);
        }

        /// <summary>
        /// 生成报告部分
        /// </summary>
        /// <param name="order"></param>
        private void ActionGenerateDoc(DcMaterialOrder order)
        {
            if (MessageBox.Show("你确定要在桌面上创建文档吗?", "请问",
                MessageBoxButton.YesNo, MessageBoxImage.Information)
                == MessageBoxResult.No)
            {
                return;
            }

            if (order != null)
            {
                ReportMaterialOrder report = new ReportMaterialOrder();
                report.Output(order);
            }
        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchOrderPO) && string.IsNullOrEmpty(SearchSupplier));
        }

        private void ActionAll()
        {
            SearchOrderPO = "";
            SearchSupplier = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 10;
            var service = new MaterialOrderServiceClient();
            RecordCount = service.GetMaterialOrderCountBySearch(SearchOrderPO, SearchSupplier);
            ActionPaging();
        }
        /// <summary>
        /// 分页动作的时候读入数据
        /// </summary>
        private void ActionPaging()
        {
            var service = new MaterialOrderServiceClient();
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var orders = service.GetMaterialOrderBySearchInPage(skip, take, SearchOrderPO, SearchSupplier);
            MaterialOrders.Clear();
            orders.ToList<DcMaterialOrder>().ForEach(o => MaterialOrders.Add(o));

            CurrentSelectIndex = 0;
            CurrentSelectItem = MaterialOrders.FirstOrDefault();
            ActionSelectionChanged(CurrentSelectItem);
        }


        #region Proeperties
        private string searchOrderPO;
        public string SearchOrderPO
        {
            get { return searchOrderPO; }
            set
            {
                if (searchOrderPO == value)
                    return;
                searchOrderPO = value;
                RaisePropertyChanged(() => SearchOrderPO);
            }
        }
        private string searchSupplier;
        public string SearchSupplier
        {
            get { return searchSupplier; }
            set
            {
                if (searchSupplier == value)
                    return;
                searchSupplier = value;
                RaisePropertyChanged(() => SearchSupplier);
            }
        }

        public ObservableCollection<DcMaterialOrder> MaterialOrders { get; set; }
        public ObservableCollection<DcMaterialOrderItem> MaterialOrderItems { get; set; }
        private int currrentSelectIndex;
        public int CurrentSelectIndex
        {
            get { return currrentSelectIndex; }
            set { currrentSelectIndex = value; RaisePropertyChanged(nameof(CurrentSelectIndex)); }
        }

        private DcMaterialOrder currentSelectItem;
        public DcMaterialOrder CurrentSelectItem
        {
            get { return currentSelectItem; }
            set { currentSelectItem = value; RaisePropertyChanged(nameof(CurrentSelectItem)); }
        }


        #endregion

        #region Commands
        public RelayCommand Add { get; private set; }
        public RelayCommand<DcMaterialOrder> Edit { get; set; }
        public RelayCommand<DcMaterialOrder> Doc { get; private set; }
        public RelayCommand<DcMaterialOrder> Refresh { get; set; }
        public RelayCommand<DcMaterialOrder> SelectionChanged { get; set; }

        public RelayCommand<DcMaterialOrder> AddItem { get; private set; }
        public RelayCommand<DcMaterialOrderItem> EditItem { get; private set; }
        #endregion
    }
}
