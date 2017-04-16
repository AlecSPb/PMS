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

namespace PMSClient.ViewModel
{
    public class PlanEditVM : BaseViewModelEdit
    {
        public PlanEditVM()
        {
            InitializeProperties();
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
        }

        public void SetNew(DcOrder order)
        {
            if (order==null)
            {
                return;
            }
            IsNew = true;
            #region 新建初始化
            var plan = new DcPlanVHP();
            plan.ID = Guid.NewGuid();
            plan.OrderID = order.ID;
            plan.PlanDate = DateTime.Now.Date;
            plan.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            plan.PlanLot = 1;
            plan.MoldType = PMSCommon.MoldType.超强.ToString();
            plan.VHPDeviceCode = "A";
            plan.Temperature = 0;
            plan.Pressure = 0;
            plan.Vaccum = 1E-3;
            plan.ProcessCode = "W1";
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
            plan.MillingRequirement = "常规要求";
            plan.MachineRequirement = "常规要求";
            plan.FillingRequirement = "常规要求";
            plan.SpecialRequirement = "无";
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
            if (plan != null)
            {
                IsNew = true;
                CurrentPlan = plan;
            }
        }

        private void InitializeProperties()
        {
            States = new List<string>();
            States.Clear();
            PMSBasicDataService.VHPPlanStates.ToList().ForEach(s => States.Add(s));


            ProcessCodes = new List<string>();
            ProcessCodes.Clear();
            PMSBasicDataService.VHPProcesses.ToList().ForEach(p => ProcessCodes.Add(p.CodeName));


            DeviceCodes = new List<string>();
            DeviceCodes.Clear();
            PMSBasicDataService.VHPDevices.ToList().ForEach(d => DeviceCodes.Add(d.CodeName));

            Compounds = new List<DcBDCompound>();
            Compounds.Clear();
            PMSBasicDataService.Compounds.ToList().ForEach(c => Compounds.Add(c));

            Quantities = new List<int>();
            PMSBasicDataService.SetListDS(Quantities, 10);

            PlanLots = new List<int>();
            PMSBasicDataService.SetListDS(PlanLots, 4);

            MoldTypes = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.MoldType>(MoldTypes);

            MoldDiameters = new List<double>();
            PMSBasicDataService.SetListDS(PMSCommon.CustomData.MoldDiameter, MoldDiameters);


            GrainSizes = new List<string>();
            PMSBasicDataService.SetListDS(PMSCommon.CustomData.GrainSize, GrainSizes);
        }


        private void ActionSave()
        {
            try
            {
                var service = new PlanVHPServiceClient();
                if (IsNew)
                {
                    service.AddVHPPlan(CurrentPlan);
                }
                else
                {
                    service.UpdateVHPPlan(CurrentPlan);
                }
                service.Close();
                PMSHelper.ViewModels.Misson.RefreshData();
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

        public List<string> MoldTypes { get; set; }
        public List<double> MoldDiameters { get; set; }
        public List<string> GrainSizes { get; set; }
        public List<DcBDCompound> Compounds { get; set; }


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

    }
}
