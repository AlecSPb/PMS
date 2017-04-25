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

        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }

        private void InitializeProperties()
        {
            SearchCompositoinStandard = "";
            MaterialInventoryOuts = new ObservableCollection<DcMaterialInventoryOut>();
        }
        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);

            Add = new RelayCommand(ActionAdd,CanAdd);
            Edit = new RelayCommand<DcMaterialInventoryOut>(ActionEdit,CanEdit);

            GoToMaterialInventoryIn = new RelayCommand(() => NavigationService.GoTo(PMSViews.MaterialInventoryIn),
                ()=>PMSHelper.CurrentSession.IsAuthorized("浏览原料库存"));

        }

        private bool CanEdit(DcMaterialInventoryOut arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑原料库存");
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑原料库存");
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
            RecordCount = service.GetMaterialInventoryOutCount();
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
            var result = service.GetMaterialInventoryOuts(skip, take);
            service.Close();
            MaterialInventoryOuts.Clear();
            result.ToList().ForEach(o => MaterialInventoryOuts.Add(o));
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





        private ObservableCollection<DcMaterialInventoryOut> materialInventoryOuts;
        public ObservableCollection<DcMaterialInventoryOut> MaterialInventoryOuts
        {
            get { return materialInventoryOuts; }
            set { materialInventoryOuts = value; RaisePropertyChanged(nameof(MaterialInventoryOuts)); }
        }

        #endregion

        #region Commands
        public RelayCommand Add { get; private set; }
        public RelayCommand<DcMaterialInventoryOut> Edit { get; private set; }

        public RelayCommand GoToMaterialInventoryIn { get; set; }
        #endregion



    }
}
