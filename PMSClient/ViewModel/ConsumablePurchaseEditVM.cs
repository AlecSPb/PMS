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
    public class ConsumablePurchaseEditVM : BaseViewModelEdit
    {
        public ConsumablePurchaseEditVM()
        {
            States = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.SimpleState>(States);

            ConsumableCategories = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.ConsumableCategory>(ConsumableCategories);

            ConsumableGrades = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.ConsumableGrade>(ConsumableGrades);

            ConsumableUnits = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.ConsumableUnit>(ConsumableUnits);

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
            PMSHelper.ViewModels.ConsumableInventorySelect.SetRequestView(PMSViews.ConsumablePurchaseEdit);
            PMSHelper.ViewModels.ConsumableInventorySelect.RefreshData();
            NavigationService.GoTo(PMSViews.ConsumableInventorySelect);
        }

        public void SetBySelect(DcConsumableInventory model)
        {
            if (model != null)
            {
                CurrentConsumablePurchase.Category = model.Category;
                CurrentConsumablePurchase.ItemName = model.ItemName;
                CurrentConsumablePurchase.Specification = model.Specification;
                CurrentConsumablePurchase.Details = model.Details;
                CurrentConsumablePurchase.QuantityUnit = model.QuantityUnit;
                CurrentConsumablePurchase.Quantity = model.Quantity;
                CurrentConsumablePurchase.Grade = model.Grade;
            }
        }

        public void SetNew()
        {
            IsNew = true;
            var model = new DcConsumablePurchase();
            #region 初始化
            IsNew = true;
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.State = PMSCommon.SimpleState.正常.ToString();
            model.Category = PMSCommon.ConsumableCategory.劳保用品.ToString();
            model.ItemName = "品名";
            model.Specification = "";
            model.Details = "";
            model.Quantity = 0;
            model.QuantityUnit = PMSCommon.ConsumableUnit.个.ToString();
            model.Grade = PMSCommon.ConsumableGrade.Grade_B.ToString();
            model.ProcessHistory = "";
            model.Supplier = "";
            model.TotalCost = 0;
            model.Remark = "";
            model.LastUpdateTime = DateTime.Now;

            #endregion
            CurrentConsumablePurchase = model;
        }
        public void SetDuplicate(DcConsumablePurchase model)
        {
            if (model != null)
            {
                IsNew = true;
                CurrentConsumablePurchase = model;
                CurrentConsumablePurchase.ID = Guid.NewGuid();
                CurrentConsumablePurchase.CreateTime = DateTime.Now;
                CurrentConsumablePurchase.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                CurrentConsumablePurchase.State = PMSCommon.SimpleState.正常.ToString();
                CurrentConsumablePurchase.LastUpdateTime = DateTime.Now;
                CurrentConsumablePurchase.Category = model.Category;
                CurrentConsumablePurchase.ItemName = model.ItemName;
                CurrentConsumablePurchase.Specification = model.Specification;
                CurrentConsumablePurchase.Details = model.Details;
                CurrentConsumablePurchase.QuantityUnit = model.QuantityUnit;
                CurrentConsumablePurchase.Quantity = model.Quantity;
                CurrentConsumablePurchase.Grade = model.Grade;
                CurrentConsumablePurchase.ProcessHistory = "";
                CurrentConsumablePurchase.Remark = "";
                CurrentConsumablePurchase.Supplier = model.Supplier;
                CurrentConsumablePurchase.TotalCost = model.TotalCost;
            }
        }
        public void SetEdit(DcConsumablePurchase model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentConsumablePurchase = model;
            }
        }

        private void GoBack()
        {
            NavigationService.GoTo(PMSViews.ConsumablePurchase);
        }

        private void ActionSave()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录？"))
            {
                return;
            }
            if (CurrentConsumablePurchase.State == "作废")
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定要作废吗？"))
                {
                    return;
                }
            }

            if (!VMHelper.ComumableHelper.IsStrEmptyNull(CurrentConsumablePurchase.ItemName, "品名不能为空"))
            {
                return;
            }

            try
            {
                string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                var service = new ConsumableServiceClient();
                if (IsNew)
                {
                    service.AddConsumablePurchase(CurrentConsumablePurchase);
                }
                else
                {
                    CurrentConsumablePurchase.LastUpdateTime = DateTime.Now;
                    service.UpdateConsumablePurchase(CurrentConsumablePurchase);
                }
                service.Close();
                PMSHelper.ViewModels.ConsumablePurchase.RefreshData();
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

        private DcConsumablePurchase currentConsumablePurchase;
        public DcConsumablePurchase CurrentConsumablePurchase
        {
            get { return currentConsumablePurchase; }
            set
            {
                currentConsumablePurchase = value;
                RaisePropertyChanged(nameof(CurrentConsumablePurchase));
            }
        }

        public RelayCommand Select { get; set; }
    }
}
