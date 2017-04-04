using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;

namespace PMSClient.ViewModel
{
    public class RecordMillingEditVM : BaseViewModelEdit
    {
        public RecordMillingEditVM()
        {

            Save = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(ActionSave);
        }
        public void SetKeyProperties(ModelObject model)
        {
            if (model != null)
            {
                IsNew = model.IsNew;
                CurrentRecordMilling = model.Model as DcRecordMilling;
            }

        }

        private void ActionSave()
        {
            if (CurrentRecordMilling != null)
            {
                using (var dc = new RecordMillingServiceClient())
                {
                    if (IsNew)
                    {
                        dc.AddRecordMilling(CurrentRecordMilling);
                    }
                    else
                    {
                        dc.UpdateRecordMilling(CurrentRecordMilling);
                    }

                    //TODO:以后将这里的重复代替掉
                    NavigationService.GoTo(new MsgObject() { MsgToken = VToken.RecordMilling });
                }
            }
        }

        private DcRecordMilling currentRecordMilling;

        public DcRecordMilling CurrentRecordMilling
        {
            get { return currentRecordMilling; }
            set
            {
                currentRecordMilling = value;
                RaisePropertyChanged(nameof(CurrentRecordMilling));
            }
        }

    }
}
