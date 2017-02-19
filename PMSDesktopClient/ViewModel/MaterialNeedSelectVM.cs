using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSCommon;
using PMSDesktopClient.PMSMainService;
using System.Collections.ObjectModel;
using PMSDesktopClient.View;

namespace PMSDesktopClient.ViewModel
{
    public class MaterialNeedSelectVM : ViewModelBase
    {
        private bool isNew;
        private DcMaterialOrderItem item;
        public MaterialNeedSelectVM(ModelObject materialOrder)
        {
            isNew = materialOrder.IsNew;
            var order = materialOrder.Model as DcMaterialOrder;
            item = new PMSMainService.DcMaterialOrderItem();
            item.ID = Guid.NewGuid();
            item.MaterialOrderID = order.ID;
            item.State = PMSCommon.NoneOrderState.UnDeleted.ToString();
            item.Creator = (App.Current as App).CurrentUser.UserName;
            item.CreateTime = DateTime.Now;
            item.Composition = "Composition";
            item.PMIWorkNumber = "WorkNumber";
            item.Purity = "Purity";
            item.Description = "";
            item.ProvideRawMaterial = "";
            item.UnitPrice = 0;
            item.Weight = 0;
            item.DeliveryDate = DateTime.Now.AddDays(7);




            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }


        private void InitializeProperties()
        {
            SearchCompositoinStandard = "";
            MainMaterialNeeds = new ObservableCollection<DcMaterialNeed>();
        }
        private void InitializeCommands()
        {
            GiveUp = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VToken.MaterialOrder }));
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);
            Select = new RelayCommand<PMSMainService.DcMaterialNeed>(ActionSelect);



        }

        private void ActionSelect(DcMaterialNeed need)
        {
            if (need != null)
            {
                item.Composition = need.Composition;
                item.PMIWorkNumber = need.PMIWorkingNumber;
                item.Purity = need.Purity;
                item.Weight = need.Weight;

                MsgObject msg = new MsgObject();
                msg.MsgToken = VToken.MaterialOrderItemEdit;
                msg.MsgModel = new ModelObject() { IsNew = true, Model = item };
                NavigationService.GoTo(msg);
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


        #region PagingProperties
        private int pageIndex;
        public int PageIndex
        {
            get { return pageIndex; }
            set
            {
                pageIndex = value;
                RaisePropertyChanged(nameof(PageIndex));
            }
        }

        private int pageSize;
        public int PageSize
        {
            get { return pageSize; }
            set
            {
                pageSize = value;
                RaisePropertyChanged(nameof(PageSize));
            }
        }

        private int recordCount;
        public int RecordCount
        {
            get { return recordCount; }
            set
            {
                recordCount = value;
                RaisePropertyChanged(nameof(RecordCount));
            }
        }
        #endregion

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
        public RelayCommand GiveUp { get; private set; }
        public RelayCommand Search { get; private set; }
        public RelayCommand All { get; set; }
        public RelayCommand<DcMaterialNeed> Select { get; private set; }
        public RelayCommand PageChanged { get; private set; }
        #endregion



    }
}
