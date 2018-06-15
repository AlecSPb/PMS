using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using PMSClient.ViewModel.Model;

namespace PMSClient.ViewModel
{
    public class PlanSelectVM : BaseViewModelSelect
    {
        public PlanSelectVM()
        {
            IntitializeProperties();
            IntitializeCommands();
            SetPageParametersWhenConditionChange();
        }

        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
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

        private void IntitializeProperties()
        {
            PlanWithMissonExtras = new ObservableCollection<PlanWithMissonExtra>();
            SearchComposition = SearchVHPDate = "";
        }

        private void IntitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            GiveUp = new RelayCommand(() => NavigationService.GoTo(requestView));
            Select = new RelayCommand<PlanWithMissonExtra>(ActionSelect);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);
            SelectBatch = new RelayCommand(ActionSelectBatch);
        }

        private void ActionSelectBatch()
        {
            int count = PlanWithMissonExtras.Where(i => i.IsSelected == true).Count();
            if (!PMSDialogService.ShowYesNo("请问", $"确定添加选定的{count}个项目到记录？"))
            {
                return;
            }

            switch (requestView)
            {
                case PMSViews.RecordMilling:
                case PMSViews.RecordMillingEdit:
                    BatchSaveMillingRecords();
                    break;
                case PMSViews.RecordDeMold:
                case PMSViews.RecordDeMoldEdit:
                    BatchSaveDeMoldRecords();
                    break;
                case PMSViews.RecordMachine:
                case PMSViews.RecordMachineEdit:
                    BatchSaveMachineRecords();
                    break;
                case PMSViews.RecordTest:
                case PMSViews.RecordTestEdit:
                    BatchSaveTestRecords();
                    break;
                case PMSViews.ProductEdit:
                case PMSViews.DeliveryItemEdit:
                    PMSDialogService.Show("产品库和发货在此处无法使用多选功能");
                    break;
                default:
                    break;
            }
            PMSDialogService.ShowYes("成功", "记录导入完成，请刷新列表");
        }

        #region BatchSaveArea
        private void BatchSaveMillingRecords()
        {
            using (var service = new RecordMillingServiceClient())
            {
                foreach (var item in PlanWithMissonExtras)
                {
                    if (item.IsSelected)
                    {
                        var temp = PMSNewModelCollection.NewRecordMilling();
                        temp.Composition = item.PlanMisson.Misson.CompositionStandard;
                        temp.VHPPlanLot = UsefulPackage.PMSTranslate.PlanLot(item.PlanMisson);
                        temp.PMINumber = item.PlanMisson.Misson.PMINumber;
                        temp.RoomTemperature = item.PlanMisson.Plan.RoomTemperature;
                        temp.RoomHumidity = item.PlanMisson.Plan.RoomHumidity;
                        temp.MaterialType = PMSCommon.CustomData.MaterialTypes[4];//默认其他
                        service.AddRecordMillingByUID(temp, PMSHelper.CurrentSession.CurrentUser.UserName);
                    }
                }
                NavigationService.GoTo(PMSViews.RecordMilling);
                PMSHelper.ViewModels.RecordMilling.RefreshData();
            }
        }
        private void BatchSaveDeMoldRecords()
        {
            using (var service = new RecordDeMoldServiceClient())
            {
                foreach (var item in PlanWithMissonExtras)
                {
                    if (item.IsSelected)
                    {
                        var temp = PMSNewModelCollection.NewRecordDeMold();
                        temp.Composition = item.PlanMisson.Misson.CompositionStandard;
                        temp.VHPPlanLot = UsefulPackage.PMSTranslate.PlanLot(item.PlanMisson);
                        temp.PMINumber = item.PlanMisson.Misson.PMINumber;
                        temp.Dimension = item.PlanMisson.Misson.Dimension;
                        temp.PlanType = item.PlanMisson.Plan.PlanType;
                        temp.CalculationDensity = item.PlanMisson.Plan.CalculationDensity;
                        temp.CalculateDimension = $"{item.PlanMisson.Plan.MoldDiameter.ToString("F2")}mm OD x {item.PlanMisson.Plan.Thickness}mm";
                        service.AddRecordDeMoldByUID(temp, PMSHelper.CurrentSession.CurrentUser.UserName);
                    }
                }
                NavigationService.GoTo(PMSViews.RecordDeMold);
                PMSHelper.ViewModels.RecordDeMold.RefreshData();
            }
        }
        private void BatchSaveMachineRecords()
        {
            using (var service = new RecordMachineServiceClient())
            {
                foreach (var item in PlanWithMissonExtras)
                {
                    if (item.IsSelected)
                    {
                        var temp = PMSNewModelCollection.NewRecordMachine();
                        temp.PMINumber = item.PlanMisson.Misson.PMINumber;
                        temp.Composition = item.PlanMisson.Misson.CompositionStandard;
                        temp.Dimension = item.PlanMisson.Misson.Dimension;
                        temp.BlankDimension = "";
                        temp.VHPPlanLot = UsefulPackage.PMSTranslate.PlanLot(item.PlanMisson);//粗略设定
                        service.AddRecordMachineByUID(temp, PMSHelper.CurrentSession.CurrentUser.UserName);
                    }
                }
                NavigationService.GoTo(PMSViews.RecordMachine);
                PMSHelper.ViewModels.RecordMachine.RefreshData();
            }
        }
        private void BatchSaveTestRecords()
        {
            using (var service = new RecordTestServiceClient())
            {
                foreach (var item in PlanWithMissonExtras)
                {
                    if (item.IsSelected)
                    {
                        var temp = PMSNewModelCollection.NewRecordTest();
                        temp.PMINumber = item.PlanMisson.Misson.PMINumber;
                        temp.Composition = item.PlanMisson.Misson.CompositionStandard;
                        temp.CompositionAbbr = item.PlanMisson.Misson.CompositionAbbr;
                        temp.PO = item.PlanMisson.Misson.PO;
                        temp.ProductID = UsefulPackage.PMSTranslate.PlanLot(item.PlanMisson);
                        temp.Customer = item.PlanMisson.Misson.CustomerName;
                        temp.Dimension = item.PlanMisson.Misson.Dimension;
                        temp.DimensionActual = item.PlanMisson.Misson.Dimension;
                        temp.CompositionAbbr = item.PlanMisson.Misson.CompositionAbbr;
                        temp.OrderDate = item.PlanMisson.Misson.CreateTime;
                        service.AddRecordTestByUID(temp, PMSHelper.CurrentSession.CurrentUser.UserName);
                    }
                }
                NavigationService.GoTo(PMSViews.RecordTest);
                PMSHelper.ViewModels.RecordTest.RefreshData();
            }
        }
        #endregion
        private void ActionSelect(PlanWithMissonExtra plan)
        {
            if (plan != null)
            {
                switch (requestView)
                {
                    case PMSViews.RecordMillingEdit:
                        PMSHelper.ViewModels.RecordMillingEdit.SetBySelect(plan.PlanMisson);
                        break;
                    case PMSViews.RecordVHPQuickEdit:
                        break;
                    case PMSViews.RecordDeMoldEdit:
                        PMSHelper.ViewModels.RecordDeMoldEdit.SetBySelect(plan.PlanMisson);
                        break;
                    case PMSViews.RecordMachineEdit:
                        PMSHelper.ViewModels.RecordMachineEdit.SetBySelect(plan.PlanMisson);
                        break;
                    case PMSViews.RecordTestEdit:
                        PMSHelper.ViewModels.RecordTestEdit.SetBySelect(plan.PlanMisson);
                        break;
                    case PMSViews.ProductEdit:
                        PMSHelper.ViewModels.ProductEdit.SetBySelect(plan.PlanMisson);
                        break;
                    case PMSViews.DeliveryItemEdit:
                        PMSHelper.ViewModels.DeliveryItemEdit.SetBySelect(plan.PlanMisson);
                        break;
                    case PMSViews.FeedBackEdit:
                        PMSHelper.ViewModels.FeedBackEdit.SetBySelect(plan.PlanMisson);
                        break;

                    case PMSViews.PlanEdit:
                        PMSHelper.ViewModels.PlanEdit.SetBySelect(plan.PlanMisson.Plan);
                        break;
                    default:
                        break;
                }

                NavigationService.GoTo(requestView);
            }
        }
        private void ActionAll()
        {
            SearchComposition = SearchVHPDate = "";
            SetPageParametersWhenConditionChange();
        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchComposition) && string.IsNullOrEmpty(SearchVHPDate));
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 20;
            using (var service = new MissonServiceClient())
            {
                RecordCount = service.GetPlanExtraCount(SearchVHPDate, SearchComposition);
            }
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
            //只显示Checked过的计划
            using (var service = new MissonServiceClient())
            {
                var orders = service.GetPlanExtra(skip, take, SearchVHPDate, SearchComposition);
                PlanWithMissonExtras.Clear();
                orders.ToList().ForEach(o => PlanWithMissonExtras.Add(new PlanWithMissonExtra { IsSelected = false, PlanMisson = o }));
            }
        }





        #region Commands
        public RelayCommand<PlanWithMissonExtra> Select { get; set; }

        public RelayCommand SelectBatch { get; set; }
        #endregion

        #region Properties
        public ObservableCollection<PlanWithMissonExtra> PlanWithMissonExtras { get; set; }


        private string searchComposition;
        public string SearchComposition
        {
            get { return searchComposition; }
            set { searchComposition = value; RaisePropertyChanged(nameof(searchComposition)); }
        }
        private string searchVHPDate;
        public string SearchVHPDate
        {
            get { return searchVHPDate; }
            set { searchVHPDate = value; RaisePropertyChanged(nameof(searchVHPDate)); }
        }
        #endregion

    }
}
