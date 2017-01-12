using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSCommon;
using PMSModel;
using PMSDesktopClient.ServiceReference;

using System.Collections.ObjectModel;

namespace PMSDesktopClient.ViewModel
{
    public class OrderVM : ViewModelBase
    {
        public OrderVM()
        {
            InitialProperties();
            InitialCommands();
        }

        private void FillOrders()
        {
            MainOrders = new ObservableCollection<OrderDc>();
            MainOrders.Clear();

        }

        private void InitialCommands()
        {
            Navigate = new RelayCommand(() => NavigationService.NavigateTo("NavigationView"));
            PageCommand = new RelayCommand(() =>
              {

              });
        }
        #region Properties
        private ObservableCollection<OrderDc> mainOrders;
        public ObservableCollection<OrderDc> MainOrders
        {
            get { return mainOrders; }
            set { mainOrders = value; RaisePropertyChanged(nameof(MainOrders)); }
        }

        #endregion

        #region Commands
        public RelayCommand Navigate { get; private set; }
        public RelayCommand PageCommand { get; private set; }
        #endregion
    }
}
