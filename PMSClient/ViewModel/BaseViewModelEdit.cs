using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace PMSClient.ViewModel
{
    public class BaseViewModelEdit:ViewModelBase
    {
        #region Commands
        public RelayCommand Save { get; set; }
        public RelayCommand GiveUp { get; set; }
        #endregion
    }
}
