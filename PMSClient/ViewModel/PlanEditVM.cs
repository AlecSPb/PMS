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
            plan.MoldType = "GQ";
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
            plan.GrainSize = "-200";
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
            Molds = new ObservableCollection<DcBDVHPMold>();
            Molds.Clear();
            PMSBasicDataService.VHPMolds.ToList().ForEach(m => Molds.Add(m));

            States = new ObservableCollection<string>();
            States.Clear();
            PMSBasicDataService.VHPPlanStates.ToList().ForEach(s => States.Add(s));

            ProcessCodes = new ObservableCollection<string>();
            ProcessCodes.Clear();
            PMSBasicDataService.VHPProcesses.ToList().ForEach(p => ProcessCodes.Add(p.CodeName));


            DeviceCodes = new ObservableCollection<string>();
            DeviceCodes.Clear();
            PMSBasicDataService.VHPDevices.ToList().ForEach(d => DeviceCodes.Add(d.CodeName));


            Compounds = new ObservableCollection<DcBDCompound>();
            Compounds.Clear();
            PMSBasicDataService.Compounds.ToList().ForEach(c => Compounds.Add(c));
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

        public ObservableCollection<DcBDVHPMold> Molds { get; set; }
        public ObservableCollection<string> States { get; set; }
        public ObservableCollection<string> ProcessCodes { get; set; }
        public ObservableCollection<string> DeviceCodes { get; set; }
        public ObservableCollection<DcBDCompound> Compounds { get; set; }


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
