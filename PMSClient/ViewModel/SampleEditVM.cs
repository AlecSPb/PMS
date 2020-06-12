using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.SampleService;
using PMSClient.MainService;

namespace PMSClient.ViewModel
{
    public class SampleEditVM : BaseViewModelEdit
    {
        public SampleEditVM()
        {
            States = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.SimpleState>(States);

            SampleTrackingStages = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.SampleTrackingStage>(SampleTrackingStages);

            SampleFors = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.SampleFor>(SampleFors);

            SampleTypes = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.SampleType>(SampleTypes);

            InitializeCommands();
        }


        private void InitializeCommands()
        {
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
            SelectMisson = new RelayCommand(ActionSelectMisson);
            SelectPlan = new RelayCommand(ActionSelectPlan);
        }

        private void ActionSelectPlan()
        {
            PMSHelper.ViewModels.PlanSelect.SetRequestView(PMSViews.SampleEdit);
            PMSHelper.ViewModels.PlanSelect.SetSearchItem(composition: CurrentSample.Composition, searchlot: "", pminumber: "");
            PMSHelper.ViewModels.PlanSelect.RefreshData();
            NavigationService.GoTo(PMSViews.PlanSelect);
        }

        private void ActionSelectMisson()
        {
            PMSHelper.ViewModels.MissonSelect.SetRequestView(PMSViews.SampleEdit);
            PMSHelper.ViewModels.MissonSelect.RefreshData();
            NavigationService.GoTo(PMSViews.MissonSelect);
        }

        public void SetBySelectPlan(PMSClient.NewService.DcPlanExtra plan)
        {
            if (plan != null)
            {
                CurrentSample.ProductID = plan.Plan.SearchCode + "-1";
                CurrentSample.SampleID = CurrentSample.ProductID;
                CurrentSample.TrackingStage = PMSCommon.SampleTrackingStage.未核验.ToString();
                CurrentSample.TraceInformation = $"{DateTime.Now.ToString("yyyy-MM-dd")}已准备;";
            }
        }

        public void SetBySelectMisson(PMSClient.NewService.DcOrder order)
        {
            if (order != null)
            {
                CurrentSample.ProductID = "无";
                CurrentSample.SampleID = "无";
                CurrentSample.Composition = order.CompositionStandard;
                CurrentSample.PMINumber = order.PMINumber;
                CurrentSample.PO = order.PO;
                CurrentSample.Customer = order.CustomerName;
                CurrentSample.SampleType = PMSCommon.SampleType.块状.ToString();
                CurrentSample.TrackingStage = PMSCommon.SampleTrackingStage.未取样.ToString();
                CurrentSample.SampleFor = PMSCommon.SampleFor.Customer.ToString();

                bool need_need = Helpers.OrderHelper.NeedSample(order.SampleNeed);
                bool need_anlysis = Helpers.OrderHelper.NeedSample(order.SampleForAnlysis);
                string sample_str = "";

                bool need_need_remark = Helpers.OrderHelper.HasSampleRemark(order.SampleNeedRemark);
                bool need_anlysis_remark = Helpers.OrderHelper.HasSampleRemark(order.SampleForAnlysisRemark);
                string sample_remark_str = "";

                if (need_need)
                {
                    sample_str += $"客户样品:{order.SampleNeed};";
                }

                if (need_anlysis)
                {
                    sample_str += $"自分析样品:{order.SampleForAnlysis};";
                }
                CurrentSample.OriginalRequirement = sample_str;

                if (need_need_remark)
                {
                    sample_remark_str += $"客户样品:{order.SampleNeedRemark};";
                }

                if (need_anlysis_remark)
                {
                    sample_remark_str += $"自分析样品:{order.SampleForAnlysisRemark};";
                }
                CurrentSample.OriginalRequirementRemark = sample_remark_str;


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
            model.SampleID = "无";
            model.OriginalRequirement = "无";
            model.OriginalRequirementRemark = "无";
            model.Composition = "无";
            model.PMINumber = "无";
            model.PO = "无";
            model.TrackingStage = SampleTrackingStages[0];
            model.Customer = "无";
            model.TraceInformation = "";
            model.ICPOES = "";
            model.GDMS = "";
            model.IGA = "";
            model.Thermal = VMHelper.SampleVMHelper.Thermal;
            model.Permittivity = VMHelper.SampleVMHelper.Permittivity;
            model.OtherTestResult = "";
            model.SampleType = PMSCommon.SampleType.块状.ToString();
            model.SampleFor = PMSCommon.SampleFor.Customer.ToString();
            model.Quantity = 1;
            model.Weight = "无";
            model.MoreInformation = "无";
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
                CurrentSample.PO = model.PO;
                CurrentSample.Customer = model.Customer;
                CurrentSample.OriginalRequirement = model.OriginalRequirement;
                CurrentSample.OriginalRequirementRemark = model.OriginalRequirementRemark;
                CurrentSample.MoreInformation = model.MoreInformation;
                CurrentSample.TrackingStage = PMSCommon.SampleTrackingStage.未取样.ToString();
                CurrentSample.TraceInformation = model.TraceInformation;
                CurrentSample.Weight = model.Weight;
                CurrentSample.Quantity = model.Quantity;
                CurrentSample.SampleFor = model.SampleFor;

                CurrentSample.ICPOES = model.ICPOES;
                CurrentSample.GDMS = model.GDMS;
                CurrentSample.IGA = model.IGA;
                CurrentSample.OtherTestResult = model.OtherTestResult;


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
            //检查PMINumber是否符合格式规范
            VMHelper.CommonVMHelper.CheckPMINumber(CurrentSample.PMINumber);

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
        public List<string> SampleTrackingStages { get; set; }
        public List<string> SampleFors { get; set; }
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

        public RelayCommand SelectMisson { get; set; }

        public RelayCommand SelectPlan { get; set; }

    }
}
