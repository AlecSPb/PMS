using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSClient.MainService;

namespace PMSClient.ViewModel
{
    public class RecordDeMoldEditVM : BaseViewModelEdit
    {
        public RecordDeMoldEditVM()
        {
            Save = new RelayCommand(ActionSave);
            GiveUp = new RelayCommand(ActionGiveUp);
            Select = new RelayCommand(ActionSelect);
        }

        private void ActionSelect()
        {
            PMSHelper.ViewModels.PlanSelect.SetRequestView(PMSViews.RecordDeMoldEdit);
            NavigationService.GoTo(PMSViews.PlanSelect);
        }

        public void SetKeyProperties(ModelObject model)
        {
            IsNew = model.IsNew;
            CurrentRecordDeMold = model.Model as DcRecordDeMold;
        }

        public void SetNew()
        {
            var model = new DcRecordDeMold();
            #region 初始化
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.State = PMSCommon.SimpleState.UnDeleted.ToString();
            model.VHPPlanLot = DateTime.Now.AddDays(-1).ToString("yyMMdd");
            model.Composition = "成分";
            model.Temperature1 = "0";
            model.Temperature2 = "0";
            model.Weight = 0;
            model.Diameter1 = 0;
            model.Diameter2 = 0;
            model.Thickness1 = 0;
            model.Thickness2 = 0;
            model.Thickness3 = 0;
            model.Thickness4 = 0;
            model.Remark = "无";
            #endregion
            IsNew = true;
            CurrentRecordDeMold = model;
        }
        public void SetEdit(DcRecordDeMold model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentRecordDeMold = model;
            }
        }

        public void SetByDuplicate(DcRecordDeMold model)
        {
            if (model!=null)
            {
                IsNew = true;
                model.ID = Guid.NewGuid();
                model.CreateTime = DateTime.Now;
                model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;

                CurrentRecordDeMold = model;
            }
        }

        public void SetBySelect(DcMissonWithPlan plan)
        {
            if (plan != null)
            {
                CurrentRecordDeMold.Composition = plan.CompositionStandard;
                CurrentRecordDeMold.VHPPlanLot = plan.PlanDate.ToString("yyMMdd") + "-" + plan.VHPDeviceCode;
                RaisePropertyChanged(nameof(CurrentRecordDeMold));
            }
        }
        private void ActionGiveUp()
        {
            GoBack();
        }

        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.RecordDeMold);
        }

        private void ActionSave()
        {
            try
            {
                using (var service = new RecordDeMoldServiceClient())
                {
                    if (IsNew)
                    {
                        service.AddRecordDeMold(CurrentRecordDeMold);
                    }
                    else
                    {
                        service.UpdateRecordDeMold(CurrentRecordDeMold);
                    }
                }
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private DcRecordDeMold currentRecordDeMold;
        public DcRecordDeMold CurrentRecordDeMold
        {
            get { return currentRecordDeMold; }
            set { currentRecordDeMold = value; RaisePropertyChanged(nameof(CurrentRecordDeMold)); }
        }

        public RelayCommand Select { get; set; }

    }
}
