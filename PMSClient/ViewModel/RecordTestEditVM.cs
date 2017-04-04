using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using System.Collections.ObjectModel;

namespace PMSClient.ViewModel
{
    public class RecordTestEditVM : BaseViewModelEdit
    {
        public RecordTestEditVM()
        {
            States = new ObservableCollection<string>();
            var states = Enum.GetNames(typeof(PMSCommon.TestResultState));
            states.ToList().ForEach(s => States.Add(s));

            TestTypes = new ObservableCollection<string>();
            var testTypes = Enum.GetNames(typeof(PMSCommon.TestType));
            testTypes.ToList().ForEach(t => TestTypes.Add(t));


            GiveUp = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VToken.RecordTest }));

            Save = new RelayCommand(ActionSave);
        }
        public void SetKeyProperties(ModelObject model)
        {
            CurrentRecordTest = model.Model as DcRecordTest;
            IsNew = model.IsNew;
        }

        private void ActionSave()
        {
            var service = new RecordTestServiceClient();
            if (IsNew)
            {
                service.AddRecordTest(CurrentRecordTest);
            }
            else
            {
                service.UpdateRecordTest(CurrentRecordTest);
            }

            NavigationService.GoTo(new MsgObject() { MsgToken = VToken.RecordTest });
        }
        public ObservableCollection<string> TestTypes { get; set; }
        public ObservableCollection<string> States { get; set; }
        public DcRecordTest CurrentRecordTest { get; set; }


    }
}
