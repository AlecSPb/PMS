using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.ExtraService;
using PMSClient.MainService;

namespace PMSClient.ViewModel
{
    public class PMICounterEditVM : BaseViewModelEdit
    {
        public PMICounterEditVM()
        {
            States = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.SimpleState>(States);

            PMICounterGroups = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.PMICounterGroup>(PMICounterGroups);

            InitializeCommands();
        }


        private void InitializeCommands()
        {
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
            //Select = new RelayCommand(ActionSelect);
        }

        //private void ActionSelect()
        //{
        //    PMSHelper.ViewModels.PlanSelect.SetRequestView(PMSViews.PMICounterEdit);
        //    PMSHelper.ViewModels.PlanSelect.RefreshData();
        //    NavigationService.GoTo(PMSViews.PlanSelect);
        //}
        //public void SetBySelect(DcPlanWithMisson plan)
        //{
        //    if (plan != null)
        //    {
        //        CurrentPMICounter.ProductID = UsefulPackage.PMSTranslate.PlanLot(plan);
        //        CurrentPMICounter.Composition = plan.Misson.CompositionStandard;
        //        CurrentPMICounter.Details = plan.Misson.PMINumber;
        //        //RaisePropertyChanged(nameof(CurrentRecordTest));
        //    }
        //}
        public void SetNew()
        {
            IsNew = true;
            var model = new DcPMICounter();
            #region 初始化
            IsNew = true;
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.State = PMSCommon.SimpleState.正常.ToString();
            model.ItemGroup = PMICounterGroups[0];
            model.ItemName = "无";
            model.ItemSpecification = "未处理";
            model.ItemCount = 1;
            model.ItemDetails = "无";
            model.Unit = "片";
            model.ItemHistory = "";
            model.Remark = "";
            #endregion
            CurrentPMICounter = model;
        }
        public void SetDuplicate(DcPMICounter model)
        {
            if (model != null)
            {
                IsNew = true;
                CurrentPMICounter = new DcPMICounter();
                CurrentPMICounter.ID = Guid.NewGuid();
                CurrentPMICounter.CreateTime = DateTime.Now;
                CurrentPMICounter.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                CurrentPMICounter.State = PMSCommon.SimpleState.正常.ToString();
                CurrentPMICounter.ItemGroup= model.ItemGroup;
                CurrentPMICounter.ItemName = model.ItemName;
                CurrentPMICounter.ItemCount = model.ItemCount;
                CurrentPMICounter.ItemSpecification = model.ItemSpecification;
                CurrentPMICounter.ItemDetails = model.ItemDetails;
                CurrentPMICounter.Unit = model.Unit;
                CurrentPMICounter.ItemHistory = "";
                CurrentPMICounter.Remark = "";
            }
        }
        public void SetEdit(DcPMICounter model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentPMICounter = model;
            }
        }

        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.PMICounter);
        }

        private void ActionSave()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录？"))
            {
                return;
            }
            if (CurrentPMICounter.State == "作废")
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定要作废吗？"))
                {
                    return;
                }
            }

            try
            {
                string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                var service = new PMICounterServiceClient();
                if (IsNew)
                {
                    service.AddPMICounter(CurrentPMICounter);
                }
                else
                {
                    service.UpdatePMICounter(CurrentPMICounter);
                }
                service.Close();
                PMSHelper.ViewModels.PMICounter.RefreshData();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
        public List<string> PMICounterGroups { get; set; }
        public List<string> States { get; set; }

        private DcPMICounter currentPMICounter;
        public DcPMICounter CurrentPMICounter
        {
            get { return currentPMICounter; }
            set
            {
                currentPMICounter = value;
                RaisePropertyChanged(nameof(CurrentPMICounter));
            }
        }

        public RelayCommand Select { get; set; }

    }
}
