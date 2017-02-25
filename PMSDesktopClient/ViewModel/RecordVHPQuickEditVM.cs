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
using System.Windows.Input;

namespace PMSDesktopClient.ViewModel
{
    public class RecordVHPQuickEditVM : ViewModelBase
    {
        public RecordVHPQuickEditVM()
        {
            InitializeProperties();
            InitializeCommmands();

            LoadRecordVHP();
            LoadRecordVHPItemsByRecordVHP(RecordVHPs.FirstOrDefault());
            EmptyCurrentRecordVHPItem();
        }
        private bool isNew;

        private void InitializeProperties()
        {
            isNew = true;
            RecordVHPs = new ObservableCollection<PMSMainService.DcRecordVHP>();
            CurrentRecordVHPItem = new PMSMainService.DcRecordVHPItem();
            RecordVHPItems = new ObservableCollection<DcRecordVHPItem>();
        }

        private void InitializeCommmands()
        {
            GiveUp = new RelayCommand(() =>
            {
                NavigationService.GoTo(new MsgObject() { MsgToken = VToken.RecordVHP });
                Messenger.Default.Send<MsgObject>(new MsgObject() { MsgToken = VToken.RecordVHPRefresh });
            });

            Refresh = new RelayCommand(() => LoadRecordVHP());
            CopyFill = new RelayCommand<DcRecordVHPItem>(ActionCopyFill);
            Save = new RelayCommand(ActionSave);

            SelectionChanged = new RelayCommand<DcRecordVHP>(obj => { LoadRecordVHPItemsByRecordVHP(obj); });

            EditItem = new RelayCommand<PMSMainService.DcRecordVHPItem>(ActionEditItem);
            New = new RelayCommand(ActionNew);

            Chart = new RelayCommand(ActionChart);
        }

        private void ActionChart()
        {

        }

        private void ActionNew()
        {
            EmptyCurrentRecordVHPItem();
            isNew = true;
        }

        private void ActionEditItem(DcRecordVHPItem obj)
        {
            if (obj != null)
            {
                CurrentRecordVHPItem = obj;
                isNew = false;
            }
        }

        public void LoadRecordVHPItemsByRecordVHP(DcRecordVHP model)
        {
            if (model != null)
            {
                CurrentRecordVHP = model;
                using (var service = new RecordVHPServiceClient())
                {
                    //这里使用异步操作
                    var task = service.GetRecordVHPItemsByRecrodVHPIDAsync(model.ID);
                    RecordVHPItems.Clear();
                    var result = task.Result.ToList();
                    result.ToList().ForEach(i => RecordVHPItems.Add(i));
                }
            }
        }


        private void EmptyCurrentRecordVHPItem()
        {
            if (CurrentRecordVHP != null)
            {
                var model = new DcRecordVHPItem();
                model.ID = Guid.NewGuid();
                model.RecordVHPID = CurrentRecordVHP.ID;
                model.CurrentTime = DateTime.Now;
                model.Creator = (App.Current as App).CurrentUser.UserName;
                model.State = PMSCommon.CommonState.Show.ToString();
                model.PV1 = 0;
                model.PV2 = 0;
                model.PV3 = 0;
                model.SV = 0;
                model.Ton = 0;
                model.Vaccum = 1E-3;
                model.Shift1 = 0;
                model.Shift2 = 0;
                model.Omega = 0;
                model.WaterTemperatureIn = 0;
                model.WaterTemperatureOut = 0;
                model.ExtraInformation = "";
                isNew = true;
                CurrentRecordVHPItem = model;

            }
        }

        private void LoadRecordVHP()
        {
            using (var service = new RecordVHPServiceClient())
            {
                var result = service.GetTopRecordVHP(5).ToList();
                RecordVHPs.Clear();
                result.ToList().ForEach(r => RecordVHPs.Add(r));

                CurrentDataGridSelectIndex = 0;
                CurrentRecordVHP = RecordVHPs.FirstOrDefault();
            }
        }


        private void ActionSave()
        {
            if (CurrentRecordVHP == null)
            {
                return;
            }
            //CurrentRecordVHPItem.RecordVHPID = CurrentRecordVHP.ID;
            try
            {
                if (CurrentRecordVHPItem != null)
                {
                    using (var service = new RecordVHPServiceClient())
                    {
                        if (isNew)
                        {
                            service.AddRecordVHPItem(CurrentRecordVHPItem);
                        }
                        else
                        {
                            service.UpdateReocrdVHPItem(CurrentRecordVHPItem);
                        }

                        ReLocateRecordVHPAndRefreshItems();
                        EmptyCurrentRecordVHPItem();
                        NavigationService.ShowStateMessage("保存完毕");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void ActionCopyFill(DcRecordVHPItem obj)
        {
            if (obj != null)
            {
                //using (var service = new RecordVHPServiceClient())
                //{
                //    service.AddRecordVHPItem(obj);
                //    ReLocateRecordVHPAndRefreshItems();
                //}
                var model = new DcRecordVHPItem();

                model.RecordVHPID = obj.RecordVHPID;

                model.ID = Guid.NewGuid();
                model.CurrentTime = DateTime.Now;
                model.Creator = (App.Current as App).CurrentUser.UserName;
                model.PV1 = obj.PV1;
                model.PV2 = obj.PV2;
                model.PV3 = obj.PV3;
                model.SV = obj.SV;

                model.Ton = obj.Ton;
                model.Vaccum = obj.Vaccum;
                model.Shift1 = obj.Shift1;
                model.Shift2 = obj.Shift2;
                model.Omega = obj.Omega;
                model.WaterTemperatureIn = obj.WaterTemperatureIn;
                model.WaterTemperatureOut = obj.WaterTemperatureOut;
                model.ExtraInformation = obj.ExtraInformation;

                isNew = true;
                CurrentRecordVHPItem = model;
                NavigationService.ShowStateMessage("填充选定项完毕");
            }
        }

        /// <summary>
        /// 重新定位RecordVHP并且刷新对应的Items
        /// </summary>
        private void ReLocateRecordVHPAndRefreshItems()
        {
            if (CurrentDataGridSelectIndex < RecordVHPs.Count() && CurrentDataGridSelectIndex >= 0)
            {
                LoadRecordVHPItemsByRecordVHP(RecordVHPs[CurrentDataGridSelectIndex]);
            }
            else
            {
                LoadRecordVHPItemsByRecordVHP(RecordVHPs.FirstOrDefault());
            }

            System.Diagnostics.Debug.Print(CurrentDataGridSelectIndex.ToString());
        }

        #region Properties
        public ObservableCollection<DcRecordVHP> RecordVHPs { get; set; }
        public ObservableCollection<DcRecordVHPItem> RecordVHPItems { get; set; }
        public DcRecordVHP CurrentRecordVHP { get; set; }

        private DcRecordVHPItem currentRecordVHPItem;

        public DcRecordVHPItem CurrentRecordVHPItem
        {
            get { return currentRecordVHPItem; }
            set
            {
                currentRecordVHPItem = value;
                RaisePropertyChanged(nameof(CurrentRecordVHPItem));
            }
        }

        public int CurrentDataGridSelectIndex { get; set; }
        #endregion

        #region Commands
        public RelayCommand Refresh { get; set; }
        public RelayCommand GiveUp { get; set; }
        public RelayCommand Save { get; set; }
        public RelayCommand<DcRecordVHP> SelectionChanged { get; set; }
        public RelayCommand New { get; set; }
        public RelayCommand<DcRecordVHPItem> EditItem { get; set; }
        public RelayCommand<DcRecordVHPItem> CopyFill { get; set; }

        public RelayCommand Chart { get; set; }
        #endregion
    }
}
