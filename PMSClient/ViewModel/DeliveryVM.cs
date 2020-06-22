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
using PMSClient.Helpers;



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
            Check = new RelayCommand<DcDelivery>(ActionCheck, CanLabel);

            AddItem = new RelayCommand<DcDelivery>(ActionAddItem, CanAddItem);
            EditItem = new RelayCommand<DcDeliveryItem>(ActionEditItem, CanEditItem);
            SearchRecordTest = new RelayCommand<DcDeliveryItem>(ActionRecordTest, CanRecordTest);
            DeliverySheet = new RelayCommand<DcDelivery>(ActionDeliverySheet, CanDeliverySheet);
            BarCode = new RelayCommand<DcDelivery>(ActionBarCode, CanDeliverySheet);

            SelectionChanged = new RelayCommand<DcDelivery>(ActionSelectionChanged);
            GoToDeliveryItemList = new RelayCommand(ActionGoToDeliveryItemList);

            ScanAdd = new RelayCommand<DcDelivery>(AcitonScanAdd, CanScanAdd);

            QuickSave = new RelayCommand<DcDeliveryItem>(ActionQuickSave, CanQuickSave);
            SaveAllItems = new RelayCommand(ActionSaveAllItems, CanSaveAllItems);
            ExpressTrack = new RelayCommand(ActionExpressTrack, CanExpressTrack);

            ExpressSetting = new RelayCommand(ActionExpressSetting, CanExpressTrack);

            SampleTrace = new RelayCommand(ActionSampleTrace, CanAdd);
        }

        private void ActionSampleTrace()
        {
            //检查发货靶材的样品发出情况
            new Components.DeliveryItemSampleCheck.DeliveryItemSampleCheckService().Run();
        }

        private void ActionCheck(DcDelivery obj)
        {
            if (obj != null)
            {
                var tool = new DataProcess.QuickCheck.QuickCheck();

                //传入delivery到vm中
                var context = new DataProcess.QuickCheck.QuickCheckVM(obj);
                tool.DataContext = context;
                tool.Show();
            }
        }

        private void ActionExpressSetting()
        {
            var win = new ToolWindow.ExpressSetting();
            win.ShowDialog();
        }

        private void ActionBarCode(DcDelivery obj)
        {
            if (obj == null) return;
            var helper = new ReportsHelperNew.ReportDeliveryBarCode(obj.ID);
            helper.Intialize("条形码.docx");
            helper.Output();

        }

        private bool CanExpressTrack()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditDelivery) || PMSHelper.CurrentSession.IsInGroup(new string[] { "发货组" });
        }

        private void ActionExpressTrack()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定追踪【未完成】的发货物流情况吗？"))
                return;
            //追踪物流情况
            new Express.Operation().TraceUnCompleted();

        }

        private void ActionSaveAllItems()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定全部保存吗？"))
                return;
            try
            {
                using (var service = new DeliveryServiceClient())
                {
                    foreach (var item in DeliveryItems)
                    {
                        service.UpdateDeliveryItemByUID(item, PMSHelper.CurrentSession.CurrentUser.UserName);
                    }
                }
                PMSDialogService.Show("全部保存成功，确定后刷新");
                ActionSelectionChanged(CurrentSelectItem);

            }
            catch (Exception ex)
            {
                PMSDialogService.Show("保存失败");
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private bool CanSaveAllItems()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditDelivery) || PMSHelper.CurrentSession.IsInGroup(new string[] { "发货组" });
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
                PMSDialogService.Show("已保存到服务器,确定后刷新");
                ActionSelectionChanged(CurrentSelectItem);
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private bool CanQuickSave(DcDeliveryItem arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditDelivery) || PMSHelper.CurrentSession.IsInGroup(new string[] { "发货组" });
        }

        private void AcitonScanAdd(DcDelivery obj)
        {
            var tool = new DataProcess.ScanInput.ScanInput();
            tool.TxtValue.Text = "箱号";
            tool.TxtText.Text = "发货类型";
            tool.ChkCurrentCheck.Visibility = System.Windows.Visibility.Visible;
            tool.ChkCurrentCheck.Content = "进行ID重复检查";
            tool.ChkCurrentCheck2.Visibility = System.Windows.Visibility.Visible;
            tool.ChkCurrentCheck2.Content = "此次为样品发货";

            //传入delivery到vm中
            var context = new DataProcess.ScanInput.ScanInputDeliveryVM(obj);
            var list = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.DeliveryType>(list);
            context.Texts.Clear();
            context.Texts.AddRange(list);
            context.CurrentText = context.Texts[0];

            context.CurrentCheck = true;

            tool.DataContext = context;
            tool.Show();
        }

        private bool CanScanAdd(DcDelivery obj)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditDelivery) || PMSHelper.CurrentSession.IsInGroup(new string[] { "发货组" });
        }

        private bool CanFinish(DcDelivery arg)
        {
            return PMSHelper.CurrentSession.IsInGroup(new string[] { "管理员", "统筹组", "发货组" })
                && arg?.State != PMSCommon.DeliveryState.最终完成.ToString();
        }
        private bool CheckDeliveryState(string state)
        {
            return state == PMSCommon.DeliveryState.未完成.ToString();
        }
        private void ActionFinish(DcDelivery model)
        {
            if (model != null)
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定完成发货吗?,\r\nY=停止快递追踪,标记已签收,记录今天为签收时间 \r\nN=取消操作"))
                {
                    return;
                }
                try
                {
                    using (var service = new DeliveryServiceClient())
                    {
                        model.State = PMSCommon.DeliveryState.最终完成.ToString();
                        model.FinishTime = DateTime.Now;
                        model.LastUpdateTime = DateTime.Now;
                        //记录签收信息
                        model.IsCustomerSigned = true;
                        model.CustomerSignedDate = DateTime.Today;
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
            return (PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditDelivery) ||
                PMSHelper.CurrentSession.IsAuthorized(PMSAccess.CanDocDeliverySheet) ||
                PMSHelper.CurrentSession.IsInGroup(new string[] { "发货组" })
                ) && arg?.State != "未核验";
        }

        private void ActionDeliverySheet(DcDelivery model)
        {

            var dialog = new ToolWindow.DeliverySheetChooseDialog();
            bool dialogResult = (bool)dialog.ShowDialog();
            if (dialogResult)
            {
                try
                {
                    if (dialog.DeliverySheetType != "TCB")
                    {
                        WordDeliverySheet report = new WordDeliverySheet(dialog.DeliverySheetType);
                        report.SetModel(model);
                        report.Output();
                    }
                    else
                    {
                        var report_tcb = new WordDeliverySheetTCB();
                        report_tcb.SetModel(model);
                        report_tcb.Output();
                    }
                }
                catch (Exception ex)
                {
                    PMSHelper.CurrentLog.Error(ex);
                }
            }
        }

        private bool CanRecordTest(DcDeliveryItem arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.ReadRecordTest) || PMSHelper.CurrentSession.IsInGroup(new string[] { "发货组" });
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
            return (PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditDelivery) || 
                PMSHelper.CurrentSession.IsAuthorized(PMSAccess.CanDocDeliverySheet) || 
                PMSHelper.CurrentSession.IsInGroup(new string[] { "发货组" })
                ) && arg?.State != "未核验";
        }

        private bool CanEditItem(DcDeliveryItem arg)
        {
            return PMSHelper.CurrentSession.IsInGroup("DeliveryViewEdit");
        }

        private bool CanAddItem(DcDelivery arg)
        {
            return PMSHelper.CurrentSession.IsInGroup("DeliveryViewAdd");
        }
        /// <summary>
        /// 权限代码=编辑发货记录
        /// </summary>
        /// <returns></returns>
        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsInGroup("DeliveryViewAdd");
        }

        private bool CanEdit(DcDelivery arg)
        {
            return PMSHelper.CurrentSession.IsInGroup("DeliveryViewEdit");
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
                    result.OrderBy(i => i.OrderNumber)
                        .ThenBy(i => i.PackNumber)
                        .ThenBy(i => i.ProductID)
                        .ToList().ForEach(i => DeliveryItems.Add(i));

                    RaisePropertyChanged(nameof(TotalItems));
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
            //                    PMSHelper.CurrentLog.Error(ex);
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
                    items = service.GetDeliveryItemByDeliveryID(model.ID).OrderBy(i => i.PackNumber).ThenBy(i => i.ProductID).ToArray();
                }

                var select_win = new ToolWindow.LabelItemSelect();
                select_win.ShowDialog();
                if (select_win.DialogResult == false)
                {
                    return;
                }
                var dialog_result = select_win.Result;

                foreach (var item in items)
                {
                    if (dialog_result.HasComposition)
                    {
                        //sb.Append("Composition:");
                        sb.AppendLine(item.Composition);
                    }

                    if (dialog_result.HasCompositionAbbr)
                    {
                        string abbr = "";
                        if (item.Dimension.Contains("230"))
                        {
                            var result = CompositionHelper.WhatItIs(item.Composition);
                            if (result == TargetType.CIGS)
                            {
                                abbr = result.ToString() + "-" + CompositionHelper.GetGa(item.Composition);
                            }
                            else
                            {
                                abbr = result.ToString();
                            }
                        }
                        else
                        {
                            abbr = item.Abbr;
                        }
                        //2020-1-4
                        //如果成分包含NaF或者KF或者RbF或者Na2S之类的字样，则保留
                        string postfix = CompositionHelper.CheckAllAdditive(item.Composition);
                        if (!string.IsNullOrEmpty(postfix))
                        {
                            abbr += "+" + postfix;
                        }
                        sb.AppendLine(abbr);
                    }

                    if (dialog_result.HasDimension)
                    {
                        //sb.Append("Dimension:");
                        sb.AppendLine(item.Dimension);
                    }

                    if (dialog_result.HasCustomer)
                    {
                        //sb.Append("Customer:");
                        sb.AppendLine(item.Customer);
                    }

                    if (dialog_result.HasPO)
                    {
                        if (!item.PO.StartsWith("PO"))
                        {
                            sb.Append("PO:");
                        }
                        sb.AppendLine(item.PO);
                    }
                    if (dialog_result.HasPlateLot)
                    {
                        try
                        {
                            using (var s_bonding = new RecordBondingServiceClient())
                            {
                                var bonding = s_bonding.GetRecordBondingByProductID(item.ProductID.Trim())
                                    .FirstOrDefault();
                                if (bonding != null)
                                {
                                    sb.Append("BP-Lot:");
                                    sb.AppendLine(bonding.PlateLot);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            PMSHelper.CurrentLog.Error(ex);
                        }

                    }

                    if (dialog_result.HasPlateDrawing)
                    {
                        try
                        {
                            using (var s_test = new RecordTestServiceClient())
                            {
                                var test = s_test.GetRecordTestByProductID(item.ProductID.Trim()).FirstOrDefault();
                                if (test != null)
                                {
                                    using (var s_order = new PMSClient.NewService.NewServiceClient())
                                    {
                                        var order = s_order.GetOrderByPMINumber(test.PMINumber.Trim());
                                        if (order != null)
                                        {
                                            sb.Append("BP-Drawing:");
                                            sb.AppendLine(order.PlateDrawing ?? "None");
                                        }
                                    }
                                }

                            }

                        }
                        catch (Exception ex)
                        {
                            PMSHelper.CurrentLog.Error(ex);
                        }
                    }

                    if (dialog_result.HasProductID)
                    {
                        sb.AppendLine(item.ProductID.Trim());
                    }

                    if (dialog_result.HasSeperator)
                    {
                        sb.AppendLine("-----------------------------");
                    }
                }
                string mainContent = $"发往: {country}\r-----------------\r{sb.ToString()}";

                var pageTitle = "发货单标签打印输出";
                var tips = "请复制左边内容后点击打开模板按钮，粘贴到模板合适位置，可以自行修改，然后打印标签";
                var template = "发货单";
                var helpimage = "deliverysheet.png";
                PMSHelper.ToolViewModels.LabelOutPut.SetAllParameters(PMSViews.Delivery, pageTitle,
                    tips, template, mainContent, helpimage);

                var win = new Tool.LabelOutPutWindow();
                win.Show();
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
            try
            {
                using (var s = new DeliveryServiceClient())
                {
                    DateTime lastUpdateTime = s.GetDeliveryLastUpdateTime(model.ID);
                    if (lastUpdateTime > model.LastUpdateTime)
                    {
                        PMSDialogService.ShowWarning("服务器端数据已更新,确定后自动刷新，然后再试");
                        SetPageParametersWhenConditionChange();
                        return;
                    }
                    PMSHelper.ViewModels.DeliveryEdit.SetEdit(model);
                    NavigationService.GoTo(PMSViews.DeliveryEdit);
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }

        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 20;
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

        public int TotalItems
        {
            get
            {
                return DeliveryItems.Count;
            }
        }


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
        public RelayCommand<DcDelivery> Check { get; set; }
        public RelayCommand<DcDelivery> DeliverySheet { get; set; }
        public RelayCommand<DcDelivery> BarCode { get; set; }
        public RelayCommand<DcDelivery> AddItem { get; set; }
        public RelayCommand<DcDeliveryItem> EditItem { get; set; }
        public RelayCommand<DcDeliveryItem> SearchRecordTest { get; set; }
        public RelayCommand<DcDelivery> SelectionChanged { get; set; }

        public RelayCommand GoToDeliveryItemList { get; set; }

        public RelayCommand<DcDelivery> ScanAdd { get; set; }

        public RelayCommand<DcDeliveryItem> QuickSave { get; set; }
        public RelayCommand SaveAllItems { get; set; }
        public RelayCommand ExpressTrack { get; set; }
        public RelayCommand SampleTrace { get; set; }
        public RelayCommand ExpressSetting { get; set; }

        #endregion



    }
}
