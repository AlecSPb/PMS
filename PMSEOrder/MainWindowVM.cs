using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSEOrder.Model;
using GalaSoft.MvvmLight.Messaging;

namespace PMSEOrder
{
    public class MainWindowVM : ViewModelBase
    {
        public MainWindowVM()
        {
            Initialize();
            LoadData();
        }

        public void Initialize()
        {
            Orders = new ObservableCollection<Order>();
            searchCustomer = searchPO = searchComposition = "";


            New = new RelayCommand(ActionNew);
            Edit = new RelayCommand<Order>(ActionEdit);
            Duplicate = new RelayCommand<Order>(ActionDuplicate);
            ExportSingle = new RelayCommand<Order>(ActionExportSingle);
            ExportUnSend = new RelayCommand(ActionExportUnSend);
            StateChange = new RelayCommand<Order>(ActionStateChange);
            Search = new RelayCommand(ActionSearch);
            Backup = new RelayCommand(ActionBackup);
            Excel = new RelayCommand(ActionExcel);
            PMSRefresh = new RelayCommand(ActionPMSRefresh);

            Messenger.Default.Register<NotificationMessage>(this, "MSG", ActionDo);
        }

        private void ActionDo(NotificationMessage obj)
        {
            if (obj.Notification == "RefreshMainWindow")
            {
                LoadData();
            }
        }

        private void ActionPMSRefresh()
        {
            throw new NotImplementedException();
        }

        private void ActionStateChange(Order obj)
        {
            throw new NotImplementedException();
        }

        private void ActionExcel()
        {
            throw new NotImplementedException();
        }

        private void ActionBackup()
        {
            throw new NotImplementedException();
        }

        private void ActionSearch()
        {
            LoadData();
        }

        private void ActionExportUnSend()
        {
            throw new NotImplementedException();
        }

        private void ActionExportSingle(Order obj)
        {
            throw new NotImplementedException();
        }

        private void ActionDuplicate(Order obj)
        {
            if (obj == null) return;
            var edit = new OrderEditView();
            ((OrderEditVM)edit.DataContext).SetDuplicate(obj);
            edit.ShowDialog();
        }

        private void ActionEdit(Order obj)
        {
            if (obj == null) return;
            var edit = new OrderEditView();
            ((OrderEditVM)edit.DataContext).SetEdit(obj);
            edit.ShowDialog();
        }

        private void ActionNew()
        {
            var edit = new OrderEditView();
            ((OrderEditVM)edit.DataContext).SetNew();
            edit.ShowDialog();
        }

        private void LoadData()
        {
            var s = new Service.DataService();
            var orders = s.GetAllOrder();
            var filter_orders = orders.Where(i => 
                                            i.CustomerName.ToLower().Contains(SearchCustomer.ToLower()) 
                                            && i.Composition.ToLower().Contains(SearchComposition.ToLower()) 
                                            && i.PO.ToLower().Contains(SearchPO.ToLower()))
                                      .OrderByDescending(i => i.CreateTime).ToList();
            Orders.Clear();
            filter_orders.ForEach(i => Orders.Add(i));
        }

        private string searchCustomer;
        public string SearchCustomer
        {
            get
            {
                return searchCustomer;
            }
            set
            {
                searchCustomer = value;
                RaisePropertyChanged(nameof(SearchCustomer));
            }
        }

        private string searchComposition;
        public string SearchComposition
        {
            get
            {
                return searchComposition;
            }
            set
            {
                searchComposition = value;
                RaisePropertyChanged(nameof(SearchComposition));
            }
        }

        private string searchPO;
        public string SearchPO
        {
            get
            {
                return searchPO;
            }
            set
            {
                searchPO = value;
                RaisePropertyChanged(nameof(SearchPO));
            }
        }



        public ObservableCollection<Order> Orders { get; set; }
        public RelayCommand New { get; set; }
        public RelayCommand<Order> Edit { get; set; }
        public RelayCommand<Order> Duplicate { get; set; }
        public RelayCommand<Order> ExportSingle { get; set; }
        public RelayCommand<Order> StateChange { get; set; }



        public RelayCommand ExportUnSend { get; set; }
        public RelayCommand Search { get; set; }
        public RelayCommand Backup { get; set; }
        public RelayCommand Excel { get; set; }
        public RelayCommand PMSRefresh { get; set; }
    }
}
