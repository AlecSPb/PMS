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
    public class PlanVM : BaseViewModelPage
    {
        public PlanVM()
        {
            IntitializeCommands();
            IntitializeProperties();
            SetPageParametersWhenConditionChange();
        }

        private void ActionRefresh(string obj)
        {
            SetPageParametersWhenConditionChange();
        }

        private void IntitializeProperties()
        {
            PlanWithMissons = new ObservableCollection<DcPlanWithMisson>();
        }

        private void IntitializeCommands()
        {
            GoToMisson = new RelayCommand(() => NavigationService.GoTo(PMSViews.Misson));
            All = new RelayCommand(ActionAll);
            PageChanged = new RelayCommand(ActionPaging);
            GoToSearchPlan = new RelayCommand(() => NavigationService.GoTo(PMSViews.PlanSearch));
            Label = new RelayCommand<DcPlanWithMisson>(ActionLabel);
            SearchMisson = new RelayCommand<DcPlanWithMisson>(ActionSearchMisson);
            SelectionChanged = new RelayCommand<DcPlanWithMisson>(ActionSelectionChanged);
        }

        private void ActionSelectionChanged(DcPlanWithMisson model)
        {
            if (model!=null)
            {
                CurrentPlanWithMisson = model;
            }
        }

        private void ActionSearchMisson(DcPlanWithMisson model)
        {
            if (model != null)
            {
                PMSHelper.ViewModels.Misson.SetSearchCondition("", model.Misson.PMINumber);
                NavigationService.GoTo(PMSViews.Misson);
            }
        }

        private void ActionLabel(DcPlanWithMisson model)
        {
            if (model != null)
            {

                var sb = new StringBuilder();
                sb.AppendLine(model.Misson.CompositionStandard);
                sb.AppendLine(model.Misson.Dimension);
                sb.AppendLine(UsefulPackage.PMSTranslate.PlanLot(model));

                var mainContent = sb.ToString();

                var pageTitle = "热压产品毛坯标签打印输出";
                var tips = @"点击打开模板按钮，粘贴不同内容到模板合适位置，热压编号是自动生成的，可能不正确，请再自行修改，然后打印标签\r\n
一般生成2张标签，一份用于取模，一份用于加工后补救被水浸泡的原标签";
                var template = "毛坯标签";
                var helpimage = "productionlabel.png";
                PMSHelper.ToolViewModels.LabelOutPut.SetAllParameters(PMSViews.Plan, pageTitle,
                    tips, template, mainContent, helpimage);
                NavigationService.GoTo(PMSViews.LabelOutPut);
            }
        }
        private void ActionAll()
        {
            SetPageParametersWhenConditionChange();
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 20;
            //var service = new MissonServiceClient();
            //只显示Checked过的计划
            //RecordCount = service.GetMissonWithPlanCheckedCount();
            using (var service = new MissonServiceClient())
            {
                RecordCount = service.GetPlanWithMissonCheckedCount();
            }
            ActionPaging();
        }
        /// <summary>
        /// 分页动作的时候读入数据
        /// </summary>
        private void ActionPaging()
        {
            //var service = new MissonServiceClient();
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            //只显示Checked过的计划
            //var orders = service.GetMissonWithPlanChecked(skip, take);
            using (var service = new MissonServiceClient())
            {
                var orders = service.GetPlanWithMissonChecked(skip, take);
                PlanWithMissons.Clear();
                orders.ToList().ForEach(o => PlanWithMissons.Add(o));
            }
            CurrentPlanWithMisson = PlanWithMissons.FirstOrDefault();
        }

        private DcPlanWithMisson currentPlanWithMisson;

        public DcPlanWithMisson CurrentPlanWithMisson
        {
            get { return currentPlanWithMisson; }
            set { currentPlanWithMisson = value; RaisePropertyChanged(nameof(CurrentPlanWithMisson)); }
        }


        #region Commands
        public RelayCommand GoToMisson { get; set; }
        public RelayCommand GoToSearchPlan { get; set; }
        public RelayCommand<DcPlanWithMisson> Label { get; set; }
        public RelayCommand<DcPlanWithMisson> SearchMisson { get; set; }
        public RelayCommand<DcPlanWithMisson> SelectionChanged{ get; set; }
        #endregion

        #region Properties
        public ObservableCollection<DcPlanWithMisson> PlanWithMissons { get; set; }

        #endregion

    }
}
