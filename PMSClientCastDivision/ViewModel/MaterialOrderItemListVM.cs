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
            MaterialOrderItemExtras = new ObservableCollection<DcMaterialOrderItemExtra>();
        }
        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(this.ActionAll);
            SelectionChanged = new RelayCommand<DcMaterialOrderItemExtra>(ActionSelectionChanged);
            Location = new RelayCommand<DcMaterialOrderItemExtra>(ActionLocation);
            Finish = new RelayCommand<DcMaterialOrderItemExtra>(ActionFinish);
            Label = new RelayCommand<DcMaterialOrderItemExtra>(ActionLabel);
        }

        private void ActionLabel(DcMaterialOrderItemExtra model)
        {
            if (model != null)
            {

                var sb = new StringBuilder();
                sb.Append("条目编号:");
                sb.AppendLine(model.MaterialOrderItem.OrderItemNumber);
                sb.Append("材料成分:");
                sb.AppendLine(model.MaterialOrderItem.Composition);
                sb.Append("材料净重:");
                sb.AppendLine($"{model.MaterialOrderItem.Weight.ToString("F3")}kg");
                sb.Append("内部编号:");
                sb.AppendLine(model.MaterialOrderItem.PMINumber);
                sb.Append("采购订单:");
                sb.AppendLine(model.MaterialOrder.OrderPO);
                sb.AppendLine(model.MaterialOrder.Supplier);

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

        private void ActionFinish(DcMaterialOrderItemExtra model)
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
                        service.FinishMaterialOrderItem(model.MaterialOrderItem.ID,PMSHelper.CurrentSession.CurrentUser.UserName);
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

        private void ActionLocation(DcMaterialOrderItemExtra model)
        {
            PMSHelper.ViewModels.MaterialOrder.SetSearch(model.MaterialOrder.OrderPO, "");
            NavigationService.GoTo(PMSViews.MaterialOrder);
        }

        private void ActionSelectionChanged(DcMaterialOrderItemExtra model)
        {
            if (model != null)
            {
                //using (var service = new MaterialOrderServiceClient())
                //{
                //    var result = service.GetMaterialOrderItembyMaterialID(model.ID);
                //    MaterialOrderItems.Clear();
                //    result.ToList().ForEach(i => MaterialOrderItems.Add(i));
                //    CurrentSelectItem = model;
                //}
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
            MaterialOrderItemExtras.Clear();
            orders.ToList().ForEach(o => MaterialOrderItemExtras.Add(o));

            CurrentSelectItem = MaterialOrderItemExtras.FirstOrDefault();
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
        public ObservableCollection<DcMaterialOrderItemExtra> MaterialOrderItemExtras { get; set; }

        private DcMaterialOrderItemExtra currentSelectItem;
        public DcMaterialOrderItemExtra CurrentSelectItem
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
        public RelayCommand<DcMaterialOrderItemExtra> SelectionChanged { get; set; }

        public RelayCommand<DcMaterialOrderItemExtra> Location { get; set; }
        public RelayCommand<DcMaterialOrderItemExtra> Finish { get; set; }
        public RelayCommand<DcMaterialOrderItemExtra> Label { get; set; }
        #endregion
    }
}
