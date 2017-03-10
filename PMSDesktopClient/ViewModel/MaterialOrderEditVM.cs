using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.PMSMainService;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;

namespace PMSClient.ViewModel
{
    public class MaterialOrderEditVM : ViewModelBase
    {
        private bool isNew;
        public MaterialOrderEditVM(ModelObject model)
        {
            isNew = model.IsNew;
            CurrentMaterialOrder = model.Model as DcMaterialOrder;

            InitializeProperties();

            InitialCommmands();

        }

        private void InitializeProperties()
        {
            OrderStates = new ObservableCollection<string>();
            var states = Enum.GetNames(typeof(PMSCommon.OrderState));
            states.ToList().ForEach(s => OrderStates.Add(s));


            OrderPriorities = new ObservableCollection<string>();
            var priorities = Enum.GetNames(typeof(PMSCommon.OrderPriority));
            priorities.ToList().ForEach(p => OrderPriorities.Add(p));

            Suppliers = new ObservableCollection<DcBDSupplier>();
            var service = new PMSMainService.SupplierServiceClient();
            var suppliers = service.GetSuppliers();
            suppliers.ToList().ForEach(s => Suppliers.Add(s));
        }

        private void InitialCommmands()
        {
            GiveUp = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VToken.MaterialOrder }));
            Save = new RelayCommand(ActionSave);
        }

        private void ActionSave()
        {
            var service = new MaterialOrderServiceClient();
            if (isNew)
            {
                service.AddMaterialOrder(CurrentMaterialOrder);
            }
            else
            {
                service.UpdateMaterialOrder(CurrentMaterialOrder);
            }
            NavigationService.GoTo(new MsgObject() { MsgToken = VToken.MaterialOrder });
            NavigationService.Refresh(VToken.MaterialOrderRefresh);
        }
        public ObservableCollection<string> OrderStates { get; set; }
        public ObservableCollection<string> OrderPriorities { get; set; }
        public ObservableCollection<DcBDSupplier> Suppliers { get; set; }

        public DcMaterialOrder CurrentMaterialOrder { get; set; }

        public RelayCommand GiveUp { get; private set; }
        public RelayCommand Save { get; private set; }
    }
}
