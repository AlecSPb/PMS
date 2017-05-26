using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using PMSClient.ViewModel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PMSClient.ViewModel
{
    public class PlateSelectVM : BaseViewModelSelect
    {
        public PlateSelectVM()
        {
            InitializeCommands();
            InitializeProperties();
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
            Select = new RelayCommand<PlateExtra>(ActionSelect);
            SelectBatch = new RelayCommand(ActionSelectBatch);
            GiveUp = new RelayCommand(GoBack);
        }

        private void ActionSelectBatch()
        {
            if (!PMSDialogService.ShowYesNo("请问","请问要批量添加选定记录？"))
            {
                return;
            }
            try
            {
                switch (requestView)
                {
                    case PMSViews.DeliveryItemEdit:
                        var id = PMSHelper.ViewModels.DeliveryItemEdit.CurrentDeliveryItem.DeliveryID;
                        BatchSaveDelivery(id);
                        NavigationService.GoTo(PMSViews.Delivery);
                        break;
                    default:
                        break;
                }

                PMSDialogService.ShowYes("成功", "请刷新列表查看最新数据");
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private void BatchSaveDelivery(Guid id)
        {
            var serviceDelivery = new DeliveryServiceClient();
            var servicePlate = new PlateServiceClient();
            PlateExtras.Where(i=>i.IsSelected).ToList().ForEach(i =>
            {
                var deliveryItem = PMSNewModelCollection.NewDeliveryItem(id);
                deliveryItem.ProductType = PMSCommon.ProductType.背板.ToString();
                deliveryItem.ProductID = i.Plate.PlateLot;
                deliveryItem.Composition = i.Plate.PlateMaterial;
                deliveryItem.Abbr = i.Plate.PlateMaterial;
                deliveryItem.Customer = "无";
                deliveryItem.Weight = i.Plate.Weight;
                deliveryItem.PO = "无";
                deliveryItem.Dimension = i.Plate.Dimension;
                deliveryItem.DimensionActual = i.Plate.Dimension;
                deliveryItem.Defects = i.Plate.Defects;
                deliveryItem.Remark = i.Plate.Appearance;
                //System.Diagnostics.Debug.Print(item.IsSelected.ToString() + item.Product.ProductID);
                var uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                serviceDelivery.AddDeliveryItemByUID(deliveryItem, uid);

                i.Plate.State = PMSCommon.InventoryState.发货.ToString();
                servicePlate.UpdatePlateByUID(i.Plate, uid);
            });
            serviceDelivery.Close();
            servicePlate.Close();

        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchPlateLot) && string.IsNullOrEmpty(SearchSupplier));
        }

        private void ActionAll()
        {
            SearchPlateLot = SearchSupplier =SearchPrintNumber = "";
            SetPageParametersWhenConditionChange();
        }
        private PMSViews requestView;
        public void SetRequestView(PMSViews view)
        {
            requestView = view;
        }
        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void ActionEdit(PlateExtra model)
        {
            PMSHelper.ViewModels.PlateEdit.SetEdit(model.Plate);
            NavigationService.GoTo(PMSViews.PlateEdit);
        }

        private void ActionSelect(PlateExtra model)
        {

            if (model != null)
            {
                switch (requestView)
                {
                    case PMSViews.DeliveryItemEdit:
                        PMSHelper.ViewModels.DeliveryItemEdit.SetBySelect(model.Plate);
                        break;
                    case PMSViews.RecordBondingEdit:
                        PMSHelper.ViewModels.RecordBondingEdit.SetBySelect(model.Plate);
                        break;
                    default:
                        break;
                }
                GoBack();

            }
        }
        private void ActionSelectAndSend(PlateExtra model)
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定要将此背板设置为发出状态？"))
            {
                return;
            }

            if (model != null)
            {
                using (var service = new PlateServiceClient())
                {
                    model.Plate.State = PMSCommon.InventoryState.发货.ToString();
                    service.UpdatePlate(model.Plate);
                }
                ActionSelect(model);
            }
        }
        private void GoBack()
        {
            NavigationService.GoTo(requestView);
        }

        private void InitializeProperties()
        {
            PlateExtras = new ObservableCollection<PlateExtra>();
            searchSupplier = searchPlateLot =searchPrintNumber = "";

        }
        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 20;
            using (var service = new PlateServiceClient())
            {
                RecordCount = service.GetPlateCount(SearchPlateLot, SearchSupplier, SearchPrintNumber);
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
                var orders = service.GetPlates(skip, take, SearchPlateLot, SearchSupplier, SearchPrintNumber);
                PlateExtras.Clear();
                orders.ToList().ForEach(o => PlateExtras.Add(new PlateExtra { Plate = o }));
            }
            CurrentSelectItem = PlateExtras.FirstOrDefault();
        }
        #region Commands
        public RelayCommand<PlateExtra> Select { get; set; }

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
        private string searchPrintNumber;
        public string SearchPrintNumber
        {
            get { return searchPrintNumber; }
            set
            {
                if (searchPrintNumber == value)
                    return;
                searchPrintNumber = value;
                RaisePropertyChanged(() => SearchPrintNumber);
            }
        }
        public ObservableCollection<PlateExtra> PlateExtras { get; set; }
        private PlateExtra currentSelectItem;

        public PlateExtra CurrentSelectItem
        {
            get { return currentSelectItem; }
            set { currentSelectItem = value; RaisePropertyChanged(nameof(CurrentSelectItem)); }
        }
        #endregion


        public RelayCommand<PlateExtra> Send { get; set; }
        public RelayCommand SelectBatch { get; set; }
    }
}
