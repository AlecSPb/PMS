using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using PMSDesktopClient.PMSMainService;
using GalaSoft.MvvmLight.Messaging;
using bt = BarTender;



namespace PMSDesktopClient.ViewModel
{
    public class RecordDeliveryVM : ViewModelBase
    {
        public RecordDeliveryVM()
        {
            Messenger.Default.Register<MsgObject>(this, VToken.RecordDeliveryRefresh, ActionRefresh);
            Messenger.Default.Register<MsgObject>(this, VToken.RecordDeliveryItemRefresh, ActionRefreshItems);
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }

        private void ActionRefreshItems(MsgObject obj)
        {
            ActionSelectionChanged(CurrentSelectItem);
        }

        public override void Cleanup()
        {
            Messenger.Default.Unregister(this);
            base.Cleanup();
        }

        private void ActionRefresh(MsgObject obj)
        {
            SetPageParametersWhenConditionChange();
        }

        private void InitializeProperties()
        {
            RecordDeliveries = new ObservableCollection<DcRecordDelivery>();
            RecordDeliveryItems = new ObservableCollection<DcRecordDeliveryItem>();
        }
        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            GoToNavigation = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VToken.Navigation }));
            Add = new RelayCommand(ActionAdd);
            Edit = new RelayCommand<PMSMainService.DcRecordDelivery>(ActionEdit);
            Doc = new RelayCommand<PMSMainService.DcRecordDelivery>(ActionDoc);
            AddItem = new RelayCommand<PMSMainService.DcRecordDelivery>(ActionAddItem);
            EditItem = new RelayCommand<PMSMainService.DcRecordDeliveryItem>(ActionEditItem);
            SelectionChanged = new RelayCommand<PMSMainService.DcRecordDelivery>(ActionSelectionChanged);
        }

        private void ActionSelectionChanged(DcRecordDelivery obj)
        {
            if (obj != null)
            {
                using (var service = new RecordDeliveryServiceClient())
                {
                    var result = service.GetRecordDeliveryItemByRecordDeliveryID(obj.ID);
                    RecordDeliveryItems.Clear();
                    result.ToList().ForEach(i => RecordDeliveryItems.Add(i));

                    CurrentSelectItem = obj;
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

        private void ActionEditItem(DcRecordDeliveryItem obj)
        {
            MsgObject msg = new MsgObject();
            msg.MsgToken = VToken.RecordDeliveryItemEdit;
            msg.MsgModel = new ModelObject() { IsNew = false, Model = obj };

            NavigationService.GoTo(msg);
        }

        private void ActionAddItem(DcRecordDelivery obj)
        {
            //传递RecordDelivery到RecordTestSelect
            MsgObject msg = new MsgObject();
            msg.MsgToken = VToken.RecordTestSelect;
            msg.MsgModel = new ModelObject() { IsNew = true, Model = obj };
            NavigationService.GoTo(msg);
        }

        private void ActionAdd()
        {
            var model = new DcRecordDelivery();
            model.ID = Guid.NewGuid();
            model.InvoiceNumber = "InvoiceNumber";
            model.DeliveryName = DateTime.Now.ToString("yyMMdd") + "A";
            model.DeliveryNumber = "UPS";
            model.CreateTime = DateTime.Now;
            model.Creator = (App.Current as App).CurrentUser.UserName;
            model.State = PMSCommon.CommonState.UnChecked.ToString();
            model.PackageInformation = "50kg";
            model.PackageType = "Wood";
            model.Remark = "";
            model.ShipTime = DateTime.Now;
            model.Address = "Address Here";
            model.Country = "USA";

            MsgObject msg = new PMSDesktopClient.MsgObject();
            msg.MsgToken = VToken.RecordDeliveryEdit;
            msg.MsgModel = new PMSDesktopClient.ModelObject() { IsNew = true, Model = model };
            NavigationService.GoTo(msg);
        }

        private void ActionEdit(DcRecordDelivery obj)
        {
            MsgObject msg = new PMSDesktopClient.MsgObject();
            msg.MsgToken = VToken.RecordDeliveryEdit;
            msg.MsgModel = new ModelObject() { IsNew = false, Model = obj };
            NavigationService.GoTo(msg);
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
        public RelayCommand GoToNavigation { get; set; }
        public RelayCommand Add { get; set; }
        public RelayCommand<DcRecordDelivery> Edit { get; set; }
        public RelayCommand<DcRecordDelivery> Doc { get; set; }
        public RelayCommand<DcRecordDelivery> AddItem { get; set; }
        public RelayCommand<DcRecordDeliveryItem> EditItem { get; set; }
        public RelayCommand<DcRecordDelivery> SelectionChanged { get; set; }
        #endregion


        #region PagingProperties
        public RelayCommand PageChanged { get; private set; }
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

    }
}
