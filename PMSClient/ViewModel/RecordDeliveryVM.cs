using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using PMSClient.MainService;
using GalaSoft.MvvmLight.Messaging;
using bt = BarTender;



namespace PMSClient.ViewModel
{
    public class RecordDeliveryVM : BaseViewModelPage
    {
        public RecordDeliveryVM()
        {
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }

        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }


        public void RefreshDataItem()
        {
            ActionSelectionChanged(CurrentSelectItem);
        }
        private void InitializeProperties()
        {
            RecordDeliveries = new ObservableCollection<DcRecordDelivery>();
            RecordDeliveryItems = new ObservableCollection<DcRecordDeliveryItem>();
        }
        private void InitializeCommands()
        {
            All = new RelayCommand(ActionAll);
            PageChanged = new RelayCommand(ActionPaging);
            Add = new RelayCommand(ActionAdd);
            Edit = new RelayCommand<DcRecordDelivery>(ActionEdit);
            Doc = new RelayCommand<DcRecordDelivery>(ActionDoc);
            AddItem = new RelayCommand<DcRecordDelivery>(ActionAddItem);
            EditItem = new RelayCommand<DcRecordDeliveryItem>(ActionEditItem);
            SelectionChanged = new RelayCommand<DcRecordDelivery>(ActionSelectionChanged);
        }

        private void ActionAll()
        {
            SetPageParametersWhenConditionChange();
        }

        private void ActionSelectionChanged(DcRecordDelivery model)
        {
            if (model != null)
            {
                using (var service = new RecordDeliveryServiceClient())
                {
                    var result = service.GetRecordDeliveryItemByRecordDeliveryID(model.ID);
                    RecordDeliveryItems.Clear();
                    result.ToList().ForEach(i => RecordDeliveryItems.Add(i));

                    CurrentSelectItem = model;
                }
            }
        }

        private bt.Application btApp;
        private bt.Format btnFormat;
        private void ActionDoc(DcRecordDelivery obj)
        {

            string title = obj.Country;


            StringBuilder sb = new StringBuilder();
            DcRecordDeliveryItem[] items;
            using (var service = new RecordDeliveryServiceClient())
            {
                items = service.GetRecordDeliveryItemByRecordDeliveryID(obj.ID);
            }
            foreach (var item in items)
            {
                sb.Append(item.Composition);
                sb.Append("-");
                sb.AppendLine(item.ProductID);
            }

            string output = sb.ToString();

            try
            {
                btApp = new bt.Application();
                string templateAddress = System.IO.Path.Combine(Environment.CurrentDirectory, "DocTemplate", "10070.btw");
                if (!System.IO.File.Exists(templateAddress))
                {
                    return;
                }
                btnFormat = btApp.Formats.Open(templateAddress, false, "");
                btnFormat.PrintSetup.IdenticalCopiesOfLabel = 1;
                btnFormat.PrintSetup.NumberSerializedLabels = 1;

                btnFormat.SetNamedSubStringValue("Title", title);
                btnFormat.SetNamedSubStringValue("Content", output);

                btnFormat.PrintOut(true, true);
                btnFormat.Close(bt.BtSaveOptions.btSaveChanges);
                btApp.Quit(bt.BtSaveOptions.btSaveChanges);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ActionEditItem(DcRecordDeliveryItem model)
        {
            PMSHelper.ViewModels.RecordDeliveryItemEdit.SetEdit(model);
            NavigationService.GoTo(PMSViews.RecordDeliveryItemEdit);
        }

        private void ActionAddItem(DcRecordDelivery model)
        {
            //传递RecordDelivery
            PMSHelper.ViewModels.RecordDeliveryItemEdit.SetNew(model);
            NavigationService.GoTo(PMSViews.RecordDeliveryItemEdit);
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.RecordDeliveryEdit.SetNew();
            NavigationService.GoTo(PMSViews.RecordDeliveryEdit);
        }

        private void ActionEdit(DcRecordDelivery model)
        {
            PMSHelper.ViewModels.RecordDeliveryEdit.SetEdit(model);
            NavigationService.GoTo(PMSViews.RecordDeliveryEdit);
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 10;
            var service = new RecordDeliveryServiceClient();
            RecordCount = service.GetDeliveryCount();
            ActionPaging();
        }
        private void ActionPaging()
        {
            var service = new RecordDeliveryServiceClient();
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var models = service.GetDelivery(skip, take);
            RecordDeliveries.Clear();
            models.ToList<DcRecordDelivery>().ForEach(o => RecordDeliveries.Add(o));

            CurrentSelectIndex = 0;
            CurrentSelectItem = RecordDeliveries.FirstOrDefault();
            ActionSelectionChanged(CurrentSelectItem);
        }
        #region Properties
        public ObservableCollection<DcRecordDelivery> RecordDeliveries { get; set; }
        public ObservableCollection<DcRecordDeliveryItem> RecordDeliveryItems { get; set; }
        private int currentSelectIndex;

        public int CurrentSelectIndex
        {
            get { return currentSelectIndex; }
            set { currentSelectIndex = value; RaisePropertyChanged(nameof(CurrentSelectIndex)); }
        }
        private DcRecordDelivery currentSelectItem;

        public DcRecordDelivery CurrentSelectItem
        {
            get { return currentSelectItem; }
            set { currentSelectItem = value; RaisePropertyChanged(nameof(CurrentSelectItem)); }
        }

        #endregion
        #region Commands
        public RelayCommand Add { get; set; }
        public RelayCommand<DcRecordDelivery> Edit { get; set; }
        public RelayCommand<DcRecordDelivery> Doc { get; set; }
        public RelayCommand<DcRecordDelivery> AddItem { get; set; }
        public RelayCommand<DcRecordDeliveryItem> EditItem { get; set; }
        public RelayCommand<DcRecordDelivery> SelectionChanged { get; set; }
        #endregion


    
    }
}
