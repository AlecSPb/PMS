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
using PMSClient.ReportsHelper;
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
            Label = new RelayCommand<DcDelivery>(ActionLabel, CanLabel);
            Finish = new RelayCommand<DcDelivery>(ActionFinish, CanFinish);
            AddItem = new RelayCommand<DcDelivery>(ActionAddItem, CanAddItem);
            EditItem = new RelayCommand<DcDeliveryItem>(ActionEditItem, CanEditItem);
            SearchRecordTest = new RelayCommand<DcDeliveryItem>(ActionRecordTest, CanRecordTest);
            DeliverySheet = new RelayCommand<MainService.DcDelivery>(ActionDeliverySheet, CanDeliverySheet);

            SelectionChanged = new RelayCommand<DcDelivery>(ActionSelectionChanged);
            GoToDeliveryItemList = new RelayCommand(ActionGoToDeliveryItemList);

            ScanAdd = new RelayCommand<DcDelivery>(AcitonScanAdd, CanScanAdd);

            QuickSave = new RelayCommand<DcDeliveryItem>(ActionQuickSave, CanQuickSave);
        }

        private void ActionQuickSave(DcDeliveryItem obj)
        {
            try
            {
                if (obj == null)
                    return;
                using (var service = new DeliveryServiceClient())
                {
                    service.UpdateDeliveryItemByUID(obj, PMSHelper.CurrentSession.CurrentUser.UserName);
                }
                PMSDialogService.Show("已保存到服务器");
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private bool CanQuickSave(DcDeliveryItem arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditDelivery);
        }

        private void AcitonScanAdd(DcDelivery obj)
        {
            var tool = new DataProcess.ScanInput.ScanInput();
            tool.Show();
        }

        private bool CanScanAdd(DcDelivery obj)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditDelivery);
        }

        private bool CanFinish(DcDelivery arg)
        {
            if (arg != null)
            {
                return (PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditDelivery) || PMSHelper.CurrentSession.IsAuthorized(PMSAccess.CanDocDeliverySheet)) && CheckDeliveryState(arg.State);
            }
            else
            {
                return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditDelivery) || PMSHelper.CurrentSession.IsAuthorized(PMSAccess.CanDocDeliverySheet);
            }
        }
        private bool CheckDeliveryState(string state)
        {
            return state == PMSCommon.DeliveryState.未完成.ToString();
        }
        private void ActionFinish(DcDelivery model)
        {
            if (model != null)
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定完成发货吗?"))
                {
                    return;
                }
                try
                {
                    using (var service = new DeliveryServiceClient())
                    {
                        model.State = PMSCommon.DeliveryState.完成.ToString();
                        model.FinishTime = DateTime.Now;
                        service.UpdateDeliveryByUID(model, PMSHelper.CurrentSession.CurrentUser.UserName);
                    }
                    NavigationService.Status("发货完成");
                    SetPageParametersWhenConditionChange();
                }
                catch (Exception ex)
                {
                    PMSHelper.CurrentLog.Error(ex);
                }
            }
        }

        public void SetSearch(string deliveryName)
        {
            SearchDeliveryName = deliveryName;
            SetPageParametersWhenConditionChange();
        }


        private bool CanDeliverySheet(DcDelivery arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditDelivery) || PMSHelper.CurrentSession.IsAuthorized(PMSAccess.CanDocDeliverySheet);
        }

        private void ActionDeliverySheet(DcDelivery model)
        {

            var dialog = new ToolWindow.DeliverySheetChooseDialog();
            bool dialogResult = (bool)dialog.ShowDialog();
            if (dialogResult)
            {
                try
                {
                    WordDeliverySheet report = new WordDeliverySheet(dialog.DeliverySheetType);
                    report.SetModel(model);
                    report.Output();
                    PMSDialogService.ShowYes("生成成功", "请打开桌面上的发货单文档\r\n如果排版变形，请全选更改为宋体");
                }
                catch (Exception ex)
                {
                    PMSHelper.CurrentLog.Error(ex);
                }
            }
        }

        private bool CanRecordTest(DcDeliveryItem arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.ReadRecordTest);
        }

        private void ActionRecordTest(DcDeliveryItem model)
        {
            if (model != null)
            {
                PMSHelper.ViewModels.RecordTest.SetSearch("", model.ProductID);
                NavigationService.GoTo(PMSViews.RecordTest);
            }
        }

        private void ActionGoToDeliveryItemList()
        {
            NavigationService.GoTo(PMSViews.DeliveryItemList);
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private bool CanLabel(DcDelivery arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditDelivery) || PMSHelper.CurrentSession.IsAuthorized(PMSAccess.CanDocDeliverySheet);
        }

        private bool CanEditItem(DcDeliveryItem arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditDelivery);
        }

        private bool CanAddItem(DcDelivery arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditDelivery);
        }
        /// <summary>
        /// 权限代码=编辑发货记录
        /// </summary>
        /// <returns></returns>
        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditDelivery);
        }

        private bool CanEdit(DcDelivery arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditDelivery);
        }

        private void ActionAll()
        {
            SearchDeliveryName = "";
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
        private void ActionLabel(DcDelivery model)
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
                    items = service.GetDeliveryItemByDeliveryID(model.ID).OrderBy(i => i.PackNumber).ToArray();
                }
                //int counter = 1;
                foreach (var item in items)
                {
                    //sb.Append(item.Composition.Trim());
                    sb.Append($"[{item.PackNumber.ToString()}]");
                    sb.Append($">[{item.ProductID.Trim()}]");
                    sb.AppendLine($">[PO#{item.PO.ToString()}]");
                    //counter++;
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
            PageSize = 5;
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
        public RelayCommand<DcDelivery> Label { get; set; }
        public RelayCommand<DcDelivery> Finish { get; set; }
        public RelayCommand<DcDelivery> DeliverySheet { get; set; }
        public RelayCommand<DcDelivery> AddItem { get; set; }
        public RelayCommand<DcDeliveryItem> EditItem { get; set; }
        public RelayCommand<DcDeliveryItem> SearchRecordTest { get; set; }
        public RelayCommand<DcDelivery> SelectionChanged { get; set; }

        public RelayCommand GoToDeliveryItemList { get; set; }

        public RelayCommand<DcDelivery> ScanAdd { get; set; }

        public RelayCommand<DcDeliveryItem> QuickSave { get; set; }
        #endregion



    }
}
