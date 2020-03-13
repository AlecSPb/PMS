using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSClient.BasicService;
using PMSClient.MainService;
using System.Collections.ObjectModel;
using PMSClient.PMSIndexService;

namespace PMSClient.ViewModel
{
    public class PlanEditVM : BaseViewModelEdit
    {
        public PlanEditVM()
        {
            InitializeProperties();
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
            SelectMisson = new RelayCommand(ActionSelectMisson);
        }

        private void ActionSelectMisson()
        {
            if (PMSDialogService.ShowYesNo("注意", "此功能允许将其他任务的计划条件复制过来，需谨慎操作，确定继续吗?"))
            {
                PMSHelper.ViewModels.PlanSelect.SetRequestView(PMSViews.PlanEdit);
                //PMSHelper.ViewModels.PlanSelect.RefreshData();
                NavigationService.GoTo(PMSViews.PlanSelect);
            }
        }

        public void SetBySelect(DcPlanVHP model)
        {
            if (model != null && CurrentPlan != null)
            {
                CurrentPlan.PlanDate = DateTime.Today;
                CurrentPlan.PlanLot = model.PlanLot;
                CurrentPlan.PlanType = model.PlanType;
                CurrentPlan.MoldType = model.MoldType;
                CurrentPlan.VHPDeviceCode = model.VHPDeviceCode;
                CurrentPlan.Temperature = model.Temperature;
                CurrentPlan.Pressure = model.Temperature;
                CurrentPlan.Vaccum = model.Vaccum;
                CurrentPlan.SearchCode = "";
                CurrentPlan.ProcessCode = model.ProcessCode;
                CurrentPlan.PrePressure = model.PrePressure;
                CurrentPlan.PreTemperature = model.PreTemperature;
                CurrentPlan.Quantity = model.Quantity;
                CurrentPlan.AllWeight = model.AllWeight;
                CurrentPlan.SingleWeight = model.SingleWeight;
                CurrentPlan.MoldDiameter = model.MoldDiameter;
                CurrentPlan.Thickness = model.Thickness;
                CurrentPlan.CalculationDensity = model.CalculationDensity;
                CurrentPlan.GrainSize = model.GrainSize;
                CurrentPlan.RoomHumidity = model.RoomHumidity;
                CurrentPlan.RoomTemperature = model.RoomTemperature;
                CurrentPlan.KeepTempTime = model.KeepTempTime;
                CurrentPlan.MillingRequirement = model.MillingRequirement;
                CurrentPlan.MachineRequirement = model.MachineRequirement;
                CurrentPlan.FillingRequirement = model.FillingRequirement;
                CurrentPlan.SpecialRequirement = model.SpecialRequirement;
                CurrentPlan.VHPRequirement = model.VHPRequirement;
                CurrentPlan.IsLocked = false;
            }
        }


        public void SetNew(DcOrder order)
        {
            if (order == null)
            {
                return;
            }
            IsNew = true;
            #region 新建初始化
            var plan = new DcPlanVHP();
            plan.ID = Guid.NewGuid();
            plan.OrderID = order.ID;
            plan.PlanDate = DateTime.Today;
            plan.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            plan.PlanLot = 1;
            plan.PlanType = PMSCommon.VHPPlanType.加工.ToString();
            plan.MoldType = PMSCommon.MoldType.CFC.ToString();
            plan.VHPDeviceCode = "A";
            plan.Temperature = 0;
            plan.Pressure = 0;
            plan.Vaccum = 1E-3;
            plan.ProcessCode = ProcessCodes[0];
            plan.PrePressure = 10;
            plan.PreTemperature = 25;
            plan.Quantity = 1;
            plan.AllWeight = plan.SingleWeight = 0;
            plan.MoldDiameter = 230;
            plan.Thickness = 5;
            plan.CreateTime = DateTime.Now;
            plan.State = PMSCommon.VHPPlanState.已核验.ToString();
            plan.CalculationDensity = 5.75;
            plan.GrainSize = PMSCommon.CustomData.GrainSize[0];
            plan.RoomHumidity = 70;
            plan.RoomTemperature = 23;
            plan.KeepTempTime = 120;
            plan.MillingRequirement = "无";
            plan.MachineRequirement = "无";
            plan.FillingRequirement = PMSCommon.CustomData.FillingRequirement[2];
            plan.SpecialRequirement = "无";
            plan.VHPRequirement = "无";
            plan.Grade = 0;
            plan.Conclusion = "无";
            plan.UpdateTime = DateTime.Now;
            plan.Updator = PMSHelper.CurrentSession.CurrentUser.UserName;
            plan.IsLocked = false;
            #endregion
            CurrentPlan = plan;
        }
        public void SetEdit(DcPlanVHP plan)
        {
            if (plan != null)
            {
                IsNew = false;
                CurrentPlan = plan;
            }
        }

        public void SetDuplicate(DcPlanVHP plan)
        {
            //TODO:这里需要深度拷贝
            if (plan != null)
            {
                IsNew = true;
                CurrentPlan = plan;
                CurrentPlan.ID = Guid.NewGuid();
                CurrentPlan.CreateTime = DateTime.Now;
                CurrentPlan.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                CurrentPlan.State = PMSCommon.VHPPlanState.已核验.ToString();
                CurrentPlan.IsLocked = false;
                CurrentPlan.PlanDate = DateTime.Today;
            }
        }

        private void InitializeProperties()
        {
            States = new List<string>();
            States.Clear();
            PMSBasicDataService.VHPPlanStates.ToList().ForEach(s => States.Add(s));


            ProcessCodes = new List<string>();
            ProcessCodes.Clear();
            PMSBasicDataService.SetListDS(PMSCommon.CustomData.ProcessCode, ProcessCodes);


            DeviceCodes = new List<string>();
            DeviceCodes.Clear();
            PMSBasicDataService.SetListDS(PMSCommon.CustomData.VHPDevice, DeviceCodes);

            Compounds = new List<DcBDCompound>();
            Compounds.Clear();
            //PMSBasicDataService.Compounds.ToList().ForEach(c => Compounds.Add(c));

            Quantities = new List<int>();
            PMSBasicDataService.SetListDS(Quantities, 10);

            PlanLots = new List<int>();
            PMSBasicDataService.SetListDS(PlanLots, 4);

            MoldTypes = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.MoldType>(MoldTypes);

            PlanTypes = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.VHPPlanType>(PlanTypes);

            MoldDiameters = new List<double>();
            PMSBasicDataService.SetListDS(PMSCommon.CustomData.MoldDiameter, MoldDiameters);


            GrainSizes = new List<string>();
            PMSBasicDataService.SetListDS(PMSCommon.CustomData.GrainSize, GrainSizes);

            MillingRequirements = new List<string>();
            PMSBasicDataService.SetListDS(PMSCommon.CustomData.MillingRequirement, MillingRequirements);
            FillingRequirements = new List<string>();
            PMSBasicDataService.SetListDS(PMSCommon.CustomData.FillingRequirement, FillingRequirements);
            MachineRequirements = new List<string>();
            PMSBasicDataService.SetListDS(PMSCommon.CustomData.MachineRequirement, MachineRequirements);
            SpecialRequirements = new List<string>();
            PMSBasicDataService.SetListDS(PMSCommon.CustomData.SpecialRequirement, SpecialRequirements);

        }


        private void ActionSave()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录？"))
            {
                return;
            }
            if (CurrentPlan.State == "作废")
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定作废这条记录？"))
                {
                    return;
                }
            }
            try
            {
                string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                var service = new PlanVHPServiceClient();

                if (CurrentPlan.PlanDate.Date < DateTime.Now.Date)
                {
                    PMSDialogService.ShowWarning("不允许计划日期早于今天日期");
                    return;
                }

                if (IsNew)
                {
                    //新添加-检查流程
                    //检查是否同一天同一台设备已经安排有计划
                    using (var check = new MissonServiceClient())
                    {
                        string search_code = $"{CurrentPlan.PlanDate.ToString("yyMMdd")}-{CurrentPlan.VHPDeviceCode}";
                        var results = check.GetPlanExtra(0, 20, search_code, string.Empty);
                        if (results.Count() > 0)
                        {
                            if (!PMSDialogService.ShowYesNo("确定",
                                    $"同一天同一台设备已经安排了[{results.Count()}]个计划，\r\n仍继续添加此计划？") == true)
                                return;
                        }
                    }
                    //检查后续代码和工艺代码是否符合常规

                    string check_result = Helpers.VHPHelper.CheckPlanTypeAndProcessCode(CurrentPlan.PlanType, CurrentPlan.ProcessCode);
                    if (check_result != "")
                    {
                        if (!PMSDialogService.ShowYesNo("提醒",
                            $"{check_result},\r\n而当前工艺代码是[{CurrentPlan.ProcessCode}],热压类型是[{CurrentPlan.PlanType}],\r\n仍继续添加此计划吗？"))
                            return;
                    }


                    service.AddVHPPlanByUID(CurrentPlan, uid);
                }
                else
                {
                    CurrentPlan.UpdateTime = DateTime.Now;
                    CurrentPlan.Updator = PMSHelper.CurrentSession.CurrentUser.UserName;
                    service.UpdateVHPPlanByUID(CurrentPlan, uid);
                }
                service.Close();
                //计算并保存热压指数
                using (var serviceCalc = new PMSIndexServiceClient())
                {
                    serviceCalc.CalculateProductionIndex(CurrentPlan.OrderID);
                    serviceCalc.CalculateMaterialIndex(CurrentPlan.OrderID);
                }

                PMSHelper.ViewModels.Misson.RefreshData();
                NavigationService.Status("保存成功，请刷新列表");
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.Misson);
        }

        public List<string> States { get; set; }
        public List<string> ProcessCodes { get; set; }
        public List<string> DeviceCodes { get; set; }

        public List<int> Quantities { get; set; }
        public List<int> PlanLots { get; set; }

        public List<string> PlanTypes { get; set; }
        public List<string> MoldTypes { get; set; }
        public List<double> MoldDiameters { get; set; }
        public List<string> GrainSizes { get; set; }
        public List<DcBDCompound> Compounds { get; set; }
        public List<string> MillingRequirements { get; set; }
        public List<string> FillingRequirements { get; set; }
        public List<string> MachineRequirements { get; set; }
        public List<string> SpecialRequirements { get; set; }

        private DcPlanVHP currentPlan;
        public DcPlanVHP CurrentPlan
        {
            get
            {
                return currentPlan;
            }
            set
            {
                Set<DcPlanVHP>(ref currentPlan, value);
            }
        }


        public RelayCommand SelectMisson { get; set; }



    }
}
