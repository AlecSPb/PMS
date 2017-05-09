using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using System.Collections.ObjectModel;
using PMSCommon;

namespace PMSClient.ViewModel
{
    public class RecordDeMoldVM : BaseViewModelPage
    {
        public RecordDeMoldVM()
        {
            searchVHPPlanLot = "";
            PageChanged = new RelayCommand(ActionPaging);

            RecordDeMolds = new ObservableCollection<DcRecordDeMold>();
            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<DcRecordDeMold>(ActionEdit, CanEdit);
            SetPageParametersWhenConditionChange();
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
            Label = new RelayCommand<DcRecordDeMold>(ActionLabel);

            Duplicate = new RelayCommand<DcRecordDeMold>(ActionDuplicate, CanDuplicate);
        }

        private void ActionLabel(DcRecordDeMold model)
        {
            if (model != null)
            {

                var sb = new StringBuilder();
                sb.AppendLine(model.Composition);
                sb.AppendLine(model.Dimension);
                sb.AppendLine(model.PlanType);
                sb.AppendLine(model.VHPPlanLot);

                var mainContent = sb.ToString();

                var pageTitle = "热压毛坯标签打印输出";
                var tips = @"点击打开模板按钮，粘贴不同内容到模板合适位置，热压编号是自动生成的，可能不正确，请再自行修改，然后打印标签，一般生成2张标签，一份用于取模，一份用于加工后补救被水浸泡的原标签";
                var template = "毛坯标签";
                var helpimage = "productionlabel.png";
                PMSHelper.ToolViewModels.LabelOutPut.SetAllParameters(PMSViews.RecordDeMold, pageTitle,
                    tips, template, mainContent, helpimage);
                NavigationService.GoTo(PMSViews.LabelOutPut);
            }
        }

        private bool CanDuplicate(DcRecordDeMold arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑取模记录");
        }

        private bool CanEdit(DcRecordDeMold arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑取模记录");
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑取模记录");
        }

        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }
        private void ActionAll()
        {
            SearchVHPPlanLot = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }


        private static void GoToEditView()
        {
            NavigationService.GoTo(PMSViews.RecordDeMoldEdit);
        }

        private void ActionDuplicate(DcRecordDeMold model)
        {
            if (PMSDialogService.ShowYesNo("请问", "确定复用这条记录？"))
            {
                PMSHelper.ViewModels.RecordDeMoldEdit.SetByDuplicate(model);
                GoToEditView();
            }
        }
        private void ActionEdit(DcRecordDeMold model)
        {
            PMSHelper.ViewModels.RecordDeMoldEdit.SetEdit(model);
            GoToEditView();
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.RecordDeMoldEdit.SetNew();
            GoToEditView();
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 20;
            var service = new RecordDeMoldServiceClient();
            RecordCount = service.GetRecordDeMoldsCountByVHPPlanLot(SearchVHPPlanLot);
            service.Close();
            ActionPaging();
        }
        private void ActionPaging()
        {

            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var service = new RecordDeMoldServiceClient();
            var models = service.GetRecordDeMoldsByVHPPlanLot(skip, take, SearchVHPPlanLot);
            service.Close();
            RecordDeMolds.Clear();
            models.ToList().ForEach(o => RecordDeMolds.Add(o));
            CurrentRecordDeMold = RecordDeMolds.FirstOrDefault();
        }
        private DcRecordDeMold currentRecordDeMold;

        public DcRecordDeMold CurrentRecordDeMold
        {
            get { return currentRecordDeMold; }
            set { currentRecordDeMold = value; RaisePropertyChanged(nameof(CurrentRecordDeMold)); }
        }


        private string searchVHPPlanLot;
        public string SearchVHPPlanLot
        {
            get { return searchVHPPlanLot; }
            set { searchVHPPlanLot = value; RaisePropertyChanged(nameof(SearchVHPPlanLot)); }
        }
        public ObservableCollection<DcRecordDeMold> RecordDeMolds { get; set; }

        public RelayCommand Add { get; set; }
        public RelayCommand<DcRecordDeMold> Edit { get; set; }
        public RelayCommand<DcRecordDeMold> Duplicate { get; set; }
        public RelayCommand<DcRecordDeMold> Label { get; set; }
    }
}
