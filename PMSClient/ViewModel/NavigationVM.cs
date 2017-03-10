using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;


namespace PMSClient.ViewModel
{
    public class NavigationVM : ViewModelBase
    {
        public NavigationVM()
        {
            InitialCommands();
            SetCurrentUserName();
        }

        private void SetCurrentUserName()
        {
            var username = (App.Current as App).CurrentUser.UserName;
            LogInformation = $"当前用户:{username}";
        }

        private string logInformation;

        public string LogInformation
        {
            get { return logInformation; }
            set { logInformation = value; RaisePropertyChanged(nameof(LogInformation)); }
        }

        private void InitialCommands()
        {
            GoToOrder = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VToken.Order }));
            GoToOrderCheck = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VToken.OrderCheck }));
            GoToMisson = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VToken.Misson }));
            GoToPlan = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VToken.Plan }));

            GoToMaterialNeed = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VToken.MaterialNeed }));
            GoToMaterialOrder = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VToken.MaterialOrder }));
            GoToMaterialInventory = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VToken.MaterialInventory }));

            GoToMillingRecord = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VToken.RecordMilling }));
            GoToVHPRecord = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VToken.RecordVHP }));
            //GoToBlankRecord = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = null })));
            //GoToMachineRecord = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken =null })));
            GoToDeliveryRecord = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VToken.RecordDelivery }));
            GoToBondingRecord = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VToken.RecordBonding }));
            GoToTestResultRecord = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VToken.RecordTestResult }));

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
