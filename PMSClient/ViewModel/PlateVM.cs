using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
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
            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<DcPlate>(ActionEdit, CanEdit);

            SelectAndSend = new RelayCommand<DcPlate>(ActionSelectAndSend);
        }

        private void ActionSelectAndSend(DcPlate model)
        {
            if (!PMSDialogService.ShowYesNo("请问","确定设置为发货状态吗？"))
            {
                return;
            }

            if (model!=null)
            {
                using (var service=new PlateServiceClient())
                {
                    model.State = PMSCommon.InventoryState.发货.ToString();
                    service.UpdatePlate(model);
                }
                SetPageParametersWhenConditionChange();
            }
        }

        private bool CanEdit(DcPlate arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑背板记录");
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑背板记录");
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
        public RelayCommand Add { get; set; }
        public RelayCommand<DcPlate> Edit { get; set; }
        public RelayCommand<DcPlate> SelectAndSend { get; set; }


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

    }
}
