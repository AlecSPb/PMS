using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;

namespace PMSClient.ViewModel
{
    public class PlanSearchVM : BaseViewModelPage
    {
        public PlanSearchVM()
        {
            IntitializeCommands();
            IntitializeProperties();
            SetPageParametersWhenConditionChange();
        }

        private void IntitializeProperties()
        {
            PlanWithMissons = new ObservableCollection<DcPlanWithMisson>();
            searchComposition = "";
            searchVHPDate = "";
        }

        private void IntitializeCommands()
        {
            GoToMisson = new RelayCommand(() => NavigationService.GoTo(PMSViews.Misson));
            Refresh = new RelayCommand(ActionRefresh);
            All = new RelayCommand(ActionAll);
            Search = new RelayCommand(ActionSearch);
            PageChanged = new RelayCommand(ActionPaging);
            GiveUp = new RelayCommand(() => NavigationService.GoTo(PMSViews.Plan));
        }

        private void ActionAll()
        {
            SearchVHPDate = SearchComposition = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void ActionRefresh()
        {
            SetPageParametersWhenConditionChange();
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 20;

            using (var service = new MissonServiceClient())
            {
                RecordCount = service.GetPlanExtraCount(SearchVHPDate, SearchComposition);
            }
            //只显示Checked过的计划
            ActionPaging();
        }
        /// <summary>
        /// 分页动作的时候读入数据
        /// </summary>
        private void ActionPaging()
        {
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            //只显示Checked过的计划
            using (var service = new MissonServiceClient())
            {
                var orders = service.GetPlanExtra(skip, take, searchVHPDate, SearchComposition);
                PlanWithMissons.Clear();
                orders.ToList().ForEach(o => PlanWithMissons.Add(o));
            }
        }

        #region Commands
        public RelayCommand GoToMisson { get; set; }
        public RelayCommand Refresh { get; set; }

        public RelayCommand GiveUp { get; set; }


        #endregion

        #region Properties
        public ObservableCollection<DcPlanWithMisson> PlanWithMissons { get; set; }


        private string searchComposition;
        public string SearchComposition
        {
            get { return searchComposition; }
            set { searchComposition = value; RaisePropertyChanged(nameof(searchComposition)); }
        }
        private string searchVHPDate;
        public string SearchVHPDate
        {
            get { return searchVHPDate; }
            set { searchVHPDate = value; RaisePropertyChanged(nameof(searchVHPDate)); }
        }
        #endregion

    }
}
