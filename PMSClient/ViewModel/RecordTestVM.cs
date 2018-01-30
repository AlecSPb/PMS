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
            QuickAdd = new RelayCommand(ActionQuickAdd, CanQuickAdd);
            Output = new RelayCommand(ActionOutput);
            BatchDoc = new RelayCommand(ActionBatchDoc);
        }

        /// <summary>
        /// 批量生成COA
        /// </summary>
        private void ActionBatchDoc()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定要为选择的项目生成COA报告吗"))
                return;

            try
            {
                WordCOA report = new WordCOA();
                foreach (var item in RecordTestExtras)
                {
                    if (item.IsSelected)
                    {
                        report.SetModel(item.RecordTest);
                        report.Output();
                    }
                }

            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                NavigationService.Status(ex.Message);
            }
            PMSDialogService.ShowYes("提示", "创建完毕，请打开桌面的文件夹，仔细检查内容是否正确");
            NavigationService.Status("创建完毕！");
        }

        private void ActionOutput()
        {
            if (!PMSDialogService.ShowYesNo("询问", "数据导出时间会比较长，请在弹出完成对话框之前不要进行其他操作。\r\n确定明白请点确定开始"))
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
            string titleString = "状态,后续,产品ID,测试类型,成分,缩写,内部编号,PO,客户,要求尺寸,重量,密度,电阻率,缺陷,实际尺寸,样品,创建时间,创建者";
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

            PMSDialogService.ShowYes("数据导出完成到桌面，请右键-打开方式-Excel打开文件");

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

                var sb = new StringBuilder();
                sb.AppendLine(model.RecordTest.Composition);
                sb.AppendLine(model.RecordTest.ProductID);
                sb.AppendLine(model.RecordTest.Dimension);

                var mainContent = sb.ToString();

                //var pageTitle = "产品标签打印输出";
                //var tips = @"点击打开模板按钮，粘贴不同内容到模板合适位置，热压编号是自动生成的，可能不正确，请再自行修改，然后打印标签";
                //var template = "产品标签";
                //var helpimage = "productionlabel.png";
                //PMSHelper.ToolViewModels.LabelOutPut.SetAllParameters(PMSViews.RecordTest, pageTitle,
                //    tips, template, mainContent, helpimage);
                //NavigationService.GoTo(PMSViews.LabelOutPut);

                //2017-12-18
                PMSClient.Tool.LabelCopyWindow lcw = new Tool.LabelCopyWindow
                {
                    LabelInformation = mainContent
                };
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
            return !string.IsNullOrEmpty(SearchProductID) || !string.IsNullOrEmpty(SearchCompositionStd);
        }

        private void ActionAll()
        {
            SearchProductID = SearchCompositionStd = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void ActionDuplicate(RecordTestExtra model)
        {
            if (PMSDialogService.ShowYesNo("请问", "确定复用这条记录？"))
            {
                PMSHelper.ViewModels.RecordTestEdit.SetDuplicate(model.RecordTest);
                NavigationService.GoTo(PMSViews.RecordTestEdit);
            }

        }

        private void ActionEdit(RecordTestExtra model)
        {
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
            SearchCompositionStd = searchProductID = "";

        }
        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 20;
            using (var service = new RecordTestServiceClient())
            {
                RecordCount = service.GetRecordTestCountBySearchInPage(SearchProductID, SearchCompositionStd);
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
                var orders = service.GetRecordTestBySearchInPage(skip, take, SearchProductID, SearchCompositionStd);
                RecordTestExtras.Clear();
                orders.ToList().ForEach(o => RecordTestExtras.Add(new RecordTestExtra { RecordTest = o }));
            }

            CurrentSelectItem = RecordTestExtras.FirstOrDefault();
        }
        #region Commands
        public RelayCommand Report { get; set; }
        public RelayCommand Add { get; set; }
        public RelayCommand<RecordTestExtra> Edit { get; set; }
        public RelayCommand<RecordTestExtra> Doc { get; set; }

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

        public RelayCommand<RecordTestExtra> SelectionChanged { get; set; }
        public RelayCommand<RecordTestExtra> Duplicate { get; set; }
        public RelayCommand<RecordTestExtra> Label { get; set; }
        public RelayCommand QuickAdd { get; set; }

        public RelayCommand BatchDoc { get; set; }

        public RelayCommand Output { get; set; }
        #endregion
    }
}
