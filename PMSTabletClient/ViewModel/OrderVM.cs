using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSModel;
using System.Collections.ObjectModel;
using PMSTabletClient.Models;

namespace PMSTabletClient.ViewModel
{
    public class OrderVM : ViewModelBase
    {
        public OrderVM()
        {
            MainOrders = new ObservableCollection<MainOrder>();
            ModelFactory.FillOrder(MainOrders);
            BackToNavigation = new RelayCommand(ActionBackToNavigation);
        }

        private void ActionBackToNavigation()
        {
            NavigationWizard.GoToMainNavigation();
        }

        #region Properties
        public ObservableCollection<MainOrder> MainOrders { get; set; }
        #endregion

        #region Commands
        public RelayCommand BackToNavigation { get; set; }
        #endregion
    }
}
