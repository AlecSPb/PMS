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
            CommandVHPRecord = new RelayCommand(ActionVHPRecord);
        }

        private void ActionVHPRecord()
        {
            Messenger.Default.Send<string>(null, ViewToken.RecordVHP);
        }
        #region Commands
        public RelayCommand CommandVHPRecord { get; private set; }
        #endregion


    }
}
