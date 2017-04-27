using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.BasicService;
using System.Collections.ObjectModel;

namespace PMSClient.ViewModel
{
    public class CustomerVM: BaseViewModel
    {
        public CustomerVM()
        {
            Customers = new ObservableCollection<DcBDCustomer>();
            Add = new RelayCommand(ActionAdd,CanAdd);
            Edit = new RelayCommand<DcBDCustomer>(ActionEdit,CanEdit);
            RefreshData();
        }

        private bool CanEdit(DcBDCustomer arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑客户记录");
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑客户记录");
        }

        private void ActionEdit(DcBDCustomer model)
        {
            PMSHelper.ViewModels.CustomerEdit.SetEdit(model);
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
                using (var service=new CustomerServiceClient())
                {
                    service.GetCustomer().ToList().ForEach(i => Customers.Add(i));
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
        public ObservableCollection<DcBDCustomer> Customers { get; set; }

        public RelayCommand Add { get; set; }
        public RelayCommand<DcBDCustomer> Edit { get; set; }
    }
}
