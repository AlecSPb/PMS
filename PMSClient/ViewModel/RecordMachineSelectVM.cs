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
    public class RecordMachineSelectVM : BaseViewModelSelect
    {
        public RecordMachineSelectVM()
        {
            searchVHPPlanLot = "";
            RecordMachines = new ObservableCollection<DcRecordMachine>();
            PageChanged = new RelayCommand(ActionPaging);
            GiveUp = new RelayCommand(GoBack);
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
            Select = new RelayCommand<DcRecordMachine>(ActionSelect);
            SetPageParametersWhenConditionChange();
        }

        private PMSViews requestView;
        public void SetRequestView(PMSViews view)
        {
            requestView = view;
        }

        private void GoBack()
        {
            NavigationService.GoTo(requestView);
        }

        private void ActionSelect(DcRecordMachine model)
        {
            if (model != null)
            {
                switch (requestView)
                {
                    case PMSViews.RecordTestEdit:
                        PMSHelper.ViewModels.RecordTestEdit.SetDimensionActual(model);
                        break;
                    default:
                        break;
                }
                GoBack();
            }
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
            NavigationService.GoTo(PMSViews.RecordMachineEdit);
        }


        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 20;
            var service = new RecordMachineServiceClient();
            RecordCount = service.GetRecordMachineCountByVHPPlanLot(SearchVHPPlanLot);
            service.Close();
            ActionPaging();
        }
        private void ActionPaging()
        {
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var service = new RecordMachineServiceClient();
            var models = service.GetRecordMachinesByVHPPlanLot(skip, take, SearchVHPPlanLot);
            service.Close();
            RecordMachines.Clear();
            models.ToList().ForEach(o => RecordMachines.Add(o));
        }


        private string searchVHPPlanLot;
        public string SearchVHPPlanLot
        {
            get { return searchVHPPlanLot; }
            set { searchVHPPlanLot = value; RaisePropertyChanged(nameof(SearchVHPPlanLot)); }
        }

        public ObservableCollection<DcRecordMachine> RecordMachines { get; set; }

        public RelayCommand<DcRecordMachine> Select { get; set; }
    }
}
