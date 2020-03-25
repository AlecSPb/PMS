using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;
using System.IO;
using PMSClient.ViewModel.Model;
using PMSClient.ReportsHelper;
using PMSClient.ToolWindow;
using PMSClient.CheckLogic;
using CommonHelper;
using Newtonsoft.Json;


namespace PMSClient.ViewModel
{
    public class RecordTestVM : BaseViewModelPage
    {
        public RecordTestVM()
        {
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }

        /// <summary>
        /// 设置好搜索字段Product和Composition
        /// </summary>
        /// <param name="composition"></param>
        /// <param name="productid"></param>
        public void SetSearch(string composition, string productid)
        {
            SearchCompositionStd = composition;
            SearchProductID = productid;
            //必须重新激发
            RaisePropertyChanged(nameof(SearchCompositionStd));
            RaisePropertyChanged(nameof(SearchProductID));
            SetPageParametersWhenConditionChange();
        }

        public void SetSearch(string vhpnumber)
        {
            SearchCompositionStd = "";
            SearchProductID = vhpnumber;
        }

        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }

        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);
            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<RecordTestExtra>(ActionEdit, CanEdit);
            Doc = new RelayCommand<RecordTestExtra>(ActionDoc, CanDoc);
            SelectionChanged = new RelayCommand<RecordTestExtra>(ActionSelectionChanged);
            Duplicate = new RelayCommand<RecordTestExtra>(ActionDuplicate, CanDuplicate);
            Label = new RelayCommand<RecordTestExtra>(ActionLabel);
            Check = new RelayCommand<RecordTestExtra>(ActionCheck, CanCheck);


            QuickAdd = new RelayCommand(ActionQuickAdd, CanQuickAdd);
            Output = new RelayCommand(ActionOutput);
            BatchDoc = new RelayCommand(ActionBatchDoc);
            QuickDoc = new RelayCommand(ActionQuickDoc, CanQuickDoc);
            QuickChart = new RelayCommand(ActionQuickChart, CanQuickChart);

            Compare = new RelayCommand<RecordTestExtra>(ActionCompare);
            QuickLabel = new RelayCommand(ActionQuickLabel);

            ShowOrderInformation = new RelayCommand<RecordTestExtra>(ActionShowOrderInformation);

            EnterCSCAN = new RelayCommand<RecordTestExtra>(ActionEnterCscan, CanEnterCscan);

            OneKeyCheck = new RelayCommand(ActonOneKeyCheck);

            DeepSearch = new RelayCommand<RecordTestExtra>(ActionDeepSearch, CanDeepSearch);
            SaveJson = new RelayCommand<RecordTestExtra>(ActionSaveJson, CanSaveJson);
        }

        private void ActionSaveJson(RecordTestExtra obj)
        {
            if (obj != null)
            {
                var json_obj = obj.RecordTest;
                string json = JsonConvert.SerializeObject(json_obj);
                string initialPath = XSHelper.FileHelper.GetDesktopPath();
                string filename = $"{StringUtil.RemoveSlash(json_obj.Composition)}-{json_obj.ProductID}";
                XSDialogResult parameter = XSHelper.DialogHelper.ShowSaveDialog(initialPath, "Data|*.json", filename);
                if (parameter.HasSelected)
                {
                    XSHelper.FileHelper.SaveText(parameter.SelectPath, json);
                    XSHelper.MessageHelper.ShowInfo("保存成功");
                }
            }
        }

        private bool CanSaveJson(RecordTestExtra arg)
        {
            return PMSHelper.CurrentSession.IsInGroup(new string[] { "统筹组", "测试组", "管理员" });
        }

        private void ActionDeepSearch(RecordTestExtra obj)
        {
            if (obj == null) return;
            var win = new ToolWindow.IntegratedSearch();
            win.TxtProductID.Text = obj.RecordTest.ProductID;
            win.TriggerBtnSearch_Click();
            win.Show();
        }

        private bool CanDeepSearch(RecordTestExtra arg)
        {
            return PMSHelper.CurrentSession.IsInGroup(new string[] { "统筹组", "测试组", "管理员" });
        }

        private void ActonOneKeyCheck()
        {
            var win = new CheckLogic.RecordTestCheckDialog();
            win.Show();
        }

        private bool CanEnterCscan(RecordTestExtra arg)
        {
            return PMSHelper.CurrentSession.IsInGroup(new string[] { "测试组", "热压组", "管理员" });
        }

        private void ActionEnterCscan(RecordTestExtra obj)
        {

            if (obj != null)
            {
                //最后一次更新时间检查
                try
                {
                    using (var s = new RecordTestServiceClient())
                    {
                        DateTime lastUpdateTime = s.GetLastUpdateTime(obj.RecordTest.ID);
                        if (lastUpdateTime > obj.RecordTest.LastUpdateTime)
                        {
                            PMSDialogService.ShowWarning("服务器端数据已更新,确定后自动刷新，然后再试");
                            SetPageParametersWhenConditionChange();
                            return;
                        }
                    }
                }
                catch (Exception)
                {

                }

                //编辑锁定检查，如果有锁定提示，没有锁定继续
                //三个地方解锁，保存后解锁，取消后解锁，管理全局解锁，自我全局解锁

                try
                {
                    //检查编辑锁定  这里只检查锁定，不锁定
                    string lockinfo = Helpers.EditLockHelper.CheckLock(obj.RecordTest.ID);
                    if (lockinfo != null)
                    {
                        PMSDialogService.Show(lockinfo.ToString());
                        return;
                    }

                }
                catch (Exception)
                {

                }



                var dialog = new ToolDialog.EnterCSCAN();
                dialog.ProductInformation = obj.RecordTest.ProductID + "+" + obj.RecordTest.Composition;
                dialog.CSCAN = obj.RecordTest.CScan;

                dialog.ShowDialog();
                if (dialog.DialogResult == true)
                {
                    obj.RecordTest.CScan = dialog.CSCAN;

                    using (var service = new RecordTestServiceClient())
                    {
                        //刷新最后更新时间
                        obj.RecordTest.LastUpdateTime = DateTime.Now;
                        service.UpdateRecordTestByUID(obj.RecordTest, PMSHelper.CurrentSession.CurrentUser.UserName);
                    }
                }
            }
        }

        /// <summary>
        /// 显示订单信息
        /// </summary>
        /// <param name="obj"></param>
        private void ActionShowOrderInformation(RecordTestExtra obj)
        {
            if (obj != null)
            {
                using (var service = new OrderServiceClient())
                {
                    var result = service.GetOrders(0, 1, "", "", obj.RecordTest.PMINumber).FirstOrDefault();
                    if (result != null)
                    {
                        var window = new ToolWindow.InformationWindow();
                        window.WindowTitle = "订单信息";
                        StringBuilder sb = new StringBuilder();
                        #region 订单信息
                        sb.Append("【成分】:");
                        sb.AppendLine(result.CompositionStandard);
                        sb.Append("【客户】:");
                        sb.AppendLine(result.CustomerName);
                        sb.Append("【PO】:");
                        sb.AppendLine(result.PO);
                        sb.Append("【尺寸】:");
                        sb.AppendLine(result.Dimension);
                        sb.Append("【尺寸要求】:");
                        sb.AppendLine(result.DimensionDetails);
                        sb.Append("【数量】:");
                        sb.AppendLine($"{result.Quantity}{result.QuantityUnit}");
                        sb.Append("【最低要求】:");
                        sb.AppendLine(result.MinimumAcceptDefect);
                        sb.Append("【客户样品】:");
                        sb.AppendLine(result.SampleNeed);
                        sb.Append("【自分析样品】:");
                        sb.AppendLine(result.SampleForAnlysis);
                        sb.Append("【配有背板】:");
                        sb.AppendLine(result.WithBackingPlate);
                        sb.Append("【特殊要求】:");
                        sb.AppendLine(result.SpecialRequirement);
                        sb.Append("【交付日期】:");
                        sb.AppendLine(result.DeadLine.ToLongDateString());
                        sb.Append("【PartNumber】:");
                        sb.AppendLine(result.PartNumber);
                        sb.Append("【二次加工尺寸】:");
                        sb.AppendLine(result.SecondMachineDimension);
                        sb.Append("【二次加工细节】:");
                        sb.AppendLine(result.SecondMachineDetails);
                        #endregion
                        if (result.DimensionDetails.Contains("FR="))
                        {
                            window.WindowWarning = "此靶材有倒角要求";
                        }
                        else
                        {
                            window.WindowWarning = "";
                        }
                        window.WindowContent = sb.ToString();
                        window.Show();
                    }
                }
            }
        }

        private void ActionQuickLabel()
        {
            using (var service = new RecordTestServiceClient())
            {
                StringBuilder sb = new StringBuilder();
                var result = service.GetRecordTestBySearch(1,30,"","","");
                foreach (var item in result)
                {
                    sb.AppendLine(VMHelper.RecordTestVMHelper.CreateLabel(item));
                }
                var win = new LabelCopyWindow();
                win.LabelInformation = sb.ToString();
                win.Show();
            }
        }

        private void ActionQuickChart()
        {
            if (RecordTestExtras.Count < 3)
            {
                PMSDialogService.ShowWarning("至少要显示出3条记录，才可以使用图表功能");
                return;
            }
            var chart = new DensityChart();
            var vm = new DensityChartVM();

            vm.DisplayRecord(RecordTestExtras.ToList());

            chart.DataContext = vm;
            chart.Show();
        }

        private bool CanQuickChart()
        {
            return true;
        }

        private bool CanCheck(RecordTestExtra arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditRecordTest)
                && arg?.RecordTest.State == PMSCommon.CommonState.未核验.ToString();
        }

        private void ActionCheck(RecordTestExtra obj)
        {
            if (obj != null)
            {
                if (PMSDialogService.ShowYesNo("请问", "确定要将此块记录设置为[已核验]吗？"))
                {
                    string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                    using (var service = new RecordTestServiceClient())
                    {
                        var model = obj.RecordTest;
                        model.State = PMSCommon.CommonState.已核验.ToString();

                        #region 核验


                        string composition = model.CompositionXRF;
                        //检测是否错误输入Si，S，P，B，C之类不可测试的元素
                        if (composition.Contains("Si atm%")
                            || composition.Contains("S atm%")
                            || composition.Contains("P atm%")
                            || composition.Contains("B atm%")
                            || composition.Contains("C atm%")
                            )
                        {
                            if (!PMSDialogService.ShowYesNo("请问", "成分误包含有Si，S，P，B，C,确定继续保存吗？"))
                            {
                                return;
                            }
                        }

                        //密度检查
                        string abbr = model.CompositionAbbr;
                        double density = 0;
                        double.TryParse(model.Density, out density);

                        //if (string.IsNullOrEmpty(CurrentRecordTest.CompositionXRF))
                        //{
                        //    CurrentRecordTest.CompositionXRF = "无";
                        //}

                        if (!string.IsNullOrEmpty(abbr) && density != 0)
                        {
                            CheckResult msg = RecordTestCheckLogic.IsDensityOK(abbr, density);
                            if (!msg.IsCheckOK)
                            {
                                PMSDialogService.ShowWarning(msg.Message);
                            }
                        }

                        //BridgeLine成分检查警告
                        if (!RecordTestCheckLogic.IsBridgeLineCompositionOK(model.Customer,
                            model.CompositionXRF))
                        {
                            RecordTestCheckLogic.ShowWarningDialog("BridgeLine的成分测试需要有13点数据！");

                        }

                        #endregion

                        service.UpdateRecordTestByUID(model, uid);
                    }

                    SetPageParametersWhenConditionChange();

                }
            }
        }

        private void ActionCompare(RecordTestExtra obj)
        {
            if (obj == null) return;
            SearchCompositionStd = obj.RecordTest.Composition;
            SetPageParametersWhenConditionChange();
        }

        private bool CanQuickDoc()
        {
            return true;
        }

        private void ActionQuickDoc()
        {
            try
            {
                var tool = new DataProcess.QuickReport.QuickReport();
                var context = new DataProcess.QuickReport.QuickReportVM();
                tool.DataContext = context;
                tool.Show();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        /// <summary>
        /// 批量生成COA
        /// </summary>
        private void ActionBatchDoc()
        {
            if (RecordTestExtras.ToList().Count(i => i.IsSelected) == 0)
            {
                PMSDialogService.ShowWarning("没有选中任何项目");
                return;
            }

            if (!PMSDialogService.ShowYesNo("请问", "确定要为选择的项目生成报告吗"))
                return;

            var dtWindow = new DocumentTypeSelect();
            dtWindow.ShowDialog();
            if (dtWindow.DialogResult != true)
            {
                return;
            }

            string documentType = dtWindow.DocumentType;

            try
            {
                if (documentType == "COA")
                {
                    WordCOANew report = new WordCOANew();
                    foreach (var item in RecordTestExtras)
                    {
                        if (item.IsSelected)
                        {
                            report.SetModel(item.RecordTest);
                            report.Output();
                        }
                    }
                }
                else if (documentType == "COABL")
                {
                    WordCOABridgeLineNew report = new WordCOABridgeLineNew();
                    foreach (var item in RecordTestExtras)
                    {
                        if (item.IsSelected)
                        {
                            report.SetModel(item.RecordTest);
                            report.Output();
                        }
                    }
                }
                else if (documentType == "TEST")
                {
                    WordRecordTest report = new WordRecordTest();
                    foreach (var item in RecordTestExtras)
                    {
                        if (item.IsSelected)
                        {
                            report.SetModel(item.RecordTest);
                            report.Output();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                NavigationService.Status(ex.Message);
            }
            PMSDialogService.Show("提示", "创建完毕，请打开桌面的文件夹，仔细检查内容是否正确");
            NavigationService.Status("创建完毕！");
        }

        private void ActionOutput()
        {
            if (!PMSDialogService.ShowYesNo("询问", "数据导出时间会比较长，" +
                "请在弹出完成对话框之前不要进行其他操作。\r\n确定明白请点确定开始"))
            {
                return;
            }

            int pageIndex = 1;
            int pageSize = 20;
            int recordCount = 0;
            using (var service = new RecordTestServiceClient())
            {
                recordCount = service.GetRecordTestCountBySearchInPage(SearchProductID, SearchCompositionStd);
            }

            int pageCount = recordCount / PageSize + (recordCount % PageSize == 0 ? 0 : 1);

            int skip = 0, take = 0;
            take = pageSize;
            skip = (pageIndex - 1) * pageSize;

            string outputfile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
                , "导出数据-测试" + DateTime.Now.ToString("yyyyMMddmmhhss") + ".csv");
            StreamWriter sw = new StreamWriter(new FileStream(outputfile, FileMode.Append), System.Text.Encoding.GetEncoding("GB2312"));
            string titleString = "状态,后续,产品ID,测试类型,成分,缩写,内部编号,PO,客户,要求尺寸,重量,密度,电阻率,缺陷,实际尺寸,样品,创建时间,创建者,备注,粗糙度";
            sw.WriteLine(titleString);
            using (var service = new RecordTestServiceClient())
            {
                try
                {
                    string outputString = "";
                    while (pageIndex <= pageCount)
                    {
                        var models = service.GetRecordTestBySearchInPage(skip, take, SearchProductID, SearchCompositionStd);
                        outputString = PMSOuputHelper.GetRecordTestOupput(models);
                        sw.Write(outputString.ToString());
                        sw.Flush();

                        pageIndex++;
                        skip = (pageIndex - 1) * pageSize;
                    }
                }
                catch (Exception ex)
                {
                    PMSHelper.CurrentLog.Error(ex);
                }
            }
            sw.Close();

            PMSDialogService.Show("数据导出完成到桌面，请右键-打开方式-Excel打开文件");

        }

        private bool CanQuickAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditRecordTest);
        }

        private void ActionQuickAdd()
        {
            PMSHelper.ViewModels.PlanSelect.SetRequestView(PMSViews.RecordTest);
            PMSHelper.ViewModels.PlanSelect.RefreshData();
            NavigationService.GoTo(PMSViews.PlanSelect);
        }

        private void ActionLabel(RecordTestExtra model)
        {
            if (model != null)
            {
                var s = VMHelper.RecordTestVMHelper.CreateLabel(model.RecordTest);
                var lcw = new ToolWindow.LabelCopyWindow();
                lcw.LabelInformation = s;
                lcw.Show();
            }
        }

        private bool CanDuplicate(RecordTestExtra arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditRecordTest);
        }

        private bool CanDoc(RecordTestExtra arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.ReadRecordTest);
        }

        private bool CanEdit(RecordTestExtra arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditRecordTest);
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditRecordTest);
        }

        private void ActionSelectionChanged(RecordTestExtra model)
        {
            CurrentSelectItem = model;
        }

        private bool CanSearch()
        {
            return !string.IsNullOrEmpty(SearchProductID) || !string.IsNullOrEmpty(SearchCompositionStd) || !string.IsNullOrEmpty(SearchPMINumber);
        }

        private void ActionAll()
        {
            SearchProductID = SearchCompositionStd = SearchPMINumber = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void ActionDuplicate(RecordTestExtra model)
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定复用这条记录？")) return;
            var dialog = new ToolWindow.DuplicateNumberDialog();
            dialog.ShowDialog();

            if (dialog.Quantity > 0)
            {
                if (dialog.Quantity == 1)
                {
                    PMSHelper.ViewModels.RecordTestEdit.SetDuplicate(model.RecordTest);
                    NavigationService.GoTo(PMSViews.RecordTestEdit);
                }
                else
                {
                    var modelhelper = new Helpers.RecordTestDuplicateHelper();
                    modelhelper.Run(CurrentSelectItem.RecordTest, dialog.Quantity);
                }
                SetPageParametersWhenConditionChange();
            }

        }

        private void ActionEdit(RecordTestExtra model)
        {
            if (model == null) return;
            try
            {
                using (var s = new RecordTestServiceClient())
                {
                    DateTime lastUpdateTime = s.GetLastUpdateTime(model.RecordTest.ID);
                    if (lastUpdateTime > model.RecordTest.LastUpdateTime)
                    {
                        PMSDialogService.ShowWarning("服务器端数据已更新,确定后自动刷新，然后再试");
                        SetPageParametersWhenConditionChange();
                        return;
                    }
                }
            }
            catch (Exception)
            {

            }


            //编辑锁定检查，如果有锁定提示，没有锁定继续
            //三个地方解锁，保存后解锁，取消后解锁，管理全局解锁，自我全局解锁

            try
            {
                //检查编辑锁定
                string lockinfo = Helpers.EditLockHelper.CheckLock(model.RecordTest.ID);
                if (lockinfo != null)
                {
                    PMSDialogService.Show(lockinfo.ToString());
                    return;
                }
                else
                {
                    //锁定
                    Helpers.EditLockHelper.Lock(model.RecordTest.ID, "测试记录");
                }

            }
            catch (Exception)
            {

            }

            PMSHelper.ViewModels.RecordTestEdit.SetEdit(model.RecordTest);
            NavigationService.GoTo(PMSViews.RecordTestEdit);
        }

        private void ActionDoc(RecordTestExtra model)
        {
            if (model != null)
            {
                PMSHelper.ViewModels.RecordTestDoc.SetModel(model.RecordTest);
                var docWindow = new PMSClient.View.RecordTestDocWindow();
                docWindow.Show();
                //NavigationService.GoTo(PMSViews.RecordTestDoc);
            }

        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.RecordTestEdit.SetNew();
            NavigationService.GoTo(PMSViews.RecordTestEdit);
        }

        private void InitializeProperties()
        {
            RecordTestExtras = new ObservableCollection<RecordTestExtra>();
            SearchCompositionStd = SearchProductID = SearchPMINumber = "";

        }
        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 30;
            using (var service = new RecordTestServiceClient())
            {
                RecordCount = service.GetRecordTestCountBySearch(SearchProductID, SearchCompositionStd, SearchPMINumber);
            }

            ActionPaging();
        }
        private void ActionPaging()
        {
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;

            using (var service = new RecordTestServiceClient())
            {
                var orders = service.GetRecordTestBySearch(skip, take, SearchProductID, SearchCompositionStd, SearchPMINumber);
                RecordTestExtras.Clear();
                orders.ToList().ForEach(o => RecordTestExtras.Add(new RecordTestExtra { RecordTest = o }));
            }

            CurrentSelectItem = RecordTestExtras.FirstOrDefault();
        }

        #region 属性
        private string searchPMINumber;
        public string SearchPMINumber
        {
            get { return searchPMINumber; }
            set
            {
                if (searchPMINumber == value)
                    return;
                searchPMINumber = value;
                RaisePropertyChanged(() => SearchPMINumber);
            }
        }
        private string searchProductID;
        public string SearchProductID
        {
            get { return searchProductID; }
            set
            {
                if (searchProductID == value)
                    return;
                searchProductID = value;
                RaisePropertyChanged(() => SearchProductID);
            }
        }
        private string searchCompositionStd;
        public string SearchCompositionStd
        {
            get { return searchCompositionStd; }
            set
            {
                if (searchCompositionStd == value)
                    return;
                searchCompositionStd = value;
                RaisePropertyChanged(() => SearchCompositionStd);
            }
        }

        public ObservableCollection<RecordTestExtra> RecordTestExtras { get; set; }
        private RecordTestExtra currentSelectItem;

        public RecordTestExtra CurrentSelectItem
        {
            get { return currentSelectItem; }
            set { currentSelectItem = value; RaisePropertyChanged(nameof(CurrentSelectItem)); }
        }

        #endregion
        #region Commands

        public RelayCommand Report { get; set; }
        public RelayCommand Add { get; set; }
        public RelayCommand<RecordTestExtra> Edit { get; set; }
        public RelayCommand<RecordTestExtra> Doc { get; set; }
        public RelayCommand<RecordTestExtra> SelectionChanged { get; set; }
        public RelayCommand<RecordTestExtra> Duplicate { get; set; }
        public RelayCommand<RecordTestExtra> Label { get; set; }
        public RelayCommand<RecordTestExtra> Check { get; set; }

        public RelayCommand<RecordTestExtra> Compare { get; set; }

        public RelayCommand QuickAdd { get; set; }

        public RelayCommand BatchDoc { get; set; }

        public RelayCommand Output { get; set; }

        public RelayCommand QuickDoc { get; set; }
        public RelayCommand QuickChart { get; set; }
        public RelayCommand QuickLabel { get; set; }


        public RelayCommand<RecordTestExtra> ShowOrderInformation { get; set; }
        public RelayCommand<RecordTestExtra> EnterCSCAN { get; set; }
        public RelayCommand<RecordTestExtra> DeepSearch { get; set; }
        public RelayCommand<RecordTestExtra> SaveJson { get; set; }

        public RelayCommand OneKeyCheck { get; set; }
        #endregion
    }
}
