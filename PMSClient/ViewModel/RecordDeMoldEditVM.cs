using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSClient.MainService;

namespace PMSClient.ViewModel
{
    public class RecordDeMoldEditVM : BaseViewModelEdit
    {
        public RecordDeMoldEditVM()
        {
            Save = new RelayCommand(ActionSave);
            GiveUp = new RelayCommand(ActionGiveUp);
        }
        public RecordDeMoldEditVM(ModelObject model)
        {
            IsNew = model.IsNew;
            CurrentRecordDeMold = model.Model as DcRecordDeMold;

        }

        private void ActionGiveUp()
        {
            GoBack();
        }

        private static void GoBack()
        {
            NavigationService.GoTo(VToken.RecordDeMold);
        }

        private void ActionSave()
        {
            try
            {
                using (var service = new RecordDeMoldServiceClient())
                {
                    if (IsNew)
                    {
                        service.AddRecordDeMold(CurrentRecordDeMold);
                    }
                    else
                    {
                        service.UpdateRecordDeMold(CurrentRecordDeMold);
                    }
                }
                GoBack();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DcRecordDeMold currentRecordDeMold;
        public DcRecordDeMold CurrentRecordDeMold
        {
            get { return currentRecordDeMold; }
            set { currentRecordDeMold = value; RaisePropertyChanged(nameof(CurrentRecordDeMold)); }
        }



    }
}
