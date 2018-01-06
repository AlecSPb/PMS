﻿using System;
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
    public class CompoundEditVM : BaseViewModelEdit
    {
        public CompoundEditVM()
        {
            InitializeProperties();
            InitialCommands();
        }

        public void SetNew()
        {

            var empty = new DcMaterialInventoryIn();
            #region 初始化
            empty.Id = Guid.NewGuid();
            empty.PMINumber = DateTime.Now.ToString("yyMMdd");
            empty.MaterialLot = DateTime.Now.ToString("yyMMdd") + "A";
            empty.Composition = "成分";
            empty.State = PMSCommon.InventoryState.库存.ToString();
            empty.CreateTime = DateTime.Now;
            empty.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            empty.Supplier = PMSCommon.MaterialSupplier.三杰.ToString();
            empty.Weight = 1;
            empty.Purity = "4.5N";
            empty.Remark = "无";
            #endregion

            IsNew = true;
            CurrentMaterialInventoryIn = empty;
        }

        public void SetEdit(DcMaterialInventoryIn model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentMaterialInventoryIn = model;
            }
        }

        public void SetBySelect(DcMaterialOrderItem item)
        {
            if (item != null)
            {
                CurrentMaterialInventoryIn.Composition = item.Composition;
                CurrentMaterialInventoryIn.MaterialLot = item.OrderItemNumber;
                CurrentMaterialInventoryIn.PMINumber = item.PMINumber;
                CurrentMaterialInventoryIn.Weight = item.Weight;
                CurrentMaterialInventoryIn.Purity = item.Purity;
                //RaisePropertyChanged(nameof(CurrentMaterialInventoryIn));
            }
        }


        private void InitializeProperties()
        {
            States = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.InventoryState>(States);

            Suppliers = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.MaterialSupplier>(Suppliers);
        }

        private void InitialCommands()
        {
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
            Select = new RelayCommand(ActionSelect);
        }

        private void GoBack()
        {
            NavigationService.GoTo(PMSViews.MaterialInventoryIn);
        }

        private void ActionSelect()
        {
            PMSHelper.ViewModels.MaterialOrderItemSelect.SetRequestView(PMSViews.MaterialInventoryInEdit);
            NavigationService.GoTo(PMSViews.MaterialOrderItemSelect);
        }

        private void ActionSave()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录？"))
            {
                return;
            }
            if (CurrentMaterialInventoryIn.State == "作废")
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定作废这条记录？"))
                {
                    return;
                }
            }
            try
            {
                string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                var service = new MaterialInventoryServiceClient();
                if (IsNew)
                {
                    service.AddMaterialInventoryInByUID(CurrentMaterialInventoryIn,uid);
                }
                else
                {
                    service.UpdateMaterialInventoryInByUID(CurrentMaterialInventoryIn,uid);
                }
                service.Close();
                PMSHelper.ViewModels.MaterialInventoryIn.RefreshData();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private DcMaterialInventoryIn currentMaterialInventoryIn;
        public DcMaterialInventoryIn CurrentMaterialInventoryIn
        {
            get { return currentMaterialInventoryIn; }
            set
            {
                currentMaterialInventoryIn = value;
                RaisePropertyChanged(nameof(CurrentMaterialInventoryIn));
            }
        }
        public List<string> States { get; set; }
        public List<string> Suppliers { get; set; }
        public RelayCommand Select { get; set; }
    }
}