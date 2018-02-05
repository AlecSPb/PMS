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
    public class MaterialInventoryOutVM : BaseViewModelPage
    {
        public MaterialInventoryOutVM()
        {
            InitializeProperties();
            InitializeCommands();
            DoAllGetJob();
        }
        /// <summary>
        /// 综合查询
        /// </summary>
        /// <param name="pminumber"></param>
        public void SetSearch(string pminumber)
        {
            SearchComposition = "";
            SearchMaterialLot = "";
            SearchPMINumber = pminumber;
            SearchReceiver = "";
            SetPageParametersWhenConditionChange();
        }

        public void RefreshData()
        {
            DoAllGetJob();
        }

        private void InitializeProperties()
        {
            searchComposition = searchMaterialLot = searchPMINumber = searchReceiver = "";
            MaterialInventoryOuts = new ObservableCollection<DcMaterialInventoryOut>();
            NewMaterialUsedList = new ObservableCollection<DcRecordMilling>();
        }
        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);

            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<DcMaterialInventoryOut>(ActionEdit, CanEdit);

            GoToMaterialInventoryIn = new RelayCommand(() => NavigationService.GoTo(PMSViews.MaterialInventoryIn),
                () => PMSHelper.CurrentSession.IsAuthorized(PMSAccess.ReadMaterialInventoryOut));

        }

        private bool CanEdit(DcMaterialInventoryOut arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditMaterialInventoryOut);
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditMaterialInventoryOut);
        }

        private void ActionEdit(DcMaterialInventoryOut model)
        {
            if (model != null)
            {
                PMSHelper.ViewModels.MaterialInventoryOutEdit.SetEdit(model);
                NavigationService.GoTo(PMSViews.MaterialInventoryOutEdit);
            }
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.MaterialInventoryOutEdit.SetNew();
            NavigationService.GoTo(PMSViews.MaterialInventoryOutEdit);
        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchComposition) && string.IsNullOrEmpty(SearchMaterialLot)
                && string.IsNullOrEmpty(SearchPMINumber) && string.IsNullOrEmpty(SearchReceiver));
        }

        private void ActionAll()
        {
            SearchComposition = SearchMaterialLot = SearchPMINumber = SearchReceiver = "";
            DoAllGetJob();
        }

        private void ActionSearch()
        {
            DoAllGetJob();
        }

        private void DoAllGetJob()
        {
            SetPageParametersWhenConditionChange();
            GetNewMaterialUsed();
        }

        private void GetNewMaterialUsed()
        {
            try
            {
                using (var service = new RecordMillingServiceClient())
                {
                    string materialType = PMSCommon.CustomData.MaterialTypes[4];
                    int take = 20;
                    var tempStored = service.GetRecordMillingByMaterialType(materialType, take);
                    NewMaterialUsedList.Clear();
                    tempStored.ToList().ForEach(i => NewMaterialUsedList.Add(i));
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 20;
            var service = new MaterialInventoryServiceClient();
            RecordCount = service.GetMaterialInventoryOutCountBySearch(SearchReceiver, SearchComposition,
                SearchMaterialLot, SearchPMINumber);
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
            var result = service.GetMaterialInventoryOutsBySearch(skip, take, SearchReceiver, SearchComposition,
                SearchMaterialLot, SearchPMINumber);
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

        private string searchReceiver;
        public string SearchReceiver
        {
            get { return searchReceiver; }
            set { searchReceiver = value; RaisePropertyChanged(nameof(SearchReceiver)); }
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

        private ObservableCollection<DcRecordMilling> newMaterialUsedList;

        public ObservableCollection<DcRecordMilling> NewMaterialUsedList
        {
            get { return newMaterialUsedList; }
            set { newMaterialUsedList = value; }
        }

        #endregion

        #region Commands
        public RelayCommand Add { get; private set; }
        public RelayCommand<DcMaterialInventoryOut> Edit { get; private set; }

        public RelayCommand GoToMaterialInventoryIn { get; set; }
        #endregion



    }
}
