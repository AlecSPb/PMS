using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSEOrder.Model;

namespace PMSEOrder
{
    public class MainWindowVM:ViewModelBase
    {
        public MainWindowVM()
        {

        }

        public void Initialize()
        {
            Orders = new ObservableCollection<Order>();
            searchCustomer = searchPO = searchComposition = "";


            New = new RelayCommand(ActionNew);
            Edit = new RelayCommand<Order>(ActionEdit);
            Copy = new RelayCommand<Order>(ActionDuplicate);
            ExportSingle = new RelayCommand<Order>(ActionExportSingle);
            ExportUnSend = new RelayCommand(ActionExportUnSend);
            StateChange = new RelayCommand<Order>(ActionStateChange);
            Search = new RelayCommand(ActionSearch);
            Backup = new RelayCommand(ActionBackup);
            Excel = new RelayCommand(ActionExcel);
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        private void ActionEdit(Order obj)
        {
            throw new NotImplementedException();
        }

        private void ActionNew()
        {
            var edit = new OrderEditView();
            edit.ShowDialog();
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
        public RelayCommand<Order> Copy { get; set; }
        public RelayCommand<Order> ExportSingle { get; set; }
        public RelayCommand<Order> StateChange { get; set; }



        public RelayCommand ExportUnSend { get; set; }
        public RelayCommand Search { get; set; }
        public RelayCommand Backup { get; set; }
        public RelayCommand Excel { get; set; }
    }
}
