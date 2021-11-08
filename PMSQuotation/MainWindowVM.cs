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
using AutoMapper;
using Newtonsoft.Json;

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
            FromJson = new RelayCommand(ActionFromJson);

            Edit = new RelayCommand<Quotation>(ActionEdit);
            Clone = new RelayCommand<Quotation>(ActionClone);
            DeepClone = new RelayCommand<Quotation>(ActionDeepClone);
            ChangeCurrency = new RelayCommand<Quotation>(ActionChangeCurrency);
            Doc = new RelayCommand<Quotation>(ActionDoc);
            ToJson = new RelayCommand<Quotation>(ActionToJson);
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

        private void ActionChangeCurrency(Quotation obj)
        {
            if (obj == null) return;
            if (!XS.MessageBox.ShowYesNo("are you going to change the currency type?")) return;

            try
            {
                double currency_rate = dict_service.GetDouble("rmb_usd_exchange_rate");
                if (currency_rate <= 0) return;

                if (obj.CurrencyType == "RMB")
                {
                    if (!XS.MessageBox.ShowYesNo("will change to USD,go on?")) return;

                    obj.CurrencyType = "USD";
                    #region change_to_usd
                    obj.TotalCost = obj.TotalCost / currency_rate;
                    obj.PackageFee = obj.PackageFee / currency_rate;
                    obj.ShippingFee = obj.ShippingFee / currency_rate;
                    obj.CustomFee = obj.CustomFee / currency_rate;
                    obj.TaxFee = obj.TaxFee / currency_rate;

                    obj.LastUpdateTime = DateTime.Now;
                    obj.Remark = $"currency is changed to {obj.CurrencyType} at {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")};";
                    db_service.Update(obj);

                    var items = db_service.GetQuotationItems(obj.ID, false);
                    foreach (var item in items)
                    {
                        item.UnitPrice = item.UnitPrice / currency_rate;
                        item.TotalPrice = item.TotalPrice / currency_rate;

                        db_service.UpdateItem(item);
                    }
                    #endregion
                }
                else
                {
                    if (!XS.MessageBox.ShowYesNo("will change to RMB,go on?")) return;

                    obj.CurrencyType = "RMB";
                    #region change_to_usd
                    obj.TotalCost = obj.TotalCost * currency_rate;
                    obj.PackageFee = obj.PackageFee * currency_rate;
                    obj.ShippingFee = obj.ShippingFee * currency_rate;
                    obj.CustomFee = obj.CustomFee * currency_rate;
                    obj.TaxFee = obj.TaxFee * currency_rate;

                    obj.LastUpdateTime = DateTime.Now;
                    db_service.Update(obj);

                    var items = db_service.GetQuotationItems(obj.ID, false);
                    foreach (var item in items)
                    {
                        item.UnitPrice = item.UnitPrice * currency_rate;
                        item.TotalPrice = item.TotalPrice * currency_rate;

                        item.Note += $"currency is changed to {obj.CurrencyType} at {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")};";

                        db_service.UpdateItem(item);
                    }
                    #endregion
                }

                LoadQuotations();

                XS.MessageBox.ShowInfo("change currency type success");
            }
            catch (Exception ex)
            {
                XS.MessageBox.ShowError(ex.Message);
            }

        }

        private void ActionFromJson()
        {
            try
            {
                var dialog_result = XS.Dialog.ShowOpenDialog(XS.File.GetDesktopPath(), "(*.json)|*.json");
                if (dialog_result.HasSelected)
                {
                    string s_json = XS.File.ReadText(dialog_result.SelectPath);
                    QuotationJsonModel model = JsonConvert.DeserializeObject<QuotationJsonModel>(s_json);
                    if (model != null)
                    {
                        if (model.Quotation != null && model.QuotationItems != null)
                        {
                            #region Import
                            Quotation new_obj = model.Quotation;
                            new_obj.CreateTime = DateTime.Now;
                            new_obj.LastUpdateTime = DateTime.Now;
                            new_obj.ExpirationTime = DateTime.Now.AddMonths(1);
                            new_obj.State = QuotationState.UnFinished.ToString();
                            new_obj.Creator = dict_service.GetString("creator");
                            db_service.Add(new_obj);

                            var new_quotation = db_service.GetQuotationLastestCreateTime();
                            var items = model.QuotationItems;
                            foreach (var item in items)
                            {
                                var new_item = item;
                                new_item.QuotationID = new_quotation.ID;
                                new_item.CreateTime = DateTime.Now;
                                new_item.Creator = dict_service.GetString("creator");
                                new_item.State = QuotationItemState.Checked.ToString();
                                db_service.AddItem(new_item);
                            }
                            #endregion
                            XS.MessageBox.ShowInfo("import json success");
                            LoadQuotations();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XS.MessageBox.ShowError(ex.Message);
            }
        }

        private void ActionToJson(Quotation obj)
        {
            if (obj == null) return;
            if (!XS.MessageBox.ShowYesNo("export json file?")) return;

            try
            {
                QuotationJsonModel model = new QuotationJsonModel();
                model.Quotation = obj;
                model.QuotationItems = db_service.GetQuotationItems(obj.ID, false);

                string s_json = JsonConvert.SerializeObject(model);
                var dialog_result = XS.Dialog.ShowSaveDialog(XS.File.GetDesktopPath(), "(*.json)|*.json",
                    $"PMI{DateTime.Now.ToString("yyyyMMddHHmmss")}.json");
                if (dialog_result.HasSelected)
                {
                    XS.File.SaveText(dialog_result.SelectPath, s_json);
                }
                XS.MessageBox.ShowInfo("export json success");
                LoadQuotations();
            }
            catch (Exception ex)
            {
                XS.MessageBox.ShowError(ex.Message);
            }
        }

        private void ActionDeepClone(Quotation obj)
        {
            if (obj == null) return;
            if (!XS.MessageBox.ShowYesNo("Are you sure to deep clone this quotation?")) return;
            try
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Quotation, Quotation>();
                    cfg.CreateMap<QuotationItem, QuotationItem>();
                });

                Quotation new_obj = Mapper.Map<Quotation>(obj);
                new_obj.CreateTime = DateTime.Now;
                new_obj.LastUpdateTime = DateTime.Now;
                new_obj.ExpirationTime = DateTime.Now.AddMonths(1);
                new_obj.State = QuotationState.UnFinished.ToString();
                new_obj.Creator = dict_service.GetString("creator");
                db_service.Add(new_obj);

                var new_quotation = db_service.GetQuotationLastestCreateTime();
                var items = db_service.GetQuotationItems(obj.ID, false);
                foreach (var item in items)
                {
                    var new_item = Mapper.Map<QuotationItem>(item);
                    new_item.QuotationID = new_quotation.ID;
                    new_item.CreateTime = DateTime.Now;
                    new_item.Creator = dict_service.GetString("creator");
                    new_item.State = QuotationItemState.Checked.ToString();
                    db_service.AddItem(new_item);
                }

                XS.MessageBox.ShowInfo("Deep Copy Success");
                LoadQuotations();
            }
            catch (Exception ex)
            {
                XS.MessageBox.ShowError(ex.Message);
            }

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
        public RelayCommand FromJson { get; set; }
        public RelayCommand<Quotation> Edit { get; set; }
        public RelayCommand<Quotation> Delete { get; set; }
        public RelayCommand<Quotation> Clone { get; set; }
        public RelayCommand<Quotation> DeepClone { get; set; }
        public RelayCommand<Quotation> Doc { get; set; }
        public RelayCommand<Quotation> ChangeCurrency { get; set; }
        public RelayCommand<Quotation> ToJson { get; set; }

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
