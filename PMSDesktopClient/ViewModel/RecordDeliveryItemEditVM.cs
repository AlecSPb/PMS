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
    public class RecordDeliveryItemEditVM : ViewModelBase
    {
        private bool isNew;
        public RecordDeliveryItemEditVM(ModelObject model)
        {
            isNew = model.IsNew;
            CurrentRecordDeliveryItem = model.Model as DcRecordDeliveryItem;

            States = new ObservableCollection<string>();
            var states = Enum.GetNames(typeof(PMSCommon.SimpleState));
            states.ToList().ForEach(s => States.Add(s));
            InitialCommands();
        }
        private void InitialCommands()
        {
            GiveUp = new RelayCommand(() =>
            {
                NavigationService.GoTo(new MsgObject() { MsgToken = VToken.RecordDelivery });
                NavigationService.Refresh(VToken.RecordDeliveryRefresh);
            });
            Save = new RelayCommand(ActionSave);
        }

        private void ActionSave()
        {
            if (CurrentRecordDeliveryItem != null)
            {
                var service = new RecordDeliveryServiceClient();
                if (isNew)
                {
                    service.AddRecordDeliveryItem(CurrentRecordDeliveryItem);
                }
                else
                {
                    service.UpdateReocrdDeliveryItem(CurrentRecordDeliveryItem);
                }
                NavigationService.GoTo(new MsgObject() { MsgToken = VToken.RecordDelivery });
                NavigationService.Refresh(VToken.RecordDeliveryRefresh);
            }
        }
        public ObservableCollection<string> States { get; set; }
        #region Commands
        public RelayCommand GiveUp { get; set; }
        public RelayCommand Save { get; set; }
        #endregion
        public DcRecordDeliveryItem CurrentRecordDeliveryItem { get; set; }
    }
}
