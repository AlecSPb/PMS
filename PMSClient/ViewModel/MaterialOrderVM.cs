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
using DocGenerator;
using gn = DocGenerator.DocModels;
using AutoMapper;
using System.Windows;
using PMSClient;

namespace PMSClient.ViewModel
{
    public class MaterialOrderVM : BaseViewModelPage
    {
        public MaterialOrderVM()
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
        private void InitializeProperties()
        {
            SearchOrderPO = "";
            SearchSupplier = "";
            MaterialOrders = new ObservableCollection<DcMaterialOrder>();
            MaterialOrderItems = new ObservableCollection<DcMaterialOrderItem>();
        }
        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);
            Doc = new RelayCommand<MainService.DcMaterialOrder>(ActionGenerateDoc);

            Add = new RelayCommand(ActionAdd);
            Edit = new RelayCommand<MainService.DcMaterialOrder>(ActionEdit);

            AddItem = new RelayCommand<MainService.DcMaterialOrder>(ActionAddItem);
            EditItem = new RelayCommand<MainService.DcMaterialOrderItem>(ActionEditItem);

            SelectionChanged = new RelayCommand<MainService.DcMaterialOrder>(ActionSelectionChanged);

        }

        private void ActionSelectionChanged(DcMaterialOrder obj)
        {
            if (obj != null)
            {
                using (var service = new MaterialOrderServiceClient())
                {
                    var result = service.GetMaterialOrderItembyMaterialID(obj.ID);
                    MaterialOrderItems.Clear();
                    result.ToList().ForEach(i => MaterialOrderItems.Add(i));
                    CurrentSelectItem = obj;
                }
            }
        }

        private void ActionEditItem(DcMaterialOrderItem item)
        {
            if (item != null)
            {
                PMSHelper.ViewModels.MaterialOrderItemEdit.SetEdit(item);
                NavigationService.GoTo(PMSViews.MaterialOrderItemEdit);
            }
        }

        private void ActionAddItem(DcMaterialOrder order)
        {
            if (order != null)
            {
                PMSHelper.ViewModels.MaterialOrderItemEdit.SetNew(order);
                NavigationService.GoTo(PMSViews.MaterialOrderItemEdit);
            }
        }

        private void ActionEdit(DcMaterialOrder order)
        {
            if (order != null)
            {
                PMSHelper.ViewModels.MaterialOrderEdit.SetEdit(order);
                NavigationService.GoTo(PMSViews.MaterialOrderEdit);
            }
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.MaterialOrderEdit.SetNew();
            NavigationService.GoTo(PMSViews.MaterialOrderEdit);
        }

        private void ActionGenerateDoc(DcMaterialOrder args)
        {
            if (MessageBox.Show("Do you want to create doc in desktop?", "Ask",
                MessageBoxButton.YesNo, MessageBoxImage.Information)
                == MessageBoxResult.No)
            {
                return;
            }

            if (args != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<DcMaterialOrder, gn.MaterialOrder>();
                    cfg.CreateMap<DcMaterialOrderItem, gn.MaterialOrderItem>();
                });
                var mapper = config.CreateMapper();

                var readyModel = mapper.Map<DcMaterialOrder, gn.MaterialOrder>(args);
                gn.MaterialOrder model = readyModel;

                var mainGenerator = new GeneralGenerator();
                IDoc<gn.MaterialOrder> generator = new GeneratorMaterialOrder();
                string source = nameof(DocTemplateEnum.MaterialOrder);
                string target = model.OrderPO;
                mainGenerator.Generate<gn.MaterialOrder>(generator, model, source, "PO" + target);

                MessageBox.Show("File At:" + mainGenerator.TargetFolder, "Doc has been Created");
            }
        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchOrderPO) && string.IsNullOrEmpty(SearchSupplier));
        }

        private void ActionAll()
        {
            SearchOrderPO = "";
            SearchSupplier = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 10;
            var service = new MaterialOrderServiceClient();
            RecordCount = service.GetMaterialOrderCountBySearch(SearchOrderPO, SearchSupplier);
            ActionPaging();
        }
        /// <summary>
        /// 分页动作的时候读入数据
        /// </summary>
        private void ActionPaging()
        {
            var service = new MaterialOrderServiceClient();
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var orders = service.GetMaterialOrderBySearchInPage(skip, take, SearchOrderPO, SearchSupplier);
            MaterialOrders.Clear();
            orders.ToList<DcMaterialOrder>().ForEach(o => MaterialOrders.Add(o));

            CurrentSelectIndex = 0;
            CurrentSelectItem = MaterialOrders.FirstOrDefault();
            ActionSelectionChanged(CurrentSelectItem);
        }


        #region Proeperties
        private string searchOrderPO;
        public string SearchOrderPO
        {
            get { return searchOrderPO; }
            set
            {
                if (searchOrderPO == value)
                    return;
                searchOrderPO = value;
                RaisePropertyChanged(() => SearchOrderPO);
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

        public ObservableCollection<DcMaterialOrder> MaterialOrders { get; set; }
        public ObservableCollection<DcMaterialOrderItem> MaterialOrderItems { get; set; }
        private int currrentSelectIndex;
        public int CurrentSelectIndex
        {
            get { return currrentSelectIndex; }
            set { currrentSelectIndex = value; RaisePropertyChanged(nameof(CurrentSelectIndex)); }
        }

        private DcMaterialOrder currentSelectItem;
        public DcMaterialOrder CurrentSelectItem
        {
            get { return currentSelectItem; }
            set { currentSelectItem = value; RaisePropertyChanged(nameof(CurrentSelectItem)); }
        }


        #endregion

        #region Commands
        public RelayCommand Add { get; private set; }
        public RelayCommand<DcMaterialOrder> Edit { get; set; }
        public RelayCommand<DcMaterialOrder> Doc { get; private set; }
        public RelayCommand<DcMaterialOrder> Refresh { get; set; }
        public RelayCommand<DcMaterialOrder> SelectionChanged { get; set; }

        public RelayCommand<DcMaterialOrder> AddItem { get; private set; }
        public RelayCommand<DcMaterialOrderItem> EditItem { get; private set; }
        #endregion
    }
}
