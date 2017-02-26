using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSDesktopClient.PMSMainService;
using System.Collections.ObjectModel;

namespace PMSDesktopClient.ViewModel
{
    public class RecordVHPEditVM : ViewModelBase
    {
        private bool isNew;
        public RecordVHPEditVM(ModelObject model)
        {
            isNew = model.IsNew;
            CurrentRecordVHP = model.Model as DcRecordVHP;

            States = new ObservableCollection<string>();
            var states = BDInstance.CommonStates;
            states.ToList().ForEach(s => States.Add(s));


            GiveUp = new RelayCommand(() =>
            {
                NavigationService.GoTo(new MsgObject() { MsgToken = VToken.RecordVHP });
                NavigationService.Refresh(VToken.RecordVHPRefresh);
            });
            Save = new RelayCommand(ActionSave);
        }

        private void ActionSave()
        {
            var service = new RecordVHPServiceClient();
            if (isNew)
            {
                service.AddRecordVHP(CurrentRecordVHP);
            }
            else
            {
                service.UpdateReocrdVHP(CurrentRecordVHP);
            }
            NavigationService.GoTo(new MsgObject() { MsgToken = VToken.RecordVHP });
            NavigationService.Refresh(VToken.RecordVHPRefresh);
        }

        public DcRecordVHP CurrentRecordVHP { get; set; }

        public ObservableCollection<string> States { get; set; }
        public RelayCommand GiveUp { get; set; }
        public RelayCommand Save { get; set; }
    }
}
