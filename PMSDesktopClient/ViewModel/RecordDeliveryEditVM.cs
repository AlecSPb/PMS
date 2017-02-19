using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSDesktopClient.PMSMainService;
using System.Collections.ObjectModel;

namespace PMSDesktopClient.ViewModel
{
    public class RecordDeliveryEditVM : ViewModelBase
    {
        private bool isNew;
        public RecordDeliveryEditVM(ModelObject obj)
        {
            isNew = obj.IsNew;
            CurrentRecordDelivery = obj.Model as DcRecordDelivery;
            InitialCommands();
            InitialProperties();
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
            GiveUp = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VT.RecordDelivery }));
            Save = new RelayCommand(ActionSave);
        }

        private void ActionSave()
        {
            var service = new RecordDeliveryServiceClient();
            if (isNew)
            {
                service.AddRecordDelivery(CurrentRecordDelivery);
            }
            else
            {
                service.UpdateReocrdDelivery(CurrentRecordDelivery);
            }

            NavigationService.GoTo(new MsgObject() { MsgToken = VT.RecordDelivery });
        }


        #region Properties
        public DcRecordDelivery CurrentRecordDelivery { get; set; }
        public ObservableCollection<string> OrderStates { get; set; }
        public ObservableCollection<string> Countries { get; set; }
        #endregion

        #region Commands
        public RelayCommand GiveUp { get; set; }
        public RelayCommand Save { get; set; }
        #endregion
    }
}
