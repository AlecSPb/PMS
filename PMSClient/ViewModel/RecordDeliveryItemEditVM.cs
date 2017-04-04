using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSClient.MainService;
using System.Collections.ObjectModel;

namespace PMSClient.ViewModel
{
    public class RecordDeliveryItemEditVM : BaseViewModelEdit
    {
        public RecordDeliveryItemEditVM()
        {
            States = new ObservableCollection<string>();
            var states = Enum.GetNames(typeof(PMSCommon.SimpleState));
            states.ToList().ForEach(s => States.Add(s));
            InitialCommands();
        }
        public RecordDeliveryItemEditVM(ModelObject model)
        {
            IsNew = model.IsNew;
            CurrentRecordDeliveryItem = model.Model as DcRecordDeliveryItem;
        }
        private void InitialCommands()
        {
            GiveUp = new RelayCommand(() =>
            {
                NavigationService.GoTo(new MsgObject() { MsgToken = VToken.RecordDelivery });
                NavigationService.Refresh(VToken.RecordDeliveryItemRefresh);
            });
            Save = new RelayCommand(ActionSave);
        }

        private void ActionSave()
        {
            if (CurrentRecordDeliveryItem != null)
            {
                var service = new RecordDeliveryServiceClient();
                if (IsNew)
                {
                    service.AddRecordDeliveryItem(CurrentRecordDeliveryItem);
                }
                else
                {
                    service.UpdateReocrdDeliveryItem(CurrentRecordDeliveryItem);
                }
                NavigationService.GoTo(new MsgObject() { MsgToken = VToken.RecordDelivery });
                NavigationService.Refresh(VToken.RecordDeliveryItemRefresh);
            }
        }
        public ObservableCollection<string> States { get; set; }
        public DcRecordDeliveryItem CurrentRecordDeliveryItem { get; set; }
    }
}
