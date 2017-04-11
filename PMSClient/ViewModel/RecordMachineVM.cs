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
            Add = new RelayCommand(ActionAdd);
            Edit = new RelayCommand<DcRecordMachine>(ActionEdit);
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
            Duplicate = new RelayCommand<DcRecordMachine>(ActionDuplicate);

            SetPageParametersWhenConditionChange();
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
            ActionPaging();
        }
        private void ActionPaging()
        {
            var service = new RecordMachineServiceClient();
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var models = service.GetRecordMachines(skip, take);
            RecordMachines.Clear();
            models.ToList<DcRecordMachine>().ForEach(o => RecordMachines.Add(o));
        }



        public ObservableCollection<DcRecordMachine> RecordMachines { get; set; }

        public RelayCommand Add { get; set; }
        public RelayCommand<DcRecordMachine> Edit { get; set; }
        public RelayCommand<DcRecordMachine> Duplicate { get; set; }
    }
}
