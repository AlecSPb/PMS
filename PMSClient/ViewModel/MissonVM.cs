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

namespace PMSClient.ViewModel
{
    public class MissonVM : BaseViewModelPage
    {
        public MissonVM()
        {
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }

        public void RefreshData()
        {
            ActionSelectionChanged(CurrentSelectItem);
        }

        private void InitializeProperties()
        {
            missonTarget = 0;
            searchCompostionStandard = searchPMINumber = "";
            Missons = new ObservableCollection<DcOrder>();
            PlanVHPs = new ObservableCollection<DcPlanVHP>();
        }
        private void InitializeCommands()
        {
            GoToPlan = new RelayCommand(() => NavigationService.GoTo(PMSViews.Plan));
            GoToMaterialNeed = new RelayCommand(() => NavigationService.GoTo(PMSViews.MaterialNeed));




            Search = new RelayCommand(ActionSearch);
            PageChanged = new RelayCommand(ActionPaging);
            AddNewPlan = new RelayCommand<DcOrder>(ActionAddNewPlan, CanAddNewPlan);
            EditPlan = new RelayCommand<DcPlanVHP>(ActionEditPlan, CanEditPlan);
            DuplicatePlan = new RelayCommand<DcPlanVHP>(ActionDuplicatePlan, CanDuplicatePlan);

            SelectionChanged = new RelayCommand<DcOrder>(ActionSelectionChanged);
            Refresh = new RelayCommand(ActionRefresh);
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private bool CanDuplicatePlan(DcPlanVHP arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑计划安排");
        }

        private bool CanEditPlan(DcPlanVHP arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑计划安排");
        }
        /// <summary>
        /// 权限控制=编辑任务
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private bool CanAddNewPlan(DcOrder arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑计划安排");
        }

        private void ActionRefresh()
        {
            SetPageParametersWhenConditionChange();
        }

        private void ActionSelectionChanged(DcOrder model)
        {
            if (model != null)
            {
                using (var service = new PlanVHPServiceClient())
                {
                    var result = service.GetVHPPlansByOrderID(model.ID);
                    PlanVHPs.Clear();
                    result.ToList().ForEach(i => PlanVHPs.Add(i));

                    CurrentSelectItem = model;
                }
            }
        }

        private void ActionDuplicatePlan(DcPlanVHP plan)
        {
            if (plan != null)
            {
                plan.ID = Guid.NewGuid();
                plan.CreateTime = DateTime.Now;
                plan.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;

                PMSHelper.ViewModels.PlanEdit.SetDuplicate(plan);
                NavigationService.GoTo(PMSViews.PlanEdit);
            }
        }

        private void ActionEditPlan(DcPlanVHP plan)
        {
            if (plan != null)
            {
                PMSHelper.ViewModels.PlanEdit.SetEdit(plan);
                NavigationService.GoTo(PMSViews.PlanEdit);
            }
        }

        private void ActionAddNewPlan(DcOrder order)
        {
            if (order != null)
            {
                PMSHelper.ViewModels.PlanEdit.SetNew(order);
                NavigationService.GoTo(PMSViews.PlanEdit);
            }
        }

        private void SetPageParametersWhenConditionChange()
        {
            try
            {
                PageIndex = 1;
                PageSize = 15;
                var service = new MissonServiceClient();
                RecordCount = service.GetMissonsCountBySearch(SearchCompositionStandard, SearchPMINumber);
                //TODO:实现任务计划
                MissonTarget += 1;
                //MissonTarget = service.GetMissonsUnFinished();
                service.Close();
                ActionPaging();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
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
            Missons.Clear();
            orders.ToList().ForEach(o => Missons.Add(o));

            CurrentSelectIndex = 0;
            CurrentSelectItem = orders.FirstOrDefault();
            ActionSelectionChanged(CurrentSelectItem);
        }

        #region Proeperties

        public ObservableCollection<DcOrder> Missons { get; set; }
        public ObservableCollection<DcPlanVHP> PlanVHPs { get; set; }

        private int currentSelectIndex;
        public int CurrentSelectIndex
        {
            get { return currentSelectIndex; }
            set { currentSelectIndex = value; RaisePropertyChanged(nameof(CurrentSelectIndex)); }
        }
        private DcOrder currentSelectItem;
        public DcOrder CurrentSelectItem
        {
            get { return currentSelectItem; }
            set { currentSelectItem = value; RaisePropertyChanged(nameof(CurrentSelectItem)); }
        }


        private string searchCompostionStandard;

        public string SearchCompositionStandard
        {
            get { return searchCompostionStandard; }
            set { searchCompostionStandard = value; RaisePropertyChanged((SearchCompositionStandard)); }
        }
        private string searchPMINumber;

        public string SearchPMINumber
        {
            get { return searchPMINumber; }
            set { searchPMINumber = value; RaisePropertyChanged((SearchPMINumber)); }
        }
        private int missonTarget;

        public int MissonTarget
        {
            get { return missonTarget; }
            set { missonTarget = value; RaisePropertyChanged(nameof(MissonTarget)); }
        }



        #endregion

        #region Commands
        public RelayCommand GoToPlan { get; private set; }
        public RelayCommand GoToMaterialNeed { get; private set; }
        public RelayCommand Add { get; private set; }
        public RelayCommand Refresh { get; set; }
        public RelayCommand<DcOrder> AddNewPlan { get; set; }
        public RelayCommand<DcPlanVHP> EditPlan { get; set; }
        public RelayCommand<DcPlanVHP> DuplicatePlan { get; set; }
        public RelayCommand<DcOrder> SelectionChanged { get; set; }
        #endregion
    }
}
