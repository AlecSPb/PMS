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
    public class RecordMillingEditVM : BaseViewModelEdit
    {
        public RecordMillingEditVM()
        {
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);

            InitializeBasicData();

            Select = new RelayCommand(() =>
            {
                PMSHelper.ViewModels.PlanSelect.SetRequestView(PMSViews.RecordMillingEdit);
                PMSHelper.ViewModels.PlanSelect.RefreshData();
                NavigationService.GoTo(PMSViews.PlanSelect);
            });
            Calculator = new RelayCommand(ActionCalculator);
        }

        private void InitializeBasicData()
        {
            States = new List<string>();
            PMSBasicDataService.SetListDS<SimpleState>(States);

            MillingTools = new List<string>();
            PMSBasicDataService.SetListDS<MillingTool>(MillingTools);

            MillingTimes = new List<string>();
            PMSBasicDataService.SetListDS(PMSCommon.CustomData.MillingTime, MillingTimes);

            GasProtections = new List<string>();
            PMSBasicDataService.SetListDS<MillingGas>(GasProtections);

            MillingMaterialSources = new List<string>();
            PMSBasicDataService.SetListDS<MillingMaterialSource>(MillingMaterialSources);

            MillingMaterialTypes = new List<string>();
            PMSBasicDataService.SetListDS(PMSCommon.CustomData.MaterialTypes, MillingMaterialTypes);

            GrainSizes = new List<string>();
            PMSBasicDataService.SetListDS(PMSCommon.CustomData.GrainSize, GrainSizes);
        }
        private void ActionCalculator()
        {
            PMSHelper.ToolViewModels.MaterialNeedCalcualtion.SetRequestView(PMSViews.RecordMillingEdit);
            NavigationService.GoTo(PMSViews.MaterialNeedCalcuationTool);
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
            model.PlanBatchNumber = 0;
            model.PMINumber = UsefulPackage.PMSTranslate.PMINumber();
            model.RoomHumidity = 0;
            model.RoomTemperature = 0;
            model.VHPPlanLot = UsefulPackage.PMSTranslate.PlanLot();
            model.Composition = "填入成分";
            model.GasProtection = PMSCommon.MillingGas.Ar气.ToString();
            model.GrainSize = "未知";
            model.MaterialType = PMSCommon.CustomData.MaterialTypes[4];//默认其他
            model.MaterialSource = PMSCommon.MillingMaterialSource.SJ.ToString();
            model.MillingTool = PMSCommon.MillingTool.行星球磨.ToString();
            model.MillingTime = "无";
            model.Remark = "";
            model.WeightIn = 0;
            model.WeightOut = 0;
            model.WeightRemain = 0;
            model.Ratio = 0;
            model.Water = "无";
            model.Oxygen = "无";
            model.MeltingPoint = "无";
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
                CurrentRecordMilling.PMINumber = plan.Misson.PMINumber;
                CurrentRecordMilling.RoomTemperature = plan.Plan.RoomTemperature;
                CurrentRecordMilling.RoomHumidity = plan.Plan.RoomHumidity;
                //RaisePropertyChanged(nameof(CurrentRecordMilling));
            }
        }



        private void ActionSave()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录？"))
            {
                return;
            }
            if (CurrentRecordMilling.State == "作废")
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定作废这条记录？"))
                {
                    return;
                }
            }
            if (CurrentRecordMilling != null)
            {
                try
                {
                    string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                    using (var service = new RecordMillingServiceClient())
                    {
                        if (IsNew)
                        {
                            service.AddRecordMillingByUID(CurrentRecordMilling, uid);
                        }
                        else
                        {
                            service.UpdateRecordMillingByUID(CurrentRecordMilling, uid);
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
        public List<string> MillingMaterialSources { get; set; }
        public List<string> MillingMaterialTypes { get; set; }
        public List<string> MillingTimes { get; set; }
        public List<string> MillingTools { get; set; }
        public List<string> GasProtections { get; set; }
        public List<string> GrainSizes { get; set; }

        public RelayCommand Select { get; set; }
        public RelayCommand Calculator { get; set; }
    }
}
