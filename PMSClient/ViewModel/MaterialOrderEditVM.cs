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
            model.State = PMSCommon.MaterialOrderState.未核验.ToString();
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.Supplier ="";
            model.SupplierAbbr = "SJ";
            model.SupplierEmail = "";
            model.SupplierReceiver = "";
            model.SupplierAddress = "";
            model.ShipFee = 0;
            model.Priority = PMSCommon.MaterialOrderItemPriority.普通.ToString();
            model.Remark = "";
            model.OrderPO = DateTime.Now.ToString("yyMMdd") + model.SupplierAbbr;
            model.FinishTime = DateTime.Now;
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
            OrderStates = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.MaterialOrderState>(OrderStates);


            OrderPriorities = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.OrderPriority>(OrderPriorities);

            Suppliers = new List<DcBDSupplier>();
            BasicData.Suppliers.ForEach(i => Suppliers.Add(i));
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
            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录？"))
            {
                return;
            }
            if (CurrentMaterialOrder.State == "作废")
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定作废这条订单记录？"))
                {
                    return;
                }
            }
            try
            {
                string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                var service = new MaterialOrderServiceClient();
                if (IsNew)
                {
                    service.AddMaterialOrderByUID(CurrentMaterialOrder, uid);
                }
                else
                {
                    service.UpdateMaterialOrderByUID(CurrentMaterialOrder, uid);
                                        
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
        public List<string> OrderStates { get; set; }
        public List<string> OrderPriorities { get; set; }
        public List<DcBDSupplier> Suppliers { get; set; }

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
