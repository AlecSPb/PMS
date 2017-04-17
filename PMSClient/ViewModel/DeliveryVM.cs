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
//using bt = BarTender;



namespace PMSClient.ViewModel
{
    public class DeliveryVM : BaseViewModelPage
    {
        public DeliveryVM()
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
            searchDeliveryName = "";
            Deliveries = new ObservableCollection<DcDelivery>();
            DeliveryItems = new ObservableCollection<DcDeliveryItem>();
        }
        private void InitializeCommands()
        {
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
            PageChanged = new RelayCommand(ActionPaging);
            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<DcDelivery>(ActionEdit, CanEdit);
            Doc = new RelayCommand<DcDelivery>(ActionDoc, CanDoc);
            AddItem = new RelayCommand<DcDelivery>(ActionAddItem, CanAddItem);
            EditItem = new RelayCommand<DcDeliveryItem>(ActionEditItem, CanEditItem);
            SelectionChanged = new RelayCommand<DcDelivery>(ActionSelectionChanged);
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private bool CanDoc(DcDelivery arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑发货记录")|| PMSHelper.CurrentSession.IsAuthorized("生成发货单标签"); 
        }

        private bool CanEditItem(DcDeliveryItem arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑发货记录");
        }

        private bool CanAddItem(DcDelivery arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑发货记录");
        }
        /// <summary>
        /// 权限代码=编辑发货记录
        /// </summary>
        /// <returns></returns>
        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑发货记录");
        }

        private bool CanEdit(DcDelivery arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑发货记录");
        }

        private void ActionAll()
        {
            SetPageParametersWhenConditionChange();
        }

        private void ActionSelectionChanged(DcDelivery model)
        {
            if (model != null)
            {
                using (var service = new DeliveryServiceClient())
                {
                    var result = service.GetDeliveryItemByDeliveryID(model.ID);
                    DeliveryItems.Clear();
                    result.ToList().ForEach(i => DeliveryItems.Add(i));

                    CurrentSelectItem = model;
                }
            }
        }

        /// <summary>
        /// 发货单标签打印区域
        /// </summary>
        //private bt.Application btApp;
        //private bt.Format btnFormat;
        private void ActionDoc(DcDelivery model)
        {
            #region 必须使用Automation版本的Bartender才允许自动化调用，这个版本36000RMB
            //string title = model.Country;
            //StringBuilder sb = new StringBuilder();
            //DcDeliveryItem[] items;
            //using (var service = new DeliveryServiceClient())
            //{
            //    items = service.GetDeliveryItemByDeliveryID(model.ID);
            //}
            //foreach (var item in items)
            //{
            //    sb.Append(item.Composition);
            //    sb.Append("-");
            //    sb.AppendLine(item.ProductID);
            //}

            //string output = sb.ToString();

            //try
            //{
            //    btApp = new bt.Application();
            //    string templateAddress = System.IO.Path.Combine(Environment.CurrentDirectory, "DocTemplate", "10070.btw");
            //    if (!System.IO.File.Exists(templateAddress))
            //    {
            //        return;
            //    }
            //    btnFormat = btApp.Formats.Open(templateAddress, false, "");
            //    btnFormat.PrintSetup.IdenticalCopiesOfLabel = 1;
            //    btnFormat.PrintSetup.NumberSerializedLabels = 1;

            //    btnFormat.SetNamedSubStringValue("MainTitle", title);
            //    btnFormat.SetNamedSubStringValue("MainContent", output);
            //    btnFormat.Close(bt.BtSaveOptions.btSaveChanges);

            //    btnFormat.PrintOut(true, true);
            //    btnFormat.Close(bt.BtSaveOptions.btSaveChanges);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    btApp.Quit(bt.BtSaveOptions.btSaveChanges);
            //}
            #endregion
            if (model != null)
            {
                string country = model.Country;
                StringBuilder sb = new StringBuilder();
                DcDeliveryItem[] items;
                using (var service = new DeliveryServiceClient())
                {
                    items = service.GetDeliveryItemByDeliveryID(model.ID);
                }
                int counter = 1;
                foreach (var item in items)
                {
                    //sb.Append(item.Composition.Trim());
                    sb.Append($"No {counter}");
                    sb.Append(" = ");
                    sb.AppendLine($"[{item.ProductID.Trim()}]");
                    counter++;
                }
                string mainContent = $"发往: {country}\r{sb.ToString()}";

                var pageTitle = "发货单标签打印输出";
                var tips = "请复制左边内容后点击打开模板按钮，粘贴到模板合适位置，可以自行修改，然后打印标签";
                var template = "发货单";
                var helpimage = "deliverysheet.png";
                PMSHelper.ToolViewModels.LabelOutPut.SetAllParameters(PMSViews.Delivery, pageTitle,
                    tips, template, mainContent, helpimage);
                NavigationService.GoTo(PMSViews.LabelOutPut);
            }
        }

        private void ActionEditItem(DcDeliveryItem model)
        {
            PMSHelper.ViewModels.DeliveryItemEdit.SetEdit(model);
            NavigationService.GoTo(PMSViews.DeliveryItemEdit);
        }

        private void ActionAddItem(DcDelivery model)
        {
            //传递Delivery
            PMSHelper.ViewModels.DeliveryItemEdit.SetNew(model);
            NavigationService.GoTo(PMSViews.DeliveryItemEdit);
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.DeliveryEdit.SetNew();
            NavigationService.GoTo(PMSViews.DeliveryEdit);
        }

        private void ActionEdit(DcDelivery model)
        {
            PMSHelper.ViewModels.DeliveryEdit.SetEdit(model);
            NavigationService.GoTo(PMSViews.DeliveryEdit);
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 10;
            var service = new DeliveryServiceClient();
            RecordCount = service.GetDeliveryCountBySearch(SearchDeliveryName);
            service.Close();
            ActionPaging();
        }
        private void ActionPaging()
        {

            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var service = new DeliveryServiceClient();
            var models = service.GetDeliveryBySearch(skip, take, SearchDeliveryName);
            service.Close();
            Deliveries.Clear();
            models.ToList().ForEach(o => Deliveries.Add(o));

            CurrentSelectIndex = 0;
            CurrentSelectItem = Deliveries.FirstOrDefault();
            ActionSelectionChanged(CurrentSelectItem);
        }
        #region Properties
        public ObservableCollection<DcDelivery> Deliveries { get; set; }
        public ObservableCollection<DcDeliveryItem> DeliveryItems { get; set; }
        private int currentSelectIndex;

        public int CurrentSelectIndex
        {
            get { return currentSelectIndex; }
            set { currentSelectIndex = value; RaisePropertyChanged(nameof(CurrentSelectIndex)); }
        }
        private DcDelivery currentSelectItem;

        public DcDelivery CurrentSelectItem
        {
            get { return currentSelectItem; }
            set { currentSelectItem = value; RaisePropertyChanged(nameof(CurrentSelectItem)); }
        }


        private string searchDeliveryName;

        public string SearchDeliveryName
        {
            get { return searchDeliveryName; }
            set { searchDeliveryName = value; RaisePropertyChanged(nameof(SearchDeliveryName)); }
        }

        #endregion
        #region Commands
        public RelayCommand Add { get; set; }
        public RelayCommand<DcDelivery> Edit { get; set; }
        public RelayCommand<DcDelivery> Doc { get; set; }
        public RelayCommand<DcDelivery> AddItem { get; set; }
        public RelayCommand<DcDeliveryItem> EditItem { get; set; }
        public RelayCommand<DcDelivery> SelectionChanged { get; set; }
        #endregion



    }
}
