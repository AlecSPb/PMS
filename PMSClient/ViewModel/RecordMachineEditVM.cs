using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;
using GalaSoft.MvvmLight.CommandWpf;

namespace PMSClient.ViewModel
{
    public class RecordMachineEditVM : BaseViewModelEdit
    {
        public RecordMachineEditVM()
        {
            GiveUp = new RelayCommand(ActionGiveUp);
            Save = new RelayCommand(ActionSave);
            Select = new RelayCommand(ActionSelect);
        }

        private void ActionSelect()
        {
            PMSHelper.ViewModels.PlanSelect.SetRequestView(PMSViews.RecordMachineEdit);
            NavigationService.GoTo(PMSViews.PlanSelect);
        }

        public void SetNew()
        {
            IsNew = true;
            #region 初始化
            var model = new DcRecordMachine();
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.State = PMSCommon.CommonState.已核验.ToString();
            model.VHPPlanLot = UsefulPackage.PMSTranslate.PlanLot();
            model.Composition = "成分";
            model.Diameter1 = 0;
            model.Diameter2 = 0;
            model.Dimension = "230mm OD x 4mm";
            model.Thickness1 = 0;
            model.Thickness2 = 0;
            model.Thickness3 = 0;
            model.Thickness4 = 0;
            model.ExtraRequirement = "无缺口无划痕";
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
                CurrentRecordMachine.Composition = plan.Misson.CompositionStandard;
                CurrentRecordMachine.Dimension = plan.Misson.Dimension;
                CurrentRecordMachine.VHPPlanLot= UsefulPackage.PMSTranslate.PlanLot(plan);
                //RaisePropertyChanged(nameof(CurrentRecordMachine));
            }
        }



        private void ActionSave()
        {
            try
            {
                using (var service = new RecordMachineServiceClient())
                {
                    if (IsNew)
                    {
                        service.AddRecordMachine(CurrentRecordMachine);
                    }
                    else
                    {
                        service.UpdateRecordMachine(CurrentRecordMachine);
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

        public RelayCommand Select { get; set; }
    }
}
