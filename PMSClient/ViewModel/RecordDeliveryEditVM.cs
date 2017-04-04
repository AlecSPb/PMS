using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;

namespace PMSClient.ViewModel
{
    public class RecordDeliveryEditVM : BaseViewModelEdit
    {
        public RecordDeliveryEditVM()
        {
            InitialCommands();
            InitialProperties();
        }
        public void  SetKeyProperties(ModelObject obj)
        {
            IsNew = obj.IsNew;
            CurrentRecordDelivery = obj.Model as DcRecordDelivery;

        }

        private void InitialProperties()
        {
            OrderStates = new ObservableCollection<string>();
            var states = Enum.GetNames(typeof(PMSCommon.TestResultState));
            states.ToList().ForEach(s => OrderStates.Add(s));

            Countries = new ObservableCollection<string>();
            var countries = Enum.GetNames(typeof(PMSCommon.Country));
            countries.ToList().ForEach(s => Countries.Add(s));
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
            var service = new RecordDeliveryServiceClient();
            if (IsNew)
            {
                service.AddRecordDelivery(CurrentRecordDelivery);
            }
            else
            {
                service.UpdateReocrdDelivery(CurrentRecordDelivery);
            }

            NavigationService.GoTo(new MsgObject() { MsgToken = VToken.RecordDelivery });
            NavigationService.Refresh(VToken.RecordDeliveryRefresh);
        }


        #region Properties
        public DcRecordDelivery CurrentRecordDelivery { get; set; }
        public ObservableCollection<string> OrderStates { get; set; }
        public ObservableCollection<string> Countries { get; set; }
        #endregion

    }
}
