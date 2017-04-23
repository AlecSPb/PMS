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
    public class RecordDeMoldSelectVM : BaseViewModelPage
    {
        public RecordDeMoldSelectVM()
        {
            searchVHPPlanLot = "";
            PageChanged = new RelayCommand(ActionPaging);

            RecordDeMolds = new ObservableCollection<DcRecordDeMold>();
            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<DcRecordDeMold>(ActionEdit, CanEdit);
            SetPageParametersWhenConditionChange();
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);

            Duplicate = new RelayCommand<DcRecordDeMold>(ActionDuplicate, CanDuplicate);
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
    }
}
