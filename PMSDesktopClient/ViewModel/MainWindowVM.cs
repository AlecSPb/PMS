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
            Navigate = new RelayCommand(() =>NavigationService.NavigateTo("OrderView"));
        }


        #region Commands
        public RelayCommand Navigate { get; private set; }
        #endregion
    }
}
