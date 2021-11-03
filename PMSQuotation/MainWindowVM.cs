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

            searchCustomer = searchKeyword = "";

            ShowDeleted = false;

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

            Messenger.Default.Register<NotificationMessage>(this, "MSG", ActionDo);
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
            throw new NotImplementedException();
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


        public ObservableCollection<Quotation> Quotations { get; set; }

        public ObservableCollection<QuotationItem> CurrentQuotationItems { get; set; }

        private QuotationDbService db_service;

        public void LoadQuotations()
        {
            var models = db_service.GetQuotations(SearchCustomer, SearchKeyword,ShowDeleted);

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
                var models = db_service.GetQuotationItems(CurrentQuotation.ID);
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
