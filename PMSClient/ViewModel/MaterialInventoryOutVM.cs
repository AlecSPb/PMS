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
            SetPageParametersWhenConditionChange();
        }

        public override void Cleanup()
        {
            Messenger.Default.Unregister(this);
            base.Cleanup();
        }
        private void ActionRefresh(MsgObject obj)
        {
            SetPageParametersWhenConditionChange();
        }

        private void InitializeProperties()
        {
            SearchCompositoinStandard = "";
            MainMaterialNeeds = new ObservableCollection<DcMaterialInventoryIn>();
        }
        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);

            Add = new RelayCommand(ActionAdd);
            Edit = new RelayCommand<DcMaterialInventoryIn>(ActionEdit);



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
            return !(string.IsNullOrEmpty(SearchCompositoinStandard));
        }

        private void ActionAll()
        {
            SearchCompositoinStandard = "";
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
            RecordCount = service.GetMaterialInventoryCountBySearch(SearchCompositoinStandard);
            ActionPaging();
        }
        /// <summary>
        /// 分页动作的时候读入数据
        /// </summary>
        private void ActionPaging()
        {
            var service = new MaterialInventoryServiceClient();
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var result = service.GetMaterialInventoryIns(skip, take);
            MainMaterialNeeds.Clear();
            result.ToList<DcMaterialInventoryIn>().ForEach(o => MainMaterialNeeds.Add(o));
        }

        #region Proeperties
        private string searchCompositionStandard;
        public string SearchCompositoinStandard
        {
            get { return searchCompositionStandard; }
            set
            {
                if (searchCompositionStandard == value)
                    return;
                searchCompositionStandard = value;
                RaisePropertyChanged(() => SearchCompositoinStandard);
            }
        }





        private ObservableCollection<DcMaterialInventoryIn> mainMaterialNeeds;
        public ObservableCollection<DcMaterialInventoryIn> MainMaterialNeeds
        {
            get { return mainMaterialNeeds; }
            set { mainMaterialNeeds = value; RaisePropertyChanged(nameof(MainMaterialNeeds)); }
        }

        #endregion

        #region Commands
        public RelayCommand Add { get; private set; }
        public RelayCommand<DcMaterialInventoryIn> Edit { get; private set; }
        #endregion



    }
}
