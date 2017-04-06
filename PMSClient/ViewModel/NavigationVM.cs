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
        private LogInformation _session;
        public NavigationVM()
        {
            InitialCommands();
            _session = PMSHelper.CurrentLogInformation;
        }


        private string logInformation;

        public string LogInformation
        {
            get { return logInformation; }
            set { logInformation = value; RaisePropertyChanged(nameof(LogInformation)); }
        }

        private void InitialCommands()
        {
            GoToOrder = new RelayCommand(() => NavigationService.GoTo(PMSViews.Order), () => _session.IsAuthorized("000001"));
            GoToOrderCheck = new RelayCommand(() => NavigationService.GoTo(PMSViews.OrderCheck), () => _session.IsAuthorized("000003"));

            GoToMaterialNeed = new RelayCommand(() => NavigationService.GoTo(PMSViews.MaterialNeed), () => _session.IsAuthorized("000004"));
            GoToMaterialOrder = new RelayCommand(() => NavigationService.GoTo(PMSViews.MaterialOrder), () => _session.IsAuthorized("000006"));
            GoToMaterialInventory = new RelayCommand(() => NavigationService.GoTo(PMSViews.MaterialInventory), () => _session.IsAuthorized("000008"));

            GoToMisson = new RelayCommand(() => NavigationService.GoTo(PMSViews.Misson), () => _session.IsAuthorized("000010"));
            GoToPlan = new RelayCommand(() => NavigationService.GoTo(PMSViews.Plan), () => _session.IsAuthorized("000012"));

            GoToMillingRecord = new RelayCommand(() => NavigationService.GoTo(PMSViews.RecordMilling), () => _session.IsAuthorized("000013"));
            GoToVHPRecord = new RelayCommand(() => NavigationService.GoTo(PMSViews.RecordVHP), () => _session.IsAuthorized("000015"));
            GoToDeMoldRecord = new RelayCommand(() => NavigationService.GoTo(PMSViews.RecordDeMold), () => _session.IsAuthorized("000017"));
            GoToMachineRecord = new RelayCommand(() => NavigationService.GoTo(PMSViews.RecordMachine), () => _session.IsAuthorized("000019"));
            GoToTestResultRecord = new RelayCommand(() => NavigationService.GoTo(PMSViews.RecordTest), () => _session.IsAuthorized("000021"));
            GoToDeliveryRecord = new RelayCommand(() => NavigationService.GoTo(PMSViews.RecordDelivery), () => _session.IsAuthorized("000023"));
            GoToBondingRecord = new RelayCommand(() => NavigationService.GoTo(PMSViews.RecordBonding), () => _session.IsAuthorized("000025"));
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
