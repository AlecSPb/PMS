using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSDesktopClient.PMSMainService;
using System.Collections.ObjectModel;

namespace PMSDesktopClient.ViewModel
{
    public class RecordTestResultEditVM:ViewModelBase
    {
        private bool isNew;
        public RecordTestResultEditVM(ModelObject model)
        {
            CurrentRecordTestResult = model.Model as DcRecordTestResult;
            isNew = model.IsNew;
            States = new ObservableCollection<string>();
            var states = Enum.GetNames(typeof(PMSCommon.TestResultState));
            states.ToList().ForEach(s => States.Add(s));

            TestTypes = new ObservableCollection<string>();
            var testTypes = Enum.GetNames(typeof(PMSCommon.TestType));
            testTypes.ToList().ForEach(t => TestTypes.Add(t));


            GiveUp = new RelayCommand(() => NavigationService.GoTo(VT.RecordTestResult.ToString()));

            Save = new RelayCommand(ActionSave);
        }

        private void ActionSave()
        {
            var service = new RecordTestResultServiceClient();
            if (isNew)
            {
                service.AddRecordTestResult(CurrentRecordTestResult);
            }
            else
            {
                service.UpdateRecordTestResult(CurrentRecordTestResult);
            }

            NavigationService.GoTo(VT.RecordTestResult.ToString());
        }
        public ObservableCollection<string> TestTypes { get; set; }
        public ObservableCollection<string> States { get; set; }
        public DcRecordTestResult CurrentRecordTestResult { get; set; }

        public RelayCommand GiveUp { get; private set; }
        public RelayCommand Save { get; private set; }

    }
}
