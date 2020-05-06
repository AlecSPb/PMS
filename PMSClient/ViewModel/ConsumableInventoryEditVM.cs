using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using PMSClient.ConsumableService;

namespace PMSClient.ViewModel
{
    public class ConsumableInventoryEditVM : BaseViewModelEdit
    {
        public ConsumableInventoryEditVM()
        {
            States = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.SimpleState>(States);

            ConsumableCategories = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.ConsumableCategory>(ConsumableCategories);

            ConsumableGrades = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.ConsumableGrade>(ConsumableGrades);

            ConsumableUnits = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.ConsumableUnit>(ConsumableUnits);

            StorePositions = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.ConsumableStorePosition>(StorePositions);
            InitializeCommands();
        }


        private void InitializeCommands()
        {
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
        }

        public void SetNew()
        {
            IsNew = true;
            var model = new DcConsumableInventory();
            #region 初始化
            IsNew = true;
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.State = PMSCommon.SimpleState.正常.ToString();
            model.Category = PMSCommon.ConsumableCategory.劳动保护.ToString();
            model.ItemName = "品名";
            model.Specification = "";
            model.Details = "";
            model.Quantity = 0;
            model.QuantityUnit = PMSCommon.ConsumableUnit.个.ToString();
            model.Grade = PMSCommon.ConsumableGrade.Grade_B.ToString();
            model.MaxWarningQuantity = 1000;
            model.MinWarningQuantity = 100;
            model.History = "";
            model.CountHistory = "";
            model.Remark = "";
            model.LastUpdateTime = DateTime.Now;
            #endregion
            CurrentConsumableInventory = model;
        }
        public void SetDuplicate(DcConsumableInventory model)
        {
            if (model != null)
            {
                IsNew = true;
                CurrentConsumableInventory = model;
                CurrentConsumableInventory.ID = Guid.NewGuid();
                CurrentConsumableInventory.CreateTime = DateTime.Now;
                CurrentConsumableInventory.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                CurrentConsumableInventory.State = PMSCommon.SimpleState.正常.ToString();
                CurrentConsumableInventory.LastUpdateTime = DateTime.Now;
                CurrentConsumableInventory.Category = model.Category;
                CurrentConsumableInventory.ItemName = model.ItemName;
                CurrentConsumableInventory.Specification = model.Specification;
                CurrentConsumableInventory.Details = model.Details;
                CurrentConsumableInventory.Quantity = model.Quantity;
                CurrentConsumableInventory.QuantityUnit = model.QuantityUnit;
                CurrentConsumableInventory.Grade = model.Grade;
                CurrentConsumableInventory.StorePosition = model.StorePosition;
                CurrentConsumableInventory.PersonInCharge = model.PersonInCharge;
                CurrentConsumableInventory.MaxWarningQuantity = model.MaxWarningQuantity;
                CurrentConsumableInventory.MinWarningQuantity = model.MinWarningQuantity;
                CurrentConsumableInventory.History = "";
                CurrentConsumableInventory.CountHistory = "";
                CurrentConsumableInventory.Remark = "";
            }
        }

        public void SetEdit(DcConsumableInventory model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentConsumableInventory = model;
            }
        }

        private void GoBack()
        {
            NavigationService.GoTo(PMSViews.ConsumableInventory);
        }

        private void ActionSave()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录？"))
            {
                return;
            }
            if (CurrentConsumableInventory.State == "作废")
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定要作废吗？"))
                {
                    return;
                }
            }

            if (!VMHelper.ComumableHelper.IsStrEmptyNull(CurrentConsumableInventory.ItemName, "品名不能为空"))
            {
                return;
            }

            try
            {
                var service = new ConsumableServiceClient();
                if (IsNew)
                {
                    service.AddConsumableInventory(CurrentConsumableInventory);
                }
                else
                {
                    if (CurrentConsumableInventory.State == PMSCommon.SimpleState.正常.ToString())
                    {
                        CurrentConsumableInventory.LastUpdateTime = DateTime.Now;
                    }
                    service.UpdateConsumableInventory(CurrentConsumableInventory);
                }
                service.Close();
                PMSHelper.ViewModels.ConsumableInventory.RefreshData();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
        public List<string> ConsumableCategories { get; set; }
        public List<string> ConsumableUnits { get; set; }
        public List<string> ConsumableGrades { get; set; }
        public List<string> States { get; set; }
        public List<string> StorePositions { get; set; }

        private DcConsumableInventory currentConsumableInventory;
        public DcConsumableInventory CurrentConsumableInventory
        {
            get { return currentConsumableInventory; }
            set
            {
                currentConsumableInventory = value;
                RaisePropertyChanged(nameof(CurrentConsumableInventory));
            }
        }
    }
}
