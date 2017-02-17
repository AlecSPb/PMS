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
            GoToOrder = new RelayCommand(() => NavigationService.GoTo(VNCollection.Order));
            GoToOrderCheck = new RelayCommand(() => NavigationService.GoTo(VNCollection.OrderCheck));
            GoToMisson = new RelayCommand(() => NavigationService.GoTo(VNCollection.Misson));
            GoToPlan = new RelayCommand(() => NavigationService.GoTo(VNCollection.Plan));
            GoToMaterialNeed = new RelayCommand(() => NavigationService.GoTo(VNCollection.MaterialNeed));
            GoToMaterialOrder = new RelayCommand(() => NavigationService.GoTo(VNCollection.MaterialOrder));
            GoToMaterialInventory = new RelayCommand(() => NavigationService.GoTo(VNCollection.MaterialInventory));
            GoToMillingRecord = new RelayCommand(() => NavigationService.GoTo("MillingRecordView"));
            GoToVHPRecord = new RelayCommand(() => NavigationService.GoTo("VHPRecordView"));
            GoToBlankRecord = new RelayCommand(() => NavigationService.GoTo("BlankRecordView"));
            GoToMachineRecord = new RelayCommand(() => NavigationService.GoTo("MachineRecordView"));
            GoToDeliveryRecord = new RelayCommand(() => NavigationService.GoTo(VT.RecordDelivery.ToString()));
            GoToBondingRecord = new RelayCommand(() => NavigationService.GoTo("BondingRecordView"));
            GoToTestResultRecord = new RelayCommand(() => NavigationService.GoTo(VT.RecordTestResult.ToString()));

        }
        public RelayCommand GoToOrder { get; private set; }
        public RelayCommand GoToOrderCheck { get; private set; }
        public RelayCommand GoToMisson { get; private set; }
        public RelayCommand GoToPlan { get; private set; }

        public RelayCommand GoToMaterialNeed { get; private set; }
        public RelayCommand GoToMaterialOrder { get; private set; }
        public RelayCommand GoToMaterialInventory { get; private set; }
        public RelayCommand GoToMillingRecord{ get; private set; }
        public RelayCommand GoToVHPRecord { get; private set; }
        public RelayCommand GoToBlankRecord { get; private set; }
        public RelayCommand GoToMachineRecord { get; private set; }
        public RelayCommand GoToDeliveryRecord { get; private set; }
        public RelayCommand GoToBondingRecord { get; private set; }


        public RelayCommand GoToTestResultRecord { get; private set; }
    }
}
