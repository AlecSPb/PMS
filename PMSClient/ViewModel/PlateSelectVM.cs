using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
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
            Select = new RelayCommand<DcPlate>(ActionSelect);
            GiveUp = new RelayCommand(GoBack);
        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchPlateID) && string.IsNullOrEmpty(SearchSupplier));
        }

        private void ActionAll()
        {
            SearchPlateID = SearchSupplier = "";
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

        private void ActionEdit(DcPlate model)
        {
            PMSHelper.ViewModels.PlateEdit.SetEdit(model);
            NavigationService.GoTo(PMSViews.PlateEdit);
        }

        private void ActionSelect(DcPlate model)
        {

            if (model != null)
            {
                switch (requestView)
                {
                    case PMSViews.DeliveryItemEdit:
                        PMSHelper.ViewModels.DeliveryItemEdit.SetBySelect(model);
                        break;
                    case PMSViews.RecordBondingEdit:
                        PMSHelper.ViewModels.RecordBondingEdit.SetBySelect(model);
                        break;
                    default:
                        break;
                }
                GoBack();

            }
        }
        private void ActionSelectAndSend(DcPlate model)
        {
            if (MessageBox.Show("确定要将此背板设置为发出状态？", "请问", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
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
                ActionSelect(model);
            }
        }
        private void GoBack()
        {
            NavigationService.GoTo(requestView);
        }

        private void InitializeProperties()
        {
            Plates = new ObservableCollection<DcPlate>();
            SearchSupplier = searchPlateID = "";

        }
        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 20;
            using (var service = new PlateServiceClient())
            {
                RecordCount = service.GetPlateCount(SearchPlateID, SearchSupplier);
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
                var orders = service.GetPlates(skip, take, SearchPlateID, SearchSupplier);
                Plates.Clear();
                orders.ToList().ForEach(o => Plates.Add(o));
            }
            CurrentSelectItem = Plates.FirstOrDefault();
        }
        #region Commands
        public RelayCommand<DcPlate> Select { get; set; }

        private string searchPlateID;
        public string SearchPlateID
        {
            get { return searchPlateID; }
            set
            {
                if (searchPlateID == value)
                    return;
                searchPlateID = value;
                RaisePropertyChanged(() => SearchPlateID);
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


        public RelayCommand<DcPlate> SelectAndSend { get; set; }
    }
}
