using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.BasicService;
using System.Collections.ObjectModel;
using PMSClient.ViewModel.Model;

namespace PMSClient.ViewModel
{
    public class CustomerVM : BaseViewModel
    {
        public CustomerVM()
        {
            Customers = new ObservableCollection<CustomerExtra>();
            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<CustomerExtra>(ActionEdit, CanEdit);
            ReOrder = new RelayCommand(ActionReOrder);
            RefreshData();
        }

        private void ActionReOrder()
        {
            try
            {
                Customers.Clear();

                List<CustomerExtra> customers = new List<CustomerExtra>();

                using (var service = new CustomerServiceClient())
                {
                    var s = service.GetCustomer().OrderBy(i => i.CustomerName).ToList();
                    using (var newservice = new NewService.NewServiceClient())
                    {
                        s.ForEach(i =>
                        {
                            DateTime lastorderdate = newservice.GetLastOrderDateByCustomerName(i.CustomerName);
                            customers.Add(new CustomerExtra { Customer = i, LastOrderDate = lastorderdate });
                        });
                    }

                    customers.OrderByDescending(i => i.LastOrderDate).ToList().ForEach(i =>
                    {
                        Customers.Add(i);
                    });

                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private bool CanEdit(CustomerExtra arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditCustomer);
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditCustomer);
        }

        private void ActionEdit(CustomerExtra model)
        {
            PMSHelper.ViewModels.CustomerEdit.SetEdit(model.Customer);
            NavigationService.GoTo(PMSViews.CustomerEdit);
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.CustomerEdit.SetNew();
            NavigationService.GoTo(PMSViews.CustomerEdit);
        }

        public void RefreshData()
        {
            try
            {
                Customers.Clear();

                using (var service = new CustomerServiceClient())
                {
                    var s = service.GetCustomer().OrderBy(i => i.CustomerName).ToList();
                    using (var newservice = new NewService.NewServiceClient())
                    {
                        s.ForEach(i =>
                        {
                            DateTime lastorderdate = newservice.GetLastOrderDateByCustomerName(i.CustomerName);
                            Customers.Add(new CustomerExtra { Customer = i, LastOrderDate = lastorderdate });
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
        public ObservableCollection<CustomerExtra> Customers { get; set; }

        public RelayCommand Add { get; set; }
        public RelayCommand ReOrder { get; set; }
        public RelayCommand<CustomerExtra> Edit { get; set; }
    }
}
