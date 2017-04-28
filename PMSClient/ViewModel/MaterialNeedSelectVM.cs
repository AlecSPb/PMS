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
using PMSClient.ViewModel.Model;

namespace PMSClient.ViewModel
{
    public class MaterialNeedSelectVM : BaseViewModelSelect
    {

        public MaterialNeedSelectVM()
        {
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }
        private PMSViews requestView;
        public void SetRequestView(PMSViews viewToken)
        {
            requestView = viewToken;
        }

        private void InitializeProperties()
        {
            SearchCompositoinStandard = "";
            MainMaterialNeedExtras = new ObservableCollection<MaterialNeedExtra>();
        }
        private void InitializeCommands()
        {
            GiveUp = new RelayCommand(() => NavigationService.GoTo(requestView));
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);
            Select = new RelayCommand<MaterialNeedExtra>(ActionSelect);
            SelectBatch = new RelayCommand(ActionSelectBatch);
        }

        private void ActionSelectBatch()
        {
            throw new NotImplementedException();
        }

        private void ActionSelect(MaterialNeedExtra need)
        {
            if (need != null)
            {
                switch (requestView)
                {
                    case PMSViews.MaterialOrderItemEdit:
                        PMSHelper.ViewModels.MaterialOrderItemEdit.SetBySelect(need.MaterialNeed);
                        break;
                    default:
                        break;
                }
                NavigationService.GoTo(requestView);
            }
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
            var service = new MaterialNeedServiceClient();
            var result = service.GetMaterialNeedBySearchInPage(skip, take, SearchCompositoinStandard);
            service.Close();
            MainMaterialNeedExtras.Clear();
            result.ToList().ForEach(o => MainMaterialNeedExtras.Add(new MaterialNeedExtra { MaterialNeed =o}));
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


        private ObservableCollection<MaterialNeedExtra> mainMaterialNeeds;
        public ObservableCollection<MaterialNeedExtra> MainMaterialNeedExtras
        {
            get { return mainMaterialNeeds; }
            set { mainMaterialNeeds = value; RaisePropertyChanged(nameof(MainMaterialNeedExtras)); }
        }

        #endregion

        #region Commands
        public RelayCommand<MaterialNeedExtra> Select { get; private set; }
        public RelayCommand SelectBatch { get; set; }
        #endregion



    }
}
