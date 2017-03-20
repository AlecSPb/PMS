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
        private bool isNew;
        public RecordMillingEditVM(ModelObject model)
        {
            if (model != null)
            {
                isNew = model.IsNew;
                CurrentRecordMiiling = model.Model as DcRecordMilling;
            }

            Save = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(ActionSave);
        }

        private void ActionSave()
        {
            if (CurrentRecordMiiling != null)
            {
                using (var dc = new RecordMillingServiceClient())
                {
                    if (isNew)
                    {
                        dc.AddRecordMilling(CurrentRecordMiiling);
                    }
                    else
                    {
                        dc.UpdateRecordMilling(CurrentRecordMiiling);
                    }

                    //TODO:以后将这里的重复代替掉
                    NavigationService.GoTo(new MsgObject() { MsgToken = VToken.Navigation });
                }
            }
        }

        private DcRecordMilling currentRecordMilling;

        public DcRecordMilling CurrentRecordMiiling
        {
            get { return currentRecordMilling; }
            set { currentRecordMilling = value; RaisePropertyChanged(nameof(CurrentRecordMiiling)); }
        }

    }
}
