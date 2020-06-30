using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace PMSShipment.VMHelper
{
    public class BaseViewModel:ViewModelBase
    {
        public BaseViewModel()
        {

        }
        public RelayCommand Search { get; set; }
        public RelayCommand All { get; set; }
        public RelayCommand GoToNavigation { get; private set; }
    }
}
