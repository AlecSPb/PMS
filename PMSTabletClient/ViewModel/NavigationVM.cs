using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSTabletClient.ViewModel
{
    public class NavigationVM : ViewModelBase
    {
        public NavigationVM()
        {
            GoToVHPRecord = new RelayCommand(ActionGoToVHPRecord);
            GoToProduct = new RelayCommand(ActionGoToProduct);
        }

        private void ActionGoToProduct()
        {
            Messenger.Default.Send<string>(null, ViewToken.Product);
        }

        private void ActionGoToVHPRecord()
        {
            Messenger.Default.Send<string>(null, ViewToken.RecordVHP);
        }
        #region Commands
        public RelayCommand GoToVHPRecord { get; private set; }
        public RelayCommand GoToProduct { get; private set; }
        #endregion


    }
}
