using System;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.SanjieService;
using System.Collections.ObjectModel;
using PMSClient.ViewModel.Model;
using System.Collections.Generic;
using PMSClient.ReportsHelper;

namespace PMSClient.ViewModel
{
    public class MaterialOrderItemListVM : BaseViewModelPage
    {
        public MaterialOrderItemListVM()
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
            searchPMINumber = "";
            searchComposition = "";
            searchOrderItemNumber = "";
            MaterialOrderItemExtraSelects = new ObservableCollection<MaterialOrderItemExtraSelect>();
        }
        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);
            SelectionChanged = new RelayCommand<MaterialOrderItemExtraSelect>(ActionSelectionChanged);
            Location = new RelayCommand<MaterialOrderItemExtraSelect>(ActionLocation);
            Finish = new RelayCommand<MaterialOrderItemExtraSelect>(ActionFinish, CanFinish);
            Label = new RelayCommand<MaterialOrderItemExtraSelect>(ActionLabel);
            Doc = new RelayCommand(ActionDoc);
        }

        private bool CanFinish(MaterialOrderItemExtraSelect arg)
        {
            if (arg != null)
            {
                return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditMaterialOrder) 
                    && CheckOrderItemState(arg.Item.MaterialOrderItem.State);
            }
            else
            {
                return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditMaterialOrder);
            }
        }

        private bool CheckOrderItemState(string state)
        {
            return state == PMSCommon.MaterialOrderItemState.未完成.ToString();
        }

        private void ActionDoc()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定以选中的项目创建发货单吗？"))
            {
                return;
            }
            try
            {
                if (MaterialOrderItemExtraSelects.Where(i => i.IsSelected).Count() == 0)
                {
                    PMSDialogService.ShowYes("选中数目为0，请至少选择一个");
                    return;
                }

                List<DcMaterialOrderItemExtra> selectedItems = new List<DcMaterialOrderItemExtra>();
                selectedItems.Clear();
                MaterialOrderItemExtraSelects.Where(i => i.IsSelected).ToList().ForEach(i =>
                {
                    selectedItems.Add(i.Item);
                });

                var report = new WordMaterialDeliverySheet();
                report.SetModel(selectedItems);
                report.Output();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }

        }

        private void ActionLabel(MaterialOrderItemExtraSelect model)
        {
            if (model != null)
            {

                var sb = new StringBuilder();
                sb.Append("编号:");
                sb.AppendLine(model.Item.MaterialOrderItem.PMINumber);
                sb.Append("订单:");
                sb.AppendLine(model.Item.MaterialOrder.OrderPO);
                sb.Append("条目:");
                sb.AppendLine(model.Item.MaterialOrderItem.OrderItemNumber);
                sb.Append("成分:");
                sb.AppendLine(model.Item.MaterialOrderItem.Composition);
                sb.Append("净重:");
                sb.AppendLine($"{model.Item.MaterialOrderItem.Weight.ToString("F3")}kg");
                sb.AppendLine("批号:");

                var mainContent = sb.ToString();

                var pageTitle = "原料标签打印输出";
                var tips = @"点击打开模板按钮，粘贴不同内容到模板合适位置，可以再自行修改，然后打印标签";
                var template = "CastLabel";
                var helpimage = "productionlabel.png";
                PMSHelper.ToolViewModels.LabelOutPut.SetAllParameters(PMSViews.MaterialOrderItemList, pageTitle,
                    tips, template, mainContent, helpimage);
                NavigationService.GoTo(PMSViews.LabelOutPut);
            }
        }

        private void ActionFinish(MaterialOrderItemExtraSelect model)
        {
            if (model != null)
            {
                try
                {
                    if (!PMSDialogService.ShowYesNo("请问", $"确定已经完成这个项目{model.Item.MaterialOrderItem.Composition}了吗？"))
                    {
                        return;
                    }
                    using (var service = new SanjieServiceClient())
                    {
                        service.FinishMaterialOrderItem(model.Item.MaterialOrderItem.ID, PMSHelper.CurrentSession.CurrentUser.UserName);
                    }
                    SetPageParametersWhenConditionChange();
                    PMSDialogService.ShowYes("项目已完成，并暂入库，万一有操作失误，联系先锋材料进行修正");
                    NavigationService.Status("保存完毕");
                }
                catch (Exception ex)
                {
                    PMSHelper.CurrentLog.Error(ex);
                }
            }
        }

        private void ActionLocation(MaterialOrderItemExtraSelect model)
        {
            PMSHelper.ViewModels.MaterialOrder.SetSearch(model.Item.MaterialOrder.OrderPO, "");
            NavigationService.GoTo(PMSViews.MaterialOrder);
        }

        private void ActionSelectionChanged(MaterialOrderItemExtraSelect model)
        {
            if (model != null)
            {
                CurrentSelectItem = model;
            }
        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchPMINumber) && string.IsNullOrEmpty(SearchOrderItemNumber) && string.IsNullOrEmpty(SearchComposition));
        }

        private void ActionAll()
        {
            searchPMINumber = "";
            searchOrderItemNumber = "";
            searchComposition = "";
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
            var service = new SanjieServiceClient();
            RecordCount = service.GetMaterialOrderItemExtrasCount(SearchComposition, SearchPMINumber, SearchOrderItemNumber);
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
            var orders = service.GetMaterialOrderItemExtras(skip, take, SearchComposition, SearchPMINumber, SearchOrderItemNumber);
            service.Close();
            MaterialOrderItemExtraSelects.Clear();
            orders.ToList().ForEach(o => MaterialOrderItemExtraSelects.Add(
                new MaterialOrderItemExtraSelect { IsSelected = false, Item = o }));

            CurrentSelectItem = MaterialOrderItemExtraSelects.FirstOrDefault();
            ActionSelectionChanged(CurrentSelectItem);
        }


        #region Proeperties
        private string searchOrderItemNumber;

        public string SearchOrderItemNumber
        {
            get { return searchOrderItemNumber; }
            set
            {
                searchOrderItemNumber = value;
                RaisePropertyChanged(nameof(SearchOrderItemNumber));
            }
        }

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

        private string searchComposition;
        public string SearchComposition
        {
            get { return searchComposition; }
            set
            {
                if (searchComposition == value)
                    return;
                searchComposition = value;
                RaisePropertyChanged(() => SearchComposition);
            }
        }
        public ObservableCollection<MaterialOrderItemExtraSelect> MaterialOrderItemExtraSelects { get; set; }

        private MaterialOrderItemExtraSelect currentSelectItem;
        public MaterialOrderItemExtraSelect CurrentSelectItem
        {
            get { return currentSelectItem; }
            set
            {
                currentSelectItem = value;
                RaisePropertyChanged(nameof(CurrentSelectItem));
            }
        }


        #endregion

        #region Commands
        public RelayCommand<MaterialOrderItemExtraSelect> SelectionChanged { get; set; }
        public RelayCommand<MaterialOrderItemExtraSelect> Location { get; set; }
        public RelayCommand<MaterialOrderItemExtraSelect> Finish { get; set; }
        public RelayCommand<MaterialOrderItemExtraSelect> Label { get; set; }

        public RelayCommand Doc { get; set; }
        #endregion
    }
}
