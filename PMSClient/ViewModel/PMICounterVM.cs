using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.ExtraService;
using PMSClient.MainService;

namespace PMSClient.ViewModel
{
    public class PMICounterVM : BaseViewModelPage
    {
        public PMICounterVM()
        {
            searchItemName = searchItemGroup = "";
            PMICounters = new ObservableCollection<DcPMICounter>();

            InitializeCommands();

            SetPageParametersWhenConditionChange();
        }

        private void InitializeCommands()
        {
            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<DcPMICounter>(ActionEdit, CanEdit);
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
            PageChanged = new RelayCommand(ActionPaging);
            Duplicate = new RelayCommand<DcPMICounter>(ActionDuplicate, CanDuplicate);
            QuickChange = new RelayCommand<DcPMICounter>(ActionQuickChange, CanQuickChange);
            ShowItemHistory = new RelayCommand<DcPMICounter>(ActionShowItemHistory);
            ShowItemRemark = new RelayCommand<DcPMICounter>(ActionShowItemRemark);
        }

        private void ActionShowItemRemark(DcPMICounter obj)
        {
            if (obj != null)
            {
                var dialog = new WPFControls.NormalizedDataViewer();
                dialog.SetMainStrings(obj.Remark);
                dialog.ShowDialog();
            }
        }

        private void ActionShowItemHistory(DcPMICounter obj)
        {
            if (obj != null)
            {
                var dialog = new WPFControls.NormalizedDataViewer();
                dialog.SetMainStrings(obj.ItemHistory);
                dialog.ShowDialog();
            }
        }

        private bool CanQuickChange(DcPMICounter arg)
        {
            return CanAdd();
        }

        private void ActionQuickChange(DcPMICounter obj)
        {
            if (obj == null) return;
            var dialog = new ToolDialog.PMICounterQuickEditDialog();
            dialog.Remark = obj.Remark;
            dialog.ShowDialog();

            obj.Remark = dialog.Remark;
            if (dialog.EditType == ToolDialog.PMICounterEditType.IsCancel)
            { return; }
            else if (dialog.EditType == ToolDialog.PMICounterEditType.IsAdd)
            {
                obj.ItemCount += dialog.Counter;
                obj.ItemHistory = AddItemHistory(obj.ItemHistory, "+", dialog.Counter);
            }
            else
            {
                if (obj.ItemGroup == "背板")
                {
                    //TODO:添加自动添加背板库存记录功能
                    if (PMSDialogService.ShowYesNo("请问", "需要添加对应数量的此背板到背板库存里面？"))
                    {
                        var model = new DcPlate();
                        #region 初始化
                        model.ID = Guid.NewGuid();
                        model.CreateTime = DateTime.Now;
                        model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                        model.State = PMSCommon.InventoryState.库存.ToString();
                        model.PlateLot = UsefulPackage.PMSTranslate.PlateLot();
                        model.PrintNumber = "无";
                        model.PlateMaterial = PMSCommon.PlateMaterial.Cu.ToString();
                        model.Supplier = PMSCommon.CustomData.PlateSupplier[3];
                        model.UseCount = "0";
                        model.Hardness = "未知";
                        model.LastWeldMaterial = PMSCommon.CustomData.PlateLastWeldMaterial[0].ToString();
                        model.Weight = "未知";
                        model.Appearance = "正常";
                        model.Defects = "无";

                        model.Remark = obj.ItemDetails;
                        model.Dimension = obj.ItemSpecification;

                        #endregion
                        int number = dialog.Counter;
                        string prefix = model.PlateLot.Substring(0, 10);
                        string uid = PMSHelper.CurrentSession.CurrentUser.UserName;

                        using (var service = new PlateServiceClient())
                        {
                            for (int i = 0; i < number; i++)
                            {
                                model.ID = Guid.NewGuid();
                                model.CreateTime = DateTime.Now;
                                model.PlateLot = prefix + (i + 1).ToString();
                                service.AddPlateByUID(model, uid);
                            }
                        }




                    }
                }


                obj.ItemCount -= dialog.Counter;
                obj.ItemHistory = AddItemHistory(obj.ItemHistory, "-", dialog.Counter);

            }
            //排序计数+1
            obj.RowOrder += 1;

            using (var service = new PMICounterServiceClient())
            {
                service.UpdatePMICounter(obj);
                SetPageParametersWhenConditionChange();

            }
        }

        private string AddItemHistory(string history, string sign, int count)
        {
            return $"{DateTime.Today.ToString("yyMMdd")}{sign}{count};{history}";
        }

        private void ActionDuplicate(DcPMICounter obj)
        {
            if (PMSDialogService.ShowYesNo("请问", "确定复用这条记录？"))
            {
                if (obj != null)
                {
                    PMSHelper.ViewModels.PMICounterEdit.SetDuplicate(obj);
                    NavigationService.GoTo(PMSViews.PMICounterEdit);
                }
            }

        }

        private bool CanDuplicate(DcPMICounter arg)
        {
            return PMSHelper.CurrentSession.IsInGroup(groupnames);
        }

        private void ActionAll()
        {
            SearchItemName = SearchItemGroup = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private string[] groupnames = { "管理员", "测试组", "质量组", "统筹组" };
        private bool CanEdit(DcPMICounter arg)
        {
            return PMSHelper.CurrentSession.IsInGroup(groupnames);
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsInGroup(groupnames);
        }

        private void ActionEdit(DcPMICounter model)
        {
            PMSHelper.ViewModels.PMICounterEdit.SetEdit(model);
            NavigationService.GoTo(PMSViews.PMICounterEdit);
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.PMICounterEdit.SetNew();
            NavigationService.GoTo(PMSViews.PMICounterEdit);
        }

        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }


        private string searchItemName;
        public string SearchItemName
        {
            get { return searchItemName; }
            set { searchItemName = value; RaisePropertyChanged(nameof(SearchItemName)); }
        }

        private string searchItemGroup;
        public string SearchItemGroup
        {
            get { return searchItemGroup; }
            set { searchItemGroup = value; RaisePropertyChanged(nameof(SearchItemGroup)); }
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 30;
            using (var service = new PMICounterServiceClient())
            {
                RecordCount = service.GetPMICounterCount(SearchItemGroup, SearchItemName);
            }
            ActionPaging();
        }
        private void ActionPaging()
        {
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            using (var service = new PMICounterServiceClient())
            {
                var orders = service.GetPMICounter(SearchItemGroup, SearchItemName, skip, take);
                PMICounters.Clear();
                orders.ToList().ForEach(o => PMICounters.Add(o));
            }
        }
        #region Commands
        public ObservableCollection<DcPMICounter> PMICounters { get; set; }

        public RelayCommand Add { get; set; }
        public RelayCommand<DcPMICounter> QuickChange { get; set; }
        public RelayCommand<DcPMICounter> Edit { get; set; }
        public RelayCommand<DcPMICounter> Duplicate { get; set; }
        public RelayCommand<DcPMICounter> ShowItemHistory { get; set; }
        public RelayCommand<DcPMICounter> ShowItemRemark { get; set; }
        #endregion

    }
}
