using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.SampleService;
using PMSClient.MainService;

namespace PMSClient.ViewModel
{
    public class SampleSelectVM : BaseViewModelSelect
    {
        public SampleSelectVM()
        {
            searchComposition = searchProductID = searchTrackingStage = searchSampleID = searchPMINumber = "";

            Samples = new ObservableCollection<DcSample>();

            InitializeCommands();

            TrackingStages = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.SampleTrackingStage>(TrackingStages);
            TrackingStages.Add("");

            SetPageParametersWhenConditionChange();
        }
        public List<string> TrackingStages { get; set; }
        private void InitializeCommands()
        {
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
            PageChanged = new RelayCommand(ActionPaging);

            SelectionChanged = new RelayCommand<DcSample>(ActionSelectionChanged);

            ShowTestResult = new RelayCommand<string>(ActionShowTestResult);
            SampleTrace = new RelayCommand(ActionSampleTrace, CanAdd);

            Select = new RelayCommand<DcSample>(ActionSelect);
            GiveUp = new RelayCommand(ActionGiveUp);
        }

        private void ActionGiveUp()
        {
            NavigationService.GoTo(requestView);
        }

        private void ActionSelect(DcSample obj)
        {
            if (obj != null)
            {
                switch (requestView)
                {
                    case PMSViews.DeliveryItemEdit:
                        PMSHelper.ViewModels.DeliveryItemEdit.SetBySelect(obj);
                        break;
                    default:
                        break;
                }
            }
            NavigationService.GoTo(requestView);
        }

        private void ActionSampleTrace()
        {
            //检查发货靶材的样品发出情况
            new Components.DeliveryItemSampleCheck.DeliveryItemSampleCheckService().Run();
        }

        private void ActionShowTestResult(string parameter)
        {
            switch (parameter)
            {
                case "ICPOES":
                    SetKeyValue(CurrentSample.ICPOES);
                    break;
                case "GDMS":
                    SetKeyValue(CurrentSample.GDMS);
                    break;
                case "IGA":
                    SetKeyValue(CurrentSample.IGA);
                    break;
                case "Thermal":
                    SetKeyValue(CurrentSample.Thermal);
                    break;
                case "Permittivity":
                    SetKeyValue(CurrentSample.Permittivity);
                    break;
                case "OtherTestResult":
                    SetKeyValue(CurrentSample.OtherTestResult);
                    break;
                default:
                    break;
            }
        }

        private void SetKeyValue(string testResult)
        {
            if (string.IsNullOrEmpty(testResult)) return;
            var dialog = new WPFControls.KeyValueTestResultReadOnly();
            dialog.KeyStrings = testResult;
            dialog.ShowDialog();

        }

        private void ActionSelectionChanged(DcSample obj)
        {
            if (obj != null)
            {
                CurrentSample = obj;
            }
        }

        private void ActionAll()
        {
            SearchProductID = SearchPMINumber = SearchComposition = SearchSampleID = "";
            SearchTrackingStage = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsInGroup("SampleViewEdit");
        }

        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }
        private PMSViews requestView;
        public void SetRequestView(PMSViews request)
        {
            requestView = request;
        }
        private string searchSampleID;
        public string SearchSampleID
        {
            get { return searchSampleID; }
            set { searchSampleID = value; RaisePropertyChanged(nameof(SearchSampleID)); }
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

        private string searchTrackingStage;
        public string SearchTrackingStage
        {
            get { return searchTrackingStage; }
            set { searchTrackingStage = value; RaisePropertyChanged(nameof(SearchTrackingStage)); }
        }

        private string searchPMINumber;
        public string SearchPMINumber
        {
            get { return searchPMINumber; }
            set { searchPMINumber = value; RaisePropertyChanged(nameof(SearchPMINumber)); }
        }

        private DcSample currentSample;
        public DcSample CurrentSample
        {
            get { return currentSample; }
            set { currentSample = value; RaisePropertyChanged(nameof(CurrentSample)); }
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 30;
            using (var service = new SampleServiceClient())
            {
                RecordCount = service.GetSampleAllCount(SearchPMINumber, SearchSampleID, SearchProductID, SearchComposition, SearchTrackingStage);
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
                var orders = service.GetSampleAll(skip, take, SearchPMINumber, SearchSampleID,
                    SearchProductID, SearchComposition, SearchTrackingStage);
                Samples.Clear();
                orders.ToList().ForEach(o => Samples.Add(o));

                CurrentSample = Samples.FirstOrDefault();
            }
        }
        #region Commands
        public ObservableCollection<DcSample> Samples { get; set; }

        public RelayCommand<DcSample> SelectionChanged { get; set; }

        public RelayCommand SampleTrace { get; set; }

        public RelayCommand<String> ShowTestResult { get; set; }


        public RelayCommand<DcSample> Select { get; set; }


        #endregion

    }
}
