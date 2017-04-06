using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;

namespace PMSClient.ViewModel
{
    public class PlanSelectForRecordTestVM : PlanSelectBase
    {
        private DcRecordTest testresult;

        public PlanSelectForRecordTestVM()
        {
            testresult = new MainService.DcRecordTest();
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
            testresult.CompositionAbbr = obj.CompositionAbbr;
            testresult.Customer = obj.CustomerName;
            testresult.Dimension = obj.Dimension;
            testresult.ProductID = obj.PlanDate.ToString("yyMMdd") + "-" + obj.VHPDeviceCode + "-" + 1;
            testresult.PO = obj.PO;
            testresult.DimensionActual = testresult.Dimension;

            MsgObject msg = new MsgObject();
            msg.NavigateTo = VToken.RecordTestEdit;
            msg.MsgModel = new ModelObject() { IsNew = true, Model = testresult };
            NavigationService.GoTo(msg);
        }


        private void IntitializeCommands()
        {
            GiveUp = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { NavigateTo=VToken.RecordTest}));
            Select = new RelayCommand<MainService.DcMissonWithPlan>(ActionSelect);
        }

    }
}
