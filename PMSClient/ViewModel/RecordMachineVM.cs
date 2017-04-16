using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;

namespace PMSClient.ViewModel
{
    public class RecordMachineVM : BaseViewModelPage
    {
        public RecordMachineVM()
        {
            RecordMachines = new ObservableCollection<DcRecordMachine>();
            PageChanged = new RelayCommand(ActionPaging);
            Add = new RelayCommand(ActionAdd,CanAdd);
            Edit = new RelayCommand<DcRecordMachine>(ActionEdit,CanEdit);
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
            Duplicate = new RelayCommand<DcRecordMachine>(ActionDuplicate,CanDuplicate);

            SetPageParametersWhenConditionChange();
        }

        private bool CanDuplicate(DcRecordMachine arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑加工记录");
        }

        private bool CanEdit(DcRecordMachine arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑加工记录");
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑加工记录");
        }

        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }

        private void ActionDuplicate(DcRecordMachine model)
        {
            PMSHelper.ViewModels.RecordMachineEdit.SetByDuplicate(model);
            GoToEditView();
        }

        private void ActionAll()
        {
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void ActionEdit(DcRecordMachine model)
        {
            PMSHelper.ViewModels.RecordMachineEdit.SetEdit(model);
            GoToEditView();
        }

        private static void GoToEditView()
        {
            NavigationService.GoTo(PMSViews.RecordMachineEdit);
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.RecordMachineEdit.SetNew();
            GoToEditView();
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 10;
            var service = new RecordMachineServiceClient();
            RecordCount = service.GetRecordMachineCount();
            service.Close();
            ActionPaging();
        }
        private void ActionPaging()
        {
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var service = new RecordMachineServiceClient();
            var models = service.GetRecordMachines(skip, take);
            service.Close();
            RecordMachines.Clear();
            models.ToList<DcRecordMachine>().ForEach(o => RecordMachines.Add(o));
        }



        public ObservableCollection<DcRecordMachine> RecordMachines { get; set; }

        public RelayCommand Add { get; set; }
        public RelayCommand<DcRecordMachine> Edit { get; set; }
        public RelayCommand<DcRecordMachine> Duplicate { get; set; }
    }
}
