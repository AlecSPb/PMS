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

namespace PMSDesktopClient.ViewModel
{
    public class RecordDeliveryVM : ViewModelBase
    {
        public RecordDeliveryVM()
        {
            Messenger.Default.Register<MsgObject>(this, VToken.RecordDeliveryRefresh, ActionRefresh);
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
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
        }

        private void ActionDoc(DcRecordDelivery obj)
        {
            throw new NotImplementedException();
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
            msg.MsgToken = VToken.RecordTestResultSelect;
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
        }
        public RelayCommand GoToNavigation { get; set; }
        public RelayCommand Add { get; set; }
        public RelayCommand<DcRecordDelivery> Edit { get; set; }

        public RelayCommand<DcRecordDelivery> Doc { get; set; }

        public RelayCommand<DcRecordDelivery> AddItem { get; set; }

        public RelayCommand<DcRecordDeliveryItem> EditItem { get; set; }

        public ObservableCollection<DcRecordDelivery> RecordDeliveries { get; set; }

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
