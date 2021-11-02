using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSQuotation.Models;
using PMSQuotation.Services;
using XSHelper;

namespace PMSQuotation
{
    public class MainWindowVM : ViewModelBase
    {
        public MainWindowVM()
        {
            searchCustomer = searchKeyword = "";

            SearchStates = new List<string>();
            SearchStates.Add("");
            foreach (var item in Enum.GetValues(typeof(ModelState)))
            {
                SearchStates.Add(item.ToString());
            }
            SearchState = ModelState.Finished.ToString();

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


            DBFolder = new RelayCommand(ActionDBFolder);
            #endregion


            LoadQuotations();
        }

        private void ActionDelete(Quotation obj)
        {
            throw new NotImplementedException();
        }

        private void ActionItemClone(QuotationItem obj)
        {
            throw new NotImplementedException();
        }

        private void ActionItemDelete(QuotationItem obj)
        {
            throw new NotImplementedException();
        }

        private void ActionItemEdit(QuotationItem obj)
        {
            throw new NotImplementedException();
        }

        private void ActionItemNew(Quotation obj)
        {
            throw new NotImplementedException();
        }

        private void ActionDoc(Quotation obj)
        {
            throw new NotImplementedException();
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

        public string SearchState { get; set; }

        public List<string> SearchStates { get; set; }

        public ObservableCollection<Quotation> Quotations { get; set; }

        public ObservableCollection<QuotationItem> CurrentQuotationItems { get; set; }

        private QuotationDbService service = new QuotationDbService();

        public void LoadQuotations()
        {
            var models = service.GetQuotations(SearchCustomer, SearchKeyword, SearchState);

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
        }

        public void LoadQuotationItems()
        {
            if (CurrentQuotation != null)
            {
                var models = service.GetQuotationItems(CurrentQuotation.ID);
                CurrentQuotationItems.Clear();
                foreach (var item in models)
                {
                    CurrentQuotationItems.Add(item);
                }
            }
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

        public RelayCommand<Quotation> SelectionChanged { get; set; }



        public RelayCommand DBFolder { get; set; }
    }
}
