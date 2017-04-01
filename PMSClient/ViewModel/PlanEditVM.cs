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
            GiveUp = new RelayCommand(ActionGiveUp);
            Save = new RelayCommand(ActionSave);
        }
        public  void SetKeyProperties(ModelObject msg)
        {
            CurrentPlan = msg.Model as DcPlanVHP;
            IsNew = msg.IsNew;
        }

        private void InitializeProperties()
        {
            Molds = new ObservableCollection<DcBDVHPMold>();
            var service = new VHPMoldServiceClient();
            var molds = service.GetVHPMold();
            molds.ToList().ForEach(m => Molds.Add(m));

            States = new ObservableCollection<string>();
            var states = Enum.GetNames(typeof(PMSCommon.VHPPlanState));
            states.ToList().ForEach(s => States.Add(s));

            ProcessCodes = new ObservableCollection<string>();
            var service2 = new VHPProcessServiceClient();
            var processCodes = service2.GetVHPProcess();
            processCodes.ToList().ForEach(p => ProcessCodes.Add(p.CodeName));


            DeviceCodes = new ObservableCollection<string>();
            var service3 = new VHPDeviceServiceClient();
            var devices = service3.GetVHPDevice();
            devices.ToList().ForEach(d => DeviceCodes.Add(d.CodeName));


            Compounds = new ObservableCollection<DcBDCompound>();
            var service4 = new CompoundServiceClient();
            var compounds = service4.GetAllCompounds();
            compounds.ToList().ForEach(c => Compounds.Add(c));
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

                NavigationService.GoTo(new MsgObject() { MsgToken = VToken.Misson });
                NavigationService.Refresh(VToken.MissonRefresh);
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex.Message);
            }
        }

        private void ActionGiveUp()
        {
            NavigationService.GoTo(new MsgObject() { MsgToken = VToken.Misson });
        }
        public ObservableCollection<DcBDVHPMold> Molds { get; set; }
        public ObservableCollection<string> States { get; set; }
        public ObservableCollection<string> ProcessCodes { get; set; }
        public ObservableCollection<string> DeviceCodes { get; set; }
        public ObservableCollection<DcBDCompound> Compounds { get; set; }


        public DcPlanVHP CurrentPlan { get; set; }

    }
}
