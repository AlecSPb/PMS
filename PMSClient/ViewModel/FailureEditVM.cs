using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.FailureService;
using PMSClient.MainService;

namespace PMSClient.ViewModel
{
    public class FailureEditVM : BaseViewModelEdit
    {
        public FailureEditVM()
        {
            States = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.SimpleState>(States);

            FailureStages = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.FailureStage>(FailureStages);

            InitializeCommands();
        }


        private void InitializeCommands()
        {
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
            Select = new RelayCommand(ActionSelect);
        }

        private void ActionSelect()
        {
            PMSHelper.ViewModels.PlanSelect.SetRequestView(PMSViews.FailureEdit);
            PMSHelper.ViewModels.PlanSelect.RefreshData();
            NavigationService.GoTo(PMSViews.PlanSelect);
        }
        public void SetBySelect(PMSClient.NewService.DcPlanExtra plan)
        {
            if (plan != null)
            {
                CurrentFailure.ProductID = UsefulPackage.PMSTranslate.PlanLot(plan);
                CurrentFailure.Composition = plan.Misson.CompositionStandard;
                CurrentFailure.Details = plan.Misson.PMINumber;
                //RaisePropertyChanged(nameof(CurrentRecordTest));
            }
        }
        public void SetNew()
        {
            IsNew = true;
            var model = new DcFailure();
            #region 初始化
            IsNew = true;
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.State = PMSCommon.SimpleState.正常.ToString();
            model.ProductID = "无";
            model.Composition = "无";
            model.Details = "无";
            model.Stage = FailureStages[0];
            model.Problem = "无";
            model.Process = "未处理";
            model.Remark = "无";

            #endregion
            CurrentFailure = model;
        }
        public void SetDuplicate(DcFailure model)
        {
            if (model != null)
            {
                IsNew = true;
                CurrentFailure = new DcFailure();
                CurrentFailure.ID = Guid.NewGuid();
                CurrentFailure.CreateTime = DateTime.Now;
                CurrentFailure.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                CurrentFailure.State = PMSCommon.SimpleState.正常.ToString();
                CurrentFailure.ProductID = model.ProductID;
                CurrentFailure.Composition = model.Composition;
                CurrentFailure.Details = model.Details;
                CurrentFailure.Stage = model.Stage; ;
                CurrentFailure.Problem = model.Problem;
                CurrentFailure.Process = model.Process;
                CurrentFailure.Remark = model.Remark;
            }
        }
        public void SetEdit(DcFailure model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentFailure = model;
            }
        }

        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.Failure);
        }

        private void ActionSave()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录？"))
            {
                return;
            }
            if (CurrentFailure.State == "作废")
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定要作废吗？"))
                {
                    return;
                }
            }
            try
            {
                string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                var service = new FailureServiceClient();
                if (IsNew)
                {
                    service.AddFailure(CurrentFailure);
                }
                else
                {
                    service.UpdateFailure(CurrentFailure);
                }
                service.Close();
                PMSHelper.ViewModels.Failure.RefreshData();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
        public List<string> FailureStages { get; set; }
        public List<string> States { get; set; }

        private DcFailure currentFailure;
        public DcFailure CurrentFailure
        {
            get { return currentFailure; }
            set
            {
                currentFailure = value;
                RaisePropertyChanged(nameof(CurrentFailure));
            }
        }

        public RelayCommand Select { get; set; }

    }
}
