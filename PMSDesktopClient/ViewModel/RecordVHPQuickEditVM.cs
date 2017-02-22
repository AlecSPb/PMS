using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSDesktopClient.PMSMainService;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;

namespace PMSDesktopClient.ViewModel
{
    public class RecordVHPQuickEditVM : ViewModelBase
    {
        public RecordVHPQuickEditVM()
        {

            QuickRecordVHPs = new ObservableCollection<PMSMainService.DcRecordVHP>();
            CurrentRecordVHPItem = new PMSMainService.DcRecordVHPItem();

            EmptyCurrentRecordVHPItem();

            GiveUp = new RelayCommand(() =>
            {
                NavigationService.GoTo(new MsgObject() { MsgToken = VToken.RecordVHP });
                Messenger.Default.Send<MsgObject>(new MsgObject() { MsgToken = VToken.RecordVHPRefresh });
            });

            Refresh = new RelayCommand(ActionRefresh);
            Duplicate = new RelayCommand<PMSMainService.DcRecordVHPItem>(ActionDuplicate);
            Save = new RelayCommand(ActionSave);

            LoadData();
        }

        private void EmptyCurrentRecordVHPItem()
        {
            CurrentRecordVHPItem.ID = Guid.NewGuid();
            CurrentRecordVHPItem.CurrentTime = DateTime.Now;
            CurrentRecordVHPItem.Creator = (App.Current as App).CurrentUser.UserName;
            CurrentRecordVHPItem.State = PMSCommon.CommonState.Show.ToString();
            CurrentRecordVHPItem.PV1 = 0;
            CurrentRecordVHPItem.PV2 = 0;
            CurrentRecordVHPItem.PV3 = 0;
            CurrentRecordVHPItem.SV = 0;
            CurrentRecordVHPItem.Ton = 0;
            CurrentRecordVHPItem.Vaccum = 1E-3;
            CurrentRecordVHPItem.Shift1 = 0;
            CurrentRecordVHPItem.Shift2 = 0;
            CurrentRecordVHPItem.Omega = 0;
            CurrentRecordVHPItem.WaterTemperatureIn = 0;
            CurrentRecordVHPItem.WaterTemperatureOut = 0;
            CurrentRecordVHPItem.ExtraInformation = "";
        }

        private void LoadData()
        {
            using (var service = new RecordVHPServiceClient())
            {
                var result = service.GetTopRecordVHP(5).ToList();
                QuickRecordVHPs.Clear();
                result.ToList().ForEach(r => QuickRecordVHPs.Add(r));

            }
        }

        private void ActionSave()
        {
            if (CurrentRecordVHP == null)
            {
                return;
            }

            CurrentRecordVHPItem.RecordVHPID = CurrentRecordVHP.ID;

            if (CurrentRecordVHPItem != null)
            {
                using (var service = new RecordVHPServiceClient())
                {
                    service.AddRecordVHPItem(CurrentRecordVHPItem);
                }
                LoadData();
                EmptyCurrentRecordVHPItem();
            }
        }

        private void ActionDuplicate(DcRecordVHPItem obj)
        {
            if (obj != null)
            {
                obj.ID = Guid.NewGuid();
                obj.CurrentTime = DateTime.Now;
                obj.Creator = (App.Current as App).CurrentUser.UserName;
                using (var service=new RecordVHPServiceClient())
                {
                    service.AddRecordVHPItem(obj);
                    LoadData();
                }
            }
        }

        private void ActionRefresh()
        {
            LoadData();
        }

        public ObservableCollection<DcRecordVHP> QuickRecordVHPs { get; set; }

        public DcRecordVHP CurrentRecordVHP { get; set; }
        public DcRecordVHPItem CurrentRecordVHPItem { get; set; }

        public RelayCommand Refresh { get; set; }
        public RelayCommand GiveUp { get; set; }
        public RelayCommand Save { get; set; }

        public RelayCommand<DcRecordVHPItem> Duplicate { get; set; }
    }
}
