using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSCommon;
using PMSClient.SanjieService;
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
            searchOrderPO = "";
            totalCost = 0;
            MaterialOrders = new ObservableCollection<DcMaterialOrder>();
            MaterialOrderItems = new ObservableCollection<DcMaterialOrderItem>();
        }
        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);
            Doc = new RelayCommand<DcMaterialOrder>(ActionGenerateDoc);
            Finish = new RelayCommand<DcMaterialOrder>(ActionFinish, CanFinish);
            FinishItem = new RelayCommand<DcMaterialOrderItem>(ActionFinishItem, CanFinishItem);
            SelectionChanged = new RelayCommand<DcMaterialOrder>(ActionSelectionChanged);

            GoToMaterialOrderItemList = new RelayCommand(ActionGoToMaterialOrderItemList, CanGoToMaterialOrderItemList);

        }

        private bool CanFinishItem(DcMaterialOrderItem arg)
        {
            if (arg != null)
            {
                return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditMaterialOrderItem) && CheckOrderItemState(arg.State);
            }
            else
            {
                return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditMaterialOrderItem);
            }
        }
        private bool CheckOrderItemState(string state)
        {
            return state == PMSCommon.MaterialOrderItemState.未完成.ToString()
                || state == PMSCommon.MaterialOrderItemState.紧急.ToString();
        }

        private bool CanFinish(DcMaterialOrder arg)
        {
            return true;
        }

        private bool CanGoToMaterialOrderItemList()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.ReadMaterialOrder);
        }

        private void ActionFinishItem(DcMaterialOrderItem model)
        {
            if (model != null)
            {
                try
                {
                    if (!PMSDialogService.ShowYesNo("请问", "确定已经完成这个项目了吗？"))
                    {
                        return;
                    }
                    using (var service = new SanjieServiceClient())
                    {
                        service.FinishMaterialOrderItem(model.ID, PMSHelper.CurrentSession.CurrentUser.UserName);
                    }
                    SetPageParametersWhenConditionChange();
                    NavigationService.Status("保存完毕");
                }
                catch (Exception ex)
                {
                    PMSHelper.CurrentLog.Error(ex);
                }
            }
        }

        private void ActionFinish(DcMaterialOrder model)
        {
            if (model != null)
            {
                try
                {
                    if (!PMSDialogService.ShowYesNo("请问", "确定已经完成这个订单了吗？"))
                    {
                        return;
                    }
                    using (var service = new SanjieServiceClient())
                    {
                        service.FinishMaterialOrder(model.ID, PMSHelper.CurrentSession.CurrentUser.UserName);
                    }
                    SetPageParametersWhenConditionChange();
                    NavigationService.Status("保存完毕");
                }
                catch (Exception ex)
                {
                    PMSHelper.CurrentLog.Error(ex);
                }
            }
        }

        public void SetSearch(string orderPO, string supplier)
        {
            SearchOrderPO = orderPO;
            SetPageParametersWhenConditionChange();
        }



        private void ActionGoToMaterialOrderItemList()
        {
            NavigationService.GoTo(PMSViews.MaterialOrderItemList);
        }

        private void ActionSelectionChanged(DcMaterialOrder model)
        {
            if (model != null)
            {
                using (var service = new SanjieServiceClient())
                {
                    var result = service.GetMaterialOrderItembyMaterialID(model.ID);
                    MaterialOrderItems.Clear();
                    result.ToList().ForEach(i => MaterialOrderItems.Add(i));
                    CurrentSelectItem = model;

                    CalculateTotalCost();
                }
            }
        }

        private void CalculateTotalCost()
        {
            double sumCost = 0;
            foreach (var item in MaterialOrderItems)
            {
                var singleCost = item.UnitPrice * item.Weight;
                sumCost += singleCost;
            }
            TotalCost = sumCost;
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

            try
            {
                if (order != null)
                {
                    NavigationService.Status("开始创建报告……");
                    WordMaterialOrderHorizontal report = new WordMaterialOrderHorizontal();
                    report.SetModel(order);
                    report.Output();
                    NavigationService.Status("原材料订单创建完毕！");
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                NavigationService.Status(ex.Message);
            }
        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchOrderPO));
        }

        private void ActionAll()
        {
            SearchOrderPO = "";
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
            var service = new SanjieServiceClient();
            RecordCount = service.GetMaterialOrderCount(SearchOrderPO);
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
            var service = new SanjieServiceClient();
            var orders = service.GetMaterialOrder(skip, take, SearchOrderPO);
            service.Close();
            MaterialOrders.Clear();
            orders.ToList().ForEach(o => MaterialOrders.Add(o));

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
        private double totalCost;

        public double TotalCost
        {
            get { return totalCost; }
            set { totalCost = value; RaisePropertyChanged(nameof(TotalCost)); }
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
        public RelayCommand<DcMaterialOrderItem> FinishItem { get; private set; }
        public RelayCommand<DcMaterialOrder> Finish { get; private set; }
        public RelayCommand<DcMaterialOrder> Doc { get; private set; }
        public RelayCommand<DcMaterialOrder> Refresh { get; set; }
        public RelayCommand<DcMaterialOrder> SelectionChanged { get; set; }
        public RelayCommand GoToMaterialOrderItemList { get; set; }
        #endregion
    }
}
