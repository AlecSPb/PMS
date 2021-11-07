using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSQuotation.Models;
using PMSQuotation.Services;
using XSHelper;

namespace PMSQuotation
{
    public class MainWindowVM : ViewModelBase
    {
        public MainWindowVM()
        {
            db_service = new QuotationDbService();
            calc_service = new CalculationService();
            dict_service = new DataDictionaryService();

            searchCustomer = searchKeyword = "";

            ShowDeleted = false;

            statusBarInfo = "";

            Quotations = new ObservableCollection<Quotation>();
            CurrentQuotationItems = new ObservableCollection<QuotationItem>();

            #region Commands
            Search = new RelayCommand(ActionSearch);
            SelectionChanged = new RelayCommand<Quotation>(ActionSelectionChanged);

            New = new RelayCommand(ActionNew);
            Edit = new RelayCommand<Quotation>(ActionEdit);
            Clone = new RelayCommand<Quotation>(ActionClone);
            Doc = new RelayCommand<Quotation>(ActionDoc);
            ItemNew = new RelayCommand<Quotation>(ActionItemNew);
            Delete = new RelayCommand<Quotation>(ActionDelete);

            ItemEdit = new RelayCommand<QuotationItem>(ActionItemEdit);
            ItemDelete = new RelayCommand<QuotationItem>(ActionItemDelete);
            ItemClone = new RelayCommand<QuotationItem>(ActionItemClone);
            ShowUnitPriceDetail = new RelayCommand<QuotationItem>(ActionShowUnitPriceDetail);

            DBFolder = new RelayCommand(ActionDBFolder);
            DataDictionary = new RelayCommand(ActionDataDictionary);
            #endregion


            LoadQuotations();

            Messenger.Default.Register<NotificationMessage>(this, "MSG", ActionDo);
        }

        private QuotationDbService db_service;
        private CalculationService calc_service;
        private DataDictionaryService dict_service;

        private void ActionShowUnitPriceDetail(QuotationItem obj)
        {
            if (obj != null && !string.IsNullOrEmpty(obj.UnitPriceDetail))
            {
                var win = new Tools.UnitPriceCalculatorReadOnly();
                win.SetJson(obj.UnitPriceDetail);
                win.ShowDialog();
            }
        }

        private void ActionDataDictionary()
        {
            var win = new DataDictionaryView();
            var vm = new DataDictionaryVM();
            win.DataContext = vm;
            win.ShowDialog();


            //refresh CalculationCurrency
            CalculationCurrency = dict_service.GetString("basecurrency") ?? "None";
        }

        private void ActionDo(NotificationMessage obj)
        {
            if (obj.Notification == "RefreshMain")
            {
                LoadQuotations();
            }
        }


        private void ActionItemDelete(QuotationItem obj)
        {
            if (obj != null)
            {
                if (XSHelper.XS.MessageBox.ShowYesNo("Are you sure to delete?", "Ask"))
                {
                    obj.State = QuotationState.Deleted.ToString();
                    db_service.UpdateItem(obj);
                    LoadQuotations();
                }
            }
        }

        private void ActionItemClone(QuotationItem obj)
        {
            if (obj == null) return;
            var win = new QuotationItemEditView();
            var vm = new QuotationItemEditVM();
            vm.SetClone(obj);
            win.DataContext = vm;

            win.ShowDialog();
        }

        private void ActionItemEdit(QuotationItem obj)
        {
            if (obj == null) return;
            var win = new QuotationItemEditView();
            var vm = new QuotationItemEditVM();
            vm.SetEdit(obj);
            win.DataContext = vm;

            win.ShowDialog();
        }

        private void ActionItemNew(Quotation obj)
        {
            if (obj == null) return;
            var win = new QuotationItemEditView();
            var vm = new QuotationItemEditVM();
            vm.SetNew(obj.ID);
            win.DataContext = vm;

            win.ShowDialog();
        }

        private void ActionDoc(Quotation obj)
        {
            if (obj != null)
            {
                var dialog = new Docs.SelectDocTypeDialog();
                if (dialog.ShowDialog() == true)
                {
                    string selectedDocType = dialog.SelectedDocType;

                    var option = new Docs.DocOptions();
                    option.DocType = selectedDocType;
                    var doc_service = new Docs.DocService(obj, option);
                    doc_service.CreateDocument();
                }
            }
        }
        private void ActionDelete(Quotation obj)
        {
            if (obj != null)
            {
                if (XSHelper.XS.MessageBox.ShowYesNo("Are you sure to delete?", "Ask"))
                {
                    obj.LastUpdateTime = DateTime.Now;
                    obj.State = QuotationState.Deleted.ToString();
                    db_service.Update(obj);
                    LoadQuotations();
                }
            }
        }

        private void ActionClone(Quotation obj)
        {
            if (obj == null) return;

            QuotationEditView view = new QuotationEditView();
            var vm = new QuotationEditVM();
            vm.SetClone(obj);
            view.DataContext = vm;
            view.ShowDialog();
        }

        private void ActionEdit(Quotation obj)
        {
            if (obj == null) return;
            if (obj.State != "UnFinished" && !XS.MessageBox.ShowYesNo("are you sure to edit this record?"))
            {
                return;
            }

            QuotationEditView view = new QuotationEditView();
            var vm = new QuotationEditVM();
            vm.SetEdit(obj);
            view.DataContext = vm;
            view.ShowDialog();

        }

        private void ActionNew()
        {
            QuotationEditView view = new QuotationEditView();
            var vm = new QuotationEditVM();
            vm.SetNew();
            view.DataContext = vm;
            view.ShowDialog();
        }

        private void ActionDBFolder()
        {
            try
            {
                string dbpath = @"DB\";
                System.Diagnostics.Process.Start(dbpath);
            }
            catch (Exception)
            {

            }
        }

        private void ActionSelectionChanged(Quotation model)
        {
            if (model != null)
            {
                CurrentQuotation = model;
                LoadQuotationItems();
            }
        }

        private void ActionSearch()
        {
            LoadQuotations();
        }

        private string searchCustomer;

        public string SearchCustomer
        {
            get { return searchCustomer; }
            set { searchCustomer = value; RaisePropertyChanged(nameof(SearchCustomer)); }
        }

        private string searchKeyword;

        public string SearchKeyword
        {
            get { return searchKeyword; }
            set { searchKeyword = value; RaisePropertyChanged(nameof(SearchKeyword)); }
        }

        private bool showDeleted;

        public bool ShowDeleted
        {
            get { return showDeleted; }
            set { showDeleted = value; RaisePropertyChanged(nameof(ShowDeleted)); }
        }

        private string statusBarInfo;

        public string StatusBarInfo
        {
            get { return statusBarInfo; }
            set { statusBarInfo = value; RaisePropertyChanged(nameof(StatusBarInfo)); }
        }

        private string calculationCurrency;

        public string CalculationCurrency
        {
            get { return calculationCurrency; }
            set { calculationCurrency = value; RaisePropertyChanged(nameof(CalculationCurrency)); }
        }

        public ObservableCollection<Quotation> Quotations { get; set; }

        public ObservableCollection<QuotationItem> CurrentQuotationItems { get; set; }

        public void LoadQuotations()
        {
            var models = db_service.GetQuotations(SearchCustomer, SearchKeyword, ShowDeleted);

            Quotations.Clear();
            foreach (var item in models)
            {
                Quotations.Add(item);
            }

            if (Quotations.Count > 0)
            {
                CurrentQuotation = Quotations.FirstOrDefault();
                LoadQuotationItems();
            }

            //refresh CalculationCurrency
            CalculationCurrency = dict_service.GetString("basecurrency") ?? "None";
        }

        public void LoadQuotationItems()
        {
            if (CurrentQuotation != null)
            {
                var models = db_service.GetQuotationItems(CurrentQuotation.ID, ShowDeleted);
                CurrentQuotationItems.Clear();
                foreach (var item in models)
                {
                    CurrentQuotationItems.Add(item);
                }
                //calculate all fee
                CalculateFee();
            }
        }


        private void CalculateFee()
        {
            var result = calc_service.Calculate(CurrentQuotation);

            string currencyType = "";
            if (CurrentQuotation.CurrencyType == "RMB")
            {
                currencyType = "￥";
            }
            else if (CurrentQuotation.CurrencyType == "USD")
            {
                currencyType = "$";
            }

            StatusBarInfo = $"Total={currencyType}{(result.TargetFee + result.ExtraFee + result.TaxFee).ToString("F2")} " +
                $"Target={currencyType}{result.TargetFee.ToString("F2")} " +
                $"Extra={currencyType}{result.ExtraFee.ToString("F2")} " +
                $"Tax={currencyType}{result.TaxFee.ToString("F2")}";
        }



        public Quotation CurrentQuotation { get; set; }


        public RelayCommand Search { get; set; }

        public RelayCommand New { get; set; }
        public RelayCommand<Quotation> Edit { get; set; }
        public RelayCommand<Quotation> Delete { get; set; }
        public RelayCommand<Quotation> Clone { get; set; }
        public RelayCommand<Quotation> Doc { get; set; }

        public RelayCommand<Quotation> ItemNew { get; set; }

        public RelayCommand<QuotationItem> ItemEdit { get; set; }
        public RelayCommand<QuotationItem> ItemDelete { get; set; }
        public RelayCommand<QuotationItem> ItemClone { get; set; }
        public RelayCommand<QuotationItem> ShowUnitPriceDetail { get; set; }

        public RelayCommand<Quotation> SelectionChanged { get; set; }

        public RelayCommand DataDictionary { get; set; }

        public RelayCommand DBFolder { get; set; }
    }
}
