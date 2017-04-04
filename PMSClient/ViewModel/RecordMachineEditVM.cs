using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;
using GalaSoft.MvvmLight.CommandWpf;

namespace PMSClient.ViewModel
{
    public class RecordMachineEditVM : BaseViewModelEdit
    {
        public RecordMachineEditVM()
        {
            GiveUp = new RelayCommand(ActionGiveUp);
            Save = new RelayCommand(ActionSave);
        }
        public RecordMachineEditVM(ModelObject model) : this()
        {
            IsNew = model.IsNew;
            CurrentRecordMachine = model.Model as DcRecordMachine;
        }

        private void ActionSave()
        {
            try
            {
                using (var service = new RecordMachineServiceClient())
                {
                    if (IsNew)
                    {
                        service.AddRecordMachine(CurrentRecordMachine);
                    }
                    else
                    {
                        service.UpdateRecordMachine(CurrentRecordMachine);
                    }
                }

                GoBack();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void ActionGiveUp()
        {
            GoBack();
        }

        private static void GoBack()
        {
            NavigationService.GoTo(VToken.RecordMachine);
        }

        private DcRecordMachine currentRecordMachine;

        public DcRecordMachine CurrentRecordMachine
        {
            get { return currentRecordMachine; }
            set { currentRecordMachine = value; RaisePropertyChanged(nameof(CurrentRecordMachine)); }
        }


    }
}
