using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSCommon;
using PMSClient.MainService;
using System.Collections.ObjectModel;
using System.Windows;
using System.IO;
using PMSClient.ToolWindow;

namespace PMSClient.ViewModel
{
    public class OrderVM : BaseViewModelPage
    {
        public OrderVM()
        {
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }

        /// <summary>
        ///综合搜索
        /// </summary>
        /// <param name="pminumber"></param>
        public void SetSearch(string pminumber)
        {
            SearchCustomer = "";
            SearchCompositionStandard = "";
            SearchPMINumber = pminumber;
            SetPageParametersWhenConditionChange();
        }



        /// <summary>
        /// 用于编辑后刷新订单调用
        /// </summary>
        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }


        private void InitializeProperties()
        {
            searchCustomer = "";
            searchCompositionStandard = "";
            searchPMINumber = "";
            MainOrders = new ObservableCollection<DcOrder>();
            UnFinishedOrderCount = 0;
        }
        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);

            Add = new RelayCommand(ActionAdd, CanAdd);

            Edit = new RelayCommand<DcOrder>(order =>
            {
                //修改提示
                if (!PMSDialogService.ShowYesNo("请问", "确定要【修改】这个订单吗？"))
                    return;
                PMSHelper.ViewModels.OrderEdit.SetEdit(order);
                NavigationService.GoTo(PMSViews.OrderEdit);
            }, CanEdit);

            Duplicate = new RelayCommand<DcOrder>(ActionDuplicate, CanEdit);
            Check = new RelayCommand<DcOrder>(ActionCheck, CanCheck);
            SelectionChanged = new RelayCommand<DcOrder>(ActionSelectionChanged);
            OnlyUnCompleted = new RelayCommand(ActionOnlyUnCompleted);

            Output = new RelayCommand(ActionOutput);

            SampleSheet = new RelayCommand(ActionSampleSheet);
        }

        private void ActionSampleSheet()
        {
            if (!PMSDialogService.ShowYesNo("请问", "即将输出 未完成订单中 包含样品需求的订单，继续？"))
                return;

            try
            {
                var report = new ReportsHelperNew.ReportSampleSheet();
                report.Intialize("样品需求");
                report.Output();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
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
            using (var service = new OrderServiceClient())
            {
                recordCount = service.GetOrderCount(SearchCustomer, SearchCompositionStandard, SearchPMINumber);
            }

            int pageCount = recordCount / PageSize + (recordCount % PageSize == 0 ? 0 : 1);

            int skip = 0, take = 0;
            take = pageSize;
            skip = (pageIndex - 1) * pageSize;

            string outputfile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
                , "导出数据-订单" + DateTime.Now.ToString("yyyyMMddmmhhss") + ".csv");
            StreamWriter sw = new StreamWriter(new FileStream(outputfile, FileMode.Append), System.Text.Encoding.GetEncoding("GB2312"));
            string titleString = "订单状态,优先级,创建时间,交付日期,策略,内部编号,客户,标准成分,缩写,纯度,PO,产品类型,数量,单位,加工尺寸,加工要求,样品要求,可接受缺陷,完成日期";
            sw.WriteLine(titleString);
            using (var service = new OrderServiceClient())
            {
                try
                {
                    string outputString = "";
                    while (pageIndex <= pageCount)
                    {
                        var models = service.GetOrders(skip, take, SearchCustomer, SearchCompositionStandard, SearchPMINumber);
                        outputString = PMSOuputHelper.GetOrderOupput(models);
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

        private void ActionOnlyUnCompleted()
        {
            NavigationService.GoTo(PMSViews.OrderUnCompleted);
        }

        private void ActionSelectionChanged(DcOrder model)
        {
            if (model != null)
            {
                CurrentOrder = model;
            }
        }

        /// <summary>
        /// 核验订单权限编码=编辑订单核验
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private bool CanCheck(DcOrder arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditOrderCheck);
        }

        private void ActionCheck(DcOrder order)
        {
            if (order != null)
            {
                //PMSHelper.ViewModels.OrderCheckEdit.SetEdit(order);
                //NavigationService.GoTo(PMSViews.OrderCheckEdit);
                //使用窗口的方式进行核验
                OrderCheckDialog dg = new OrderCheckDialog();
                dg.CurrentOrder = order;
                dg.ShowDialog();
            }
        }
        #region 权限控制代码=编辑订单
        private bool CanEdit(DcOrder arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑订单");
        }
        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑订单");
        }
        #endregion


        private void ActionDuplicate(DcOrder order)
        {
            if (PMSDialogService.ShowYesNo("请问", "确定复用这条记录？"))
            {
                if (order != null)
                {
                    PMSHelper.ViewModels.OrderEdit.SetDuplicate(order);
                    NavigationService.GoTo(PMSViews.OrderEdit);
                }
            }

        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.OrderEdit.SetNew();
            NavigationService.GoTo(PMSViews.OrderEdit);
        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchCustomer) && string.IsNullOrEmpty(SearchCompositionStandard) && string.IsNullOrEmpty(SearchPMINumber));
        }

        private void ActionAll()
        {
            SearchCustomer = "";
            SearchCompositionStandard = "";
            SearchPMINumber = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void SetPageParametersWhenConditionChange()
        {
            try
            {
                PageIndex = 1;
                PageSize = 30;
                using (var service = new OrderServiceClient())
                {
                    RecordCount = service.GetOrderCount(SearchCustomer, SearchCompositionStandard, SearchPMINumber);
                    //获得未完成的订单数
                    UnFinishedOrderCount = service.GetOrderUnFinishedCount();
                    service.Close();
                }
                ActionPaging();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
        /// <summary>
        /// 分页动作的时候读入数据
        /// </summary>
        private void ActionPaging()
        {
            try
            {
                int skip, take = 0;
                skip = (PageIndex - 1) * PageSize;
                take = PageSize;
                var service = new OrderServiceClient();
                var orders = service.GetOrders(skip, take, SearchCustomer, SearchCompositionStandard, SearchPMINumber);
                service.Close();
                MainOrders.Clear();
                orders.ToList().ForEach(o => MainOrders.Add(o));
                CurrentOrder = MainOrders.FirstOrDefault();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }

        }


        #region Proeperties
        private int unFinishedOrderCount;
        public int UnFinishedOrderCount
        {
            get { return unFinishedOrderCount; }
            set
            {
                unFinishedOrderCount = value;
                RaisePropertyChanged(nameof(UnFinishedOrderCount));
            }
        }
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
        private string searchCustomer;
        public string SearchCustomer
        {
            get { return searchCustomer; }
            set
            {
                if (searchCustomer == value)
                    return;
                searchCustomer = value;
                RaisePropertyChanged(() => SearchCustomer);
            }
        }
        private string searchCompositionStandard;
        public string SearchCompositionStandard
        {
            get { return searchCompositionStandard; }
            set
            {
                if (searchCompositionStandard == value)
                    return;
                searchCompositionStandard = value;
                RaisePropertyChanged(() => SearchCompositionStandard);
            }
        }

        private DcOrder currentOrder;

        public DcOrder CurrentOrder
        {
            get { return currentOrder; }
            set { currentOrder = value; RaisePropertyChanged(nameof(CurrentOrder)); }
        }




        private ObservableCollection<DcOrder> mainOrders;
        public ObservableCollection<DcOrder> MainOrders
        {
            get { return mainOrders; }
            set { mainOrders = value; RaisePropertyChanged(nameof(MainOrders)); }
        }

        #endregion

        #region Commands
        public RelayCommand Add { get; private set; }
        public RelayCommand<DcOrder> Edit { get; private set; }
        public RelayCommand<DcOrder> Duplicate { get; private set; }
        public RelayCommand<DcOrder> Check { get; private set; }
        public RelayCommand<DcOrder> SelectionChanged { get; set; }
        public RelayCommand OnlyUnCompleted { get; set; }

        public RelayCommand Output { get; set; }

        public RelayCommand SampleSheet { get; set; }
        #endregion
    }
}
