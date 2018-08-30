using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using PMSClient.ViewModel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.ViewModel
{
    public class RecordBondingSelectVM : BaseViewModelSelect
    {
        public RecordBondingSelectVM()
        {
            RecordBondingExtras = new ObservableCollection<RecordBondingExtra>();
            InitializeCommands();
            searchCompositionStd = searchProductID = "";
            SetPageParametersWhenConditionChange();
        }

        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }
        private void InitializeCommands()
        {
            Select = new RelayCommand<RecordBondingExtra>(ActionSelect);
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
            GiveUp = new RelayCommand(GoBack);
            PageChanged = new RelayCommand(ActionPaging);
            SelectBatch = new RelayCommand(ActionSelectBatch);
        }

        private void ActionSelectBatch()
        {
            var count = RecordBondingExtras.Where(i => i.IsSelected).Count();
            if (!PMSDialogService.ShowYesNo("请问", "确定要批量添加选中的记录吗？"))
            {
                return;
            }
            switch (requestView)
            {
                case PMSViews.ProductEdit:
                    BatchSaveProduct();
                    NavigationService.GoTo(PMSViews.Product);
                    break;
                default:
                    break;
            }
            PMSDialogService.Show("请问", "请刷新列表");
        }

        private void BatchSaveProduct()
        {
            try
            {
                using (var service = new ProductServiceClient())
                {
                    RecordBondingExtras.ToList().ForEach(i =>
                    {
                        if (i.IsSelected)
                        {
                            var temp = PMSNewModelCollection.NewProduct();
                            temp.ProductType = PMSCommon.ProductType.绑定.ToString();
                            temp.ProductID = i.RecordBonding.TargetProductID;
                            temp.Customer = i.RecordBonding.TargetCustomer;
                            temp.Composition = i.RecordBonding.TargetComposition;
                            temp.Abbr = i.RecordBonding.TargetAbbr;
                            temp.PO = i.RecordBonding.TargetPO;
                            temp.Weight = i.RecordBonding.TargetWeight;
                            temp.Dimension = i.RecordBonding.TargetDimension;
                            temp.DimensionActual = i.RecordBonding.TargetDimensionActual;
                            temp.Defects = i.RecordBonding.TargetDefects;
                            temp.Remark = $"背板编号:{i.RecordBonding.PlateLot}";

                            service.AddProduct(temp);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private PMSViews requestView;
        public void SetRequestView(PMSViews view)
        {
            requestView = view;
        }

        private void GoBack()
        {
            NavigationService.GoTo(requestView);
        }
        private void ActionSelect(RecordBondingExtra model)
        {
            if (model != null)
            {
                switch (requestView)
                {
                    case PMSViews.ProductEdit:
                        PMSHelper.ViewModels.ProductEdit.SetBySelect(model.RecordBonding);
                        break;
                    default:
                        break;
                }
                GoBack();
            }
        }

        private void ActionAll()
        {
            SearchProductID = SearchCompositionStd = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 30;
            using (var service = new RecordBondingServiceClient())
            {
                RecordCount = service.GetRecordBondingCount(SearchProductID, SearchCompositionStd);
            }

            ActionPaging();
        }
        private void ActionPaging()
        {
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;

            using (var service = new RecordBondingServiceClient())
            {
                var orders = service.GetRecordBondings(skip, take, SearchProductID, SearchCompositionStd);
                RecordBondingExtras.Clear();
                orders.ToList().ForEach(o => RecordBondingExtras.Add(new Model.RecordBondingExtra { RecordBonding = o }));
            }
        }


        public ObservableCollection<RecordBondingExtra> RecordBondingExtras { get; set; }

        private string searchCompositionStd;
        public string SearchCompositionStd
        {
            get { return searchCompositionStd; }
            set { searchCompositionStd = value; RaisePropertyChanged(nameof(SearchCompositionStd)); }
        }

        private string searchProductID;
        public string SearchProductID
        {
            get { return searchProductID; }
            set { searchProductID = value; RaisePropertyChanged(nameof(SearchProductID)); }
        }


        public RelayCommand<RecordBondingExtra> Select { get; set; }

        public RelayCommand SelectBatch { get; set; }
    }
}
