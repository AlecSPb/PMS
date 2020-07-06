using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSShipment.TCB;
using GalaSoft.MvvmLight.Messaging;

namespace PMSShipment
{
    public class SetVM : ViewModelBase
    {
        public SetVM()
        {
            TrackStates = new List<string>();
            PMSMethods.SetListDS<PMSCommon.DeliveryItemTCBState>(TrackStates);
            Save = new RelayCommand(ActionSave);
        }

        public void SetCurrentDeliveryItem(DcDeliveryItem model)
        {
            if (model != null)
            {
                CurrentDeliveryItem = model;
            }
        }

        private void ActionSave()
        {
            try
            {
                using (var service = new TCBServiceClient())
                {
                    CurrentDeliveryItem.TrackingHistory = CommonHelper.AppendHistory(CurrentDeliveryItem.TrackingHistory,
                        CurrentDeliveryItem.TCBState);
                    service.UpdateDeliveryItemTCB(CurrentDeliveryItem);
                }
                Messenger.Default.Send<NotificationMessage>(null, "CloseSetWindow");
                Messenger.Default.Send<NotificationMessage>(null, "Refresh");

            }
            catch (Exception)
            {

            }
        }

        public List<string> TrackStates { get; set; }

        private DcDeliveryItem currentDeliveryItem;
        public DcDeliveryItem CurrentDeliveryItem
        {
            get
            {
                return currentDeliveryItem;
            }
            set
            {
                currentDeliveryItem = value;
                RaisePropertyChanged(nameof(CurrentDeliveryItem));
            }
        }

        public RelayCommand Save { get; set; }



    }
}
