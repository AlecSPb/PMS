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
            Messenger.Default.Register<MsgObject>(this, VToken.MissonRefresh, ActionRefreshItems);
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }

        private void ActionRefreshItems(MsgObject obj)
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
            GoToPlan = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VToken.Plan }));


            PageChanged = new RelayCommand(ActionPaging);
            AddNewPlan = new RelayCommand<MainService.DcOrder>(ActionAddNewPlan);
            EditPlan = new RelayCommand<DcPlanVHP>(ActionEditPlan);
            DuplicatePlan = new RelayCommand<MainService.DcPlanVHP>(ActionDuplicatePlan);

            SelectionChanged = new RelayCommand<MainService.DcOrder>(ActionSelectionChanged);
            Refresh = new RelayCommand(ActionRefresh);
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
                    var result = service.GetPlansByOrderID(obj.ID);
                    PlanVHPs.Clear();
                    result.ToList().ForEach(i => PlanVHPs.Add(i));

                    CurrentSelectItem = obj;
                }
            }
        }

        private void ActionDuplicatePlan(DcPlanVHP obj)
        {
            if (obj != null)
            {
                obj.ID = Guid.NewGuid();
                obj.CreateTime = DateTime.Now;
                obj.Creator = PMSHelper.CurrentLogInformation.CurrentUser.UserName;
                
                var msg = new MsgObject();
                msg.MsgToken = VToken.PlanEdit;
                msg.MsgModel = new ModelObject() { IsNew = true, Model = obj };
                NavigationService.GoTo(msg);
            }
        }

        private void ActionEditPlan(DcPlanVHP obj)
        {
            if (obj != null)
            {
                var msg = new MsgObject();
                msg.MsgToken = VToken.PlanEdit;
                msg.MsgModel = new ModelObject() { IsNew = false, Model = obj };
                NavigationService.GoTo(msg);
            }
        }

        private void ActionAddNewPlan(DcOrder order)
        {
            if (order != null)
            {

                var plan = EmptyModel.GetPlanVHP(order);
                var msg = new MsgObject();
                msg.MsgToken = VToken.PlanEdit;
                msg.MsgModel = new ModelObject() { IsNew = true, Model = plan };
                NavigationService.GoTo(msg);
            }
        }

        private void SetPageParametersWhenConditionChange()
        {
            try
            {
                PageIndex = 1;
                PageSize = 10;
                var service = new MissonServiceClient();
                RecordCount = service.GetMissonCountBySearch();
                ActionPaging();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex.Message);
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
            var orders = service.GetMissonBySearchInPage(skip, take);
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
