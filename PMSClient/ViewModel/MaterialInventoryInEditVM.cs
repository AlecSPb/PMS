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
    public class MaterialInventoryInEditVM : BaseViewModelEdit
    {
        public MaterialInventoryInEditVM()
        {
            InitializeProperties();
            InitialCommands();
        }

        public void SetNew()
        {

            var empty = new DcMaterialInventoryIn();
            #region 初始化
            empty.Id = Guid.NewGuid();
            empty.PMINumber = Helpers.DefaultHelper.DefaultPMINumber();
            empty.MaterialLot = DateTime.Now.ToString("yyMMdd") + "A";
            empty.Composition = "成分";
            empty.State = PMSCommon.InventoryState.库存.ToString();
            empty.CreateTime = DateTime.Now;
            empty.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            empty.Supplier = PMSCommon.MaterialSupplier.三杰.ToString();
            empty.Weight = 1;
            empty.Purity = "4.5N";
            empty.Remark = "无";
            empty.QuickRemark = "";
            empty.SupplierPO = "";
            empty.GDMS = VMHelper.SampleVMHelper.GDMS;
            empty.ICPOES = VMHelper.SampleVMHelper.ICPOES;


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
                CurrentMaterialInventoryIn.GDMS = VMHelper.SampleVMHelper.GDMS;
                CurrentMaterialInventoryIn.ICPOES = VMHelper.SampleVMHelper.ICPOES;
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
                    service.AddMaterialInventoryInByUID(CurrentMaterialInventoryIn, uid);
                }
                else
                {
                    service.UpdateMaterialInventoryInByUID(CurrentMaterialInventoryIn, uid);
                }

                //询问是否增加对应的出库记录
                if (CurrentMaterialInventoryIn.State == PMSCommon.InventoryState.发货.ToString())
                {
                    var material_in = CurrentMaterialInventoryIn;
                    string dialog_msg = $"是否增加一条[{material_in.PMINumber}]{material_in.Composition}出库记录？";
                    if (PMSDialogService.ShowYesNo("请问", dialog_msg))
                    {
                        var material_out = new DcMaterialInventoryOut();
                        material_out.Id = Guid.NewGuid();
                        material_out.State = PMSCommon.SimpleState.正常.ToString();
                        material_out.CreateTime = DateTime.Now;
                        material_out.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                        material_out.Receiver = PMSCommon.MaterialComsumer.车间.ToString();
                        material_out.ActualWeight = 0;

                        material_out.Composition = material_in.Composition;
                        material_out.MaterialLot = material_in.MaterialLot;
                        material_out.PMINumber = material_in.PMINumber;
                        material_out.Purity = material_in.Purity;
                        material_out.Weight = material_in.Weight;
                        material_out.Remark = material_in.Supplier;

                        service.AddMaterialInventoryOutByUID(material_out, uid);
                    }
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
