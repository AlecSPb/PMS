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
            #region 初始化
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.State = PMSCommon.OrderState.未核验.ToString();
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.Supplier = "Sanjie";
            model.SupplierAbbr = "SJ";
            model.SupplierEmail = "sj_materials@163.com";
            model.SupplierReceiver = "Mr.Wang";
            model.SupplierAddress = "Chengdu,Sichuan CHINA";
            model.ShipFee = 0;
            model.Priority = PMSCommon.OrderPriority.通常.ToString();
            model.Remark = "";
            model.OrderPO = DateTime.Now.ToString("yyMMdd") + "_" + model.SupplierAbbr;
            #endregion

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
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
        }

        private void GoBack()
        {
            NavigationService.GoTo(PMSViews.MaterialOrder);
        }

        private void ActionSave()
        {
            try
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
                service.Close();
                PMSHelper.ViewModels.MaterialOrder.RefreshData();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }

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
