using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSCommon;
using PMSClient.SanjieService;
using System.Collections.ObjectModel;

namespace PMSClient.ViewModel
{
    public class MaterialInventoryOutVM : BaseViewModelPage
    {
        public MaterialInventoryOutVM()
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
            searchComposition = searchMaterialLot = searchPMINumber = "";
            MaterialInventoryOuts = new ObservableCollection<DcMaterialInventoryOut>();
        }
        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);

            GoToMaterialInventoryIn = new RelayCommand(() => NavigationService.GoTo(PMSViews.MaterialInventoryIn),
                () => PMSHelper.CurrentSession.IsAuthorized(PMSAccess.ReadMaterialInventoryIn));

        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchComposition) && string.IsNullOrEmpty(SearchMaterialLot)
                && string.IsNullOrEmpty(SearchPMINumber));
        }

        private void ActionAll()
        {
            SearchComposition = SearchMaterialLot = SearchPMINumber = "";
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
            var service = new SanjieServiceClient();
            RecordCount = service.GetMaterialInventoryOutCount(SearchComposition, SearchMaterialLot, SearchPMINumber);
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
            var service = new SanjieServiceClient();
            var result = service.GetMaterialInventoryOuts(skip, take, SearchComposition, SearchMaterialLot, SearchPMINumber);
            service.Close();
            MaterialInventoryOuts.Clear();
            result.ToList().ForEach(o => MaterialInventoryOuts.Add(o));
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
        private string searchPMINumber;
        public string SearchPMINumber
        {
            get { return searchPMINumber; }
            set { searchPMINumber = value; RaisePropertyChanged(nameof(SearchPMINumber)); }
        }





        private ObservableCollection<DcMaterialInventoryOut> materialInventoryOuts;
        public ObservableCollection<DcMaterialInventoryOut> MaterialInventoryOuts
        {
            get { return materialInventoryOuts; }
            set { materialInventoryOuts = value; RaisePropertyChanged(nameof(MaterialInventoryOuts)); }
        }

        #endregion

        #region Commands

        public RelayCommand GoToMaterialInventoryIn { get; set; }
        #endregion



    }
}
