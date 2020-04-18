using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using System.Collections.ObjectModel;

using PMSClient.OutsideProcessService;
using System.Windows;

namespace PMSClient.ViewModel
{
    public class OutsideProcessVM : BaseViewModelPage
    {
        public OutsideProcessVM()
        {
            InitializeProperties();
            InitializeCommands();
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
            Edit = new RelayCommand<DcOutsideProcess>(ActionEdit, CanEdit);
            Duplicate = new RelayCommand<DcOutsideProcess>(ActionDuplicate, CanDuplicate);
            Print1 = new RelayCommand(ActionPrint1, CanPrint1);
            Print2 = new RelayCommand(ActionPrint2, CanPrint1);
            ScanAdd = new RelayCommand(ActionScanAdd, CanScanAdd);

            Send = new RelayCommand<DcOutsideProcess>(ActionSend, CanEdit);
            Receive = new RelayCommand<DcOutsideProcess>(ActionReceive, CanEdit);
            Fail = new RelayCommand<DcOutsideProcess>(ActionFail, CanEdit);
        }

        private void ActionFail(DcOutsideProcess obj)
        {
            if (obj != null)
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定要添加此记录到报废记录中吗？"))
                {
                    return;
                }
                if (obj != null)
                {
                    try
                    {
                        using (var service = new FailureService.FailureServiceClient())
                        {
                            int check_exist_count = service.GetFailuresCountByProductID(obj.ProductID);

                            if (check_exist_count == 0)
                            {
                                var model = VMHelper.FailureVMHelper.GetNewFailure();
                                model.ProductID = obj.ProductID;
                                model.Stage = "外协";
                                model.Composition = obj.Composition;
                                model.Details = obj.PMINumber;
                                model.Remark = obj.Remark;
                                model.Problem = "外协失败";
                                model.Process = "无";

                                service.AddFailure(model);
                                PMSDialogService.Show("添加成功");

                            }
                            else
                            {
                                PMSDialogService.ShowWarning("报废库已存在");
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        private void ActionReceive(DcOutsideProcess obj)
        {
            AddProcess(obj, "Receive");
        }

        private void ActionSend(DcOutsideProcess obj)
        {
            AddProcess(obj, "Send");
        }

        private void AddProcess(DcOutsideProcess obj, string type = "Send")
        {
            string ask_msg = type == "Send" ? "发出" : "取回";
            if (!PMSDialogService.ShowYesNo("请问", $"确定要为[{obj.ProductID}]追加[{ask_msg}]记录吗？"))
                return;
            if (obj == null) return;

            try
            {
                using (var s = new OutsideProcessServiceClient())
                {
                    string type_value = "";
                    if (type == "Send")
                    {
                        type_value = Application.Current.Resources["ButtonSended"] as string;
                        obj.State = PMSCommon.OutsideProcessState.已发出.ToString();
                    }
                    else
                    {
                        type_value = Application.Current.Resources["ButtonGetBack"] as string;
                        obj.State = PMSCommon.OutsideProcessState.已取回.ToString();
                    }
                    obj.ProgressBar += $"{DateTime.Now.ToString("yyyy-MM-dd")}{type_value};";
                    s.Update(obj);
                }
                SetPageParametersWhenConditionChange();
            }
            catch (Exception ex)
            {
                PMSDialogService.ShowWarning(ex.Message);
            }
        }


        private void ActionScanAdd()
        {
            var tool = new DataProcess.ScanInput.ScanInput();
            tool.TxtValue.Visibility = System.Windows.Visibility.Collapsed;
            tool.CboValue.Visibility = System.Windows.Visibility.Collapsed;

            tool.TxtText.Visibility = System.Windows.Visibility.Visible;
            tool.CboText.Visibility = System.Windows.Visibility.Visible;
            tool.TxtText.Text = "委托方";

            var context = new DataProcess.ScanInput.ScanInputOutsideProcessVM();
            var processors = new List<string>();
            PMSMethods.SetListDS<PMSCommon.OutsideProcessor>(processors);
            context.Texts.Clear();
            processors.ForEach(i => context.Texts.Add(i));

            context.CurrentText = PMSCommon.OutsideProcessor.成都炬科光学.ToString();
            tool.DataContext = context;
            tool.Show();
        }

        private bool CanScanAdd()
        {
            return PMSHelper.CurrentSession.IsInGroup(AccessGrant.EditOutsideProcess);
        }

        private void ActionPrint2()
        {
            if (!PMSDialogService.ShowYesNo("警告", "确定要生成[已发出]橙红色的外协加工清单吗？" +
                "\r\n!!!外协加工单要交给承运人核对"))
                return;
            try
            {
                var word = new ReportsHelperNew.ReportOutsideProcessSheetOut();
                word.Intialize("外协清单", 50);
                word.Output();
            }
            catch (Exception ex)
            {
                PMSDialogService.ShowWarning(ex.Message);
            }
        }

        private void ActionPrint1()
        {
            if (!PMSDialogService.ShowYesNo("警告", "确定要生成[未发出]绿色的外协加工清单吗？" +
                "\r\n!!!外协加工单要交给承运人核对"))
                return;
            try
            {
                var word = new ReportsHelperNew.ReportOutsideProcessSheetIn();
                word.Intialize("外协清单", 50);
                word.Output();
            }
            catch (Exception ex)
            {
                PMSDialogService.ShowWarning(ex.Message);
            }
        }

        private bool CanPrint1()
        {
            return PMSHelper.CurrentSession.IsInGroup(AccessGrant.ViewOutsideProcess);
        }

        private bool CanDuplicate(DcOutsideProcess arg)
        {
            return PMSHelper.CurrentSession.IsInGroup(AccessGrant.EditOutsideProcess);
        }

        private void ActionDuplicate(DcOutsideProcess model)
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定复用这条记录吗？"))
            {
                return;
            }
            PMSHelper.ViewModels.OutsideProcessEdit.SetDuplicate(model);
            NavigationService.GoTo(PMSViews.OutsideProcessEdit);
        }

        private bool CanEdit(DcOutsideProcess arg)
        {
            return PMSHelper.CurrentSession.IsInGroup(AccessGrant.EditOutsideProcess);

        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsInGroup(AccessGrant.EditOutsideProcess);
        }


        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchProductID) && string.IsNullOrEmpty(SearchComposition));
        }

        private void ActionAll()
        {
            searchProductID = searchComposition = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void ActionEdit(DcOutsideProcess model)
        {
            PMSHelper.ViewModels.OutsideProcessEdit.SetEdit(model);
            NavigationService.GoTo(PMSViews.OutsideProcessEdit);
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.OutsideProcessEdit.SetNew();
            NavigationService.GoTo(PMSViews.OutsideProcessEdit);
        }

        private void InitializeProperties()
        {
            OutsideProcesses = new ObservableCollection<DcOutsideProcess>();
            searchProductID = searchComposition = "";

        }
        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 30;
            using (var service = new OutsideProcessServiceClient())
            {
                RecordCount = service.GetOutsideProcessCount(SearchProductID, SearchComposition);
            }
            ActionPaging();
        }
        private void ActionPaging()
        {
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            using (var service = new OutsideProcessServiceClient())
            {
                var orders = service.GetOutsideProcess(skip, take, SearchProductID, SearchComposition);
                OutsideProcesses.Clear();
                orders.ToList().ForEach(o => OutsideProcesses.Add(o));
            }
        }
        #region Commands
        public RelayCommand Add { get; set; }
        public RelayCommand<DcOutsideProcess> Edit { get; set; }


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
        private string searchComposition;
        public string SearchComposition
        {
            get { return searchComposition; }
            set
            {
                if (searchComposition == value)
                    return;
                searchComposition = value;
                RaisePropertyChanged(() => SearchComposition);
            }
        }

        public ObservableCollection<DcOutsideProcess> OutsideProcesses { get; set; }
        #endregion
        public RelayCommand<DcOutsideProcess> Duplicate { get; set; }
        public RelayCommand<DcOutsideProcess> Send { get; set; }
        public RelayCommand<DcOutsideProcess> Receive { get; set; }
        public RelayCommand<DcOutsideProcess> Fail { get; set; }
        public RelayCommand Print1 { get; set; }
        public RelayCommand Print2 { get; set; }
        public RelayCommand ScanAdd { get; set; }

    }
}
