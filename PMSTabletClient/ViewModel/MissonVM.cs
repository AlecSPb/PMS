using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace PMSTabletClient.ViewModel
{
   public  class MissonVM:ViewModelBase
    {
        public MissonVM()
        {

            BackToNavigation = new RelayCommand(ActionBackToNavigation);
        }

        private void ActionBackToNavigation()
        {
            NavigationWizard.GoToMainNavigation();
        }


        #region Commands
        public RelayCommand BackToNavigation { get; set; }
        #endregion
    }
}
