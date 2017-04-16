using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using PMSClient.MainService;



namespace PMSClient.ViewModel
{
    public class RecordMillingVM : BaseViewModelPage
    {
        public RecordMillingVM()
        {
            RecordMillings = new ObservableCollection<DcRecordMilling>();
            SetPageParametersWhenConditionChange();
            InitializeCommands();
        }

        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }
        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Add = new RelayCommand(ActionAdd,CanAdd);
            Edit = new RelayCommand<DcRecordMilling>(ActionEdit,CanEdit);
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
        }

        private bool CanEdit(DcRecordMilling arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑制粉记录");
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑制粉记录");
        }

        private void ActionAll()
        {
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void ActionEdit(DcRecordMilling model)
        {
            if (model != null)
            {
                PMSHelper.ViewModels.RecordMillingEdit.SetEdit(model);
                NavigationService.GoTo(PMSViews.RecordMillingEdit);
            }
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.RecordMillingEdit.SetNew();
            NavigationService.GoTo(PMSViews.RecordMillingEdit);
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 10;
            var service = new RecordMillingServiceClient();
            RecordCount = service.GetRecordMillingCount();
            service.Close();
            ActionPaging();
        }
        private void ActionPaging()
        {

            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var service = new RecordMillingServiceClient();
            var models = service.GetRecordMillings(skip, take);
            service.Close();
            RecordMillings.Clear();
            models.ToList<DcRecordMilling>().ForEach(o => RecordMillings.Add(o));
        }



        #region DerivedPart
        public ObservableCollection<DcRecordMilling> RecordMillings { get; set; }

        public RelayCommand Add { get; set; }
        public RelayCommand<DcRecordMilling> Edit { get; set; }
        #endregion
    }
}
