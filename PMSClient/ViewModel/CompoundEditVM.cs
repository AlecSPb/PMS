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
using PMSClient.BasicService;


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

            var empty = new DcBDCompound();
            #region 初始化
            empty.ID = Guid.NewGuid();
            empty.MaterialName = "材料名称";
            empty.Density = 0.0;
            empty.MeltingPoint = "无";
            empty.BoilingPoint = "无";
            empty.SpecialProperty = "无";
            empty.InformationSource = "其他";
            empty.State = PMSCommon.SimpleState.正常.ToString();
            empty.CreateTime = DateTime.Now;
            empty.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            empty.Remark = "无";
            #endregion

            IsNew = true;
            CurrentCompound = empty;
        }

        public void SetEdit(DcBDCompound model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentCompound = model;
            }
        }

        public void SetBySelect(DcMaterialOrderItem item)
        {
            if (item != null)
            {
                //CurrentCompound.Composition = item.Composition;
                //CurrentCompound.MaterialLot = item.OrderItemNumber;
                //CurrentCompound.PMINumber = item.PMINumber;
                //CurrentCompound.Weight = item.Weight;
                //CurrentCompound.Purity = item.Purity;
                //RaisePropertyChanged(nameof(CurrentMaterialInventoryIn));
            }
        }


        private void InitializeProperties()
        {
            States = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.SimpleState>(States);

            InformationSources = new List<string>();
            PMSBasicDataService.SetListDS(PMSCommon.CustomData.InformationSources, InformationSources);
        }

        private void InitialCommands()
        {
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
            Select = new RelayCommand(ActionSelect);
        }

        private void GoBack()
        {
            NavigationService.GoTo(PMSViews.BDCompound);
        }

        public void SetBySelect(DcMaterialInventoryIn materialIn)
        {
            CurrentCompound.MaterialName = materialIn.Composition;    
        }

        private void ActionSelect()
        {
            //转到材料入库界面,选择材料
            PMSHelper.ViewModels.MaterialInventoryInSelect.SetRequestView(PMSViews.BDCompoundEdit);
            NavigationService.GoTo(PMSViews.MaterialInventoryInSelect);
        }

        private void ActionSave()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录？"))
            {
                return;
            }
            if (CurrentCompound.State == "作废")
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定作废这条记录？"))
                {
                    return;
                }
            }
            try
            {
                string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                CurrentCompound.Creator = uid;
                var service = new CompoundServiceClient();
                if (IsNew)
                {
                    service.AddCompound(CurrentCompound);
                }
                else
                {
                    service.UpdateCompound(CurrentCompound);
                }
                service.Close();
                PMSHelper.ViewModels.Compound.RefreshData();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private DcBDCompound currentCompound;
        public DcBDCompound CurrentCompound
        {
            get { return currentCompound; }
            set
            {
                currentCompound = value;
                RaisePropertyChanged(nameof(CurrentCompound));
            }
        }
        public List<string> States { get; set; }
        public List<string> InformationSources { get; set; }
        public RelayCommand Select { get; set; }
    }
}
