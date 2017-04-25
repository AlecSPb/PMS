using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSClient.MainService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.ViewModel
{
    public class MaterialInventoryInSelectVM : BaseViewModelSelect
    {
        public MaterialInventoryInSelectVM()
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
            MaterialInventoryIns = new ObservableCollection<DcMaterialInventoryIn>();
        }
        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);
            GiveUp = new RelayCommand(GoBack);
            Select = new RelayCommand<DcMaterialInventoryIn>(ActionSelect);

        }

        private void GoBack()
        {
            NavigationService.GoTo(requestView);
        }

        private PMSViews requestView;
        public void SetRequestView(PMSViews view)
        {
            requestView = view;
        }

        private void ActionSelect(DcMaterialInventoryIn model)
        {
            if (model != null)
            {
                switch (requestView)
                {
                    case PMSViews.MaterialInventoryOutEdit:
                        PMSHelper.ViewModels.MaterialInventoryOutEdit.SetBySelect(model);
                        break;
                    default:
                        break;
                }
                GoBack();
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
            var service = new MaterialInventoryServiceClient();
            //TODO:完成搜索
            RecordCount = service.GetMaterialInventoryInCount();
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
            var result = service.GetMaterialInventoryIns(skip, take);
            service.Close();
            MaterialInventoryIns.Clear();
            result.ToList().ForEach(o => MaterialInventoryIns.Add(o));
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

        public RelayCommand<DcMaterialInventoryIn> Select { get; set; }
        #endregion




    }
}
