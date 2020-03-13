using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.Sample;
using PMSClient.MainService;

namespace PMSClient.ViewModel
{
    public class SampleEditVM : BaseViewModelEdit
    {
        public SampleEditVM()
        {
            States = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.SimpleState>(States);

            SampleTypes = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.SampleType>(SampleTypes);

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
            PMSHelper.ViewModels.PlanSelect.SetRequestView(PMSViews.SampleEdit);
            PMSHelper.ViewModels.PlanSelect.RefreshData();
            NavigationService.GoTo(PMSViews.PlanSelect);
        }
        public void SetBySelect(DcPlanWithMisson plan)
        {
            if (plan != null)
            {
                CurrentSample.ProductID = UsefulPackage.PMSTranslate.PlanLot(plan);
                CurrentSample.Composition = plan.Misson.CompositionStandard;
                CurrentSample.PMINumber = plan.Misson.PMINumber;
                CurrentSample.Customer = plan.Misson.CustomerName;
                CurrentSample.SampleType = PMSCommon.SampleType.未取样.ToString();

                bool need_need = Helpers.OrderHelper.NeedSample(plan.Misson.SampleNeed);
                bool need_anlysis = Helpers.OrderHelper.NeedSample(plan.Misson.SampleForAnlysis);
                string sample_str = "";
                if (need_need)
                {
                    sample_str += $"客户样品:{plan.Misson.SampleNeed};";
                }

                if (need_anlysis)
                {
                    sample_str += $"自分析样品:{plan.Misson.SampleForAnlysis};";
                }
                CurrentSample.OriginalRequirement = sample_str;
                //RaisePropertyChanged(nameof(CurrentRecordTest));
            }
        }




        public void SetNew()
        {
            IsNew = true;
            var model = new DcSample();
            #region 初始化
            IsNew = true;
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.State = PMSCommon.SimpleState.正常.ToString();
            model.ProductID = "无";
            model.OriginalRequirement = "";
            model.Composition = "无";
            model.PMINumber = "无";
            model.SampleType = SampleTypes[0];
            model.Customer = "无";
            model.TraceInformation = "";
            model.TestResult = "";
            model.MoreInformation = "无";
            model.MoreTestResult = "";
            model.Remark = "无";

            #endregion
            CurrentSample = model;
        }
        public void SetDuplicate(DcSample model)
        {
            if (model != null)
            {
                IsNew = true;
                CurrentSample = new DcSample();
                CurrentSample.ID = Guid.NewGuid();
                CurrentSample.CreateTime = DateTime.Now;
                CurrentSample.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                CurrentSample.State = PMSCommon.SimpleState.正常.ToString();
                CurrentSample.ProductID = model.ProductID;
                CurrentSample.Composition = model.Composition;
                CurrentSample.SampleType = model.SampleType;
                CurrentSample.PMINumber = model.PMINumber;
                CurrentSample.Customer = model.Customer;
                CurrentSample.OriginalRequirement = model.OriginalRequirement;
                CurrentSample.MoreInformation = model.MoreInformation;
                CurrentSample.TestResult = model.TestResult;
                CurrentSample.MoreTestResult = model.MoreTestResult;
                CurrentSample.TraceInformation = model.TraceInformation;
                CurrentSample.Remark = model.Remark;
            }
        }
        public void SetEdit(DcSample model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentSample = model;
            }
        }

        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.Sample);
        }

        private void ActionSave()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录？"))
            {
                return;
            }
            if (CurrentSample.State == "作废")
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定要作废吗？"))
                {
                    return;
                }
            }
            try
            {
                string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                var service = new SampleServiceClient();
                if (IsNew)
                {
                    service.AddSample(CurrentSample);
                }
                else
                {
                    service.UpdateSample(CurrentSample);
                }
                service.Close();
                PMSHelper.ViewModels.Sample.RefreshData();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
        public List<string> SampleTypes { get; set; }
        public List<string> States { get; set; }

        private DcSample currentSample;
        public DcSample CurrentSample
        {
            get { return currentSample; }
            set
            {
                currentSample = value;
                RaisePropertyChanged(nameof(CurrentSample));
            }
        }

        public RelayCommand Select { get; set; }

    }
}
