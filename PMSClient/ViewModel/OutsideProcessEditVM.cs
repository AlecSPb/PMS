using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using PMSClient.OutsideProcessService;

namespace PMSClient.ViewModel
{
    public class OutsideProcessEditVM : BaseViewModelEdit
    {
        public OutsideProcessEditVM()
        {
            States = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.OutsideProcessState>(States);

            Processors = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.OutsideProcessor>(Processors);

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
            PMSHelper.ViewModels.PlanSelect.SetRequestView(PMSViews.OutsideProcessEdit);
            PMSHelper.ViewModels.PlanSelect.RefreshData();
            NavigationService.GoTo(PMSViews.PlanSelect);
        }
        public void SetBySelect(DcPlanWithMisson plan)
        {
            if (plan != null)
            {
                CurrentOutsideProcess.ProductID = plan.Plan.SearchCode+"-1";
                CurrentOutsideProcess.Composition = plan.Misson.CompositionStandard;
                CurrentOutsideProcess.Customer = plan.Misson.CustomerName;
                CurrentOutsideProcess.Dimension = plan.Misson.Dimension;
                CurrentOutsideProcess.PMINumber = plan.Misson.PMINumber;
                CurrentOutsideProcess.PONumber = plan.Misson.PO;
                
                //RaisePropertyChanged(nameof(CurrentRecordTest));
            }
        }
        public void SetNew()
        {
            IsNew = true;
            var model = new DcOutsideProcess();
            #region 初始化
            IsNew = true;
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.State = PMSCommon.OutsideProcessState.未完成.ToString();
            model.ProductID = "ID";
            model.Composition = "成分";
            model.Customer = "";
            model.Dimension = "";
            model.PMINumber = "";
            model.PONumber = "";
            model.Processor = "炬科光学";
            model.ProgressBar = "";
            model.Remark = "";

            #endregion
            CurrentOutsideProcess = model;
        }
        public void SetDuplicate(DcOutsideProcess model)
        {
            if (model != null)
            {
                IsNew = true;
                CurrentOutsideProcess = model;
                CurrentOutsideProcess.ID = Guid.NewGuid();
                CurrentOutsideProcess.CreateTime = DateTime.Now;
                CurrentOutsideProcess.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                CurrentOutsideProcess.State = PMSCommon.OutsideProcessState.未完成.ToString();
                CurrentOutsideProcess.ProductID = model.ProductID;
                CurrentOutsideProcess.Composition = model.Composition;
                CurrentOutsideProcess.Dimension = model.Dimension;
                CurrentOutsideProcess.Customer = model.Customer;
                CurrentOutsideProcess.PMINumber = model.PMINumber;
                CurrentOutsideProcess.PONumber = model.PONumber;
                CurrentOutsideProcess.Processor = model.Processor;
                CurrentOutsideProcess.ProgressBar = model.ProgressBar;
                CurrentOutsideProcess.Remark = model.Remark;
            }

        }
        public void SetEdit(DcOutsideProcess model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentOutsideProcess = model;
            }
        }

        private void GoBack()
        {
            NavigationService.GoTo(PMSViews.OutsideProcess);
        }

        private void ActionSave()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录？"))
            {
                return;
            }
            if (CurrentOutsideProcess.State == "作废")
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定要作废吗？"))
                {
                    return;
                }
            }
            try
            {
                string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                var service = new OutsideProcessServiceClient();
                if (IsNew)
                {
                    service.Add(CurrentOutsideProcess);
                }
                else
                {
                    service.Update(CurrentOutsideProcess);
                }
                service.Close();
                PMSHelper.ViewModels.OutsideProcess.RefreshData();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
        public List<string> States { get; set; }
        public List<string> Processors { get; set; }

        private DcOutsideProcess currentOutsideProcess;
        public DcOutsideProcess CurrentOutsideProcess
        {
            get { return currentOutsideProcess; }
            set
            {
                currentOutsideProcess = value;
                RaisePropertyChanged(nameof(CurrentOutsideProcess));
            }
        }


        public RelayCommand Select { get; set; }
    }
}
