using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.ExtraService;
using PMSClient.MainService;

namespace PMSClient.ViewModel
{
    public class RemainInventoryEditVM : BaseViewModelEdit
    {
        public RemainInventoryEditVM()
        {
            States = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.SimpleState>(States);

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
            PMSHelper.ViewModels.PlanSelect.SetRequestView(PMSViews.RemainInventoryEdit);
            PMSHelper.ViewModels.PlanSelect.RefreshData();
            NavigationService.GoTo(PMSViews.PlanSelect);
        }
        public void SetBySelect(DcPlanWithMisson plan)
        {
            if (plan != null)
            {
                CurrentRemainInventory.ProductID = UsefulPackage.PMSTranslate.PlanLot(plan);
                CurrentRemainInventory.Composition = plan.Misson.CompositionStandard;
                CurrentRemainInventory.Dimension = plan.Misson.Dimension;
                CurrentRemainInventory.Details = plan.Misson.PMINumber;
                //RaisePropertyChanged(nameof(CurrentRecordTest));
            }
        }
        public void SetNew()
        {
            IsNew = true;
            var model = new DcRemainInventory();
            #region 初始化
            IsNew = true;
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.State = PMSCommon.InventoryState.库存.ToString();
            model.ProductID = "无";
            model.Composition = "无";
            model.Details = "无";
            model.Dimension = "未知";

            #endregion
            CurrentRemainInventory = model;
        }
        public void SetDuplicate(DcRemainInventory model)
        {
            if (model != null)
            {
                IsNew = true;
                CurrentRemainInventory = new DcRemainInventory();
                CurrentRemainInventory.ID = Guid.NewGuid();
                CurrentRemainInventory.CreateTime = DateTime.Now;
                CurrentRemainInventory.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                CurrentRemainInventory.State = PMSCommon.InventoryState.库存.ToString();
                CurrentRemainInventory.ProductID = model.ProductID;
                CurrentRemainInventory.Composition = model.Composition;
                CurrentRemainInventory.Details = model.Details;
                CurrentRemainInventory.Dimension = model.Dimension;
            }
        }
        public void SetEdit(DcRemainInventory model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentRemainInventory = model;
            }
        }

        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.RemainInventory);
        }

        private void ActionSave()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录？"))
            {
                return;
            }
            if (CurrentRemainInventory.State == "作废")
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定要作废吗？"))
                {
                    return;
                }
            }
            try
            {
                string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                var service = new RemainInventoryServiceClient();
                if (IsNew)
                {
                    service.AddRemainInventory(CurrentRemainInventory);
                }
                else
                {
                    service.UpdateRemainInventory(CurrentRemainInventory);
                }
                service.Close();
                PMSHelper.ViewModels.RemainInventory.RefreshData();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
        public List<string> RemainInventoryStages { get; set; }
        public List<string> States { get; set; }

        private DcRemainInventory currentRemainInventory;
        public DcRemainInventory CurrentRemainInventory
        {
            get { return currentRemainInventory; }
            set
            {
                currentRemainInventory = value;
                RaisePropertyChanged(nameof(CurrentRemainInventory));
            }
        }

        public RelayCommand Select { get; set; }

    }
}
