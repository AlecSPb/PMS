﻿using System;
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
        public void SetKeyProperties(ModelObject model)
        {
            IsNew = model.IsNew;
            CurrentMaterialOrder = model.Model as DcMaterialOrder;
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
            GiveUp = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { NavigateTo = VToken.MaterialOrder }));
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
            NavigationService.GoTo(new MsgObject() { NavigateTo = VToken.MaterialOrder });
            NavigationService.Refresh(VToken.MaterialOrderRefresh);
        }
        public ObservableCollection<string> OrderStates { get; set; }
        public ObservableCollection<string> OrderPriorities { get; set; }
        public ObservableCollection<DcBDSupplier> Suppliers { get; set; }

        public DcMaterialOrder CurrentMaterialOrder { get; set; }
    }
}
