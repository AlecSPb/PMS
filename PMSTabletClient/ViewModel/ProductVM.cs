using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace PMSTabletClient.ViewModel
{
    public class ProductVM : ViewModelBase
    {
        public ProductVM()
        {
            BackToMain = new RelayCommand(ActionBackToMain);
        }

        private void ActionBackToMain()
        {
            NavigationWizard.GoToMainNavigation();
        }
        #region Commands
        public RelayCommand BackToMain { get; private set; }
        #endregion


    }
}
