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
            Missons = new ObservableCollection<DcOrder>();
            PlanVHPs = new ObservableCollection<DcPlanVHP>();
        }
        private void InitializeCommands()
        {
            GoToPlan = new RelayCommand(() => NavigationService.GoTo(PMSViews.Plan));


            PageChanged = new RelayCommand(ActionPaging);
            AddNewPlan = new RelayCommand<MainService.DcOrder>(ActionAddNewPlan,CanAddNewPlan);
            EditPlan = new RelayCommand<DcPlanVHP>(ActionEditPlan,CanEditPlan);
            DuplicatePlan = new RelayCommand<MainService.DcPlanVHP>(ActionDuplicatePlan,CanDuplicatePlan);

            SelectionChanged = new RelayCommand<MainService.DcOrder>(ActionSelectionChanged);
            Refresh = new RelayCommand(ActionRefresh);
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

        private void ActionSelectionChanged(DcOrder obj)
        {
            if (obj != null)
            {
                using (var service = new MissonServiceClient())
                {
                    var result = service.GetPlans(obj.ID);
                    PlanVHPs.Clear();
                    result.ToList().ForEach(i => PlanVHPs.Add(i));

                    CurrentSelectItem = obj;
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
                PageSize = 10;
                var service = new MissonServiceClient();
                RecordCount = service.GetMissonsCount();
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
            var service = new MissonServiceClient();
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var orders = service.GetMissons(skip, take);
            Missons.Clear();
            orders.ToList<DcOrder>().ForEach(o => Missons.Add(o));

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

        #endregion

        #region Commands
        public RelayCommand GoToPlan { get; private set; }
        public RelayCommand Add { get; private set; }
        public RelayCommand Refresh { get; set; }
        public RelayCommand<DcOrder> AddNewPlan { get; set; }
        public RelayCommand<DcPlanVHP> EditPlan { get; set; }
        public RelayCommand<DcPlanVHP> DuplicatePlan { get; set; }
        public RelayCommand<DcOrder> SelectionChanged { get; set; }
        #endregion
    }
}
