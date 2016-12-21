using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace PMSTabletClient.ViewModel
{
    public class RecordVHPVM : ViewModelBase
    {
        public RecordVHPVM()
        {
            CommandBackToMain = new RelayCommand(ActionBackToMain);
        }

        private void ActionBackToMain()
        {
            Messenger.Default.Send<string>(null, ViewToken.MainNavigate);
        }

        #region Commands
        public RelayCommand CommandBackToMain { get; private set; }
        #endregion


    }
}
