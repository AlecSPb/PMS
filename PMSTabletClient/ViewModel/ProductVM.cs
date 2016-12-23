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
            GoToProductEdit = new RelayCommand(ActionBackToProductEdit);
            GoToProductReport = new RelayCommand(ActionBackToProductReport);
        }

        private void ActionBackToProductReport()
        {
            NavigationWizard.GoToProductReport();
        }

        private void ActionBackToProductEdit()
        {
            NavigationWizard.GoToProductEdit();
        }

        private void ActionBackToMain()
        {
            NavigationWizard.GoToMainNavigation();
        }
        #region Commands
        public RelayCommand BackToMain { get; private set; }
        public RelayCommand GoToProductEdit { get; set; }
        public RelayCommand GoToProductReport { get; set; }
        #endregion


    }
}
