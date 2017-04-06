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
    public class MaterialNeedVM : BaseViewModelPage
    {
        public MaterialNeedVM()
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
            MainMaterialNeeds = new ObservableCollection<DcMaterialNeed>();
        }
        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);

            Add = new RelayCommand(ActionAdd);
            Edit = new RelayCommand<DcMaterialNeed>(ActionEdit);



        }

        private void ActionEdit(DcMaterialNeed model)
        {
            if (model != null)
            {
                PMSHelper.ViewModels.MaterialNeedEdit.SetEdit(model);
                NavigationService.GoTo(PMSViews.MaterialNeedEdit);
            }
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.MaterialNeedEdit.SetNew();
            NavigationService.GoTo(PMSViews.MaterialNeedEdit);
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
            var service = new MaterialNeedServiceClient();
            RecordCount = service.GetMaterialNeedCountBySearch(SearchCompositoinStandard);
            ActionPaging();
        }
        /// <summary>
        /// 分页动作的时候读入数据
        /// </summary>
        private void ActionPaging()
        {
            var service = new MaterialNeedServiceClient();
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var result = service.GetMaterialNeedBySearchInPage(skip, take, SearchCompositoinStandard);
            MainMaterialNeeds.Clear();
            result.ToList<DcMaterialNeed>().ForEach(o => MainMaterialNeeds.Add(o));
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





        private ObservableCollection<DcMaterialNeed> mainMaterialNeeds;
        public ObservableCollection<DcMaterialNeed> MainMaterialNeeds
        {
            get { return mainMaterialNeeds; }
            set { mainMaterialNeeds = value; RaisePropertyChanged(nameof(MainMaterialNeeds)); }
        }

        #endregion

        #region Commands
        public RelayCommand Add { get; private set; }
        public RelayCommand<DcMaterialNeed> Edit { get; private set; }
        #endregion



    }
}
