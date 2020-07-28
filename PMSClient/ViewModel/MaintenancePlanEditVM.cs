using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.Maintainance;

namespace PMSClient.ViewModel
{
    public class MaintenancePlanEditVM:BaseViewModelEdit
    {
        public MaintenancePlanEditVM()
        {
            InitializeProperties();
            InitialCommands();
        }

        public void SetNew()
        {

            var empty = new DcMaintenancePlan();
            #region 初始化
            empty.ID = Guid.NewGuid();
            empty.VHPMachineCode = PMSCommon.MaintainMachine.VHP_A.ToString();
            empty.PlanType = PMSCommon.MaintainType.Routine例行保养.ToString();
            empty.PlanItem = "";
            empty.PlanInterval = PMSCommon.MaintainInterval.EveryMonth每月.ToString();
            empty.Content = "";
            empty.Standard = "";
            empty.CommonFailure = "";
            empty.ProcessMethod = "";
            empty.State = PMSCommon.SimpleState.正常.ToString();
            empty.CreateTime = DateTime.Now;
            empty.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            empty.Remark = "无";
            #endregion

            IsNew = true;
            CurrentPlan = empty;
        }

        public void SetEdit(DcMaintenancePlan model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentPlan = model;
            }
        }
        public void SetDuplicate(DcMaintenancePlan model)
        {
            if (model != null)
            {
                IsNew = true;
                model.ID = Guid.NewGuid();
                model.CreateTime = DateTime.Now;
                model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                model.State = PMSCommon.SimpleState.正常.ToString();
                CurrentPlan = model;
            }
        }

        private void InitializeProperties()
        {
            States = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.SimpleState>(States);

            PlanTypes = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.MaintainType>(PlanTypes);

            VHPMachineCodes = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.MaintainMachine>(VHPMachineCodes);
            PlanIntervals = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.MaintainInterval>(PlanIntervals);
        }

        private void InitialCommands()
        {
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
        }

        private void GoBack()
        {
            
            NavigationService.GoTo(PMSViews.MaintainPlan);
        }


        private void ActionSave()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录？"))
            {
                return;
            }
            if (CurrentPlan.State == "作废")
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定作废这条记录？"))
                {
                    return;
                }
            }
            try
            {
                string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                CurrentPlan.Creator = uid;
                var service = new MaintenanceServiceClient();
                if (IsNew)
                {
                    service.AddMainitenancePlan(CurrentPlan);
                }
                else
                {
                    service.UpdateMainitenancePlan(CurrentPlan);
                }
                service.Close();
                PMSHelper.ViewModels.MaintenancePlan.RefreshData();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private DcMaintenancePlan currentPlan;
        public DcMaintenancePlan CurrentPlan
        {
            get { return currentPlan; }
            set
            {
                currentPlan = value;
                RaisePropertyChanged(nameof(CurrentPlan));
            }
        }
        public List<string> States { get; set; }
        public List<string> PlanTypes { get; set; }
        public List<string> VHPMachineCodes { get; set; }
        public List<string> PlanIntervals { get; set; }

    }
}
