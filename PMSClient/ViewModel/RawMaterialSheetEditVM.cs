using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.Other;

namespace PMSClient.ViewModel
{
    public class RawMaterialSheetEditVM : BaseViewModelEdit
    {
        public RawMaterialSheetEditVM()
        {
            States = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.RawMaterialSheetState>(States);

            InitializeCommands();
        }


        private void InitializeCommands()
        {
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
            Select = new RelayCommand(ActionSelect);
        }

        private void ActionSelect()
        {

        }

        public void SetNew()
        {
            IsNew = true;
            var model = new DcRawMaterialSheet();
            #region 初始化
            IsNew = true;
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.State = PMSCommon.RawMaterialSheetState.在库.ToString();
            model.Lot= "无";
            model.Composition = "无";
            model.Purity = "5N";
            model.Supplier = "中国";
            model.Weight = 0;
            model.StoreTime = DateTime.Now;
            model.Remark = "无";
            model.IsSampleTaking = false;
            model.SampleTakingTime = DateTime.Now;
            model.GDMS = VMHelper.SampleVMHelper.GDMS;
            model.ICPOES = VMHelper.SampleVMHelper.ICPOES;

            #endregion
            CurrentRawMaterialSheet = model;
        }
        public void SetDuplicate(DcRawMaterialSheet model)
        {
            if (model != null)
            {
                IsNew = true;
                CurrentRawMaterialSheet = new DcRawMaterialSheet();
                CurrentRawMaterialSheet.ID = Guid.NewGuid();
                CurrentRawMaterialSheet.CreateTime = DateTime.Now;
                CurrentRawMaterialSheet.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                CurrentRawMaterialSheet.State = PMSCommon.RawMaterialSheetState.在库.ToString();
                CurrentRawMaterialSheet.Lot= model.Lot;
                CurrentRawMaterialSheet.Composition = model.Composition;
                CurrentRawMaterialSheet.Supplier = model.Supplier;
                CurrentRawMaterialSheet.Weight = model.Weight;
                CurrentRawMaterialSheet.StoreTime = DateTime.Now;
                CurrentRawMaterialSheet.Remark = model.Remark;
                CurrentRawMaterialSheet.IsSampleTaking = false;
                CurrentRawMaterialSheet.SampleTakingTime = DateTime.Now;
                CurrentRawMaterialSheet.GDMS = VMHelper.SampleVMHelper.GDMS;
                CurrentRawMaterialSheet.ICPOES = VMHelper.SampleVMHelper.ICPOES;
            }
        }
        public void SetEdit(DcRawMaterialSheet model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentRawMaterialSheet = model;
            }
        }

        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.RawMaterialSheet);
        }

        private void ActionSave()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录？"))
            {
                return;
            }
            if (CurrentRawMaterialSheet.State == "作废")
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定要作废吗？"))
                {
                    return;
                }
            }
            try
            {
                string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                var service = new RawMaterialSheetServiceClient();
                if (IsNew)
                {
                    service.AddRawMaterialSheet(CurrentRawMaterialSheet);
                }
                else
                {
                    service.UpdateRawMaterialSheet(CurrentRawMaterialSheet);
                }
                service.Close();
                PMSHelper.ViewModels.RawMaterialSheet.RefreshData();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
        public List<string> States { get; set; }

        private DcRawMaterialSheet currentRawMaterialSheet;
        public DcRawMaterialSheet CurrentRawMaterialSheet
        {
            get { return currentRawMaterialSheet; }
            set
            {
                currentRawMaterialSheet = value;
                RaisePropertyChanged(nameof(CurrentRawMaterialSheet));
            }
        }

        public RelayCommand Select { get; set; }

    }
}
