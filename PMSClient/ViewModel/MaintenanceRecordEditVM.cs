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
    public class MaintenanceRecordEditVM:BaseViewModelEdit
    {
        public MaintenanceRecordEditVM()
        {
            InitializeProperties();
            InitialCommands();
        }

        public void SetNew()
        {

            var empty = new DcMaintenanceRecord();
            #region 初始化
            empty.ID = Guid.NewGuid();
            empty.VHPMachineCode = PMSCommon.MaintainMachine.VHP_A.ToString();
            empty.PlanType = PMSCommon.MaintainType.Routine例行保养.ToString();
            empty.PlanItem = "";
            empty.PlanInterval = PMSCommon.MaintainInterval.EveryMonth每月.ToString();
            empty.Content = "";
            empty.Persons = "";
            empty.Log = "";
            empty.State = PMSCommon.SimpleState.正常.ToString();
            empty.CreateTime = DateTime.Now;
            empty.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            empty.Remark = "无";
            #endregion

            IsNew = true;
            CurrentRecord = empty;
        }


        public void SetBySelect(DcMaintenancePlan plan)
        {
            if (plan != null)
            {
                CurrentRecord.VHPMachineCode = plan.VHPMachineCode;
                CurrentRecord.PlanType = plan.PlanType;
                CurrentRecord.PlanItem = plan.PlanItem;
                CurrentRecord.PlanInterval = plan.PlanInterval;
                CurrentRecord.Content = plan.Content;
            }
        }

        public void SetEdit(DcMaintenanceRecord model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentRecord = model;
            }
        }
        public void SetDuplicate(DcMaintenanceRecord model)
        {
            if (model != null)
            {
                IsNew = true;
                model.ID = Guid.NewGuid();
                model.CreateTime = DateTime.Now;
                model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                model.State = PMSCommon.SimpleState.正常.ToString();
                CurrentRecord = model;
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
            Select = new RelayCommand(ActionSelect);
        }

        private void GoBack()
        {
            NavigationService.GoTo(PMSViews.MaintainRecord);
        }

        private void ActionSelect()
        {
            //转到材料入库界面,选择材料
            PMSHelper.ViewModels.MaintenancePlanSelect.SetRequestView(PMSViews.MaintainRecordEdit);
            NavigationService.GoTo(PMSViews.MaintainPlanSelect);
        }

        private void ActionSave()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录？"))
            {
                return;
            }
            if (CurrentRecord.State == "作废")
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定作废这条记录？"))
                {
                    return;
                }
            }
            try
            {
                string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                CurrentRecord.Creator = uid;
                var service = new MaintenanceServiceClient();
                if (IsNew)
                {
                    service.AddMainitenanceRecord(CurrentRecord);
                }
                else
                {
                    service.UpdateMainitenanceRecord(CurrentRecord);
                }
                service.Close();
                PMSHelper.ViewModels.MaintenanceRecord.RefreshData();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private DcMaintenanceRecord currentRecord;
        public DcMaintenanceRecord CurrentRecord
        {
            get { return currentRecord; }
            set
            {
                currentRecord = value;
                RaisePropertyChanged(nameof(CurrentRecord));
            }
        }
        public List<string> States { get; set; }
        public List<string> PlanTypes { get; set; }
        public List<string> VHPMachineCodes { get; set; }
        public List<string> PlanIntervals { get; set; }
        public RelayCommand Select { get; set; }
    }
}
