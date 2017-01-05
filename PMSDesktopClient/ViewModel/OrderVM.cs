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
using PMSFakeService;
using PMSIService;
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

        private void InitialProperties()
        {
            MainOrders = new ObservableCollection<MainOrder>();
            IOrder orderService = new FakeOrderService();
            MainOrders.Clear();
            orderService.GetAll().ToList<MainOrder>().ForEach(order => MainOrders.Add(order));
        }

        private void InitialCommands()
        {
            Navigate = new RelayCommand(() => NavigationService.NavigateTo("NavigationView"));

        }
        #region Properties
        private ObservableCollection<MainOrder> mainOrders;
        public ObservableCollection<MainOrder> MainOrders
        {
            get { return mainOrders; }
            set { mainOrders = value; RaisePropertyChanged(nameof(MainOrders)); }
        }

        #endregion

        #region Commands
        public RelayCommand Navigate { get; private set; }
        #endregion
    }
}
