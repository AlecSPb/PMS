using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSDesktopClient.PMSMainService;

namespace PMSDesktopClient.ViewModel
{
    public class PlanSelectForRecordVHPVM : PlanSelectBase
    {
        public PlanSelectForRecordVHPVM()
        {
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            GiveUp = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VToken.RecordVHP }));
            Select = new RelayCommand<PMSMainService.DcMissonWithPlan>(ActionSelect);
        }

        private void ActionSelect(DcMissonWithPlan obj)
        {
            //var recordvhp = new DcRecordVHP();
            //recordvhp.ID = Guid.NewGuid();
            //recordvhp.CreateTime = DateTime.Now;
            //recordvhp.Creator = (App.Current as App).CurrentUser.UserName;
            //recordvhp.State = PMSCommon.CommonState.Show.ToString();

            //recordvhp.PlanID = obj.PlanID;
            //recordvhp.Composition = obj.CompositionStandard;
            //recordvhp.PlanDate = obj.PlanDate;
            //recordvhp.MoldType = obj.MoldType;
            //recordvhp.MoldDiameter = obj.MoldDiameter;
            //recordvhp.VHPDeviceCode = obj.VHPDeviceCode;

            //recordvhp.PreTemperature = obj.PreTemperature;
            //recordvhp.PrePressure = obj.PrePressure;
            //recordvhp.Pressure = obj.Pressure;
            //recordvhp.Vaccum = obj.Vaccum;
            //recordvhp.Temperature = obj.Temperature;
            //recordvhp.KeepTempTime = obj.KeepTempTime;
            //recordvhp.Remark = obj.VHPRequirement;

            //var msg = new MsgObject();
            //msg.MsgToken = VToken.RecordVHPEdit;
            //msg.MsgModel = new ModelObject() { IsNew = true, Model = recordvhp };
            //NavigationService.GoTo(msg);
        }
    }
}
