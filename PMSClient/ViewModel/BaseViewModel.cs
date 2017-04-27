using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace PMSClient.ViewModel
{
    public class BaseViewModel
    {
        public BaseViewModel()
        {
            GoToNavigation = new RelayCommand(() => NavigationService.GoTo(PMSViews.Navigation));
        }
        public RelayCommand Search { get; set; }
        public RelayCommand All { get; set; }
        public RelayCommand GoToNavigation { get; private set; }
    }
}
