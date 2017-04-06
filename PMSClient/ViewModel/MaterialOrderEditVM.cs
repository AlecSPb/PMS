using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.BasicService;
using PMSClient.MainService;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;

namespace PMSClient.ViewModel
{
    public class MaterialOrderEditVM : BaseViewModelEdit
    {
        public MaterialOrderEditVM()
        {
            InitializeProperties();
            InitialCommmands();
        }

        public void SetNew()
        {
            var model = new DcMaterialOrder();
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.State = PMSCommon.OrderState.UnChecked.ToString();
            model.Creator = PMSHelper.CurrentLogInformation.CurrentUser.UserName;
            model.Supplier = "Sanjie";
            model.SupplierAbbr = "SJ";
            model.SupplierEmail = "sj_materials@163.com";
            model.SupplierReceiver = "Mr.Wang";
            model.SupplierAddress = "Chengdu,Sichuan CHINA";
            model.ShipFee = 0;
            model.Priority = PMSCommon.OrderPriority.Normal.ToString();
            model.Remark = "";
            model.OrderPO = DateTime.Now.ToString("yyMMdd") + "_" + model.SupplierAbbr;

            IsNew = true;
            CurrentMaterialOrder = model;
        }
        public void SetEdit(DcMaterialOrder model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentMaterialOrder = model;
            }
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
            var service = new SupplierServiceClient();
            var suppliers = service.GetSuppliers();
            suppliers.ToList().ForEach(s => Suppliers.Add(s));
        }

        private void InitialCommmands()
        {
            GiveUp = new RelayCommand(() => NavigationService.GoTo(PMSViews.MaterialOrder));
            Save = new RelayCommand(ActionSave);
        }

        private void ActionSave()
        {
            var service = new MaterialOrderServiceClient();
            if (IsNew)
            {
                service.AddMaterialOrder(CurrentMaterialOrder);
            }
            else
            {
                service.UpdateMaterialOrder(CurrentMaterialOrder);
            }
            NavigationService.GoTo(PMSViews.MaterialOrder);
        }
        public ObservableCollection<string> OrderStates { get; set; }
        public ObservableCollection<string> OrderPriorities { get; set; }
        public ObservableCollection<DcBDSupplier> Suppliers { get; set; }

        private DcMaterialOrder currentMaterialOrder;
        public DcMaterialOrder CurrentMaterialOrder
        {
            get { return currentMaterialOrder; }
            set
            {
                currentMaterialOrder = value;
                RaisePropertyChanged(nameof(CurrentMaterialOrder));
            }
        }
    }
}
