using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace PMSClient.ViewModel
{
    public class BaseViewModelEdit : ViewModelBase
    {
        public BaseViewModelEdit()
        {
            GiveUp = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VToken.RecordMilling }));
        }
        #region Commands 
        public RelayCommand Save { get; set; }
        public RelayCommand GiveUp { get; set; }
        #endregion
    }
}
