using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.CustomControls;
using PMSClient.MainService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.ViewModel
{
    public class PlateVM : BaseViewModelPage
    {
        public PlateVM()
        {
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }

        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }

        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);
            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<DcPlate>(ActionEdit, CanEdit);
            Duplicate = new RelayCommand<DcPlate>(ActionDuplicate, CanDuplicate);
            BatchDuplicate = new RelayCommand<DcPlate>(ActionBatchDuplicate,CanBatchDuplicate);
            SelectAndSend = new RelayCommand<DcPlate>(ActionSelectAndSend, CanSelect);
        }

        private bool CanBatchDuplicate(DcPlate arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditPlate);
        }

        private bool CanSelect(DcPlate arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditPlate);
        }

        private bool CanDuplicate(DcPlate arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditPlate);
        }

        private void ActionBatchDuplicate(DcPlate model)
        {
            BatchDuplicateDialog dialog = new BatchDuplicateDialog();
            if (dialog.ShowDialog() == true)
            {
                int number = dialog.DuplicateNumber;
                string prefix = dialog.DuplicatePrefix;
                try
                {
                    using (var service = new PlateServiceClient())
                    {
                        for (int i = 0; i < number; i++)
                        {
                            var temp = new DcPlate();
                            temp.ID = Guid.NewGuid();
                            temp.CreateTime = DateTime.Now;
                            temp.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                            temp.State = PMSCommon.InventoryState.库存.ToString();
                            temp.Appearance = model.Appearance;
                            temp.Defects = model.Defects;
                            temp.Dimension = model.Dimension;
                            temp.Hardness = model.Hardness;
                            temp.LastWeldMaterial = model.LastWeldMaterial;
                            temp.PlateLot = prefix + (i + 1).ToString("00");
                            temp.PlateMaterial = model.PlateMaterial;
                            temp.Remark = model.Remark;
                            temp.Supplier = model.Supplier;
                            temp.UseCount = model.UseCount;
                            temp.Weight = model.Weight;

                            service.AddPlateByUID(temp, PMSHelper.CurrentSession.CurrentUser.UserName);
                        }
                    }
                    PMSDialogService.ShowYes("批量复制已完成，请刷新列表查看");
                }
                catch (Exception ex)
                {
                    PMSHelper.CurrentLog.Error(ex);
                }
            }
        }

        private void ActionDuplicate(DcPlate model)
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定复用这条记录吗？"))
            {
                return;
            }
            PMSHelper.ViewModels.PlateEdit.SetDuplicate(model);
            NavigationService.GoTo(PMSViews.PlateEdit);
        }

        private void ActionSelectAndSend(DcPlate model)
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定设置为发货状态吗？"))
            {
                return;
            }

            if (model != null)
            {
                using (var service = new PlateServiceClient())
                {
                    model.State = PMSCommon.InventoryState.发货.ToString();
                    service.UpdatePlate(model);
                }
                SetPageParametersWhenConditionChange();
            }
        }

        private bool CanEdit(DcPlate arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditPlate);
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditPlate);
        }


        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchPlateLot) && string.IsNullOrEmpty(SearchSupplier));
        }

        private void ActionAll()
        {
            SearchPlateLot = SearchSupplier = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void ActionEdit(DcPlate model)
        {
            PMSHelper.ViewModels.PlateEdit.SetEdit(model);
            NavigationService.GoTo(PMSViews.PlateEdit);
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.PlateEdit.SetNew();
            NavigationService.GoTo(PMSViews.PlateEdit);
        }

        private void InitializeProperties()
        {
            Plates = new ObservableCollection<DcPlate>();
            SearchSupplier = searchPlateLot = "";

        }
        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 20;
            using (var service = new PlateServiceClient())
            {
                RecordCount = service.GetPlateCount(SearchPlateLot, SearchSupplier);
            }
            ActionPaging();
        }
        private void ActionPaging()
        {
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            using (var service = new PlateServiceClient())
            {
                var orders = service.GetPlates(skip, take, SearchPlateLot, SearchSupplier);
                Plates.Clear();
                orders.ToList().ForEach(o => Plates.Add(o));
            }
            CurrentSelectItem = Plates.FirstOrDefault();
        }
        #region Commands
        public RelayCommand Add { get; set; }
        public RelayCommand<DcPlate> Edit { get; set; }
        public RelayCommand<DcPlate> SelectAndSend { get; set; }


        private string searchPlateLot;
        public string SearchPlateLot
        {
            get { return searchPlateLot; }
            set
            {
                if (searchPlateLot == value)
                    return;
                searchPlateLot = value;
                RaisePropertyChanged(() => SearchPlateLot);
            }
        }
        private string searchSupplier;
        public string SearchSupplier
        {
            get { return searchSupplier; }
            set
            {
                if (searchSupplier == value)
                    return;
                searchSupplier = value;
                RaisePropertyChanged(() => SearchSupplier);
            }
        }

        public ObservableCollection<DcPlate> Plates { get; set; }

        private DcPlate currentSelectItem;
        public DcPlate CurrentSelectItem
        {
            get { return currentSelectItem; }
            set { currentSelectItem = value; RaisePropertyChanged(nameof(CurrentSelectItem)); }
        }

        #endregion
        public RelayCommand<DcPlate> Duplicate { get; set; }
        public RelayCommand<DcPlate> BatchDuplicate { get; set; }
    }
}
