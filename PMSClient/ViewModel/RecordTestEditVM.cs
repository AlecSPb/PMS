using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.PMSMainService;
using System.Collections.ObjectModel;

namespace PMSClient.ViewModel
{
    public class RecordTestEditVM : ViewModelBase
    {
        private bool isNew;
        public RecordTestEditVM(ModelObject model)
        {
            CurrentRecordTestResult = model.Model as DcRecordTest;
            isNew = model.IsNew;
            States = new ObservableCollection<string>();
            var states = Enum.GetNames(typeof(PMSCommon.TestResultState));
            states.ToList().ForEach(s => States.Add(s));

            TestTypes = new ObservableCollection<string>();
            var testTypes = Enum.GetNames(typeof(PMSCommon.TestType));
            testTypes.ToList().ForEach(t => TestTypes.Add(t));


            GiveUp = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VToken.RecordTestResult }));

            Save = new RelayCommand(ActionSave);
        }

        private void ActionSave()
        {
            var service = new RecordTestServiceClient();
            if (isNew)
            {
                service.AddRecordTest(CurrentRecordTestResult);
            }
            else
            {
                service.UpdateRecordTest(CurrentRecordTestResult);
            }

            NavigationService.GoTo(new MsgObject() { MsgToken = VToken.RecordTestResult });
        }
        public ObservableCollection<string> TestTypes { get; set; }
        public ObservableCollection<string> States { get; set; }
        public DcRecordTest CurrentRecordTestResult { get; set; }

        public RelayCommand GiveUp { get; private set; }
        public RelayCommand Save { get; private set; }

    }
}
