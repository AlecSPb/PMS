using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSCommon;

namespace PMSDesktopClient.ViewModel
{
    public class MainWindowVM:ViewModelBase
    {
        public MainWindowVM()
        {
            InitialCommands();
        }

        private void InitialCommands()
        {
            GoToLogIn = new RelayCommand(() => Messenger.Default.Send<object>(null, ViewToken.LogIn));
            GoToOrder=new RelayCommand(() => Messenger.Default.Send<object>(null, ViewToken.Order));
            GoToOrderEdit = new RelayCommand(() => Messenger.Default.Send<object>(null, ViewToken.OrderEdit));
            GoToOrderReview = new RelayCommand(() => Messenger.Default.Send<object>(null, ViewToken.OrderReview));
            GoToMisson = new RelayCommand(() => Messenger.Default.Send<object>(null, ViewToken.Misson));
            GoToPlan = new RelayCommand(() => Messenger.Default.Send<object>(null, ViewToken.Plan));
        }


        #region Commands
        public RelayCommand GoToLogIn { get; private set; }
        public RelayCommand GoToLogOut { get; private set; }
        public RelayCommand GoToOrder { get; private set; }
        public RelayCommand GoToOrderEdit { get; private set; }
        public RelayCommand GoToOrderReview { get; private set; }
        public RelayCommand GoToMisson { get; private set; }
        public RelayCommand GoToPlan { get; private set; }
        public RelayCommand GoToProduct { get; private set; }
        public RelayCommand GoToDelivery { get; private set; }
        public RelayCommand GoToRecordVHP { get; private set; }
        public RelayCommand GoToRecordMilling { get; private set; }
        public RelayCommand GoToRecordMachine { get; private set; }
        #endregion
    }
}
