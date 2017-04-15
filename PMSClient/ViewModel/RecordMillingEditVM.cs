using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;
using GalaSoft.MvvmLight.CommandWpf;

namespace PMSClient.ViewModel
{
    public class RecordMillingEditVM : BaseViewModelEdit
    {
        public RecordMillingEditVM()
        {
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);

            States = new List<string>();
            States.Clear();
            PMSBasicData.SimpleStates.ToList().ForEach(i=>States.Add(i));


            Select = new RelayCommand(() =>
            {
                PMSHelper.ViewModels.PlanSelect.SetRequestView(PMSViews.RecordMillingEdit);
                NavigationService.GoTo(PMSViews.PlanSelect);
            });
        }

        public void SetNew()
        {
            IsNew = true;
            #region 数据初始化
            var model = new DcRecordMilling();
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.State = PMSCommon.SimpleState.正常.ToString();
            model.VHPPlanLot = UsefulPackage.PMSTranslate.PlanLot();
            model.Composition = "填入成分";
            model.GasProtection = "Ar";
            model.MaterialSource = "Sanjie";
            model.MillingTool = "球磨";
            model.Remark = "";
            model.WeightIn = 0;
            model.WeightOut = 0;
            model.WeightRemain = 0;
            #endregion
            CurrentRecordMilling = model;
        }

        public void SetEdit(DcRecordMilling model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentRecordMilling = model;
            }
        }
        public void SetByDuplicate(DcRecordMilling model)
        {
            if (model != null)
            {
                IsNew = true;
                model.ID = Guid.NewGuid();
                model.CreateTime = DateTime.Now;
                model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                CurrentRecordMilling = model;
            }
        }
        public void SetBySelect(DcPlanWithMisson plan)
        {
            if (plan != null)
            {
                CurrentRecordMilling.Composition = plan.Misson.CompositionStandard;
                CurrentRecordMilling.VHPPlanLot = UsefulPackage.PMSTranslate.PlanLot(plan);
                //RaisePropertyChanged(nameof(CurrentRecordMilling));
            }
        }



        private void ActionSave()
        {
            if (CurrentRecordMilling != null)
            {
                try
                {
                    using (var dc = new RecordMillingServiceClient())
                    {
                        if (IsNew)
                        {
                            dc.AddRecordMilling(CurrentRecordMilling);
                        }
                        else
                        {
                            dc.UpdateRecordMilling(CurrentRecordMilling);
                        }
                        PMSHelper.ViewModels.RecordMilling.RefreshData();
                        GoBack();
                    }
                }
                catch (Exception ex)
                {
                    PMSHelper.CurrentLog.Error(ex);
                }
            }
        }

        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.RecordMilling);
        }

        private DcRecordMilling currentRecordMilling;

        public DcRecordMilling CurrentRecordMilling
        {
            get { return currentRecordMilling; }
            set
            {
                currentRecordMilling = value;
                RaisePropertyChanged(nameof(CurrentRecordMilling));
            }
        }
        public List<string> States { get; set; }
        public RelayCommand Select { get; set; }
    }
}
