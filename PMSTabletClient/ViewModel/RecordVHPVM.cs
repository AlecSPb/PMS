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
            BackToMain = new RelayCommand(ActionBackToMain);
            GoToRecordVHPEdit = new RelayCommand(ActionGoToRecordVHPRecordEdit);
        }

        private void ActionGoToRecordVHPRecordEdit()
        {
            NavigationWizard.GoToVHPRecordEdit();
        }

        private void ActionBackToMain()
        {
            NavigationWizard.GoToMainNavigation();
        }

        #region Commands
        public RelayCommand BackToMain { get; private set; }
        public RelayCommand GoToRecordVHPEdit { get; private set; }
        #endregion


    }
}
