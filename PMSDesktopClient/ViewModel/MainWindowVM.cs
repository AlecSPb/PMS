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
            Navigate = new RelayCommand<string>(arg=>NavigationService.NavigateTo(arg));

        }


        #region Commands
        public RelayCommand<string> Navigate { get; private set; }
        #endregion
    }
}
