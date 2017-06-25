using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using System.Collections.ObjectModel;
using PMSClient.Tool;

namespace PMSClient.ViewModel
{
    public class OutSourceSelectVM : BaseViewModelSelect
    {
        public OutSourceSelectVM()
        {
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }



        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }

        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            Select = new RelayCommand<DcOutSource>(ActionSelect);
            All = new RelayCommand(ActionAll);
            GiveUp = new RelayCommand(ActionGiveUp);
        }

        private void ActionGiveUp()
        {
            NavigationService.GoTo(RequestView);
        }

        public PMSViews RequestView { get; set; }

        private void ActionSelect(DcOutSource model)
        {
            switch (RequestView)
            {
                case PMSViews.ProductEdit:
                    BatchProduct(model);
                    break;
                default:
                    break;
            }
        }

        private void BatchProduct(DcOutSource para)
        {
            var window = new BatchOutSourceProduct();
            bool? result = window.ShowDialog();
            if (result == true)
            {
                string first = window.txtFirst.Text;
                string mid = window.txtMid.Text;
                int count = int.Parse(window.cboLast.SelectedItem.ToString());
                using (var service = new ProductServiceClient())
                {
                    for (int i = 0; i < count; i++)
                    {
                        var model = new DcProduct();
                        model.ID = Guid.NewGuid();
                        model.ProductID = $"{first}-{mid}-{i + 1}";
                        model.CreateTime = DateTime.Now;
                        model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                        model.Composition = para.OrderName;
                        model.Abbr = para.OrderName;
                        model.Weight = "无";
                        model.PO = "无";
                        model.Customer = "无";
                        model.Position = PMSCommon.GoodPosition.A1.ToString();
                        model.ProductType = PMSCommon.ProductType.靶材.ToString();
                        model.State = PMSCommon.InventoryState.库存.ToString();
                        model.Remark = "";

                        model.Dimension = para.Dimension;
                        model.DimensionActual = para.Dimension;
                        model.Defects = "无";

                        service.AddProductByUID(model, PMSHelper.CurrentSession.CurrentUser.UserName);
                    }
                }
            }
            NavigationService.GoTo(PMSViews.Product);
        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchOrderLot) && string.IsNullOrEmpty(SearchOrderName) && string.IsNullOrEmpty(SearchSupplier));
        }

        private void ActionAll()
        {
            searchOrderLot = searchOrderName = searchSupplier = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void InitializeProperties()
        {
            OutSources = new ObservableCollection<DcOutSource>();
            searchOrderLot = searchOrderName = searchSupplier = "";

        }
        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 20;
            using (var service = new OutSourceServiceClient())
            {
                RecordCount = service.GetOutSourcesCount(SearchOrderLot, SearchOrderName, SearchSupplier);
            }
            ActionPaging();
        }
        private void ActionPaging()
        {
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            using (var service = new OutSourceServiceClient())
            {
                var orders = service.GetOutSources(skip, take, SearchOrderLot, SearchOrderName, SearchSupplier);
                OutSources.Clear();
                orders.ToList().ForEach(o => OutSources.Add(o));
            }
        }
        #region Commands
        private string searchOrderLot;
        public string SearchOrderLot
        {
            get { return searchOrderLot; }
            set
            {
                if (searchOrderLot == value)
                    return;
                searchOrderLot = value;
                RaisePropertyChanged(() => SearchOrderLot);
            }
        }
        private string searchOrderName;
        public string SearchOrderName
        {
            get { return searchOrderName; }
            set
            {
                if (searchOrderName == value)
                    return;
                searchOrderName = value;
                RaisePropertyChanged(() => SearchOrderName);
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

        public ObservableCollection<DcOutSource> OutSources { get; set; }
        #endregion
        public RelayCommand<DcOutSource> Select { get; set; }
    }
}
