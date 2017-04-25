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
            searchPlanDate1 = DateTime.Now.AddDays(-90).Date;
            searchPlanDate2 = DateTime.Now.AddDays(1).Date;
            searchCompositionStd = "";
        }

        private void IntitializeCommands()
        {
            GoToMisson = new RelayCommand(() => NavigationService.GoTo(PMSViews.Misson));
            Refresh = new RelayCommand(ActionRefresh);
            Search = new RelayCommand(ActionSearch);
            PageChanged = new RelayCommand(ActionPaging);
            GiveUp = new RelayCommand(() => NavigationService.GoTo(PMSViews.Plan));
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
            {//TODO:切换搜索
                RecordCount = service.GetPlanWithMissonCheckedCountByDateRange2(SearchPlanDate1, SearchPlanDate2,SearchCompositionStd);
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
                var orders = service.GetPlanWithMissonCheckedByDateRange2(skip, take, SearchPlanDate1, SearchPlanDate2,SearchCompositionStd);
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

        private DateTime searchPlanDate1;
        public DateTime SearchPlanDate1
        {
            get { return searchPlanDate1; }
            set
            {
                if (value < searchPlanDate2)
                {
                    searchPlanDate1 = value;
                    RaisePropertyChanged(nameof(SearchPlanDate1));
                }
            }
        }

        private DateTime searchPlanDate2;
        public DateTime SearchPlanDate2
        {
            get { return searchPlanDate2; }
            set
            {
                if (value > searchPlanDate1)
                {
                    searchPlanDate2 = value;
                    RaisePropertyChanged(nameof(SearchPlanDate2));
                }
            }
        }

        private string searchCompositionStd;
        public string SearchCompositionStd
        {
            get { return searchCompositionStd; }
            set { searchCompositionStd = value; RaisePropertyChanged(nameof(searchCompositionStd)); }
        }

        #endregion

    }
}
