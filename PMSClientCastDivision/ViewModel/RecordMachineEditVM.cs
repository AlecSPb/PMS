using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;
using GalaSoft.MvvmLight.CommandWpf;
using PMSCommon;

namespace PMSClient.ViewModel
{
    public class RecordMachineEditVM : BaseViewModelEdit
    {
        public RecordMachineEditVM()
        {
            MachineDefects = new List<string>();
            PMSBasicDataService.SetListDS<TestDefectsTypes>(MachineDefects);

            States = new List<string>();
            PMSBasicDataService.SetListDS<SimpleState>(States);

            GiveUp = new RelayCommand(ActionGiveUp);
            Save = new RelayCommand(ActionSave);
            SelectPlan = new RelayCommand(ActionSelectPlan);
            SelectDeMold = new RelayCommand(ActionSelectDeMold);
        }

        private void ActionSelectPlan()
        {
            PMSHelper.ViewModels.PlanSelect.SetRequestView(PMSViews.RecordMachineEdit);
            NavigationService.GoTo(PMSViews.PlanSelect);
        }

        private void ActionSelectDeMold()
        {
            PMSHelper.ViewModels.RecordDeMoldSelect.SetRequestView(PMSViews.RecordMachineEdit);
            NavigationService.GoTo(PMSViews.RecordDeMoldSelect);
        }

        public void SetNew()
        {
            IsNew = true;
            #region 初始化
            var model = new DcRecordMachine();
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.PMINumber = "";
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.State = PMSCommon.SimpleState.正常.ToString();
            model.VHPPlanLot = UsefulPackage.PMSTranslate.PlanLot();
            model.Composition = "成分";
            model.Diameter1 = 0;
            model.Diameter2 = 0;
            model.Dimension = "230mm OD x 4mm";
            model.BlankDimension = "无";
            model.Thickness1 = 0;
            model.Thickness2 = 0;
            model.Thickness3 = 0;
            model.Thickness4 = 0;
            model.ExtraRequirement = "正常要求";
            model.Defects = PMSCommon.TestDefectsTypes.无缺陷.ToString();
            #endregion
            CurrentRecordMachine = model;
        }

        public void SetEdit(DcRecordMachine model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentRecordMachine = model;
            }
        }
        public void SetByDuplicate(DcRecordMachine model)
        {
            if (model != null)
            {
                IsNew = true;
                model.ID = Guid.NewGuid();
                model.CreateTime = DateTime.Now;
                model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                CurrentRecordMachine = model;
            }
        }

        public void SetBySelect(DcPlanWithMisson plan)
        {
            if (plan != null)
            {
                CurrentRecordMachine.PMINumber = plan.Misson.PMINumber;
                CurrentRecordMachine.Composition = plan.Misson.CompositionStandard;
                CurrentRecordMachine.Dimension = plan.Misson.Dimension;
                CurrentRecordMachine.BlankDimension = "";
                CurrentRecordMachine.VHPPlanLot = UsefulPackage.PMSTranslate.PlanLot(plan);//粗略设定
                //RaisePropertyChanged(nameof(CurrentRecordMachine));
            }
        }

        public void SetBySelect(DcRecordDeMold model)
        {
            if (model != null)
            {
                CurrentRecordMachine.VHPPlanLot = model.VHPPlanLot;
                CurrentRecordMachine.BlankDimension = UsefulPackage.PMSTranslate.Dimension(model);
                //RaisePropertyChanged(nameof(CurrentRecordMachine));
            }
        }

        private void ActionSave()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录？"))
            {
                return;
            }
            if (CurrentRecordMachine.State == "作废")
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定作废这条记录？"))
                {
                    return;
                }
            }
            try
            {
                string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                using (var service = new RecordMachineServiceClient())
                {
                    if (IsNew)
                    {
                        service.AddRecordMachineByUID(CurrentRecordMachine, uid);
                    }
                    else
                    {
                        service.UpdateRecordMachineByUID(CurrentRecordMachine, uid);
                    }
                }
                PMSHelper.ViewModels.RecordMachine.RefreshData();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private void ActionGiveUp()
        {
            GoBack();
        }

        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.RecordMachine);
        }

        private DcRecordMachine currentRecordMachine;

        public DcRecordMachine CurrentRecordMachine
        {
            get { return currentRecordMachine; }
            set { currentRecordMachine = value; RaisePropertyChanged(nameof(CurrentRecordMachine)); }
        }

        public RelayCommand SelectPlan{ get; set; }
        public RelayCommand SelectDeMold { get; set; }

        public List<string> States { get; set; }
        public List<string> MachineDefects { get; set; }
    }
}
