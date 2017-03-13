using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSTabletClient.PMSMainService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;

namespace PMSTabletClient.ViewModel
{
    public class PlanSelectForRecordTestResultVM : PlanSelectBase
    {
        private DcRecordTest testresult;

        public PlanSelectForRecordTestResultVM()
        {
            testresult = new PMSMainService.DcRecordTest();
            testresult.ID = Guid.NewGuid();
            testresult.CreateTime = DateTime.Now;
            testresult.Creator = (App.Current as App).CurrentUser.UserName;
            testresult.TestType = PMSCommon.TestType.Product.ToString();
            testresult.State = "Checked";
            testresult.Weight = "0";
            testresult.Remark = "";
            testresult.Resistance = "";
            testresult.Sample = "";
            testresult.CompositionXRF = "";
            testresult.Density = "0";

            IntitializeCommands();
        }


        private void ActionSelect(DcMissonWithPlan obj)
        {
            testresult.Composition = obj.CompositionStandard;
            testresult.CompositionAbbr = obj.CompositoinAbbr;
            testresult.Customer = obj.CustomerName;
            testresult.Dimension = obj.Dimension;
            testresult.ProductID = obj.PlanDate.ToString("yyMMdd") + "-" + obj.VHPDeviceCode + "-" + 1;
            testresult.PO = obj.PO;
            testresult.DimensionActual = testresult.Dimension;

            MsgObject msg = new MsgObject();
            msg.MsgToken = VToken.RecordTestEdit;
            msg.MsgModel = new ModelObject() { IsNew = true, Model = testresult };
            NavigationService.GoTo(msg);
        }


        private void IntitializeCommands()
        {
            GiveUp = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken=VToken.RecordTest}));
            Select = new RelayCommand<PMSMainService.DcMissonWithPlan>(ActionSelect);
        }

    }
}
