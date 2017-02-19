using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;


namespace PMSDesktopClient.ViewModel
{
    public class NavigationVM : ViewModelBase
    {
        public NavigationVM()
        {
            InitialCommands();
        }

        private void InitialCommands()
        {
            GoToOrder = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VT.Order }));
            GoToOrderCheck = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VT.OrderCheck }));
            GoToMisson = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VT.Misson }));
            GoToPlan = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VT.Plan }));

            GoToMaterialNeed = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VT.MaterialNeed }));
            GoToMaterialOrder = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VT.MaterialOrder }));
            GoToMaterialInventory = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VT.MaterialInventory }));

            GoToMillingRecord = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VT.RecordMilling }));
            GoToVHPRecord = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VT.RecordVHP }));
            //GoToBlankRecord = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = null })));
            //GoToMachineRecord = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken =null })));
            GoToDeliveryRecord = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VT.RecordDelivery }));
            GoToBondingRecord = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VT.RecordBonding }));
            GoToTestResultRecord = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VT.RecordTestResult }));

        }
        public RelayCommand GoToOrder { get; private set; }
        public RelayCommand GoToOrderCheck { get; private set; }
        public RelayCommand GoToMisson { get; private set; }
        public RelayCommand GoToPlan { get; private set; }

        public RelayCommand GoToMaterialNeed { get; private set; }
        public RelayCommand GoToMaterialOrder { get; private set; }
        public RelayCommand GoToMaterialInventory { get; private set; }
        public RelayCommand GoToMillingRecord { get; private set; }
        public RelayCommand GoToVHPRecord { get; private set; }
        public RelayCommand GoToBlankRecord { get; private set; }
        public RelayCommand GoToMachineRecord { get; private set; }
        public RelayCommand GoToDeliveryRecord { get; private set; }
        public RelayCommand GoToBondingRecord { get; private set; }


        public RelayCommand GoToTestResultRecord { get; private set; }
    }
}
