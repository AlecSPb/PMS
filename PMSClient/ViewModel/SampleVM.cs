using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.Sample;

namespace PMSClient.ViewModel
{
    public class SampleVM : BaseViewModelPage
    {
        public SampleVM()
        {
            searchComposition = searchProductID = searchSampleType = "";
            Samples = new ObservableCollection<DcSample>();

            InitializeCommands();

            SampleTypes = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.SampleType>(SampleTypes);
            SampleTypes.Add("");

            SetPageParametersWhenConditionChange();
        }
        public List<string> SampleTypes { get; set; }
        private void InitializeCommands()
        {
            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<DcSample>(ActionEdit, CanEdit);
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
            PageChanged = new RelayCommand(ActionPaging);
            Duplicate = new RelayCommand<DcSample>(ActionDuplicate, CanDuplicate);
            Prepared = new RelayCommand<DcSample>(ActionPrepared, CanEdit);
            Checked = new RelayCommand<DcSample>(ActionChecked, CanEdit);
            Sent = new RelayCommand<DcSample>(ActionSent, CanEdit);

        }

        private void ActionSent(DcSample obj)
        {
            AddProcess(obj, "已发出");
        }

        private void ActionChecked(DcSample obj)
        {
            AddProcess(obj, "已核验");
        }

        private void ActionPrepared(DcSample obj)
        {
            AddProcess(obj, "已准备");
        }

        private void AddProcess(DcSample obj, string type = "")
        {
            if (!PMSDialogService.ShowYesNo("请问", $"确定要为[{obj.ProductID}]追加[{type}]记录吗？"))
                return;

            if (obj == null) return;
            //分析要改变的类型
            PMSCommon.SampleType sampleType = PMSCommon.SampleType.未取样;
            switch (type)
            {
                case "已准备":
                    sampleType = PMSCommon.SampleType.未核验;
                    break;
                case "已核验":
                    sampleType = PMSCommon.SampleType.已核验;
                    break;
                case "已发出":
                    sampleType = PMSCommon.SampleType.已发出;
                    break;
                default:
                    break;
            }

            try
            {
                using (var s = new SampleServiceClient())
                {
                    obj.SampleType = sampleType.ToString();
                    obj.TraceInformation += $"{DateTime.Now.ToString("yyyy-MM-dd")}{sampleType.ToString()};";
                    s.UpdateSample(obj);
                }
                SetPageParametersWhenConditionChange();
            }
            catch (Exception ex)
            {
                PMSDialogService.ShowWarning(ex.Message);
            }
        }

        private void ActionDuplicate(DcSample obj)
        {
            if (PMSDialogService.ShowYesNo("请问", "确定复用这条记录？"))
            {
                if (obj != null)
                {
                    PMSHelper.ViewModels.SampleEdit.SetDuplicate(obj);
                    NavigationService.GoTo(PMSViews.SampleEdit);
                }
            }

        }

        private bool CanDuplicate(DcSample arg)
        {
            return PMSHelper.CurrentSession.IsOKInGroup(AccessGrant.Sample);
        }

        private void ActionAll()
        {
            SearchProductID = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private bool CanEdit(DcSample arg)
        {
            return PMSHelper.CurrentSession.IsOKInGroup(AccessGrant.Sample);
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsOKInGroup(AccessGrant.Sample);
        }

        private void ActionEdit(DcSample model)
        {
            PMSHelper.ViewModels.SampleEdit.SetEdit(model);
            NavigationService.GoTo(PMSViews.SampleEdit);
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.SampleEdit.SetNew();
            NavigationService.GoTo(PMSViews.SampleEdit);
        }

        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }

        private string searchProductID;
        public string SearchProductID
        {
            get { return searchProductID; }
            set { searchProductID = value; RaisePropertyChanged(nameof(SearchProductID)); }
        }
        private string searchComposition;
        public string SearchComposition
        {
            get { return searchComposition; }
            set { searchComposition = value; RaisePropertyChanged(nameof(SearchComposition)); }
        }

        private string searchSampleType;
        public string SearchSampleType
        {
            get { return searchSampleType; }
            set { searchSampleType = value; RaisePropertyChanged(nameof(SearchSampleType)); }
        }
        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 30;
            using (var service = new SampleServiceClient())
            {
                RecordCount = service.GetSampleAllCount(SearchProductID, SearchComposition, SearchSampleType);
            }
            ActionPaging();
        }
        private void ActionPaging()
        {
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            using (var service = new SampleServiceClient())
            {
                var orders = service.GetSampleAll(skip, take,
                    SearchProductID, SearchComposition, SearchSampleType);
                Samples.Clear();
                orders.ToList().ForEach(o => Samples.Add(o));
            }
        }
        #region Commands
        public ObservableCollection<DcSample> Samples { get; set; }

        public RelayCommand Add { get; set; }
        public RelayCommand<DcSample> Edit { get; set; }
        public RelayCommand<DcSample> Duplicate { get; set; }
        public RelayCommand<DcSample> Prepared { get; set; }
        public RelayCommand<DcSample> Checked { get; set; }
        public RelayCommand<DcSample> Sent { get; set; }

        #endregion

    }
}
