using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSClient.MainService;
using System.Collections.ObjectModel;

namespace PMSClient.ViewModel
{
    public class OrderCheckEditVM : BaseViewModelEdit
    {
        public OrderCheckEditVM()
        {
            InitializeCommands();
            InitializeProperties();
        }

        public void SetEdit(DcOrder order)
        {
            if (order != null)
            {
                IsNew = false;
                CurrentOrder = order;
            }
        }



        private void InitializeCommands()
        {
            Save = new RelayCommand(ActionSave, CanSave);
            GiveUp = new RelayCommand(GoBack);
        }

        public void InitializeProperties()
        {
            OrderStates = new ObservableCollection<string>();
            var states = Enum.GetNames(typeof(PMSCommon.OrderState));
            states.ToList().ForEach(s => OrderStates.Add(s));


            OrderPriorities = new ObservableCollection<string>();
            var priorities = Enum.GetNames(typeof(PMSCommon.OrderPriority));
            priorities.ToList().ForEach(p => OrderPriorities.Add(p));


            PolicyTypes = new ObservableCollection<string>();
            var policyTypes = Enum.GetNames(typeof(PMSCommon.OrderPolicyType));
            policyTypes.ToList().ForEach(p => PolicyTypes.Add(p));
        }

        private bool CanSave()
        {
            return true;
        }

        private void ActionSave()
        {
            try
            {
                CurrentOrder.Reviewer = PMSHelper.CurrentSession.CurrentUser.UserName;
                CurrentOrder.ReviewPassed = true;
                CurrentOrder.ReviewDate = DateTime.Now;

                var service = new OrderServiceClient();
                if (IsNew)
                {
                    service.AddOrder(CurrentOrder);
                }
                else
                {
                    service.UpdateOrder(CurrentOrder);
                }
                PMSHelper.ViewModels.OrderCheck.RefreshData();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.OrderCheck);
        }

        private DcOrder currentOrder;

        public DcOrder CurrentOrder
        {
            get { return currentOrder; }
            set
            {
                currentOrder = value;
                RaisePropertyChanged(nameof(CurrentOrder));
            }
        }

        public ObservableCollection<string> OrderStates { get; set; }
        public ObservableCollection<string> OrderPriorities { get; set; }

        public ObservableCollection<string> PolicyTypes { get; set; }

    }
}
