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
    public class RecordVHPQuickEditVM:ViewModelBase
    {
        public RecordVHPQuickEditVM()
        {
            BackToRecordVHP = new RelayCommand(ActionBackToRecordVHP);
        }
        private void ActionBackToRecordVHP()
        {
            NavigationWizard.GoToRecordVHP();
        }


        #region Commands
        public RelayCommand BackToRecordVHP { get; private set; }
        #endregion
    }
}
