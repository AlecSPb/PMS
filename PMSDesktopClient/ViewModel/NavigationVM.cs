using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;


namespace PMSDesktopClient.ViewModel
{
    public class NavigationVM:ViewModelBase
    {
        public NavigationVM()
        {
            Navigate = new RelayCommand<string>(ActionNavigate);
        }

        private void ActionNavigate(string viewModelName)
        {
            NavigationService.GoTo(viewModelName);
        }

        public RelayCommand<string> Navigate { get; set; }
    }
}
