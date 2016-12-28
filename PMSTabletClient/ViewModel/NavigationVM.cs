using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSCommon;

namespace PMSTabletClient.ViewModel
{
    public class NavigationVM : ViewModelBase
    {
        public NavigationVM()
        {
            InitialCommands();
        }

        private void InitialCommands()
        {
            GoToVHPRecord = new RelayCommand(() => Messenger.Default.Send<string>(null, ViewToken.RecordVHP));
            GoToProduct = new RelayCommand(() => Messenger.Default.Send<string>(null, ViewToken.Product));
            GoToOrder = new RelayCommand(() => Messenger.Default.Send<string>(null, ViewToken.Order));
            GoToMisson = new RelayCommand(() => Messenger.Default.Send<string>(null, ViewToken.Misson));
            GoToPlan = new RelayCommand(() => Messenger.Default.Send<string>(null, ViewToken.Plan));
        }
        #region Commands
        public RelayCommand GoToVHPRecord { get; private set; }
        public RelayCommand GoToProduct { get; private set; }
        public RelayCommand GoToOrder { get; private set; }
        public RelayCommand GoToMisson { get; private set; }
        public RelayCommand GoToPlan { get; private set; }
        #endregion


    }
}
