using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSClient.Helper;

namespace PMSClient.ViewModel
{
    public class NavigationVM : ViewModelBase
    {
        private LogInformation _logIn;
        public NavigationVM()
        {
            InitialCommands();
            _logIn = PMSHelper.CurrentLogInformation;
        }


        private string logInformation;

        public string LogInformation
        {
            get { return logInformation; }
            set { logInformation = value; RaisePropertyChanged(nameof(LogInformation)); }
        }

        private void InitialCommands()
        {
            GoToOrder = new RelayCommand(ActionOrder, CanOrder);
            GoToOrderCheck = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { NavigateTo = VToken.OrderCheck }));
            GoToMisson = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { NavigateTo = VToken.Misson }));
            GoToPlan = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { NavigateTo = VToken.Plan }));

            GoToMaterialNeed = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { NavigateTo = VToken.MaterialNeed }));
            GoToMaterialOrder = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { NavigateTo = VToken.MaterialOrder }));
            GoToMaterialInventory = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { NavigateTo = VToken.MaterialInventory }));

            GoToMillingRecord = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { NavigateTo = VToken.RecordMilling }));
            GoToVHPRecord = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { NavigateTo = VToken.RecordVHP }));
            GoToDeMoldRecord = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { NavigateTo = VToken.RecordDeMold }));
            GoToMachineRecord = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { NavigateTo = VToken.RecordMachine }));
            GoToDeliveryRecord = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { NavigateTo = VToken.RecordDelivery }));
            GoToBondingRecord = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { NavigateTo = VToken.RecordBonding }));
            GoToTestResultRecord = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { NavigateTo = VToken.RecordTest }));

        }

        private void ActionOrder()
        {
            NavigationService.GoTo(new MsgObject() { NavigateTo = VToken.Order });
        }

        private bool CanOrder()
        {
            return _logIn.IsAuthorized("000001");
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
        public RelayCommand GoToDeMoldRecord { get; private set; }
        public RelayCommand GoToMachineRecord { get; private set; }
        public RelayCommand GoToDeliveryRecord { get; private set; }
        public RelayCommand GoToBondingRecord { get; private set; }


        public RelayCommand GoToTestResultRecord { get; private set; }
    }
}
