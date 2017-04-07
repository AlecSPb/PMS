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
            GiveUp = new RelayCommand(() => NavigationService.GoTo(PMSViews.RecordMilling));
            Save = new RelayCommand(ActionSave);

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
            model.State = PMSCommon.SimpleState.UnDeleted.ToString();
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

        public void SetBySelect(DcMissonWithPlan plan)
        {
            if (plan!=null)
            {
                CurrentRecordMilling.Composition = plan.CompositionStandard;
                RaisePropertyChanged(nameof(CurrentRecordMilling));
            }
        }



        private void ActionSave()
        {
            if (CurrentRecordMilling != null)
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

                    //TODO:以后将这里的重复代替掉
                    NavigationService.GoTo(PMSViews.RecordMilling);
                }
            }
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



        public RelayCommand Select { get; set; }
    }
}
