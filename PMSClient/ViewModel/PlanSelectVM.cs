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
                    default:
                        break;
                }

                NavigationService.GoTo(requestView);
            }
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
            PlanWithMissons = new ObservableCollection<PlanWithMissonExtra>();
            SearchComposition = SearchVHPDate = "";
        }

        private void IntitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            GiveUp = new RelayCommand(() => NavigationService.GoTo(requestView));
            Select = new RelayCommand<PlanWithMissonExtra>(ActionSelect);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);
            BatchSelect = new RelayCommand(ActionBatchSelect);
        }

        private void ActionBatchSelect()
        {
            int count = PlanWithMissons.Where(i => i.IsSelected == true).Count();
            if (!PMSDialogService.ShowYesNo("请问", $"确定添加选定的{count}个项目到记录？"))
            {
                return;
            }

            switch (requestView)
            {
                case PMSViews.RecordMillingEdit:
                    BatchSaveMillingRecords();
                    break;
                case PMSViews.RecordVHPQuickEdit:
                    break;
                case PMSViews.RecordDeMoldEdit:
                    BatchSaveDeMoldRecords();
                    break;
                case PMSViews.RecordMachineEdit:
                    BatchSaveMachineRecords();
                    break;
                case PMSViews.RecordTestEdit:
                    BatchSaveTestRecords();
                    break;
                default:
                    break;
            }

            NavigationService.GoTo(requestView);
        }

        #region BatchSaveArea
        private void BatchSaveMillingRecords()
        {
            using (var service = new RecordMillingServiceClient())
            {
                foreach (var item in PlanWithMissons)
                {
                    if (item.IsSelected)
                    {
                        var temp = PMSNewModelCollection.NewRecordMilling();
                        temp.Composition = item.PlanMisson.Misson.CompositionStandard;
                        temp.VHPPlanLot = UsefulPackage.PMSTranslate.PlanLot(plan);
                        temp.PMINumber = item.PlanMisson.Misson.PMINumber;
                        temp.RoomTemperature = item.PlanMisson.Plan.RoomTemperature;
                        temp.RoomHumidity = item.PlanMisson.Plan.RoomHumidity;
                        service.AddRecordMillingByUID(temp, PMSHelper.CurrentSession.CurrentUser.UserName);
                    }
                }
            }
        }
        private void BatchSaveDeMoldRecords()
        {

        }
        private void BatchSaveMachineRecords()
        {

        }
        private void BatchSaveTestRecords()
        {

        }
        #endregion

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
                PlanWithMissons.Clear();
                orders.ToList().ForEach(o => PlanWithMissons.Add(new PlanWithMissonExtra { IsSelected = false, PlanMisson = o }));
            }
        }





        #region Commands
        public RelayCommand<PlanWithMissonExtra> Select { get; set; }

        public RelayCommand BatchSelect { get; set; }
        #endregion

        #region Properties
        public ObservableCollection<PlanWithMissonExtra> PlanWithMissons { get; set; }


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
