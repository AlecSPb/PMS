using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.BasicService;

namespace PMSClient.ViewModel
{
    public class CustomerEditVM:BaseViewModelEdit
    {
        public CustomerEditVM()
        {
            GiveUp = new RelayCommand(() => NavigationService.GoTo(PMSViews.Customer));
            Save = new RelayCommand(ActionSave);
            IsCustomerNameReadOnly =true;
        }

        private void ActionSave()
        {
            if (!PMSDialogService.ShowYesNo("",""))
            {
                return;
            }

            if (CurrentCustomer!=null)
            {
                using (var service = new CustomerServiceClient())
                {
                    service.AddCustomer(CurrentCustomer);
                }
                PMSHelper.ViewModels.Customer.RefreshData();
                NavigationService.GoTo(PMSViews.Customer);
            }
        }

        public void SetNew()
        {
            IsCustomerNameReadOnly = false;
            IsNew = true;
            CurrentCustomer = new DcBDCustomer();
            CurrentCustomer.ID = Guid.NewGuid();
            CurrentCustomer.State = "正常";
            CurrentCustomer.CustomerName = "这里的名称一定要一次写对";
        }
        public void SetEdit(DcBDCustomer model)
        {
            IsCustomerNameReadOnly = true;
            if (model!=null)
            {
                IsNew = false;
                CurrentCustomer = model;
            }
        }
        private DcBDCustomer currentCustomer;
        public DcBDCustomer CurrentCustomer
        {
            get { return currentCustomer; }
            set { currentCustomer = value; RaisePropertyChanged(nameof(CurrentCustomer)); }
        }

        private bool isCustomerNameReadOnly;

        public bool IsCustomerNameReadOnly
        {
            get { return isCustomerNameReadOnly; }
            set { isCustomerNameReadOnly = value;RaisePropertyChanged(nameof(IsCustomerNameReadOnly)); }
        }

    }

}
