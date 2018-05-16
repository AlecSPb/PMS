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
    public class MaterialInventoryOutEditVM : BaseViewModelEdit
    {
        public MaterialInventoryOutEditVM()
        {
            InitializeProperties();
            InitialCommands();
        }

        public void SetNew()
        {

            var empty = new DcMaterialInventoryOut();
            #region 初始化
            empty.Id = Guid.NewGuid();
            empty.PMINumber = DateTime.Now.ToString("yyMMdd");
            empty.MaterialLot = DateTime.Now.ToString("yyMMdd") + "A";
            empty.Composition = "成分";
            empty.State = PMSCommon.SimpleState.正常.ToString();
            empty.CreateTime = DateTime.Now;
            empty.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            empty.Receiver = PMSCommon.MaterialComsumer.车间.ToString();
            empty.Weight = 1;
            empty.Purity = "5N";
            empty.Remark = "无";
            empty.ActualWeight = 0;
            #endregion

            IsNew = true;
            CurrentMaterialInventoryOut = empty;
        }

        public void SetEdit(DcMaterialInventoryOut model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentMaterialInventoryOut = model;
            }
        }

        public void SetBySelect(DcMaterialInventoryIn model)
        {
            try
            {
                using (var service = new MaterialInventoryServiceClient())
                {
                    model.State = PMSCommon.InventoryState.发货.ToString();
                    service.UpdateMaterialInventoryInByUID(model, PMSHelper.CurrentSession.CurrentUser.UserName);
                    PMSDialogService.ShowYes("请问", $"[{model.PMINumber}{model.Composition}{model.MaterialLot}]的入库记录已标记为出库");
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
            if (model != null)
            {
                CurrentMaterialInventoryOut.Composition = model.Composition;
                CurrentMaterialInventoryOut.MaterialLot = model.MaterialLot;
                CurrentMaterialInventoryOut.PMINumber = model.PMINumber;
                CurrentMaterialInventoryOut.Purity = model.Purity;
                CurrentMaterialInventoryOut.Weight = model.Weight;
                CurrentMaterialInventoryOut.Remark = model.Supplier;
                //RaisePropertyChanged(nameof(CurrentMaterialInventoryOut));
            }
        }


        private void InitializeProperties()
        {
            States = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.SimpleState>(States);
            Receivers = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.MaterialComsumer>(Receivers);
        }

        private void InitialCommands()
        {
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
            Select = new RelayCommand(ActionSelect);
        }

        private void ActionSelect()
        {
            PMSHelper.ViewModels.MaterialInventoryInSelect.SetRequestView(PMSViews.MaterialInventoryOutEdit);
            NavigationService.GoTo(PMSViews.MaterialInventoryInSelect);
        }

        private void ActionSave()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条出库记录？"))
            {
                return;
            }
            if (CurrentMaterialInventoryOut.State == "作废")
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定作废这条出库记录？"))
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
                    service.AddMaterialInventoryOutByUID(CurrentMaterialInventoryOut, uid);
                }
                else
                {
                    service.UpdateMaterialInventoryOutByUID(CurrentMaterialInventoryOut, uid);
                }
                service.Close();
                PMSHelper.ViewModels.MaterialInventoryOut.RefreshData();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.MaterialInventoryOut);
        }

        private DcMaterialInventoryOut currentMaterialInventoryOut;
        public DcMaterialInventoryOut CurrentMaterialInventoryOut
        {
            get { return currentMaterialInventoryOut; }
            set
            {
                currentMaterialInventoryOut = value;
                RaisePropertyChanged(nameof(CurrentMaterialInventoryOut));
            }
        }
        public List<string> States { get; set; }
        public List<string> Receivers { get; set; }

        public RelayCommand Select { get; set; }
    }
}
