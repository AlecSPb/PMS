using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSCommon;
using PMSClient.MainService;
using System.Collections.ObjectModel;

namespace PMSClient.ViewModel
{
    public class MaterialInventoryInUnCompletedVM : BaseViewModelPage
    {
        public MaterialInventoryInUnCompletedVM()
        {
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }

        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }

        private void InitializeProperties()
        {
            searchComposition = searchMaterialLot = searchPMINumber = searchSupplier = "";
            MaterialInventoryIns = new ObservableCollection<DcMaterialInventoryIn>();
        }
        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);

            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<DcMaterialInventoryIn>(ActionEdit, CanEdit);

            GoToMaterialInventoryOut = new RelayCommand(() => NavigationService.GoTo(PMSViews.MaterialInventoryOut),
                () => PMSHelper.CurrentSession.IsAuthorized(PMSAccess.ReadMaterialInventoryIn));

        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditMaterialInventoryIn);
        }

        private bool CanEdit(DcMaterialInventoryIn arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditMaterialInventoryIn);
        }

        private void ActionEdit(DcMaterialInventoryIn model)
        {
            if (model != null)
            {
                PMSHelper.ViewModels.MaterialInventoryInEdit.SetEdit(model);
                NavigationService.GoTo(PMSViews.MaterialInventoryInEdit);
            }
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.MaterialInventoryInEdit.SetNew();
            NavigationService.GoTo(PMSViews.MaterialInventoryInEdit);
        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchComposition) && string.IsNullOrEmpty(SearchMaterialLot)
                && string.IsNullOrEmpty(SearchSupplier) && string.IsNullOrEmpty(SearchPMINumber));
        }

        private void ActionAll()
        {
            SearchComposition = SearchPMINumber = SearchMaterialLot = SearchSupplier = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 20;
            var service = new MaterialInventoryServiceClient();
            RecordCount = service.GetMaterialInventoryInCountBySearch(SearchSupplier, SearchComposition, SearchMaterialLot, SearchPMINumber);
            service.Close();
            ActionPaging();
        }
        /// <summary>
        /// 分页动作的时候读入数据
        /// </summary>
        private void ActionPaging()
        {

            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var service = new MaterialInventoryServiceClient();
            var result = service.GetMaterialInventoryInsBySearch(skip, take, SearchSupplier, SearchComposition, SearchMaterialLot, SearchPMINumber);
            service.Close();
            MaterialInventoryIns.Clear();
            result.ToList().ForEach(o => MaterialInventoryIns.Add(o));
        }

        #region Proeperties
        private string searchComposition;
        public string SearchComposition
        {
            get { return searchComposition; }
            set
            {
                if (searchComposition == value)
                    return;
                searchComposition = value;
                RaisePropertyChanged(() => SearchComposition);
            }
        }

        private string searchMaterialLot;
        public string SearchMaterialLot
        {
            get { return searchMaterialLot; }
            set { searchMaterialLot = value; RaisePropertyChanged(nameof(SearchMaterialLot)); }
        }

        private string searchSupplier;
        public string SearchSupplier
        {
            get { return searchSupplier; }
            set { searchSupplier = value; RaisePropertyChanged(nameof(SearchSupplier)); }
        }

        private string searchPMINumber;
        public string SearchPMINumber
        {
            get { return searchPMINumber; }
            set { searchPMINumber = value; RaisePropertyChanged(nameof(SearchPMINumber)); }
        }



        private ObservableCollection<DcMaterialInventoryIn> materialInventoryIns;
        public ObservableCollection<DcMaterialInventoryIn> MaterialInventoryIns
        {
            get { return materialInventoryIns; }
            set { materialInventoryIns = value; RaisePropertyChanged(nameof(MaterialInventoryIns)); }
        }

        #endregion

        #region Commands
        public RelayCommand Add { get; private set; }
        public RelayCommand<DcMaterialInventoryIn> Edit { get; private set; }


        public RelayCommand GoToMaterialInventoryOut { get; set; }
        #endregion



    }
}
