using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSDesktopClient.PMSMainService;
using System.Collections.ObjectModel;

namespace PMSDesktopClient.ViewModel
{
    public class MaterialOrderEditVM : ViewModelBase
    {
        private bool isNew;
        public MaterialOrderEditVM(MessageObject msg)
        {
            isNew = msg.IsAdd;
            CurrentMaterialOrder = msg.ModelObject as DcMaterialOrder;

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

            InitialCommmands();

        }

        private void InitialCommmands()
        {
            GiveUp = new RelayCommand(() => NavigationService.GoTo(VNCollection.MaterialOrder));
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
            NavigationService.GoTo(VNCollection.MaterialOrder);
        }
        public ObservableCollection<string> OrderStates { get; set; }
        public ObservableCollection<string> OrderPriorities { get; set; }
        public ObservableCollection<DcBDSupplier> Suppliers { get; set; }

        public DcMaterialOrder CurrentMaterialOrder { get; set; }

        public RelayCommand GiveUp { get; private set; }
        public RelayCommand Save { get; private set; }
    }
}
